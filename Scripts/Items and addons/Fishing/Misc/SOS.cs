using System;
using Server.Network;
using Server.Gumps;
using Server.Misc;

namespace Server.Items
{
	public class SOS : Item
	{
		public override int LabelNumber
		{
			get
			{
				if ( IsAncient )
					return 1063450; // an ancient SOS

				return 1041081; // a waterstained SOS
			}
		}

		private int m_Level;
		private Map m_TargetMap;
		private Point3D m_TargetLocation;
		public string MapWorld;
		public string ShipStory;
		public string ShipName;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsAncient { get{ return ( m_Level >= 4 ); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Level
		{
			get{ return m_Level; }
			set
			{
				m_Level = Math.Max( 1, Math.Min( value, 4 ) );
				UpdateHue();
				InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map TargetMap { get{ return m_TargetMap; } set{ m_TargetMap = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D TargetLocation { get{ return m_TargetLocation; } set{ m_TargetLocation = value; } }

		[CommandProperty(AccessLevel.Owner)]
		public string Map_World { get { return MapWorld; } set { MapWorld = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Ship_Story { get { return ShipStory; } set { ShipStory = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Ship_Name { get { return ShipName; } set { ShipName = value; InvalidateProperties(); } }

		public void UpdateHue()
		{
			if ( IsAncient )
				Hue = Utility.RandomList( 0xB8E, 0xB8F, 0xB90, 0xB91, 0xB92, 0xB89, 0xB8B );
			else
				Hue = 0;
		}

		[Constructable]
		public SOS( string world, int level ) : base( 0x14ED )
		{
			if ( level < 1 ){ level = MessageInABottle.GetRandomLevel(); }

			if ( world == "the Town of Skara Brae" ){ world = "the Land of Sosaria"; } // NO SOSs IN SKARA BRAE
			else if ( world == "the Moon of Luna" ){ world = "the Land of Sosaria"; } // NO SOSs ON THE MOON
			else if ( world == "the Underworld" ){ world = "the Land of Sosaria"; } // NO SOSs IN THE UNDERWORLD

			Weight = 1.0;

			Point3D loc = Worlds.GetRandomLocation( world, "sea" );
			Map map = Worlds.GetMyDefaultMap( world );

			MapWorld = world;
			m_Level = level;
			m_TargetMap = map;

			m_TargetLocation = loc;

			UpdateHue();


			ShipName = RandomThings.GetRandomShipName( "", 0 );


			string Beast = "a sea dragon";
			switch ( Utility.Random( 12 ) )
			{
				case 0: Beast = "a gigantic monster"; break;
				case 1: Beast = "a sea hag"; break;
				case 2: Beast = "a leviathan"; break;
				case 3: Beast = "a sea dragon"; break;
				case 4: Beast = "a sea giant"; break;
				case 5: Beast = "a storm giant"; break;
				case 6: Beast = "a sea serpent"; break;
				case 7: Beast = "a demon of the sea"; break;
				case 8: Beast = "a rotting squid"; break;
				case 9: Beast = "a giant beast"; break;
				case 10: Beast = "a dragon turtle"; break;
				case 11: Beast = "a huge creature"; break;
			}

			if ( IsAncient ){ ShipStory = "This parchment is very old and almost crumbles in your hand. You know that whoever wrote this has been dead for possibly centuries, but it reads... "; }

			switch ( Utility.Random( 5 ) )
			{
				case 0: ShipStory = ShipStory + "We were sailing in " + MapWorld + " when " + Beast + " rose from the depths of the ocean and attacked our ship! The hull has taken alot of damage and '" + ShipName + "' is slowly sinking into the depths of the sea! Whoever finds this, send a ship to the coordinates below! Hurry! I am not sure how long we will last out here!"; break;
				case 1: ShipStory = ShipStory + "If ya never seen " + Beast + " before, consider yerself lucky. There be little warning before they hit our ship, '" + ShipName + "', while sailing in " + MapWorld + ". We thought we hit a reef but we were wrong. It tore the ship apart. Only me and " + QuestCharacters.ParchmentWriter() + " managed to survive the onslaught of the beast. Now we sit here, on some island. The coordinates I last remember is where your ship went down. We may be close to there if you can send a ship. There be gold for payment if you do."; break;
				case 2: ShipStory = ShipStory + "I am writing this with my dying strength on board '" + ShipName + "'. " + QuestCharacters.ParchmentWriter() + " the Pirate came upon us in the night while far from land in " + MapWorld + ". We didn't stand a chance. We tried to outrun er but the wind was against us to be sure. He set our ship ablaze and fled off into the distance. Now we slowly sink into the ocean. If you find this, I wrote our coordinates below. You may still get here in time to save the others. If you can, tell " + QuestCharacters.ParchmentWriter() + " my tale so they never live wondering my fate. They live somewhere in " + RandomThings.GetRandomCity() + "."; break;
				case 3: ShipStory = ShipStory + "'" + ShipName + "' be sinking far from land. What we thought was a merchant ship was actually a war ship in disguise. They be hunting us pirates on the high seas in " + MapWorld + "...and today our luck ran out. Their cannons ripped through our sails, and tore holes in our hull. They killed most of the crew, where only " + Utility.RandomMinMax( 3, 16 ) + " of us survived. They be gone now, but the sharks started circling the wreck. I just saw " + QuestCharacters.ParchmentWriter() + " being pulled below the waves, blood gushing up from below. I be on the largest piece of flotsam and can only hope I survive till ya get here."; break;
				case 4: ShipStory = ShipStory + "I knew " + QuestCharacters.ParchmentWriter() + " weren't no good at being a captain of '" + ShipName + "'. Now this probably be our end here in " + MapWorld + ". We be under attack by " + Beast + " and we have no chance of making it to " + RandomThings.GetRandomCity() + " now. I fear that me never see me wife again. If ye find this note, please find us before we sink. I have an ancient artifact I could trade for yer help."; break;
			}
		}

		public SOS( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 4 ); // version
			writer.Write( m_Level );
			writer.Write( m_TargetMap );
			writer.Write( m_TargetLocation );
            writer.Write( MapWorld );
            writer.Write( ShipName );
            writer.Write( ShipStory );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = reader.ReadInt();
			m_TargetMap = reader.ReadMap();
			m_TargetLocation = reader.ReadPoint3D();
            MapWorld = reader.ReadString();
            ShipName = reader.ReadString();
            ShipStory = reader.ReadString();
			ItemID = 0x12AD;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.CloseGump( typeof( MessageGump ) );
				from.SendGump( new MessageGump( m_TargetMap, m_TargetLocation, MapWorld, ShipStory ) );
				from.PlaySound( 0x249 );
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		private class MessageGump : Gump
		{
			public MessageGump( Map map, Point3D loc, string world, string story ) : base( 150, 50 )
			{
				int xLong = 0, yLat = 0;
				int xMins = 0, yMins = 0;
				bool xEast = false, ySouth = false;
				string fmt;

				if ( Sextant.Format( loc, map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
					fmt = String.Format( "{0}°{1}'{2},{3}°{4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
				else
					fmt = "?????";

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(46, 26, 1247);
				AddHtml( 102, 58, 284, 202, @"<BODY><BASEFONT Color=#111111><BIG>" + story + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
				AddHtml( 102, 264, 280, 22, @"<BODY><BASEFONT Color=#111111><BIG>" + fmt + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}

			public override void OnResponse( NetState state, RelayInfo info ) 
			{
				Mobile from = state.Mobile; 
				from.PlaySound( 0x249 );
			}
		}
	}
}