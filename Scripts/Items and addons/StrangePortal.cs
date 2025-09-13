using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Network;


namespace Server.Items
{
	public class StrangePortal : Item
	{
		public int GateLocation;


		[CommandProperty(AccessLevel.Owner)]
		public int Gate_Location { get { return GateLocation; } set { GateLocation = value; InvalidateProperties(); } }


		[Constructable]
		public StrangePortal() : base(0xF6C)
		{
			Movable = false;
			Light = LightType.Circle300;


			string sCalled = "a strange";
			switch( Utility.RandomMinMax( 0, 6 ) )
			{
				case 0: sCalled = "an odd"; break;
				case 1: sCalled = "an unusual"; break;
				case 2: sCalled = "a bizarre"; break;
				case 3: sCalled = "a curious"; break;
				case 4: sCalled = "a peculiar"; break;
				case 5: sCalled = "a strange"; break;
				case 6: sCalled = "a weird"; break;
			}


			Name = sCalled + " portal";
			Hue = RandomThings.GetRandomColor(0);
			GateLocation = 0;
		}


		public StrangePortal(Serial serial) : base(serial)
		{
		}


		public override bool OnMoveOver( Mobile m )
		{
			if ( GateLocation == 0 )
			{
				GateLocation = Utility.RandomMinMax( 15, 26 );
				string world = Worlds.GetMyWorld( m.Map, m.Location, m.X, m.Y );
				if ( world == "the Land of Lodoria" ){ GateLocation = Utility.RandomMinMax( 0, 14 ); }
				else if ( world == "the Serpent Island" ){ GateLocation = Utility.RandomMinMax( 27, 41 ); }
				else if ( world == "the Isles of Dread" ){ GateLocation = Utility.RandomMinMax( 42, 47 ); }
				else if ( world == "the Savaged Empire" ){ GateLocation = Utility.RandomMinMax( 48, 56 ); }
				else if ( world == "the Underworld" ){ GateLocation = Utility.RandomMinMax( 57, 64 ); }
			}


			if ( m is PlayerMobile )
			{
				UseGate( m, GateLocation );
				Effects.PlaySound( m.Location, m.Map, 0x1FC );
			}


			return false;
		}


		public override void OnAfterSpawn()
		{
			Server.Mobiles.PremiumSpawner.SpreadItems( this );
			base.OnAfterSpawn();
		}


		public static void UseGate( Mobile m, int portal )
		{
			Point3D loc = new Point3D(0, 0, 0);
			Map map = Map.Trammel;
			int teleport = 22;


			switch ( portal )
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
				case 14: loc = new Point3D(6035, 2574, 0); map = Map.Felucca; break; // Stonegate Castle


				case 15: loc = new Point3D(5854, 1756, 0); map = Map.Trammel; break; // the Caverns of Poseidon
				case 16: loc = new Point3D(5354, 923, 0); map = Map.Trammel; break; // the Ancient Pyramid
				case 17: loc = new Point3D(5965, 636, 0); map = Map.Trammel; break; // Dungeon Exodus
				case 18: loc = new Point3D(262, 3380, 0); map = Map.Trammel; break; // the Cave of Banished Mages
				case 19: loc = new Point3D(5981, 2154, 0); map = Map.Trammel; break; // Dungeon Clues
				case 20: loc = new Point3D(5550, 393, 0); map = Map.Trammel; break; // Dardin's Pit
				case 21: loc = new Point3D(5259, 262, 0); map = Map.Trammel; break; // Dungeon Doom
				case 22: loc = new Point3D(5526, 1228, 0); map = Map.Trammel; break; // the Fires of Hell
				case 23: loc = new Point3D(5587, 1602, 0); map = Map.Trammel; break; // the Mines of Morinia
				case 24: loc = new Point3D(5995, 423, 0); map = Map.Trammel; break; // the Perinian Depths
				case 25: loc = new Point3D(5638, 821, 0); map = Map.Trammel; break; // the Dungeon of Time Awaits
				case 26: loc = new Point3D(100, 3389, 0); map = Map.TerMur; break; // Forgotten Halls


				case 27: loc = new Point3D(1955, 523, 0); map = Map.Malas; break; // the Ancient Prison
				case 28: loc = new Point3D(2090, 863, 0); map = Map.Malas; break; // the Cave of Fire
				case 29: loc = new Point3D(2440, 53, 2); map = Map.Malas; break; // the Cave of Souls
				case 30: loc = new Point3D(2032, 76, 0); map = Map.Malas; break; // Dungeon Ankh
				case 31: loc = new Point3D(1947, 216, 0); map = Map.Malas; break; // Dungeon Bane
				case 32: loc = new Point3D(2189, 425, 0); map = Map.Malas; break; // Dungeon Hate
				case 33: loc = new Point3D(2221, 816, 0); map = Map.Malas; break; // Dungeon Scorn
				case 34: loc = new Point3D(1957, 710, 0); map = Map.Malas; break; // Dungeon Torment
				case 35: loc = new Point3D(2361, 403, 0); map = Map.Malas; break; // Dungeon Vile
				case 36: loc = new Point3D(2160, 173, 2); map = Map.Malas; break; // Dungeon Wicked
				case 37: loc = new Point3D(2311, 912, 2); map = Map.Malas; break; // Dungeon Wrath
				case 38: loc = new Point3D(2459, 880, 0); map = Map.Malas; break; // the Flooded Temple
				case 39: loc = new Point3D(2064, 509, 0); map = Map.Malas; break; // the Gargoyle Crypts
				case 40: loc = new Point3D(2457, 506, 0); map = Map.Malas; break; // the Serpent Sanctum
				case 41: loc = new Point3D(2327, 183, 2); map = Map.Malas; break; // the Tomb of the Fallen Wizard


				case 42: loc = new Point3D(729, 2635, -28); map = Map.TerMur; break; // the Blood Temple
				case 43: loc = new Point3D(323, 2836, 0); map = Map.TerMur; break; // the Ice Queen Fortress
				case 44: loc = new Point3D(366, 3886, 0); map = Map.TerMur; break; // the Scurvy Reef
				case 45: loc = new Point3D(1968, 1363, 61); map = Map.Ilshenar; break; // the Glacial Scar
				case 46: loc = new Point3D(6142, 3660, -20); map = Map.Felucca; break; // the Temple of Osirus
				case 47: loc = new Point3D(6021, 1968, 0); map = Map.Felucca; break; // the Sanctum of Saltmarsh


				case 48: loc = new Point3D(774, 1984, -28); map = Map.TerMur; break; // the Dungeon of the Mad Archmage
				case 49: loc = new Point3D(51, 2619, -28); map = Map.TerMur; break; // the Tombs
				case 50: loc = new Point3D(342, 2296, -1); map = Map.TerMur; break; // the Dungeon of the Lich King
				case 51: loc = new Point3D(1143, 2403, -28); map = Map.TerMur; break; // the Halls of Ogrimar
				case 52: loc = new Point3D(692, 2319, -27); map = Map.TerMur; break; // Dungeon Rock
				case 53: loc = new Point3D(647, 3860, 39); map = Map.TerMur; break; // the Undersea Castle
				case 54: loc = new Point3D(231, 3650, 25); map = Map.TerMur; break; // the Azure Castle
				case 55: loc = new Point3D(436, 3311, 20); map = Map.TerMur; break; // the Tomb of Kazibal
				case 56: loc = new Point3D(670, 3357, 20); map = Map.TerMur; break; // the Catacombs of Azerok


				case 57: loc = new Point3D(1851, 1233, -42); map = Map.Ilshenar; break; // the Stygian Abyss
				case 58: loc = new Point3D(6413, 2004, -40); map = Map.Felucca; break; // the Daemon's Crag
				case 59: loc = new Point3D(7003, 2437, -11); map = Map.Felucca; break; // the Zealan Tombs
				case 60: loc = new Point3D(6368, 968, 25); map = Map.Felucca; break; // the Hall of the Mountain King
				case 61: loc = new Point3D(6826, 1123, -92); map = Map.Felucca; break; // Morgaelin's Inferno
				case 62: loc = new Point3D(5950, 1654, -5); map = Map.Felucca; break; // the Depths of Carthax Lake
				case 63: loc = new Point3D(5989, 484, 1); map = Map.Felucca; break; // Argentrock Castle
				case 64: loc = new Point3D(1125, 3684, 0); map = Map.Felucca; break; // the Ancient Sky Ship
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


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( GateLocation );
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            GateLocation = reader.ReadInt();
		}
	}
}