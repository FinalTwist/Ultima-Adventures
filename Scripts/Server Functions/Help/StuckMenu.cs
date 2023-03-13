using System;
using Server.Network;
using Server.Gumps;
using Server.Regions;
using Server.Misc;

namespace Server.Menus.Questions
{
	public class StuckMenuEntry
	{
		private TextDefinition m_Name;
		private Point3D[] m_Locations;

		public TextDefinition Name{ get{ return m_Name; } }
		public Point3D[] Locations{ get{ return m_Locations; } }

		public StuckMenuEntry( TextDefinition name, Point3D[] locations )
		{
			m_Name = name;
			m_Locations = locations;
		}
	}

	public class StuckMenu : Gump
	{
		public void AddHtmlText( int x, int y, int width, int height, TextDefinition text, bool back, bool scroll )
		{
			if ( text != null && text.Number > 0 )
				AddHtmlLocalized( x, y, width, height, text.Number, back, scroll );
			else if ( text != null && text.String != null )
				AddHtml( x, y, width, height, text.String, back, scroll );
		}

		private static StuckMenuEntry[] m_Entries = new StuckMenuEntry[] { new StuckMenuEntry( "Sosaria", new Point3D[] { new Point3D( 3213, 3673, 0 ) } ) };
		private static StuckMenuEntry[] m_LunaEntries = new StuckMenuEntry[] { new StuckMenuEntry( "Luna", new Point3D[] { new Point3D( 5884, 2864, 0 ) } ) };
		private static StuckMenuEntry[] m_AmbrosiaEntries = new StuckMenuEntry[] { new StuckMenuEntry( "Ambrosia", new Point3D[] { new Point3D( 3325, 3934, 0 ) } ) };
		private static StuckMenuEntry[] m_UmberEntries = new StuckMenuEntry[] { new StuckMenuEntry( "Umber Veil", new Point3D[] { new Point3D( 2982, 3696, 0 ) } ) };
		private static StuckMenuEntry[] m_SerpentEntries = new StuckMenuEntry[] { new StuckMenuEntry( "Serpent Island", new Point3D[] { new Point3D( 2191, 315, 0 ) } ) };
		private static StuckMenuEntry[] m_LodorEntries = new StuckMenuEntry[] { new StuckMenuEntry( "Lodoria", new Point3D[] { new Point3D( 5727, 3467, 0 ) } ) };
		private static StuckMenuEntry[] m_DreadEntries = new StuckMenuEntry[] { new StuckMenuEntry( "Isles of Dread", new Point3D[] { new Point3D( 237, 324, 3 ) } ) };
		private static StuckMenuEntry[] m_SavageEntries = new StuckMenuEntry[] { new StuckMenuEntry( "Savaged Empire", new Point3D[] { new Point3D( 1110, 2541, -1 ) } ) };
		private static StuckMenuEntry[] m_KuldarEntries = new StuckMenuEntry[] { new StuckMenuEntry( "Bottle World", new Point3D[] { new Point3D( 6714, 705, 0 ) } ) };
		private static StuckMenuEntry[] m_BardEntries = new StuckMenuEntry[] { new StuckMenuEntry( "Skara Brae", new Point3D[] { new Point3D( 7041, 213, 0 ) } ) };
		private static StuckMenuEntry[] m_UnderworldEntries = new StuckMenuEntry[] { new StuckMenuEntry( "Underworld", new Point3D[] { new Point3D( 1501, 477, -17 ) } ) };
		private static StuckMenuEntry[] m_DarkMoorEntries = new StuckMenuEntry[] { new StuckMenuEntry( "DarkMoor", new Point3D[] { new Point3D( 747, 1000, -23 ) } ) };

		private Mobile m_Mobile, m_Sender;
		private bool m_MarkUse;
		private Map m_Map;

		private Timer m_Timer;

		public StuckMenu( Mobile beholder, Mobile beheld, bool markUse ) : base( 25, 25 )
		{
			m_Sender = beholder;
			m_Mobile = beheld;
			m_MarkUse = markUse;
			m_Map = Map.Trammel;

			StuckMenuEntry[] entries = m_Entries;

			string myWorld = Worlds.GetMyWorld( m_Mobile.Map, m_Mobile.Location, m_Mobile.X, m_Mobile.Y );

			if ( myWorld == "the Moon of Luna" ) { entries = m_LunaEntries; m_Map = Map.Trammel; }
			else if ( myWorld == "the Land of Ambrosia" ) { entries = m_AmbrosiaEntries; m_Map = Map.Trammel; }
			else if ( myWorld == "the Island of Umber Veil" ) { entries = m_UmberEntries; m_Map = Map.Trammel; }
			else if ( myWorld == "the Bottle World of Kuldar" ) { entries = m_KuldarEntries; m_Map = Map.Trammel; }
			else if ( myWorld == "the Town of Skara Brae" ) { entries = m_BardEntries; m_Map = Map.Felucca; }
			else if ( myWorld == "the Land of Lodoria" ) { entries = m_LodorEntries; m_Map = Map.Felucca; }
			else if ( myWorld == "the Land of Sosaria" ) { entries = m_Entries; m_Map = Map.Trammel; }
			else if ( myWorld == "the Underworld" ) { entries = m_UnderworldEntries; m_Map = Map.Ilshenar; }
			else if ( myWorld == "DarkMoor" ) { entries = m_DarkMoorEntries; m_Map = Map.Ilshenar; }
			else if ( myWorld == "the Serpent Island" ) { entries = m_SerpentEntries; m_Map = Map.Malas; }
			else if ( myWorld == "the Isles of Dread" ) { entries = m_DreadEntries; m_Map = Map.Tokuno; }
			else if ( myWorld == "the Savaged Empire" ) { entries = m_SavageEntries; m_Map = Map.TerMur; }

            this.Closable=true;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 154);
			AddImage(105, 0, 154);
			AddImage(0, 30, 154);
			AddImage(105, 30, 154);
			AddImage(2, 2, 129);
			AddImage(103, 2, 129);
			AddImage(2, 28, 129);
			AddImage(103, 28, 129);
			AddImage(345, 86, 131);
			AddImage(8, 8, 150);
			AddImage(157, 24, 156);
			AddImage(46, 286, 130);
			AddImage(18, 281, 159);
			AddImage(181, 26, 156);
			AddHtml( 171, 21, 219, 22, @"<BODY><BASEFONT Color=#FBFBFB><BIG>STUCK IN THE WORLD</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			for ( int i = 0; i < entries.Length; i++ )
			{
				StuckMenuEntry entry = entries[i];

				AddButton(86, 132, 4005, 4005, i + 1, GumpButtonType.Reply, 0);
				AddHtml( 128, 132, 206, 55, @"<BODY><BASEFONT Color=#FCFF00><BIG>Take me to a safe place in " + myWorld + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}

			AddHtml( 128, 217, 206, 22, @"<BODY><BASEFONT Color=#FCFF00><BIG>Cancel</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(86, 217, 4017, 4017, 0, GumpButtonType.Reply, 0);
		}

		public void BeginClose()
		{
			StopClose();

			m_Timer = new CloseTimer( m_Mobile );
			m_Timer.Start();

			m_Mobile.Frozen = true;
		}

		public void StopClose()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			m_Mobile.Frozen = false;
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			StopClose();

			if ( info.ButtonID == 0 )
			{
				if ( m_Mobile == m_Sender )
				{
					m_Mobile.SendSound( 0x4A ); 
					m_Mobile.SendMessage( "You choose to remain where you are." );
					m_Mobile.CloseGump( typeof(Server.Engines.Help.HelpGump) );
					m_Mobile.SendGump( new Server.Engines.Help.HelpGump( m_Mobile, 1 ) );
				}
			}
			else
			{
				int index = info.ButtonID - 1;

				StuckMenuEntry[] entries = m_Entries;

				string myWorld = Worlds.GetMyWorld( m_Mobile.Map, m_Mobile.Location, m_Mobile.X, m_Mobile.Y );

				if ( m_Mobile.Map == Map.Trammel && m_Mobile.X > 147 && m_Mobile.X < 257 && m_Mobile.Y > 2245 && m_Mobile.Y < 2313 )
					m_Mobile.SendMessage( "This doesn't work here." );

				if ( myWorld == "the Moon of Luna" ) { entries = m_LunaEntries; }
				else if ( myWorld == "the Land of Ambrosia" ) { entries = m_AmbrosiaEntries; }
				else if ( myWorld == "the Island of Umber Veil" ) { entries = m_UmberEntries; }
				else if ( myWorld == "the Bottle World of Kuldar" ) { entries = m_KuldarEntries; }
				else if ( myWorld == "the Town of Skara Brae" ) { entries = m_BardEntries; }
				else if ( myWorld == "the Land of Lodoria" ) { entries = m_LodorEntries; }
				else if ( myWorld == "the Land of Sosaria" ) { entries = m_Entries; }
				else if ( myWorld == "the Serpent Island" ) { entries = m_SerpentEntries; }
				else if ( myWorld == "the Isles of Dread" ) { entries = m_DreadEntries; }
				else if ( myWorld == "the Savaged Empire" ) { entries = m_SavageEntries; }
				else if ( myWorld == "the Underworld" ) { entries = m_UnderworldEntries; }

				if ( index >= 0 && index < entries.Length )
					Teleport( entries[index] );
			}
		}

		private void Teleport( StuckMenuEntry entry )
		{
			if ( m_MarkUse ) 
			{
				m_Mobile.SendLocalizedMessage( 1010589 ); // You will be teleported within the next two minutes.

				new TeleportTimer( m_Mobile, entry, TimeSpan.FromSeconds( 10.0 + (Utility.RandomDouble() * 110.0) ), m_Map ).Start();

				m_Mobile.UsedStuckMenu();
			}
			else
			{
				new TeleportTimer( m_Mobile, entry, TimeSpan.Zero, m_Map ).Start();
			}
		}

		private class CloseTimer : Timer
		{
			private Mobile m_Mobile;
			private DateTime m_End;

			public CloseTimer( Mobile m ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Mobile = m;
				m_End = DateTime.UtcNow + TimeSpan.FromMinutes( 3.0 );
			}

			protected override void OnTick()
			{
				if ( m_Mobile.NetState == null || DateTime.UtcNow > m_End )
				{
					m_Mobile.Frozen = false;
					m_Mobile.CloseGump( typeof( StuckMenu ) );

					Stop();
				}
				else
				{
					m_Mobile.Frozen = true;
				}
			} 
		} 

		private class TeleportTimer : Timer
		{
			private Map m_Map;
			private Mobile m_Mobile;
			private StuckMenuEntry m_Destination;
			private DateTime m_End;

			public TeleportTimer( Mobile mobile, StuckMenuEntry destination, TimeSpan delay, Map world ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 1.0 ) )
			{
				Priority = TimerPriority.TwoFiftyMS;

				m_Mobile = mobile;
				m_Map = world;
				m_Destination = destination;
				m_End = DateTime.UtcNow + delay;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow < m_End )
				{
					m_Mobile.Frozen = true;
				}
				else
				{
					m_Mobile.Frozen = false;
					Stop();

					int idx = Utility.Random( m_Destination.Locations.Length );
					Point3D dest = m_Destination.Locations[idx];

					Map destMap = m_Map;

					Mobiles.BaseCreature.TeleportPets( m_Mobile, dest, destMap );
					m_Mobile.MoveToWorld( dest, destMap );
				}
			}
		}
	}
}