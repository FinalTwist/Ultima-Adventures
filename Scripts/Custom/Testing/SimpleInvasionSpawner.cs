/* Scripts/Custom/Engines/InvasionSpawner.cs
 * ChangeLog:
 * 
 * 8/22/10, Packer898
 *      -Removed simple Enabled bool which came from ComplexInvasionSpawner.cs it was 
 *      leftover code from more advanced version of this spawner. 
 * 8/21/10, Packer898
 *      -Added correct Enabled version to start Spawn(), ToggleGuards(), or Despawn().
 * 8/20/10, Packer898
 *      -Revised for SVN compliance.
 *      -Removed Utility.RandomChance (custom method)
 * 8/15/08, Packer898
 *      -Initial Creation
 */
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Regions;
using Server.Mobiles;

namespace Server.Engines.InvasionSpawner
{
    public enum eStage1Type
    {
        HeadlessOnes,
        Mongbats,
        SewerRats,
        Slimes,
        Snakes
    }

    public enum eStage2Type
    {
        None,
        Brigands,
        EvilMages,
        Lizardmen,
        Orc,
        Skeletons,
        Trolls,
        Zombies
    }

    public enum eStage3Type
    {
        None,
        BoneMagi,
        Cyclops,
        EvilMageLords,
        Gargoyles,
        Ogres,
        OrcishMages
    }

    public enum eStage4Type
    {
        None,
        AncientLichs,
        Balrons,
        Daemons,
        Dragons,
        Liches,
        Titans
    }

    public enum eChampionType
    {
        None,
        Barracoon,
        Harrower,
        LordOaks,
        Mephitis,
        Neira,
        Rikktor,
        Semidar,
        Serado
    }

    public class InvasionSpawner : Item
    {
        //Adjust for monster spawn count
        private int m_Stage1Count = 100;
        private int m_Stage2Count = 70;
        private int m_Stage3Count = 20;
        private int m_Stage4Count = 10;
        //End Adjustment

        private InvasionSpawner m_InvasionSpawner;
        private eStage1Type m_Stage1Type;
        private eStage2Type m_Stage2Type;
        private eStage3Type m_Stage3Type;
        private eStage4Type m_Stage4Type;
        private eChampionType m_ChampionType;

        private int m_CurrentStage;
        private int m_ArtifactChance;
        private int m_BlessDeedChance;
        private int m_MinSpawnZ;
        private int m_MaxSpawnZ;

        private bool m_Enabled;
        private bool m_Broadcast;
        private bool m_RewardsEnabled;

        private string m_RegionName;
        private Point3D m_Top;
        private Point3D m_Bottom;
        private WayPoint m_Waypoint;
        private List<BaseCreature> m_Spawned;
        private Timer m_Timer;

        #region Public Properties
        [CommandProperty( AccessLevel.GameMaster )]
        public int Stage1Count
        {
            get { return m_Stage1Count; }
            set { m_Stage1Count = value; InvalidateProperties(); }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int Stage2Count
        {
            get { return m_Stage2Count; }
            set { m_Stage2Count = value; InvalidateProperties(); }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int Stage3Count
        {
            get { return m_Stage3Count; }
            set { m_Stage3Count = value; InvalidateProperties(); }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int Stage4Count
        {
            get { return m_Stage4Count; }
            set { m_Stage4Count = value; InvalidateProperties(); }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public eStage1Type Stage1Monster
        {
            get { return m_Stage1Type; }
            set
            {
                if( ( value >= eStage1Type.HeadlessOnes ) && ( value <= eStage1Type.Snakes ) )
                {
                    m_Stage1Type = value;
                }
                else
                {
                    return;
                }
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public eStage2Type Stage2Monster
        {
            get { return m_Stage2Type; }
            set
            {
                if( ( value >= eStage2Type.None ) && ( value <= eStage2Type.Zombies ) )
                {
                    m_Stage2Type = value;
                }
                else
                {
                    return;
                }
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public eStage3Type Stage3Monster
        {
            get { return m_Stage3Type; }
            set
            {
                if( ( value >= eStage3Type.None ) && ( value <= eStage3Type.OrcishMages ) )
                {
                    m_Stage3Type = value;
                }
                else
                {
                    return;
                }
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public eStage4Type Stage4Monster
        {
            get { return m_Stage4Type; }
            set
            {
                if( ( value >= eStage4Type.None ) && ( value <= eStage4Type.Titans ) )
                {
                    m_Stage4Type = value;
                }
                else
                {
                    return;
                }
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public eChampionType Champion
        {
            get { return m_ChampionType; }
            set
            {
                if( ( value >= eChampionType.None ) && ( value <= eChampionType.Serado ) )
                {
                    m_ChampionType = value;
                }
                else
                {
                    return;
                }
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public bool Enabled
        {
            get { return m_Enabled; }
            set
            {
                m_Enabled = value;

                if( m_Enabled )
                    Spawn();
                else
                {
                    Despawn( true );
                    ToggleGuards( true );
                }
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int ArtifactChance { get { return m_ArtifactChance; } set { m_ArtifactChance = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public int BlessDeedChance { get { return m_BlessDeedChance; } set { m_BlessDeedChance = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public int MinSpawnZ { get { return m_MinSpawnZ; } set { m_MinSpawnZ = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public int MaxSpawnZ { get { return m_MaxSpawnZ; } set { m_MaxSpawnZ = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public bool Broadcast { get { return m_Broadcast; } set { m_Broadcast = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public bool RewardsEnabled { get { return m_RewardsEnabled; } set { m_RewardsEnabled = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public Point3D CoordsTop { get { return m_Top; } set { m_Top = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public Point3D CoordsBottom { get { return m_Bottom; } set { m_Bottom = value; } }

        [CommandProperty( AccessLevel.Administrator )]
        public WayPoint Waypoint { get { return m_Waypoint; } set { m_Waypoint = value; } }

        public List<BaseCreature> Spawned { get { return m_Spawned; } set { m_Spawned = value; } }

        #endregion

        #region Reward Artifact Types
        private Type[] Artifacts = new Type[]
		{
			typeof( AxeOfTheHeavens ), typeof( BladeOfInsanity ), typeof( BladeOfTheRighteous ),
			typeof( BlazeOfDeath ), typeof( BoneCrusher ), typeof( CavortingClub ),
            typeof( ColdBlood ), typeof( EnchantedTitanLegBone ), typeof( Frostbringer ), typeof( LegacyOfTheDreadLord ),
            typeof( NightsKiss ), typeof( NoxRangersHeavyCrossbow ), typeof( SerpentsFang ),
			typeof( StaffOfPower ), typeof( StaffOfTheMagi ), typeof( TheBeserkersMaul ),
            typeof( TheTaskmaster ), typeof( TitansHammer ), typeof( WrathOfTheDryad ), typeof( ZyronicClaw )
		};
        #endregion

        [Constructable]
        public InvasionSpawner()
            : base( 0x1f13 )
        {
            Name = "a town invasion spawner";
            m_Spawned = new List<BaseCreature>();

            Visible = false;
            Movable = false;
            m_Enabled = false;

            m_ArtifactChance = 50;//Adjust for % to drop artifact from above artifact array
            m_BlessDeedChance = 30;//Adjust for % chance to drop cursed CBD - can remove if unwanted
            m_Broadcast = false;//m_Broadcast = true; will announce shard when the event begins and ends
            m_RewardsEnabled = true;//m_RewardsEnabled = false; will disable both the Artifact and CBD drops
        }

        public override void OnDoubleClick( Mobile from )
        {
            if( from is PlayerMobile )
            {
                PlayerMobile pm = (PlayerMobile)from;

                if( pm.AccessLevel > AccessLevel.Player )
                    pm.SendGump( new PropertiesGump( from, this ) );
            }
        }

        public void Spawn()
        {
            switch( m_CurrentStage + 1 )//CurrentStage starts at 0, added one for readability and to match stages...
            {
                case 1:
                {
                    ToggleGuards( false );

                    switch( m_Stage1Type )
                    {
                        case eStage1Type.HeadlessOnes: AddMonster( typeof(HeadlessOne), false, m_Stage1Count ); break;
                        case eStage1Type.Mongbats: AddMonster( typeof( Mongbat ), false, m_Stage1Count ); break;
                        case eStage1Type.SewerRats: AddMonster( typeof(Sewerrat), false, m_Stage1Count ); break;
                        case eStage1Type.Slimes: AddMonster( typeof(Slime), false, m_Stage1Count ); break;
                        case eStage1Type.Snakes: AddMonster( typeof(Snake), false, m_Stage1Count ); break;
                    }
                    break;
                }
                case 2:
                {
                    switch( m_Stage2Type )
                    {
                        case eStage2Type.None: m_CurrentStage += 1; break;
                        case eStage2Type.Brigands: AddMonster( typeof( Brigand ), false, m_Stage2Count ); break;
                        case eStage2Type.EvilMages: AddMonster( typeof( EvilMage ), false, m_Stage2Count ); break;
                        case eStage2Type.Lizardmen: AddMonster( typeof( Lizardman ), false, m_Stage2Count ); break;
                        case eStage2Type.Orc: AddMonster( typeof( Orc ), false, m_Stage2Count ); break;
                        case eStage2Type.Skeletons: AddMonster( typeof( Skeleton ), false, m_Stage2Count ); break;
                        case eStage2Type.Trolls: AddMonster( typeof( Troll ), false, m_Stage2Count ); break;
                        case eStage2Type.Zombies: AddMonster( typeof( Zombie ), false, m_Stage2Count ); break;
                    }
                    break;
                }
                case 3:
                {
                    switch( m_Stage3Type )
                    {
                        case eStage3Type.None: m_CurrentStage += 1; break;
                        case eStage3Type.BoneMagi: AddMonster( typeof( BoneMagi ), false, m_Stage3Count ); break;
                        case eStage3Type.Cyclops: AddMonster( typeof( Cyclops ), false, m_Stage3Count ); break;
                        case eStage3Type.EvilMageLords: AddMonster( typeof( EvilMageLord ), false, m_Stage3Count ); break;
                        case eStage3Type.Gargoyles: AddMonster( typeof( Gargoyle ), false, m_Stage3Count ); break;
                        case eStage3Type.Ogres: AddMonster( typeof( Ogre ), false, m_Stage3Count ); break;
                        case eStage3Type.OrcishMages: AddMonster( typeof( OrcishMage ), false, m_Stage3Count ); break;
                    }
                    break;
                }
                case 4:
                {
                    switch( m_Stage4Type )
                    {
                        case eStage4Type.None: m_CurrentStage += 1; break;
                        case eStage4Type.AncientLichs: AddMonster( typeof( AncientLich ), false, m_Stage4Count ); break;
                        case eStage4Type.Balrons: AddMonster( typeof( Balron ), false, m_Stage4Count ); break;
                        case eStage4Type.Daemons: AddMonster( typeof( Daemon ), false, m_Stage4Count ); break;
                        case eStage4Type.Dragons: AddMonster( typeof( Dragon ), false, m_Stage4Count ); break;
                        case eStage4Type.Liches: AddMonster( typeof( Lich ), false, m_Stage4Count ); break;
                        case eStage4Type.Titans: AddMonster( typeof( Titan ), false, m_Stage4Count ); break;
                    }
                    break;
                }
            }

            if( m_Timer != null )
                m_Timer.Stop();

            m_Timer = new InternalTimer( this, false );
            m_Timer.Start();
        }

        public void SpawnChamp()
        {
            Despawn( false );

            switch( m_ChampionType )
            {
                case eChampionType.None:
                {
                    if( m_Timer != null )
                        m_Timer.Stop();

                    if( m_Broadcast )
                        AnnounceMessage( false );

                    Despawn( true );
                    ToggleGuards( true );
                    Enabled = false;
                    break;
                }
                case eChampionType.Barracoon: AddMonster( typeof( Barracoon ), true, 1 ); break;
                case eChampionType.Harrower: AddMonster( typeof( Harrower ), true, 1 ); break;
                case eChampionType.LordOaks: AddMonster( typeof( LordOaks ), true, 1 ); break;
                case eChampionType.Mephitis: AddMonster( typeof( Mephitis ), true, 1 ); break;
                case eChampionType.Neira: AddMonster( typeof( Neira ), true, 1 ); break;
                case eChampionType.Rikktor: AddMonster( typeof( Rikktor ), true, 1 ); break;
                case eChampionType.Semidar: AddMonster( typeof( Semidar ), true, 1 ); break;
                case eChampionType.Serado: AddMonster( typeof( Serado ), true, 1 ); break;
            }

            m_Timer = new InternalTimer( this, true );
            m_Timer.Start();
        }

        public void AddMonster( Type type, bool champ, int count )
        {
            for( int i = 0; i < count; ++i )
            {
                object monster = Activator.CreateInstance( type );

                if( monster != null && monster is Mobile )
                {
                    Point3D location = FindSpawnLocation();
                    BaseCreature from = (BaseCreature)monster;

                    from.MoveToWorld( location, this.Map );
                    from.Home = location;
                    from.Tamable = false;

                    if( m_Waypoint != null )
                        from.CurrentWayPoint = m_Waypoint;

                    m_Spawned.Add( from );

                    if( m_RewardsEnabled )
                    {
                        if( champ && m_ArtifactChance > Utility.Random( 100 ) )
                        {
                            Item item = (Item)Activator.CreateInstance( Artifacts[Utility.Random( Artifacts.Length )] );

                            from.AddItem( item );
                        }

                        if( champ && m_BlessDeedChance > Utility.Random( 100 ) )
                        {
                            ClothingBlessDeed cbd = new ClothingBlessDeed();
                            cbd.LootType = LootType.Cursed;
                            cbd.Name = "a cursed clothing bless deed";
                            cbd.Hue = 1157;
                            from.AddItem( cbd );
                        }
                    }
                }
            }
        }

        public void Despawn( bool clearCurrentStage )
        {
            if( m_Timer != null )
                m_Timer.Stop();

            for( int i = 0; i < m_Spawned.Count; ++i )
                if( m_Spawned[i] != null && !m_Spawned[i].Deleted && m_Spawned[i].Alive )
                    m_Spawned[i].Delete();

            if( m_Spawned.Count != 0 )
                m_Spawned.Clear();

            if( clearCurrentStage )
                m_CurrentStage = 0;
        }

        public void RegionQuery()
        {
            Map map = this.Map;

            if( map != null )
            {
                Region reg = Server.Region.Find( this.Location, map );

                StringBuilder builder = new StringBuilder();

                builder.Append( reg.ToString() );
                reg = reg.Parent;

                while( reg != null )
                {
                    builder.Append( " <- " + reg.ToString() );
                    reg = reg.Parent;
                }

                m_RegionName = builder.ToString();
            }
        }

        public void ToggleGuards( bool guardsEnabled )
        {
            GuardedRegion reg = (GuardedRegion)Region.Find( this.Location, this.Map ).GetRegion( typeof( GuardedRegion ) );

            if( reg == null )
                return;

            //if( guardsEnabled )
            //    reg.Disabled = true;
            //else
            //    reg.Disabled = false;
        }

        public void AnnounceMessage( bool starting )
        {
            if( m_Broadcast )
            {
                RegionQuery();

                if( starting )
                    World.Broadcast( 1161, true, "[System Message]: Attention! {0} is currently under attack!", m_RegionName );
                else
                    World.Broadcast( 1161, true, "[System Message]: Attention! Local militia have restored order to {0}!", m_RegionName );
            }
        }

        public Point3D FindSpawnLocation()
        {
            int x = 0, y = 0, z = 0;

            do
            {
                x = Utility.Random( m_Top.X, ( m_Bottom.X - m_Top.X ) );
                y = Utility.Random( m_Top.Y, ( m_Bottom.Y - m_Top.Y ) );
                z = Utility.Random( m_MinSpawnZ, m_MaxSpawnZ );
            }
            while( !Map.CanSpawnMobile( x, y, z ) );

            return new Point3D( x, y, z );
        }

        public void DeleteAllWayPoints()
        {
            while( m_Waypoint != null && !m_Waypoint.Deleted )
            {
                WayPoint t = m_Waypoint;
                m_Waypoint = m_Waypoint.NextPoint;
                t.Delete();
            }
        }

        public override void OnDelete()
        {
            Despawn( true );
            DeleteAllWayPoints();
            base.OnDelete();
        }

        public InvasionSpawner( Serial serial )
            : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 0 ); //version

            writer.Write( (int)m_Stage1Type );
            writer.Write( (int)m_Stage2Type );
            writer.Write( (int)m_Stage3Type );
            writer.Write( (int)m_Stage4Type );
            writer.Write( (int)m_ChampionType );
            writer.WriteEncodedInt( (int)m_Stage1Count );
            writer.WriteEncodedInt( (int)m_Stage2Count );
            writer.WriteEncodedInt( (int)m_Stage3Count );
            writer.WriteEncodedInt( (int)m_Stage4Count );
            writer.WriteEncodedInt( (int)m_CurrentStage );
            writer.Write( (bool)m_RewardsEnabled );
            writer.WriteEncodedInt( (int)m_ArtifactChance );
            writer.WriteEncodedInt( (int)m_BlessDeedChance );
            writer.WriteEncodedInt( (int)m_MinSpawnZ );
            writer.WriteEncodedInt( (int)m_MaxSpawnZ );
            writer.Write( (bool)m_Enabled );
            writer.Write( (bool)m_Broadcast );
            writer.Write( (Point3D)m_Top );
            writer.Write( (Point3D)m_Bottom );
            writer.Write( (string)m_RegionName );
            writer.WriteMobileList<BaseCreature>( m_Spawned );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();

            switch( version )
            {
                case 0:
                {
                    m_Stage1Type = (eStage1Type)reader.ReadInt();
                    m_Stage2Type = (eStage2Type)reader.ReadInt();
                    m_Stage3Type = (eStage3Type)reader.ReadInt();
                    m_Stage4Type = (eStage4Type)reader.ReadInt();
                    m_ChampionType = (eChampionType)reader.ReadInt();
                    m_Stage1Count = reader.ReadEncodedInt();
                    m_Stage2Count = reader.ReadEncodedInt();
                    m_Stage3Count = reader.ReadEncodedInt();
                    m_Stage4Count = reader.ReadEncodedInt();
                    m_CurrentStage = reader.ReadEncodedInt();
                    m_RewardsEnabled = reader.ReadBool();
                    m_ArtifactChance = reader.ReadEncodedInt();
                    m_BlessDeedChance = reader.ReadEncodedInt();
                    m_MinSpawnZ = reader.ReadEncodedInt();
                    m_MaxSpawnZ = reader.ReadEncodedInt();
                    m_Enabled = reader.ReadBool();
                    m_Broadcast = reader.ReadBool();
                    m_Top = reader.ReadPoint3D();
                    m_Bottom = reader.ReadPoint3D();
                    m_RegionName = reader.ReadString();
                    m_Spawned = reader.ReadStrongMobileList<BaseCreature>();

                    break;
                }
            }
        }

        public class InternalTimer : Timer
        {
            InvasionSpawner m_Spawner;

            bool m_Champ;
            int lastCount; //Prevent spamming the shard

            public InternalTimer( InvasionSpawner spawner, bool champ )
                : base( TimeSpan.FromSeconds( 5.0 ), TimeSpan.FromSeconds( 5.0 ) )
            {
                m_Spawner = spawner;
                m_Champ = champ;
            }

            protected override void OnTick()
            {
                if( m_Spawner.Enabled )
                {
                    int count = 0;

                    for( int i = 0; i < m_Spawner.Spawned.Count; ++i )
                        if( m_Spawner.Spawned[i] != null && !m_Spawner.Spawned[i].Deleted && m_Spawner.Spawned[i].Alive )
                            ++count;

                    if( !m_Champ ) //Monsters
                    {
                        if( m_Spawner.Broadcast && lastCount != count && ( count % 20 ) == 0 )
                        {// ( count % 20 ) == 0 means this will broadcast shardwide every 20 kills
                            m_Spawner.AnnounceMessage( true );
                            lastCount = count;
                        }

                        if( count == 0 )//Monsters are all dead 
                        {
                            if( m_Spawner.m_CurrentStage >= 0 && m_Spawner.m_CurrentStage <= 3 )
                            {
                                ++m_Spawner.m_CurrentStage;
                                m_Spawner.Spawn();
                            }
                            else if( m_Spawner.m_CurrentStage == 4 ) //All 4 tiers of monsters have been slayed
                                m_Spawner.SpawnChamp();
                        }
                    }
                    else //Champion
                    {
                        if( count == 0 )//Champion is dead
                        {
                            if( m_Spawner.Broadcast )
                                m_Spawner.AnnounceMessage( false );

                            m_Spawner.Despawn( true );
                            m_Spawner.ToggleGuards( true );
                            m_Spawner.Enabled = false;
                        }
                    }
                }
            }
        }
    }
}