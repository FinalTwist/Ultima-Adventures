//Engine r53
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Server;
using Server.Commands;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Regions;
using CPA = Server.CommandPropertyAttribute;

namespace Server.Mobiles
{
	public class PremiumSpawner : Item
	{
		private int m_Team;
		private int m_HomeRange;          // = old SpawnRange
		private int m_WalkingRange = -1;  // = old HomeRange
		private int m_SpawnID = 1;
		private int m_Count;
		private int m_CountA;
		private int m_CountB;
		private int m_CountC;
		private int m_CountD;
		private int m_CountE;
		private TimeSpan m_MinDelay;
		private TimeSpan m_MaxDelay;
		private List<string> m_CreaturesName; // creatures to be spawned
		private List<IEntity> m_Creatures;    // spawned creatures
		private List<string> m_CreaturesNameA;
		private List<IEntity> m_CreaturesA;
		private List<string> m_CreaturesNameB;
		private List<IEntity> m_CreaturesB;
		private List<string> m_CreaturesNameC;
		private List<IEntity> m_CreaturesC;
		private List<string> m_CreaturesNameD;
		private List<IEntity> m_CreaturesD;
		private List<string> m_CreaturesNameE;
		private List<IEntity> m_CreaturesE;
		private DateTime m_End;
		private InternalTimer m_Timer;
		private bool m_Running;
		private bool m_Group;
		private WayPoint m_WayPoint;

		public bool IsFull{ get{ return ( m_Creatures != null && m_Creatures.Count >= m_Count ); } }
		public bool IsFulla{ get{ return ( m_CreaturesA != null && m_CreaturesA.Count >= m_CountA ); } }
		public bool IsFullb{ get{ return ( m_CreaturesB != null && m_CreaturesB.Count >= m_CountB ); } }
		public bool IsFullc{ get{ return ( m_CreaturesC != null && m_CreaturesC.Count >= m_CountC ); } }
		public bool IsFulld{ get{ return ( m_CreaturesD != null && m_CreaturesD.Count >= m_CountD ); } }
		public bool IsFulle{ get{ return ( m_CreaturesE != null && m_CreaturesE.Count >= m_CountE ); } }
		
		//public override bool HandlesOnMovement{ get{ return MyServerSettings.EnableAmbientSoundEffects(); } } //FINAL
		//private DateTime m_NextSound;	
		//public DateTime NextSound{ get{ return m_NextSound; } set{ m_NextSound = value; } }
		
		public List<string> CreaturesName
		{
			get { return m_CreaturesName; }
			set
			{
				m_CreaturesName = value;
				if ( m_CreaturesName.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public List<string> SubSpawnerA
		{
			get { return m_CreaturesNameA; }
			set
			{
				m_CreaturesNameA = value;
				if ( m_CreaturesNameA.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public List<string> SubSpawnerB
		{
			get { return m_CreaturesNameB; }
			set
			{
				m_CreaturesNameB = value;
				if ( m_CreaturesNameB.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public List<string> SubSpawnerC
		{
			get { return m_CreaturesNameC; }
			set
			{
				m_CreaturesNameC = value;
				if ( m_CreaturesNameC.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public List<string> SubSpawnerD
		{
			get { return m_CreaturesNameD; }
			set
			{
				m_CreaturesNameD = value;
				if ( m_CreaturesNameD.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public List<string> SubSpawnerE
		{
			get { return m_CreaturesNameE; }
			set
			{
				m_CreaturesNameE = value;
				if ( m_CreaturesNameE.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public virtual int CreaturesNameCount { get { return m_CreaturesName.Count; } }
		public virtual int CreaturesNameCountA { get { return m_CreaturesNameA.Count; } }
		public virtual int CreaturesNameCountB { get { return m_CreaturesNameB.Count; } }
		public virtual int CreaturesNameCountC { get { return m_CreaturesNameC.Count; } }
		public virtual int CreaturesNameCountD { get { return m_CreaturesNameD.Count; } }
		public virtual int CreaturesNameCountE { get { return m_CreaturesNameE.Count; } }

		public override void OnAfterDuped( Item newItem )
		{
			PremiumSpawner s = newItem as PremiumSpawner;

			if ( s == null )
				return;

			s.m_CreaturesName = new List<string>( m_CreaturesName );
			s.m_CreaturesNameA = new List<string>( m_CreaturesNameA );
			s.m_CreaturesNameB = new List<string>( m_CreaturesNameB );
			s.m_CreaturesNameC = new List<string>( m_CreaturesNameC );
			s.m_CreaturesNameD = new List<string>( m_CreaturesNameD );
			s.m_CreaturesNameE = new List<string>( m_CreaturesNameE );
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Count
		{
			get { return m_Count; }
			set { m_Count = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int CountA
		{
			get { return m_CountA; }
			set { m_CountA = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int CountB
		{
			get { return m_CountB; }
			set { m_CountB = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int CountC
		{
			get { return m_CountC; }
			set { m_CountC = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int CountD
		{
			get { return m_CountD; }
			set { m_CountD = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int CountE
		{
			get { return m_CountE; }
			set { m_CountE = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public WayPoint WayPoint
		{
			get
			{
				return m_WayPoint;
			}
			set
			{
				m_WayPoint = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Running
		{
			get { return m_Running; }
			set
			{
				if ( value )
					Start();
				else
					Stop();

				InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int HomeRange
		{
			get { return m_HomeRange; }
			set { m_HomeRange = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )] 
		public int WalkingRange 
		{ 
		   get { return m_WalkingRange; } 
		   set { m_WalkingRange = value; InvalidateProperties(); } 
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int SpawnID
		{
			get { return m_SpawnID; }
			set { m_SpawnID = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Team
		{
			get { return m_Team; }
			set { m_Team = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan MinDelay
		{
			get { return m_MinDelay; }
			set { m_MinDelay = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan MaxDelay
		{
			get { return m_MaxDelay; }
			set { m_MaxDelay = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan NextSpawn
		{
			get
			{
				if ( m_Running )
					return m_End - DateTime.UtcNow;
				else
					return TimeSpan.FromSeconds( 0 );
			}
			set
			{
				Start();
				DoTimer( value );
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Group
		{
			get { return m_Group; }
			set { m_Group = value; InvalidateProperties(); }
		}

		[Constructable]
		public PremiumSpawner( int amount, int subamountA, int subamountB, int subamountC, int subamountD, int subamountE, int spawnid, int minDelay, int maxDelay, int team, int homeRange, int walkingRange, string creatureName, string creatureNameA, string creatureNameB, string creatureNameC, string creatureNameD, string creatureNameE ) : base( 0x1f13 )
		{
			List<string> creaturesName = new List<string>();
			creaturesName.Add( creatureName );

			List<string> creatureNameAA = new List<string>();
			creaturesName.Add( creatureNameA );

			List<string> creatureNameBB = new List<string>();
			creaturesName.Add( creatureNameB );

			List<string> creatureNameCC = new List<string>();
			creaturesName.Add( creatureNameC );

			List<string> creatureNameDD = new List<string>();
			creaturesName.Add( creatureNameD );

			List<string> creatureNameEE = new List<string>();
			creaturesName.Add( creatureNameE );

			InitSpawn( amount, subamountA, subamountB, subamountC, subamountD, subamountE, spawnid, TimeSpan.FromMinutes( minDelay ), TimeSpan.FromMinutes( maxDelay ), team, homeRange, walkingRange, creaturesName, creatureNameAA, creatureNameBB, creatureNameCC, creatureNameDD, creatureNameEE );
		}

		[Constructable]
		public PremiumSpawner( string creatureName ) : base( 0x1f13 )
		{
			List<string> creaturesName = new List<string>();
			creaturesName.Add( creatureName );

			List<string> creatureNameAA = new List<string>();
			List<string> creatureNameBB = new List<string>();
			List<string> creatureNameCC = new List<string>();
			List<string> creatureNameDD = new List<string>();
			List<string> creatureNameEE = new List<string>();

			InitSpawn( 1, 0, 0, 0, 0, 0, 1, TimeSpan.FromMinutes( 5 ), TimeSpan.FromMinutes( 10 ), 0, 4, -1, creaturesName, creatureNameAA, creatureNameBB, creatureNameCC, creatureNameDD, creatureNameEE );
		}

		[Constructable]
		public PremiumSpawner() : base( 0x1f13 )
		{
			List<string> creaturesName = new List<string>();

			List<string> creatureNameAA = new List<string>();
			List<string> creatureNameBB = new List<string>();
			List<string> creatureNameCC = new List<string>();
			List<string> creatureNameDD = new List<string>();
			List<string> creatureNameEE = new List<string>();

			InitSpawn( 1, 0, 0, 0, 0, 0, 1, TimeSpan.FromMinutes( 5 ), TimeSpan.FromMinutes( 10 ), 0, 4, -1, creaturesName, creatureNameAA, creatureNameBB, creatureNameCC, creatureNameDD, creatureNameEE );
		}

		public PremiumSpawner( int amount, int subamountA, int subamountB, int subamountC, int subamountD, int subamountE, int spawnid, TimeSpan minDelay, TimeSpan maxDelay, int team, int homeRange, int walkingRange, List<string> creaturesName, List<string> creatureNameAA, List<string> creatureNameBB, List<string> creatureNameCC, List<string> creatureNameDD, List<string> creatureNameEE )
			: base( 0x1f13 )
		{
			InitSpawn( amount, subamountA, subamountB, subamountC, subamountD, subamountE, spawnid, minDelay, maxDelay, team, homeRange, walkingRange, creaturesName, creatureNameAA, creatureNameBB, creatureNameCC, creatureNameDD, creatureNameEE );
		}

		public override string DefaultName
		{
			get { return "PremiumSpawner"; }
		}

		public void InitSpawn( int amount, int subamountA, int subamountB, int subamountC, int subamountD, int subamountE, int SpawnID, TimeSpan minDelay, TimeSpan maxDelay, int team, int homeRange, int walkingRange, List<string> creaturesName, List<string> creatureNameAA, List<string> creatureNameBB, List<string> creatureNameCC, List<string> creatureNameDD, List<string> creatureNameEE )
		{
			Name = "PremiumSpawner";
			m_SpawnID = SpawnID;
			Visible = false;
			Movable = false;
			m_Running = true;
			m_Group = false;
			m_MinDelay = minDelay;
			m_MaxDelay = maxDelay;
			m_Count = amount;
			m_CountA = subamountA;
			m_CountB = subamountB;
			m_CountC = subamountC;
			m_CountD = subamountD;
			m_CountE = subamountE;
			m_Team = team;
			m_HomeRange = homeRange;
			m_WalkingRange = walkingRange;
			m_CreaturesName = creaturesName;
			m_CreaturesNameA = creatureNameAA;
			m_CreaturesNameB = creatureNameBB;
			m_CreaturesNameC = creatureNameCC;
			m_CreaturesNameD = creatureNameDD;
			m_CreaturesNameE = creatureNameEE;
			m_Creatures = new List<IEntity>();
			m_CreaturesA = new List<IEntity>();
			m_CreaturesB = new List<IEntity>();
			m_CreaturesC = new List<IEntity>();
			m_CreaturesD = new List<IEntity>();
			m_CreaturesE = new List<IEntity>();
			DoTimer( TimeSpan.FromSeconds( 1 ) );
		}
			
		public PremiumSpawner( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.AccessLevel < AccessLevel.GameMaster )
				return;

			PremiumSpawnerGump g = new PremiumSpawnerGump( this );
			from.SendGump( g );
		}



		//public override void OnMovement( Mobile m, Point3D oldLocation )
		//{
			/*
			if( m is PlayerMobile && MyServerSettings.EnableDungeonSoundEffects() )
			{
				if ( DateTime.UtcNow >= m_NextSound && Utility.InRange( m.Location, this.Location, 10 ) )
				{
					if ( Utility.RandomDouble() > 0.80 )
					{
						int sound = HiddenChest.DungeonSounds( this );	
						m.PlaySound( sound );	
					}
					m_NextSound = (DateTime.UtcNow + TimeSpan.FromSeconds( 60 ));	
				}
			}
			
				else if ( Server.Misc.Worlds.IsFireDungeon( this.Location, this.Map ) ){			category = "fire"; }
				else if ( Server.Misc.Worlds.IsIceDungeon( this.Location, this.Map ) ){				category = "snow"; }
				else if ( Server.Misc.Worlds.IsSeaDungeon( this.Location, this.Map ) ){				category = "sea"; }
				else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "dirt" ) 
						&& Server.Misc.Worlds.TestMountain ( this.Map, this.X, this.Y, 15 ) ){		category = "mountain"; }
				else if ( Server.Misc.Worlds.TestMountain ( this.Map, this.X, this.Y, 10 ) ){ 		category = "mountain"; }
				else if ( Server.Misc.Worlds.TestOcean ( this.Map, this.X, this.Y, 15 ) ){ 			category = "sea"; }
				else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "snow" ) ){		category = "snow"; }
				else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "cave" ) ){		category = "dungeon"; if ( Utility.RandomBool() ){ category = "mountain"; } }
				else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "swamp" ) ){		category = "swamp"; }
				else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "jungle" ) ){		category = "jungle"; }
				else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "forest" ) ){		category = "forest"; }
				else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "sand" ) ){		category = "sand"; }
				else if ( Server.Misc.Worlds.TestMountain ( this.Map, this.X, this.Y, 15 ) ){ 		category = "mountain"; }
				else if ( reg.IsPartOf( typeof( DungeonRegion ) ) ){								category = "dungeon"; }
			*/
		//}


		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Running )
			{
				list.Add( 1060742 ); // active

				list.Add( 1060656, m_Count.ToString() );
				list.Add( 1061169, m_HomeRange.ToString() );
				list.Add( 1060658, "walking range\t{0}", m_WalkingRange );

				list.Add( 1060663, "SpawnID\t{0}", m_SpawnID.ToString() );

//				list.Add( 1060659, "group\t{0}", m_Group );
//				list.Add( 1060660, "team\t{0}", m_Team );
				list.Add( 1060661, "speed\t{0} to {1}", m_MinDelay, m_MaxDelay );

				for ( int i = 0; i < 2 && i < m_CreaturesName.Count; ++i )
					list.Add( 1060662 + i, "{0}\t{1}", m_CreaturesName[i], CountCreatures( m_CreaturesName[i] ) );
			}
			else
			{
				list.Add( 1060743 ); // inactive
			}
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			if ( m_Running )
				LabelTo( from, "[Running]" );
			else
				LabelTo( from, "[Off]" );
		}

		public void Start()
		{
			if ( !m_Running )
			{
				if ( m_CreaturesName.Count > 0 || m_CreaturesNameA.Count > 0 || m_CreaturesNameB.Count > 0 || m_CreaturesNameC.Count > 0 || m_CreaturesNameD.Count > 0 || m_CreaturesNameE.Count > 0 )
				{
					m_Running = true;
					DoTimer();
				}
			}
		}

		public void Stop()
		{
			if ( m_Running )
			{
				m_Timer.Stop();
				m_Running = false;
			}
		}

		public static string ParseType( string s )
		{
			return s.Split( null, 2 )[0];
		}

		public void Defrag()
		{
			bool removed = false;

			for ( int i = 0; i < m_Creatures.Count; ++i )
			{
				IEntity e = m_Creatures[i];

				if ( e is Item )
				{
					Item item = (Item)e;

					if ( item.Deleted || item.Parent != null )
					{
						m_Creatures.RemoveAt( i );
						--i;
						removed = true;
					}
				}
				else if ( e is Mobile )
				{
					Mobile m = (Mobile)e;

					if ( m.Deleted )
					{
						m_Creatures.RemoveAt( i );
						--i;
						removed = true;
					}
					else if ( m is BaseCreature )
					{
						BaseCreature bc = (BaseCreature)m;
						if ( bc.Controlled || bc.IsStabled )
						{
							m_Creatures.RemoveAt( i );
							--i;
							removed = true;
						}
					}
				}
				else
				{
					m_Creatures.RemoveAt( i );
					--i;
					removed = true;
				}
			}

			for ( int i = 0; i < m_CreaturesA.Count; ++i )
			{
				IEntity e = m_CreaturesA[i];

				if ( e is Item )
				{
					Item item = (Item)e;

					if ( item.Deleted || item.Parent != null )
					{
						m_CreaturesA.RemoveAt( i );
						--i;
						removed = true;
					}
				}
				else if ( e is Mobile )
				{
					Mobile m = (Mobile)e;

					if ( m.Deleted )
					{
						m_CreaturesA.RemoveAt( i );
						--i;
						removed = true;
					}
					else if ( m is BaseCreature )
					{
						BaseCreature bc = (BaseCreature)m;
						if ( bc.Controlled || bc.IsStabled )
						{
							m_CreaturesA.RemoveAt( i );
							--i;
							removed = true;
						}
					}
				}
				else
				{
					m_CreaturesA.RemoveAt( i );
					--i;
					removed = true;
				}
			}

			for ( int i = 0; i < m_CreaturesB.Count; ++i )
			{
				IEntity e = m_CreaturesB[i];

				if ( e is Item )
				{
					Item item = (Item)e;

					if ( item.Deleted || item.Parent != null )
					{
						m_CreaturesB.RemoveAt( i );
						--i;
						removed = true;
					}
				}
				else if ( e is Mobile )
				{
					Mobile m = (Mobile)e;

					if ( m.Deleted )
					{
						m_CreaturesB.RemoveAt( i );
						--i;
						removed = true;
					}
					else if ( m is BaseCreature )
					{
						BaseCreature bc = (BaseCreature)m;
						if ( bc.Controlled || bc.IsStabled )
						{
							m_CreaturesB.RemoveAt( i );
							--i;
							removed = true;
						}
					}
				}
				else
				{
					m_CreaturesB.RemoveAt( i );
					--i;
					removed = true;
				}
			}

			for ( int i = 0; i < m_CreaturesC.Count; ++i )
			{
				IEntity e = m_CreaturesC[i];

				if ( e is Item )
				{
					Item item = (Item)e;

					if ( item.Deleted || item.Parent != null )
					{
						m_CreaturesC.RemoveAt( i );
						--i;
						removed = true;
					}
				}
				else if ( e is Mobile )
				{
					Mobile m = (Mobile)e;

					if ( m.Deleted )
					{
						m_CreaturesC.RemoveAt( i );
						--i;
						removed = true;
					}
					else if ( m is BaseCreature )
					{
						BaseCreature bc = (BaseCreature)m;
						if ( bc.Controlled || bc.IsStabled )
						{
							m_CreaturesC.RemoveAt( i );
							--i;
							removed = true;
						}
					}
				}
				else
				{
					m_CreaturesC.RemoveAt( i );
					--i;
					removed = true;
				}
			}

			for ( int i = 0; i < m_CreaturesD.Count; ++i )
			{
				IEntity e = m_CreaturesD[i];

				if ( e is Item )
				{
					Item item = (Item)e;

					if ( item.Deleted || item.Parent != null )
					{
						m_CreaturesD.RemoveAt( i );
						--i;
						removed = true;
					}
				}
				else if ( e is Mobile )
				{
					Mobile m = (Mobile)e;

					if ( m.Deleted )
					{
						m_CreaturesD.RemoveAt( i );
						--i;
						removed = true;
					}
					else if ( m is BaseCreature )
					{
						BaseCreature bc = (BaseCreature)m;
						if ( bc.Controlled || bc.IsStabled )
						{
							m_CreaturesD.RemoveAt( i );
							--i;
							removed = true;
						}
					}
				}
				else
				{
					m_CreaturesD.RemoveAt( i );
					--i;
					removed = true;
				}
			}

			for ( int i = 0; i < m_CreaturesE.Count; ++i )
			{
				IEntity e = m_CreaturesE[i];

				if ( e is Item )
				{
					Item item = (Item)e;

					if ( item.Deleted || item.Parent != null )
					{
						m_CreaturesE.RemoveAt( i );
						--i;
						removed = true;
					}
				}
				else if ( e is Mobile )
				{
					Mobile m = (Mobile)e;

					if ( m.Deleted )
					{
						m_CreaturesE.RemoveAt( i );
						--i;
						removed = true;
					}
					else if ( m is BaseCreature )
					{
						BaseCreature bc = (BaseCreature)m;
						if ( bc.Controlled || bc.IsStabled )
						{
							m_CreaturesE.RemoveAt( i );
							--i;
							removed = true;
						}
					}
				}
				else
				{
					m_CreaturesE.RemoveAt( i );
					--i;
					removed = true;
				}
			}

			if ( removed )
				InvalidateProperties();
		}

		public void OnTick()
		{

			DoTimer();

			if (World.Saving || this.Map == null )
				return;

			String reg = Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y );
			if (reg != null && AdventuresFunctions.RegionIsInfected( reg ) )
			{
				bool infected = false;

				foreach ( Mobile m in this.GetMobilesInRange( 30 ) )
				{
					if (m is BaseCreature)
					{
						if ( ((BaseCreature)m).CanInfect && !infected)
							infected = true;
					}
				}
				if (infected)
					return;
			}

			if ( m_Group )
			{
				Defrag();

				if  ( m_Creatures.Count == 0 || m_CreaturesA.Count == 0 || m_CreaturesB.Count == 0 || m_CreaturesC.Count == 0 || m_CreaturesD.Count == 0 || m_CreaturesE.Count == 0 )
				{
					Respawn();
				}
				else
				{
					return;
				}
			}
			else
			{
				Spawn();
				SpawnA();
				SpawnB();
				SpawnC();
				SpawnD();
				SpawnE();
			}
		}

		public static void Reconfigure( PremiumSpawner spawner, bool respawn )
		{
			if ( spawner.SpawnID == 8888 )
			{
				spawner.Count = ( int )( spawner.CountE * 0.15 );
					if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ spawner.Count = ( int )( spawner.CountE * 0.20 ); }
					if ( spawner.Count < 1 ){ spawner.Count = 1; }

				spawner.CountA = ( int )( spawner.CountE * 0.12 );
					if ( spawner.CountA < 1 && Utility.RandomMinMax( 1, 5 ) == 1 ){ spawner.CountA = 1; }

				spawner.CountB = ( int )( spawner.CountE * 0.09 );
					if ( spawner.CountB < 1 && Utility.RandomMinMax( 1, 5 ) == 1 ){ spawner.CountB = 1; }

				spawner.CountC = ( int )( spawner.CountE * 0.06 );
					if ( spawner.CountC < 1 && Utility.RandomMinMax( 1, 5 ) == 1 ){ spawner.CountC = 1; }

				spawner.CountD = ( int )( spawner.CountE * 0.03 );
					if ( spawner.CountD < 1 && Utility.RandomMinMax( 1, 5 ) == 1 ){ spawner.CountD = 1; }

				if ( respawn ){ spawner.Respawn(); }
				else
				{
					spawner.RemoveCreatures();
					spawner.RemoveCreaturesA();
					spawner.RemoveCreaturesB();
					spawner.RemoveCreaturesC();
					spawner.RemoveCreaturesD();
					spawner.RemoveCreaturesE();
				}
			}
		}

		public static void SpreadOut( Mobile m )
		{
						if ( m is BaseVendor || m is Adventurers || m is Jedi )
			{
				///////////////// SPREAD WANDERING HEALERS AROUND THE LAND /////////////////
				if ( m.X >= 0 && m.Y >= 0 && m.X <= 6 && m.Y <= 6 && m.Map == Map.Felucca ){ m.Location = Worlds.GetRandomLocation( "the Land of Lodoria", "land" ); m.WhisperHue = 911; }
				else if ( m.X >= 0 && m.Y >= 0 && m.X <= 6 && m.Y <= 6 && m.Map == Map.Trammel ){ m.Location = Worlds.GetRandomLocation( "the Land of Sosaria", "land" ); m.WhisperHue = 911; }
				else if ( m.X >= 0 && m.Y >= 0 && m.X <= 6 && m.Y <= 6 && m.Map == Map.Malas ){ m.Location = Worlds.GetRandomLocation( "the Serpent Island", "land" ); m.WhisperHue = 911; }
				else if ( m.X >= 0 && m.Y >= 0 && m.X <= 6 && m.Y <= 6 && m.Map == Map.Tokuno ){ m.Location = Worlds.GetRandomLocation( "the Isles of Dread", "land" ); m.WhisperHue = 911; }
				else if ( m.X >= 1125 && m.Y >= 298 && m.X <= 1131 && m.Y <= 305 && m.Map == Map.TerMur ){ m.Location = Worlds.GetRandomLocation( "the Savaged Empire", "land" ); m.WhisperHue = 911; }
				else if ( m.X >= 5457 && m.Y >= 3300 && m.X <= 5459 && m.Y <= 3302 && m.Map == Map.Trammel ){ m.Location = Worlds.GetRandomLocation( "the Land of Ambrosia", "land" ); m.WhisperHue = 911; }
				else if ( m.X >= 608 && m.Y >= 4090 && m.X <= 704 && m.Y <= 4096 && m.Map == Map.Trammel ){ m.Location = Worlds.GetRandomLocation( "the Island of Umber Veil", "land" ); m.WhisperHue = 911; }
				else if ( m.X >= 6126 && m.Y >= 827 && m.X <= 6132 && m.Y <= 833 && m.Map == Map.Trammel ){ m.Location = Worlds.GetRandomLocation( "the Bottle World of Kuldar", "land" ); m.WhisperHue = 911; }
				else if ( m.X == 4 && m.Y == 4 && m.Map == Map.Ilshenar ){ m.Location = Worlds.GetRandomLocation( "the Underworld", "land" ); m.WhisperHue = 911; }
			}
			else
			{
				///////////////// SPREAD SEA SPAWNS OVER THE OCEANS AROUND THE LAND /////////////////
				if ( m.X >= 0 && m.Y >= 0 && m.X <= 6 && m.Y <= 6 && m.Map == Map.Felucca ){ m.Location = Worlds.GetRandomLocation( "the Land of Lodoria", "sea" ); m.WhisperHue = 999; }
				else if ( m.X >= 0 && m.Y >= 0 && m.X <= 6 && m.Y <= 6 && m.Map == Map.Trammel ){ m.Location = Worlds.GetRandomLocation( "the Land of Sosaria", "sea" ); m.WhisperHue = 999; }
				else if ( m.X >= 0 && m.Y >= 0 && m.X <= 6 && m.Y <= 6 && m.Map == Map.Malas ){ m.Location = Worlds.GetRandomLocation( "the Serpent Island", "sea" ); m.WhisperHue = 999; }
				else if ( m.X >= 0 && m.Y >= 0 && m.X <= 6 && m.Y <= 6 && m.Map == Map.Tokuno ){ m.Location = Worlds.GetRandomLocation( "the Isles of Dread", "sea" ); m.WhisperHue = 999; }
				else if ( m.X >= 1125 && m.Y >= 298 && m.X <= 1131 && m.Y <= 305 && m.Map == Map.TerMur ){ m.Location = Worlds.GetRandomLocation( "the Savaged Empire", "sea" ); m.WhisperHue = 999; }
				else if ( m.X >= 5457 && m.Y >= 3300 && m.X <= 5459 && m.Y <= 3302 && m.Map == Map.Trammel ){ m.Location = Worlds.GetRandomLocation( "the Land of Ambrosia", "sea" ); m.WhisperHue = 999; }
				else if ( m.X >= 608 && m.Y >= 4090 && m.X <= 704 && m.Y <= 4096 && m.Map == Map.Trammel ){ m.Location = Worlds.GetRandomLocation( "the Island of Umber Veil", "sea" ); m.WhisperHue = 999; }
				else if ( m.X >= 6126 && m.Y >= 827 && m.X <= 6132 && m.Y <= 833 && m.Map == Map.Trammel ){ m.Location = Worlds.GetRandomLocation( "the Bottle World of Kuldar", "sea" ); m.WhisperHue = 999; }
				else if ( m.X == 3 && m.Y == 3 && m.Map == Map.Ilshenar ){ m.Location = Worlds.GetRandomLocation( "the Underworld", "sea" ); m.WhisperHue = 999; }
				else if ( m.X == 4 && m.Y == 4 && m.Map == Map.Ilshenar ){ m.Location = Worlds.GetRandomLocation( "the Underworld", "land" ); m.WhisperHue = 911; }

				if ( m.WhisperHue == 999 && ( m is Balron || m is Daemon || m is Dragons || m is Wyrms ) ){ m.CanSwim = true; }
			}
		}

		public static void SpreadItems( Item i )
		{
			if ( i is LandChest || i is StrangePortal )
			{
				///////////////// SPREAD CORPSES AND PORTALS AROUND THE LAND /////////////////
				if ( i.X >= 0 && i.Y >= 0 && i.X <= 6 && i.Y <= 6 && i.Map == Map.Felucca ){ i.Location = Worlds.GetRandomLocation( "the Land of Lodoria", "land" ); }
				else if ( i.X >= 0 && i.Y >= 0 && i.X <= 6 && i.Y <= 6 && i.Map == Map.Trammel ){ i.Location = Worlds.GetRandomLocation( "the Land of Sosaria", "land" ); }
				else if ( i.X >= 0 && i.Y >= 0 && i.X <= 6 && i.Y <= 6 && i.Map == Map.Malas ){ i.Location = Worlds.GetRandomLocation( "the Serpent Island", "land" ); }
				else if ( i.X >= 0 && i.Y >= 0 && i.X <= 6 && i.Y <= 6 && i.Map == Map.Tokuno ){ i.Location = Worlds.GetRandomLocation( "the Isles of Dread", "land" ); }
				else if ( i.X >= 1125 && i.Y >= 298 && i.X <= 1131 && i.Y <= 305 && i.Map == Map.TerMur ){ i.Location = Worlds.GetRandomLocation( "the Savaged Empire", "land" ); }
				else if ( i.X >= 5457 && i.Y >= 3300 && i.X <= 5459 && i.Y <= 3302 && i.Map == Map.Trammel ){ i.Location = Worlds.GetRandomLocation( "the Land of Ambrosia", "land" ); }
				else if ( i.X >= 608 && i.Y >= 4090 && i.X <= 704 && i.Y <= 4096 && i.Map == Map.Trammel ){ i.Location = Worlds.GetRandomLocation( "the Island of Umber Veil", "land" ); }
				else if ( i.X == 4 && i.Y == 4 && i.Map == Map.Ilshenar ){ i.Location = Worlds.GetRandomLocation( "the Underworld", "land" ); }
			}
			else if ( i is WaterChest )
			{
				///////////////// SPREAD BOATS OVER THE OCEANS /////////////////
				if ( i.X >= 0 && i.Y >= 0 && i.X <= 6 && i.Y <= 6 && i.Map == Map.Felucca ){ i.Location = Worlds.GetRandomLocation( "the Land of Lodoria", "sea" ); }
				else if ( i.X >= 0 && i.Y >= 0 && i.X <= 6 && i.Y <= 6 && i.Map == Map.Trammel ){ i.Location = Worlds.GetRandomLocation( "the Land of Sosaria", "sea" ); }
				else if ( i.X >= 0 && i.Y >= 0 && i.X <= 6 && i.Y <= 6 && i.Map == Map.Malas ){ i.Location = Worlds.GetRandomLocation( "the Serpent Island", "sea" ); }
				else if ( i.X >= 0 && i.Y >= 0 && i.X <= 6 && i.Y <= 6 && i.Map == Map.Tokuno ){ i.Location = Worlds.GetRandomLocation( "the Isles of Dread", "sea" ); }
				else if ( i.X >= 1125 && i.Y >= 298 && i.X <= 1131 && i.Y <= 305 && i.Map == Map.TerMur ){ i.Location = Worlds.GetRandomLocation( "the Savaged Empire", "sea" ); }
				else if ( i.X >= 5457 && i.Y >= 3300 && i.X <= 5459 && i.Y <= 3302 && i.Map == Map.Trammel ){ i.Location = Worlds.GetRandomLocation( "the Land of Ambrosia", "sea" ); }
				else if ( i.X >= 608 && i.Y >= 4090 && i.X <= 704 && i.Y <= 4096 && i.Map == Map.Trammel ){ i.Location = Worlds.GetRandomLocation( "the Island of Umber Veil", "sea" ); }
				else if ( i.X >= 6126 && i.Y >= 827 && i.X <= 6132 && i.Y <= 833 && i.Map == Map.Trammel ){ i.Location = Worlds.GetRandomLocation( "the Bottle World of Kuldar", "sea" ); }
				else if ( i.X == 3 && i.Y == 3 && i.Map == Map.Ilshenar ){ i.Location = Worlds.GetRandomLocation( "the Underworld", "sea" ); }
			}
		}
		
		public void Respawn() // remove all creatures and spawn all again
		{
			RemoveCreatures();
			RemoveCreaturesA();
			RemoveCreaturesB();
			RemoveCreaturesC();
			RemoveCreaturesD();
			RemoveCreaturesE();

			for ( int i = 0; i < m_Count; i++ )
				Spawn();
			for ( int i = 0; i < m_CountA; i++ )
				SpawnA();
			for ( int i = 0; i < m_CountB; i++ )
				SpawnB();
			for ( int i = 0; i < m_CountC; i++ )
				SpawnC();
			for ( int i = 0; i < m_CountD; i++ )
				SpawnD();
			for ( int i = 0; i < m_CountE; i++ )
				SpawnE();
		}
		
		public void Spawn()
		{
			if ( CreaturesNameCount > 0 )
				Spawn( Utility.Random( CreaturesNameCount ) );
		}

		public void SpawnA()
		{
			if ( CreaturesNameCountA > 0 )
				SpawnA( Utility.Random( CreaturesNameCountA ) );
		}

		public void SpawnB()
		{
			if ( CreaturesNameCountB > 0 )
				SpawnB( Utility.Random( CreaturesNameCountB ) );
		}

		public void SpawnC()
		{
			if ( CreaturesNameCountC > 0 )
				SpawnC( Utility.Random( CreaturesNameCountC ) );
		}

		public void SpawnD()
		{
			if ( CreaturesNameCountD > 0 )
				SpawnD( Utility.Random( CreaturesNameCountD ) );
		}

		public void SpawnE()
		{
			if ( CreaturesNameCountE > 0 )
				SpawnE( Utility.Random( CreaturesNameCountE ) );
		}

		public void Spawn( string creatureName )
		{
			for ( int i = 0; i < m_CreaturesName.Count; i++ )
			{
				if ( m_CreaturesName[i] == creatureName )
				{
					Spawn( i );
					break;
				}
			}
		}

		public void SpawnA( string creatureNameA )
		{
			for ( int i = 0; i < m_CreaturesNameA.Count; i++ )
			{
				if ( (string)m_CreaturesNameA[i] == creatureNameA )
				{
					SpawnA( i );
					break;
				}
			}
		}

		public void SpawnB( string creatureNameB )
		{
			for ( int i = 0; i < m_CreaturesNameB.Count; i++ )
			{
				if ( (string)m_CreaturesNameB[i] == creatureNameB )
				{
					SpawnB( i );
					break;
				}
			}
		}

		public void SpawnC( string creatureNameC )
		{
			for ( int i = 0; i < m_CreaturesNameC.Count; i++ )
			{
				if ( (string)m_CreaturesNameC[i] == creatureNameC )
				{
					SpawnC( i );
					break;
				}
			}
		}

		public void SpawnD( string creatureNameD )
		{
			for ( int i = 0; i < m_CreaturesNameD.Count; i++ )
			{
				if ( (string)m_CreaturesNameD[i] == creatureNameD )
				{
					SpawnD( i );
					break;
				}
			}
		}

		public void SpawnE( string creatureNameE )
		{
			for ( int i = 0; i < m_CreaturesNameE.Count; i++ )
			{
				if ( (string)m_CreaturesNameE[i] == creatureNameE )
				{
					SpawnE( i );
					break;
				}
			}
		}

		protected virtual IEntity CreateSpawnedObject( int index )
		{
			if ( index >= m_CreaturesName.Count )
				return null;

			Type type = ScriptCompiler.FindTypeByName( ParseType( m_CreaturesName[index] ) );

			if ( type != null )
			{
				try
				{
					return Build( CommandSystem.Split( m_CreaturesName[index] ) );
				}
				catch
				{
				}
			}

			return null;
		}

		protected virtual IEntity CreateSpawnedObjectA( int index )
		{
			if ( index >= m_CreaturesNameA.Count )
				return null;

			Type type = ScriptCompiler.FindTypeByName( ParseType( m_CreaturesNameA[index] ) );

			if ( type != null )
			{
				try
				{
					return Build( CommandSystem.Split( m_CreaturesNameA[index] ) );
				}
				catch
				{
				}
			}

			return null;
		}

		protected virtual IEntity CreateSpawnedObjectB( int index )
		{
			if ( index >= m_CreaturesNameB.Count )
				return null;

			Type type = ScriptCompiler.FindTypeByName( ParseType( m_CreaturesNameB[index] ) );

			if ( type != null )
			{
				try
				{
					return Build( CommandSystem.Split( m_CreaturesNameB[index] ) );
				}
				catch
				{
				}
			}

			return null;
		}

		protected virtual IEntity CreateSpawnedObjectC( int index )
		{
			if ( index >= m_CreaturesNameC.Count )
				return null;

			Type type = ScriptCompiler.FindTypeByName( ParseType( m_CreaturesNameC[index] ) );

			if ( type != null )
			{
				try
				{
					return Build( CommandSystem.Split( m_CreaturesNameC[index] ) );
				}
				catch
				{
				}
			}

			return null;
		}

		protected virtual IEntity CreateSpawnedObjectD( int index )
		{
			if ( index >= m_CreaturesNameD.Count )
				return null;

			Type type = ScriptCompiler.FindTypeByName( ParseType( m_CreaturesNameD[index] ) );

			if ( type != null )
			{
				try
				{
					return Build( CommandSystem.Split( m_CreaturesNameD[index] ) );
				}
				catch
				{
				}
			}

			return null;
		}

		protected virtual IEntity CreateSpawnedObjectE( int index )
		{
			if ( index >= m_CreaturesNameE.Count )
				return null;

			Type type = ScriptCompiler.FindTypeByName( ParseType( m_CreaturesNameE[index] ) );

			if ( type != null )
			{
				try
				{
					return Build( CommandSystem.Split( m_CreaturesNameE[index] ) );
				}
				catch
				{
				}
			}

			return null;
		}

		public static IEntity Build( string[] args )
		{
			string name = args[0];

			Add.FixArgs( ref args );

			string[,] props = null;

			for ( int i = 0; i < args.Length; ++i )
			{
				if ( Insensitive.Equals( args[i], "set" ) )
				{
					int remains = args.Length - i - 1;

					if ( remains >= 2 )
					{
						props = new string[remains / 2, 2];

						remains /= 2;

						for ( int j = 0; j < remains; ++j )
						{
							props[j, 0] = args[i + (j * 2) + 1];
							props[j, 1] = args[i + (j * 2) + 2];
						}

						Add.FixSetString( ref args, i );
					}

					break;
				}
			}

			Type type = ScriptCompiler.FindTypeByName( name );

			if ( !Add.IsEntity( type ) ) {
				return null;
			}

			PropertyInfo[] realProps = null;

			if ( props != null )
			{
				realProps = new PropertyInfo[props.GetLength( 0 )];

				PropertyInfo[] allProps = type.GetProperties( BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public );

				for ( int i = 0; i < realProps.Length; ++i )
				{
					PropertyInfo thisProp = null;

					string propName = props[i, 0];

					for ( int j = 0; thisProp == null && j < allProps.Length; ++j )
					{
						if ( Insensitive.Equals( propName, allProps[j].Name ) )
							thisProp = allProps[j];
					}

					if ( thisProp != null )
					{
						CPA attr = Properties.GetCPA( thisProp );

						if ( attr != null && AccessLevel.GameMaster >= attr.WriteLevel && thisProp.CanWrite && !attr.ReadOnly )
							realProps[i] = thisProp;
					}
				}
			}

			ConstructorInfo[] ctors = type.GetConstructors();

			for ( int i = 0; i < ctors.Length; ++i )
			{
				ConstructorInfo ctor = ctors[i];

				if ( !Add.IsConstructable( ctor, AccessLevel.GameMaster ) )
					continue;

				ParameterInfo[] paramList = ctor.GetParameters();

				if ( args.Length == paramList.Length )
				{
					object[] paramValues = Add.ParseValues( paramList, args );

					if ( paramValues == null )
						continue;

					object built = ctor.Invoke( paramValues );

					if ( built != null && realProps != null )
					{
						for ( int j = 0; j < realProps.Length; ++j )
						{
							if ( realProps[j] == null )
								continue;

							string result = Properties.InternalSetValue( built, realProps[j], props[j, 1] );
						}
					}

					return (IEntity)built;
				}
			}

			return null;
		}

		public void Spawn( int index )
		{
			Map map = Map;

			if ( map == null || map == Map.Internal || CreaturesNameCount == 0 || index >= CreaturesNameCount || Parent != null )
				return;

			Defrag();

			if ( m_Creatures.Count >= m_Count )
				return;

			int amount = 1;

			if (m_Creatures.Count == (m_Count-1) || m_Count == 1)
			{
				Region reg = Region.Find( this.Location, this.Map );
				if ( (reg.IsPartOf( typeof(BardDungeonRegion) ) || reg.IsPartOf( typeof( DungeonRegion ) ) ) && Utility.RandomDouble() > 0.93 )
				{
					double bleh = Utility.RandomDouble();
					if (m_Count <= 4)
					{
						if (bleh >= 0.85)
							amount += Utility.RandomMinMax(2,4);
						else
							amount += Utility.RandomMinMax(1,3);
					}
					else
					{
							amount += Utility.RandomMinMax(1,2);
					}
				}
			}

			while (amount > 0)
			{
				amount -= 1;

				IEntity ent = CreateSpawnedObject( index );

				if ( ent is Mobile )
				{
					Mobile m = (Mobile)ent;

					if ( ( m is Mangar || m is Exodus || m is Surtaz || m is Vulcrum || m is Arachnar || m is CaddelliteDragon || (m is BaseCreature && ( ((BaseCreature)m).HitsMaxSeed > 1000 || m.Fame > 15000 ) ) ) && amount > 0)
						amount = 0;

					m_Creatures.Add( m );
					
					Point3D loc = ( m is BaseVendor ? this.Location : GetSpawnPosition() );
					
									if ( m is WanderingHealer || m is Adventurers || m is Jedi )
					{
						loc = GetSpawnPosition();
					}
					else if ( m is Syth && SpawnID == 9999 )
					{
						loc = GetSpawnSpotLandDungeon();
					}

					m.OnBeforeSpawn( loc, map );
					InvalidateProperties();

					m.MoveToWorld( loc, map );

					if ( m is BaseCreature )
					{
						BaseCreature c = (BaseCreature)m;
						
						if( m_WalkingRange >= 0 )
							c.RangeHome = m_WalkingRange;
						else
							c.RangeHome = m_HomeRange;

						c.CurrentWayPoint = m_WayPoint;

						if ( m_Team > 0 )
							c.Team = m_Team;

						c.Home = this.Location;

						Server.Misc.MorphingTime.SetGender( m );

						if ( Region.Find( this.Location, this.Map ) != Region.Find( m.Location, m.Map ) ){ m.Delete(); Defrag(); return; } // REMOVE IF NOT IN SAME REGION
						c.SpawnerSpawned = true;
					}

					m.OnAfterSpawn();

					if (m != null && m.Map != null)
					{
						//check for bugged enemies spawning at 0,0,0
						Point3D loccheck = m.Location;
						if (loccheck.X == 0 && loccheck.Y == 0)
						{
							Console.WriteLine( m + " spawned at 0,0 at map " + m.Map + " from spawner at " + this.Location );
						}
					}
				}
				else if ( ent is Item && m_Creatures.Count < m_Count )
				{
					Item item = (Item)ent;

					m_Creatures.Add( item );

					Point3D loc = GetSpawnPosition();

					item.OnBeforeSpawn( loc, map );
					InvalidateProperties();

					item.MoveToWorld( loc, map );

					if ( Region.Find( this.Location, this.Map ) != Region.Find( item.Location, item.Map ) ){ item.Delete(); Defrag(); return; } // REMOVE IF NOT IN SAME REGION

					item.OnAfterSpawn();
				}
			}
		}

		public void SpawnA( int index )
		{
			Map map = Map;

			if ( map == null || map == Map.Internal || CreaturesNameCountA == 0 || index >= CreaturesNameCountA || Parent != null )
				return;

			Defrag();

			if ( m_CreaturesA.Count >= m_CountA )
				return;

			int amount = 1;

			if (m_CreaturesA.Count == (m_CountA-1) || m_CountA == 1)
			{
				Region reg = Region.Find( this.Location, this.Map );
				if ( (reg.IsPartOf( typeof( BardDungeonRegion ) ) || reg.IsPartOf( typeof( DungeonRegion ) ) ) && Utility.RandomDouble() > 0.93 )
				{
					double bleh = Utility.RandomDouble();
					if (m_CountA <= 4)
					{
						if (bleh >= 0.85)
							amount += Utility.RandomMinMax(2,3);
						else
							amount += Utility.RandomMinMax(1,2);
					}
					else
					{
							amount += Utility.RandomMinMax(1,2);
					}
				}
			}

			while (amount > 0)
			{
				amount -= 1;

				IEntity ent = CreateSpawnedObjectA( index );

				if ( ent is Mobile )
				{
					Mobile m = (Mobile)ent;

					if ( ( m is Mangar || m is Exodus || m is Surtaz || m is Vulcrum || m is Arachnar || m is CaddelliteDragon || (m is BaseCreature && ( ((BaseCreature)m).HitsMaxSeed > 1000 || m.Fame > 15000 ) ) ) && amount > 0)
						amount = 0;

					m_CreaturesA.Add( m );
					

					Point3D loc = ( m is BaseVendor ? this.Location : GetSpawnPosition() );
					
					if ( m is WanderingHealer || m is Adventurers || m is Jedi )
					{
						loc = GetSpawnPosition();
					}
					else if ( m is Syth && SpawnID == 9999 )
					{
						loc = GetSpawnSpotLandDungeon();
					}

					m.OnBeforeSpawn( loc, map );
					InvalidateProperties();

					m.MoveToWorld( loc, map );

					if ( m is BaseCreature )
					{
						BaseCreature c = (BaseCreature)m;
						
						if( m_WalkingRange >= 0 )
							c.RangeHome = m_WalkingRange;
						else
							c.RangeHome = m_HomeRange;

						c.CurrentWayPoint = m_WayPoint;

						if ( m_Team > 0 )
							c.Team = m_Team;

						c.Home = this.Location;

						Server.Misc.MorphingTime.SetGender( m );

						if ( Region.Find( this.Location, this.Map ) != Region.Find( m.Location, m.Map ) ){ m.Delete(); Defrag(); return; } // REMOVE IF NOT IN SAME REGION
						c.SpawnerSpawned = true;
					}
					
					m.OnAfterSpawn();

					if (m != null && m.Map != null)
					{
						//check for bugged enemies spawning at 0,0,0
						Point3D loccheck = m.Location;
						if (loccheck.X == 0 && loccheck.Y == 0)
						{
							Console.WriteLine( m + " spawned at 0,0 at map " + m.Map + " from spawner at " + this.Location );
						}
					}
				}
				else if ( ent is Item && m_CreaturesA.Count < m_CountA )
				{
					Item item = (Item)ent;

					m_CreaturesA.Add( item );

					Point3D loc = GetSpawnPosition();

					item.OnBeforeSpawn( loc, map );
					InvalidateProperties();

					item.MoveToWorld( loc, map );

					if ( Region.Find( this.Location, this.Map ) != Region.Find( item.Location, item.Map ) ){ item.Delete(); Defrag(); return; } // REMOVE IF NOT IN SAME REGION

					item.OnAfterSpawn();
				}
			}
		}

		public void SpawnB( int index )
		{
			Map map = Map;

			if ( map == null || map == Map.Internal || CreaturesNameCountB == 0 || index >= CreaturesNameCountB || Parent != null )
				return;

			Defrag();

			if ( m_CreaturesB.Count >= m_CountB )
				return;

			int amount = 1;

			if (m_CreaturesB.Count == (m_CountB-1) || m_CountB == 1)
			{
				Region reg = Region.Find( this.Location, this.Map );
				if ( ( reg.IsPartOf( typeof( BardDungeonRegion ) ) || reg.IsPartOf( typeof( DungeonRegion ) ) ) && Utility.RandomDouble() > 0.93 )
				{
					double bleh = Utility.RandomDouble();
					if (m_CountB <= 4)
					{
						if (bleh >= 0.85)
							amount += Utility.RandomMinMax(2,3);
						else
							amount += Utility.RandomMinMax(1,2);
					}
					else
					{
							amount += Utility.RandomMinMax(1,2);
					}
				}
			}

			while (amount > 0)
			{
				amount -= 1;

				IEntity ent = CreateSpawnedObjectB( index );

				if ( ent is Mobile )
				{
					Mobile m = (Mobile)ent;

					m_CreaturesB.Add( m );
					
					if ( ( m is Mangar || m is Exodus || m is Surtaz || m is Vulcrum || m is Arachnar || m is CaddelliteDragon || (m is BaseCreature && ( ((BaseCreature)m).HitsMaxSeed > 1000 || m.Fame > 15000 ) ) ) && amount > 0)
						amount = 0;

					Point3D loc = ( m is BaseVendor ? this.Location : GetSpawnPosition() );
					
									if ( m is WanderingHealer || m is Adventurers || m is Jedi )
					{
						loc = GetSpawnPosition();
					}
					else if ( m is Syth && SpawnID == 9999 )
					{
						loc = GetSpawnSpotLandDungeon();
					}

					m.OnBeforeSpawn( loc, map );
					InvalidateProperties();

					m.MoveToWorld( loc, map );

					if ( m is BaseCreature )
					{
						BaseCreature c = (BaseCreature)m;
						
						if( m_WalkingRange >= 0 )
							c.RangeHome = m_WalkingRange;
						else
							c.RangeHome = m_HomeRange;

						c.CurrentWayPoint = m_WayPoint;

						if ( m_Team > 0 )
							c.Team = m_Team;

						c.Home = this.Location;

						Server.Misc.MorphingTime.SetGender( m );

						if ( Region.Find( this.Location, this.Map ) != Region.Find( m.Location, m.Map ) ){ m.Delete(); Defrag(); return; } // REMOVE IF NOT IN SAME REGION
						c.SpawnerSpawned = true;
					}

					m.OnAfterSpawn();

					if (m != null && m.Map != null)
					{
						//check for bugged enemies spawning at 0,0,0
						Point3D loccheck = m.Location;
						if (loccheck.X == 0 && loccheck.Y == 0)
						{
							Console.WriteLine( m + " spawned at 0,0 at map " + m.Map + " from spawner at " + this.Location );
						}
					}
				}
				else if ( ent is Item && m_CreaturesB.Count < m_CountB )
				{
					Item item = (Item)ent;

					m_CreaturesB.Add( item );

					Point3D loc = GetSpawnPosition();

					item.OnBeforeSpawn( loc, map );
					InvalidateProperties();

					item.MoveToWorld( loc, map );

					if ( Region.Find( this.Location, this.Map ) != Region.Find( item.Location, item.Map ) ){ item.Delete(); Defrag(); return; } // REMOVE IF NOT IN SAME REGION

					item.OnAfterSpawn();
				}
			}
		}

		public void SpawnC( int index )
		{
			Map map = Map;

			if ( map == null || map == Map.Internal || CreaturesNameCountC == 0 || index >= CreaturesNameCountC || Parent != null )
				return;

			Defrag();

			if ( m_CreaturesC.Count >= m_CountC )
				return;


			IEntity ent = CreateSpawnedObjectC( index );

			if ( ent is Mobile )
			{
				Mobile m = (Mobile)ent;

				m_CreaturesC.Add( m );

				Point3D loc = ( m is BaseVendor ? this.Location : GetSpawnPosition() );
				
				if ( m is WanderingHealer || m is Adventurers || m is Jedi )
				{
					loc = GetSpawnPosition();
				}
				else if ( m is Syth && SpawnID == 9999 )
				{
					loc = GetSpawnSpotLandDungeon();
				}

				m.OnBeforeSpawn( loc, map );
				InvalidateProperties();

				m.MoveToWorld( loc, map );

				if ( m is BaseCreature )
				{
					BaseCreature c = (BaseCreature)m;
					
					if( m_WalkingRange >= 0 )
						c.RangeHome = m_WalkingRange;
					else
						c.RangeHome = m_HomeRange;

					c.CurrentWayPoint = m_WayPoint;

					if ( m_Team > 0 )
						c.Team = m_Team;

					c.Home = this.Location;

					Server.Misc.MorphingTime.SetGender( m );

					if ( Region.Find( this.Location, this.Map ) != Region.Find( m.Location, m.Map ) ){ m.Delete(); Defrag(); return; } // REMOVE IF NOT IN SAME REGION
					c.SpawnerSpawned = true;
				}

				m.OnAfterSpawn();

				//check for bugged enemies spawning at 0,0,0
				if (m != null && m.Map != null)
				{
					Point3D loccheck = m.Location;
					if (loccheck.X == 0 && loccheck.Y == 0)
					{
						Console.WriteLine( m + " spawned at 0,0 at map " + m.Map + " from spawner at " + this.Location );
					}
				}

			}
			else if ( ent is Item )
			{
				Item item = (Item)ent;

				m_CreaturesC.Add( item );

				Point3D loc = GetSpawnPosition();

				item.OnBeforeSpawn( loc, map );
				InvalidateProperties();

				item.MoveToWorld( loc, map );

				if ( Region.Find( this.Location, this.Map ) != Region.Find( item.Location, item.Map ) ){ item.Delete(); Defrag(); return; } // REMOVE IF NOT IN SAME REGION

				item.OnAfterSpawn();
			}
		}

		public void SpawnD( int index )
		{
			Map map = Map;

			if ( map == null || map == Map.Internal || CreaturesNameCountD == 0 || index >= CreaturesNameCountD || Parent != null )
				return;

			Defrag();

			if ( m_CreaturesD.Count >= m_CountD )
				return;


			IEntity ent = CreateSpawnedObjectD( index );

			if ( ent is Mobile )
			{
				Mobile m = (Mobile)ent;

				m_CreaturesD.Add( m );

				Point3D loc = ( m is BaseVendor ? this.Location : GetSpawnPosition() );
				
				if ( m is WanderingHealer || m is Adventurers || m is Jedi )
				{
					loc = GetSpawnPosition();
				}
				else if ( m is Syth && SpawnID == 9999 )
				{
					loc = GetSpawnSpotLandDungeon();
				}

				m.OnBeforeSpawn( loc, map );
				InvalidateProperties();

				m.MoveToWorld( loc, map );

				if ( m is BaseCreature )
				{
					BaseCreature c = (BaseCreature)m;
					
					if( m_WalkingRange >= 0 )
						c.RangeHome = m_WalkingRange;
					else
						c.RangeHome = m_HomeRange;

					c.CurrentWayPoint = m_WayPoint;

					if ( m_Team > 0 )
						c.Team = m_Team;

					c.Home = this.Location;

					Server.Misc.MorphingTime.SetGender( m );

					if ( Region.Find( this.Location, this.Map ) != Region.Find( m.Location, m.Map ) ){ m.Delete(); Defrag(); return; } // REMOVE IF NOT IN SAME REGION
					c.SpawnerSpawned = true;
				}

				m.OnAfterSpawn();
			}
			else if ( ent is Item )
			{
				Item item = (Item)ent;

				m_CreaturesD.Add( item );

				Point3D loc = GetSpawnPosition();

				item.OnBeforeSpawn( loc, map );
				InvalidateProperties();

				item.MoveToWorld( loc, map );

				if ( Region.Find( this.Location, this.Map ) != Region.Find( item.Location, item.Map ) ){ item.Delete(); Defrag(); return; } // REMOVE IF NOT IN SAME REGION

				item.OnAfterSpawn();
			}
		}

		public void SpawnE( int index )
		{
			Map map = Map;

			if ( map == null || map == Map.Internal || CreaturesNameCountE == 0 || index >= CreaturesNameCountE || Parent != null )
				return;

			Defrag();

			if ( m_CreaturesE.Count >= m_CountE )
				return;


			IEntity ent = CreateSpawnedObjectE( index );

			if ( ent is Mobile )
			{
				Mobile m = (Mobile)ent;

				m_CreaturesE.Add( m );
				
				Point3D loc = ( m is BaseVendor ? this.Location : GetSpawnPosition() );
				
				if ( m is WanderingHealer || m is Adventurers || m is Jedi )
				{
					loc = GetSpawnPosition();
				}
				else if ( m is Syth && SpawnID == 9999 )
				{
					loc = GetSpawnSpotLandDungeon();
				}

				m.OnBeforeSpawn( loc, map );
				InvalidateProperties();

				m.MoveToWorld( loc, map );

				if ( m is BaseCreature )
				{
					BaseCreature c = (BaseCreature)m;
					
					if( m_WalkingRange >= 0 )
						c.RangeHome = m_WalkingRange;
					else
						c.RangeHome = m_HomeRange;

					c.CurrentWayPoint = m_WayPoint;

					if ( m_Team > 0 )
						c.Team = m_Team;

					c.Home = this.Location;

					Server.Misc.MorphingTime.SetGender( m );

					if ( Region.Find( this.Location, this.Map ) != Region.Find( m.Location, m.Map ) ){ m.Delete(); Defrag(); return; } // REMOVE IF NOT IN SAME REGION
					c.SpawnerSpawned = true;
				}

				m.OnAfterSpawn();
			}
			else if ( ent is Item )
			{
				Item item = (Item)ent;

				m_CreaturesE.Add( item );

				Point3D loc = GetSpawnPosition();

				item.OnBeforeSpawn( loc, map );
				InvalidateProperties();

				item.MoveToWorld( loc, map );

				if ( Region.Find( this.Location, this.Map ) != Region.Find( item.Location, item.Map ) ){ item.Delete(); Defrag(); return; } // REMOVE IF NOT IN SAME REGION

				item.OnAfterSpawn();
			}
		}

		public Point3D GetSpawnPosition()
		{
			Map map = Map;

			if ( map == null )
				return Location;

			// Try 10 times to find a Spawnable location.
			for ( int i = 0; i < 10; i++ )
			{
				int x, y;

				if ( m_HomeRange > 0 ) {
					x = Location.X + (Utility.Random( (m_HomeRange * 2) + 1 ) - m_HomeRange);
					y = Location.Y + (Utility.Random( (m_HomeRange * 2) + 1 ) - m_HomeRange);
				} else {
					x = Location.X;
					y = Location.Y;
				}

				int z = Map.GetAverageZ( x, y );

				Region regSpawner = Region.Find( this.Location, this.Map );
				Region regSpawn1 = Region.Find( new Point3D(x, y, this.Z), this.Map );
				Region regSpawn2 = Region.Find( new Point3D(x, y, z), this.Map );

				if ( Map.CanSpawnMobile( new Point2D( x, y ), this.Z ) && regSpawner == regSpawn1 )
					return new Point3D( x, y, this.Z );
				else if ( Map.CanSpawnMobile( new Point2D( x, y ), z ) && regSpawner == regSpawn2 )
					return new Point3D( x, y, z );
			}

			return this.Location;
		}

        public Point3D GetSpawnSpotLandDungeon()
        {
			if ( Utility.RandomBool() )
				return Server.Misc.Worlds.GetRandomDungeonSpot( Map );

			return Server.Misc.Worlds.GetRandomLocation( Server.Misc.Worlds.GetMyWorld( Map, Location, X, Y ), "land" );
		}

		public void DoTimer()
		{
			if ( !m_Running )
				return;

			int minSeconds = (int)m_MinDelay.TotalSeconds;
			int maxSeconds = (int)m_MaxDelay.TotalSeconds;

			TimeSpan delay = TimeSpan.FromSeconds( Utility.RandomMinMax( minSeconds, maxSeconds ) );
			DoTimer( delay );
		}

		public void DoTimer( TimeSpan delay )
		{
			if ( !m_Running )
				return;

			m_End = DateTime.UtcNow + delay;

			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = new InternalTimer( this, delay );
			m_Timer.Start();
		}

		private class InternalTimer : Timer
		{
			private PremiumSpawner m_PremiumSpawner;

			public InternalTimer( PremiumSpawner spawner, TimeSpan delay ) : base( delay )
			{
				if ( spawner.IsFull || spawner.IsFulla || spawner.IsFullb || spawner.IsFullc || spawner.IsFulld || spawner.IsFulle )
					Priority = TimerPriority.FiveSeconds;
				else
					Priority = TimerPriority.OneSecond;

				m_PremiumSpawner = spawner;
			}

			protected override void OnTick()
			{
				if ( m_PremiumSpawner != null )
					if ( !m_PremiumSpawner.Deleted )
						m_PremiumSpawner.OnTick();
			}
		}

		public int CountCreatures( string creatureName )
		{
			Defrag();

			int count = 0;

			for ( int i = 0; i < m_Creatures.Count; ++i )
				if ( Insensitive.Equals( creatureName, m_Creatures[i].GetType().Name ) )
					++count;

			return count;
		}

		public int CountCreaturesA( string creatureNameA )
		{
			Defrag();

			int count = 0;

			for ( int i = 0; i < m_CreaturesA.Count; ++i )
				if ( Insensitive.Equals( creatureNameA, m_CreaturesA[i].GetType().Name ) )
					++count;

			return count;
		}

		public int CountCreaturesB( string creatureNameB )
		{
			Defrag();

			int count = 0;

			for ( int i = 0; i < m_CreaturesB.Count; ++i )
				if ( Insensitive.Equals( creatureNameB, m_CreaturesB[i].GetType().Name ) )
					++count;

			return count;
		}

		public int CountCreaturesC( string creatureNameC )
		{
			Defrag();

			int count = 0;

			for ( int i = 0; i < m_CreaturesC.Count; ++i )
				if ( Insensitive.Equals( creatureNameC, m_CreaturesC[i].GetType().Name ) )
					++count;

			return count;
		}

		public int CountCreaturesD( string creatureNameD )
		{
			Defrag();

			int count = 0;

			for ( int i = 0; i < m_CreaturesD.Count; ++i )
				if ( Insensitive.Equals( creatureNameD, m_CreaturesD[i].GetType().Name ) )
					++count;

			return count;
		}

		public int CountCreaturesE( string creatureNameE )
		{
			Defrag();

			int count = 0;

			for ( int i = 0; i < m_CreaturesE.Count; ++i )
				if ( Insensitive.Equals( creatureNameE, m_CreaturesE[i].GetType().Name ) )
					++count;

			return count;
		}

		public void RemoveCreatures( string creatureName )
		{
			Defrag();

			for ( int i = 0; i < m_Creatures.Count; ++i )
			{
				IEntity e = m_Creatures[i];

				if ( Insensitive.Equals( creatureName, e.GetType().Name ) )
						e.Delete();
			}

			InvalidateProperties();
		}

		public void RemoveCreaturesA( string creatureNameA )
		{
			Defrag();

			for ( int i = 0; i < m_CreaturesA.Count; ++i )
			{
				IEntity e = m_CreaturesA[i];

				if ( Insensitive.Equals( creatureNameA, e.GetType().Name ) )
						e.Delete();
			}

			InvalidateProperties();
		}

		public void RemoveCreaturesB( string creatureNameB )
		{
			Defrag();

			for ( int i = 0; i < m_CreaturesB.Count; ++i )
			{
				IEntity e = m_CreaturesB[i];

				if ( Insensitive.Equals( creatureNameB, e.GetType().Name ) )
						e.Delete();
			}

			InvalidateProperties();
		}

		public void RemoveCreaturesC( string creatureNameC )
		{
			Defrag();

			for ( int i = 0; i < m_CreaturesC.Count; ++i )
			{
				IEntity e = m_CreaturesC[i];

				if ( Insensitive.Equals( creatureNameC, e.GetType().Name ) )
						e.Delete();
			}

			InvalidateProperties();
		}

		public void RemoveCreaturesD( string creatureNameD )
		{
			Defrag();

			for ( int i = 0; i < m_CreaturesD.Count; ++i )
			{
				IEntity e = m_CreaturesD[i];

				if ( Insensitive.Equals( creatureNameD, e.GetType().Name ) )
						e.Delete();
			}

			InvalidateProperties();
		}

		public void RemoveCreaturesE( string creatureNameE )
		{
			Defrag();

			for ( int i = 0; i < m_CreaturesE.Count; ++i )
			{
				IEntity e = m_CreaturesE[i];

				if ( Insensitive.Equals( creatureNameE, e.GetType().Name ) )
						e.Delete();
			}

			InvalidateProperties();
		}
		
		public void RemoveCreatures()
		{
			Defrag();

			for ( int i = 0; i < m_Creatures.Count; ++i )
				m_Creatures[i].Delete();

			InvalidateProperties();
		}

		public void RemoveCreaturesA()
		{
			Defrag();

			for ( int i = 0; i < m_CreaturesA.Count; ++i )
				m_CreaturesA[i].Delete();

			InvalidateProperties();
		}

		public void RemoveCreaturesB()
		{
			Defrag();

			for ( int i = 0; i < m_CreaturesB.Count; ++i )
				m_CreaturesB[i].Delete();

			InvalidateProperties();
		}

		public void RemoveCreaturesC()
		{
			Defrag();

			for ( int i = 0; i < m_CreaturesC.Count; ++i )
				m_CreaturesC[i].Delete();

			InvalidateProperties();
		}

		public void RemoveCreaturesD()
		{
			Defrag();

			for ( int i = 0; i < m_CreaturesD.Count; ++i )
				m_CreaturesD[i].Delete();

			InvalidateProperties();
		}

		public void RemoveCreaturesE()
		{
			Defrag();

			for ( int i = 0; i < m_CreaturesE.Count; ++i )
				m_CreaturesE[i].Delete();

			InvalidateProperties();
		}

		public void BringToHome()
		{
			Defrag();

			for ( int i = 0; i < m_Creatures.Count; ++i )
			{
				IEntity e = m_Creatures[i];

				if ( e is Mobile )
				{
					Mobile m = (Mobile)e;

					m.MoveToWorld( Location, Map );
				}
				else if ( e is Item )
				{
					Item item = (Item)e;

					item.MoveToWorld( Location, Map );
				}
			}

			for ( int i = 0; i < m_CreaturesA.Count; ++i )
			{
				object o = m_CreaturesA[i];

				if ( o is Mobile )
				{
					Mobile m = (Mobile)o;

					m.MoveToWorld( Location, Map );
				}
				else if ( o is Item )
				{
					Item item = (Item)o;

					item.MoveToWorld( Location, Map );
				}
			}

			for ( int i = 0; i < m_CreaturesB.Count; ++i )
			{
				object o = m_CreaturesB[i];

				if ( o is Mobile )
				{
					Mobile m = (Mobile)o;

					m.MoveToWorld( Location, Map );
				}
				else if ( o is Item )
				{
					Item item = (Item)o;

					item.MoveToWorld( Location, Map );
				}
			}

			for ( int i = 0; i < m_CreaturesC.Count; ++i )
			{
				object o = m_CreaturesC[i];

				if ( o is Mobile )
				{
					Mobile m = (Mobile)o;

					m.MoveToWorld( Location, Map );
				}
				else if ( o is Item )
				{
					Item item = (Item)o;

					item.MoveToWorld( Location, Map );
				}
			}

			for ( int i = 0; i < m_CreaturesD.Count; ++i )
			{
				object o = m_CreaturesD[i];

				if ( o is Mobile )
				{
					Mobile m = (Mobile)o;

					m.MoveToWorld( Location, Map );
				}
				else if ( o is Item )
				{
					Item item = (Item)o;

					item.MoveToWorld( Location, Map );
				}
			}

			for ( int i = 0; i < m_CreaturesE.Count; ++i )
			{
				object o = m_CreaturesE[i];

				if ( o is Mobile )
				{
					Mobile m = (Mobile)o;

					m.MoveToWorld( Location, Map );
				}
				else if ( o is Item )
				{
					Item item = (Item)o;

					item.MoveToWorld( Location, Map );
				}
			}

		}

		public override void OnDelete()
		{
			base.OnDelete();

			RemoveCreatures();
			RemoveCreaturesA();
			RemoveCreaturesB();
			RemoveCreaturesC();
			RemoveCreaturesD();
			RemoveCreaturesE();
			if ( m_Timer != null )
				m_Timer.Stop();
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 4 ); // version
			writer.Write( m_WalkingRange );

			writer.Write( m_SpawnID );
			writer.Write( m_CountA );
			writer.Write( m_CountB );
			writer.Write( m_CountC );
			writer.Write( m_CountD );
			writer.Write( m_CountE );

			writer.Write( m_WayPoint );

			writer.Write( m_Group );

			writer.Write( m_MinDelay );
			writer.Write( m_MaxDelay );
			writer.Write( m_Count );
			writer.Write( m_Team );
			writer.Write( m_HomeRange );
			writer.Write( m_Running );
			
			if ( m_Running )
				writer.WriteDeltaTime( m_End );

			writer.Write( m_CreaturesName.Count );

			for ( int i = 0; i < m_CreaturesName.Count; ++i )
				writer.Write( m_CreaturesName[i] );

			writer.Write( m_CreaturesNameA.Count );

			for ( int i = 0; i < m_CreaturesNameA.Count; ++i )
				writer.Write( (string)m_CreaturesNameA[i] );

			writer.Write( m_CreaturesNameB.Count );

			for ( int i = 0; i < m_CreaturesNameB.Count; ++i )
				writer.Write( (string)m_CreaturesNameB[i] );

			writer.Write( m_CreaturesNameC.Count );

			for ( int i = 0; i < m_CreaturesNameC.Count; ++i )
				writer.Write( (string)m_CreaturesNameC[i] );

			writer.Write( m_CreaturesNameD.Count );

			for ( int i = 0; i < m_CreaturesNameD.Count; ++i )
				writer.Write( (string)m_CreaturesNameD[i] );

			writer.Write( m_CreaturesNameE.Count );

			for ( int i = 0; i < m_CreaturesNameE.Count; ++i )
				writer.Write( (string)m_CreaturesNameE[i] );

			writer.Write( m_Creatures.Count );

			for ( int i = 0; i < m_Creatures.Count; ++i )
			{
				IEntity e = m_Creatures[i];

				if ( e is Item )
					writer.Write( (Item)e );
				else if ( e is Mobile )
					writer.Write( (Mobile)e );
				else
					writer.Write( Serial.MinusOne );
			}

			writer.Write( m_CreaturesA.Count );

			for ( int i = 0; i < m_CreaturesA.Count; ++i )
			{
				IEntity e = m_CreaturesA[i];

				if ( e is Item )
					writer.Write( (Item)e );
				else if ( e is Mobile )
					writer.Write( (Mobile)e );
				else
					writer.Write( Serial.MinusOne );
			}

			writer.Write( m_CreaturesB.Count );

			for ( int i = 0; i < m_CreaturesB.Count; ++i )
			{
				IEntity e = m_CreaturesB[i];

				if ( e is Item )
					writer.Write( (Item)e );
				else if ( e is Mobile )
					writer.Write( (Mobile)e );
				else
					writer.Write( Serial.MinusOne );
			}

			writer.Write( m_CreaturesC.Count );

			for ( int i = 0; i < m_CreaturesC.Count; ++i )
			{
				IEntity e = m_CreaturesC[i];

				if ( e is Item )
					writer.Write( (Item)e );
				else if ( e is Mobile )
					writer.Write( (Mobile)e );
				else
					writer.Write( Serial.MinusOne );
			}

			writer.Write( m_CreaturesD.Count );

			for ( int i = 0; i < m_CreaturesD.Count; ++i )
			{
				IEntity e = m_CreaturesD[i];

				if ( e is Item )
					writer.Write( (Item)e );
				else if ( e is Mobile )
					writer.Write( (Mobile)e );
				else
					writer.Write( Serial.MinusOne );
			}

			writer.Write( m_CreaturesE.Count );

			for ( int i = 0; i < m_CreaturesE.Count; ++i )
			{
				IEntity e = m_CreaturesE[i];

				if ( e is Item )
					writer.Write( (Item)e );
				else if ( e is Mobile )
					writer.Write( (Mobile)e );
				else
					writer.Write( Serial.MinusOne );
			}

		}

		private static WarnTimer m_WarnTimer;

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 4:
				{
					m_WalkingRange = reader.ReadInt();
					m_SpawnID = reader.ReadInt();
					m_CountA = reader.ReadInt();
					m_CountB = reader.ReadInt();
					m_CountC = reader.ReadInt();
					m_CountD = reader.ReadInt();
					m_CountE = reader.ReadInt();

					goto case 3;
				}
				case 3:
				case 2:
				{
					m_WayPoint = reader.ReadItem() as WayPoint;

					goto case 1;
				}

				case 1:
				{
					m_Group = reader.ReadBool();
					
					goto case 0;
				}

				case 0:
				{
					m_MinDelay = reader.ReadTimeSpan();
					m_MaxDelay = reader.ReadTimeSpan();
					m_Count = reader.ReadInt();
					m_Team = reader.ReadInt();
					m_HomeRange = reader.ReadInt();
					m_Running = reader.ReadBool();

					TimeSpan ts = TimeSpan.Zero;

					if ( m_Running )
						ts = reader.ReadDeltaTime() - DateTime.UtcNow;
					
					int size = reader.ReadInt();
					m_CreaturesName = new List<string>( size );
					for ( int i = 0; i < size; ++i )
					{
						string creatureString = reader.ReadString();

						m_CreaturesName.Add( creatureString );
						string typeName = ParseType( creatureString );

						if ( ScriptCompiler.FindTypeByName( typeName ) == null )
						{
							if ( m_WarnTimer == null )
								m_WarnTimer = new WarnTimer();

							m_WarnTimer.Add( Location, Map, typeName );
						}
					}

					int sizeA = reader.ReadInt();
					m_CreaturesNameA = new List<string>( sizeA );
					for ( int i = 0; i < sizeA; ++i )
					{
						string creatureString = reader.ReadString();

						m_CreaturesNameA.Add( creatureString );
						string typeName = ParseType( creatureString );

						if ( ScriptCompiler.FindTypeByName( typeName ) == null )
						{
							if ( m_WarnTimer == null )
								m_WarnTimer = new WarnTimer();

							m_WarnTimer.Add( Location, Map, typeName );
						}
					}

					int sizeB = reader.ReadInt();
					m_CreaturesNameB = new List<string>( sizeB );
					for ( int i = 0; i < sizeB; ++i )
					{
						string creatureString = reader.ReadString();

						m_CreaturesNameB.Add( creatureString );
						string typeName = ParseType( creatureString );

						if ( ScriptCompiler.FindTypeByName( typeName ) == null )
						{
							if ( m_WarnTimer == null )
								m_WarnTimer = new WarnTimer();

							m_WarnTimer.Add( Location, Map, typeName );
						}
					}

					int sizeC = reader.ReadInt();
					m_CreaturesNameC = new List<string>( sizeC );
					for ( int i = 0; i < sizeC; ++i )
					{
						string creatureString = reader.ReadString();

						m_CreaturesNameC.Add( creatureString );
						string typeName = ParseType( creatureString );

						if ( ScriptCompiler.FindTypeByName( typeName ) == null )
						{
							if ( m_WarnTimer == null )
								m_WarnTimer = new WarnTimer();

							m_WarnTimer.Add( Location, Map, typeName );
						}
					}

					int sizeD = reader.ReadInt();
					m_CreaturesNameD = new List<string>( sizeD );
					for ( int i = 0; i < sizeD; ++i )
					{
						string creatureString = reader.ReadString();

						m_CreaturesNameD.Add( creatureString );
						string typeName = ParseType( creatureString );

						if ( ScriptCompiler.FindTypeByName( typeName ) == null )
						{
							if ( m_WarnTimer == null )
								m_WarnTimer = new WarnTimer();

							m_WarnTimer.Add( Location, Map, typeName );
						}
					}

					int sizeE = reader.ReadInt();
					m_CreaturesNameE = new List<string>( sizeE );
					for ( int i = 0; i < sizeE; ++i )
					{
						string creatureString = reader.ReadString();

						m_CreaturesNameE.Add( creatureString );
						string typeName = ParseType( creatureString );

						if ( ScriptCompiler.FindTypeByName( typeName ) == null )
						{
							if ( m_WarnTimer == null )
								m_WarnTimer = new WarnTimer();

							m_WarnTimer.Add( Location, Map, typeName );
						}
					}

					int count = reader.ReadInt();
					m_Creatures = new List<IEntity>( count );
					for ( int i = 0; i < count; ++i )
					{
						IEntity e = World.FindEntity( reader.ReadInt() );

						if ( e != null )
							m_Creatures.Add( e );
					}

					int countA = reader.ReadInt();
					m_CreaturesA = new List<IEntity>( countA );
					for ( int i = 0; i < countA; ++i )
					{
						IEntity e = World.FindEntity( reader.ReadInt() );

						if ( e != null )
							m_CreaturesA.Add( e );
					}

					int countB = reader.ReadInt();
					m_CreaturesB = new List<IEntity>( countB );
					for ( int i = 0; i < countB; ++i )
					{
						IEntity e = World.FindEntity( reader.ReadInt() );

						if ( e != null )
							m_CreaturesB.Add( e );
					}

					int countC = reader.ReadInt();
					m_CreaturesC = new List<IEntity>( countC );
					for ( int i = 0; i < countC; ++i )
					{
						IEntity e = World.FindEntity( reader.ReadInt() );

						if ( e != null )
							m_CreaturesC.Add( e );
					}

					int countD = reader.ReadInt();
					m_CreaturesD = new List<IEntity>( countD );
					for ( int i = 0; i < countD; ++i )
					{
						IEntity e = World.FindEntity( reader.ReadInt() );

						if ( e != null )
							m_CreaturesD.Add( e );
					}

					int countE = reader.ReadInt();
					m_CreaturesE = new List<IEntity>( countE );
					for ( int i = 0; i < countE; ++i )
					{
						IEntity e = World.FindEntity( reader.ReadInt() );

						if ( e != null )
							m_CreaturesE.Add( e );
					}

					if ( m_Running )
						DoTimer( ts );

					break;
				}
			}

			if ( version < 3 && Weight == 0 )
				Weight = -1;
		}

		private class WarnTimer : Timer
		{
			private List<WarnEntry> m_List;

			private class WarnEntry
			{
				public Point3D m_Point;
				public Map m_Map;
				public string m_Name;

				public WarnEntry( Point3D p, Map map, string name )
				{
					m_Point = p;
					m_Map = map;
					m_Name = name;
				}
			}

			public WarnTimer() : base( TimeSpan.FromSeconds( 1.0 ) )
			{
				m_List = new List<WarnEntry>();
				Start();
			}

			public void Add( Point3D p, Map map, string name )
			{
				m_List.Add( new WarnEntry( p, map, name ) );
			}

			protected override void OnTick()
			{
				try
				{
					Console.WriteLine( "Warning: {0} bad spawns detected, logged: 'PremiumBadspawn.log'", m_List.Count );

					using ( StreamWriter op = new StreamWriter( "PremiumBadspawn.log", true ) )
					{
						op.WriteLine( "# Bad spawns : {0}", DateTime.UtcNow );
						op.WriteLine( "# Format: X Y Z F Name" );
						op.WriteLine();

						foreach ( WarnEntry e in m_List )
							op.WriteLine( "{0}\t{1}\t{2}\t{3}\t{4}", e.m_Point.X, e.m_Point.Y, e.m_Point.Z, e.m_Map, e.m_Name );

						op.WriteLine();
						op.WriteLine();
					}
				}
				catch
				{
				}
			}
		}
	}
}
