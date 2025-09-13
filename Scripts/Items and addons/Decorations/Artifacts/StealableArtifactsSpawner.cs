using System;
using System.Collections;
using Server;
using Server.Commands;

namespace Server.Items
{
	public class StealableArtifactsSpawner : Item
	{
		public class StealableEntry
		{
			private Map m_Map;
			private Point3D m_Location;
			private int m_MinDelay;
			private int m_MaxDelay;
			private Type m_Type;
			private int m_Hue;

			public Map Map{ get{ return m_Map; } }
			public Point3D Location{ get{ return m_Location; } }
			public int MinDelay{ get{ return m_MinDelay; } }
			public int MaxDelay{ get{ return m_MaxDelay; } }
			public Type Type{ get{ return m_Type; } }
			public int Hue{ get{ return m_Hue; } }

			public StealableEntry( Map map, Point3D location, int minDelay, int maxDelay, Type type ) : this( map, location, minDelay, maxDelay, type, 0 )
			{
			}

			public StealableEntry( Map map, Point3D location, int minDelay, int maxDelay, Type type, int hue )
			{
				m_Map = map;
				m_Location = location;
				m_MinDelay = minDelay;
				m_MaxDelay = maxDelay;
				m_Type = type;
				m_Hue = hue;
			}

			public Item CreateInstance()
			{
				Item item = (Item) Activator.CreateInstance( m_Type );

				if ( m_Hue > 0 )
					item.Hue = m_Hue;

				item.Movable = false;
				item.MoveToWorld( this.Location, this.Map );

				return item;
			}
		}

		private static StealableEntry[] m_Entries = new StealableEntry[]
			{
				// Artifact rarity 1
				new StealableEntry( Map.Trammel, new Point3D( 5603, 1231, 0 ), 72, 108, typeof( RockArtifact ) ),
				new StealableEntry( Map.Trammel, new Point3D( 3831, 3300, 46 ), 72, 108, typeof( SkullCandleArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 2196, 842, 6 ), 72, 108, typeof( BottleArtifact ) ),
				new StealableEntry( Map.Trammel, new Point3D( 231, 3496, 20 ), 72, 108, typeof( DamagedBooksArtifact ) ),
				// Artifact rarity 2
				new StealableEntry( Map.Felucca, new Point3D( 5698, 525, 0 ), 144, 216, typeof( StretchedHideArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 2457, 491, 0 ), 144, 216, typeof( BrazierArtifact ) ),
				// Artifact rarity 3
				new StealableEntry( Map.Trammel, new Point3D( 5661, 3281, 0 ), 288, 432, typeof( LampPostArtifact ), GetLampPostHue() ),
				new StealableEntry( Map.Trammel, new Point3D( 4021, 3423, 26 ), 288, 432, typeof( BooksNorthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 2051, 60, 0 ), 288, 432, typeof( BooksWestArtifact ) ),
				new StealableEntry( Map.Trammel, new Point3D( 5936, 1431, 6 ), 288, 432, typeof( BooksFaceDownArtifact ) ),
				// Artifact rarity 5
				new StealableEntry( Map.Trammel, new Point3D( 5234, 230, 5 ), 1152, 1728, typeof( StuddedLeggingsArtifact ) ),
				new StealableEntry( Map.Trammel, new Point3D( 5479, 900, 0 ), 1152, 1728, typeof( EggCaseArtifact ) ),
				new StealableEntry( Map.Felucca, new Point3D( 5840, 361, 0 ), 1152, 1728, typeof( SkinnedGoatArtifact ) ),
				new StealableEntry( Map.Trammel, new Point3D( 4246, 3771, 0 ), 1152, 1728, typeof( GruesomeStandardArtifact ) ),
				new StealableEntry( Map.Trammel, new Point3D( 5374, 763, 0 ), 1152, 1728, typeof( BloodyWaterArtifact ) ),
				new StealableEntry( Map.Felucca, new Point3D( 5438, 187, 6 ), 1152, 1728, typeof( TarotCardsArtifact ) ),
				new StealableEntry( Map.Trammel, new Point3D( 5584, 412, 10 ), 1152, 1728, typeof( BackpackArtifact ) ),
				// Artifact rarity 7
				new StealableEntry( Map.Felucca, new Point3D( 6118, 208, 27 ), 4608, 6912, typeof( StuddedTunicArtifact ) ),
				new StealableEntry( Map.Felucca, new Point3D( 5142, 1669, 0 ), 4608, 6912, typeof( CocoonArtifact ) ),
				// Artifact rarity 8
				new StealableEntry( Map.Trammel, new Point3D( 4337, 3452, 25 ), 9216, 13824, typeof( SkinnedDeerArtifact ) ),
				// Artifact rarity 9
				new StealableEntry( Map.Felucca, new Point3D( 5608, 1839, 0 ), 18432, 27648, typeof( SaddleArtifact ) ),
				new StealableEntry( Map.Trammel, new Point3D( 5627, 2193, 5 ), 18432, 27648, typeof( LeatherTunicArtifact ) ),
				// Artifact rarity 12
				new StealableEntry( Map.Malas, new Point3D( 2207, 425, 0 ), 147456, 221184, typeof( RuinedPaintingArtifact ) )



/*
				// Yomotsu Mines - Artifact rarity 1
				new StealableEntry( Map.Malas, new Point3D(  18, 110, -1 ), 72, 108, typeof( Basket1Artifact ) ),
				new StealableEntry( Map.Malas, new Point3D(  66, 114, -1 ), 72, 108, typeof( Basket2Artifact ) ),
				// Yomotsu Mines - Artifact rarity 2
				new StealableEntry( Map.Malas, new Point3D(  63,  12, 11 ), 144, 216, typeof( Basket4Artifact ) ),
				new StealableEntry( Map.Malas, new Point3D(   5,  29, -1 ), 144, 216, typeof( Basket5NorthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D(  30,  81,  3 ), 144, 216, typeof( Basket5WestArtifact ) ),
				// Yomotsu Mines - Artifact rarity 3
				new StealableEntry( Map.Malas, new Point3D( 115,   7, -1 ), 288, 432, typeof( Urn1Artifact ) ),
				new StealableEntry( Map.Malas, new Point3D(  85,  13, -1 ), 288, 432, typeof( Urn2Artifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 110,  53, -1 ), 288, 432, typeof( Sculpture1Artifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 108,  37, -1 ), 288, 432, typeof( Sculpture2Artifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 121,  14, -1 ), 288, 432, typeof( TeapotNorthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 121, 115, -1 ), 288, 432, typeof( TeapotWestArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D(  84,  40, -1 ), 288, 432, typeof( TowerLanternArtifact ) ),
				// Yomotsu Mines - Artifact rarity 9
				new StealableEntry( Map.Malas, new Point3D(  94,   7, -1 ), 18432, 27648, typeof( ManStatuetteSouthArtifact ) ),

				// Fan Dancer's Dojo - Artifact rarity 1
				new StealableEntry( Map.Malas, new Point3D( 113, 640, -2 ), 72, 108, typeof( Basket3NorthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 102, 355, -1 ), 72, 108, typeof( Basket3WestArtifact ) ),
				// Fan Dancer's Dojo - Artifact rarity 2
				new StealableEntry( Map.Malas, new Point3D(  99, 370, -1 ), 144, 216, typeof( Basket6Artifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 100, 357, -1 ), 144, 216, typeof( ZenRock1Artifact ) ),
				// Fan Dancer's Dojo - Artifact rarity 3
				new StealableEntry( Map.Malas, new Point3D(  73, 473, -1 ), 288, 432, typeof( FanNorthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D(  99, 372, -1 ), 288, 432, typeof( FanWestArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D(  92, 326, -1 ), 288, 432, typeof( BowlsVerticalArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D(  97, 470, -1 ), 288, 432, typeof( ZenRock2Artifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 103, 691, -1 ), 288, 432, typeof( ZenRock3Artifact ) ),
				// Fan Dancer's Dojo - Artifact rarity 4
				new StealableEntry( Map.Malas, new Point3D( 103, 336,  4 ), 576, 864, typeof( Painting1NorthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D(  59, 381,  4 ), 576, 864, typeof( Painting1WestArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D(  84, 401,  2 ), 576, 864, typeof( Painting2NorthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D(  59, 392,  2 ), 576, 864, typeof( Painting2WestArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 107, 483, -1 ), 576, 864, typeof( TripleFanNorthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D(  50, 475, -1 ), 576, 864, typeof( TripleFanWestArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 107, 460, -1 ), 576, 864, typeof( BowlArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D(  90, 502, -1 ), 576, 864, typeof( CupsArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 107, 688, -1 ), 576, 864, typeof( BowlsHorizontalArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 112, 676, -1 ), 576, 864, typeof( SakeArtifact ) ),
				// Fan Dancer's Dojo - Artifact rarity 5
				new StealableEntry( Map.Malas, new Point3D( 135, 614, -1 ), 1152, 1728, typeof( SwordDisplay1NorthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D(  50, 482, -1 ), 1152, 1728, typeof( SwordDisplay1WestArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 119, 672, -1 ), 1152, 1728, typeof( Painting3Artifact ) ),
				// Fan Dancer's Dojo - Artifact rarity 6
				new StealableEntry( Map.Malas, new Point3D(  90, 326, -1 ), 2304, 3456, typeof( Painting4NorthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D(  99, 354, -1 ), 2304, 3456, typeof( Painting4WestArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 179, 652, -1 ), 2304, 3456, typeof( SwordDisplay2NorthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 118, 627, -1 ), 2304, 3456, typeof( SwordDisplay2WestArtifact ) ),
				// Fan Dancer's Dojo - Artifact rarity 7
				new StealableEntry( Map.Malas, new Point3D(  90, 483, -1 ), 4608, 6912, typeof( FlowersArtifact ) ),
				// Fan Dancer's Dojo - Artifact rarity 8
				new StealableEntry( Map.Malas, new Point3D(  71, 562, -1 ), 9216, 13824, typeof( DolphinLeftArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 102, 677, -1 ), 9216, 13824, typeof( DolphinRightArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D(  61, 499,  0 ), 9216, 13824, typeof( SwordDisplay3SouthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 182, 669, -1 ), 9216, 13824, typeof( SwordDisplay3EastArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 162, 647, -1 ), 9216, 13824, typeof( SwordDisplay4WestArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 124, 624,  0 ), 9216, 13824, typeof( Painting5NorthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 146, 649,  2 ), 9216, 13824, typeof( Painting5WestArtifact ) ),
				// Fan Dancer's Dojo - Artifact rarity 9
				new StealableEntry( Map.Malas, new Point3D( 100, 488, -1 ), 18432, 27648, typeof( SwordDisplay4NorthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 175, 606,  0 ), 18432, 27648, typeof( SwordDisplay5NorthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 157, 608, -1 ), 18432, 27648, typeof( SwordDisplay5WestArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 187, 643,  1 ), 18432, 27648, typeof( Painting6NorthArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 146, 623,  1 ), 18432, 27648, typeof( Painting6WestArtifact ) ),
				new StealableEntry( Map.Malas, new Point3D( 178, 629, -1 ), 18432, 27648, typeof( ManStatuetteEastArtifact ) )
*/



			};

		public static StealableEntry[] Entries{ get{ return m_Entries; } }

		private static Type[] m_TypesOfEntries = null;
		public static Type[] TypesOfEntires
		{
			get
			{
				if( m_TypesOfEntries == null )
				{
					m_TypesOfEntries = new Type[m_Entries.Length];

					for( int i = 0; i < m_Entries.Length; i++ )
						m_TypesOfEntries[i] = m_Entries[i].Type;
				}

				return m_TypesOfEntries;
			}
		}

		private static StealableArtifactsSpawner m_Instance;

		public static StealableArtifactsSpawner Instance{ get{ return m_Instance; } }

		private static int GetLampPostHue()
		{
			if ( 0.9 > Utility.RandomDouble() )
				return 0;

			return Utility.RandomList( 0x455, 0x47E, 0x482, 0x486, 0x48F, 0x4F2, 0x58C, 0x66C );
		}


		public static void Initialize()
		{
			CommandSystem.Register( "GenStealArties", AccessLevel.Administrator, new CommandEventHandler( GenStealArties_OnCommand ) );
			CommandSystem.Register( "RemoveStealArties", AccessLevel.Administrator, new CommandEventHandler( RemoveStealArties_OnCommand ) );
		}

		[Usage( "GenStealArties" )]
		[Description( "Generates the stealable artifacts spawner." )]
		public static void GenStealArties_OnCommand( CommandEventArgs args )
		{
			Mobile from = args.Mobile;

			if ( Create() )
				from.SendMessage( "Stealable artifacts spawner generated." );
			else
				from.SendMessage( "Stealable artifacts spawner already present." );
		}

		[Usage( "RemoveStealArties" )]
		[Description( "Removes the stealable artifacts spawner and every not yet stolen stealable artifacts." )]
		public static void RemoveStealArties_OnCommand( CommandEventArgs args )
		{
			Mobile from = args.Mobile;

			if ( Remove() )
				from.SendMessage( "Stealable artifacts spawner removed." );
			else
				from.SendMessage( "Stealable artifacts spawner not present." );
		}

		public static bool Create()
		{
			if ( m_Instance != null && !m_Instance.Deleted )
				return false;

			m_Instance = new StealableArtifactsSpawner();
			return true;
		}

		public static bool Remove()
		{
			if ( m_Instance == null )
				return false;

			m_Instance.Delete();
			m_Instance = null;
			return true;
		}

		public static StealableInstance GetStealableInstance( Item item )
		{
			if ( Instance == null )
				return null;

			return (StealableInstance) Instance.m_Table[item];
		}


		public class StealableInstance
		{
			private StealableEntry m_Entry;
			private Item m_Item;
			private DateTime m_NextRespawn;

			public StealableEntry Entry{ get{ return m_Entry; } }

			public Item Item
			{
				get{ return m_Item; }
				set
				{
					if ( m_Item != null && value == null )
					{
						int delay = Utility.RandomMinMax( this.Entry.MinDelay, this.Entry.MaxDelay );
						this.NextRespawn = DateTime.UtcNow + TimeSpan.FromMinutes( delay );
					}

					if ( Instance != null )
					{
						if ( m_Item != null	)
							Instance.m_Table.Remove( m_Item );

						if ( value != null )
							Instance.m_Table[value] = this;
					}

					m_Item = value;
				}
			}

			public DateTime NextRespawn
			{
				get{ return m_NextRespawn; }
				set{ m_NextRespawn = value; }
			}

			public StealableInstance( StealableEntry entry ) : this( entry, null, DateTime.UtcNow )
			{
			}

			public StealableInstance( StealableEntry entry, Item item, DateTime nextRespawn )
			{
				m_Item = item;
				m_NextRespawn = nextRespawn;
				m_Entry = entry;
			}

			public void CheckRespawn()
			{
				if ( this.Item != null && ( this.Item.Deleted || this.Item.Movable || this.Item.Parent != null ) )
					this.Item = null;

				if ( this.Item == null && DateTime.UtcNow >= this.NextRespawn )
				{
					this.Item = this.Entry.CreateInstance();
				}
			}
		}

		private Timer m_RespawnTimer;
		private StealableInstance[] m_Artifacts;
		private Hashtable m_Table;

		public override string DefaultName
		{
			get { return "Stealable Artifacts Spawner - Internal"; }
		}

		private StealableArtifactsSpawner() : base( 1 )
		{
			Movable = false;

			m_Artifacts = new StealableInstance[m_Entries.Length];
			m_Table = new Hashtable( m_Entries.Length );

			for ( int i = 0; i < m_Entries.Length; i++ )
			{
				m_Artifacts[i] = new StealableInstance( m_Entries[i] );
			}

			m_RespawnTimer = Timer.DelayCall( TimeSpan.Zero, TimeSpan.FromMinutes( 15.0 ), new TimerCallback( CheckRespawn ) );
		}

		public override void OnDelete()
		{
			base.OnDelete();

			if ( m_RespawnTimer != null )
			{
				m_RespawnTimer.Stop();
				m_RespawnTimer = null;
			}

			foreach ( StealableInstance si in m_Artifacts )
			{
				if ( si.Item != null )
					si.Item.Delete();
			}

			m_Instance = null;
		}

		public void CheckRespawn()
		{
			foreach ( StealableInstance si in m_Artifacts )
			{
				si.CheckRespawn();
			}
		}

		public StealableArtifactsSpawner( Serial serial ) : base( serial )
		{
			m_Instance = this;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version

			writer.WriteEncodedInt( m_Artifacts.Length );

			for ( int i = 0; i < m_Artifacts.Length; i++ )
			{
				StealableInstance si = m_Artifacts[i];

				writer.Write( (Item) si.Item );
				writer.WriteDeltaTime( (DateTime) si.NextRespawn );
			}
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_Artifacts = new StealableInstance[m_Entries.Length];
			m_Table = new Hashtable( m_Entries.Length );

			int length = reader.ReadEncodedInt();

			for ( int i = 0; i < length; i++ )
			{
				Item item = reader.ReadItem();
				DateTime nextRespawn = reader.ReadDeltaTime();

				if ( i < m_Artifacts.Length )
				{
					StealableInstance si = new StealableInstance( m_Entries[i], item, nextRespawn );
					m_Artifacts[i] = si;

					if ( si.Item != null )
						m_Table[si.Item] = si;
				}
			}

			for ( int i = length; i < m_Entries.Length; i++ )
			{
				m_Artifacts[i] = new StealableInstance( m_Entries[i] );
			}

			m_RespawnTimer = Timer.DelayCall( TimeSpan.Zero, TimeSpan.FromMinutes( 15.0 ), new TimerCallback( CheckRespawn ) );
		}
	}
}