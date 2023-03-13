using System;
using Server;
using Server.Misc;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using System.Collections;
using System.Collections.Generic;
using Server.Items;

namespace Server.Misc
{
    class BuildQuests
    {
		public static void SearchCreate()
		{
			Item pedestal = new SearchBase(0);
				pedestal.Delete();

			Item prisoner = new Prisoner();
				prisoner.Delete();

			ArrayList SBtargets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			if ( item is SearchBase || ( item is Prisoner ) )
			{
				SBtargets.Add( item );
			}
			for ( int i = 0; i < SBtargets.Count; ++i )
			{
				Item item = ( Item )SBtargets[ i ];
				item.Delete();
			}

			int dungeons = 90;
			int area = 0;

			while ( dungeons > 0 )
			{
				dungeons--;
				area++;

				if ( area == 1 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Mage Mansion
				else if ( area == 2 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Isle of the Lich
				else if ( area == 3 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Altar of the Blood God
				else if ( area == 4 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the City of the Dead
				else if ( area == 5 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Mausoleum
				else if ( area == 6 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Valley of Dark Druids
				else if ( area == 7 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Vordo's Castle
				else if ( area == 8 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Crypts of Kuldar
				else if ( area == 9 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Caverns of Poseidon
				else if ( area == 10 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Zealan Tombs
				else if ( area == 11 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Argentrock Castle
				else if ( area == 12 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Hall of the Mountain King
				else if ( area == 13 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Depths of Carthax Lake
				else if ( area == 14 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Morgaelin's Inferno
				else if ( area == 15 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Tower of Brass
				else if ( area == 16 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Kuldara Sewers
				else if ( area == 17 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Daemon's Crag
				else if ( area == 18 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Ratmen Lair
				else if ( area == 19 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Ancient Pyramid
				else if ( area == 20 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Exodus
				else if ( area == 21 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Cave of Banished Mages
				else if ( area == 22 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Clues
				else if ( area == 23 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dardin's Pit
				else if ( area == 24 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Doom
				else if ( area == 25 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Fires of Hell
				else if ( area == 26 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Mines of Morinia
				else if ( area == 27 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Perinian Depths
				else if ( area == 28 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Dungeon of Time Awaits
				else if ( area == 29 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Pirate Cave
				else if ( area == 30 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Dragon's Maw
				else if ( area == 31 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Cave of the Zuluu
				else if ( area == 32 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Vault of the Black Knight
				else if ( area == 33 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Undersea Pass
				else if ( area == 34 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Castle of Dracula
				else if ( area == 35 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Crypts of Dracula
				else if ( area == 36 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Lodoria Catacombs
				else if ( area == 37 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Covetous
				else if ( area == 38 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Deceit
				else if ( area == 39 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Despise
				else if ( area == 40 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Destard
				else if ( area == 41 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the City of Embers
				else if ( area == 42 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Hythloth
				else if ( area == 43 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Frozen Hells
				else if ( area == 44 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Ice Fiend Lair
				else if ( area == 45 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Halls of Undermountain
				else if ( area == 46 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Shame
				else if ( area == 47 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Terathan Keep
				else if ( area == 48 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Volcanic Cave
				else if ( area == 49 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Wrong
				else if ( area == 50 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Blood Temple
				else if ( area == 51 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Ice Queen Fortress
				else if ( area == 52 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon of the Mad Archmage
				else if ( area == 53 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon of the Lich King
				else if ( area == 54 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Halls of Ogrimar
				else if ( area == 55 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Ratmen Mines
				else if ( area == 56 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Rock
				else if ( area == 57 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Storm Giant Lair
				else if ( area == 58 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Corrupt Pass
				else if ( area == 59 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Tombs
				else if ( area == 60 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Ancient Prison
				else if ( area == 61 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Cave of Fire
				else if ( area == 62 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Cave of Souls
				else if ( area == 63 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Ankh
				else if ( area == 64 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Bane
				else if ( area == 65 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Hate
				else if ( area == 66 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Scorn
				else if ( area == 67 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Torment
				else if ( area == 68 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Vile
				else if ( area == 69 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Wicked
				else if ( area == 70 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Dungeon Wrath
				else if ( area == 71 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Flooded Temple
				else if ( area == 72 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Gargoyle Crypts
				else if ( area == 73 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Serpent Sanctum
				else if ( area == 74 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Tomb of the Fallen Wizard
				else if ( area == 75 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Vordo's Dungeon
				else if ( area == 76 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Forgotten Halls
				else if ( area == 77 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Ancient Elven Mine
				else if ( area == 78 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Tomb of Kazibal
				else if ( area == 79 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Scurvy Reef
				else if ( area == 80 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // Stonegate Castle
				else if ( area == 81 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Undersea Castle
				else if ( area == 82 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Catacombs of Azerok
				else if ( area == 83 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Azure Castle
				else if ( area == 84 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Glacial Scar
				else if ( area == 85 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Temple of Osirus
				else if ( area == 86 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Stygian Abyss
				else if ( area == 87 ){ pedestal = new SearchBase(0); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Sanctum of Saltmarsh
				else if ( area == 88 ){ pedestal = new SearchBase(1); MoveQuestPedestals( pedestal, area ); prisoner = new Prisoner(); MoveQuestPedestals( prisoner, area ); } // the Ancient Sky Ship
			}
		}

		public static void MoveQuestPedestals( Item item, int area )
		{
			Point3D loc = new Point3D(0, 0, 0);
			Map map = null;

			if ( area == 1 ){ loc = new Point3D(792, 312, 66); map = Map.TerMur; }

			else if ( area == 2 ){ loc = new Point3D(1057, 434, 88); map = Map.TerMur; }

			else if ( area == 3 ){ loc = new Point3D(1143, 971, 75); map = Map.Tokuno; }

			else if ( area == 4 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the City of the Dead"
			{
				case 1:	loc = new Point3D(5683, 3261, 0); map = Map.Trammel; break;
				case 2:	loc = new Point3D(5669, 3291, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(5758, 3331, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(5754, 3245, 0); map = Map.Trammel; break;
			}
			} else if ( area == 5 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Mausoleum"
			{
				case 1:	loc = new Point3D(3992, 3295, 20); map = Map.Trammel; break;
				case 2:	loc = new Point3D(3882, 3282, 40); map = Map.Trammel; break;
				case 3:	loc = new Point3D(3831, 3363, 40); map = Map.Trammel; break;
				case 4:	loc = new Point3D(3964, 3453, 0); map = Map.Trammel; break;
			}
			} else if ( area == 6 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Valley of Dark Druids"
			{
				case 1:	loc = new Point3D(6828, 200, 5); map = Map.Trammel; break;
				case 2:	loc = new Point3D(6790, 146, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(6783, 184, 50); map = Map.Trammel; break;
				case 4:	loc = new Point3D(6792, 209, 30); map = Map.Trammel; break;
			}
			} else if ( area == 7 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Vordo's Castle"
			{
				case 1:	loc = new Point3D(6893, 2866, 72); map = Map.Trammel; break;
				case 2:	loc = new Point3D(6896, 2909, 72); map = Map.Trammel; break;
				case 3:	loc = new Point3D(6910, 2907, 50); map = Map.Trammel; break;
				case 4:	loc = new Point3D(6905, 2858, 50); map = Map.Trammel; break;
			}
			} else if ( area == 8 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Crypts of Kuldar"
			{
				case 1:	loc = new Point3D(6508, 2940, 55); map = Map.Trammel; break;
				case 2:	loc = new Point3D(6493, 2879, 45); map = Map.Trammel; break;
				case 3:	loc = new Point3D(6547, 2842, 50); map = Map.Trammel; break;
				case 4:	loc = new Point3D(6591, 2884, 45); map = Map.Trammel; break;
			}
			} else if ( area == 9 ){ switch ( Utility.RandomMinMax( 1, 6 ) ) // "the Caverns of Poseidon"
			{
				case 1:	loc = new Point3D(5629, 972, 0); map = Map.Trammel; break;
				case 2:	loc = new Point3D(5712, 1689, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(5839, 1743, 5); map = Map.Trammel; break;
				case 4:	loc = new Point3D(5996, 1723, 5); map = Map.Trammel; break;
				case 5:	loc = new Point3D(5305, 2058, 0); map = Map.Trammel; break;
				case 6:	loc = new Point3D(5366, 2073, 0); map = Map.Trammel; break;
			}
			} else if ( area == 10 ){ switch ( Utility.RandomMinMax( 1, 8 ) ) // "the Zealan Tombs"
			{
				case 1:	loc = new Point3D(6493, 672, 10); map = Map.Felucca; break;
				case 2:	loc = new Point3D(6436, 753, -20); map = Map.Felucca; break;
				case 3:	loc = new Point3D(6963, 2431, 0); map = Map.Felucca; break;
				case 4:	loc = new Point3D(7010, 2463, 0); map = Map.Felucca; break;
				case 5:	loc = new Point3D(6971, 2528, 0); map = Map.Felucca; break;
				case 6:	loc = new Point3D(6700, 2395, 0); map = Map.Felucca; break;
				case 7:	loc = new Point3D(7025, 2253, 0); map = Map.Felucca; break;
				case 8:	loc = new Point3D(6804, 2332, 5); map = Map.Felucca; break;
			}
			} else if ( area == 11 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Argentrock Castle"
			{
				case 1:	loc = new Point3D(6085, 475, 5); map = Map.Felucca; break;
				case 2:	loc = new Point3D(6014, 684, 0); map = Map.Felucca; break;
				case 3:	loc = new Point3D(6044, 675, 0); map = Map.Felucca; break;
				case 4:	loc = new Point3D(6225, 638, -20); map = Map.Felucca; break;
			}
			} else if ( area == 12 ){ switch ( Utility.RandomMinMax( 1, 5 ) ) // "the Hall of the Mountain King"
			{
				case 1:	loc = new Point3D(7006, 1804, 0); map = Map.Felucca; break;
				case 2:	loc = new Point3D(6885, 1979, -45); map = Map.Felucca; break;
				case 3:	loc = new Point3D(6335, 920, 25); map = Map.Felucca; break;
				case 4:	loc = new Point3D(6413, 1261, 0); map = Map.Felucca; break;
				case 5:	loc = new Point3D(6481, 1246, 0); map = Map.Felucca; break;
			}
			} else if ( area == 13 ){ switch ( Utility.RandomMinMax( 1, 6 ) ) // "the Depths of Carthax Lake"
			{
				case 1:	loc = new Point3D(5868, 1688, 45); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5936, 1653, -5); map = Map.Felucca; break;
				case 3:	loc = new Point3D(5442, 2296, 0); map = Map.Felucca; break;
				case 4:	loc = new Point3D(5488, 2279, 0); map = Map.Felucca; break;
				case 5:	loc = new Point3D(5352, 2246, 5); map = Map.Felucca; break;
				case 6:	loc = new Point3D(5336, 2351, -10); map = Map.Felucca; break;
			}
			} else if ( area == 14 ){ switch ( Utility.RandomMinMax( 1, 3 ) ) // "Morgaelin's Inferno"
			{
				case 1:	loc = new Point3D(6917, 1084, 15); map = Map.Felucca; break;
				case 2:	loc = new Point3D(6902, 1180, -78); map = Map.Felucca; break;
				case 3:	loc = new Point3D(6037, 2143, 30); map = Map.Felucca; break;
			}
			} else if ( area == 15 ){ switch ( Utility.RandomMinMax( 1, 14 ) ) // "the Tower of Brass"
			{
				case 1:		loc = new Point3D(486, 3818, 56); map = Map.Trammel; break;
				case 2:		loc = new Point3D(6407, 3088, 5); map = Map.Trammel; break;
				case 3:		loc = new Point3D(6799, 3211, -20); map = Map.Trammel; break;
				case 4:		loc = new Point3D(6857, 3196, 5); map = Map.Trammel; break;
				case 5:		loc = new Point3D(6779, 3122, 3); map = Map.Trammel; break;
				case 6:		loc = new Point3D(6274, 3444, 30); map = Map.Trammel; break;
				case 7:		loc = new Point3D(6570, 3381, 0); map = Map.Trammel; break;
				case 8:		loc = new Point3D(6898, 3337, 40); map = Map.Trammel; break;
				case 9:		loc = new Point3D(6527, 3573, 0); map = Map.Trammel; break;
				case 10:	loc = new Point3D(6952, 3582, 0); map = Map.Trammel; break;
				case 11:	loc = new Point3D(6937, 3536, 20); map = Map.Trammel; break;
				case 12:	loc = new Point3D(6954, 3499, 40); map = Map.Trammel; break;
				case 13:	loc = new Point3D(6831, 3856, 5); map = Map.Trammel; break;
				case 14:	loc = new Point3D(6269, 3938, 0); map = Map.Trammel; break;
			}
			} else if ( area == 16 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Kuldara Sewers"
			{
				case 1:	loc = new Point3D(6226, 2902, 10); map = Map.Trammel; break;
				case 2:	loc = new Point3D(6207, 2991, 5); map = Map.Trammel; break;
				case 3:	loc = new Point3D(6252, 3011, -25); map = Map.Trammel; break;
				case 4:	loc = new Point3D(6333, 2826, 6); map = Map.Trammel; break;
			}
			} else if ( area == 17 ){ switch ( Utility.RandomMinMax( 1, 3 ) ) // "the Daemon's Crag"
			{
				case 1:	loc = new Point3D(6376, 2091, -5); map = Map.Felucca; break;
				case 2:	loc = new Point3D(6241, 2177, -59); map = Map.Felucca; break;
				case 3:	loc = new Point3D(5919, 2195, 0); map = Map.Felucca; break;
			}
			} else if ( area == 18 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Ratmen Lair"
			{
				case 1:	loc = new Point3D(2719, 3725, 0); map = Map.Trammel; break;
				case 2:	loc = new Point3D(2754, 3784, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(2796, 3753, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(2747, 3750, 0); map = Map.Trammel; break;
			}
			} else if ( area == 19 ){ switch ( Utility.RandomMinMax( 1, 7 ) ) // "the Ancient Pyramid"
			{
				case 1:	loc = new Point3D(5359, 918, 0); map = Map.Trammel; break;
				case 2:	loc = new Point3D(5288, 901, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(5323, 955, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(5368, 767, 0); map = Map.Trammel; break;
				case 5:	loc = new Point3D(5309, 784, 0); map = Map.Trammel; break;
				case 6:	loc = new Point3D(5243, 761, 0); map = Map.Trammel; break;
				case 7:	loc = new Point3D(5302, 724, 0); map = Map.Trammel; break;
			}
			} else if ( area == 20 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Exodus"
			{
				case 1:	loc = new Point3D(5937, 584, 0); map = Map.Trammel; break;
				case 2:	loc = new Point3D(5975, 611, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(5939, 696, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(5883, 597, 0); map = Map.Trammel; break;
			}
			} else if ( area == 21 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Cave of Banished Mages"
			{
				case 1:	loc = new Point3D(230, 3509, 20); map = Map.Trammel; break;
				case 2:	loc = new Point3D(231, 3486, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(124, 3767, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(124, 3462, 0); map = Map.Trammel; break;
			}
			} else if ( area == 22 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Clues"
			{
				case 1:	loc = new Point3D(5905, 2120, 0); map = Map.Trammel; break;
				case 2:	loc = new Point3D(5960, 2222, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(5608, 2204, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(5611, 2120, 0); map = Map.Trammel; break;
			}
			} else if ( area == 23 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dardin's Pit"
			{
				case 1:	loc = new Point3D(5660, 411, 0); map = Map.Trammel; break;
				case 2:	loc = new Point3D(5641, 382, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(5584, 419, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(5501, 420, 0); map = Map.Trammel; break;
			}
			} else if ( area == 24 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Doom"
			{
				case 1:	loc = new Point3D(5374, 297, 0); map = Map.Trammel; break;
				case 2:	loc = new Point3D(5321, 286, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(5276, 297, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(5263, 235, 0); map = Map.Trammel; break;
			}
			} else if ( area == 25 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Fires of Hell"
			{
				case 1:	loc = new Point3D(5609, 1240, 1); map = Map.Trammel; break;
				case 2:	loc = new Point3D(5482, 1233, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(5311, 1376, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(5255, 1400, 0); map = Map.Trammel; break;
			}
			} else if ( area == 26 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Mines of Morinia"
			{
				case 1:	loc = new Point3D(5685, 1462, 0); map = Map.Trammel; break;
				case 2:	loc = new Point3D(5705, 1538, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(5623, 1512, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(5633, 1614, 0); map = Map.Trammel; break;
			}
			} else if ( area == 27 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Perinian Depths"
			{
				case 1:	loc = new Point3D(5912, 475, 0); map = Map.Trammel; break;
				case 2:	loc = new Point3D(5975, 367, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(5894, 386, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(5859, 453, 0); map = Map.Trammel; break;
			}
			} else if ( area == 28 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Dungeon of Time Awaits"
			{
				case 1:	loc = new Point3D(5588, 897, 0); map = Map.Trammel; break;
				case 2:	loc = new Point3D(5537, 879, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(5498, 849, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(5566, 795, 0); map = Map.Trammel; break;
			}
			} else if ( area == 29 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Pirate Cave"
			{
				case 1:	loc = new Point3D(5459, 1676, 0); map = Map.Trammel; break;
				case 2:	loc = new Point3D(5476, 1675, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(5473, 1697, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(5436, 1676, 0); map = Map.Trammel; break;
			}
			} else if ( area == 30 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Dragon's Maw"
			{
				case 1:	loc = new Point3D(4839, 3852, 5); map = Map.Trammel; break;
				case 2:	loc = new Point3D(4755, 3692, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(4351, 3904, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(4476, 3935, 0); map = Map.Trammel; break;
			}
			} else if ( area == 31 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Cave of the Zuluu"
			{
				case 1:	loc = new Point3D(6982, 3840, 5); map = Map.Trammel; break;
				case 2:	loc = new Point3D(6997, 3913, 25); map = Map.Trammel; break;
				case 3:	loc = new Point3D(6654, 3660, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(6241, 3256, 0); map = Map.Trammel; break;
			}
			} else if ( area == 32 ){ switch ( Utility.RandomMinMax( 1, 6 ) ) // "the Vault of the Black Knight"
			{
				case 1:	loc = new Point3D(6521, 537, 0); map = Map.Felucca; break;
				case 2:	loc = new Point3D(6626, 340, 0); map = Map.Felucca; break;
				case 3:	loc = new Point3D(6587, 159, 20); map = Map.Felucca; break;
				case 4:	loc = new Point3D(6235, 260, 0); map = Map.Felucca; break;
				case 5:	loc = new Point3D(6270, 411, 40); map = Map.Felucca; break;
				case 6:	loc = new Point3D(6289, 561, 0); map = Map.Felucca; break;
			}
			} else if ( area == 33 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Undersea Pass"
			{
				case 1:	loc = new Point3D(6363, 3864, 10); map = Map.Felucca; break;
				case 2:	loc = new Point3D(6323, 3425, 10); map = Map.Felucca; break;
				case 3:	loc = new Point3D(6310, 3273, 10); map = Map.Felucca; break;
				case 4:	loc = new Point3D(6310, 3346, 10); map = Map.Felucca; break;
			}
			} else if ( area == 34 ){ switch ( Utility.RandomMinMax( 1, 8 ) ) // "the Castle of Dracula"
			{
				case 1:	loc = new Point3D(6852, 1588, 0); map = Map.Felucca; break;
				case 2:	loc = new Point3D(6966, 1499, 0); map = Map.Felucca; break;
				case 3:	loc = new Point3D(6803, 1539, 0); map = Map.Felucca; break;
				case 4:	loc = new Point3D(6876, 1582, 0); map = Map.Felucca; break;
				case 5:	loc = new Point3D(6971, 1619, 0); map = Map.Felucca; break;
				case 6:	loc = new Point3D(6877, 1468, 0); map = Map.Felucca; break;
				case 7:	loc = new Point3D(6776, 1636, 0); map = Map.Felucca; break;
				case 8:	loc = new Point3D(6997, 1639, 0); map = Map.Felucca; break;
			}
			} else if ( area == 35 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Crypts of Dracula"
			{
				case 1:	loc = new Point3D(5737, 2835, 0); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5734, 2792, 0); map = Map.Felucca; break;
				case 3:	loc = new Point3D(5766, 2722, 0); map = Map.Felucca; break;
				case 4:	loc = new Point3D(5826, 2678, 0); map = Map.Felucca; break;
			}
			} else if ( area == 36 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Lodoria Catacombs"
			{
				case 1:	loc = new Point3D(5501, 1804, 0); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5555, 1888, 0); map = Map.Felucca; break;
				case 3:	loc = new Point3D(5450, 1824, 0); map = Map.Felucca; break;
				case 4:	loc = new Point3D(5608, 1832, 0); map = Map.Felucca; break;
			}
			} else if ( area == 37 ){ switch ( Utility.RandomMinMax( 1, 6 ) ) // "Dungeon Covetous"
			{
				case 1:	loc = new Point3D(5543, 2032, 0); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5510, 1981, 0); map = Map.Felucca; break;
				case 3:	loc = new Point3D(5431, 2021, 0); map = Map.Felucca; break;
				case 4:	loc = new Point3D(5446, 1969, 0); map = Map.Felucca; break;
				case 5:	loc = new Point3D(5592, 2493, 40); map = Map.Felucca; break;
				case 6:	loc = new Point3D(5769, 2569, -20); map = Map.Felucca; break;
			}
			} else if ( area == 38 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Deceit"
			{
				case 1:	loc = new Point3D(5309, 648, 0); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5316, 735, 0); map = Map.Felucca; break;
				case 3:	loc = new Point3D(5264, 664, 0); map = Map.Felucca; break;
				case 4:	loc = new Point3D(5144, 712, 0); map = Map.Felucca; break;
			}
			} else if ( area == 39 ){ switch ( Utility.RandomMinMax( 1, 6 ) ) // "Dungeon Despise"
			{
				case 1:	loc = new Point3D(5432, 847, 45); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5514, 935, 20); map = Map.Felucca; break;
				case 3:	loc = new Point3D(5553, 815, 47); map = Map.Felucca; break;
				case 4:	loc = new Point3D(5539, 916, 29); map = Map.Felucca; break;
				case 5:	loc = new Point3D(5500, 2426, 10); map = Map.Felucca; break;
				case 6:	loc = new Point3D(5175, 2428, 45); map = Map.Felucca; break;
			}
			} else if ( area == 40 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Destard"
			{
				case 1:	loc = new Point3D(5192, 1007, 0); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5133, 833, 0); map = Map.Felucca; break;
				case 3:	loc = new Point3D(5253, 913, -23); map = Map.Felucca; break;
				case 4:	loc = new Point3D(5259, 790, 0); map = Map.Felucca; break;
			}
			} else if ( area == 41 ){ switch ( Utility.RandomMinMax( 1, 3 ) ) // "the City of Embers"
			{
				case 1:	loc = new Point3D(5726, 1296, 2); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5649, 1406, 0); map = Map.Felucca; break;
				case 3:	loc = new Point3D(5680, 1432, 0); map = Map.Felucca; break;
			}
			} else if ( area == 42 ){ switch ( Utility.RandomMinMax( 1, 2 ) ) // "Dungeon Hythloth"
			{
				case 1:		loc = new Point3D(6112, 222, 22); map = Map.Felucca; break;
				case 2:		loc = new Point3D(6105, 32, 27); map = Map.Felucca; break;
				case 3:		loc = new Point3D(6048, 157, 0); map = Map.Felucca; break;
				case 4:		loc = new Point3D(5912, 233, 44); map = Map.Felucca; break;
				case 5:		loc = new Point3D(5964, 80, 0); map = Map.Felucca; break;
				case 6:		loc = new Point3D(6087, 168, 0); map = Map.Felucca; break;
				case 7:		loc = new Point3D(6111, 90, 0); map = Map.Felucca; break;
				case 8:		loc = new Point3D(6058, 51, 0); map = Map.Felucca; break;
				case 9:		loc = new Point3D(5958, 220, 22); map = Map.Felucca; break;
				case 10:	loc = new Point3D(5984, 149, 0); map = Map.Felucca; break;
				case 11:	loc = new Point3D(5997, 56, 22); map = Map.Felucca; break;
				case 12:	loc = new Point3D(5936, 96, 22); map = Map.Felucca; break;
			}
			} else if ( area == 43 ){ switch ( Utility.RandomMinMax( 1, 2 ) ) // "the Frozen Hells"
			{
				case 1:	loc = new Point3D(5705, 169, -4); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5672, 176, -6); map = Map.Felucca; break;
			}
			} else if ( area == 44 ){ switch ( Utility.RandomMinMax( 1, 2 ) ) // "the Ice Fiend Lair"
			{
				case 1:	loc = new Point3D(5681, 332, 0); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5656, 302, 0); map = Map.Felucca; break;
			}
			} else if ( area == 45 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Halls of Undermountain"
			{
				case 1:	loc = new Point3D(5333, 472, 0); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5325, 393, 0); map = Map.Felucca; break;
				case 3:	loc = new Point3D(5245, 397, 0); map = Map.Felucca; break;
				case 4:	loc = new Point3D(5235, 439, 0); map = Map.Felucca; break;
			}
			} else if ( area == 46 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Shame"
			{
				case 1:	loc = new Point3D(5818, 77, 0); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5851, 106, 10); map = Map.Felucca; break;
				case 3:	loc = new Point3D(5661, 112, 11); map = Map.Felucca; break;
				case 4:	loc = new Point3D(5434, 180, 0); map = Map.Felucca; break;
			}
			} else if ( area == 47 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Terathan Keep"
			{
				case 1:	loc = new Point3D(5282, 1551, 0); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5128, 1584, 0); map = Map.Felucca; break;
				case 3:	loc = new Point3D(5144, 1713, 0); map = Map.Felucca; break;
				case 4:	loc = new Point3D(5338, 1770, -125); map = Map.Felucca; break;
			}
			} else if ( area == 48 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Volcanic Cave"
			{
				case 1:	loc = new Point3D(5988, 3426, 0); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5934, 3403, 0); map = Map.Felucca; break;
				case 3:	loc = new Point3D(5862, 3424, 0); map = Map.Felucca; break;
				case 4:	loc = new Point3D(5996, 3511, 0); map = Map.Felucca; break;
			}
			} else if ( area == 49 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Wrong"
			{
				case 1:	loc = new Point3D(5547, 1294, 0); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5419, 1311, 0); map = Map.Felucca; break;
				case 3:	loc = new Point3D(5453, 1360, 5); map = Map.Felucca; break;
				case 4:	loc = new Point3D(5448, 1440, 0); map = Map.Felucca; break;
			}
			} else if ( area == 50 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Blood Temple"
			{
				case 1:	loc = new Point3D(758, 2526, -28); map = Map.TerMur; break;
				case 2:	loc = new Point3D(697, 2549, -28); map = Map.TerMur; break;
				case 3:	loc = new Point3D(779, 2571, -28); map = Map.TerMur; break;
				case 4:	loc = new Point3D(723, 2624, -28); map = Map.TerMur; break;
			}
			} else if ( area == 51 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Ice Queen Fortress"
			{
				case 1:	loc = new Point3D(439, 2788, 22); map = Map.TerMur; break;
				case 2:	loc = new Point3D(267, 2762, 44); map = Map.TerMur; break;
				case 3:	loc = new Point3D(341, 2768, 22); map = Map.TerMur; break;
				case 4:	loc = new Point3D(296, 2851, 22); map = Map.TerMur; break;
			}
			} else if ( area == 52 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon of the Mad Archmage"
			{
				case 1:	loc = new Point3D(768, 1921, -28); map = Map.TerMur; break;
				case 2:	loc = new Point3D(649, 1942, -28); map = Map.TerMur; break;
				case 3:	loc = new Point3D(612, 2003, -29); map = Map.TerMur; break;
				case 4:	loc = new Point3D(552, 2039, -28); map = Map.TerMur; break;
			}
			} else if ( area == 53 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon of the Lich King"
			{
				case 1:	loc = new Point3D(346, 2143, -1); map = Map.TerMur; break;
				case 2:	loc = new Point3D(499, 2141, -1); map = Map.TerMur; break;
				case 3:	loc = new Point3D(510, 2337, 0); map = Map.TerMur; break;
				case 4:	loc = new Point3D(335, 2326, -1); map = Map.TerMur; break;
			}
			} else if ( area == 54 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Halls of Ogrimar"
			{
				case 1:	loc = new Point3D(1155, 2360, -28); map = Map.TerMur; break;
				case 2:	loc = new Point3D(937, 2411, -28); map = Map.TerMur; break;
				case 3:	loc = new Point3D(972, 2342, -28); map = Map.TerMur; break;
				case 4:	loc = new Point3D(1083, 2361, 2); map = Map.TerMur; break;
			}
			} else if ( area == 55 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Ratmen Mines"
			{
				case 1:	loc = new Point3D(988, 2185, -3); map = Map.TerMur; break;
				case 2:	loc = new Point3D(897, 2127, -28); map = Map.TerMur; break;
				case 3:	loc = new Point3D(898, 2187, -28); map = Map.TerMur; break;
				case 4:	loc = new Point3D(1030, 2140, 22); map = Map.TerMur; break;
			}
			} else if ( area == 56 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Rock"
			{
				case 1:	loc = new Point3D(641, 2327, -32); map = Map.TerMur; break;
				case 2:	loc = new Point3D(609, 2223, -32); map = Map.TerMur; break;
				case 3:	loc = new Point3D(704, 2178, -32); map = Map.TerMur; break;
				case 4:	loc = new Point3D(743, 2307, -32); map = Map.TerMur; break;
			}
			} else if ( area == 57 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Storm Giant Lair"
			{
				case 1:	loc = new Point3D(569, 2751, -28); map = Map.TerMur; break;
				case 2:	loc = new Point3D(619, 2742, -28); map = Map.TerMur; break;
				case 3:	loc = new Point3D(594, 2678, -28); map = Map.TerMur; break;
				case 4:	loc = new Point3D(621, 2717, -28); map = Map.TerMur; break;
			}
			} else if ( area == 58 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Corrupt Pass"
			{
				case 1:	loc = new Point3D(64, 2421, -28); map = Map.TerMur; break;
				case 2:	loc = new Point3D(11, 2420, -28); map = Map.TerMur; break;
				case 3:	loc = new Point3D(51, 2328, -30); map = Map.TerMur; break;
				case 4:	loc = new Point3D(147, 2201, -30); map = Map.TerMur; break;
			}
			} else if ( area == 59 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Tombs"
			{
				case 1:	loc = new Point3D(76, 2528, -28); map = Map.TerMur; break;
				case 2:	loc = new Point3D(124, 2560, -23); map = Map.TerMur; break;
				case 3:	loc = new Point3D(96, 2777, -28); map = Map.TerMur; break;
				case 4:	loc = new Point3D(24, 2669, -28); map = Map.TerMur; break;
			}
			} else if ( area == 60 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Ancient Prison"
			{
				case 1:	loc = new Point3D(1980, 475, 0); map = Map.Malas; break;
				case 2:	loc = new Point3D(2002, 554, 0); map = Map.Malas; break;
				case 3:	loc = new Point3D(1945, 525, 0); map = Map.Malas; break;
				case 4:	loc = new Point3D(1948, 387, 0); map = Map.Malas; break;
			}
			} else if ( area == 61 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Cave of Fire"
			{
				case 1:	loc = new Point3D(2039, 863, 0); map = Map.Malas; break;
				case 2:	loc = new Point3D(2061, 918, 0); map = Map.Malas; break;
				case 3:	loc = new Point3D(2106, 900, 0); map = Map.Malas; break;
				case 4:	loc = new Point3D(2151, 870, 0); map = Map.Malas; break;
			}
			} else if ( area == 62 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Cave of Souls"
			{
				case 1:	loc = new Point3D(2443, 186, 0); map = Map.Malas; break;
				case 2:	loc = new Point3D(2447, 156, 0); map = Map.Malas; break;
				case 3:	loc = new Point3D(2503, 162, 0); map = Map.Malas; break;
				case 4:	loc = new Point3D(2482, 87, 0); map = Map.Malas; break;
			}
			} else if ( area == 63 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Ankh"
			{
				case 1:	loc = new Point3D(2067, 179, 0); map = Map.Malas; break;
				case 2:	loc = new Point3D(2078, 205, 0); map = Map.Malas; break;
				case 3:	loc = new Point3D(2049, 202, 0); map = Map.Malas; break;
				case 4:	loc = new Point3D(2058, 46, 0); map = Map.Malas; break;
			}
			} else if ( area == 64 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Bane"
			{
				case 1:	loc = new Point3D(1924, 188, 2); map = Map.Malas; break;
				case 2:	loc = new Point3D(1970, 156, 2); map = Map.Malas; break;
				case 3:	loc = new Point3D(1968, 222, 0); map = Map.Malas; break;
				case 4:	loc = new Point3D(1909, 50, 0); map = Map.Malas; break;
			}
			} else if ( area == 65 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Hate"
			{
				case 1:	loc = new Point3D(2235, 506, 2); map = Map.Malas; break;
				case 2:	loc = new Point3D(2160, 479, 2); map = Map.Malas; break;
				case 3:	loc = new Point3D(2219, 398, 0); map = Map.Malas; break;
				case 4:	loc = new Point3D(2197, 390, 0); map = Map.Malas; break;
			}
			} else if ( area == 66 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Scorn"
			{
				case 1:	loc = new Point3D(2214, 849, 0); map = Map.Malas; break;
				case 2:	loc = new Point3D(2238, 862, 0); map = Map.Malas; break;
				case 3:	loc = new Point3D(2234, 812, 0); map = Map.Malas; break;
				case 4:	loc = new Point3D(2192, 843, 0); map = Map.Malas; break;
			}
			} else if ( area == 67 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Torment"
			{
				case 1:	loc = new Point3D(1978, 834, 0); map = Map.Malas; break;
				case 2:	loc = new Point3D(1976, 810, 0); map = Map.Malas; break;
				case 3:	loc = new Point3D(1933, 815, 0); map = Map.Malas; break;
				case 4:	loc = new Point3D(1935, 853, 0); map = Map.Malas; break;
			}
			} else if ( area == 68 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Vile"
			{
				case 1:	loc = new Point3D(2315, 502, 20); map = Map.Malas; break;
				case 2:	loc = new Point3D(2360, 498, 0); map = Map.Malas; break;
				case 3:	loc = new Point3D(2360, 393, 0); map = Map.Malas; break;
				case 4:	loc = new Point3D(2334, 403, 0); map = Map.Malas; break;
			}
			} else if ( area == 69 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Wicked"
			{
				case 1:	loc = new Point3D(2152, 167, 2); map = Map.Malas; break;
				case 2:	loc = new Point3D(2177, 185, 0); map = Map.Malas; break;
				case 3:	loc = new Point3D(2182, 239, 2); map = Map.Malas; break;
				case 4:	loc = new Point3D(2205, 165, 2); map = Map.Malas; break;
			}
			} else if ( area == 70 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Dungeon Wrath"
			{
				case 1:	loc = new Point3D(2343, 839, 0); map = Map.Malas; break;
				case 2:	loc = new Point3D(2299, 833, 2); map = Map.Malas; break;
				case 3:	loc = new Point3D(2299, 892, 0); map = Map.Malas; break;
				case 4:	loc = new Point3D(2332, 866, 2); map = Map.Malas; break;
			}
			} else if ( area == 71 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Flooded Temple"
			{
				case 1:	loc = new Point3D(2494, 838, 0); map = Map.Malas; break;
				case 2:	loc = new Point3D(2484, 875, 0); map = Map.Malas; break;
				case 3:	loc = new Point3D(2455, 874, 0); map = Map.Malas; break;
				case 4:	loc = new Point3D(2428, 718, 2); map = Map.Malas; break;
			}
			} else if ( area == 72 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Gargoyle Crypts"
			{
				case 1:	loc = new Point3D(2089, 503, 0); map = Map.Malas; break;
				case 2:	loc = new Point3D(2079, 475, 0); map = Map.Malas; break;
				case 3:	loc = new Point3D(2108, 534, 0); map = Map.Malas; break;
				case 4:	loc = new Point3D(2096, 535, 0); map = Map.Malas; break;
			}
			} else if ( area == 73 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Serpent Sanctum"
			{
				case 1:	loc = new Point3D(2500, 500, 0); map = Map.Malas; break;
				case 2:	loc = new Point3D(2516, 462, 0); map = Map.Malas; break;
				case 3:	loc = new Point3D(2448, 465, 0); map = Map.Malas; break;
				case 4:	loc = new Point3D(2508, 569, 0); map = Map.Malas; break;
			}
			} else if ( area == 74 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Tomb of the Fallen Wizard"
			{
				case 1:	loc = new Point3D(2371, 240, 2); map = Map.Malas; break;
				case 2:	loc = new Point3D(2330, 207, 2); map = Map.Malas; break;
				case 3:	loc = new Point3D(2356, 42, 0); map = Map.Malas; break;
				case 4:	loc = new Point3D(2288, 74, 0); map = Map.Malas; break;
			}
			} else if ( area == 75 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "Vordo's Dungeon"
			{
				case 1:	loc = new Point3D(6469, 713, 0); map = Map.Trammel; break;
				case 2:	loc = new Point3D(6278, 468, 0); map = Map.Trammel; break;
				case 3:	loc = new Point3D(6442, 451, 0); map = Map.Trammel; break;
				case 4:	loc = new Point3D(6286, 510, 0); map = Map.Trammel; break;
			}
			} else if ( area == 76 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Forgotten Halls"
			{
				case 1:	loc = new Point3D(532, 3573, 0); map = Map.TerMur; break;
				case 2:	loc = new Point3D(169, 3364, 0); map = Map.TerMur; break;
				case 3:	loc = new Point3D(92, 3410, 0); map = Map.TerMur; break;
				case 4:	loc = new Point3D(56, 3251, 20); map = Map.TerMur; break;
			}
			} else if ( area == 77 ){ loc = new Point3D(6818, 2691, 0); map = Map.Felucca; // "the Ancient Elven Mine"
			} else if ( area == 78 ){ switch ( Utility.RandomMinMax( 1, 6 ) ) // "the Tomb of Kazibal"
			{

				case 1:	loc = new Point3D(477, 3388, 0); map = Map.TerMur; break;
				case 2:	loc = new Point3D(490, 3310, 0); map = Map.TerMur; break;
				case 3:	loc = new Point3D(467, 3322, 0); map = Map.TerMur; break;
				case 4:	loc = new Point3D(415, 3319, 0); map = Map.TerMur; break;
				case 5:	loc = new Point3D(424, 3287, 35); map = Map.TerMur; break;
				case 6:	loc = new Point3D(424, 3281, 15); map = Map.TerMur; break;
			}
			} else if ( area == 79 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Scurvy Reef"
			{
				case 1:	loc = new Point3D(396, 3905, 0); map = Map.TerMur; break;
				case 2:	loc = new Point3D(441, 3825, 5); map = Map.TerMur; break;
				case 3:	loc = new Point3D(400, 3969, 30); map = Map.TerMur; break;
				case 4:	loc = new Point3D(394, 4060, 0); map = Map.TerMur; break;
			}
			} else if ( area == 80 ){ switch ( Utility.RandomMinMax( 1, 5 ) ) // "Stonegate Castle"
			{
				case 1:	loc = new Point3D(6274, 2506, 0); map = Map.Felucca; break;
				case 2:	loc = new Point3D(6418, 2599, 0); map = Map.Felucca; break;
				case 3:	loc = new Point3D(6255, 2409, 0); map = Map.Felucca; break;
				case 4:	loc = new Point3D(6823, 2850, 0); map = Map.Felucca; break;
				case 5:	loc = new Point3D(6354, 2495, 60); map = Map.Felucca; break;
			}
			} else if ( area == 81 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Undersea Castle"
			{
				case 1:	loc = new Point3D(692, 3814, -5); map = Map.TerMur; break;
				case 2:	loc = new Point3D(642, 3852, 39); map = Map.TerMur; break;
				case 3:	loc = new Point3D(681, 4063, -5); map = Map.TerMur; break;
				case 4:	loc = new Point3D(925, 3256, 40); map = Map.TerMur; break;
			}
			} else if ( area == 82 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Catacombs of Azerok"
			{
				case 1:	loc = new Point3D(774, 3394, 20); map = Map.TerMur; break;
				case 2:	loc = new Point3D(750, 3436, 20); map = Map.TerMur; break;
				case 3:	loc = new Point3D(800, 3412, 20); map = Map.TerMur; break;
				case 4:	loc = new Point3D(782, 3285, 0); map = Map.TerMur; break;
			}
			} else if ( area == 83 ){ switch ( Utility.RandomMinMax( 1, 3 ) ) // "the Azure Castle"
			{
				case 1:	loc = new Point3D(225, 3642, 5); map = Map.TerMur; break;
				case 2:	loc = new Point3D(320, 3636, 0); map = Map.TerMur; break;
				case 3:	loc = new Point3D(271, 3616, 50); map = Map.TerMur; break;
			}
			} else if ( area == 84 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Glacial Scar"
			{
				case 1:	loc = new Point3D(2183, 1451, 70); map = Map.Ilshenar; break;
				case 2:	loc = new Point3D(1950, 1524, -14); map = Map.Ilshenar; break;
				case 3:	loc = new Point3D(1719, 1520, 10); map = Map.Ilshenar; break;
				case 4:	loc = new Point3D(2181, 1295, 40); map = Map.Ilshenar; break;
			}
			} else if ( area == 85 ){ switch ( Utility.RandomMinMax( 1, 8 ) ) // "the Temple of Osirus"
			{
				case 1:	loc = new Point3D(6081, 3855, -40); map = Map.Felucca; break;
				case 2:	loc = new Point3D(6240, 3797, -5); map = Map.Felucca; break;
				case 3:	loc = new Point3D(6223, 3869, -5); map = Map.Felucca; break;
				case 4:	loc = new Point3D(6113, 950, 5); map = Map.Felucca; break;
				case 5:	loc = new Point3D(6084, 3965, 57); map = Map.Felucca; break;
				case 6:	loc = new Point3D(6146, 3662, -40); map = Map.Felucca; break;
				case 7:	loc = new Point3D(6130, 3651, -40); map = Map.Felucca; break;
				case 8:	loc = new Point3D(6138, 3638, -40); map = Map.Felucca; break;
			}
			} else if ( area == 86 ){ switch ( Utility.RandomMinMax( 1, 5 ) ) // "the Stygian Abyss"
			{
				case 1:	loc = new Point3D(2013, 1161, -8); map = Map.Ilshenar; break;
				case 2:	loc = new Point3D(1669, 1220, -43); map = Map.Ilshenar; break;
				case 3:	loc = new Point3D(1791, 1330, -42); map = Map.Ilshenar; break;
				case 4:	loc = new Point3D(1870, 1276, -37); map = Map.Ilshenar; break;
				case 5:	loc = new Point3D(1793, 1166, -42); map = Map.Ilshenar; break;
			}
			} else if ( area == 87 ){ switch ( Utility.RandomMinMax( 1, 4 ) ) // "the Sanctum of Saltmarsh"
			{
				case 1:	loc = new Point3D(6136, 1311, 10); map = Map.Felucca; break;
				case 2:	loc = new Point3D(5710, 2070, -50); map = Map.Felucca; break;
				case 3:	loc = new Point3D(5748, 2086, 0); map = Map.Felucca; break;
				case 4:	loc = new Point3D(6014, 1980, 0); map = Map.Felucca; break;
			}
			} else if ( area == 88 ){ switch ( Utility.RandomMinMax( 1, 6 ) ) // "the Ancient Sky Ship"
			{
				case 1:	loc = new Point3D(908, 4028, 0); map = Map.TerMur; break;
				case 2:	loc = new Point3D(1148, 3652, 0); map = Map.TerMur; break;
				case 3:	loc = new Point3D(1211, 3895, 0); map = Map.TerMur; break;
				case 4:	loc = new Point3D(1196, 4009, 0); map = Map.TerMur; break;
				case 5:	loc = new Point3D(1015, 3909, 0); map = Map.TerMur; break;
				case 6:	loc = new Point3D(1150, 3764, 0); map = Map.TerMur; break;
			}
			}

			int AlwaysAllow = 1;
				if ( item is Prisoner ){ AlwaysAllow = 0; }

			if ( item != null && loc.X > 0 && loc.Y > 0 && map != null && CanUseSpot( loc, map, AlwaysAllow ) )
			{
				item.MoveToWorld(loc, map);
			}
			else if ( item != null )
			{
				item.Delete();
			}
		}

		public static bool CanUseSpot( Point3D loc, Map map, int priority )
		{
			bool CanUse = true;

			if ( Utility.Random( 5 ) > 0 && priority != 1 )
			{
				CanUse = false;
			}
			else if ( loc.X > 0 && loc.Y > 0 && map != null )
			{
				IPooledEnumerable eable = map.GetItemsInRange( loc, 0 );

				foreach ( Item item in eable )
				{
					CanUse = false;
				}

				eable.Free();
			}

			return CanUse;
		}
	}
}