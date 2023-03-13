using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Spells;
using System.Collections.Generic;
using Server.Misc;
using System.Collections;
using System.Text;
using System.IO;
using Server.Regions;
using Server.Targeting;

namespace Server.Items
{
	public class HiddenTrap : Item
	{
		public int HiddenTrapType;

		[CommandProperty(AccessLevel.Owner)]
		public int Hidden_TrapType { get { return HiddenTrapType; } set { HiddenTrapType = value; InvalidateProperties(); } }

		[Constructable]
		public HiddenTrap() : base(0x5488)
		{
			//ItemID = 0x1647;
			//Light = LightType.Circle150;	
			
			Visible = false;
			Name = "Strange Device";

			//Layer = Layer.None;
			Movable = false;
			//LootType = LootType.Blessed;
			//if (Utility.RandomDouble() < 0.66)
			Light = LightType.Circle150;			
		}

		public HiddenTrap(Serial serial) : base(serial)
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			string sTrapType = "";

			string textSay = "";
			string textLog = "";

			if ( !CanSetOffTraps( m ) )
			{
				return true;
			}

			if ( m is PlayerMobile )
			{
				int nSprung = CheckTrapAvoidance( m, this );

				if ( nSprung == 1 )
				{
					int nTrapType = Utility.RandomMinMax( 1, 25 );
						if ( HiddenTrapType > 0 ){ nTrapType = HiddenTrapType; }

						if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
						{
							nTrapType = Utility.RandomList( 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 16, 18, 19, 20, 21, 22, 23 );
							nTrapType = HiddenTrapType;
						}

					if ( m is PlayerMobile && Spells.Research.ResearchAirWalk.UnderEffect( m ) )
					{
						Point3D air = new Point3D( ( m.X+1 ), ( m.Y+1 ), ( m.Z+5 ) );
						Effects.SendLocationParticles(EffectItem.Create(air, m.Map, EffectItem.DefaultDuration), 0x2007, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( m, 0 ), 0, 5022, 0);
						m.PlaySound( 0x014 );
					}
					else if ( nTrapType == 1 && SavingThrow( m, "Magic", true ) == false ) // REVEALING TRAP
					{
						textSay = "You triggered a magical revealing trap!";
						textLog = "a revealing trap";
			
						if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
						{
							textSay = "You stepped on a statically charged tile, revealing you!";
							textLog = "a statically charged tile";
						}

						if ( m.Hidden != false ){ m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay); sTrapType = textLog; }
					}
					else if ( nTrapType == 2 && SavingThrow( m, "Agility", true ) == false ) // TRIP WIRE
					{
						int HowBad = Utility.RandomMinMax( 1, 5 );

						if ( HowBad == 1 )
						{
							textSay = "You tripped over a wire and dropped your backpack!";
							textLog = "a trip wire trap";
				
							if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
							{
								textSay = "You tripped over a loose deck plate and dropped your backpack!";
								textLog = "a loose deck plate";
							}

							int nDrop = 0;

							List<Item> belongings = new List<Item>();
							foreach( Item i in m.Backpack.Items )
							{
								belongings.Add(i);
								nDrop = 1;
							}

							if ( nDrop > 0 )
							{
								Container box = new DroppedContainer();
								foreach ( Item stuff in belongings )
								{
									if ( stuff != null && stuff.LootType != LootType.Blessed )
										box.DropItem(stuff); //BaseContainer.DropItemFix( stuff, m, box.ItemID, box.GumpID );
								}
								box.MoveToWorld( this.Location, this.Map );
								m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
								m.PlaySound( m.Female ? 812 : 1086 );
								sTrapType = textLog;
							}
						}
						else
						{
							textSay = "You tripped over a wire and dropped one of your equipped items!";
							textLog = "a trip wire trap";
				
							if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
							{
								textSay = "You tripped over a loose deck plate and dropped one of your equipped items!";
								textLog = "a loose deck plate";
							}

							Item iTripped = GetMyItem( m );

							if ( iTripped != null )
							{
								m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
								m.PlaySound( m.Female ? 812 : 1086 );
								iTripped.MoveToWorld( this.Location, this.Map );
								sTrapType = textLog;
							}
						}
					}
					else if ( nTrapType == 3 && SavingThrow( m, "Magic", true ) == false ) // COINS TO LEAD TRAP
					{
						textSay = "A trap triggered, making your coins heavier!";
						textLog = "a transmutation trap";
			
						if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
						{
							textSay = "You stepped over a molecular atomizer, making your coins turn to lead!";
							textLog = "a molecular atomizer";
						}

						Container cont = m.Backpack;
						int nDull = 0;

						int m_gAmount = m.Backpack.GetAmount( typeof( Gold ) );
						int m_cAmount = m.Backpack.GetAmount( typeof( DDCopper ) );
						int m_sAmount = m.Backpack.GetAmount( typeof( DDSilver ) );
						int m_xAmount = m.Backpack.GetAmount( typeof( DDXormite ) );

						if ( cont.ConsumeTotal( typeof( Gold ), m_gAmount ) )
						{
							m.AddToBackpack( new LeadCoin( m_gAmount ) );
							nDull = 1;
						}
						if ( cont.ConsumeTotal( typeof( DDCopper ), m_cAmount ) )
						{
							m.AddToBackpack( new LeadCoin( m_cAmount ) );
							nDull = 1;
						}
						if ( cont.ConsumeTotal( typeof( DDSilver ), m_sAmount ) )
						{
							m.AddToBackpack( new LeadCoin( m_sAmount ) );
							nDull = 1;
						}
						if ( cont.ConsumeTotal( typeof( DDXormite ), m_xAmount ) )
						{
							m.AddToBackpack( new LeadCoin( m_xAmount ) );
							nDull = 1;
						}
						if ( nDull > 0 )
						{
							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
							m.FixedParticles( 0x374A, 10, 15, 5028, EffectLayer.Waist );
							m.PlaySound( 0x1E1 );
						}
						sTrapType = textLog;
					}
					else if ( nTrapType == 4 && SavingThrow( m, "Magic", true ) == false ) // LOSE ITEM TRAP
					{
						textSay = "A trap triggered, almost ruining one of your protected items!";
						textLog = "a destructive trap";
			
						if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
						{
							textSay = "You stepped over a molecular oxidizer, almost ruining one of your protected items!";
							textLog = "a molecular oxidizer";
						}

						Container cont = m.Backpack;
						Item iRuined = GetMyItem( m );

						if ( iRuined != null )
						{

							
								textSay = "A trap triggered, rusting one of your equipped items!";
					
								if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
								{
									textSay = "You stepped over a molecular oxidizer, rusting one of your equipped items!";
								}

								int Rusted = 0;
								if ( iRuined is BaseWeapon )
								{
									BaseWeapon iRusted = (BaseWeapon)iRuined;

									if ( Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( iRuined ) )
									{
										m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
										RustyJunk broke = new RustyJunk();
										broke.ItemID = iRuined.ItemID;
										broke.Name = "rusted item";
										broke.Weight = iRuined.Weight;
										m.AddToBackpack ( broke );
										Rusted = 1;
									}
								}
								if ( iRuined is BaseArmor )
								{
									BaseArmor iRusted = (BaseArmor)iRuined;

									if ( Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( iRuined ) )
									{
										m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
										RustyJunk broke = new RustyJunk();
										broke.ItemID = iRuined.ItemID;
										broke.Name = "rusted item";
										broke.Weight = Utility.RandomMinMax( 1, 4 );
										m.AddToBackpack ( broke );
										Rusted = 1;
									}
								}
								if ( Rusted == 0 )
								{
									textSay = "A trap triggered, ruining one of your equipped items!";
						
									if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
									{
										textSay = "You stepped over a molecular oxidizer, ruining one of your equipped items!";
									}

									m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
									BrokenGear broke = new BrokenGear();
									broke.ItemID = iRuined.ItemID;
									broke.Name = "Ruined Item";
									broke.Weight = iRuined.Weight;
									m.AddToBackpack ( broke );
								}
								iRuined.Delete();
							
							m.FixedParticles( 0x374A, 10, 15, 5028, EffectLayer.Waist );
							m.PlaySound( 0x1E1 );
							sTrapType = textLog;
						}
					}
					else if ( nTrapType == 5 && SavingThrow( m, "Magic", true ) == false ) // LOSE A STAT TRAP
					{
						int nStat = Utility.RandomMinMax( 1, 3 );

						if ( nStat == 1 )
						{
							if ( m.RawStr > 10 )
							{
								textSay = "A trap triggered, making you feel weaker!";
								textLog = "a weakness trap";
					
								if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
								{
									textSay = "You walked over some bacteria, making you feel weaker!";
									textLog = "a bacterial contamination";
								}

								m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
								m.FixedParticles( 0x3779, 10, 15, 5009, EffectLayer.Waist );
								m.PlaySound( 0x1E6 );
								m.RawStr = m.RawStr - 1; 
								sTrapType = textLog;
							}
						}
						else if ( nStat == 2 )
						{
							if ( m.RawDex > 10 )
							{
								textSay = "A trap triggered, making you feel sluggish!";
								textLog = "a slowness trap";
					
								if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
								{
									textSay = "You walked over some bacteria, making you feel sluggish!";
									textLog = "a bacterial contamination";
								}

								m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
								m.FixedParticles( 0x3779, 10, 15, 5002, EffectLayer.Head );
								m.PlaySound( 0x1DF );
								m.RawDex = m.RawDex - 1; 
								sTrapType = textLog;
							}
						}
						else
						{
							if ( m.RawInt > 10 )
							{
								textSay = "A trap triggered, making your mind cloudy!";
								textLog = "a mind numbing trap";
					
								if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
								{
									textSay = "You walked over some bacteria, making your mind cloudy!";
									textLog = "a bacterial contamination";
								}

								m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
								m.FixedParticles( 0x3779, 10, 15, 5004, EffectLayer.Head );
								m.PlaySound( 0x1E4 );
								m.RawInt = m.RawInt - 1;
								sTrapType = textLog;
							}
						}
					}
					else if ( nTrapType == 6 && SavingThrow( m, "Poison", true ) == false ) // POISON TRAP
					{
						textSay = "A trap triggered, making you feel ill!";
						textLog = "a poison gas trap";
			
						if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
						{
							textSay = "You stepped on some biological contamination, making you feel ill!";
							textLog = "a biological contamination";
						}

						int itHurts = m.PoisonResistance;
						int itSicks = 0;

						if ( itHurts >= 70 ){ itSicks = 1; }
						else if ( itHurts >= 50 ){ itSicks = 2; }
						else if ( itHurts >= 30 ){ itSicks = 3; }
						else if ( itHurts >= 10 ){ itSicks = 4; }
						else { itSicks = 5; }

						switch( Utility.RandomMinMax( 1, itSicks ) )
						{
							case 1: m.ApplyPoison( m, Poison.Lesser );	break;
							case 2: m.ApplyPoison( m, Poison.Regular );	break;
							case 3: m.ApplyPoison( m, Poison.Greater );	break;
							case 4: m.ApplyPoison( m, Poison.Deadly );	break;
							case 5: m.ApplyPoison( m, Poison.Lethal );	break;
						}

						Effects.SendLocationEffect( this.Location, this.Map, 0x11A8 - 2, 16, 3, 0, 0 );
						Effects.PlaySound( this.Location, this.Map, 0x231 );
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);

						sTrapType = textLog;
					}
					else if ( nTrapType == 7 && SavingThrow( m, "Magic", true ) == false ) // DRAIN TRAP
					{
						int nStat = Utility.RandomMinMax( 1, 3 );

						if ( nStat == 1 )
						{
							textSay = "A trap triggered, making you feel near dead!";
							textLog = "a life draining trap";
				
							if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
							{
								textSay = "You stepped over a radioactive spill, making you feel near dead!";
								textLog = "a radioactive spill";
							}

							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
							m.FixedParticles( 0x3779, 10, 15, 5009, EffectLayer.Waist );
							m.PlaySound( 0x1E6 );
							m.Hits = 1; 
							sTrapType = textLog;
						}
						else if ( nStat == 2 )
						{
							textSay = "A trap triggered, making you feel really tired!";
							textLog = "a stamina draining trap";
				
							if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
							{
								textSay = "You stepped over a radioactive spill, making you feel really tired!";
								textLog = "a radioactive spill";
							}

							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
							m.FixedParticles( 0x3779, 10, 15, 5002, EffectLayer.Head );
							m.PlaySound( 0x1DF );
							m.Stam = 0; 
							sTrapType = textLog;
						}
						else
						{
							textSay = "A trap triggered, draining your mana!";
							textLog = "a mana draining trap";
				
							if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
							{
								textSay = "You stepped over a radioactive spill, draining your mana!";
								textLog = "a radioactive spill";
							}

							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
							m.FixedParticles( 0x3779, 10, 15, 5004, EffectLayer.Head );
							m.PlaySound( 0x1E4 );
							m.Mana = 0; 
							sTrapType = textLog;
						}
					}
					else if ( nTrapType == 8 && SavingThrow( m, "Magic", true ) == false ) // GEM STONE TRAP
					{
						List<Item> items = new List<Item>();
						int nAmount = 0;

						foreach( Item i in m.Backpack.FindItemsByType( typeof( Ruby ), true ) ){ items.Add(i); }
						foreach( Item i in m.Backpack.FindItemsByType( typeof( Amber ), true ) ){ items.Add(i); }
						foreach( Item i in m.Backpack.FindItemsByType( typeof( Amethyst ), true ) ){ items.Add(i); }
						foreach( Item i in m.Backpack.FindItemsByType( typeof( Citrine ), true ) ){ items.Add(i); }
						foreach( Item i in m.Backpack.FindItemsByType( typeof( Emerald ), true ) ){ items.Add(i); }
						foreach( Item i in m.Backpack.FindItemsByType( typeof( Diamond ), true ) ){ items.Add(i); }
						foreach( Item i in m.Backpack.FindItemsByType( typeof( Sapphire ), true ) ){ items.Add(i); }
						foreach( Item i in m.Backpack.FindItemsByType( typeof( StarSapphire ), true ) ){ items.Add(i); }
						foreach( Item i in m.Backpack.FindItemsByType( typeof( Tourmaline ), true ) ){ items.Add(i); }
						foreach( Item i in m.Backpack.FindItemsByType( typeof( DDRelicGem ), true ) ){ items.Add(i); }
						foreach( Item i in m.Backpack.FindItemsByType( typeof( MageEye ), true ) ){ items.Add(i); }

						foreach ( Item item in items )
						{
							if ( item != null )
							{
								nAmount = nAmount + item.Amount;
								item.Delete();
							}
						}
						if ( nAmount > 0 )
						{
							textSay = "A trap triggered, making your gems fuse together!";
							textLog = "a lode stone trap";
				
							if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
							{
								textSay = "You stepped over a damaged power coil, fusing your gemstones together!";
								textLog = "a damaged power coil";
							}

							RuinedGems rocks = new RuinedGems();
							rocks.Weight = nAmount * 5.0;
							rocks.RuinedCount = nAmount;
							m.AddToBackpack ( rocks );
							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
							m.FixedParticles( 0x374A, 10, 15, 5028, EffectLayer.Waist );
							m.PlaySound( 0x1E1 );
							sTrapType = textLog;
						}
					}
					else if ( nTrapType == 9 && SavingThrow( m, "Magic", true ) == false ) // REAGENT TRAP
					{
						int nAmount = 0;

						List<Item> regv = new List<Item>();
						foreach( Item i in m.Backpack.Items )
						{
							if ( Server.Misc.MaterialInfo.IsReagent( i ) )
							{
								nAmount = nAmount + i.Amount;
								regv.Add(i);
							}
						}
						foreach ( Item regt in regv )
						{
							regt.Delete();
						}

						if ( nAmount > 0 )
						{
							textSay = "You walked into a toxic cloud, ruining your reagents!";
							textLog = "a toxic cloud trap";

							RottedReagents regs = new RottedReagents();
							regs.Weight = nAmount * 0.1;
							regs.RottedCount = nAmount;
							m.AddToBackpack ( regs );
							Effects.SendLocationEffect( this.Location, this.Map, 0x11A8 - 2, 16, 3, 0, 0 );
							Effects.PlaySound( this.Location, this.Map, 0x231 );
							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
							sTrapType = textLog;
						}
					}
					else if ( nTrapType == 10 && SavingThrow( m, "Magic", true ) == false ) // BOOK BOUND TRAP
					{
						Container cont = m.Backpack;
						int nDull = 0;

						List<Item> items = new List<Item>();

						Item handy = m.FindItemOnLayer( Layer.OneHanded );
						if ( handy is Spellbook )
						{
							items.Add(handy); nDull = 1;
						}

						Item tally = m.FindItemOnLayer( Layer.Talisman );
						if ( tally is Spellbook )
						{
							items.Add(tally); nDull = 1;
						}

						foreach( Item i in m.Backpack.FindItemsByType( typeof( Spellbook ), true ) )
						{
							if (i.Parent is BookBox){} else
							{
								if ( i.LootType != LootType.Blessed )
								{
									if ( CheckInsuranceOnTrap( i, m ) == true )
									{
										m.LocalOverheadMessage(MessageType.Emote, 1150, true, "One of your books was almost magically bound!");
									}
									else
									{
										items.Add(i); nDull = 1;
									}
								}
							}
						}
						foreach( Item i in m.Backpack.FindItemsByType( typeof( Runebook ), true ) )
						{
							if (i.Parent is BookBox){} else
							{
								items.Add(i);
								nDull = 1;
							}
						}

						if ( nDull > 0 )
						{
							Container box = new BookBox();
							foreach ( Item item in items )
							{
								box.DropItem(item);
							}

							m.AddToBackpack ( box );

							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "A trap triggered, locking your books in a magic box!");
							m.FixedParticles( 0x374A, 10, 15, 5028, EffectLayer.Waist );
							m.PlaySound( 0x1E1 );
							sTrapType = "a book bound trap";
						}
					}
					else if ( nTrapType == 11 && SavingThrow( m, "Magic", true ) == false ) // TELEPORT TRAP
					{
						textSay = "A trap triggered, teleporting you away from here!";
						textLog = "a teleportation trap";
			
						if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
						{
							textSay = "You stepped over an overcharged transporter pad, transporting you away from here!";
							textLog = "an overcharged transporter pad";
						}

						string world = Worlds.GetMyWorld( m.Map, m.Location, m.X, m.Y );
						Point3D p = Worlds.GetRandomLocation( world, "land" );
						Map map = Worlds.GetMyDefaultMap( world );

						if ( p != Point3D.Zero )
						{
							Server.Mobiles.BaseCreature.TeleportPets( m, p, map );
							m.MoveToWorld( p, map );
							Effects.PlaySound( m.Location, m.Map, 0x1FC );
							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
							sTrapType = textLog;
						}
					}
					else if ( nTrapType == 12  && SavingThrow( m, "Magic", true ) == false && m.Fame > 0 ) // FAME TRAP
					{
						int FameLoss = (int)(m.Fame - ( m.Fame * 0.20 ));
						if ( FameLoss < 0 ){ FameLoss = 0; }
						if ( FameLoss > 0 )
						{
							m.Fame = FameLoss;
							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "A trap triggered, causing some of your deeds to be forgotten!");
							m.FixedParticles( 0x374A, 10, 15, 5032, EffectLayer.Head );
							m.PlaySound( 0x1F8 );
							sTrapType = "a forgotten fame trap";
						}
					}
					else if ( nTrapType == 13 && SavingThrow( m, "Magic", true ) == false ) // CURSE ITEM TRAP
					{
						Container cont = m.Backpack;
						Item iCursed = GetMyItem( m );

						if ( iCursed != null )
						{
								m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "A trap triggered, putting a curse on one of your equipped items!");
								m.FixedParticles( 0x374A, 10, 15, 5028, EffectLayer.Waist );
								m.PlaySound( 0x1E1 );

								Container box = new CurseItem();
								box.DropItem(iCursed);
								box.ItemID = iCursed.ItemID;
								box.Hue = iCursed.Hue;
								box.Name = "Cursed Item";

								m.AddToBackpack ( box );

								sTrapType = "a curse item trap";
						
						}
					}
					else if ( nTrapType == 14 && SavingThrow( m, "Physical", true ) == false ) // FLOOR SPIKE TRAP
					{
						if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ Effects.SendLocationEffect( this.Location, this.Map, 4506 + 1, 18, 3, 0, 0 ); }
						else { Effects.SendLocationEffect( this.Location, this.Map, 4512 + 1, 18, 3, 0, 0 ); }
						Effects.PlaySound( this.Location, this.Map, 0x22C );
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "You triggered a spike trap!");
						int itHurts = (int)( (Utility.RandomMinMax(10, (m.HitsMax)) * ( 100 - m.PhysicalResistance ) ) / 100 );
						m.Damage( itHurts, m );
						sTrapType = "a spike trap";

					}
					else if ( nTrapType == 15 && SavingThrow( m, "Physical", true ) == false ) // SAW TRAP
					{
						if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ Effects.SendLocationEffect( this.Location, this.Map, 0x11AC + 1, 6, 3, 0, 0 ); }
						else { Effects.SendLocationEffect( this.Location, this.Map, 0x11B1 + 1, 6, 3, 0, 0 ); }
						Effects.PlaySound( this.Location, this.Map, 0x21C );
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "You triggered a saw blade trap!");
						int itHurts = (int)( (Utility.RandomMinMax(20,(m.HitsMax+25)) * ( 100 - m.PhysicalResistance ) ) / 100 );
						m.Damage( itHurts, m );
						sTrapType = "a saw blade trap";
					}
					else if ( nTrapType == 16 && SavingThrow( m, "Fire", true ) == false ) // FLAME TRAP
					{
						textSay = "You triggered a fire trap!";
						textLog = "a fire trap";
			
						if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
						{
							textSay = "You stepped on thermal vent, scorching you!";
							textLog = "a thermal vent";
						}

						Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
						Effects.PlaySound( this.Location, this.Map, 0x225 );
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
						int itHurts = (int)( (Utility.RandomMinMax(25, (int)((double)(m.Hits*2) * (1+ ((double)AetherGlobe.BalanceLevel / 100000))) ) * ( 100 - m.FireResistance ) ) / 100 );
						m.Damage( itHurts, m );
						sTrapType = textLog;
					}
					else if ( nTrapType == 17 && SavingThrow( m, "Physical", true ) == false ) // GIANT SPIKE TRAP
					{
						Effects.SendLocationEffect( this.Location, this.Map, 0x1D99, 48, 2, 0, 0 );
						Effects.PlaySound( this.Location, this.Map, 0x22C );
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "You triggered a giant spike trap!");
						int itHurts = (int)( (Utility.RandomMinMax(50,(m.HitsMax+50)) * ( 100 - m.PhysicalResistance ) ) / 100 );
						m.Damage( itHurts, m );
						sTrapType = "a giant spike trap";
					}
					else if ( nTrapType == 18 && SavingThrow( m, "Fire", true ) == false ) // EXPLOSION TRAP
					{
						textSay = "You triggered an explosion trap!";
						textLog = "an explosion trap";
			
						if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
						{
							textSay = "You trip over a plasma grenade!";
							textLog = "a plasma grenade";
						}

						m.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
						m.PlaySound( 0x307 );
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
						int itHurts = (int)( (Utility.RandomMinMax(10,(m.HitsMax*2)) * ( 100 - m.FireResistance ) ) / 100 );
						m.Damage( itHurts, m );
						sTrapType = textLog;
					}
					else if ( nTrapType == 19 && SavingThrow( m, "Energy", true ) == false ) // ELECTRICAL TRAP
					{
						textSay = "You triggered an electrical trap!";
						textLog = "an electrical trap";
			
						if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
						{
							textSay = "You stepped onto an electrically charged deck plate!";
							textLog = "an electrically charged deck plate";
						}

						m.BoltEffect( 0 );
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
						int itHurts = (int)( (Utility.RandomMinMax(25,150) * ( 100 - m.EnergyResistance ) ) / 100 );
						m.Damage( itHurts, m );
						sTrapType = textLog;
					}
					else if ( nTrapType == 20 && SavingThrow( m, "Agility", true ) == false ) // TRIP WIRE THAT BREAKS ARROWS
					{
						List<Item> items = new List<Item>();
						int nBroken = 0;
						int WhichArrows = Utility.RandomMinMax( 1, 2 );
						string sTripped = "";
						int nAmount = 0;

						if ( WhichArrows == 1 )
						{
							foreach( Item i in m.Backpack.FindItemsByType( typeof( Arrow ), true ) )
							{
								items.Add(i);
								nBroken = 1;
								sTripped = "arrows";
							}
						}
						else
						{
							foreach( Item i in m.Backpack.FindItemsByType( typeof( Bolt ), true ) )
							{
								items.Add(i);
								nBroken = 1;
								sTripped = "crossbow bolts";
							}
						}
						if ( nBroken > 0 )
						{
							foreach ( Item item in items )
							{
								if ( item != null )
								{
									nAmount = nAmount + item.Amount;
									item.Delete();
								}
							}
							if ( nAmount > 0 )
							{
								textSay = "You tripped over a wire and broke all of your " + sTripped + "!";
								textLog = "a trip wire trap";
					
								if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
								{
									textSay = "You tripped over a loose deck plate and broke all of your " + sTripped + "!";
									textLog = "a loose deck plate";
								}

								Shaft wood = new Shaft();
								wood.Amount = nAmount;
								m.AddToBackpack ( wood );

								m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
								m.PlaySound( m.Female ? 812 : 1086 );
								sTrapType = textLog;
							}
						}
					}
					else if ( nTrapType == 21 && SavingThrow( m, "Poison", true ) == false ) // TAINTED TRAP
					{
						List<Item> items = new List<Item>();
						int nBroken = 0;
						int nAmount = 0;

						foreach( Item i in m.Backpack.FindItemsByType( typeof( Bandage ), true ) )
						{
							items.Add(i);
							nBroken = 1;
						}
						foreach ( Item item in items )
						{
							if ( item != null )
							{
								nAmount = nAmount + item.Amount;
								item.Delete();
							}
						}
						if ( nAmount > 0 )
						{
							TaintedBandage bandage = new TaintedBandage();
							bandage.Weight = nAmount * 0.1;
							string sAmount = nAmount.ToString();
							if ( nAmount > 1 ){ bandage.Name = sAmount + " tainted bandages"; }
							m.AddToBackpack ( bandage );

							Effects.SendLocationEffect( this.Location, this.Map, 0x11A8 - 2, 16, 3, 0, 0 );
							Effects.PlaySound( this.Location, this.Map, 0x231 );
							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "You walked into a noxious cloud, tainting your bandages!");

							sTrapType = "a noxious cloud trap";
						}
					}
					else if ( nTrapType == 22 && SavingThrow( m, "Agility", true ) == false ) // TRIP WIRE THAT BREAKS POTIONS
					{
						List<Item> items = new List<Item>();
						int nBroken = 0;


						foreach( Item i in m.Backpack.Items )
						{
							if ( Server.Misc.MaterialInfo.IsPotion( i ) )
							{
								items.Add(i);
								nBroken = 1;
							}
						}


						foreach ( Item item in items )
						{
							if ( item != null )
								item.Delete();
						}
						if ( nBroken > 0 )
						{
							textSay = "You tripped over a wire and broke all of your potion bottles!";
							textLog = "a trip wire trap";
				
							if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
							{
								textSay = "You tripped over a loose deck plate and broke all of your potion bottles!";
								textLog = "a loose deck plate";
							}

							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
							m.PlaySound( 0x040 );
							sTrapType = textLog;
						}
					}
					else if ( nTrapType == 23 && SavingThrow( m, "Magic", true ) == false ) // MELT JEWELS TRAP
					{
						int puddle = 0;

						List<Item> jewelry = new List<Item>();
						foreach( Item j in m.Backpack.Items )
						{
							if ( j is BaseJewel && IsJewelryItem( j ) )
							{
								if ( j.LootType != LootType.Blessed )
									jewelry.Add(j);
							}
						}

						Item jw;

						if ( m.FindItemOnLayer( Layer.Bracelet ) != null ) { jw = m.FindItemOnLayer( Layer.Bracelet ); if ( jw.LootType != LootType.Blessed && jw is BaseJewel && IsJewelryItem( jw ) ){ jewelry.Add(jw); } }
						if ( m.FindItemOnLayer( Layer.Ring ) != null ) { jw = m.FindItemOnLayer( Layer.Ring ); if ( jw.LootType != LootType.Blessed && jw is BaseJewel && IsJewelryItem( jw ) ){ jewelry.Add(jw); } }
						if ( m.FindItemOnLayer( Layer.Helm ) != null ) { jw = m.FindItemOnLayer( Layer.Helm ); if ( jw.LootType != LootType.Blessed && jw is BaseJewel && IsJewelryItem( jw ) ){ jewelry.Add(jw); } }
						if ( m.FindItemOnLayer( Layer.Neck ) != null ) { jw = m.FindItemOnLayer( Layer.Neck ); if ( jw.LootType != LootType.Blessed && jw is BaseJewel && IsJewelryItem( jw ) ){ jewelry.Add(jw); } }
						if ( m.FindItemOnLayer( Layer.Earrings ) != null ) { jw = m.FindItemOnLayer( Layer.Earrings ); if ( jw.LootType != LootType.Blessed && jw is BaseJewel && IsJewelryItem( jw ) ){ jewelry.Add(jw); } }

						foreach ( Item jl in jewelry )
						{
							jl.Delete();
							puddle++;
						}

						if ( puddle > 0 )
						{
							textSay = "A trap triggered, melting all of your jewelry!";
							textLog = "a jewelry melting trap";
				
							if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
							{
								textSay = "You stepped over an exposed power relay, melting all of your jewelry!";
								textLog = "an exposed power relay";
							}

							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
							RustyJunk broke = new RustyJunk();
							broke.ItemID = 0x122A;
							broke.Name = "melted jewelry";
							broke.Hue = 0x9C4;
							broke.Weight = puddle;
							m.AddToBackpack ( broke );
							sTrapType = textLog;
						}
					}
					else if ( nTrapType == 24 && SavingThrow( m, "Agility", true ) == false ) // PIT TRAP
					{
						textSay = "A fall into a deep pit!";
						textLog = "a deep pit";

						CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

						string sX = m.X.ToString();
						string sY = m.Y.ToString();
						string sZ = m.Z.ToString();
						string sMap = Worlds.GetMyMapString( m.Map );
						string sZone = Worlds.GetMyWorld( m.Map, m.Location, m.X, m.Y );

						DB.CharacterPublicDoor = sX + "#" + sY + "#" + sZ + "#" + sMap + "#" + sZone;

						Effects.PlaySound( m.Location, m.Map, Utility.RandomList( 0x5D2,0x5D3 ) );
						Point3D p = new Point3D( 2602, 3688, 100 );
						Point3D b = new Point3D( 2602, 3688, 0 );
						Map map = Map.Trammel;

						Server.Mobiles.BaseCreature.TeleportPets( m, b, map );
						m.MoveToWorld( p, map );
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
						sTrapType = textLog;
					}
					else if ( nTrapType == 25 && m.Karma != 0 && SavingThrow( m, "Magic", true ) == false ) // ALIGNMENT TRAP
					{
						if ( ((PlayerMobile)m).BalanceEffect == 0 )
						{
							m.Karma = m.Karma * -1;
							textSay = "A trap triggered, making your mind warp your morality!";
							textLog = "a mind warping trap";
							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
							m.FixedParticles( 0x374A, 10, 15, 5028, EffectLayer.Waist );
							m.PlaySound( 0x1E1 );
							sTrapType = textLog;
						}
						else 
						{
							m.Karma = 0;
							textSay = "A trap triggered, making your mind question your morality!";
							textLog = "a mind warping trap";
							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, textSay);
							m.FixedParticles( 0x374A, 10, 15, 5028, EffectLayer.Waist );
							m.PlaySound( 0x1E1 );
							sTrapType = textLog;
						}
					}

					///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

					if ( sTrapType != "" )
					{
						if ( m.Hidden != false )
						{
							m.RevealingAction();
						}

						LoggingFunctions.LogTraps( m, sTrapType );
					}
				}
				this.Delete();
			}

			if ( sTrapType == "a teleportation trap" || sTrapType == "an overcharged transporter pad" || sTrapType == "a deep pit" )
				return false;

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( HiddenTrapType );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            HiddenTrapType = reader.ReadInt();
		}

		public static bool IsJewelryItem( Item j )
		{
			return Server.Misc.MaterialInfo.IsJewelry( j );
		}

		public static bool IAmAWeaponSlayer( Mobile m, Mobile enemy )
		{
			bool IsSlayer = false;

			if ( m is PlayerMobile )
			{
				if ( m.FindItemOnLayer( Layer.OneHanded ) is BaseWeapon )
				{
					Item hitter = m.FindItemOnLayer( Layer.OneHanded );
					BaseWeapon weapon = (BaseWeapon)hitter;
					SlayerName slay1 = weapon.Slayer;
					SlayerName slay2 = weapon.Slayer2;
					if ( slay1 != SlayerName.None )
					{
						SlayerEntry entry1 = SlayerGroup.GetEntryByName( slay1 );
						if ( entry1.Slays( enemy ) ){ IsSlayer = true; }
					}
					if ( slay2 != SlayerName.None )
					{
						SlayerEntry entry2 = SlayerGroup.GetEntryByName( slay2 );
						if ( entry2.Slays( enemy ) ){ IsSlayer = true; }
					}
				}
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) is BaseWeapon )
				{
					Item hitter = m.FindItemOnLayer( Layer.TwoHanded );
					BaseWeapon weapon = (BaseWeapon)hitter;
					SlayerName slay1 = weapon.Slayer;
					SlayerName slay2 = weapon.Slayer2;
					if ( slay1 != SlayerName.None )
					{
						SlayerEntry entry1 = SlayerGroup.GetEntryByName( slay1 );
						if ( entry1.Slays( enemy ) ){ IsSlayer = true; }
					}
					if ( slay2 != SlayerName.None )
					{
						SlayerEntry entry2 = SlayerGroup.GetEntryByName( slay2 );
						if ( entry2.Slays( enemy ) ){ IsSlayer = true; }
					}
				}
			}
			return IsSlayer;
		}
		public static bool CheckInsuranceOnTrap( Item item, Mobile m )
		{
			if ( item.LootType == LootType.Blessed )
			{
				return true;
			}
			
			return false;
		}

		public static bool IAmShielding( Mobile m, int skill )
		{
			bool Shielded = false;
			int shieldmintest = skill / Utility.RandomMinMax(1, 3); //Final - let's spice this up

			if ( m is PlayerMobile )
			{
				if ( m.FindItemOnLayer( Layer.TwoHanded ) is BaseShield )
				{
					if ( m.CheckSkill( SkillName.Parry, shieldmintest, skill ) ) // final
					{
						Shielded = true;
					}
				}
			}
			return Shielded;
		}

		public static bool SavingThrow( Mobile m, string save, bool isTrap )
		{
			bool madeSave = false;
			int SaveThrow = 0;
			string area = "";

			if ( save == "Magic" )
			{
				area = "magical resistance";
				SaveThrow = (int)(( m.Int + m.Skills[SkillName.MagicResist].Value + m.EnergyResistance ) / 4);
			}
			else if ( save == "Physical" )
			{
				area = "physical resistance";
				SaveThrow = (int)(( m.Str + m.PhysicalResistance ) / 3);
			}
			else if ( save == "Agility" )
			{
				area = "quick reflexes";
				SaveThrow = m.Dex;
			}
			else if ( save == "Cold" )
			{
				area = "cold resistance";
				SaveThrow = (int)(( m.Dex + m.ColdResistance ) / 3);
			}
			else if ( save == "Fire" )
			{
				area = "fire resistance";
				SaveThrow = (int)(( m.Dex + m.FireResistance ) / 3);
			}
			else if ( save == "Poison" )
			{
				area = "poison resistance";
				SaveThrow = (int)(( m.Str + m.Skills[SkillName.Poisoning].Value + m.PoisonResistance ) / 4);
			}
			else if ( save == "Energy" )
			{
				area = "energy resistance";
				SaveThrow = (int)(( m.Int + m.EnergyResistance ) / 3);
			}

			if ( SaveThrow > 60 ){ SaveThrow = 60; }

			if ( SaveThrow >= Utility.RandomMinMax( 1, 100 ) )
			{
				if ( isTrap && Server.Misc.MyServerSettings.AnnounceTrapSaves() )
				{
					string textSay = "You got near a hidden trap, but with your " + area + "...you avoid it.";
					if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
					{
						textSay = "You got near something dangerous, but with your " + area + "...you avoid it.";
					}
					m.LocalOverheadMessage(Network.MessageType.Emote, 0x3B2, false, textSay);
					m.PlaySound( m.Female ? 793 : 1065 );
				}
				madeSave = true;
			}

			return madeSave;
		}

		public static bool CanSetOffTraps( Mobile m )
		{
			if ( m is PlayerMobile && ( !m.Alive || m.Blessed == true || m.AccessLevel > AccessLevel.Player ) )
				return false;

			return true;
		}

		public static int CheckTrapAvoidance( Mobile m, Item Trap )
		{
			string textSay = "";

			int trapChance = 0;
			
			int trapdifficulty = Utility.RandomMinMax(0,3) * Utility.RandomMinMax(0, 25); // Final - let's add some spice to this mechanic
			
			if ( Trap is HiddenTrap ){ trapChance = MyServerSettings.FloorTrapTrigger(); }

			int nSprung = 1;

			if ( m is PlayerMobile && ( !m.Alive || m.Blessed == true || m.AccessLevel > AccessLevel.Player ) ) // GHOSTS AND ADMINS ARE IMMUNE //
			{
				nSprung = 0;
			}

			if ( !( Trap is SpellTrap ) && trapChance >= Utility.RandomMinMax( 1, 100 ) && m is PlayerMobile )
			{
				nSprung = 0;
			}
			else if ( m.CheckSkill( SkillName.RemoveTrap, trapdifficulty, 125 ) ) // final
			{
				if ( m is PlayerMobile )
				{
					if ( Trap is MushroomTrap )
					{
						m.LocalOverheadMessage(Network.MessageType.Emote, 0x3B2, false, "You got near a strange mushroom, but your skill in removing traps has helped you avoid it.");
					}
					else
					{
						textSay = "You got near a hidden trap, but your skill in removing traps has helped you disable it.";
						if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
						{
							textSay = "You got near something dangerous, but your skill in removing traps has helped you avoid it.";
						}
						m.LocalOverheadMessage(Network.MessageType.Emote, 0x3B2, false, textSay);
						m.PlaySound( 0x241 );
					}
				}
				nSprung = 0;
			}

			if ( m is PlayerMobile )
			{
				if ( m.Backpack != null && nSprung == 1 )
				{
					Item magicwand = m.Backpack.FindItemByType( typeof ( TrapWand ) );

					if ( magicwand != null )
					{
						TrapWand wands = (TrapWand)magicwand;
						int nPower = wands.WandPower;
						if ( nPower >= Utility.RandomMinMax( 1, 100 ) && wands.owner == m )
						{
							if ( Trap is MushroomTrap )
							{
								m.LocalOverheadMessage(Network.MessageType.Emote, 0x3B2, false, "You got near a strange mushroom, but the magic of your orb has helped you avoid it.");
							}
							else
							{
								textSay = "You got near a hidden trap, but the magic of your orb has disabled it.";
								if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
								{
									textSay = "You got near something dangerous, but the magic of your orb has helped you avoid it.";
								}
								m.LocalOverheadMessage(Network.MessageType.Emote, 0x3B2, false, textSay);
							}
							m.PlaySound( 0x1F0 );
							nSprung = 0;
						}
					}
				}
				if ( GetPlayerInfo.LuckyPlayer(m.Luck, m) && nSprung == 1 )
				{
					if ( Trap is MushroomTrap )
					{
						m.LocalOverheadMessage(Network.MessageType.Emote, 0x3B2, false, "You got near a strange mushroom, but with luck on your side...you avoid it.");
					}
					else
					{
						textSay = "You got near a hidden trap, but with luck on your side...it broke.";
						if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
						{
							textSay = "You got near something dangerous, but with luck on your side...you avoid it."; m.PlaySound( 0x54B );
						}
						else { m.PlaySound( 0x241 ); }
						m.LocalOverheadMessage(Network.MessageType.Emote, 0x3B2, false, textSay);
					}
					nSprung = 0;
				}
				if ( m.Backpack != null && nSprung == 1 )
				{
					Item tenfootpole = m.Backpack.FindItemByType( typeof ( TenFootPole ) );

					if ( tenfootpole != null )
					{
						TenFootPole poles = (TenFootPole)tenfootpole;
						if ( 50 >= Utility.RandomMinMax( 1, 100 ) )
						{
							m.PlaySound( 0x039 );
							poles.Charges = poles.Charges - 1;
							if ( poles.Charges < 1 )
							{
								if ( Trap is MushroomTrap )
								{
									m.LocalOverheadMessage(Network.MessageType.Emote, 0x3B2, false, "You break your ten foot pole, but avoid the strange mushroom nearby.");
								}
								else
								{
									textSay = "You tap your ten foot pole, disabling a hidden trap and breaking the pole.";
									if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
									{
										textSay = "You tap your ten foot pole, avoiding something dangerous and breaking the pole.";
									}
									m.LocalOverheadMessage(Network.MessageType.Emote, 0x3B2, false, textSay);
								}
								poles.Delete();
							}
							else
							{
								if ( Trap is MushroomTrap )
								{
									m.LocalOverheadMessage(Network.MessageType.Emote, 0x3B2, false, "You tap your ten foot pole, avoiding a strange mushroom nearby.");
								}
								else
								{
									textSay = "You tap your ten foot pole, disabling a hidden trap.";
									if ( Server.Misc.Worlds.IsOnSpaceship( m.Location, m.Map ) )
									{
										textSay = "You tap your ten foot pole, avoiding something dangerous.";
									}
									m.LocalOverheadMessage(Network.MessageType.Emote, 0x3B2, false, textSay);
								}
								poles.InvalidateProperties();
							}
							nSprung = 0;
						}
					}
				}
			}

			return nSprung;
		}

		public static Item GetMyItem( Mobile m )
		{
			if ( m == null )
				return null;

			Item myItem = null;
			Item myBlessCheck = null;
			int nOrder = Utility.RandomMinMax( 1, 20 );

			int nOuterTorso = 0;
			int nOneHanded = 0;
			int nTwoHanded = 0;
			int nBracelet = 0;
			int nRing = 0;
			int nHelm = 0;
			int nArms = 0;
			int nOuterLegs = 0;
			int nNeck = 0;
			int nGloves = 0;
			int nTalisman = 0;
			int nShoes = 0;
			int nCloak = 0;
			int nFirstValid = 0;
			int nWaist = 0;
			int nInnerLegs = 0;
			int nInnerTorso = 0;
			int nPants = 0;
			int nShirt = 0;
			int nEarrings = 0;

			if ( m.FindItemOnLayer( Layer.OuterTorso ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.OuterTorso ); if ( myBlessCheck.LootType == LootType.Blessed ){ nOuterTorso = 1; } }
			if ( m.FindItemOnLayer( Layer.OneHanded ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.OneHanded ); if ( myBlessCheck.LootType == LootType.Blessed ){ nOneHanded = 1; } }
			if ( m.FindItemOnLayer( Layer.TwoHanded ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.TwoHanded ); if ( myBlessCheck.LootType == LootType.Blessed ){ nTwoHanded = 1; } }
			if ( m.FindItemOnLayer( Layer.Bracelet ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.Bracelet ); if ( myBlessCheck.LootType == LootType.Blessed ){ nBracelet = 1; } }
			if ( m.FindItemOnLayer( Layer.Ring ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.Ring ); if ( myBlessCheck.LootType == LootType.Blessed ){ nRing = 1; } }
			if ( m.FindItemOnLayer( Layer.Helm ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.Helm ); if ( myBlessCheck.LootType == LootType.Blessed ){ nHelm = 1; } }
			if ( m.FindItemOnLayer( Layer.Arms ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.Arms ); if ( myBlessCheck.LootType == LootType.Blessed ){ nArms = 1; } }
			if ( m.FindItemOnLayer( Layer.OuterLegs ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.OuterLegs ); if ( myBlessCheck.LootType == LootType.Blessed ){ nOuterLegs = 1; } }
			if ( m.FindItemOnLayer( Layer.Neck ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.Neck ); if ( myBlessCheck.LootType == LootType.Blessed ){ nNeck = 1; } }
			if ( m.FindItemOnLayer( Layer.Gloves ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.Gloves ); if ( myBlessCheck.LootType == LootType.Blessed ){ nGloves = 1; } }
			if ( m.FindItemOnLayer( Layer.Talisman ) != null ) { if (!( m.FindItemOnLayer( Layer.Talisman ) is Spellbook )){ myBlessCheck = m.FindItemOnLayer( Layer.Talisman ); if ( myBlessCheck.LootType == LootType.Blessed ){ nTalisman = 1; } } }
			if ( m.FindItemOnLayer( Layer.Shoes ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.Shoes ); if ( myBlessCheck.LootType == LootType.Blessed ){ nShoes = 1; } }
			if ( m.FindItemOnLayer( Layer.Cloak ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.Cloak ); if ( myBlessCheck.LootType == LootType.Blessed ){ nCloak = 1; } }
			if ( m.FindItemOnLayer( Layer.FirstValid ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.FirstValid ); if ( myBlessCheck.LootType == LootType.Blessed ){ nFirstValid = 1; } }
			if ( m.FindItemOnLayer( Layer.Waist ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.Waist ); if ( myBlessCheck.LootType == LootType.Blessed ){ nWaist = 1; } }
			if ( m.FindItemOnLayer( Layer.InnerLegs ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.InnerLegs ); if ( myBlessCheck.LootType == LootType.Blessed ){ nInnerLegs = 1; } }
			if ( m.FindItemOnLayer( Layer.InnerTorso ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.InnerTorso ); if ( myBlessCheck.LootType == LootType.Blessed ){ nInnerTorso = 1; } }
			if ( m.FindItemOnLayer( Layer.Pants ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.Pants ); if ( myBlessCheck.LootType == LootType.Blessed ){ nPants = 1; } }
			if ( m.FindItemOnLayer( Layer.Shirt ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.Shirt ); if ( myBlessCheck.LootType == LootType.Blessed ){ nShirt = 1; } }
			if ( m.FindItemOnLayer( Layer.Earrings ) != null ) { myBlessCheck = m.FindItemOnLayer( Layer.Earrings ); if ( myBlessCheck.LootType == LootType.Blessed ){ nEarrings = 1; } }

			if (nOrder == 1)
			{
				if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
			}
			else if (nOrder == 2)
			{
				if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
			}
			else if (nOrder == 3)
			{
				if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
			}
			else if (nOrder == 4)
			{
				if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
			}
			else if (nOrder == 5)
			{
				if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
			}
			else if (nOrder == 6)
			{
				if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
			}
			else if (nOrder == 7)
			{
				if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
			}
			else if (nOrder == 8)
			{
				if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
			}
			else if (nOrder == 9)
			{
				if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
			}
			else if (nOrder == 10)
			{
				if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
			}
			else if (nOrder == 11)
			{
				if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
			}
			else if (nOrder == 12)
			{
				if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
			}
			else if (nOrder == 13)
			{
				if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
			}
			else if (nOrder == 14)
			{
				if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
			}
			else if (nOrder == 15)
			{
				if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
			}
			else if (nOrder == 16)
			{
				if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
			}
			else if (nOrder == 17)
			{
				if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
			}
			else if (nOrder == 18)
			{
				if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
			}
			else if (nOrder == 19)
			{
				if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
				else if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
			}
			else if (nOrder == 20)
			{
				if ( m.FindItemOnLayer( Layer.Earrings ) != null && nEarrings != 1) { myItem = m.FindItemOnLayer( Layer.Earrings ); }
				else if ( m.FindItemOnLayer( Layer.Waist ) != null && nWaist != 1) { myItem = m.FindItemOnLayer( Layer.Waist ); }
				else if ( m.FindItemOnLayer( Layer.OuterTorso ) != null && nOuterTorso != 1) { myItem = m.FindItemOnLayer( Layer.OuterTorso ); }
				else if ( m.FindItemOnLayer( Layer.OneHanded ) != null && nOneHanded != 1) { myItem = m.FindItemOnLayer( Layer.OneHanded ); }
				else if ( m.FindItemOnLayer( Layer.TwoHanded ) != null && nTwoHanded != 1) { myItem = m.FindItemOnLayer( Layer.TwoHanded ); }
				else if ( m.FindItemOnLayer( Layer.Bracelet ) != null && nBracelet != 1) { myItem = m.FindItemOnLayer( Layer.Bracelet ); }
				else if ( m.FindItemOnLayer( Layer.Ring ) != null && nRing != 1) { myItem = m.FindItemOnLayer( Layer.Ring ); }
				else if ( m.FindItemOnLayer( Layer.Helm ) != null && nHelm != 1) { myItem = m.FindItemOnLayer( Layer.Helm ); }
				else if ( m.FindItemOnLayer( Layer.Arms ) != null && nArms != 1) { myItem = m.FindItemOnLayer( Layer.Arms ); }
				else if ( m.FindItemOnLayer( Layer.OuterLegs ) != null && nOuterLegs != 1) { myItem = m.FindItemOnLayer( Layer.OuterLegs ); }
				else if ( m.FindItemOnLayer( Layer.Neck ) != null && nNeck != 1) { myItem = m.FindItemOnLayer( Layer.Neck ); }
				else if ( m.FindItemOnLayer( Layer.Gloves ) != null && nGloves != 1) { myItem = m.FindItemOnLayer( Layer.Gloves ); }
				else if ( m.FindItemOnLayer( Layer.Talisman ) != null && nTalisman != 1) { myItem = m.FindItemOnLayer( Layer.Talisman ); }
				else if ( m.FindItemOnLayer( Layer.Shoes ) != null && nShoes != 1) { myItem = m.FindItemOnLayer( Layer.Shoes ); }
				else if ( m.FindItemOnLayer( Layer.Cloak ) != null && nCloak != 1) { myItem = m.FindItemOnLayer( Layer.Cloak ); }
				else if ( m.FindItemOnLayer( Layer.FirstValid ) != null && nFirstValid != 1) { myItem = m.FindItemOnLayer( Layer.FirstValid ); }
				else if ( m.FindItemOnLayer( Layer.InnerLegs ) != null && nInnerLegs != 1) { myItem = m.FindItemOnLayer( Layer.InnerLegs ); }
				else if ( m.FindItemOnLayer( Layer.InnerTorso ) != null && nInnerTorso != 1) { myItem = m.FindItemOnLayer( Layer.InnerTorso ); }
				else if ( m.FindItemOnLayer( Layer.Pants ) != null && nPants != 1) { myItem = m.FindItemOnLayer( Layer.Pants ); }
				else if ( m.FindItemOnLayer( Layer.Shirt ) != null && nShirt != 1) { myItem = m.FindItemOnLayer( Layer.Shirt ); }
			}
			return myItem;
		}

		public override bool HandlesOnMovement{ get{ return MyServerSettings.EnableDungeonSoundEffects(); } }

		private DateTime m_NextSound;	
		public DateTime NextSound{ get{ return m_NextSound; } set{ m_NextSound = value; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{

			if (m is PlayerMobile && Utility.InRange( m.Location, this.Location, 2 ) && (Utility.RandomDouble() > 0.80 || Utility.RandomDouble() <= (m.Skills[SkillName.DetectHidden].Value /200) ) && !this.Visible )
			{
				this.Visible = true;	
				switch( 3 )
					{
						case 0: m.PlaySound(0x1FF); break;
						case 1: m.PlaySound(0x33B); break;
						case 2: m.PlaySound(0x520); break;

					}
			}				
			else if (m is PlayerMobile && Utility.InRange( m.Location, this.Location, 4 ) && (Utility.RandomDouble() > 0.66 || Utility.RandomDouble() <= (m.Skills[SkillName.DetectHidden].Value /175) ) && !this.Visible )
			{
					switch( 5 )
					{
						case 0: m.PlaySound(0x04A); break;
						case 1: m.PlaySound(0x051); break;
						case 2: m.PlaySound(0x343); break;
						case 3: m.PlaySound(0x4C8); break;
						case 4: m.PlaySound(0x5DE); break;
					}
			}	
			else if (m is PlayerMobile && Utility.InRange( m.Location, this.Location, 4 ) ) {}
			else if (m is PlayerMobile && Utility.InRange( m.Location, this.Location, 6 ) && this.Visible )
				this.Visible = false;
			
			
			if( m is PlayerMobile && MyServerSettings.EnableDungeonSoundEffects() )
			{
				if ( DateTime.UtcNow >= m_NextSound && Utility.InRange( m.Location, this.Location, 10 ) )
				{
					if ( Utility.RandomBool() )
					{
						int sound = HiddenChest.DungeonSounds( this );	
						m.PlaySound( sound );	
					}
					m_NextSound = (DateTime.UtcNow + TimeSpan.FromSeconds( 60 ));	
				}
			}
		}
	}
}
