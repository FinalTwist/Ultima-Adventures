using Server;
using Server.Mobiles;
using System;

public class MonstersBashSpawnInfo
{
	private string m_Name;
	private Type m_Champion;
	private Type[][] m_SpawnTypes;
	private string[] m_LevelNames;
	private int[] m_PointsPerKill;

	public string Name { get { return m_Name; } }
	public Type Champion { get { return m_Champion; } }
	public Type[][] SpawnTypes { get { return m_SpawnTypes; } }
	public string[] LevelNames { get { return m_LevelNames; } }

	public int[] PointsPerKill { get { return m_PointsPerKill; } }

	public MonstersBashSpawnInfo(string name, Type champion, string[] levelNames, int[] pointsPerKill, Type[][] spawnTypes)
	{
		m_Name = name;
		m_Champion = champion;
		m_LevelNames = levelNames;
		m_SpawnTypes = spawnTypes;
		m_PointsPerKill = pointsPerKill;
	}

	public static MonstersBashSpawnInfo[] Table { get { return m_Table; } }

	private static readonly MonstersBashSpawnInfo[] m_Table = new MonstersBashSpawnInfo[]
		{
				new MonstersBashSpawnInfo( "Abyss", typeof( Semidar ), new string[]{ "Foe", "Assassin", "Conqueror" }, new int[] {50, 70, 80, 100}, new Type[][] 	// Abyss
				{																											// Abyss
					new Type[]{ typeof( ChampionGreaterMongbat ), typeof( Imp ) },													// Level 1
					new Type[]{ typeof( ChampionGargoyle ), typeof( Harpy ) },														// Level 2
					new Type[]{ typeof( ChampionFireGargoyle ), typeof( StoneGargoyle ) },											// Level 3
					new Type[]{ typeof( ChampionDaemon ), typeof( Succubus ) }														// Level 4
				} ),
				new MonstersBashSpawnInfo( "Arachnid", typeof( Mephitis ), new string[]{ "Bane", "Killer", "Vanquisher" }, new int[] {50, 70, 80, 100}, new Type[][]	// Arachnid
				{																											// Arachnid
					new Type[]{ typeof( ChampionScorpion ), typeof( GiantSpider ) },												// Level 1
					new Type[]{ typeof( ChampionTerathanDrone ), typeof( TerathanWarrior ) },										// Level 2
					new Type[]{ typeof( ChampionDreadSpider ), typeof( TerathanMatriarch ) },										// Level 3
					new Type[]{ typeof( ChampionPoisonElemental ), typeof( TerathanAvenger ) }										// Level 4
				} ),
				new MonstersBashSpawnInfo( "Cold Blood", typeof( Rikktor ), new string[]{ "Blight", "Slayer", "Destroyer" }, new int[] {50, 70, 80, 100}, new Type[][]	// Cold Blood
				{																											// Cold Blood
					new Type[]{ typeof( ChampionLizardman ), typeof( Snake ) },														// Level 1
					new Type[]{ typeof( ChampionLavaLizard ), typeof( OphidianWarrior ) },											// Level 2
					new Type[]{ typeof( ChampionDrake ), typeof( OphidianArchmage ) },												// Level 3
					new Type[]{ typeof( ChampionDragon ), typeof( OphidianKnight ) }												// Level 4
				} ),
				new MonstersBashSpawnInfo( "Forest Lord", typeof( LordOaks ), new string[]{ "Enemy", "Curse", "Slaughterer" }, new int[] {50, 70, 80, 100}, new Type[][]	// Forest Lord
				{																											// Forest Lord
					new Type[]{ typeof( ChampionPixie ), typeof( ShadowWisp ) },													// Level 1
					new Type[]{ typeof( ChampionKirin ), typeof( Wisp ) },															// Level 2
					new Type[]{ typeof( ChampionCentaur ), typeof( Unicorn ) },														// Level 3
					new Type[]{ typeof( ChampionEtherealWarrior ), typeof( SerpentineDragon ) }										// Level 4
				} ),
				new MonstersBashSpawnInfo( "Vermin Horde", typeof( Barracoon ), new string[]{ "Adversary", "Subjugator", "Eradicator" }, new int[] {50, 70, 80, 100}, new Type[][]	// Vermin Horde
				{																											// Vermin Horde
					new Type[]{ typeof( ChampionGiantRat ), typeof( Slime ) },														// Level 1
					new Type[]{ typeof( ChampionDireWolf ), typeof( Ratman ) },														// Level 2
					new Type[]{ typeof( ChampionHellHound ), typeof( RatmanMage ) },												// Level 3
					new Type[]{ typeof( ChampionRatmanArcher ), typeof( ChampionSilverSerpent ) }											// Level 4
				} ),
				new MonstersBashSpawnInfo( "Unholy Terror", typeof( Neira ), new string[]{ "Scourge", "Punisher", "Nemesis" }, new int[] {50, 70, 80, 100}, new Type[][]	// Unholy Terror
				{																											// Unholy Terror
					(Core.AOS ?
					new Type[]{ typeof( Bogle ), typeof( Ghoul ), typeof( Shade ), typeof( Spectre ), typeof( Wraith ) }	// Level 1 (Pre-AoS)
					: new Type[]{ typeof( ChampionGhoul ), typeof( Shade ), typeof( Spectre ), typeof( Wraith ) } ),				// Level 1
					new Type[]{ typeof( ChampionBoneMagi ), typeof( Mummy ), typeof( SkeletalMage ) },								// Level 2
					new Type[]{ typeof( ChampionBoneKnight ), typeof( Lich ), typeof( SkeletalKnight ) },							// Level 3
					new Type[]{ typeof( ChampionLichLord ), typeof( RottingCorpse ) }												// Level 4
				} ),
				new MonstersBashSpawnInfo( "Sleeping Dragon", typeof( Serado ), new string[]{ "Rival", "Challenger", "Antagonist" }, new int[] {50, 70, 80, 100}, new Type[][]
				{																											// Unholy Terror
					new Type[]{ typeof( ChampionDeathwatchBeetleHatchling ), typeof( Lizardman ) },
					new Type[]{ typeof( ChampionDeathwatchBeetle ), typeof( Kappa ) },
					new Type[]{ typeof( ChampionLesserHiryu ), typeof( RevenantLion ) },
					new Type[]{ typeof( ChampionHiryu ), typeof( Oni ) }
				} ),
				new MonstersBashSpawnInfo( "Glade", typeof( Twaulo ), new string[]{ "Banisher", "Enforcer", "Eradicator" }, new int[] {50, 70, 80, 100}, new Type[][]
				{																											// Glade
					new Type[]{ typeof( ChampionPixie ), typeof( ShadowWisp ) },
					new Type[]{ typeof( ChampionCentaur ), typeof( MLDryad ) },
					new Type[]{ typeof( ChampionSatyr ), typeof( CuSidhe ) },
					new Type[]{ typeof( FerelTreefellow ), typeof( RagingGrizzlyBear ) }
				} ),
				new MonstersBashSpawnInfo( "Elementals", typeof( Vulcrum ), new string[]{ "Crumbler", "Basher", "Animator" }, new int[] {50, 70, 80, 100}, new Type[][]
				{																											// Glade
					new Type[]{ typeof( EarthElemental ), typeof( ShadowIronElemental ) },
					new Type[]{ typeof( CopperElemental), typeof( GoldenElemental ) },
					new Type[]{ typeof( ForestElemental ), typeof( ToxicElemental ) },
					new Type[]{ typeof( XormiteElemental ), typeof( DilithiumElemental ) }
				} ),
				new MonstersBashSpawnInfo( "The Corrupt", typeof( Ilhenir ), new string[]{ "Cleanser", "Expunger", "Depurator" }, new int[] {50, 70, 80, 100}, new Type[][]
				{																											// Unholy Terror
					new Type[]{ typeof( ChampionPlagueSpawn ), typeof( Bogling ) },
					new Type[]{ typeof( ChampionPlagueBeast ), typeof( BogThing ) },
					new Type[]{ typeof( ChampionPlagueBeastLord ), typeof( InterredGrizzle ) },
					new Type[]{ typeof( ChampionFetidEssence ), typeof( PestilentBandage ) }
				} )
		};

	public static MonstersBashSpawnInfo GetInfo(MonstersBashType type)
	{
		int v = (int)type;

		if (v < 0 || v >= m_Table.Length)
			v = 0;

		return m_Table[v];
	}
}
