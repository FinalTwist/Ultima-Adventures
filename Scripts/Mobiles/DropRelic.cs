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

namespace Server.Misc
{
    class DropRelic
    {
		public static void DropSpecialItem( Mobile from, Mobile killer, Container c )
		{
			BaseCreature bc = (BaseCreature)from;

			if ( killer != null && c != null && !bc.IsStabled && !bc.Controlled && !bc.IsBonded )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					Region reg = Region.Find( from.Location, from.Map );

					if ( Server.Misc.Worlds.IsOnSpaceship( from.Location, from.Map ) )
					{
						int fameCycle = (int)( from.Fame / 2400 );
							if ( fameCycle > 10 ){ fameCycle = 10; }
							if ( fameCycle < 1 ){ fameCycle = 1; }
							fameCycle = Utility.RandomMinMax( 0, fameCycle );
							while ( fameCycle > 0 )
							{
								fameCycle--;
								c.DropItem( DungeonLoot.RandomSpaceBag() );
							}
					}

					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 20 ) == 1 )
					{
						int stuffedHue = 1;

						if ( from is ElderBrownBearRiding ){ stuffedHue = 0x840; }
						else if ( from is SabretoothBearRiding ){ stuffedHue = 0x840; }
						else if ( from is ElderPolarBearRiding ){ stuffedHue = 0; }
						else if ( from is ElderBlackBearRiding ){ stuffedHue = 0x497; }
						else if ( from is CaveBearRiding ){ stuffedHue = 0x6DE; }

						if ( stuffedHue != 1 )
						{
							StuffedBear trophy = new StuffedBear();
							trophy.Hue = stuffedHue;
							trophy.Name = "stuffed trophy of " + from.Name;
							trophy.AnimalWhere = "From " + Server.Misc.Worlds.GetRegionName( from.Map, from.Location );
							string trophyKiller = killer.Name + " the " + Server.Misc.GetPlayerInfo.GetSkillTitle( killer );
							trophy.AnimalKiller = "Killed by " + trophyKiller;
							c.DropItem( trophy );
						}
						else if ( from is TigerRiding || from is SabretoothTigerRiding || from is Tiger || from is SabretoothTiger )
						{
							if ( Utility.RandomBool() ){ c.DropItem( new TigerRugEastDeed() ); } else { c.DropItem( new TigerRugSouthDeed() ); } 
						}
						else if ( from is WhiteTigerRiding || from is WhiteTiger )
						{
							if ( Utility.RandomBool() ){ c.DropItem( new WhiteTigerRugEastDeed() ); } else { c.DropItem( new WhiteTigerRugSouthDeed() ); } 
						}
					}

					if ( from is ServiceDroid || from is BattleDroid || from is SecurityDroid || from is MaintenanceDroid || from is ExcavationDroid || from is CombatDroid )
					{
						if ( Utility.RandomMinMax( 1, 300 ) < (from.Fame/100) ){ c.DropItem( new RobotSheetMetal( Utility.RandomMinMax( 4, 10 ) ) ); }
						if ( Utility.RandomMinMax( 1, 300 ) < (from.Fame/100) ){ c.DropItem( new RobotBatteries() ); }
						if ( Utility.RandomMinMax( 1, 300 ) < (from.Fame/100) ){ c.DropItem( new RobotEngineParts() ); }
						if ( Utility.RandomMinMax( 1, 300 ) < (from.Fame/100) ){ c.DropItem( new RobotCircuitBoard() ); }
						if ( Utility.RandomMinMax( 1, 300 ) < (from.Fame/100) ){ c.DropItem( new RobotTransistor() ); }
						if ( Utility.RandomMinMax( 1, 300 ) < (from.Fame/100) ){ c.DropItem( new RobotBolt( Utility.RandomMinMax( 1, 4 ) ) ); }
						if ( Utility.RandomMinMax( 1, 300 ) < (from.Fame/100) ){ c.DropItem( new RobotGears( Utility.RandomMinMax( 1, 4 ) ) ); }
						if ( Utility.RandomMinMax( 1, 300 ) < (from.Fame/100) ){ c.DropItem( new RobotOil( Utility.RandomMinMax( 1, 2 ) ) ); }
					}

					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 0, 100 ) > 95 )
					{
						int min = (int)(from.Fame/2000); if ( min < 1 ){ min = 1; }
						int max = (int)(from.Fame/1000); if ( max < 2 ){ max = 2; }
						int props = (int)(from.Fame/3000); if ( props < 1 ){ props = 1; }

						int item = 0;
						int color = 0;
						string name = "trinket";

						if ( from is Cyclops ){ 					item = 0x2C86;		name = "eye of " + from.Name + " " + from.Title; }
						else if ( from is ShamanicCyclops ){		item = 0x2C86;		name = "eye of " + from.Name + " " + from.Title; }
						else if ( from is Beholder ){				item = 0x2C9A;		name = "eye of " + from.Name + " " + from.Title; }
						else if ( from is Gazer ){					item = 0x2C9A;		name = "eye of " + from.Name + " " + from.Title; }
						else if ( from is ElderGazer ){				item = 0x2C9A;		name = "eye of " + from.Name + " " + from.Title; }
						else if ( from is Lich || from is Vordo || from is Nazghoul || from is LichLord || from is DemiLich || from is AncientLich || from is Surtaz || from is LichKing || from is UndeadDruid )
						{
							if ( from.Backpack.FindItemByType( typeof ( EvilSkull ) ) == null )
							{
																	item = 0x2C95;		name = "skull of " + from.Name + " " + from.Title; 
							}
						}

						if ( item > 0 )
						{
							BaseJewel trinket = new MagicTalisman();
							BaseRunicTool.ApplyAttributesTo( trinket, false, killer.Luck, props, min, max );
							trinket.Hue = color;
							trinket.ItemID = item;
							trinket.Name = name;
							c.DropItem( trinket );
						}
					}

					/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 0, 100 ) > 95 )
					{
						if ( from is Lich || from is Vordo || from is Nazghoul || from is LichLord || from is DemiLich || from is AncientLich || from is Surtaz || from is LichKing || from is UndeadDruid )
						{
							if ( from.Backpack.FindItemByType( typeof ( EvilSkull ) ) == null && from.Backpack.FindItemByType( typeof ( MagicTalisman ) ) == null )
							{
								int min = (int)(from.Fame/2000); if ( min < 1 ){ min = 1; }
								int max = (int)(from.Fame/1000); if ( max < 2 ){ max = 2; }
								int props = (int)(from.Fame/3000); if ( props < 1 ){ props = 1; }

								BaseHat cowl = new ReaperHood();
								BaseRunicTool.ApplyAttributesTo( cowl, false, killer.Luck, props, min, max );
								cowl.Hue = Server.Misc.RandomThings.GetRandomEvilColor();
								cowl.Name = "mask of " + from.Name + " " + from.Title;
								c.DropItem( cowl );
							}
						}
					}

					/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomBool() && from is Syth )
					{
						int min = (int)(from.Fame/2000); if ( min < 1 ){ min = 1; }
						int max = (int)(from.Fame/1000); if ( max < 2 ){ max = 2; }
						int props = (int)(from.Fame/3000); if ( props < 1 ){ props = 1; }

						BaseJewel talisman = new MagicTalisman();
						BaseRunicTool.ApplyAttributesTo( talisman, false, killer.Luck, props, min, max );
						talisman.Hue = Server.Misc.RandomThings.GetRandomColor(0);
						talisman.Name = "Mysticron of " + from.Name + " " + from.Title;
						talisman.ItemID = 0x4CDE;
						c.DropItem( talisman );
					}

					///////////////////////////////////////////////////////////////////////////////////////
							/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomBool() && from is Jedi )
					{
						int min = (int)(from.Fame/2000); if ( min < 1 ){ min = 1; }
						int max = (int)(from.Fame/1000); if ( max < 2 ){ max = 2; }
						int props = (int)(from.Fame/3000); if ( props < 1 ){ props = 1; }

						BaseJewel talisman = new MagicTalisman();
						BaseRunicTool.ApplyAttributesTo( talisman, false, killer.Luck, props, min, max );
						talisman.Hue = Server.Misc.RandomThings.GetRandomColor(0);
						talisman.Name = "Forcicron of " + from.Name + " " + from.Title;
						talisman.ItemID = 0xFFD7;
						c.DropItem( talisman );
					}

					///////////////////////////////////////////////////////////////////////////////////////
					
					
					

					SlayerEntry sythdemon = SlayerGroup.GetEntryByName( SlayerName.Exorcism );

					Mobile syth = from.LastKiller;					// SYTH GET HELL SHARDS FROM KILLING DEAEMONS
					if ( sythdemon.Slays(from) && syth != null )
					{
						if ( syth is BaseCreature )
							syth = ((BaseCreature)syth).GetMaster();

						if ( syth is PlayerMobile && Server.Misc.GetPlayerInfo.isSyth ( syth, false ) && Utility.RandomBool() )
						{
							int minhs = (int)(from.Fame/600);
								if ( minhs < 1 ){ minhs = 1; }
							int maxhs = (int)(from.Fame/400);
								if ( maxhs < 3 ){ maxhs = 3; }
							Item hellshard = new HellShard( Utility.RandomMinMax( minhs, maxhs ) );
							c.DropItem( hellshard );
						}
					}

					/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 0, 100 ) > 90 && from.Skills[SkillName.Inscribe].Base >= 20 && from.Skills[SkillName.Magery].Base >= 20 )
					{
						SlayerEntry wizardkiller = SlayerGroup.GetEntryByName( SlayerName.WizardSlayer );
						if ( wizardkiller.Slays(from) )
						{
							c.DropItem( new TomeOfWands() );
						}
					}

					/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

					if ( reg.IsPartOf( "the Tower of Brass" ) )
					{
						int BrassFame = (int)(from.Fame/1000);
							BrassFame = 100 - BrassFame;

						if ( from is FireGiant && Utility.RandomMinMax( 1, 100 ) >= BrassFame )
						{
							if ( Utility.RandomMinMax( 1, 2 ) == 1 )
							{
								BaseArmor drop = Loot.RandomArmorOrShield();

								if ( drop.Resource == CraftResource.Iron )
								{
									drop.Resource = CraftResource.Brass;
									c.DropItem( drop );
								}
								else
								{
									drop.Delete();
								}
							}
							else
							{
								BaseWeapon drop = Loot.RandomWeapon();

								if ( drop.Resource == CraftResource.Iron )
								{
									drop.Resource = CraftResource.Brass;
									c.DropItem( drop );
								}
								else
								{
									drop.Delete();
								}
							}
						}
						if ( from is BloodDemon && Utility.RandomMinMax( 1, 100 ) >= BrassFame )
						{
							if ( Utility.RandomMinMax( 1, 2 ) == 1 )
							{
								BaseArmor drop = Loot.RandomArmorOrShield();

								if ( drop.Resource == CraftResource.Iron )
								{
									MorphingItem.MorphMyItem( drop, "IGNORED", "brass", "IGNORED", MorphingTemplates.TemplateIceDemon("armors") );
									c.DropItem( drop );
								}
								else
								{
									drop.Delete();
								}
							}
							else
							{
								BaseWeapon drop = Loot.RandomWeapon();

								if ( drop.Resource == CraftResource.Iron )
								{
									MorphingItem.MorphMyItem( drop, "IGNORED", "brass", "IGNORED", MorphingTemplates.TemplateIceDemon("weapons") );
									c.DropItem( drop );
								}
								else
								{
									drop.Delete();
								}
							}
						}
					}

					/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

					if ( reg.IsPartOf( "the Ancient Elven Mine" ) )
					{
						if ( from is ShamanicCyclops && Utility.RandomMinMax( 1, 10 ) >= 9 )
						{
							if ( Utility.RandomMinMax( 1, 2 ) == 1 )
							{
								BaseArmor drop = Loot.RandomArmorOrShield();

								if ( drop.Resource == CraftResource.Iron )
								{
									MorphingItem.MorphMyItem( drop, "IGNORED", "silver", "IGNORED", MorphingTemplates.TemplateSilver("armors") );
									c.DropItem( drop );
								}
								else
								{
									drop.Delete();
								}
							}
							else
							{
								BaseWeapon drop = Loot.RandomWeapon();

								if ( drop.Resource == CraftResource.Iron )
								{
									MorphingItem.MorphMyItem( drop, "IGNORED", "silver", "IGNORED", MorphingTemplates.TemplateSilver("weapons") );
									c.DropItem( drop );
								}
								else
								{
									drop.Delete();
								}
							}
						}
						if ( Utility.RandomMinMax( 1, 10 ) == 1 && from.Fame > 2000 )
						{
							Item stone = new RareMetals( Utility.RandomMinMax( 5, 15 ), "silver stones" );
							c.DropItem(stone);
						}
						else if ( Utility.RandomMinMax( 1, 10 ) == 1 && from.Fame > 2000 )
						{
							Item ingot = new ShinySilverIngot( Utility.RandomMinMax( 2, 8 ) );
							c.DropItem(ingot);
						}
					}

					/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

					if ( reg.IsPartOf( "the Daemon's Crag" ) )
					{
						if ( ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) || Utility.RandomMinMax( 1, 20 ) == 1 ) && ( from is EvilMage || from is EvilMageLord ) )
						{
							PaganArtifact pagan = new PaganArtifact(0);
							pagan.PaganPoints = Utility.RandomMinMax( 80, 100 );
								if ( from is EvilMageLord ){ pagan.PaganPoints = Utility.RandomMinMax( 100, 120 ); }
							c.DropItem( pagan );
						}
					}

					if ( reg.IsPartOf( "the Zealan Tombs" ) )
					{
						if ( ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) || Utility.RandomMinMax( 1, 10 ) == 1 ) && from is KhumashGor )
						{
							PaganArtifact pagan = new PaganArtifact(16);
							pagan.PaganPoints = Utility.RandomMinMax( 100, 150 );
							c.DropItem( pagan );
						}
					}

					/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

					if ( 	( from is DemonOfTheSea ) || 
							( from is BloodDemon ) || 
							( from is Devil ) || 
							( from is TitanPyros ) || 
							( from is Balron ) || 
							( from is Fiend ) || 
							( from is Archfiend ) || 
							( from is LesserDemon ) || 
							( from is Xurtzar ) || 
							( from is FireDemon ) || 
							( from is DeepSeaDevil ) || 
							( from is Daemon ) || 
							( from is DaemonTemplate ) || 
							( from is BlackGateDemon ) )
					{
						if ( 90 < Utility.Random( 100 ) ){ c.DropItem( new DemonClaw() ); }
						if ( 90 < Utility.Random( 100 ) ){ DemonSkin goods = new DemonSkin(); goods.Amount = Utility.RandomMinMax( 1, 3 ); c.DropItem(goods); }
					}

					if ( from is Phoenix ){ if ( 50 < Utility.Random( 100 ) ){ c.DropItem( new PhoenixFeather() ); } }

					if ( from is Pegasus || from is Placeron ){ if ( 75 < Utility.Random( 100 ) ){ c.DropItem( new PegasusFeather() ); } }

					if ( 98 < Utility.Random( 100 ) && killer is PlayerMobile ){ Server.Misc.Skulls.MakeSkull( from, c, killer, reg.Name ); } // MAKE A SKULL //--------------------------

					if ( ( from is Unicorn ) || ( from is Dreadhorn ) || ( from is DarkUnicornRiding ) )
					{
						if ( 90 < Utility.Random( 100 ) ){ c.DropItem( new UnicornHorn() ); }
						if ( 70 < Utility.Random( 100 ) ){ UnicornSkin goods = new UnicornSkin(); goods.Amount = Utility.RandomMinMax( 1, 3 ); c.DropItem(goods); }
					}
					if ( from is Nightmare || from is AncientNightmareRiding )
					{
						if ( 70 < Utility.Random( 100 ) ){ NightmareSkin goods = new NightmareSkin(); goods.Amount = Utility.RandomMinMax( 1, 3 ); c.DropItem(goods); }
					}
					if ( from is ObsidianElemental )
					{
						if ( 1 == Utility.Random( 1000 ) ){ c.DropItem(new ObsidianStone()); }
					}
					if ( ( from is Troll ) || ( from is SeaTroll ) || ( from is SwampTroll ) || ( from is FrostTroll ) )
					{
						if ( 95 < Utility.Random( 100 ) ){ TrollSkin goods = new TrollSkin(); goods.Amount = Utility.RandomMinMax( 1, 2 ); c.DropItem(goods); }
					}
					if ( ( from is GoldenSerpent ) || ( from is BloodSnake ) || ( from is SilverSerpent ) || ( from is JungleViper ) || ( from is IceSerpent ) || 
						( from is GiantSerpent ) || ( from is GiantSnake ) || ( from is SeaSnake ) || ( from is LavaSerpent ) || ( from is LargeSnake ) || ( from is GiantAdder ) )
					{
						if ( 95 < Utility.Random( 100 ) ){ SerpentSkin goods = new SerpentSkin(); goods.Amount = Utility.RandomMinMax( 1, 2 ); c.DropItem(goods); }
					}
					if ( 	( from is AncientWyrm ) || 
							( from is LavaDragon ) || 
							( from is BottleDragon ) || 
							( from is SeaDragon ) || 
							( from is DeepSeaDragon ) || 
							( from is Dragon ) || 
							( from is Dragons ) || 
							( from is DeepSeaDragon ) || 
							( from is SeaDragon ) || 
							( from is BlackDragon ) || 
							( from is AsianDragon ) || 
							( from is PrimevalFireDragon ) || 
							( from is PrimevalGreenDragon ) || 
							( from is PrimevalNightDragon ) || 
							( from is PrimevalRedDragon ) || 
							( from is PrimevalRoyalDragon ) || 
							( from is PrimevalRunicDragon ) || 
							( from is PrimevalSeaDragon ) || 
							( from is ReanimatedDragon ) || 
							( from is VampiricDragon ) || 
							( from is PrimevalAbysmalDragon ) || 
							( from is PrimevalAmberDragon ) || 
							( from is PrimevalBlackDragon ) || 
							( from is PrimevalDragon ) || 
							( from is PrimevalSilverDragon ) || 
							( from is PrimevalVolcanicDragon ) || 
							( from is PrimevalStygianDragon ) || 
							( from is GemDragon ) || 
							( from is DragonKing ) || 
							( from is VolcanicDragon ) || 
							( from is RadiationDragon ) || 
							( from is CrystalDragon ) || 
							( from is VoidDragon ) || 
							( from is ElderDragon ) || 
							( from is BlueDragon ) || 
							( from is SlasherOfVoid ) || 
							( from is GreenDragon ) || 
							( from is WhiteDragon ) || 
							( from is ZombieDragon ) || 
							( from is NightWyrm ) || 
							( from is JungleWyrm ) || 
							( from is DesertWyrm ) || 
							( from is MountainWyrm ) || 
							( from is OnyxWyrm ) || 
							( from is EmeraldWyrm ) || 
							( from is AmethystWyrm ) || 
							( from is SapphireWyrm ) || 
							( from is GarnetWyrm ) || 
							( from is TopazWyrm ) || 
							( from is RubyWyrm ) || 
							( from is SpinelWyrm ) || 
							( from is Wyrms ) || 
							( from is QuartzWyrm ) || 
							( from is WhiteWyrm ) )
					{
						if ( 95 < Utility.Random( 100 ) ){ DragonSkin goods = new DragonSkin(); goods.Amount = Utility.RandomMinMax( 1, 3 ); c.DropItem(goods); }
						if ( 95 < Utility.Random( 100 ) ){ DragonBlood goods = new DragonBlood(); goods.Amount = Utility.RandomMinMax( 1, 3 ); c.DropItem(goods); }
						if ( ( 75 < Utility.Random( 100 ) ) && ( from is AshDragon || from is BottleDragon || from is CaddelliteDragon || from is CrystalDragon || from is ElderDragon || from is RadiationDragon || from is VoidDragon ) )
						{
							DragonTooth goods = new DragonTooth(); c.DropItem(goods);
						}
						if ( ( 50 < Utility.Random( 100 ) ) && ( 
							from is PrimevalAbysmalDragon || 
							from is PrimevalAmberDragon || 
							from is PrimevalBlackDragon || 
							from is PrimevalDragon || 
							from is PrimevalFireDragon || 
							from is PrimevalGreenDragon || 
							from is PrimevalNightDragon || 
							from is PrimevalRedDragon || 
							from is PrimevalRoyalDragon || 
							from is PrimevalRunicDragon || 
							from is PrimevalSeaDragon || 
							from is PrimevalSilverDragon || 
							from is PrimevalStygianDragon || 
							from is PrimevalVolcanicDragon || 
							from is ReanimatedDragon || 
							from is VampiricDragon  ) )
						{
							DragonTooth goods = new DragonTooth(); goods.Amount = Utility.RandomMinMax( 1, 2 ); c.DropItem(goods);
						}
						if ( from is DragonKing ){ DragonTooth goods = new DragonTooth(); goods.Amount = Utility.RandomMinMax( 1, 4 ); c.DropItem(goods); }
					}
					if ( 	( from is Lich ) || 
							( from is LichLord ) || 
							( from is Nazghoul ) || 
							( from is AncientLich ) || 
							( from is UndeadDruid ) || 
							( from is TitanLich ) || 
							( from is DemiLich ) || 
							( from is Surtaz ) )
					{
						if ( from is AncientLich ){ LichDust goods = new LichDust(); goods.Amount = Utility.RandomMinMax( 1, 3 ); c.DropItem(goods); }
						else if ( 80 < Utility.Random( 100 ) ){ LichDust goods = new LichDust(); goods.Amount = Utility.RandomMinMax( 1, 3 ); c.DropItem(goods); }
					}
					if ( 	( from is MonstrousSpider ) || 
							( from is WhiteDragon ) || 
							( from is BlackDragon ) || 
							( from is PrimevalFireDragon ) || 
							( from is PrimevalGreenDragon ) || 
							( from is PrimevalNightDragon ) || 
							( from is PrimevalRedDragon ) || 
							( from is PrimevalRoyalDragon ) || 
							( from is PrimevalRunicDragon ) || 
							( from is PrimevalSeaDragon ) || 
							( from is ReanimatedDragon ) || 
							( from is VampiricDragon ) || 
							( from is PrimevalAbysmalDragon ) || 
							( from is PrimevalAmberDragon ) || 
							( from is PrimevalBlackDragon ) || 
							( from is PrimevalDragon ) || 
							( from is PrimevalSilverDragon ) || 
							( from is PrimevalVolcanicDragon ) || 
							( from is PrimevalStygianDragon ) || 
							( from is BlueDragon ) || 
							( from is SlasherOfVoid ) || 
							( from is Dragon ) || 
							( from is Dragons && from.Body == 59 ) || 
							( from is Wyrms && from.Body == 12 ) || 
							( from is GreenDragon ) || 
							( from is MetalDragon ) || 
							( from is LavaDragon ) || 
							( from is BottleDragon ) || 
							( from is IceDragon ) || 
							( from is CaddelliteDragon ) || 
							( from is WhiteWyrm ) || 
							( from is GemDragon ) || 
							( from is NightWyrm ) || 
							( from is RadiationDragon ) || 
							( from is CrystalDragon ) || 
							( from is VoidDragon ) || 
							( from is ElderDragon ) || 
							( from is JungleWyrm ) || 
							( from is DesertWyrm ) || 
							( from is MountainWyrm ) || 
							( from is AntLion ) || 
							( from is MetalBeetle ) || 
							( from is OnyxWyrm ) || 
							( from is EmeraldWyrm ) || 
							( from is AmethystWyrm ) || 
							( from is SapphireWyrm ) || 
							( from is GarnetWyrm ) || 
							( from is TopazWyrm ) || 
							( from is RubyWyrm ) || 
							( from is SpinelWyrm ) || 
							( from is QuartzWyrm ) || 
							( from is ShadowWyrm ) || 
							( from is AncientWyrm ) || 
							( from is DragonKing ) || 
							( from is VolcanicDragon ) )
					{
						int nBodyChance = (int)(from.Fame * 0.002)+2;
						int nBodyLevel = (int)(from.Fame * 0.0006);
							if (nBodyLevel > 10){ nBodyLevel = 10; }
							if (nBodyLevel < 1){ nBodyLevel = 1; }
						if ( nBodyChance > Utility.Random( 100 ) )
						{
							CorpseChest corpseBox = new CorpseChest(nBodyLevel);
							c.DropItem(corpseBox);
						}
					}

					SlayerEntry wizardrykiller = SlayerGroup.GetEntryByName( SlayerName.WizardSlayer );
					if ( wizardrykiller.Slays(from) )
					{
						if ( Utility.Random( 4 ) == 1 )
						{
							if ( MyServerSettings.GetUnidentifiedChance() >= Utility.RandomMinMax( 1, 100 ) ){ Item wand = new UnknownWand(); c.DropItem(wand); }
							else{ Item wand = Loot.RandomWand(); Server.Misc.MaterialInfo.ColorMetal( wand, 0 ); string wandOwner = ""; if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ wandOwner = Server.LootPackEntry.MagicWandOwner() + " "; } wand.Name = wandOwner + wand.Name; c.DropItem(wand); }
						}

						if ( Utility.Random( 20 ) == 1 )
						{
							c.DropItem( Server.Items.DungeonLoot.RandomRuneMagic() );
						}

						if ( Utility.Random( 4 ) == 1 && Server.Items.BaseWizardStaff.HasStaff( killer ) )
						{
							c.DropItem( new MageEye( Utility.RandomMinMax( 1, Server.Misc.IntelligentAction.GetCreatureLevel( from ) ) ) );
						}
					}

					// HIGH SEAS /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
					if (	( from is DeepSeaSerpent ) || ( from is SeaDragon ) || ( from is GiantSquid ) || ( from is DeepSeaDragon ) || ( from is Megalodon ) || 
							( from is Jormungandr ) || ( from is Shark ) || ( from is GreatWhite ) || ( from is SeaSerpent ) || ( from is Kraken ) || ( from is Leviathan ) )
					{
						int nBodyChance = (int)(from.Fame * 0.002)+2;
						int nBodyLevel = (int)(from.Fame * 0.0006);
							if (nBodyLevel > 10){ nBodyLevel = 10; }
							if (nBodyLevel < 1){ nBodyLevel = 1; }
						if ( nBodyChance > Utility.Random( 100 ) )
						{
							CorpseSailor corpseBox = new CorpseSailor(nBodyLevel);
							c.DropItem(corpseBox);
						}
					}

					SlayerEntry neptune = SlayerGroup.GetEntryByName( SlayerName.NeptunesBane );

					if ( neptune.Slays(from) && from.Fame >= 11500 )
					{
						int nWeedChance = (int)(from.Fame * 0.002)+2;
						if ( nWeedChance > Utility.Random( 100 ) )
						{
							EnchantedSeaweed goods = new EnchantedSeaweed();
							goods.Amount = Utility.RandomMinMax( 1, 3 );
							c.DropItem(goods);
						}

						int nPearlChance = (int)(from.Fame * 0.001)+1;
						if ( nPearlChance > Utility.Random( 100 ) )
						{
							c.DropItem( new MysticalPearl() );
						}
					}
					//-------------------------------------------------------------------------

					if ( 	( neptune.Slays(from) && from.WhisperHue == 999 ) || ( from is Jormungandr ) ||
							Server.Misc.Worlds.IsSeaDungeon( from.Location, from.Map ) ) 
							// ONLY SEA CREATURES ON THE HIGH SEAS DROP from STUFF
					{
						int nWreckChance = (int)(from.Fame * 0.002)+2;
						if ( nWreckChance > Utility.Random( 100 ) )
						{
							HighSeasRelic goods = new HighSeasRelic();
							goods.RelicGoldValue = goods.RelicGoldValue + (int)(from.RawStatTotal/3);
							c.DropItem(goods);
						}
						int nBottleChance = (int)(from.Fame * 0.002)+1;
						if ( nBottleChance > Utility.Random( 100 ) )
						{
							int messageLevel = 0;

							if ( from.Fame < 2500 )
								messageLevel = 1;
							else if ( from.Fame < 5000 )
								messageLevel = 2;
							else if ( from.Fame < 10000 )
								messageLevel = 3;
							else
								messageLevel = 4;

							if ( 20 > Utility.Random( 100 ) ) { messageLevel = 0; } // 20% CHANCE FOR A RANDOM LEVEL BOTTLE

							Item message = new MessageInABottle( killer.Map, messageLevel, killer.Location, killer.X, killer.Y );
							c.DropItem(message);
						}
						int nBoxChance = (int)(from.Fame * 0.001)-1;
						if ( nBoxChance > Utility.Random( 100 ) && !(from is StormGiant) ) // STORM GIANTS ALREADY DROP A BOX
						{
							int boxLevel = 0;

							if ( from.Fame < 2500 )
								boxLevel = 1;
							else if ( from.Fame < 5000 )
								boxLevel = 2;
							else if ( from.Fame < 10000 )
								boxLevel = 3;
							else if ( from.Fame < 20000 )
								boxLevel = 4;
							else
								boxLevel = 5;

							LootChest MyChest = new LootChest(boxLevel);
							MyChest.Name = "sea chest";
							MyChest.Hue = 0;
							MyChest.ItemID = Utility.RandomList( 0x52E2, 0x52E3, 0x507E, 0x507F, 0x4910, 0x4911, 0x3332, 0x3333, 0x4FF4, 0x4FF5 );
							c.DropItem( MyChest );
						}
					}
				}
			}
		}
	}
}