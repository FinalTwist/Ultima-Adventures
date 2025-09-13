using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Gumps;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fourth;
using Server.Spells.Magical;
using Server.Spells.Bushido;
using Server.Spells.Ninjitsu;
using Server.Spells.Necromancy;
using Server.Spells.Chivalry;
using Server.Spells.DeathKnight;
using Server.Spells.Herbalist;
using Server.Spells.Undead;
using Server.Spells.Mystic;
using Server.Spells.Research;

namespace Server.Misc
{
    class Worlds
    {
		public static string GetMyWorld( Map map, Point3D location, int x, int y )
		{
			Region reg = Region.Find( location, map );
			string worldLocation = "the Land of Sosaria";

			if ( map == Map.Trammel && x > 5774 && y > 2694 && x < 6123 && y < 3074 ){ worldLocation = "the Moon of Luna"; }
			else if ( map == Map.Trammel && ( reg.IsPartOf( "Moonlight Cavern" ) || 
												reg.IsPartOf( "The Core of the Moon" ) || 
												reg.IsPartOf( "The Moon's Core" ) ) ){ worldLocation = "the Moon of Luna"; }

			else if ( map == Map.Trammel && x > 5125 && y > 3038 && x < 6124 && y < 4093 ){ worldLocation = "the Land of Ambrosia"; }
			else if ( map == Map.Trammel && x > 3229 && y > 3870 && x < 3344 && y < 3946 ){ worldLocation = "the Land of Ambrosia"; }
			else if ( map == Map.Trammel && ( reg.IsPartOf( "the Dragon's Maw" ) || 
												reg.IsPartOf( "the Cave of the Zuluu" ) || 
												reg.IsPartOf( "the Arena of The Zuluu" ) ) ){ worldLocation = "the Land of Ambrosia"; }

			else if ( map == Map.Trammel && x > 2931 && y > 3675 && x < 2999 && y < 3722 ){ worldLocation = "the Island of Umber Veil"; }
			else if ( map == Map.Trammel && x > 699 && y > 3129 && x < 2272 && y < 4095 ){ worldLocation = "the Island of Umber Veil"; }
			else if ( map == Map.Trammel && reg.IsPartOf( "the Mausoleum" ) ){ worldLocation = "the Island of Umber Veil"; }
			else if ( map == Map.Trammel && reg.IsPartOf( "the Tower of Brass" ) ){ worldLocation = "the Island of Umber Veil"; }

			else if ( map == Map.Trammel && x > 6127 && y > 828 && x < 7168 && y < 2736 ){ worldLocation = "the Bottle World of Kuldar"; }
			else if ( map == Map.Trammel && ( reg.IsPartOf( "Highrock Mine" ) || 
												reg.IsPartOf( "Waterfall Cavern" ) || 
												reg.IsPartOf( "the Crumbling Cave" ) || 
												reg.IsPartOf( "Steamfire Cave" ) || 
												reg.IsPartOf( "the Valley of Dark Druids" ) || 
												reg.IsPartOf( "Vordo's Castle Grounds" ) || 
												reg.IsPartOf( "the Kuldara Sewers" ) || 
												reg.IsPartOf( "the Crypts of Kuldar" ) || 
												reg.IsPartOf( "Vordo's Castle" ) || 
												reg.IsPartOf( "Vordo's Dungeon" ) ) ){ worldLocation = "the Bottle World of Kuldar"; }

			else if ( map == Map.Felucca && ( reg.IsPartOf( "Morgaelin's Inferno" ) || 
												reg.IsPartOf( "the Zealan Tombs" ) || 
												reg.IsPartOf( "Argentrock Castle" ) || 
												reg.IsPartOf( "the Daemon's Crag" ) || 
												reg.IsPartOf( "the Hall of the Mountain King" ) || 
												reg.IsPartOf( "the Depths of Carthax Lake" ) ) ){ worldLocation = "the Underworld"; }

			else if ( map == Map.Trammel && reg.IsPartOf( "the Chamber of Corruption" ) ){ worldLocation = "the Underworld"; }

			else if ( map == Map.TerMur && ( reg.IsPartOf( "the Ancient Crash Site" ) || 
												reg.IsPartOf( "the Obsidian Fortress" ) || 
												reg.IsPartOf( "the Ancient Sky Ship" ) ) ){ worldLocation = "the Underworld"; }

			else if ( map == Map.Ilshenar && ( reg.IsPartOf( "the Glacial Scar" ) ) ){ worldLocation = "the Isles of Dread"; }

			else if ( map == Map.Felucca && ( reg.IsPartOf( "the Temple of Osirus" ) || reg.IsPartOf( "the Sanctum of Saltmarsh" ) ) ){ worldLocation = "the Isles of Dread"; }

			else if ( reg.IsPartOf( typeof( BardTownRegion ) ) || reg.IsPartOf( typeof( BardDungeonRegion ) ) ){ worldLocation = "the Town of Skara Brae"; }

			else if ( map == Map.Felucca && reg.IsPartOf( "the Montor Sewers" ) ){ worldLocation = "the Land of Sosaria"; }
			else if ( map == Map.Felucca && !reg.IsPartOf( "the Vault of the Black Knight" ) ){ worldLocation = "the Land of Lodoria"; }

			else if ( map == Map.Malas || reg.IsPartOf( "the Vault of the Black Knight" ) ){ worldLocation = "the Serpent Island"; }

			else if (
						map == Map.TerMur && 
						( reg.IsPartOf( "the Cimmeran Mines" ) || 
						reg.IsPartOf( "the Ice Queen Fortress" ) || 
						reg.IsPartOf( "the Scurvy Reef" ) || 
						reg.IsPartOf( "the Blood Temple" ) ) ){ worldLocation = "the Isles of Dread"; }

			else if ( map == Map.TerMur && reg.IsPartOf( "the Forgotten Halls" ) ){ worldLocation = "the Land of Sosaria"; }

			else if ( map == Map.Malas && !reg.IsPartOf( typeof( SkyHomeDwelling ) ) ){ worldLocation = "the Serpent Island"; }

			// SKY CASTLES
			else if ( map == Map.Malas && ( x > 1949 ) && ( y > 1393 ) && ( x < 2061 ) && ( y < 1486 ) ){ worldLocation = "the Land of Sosaria"; }
			else if ( map == Map.Malas && ( x > 2150 ) && ( y > 1401 ) && ( x < 2270 ) && ( y < 1513 ) ){ worldLocation = "the Land of Lodoria"; }
			else if ( map == Map.Malas && ( x > 2375 ) && ( y > 1398 ) && ( x < 2442 ) && ( y < 1467 ) ){ worldLocation = "the Land of Lodoria"; }
			else if ( map == Map.Malas && ( x > 2401 ) && ( y > 1635 ) && ( x < 2468 ) && ( y < 1703 ) ){ worldLocation = "the Serpent Island"; }
			else if ( map == Map.Malas && ( x > 2408 ) && ( y > 1896 ) && ( x < 2517 ) && ( y < 2005 ) ){ worldLocation = "the Savaged Empire"; }
			else if ( map == Map.Malas && ( x > 2181 ) && ( y > 1889 ) && ( x < 2275 ) && ( y < 2003 ) ){ worldLocation = "the Isles of Dread"; }
			else if ( map == Map.Malas && ( x > 1930 ) && ( y > 1890 ) && ( x < 2022 ) && ( y < 1997 ) ){ worldLocation = "the Land of Sosaria"; }

			// DUNGEON HOMES
			else if ( map == Map.Ilshenar && ( x > 1657 ) && ( y > 795 ) && ( x < 1811 ) && ( y < 898 ) ){ worldLocation = "the Serpent Island"; }
			else if ( map == Map.Ilshenar && ( x > 1883 ) && ( y > 794 ) && ( x < 2034 ) && ( y < 902 ) ){ worldLocation = "the Land of Lodoria"; }
			else if ( map == Map.Ilshenar && ( x > 2112 ) && ( y > 794 ) && ( x < 2267 ) && ( y < 898 ) ){ worldLocation = "the Isles of Dread"; }
			else if ( map == Map.Ilshenar && ( x > 1659 ) && ( y > 953 ) && ( x < 1809 ) && ( y < 1059 ) ){ worldLocation = "the Land of Ambrosia"; }
			else if ( map == Map.Ilshenar && ( x > 1881 ) && ( y > 954 ) && ( x < 2034 ) && ( y < 1059 ) ){ worldLocation = "the Savaged Empire"; }
			else if ( map == Map.Ilshenar && ( x > 2113 ) && ( y > 952 ) && ( x < 2268 ) && ( y < 1056 ) ){ worldLocation = "the Savaged Empire"; }

			else if ( map == Map.Felucca ){ worldLocation = "the Land of Lodoria"; }
			else if ( map == Map.Trammel ){ worldLocation = "the Land of Sosaria"; }
			else if ( map == Map.Ilshenar && ( x > 1013 ) && ( y > 0 ) && ( x < 2013 ) && ( y < 775 ) ){ worldLocation = "the Underworld"; }
			else if ( map == Map.Ilshenar && ( x > 1013 ) && ( y >= 775 ) && ( x < 1646 ) && ( y < 981 ) ){ worldLocation = "the Underworld"; }	
			else if ( map == Map.Ilshenar && ( x > 0 ) && ( y > 0 ) && ( x < 1007 ) && ( y < 1279 ) ){ worldLocation = "DarkMoor"; }			
			else if ( map == Map.Malas ){ worldLocation = "the Serpent Island"; }
			else if ( map == Map.Tokuno ){ worldLocation = "the Isles of Dread"; }
			else if ( map == Map.TerMur ){ worldLocation = "the Savaged Empire"; }
			else if ( map == Map.Midland ){ worldLocation = "the Midlands"; }

			return worldLocation;
		}

		public static void EnteredTheLand( Mobile from )
		{
			if ( from is PlayerMobile )
			{
				string world = GetRegionName( from.Map, from.Location );

				bool runLog = false;

				if ( world == "the Land of Lodoria" ){ CharacterDatabase.SetDiscovered( from, world, true ); runLog = true; }
				else if ( world == "the Land of Sosaria" )
				{
					if ( from.X >= 3546 && from.Y >= 3383 && from.X <= 3590 && from.Y <= 3428 ){ /* DO NOTHING IN TIME LORD CHAMBER */ }
					else { CharacterDatabase.SetDiscovered( from, world, true ); runLog = true; }
				}
				else if ( world == "the Island of Umber Veil" ){ CharacterDatabase.SetDiscovered( from, world, true ); runLog = true; }
				else if ( world == "the Land of Ambrosia" ){ CharacterDatabase.SetDiscovered( from, world, true ); runLog = true; }
				else if ( world == "the Serpent Island" ){ CharacterDatabase.SetDiscovered( from, world, true ); runLog = true; }
				else if ( world == "the Isles of Dread" ){ CharacterDatabase.SetDiscovered( from, world, true ); runLog = true; }
				else if ( world == "the Savaged Empire" ){ CharacterDatabase.SetDiscovered( from, world, true ); runLog = true; }
				else if ( world == "the Underworld" ){ CharacterDatabase.SetDiscovered( from, world, true ); runLog = true; 				}
				else if ( world == "DarkMoor" )
				{ 
					CharacterDatabase.SetDiscovered( from, world, true ); 
					runLog = true; 
					if (from.Criminal)
						from.Criminal = false;
				}
				else if ( world == "the Bottle World of Kuldar" ){ CharacterDatabase.SetDiscovered( from, world, true ); runLog = true; }

				if ( runLog )
					LoggingFunctions.LogRegions( from, world, "enter" );
			}
		}

		public static bool NoApocalypse( Point3D p, Map map )
		{
			Region reg = Region.Find( p, map );

			if ( reg is WantedRegion || 
			reg is SavageRegion || 
			reg is VillageRegion || 
			reg is UnderHouseRegion || 
			reg is UmbraRegion || 
			reg is TownRegion || 
			reg is StartRegion || 
			reg is SkyHomeDwelling || 
			reg is SafeRegion || 
			reg is ProtectedRegion || 
			reg is PublicRegion || 
			reg is PirateRegion || 
			reg is BardTownRegion || 
			reg is DawnRegion || 
			reg is DungeonHomeRegion || 
			reg is GargoyleRegion || 
			reg is GuardedRegion || 
			reg is HouseRegion || 
			reg is LunaRegion || 
			reg is MazeRegion || 
			reg is MoonCore  )
				return true;

			return false;
		}

		public static string GetRegionName( Map map, Point3D location )
		{
			Region reg = Region.Find( location, map );

			string regionName = reg.Name;

			if ( ( reg.IsDefault || reg.Name == null || reg.Name == "" || reg.Name == " " ) )
			{
				if ( map == Map.Felucca )
				{
					if ( location.X >= 0 && location.Y >= 0 && location.X <= 5118 && location.Y <= 4092 ){ regionName = "the Land of Lodoria"; }
				}
				else if ( map == Map.Trammel )
				{
					if ( location.X >= 0 && location.Y >= 0 && location.X <= 5118 && location.Y <= 3125 ){ regionName = "the Land of Sosaria"; }
					else if ( location.X >= 699 && location.Y >= 3129 && location.X <= 2272 && location.Y <= 4095 ){ regionName = "the Island of Umber Veil"; }
					else if ( location.X >= 5122 && location.Y >= 3036 && location.X <= 6126 && location.Y <= 4095 ){ regionName = "the Land of Ambrosia"; }
					else if ( location.X >= 6127 && location.Y >= 828 && location.X <= 7167 && location.Y <= 2743 ){ regionName = "the Bottle World of Kuldar"; }
				}
				else if ( map == Map.Ilshenar )
				{
					if ( location.X >= 1013 && location.Y >= 0 && location.X < 2013 && location.Y < 775 ){ regionName = "the Underworld"; }
					if ( location.X >= 1013 && location.Y >= 775 && location.X < 1646 && location.Y <= 981 ){ regionName = "the Underworld"; }
					if ( location.X >= 0 && location.Y >= 0 && location.X < 1007 && location.Y <= 1279 ){ regionName = "DarkMoor"; }
				}
				else if ( map == Map.Malas )
				{
					if ( location.X >= 0 && location.Y >= 0 && location.X <= 1874 && location.Y <= 2042 ){ regionName = "the Serpent Island"; }
				}
				else if ( map == Map.Tokuno )
				{
					if ( location.X >= 0 && location.Y >= 0 && location.X <= 1430 && location.Y <= 1430 ){ regionName = "the Isles of Dread"; }
				}
				else if ( map == Map.TerMur )
				{
					if ( location.X >= 0 && location.Y >= 0 && location.X <= 1168 && location.Y <= 1802 ){ regionName = "the Savaged Empire"; }
				}
				else if ( map == Map.Midland )
					regionName = "the Midlands";

			}

			return regionName;
		}

		public static bool IsMainRegion( string region )
		{
			if ( 	region == "the Land of Lodoria" || 
					region == "the Land of Sosaria" || 
					region == "the Island of Umber Veil" || 
					region == "the Land of Ambrosia" || 
					region == "the Bottle World of Kuldar" || 
					region == "the Underworld" || 
					region == "the Serpent Island" || 
					region == "the Isles of Dread" || 
					region == "DarkMoor" || 
					region == "the Savaged Empire" ||
					region == "the Midlands")
				return true;

			return false;
		}

		public static string GetMyRegion( Map map, Point3D location )
		{
			Region reg = Region.Find( location, map );
			return reg.Name;
		}

		public static string GetMyMapString( Map map )
		{
			string world = "Trammel";

			if ( map == Map.Felucca ){ world = "Felucca"; }
			else if ( map == Map.Ilshenar ){ world = "Ilshenar"; }
			else if ( map == Map.Malas ){ world = "Malas"; }
			else if ( map == Map.Tokuno ){ world = "Tokuno"; }
			else if ( map == Map.TerMur ){ world = "TerMur"; }
			else if ( map == Map.Midland ){ world = "Midland"; }

			return world;
		}

		public static Map GetMyDefaultMap( string world )
		{
			Map map = Map.Trammel;

			if ( world == "the Town of Skara Brae" ){ map = Map.Felucca; }
			else if ( world == "the Land of Lodoria" ){ map = Map.Felucca; }
			else if ( world == "the Serpent Island" ){ map = Map.Malas; }
			else if ( world == "the Isles of Dread" ){ map = Map.Tokuno; }
			else if ( world == "the Savaged Empire" ){ map = Map.TerMur; }
			else if ( world == "DarkMoor" ){ map = Map.Ilshenar; }
			else if ( world == "the Underworld" ){ map = Map.Ilshenar; }
			else if ( world == "the Midlands" ){ map = Map.Midland; }
			/// THE REST ARE ON TRAMMEL ///

			return map;
		}

		public static bool IsCrypt( Point3D p, Map map )
		{
			Region reg = Region.Find( p, map );
			
			if ( reg.IsPartOf( "the Crypt" ) || 
				reg.IsPartOf( "the Lodoria Catacombs" ) || 
				reg.IsPartOf( "the Crypts of Dracula" ) || 
				reg.IsPartOf( "the Castle of Dracula" ) || 
				reg.IsPartOf( "the Graveyard" ) || 
				reg.IsPartOf( "Ravendark Woods" ) || 
				reg.IsPartOf( "the Island of Dracula" ) || 
				reg.IsPartOf( "the Village of Ravendark" ) || 
				reg.IsPartOf( "the Lodoria Cemetery" ) || 
				reg.IsPartOf( "the Lost Graveyard" ) || 
				reg.IsPartOf( "the Mausoleum" ) || 
				reg.IsPartOf( "the Kuldar Cemetery" ) || 
				reg.IsPartOf( "the Cave of Souls" ) || 
				reg.IsPartOf( "the Crypts of Kuldar" ) || 
				reg.IsPartOf( "the Zealan Graveyard" ) || 
				reg.IsPartOf( "the Zealan Tombs" ) || 
				reg.IsPartOf( "the Tombs" ) || 
				reg.IsPartOf( "the Dungeon of the Lich King" ) || 
				reg.IsPartOf( "the Tomb of Kazibal" ) || 
				reg.IsPartOf( "the Catacombs of Azerok" ) )
				return true;

			return false;
		}

		public static bool IsSeaDungeon( Point3D p, Map map )
		{
			Region reg = Region.Find( p, map );

			if ( reg.IsPartOf( "the Depths of Carthax Lake" ) || 
			reg.IsPartOf( "the Storm Giant Lair" ) || 
			reg.IsPartOf( "the Island of the Storm Giant" ) || 
			reg.IsPartOf( "the Undersea Castle" ) || 
			reg.IsPartOf( "the Scurvy Reef" ) || 
			reg.IsPartOf( "the Caverns of Poseidon" ) || 
			reg.IsPartOf( "the Flooded Temple" ) )
				return true;

			return false;
		}

		public static bool IsFireDungeon( Point3D p, Map map )
		{
			Region reg = Region.Find( p, map );

			if ( reg.IsPartOf( "the Fires of Hell" ) || 
			reg.IsPartOf( "Morgaelin's Inferno" ) || 
			reg.IsPartOf( "the City of Embers" ) || 
			reg.IsPartOf( "the Cave of Fire" ) || 
			reg.IsPartOf( "Steamfire Cave" ) || 
			reg.IsPartOf( "the Volcanic Cave" ) )
				return true;

			return false;
		}

		public static bool IsOnSpaceship( Point3D p, Map map )
		{
			Region reg = Region.Find( p, map );

			if ( reg.IsPartOf( "the Ancient Crash Site" ) || 
			reg.IsPartOf( "the Ancient Sky Ship" ) )
				return true;

			return false;
		}

		public static bool IsIceDungeon( Point3D p, Map map )
		{
			Region reg = Region.Find( p, map );

			if ( reg.IsPartOf( "the Glacial Scar" ) || 
			reg.IsPartOf( "the Frozen Hells" ) || 
			reg.IsPartOf( "the Ratmen Cave" ) || 
			reg.IsPartOf( "the Ice Fiend Lair" ) || 
			reg.IsPartOf( "the Ice Queen Fortress" ) || 
			reg.IsPartOf( "the Frozen Dungeon" ) || 
			reg.IsPartOf( "Frostwall Caverns" ) )
				return true;

			return false;
		}

		public static bool IsExploringSeaAreas( Mobile m )
		{
			if ( IsOnBoat( m ) == true && BoatToCloseToTown( m ) == false )
				return true;

			Region reg = Region.Find( m.Location, m.Map );
			
			if ( reg.IsPartOf( "the Caverns of Poseidon" ) )
				return true;

			if ( reg.IsPartOf( "the Storm Giant Lair" ) )
				return true;

			if ( reg.IsPartOf( "the Island of the Storm Giant" ) )
				return true;

			if ( reg.IsPartOf( "the Island of Poseidon" ) )
				return true;

			if ( reg.IsPartOf( "the Buccaneer's Den" ) )
				return true;

			if ( reg.IsPartOf( "the Undersea Castle" ) )
				return true;

			if ( reg.IsPartOf( "the Depths of Carthax Lake" ) )
				return true;

			if ( reg.IsPartOf( "the Scurvy Reef" ) )
				return true;

			if ( reg.IsPartOf( "the Flooded Temple" ) )
				return true;

			if ( reg.IsPartOf( typeof( PirateRegion ) ) )
				return true;

			return false;
		}

		public static bool IsOnBoat( Mobile m )
		{
			if ( m.Z != -2 )
				return false;

			int KeepSearching = 0;
			bool IsOnShip = false;

			foreach ( Item boatman in m.GetItemsInRange( 15 ) )
			{
				if ( KeepSearching != 1 )
				{
					if ( boatman is TillerMan )
					{
						IsOnShip = true;
						if ( IsOnShip == true ){ KeepSearching = 1; }
					}
				}
			}
			return IsOnShip;
		}

		public static bool ItemOnBoat( Item i )
		{
			if ( i.Z != -2 )
				return false;

			int KeepSearching = 0;
			bool IsOnShip = false;

			foreach ( Item boatman in i.GetItemsInRange( 15 ) )
			{
				if ( KeepSearching != 1 )
				{
					if ( boatman is TillerMan )
					{
						IsOnShip = true;
						if ( IsOnShip == true ){ KeepSearching = 1; }
					}
				}
			}
			return IsOnShip;
		}

		public static bool BoatToCloseToTown( Mobile m )
		{
			foreach ( Mobile landlover in m.GetMobilesInRange( 50 ) )
			{
				if ( landlover is BaseVendor || landlover is BasePerson || landlover is BaseNPC )
				{
					return true;
				}
			}
			return false;
		}

		public static bool RegionAllowedTeleport( Map map, Point3D location, int x, int y )
		{
			string world = Worlds.GetMyWorld( map, location, x, y );
			Region reg = Region.Find( location, map );

			if ( reg.IsPartOf( typeof( DungeonRegion ) ) )
				return false;

			if ( world == "the Bottle World of Kuldar" )
				return false;

			if ( world == "the Underworld" )
				return false;

			if ( world == "Doom Gauntlet" )
				return false;

			if ( world == "the Land of Ambrosia" )
				return false;

			if ( world == "the Town of Skara Brae" )
				return false;

			if ( reg.IsPartOf( "the Moon's Core" ) || reg.IsPartOf( "the Core of the Moon" ) || reg.IsPartOf( "Moonlight Cavern" ) )
				return false;

			if ( reg.IsPartOf( "the Camping Tent" ) )
				return false;

			if ( reg.IsPartOf( "the Island of Poseidon" ) )
				return false;

			if ( reg.IsPartOf( "the Dungeon Room" ) )
				return false;

			if ( reg.IsPartOf( "the Island of Stonegate" ) )
				return false;

			if ( reg.IsPartOf( "the Painting of the Glade" ) )
				return false;

			if ( reg.IsPartOf( "the Island of the Black Knight" ) )
				return false;

			if ( reg.IsPartOf( "the Castle of the Black Knight" ) )
				return false;

			if ( reg.IsPartOf( "the Castle of the Black Knight" ) )
				return false;

			if ( reg.IsPartOf( typeof( GargoyleRegion ) ) )
				return false;

			if ( reg.IsPartOf( typeof( MazeRegion ) ) )
				return false;

			if ( reg.IsPartOf( typeof( PublicRegion ) ) )
				return false;

			if ( reg.IsPartOf( "the Island of Poseidon" ) )
				return false;

			if ( reg.IsPartOf( "the Village of Ravendark" ) )
				return false;

			if ( reg.IsPartOf( typeof( BargeDeadRegion ) ) )
				return false;

			if ( world == "the Midlands" || map == Map.Midland )
				return false;
/*
			if ( reg.IsPartOf( typeof( DungeonRegion ) ) )
				return false;*/

			return true;
		}

		public static bool AllowEscape( Mobile m, Map map, Point3D location, int x, int y )
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			bool canLeave = true;
			int mX = 0;
			int mY = 0;
			int mZ = 0;
			Map mWorld = null;

			string sPublicDoor = DB.CharacterPublicDoor;
			if ( sPublicDoor != null )
			{
				if ( sPublicDoor.Length > 0 )
				{
					string[] sPublicDoors = sPublicDoor.Split('#');
					int nEntry = 1;
					foreach (string exits in sPublicDoors)
					{
						if ( nEntry == 1 ){ mX = Convert.ToInt32(exits); }
						else if ( nEntry == 2 ){ mY = Convert.ToInt32(exits); }
						else if ( nEntry == 3 ){ mZ = Convert.ToInt32(exits); }
						else if ( nEntry == 4 ){ try { mWorld = Map.Parse( exits ); } catch{} if ( mWorld == null ){ mWorld = Map.Trammel; } }
						nEntry++;
					}

					location = new Point3D( mX, mY, mZ );
					map = mWorld;
					x = mX;
					y = mY;
				}
			}

			string world = Worlds.GetMyWorld( map, location, x, y );
			Region reg = Region.Find( location, map );

			if ( world == "the Bottle World of Kuldar" && CharacterDatabase.GetDiscovered( m, "the Bottle World of Kuldar" ) )
				canLeave = false;

			if ( world == "the Town of Skara Brae" )
				canLeave = false;

			if ( reg.IsPartOf( "the Camping Tent" ) )
				canLeave = false;

			if ( reg.IsPartOf( "the Ship's Lower Deck" ) )
				canLeave = false;
			
			if ( reg.IsPartOf( "Doom Gauntlet" ) )
				canLeave = false;

			if ( world == "the Midlands" || map == Map.Midland)
				canLeave = false;

			return canLeave;
		}

		public static bool IsAllowedSpell( Mobile m, ISpell s )
		{
			if ( m.Region.IsPartOf( "the Ship's Lower Deck" ) )
				return false;

			if (	s is GateTravelSpell || 
						s is MushroomGatewaySpell || 
						s is UndeadGraveyardGatewaySpell || 
						s is HellsGateSpell || 
						s is AstralTravel || 
						s is ResearchEtherealTravel || 
						s is NaturesPassageSpell || 
						s is RecallSpell || 
						s is SacredJourneySpell )
			{
				return true;
			}

			return false;
		}

		public static bool RegionAllowedRecall( Map map, Point3D location, int x, int y )
		{
			string world = Worlds.GetMyWorld( map, location, x, y );
			Region reg = Region.Find( location, map );

			if ( world == "the Town of Skara Brae" )
				return false;

			if ( reg.IsPartOf( "Moonlight Cavern" ) )
				return false;

			if ( world == "the Bottle World of Kuldar" )
				return false;

			if ( world == "the Land of Ambrosia" )
				return false;

			if ( reg.IsPartOf( "the Village of Ravendark" ) )
				return false;
			
			if ( reg.IsPartOf( "Doom Gauntlet" ) )
				return false;

			if ( world == "the Midlands" || map == Map.Midland )
				return false;

			return true;
		}

		public static bool IsPlayerInTheLand( Map map, Point3D location, int x, int y )
		{
			string world = Worlds.GetMyWorld( map, location, x, y );
			Region reg = Region.Find( location, map );

			if ( world == "the Moon of Luna" && x >= 5801 && y >= 2716 && x <= 6125 && y <= 3034 )
				return true;
			else if ( world == "the Land of Sosaria" && x >= 0 && y >= 0 && x <= 5119 && y <= 3127 )
				return true;
			else if ( world == "the Land of Lodoria" && x >= 0 && y >= 0 && x <= 5120 && y <= 4095 )
				return true;
			else if ( world == "the Serpent Island" && x >= 0 && y >= 0 && x <= 1870 && y <= 2047 )
				return true;
			else if ( world == "the Isles of Dread" && x >= 0 && y >= 0 && x <= 1447 && y <= 1447 )
				return true;
			else if ( world == "the Savaged Empire" && x >= 136 && y >= 8 && x <= 1160 && y <= 1792 )
				return true;
			else if ( world == "the Land of Ambrosia" && x >= 5122 && y >= 3036 && x <= 6126 && y <= 4095 )
				return true;
			else if ( world == "the Island of Umber Veil" && x >= 699 && y >= 3129 && x <= 2272 && y <= 4095 )
				return true;
			else if ( world == "the Bottle World of Kuldar" && x >= 6127 && y >= 828 && x <= 7168 && y <= 2742 )
				return true;
			else if ( world == "the Underworld" && x >= 1012 && y >= 0 && x <= 2013 && y <= 775 )
				return true;
			else if ( world == "the Underworld" && x >= 1012 && y >= 775 && x <= 1646 && y <= 981 )
				return true;
			else if ( world == "DarkMoor" && x >= 0 && y >= 0 && x <= 1007 && y <= 1279 )
				return true;
			else if ( world == "the Midlands" && x <= 5084 && y <= 4088 )
				return true;

			return false;
		}

		public static bool PlayersLeftInRegion( Mobile from, Region region )
		{
			bool occupied = false;

			foreach ( NetState state in NetState.Instances )
			{
				Mobile m = state.Mobile;

				if ( m != null /* && m.AccessLevel < AccessLevel.GameMaster */ && m != from && m.Region == region )
				{
					occupied = true;
				}
			}

			return occupied;
		}

		public static void MoveToRandomDungeon( Mobile m )
		{
			Point3D loc = new Point3D(0, 0, 0);
			Map map = Map.Trammel;

			switch ( Utility.RandomMinMax( 0, 69 ) )
			{
				case 0: loc = new Point3D(5773, 2804, 0); map = Map.Felucca; break; // the Crypts of Dracula
				case 1: loc = new Point3D(5353, 91, 15); map = Map.Felucca; break; // the Mind Flayer City
				case 2: loc = new Point3D(5789, 2558, -30); map = Map.Felucca; break; // Dungeon Covetous
				case 3: loc = new Point3D(5308, 680, 0); map = Map.Felucca; break; // Dungeon Deceit
				case 4: loc = new Point3D(5185, 2442, 6); map = Map.Felucca; break; // Dungeon Despise
				case 5: loc = new Point3D(5321, 799, 0); map = Map.Felucca; break; // Dungeon Destard
				case 6: loc = new Point3D(5869, 1443, 0); map = Map.Felucca; break; // the City of Embers
				case 7: loc = new Point3D(6038, 200, 22); map = Map.Felucca; break; // Dungeon Hythloth
				case 8: loc = new Point3D(5728, 155, 1); map = Map.Felucca; break; // the Frozen Hells
				case 9: loc = new Point3D(5783, 23, 0); map = Map.Felucca; break; // Dungeon Shame
				case 10: loc = new Point3D(5174, 1703, 2); map = Map.Felucca; break; // Terathan Keep
				case 11: loc = new Point3D(5247, 436, 0); map = Map.Felucca; break; // the Halls of Undermountain
				case 12: loc = new Point3D(5859, 3427, 0); map = Map.Felucca; break; // the Volcanic Cave
				case 13: loc = new Point3D(5443, 1398, 0); map = Map.Felucca; break; // Dungeon Wrong
				case 14: loc = new Point3D(5854, 1756, 0); map = Map.Trammel; break; // the Caverns of Poseidon
				case 15: loc = new Point3D(6387, 3754, -2); map = Map.Trammel; break; // the Tower of Brass
				case 16: loc = new Point3D(3943, 3370, 0); map = Map.Trammel; break; // the Mausoleum
				case 17: loc = new Point3D(6384, 490, 0); map = Map.Trammel; break; // Vordo's Dungeon
				case 18: loc = new Point3D(7028, 3824, 5); map = Map.Trammel; break; // the Cave of the Zuluu
				case 19: loc = new Point3D(4629, 3599, 0); map = Map.Trammel; break; // the Dragon's Maw
				case 20: loc = new Point3D(5354, 923, 0); map = Map.Trammel; break; // the Ancient Pyramid
				case 21: loc = new Point3D(5965, 636, 0); map = Map.Trammel; break; // Dungeon Exodus
				case 22: loc = new Point3D(262, 3380, 0); map = Map.Trammel; break; // the Cave of Banished Mages
				case 23: loc = new Point3D(5981, 2154, 0); map = Map.Trammel; break; // Dungeon Clues
				case 24: loc = new Point3D(5550, 393, 0); map = Map.Trammel; break; // Dardin's Pit
				case 25: loc = new Point3D(5259, 262, 0); map = Map.Trammel; break; // Dungeon Abandon
				case 26: loc = new Point3D(5526, 1228, 0); map = Map.Trammel; break; // the Fires of Hell
				case 27: loc = new Point3D(5587, 1602, 0); map = Map.Trammel; break; // the Mines of Morinia
				case 28: loc = new Point3D(5995, 423, 0); map = Map.Trammel; break; // the Perinian Depths
				case 29: loc = new Point3D(5638, 821, 0); map = Map.Trammel; break; // the Dungeon of Time Awaits
				case 30: loc = new Point3D(1955, 523, 0); map = Map.Malas; break; // the Ancient Prison
				case 31: loc = new Point3D(2090, 863, 0); map = Map.Malas; break; // the Cave of Fire
				case 32: loc = new Point3D(2440, 53, 2); map = Map.Malas; break; // the Cave of Souls
				case 33: loc = new Point3D(2032, 76, 0); map = Map.Malas; break; // Dungeon Ankh
				case 34: loc = new Point3D(1947, 216, 0); map = Map.Malas; break; // Dungeon Bane
				case 35: loc = new Point3D(2189, 425, 0); map = Map.Malas; break; // Dungeon Hate
				case 36: loc = new Point3D(2221, 816, 0); map = Map.Malas; break; // Dungeon Scorn
				case 37: loc = new Point3D(1957, 710, 0); map = Map.Malas; break; // Dungeon Torment
				case 38: loc = new Point3D(2361, 403, 0); map = Map.Malas; break; // Dungeon Vile
				case 39: loc = new Point3D(2160, 173, 2); map = Map.Malas; break; // Dungeon Wicked
				case 40: loc = new Point3D(2311, 912, 2); map = Map.Malas; break; // Dungeon Wrath
				case 41: loc = new Point3D(2459, 880, 0); map = Map.Malas; break; // the Flooded Temple
				case 42: loc = new Point3D(2064, 509, 0); map = Map.Malas; break; // the Gargoyle Crypts
				case 43: loc = new Point3D(2457, 506, 0); map = Map.Malas; break; // the Serpent Sanctum
				case 44: loc = new Point3D(2327, 183, 2); map = Map.Malas; break; // the Tomb of the Fallen Wizard
				case 45: loc = new Point3D(729, 2635, -28); map = Map.TerMur; break; // the Blood Temple
				case 46: loc = new Point3D(774, 1984, -28); map = Map.TerMur; break; // the Dungeon of the Mad Archmage
				case 47: loc = new Point3D(51, 2619, -28); map = Map.TerMur; break; // the Tombs
				case 48: loc = new Point3D(342, 2296, -1); map = Map.TerMur; break; // the Dungeon of the Lich King
				case 49: loc = new Point3D(323, 2836, 0); map = Map.TerMur; break; // the Ice Queen Fortress
				case 50: loc = new Point3D(1143, 2403, -28); map = Map.TerMur; break; // the Halls of Ogrimar
				case 51: loc = new Point3D(692, 2319, -27); map = Map.TerMur; break; // Dungeon Rock
				case 52: loc = new Point3D(100, 3389, 0); map = Map.TerMur; break; // Forgotten Halls
				case 53: loc = new Point3D(366, 3886, 0); map = Map.TerMur; break; // the Scurvy Reef
				case 54: loc = new Point3D(647, 3860, 39); map = Map.TerMur; break; // the Undersea Castle
				case 55: loc = new Point3D(231, 3650, 25); map = Map.TerMur; break; // the Azure Castle
				case 56: loc = new Point3D(436, 3311, 20); map = Map.TerMur; break; // the Tomb of Kazibal
				case 57: loc = new Point3D(670, 3357, 20); map = Map.TerMur; break; // the Catacombs of Azerok
				case 58: loc = new Point3D(6035, 2574, 0); map = Map.Felucca; break; // Stonegate Castle
				case 59: loc = new Point3D(1968, 1363, 61); map = Map.Ilshenar; break; // the Glacial Scar
				case 60: loc = new Point3D(6142, 3660, -20); map = Map.Felucca; break; // the Temple of Osirus
				case 61: loc = new Point3D(1851, 1233, -42); map = Map.Ilshenar; break; // the Stygian Abyss
				case 62: loc = new Point3D(6413, 2004, -40); map = Map.Felucca; break; // the Daemon's Crag
				case 63: loc = new Point3D(7003, 2437, -11); map = Map.Felucca; break; // the Zealan Tombs
				case 64: loc = new Point3D(6368, 968, 25); map = Map.Felucca; break; // the Hall of the Mountain King
				case 65: loc = new Point3D(6826, 1123, -92); map = Map.Felucca; break; // Morgaelin's Inferno
				case 66: loc = new Point3D(5950, 1654, -5); map = Map.Felucca; break; // the Depths of Carthax Lake
				case 67: loc = new Point3D(5989, 484, 1); map = Map.Felucca; break; // Argentrock Castle
				case 68: loc = new Point3D(6021, 1968, 0); map = Map.Felucca; break; // the Sanctum of Saltmarsh
				case 69: loc = new Point3D(1125, 3684, 0); map = Map.Felucca; break; // the Ancient Sky Ship
			}

			if ( m is PlayerMobile )
			{
				Server.Mobiles.BaseCreature.TeleportPets( m, loc, map );
				m.MoveToWorld( loc, map );
			}
			else if ( m is BaseCreature )
			{
				m.MoveToWorld( loc, map );
			}
		}

		public static void MoveToRandomOcean( Mobile m )
		{
			Point3D loc = new Point3D(20, 20, 0);
			Map map = Map.Trammel;
			string world = "the Land of Sosaria";

			switch ( Utility.RandomMinMax( 0, 8 ) )
			{
				case 0: world = "the Bottle World of Kuldar";	map = Map.Trammel;	break;
				case 1: world = "the Land of Ambrosia";			map = Map.Trammel;	break;
				case 2: world = "the Island of Umber Veil";		map = Map.Trammel;	break;
				case 3: world = "the Land of Lodoria";			map = Map.Felucca;	break;
				case 4: world = "the Underworld";				map = Map.Ilshenar;	break;
				case 5: world = "the Serpent Island";			map = Map.Malas;	break;
				case 6: world = "the Isles of Dread";			map = Map.Tokuno;	break;
				case 7: world = "the Savaged Empire";			map = Map.TerMur;	break;
				case 8: world = "the Land of Sosaria";			map = Map.Trammel;	break;
			}

			loc = GetRandomLocation( world, "ocean" );

			if ( m is PlayerMobile )
			{
				Server.Mobiles.BaseCreature.TeleportPets( m, loc, map );
				m.MoveToWorld( loc, map );
			}
			else if ( m is BaseCreature )
			{
				m.MoveToWorld( loc, map );
			}
		}

		public static Point3D GetRandomDungeonSpot( Map map )
		{
			Point3D loc = new Point3D(0, 0, 0);
			int aCount = 0;
			ArrayList targets = new ArrayList();
			foreach ( Item target in World.Items.Values )
			{
				if ( target is DungeonChest || target is DungeonChestSpawner && target.Map == map && Server.Misc.MyServerSettings.GetDifficultyLevel( target.Location, target.Map ) > 0 )
				{
					Region reg = Region.Find( target.Location, target.Map );
					if ( reg.IsPartOf( typeof( DungeonRegion ) ) )
					{
						targets.Add( target );
						aCount++;
					}
				}
			}
			aCount = Utility.RandomMinMax( 1, aCount );
			int xCount = 0;
			for ( int i = 0; i < targets.Count; ++i )
			{
				xCount++;

				if ( xCount == aCount )
				{
					Item finding = ( Item )targets[ i ];
					loc = finding.Location;
				}
			}
			return loc;
		}

        public static string GetAreaEntrance( string zone, Map map )
        {
			// THIS RETURNS THE COORDINATES AND MAP OF THE DUNGEON ENTRANCE

			Point3D loc = new Point3D(0, 0, 0);

			if ( zone == "the City of the Dead" && map == Map.Trammel ){ loc = new Point3D(5828, 3263, 0); }
			else if ( zone == "the Mausoleum" && map == Map.Trammel ){ loc = new Point3D(1529, 3599, 0); }
			else if ( zone == "the Valley of Dark Druids" && map == Map.Trammel ){ loc = new Point3D(6763, 1423, 2); }
			else if ( zone == "Vordo's Castle" && map == Map.Trammel ){ loc = new Point3D(6708, 1729, 25); }
			else if ( zone == "Vordo's Dungeon" && map == Map.Trammel ){ loc = new Point3D(6708, 1729, 25); }
			else if ( zone == "the Crypts of Kuldar" && map == Map.Trammel ){ loc = new Point3D(6668, 1568, 10); }
			else if ( zone == "the Kuldara Sewers" && map == Map.Trammel ){ loc = new Point3D(6790, 1745, 24); }
			else if ( zone == "the Ancient Pyramid" && map == Map.Trammel ){ loc = new Point3D(1162, 472, 0); }
			else if ( zone == "Dungeon Exodus" && map == Map.Trammel ){ loc = new Point3D(877, 2702, 0); }
			else if ( zone == "the Cave of Banished Mages" && map == Map.Trammel ){ loc = new Point3D(3798, 1879, 2); }
			else if ( zone == "Dungeon Clues" && map == Map.Trammel ){ loc = new Point3D(3760, 2038, 0); }
			else if ( zone == "Dardin's Pit" && map == Map.Trammel ){ loc = new Point3D(3006, 446, 0); }
			else if ( zone == "Dungeon Abandon" && map == Map.Trammel ){ loc = new Point3D(1628, 2561, 0); }
			else if ( zone == "the Fires of Hell" && map == Map.Trammel ){ loc = new Point3D(3345, 1647, 0); }
			else if ( zone == "the Mines of Morinia" && map == Map.Trammel ){ loc = new Point3D(1022, 1369, 2); }
			else if ( zone == "the Perinian Depths" && map == Map.Trammel ){ loc = new Point3D(3619, 456, 0); }
			else if ( zone == "the Dungeon of Time Awaits" && map == Map.Trammel ){ loc = new Point3D(3831, 1494, 0); }
			else if ( zone == "Pirate Cave" && map == Map.Trammel ){ loc = new Point3D(1842, 2211, 0); }
			else if ( zone == "the Dragon's Maw" && map == Map.Trammel ){ loc = new Point3D(5315, 3430, 2); }
			else if ( zone == "the Cave of the Zuluu" && map == Map.Trammel ){ loc = new Point3D(5901, 3999, 0); }
			else if ( zone == "the Ratmen Lair" && map == Map.Trammel ){ loc = new Point3D(1303, 1458, 0); }
			else if ( zone == "the Caverns of Poseidon" && map == Map.Trammel ){ loc = new Point3D(198, 2295, 12); }
			else if ( zone == "the Tower of Brass" && map == Map.Trammel ){ loc = new Point3D(1593, 3376, 15); }
			else if ( zone == "the Forgotten Halls" && map == Map.Trammel ){ loc = new Point3D(3015, 944, 0); }

			else if ( zone == "the Vault of the Black Knight" && map == Map.Felucca ){ loc = new Point3D(1581, 202, 0); map = Map.Malas; }
			else if ( zone == "the Undersea Pass" && map == Map.Felucca ){ loc = new Point3D(1179, 1931, 0); }
			else if ( zone == "the Castle of Dracula" && map == Map.Felucca ){ loc = new Point3D(466, 3794, 0); }
			else if ( zone == "the Crypts of Dracula" && map == Map.Felucca ){ loc = new Point3D(466, 3794, 0); }
			else if ( zone == "the Lodoria Catacombs" && map == Map.Felucca ){ loc = new Point3D(1869, 2378, 0); }
			else if ( zone == "Dungeon Covetous" && map == Map.Felucca ){ loc = new Point3D(4019, 2436, 2); }
			else if ( zone == "Dungeon Deceit" && map == Map.Felucca ){ loc = new Point3D(2523, 757, 1); }
			else if ( zone == "Dungeon Despise" && map == Map.Felucca ){ loc = new Point3D(1278, 1852, 0); }
			else if ( zone == "Dungeon Destard" && map == Map.Felucca ){ loc = new Point3D(749, 630, 0); }
			else if ( zone == "the City of Embers" && map == Map.Felucca ){ loc = new Point3D(3196, 3318, 0); }
			else if ( zone == "Dungeon Hythloth" && map == Map.Felucca ){ loc = new Point3D(1634, 2805, 0); }
			else if ( zone == "the Frozen Hells" && map == Map.Felucca ){ loc = new Point3D(3769, 1092, 0); }
			else if ( zone == "the Ice Fiend Lair" && map == Map.Felucca ){ loc = new Point3D(3769, 1092, 0); }
			else if ( zone == "the Halls of Undermountain" && map == Map.Felucca ){ loc = new Point3D(959, 2669, 5); }
			else if ( zone == "Dungeon Shame" && map == Map.Felucca ){ loc = new Point3D(1405, 2338, 0); }
			else if ( zone == "Terathan Keep" && map == Map.Felucca ){ loc = new Point3D(624, 2403, 2); }
			else if ( zone == "the Volcanic Cave" && map == Map.Felucca ){ loc = new Point3D(3105, 3594, 0); }
			else if ( zone == "Dungeon Wrong" && map == Map.Felucca ){ loc = new Point3D(2252, 854, 1); }
			else if ( zone == "Stonegate Castle" && map == Map.Felucca ){ loc = new Point3D(1355, 404, 0); }
			else if ( zone == "the Ancient Elven Mine" && map == Map.Felucca ){ loc = new Point3D(1179, 1931, 0); }

			else if ( zone == "Dungeon of the Mad Archmage" && map == Map.TerMur ){ loc = new Point3D(464, 851, -60); }
			else if ( zone == "Dungeon of the Lich King" && map == Map.TerMur ){ loc = new Point3D(922, 1772, 26); }
			else if ( zone == "the Halls of Ogrimar" && map == Map.TerMur ){ loc = new Point3D(1107, 1380, 17); }
			else if ( zone == "the Ratmen Mines" && map == Map.TerMur ){ loc = new Point3D(157, 1369, 32); }
			else if ( zone == "Dungeon Rock" && map == Map.TerMur ){ loc = new Point3D(1092, 1038, 0); }
			else if ( zone == "the Storm Giant Lair" && map == Map.TerMur ){ loc = new Point3D(283, 466, 14); }
			else if ( zone == "the Corrupt Pass" && map == Map.TerMur ){ loc = new Point3D(155, 1125, 60); }
			else if ( zone == "the Tombs" && map == Map.TerMur ){ loc = new Point3D(222, 1361, 0); }
			else if ( zone == "the Undersea Castle" && map == Map.TerMur ){ loc = new Point3D(283, 409, 20); }
			else if ( zone == "the Azure Castle" && map == Map.TerMur ){ loc = new Point3D(774, 612, 15); }
			else if ( zone == "the Tomb of Kazibal" && map == Map.TerMur ){ loc = new Point3D(368, 298, 57); }
			else if ( zone == "the Catacombs of Azerok" && map == Map.TerMur ){ loc = new Point3D(1056, 424, 38); }

			else if ( zone == "the Ancient Prison" && map == Map.Malas ){ loc = new Point3D(748, 846, 1); }
			else if ( zone == "the Cave of Fire" && map == Map.Malas ){ loc = new Point3D(561, 1143, 0); }
			else if ( zone == "the Cave of Souls" && map == Map.Malas ){ loc = new Point3D(121, 1475, 0); }
			else if ( zone == "Dungeon Ankh" && map == Map.Malas ){ loc = new Point3D(465, 1435, 2); }
			else if ( zone == "Dungeon Bane" && map == Map.Malas ){ loc = new Point3D(310, 761, 2); }
			else if ( zone == "Dungeon Hate" && map == Map.Malas ){ loc = new Point3D(1459, 1220, 0); }
			else if ( zone == "Dungeon Scorn" && map == Map.Malas ){ loc = new Point3D(1463, 873, 2); }
			else if ( zone == "Dungeon Torment" && map == Map.Malas ){ loc = new Point3D(1690, 1225, 0); }
			else if ( zone == "Dungeon Vile" && map == Map.Malas ){ loc = new Point3D(1554, 991, 2); }
			else if ( zone == "Dungeon Wicked" && map == Map.Malas ){ loc = new Point3D(733, 260, 0); }
			else if ( zone == "Dungeon Wrath" && map == Map.Malas ){ loc = new Point3D(1803, 918, 0); }
			else if ( zone == "the Flooded Temple" && map == Map.Malas ){ loc = new Point3D(1069, 952, 2); }
			else if ( zone == "the Gargoyle Crypts" && map == Map.Malas ){ loc = new Point3D(1267, 936, 0); }
			else if ( zone == "the Serpent Sanctum" && map == Map.Malas ){ loc = new Point3D(1093, 1609, 0); }
			else if ( zone == "the Tomb of the Fallen Wizard" && map == Map.Malas ){ loc = new Point3D(1056, 1338, 0); }

			else if ( zone == "the Blood Temple" && map == Map.TerMur ){ loc = new Point3D(1258, 1231, 0); map = Map.Tokuno; }
            else if (zone == "the Altar of the Blood God" && map == Map.TerMur) { loc = new Point3D(1133, 1036, 30); map = Map.Tokuno; }
            else if ( zone == "the Ice Queen Fortress" && map == Map.TerMur ){ loc = new Point3D(319, 324, 5); map = Map.Tokuno; }
			else if ( zone == "the Scurvy Reef" && map == Map.TerMur ){ loc = new Point3D(713, 493, 1); map = Map.Tokuno; }
			else if ( zone == "the Glacial Scar" && map == Map.Ilshenar ){ loc = new Point3D(238, 171, 0); map = Map.Tokuno; }
			else if ( zone == "the Temple of Osirus" && map == Map.Felucca ){ loc = new Point3D(601, 819, 20); map = Map.Tokuno; }
			else if ( zone == "the Sanctum of Saltmarsh" && map == Map.Felucca ){ loc = new Point3D(601, 819, 20); map = Map.Tokuno; }
// final needs to be updated
			else if ( zone == "Morgaelin's Inferno" && map == Map.Felucca ){ loc = new Point3D(1459, 100, 0); map = Map.Ilshenar; }
			else if ( zone == "the Zealan Tombs" && map == Map.Felucca ){ loc = new Point3D(1094, 1229, 0); map = Map.Ilshenar; }
			else if ( zone == "Argentrock Castle" && map == Map.Felucca ){ loc = new Point3D(103, 999, 36); map = Map.Ilshenar; }
			else if ( zone == "the Daemon's Crag" && map == Map.Felucca ){ loc = new Point3D(1481, 835, 0); map = Map.Ilshenar; }
			else if ( zone == "the Stygian Abyss" && map == Map.Ilshenar ){ loc = new Point3D(824, 907, 0); }
			else if ( zone == "the Hall of the Mountain King" && map == Map.Felucca ){ loc = new Point3D(130, 102, 0); map = Map.Ilshenar; }
			else if ( zone == "the Depths of Carthax Lake" && map == Map.Felucca ){ loc = new Point3D(926, 874, 0); map = Map.Ilshenar; }
			else if ( zone == "the Ancient Sky Ship" && map == Map.TerMur ){ loc = new Point3D(66, 561, 0); map = Map.Ilshenar; }

			string my_location = "";

			int xLong = 0, yLat = 0;
			int xMins = 0, yMins = 0;
			bool xEast = false, ySouth = false;

			if ( Sextant.Format( loc, map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
			{
				my_location = String.Format( "{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
			}

            return my_location;
        }

        public static string GetDungeonListing()
        {
			// THIS RETURNS AN ALPHABETICAL LIST (BY WORLD) OF DUNGEONS & LOCATIONS

			int i = 0;
			string dungeon = "";
			string listing = "";
			string location = "";

			while ( i < 85 )
			{
				i++;

				if ( i == 1 ){ dungeon = "Dardin's Pit"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Sosaria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 2 ){ dungeon = "Dungeon Clues"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Sosaria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 3 ){ dungeon = "Dungeon Abandon"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Sosaria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 4 ){ dungeon = "Dungeon Exodus"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Sosaria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 5 ){ dungeon = "Pirate Cave"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Sosaria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 6 ){ dungeon = "the Ancient Pyramid"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Sosaria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 7 ){ dungeon = "the Cave of Banished Mages"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Sosaria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 8 ){ dungeon = "the Caverns of Poseidon"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Sosaria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 9 ){ dungeon = "the Dungeon of Time Awaits"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Sosaria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 10 ){ dungeon = "the Fires of Hell"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Sosaria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 11 ){ dungeon = "the Forgotten Halls"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Sosaria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 12 ){ dungeon = "the Mines of Morinia"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Sosaria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 13 ){ dungeon = "the Perinian Depths"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Sosaria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 14 ){ dungeon = "the Ratmen Lair"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Sosaria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 15 ){ dungeon = "the Cave of the Zuluu"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Ambrosia<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 16 ){ dungeon = "the City of the Dead"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Ambrosia<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 17 ){ dungeon = "the Dragon's Maw"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Ambrosia<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 18 ){ dungeon = "the Mausoleum"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Umber Veil<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 19 ){ dungeon = "the Tower of Brass"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Umber Veil<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 20 ){ dungeon = "Dungeon Covetous"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 21 ){ dungeon = "Dungeon Deceit"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 22 ){ dungeon = "Dungeon Despise"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 23 ){ dungeon = "Dungeon Destard"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 24 ){ dungeon = "Dungeon Hythloth"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 25 ){ dungeon = "Dungeon Shame"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 26 ){ dungeon = "Dungeon Wrong"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 27 ){ dungeon = "Stonegate Castle"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 28 ){ dungeon = "Terathan Keep"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 29 ){ dungeon = "the Ancient Elven Mine"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 30 ){ dungeon = "the Castle of Dracula"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 31 ){ dungeon = "the City of Embers"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 32 ){ dungeon = "the Crypts of Dracula"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 33 ){ dungeon = "the Frozen Hells"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 34 ){ dungeon = "the Halls of Undermountain"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 35 ){ dungeon = "the Ice Fiend Lair"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 36 ){ dungeon = "the Lodoria Catacombs"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 37 ){ dungeon = "the Undersea Pass"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 38 ){ dungeon = "the Volcanic Cave"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Lodoria<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 39 ){ dungeon = "Dungeon Ankh"; location = GetAreaEntrance( dungeon, Map.Malas ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 40 ){ dungeon = "Dungeon Bane"; location = GetAreaEntrance( dungeon, Map.Malas ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 41 ){ dungeon = "Dungeon Hate"; location = GetAreaEntrance( dungeon, Map.Malas ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 42 ){ dungeon = "Dungeon Scorn"; location = GetAreaEntrance( dungeon, Map.Malas ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 43 ){ dungeon = "Dungeon Torment"; location = GetAreaEntrance( dungeon, Map.Malas ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 44 ){ dungeon = "Dungeon Vile"; location = GetAreaEntrance( dungeon, Map.Malas ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 45 ){ dungeon = "Dungeon Wicked"; location = GetAreaEntrance( dungeon, Map.Malas ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 46 ){ dungeon = "Dungeon Wrath"; location = GetAreaEntrance( dungeon, Map.Malas ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 47 ){ dungeon = "the Ancient Prison"; location = GetAreaEntrance( dungeon, Map.Malas ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 48 ){ dungeon = "the Cave of Fire"; location = GetAreaEntrance( dungeon, Map.Malas ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 49 ){ dungeon = "the Cave of Souls"; location = GetAreaEntrance( dungeon, Map.Malas ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 50 ){ dungeon = "the Flooded Temple"; location = GetAreaEntrance( dungeon, Map.Malas ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 51 ){ dungeon = "the Gargoyle Crypts"; location = GetAreaEntrance( dungeon, Map.Malas ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 52 ){ dungeon = "the Serpent Sanctum"; location = GetAreaEntrance( dungeon, Map.Malas ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 53 ){ dungeon = "the Tomb of the Fallen Wizard"; location = GetAreaEntrance( dungeon, Map.Malas ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 54 ){ dungeon = "the Vault of the Black Knight"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Serpent Island<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 55 ){ dungeon = "the Blood Temple"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Isles of Dread<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 56 ){ dungeon = "the Glacial Scar"; location = GetAreaEntrance( dungeon, Map.Ilshenar ); listing = listing + "Isles of Dread<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 57 ){ dungeon = "the Ice Queen Fortress"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Isles of Dread<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 58 ){ dungeon = "the Sanctum of Saltmarsh"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Isles of Dread<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 59 ){ dungeon = "the Scurvy Reef"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Isles of Dread<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 60 ){ dungeon = "the Temple of Osirus"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Isles of Dread<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 61 ){ dungeon = "Dungeon of the Lich King"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Savaged Empire<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 62 ){ dungeon = "Dungeon of the Mad Archmage"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Savaged Empire<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 63 ){ dungeon = "Dungeon Rock"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Savaged Empire<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 64 ){ dungeon = "the Azure Castle"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Savaged Empire<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 65 ){ dungeon = "the Catacombs of Azerok"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Savaged Empire<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 66 ){ dungeon = "the Corrupt Pass"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Savaged Empire<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 67 ){ dungeon = "the Halls of Ogrimar"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Savaged Empire<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 68 ){ dungeon = "the Ratmen Mines"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Savaged Empire<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 69 ){ dungeon = "the Storm Giant Lair"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Savaged Empire<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 70 ){ dungeon = "the Tomb of Kazibal"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Savaged Empire<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 71 ){ dungeon = "the Tombs"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Savaged Empire<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 72 ){ dungeon = "the Undersea Castle"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Savaged Empire<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 73 ){ dungeon = "the Crypts of Kuldar"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Kuldar<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 74 ){ dungeon = "the Kuldara Sewers"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Kuldar<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 75 ){ dungeon = "the Valley of Dark Druids"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Kuldar<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 76 ){ dungeon = "Vordo's Castle"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Kuldar<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 77 ){ dungeon = "Vordo's Dungeon"; location = GetAreaEntrance( dungeon, Map.Trammel ); listing = listing + "Kuldar<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 78 ){ dungeon = "Argentrock Castle"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Underworld<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 79 ){ dungeon = "the Ancient Sky Ship"; location = GetAreaEntrance( dungeon, Map.TerMur ); listing = listing + "Underworld<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 80 ){ dungeon = "Morgaelin's Inferno"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Underworld<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 81 ){ dungeon = "the Daemon's Crag"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Underworld<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 82 ){ dungeon = "the Depths of Carthax Lake"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Underworld<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 83 ){ dungeon = "the Hall of the Mountain King"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Underworld<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 84 ){ dungeon = "the Stygian Abyss"; location = GetAreaEntrance( dungeon, Map.Ilshenar ); listing = listing + "Underworld<br>" + dungeon + "<br>" + location + "<br><br>"; }
				else if ( i == 85 ){ dungeon = "the Zealan Tombs"; location = GetAreaEntrance( dungeon, Map.Felucca ); listing = listing + "Underworld<br>" + dungeon + "<br>" + location + "<br><br>"; }
			}

            return listing;
        }

         public static Point3D GetRandomTown(string world, bool mainlandOnly) {
         	Point3D loc = new Point3D(0, 0, 0);
         	string xmlFileName = "";
         	Map map = Map.Trammel;
         	List<string> nonMainlandNames = new List<string>() {
         		"Dawn", "Death Gulch", "Fawn", "Glacial Coast", "Iceclad Fisherman", "Mountain Crest"
         	};
			switch (world) {
			    case "the Bottle World of Kuldar": //Trammel
			    case "the Land of Ambrosia":
			    case "the Island of Umber Veil":
			    case "the Moon of Luna":
			    case "the Land of Sosaria":
	             	xmlFileName = "trammel.xml";
			    break;
			    case "the Town of Skara Brae": //Felucca
			    case "the Land of Lodoria":
			   		nonMainlandNames = new List<string>() {
			   			"Elidor", "Glacial Hills", "Greensky", "Kraken Reef"
			   		};
			        xmlFileName = "felucca.xml";
			        map = Map.Felucca;
			    break;
			    // case "the Underworld": //Ilshenar
			    // case "DarkMoor":
			    //      xmlFileName = "ilshenar.xml";
			    // break;
			    case "the Serpent Island": //Malas
			        xmlFileName = "malas.xml";
			        map = Map.Malas;
			    break;
			    case "the Isles of Dread": //Tokuno
			        xmlFileName = "tokuno.xml";
			        map = Map.Tokuno;
			        break;
			    case "the Savaged Empire": //TerMur
			    	nonMainlandNames = new List<string>() {
			    		"Barbarian Mines", "Barako Mines", "Savage Sea Docks"
			    	};
			        xmlFileName = "termur.xml";
			        map = Map.TerMur;
			    break;
			}
         	if (!String.IsNullOrEmpty(xmlFileName)) {
         		LocationTree tree = new LocationTree( xmlFileName, Map.Trammel );
         		// XDocument document = XDocument.Load("trammel.xml");
	        	ParentNode root = tree.Root;
	        	ParentNode tierOne = (ParentNode)root.Children[0];
	        	if (tierOne.Children.Length > 0) { 
	        		ParentNode towns = (ParentNode)tierOne.Children[0];
	        		int random = Utility.Random(towns.Children.Length);
	        		if (towns.Children[random].GetType() == typeof( ChildNode )) {
	        			ChildNode town = (ChildNode)towns.Children[random];
	        			if (mainlandOnly) {
	        				List<string> filtered = nonMainlandNames.FindAll(n => n == town.Name);		
							if (filtered.Count > 0) {
		        				return GetRandomTown(world, mainlandOnly);
		        			}
	        			}
		        		if (town != null) {
		        			return town.Location;
		        		}
	        		} else {
	        			return GetRandomTown(world, mainlandOnly);
	        		}	
	        	}
	        }
         	return loc;
        }

        // public static Point3D GetRandomTown( string world ) {
        	
        // }

		public static Point3D GetRandomLocation( string world, string scape )
		{
            bool LandOk = false;
			Point3D loc = new Point3D(0, 0, 0);
			Point3D failover = new Point3D(0, 0, 0);
			Point3D testLocation = new Point3D(0, 0, 0);

			Map tl = Map.Trammel;
            int tx = 0;
			int ty = 0;
			int tz = 0;
			int tm = 0;
			int r = 0;
			int swrapx = 0;
			int swrapy = 0;

			if ( scape != "land" ){ swrapx = 26; swrapy = 26; }

            while ( tm < 1 )
            {
                if (world == "the Bottle World of Kuldar")
                {
					tl = Map.Trammel;
                    tx = Utility.RandomMinMax( 6166+swrapx, 7204-swrapx );
                    ty = Utility.RandomMinMax( 829+swrapy, 2741-swrapy );
                    tz = tl.GetAverageZ(tx, ty);
					if ( scape == "land" ){ failover = new Point3D(6722, 1338, 0); } else { failover = new Point3D(6348, 1096, -5); }
                }
                else if (world == "the Land of Ambrosia")
                {
					tl = Map.Trammel;
                    tx = Utility.RandomMinMax( 5160+swrapx, 6163-swrapx );
                    ty = Utility.RandomMinMax( 3036+swrapy, 4095-swrapy );
                    tz = tl.GetAverageZ(tx, ty);
					if ( scape == "land" ){ failover = new Point3D(5599, 3523, 22); } else { failover = new Point3D(5512, 3232, -5); }
                }
                else if (world == "the Island of Umber Veil")
                {
					tl = Map.Trammel;
                    tx = Utility.RandomMinMax( 737+swrapx, 2310-swrapx );
                    ty = Utility.RandomMinMax( 3130+swrapy, 4095-swrapy );
                    tz = tl.GetAverageZ(tx, ty);
					if ( scape == "land" ){ failover = new Point3D(1766, 3638, 22); } else { failover = new Point3D(880, 3796, -5); }
                }
                else if (world == "the Moon of Luna")
                {
					tl = Map.Trammel;
                    tx = Utility.RandomMinMax( 5856+swrapx, 6164-swrapx );
                    ty = Utility.RandomMinMax( 2740+swrapy, 3018-swrapy );
                    tz = tl.GetAverageZ(tx, ty);
					if ( scape == "land" ){ failover = new Point3D(5902, 2793, 0); } else { failover = new Point3D(112, 1816, -5); }
                }
                else if (world == "the Town of Skara Brae")
                {
					tl = Map.Felucca;
                    tx = Utility.RandomMinMax( 6898+swrapx, 7068-swrapx );
                    ty = Utility.RandomMinMax( 130+swrapy, 314-swrapy );
                    tz = tl.GetAverageZ(tx, ty);
					if ( scape == "land" ){ failover = new Point3D(6927, 300, 0); } else { failover = new Point3D(112, 1816, -5); }
                }
                else if (world == "the Land of Lodoria")
                {
					tl = Map.Felucca;
                    tx = Utility.RandomMinMax( 0+swrapx, 5157-swrapx );
                    ty = Utility.RandomMinMax( 0+swrapy, 4095-swrapy );
                    tz = tl.GetAverageZ(tx, ty);
					if ( scape == "land" ){ failover = new Point3D(1050, 2236, 0); } else { failover = new Point3D(3470, 2504, -5); }
                }
				else if (world == "the Underworld")
				{
					if (Utility.RandomBool())
					{
						tl = Map.Ilshenar;
						tx = Utility.RandomMinMax( 1013+swrapx, 2013-swrapx );
						ty = Utility.RandomMinMax( 10+swrapy, 775-swrapy );
						tz = tl.GetAverageZ(tx, ty);
						if ( scape == "land" ){ failover = new Point3D(1433, 855, 0); } else { failover = new Point3D(547, 1441, -5); }
					}
					else
					{
						tl = Map.Ilshenar;
						tx = Utility.RandomMinMax( 1013+swrapx, 1646-swrapx );
						ty = Utility.RandomMinMax( 775+swrapy, 981-swrapy );
						tz = tl.GetAverageZ(tx, ty);
						if ( scape == "land" ){ failover = new Point3D(1433, 855, 0); } else { failover = new Point3D(547, 1441, -5); }
					}
				}

				else if (world == "DarkMoor")
				{
						tl = Map.Ilshenar;
						tx = Utility.RandomMinMax( 50+swrapx, 1007-swrapx );
						ty = Utility.RandomMinMax( 10+swrapy, 1279-swrapy );
						tz = tl.GetAverageZ(tx, ty);
						if ( scape == "land" ){ failover = new Point3D(1433, 855, 0); } else { failover = new Point3D(547, 1441, -5); }
				}		
				
                else if (world == "the Serpent Island")
                {
					tl = Map.Malas;
                    tx = Utility.RandomMinMax( 0+swrapx, 1908-swrapx );
                    ty = Utility.RandomMinMax( 0+swrapy, 2047-swrapy );
                    tz = tl.GetAverageZ(tx, ty);
					if ( scape == "land" ){ failover = new Point3D(286, 1392, 2); } else { failover = new Point3D(1605, 536, -5); }
                }
                else if (world == "the Isles of Dread")
                {
					tl = Map.Tokuno;
                    tx = Utility.RandomMinMax( 0+swrapx, 1446-swrapx );
                    ty = Utility.RandomMinMax( 0+swrapy, 1446-swrapy );
                    tz = tl.GetAverageZ(tx, ty);
					if ( scape == "land" ){ failover = new Point3D(1176, 816, 0); } else { failover = new Point3D(626, 643, -5); }
                }
                else if (world == "the Savaged Empire")
                {
					tl = Map.TerMur;
                    tx = Utility.RandomMinMax( 170+swrapx, 1200-swrapx );
                    ty = Utility.RandomMinMax( 10+swrapy, 1795-swrapy );
                    tz = tl.GetAverageZ(tx, ty);
					if ( scape == "land" ){ failover = new Point3D(653, 1269, -2); } else { failover = new Point3D(320, 638, -5); }
                }
                else if (world == "the Land of Sosaria")
                {
					tl = Map.Trammel;
                    tx = Utility.RandomMinMax( 0+swrapx, 5158-swrapx );
                    ty = Utility.RandomMinMax( 0+swrapy, 3128-swrapy );
                    tz = tl.GetAverageZ(tx, ty);
					if ( scape == "land" ){ failover = new Point3D(2575, 1680, 20); } else { failover = new Point3D(112, 1816, -5); }
                }

                LandTile t = tl.Tiles.GetLandTile(tx, ty);

				if ( scape == "land" )
				{
					LandOk = PassableTile ( t.ID, "any" );

					Mobile mSp = new Rat();
					mSp.Name = "locator";
					mSp.MoveToWorld(new Point3D(tx, ty, tz), tl);
					Region RatReg = mSp.Region;
					mSp.Delete();
					testLocation = new Point3D(tx, ty, tz);

					if (LandOk && tl.CanSpawnMobile(tx, ty, tz) && ( Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( tl, testLocation ) ) || RatReg.IsPartOf(typeof(Regions.BardTownRegion)) ) )
					{
						loc = new Point3D(tx, ty, tz);
						tm = 1;
					}
				}
				else // GET WATER TILES
				{
					if ( Server.Misc.Worlds.IsWaterTile( t.ID, 0 ) && Server.Misc.Worlds.TestOcean ( tl, tx, ty, 2 ) ) { LandOk = true; }

					Point3D locale = new Point3D(tx, ty, tz);
					Region reg = Region.Find( locale, tl );

					if ( tz != -5 ){ LandOk = false; }

					if ( LandOk && Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( tl, locale ) ) )
					{
						loc = new Point3D(tx, ty, tz);
						tm = 1;
					}
				}

				r++; // SAFETY CATCH
				if ( r > 1000 && tm != 1)
                {
                    loc = failover;
					tm = 1;
                }
            }
            return loc;
        }

		public static Point3D GetBoatWater( int x, int y, Map map, int range )
		{
			bool WaterOk = false;
			Point3D loc = new Point3D(0, 0, 0);

			Map tm = map;
			int tx = 0;
			int ty = 0;
			int tz = 0;
			int r = 0;
			LandTile t = tm.Tiles.GetLandTile(tx, ty);

            while ( !WaterOk )
            {
				tx = Utility.RandomMinMax( x+range, x-range );
				ty = Utility.RandomMinMax( y+range, y-range );
				tz = tm.GetAverageZ(tx, ty);

				t = tm.Tiles.GetLandTile(tx, ty);

				if ( Server.Misc.Worlds.IsWaterTile ( t.ID, 0 ) ){ WaterOk = true; }

				if ( WaterOk )
				{
					loc = new Point3D(tx, ty, tz);
				}

				r++; // SAFETY CATCH
				if ( r > 50 && !WaterOk )
                {
					WaterOk = true;
                }
            }
            return loc;
        }

		public static bool IsWaterTile ( int id, int harvest )
		{
			if ( harvest == 0 && ( id==0x00A8 || id==0x00A9 || id==0x00AA || id==0x00AB || id==0x0136 || id==0x0137 || id==0x1559 || id==0x1796 || id==0x1797 || id==0x1798 || id==0x1799 || id==0x179A || id==0x179B || id==0x179C || id==0x179D || id==0x179E || id==0x179F || id==0x17A0 || id==0x17A1 || id==0x17A2 || id==0x17A3 || id==0x17A4 || id==0x17A5 || id==0x17A6 || id==0x17A7 || id==0x17A8 || id==0x17A9 || id==0x17AA || id==0x17AB || id==0x17AC || id==0x17AD || id==0x17AE || id==0x17AF || id==0x17B0 || id==0x17B1 || id==0x17B2 || id==0x17BB || id==0x17BC || id==0x346E || id==0x346F || id==0x3470 || id==0x3471 || id==0x3472 || id==0x3473 || id==0x3474 || id==0x3475 || id==0x3476 || id==0x3477 || id==0x3478 || id==0x3479 || id==0x347A || id==0x347B || id==0x347C || id==0x347D || id==0x347E || id==0x347F || id==0x3480 || id==0x3481 || id==0x3482 || id==0x3483 || id==0x3484 || id==0x3485 || id==0x3494 || id==0x3495 || id==0x3496 || id==0x3497 || id==0x3498 || id==0x349A || id==0x349B || id==0x349C || id==0x349D || id==0x349E || id==0x34A0 || id==0x34A1 || id==0x34A2 || id==0x34A3 || id==0x34A4 || id==0x34A6 || id==0x34A7 || id==0x34A8 || id==0x34A9 || id==0x34AA || id==0x34AB || id==0x34B8 || id==0x34B9 || id==0x34BA || id==0x34BB || id==0x34BD || id==0x34BE || id==0x34BF || id==0x34C0 || id==0x34C2 || id==0x34C3 || id==0x34C4 || id==0x34C5 || id==0x34C7 || id==0x34C8 || id==0x34C9 || id==0x34CA || id==0x34D2 || id==0x3529 || id==0x352A || id==0x352B || id==0x352C || id==0x3531 || id==0x3532 || id==0x3533 || id==0x3534 || id==0x3535 || id==0x3536 || id==0x3537 || id==0x3538 || id==0x353D || id==0x353E || id==0x353F || id==0x3540 || id==0x3541 || id==0x55F0 || id==0x55F1 || id==0x55F2 || id==0x55F3 || id==0x55F4 || id==0x55F5 || id==0x55F6 || id==0x55F7 || id==0x55F8 || id==0x55F9 || id==0x55FA || id==0x55FB || id==0x55FC || id==0x55FD || id==0x55FE || id==0x55FF || id==0x5600 || id==0x5601 || id==0x5602 || id==0x5603 || id==0x5604 || id==0x5605 || id==0x5606 || id==0x5607 || id==0x5608 || id==0x5609 || id==0x560A || id==0x560B || id==0x560C || id==0x560D || id==0x560E || id==0x560F || id==0x5610 || id==0x5611 || id==0x5612 || id==0x5613 || id==0x5614 || id==0x5615 || id==0x5616 || id==0x5617 || id==0x5618 || id==0x5619 || id==0x561A || id==0x561B || id==0x561C || id==0x561D || id==0x561E || id==0x561F || id==0x5620 || id==0x5621 || id==0x5622 || id==0x5623 || id==0x5624 || id==0x5633 || id==0x5634 || id==0x5635 || id==0x5636 || id==0x5637 || id==0x5638 || id==0x5639 || id==0x563A || id==0x563B || id==0x563C || id==0x563D || id==0x563F || id==0x5640 || id==0x5641 || id==0x5642 || id==0x5643 || id==0x5644 || id==0x5645 || id==0x5646 || id==0x5647 || id==0x5648 || id==0x5649 || id==0x564A || id==0x5657 || id==0x5658 || id==0x5659 || id==0x565A || id==0x565B || id==0x565C || id==0x565D || id==0x565E || id==0x565F || id==0x5660 || id==0x5661 || id==0x5662 || id==0x5663 || id==0x5664 || id==0x5665 || id==0x5666 || id==0x5667 || id==0x5668 || id==0x5669 || id==0x566A || id==0x566B || id==0x566C || id==0x566D || id==0x566E || id==0x566F ) )
				return true;

			else if ( harvest == 1 )
			{
				foreach( int t in Server.Engines.Harvest.Fishing.m_WaterTiles ) //fishing system add 0x4000 to each tile id 
				{
					if (t != 0 && t == id)
						return true;
				}
			}

			return false;
		}

		public static bool IsMiningTile ( int id, int sand )
		{
			if (sand == 0)
			{
				foreach( int t in Server.Engines.Harvest.Mining.m_MountainAndCaveTiles )
				{
					if (t != 0 && t == id)
						return true;
				}
			}
			else if (sand == 1)
			{
				foreach( int t in Server.Engines.Harvest.Mining.m_SandTiles )
				{
					if (t != 0 && t == id)
						return true;
				}
			}

			return false;
		}

		public static bool IsTreeTile ( int id )
		{

				foreach( int t in Server.Engines.Harvest.Lumberjacking.TreeTiles )
				{
					if (t != 0 && t == id)
						return true;
				}
				foreach( int t in Server.Engines.Harvest.Lumberjacking.m_TreeTiles ) //lumberjack system add 0x4000 to each tile id for some reason?!?!?! wth
				{
					if (t != 0 && t == id)
						return true;
				}

			return false;
		}

		public static bool TestTile ( Map map, int x, int y, string category )
		{
			if (map == null)
				return false;
			
			Region reg = Region.Find( new Point3D( x, y, 0 ), map );
				if ( reg.IsPartOf( typeof( DungeonRegion ) ) ){ return false; }

			int results = 0;

			LandTile landTile1 = map.Tiles.GetLandTile( x-1, y-1 );
			LandTile landTile2 = map.Tiles.GetLandTile( x, y-1 );
			LandTile landTile3 = map.Tiles.GetLandTile( x+1, y-1 );
			LandTile landTile4 = map.Tiles.GetLandTile( x-1, y );
			LandTile landTile5 = map.Tiles.GetLandTile( x, y );
			LandTile landTile6 = map.Tiles.GetLandTile( x+1, y );
			LandTile landTile7 = map.Tiles.GetLandTile( x-1, y+1 );
			LandTile landTile8 = map.Tiles.GetLandTile( x, y+1 );
			LandTile landTile9 = map.Tiles.GetLandTile( x+1, y+1 );

			if ( Server.Misc.Worlds.PassableTile ( landTile1.ID, category ) ){ results ++; }
				if ( Server.Misc.Worlds.BlockedTile ( landTile1.ID, category ) ){ results ++; }
			if ( Server.Misc.Worlds.PassableTile ( landTile2.ID, category ) ){ results ++; }
				if ( Server.Misc.Worlds.BlockedTile ( landTile2.ID, category ) ){ results ++; }
			if ( Server.Misc.Worlds.PassableTile ( landTile3.ID, category ) ){ results ++; }
				if ( Server.Misc.Worlds.BlockedTile ( landTile3.ID, category ) ){ results ++; }
			if ( Server.Misc.Worlds.PassableTile ( landTile4.ID, category ) ){ results ++; }
				if ( Server.Misc.Worlds.BlockedTile ( landTile4.ID, category ) ){ results ++; }
			if ( Server.Misc.Worlds.PassableTile ( landTile5.ID, category ) ){ results ++; }
				if ( Server.Misc.Worlds.BlockedTile ( landTile5.ID, category ) ){ results ++; }
			if ( Server.Misc.Worlds.PassableTile ( landTile6.ID, category ) ){ results ++; }
				if ( Server.Misc.Worlds.BlockedTile ( landTile6.ID, category ) ){ results ++; }
			if ( Server.Misc.Worlds.PassableTile ( landTile7.ID, category ) ){ results ++; }
				if ( Server.Misc.Worlds.BlockedTile ( landTile7.ID, category ) ){ results ++; }
			if ( Server.Misc.Worlds.PassableTile ( landTile8.ID, category ) ){ results ++; }
				if ( Server.Misc.Worlds.BlockedTile ( landTile8.ID, category ) ){ results ++; }
			if ( Server.Misc.Worlds.PassableTile ( landTile9.ID, category ) ){ results ++; }
				if ( Server.Misc.Worlds.BlockedTile ( landTile9.ID, category ) ){ results ++; }

			if ( results > 4 )
				return true;

			return false;
		}

		public static bool TestMountain ( Map map, int x, int y, int distance )
		{
			Region reg = Region.Find( new Point3D( x, y, 0 ), map );
				if ( reg.IsPartOf( typeof( DungeonRegion ) ) ){ return false; }

			int results = 0;

			LandTile landRock1 = map.Tiles.GetLandTile( x-distance, y-distance );
			LandTile landRock2 = map.Tiles.GetLandTile( x, y-distance );
			LandTile landRock3 = map.Tiles.GetLandTile( x+distance, y-distance );
			LandTile landRock4 = map.Tiles.GetLandTile( x-distance, y );
			LandTile landRock5 = map.Tiles.GetLandTile( x+distance, y );
			LandTile landRock6 = map.Tiles.GetLandTile( x-distance, y+distance );
			LandTile landRock7 = map.Tiles.GetLandTile( x, y+distance );
			LandTile landRock8 = map.Tiles.GetLandTile( x+distance, y+distance );

			if ( Server.Misc.Worlds.BlockedTile ( landRock1.ID, "rock" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( landRock2.ID, "rock" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( landRock3.ID, "rock" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( landRock4.ID, "rock" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( landRock5.ID, "rock" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( landRock6.ID, "rock" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( landRock7.ID, "rock" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( landRock8.ID, "rock" ) ){ results ++; }

			if ( results > 0 )
				return true;

			return false;
		}

		public static bool TestOcean( Map map, int x, int y, int distance )
		{
			Region reg = Region.Find( new Point3D( x, y, 0 ), map );
				if ( reg.IsPartOf( typeof( DungeonRegion ) ) ){ return false; }

			int results = 0;

			LandTile seaTile1 = map.Tiles.GetLandTile( x-distance, y-distance );
			LandTile seaTile2 = map.Tiles.GetLandTile( x, y-distance );
			LandTile seaTile3 = map.Tiles.GetLandTile( x+distance, y-distance );
			LandTile seaTile4 = map.Tiles.GetLandTile( x-distance, y );
			LandTile seaTile5 = map.Tiles.GetLandTile( x+distance, y );
			LandTile seaTile6 = map.Tiles.GetLandTile( x-distance, y+distance );
			LandTile seaTile7 = map.Tiles.GetLandTile( x, y+distance );
			LandTile seaTile8 = map.Tiles.GetLandTile( x+distance, y+distance );

			if ( Server.Misc.Worlds.BlockedTile ( seaTile1.ID, "water" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( seaTile2.ID, "water" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( seaTile3.ID, "water" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( seaTile4.ID, "water" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( seaTile5.ID, "water" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( seaTile6.ID, "water" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( seaTile7.ID, "water" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( seaTile8.ID, "water" ) ){ results ++; }

			if ( results > 0 )
				return true;

			return false;
		}

		public static bool TestShore( Map map, int x, int y, int distance )
		{
			int results = 0;

			LandTile seaTile1 = map.Tiles.GetLandTile( x-distance, y-distance );
			LandTile seaTile2 = map.Tiles.GetLandTile( x, y-distance );
			LandTile seaTile3 = map.Tiles.GetLandTile( x+distance, y-distance );
			LandTile seaTile4 = map.Tiles.GetLandTile( x-distance, y );
			LandTile seaTile5 = map.Tiles.GetLandTile( x+distance, y );
			LandTile seaTile6 = map.Tiles.GetLandTile( x-distance, y+distance );
			LandTile seaTile7 = map.Tiles.GetLandTile( x, y+distance );
			LandTile seaTile8 = map.Tiles.GetLandTile( x+distance, y+distance );

			if ( Server.Misc.Worlds.BlockedTile ( seaTile1.ID, "water" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( seaTile2.ID, "water" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( seaTile3.ID, "water" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( seaTile4.ID, "water" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( seaTile5.ID, "water" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( seaTile6.ID, "water" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( seaTile7.ID, "water" ) ){ results ++; }
			if ( Server.Misc.Worlds.BlockedTile ( seaTile8.ID, "water" ) ){ results ++; }

			if ( results > 7 )
				return false;

			return true;
		}

		public static bool PassableTile ( int id, string category )
		{
			if ( ( category == "cave" || category == "any" ) && ( 
				id == 	0x0245	 || 
				id == 	0x0246	 || 
				id == 	0x0247	 || 
				id == 	0x0248	 || 
				id == 	0x0249	 || 
				id == 	0x063B	 || 
				id == 	0x063C	 || 
				id == 	0x063D	 || 
				id == 	0x063E
			)){ return true; }

			if ( ( category == "dirt" || category == "any" ) && ( 
				id ==	0x0071	||
				id ==	0x0072	||
				id ==	0x0073	||
				id ==	0x0074	||
				id ==	0x0075	||
				id ==	0x0076	||
				id ==	0x0077	||
				id ==	0x0078	||
				id ==	0x0079	||
				id ==	0x007A	||
				id ==	0x007B	||
				id ==	0x007C	||
				id ==	0x0082	||
				id ==	0x0083	||
				id ==	0x0085	||
				id ==	0x0086	||
				id ==	0x0087	||
				id ==	0x0088	||
				id ==	0x0089	||
				id ==	0x008A	||
				id ==	0x008B	||
				id ==	0x008C	||
				id ==	0x00E8	||
				id ==	0x00E9	||
				id ==	0x00EA	||
				id ==	0x00EB	||
				id ==	0x0141	||
				id ==	0x0142	||
				id ==	0x0143	||
				id ==	0x0144	||
				id ==	0x014C	||
				id ==	0x014D	||
				id ==	0x014E	||
				id ==	0x014F	||
				id ==	0x0169	||
				id ==	0x016A	||
				id ==	0x016B	||
				id ==	0x016C	||
				id ==	0x016D	||
				id ==	0x016E	||
				id ==	0x016F	||
				id ==	0x0170	||
				id ==	0x0171	||
				id ==	0x0172	||
				id ==	0x0173	||
				id ==	0x0174	||
				id ==	0x01DC	||
				id ==	0x01DD	||
				id ==	0x01DE	||
				id ==	0x01DF	||
				id ==	0x01E0	||
				id ==	0x01E1	||
				id ==	0x01E2	||
				id ==	0x01E3	||
				id ==	0x01E4	||
				id ==	0x01E5	||
				id ==	0x01E6	||
				id ==	0x01E7	||
				id ==	0x01EC	||
				id ==	0x01ED	||
				id ==	0x01EE	||
				id ==	0x01EF	||
				id ==	0x0272	||
				id ==	0x0273	||
				id ==	0x0274	||
				id ==	0x0275	||
				id ==	0x027E	||
				id ==	0x027F	||
				id ==	0x0280	||
				id ==	0x0281	||
				id ==	0x032C	||
				id ==	0x032D	||
				id ==	0x032E	||
				id ==	0x032F	||
				id ==	0x033D	||
				id ==	0x033E	||
				id ==	0x033F	||
				id ==	0x0340	||
				id ==	0x0345	||
				id ==	0x0346	||
				id ==	0x0347	||
				id ==	0x0348	||
				id ==	0x0349	||
				id ==	0x034A	||
				id ==	0x034B	||
				id ==	0x034C	||
				id ==	0x0355	||
				id ==	0x0356	||
				id ==	0x0357	||
				id ==	0x0358	||
				id ==	0x0367	||
				id ==	0x0368	||
				id ==	0x0369	||
				id ==	0x036A	||
				id ==	0x036B	||
				id ==	0x036C	||
				id ==	0x036D	||
				id ==	0x036E	||
				id ==	0x0377	||
				id ==	0x0378	||
				id ==	0x0379	||
				id ==	0x037A	||
				id ==	0x038D	||
				id ==	0x038E	||
				id ==	0x038F	||
				id ==	0x0390	||
				id ==	0x0395	||
				id ==	0x0396	||
				id ==	0x0397	||
				id ==	0x0398	||
				id ==	0x0399	||
				id ==	0x039A	||
				id ==	0x039B	||
				id ==	0x039C	||
				id ==	0x03A5	||
				id ==	0x03A6	||
				id ==	0x03A7	||
				id ==	0x03A8	||
				id ==	0x03F6	||
				id ==	0x03F7	||
				id ==	0x03F9	||
				id ==	0x03FA	||
				id ==	0x03FB	||
				id ==	0x03FC	||
				id ==	0x03FD	||
				id ==	0x03FE	||
				id ==	0x03FF	||
				id ==	0x0400	||
				id ==	0x0401	||
				id ==	0x0402	||
				id ==	0x0403	||
				id ==	0x0404	||
				id ==	0x0405	||
				id ==	0x0547	||
				id ==	0x0548	||
				id ==	0x0549	||
				id ==	0x054A	||
				id ==	0x054B	||
				id ==	0x054C	||
				id ==	0x054D	||
				id ==	0x054E	||
				id ==	0x0553	||
				id ==	0x0554	||
				id ==	0x0555	||
				id ==	0x0556	||
				id ==	0x0597	||
				id ==	0x0598	||
				id ==	0x0599	||
				id ==	0x059A	||
				id ==	0x059B	||
				id ==	0x059C	||
				id ==	0x059D	||
				id ==	0x059E	||
				id ==	0x0623	||
				id ==	0x0624	||
				id ==	0x0625	||
				id ==	0x0626	||
				id ==	0x0627	||
				id ==	0x0628	||
				id ==	0x0629	||
				id ==	0x062A	||
				id ==	0x062B	||
				id ==	0x062C	||
				id ==	0x062D	||
				id ==	0x062E	||
				id ==	0x062F	||
				id ==	0x0630	||
				id ==	0x0631	||
				id ==	0x0632	||
				id ==	0x0633	||
				id ==	0x0634	||
				id ==	0x0635	||
				id ==	0x0636	||
				id ==	0x0637	||
				id ==	0x0638	||
				id ==	0x0639	||
				id ==	0x063A	||
				id ==	0x06F3	||
				id ==	0x06F5	||
				id ==	0x06F6	||
				id ==	0x06F7	||
				id ==	0x06F8	||
				id ==	0x06F9	||
				id ==	0x06FA
			)){ return true; }

			if ( ( category == "forest" || category == "any" ) && ( 
				id ==	0x00C4	||
				id ==	0x00C5	||
				id ==	0x00C6	||
				id ==	0x00C7	||
				id ==	0x00C8	||
				id ==	0x00C9	||
				id ==	0x00CA	||
				id ==	0x00CB	||
				id ==	0x00CC	||
				id ==	0x00CD	||
				id ==	0x00CE	||
				id ==	0x00CF	||
				id ==	0x00D0	||
				id ==	0x00D1	||
				id ==	0x00D2	||
				id ==	0x00D3	||
				id ==	0x00D4	||
				id ==	0x00D5	||
				id ==	0x00D6	||
				id ==	0x00D7	||
				id ==	0x00F0	||
				id ==	0x00F1	||
				id ==	0x00F2	||
				id ==	0x00F3	||
				id ==	0x00F8	||
				id ==	0x00F9	||
				id ==	0x00FA	||
				id ==	0x00FB	||
				id ==	0x015D	||
				id ==	0x015E	||
				id ==	0x015F	||
				id ==	0x0160	||
				id ==	0x0161	||
				id ==	0x0162	||
				id ==	0x0163	||
				id ==	0x0164	||
				id ==	0x0165	||
				id ==	0x0166	||
				id ==	0x0167	||
				id ==	0x0168	||
				id ==	0x0324	||
				id ==	0x0325	||
				id ==	0x0326	||
				id ==	0x0327	||
				id ==	0x0328	||
				id ==	0x0329	||
				id ==	0x032A	||
				id ==	0x032B	||
				id ==	0x054F	||
				id ==	0x0550	||
				id ==	0x0551	||
				id ==	0x0552	||
				id ==	0x05F1	||
				id ==	0x05F2	||
				id ==	0x05F3	||
				id ==	0x05F4	||
				id ==	0x05F9	||
				id ==	0x05FA	||
				id ==	0x05FB	||
				id ==	0x05FC	||
				id ==	0x05FD	||
				id ==	0x05FE	||
				id ==	0x05FF	||
				id ==	0x0600	||
				id ==	0x0601	||
				id ==	0x0602	||
				id ==	0x0603	||
				id ==	0x0604	||
				id ==	0x0611	||
				id ==	0x0612	||
				id ==	0x0613	||
				id ==	0x0614	||
				id ==	0x0653	||
				id ==	0x0654	||
				id ==	0x0655	||
				id ==	0x0656	||
				id ==	0x065B	||
				id ==	0x065C	||
				id ==	0x065D	||
				id ==	0x065E	||
				id ==	0x065F	||
				id ==	0x0660	||
				id ==	0x0661	||
				id ==	0x0662	||
				id ==	0x066B	||
				id ==	0x066C	||
				id ==	0x066D	||
				id ==	0x066E	||
				id ==	0x06AF	||
				id ==	0x06B0	||
				id ==	0x06B1	||
				id ==	0x06B2	||
				id ==	0x06B3	||
				id ==	0x06B4	||
				id ==	0x06BB	||
				id ==	0x06BC	||
				id ==	0x06BD	||
				id ==	0x06BE	||
				id ==	0x0709	||
				id ==	0x070A	||
				id ==	0x070B	||
				id ==	0x070C	||
				id ==	0x0715	||
				id ==	0x0716	||
				id ==	0x0717	||
				id ==	0x0718	||
				id ==	0x0719	||
				id ==	0x071A	||
				id ==	0x071B	||
				id ==	0x071C
			)){ return true; }

			if ( ( category == "grass" || category == "any" ) && ( 
				id ==	0x0003	||
				id ==	0x0004	||
				id ==	0x0005	||
				id ==	0x0006	||
				id ==	0x003B	||
				id ==	0x003C	||
				id ==	0x003D	||
				id ==	0x003E	||
				id ==	0x007D	||
				id ==	0x007E	||
				id ==	0x007F	||
				id ==	0x00C0	||
				id ==	0x00C1	||
				id ==	0x00C2	||
				id ==	0x00C3	||
				id ==	0x00D8	||
				id ==	0x00D9	||
				id ==	0x00DA	||
				id ==	0x00DB	||
				id ==	0x01A4	||
				id ==	0x01A5	||
				id ==	0x01A6	||
				id ==	0x01A7	||
				id ==	0x0242	||
				id ==	0x0243	||
				id ==	0x036F	||
				id ==	0x0370	||
				id ==	0x0371	||
				id ==	0x0372	||
				id ==	0x0373	||
				id ==	0x0374	||
				id ==	0x0375	||
				id ==	0x0376	||
				id ==	0x037B	||
				id ==	0x037C	||
				id ==	0x037D	||
				id ==	0x037E	||
				id ==	0x03BF	||
				id ==	0x03C0	||
				id ==	0x03C1	||
				id ==	0x03C2	||
				id ==	0x03C3	||
				id ==	0x03C4	||
				id ==	0x03C5	||
				id ==	0x03C6	||
				id ==	0x03CB	||
				id ==	0x03CC	||
				id ==	0x03CD	||
				id ==	0x03CE	||
				id ==	0x0579	||
				id ==	0x057A	||
				id ==	0x057B	||
				id ==	0x057C	||
				id ==	0x057D	||
				id ==	0x057E	||
				id ==	0x057F	||
				id ==	0x0580	||
				id ==	0x058B	||
				id ==	0x058C	||
				id ==	0x05D7	||
				id ==	0x05D8	||
				id ==	0x05D9	||
				id ==	0x05DA	||
				id ==	0x05DB	||
				id ==	0x05DC	||
				id ==	0x05DD	||
				id ==	0x05DE	||
				id ==	0x05E3	||
				id ==	0x05E4	||
				id ==	0x05E5	||
				id ==	0x05E6	||
				id ==	0x067D	||
				id ==	0x067E	||
				id ==	0x067F	||
				id ==	0x0680	||
				id ==	0x0681	||
				id ==	0x0682	||
				id ==	0x0683	||
				id ==	0x0684	||
				id ==	0x0689	||
				id ==	0x068A	||
				id ==	0x068B	||
				id ==	0x068C	||
				id ==	0x0695	||
				id ==	0x0696	||
				id ==	0x0697	||
				id ==	0x0698	||
				id ==	0x0699	||
				id ==	0x069A	||
				id ==	0x069B	||
				id ==	0x069C	||
				id ==	0x06A1	||
				id ==	0x06A2	||
				id ==	0x06A3	||
				id ==	0x06A4	||
				id ==	0x06B5	||
				id ==	0x06B6	||
				id ==	0x06B7	||
				id ==	0x06B8	||
				id ==	0x06B9	||
				id ==	0x06BA	||
				id ==	0x06BF	||
				id ==	0x06C0	||
				id ==	0x06C1	||
				id ==	0x06C2	||
				id ==	0x06DE	||
				id ==	0x06DF	||
				id ==	0x06E0	||
				id ==	0x06E1
			)){ return true; }

			if ( ( category == "jungle" || category == "any" ) && ( 
				id ==	0x00AC	||
				id ==	0x00AD	||
				id ==	0x00AE	||
				id ==	0x00AF	||
				id ==	0x00B0	||
				id ==	0x00B3	||
				id ==	0x00B6	||
				id ==	0x00B9	||
				id ==	0x00BC	||
				id ==	0x00BD	||
				id ==	0x00BE	||
				id ==	0x00BF	||
				id ==	0x0100	||
				id ==	0x0101	||
				id ==	0x0102	||
				id ==	0x0103	||
				id ==	0x0108	||
				id ==	0x0109	||
				id ==	0x010A	||
				id ==	0x010B	||
				id ==	0x01F0	||
				id ==	0x01F1	||
				id ==	0x01F2	||
				id ==	0x01F3	||
				id ==	0x026E	||
				id ==	0x026F	||
				id ==	0x0270	||
				id ==	0x0271	||
				id ==	0x0276	||
				id ==	0x0277	||
				id ==	0x0278	||
				id ==	0x0279	||
				id ==	0x027A	||
				id ==	0x027B	||
				id ==	0x027C	||
				id ==	0x027D	||
				id ==	0x0286	||
				id ==	0x0287	||
				id ==	0x0288	||
				id ==	0x0289	||
				id ==	0x0292	||
				id ==	0x0293	||
				id ==	0x0294	||
				id ==	0x0295	||
				id ==	0x0581	||
				id ==	0x0582	||
				id ==	0x0583	||
				id ==	0x0584	||
				id ==	0x0585	||
				id ==	0x0586	||
				id ==	0x0587	||
				id ==	0x0588	||
				id ==	0x0589	||
				id ==	0x058A	||
				id ==	0x058D	||
				id ==	0x058E	||
				id ==	0x058F	||
				id ==	0x0590	||
				id ==	0x059F	||
				id ==	0x05A0	||
				id ==	0x05A1	||
				id ==	0x05A2	||
				id ==	0x05A3	||
				id ==	0x05A4	||
				id ==	0x05A5	||
				id ==	0x05A6	||
				id ==	0x05B3	||
				id ==	0x05B4	||
				id ==	0x05B5	||
				id ==	0x05B6	||
				id ==	0x05B7	||
				id ==	0x05B8	||
				id ==	0x05B9	||
				id ==	0x05BA	||
				id ==	0x05F5	||
				id ==	0x05F6	||
				id ==	0x05F7	||
				id ==	0x05F8	||
				id ==	0x0605	||
				id ==	0x0606	||
				id ==	0x0607	||
				id ==	0x0608	||
				id ==	0x0609	||
				id ==	0x060A	||
				id ==	0x060B	||
				id ==	0x060C	||
				id ==	0x060D	||
				id ==	0x060E	||
				id ==	0x060F	||
				id ==	0x0610	||
				id ==	0x0615	||
				id ==	0x0616	||
				id ==	0x0617	||
				id ==	0x0618	||
				id ==	0x0727	||
				id ==	0x0728	||
				id ==	0x0729	||
				id ==	0x0733	||
				id ==	0x0734	||
				id ==	0x0735	||
				id ==	0x0736	||
				id ==	0x0737	||
				id ==	0x0738	||
				id ==	0x0739	||
				id ==	0x073A
			)){ return true; }

			if ( ( category == "sand" || category == "any" ) && ( 
				id ==	0x0016	||
				id ==	0x0017	||
				id ==	0x0018	||
				id ==	0x0019	||
				id ==	0x0033	||
				id ==	0x0034	||
				id ==	0x0035	||
				id ==	0x0036	||
				id ==	0x0037	||
				id ==	0x0038	||
				id ==	0x0039	||
				id ==	0x003A	||
				id ==	0x011E	||
				id ==	0x011F	||
				id ==	0x0120	||
				id ==	0x0121	||
				id ==	0x012A	||
				id ==	0x012B	||
				id ==	0x012C	||
				id ==	0x012D	||
				id ==	0x01A8	||
				id ==	0x01A9	||
				id ==	0x01AA	||
				id ==	0x01AB	||
				id ==	0x0282	||
				id ==	0x0283	||
				id ==	0x0284	||
				id ==	0x0285	||
				id ==	0x028A	||
				id ==	0x028B	||
				id ==	0x028C	||
				id ==	0x028D	||
				id ==	0x028E	||
				id ==	0x028F	||
				id ==	0x0290	||
				id ==	0x0291	||
				id ==	0x0335	||
				id ==	0x0336	||
				id ==	0x0337	||
				id ==	0x0338	||
				id ==	0x0339	||
				id ==	0x033A	||
				id ==	0x033B	||
				id ==	0x033C	||
				id ==	0x0341	||
				id ==	0x0342	||
				id ==	0x0343	||
				id ==	0x0344	||
				id ==	0x034D	||
				id ==	0x034E	||
				id ==	0x034F	||
				id ==	0x0350	||
				id ==	0x0351	||
				id ==	0x0352	||
				id ==	0x0353	||
				id ==	0x0354	||
				id ==	0x0359	||
				id ==	0x035A	||
				id ==	0x035B	||
				id ==	0x035C	||
				id ==	0x03B7	||
				id ==	0x03B8	||
				id ==	0x03B9	||
				id ==	0x03BA	||
				id ==	0x03BB	||
				id ==	0x03BC	||
				id ==	0x03BD	||
				id ==	0x03BE	||
				id ==	0x03C7	||
				id ==	0x03C8	||
				id ==	0x03C9	||
				id ==	0x03CA	||
				id ==	0x05A7	||
				id ==	0x05A8	||
				id ==	0x05A9	||
				id ==	0x05AA	||
				id ==	0x05AB	||
				id ==	0x05AC	||
				id ==	0x05AD	||
				id ==	0x05AE	||
				id ==	0x05AF	||
				id ==	0x05B0	||
				id ==	0x05B1	||
				id ==	0x05B2	||
				id ==	0x064B	||
				id ==	0x064C	||
				id ==	0x064D	||
				id ==	0x064E	||
				id ==	0x064F	||
				id ==	0x0650	||
				id ==	0x0651	||
				id ==	0x0652	||
				id ==	0x0657	||
				id ==	0x0658	||
				id ==	0x0659	||
				id ==	0x065A	||
				id ==	0x0663	||
				id ==	0x0664	||
				id ==	0x0665	||
				id ==	0x0666	||
				id ==	0x0667	||
				id ==	0x0668	||
				id ==	0x0669	||
				id ==	0x066A	||
				id ==	0x066F	||
				id ==	0x0670	||
				id ==	0x0671	||
				id ==	0x0672
			)){ return true; }

			if ( ( category == "snow" || category == "any" ) && ( 
				id ==	0x011A	||
				id ==	0x011B	||
				id ==	0x011C	||
				id ==	0x011D	||
				id ==	0x012E	||
				id ==	0x012F	||
				id ==	0x0130	||
				id ==	0x0131	||
				id ==	0x0179	||
				id ==	0x017A	||
				id ==	0x017B	||
				id ==	0x0385	||
				id ==	0x0386	||
				id ==	0x0387	||
				id ==	0x0388	||
				id ==	0x0389	||
				id ==	0x038A	||
				id ==	0x038B	||
				id ==	0x038C	||
				id ==	0x0391	||
				id ==	0x0392	||
				id ==	0x0393	||
				id ==	0x0394	||
				id ==	0x039D	||
				id ==	0x039E	||
				id ==	0x039F	||
				id ==	0x03A0	||
				id ==	0x03A1	||
				id ==	0x03A2	||
				id ==	0x03A3	||
				id ==	0x03A4	||
				id ==	0x03A9	||
				id ==	0x03AA	||
				id ==	0x03AB	||
				id ==	0x03AC	||
				id ==	0x05BF	||
				id ==	0x05C0	||
				id ==	0x05C1	||
				id ==	0x05C2	||
				id ==	0x05C3	||
				id ==	0x05C4	||
				id ==	0x05C5	||
				id ==	0x05C6	||
				id ==	0x05C7	||
				id ==	0x05C8	||
				id ==	0x05C9	||
				id ==	0x05CA	||
				id ==	0x05CB	||
				id ==	0x05CC	||
				id ==	0x05CD	||
				id ==	0x05CE	||
				id ==	0x05CF	||
				id ==	0x05D0	||
				id ==	0x05D1	||
				id ==	0x05D2	||
				id ==	0x05D3	||
				id ==	0x05D4	||
				id ==	0x05D5	||
				id ==	0x05D6	||
				id ==	0x05DF	||
				id ==	0x05E0	||
				id ==	0x05E1	||
				id ==	0x05E2	||
				id ==	0x0745	||
				id ==	0x0746	||
				id ==	0x0747	||
				id ==	0x0748	||
				id ==	0x0751	||
				id ==	0x0752	||
				id ==	0x0753	||
				id ==	0x0754	||
				id ==	0x075D	||
				id ==	0x075E	||
				id ==	0x075F	||
				id ==	0x0760
			)){ return true; }

			if ( ( category == "stone" || category == "any" ) && ( 
				id ==	0x0436	||
				id ==	0x0437	||
				id ==	0x0438	||
				id ==	0x0439	||
				id ==	0x043A	||
				id ==	0x043B	||
				id ==	0x043C	||
				id ==	0x043D	||
				id ==	0x043E	||
				id ==	0x043F	||
				id ==	0x0440	||
				id ==	0x0441	||
				id ==	0x0442	||
				id ==	0x0443	||
				id ==	0x0444	||
				id ==	0x0445
			)){ return true; }

			if ( ( category == "swamp" || category == "any" ) && ( 
				id ==	0x3D65	||
				id ==	0x3D66	||
				id ==	0x3D67	||
				id ==	0x3D68	||
				id ==	0x3D69	||
				id ==	0x3D6A	||
				id ==	0x3D6B	||
				id ==	0x3D6C	||
				id ==	0x3D6D	||
				id ==	0x3D6E	||
				id ==	0x3D6F	||
				id ==	0x3D70	||
				id ==	0x3D71	||
				id ==	0x3D72	||
				id ==	0x3D73	||
				id ==	0x3D74	||
				id ==	0x3D75	||
				id ==	0x3D76	||
				id ==	0x3D77	||
				id ==	0x3D78	||
				id ==	0x3D79	||
				id ==	0x3D7A	||
				id ==	0x3D7B	||
				id ==	0x3D7C	||
				id ==	0x3D7D	||
				id ==	0x3D7E	||
				id ==	0x3D7F	||
				id ==	0x3D80	||
				id ==	0x3D81	||
				id ==	0x3D82	||
				id ==	0x3D83	||
				id ==	0x3D84	||
				id ==	0x3D85	||
				id ==	0x3D86	||
				id ==	0x3D87	||
				id ==	0x3D88	||
				id ==	0x3D89	||
				id ==	0x3D8A	||
				id ==	0x3D8B	||
				id ==	0x3D8C	||
				id ==	0x3D8D	||
				id ==	0x3D8E	||
				id ==	0x3D8F	||
				id ==	0x3D90	||
				id ==	0x3D91	||
				id ==	0x3D92	||
				id ==	0x3D93	||
				id ==	0x3D94	||
				id ==	0x3D95	||
				id ==	0x3D96	||
				id ==	0x3D97	||
				id ==	0x3D98	||
				id ==	0x3D99	||
				id ==	0x3D9A	||
				id ==	0x3D9B	||
				id ==	0x3D9C	||
				id ==	0x3D9D	||
				id ==	0x3D9E	||
				id ==	0x3D9F	||
				id ==	0x3DA0	||
				id ==	0x3DA1	||
				id ==	0x3DA2	||
				id ==	0x3DA3	||
				id ==	0x3DA4	||
				id ==	0x3DA5	||
				id ==	0x3DA6	||
				id ==	0x3DA7	||
				id ==	0x3DA8	||
				id ==	0x3DA9	||
				id ==	0x3DAA	||
				id ==	0x3DAB	||
				id ==	0x3DAC	||
				id ==	0x3DAD	||
				id ==	0x3DAE	||
				id ==	0x3DAF	||
				id ==	0x3DB0	||
				id ==	0x3DB1	||
				id ==	0x3DB2	||
				id ==	0x3DB3	||
				id ==	0x3DB4	||
				id ==	0x3DB5	||
				id ==	0x3DB6	||
				id ==	0x3DB7	||
				id ==	0x3DB8	||
				id ==	0x3DB9	||
				id ==	0x3DBA	||
				id ==	0x3DBB	||
				id ==	0x3DBC	||
				id ==	0x3DBD	||
				id ==	0x3DBE	||
				id ==	0x3DBF	||
				id ==	0x3DC0	||
				id ==	0x3DC1	||
				id ==	0x3DC2	||
				id ==	0x3DC3	||
				id ==	0x3DC4	||
				id ==	0x3DC5	||
				id ==	0x3DC6	||
				id ==	0x3DC7	||
				id ==	0x3DC8	||
				id ==	0x3DC9	||
				id ==	0x3DCA	||
				id ==	0x3DCB	||
				id ==	0x3DCC	||
				id ==	0x3DCD	||
				id ==	0x3DCE	||
				id ==	0x3DCF	||
				id ==	0x3DD0	||
				id ==	0x3DD1	||
				id ==	0x3DD2	||
				id ==	0x3DD3	||
				id ==	0x3DD4	||
				id ==	0x3DD5	||
				id ==	0x3DD6	||
				id ==	0x3DD7	||
				id ==	0x3DD8	||
				id ==	0x3DD9	||
				id ==	0x3DDA	||
				id ==	0x3DDB	||
				id ==	0x3DDC	||
				id ==	0x3DDD	||
				id ==	0x3DDE	||
				id ==	0x3DDF	||
				id ==	0x3DE0	||
				id ==	0x3DE1	||
				id ==	0x3DE2	||
				id ==	0x3DE3	||
				id ==	0x3DE4	||
				id ==	0x3DE5	||
				id ==	0x3DE6	||
				id ==	0x3DE7	||
				id ==	0x3DE8	||
				id ==	0x3DE9	||
				id ==	0x3DEA	||
				id ==	0x3DEB	||
				id ==	0x3DEC	||
				id ==	0x3DED	||
				id ==	0x3DEE	||
				id ==	0x3DEF	||
				id ==	0x3DF0	||
				id ==	0x3DF1
			)){ return true; }

			return false;
		}

		public static bool BlockedTile ( int id, string category )
		{
			if ( ( category == "water" || category == "any" ) && ( 
				id ==	0x00A8	||
				id ==	0x00A9	||
				id ==	0x00AA	||
				id ==	0x00AB	||
				id ==	0x0136	||
				id ==	0x0138
			)){ return true; }

			if ( ( category == "cave" || category == "any" ) && ( 
				id ==	0x024A	||
				id ==	0x024B	||
				id ==	0x024C	||
				id ==	0x024D	||
				id ==	0x024E	||
				id ==	0x024F	||
				id ==	0x0250	||
				id ==	0x0251	||
				id ==	0x0252	||
				id ==	0x0253	||
				id ==	0x0254	||
				id ==	0x0255	||
				id ==	0x0256	||
				id ==	0x0257	||
				id ==	0x0258	||
				id ==	0x0259	||
				id ==	0x025A	||
				id ==	0x025B	||
				id ==	0x025C	||
				id ==	0x025D	||
				id ==	0x025E	||
				id ==	0x025F	||
				id ==	0x0260	||
				id ==	0x0261	||
				id ==	0x0262	||
				id ==	0x0263	||
				id ==	0x0264	||
				id ==	0x0265	||
				id ==	0x0266	||
				id ==	0x0267	||
				id ==	0x0268	||
				id ==	0x0269	||
				id ==	0x026A	||
				id ==	0x026B	||
				id ==	0x026C	||
				id ==	0x026D	||
				id ==	0x02BC	||
				id ==	0x02BD	||
				id ==	0x02BE	||
				id ==	0x02BF	||
				id ==	0x02C0	||
				id ==	0x02C1	||
				id ==	0x02C2	||
				id ==	0x02C3	||
				id ==	0x02C4	||
				id ==	0x02C5	||
				id ==	0x02C6	||
				id ==	0x02C7	||
				id ==	0x02C8	||
				id ==	0x02C9	||
				id ==	0x02CA	||
				id ==	0x02CB
			)){ return true; }

			if ( ( category == "dirt" || category == "any" ) && ( 
				id ==	0x008D	||
				id ==	0x008E	||
				id ==	0x008F	||
				id ==	0x0090	||
				id ==	0x0091	||
				id ==	0x0092	||
				id ==	0x0093	||
				id ==	0x0094	||
				id ==	0x0095	||
				id ==	0x0096	||
				id ==	0x0097	||
				id ==	0x0098	||
				id ==	0x0099	||
				id ==	0x009A	||
				id ==	0x009B	||
				id ==	0x009C	||
				id ==	0x009D	||
				id ==	0x009E	||
				id ==	0x009F	||
				id ==	0x00A0	||
				id ==	0x00A1	||
				id ==	0x00A2	||
				id ==	0x00A3	||
				id ==	0x00A4	||
				id ==	0x00A5	||
				id ==	0x00A6	||
				id ==	0x00A7	||
				id ==	0x00DC	||
				id ==	0x00DD	||
				id ==	0x00DE	||
				id ==	0x00DF	||
				id ==	0x00E0	||
				id ==	0x00E1	||
				id ==	0x00E2	||
				id ==	0x00E3	||
				id ==	0x02D0	||
				id ==	0x02D1	||
				id ==	0x02D2	||
				id ==	0x02D3	||
				id ==	0x02D4	||
				id ==	0x02D5	||
				id ==	0x02D6	||
				id ==	0x02D7	||
				id ==	0x02E5	||
				id ==	0x02E6	||
				id ==	0x02E7	||
				id ==	0x02E8	||
				id ==	0x02E9	||
				id ==	0x02EA	||
				id ==	0x02EB	||
				id ==	0x02EC	||
				id ==	0x02ED	||
				id ==	0x02EE	||
				id ==	0x02EF	||
				id ==	0x02F0	||
				id ==	0x02F1	||
				id ==	0x02F2	||
				id ==	0x02F3	||
				id ==	0x02F4	||
				id ==	0x02F5	||
				id ==	0x02F6	||
				id ==	0x02F7	||
				id ==	0x02F8	||
				id ==	0x02F9	||
				id ==	0x02FA	||
				id ==	0x02FB	||
				id ==	0x02FC	||
				id ==	0x02FD	||
				id ==	0x02FE	||
				id ==	0x02FF	||
				id ==	0x0303	||
				id ==	0x0304	||
				id ==	0x0305	||
				id ==	0x0306	||
				id ==	0x0307	||
				id ==	0x0308	||
				id ==	0x0309	||
				id ==	0x030A	||
				id ==	0x030B	||
				id ==	0x030C	||
				id ==	0x030D	||
				id ==	0x030E	||
				id ==	0x030F	||
				id ==	0x0310	||
				id ==	0x0311	||
				id ==	0x0312	||
				id ==	0x0313	||
				id ==	0x0314	||
				id ==	0x0315	||
				id ==	0x0316	||
				id ==	0x0317	||
				id ==	0x0318	||
				id ==	0x0319	||
				id ==	0x031A	||
				id ==	0x031B	||
				id ==	0x031C	||
				id ==	0x031D	||
				id ==	0x031E	||
				id ==	0x031F	||
				id ==	0x06F4	||
				id ==	0x0777	||
				id ==	0x0778	||
				id ==	0x0779	||
				id ==	0x077A	||
				id ==	0x077B	||
				id ==	0x077C	||
				id ==	0x077D	||
				id ==	0x077E	||
				id ==	0x077F	||
				id ==	0x0780	||
				id ==	0x0781	||
				id ==	0x0782	||
				id ==	0x0783	||
				id ==	0x0784	||
				id ==	0x0785	||
				id ==	0x0786	||
				id ==	0x0787	||
				id ==	0x0788	||
				id ==	0x0789	||
				id ==	0x078A	||
				id ==	0x078B	||
				id ==	0x078C	||
				id ==	0x078D	||
				id ==	0x078E	||
				id ==	0x078F	||
				id ==	0x0790	||
				id ==	0x0791
			)){ return true; }

			if ( ( category == "forest" || category == "any" ) && ( 
				id ==	0x00ED	||
				id ==	0x00EE	||
				id ==	0x00EF	||
				id ==	0x3AF0	||
				id ==	0x3AF1	||
				id ==	0x3AF2	||
				id ==	0x3AF3	||
				id ==	0x3AF4	||
				id ==	0x3AF5	||
				id ==	0x3AF6	||
				id ==	0x3AF7	||
				id ==	0x3AF8
			)){ return true; }

			if ( ( category == "grass" || category == "any" ) && ( 
				id ==	0x0231	||
				id ==	0x0232	||
				id ==	0x0233	||
				id ==	0x0234	||
				id ==	0x0239	||
				id ==	0x023A	||
				id ==	0x023B	||
				id ==	0x023C	||
				id ==	0x023D	||
				id ==	0x023E	||
				id ==	0x023F	||
				id ==	0x0240	||
				id ==	0x0241	||
				id ==	0x06D2	||
				id ==	0x06D3	||
				id ==	0x06D4	||
				id ==	0x06D5	||
				id ==	0x06D6	||
				id ==	0x06D7	||
				id ==	0x06D8	||
				id ==	0x06D9
			)){ return true; }

			if ( ( category == "jungle" || category == "any" ) && ( 
				id ==	0x00EC	||
				id ==	0x00FC	||
				id ==	0x00FD	||
				id ==	0x00FE	||
				id ==	0x00FF	||
				id ==	0x072A
			)){ return true; }

			if ( ( category == "rock" || category == "any" ) && ( 
				id ==	0x00E4	||
				id ==	0x00E5	||
				id ==	0x00E6	||
				id ==	0x00E7	||
				id ==	0x00F4	||
				id ==	0x00F5	||
				id ==	0x00F6	||
				id ==	0x00F7	||
				id ==	0x0104	||
				id ==	0x0105	||
				id ==	0x0106	||
				id ==	0x0107	||
				id ==	0x0110	||
				id ==	0x0111	||
				id ==	0x0112	||
				id ==	0x0113	||
				id ==	0x0122	||
				id ==	0x0123	||
				id ==	0x0124	||
				id ==	0x0125	||
				id ==	0x01D3	||
				id ==	0x01D4	||
				id ==	0x01D5	||
				id ==	0x01D6	||
				id ==	0x01D7	||
				id ==	0x01D8	||
				id ==	0x01D9	||
				id ==	0x01DA	||
				id ==	0x021F	||
				id ==	0x0220	||
				id ==	0x0221	||
				id ==	0x0222	||
				id ==	0x0223	||
				id ==	0x0224	||
				id ==	0x0225	||
				id ==	0x0226	||
				id ==	0x0227	||
				id ==	0x0228	||
				id ==	0x0229	||
				id ==	0x022A	||
				id ==	0x022B	||
				id ==	0x022C	||
				id ==	0x022D	||
				id ==	0x022E	||
				id ==	0x022F	||
				id ==	0x0230	||
				id ==	0x0235	||
				id ==	0x0236	||
				id ==	0x0237	||
				id ==	0x0238	||
				id ==	0x06CD	||
				id ==	0x06CE	||
				id ==	0x06CF	||
				id ==	0x06D0	||
				id ==	0x06D1	||
				id ==	0x06DA	||
				id ==	0x06DB	||
				id ==	0x06DC	||
				id ==	0x06DD	||
				id ==	0x06EB	||
				id ==	0x06EC	||
				id ==	0x06ED	||
				id ==	0x06EE	||
				id ==	0x06EF	||
				id ==	0x06F0	||
				id ==	0x06F1	||
				id ==	0x06F2	||
				id ==	0x06FB	||
				id ==	0x06FC	||
				id ==	0x06FD	||
				id ==	0x06FE	||
				id ==	0x070E	||
				id ==	0x070F	||
				id ==	0x0710	||
				id ==	0x0711	||
				id ==	0x0712	||
				id ==	0x0713	||
				id ==	0x0714	||
				id ==	0x071D	||
				id ==	0x071E	||
				id ==	0x071F	||
				id ==	0x0720	||
				id ==	0x072B	||
				id ==	0x072C	||
				id ==	0x072D	||
				id ==	0x072E	||
				id ==	0x072F	||
				id ==	0x0730	||
				id ==	0x0731	||
				id ==	0x0732	||
				id ==	0x073B	||
				id ==	0x073C	||
				id ==	0x073D	||
				id ==	0x073E	||
				id ==	0x0749	||
				id ==	0x074A	||
				id ==	0x074B	||
				id ==	0x074C	||
				id ==	0x074D	||
				id ==	0x074E	||
				id ==	0x074F	||
				id ==	0x0750	||
				id ==	0x0759	||
				id ==	0x075A	||
				id ==	0x075B	||
				id ==	0x075C	||
				id ==	0x09EC	||
				id ==	0x09ED	||
				id ==	0x09EE	||
				id ==	0x09EF	||
				id ==	0x09F0	||
				id ==	0x09F1	||
				id ==	0x09F2	||
				id ==	0x09F3	||
				id ==	0x09F4	||
				id ==	0x09F5	||
				id ==	0x09F6	||
				id ==	0x09F7	||
				id ==	0x09F8	||
				id ==	0x09F9	||
				id ==	0x09FA	||
				id ==	0x09FB	||
				id ==	0x09FC	||
				id ==	0x09FD	||
				id ==	0x09FE	||
				id ==	0x09FF	||
				id ==	0x0A00	||
				id ==	0x0A01	||
				id ==	0x0A02	||
				id ==	0x0A03	||
				id ==	0x3F39	||
				id ==	0x3F3A	||
				id ==	0x3F3B	||
				id ==	0x3F3C	||
				id ==	0x3F3D	||
				id ==	0x3F3E	||
				id ==	0x3F3F	||
				id ==	0x3F40	||
				id ==	0x3F41	||
				id ==	0x3F42	||
				id ==	0x3F43	||
				id ==	0x3F44	||
				id ==	0x3F45	||
				id ==	0x3F46	||
				id ==	0x3F47	||
				id ==	0x3F48	||
				id ==	0x3F49	||
				id ==	0x3F4A	||
				id ==	0x3F4B	||
				id ==	0x3F4C	||
				id ==	0x3F4D	||
				id ==	0x3F4E	||
				id ==	0x3F4F	||
				id ==	0x3F50	||
				id ==	0x3F51	||
				id ==	0x3F52	||
				id ==	0x3F53	||
				id ==	0x3F54	||
				id ==	0x3F55	||
				id ==	0x3F56	||
				id ==	0x3F57	||
				id ==	0x3F58	||
				id ==	0x3F59	||
				id ==	0x3F5A	||
				id ==	0x3F5B	||
				id ==	0x3F5C	||
				id ==	0x3F5D	||
				id ==	0x3F5E	||
				id ==	0x3F5F	||
				id ==	0x3F60	||
				id ==	0x3F61	||
				id ==	0x3F62	||
				id ==	0x3F63	||
				id ==	0x3F64	||
				id ==	0x3F65	||
				id ==	0x3F66	||
				id ==	0x3F67	||
				id ==	0x3F68	||
				id ==	0x3F82	||
				id ==	0x3F83	||
				id ==	0x3F84	||
				id ==	0x3F85	||
				id ==	0x3F86	||
				id ==	0x3F87	||
				id ==	0x3F88	||
				id ==	0x3F89	||
				id ==	0x3F8A	||
				id ==	0x3F8B	||
				id ==	0x3F8C	||
				id ==	0x3F8D	||
				id ==	0x3F8E	||
				id ==	0x3F8F	||
				id ==	0x3F92	||
				id ==	0x3F93	||
				id ==	0x3F94	||
				id ==	0x3F95	||
				id ==	0x3F96	||
				id ==	0x3F97	||
				id ==	0x3F98	||
				id ==	0x3F99	||
				id ==	0x3F9A	||
				id ==	0x3F9B	||
				id ==	0x3F9C	||
				id ==	0x3F9D	||
				id ==	0x3F9E	||
				id ==	0x3F9F	||
				id ==	0x3FA0	||
				id ==	0x3FA1	||
				id ==	0x3FA2	||
				id ==	0x3FA3	||
				id ==	0x3FA4	||
				id ==	0x3FA5	||
				id ==	0x3FA6	||
				id ==	0x3FA7	||
				id ==	0x3FA8	||
				id ==	0x3FA9	||
				id ==	0x3FAA	||
				id ==	0x3FAB	||
				id ==	0x3FAC	||
				id ==	0x3FAD	||
				id ==	0x3FAE	||
				id ==	0x3FAF	||
				id ==	0x3FB0	||
				id ==	0x3FB1	||
				id ==	0x3FB2	||
				id ==	0x3FB3	||
				id ==	0x3FB4	||
				id ==	0x3FB5	||
				id ==	0x3FB6	||
				id ==	0x3FB7	||
				id ==	0x3FB8	||
				id ==	0x3FB9	||
				id ==	0x3FBA	||
				id ==	0x3FBB	||
				id ==	0x3FBC	||
				id ==	0x3FBD	||
				id ==	0x3FBE	||
				id ==	0x3FBF	||
				id ==	0x3FC0	||
				id ==	0x3FC1	||
				id ==	0x3FC2	||
				id ==	0x3FC3	||
				id ==	0x3FC4	||
				id ==	0x3FC5	||
				id ==	0x3FC6	||
				id ==	0x3FC7	||
				id ==	0x3FC8	||
				id ==	0x3FC9	||
				id ==	0x3FCA	||
				id ==	0x3FCB	||
				id ==	0x3FCC	||
				id ==	0x3FCD	||
				id ==	0x3FCE	||
				id ==	0x3FCF
			)){ return true; }

			if ( ( category == "sand" || category == "any" ) && ( 
				id ==	0x001A	||
				id ==	0x001B	||
				id ==	0x001C	||
				id ==	0x001D	||
				id ==	0x001E	||
				id ==	0x001F	||
				id ==	0x0020	||
				id ==	0x0021	||
				id ==	0x0022	||
				id ==	0x0023	||
				id ==	0x0024	||
				id ==	0x0025	||
				id ==	0x0026	||
				id ==	0x0027	||
				id ==	0x0028	||
				id ==	0x0029	||
				id ==	0x002A	||
				id ==	0x002B	||
				id ==	0x002C	||
				id ==	0x002D	||
				id ==	0x002E	||
				id ==	0x002F	||
				id ==	0x0030	||
				id ==	0x0031	||
				id ==	0x0032	||
				id ==	0x0044	||
				id ==	0x0045	||
				id ==	0x0046	||
				id ==	0x0047	||
				id ==	0x0048	||
				id ==	0x0049	||
				id ==	0x004A	||
				id ==	0x004B	||
				id ==	0x0126	||
				id ==	0x0127	||
				id ==	0x0128	||
				id ==	0x0129	||
				id ==	0x01B9	||
				id ==	0x01BA	||
				id ==	0x01BB	||
				id ==	0x01BC	||
				id ==	0x01BD	||
				id ==	0x01BE	||
				id ==	0x01BF	||
				id ==	0x01C0	||
				id ==	0x01C1	||
				id ==	0x01C2	||
				id ==	0x01C3	||
				id ==	0x01C4	||
				id ==	0x01C5	||
				id ==	0x01C6	||
				id ==	0x01C7	||
				id ==	0x01C8	||
				id ==	0x01C9	||
				id ==	0x01CA	||
				id ==	0x01CB	||
				id ==	0x01CC	||
				id ==	0x01CD	||
				id ==	0x01CE	||
				id ==	0x01CF	||
				id ==	0x01D0	||
				id ==	0x01D1
			)){ return true; }

			if ( ( category == "snow" || category == "any" ) && ( 
				id ==	0x010C	||
				id ==	0x010D	||
				id ==	0x010E	||
				id ==	0x010F	||
				id ==	0x0114	||
				id ==	0x0115	||
				id ==	0x0116	||
				id ==	0x0117	||
				id ==	0x017C	||
				id ==	0x017D	||
				id ==	0x017E	||
				id ==	0x017F	||
				id ==	0x0180	||
				id ==	0x0181	||
				id ==	0x0182	||
				id ==	0x0183	||
				id ==	0x0184	||
				id ==	0x0185	||
				id ==	0x0186	||
				id ==	0x0187	||
				id ==	0x0188	||
				id ==	0x0189	||
				id ==	0x018A	||
				id ==	0x0755	||
				id ==	0x0756	||
				id ==	0x0757	||
				id ==	0x0758	||
				id ==	0x076D	||
				id ==	0x076E	||
				id ==	0x076F	||
				id ==	0x0770	||
				id ==	0x0771	||
				id ==	0x0772	||
				id ==	0x0773
			)){ return true; }

			return false;
		}
	}

    class WhereWorld
    {
		public static void Initialize()
		{
            CommandSystem.Register( "world", AccessLevel.Administrator, new CommandEventHandler( WhereWorld_OnCommand ) );
		}
		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "world" )]
		[Description( "Tells you what world you are in." )]
		public static void WhereWorld_OnCommand( CommandEventArgs e )
        {
			Mobile from = e.Mobile;
			string sMap = Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y );
			from.SendMessage( "You are currently in " + sMap + "." );
		}
	}
}
