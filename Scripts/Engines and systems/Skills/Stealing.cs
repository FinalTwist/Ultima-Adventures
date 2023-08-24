using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using Server.Spells.Necromancy;
using Server.Spells;
using Server.Spells.Ninjitsu;
using Server.Misc;
using System.Reflection;
using System.Text;
using Server.Regions;
using Felladrin.Automations;

namespace Server.SkillHandlers
{
	public class Stealing
	{
		
		static List<Mobile> stealingtargets = new List<Mobile>();
		
		public static void Initialize()
		{
			SkillInfo.Table[33].Callback = new SkillUseCallback( OnUse );
		}

		public static readonly bool ClassicMode = false;
		public static readonly bool SuspendOnMurder = false;

		public static bool IsInGuild( Mobile m )
		{
			return ( m is PlayerMobile && ((PlayerMobile)m).NpcGuild == NpcGuild.ThievesGuild );
		}

		public static bool IsInnocentTo( Mobile from, Mobile to )
		{
			return ( Notoriety.Compute( from, (Mobile)to ) == Notoriety.Innocent );
		}

		private class StealingTarget : Target
		{
			private Mobile m_Thief;

			public StealingTarget( Mobile thief ) : base ( 1, false, TargetFlags.None )
			{
				m_Thief = thief;

				AllowNonlocal = true;
			}

			private Item TryStealItem( Item toSteal, ref bool caught, Mobile target )
			{
				Item stolen = null;

				object root = toSteal.RootParent;

				bool isstealbase = false;
				if ( toSteal is AddonComponent || toSteal is StealBase || (toSteal is StealBox) || (root is StealBase) || (root is StealBox) || toSteal.Parent is StealBase || toSteal.Parent is StealBox)
					isstealbase = true;

				StealableArtifactsSpawner.StealableInstance si = null;
				if ( (toSteal.Parent == null || !toSteal.Movable) && !isstealbase)
					si = StealableArtifactsSpawner.GetStealableInstance( toSteal );

				/// WIZARD WANTS THEM TO BE ABLE TO STEAL THE DUNGEON CHESTS ///
				if ( toSteal is DungeonChest )
				{
					DungeonChest dBox = (DungeonChest)toSteal;

					if ( m_Thief.Blessed )
					{
						m_Thief.SendMessage( "You cannot steal while in this state." );
					}
					else if ( dBox.ItemID == 0x3582 || dBox.ItemID == 0x3583 || dBox.ItemID == 0x35AD || dBox.ItemID == 0x3868 || ( dBox.ItemID >= 0x4B5A && dBox.ItemID <= 0x4BAB ) || ( dBox.ItemID >= 0xECA && dBox.ItemID <= 0xED2 ) )
					{
						m_Thief.SendMessage( "It is best to leave the dead be." );
					}
					else if ( dBox.ItemID == 0x3564 || dBox.ItemID == 0x3565 )
					{
						m_Thief.SendMessage( "You have not use for this broken golem thing." );
					}
					else
					{
						if ( m_Thief.CheckSkill( SkillName.Stealing, 0, 125 ) )
						{
							m_Thief.SendMessage( "You dump out the entire contents while stealing the item." );
							StolenChest sBox = new StolenChest();
							int dValue = 0;

							dValue = (dBox.ContainerLevel + 1) * 50;
							sBox.ContainerID = dBox.ContainerID;
							sBox.ContainerGump = dBox.ContainerGump;
							sBox.ContainerHue = dBox.ContainerHue;
							sBox.ContainerFlip = dBox.ContainerFlip;
							sBox.ContainerWeight = dBox.ContainerWeight;
							sBox.ContainerName = dBox.ContainerName;

							sBox.ContainerValue = dValue;

							Item iBox = (Item)sBox;

							iBox.ItemID = sBox.ContainerID;
							iBox.Hue = sBox.ContainerHue;
							iBox.Weight = sBox.ContainerWeight;
							iBox.Name = sBox.ContainerName;

							Bag oBox = (Bag)iBox;

							oBox.GumpID = sBox.ContainerGump;

							m_Thief.AddToBackpack( oBox );

							Titles.AwardFame( m_Thief, dValue, true );

							LoggingFunctions.LogStandard( m_Thief, "has stolen a " + iBox.Name + "" );
						}
						else
						{
							m_Thief.SendMessage( "You were not quick enough to steal it." );
							m_Thief.RevealingAction(); // REVEALING ONLY WHEN FAILED
						}

						Item spawnBox = new DungeonChestSpawner( dBox.ContainerLevel, (double)(Utility.RandomMinMax( 45, 105 )) );
						spawnBox.MoveToWorld (new Point3D(dBox.X, dBox.Y, dBox.Z), dBox.Map);

						toSteal.Delete();
					}
				}
				else if ( toSteal is LandChest )
				{
					m_Thief.SendMessage( "It is best to leave the dead be." );
				}
				else if (m_Thief.Blessed)
				{
					m_Thief.SendMessage( "You are unable to do this in this state." );
				}
				else if ( toSteal is SunkenShip )
				{
					m_Thief.SendMessage( "You are just not that strong." );
				}
				else if ( !IsEmptyHanded( m_Thief ) )
				{
					m_Thief.SendMessage( "You cannot be wielding a weapon when trying to steal something." );
				}
				else if ( root is Mobile && ((Mobile)root).Player && IsInnocentTo( m_Thief, (Mobile)root ) && !IsInGuild( m_Thief ) )
				{
					m_Thief.SendLocalizedMessage( 1005596 ); // You must be in the thieves guild to steal from other players.
				}
				else if ( toSteal is Coffer )
				{

					if ( m_Thief is PlayerMobile && ((PlayerMobile)m_Thief).BalanceStatus > 0 )
						m_Thief.SendMessage( "That wouldn't be right.");

					Coffer coffer = (Coffer)toSteal;
					bool Pilfer = true;

					if ( m_Thief.Backpack.FindItemByType( typeof ( ThiefNote ) ) != null )
					{
						Item mail = m_Thief.Backpack.FindItemByType( typeof ( ThiefNote ) );
						ThiefNote envelope = (ThiefNote)mail;

						if ( envelope.NoteOwner == m_Thief )
						{
							if ( envelope.NoteItemArea == Server.Misc.Worlds.GetRegionName( m_Thief.Map, m_Thief.Location ) && envelope.NoteItemGot == 0 && envelope.NoteItemCategory == coffer.CofferType )
							{
								envelope.NoteItemGot = 1;
								m_Thief.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found " + envelope.NoteItem + ".");
								m_Thief.SendSound( 0x3D );
								envelope.InvalidateProperties();
								Pilfer = false;
							}
						}
						else
							m_Thief.SendMessage( "You steal the item mentioned in the note, but after realizing the quest was not given to you, you throw the item away." );
					}

					if ( Pilfer )
					{
						if ( coffer.CofferGold < 1 )
						{
							m_Thief.SendMessage( "There seems to be no gold in the coffer." );
						}
						else if ( m_Thief.CheckSkill( SkillName.Stealing, (Utility.RandomMinMax(0, 60)), 100 ) )
						{
							m_Thief.SendMessage( "You slip out " + coffer.CofferGold + " gold from the coffer." );
							m_Thief.SendSound( 0x2E6 );
							m_Thief.AddToBackpack ( new Gold( coffer.CofferGold ) );

							Titles.AwardFame( m_Thief, (int)( (double)coffer.CofferGold / 50 ), true );
							Titles.AwardKarma( m_Thief, -( (int)((double)coffer.CofferGold / 25 )), true );

							coffer.CofferRobbed = 1;
							coffer.CofferRobber = m_Thief.Name + " the " + Server.Misc.GetPlayerInfo.GetSkillTitle( m_Thief );
							coffer.CofferGold = 0;

							LoggingFunctions.LogStandard( m_Thief, "has stolen " + coffer.CofferGold + " gold from a " + coffer.CofferType + " in " + Server.Misc.Worlds.GetRegionName( m_Thief.Map, m_Thief.Location ) + "" );
						}
						else
						{
							m_Thief.SendMessage( "You fingers slip, causing you to get noticed!" );
							m_Thief.RevealingAction(); // REVEALING ONLY WHEN FAILED +++
							m_Thief.CriminalAction( true );

							if ( !m_Thief.CheckSkill( SkillName.Snooping, (Utility.RandomMinMax(0, 70)), 125 ) )
							{
								List<Mobile> spotters = new List<Mobile>();
								foreach ( Mobile m in m_Thief.GetMobilesInRange( 15 ) )
								{
									//if ( m is BaseBlue && m.CanSee( m_Thief ) && m.InLOS( m_Thief ) )
									if ( m is BaseBlue || m is BaseVendor )
									{
										m_Thief.CriminalAction( false );
										((BaseCreature)m).FocusMob = m_Thief;
										m.Combatant = m_Thief;
										m.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Stop! Thief!" ) ); 
										//((PlayerMobile)m_Thief).flagged = true;
									}

								}
							}
						}
					}
				}
				else if ( isstealbase )
				{
					if (!(toSteal is AddonComponent ) )
						return null;

					BaseAddon pedestal = (BaseAddon)((AddonComponent)toSteal).Addon;

					if (pedestal == null || !(pedestal is StealBase))
						return null;

					if ( m_Thief.Backpack.FindItemByType( typeof ( ThiefNote ) ) != null && m_Thief.CheckSkill( SkillName.Stealing, Utility.RandomMinMax(0, 60), 100 ) )
					{
						Item mail = m_Thief.Backpack.FindItemByType( typeof ( ThiefNote ) );
						ThiefNote envelope = (ThiefNote)mail;

						if ( envelope.NoteOwner == m_Thief )
						{
							if ( envelope.NoteItemArea == Server.Misc.Worlds.GetRegionName( m_Thief.Map, m_Thief.Location ) && envelope.NoteItemGot == 0 )
							{
								envelope.NoteItemGot = 1;
								m_Thief.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found " + envelope.NoteItem + ".");
								m_Thief.SendSound( 0x3D );
								m_Thief.CloseGump( typeof( Server.Items.ThiefNote.NoteGump ) );
								envelope.InvalidateProperties();
							}
						}
					}
					else if ( m_Thief.Skills[SkillName.Stealing].Value < 85 )
						((StealBase)pedestal).DoDamage( m_Thief );
					else if ( m_Thief.CheckSkill( SkillName.Stealing, Utility.RandomMinMax(85, 125 ) ) )
					{
						double difficulty = (double)Misc.MyServerSettings.GetDifficultyLevel( m_Thief.Location, m_Thief.Map );

						if (difficulty == 0 ) // divide by zero check
							difficulty = 1;

						double chance = 1 / difficulty; //difficulty is 0-5, so harder dungeons will be harder to steal from
						if (Utility.RandomDouble() < chance)
							((StealBase)pedestal).SuccessGet( m_Thief );
						else
							((StealBase)pedestal).DoDamage( m_Thief );
					}
					else 
						((StealBase)pedestal).DoDamage( m_Thief );

				}
				else if ( root is BaseVendor && ((BaseVendor)root).IsInvulnerable )
				{
					m_Thief.SendLocalizedMessage( 1005598 ); // You can't steal from shopkeepers.
				}
				else if ( root is PlayerVendor || root is PlayerBarkeeper )
				{
					m_Thief.SendLocalizedMessage( 502709 ); // You can't steal from vendors.
				}
				else if ( !m_Thief.CanSee( toSteal ) && root != null )
				{
					m_Thief.SendLocalizedMessage( 500237 ); // Target can not be seen.
				}
				else if ( m_Thief.Backpack == null || !m_Thief.Backpack.CheckHold( m_Thief, toSteal, false, true ) )
				{
					m_Thief.SendLocalizedMessage( 1048147 ); // Your backpack can't hold anything else.
				}
				else if ( si == null && ( toSteal.Parent == null && !toSteal.Movable ) && !isstealbase )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
					m_Thief.SendMessage("1");
				}
				else if ( (toSteal.LootType == LootType.Newbied || toSteal.CheckBlessed( root ))  )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
					m_Thief.SendMessage("2");
				}
				else if ( Core.AOS && si == null && toSteal is Container && !isstealbase )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
					m_Thief.SendMessage("3");
				}
				else if ( !m_Thief.InRange( toSteal.GetWorldLocation(), 1 ) && toSteal.Parent != null )
				{
					m_Thief.SendLocalizedMessage( 502703 ); // You must be standing next to an item to steal it.
				}
				else if ( si != null && m_Thief.Skills[SkillName.Stealing].Value < 100.0 )
				{
					m_Thief.SendLocalizedMessage( 1060025, "", 0x66D ); // You're not skilled enough to attempt the theft of this item.
				}
				else if ( toSteal.Parent is Mobile )
				{
					m_Thief.SendLocalizedMessage( 1005585 ); // You cannot steal items which are equipped.
				}
				else if ( root == m_Thief )
				{
					m_Thief.SendLocalizedMessage( 502704 ); // You catch yourself red-handed.
				}
				else if ( root is Mobile && ((Mobile)root).AccessLevel > AccessLevel.Player )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}
				else if ( root is Mobile && !m_Thief.CanBeHarmful( (Mobile)root ) )
				{
				}
				else if ( root is Corpse )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}
				else
				{
					double w = toSteal.Weight + toSteal.TotalWeight;
					double w2 = 0;
					if (m_Thief is PlayerMobile)
						w2 = (w / 30) * (1-((PlayerMobile)m_Thief).Agility());

					if (m_Thief is BaseCreature)
						w2 = (w / 30) * (1-((BaseCreature)m_Thief).Agility());

					if (isstealbase)
					{

					}
					if ( w > 25 )
					{
						m_Thief.SendMessage( "That is too heavy to steal." );
					}
					else if (AdventuresFunctions.IsPuritain((object)m_Thief) && (m_Thief is PlayerMobile || m_Thief is BaseCreature) && Utility.RandomDouble() < w2 )
					{
						if (m_Thief is PlayerMobile)
							m_Thief.SendMessage( "You fumble the attempt." );

						caught = true;
					}
					else
					{
						if ( toSteal.Stackable && toSteal.Amount > 1 )
						{
							int maxAmount = (int)((m_Thief.Skills[SkillName.Stealing].Value / 10.0) / toSteal.Weight);

							if ( maxAmount < 1 )
								maxAmount = 1;
							else if ( maxAmount > toSteal.Amount )
								maxAmount = toSteal.Amount;

							int amount = Utility.RandomMinMax( 1, maxAmount );

							if ( amount >= toSteal.Amount )
							{
								int pileWeight = (int)Math.Ceiling( toSteal.Weight * toSteal.Amount );
								pileWeight *= 10;

								if ( m_Thief.CheckTargetSkill( SkillName.Stealing, toSteal, pileWeight - 22.5, pileWeight + 27.5 ) )
									stolen = toSteal;
							}
							else
							{
								int pileWeight = (int)Math.Ceiling( toSteal.Weight * amount );
								pileWeight *= 10;

								if ( m_Thief.CheckTargetSkill( SkillName.Stealing, toSteal, pileWeight - 22.5, pileWeight + 27.5 ) )
								{
									stolen = Mobile.LiftItemDupe( toSteal, toSteal.Amount - amount );

									if ( stolen == null )
										stolen = toSteal;
								}
							}
						}
						else
						{
							int iw = (int)Math.Ceiling( w );
							iw *= 10;

							if ( m_Thief.CheckTargetSkill( SkillName.Stealing, toSteal, iw - 22.5, iw + 27.5 ) )
								stolen = toSteal;
						}

						if ( stolen != null )
						{
							m_Thief.SendLocalizedMessage( 502724 ); // You successfully steal the item.

							if (target != null )
							{

								if (target.Karma > 0)
									Titles.AwardKarma( m_Thief, -(int)(target.Karma/150), true );
								else 
									Titles.AwardKarma( m_Thief, (int)(target.Karma/150), true );
							}
							else 
								Titles.AwardKarma( m_Thief, -50, true );
							
							if ( si != null )
							{
								toSteal.Movable = true;
								si.Item = null;
							}
						}
						else
						{
							m_Thief.SendLocalizedMessage( 502723 ); // You fail to steal the item.
							m_Thief.RevealingAction(); // REVEALING ONLY WHEN FAILED
						}

						caught = ( m_Thief.Skills[SkillName.Stealing].Value < Utility.Random( 150 ) );
					}
				}

				if (AdventuresFunctions.IsPuritain((object)m_Thief) && caught && m_Thief is PlayerMobile)
				{
					if (root is BaseCreature && ((BaseCreature)root).midrace > 0 )
						((PlayerMobile)m_Thief).AdjustReputation( Utility.RandomMinMax(500, 2500), ((BaseCreature)root).midrace, false);
				}

				return stolen;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				//from.RevealingAction(); // NO REVEALING ON THIS SERVER

				Item stolen = null;
				object root = null;
				bool caught = false;

				if ( target is Item )
				{

					root = ((Item)target).RootParent;

					if (root is PlayerMobile)
						((PlayerMobile)from).flagged = true;

					stolen = TryStealItem( (Item)target, ref caught, null );
				} 
				else if ( target is Mobile )
				{

					Mobile mobs = target as Mobile;
					if (mobs.Region.IsPartOf( typeof( PublicRegion ) ))
						return;

					if ( from.Region.IsPartOf( typeof( HouseRegion ) ) && !mobs.Region.IsPartOf( typeof( HouseRegion ) ))
					{
						from.SendMessage( "Exploiting game mechanics is forbidden!");
						if (from.Hits > 10)
							from.Hits -= from.Hits -5;
						else
							from.Kill();
					}

					if (target is PlayerMobile)
					{
						((PlayerMobile)from).flagged = true;

						if (  from is PlayerMobile && ((PlayerMobile)from).BalanceStatus > 0 && ((Mobile)target).Karma >= 0 )
							from.SendMessage( "That wouldn't be right for someone pledged to do good.");
					}

					Container pack = ((Mobile)target).Backpack;
					
					double odds = (from.Skills[SkillName.Stealing].Value / 150) * (1- ((Math.Abs(((Mobile)target).Fame) / 30000)) ) * (1+ (from.RawDex / 150));
					
					if (odds >1)
						odds = 0.95;
					if (odds < 0.01)
						odds = 0.01;
					
					if ( pack != null && pack.Items.Count > 0 ) 
					{
							int randomIndex = Utility.Random( pack.Items.Count );

							root = target;

							if (target is PlayerMobile)
								((PlayerMobile)from).flagged = true;
								
							stolen = TryStealItem( pack.Items[randomIndex], ref caught, null );
					
					}
					else if (target is BaseCreature && Utility.RandomDouble() < odds && !(((Mobile)target).Blessed) && ((Mobile)target).Fame != 0 && !(target is CloneCharacterOnLogout.CharacterClone) ) // player targetting a basecreature Final making it more fun to steal
					{

						if (  ((PlayerMobile)from).BalanceStatus > 0 && ((Mobile)target).Karma > 20 )
							from.SendMessage( "That wouldn't be right for someone pledged to do good.");

						if (Stealing.CheckStealingTarget( (Mobile)target, false )) // check only
						{

									Item rngitem = null;
									int reward = ((Mobile)target).Fame; 
									
									if (Math.Abs(((Mobile)target).Fame) <=1000)
										reward = Utility.RandomMinMax(0, 250); // 100% easy
									else if (Math.Abs(((Mobile)target).Fame) <=5000)
										reward = Utility.RandomMinMax(100, 350); // 60% easy, 40% medium
									else if (Math.Abs(((Mobile)target).Fame) <=10000)
										reward = Utility.RandomMinMax(200, 400); // 25% easy, 62.5% medium, 12.5% rare
									else if (Math.Abs(((Mobile)target).Fame) <=18000)
										reward = Utility.RandomMinMax(300, 400); // 37.5 medium, 47% rare, 15% impossible
									else if (Math.Abs(((Mobile)target).Fame) <= 26000)
										reward = Utility.RandomMinMax(400, 500); // 70% rare, 30% impossible
									else if (Math.Abs(((Mobile)target).Fame) > 26001)
										reward = Utility.RandomMinMax(450, 500); // 40% rare, 60% impossible

									if (AdventuresFunctions.IsPuritain((object)target) && reward > 150)
										reward -= Utility.RandomMinMax(50, 149);
									
									double luckavg = 0;
									
									if (reward <= 250)// easy finds
									{
										switch ( Utility.Random( 24 ) ) // 
										{
												case 0: rngitem = Loot.RandomArmorOrHatOrClothes(); break;
												case 1: rngitem = Loot.RandomNecromancyReagent(); rngitem.Amount = Utility.RandomMinMax(1, 15); break;
												case 2: rngitem = Loot.RandomReagent(); rngitem.Amount = Utility.RandomMinMax(1, 15); break;
												case 3: rngitem = Loot.RandomClothing(); break;
												case 4: rngitem = Loot.RandomGem(); rngitem.Amount = Utility.RandomMinMax(1, 15); break;
												case 5: rngitem = Loot.RandomPotion(); break;
												case 6: rngitem = new Arrow( Utility.RandomMinMax(1, 15) ); break;
												case 7: rngitem = new Arrow( Utility.RandomMinMax(1, 15) ); break;
												case 8: rngitem = new Gold(); rngitem.Amount = (int)((double)reward * Utility.RandomDouble()); break;
												case 9: rngitem = Loot.RandomWand(); break;
												case 10: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(1, 2) ); break;
												case 11: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(1, 2) ); break;
												case 12: rngitem = Loot.RandomInstrument(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(1, 2) ); break;
										} 
										
									}

									else if (reward <=375) // medium finds
									{
										luckavg = (from.Luck + 400) /2;
										
										switch ( Utility.Random( 18 ) ) // 
										{
												case 0: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(2, 4) ); break;
												case 1: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(2, 4) ); break;
												case 2: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(2, 4) ); break;
												case 3: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(2, 4) ); break;
												case 4: rngitem = Loot.RandomInstrument(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 4); break;
												case 5: rngitem = Loot.RandomQuiver(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 4); break;
												case 6: rngitem = Loot.RandomWand(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 4); break;
												case 7: rngitem = Loot.RandomJewelry(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 4); break;
												case 8: rngitem = new Gold(); rngitem.Amount = reward; break;
												case 9: rngitem = Loot.RandomMixerReagent(); rngitem.Amount = Utility.RandomMinMax(1, 20); break;
										}
											
									}

									else if (reward <= 470) // rare finds
									{
										luckavg = (from.Luck + 800) /2;
										
										switch ( Utility.Random( 20 ) ) // 7.5%
										{
												case 0: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(4, 8)); break;
												case 1: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(4, 8)); break;
												case 2: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(4, 8)); break;
												case 3: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(4, 8)); break;
												case 4: rngitem = Loot.RandomInstrument(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 9); break;
												case 5: rngitem = Loot.RandomQuiver(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 9); break;
												case 6: rngitem = Loot.RandomWand(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 9); break;
												case 7: rngitem = Loot.RandomJewelry(); Stealing.ItemMutate( from, (int)luckavg, rngitem, 9); break;
												case 8: rngitem = new Gold(); rngitem.Amount = (int)((double)reward * (1+Utility.RandomDouble())); break;
												case 9: rngitem = Loot.RandomSecretReagent(); rngitem.Amount = Utility.RandomMinMax(1, 50); break;
												case 10: rngitem = Loot.RandomRelic(); break;
										}
									}
									

									else if (reward <= 500)  // impossible finds
									{
										luckavg = (from.Luck + 1200) /2;
										
										switch ( Utility.Random( 10 ) ) //
										{					
												case 0: rngitem = Loot.RandomArty(); break;
												case 1: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(8, 15)); break;
												case 2: rngitem = Loot.RandomInstrument(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(8, 15)); break;
												case 3: rngitem = Loot.RandomQuiver(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(8, 15)); break;
												case 4: rngitem = Loot.RandomWand(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(8, 15)); break;
												case 5: rngitem = Loot.RandomJewelry(); Stealing.ItemMutate( from, (int)luckavg, rngitem, Utility.RandomMinMax(8, 15)); break;
										}
									}
									
									
									if (rngitem != null)
									{
										rngitem.Movable = true;
										stolen = TryStealItem( rngitem, ref caught, (Mobile)target );
										if (stolen != null)
										{
											if (rngitem is Gold && AdventuresFunctions.IsPuritain((object)from))
											{
												if (target is BaseCreature && ((BaseCreature)target).midrace != 0)
												{
													BaseCreature ft = target as BaseCreature;
													Gold gd = rngitem as Gold;
													Item csh = null;

													if (ft.midrace == 1)
														 csh = new Sovereign( gd.Amount/10 );
													if (ft.midrace == 2)
														csh = new Drachma( gd.Amount/10 );
													if (ft.midrace == 3)
														csh = new Sslit( gd.Amount/10 ) ;
													if (ft.midrace == 4)
														csh = new Dubloon( gd.Amount/10 ) ;
													if (ft.midrace == 5)
														csh = new Skaal( gd.Amount/10 ) ;
													
													from.AddToBackpack(csh);
													stolen = csh;
													rngitem.Delete();
													
												}
												else 
													((Gold)rngitem).Amount /= 50;
											}
											if (rngitem != null)
												from.AddToBackpack( rngitem );
											
											StolenItem.Add( stolen, m_Thief, root as Mobile );
										
											Titles.AwardFame( from, (int)(Math.Abs(((Mobile)target).Fame)/150) , true );
											
											if (stolen.Name != null)
												from.SendMessage( "You got " + stolen.Name + " !");
											else	
												from.SendMessage( "You got something !");
											stolen = null;
											
											LevelItemManager.CheckItems( from, (Mobile)target, true );
										}
											
										else if (from.AccessLevel == AccessLevel.Player) // for testing by gm's
										{
											Stealing.CheckStealingTarget( (Mobile)target, true); // user got caught?
											rngitem.Delete();
											stolen = null;
										}
									}
									else
										from.SendMessage( "Your fingers aren't quick enough.");
						
						
						}
						else 
							from.SendMessage( "This entity is guarding its posessions closely... ");
					}
					else 
						from.SendMessage( "You can't find anything to take from this target.");

				} 
				else 
					from.SendMessage( "You don't think you could steal from that.");
				
				if ( stolen != null )
				{
		
					if (stolen.Name != null)
						from.SendMessage( "You got " + stolen.Name + " !");
					else	
						from.SendMessage( "You got something !");
					
					Container pack = from.Backpack;
					pack.DropItem(stolen);
					StolenItem.Add( stolen, m_Thief, root as Mobile );
				}

				if ( caught )
				{
					if ( root == null )
					{
						m_Thief.RevealingAction(); 
						m_Thief.CriminalAction( false );
					}
					else if ( root is Corpse && ((Corpse)root).IsCriminalAction( m_Thief ) )
					{
						m_Thief.RevealingAction(); // REVEALING ONLY WHEN FAILED +++
						m_Thief.CriminalAction( false );
					}
					else if ( root is Mobile )
					{
						Mobile mobRoot = (Mobile)root;

						bool isindarkmoor = false;

						if ( m_Thief.Map == Map.Ilshenar && m_Thief.X <= 1007 && m_Thief.Y <= 1280 )
							isindarkmoor = true;

						if ( 	mobRoot is PlayerMobile || 
								mobRoot is BaseVendor || 
								( mobRoot is BaseBlue && ((Mobile)m_Thief).Karma > 0 ) || 
								( isindarkmoor && ( mobRoot is Praetor || mobRoot is Honorae ) && ((Mobile)m_Thief).Karma < 0 ) ||
								( !isindarkmoor && ((Mobile)m_Thief).Karma < 0 && ((Mobile)root).Karma > 0) )
						{
							m_Thief.CriminalAction( true );
						}
						
						if (mobRoot is BaseCreature )
						{
							((BaseCreature)mobRoot).DoHarmful( m_Thief );
							if (mobRoot.Combatant == null)
								mobRoot.Combatant = m_Thief;
						}
						if (AdventuresFunctions.IsPuritain((object)mobRoot) && mobRoot is BaseCreature && ((BaseCreature)mobRoot).midrace != 0 && m_Thief is PlayerMobile)
							((PlayerMobile)m_Thief).AdjustReputation(Utility.RandomMinMax(20,100), ((BaseCreature)mobRoot).midrace, false);

						string message = String.Format( "You notice {0} trying to steal from {1}.", m_Thief.Name, mobRoot.Name );
						m_Thief.RevealingAction(); // REVEALING ONLY WHEN NOTICED
						Server.Items.DisguiseTimers.RemoveDisguise( m_Thief );
						foreach ( NetState ns in m_Thief.GetClientsInRange( 8 ) )
						{
							if ( ns.Mobile != m_Thief )
								ns.Mobile.SendMessage( message );
						}
					}
				}
				else if ( root is Corpse && ((Corpse)root).IsCriminalAction( m_Thief ) )
				{
					m_Thief.CriminalAction( false );
				}

				if ( root is Mobile && ((Mobile)root).Player && m_Thief is PlayerMobile && IsInnocentTo( m_Thief, (Mobile)root ) && !IsInGuild( (Mobile)root ) )
				{
					PlayerMobile pm = (PlayerMobile)m_Thief;

					pm.PermaFlags.Add( (Mobile)root );
					pm.Delta( MobileDelta.Noto );
				}
			}
		}

		public static void ItemMutate( Mobile from, int luckChance, Item item, int level )
		{
				int attributeCount;
				int min, max;
				ContainerFunctions.GetRandomAOSStats( out attributeCount, out min, out max, level );
				attributeCount = (int)((double)attributeCount / 2);

				if ( item is BaseWeapon || item is BaseArmor || item is BaseJewel || item is BaseHat || item is BaseQuiver )
				{
					if ( item is BaseWeapon )
					{
						Server.Misc.MorphingTime.MakeOrientalItem( item, from );
						Server.Misc.MorphingTime.ChangeMaterialType( item, from );
						BaseRunicTool.ApplyAttributesTo( (BaseWeapon)item, attributeCount, min, max );
					
						BaseWeapon idropped = (BaseWeapon)item;
						
						if ( Utility.RandomDouble() > 0.80 )
						{
							switch ( Utility.RandomMinMax( 0, 4 ) ) 
							{
								case 0: idropped.DamageLevel = WeaponDamageLevel.Ruin; break;
								case 1: idropped.DamageLevel = WeaponDamageLevel.Might; break;
								case 2: idropped.DamageLevel = WeaponDamageLevel.Force; break;
								case 3: idropped.DamageLevel = WeaponDamageLevel.Power; break;
								case 4: idropped.DamageLevel = WeaponDamageLevel.Vanq; break;
							}
						}
						if ( Utility.RandomDouble() > 0.80  )
						{
							switch ( Utility.RandomMinMax( 0, 4 ) ) 
							{
								case 0: idropped.DurabilityLevel = WeaponDurabilityLevel.Durable; break;
								case 1: idropped.DurabilityLevel = WeaponDurabilityLevel.Substantial; break;
								case 2: idropped.DurabilityLevel = WeaponDurabilityLevel.Massive; break;
								case 3: idropped.DurabilityLevel = WeaponDurabilityLevel.Fortified; break;
								case 4: idropped.DurabilityLevel = WeaponDurabilityLevel.Indestructible; break;
							}
						}
						if ( Utility.RandomDouble() > 0.90 ){idropped.Quality = WeaponQuality.Exceptional;}
						
						Server.Misc.MorphingTime.MakeSpaceAceItem( item, from );
					
					}
					else if ( item is BaseArmor )
					{
						Server.Misc.MorphingTime.MakeOrientalItem( item, from );
						Server.Misc.MorphingTime.ChangeMaterialType( item, from );
						BaseRunicTool.ApplyAttributesTo( (BaseArmor)item, attributeCount, min, max );
						
						BaseArmor idropped = (BaseArmor)item;

						if ( Utility.RandomDouble() > 0.80 )
						{
							switch ( Utility.RandomMinMax( 0, 4 ) ) 
							{
								case 0: idropped.ProtectionLevel = ArmorProtectionLevel.Defense; break;
								case 1: idropped.ProtectionLevel = ArmorProtectionLevel.Guarding; break;
								case 2: idropped.ProtectionLevel = ArmorProtectionLevel.Hardening; break;
								case 3: idropped.ProtectionLevel = ArmorProtectionLevel.Fortification; break;
								case 4: idropped.ProtectionLevel = ArmorProtectionLevel.Invulnerability; break;
							}
						}
						if ( Utility.RandomDouble() > 0.80 )
						{
							switch ( Utility.RandomMinMax( 0, 4 ) ) 
							{
								case 0: idropped.Durability = ArmorDurabilityLevel.Durable; break;
								case 1: idropped.Durability = ArmorDurabilityLevel.Substantial; break;
								case 2: idropped.Durability = ArmorDurabilityLevel.Massive; break;
								case 3: idropped.Durability = ArmorDurabilityLevel.Fortified; break;
								case 4: idropped.Durability = ArmorDurabilityLevel.Indestructible; break;
							}
						}
						if ( Utility.RandomDouble() > 0.90 ){idropped.Quality = ArmorQuality.Exceptional;}
						
						
						Server.Misc.MorphingTime.MakeSpaceAceItem( item, from );
					}
					else if ( item is BaseJewel )
					{
						Server.Misc.MorphingTime.MakeOrientalItem( item, from );
						BaseRunicTool.ApplyAttributesTo( (BaseJewel)item, attributeCount, min, max );
						Server.Misc.MorphingTime.MakeSpaceAceItem( item, from );
					}
					else if ( item is BaseQuiver )
					{
						BaseRunicTool.ApplyAttributesTo( (BaseQuiver)item, attributeCount, min, max );
						item.Name = LootPackEntry.MagicItemName( item, from, Region.Find( from.Location, from.Map ) );
					}
					else if ( item is BaseHat )
					{
						Server.Misc.MorphingTime.MakeOrientalItem( item, from );
						BaseRunicTool.ApplyAttributesTo( (BaseHat)item, attributeCount, min, max );
						Server.Misc.MorphingTime.MakeSpaceAceItem( item, from );
					}
				}
				else if ( item is BaseInstrument )
				{
					if ( Server.Misc.Worlds.IsOnSpaceship( from.Location, from.Map ) )
					{
						string newName = "odd alien";
						switch( Utility.RandomMinMax( 0, 6 ) )
						{
							case 0: newName = "odd"; break;
							case 1: newName = "unusual"; break;
							case 2: newName = "bizarre"; break;
							case 3: newName = "curious"; break;
							case 4: newName = "peculiar"; break;
							case 5: newName = "strange"; break;
							case 6: newName = "weird"; break;
						}

						switch( Utility.RandomMinMax( 1, 4 ) )
						{
							case 1: item = new Pipes();		item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " pipes";		break;
							case 2: item = new Pipes();		item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " pan flute";	break;
							case 3: item = new Fiddle();	item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " violin";		break;
							case 4: item = new Fiddle();	item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " fiddle";		break;
						}

						BaseInstrument lute = (BaseInstrument)item;
							lute.Resource = CraftResource.None;

						item.Hue = Server.Misc.RandomThings.GetRandomColor(0);
					}
					else 
					{
						Server.Misc.MorphingTime.ChangeMaterialType( item, from );
						Server.Misc.MorphingTime.MakeOrientalItem( item, from );
						item.Name = LootPackEntry.MagicItemName( item, from, Region.Find( from.Location, from.Map ) );
					}

					BaseRunicTool.ApplyAttributesTo( (BaseInstrument)item, attributeCount, min, max );
					
					if ( Utility.RandomDouble() < ((double)max / 200) )
						BaseRunicTool.ApplyAttributesTo( (BaseInstrument)item, attributeCount, min, max );

					SlayerName slayer = BaseRunicTool.GetRandomSlayer();

					BaseInstrument instr = (BaseInstrument)item;

					int cHue = 0;
					int cUse = 0;

					switch ( instr.Resource )
					{
						case CraftResource.AshTree: cHue = MaterialInfo.GetMaterialColor( "ash", "", 0 ); cUse = 20; break;
						case CraftResource.CherryTree: cHue = MaterialInfo.GetMaterialColor( "cherry", "", 0 ); cUse = 40; break;
						case CraftResource.EbonyTree: cHue = MaterialInfo.GetMaterialColor( "ebony", "", 0 ); cUse = 60; break;
						case CraftResource.GoldenOakTree: cHue = MaterialInfo.GetMaterialColor( "gold", "", 0 ); cUse = 80; break;
						case CraftResource.HickoryTree: cHue = MaterialInfo.GetMaterialColor( "hickory", "", 0 ); cUse = 100; break;
						case CraftResource.MahoganyTree: cHue = MaterialInfo.GetMaterialColor( "mahogany", "", 0 ); cUse = 120; break;
						case CraftResource.DriftwoodTree: cHue = MaterialInfo.GetMaterialColor( "driftwood", "", 0 ); cUse = 120; break;
						case CraftResource.OakTree: cHue = MaterialInfo.GetMaterialColor( "oak", "", 0 ); cUse = 140; break;
						case CraftResource.PineTree: cHue = MaterialInfo.GetMaterialColor( "pine", "", 0 ); cUse = 160; break;
						case CraftResource.RosewoodTree: cHue = MaterialInfo.GetMaterialColor( "rosewood", "", 0 ); cUse = 180; break;
						case CraftResource.WalnutTree: cHue = MaterialInfo.GetMaterialColor( "walnute", "", 0 ); cUse = 200; break;
					}

					instr.UsesRemaining = instr.UsesRemaining + cUse;

					if ( !( Server.Misc.Worlds.IsOnSpaceship( from.Location, from.Map ) ) )
					{
						if ( cHue > 0 ){ item.Hue = cHue; }
						else if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ item.Hue = RandomThings.GetRandomColor(0); }
					}

					instr.Quality = InstrumentQuality.Regular;
					if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ instr.Quality = InstrumentQuality.Exceptional; }

					if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ instr.Slayer = slayer; }
				}
	
		}				

		public static bool CheckStealingTarget (Mobile target, bool check)
		{
			if (stealingtargets == null)
                stealingtargets = new List<Mobile>();

			if (check)
			{
				stealingtargets.Add( target );
				return false;
			}
			else 
			{
				for ( int i = 0; i < stealingtargets.Count; i++ ) // check if mobile is in list
				{			
					Mobile m = (Mobile)stealingtargets[i];
					if (m == target) //already in the list
						return false;
				}
				
				return true; //not in the list, proceed
			}
		}
		
		public static void WipeStealingList ()
		{
			Stealing.stealingtargets.Clear();

		}
		

		public static bool IsEmptyHanded( Mobile from )
		{
			if ( from.FindItemOnLayer( Layer.OneHanded ) != null )
			{
				if ( from.FindItemOnLayer( Layer.OneHanded ) is BaseWeapon )
				{
					if ( 
						!( from.FindItemOnLayer( Layer.OneHanded ) is PugilistGlove ) && 
						!( from.FindItemOnLayer( Layer.OneHanded ) is PugilistGloves ) 
					)
					{
						return false;
					}
				}
			}
			if ( from.FindItemOnLayer( Layer.TwoHanded ) != null )
			{
				if ( from.FindItemOnLayer( Layer.TwoHanded ) is BaseWeapon )
				{
					if ( 
						!( from.FindItemOnLayer( Layer.TwoHanded ) is PugilistGlove ) && 
						!( from.FindItemOnLayer( Layer.TwoHanded ) is PugilistGloves ) 
					)
					{
						return false;
					}
				}
			}

			return true;
		}

		public static TimeSpan OnUse( Mobile m )
		{
			if ( !IsEmptyHanded( m ) )
			{
				m.SendMessage( "You cannot be wielding a weapon when trying to steal something." );
			}
			else
			{
				m.Target = new Stealing.StealingTarget( m );
				//m.RevealingAction(); // NO REVEALING ON THIS SERVER

				m.SendLocalizedMessage( 502698 ); // Which item do you want to steal?
			}

			return TimeSpan.FromSeconds( 5.0 );
		}
	}

	public class StolenItem
	{
		public static readonly TimeSpan StealTime = TimeSpan.FromMinutes( 2.0 );

		private Item m_Stolen;
		private Mobile m_Thief;
		private Mobile m_Victim;
		private DateTime m_Expires;

		public Item Stolen{ get{ return m_Stolen; } }
		public Mobile Thief{ get{ return m_Thief; } }
		public Mobile Victim{ get{ return m_Victim; } }
		public DateTime Expires{ get{ return m_Expires; } }

		public bool IsExpired{ get{ return ( DateTime.UtcNow >= m_Expires ); } }

		public StolenItem( Item stolen, Mobile thief, Mobile victim )
		{
			m_Stolen = stolen;
			m_Thief = thief;
			m_Victim = victim;

			m_Expires = DateTime.UtcNow + StealTime;
		}

		private static Queue m_Queue = new Queue();

		public static void Add( Item item, Mobile thief, Mobile victim )
		{
			Clean();

			m_Queue.Enqueue( new StolenItem( item, thief, victim ) );
		}

		public static bool IsStolen( Item item )
		{
			Mobile victim = null;

			return IsStolen( item, ref victim );
		}

		public static bool IsStolen( Item item, ref Mobile victim )
		{
			Clean();

			foreach ( StolenItem si in m_Queue )
			{
				if ( si.m_Stolen == item && !si.IsExpired )
				{
					victim = si.m_Victim;
					return true;
				}
			}

			return false;
		}

		public static void ReturnOnDeath( Mobile killed, Container corpse )
		{
			Clean();

			foreach ( StolenItem si in m_Queue )
			{
				if ( si.m_Stolen.RootParent == corpse && si.m_Victim != null && !si.IsExpired )
				{
					if ( si.m_Victim.AddToBackpack( si.m_Stolen ) )
						si.m_Victim.SendLocalizedMessage( 1010464 ); // the item that was stolen is returned to you.
					else
						si.m_Victim.SendLocalizedMessage( 1010463 ); // the item that was stolen from you falls to the ground.

					si.m_Expires = DateTime.UtcNow; // such a hack
				}
			}
		}

		public static void Clean()
		{
			while ( m_Queue.Count > 0 )
			{
				StolenItem si = (StolenItem) m_Queue.Peek();

				if ( si.IsExpired )
					m_Queue.Dequeue();
				else
					break;
			}
		}
	}
}
