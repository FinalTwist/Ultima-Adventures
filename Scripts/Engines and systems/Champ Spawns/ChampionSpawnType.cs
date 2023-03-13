using System;
using Server;
using Server.Mobiles;

namespace Server.Engines.CannedEvil
{
	public enum ChampionSpawnType
	{
		Abyss,
		Arachnid,
		ColdBlood,
		ForestLord,
		VerminHorde,
		UnholyTerror,
		SleepingDragon,
        	Glade,
        	Corrupt

//		#region SA
//		AbyssalLair,
//		LichLand
//		#endregion
	}

	public class ChampionSpawnInfo
	{
		private string m_Name;
		private Type m_Champion;
		private Type[][] m_SpawnTypes;
		private string[] m_LevelNames;

		public string Name { get { return m_Name; } }
		public Type Champion { get { return m_Champion; } }
		public Type[][] SpawnTypes { get { return m_SpawnTypes; } }
		public string[] LevelNames { get { return m_LevelNames; } }

		public ChampionSpawnInfo( string name, Type champion, string[] levelNames, Type[][] spawnTypes )
		{
			m_Name = name;
			m_Champion = champion;
			m_LevelNames = levelNames;
			m_SpawnTypes = spawnTypes;
		}

		public static ChampionSpawnInfo[] Table{ get { return m_Table; } }

		private static readonly ChampionSpawnInfo[] m_Table = new ChampionSpawnInfo[]
			{
				new ChampionSpawnInfo( "Abyss", typeof( Semidar ), new string[]{ "Foe", "Assassin", "Conqueror" }, new Type[][]					// Abyss
				{
					new Type[]{ typeof( ChampionGreaterMongbat ), typeof( ChampionImp ) },									// Level 1
					new Type[]{ typeof( ChampionGargoyle ), typeof( ChampionHarpy ) },									// Level 2
					new Type[]{ typeof( ChampionFireGargoyle ), typeof( ChampionStoneGargoyle ) },								// Level 3
					new Type[]{ typeof( ChampionDaemon ), typeof( ChampionSuccubus ) }									// Level 4
				} ),
				new ChampionSpawnInfo( "Arachnid", typeof( Mephitis ), new string[]{ "Bane", "Killer", "Vanquisher" }, new Type[][]				// Arachnid
				{
					new Type[]{ typeof( ChampionScorpion ), typeof( ChampionGiantSpider ) },								// Level 1
					new Type[]{ typeof( ChampionTerathanDrone ), typeof( ChampionTerathanWarrior ) },							// Level 2
					new Type[]{ typeof( ChampionDreadSpider ), typeof( ChampionTerathanMatriarch ) },							// Level 3
					new Type[]{ typeof( ChampionPoisonElemental ), typeof( ChampionTerathanAvenger ) }							// Level 4
				} ),
				new ChampionSpawnInfo( "Cold Blood", typeof( Rikktor ), new string[]{ "Blight", "Slayer", "Destroyer" }, new Type[][]				// Cold Blood
				{
					new Type[]{ typeof( ChampionLizardman ), typeof( ChampionSnake ) },									// Level 1
					new Type[]{ typeof( ChampionLavaLizard ), typeof( ChampionOphidianWarrior ) },								// Level 2
					new Type[]{ typeof( ChampionDrake ), typeof( ChampionOphidianArchmage ) },								// Level 3
					new Type[]{ typeof( ChampionDragon ), typeof( ChampionOphidianKnight ) }								// Level 4
				} ),
				new ChampionSpawnInfo( "Forest Lord", typeof( LordOaks ), new string[]{ "Enemy", "Curse", "Slaughterer" }, new Type[][]				// Forest Lord
				{
					new Type[]{ typeof( ChampionPixie ), typeof( ChampionShadowWisp ) },									// Level 1
					new Type[]{ typeof( ChampionKirin ), typeof( ChampionWisp ) },										// Level 2
					new Type[]{ typeof( ChampionCentaur ), typeof( ChampionUnicorn ) },									// Level 3
					new Type[]{ typeof( ChampionEtherealWarrior ), typeof( ChampionSerpentineDragon ) }							// Level 4
				} ),
				new ChampionSpawnInfo( "Vermin Horde", typeof( Barracoon ), new string[]{ "Adversary", "Subjugator", "Eradicator" }, new Type[][]		// Vermin Horde
				{
					new Type[]{ typeof( ChampionGiantRat ), typeof( ChampionSlime ) },									// Level 1
					new Type[]{ typeof( ChampionDireWolf ), typeof( ChampionRatman ) },									// Level 2
					new Type[]{ typeof( ChampionHellHound ), typeof( ChampionRatmanMage ) },								// Level 3
					new Type[]{ typeof( ChampionRatmanArcher ), typeof( ChampionSilverSerpent ) }								// Level 4
				} ),
				new ChampionSpawnInfo( "Unholy Terror", typeof( Neira ), new string[]{ "Scourge", "Punisher", "Nemesis" }, new Type[][]				// Unholy Terror
				{
					(Core.AOS ? 
					new Type[]{ typeof( Bogle ), typeof( Ghoul ), typeof( Shade ), typeof( Spectre ), typeof( Wraith ) }					// Level 1 (Pre-AoS)
					: new Type[]{ typeof( ChampionGhoul ), typeof( ChampionShade ), typeof( ChampionSpectre ), typeof( ChampionWraith ) } ),		// Level 1
                
					new Type[]{ typeof( ChampionBoneMagi ), typeof( ChampionMummy ), typeof( ChampionSkeletalMage ) },					// Level 2
					new Type[]{ typeof( ChampionBoneKnight ), typeof( ChampionLich ), typeof( ChampionSkeletalKnight ) },					// Level 3
					new Type[]{ typeof( ChampionLichLord ), typeof( ChampionRottingCorpse ) }								// Level 4
				} ),
				new ChampionSpawnInfo( "Sleeping Dragon", typeof( Serado ), new string[]{ "Rival", "Challenger", "Antagonist" } , new Type[][]			// Sleeping Dragon
				{
					new Type[]{ typeof( ChampionDeathwatchBeetleHatchling ), typeof( Champion2Lizardman ) },						// Level 1
					new Type[]{ typeof( ChampionDeathwatchBeetle ), typeof( ChampionKappa ) },								// Level 2
					new Type[]{ typeof( ChampionLesserHiryu ), typeof( ChampionRevenantLion ) },								// Level 3
					new Type[]{ typeof( ChampionHiryu ), typeof( ChampionOni ) }										// Level 4
				} ),
                		new ChampionSpawnInfo( "Glade", typeof( Twaulo ), new string[]{ "Banisher", "Enforcer", "Eradicator" } , new Type[][]				// Glade
				{
					new Type[]{ typeof( Champion2Pixie ), typeof( Champion2ShadowWisp ) },									// Level 1
					new Type[]{ typeof( Champion2Centaur ), typeof( ChampionMLDryad ) },									// Level 2
					new Type[]{ typeof( ChampionSatyr ), typeof( ChampionCuSidhe ) },									// Level 3
					new Type[]{ typeof( ChampionFeralTreefellow ), typeof( ChampionRagingGrizzlyBear ) }							// Level 4
				} ),
				new ChampionSpawnInfo( "Corrupt", typeof( Ilhenir ), new string[]{ "Cleanser", "Expunger", "Depurator" } , new Type[][]				// Corrupt
				{
					new Type[]{ typeof( ChampionPlagueSpawn ), typeof( ChampionBogling ) },									// Level 1
					new Type[]{ typeof( ChampionPlagueBeast ), typeof( ChampionBogThing ) },								// Level 2
					new Type[]{ typeof( ChampionPlagueBeastLord ), typeof( ChampionInterredGrizzle ) },							// Level 3
					new Type[]{ typeof( ChampionFetidEssence ), typeof( ChampionPestilentBandage ) }							// Level 4
				} )

//				#region SA
//				new ChampionSpawnInfo( "AbyssalLair", typeof( AbyssalInfernal ), new string[]{ "Banisher", "Enforcer", "Eradicator" } , new Type[][]		//AbyssalLair
//				{
//					new Type[]{ typeof( ChampionHordeMinion ), typeof( ChampionChaosDaemon ) },								// Level 1
//					new Type[]{ typeof( ChampionStoneHarpy ), typeof( ChampionArcaneDaemon ) },								// Level 2
//					new Type[]{ typeof( ChampionPitFiend ), typeof( ChampionMoloch ) },									// Level 3
//					new Type[]{ typeof( ChampionArchDaemon ), typeof( ChampionAbyssalAbomination ) }							// Level 4
//				} ),
//				new ChampionSpawnInfo( "LichLand", typeof( PrimevalLich ), new string[]{ "Cleanser", "Expunger", "Depurator" } , new Type[][]			// LichLand
//				{
//					new Type[]{ typeof( ChampionGoreFiend ), typeof( ChampionVampireBat ) },								// Level 1
//					new Type[]{ typeof( ChampionFleshGolem ), typeof( ChampionDarkWisp ) },									// Level 2
//					new Type[]{ typeof( ChampionUndeadGargoyle ), typeof( ChampionWight ) },								// Level 3
//					new Type[]{ typeof( ChampionSkeletalDrake ), typeof( ChampionDreamWraith ) }								// Level 4
//				} )
//				#endregion
			};

		public static ChampionSpawnInfo GetInfo( ChampionSpawnType type )
		{
			int v = (int)type;

			if( v < 0 || v >= m_Table.Length )
				v = 0;

			return m_Table[v];
		}
	}
}