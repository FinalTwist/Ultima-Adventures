using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using System.Collections.Generic;
using Server.Misc;
using System.Collections;
using Server.Targeting;

namespace Server.Items
{
	public class ItemIdentification
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.ItemID].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile from )
		{
			from.SendLocalizedMessage( 500343 ); // What do you wish to appraise and identify?
			from.Target = new InternalTarget();

			return TimeSpan.FromSeconds( 1.0 );
		}

		[PlayerVendorTarget]
		private class InternalTarget : Target
		{
			public InternalTarget() :  base ( 8, false, TargetFlags.None )
			{
				AllowNonlocal = true;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Item )
				{
					Item examine = (Item)o;
					IDItem( from, examine, o, false );
				}
				else
				{
					from.SendMessage( "That does not need to be identified.");
				}
			}
		}

		public static void ReplaceItemOrAddToBackpack(Item unidentifiedItem, Item newItem, Mobile identifier)
		{
			if (unidentifiedItem != null && unidentifiedItem.Parent is Container)
			{
				Container parent = (Container)unidentifiedItem.Parent;
				if (newItem.Stackable && parent.TryDropItem(identifier, newItem, false)) // Try to stack
				{
					if (newItem.Deleted) return; // Auto-stacked
				}
				else
					parent.AddItem(newItem); // If it bounced, force-add it back (it was there before being ID'd)

				newItem.Location = unidentifiedItem.Location; // Match the existing item's location
			}
			else
			{
				if (!identifier.AddToBackpack(newItem)) return; // Bounced to the world
				if (newItem.Deleted) return; // Auto-stacked

				Rectangle2D bounds = identifier.Backpack.Bounds;
				newItem.Location = new Point3D(bounds.X, bounds.Y, 0);
			}
		}

		public static void IDItem( Mobile from, Item examine, object o, bool automatic )
		{
			if ( !examine.Movable )
			{
				from.SendMessage( "That cannot move so you cannot identify it." );
			}
			else if ( !from.InRange( examine.GetWorldLocation(), 3 ) )
			{
				from.SendMessage( "You will need to get closer to identify that." );
			}
			else if ( !(examine.IsChildOf( from.Backpack )) && Server.Misc.MyServerSettings.IdentifyItemsOnlyInPack() ) 
			{
				from.SendMessage( "This must be in your backpack to identify." );
			}
			else if ( ( examine is UnknownLiquid ) || ( examine is UnknownReagent ) || ( examine is UnknownKeg ) )
			{
				from.SendMessage( "You need to use Taste Identification to identify that.");
			}
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			else if ( examine is UnknownScroll )
			{
				bool CanID = automatic;
				int bonus = 0;

				if ( from.CheckTargetSkill( SkillName.ItemID, o, (Utility.RandomMinMax(-5, 70)), 120 ) )
					CanID = true;

				if ( from.Skills[SkillName.Inscribe].Base >= 5 )
				{
					if ( from.CheckSkill( SkillName.Inscribe, (Utility.RandomMinMax(0, 70)), 120 ) )
						CanID = true;
						bonus = 1;
					if ( from.Skills[SkillName.Inscribe].Base >= 50 )
						bonus = 2;
				}	

				if ( examine is LibraryScroll1 || examine is LibraryScroll2 || examine is LibraryScroll3 || examine is LibraryScroll4 || examine is LibraryScroll5 || examine is LibraryScroll6 )
					bonus = 0;

				if ( CanID )
				{
					from.PlaySound(0x249);

					if ( Utility.RandomMinMax( 1, 100 ) > 10 )
					{
						UnknownScroll rolls = (UnknownScroll)examine;
						int scrollLevel = rolls.ScrollLevel + bonus;
							if ( scrollLevel > 6 ){ scrollLevel = 6; }

						int paperType = 1;

						if ( rolls.ScrollType == 1 ) // MAGERY
						{
							if ( scrollLevel == 2 ){ paperType = Utility.RandomMinMax( 13, 24 ); }
							else if ( scrollLevel == 3 ){ paperType = Utility.RandomMinMax( 25, 36 ); }
							else if ( scrollLevel == 4 ){ paperType = Utility.RandomMinMax( 37, 48 ); }
							else if ( scrollLevel == 5 ){ paperType = Utility.RandomMinMax( 49, 60 ); }
							else if ( scrollLevel == 6 ){ paperType = Utility.RandomMinMax( 57, 64 ); }
							else { paperType = Utility.RandomMinMax( 1, 12 ); }
						}
						else if ( rolls.ScrollType == 3 ) // BARD
						{
							paperType = Utility.RandomMinMax( 82, 97 );
						}
						else
						{
							if ( scrollLevel == 2 ){ paperType = Utility.RandomMinMax( 68, 70 ); }
							else if ( scrollLevel == 3 ){ paperType = Utility.RandomMinMax( 71, 73 ); }
							else if ( scrollLevel == 4 ){ paperType = Utility.RandomMinMax( 74, 76 ); }
							else if ( scrollLevel == 5 ){ paperType = Utility.RandomMinMax( 77, 79 ); }
							else if ( scrollLevel == 6 ){ paperType = Utility.RandomMinMax( 80, 81 ); }
							else { paperType = Utility.RandomMinMax( 65, 67 ); }
						}

						string paperName = "";
						Item item;
						if ( paperType == 1 ){ item = new ReactiveArmorScroll(); paperName = "reactive armor"; }
						else if ( paperType == 2 ){ item = new ClumsyScroll(); paperName = "clumsy"; }
						else if ( paperType == 3 ){ item = new CreateFoodScroll(); paperName = "create food"; }
						else if ( paperType == 4 ){ item = new FeeblemindScroll(); paperName = "feeblemind"; }
						else if ( paperType == 5 ){ item = new HealScroll(); paperName = "heal"; }
						else if ( paperType == 6 ){ item = new MagicArrowScroll(); paperName = "magic arrow"; }
						else if ( paperType == 7 ){ item = new NightSightScroll(); paperName = "night sight"; }
						else if ( paperType == 8 ){ item = new WeakenScroll(); paperName = "weaken"; }
						else if ( paperType == 9 ){ item = new AgilityScroll(); paperName = "agility"; }
						else if ( paperType == 10 ){ item = new CunningScroll(); paperName = "cunning"; }
						else if ( paperType == 11 ){ item = new CureScroll(); paperName = "cure"; }
						else if ( paperType == 12 ){ item = new HarmScroll(); paperName = "harm"; }
						else if ( paperType == 13 ){ item = new MagicTrapScroll(); paperName = "magic trap"; }
						else if ( paperType == 14 ){ item = new MagicUnTrapScroll(); paperName = "magic untrap"; }
						else if ( paperType == 15 ){ item = new ProtectionScroll(); paperName = "protection"; }
						else if ( paperType == 16 ){ item = new StrengthScroll(); paperName = "strength"; }
						else if ( paperType == 17 ){ item = new BlessScroll(); paperName = "bless"; }
						else if ( paperType == 18 ){ item = new FireballScroll(); paperName = "fireball"; }
						else if ( paperType == 19 ){ item = new MagicLockScroll(); paperName = "magic lock"; }
						else if ( paperType == 20 ){ item = new PoisonScroll(); paperName = "poison"; }
						else if ( paperType == 21 ){ item = new TelekinisisScroll(); paperName = "telekinesis"; }
						else if ( paperType == 22 ){ item = new TeleportScroll(); paperName = "teleport"; }
						else if ( paperType == 23 ){ item = new UnlockScroll(); paperName = "unlock"; }
						else if ( paperType == 24 ){ item = new WallOfStoneScroll(); paperName = "wall of stone"; }
						else if ( paperType == 25 ){ item = new ArchCureScroll(); paperName = "arch cure"; }
						else if ( paperType == 26 ){ item = new ArchProtectionScroll(); paperName = "arch protection"; }
						else if ( paperType == 27 ){ item = new CurseScroll(); paperName = "curse"; }
						else if ( paperType == 28 ){ item = new FireFieldScroll(); paperName = "fire field"; }
						else if ( paperType == 29 ){ item = new GreaterHealScroll(); paperName = "greater heal"; }
						else if ( paperType == 30 ){ item = new LightningScroll(); paperName = "lightning"; }
						else if ( paperType == 31 ){ item = new ManaDrainScroll(); paperName = "mana drain"; }
						else if ( paperType == 32 ){ item = new RecallScroll(); paperName = "recall"; }
						else if ( paperType == 33 ){ item = new BladeSpiritsScroll(); paperName = "blade spirits"; }
						else if ( paperType == 34 ){ item = new DispelFieldScroll(); paperName = "dispel field"; }
						else if ( paperType == 35 ){ item = new IncognitoScroll(); paperName = "incognito"; }
						else if ( paperType == 36 ){ item = new MagicReflectScroll(); paperName = "magic reflect"; }
						else if ( paperType == 37 ){ item = new MindBlastScroll(); paperName = "mind blast"; }
						else if ( paperType == 38 ){ item = new ParalyzeScroll(); paperName = "paralyze"; }
						else if ( paperType == 39 ){ item = new PoisonFieldScroll(); paperName = "poison field"; }
						else if ( paperType == 40 ){ item = new SummonCreatureScroll(); paperName = "summon creature"; }
						else if ( paperType == 41 ){ item = new DispelScroll(); paperName = "dispel"; }
						else if ( paperType == 42 ){ item = new EnergyBoltScroll(); paperName = "energy bolt"; }
						else if ( paperType == 43 ){ item = new ExplosionScroll(); paperName = "explosion"; }
						else if ( paperType == 44 ){ item = new InvisibilityScroll(); paperName = "invisibility"; }
						else if ( paperType == 45 ){ item = new MarkScroll(); paperName = "mark"; }
						else if ( paperType == 46 ){ item = new MassCurseScroll(); paperName = "mass curse"; }
						else if ( paperType == 47 ){ item = new ParalyzeFieldScroll(); paperName = "paralyze field"; }
						else if ( paperType == 48 ){ item = new RevealScroll(); paperName = "reveal"; }
						else if ( paperType == 49 ){ item = new ChainLightningScroll(); paperName = "chain lightning"; }
						else if ( paperType == 50 ){ item = new EnergyFieldScroll(); paperName = "energy field"; }
						else if ( paperType == 51 ){ item = new FlamestrikeScroll(); paperName = "flamestrike"; }
						else if ( paperType == 52 ){ item = new GateTravelScroll(); paperName = "gate travel"; }
						else if ( paperType == 53 ){ item = new ManaVampireScroll(); paperName = "mana vampire"; }
						else if ( paperType == 54 ){ item = new MassDispelScroll(); paperName = "mass dispel"; }
						else if ( paperType == 55 ){ item = new MeteorSwarmScroll(); paperName = "meteor swarm"; }
						else if ( paperType == 56 ){ item = new PolymorphScroll(); paperName = "polymorph"; }
						else if ( paperType == 57 ){ item = new EarthquakeScroll(); paperName = "earthquake"; }
						else if ( paperType == 58 ){ item = new EnergyVortexScroll(); paperName = "energy vortex"; }
						else if ( paperType == 59 ){ item = new ResurrectionScroll(); paperName = "resurrection"; }
						else if ( paperType == 60 ){ item = new SummonAirElementalScroll(); paperName = "summon air elemental"; }
						else if ( paperType == 61 ){ item = new SummonDaemonScroll(); paperName = "summon daemon"; }
						else if ( paperType == 62 ){ item = new SummonEarthElementalScroll(); paperName = "summon earth elemental"; }
						else if ( paperType == 63 ){ item = new SummonFireElementalScroll(); paperName = "summon fire elemental"; }
						else if ( paperType == 64 ){ item = new SummonWaterElementalScroll(); paperName = "summon water elemental"; }
						else if ( paperType == 65 ){ item = new CurseWeaponScroll(); paperName = "curse weapon"; }
						else if ( paperType == 66 ){ item = new BloodOathScroll(); paperName = "blood oath"; }
						else if ( paperType == 67 ){ item = new CorpseSkinScroll(); paperName = "corpse skin"; }
						else if ( paperType == 68 ){ item = new EvilOmenScroll(); paperName = "evil omen"; }
						else if ( paperType == 69 ){ item = new PainSpikeScroll(); paperName = "pain spike"; }
						else if ( paperType == 70 ){ item = new WraithFormScroll(); paperName = "wraith form"; }
						else if ( paperType == 71 ){ item = new MindRotScroll(); paperName = "mind rot"; }
						else if ( paperType == 72 ){ item = new SummonFamiliarScroll(); paperName = "summon familiar"; }
						else if ( paperType == 73 ){ item = new AnimateDeadScroll(); paperName = "animate dead"; }
						else if ( paperType == 74 ){ item = new HorrificBeastScroll(); paperName = "horrific beast"; }
						else if ( paperType == 75 ){ item = new PoisonStrikeScroll(); paperName = "poison strike"; }
						else if ( paperType == 76 ){ item = new WitherScroll(); paperName = "wither"; }
						else if ( paperType == 77 ){ item = new StrangleScroll(); paperName = "strangle"; }
						else if ( paperType == 78 ){ item = new LichFormScroll(); paperName = "lich form"; }
						else if ( paperType == 79 ){ item = new ExorcismScroll(); paperName = "exorcism"; }
						else if ( paperType == 80 ){ item = new VengefulSpiritScroll(); paperName = "vengeful spirit"; }
						else if ( paperType == 81 ){ item = new VampiricEmbraceScroll(); paperName = "vampiric embrace"; }
						else if ( paperType == 82 ){ item = new ArmysPaeonScroll(); paperName = "army's paeon sheet music"; }
						else if ( paperType == 83 ){ item = new EnchantingEtudeScroll(); paperName = "enchanting etude sheet music"; }
						else if ( paperType == 84 ){ item = new EnergyCarolScroll(); paperName = "energy carol sheet music"; }
						else if ( paperType == 85 ){ item = new EnergyThrenodyScroll(); paperName = "energy threnody sheet music"; }
						else if ( paperType == 86 ){ item = new FireCarolScroll(); paperName = "fire carol sheet music"; }
						else if ( paperType == 87 ){ item = new FireThrenodyScroll(); paperName = "fire threnody sheet music"; }
						else if ( paperType == 88 ){ item = new FoeRequiemScroll(); paperName = "foe requiem sheet music"; }
						else if ( paperType == 89 ){ item = new IceCarolScroll(); paperName = "ice carol sheet music"; }
						else if ( paperType == 90 ){ item = new IceThrenodyScroll(); paperName = "ice threnody sheet music"; }
						else if ( paperType == 91 ){ item = new KnightsMinneScroll(); paperName = "knight's minne sheet music"; }
						else if ( paperType == 92 ){ item = new MagesBalladScroll(); paperName = "mage's ballad sheet music"; }
						else if ( paperType == 93 ){ item = new MagicFinaleScroll(); paperName = "magic finale sheet music"; }
						else if ( paperType == 94 ){ item = new PoisonCarolScroll(); paperName = "poison carol sheet music"; }
						else if ( paperType == 95 ){ item = new PoisonThrenodyScroll(); paperName = "poison threnody sheet music"; }
						else if ( paperType == 96 ){ item = new SheepfoeMamboScroll(); paperName = "shepherd's dance sheet music"; }
						else { item = new SinewyEtudeScroll(); paperName = "sinewy etude sheet music"; }
						
						ItemIdentification.ReplaceItemOrAddToBackpack(examine, item, from);

						from.SendMessage("This seems to be a scroll of " + paperName + ".");
					}
					else
					{
						string paperName = "useless scribbles";
						switch( Utility.RandomMinMax( 1, 6 ) )
						{
							case 1: paperName = "useless scribbles"; break;
							case 2: paperName = "a useless recipe"; break;
							case 3: paperName = "a useless list of monsters"; break;
							case 4: paperName = "useless writings"; break;
							case 5: paperName = "a useless drawing"; break;
							case 6: paperName = "a useless map"; break;
						}
						from.SendMessage( "This seems to be " + paperName + "." );
					}
				}
				else
				{
					int nReaction = Utility.RandomMinMax( 0, 10 );

					if ( nReaction > 8 )
					{
						from.FixedParticles( 0x374A, 10, 15, 5021, EffectLayer.Waist );
						from.PlaySound( 0x205 );
						int nPoison = Utility.RandomMinMax( 0, 10 );
						if ( nPoison > 9 ) { from.ApplyPoison( from, Poison.Deadly ); }
						else if ( nPoison > 7 ) { from.ApplyPoison( from, Poison.Greater ); }
						else if ( nPoison > 4 ) { from.ApplyPoison( from, Poison.Regular ); }
						else { from.ApplyPoison( from, Poison.Lesser ); }
						from.SendMessage( "You accidentally trigger a poison spell!" );
					}
					else if ( nReaction > 6 )
					{
						from.FixedParticles( 0x3709, 10, 30, 5052, EffectLayer.LeftFoot );
						from.PlaySound( 0x208 );
						Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), from, from, Utility.RandomMinMax( 5, 40 ), 0, 100, 0, 0, 0 );
						from.SendMessage( "You accidentally trigger a fire spell!" );
					}
					else if ( nReaction > 4 )
					{
						from.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
						from.PlaySound( 0x307 );
						Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), from, from, Utility.RandomMinMax( 5, 40 ), 0, 100, 0, 0, 0 );
						from.SendMessage( "You accidentally trigger an explosion spell!" );
					}
					else
					{
						from.SendMessage("Failing to read the scroll, you throw it out.");
					}
				}
				examine.Delete();
			}
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			else if ( examine is UnknownWand )
			{
				if ( from.CheckTargetSkill( SkillName.ItemID, o, (Utility.RandomMinMax(-5, 70)), 120 ) || automatic )
				{
					Server.Items.UnknownWand.WandType( examine, from, from );
				}
				else
				{
					int nReaction = Utility.RandomMinMax( 0, 10 );

					if ( nReaction > 6 )
					{
						from.FixedParticles( 0x3709, 10, 30, 5052, EffectLayer.LeftFoot );
						from.PlaySound( 0x208 );
						Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), from, from, Utility.RandomMinMax( 5, 40 ), 0, 100, 0, 0, 0 );
						from.SendMessage( "The wands bursts into flames!" );
					}
					else if ( nReaction > 4 )
					{
						from.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
						from.PlaySound( 0x307 );
						Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), from, from, Utility.RandomMinMax( 5, 40 ), 0, 100, 0, 0, 0 );
						from.SendMessage( "The wand explodes in your hands!" );
					}
					else
					{
						from.SendMessage("Failing to figure out the wand, you throw it out.");
					}
				}
				examine.Delete();
			}
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			else if ( examine is UnidentifiedArtifact )
			{
				UnidentifiedArtifact relic = (UnidentifiedArtifact)examine;

				if ( relic.IDAttempt > 5 && !automatic )
				{
					from.SendMessage("Only a vendor can identify this item now as too many attempts were made.");
				}
				else if ( from.CheckTargetSkill( SkillName.ItemID, o, (Utility.RandomMinMax(-5, 70)), 120 ) || automatic )
				{
					Container pack = (Container)relic;
					List<Item> items = new List<Item>();
					foreach (Item item in pack.Items)
					{
						items.Add(item);
					}
					foreach (Item item in items)
					{
						ItemIdentification.ReplaceItemOrAddToBackpack(examine, item, from);
					}

					from.SendMessage("You successfully identify the artifact.");
					relic.Delete();
				}
				else
				{
					relic.IDAttempt = relic.IDAttempt + 1;
					from.SendMessage("You can't seem to identify this artifact.");
				}
			}
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			else if ( examine is UnidentifiedItem )
			{
				UnidentifiedItem relic = (UnidentifiedItem)examine;

				if ( relic.IDAttempt > 5 && !automatic )
				{
					from.SendMessage("Only a vendor can identify this item now as too many attempts were made.");
				}
				else if ( relic.SkillRequired != "ItemID" && !automatic )
				{
					from.SendMessage("You are using the wrong skill to figure this out.");
				}
				else if ( from.CheckTargetSkill( SkillName.ItemID, o, (Utility.RandomMinMax(-5, 70)), 120 ) || automatic )
				{
					Container pack = (Container)relic;
					List<Item> items = new List<Item>();
					foreach (Item item in pack.Items)
					{
						items.Add(item);
					}
					foreach (Item item in items)
					{
						ItemIdentification.ReplaceItemOrAddToBackpack(examine, item, from);
					}

					from.SendMessage("You successfully identify the item.");
					relic.Delete();
				}
				else
				{
					relic.IDAttempt = relic.IDAttempt + 1;
					from.SendMessage("You can't seem to identify this item.");
				}
			}
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			else if ( Server.Misc.RelicItems.IsRelicItem( examine ) == true )
			{
				from.SendMessage( Server.Misc.RelicItems.IdentifyRelicValue( from, from, examine ) );
			}
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			else if ( o is Item )
			{
				if ( o is BaseWeapon || o is BaseArmor )
				{
					from.SendMessage( "You will need to use arms lore on that type of item.");
				}
				else { from.SendMessage( "This item has already been examined by someone."); }
			}
			else if ( o is Mobile )
			{
				((Mobile)o).OnSingleClick( from );
			}
			else
			{
				from.SendLocalizedMessage( 500353 ); // You are not certain...
			}
		}
	}
}
