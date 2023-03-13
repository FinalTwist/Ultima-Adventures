using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using System.IO;
using System.Reflection;
using Server.Engines.Mahjong;

namespace Server.Items
{
	public class DungeonLoot
	{
		private static Type[] m_DungeonLootTypes = new Type[]
			{
				typeof( FoodStaleBread ),		typeof( FoodDriedBeef ),		typeof( Torch ),
				typeof( Bandage ),				typeof( Waterskin ),
				typeof( Candle ),				typeof( Lantern ),				typeof( Arrow ),
				typeof( FoodStaleBread ),		typeof( FoodDriedBeef ),		typeof( Bolt ),
				typeof( FoodStaleBread ),		typeof( FoodDriedBeef ),		typeof( Candle ),
				typeof( DirtyWaterskin ),		typeof( DirtyWaterskin ),		typeof( Torch ),
				typeof( Lockpick ),				typeof( SpoolOfThread),			typeof( Bedroll ),
				typeof( Kindling ), 			typeof( DarkYarn ), 			typeof( LightYarn ),
				typeof( ThrowingWeapon ),		typeof( MageEye ),				typeof( HarpoonRope )
			};

		public static Type[] DungeonLootTypes{ get{ return m_DungeonLootTypes; } }

		public static Item RandomItem()
		{
			return Construct( m_DungeonLootTypes );
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private static Type[] m_HighLevelScrolls = new Type[]
			{
				typeof( AnimateDeadScroll ),		typeof( ExorcismScroll ),
				typeof( EvilOmenScroll ),			typeof( HorrificBeastScroll ),			typeof( LichFormScroll ),				typeof( MindRotScroll ),
				typeof( PainSpikeScroll ),			typeof( PoisonStrikeScroll ),			typeof( StrangleScroll ),				typeof( SummonFamiliarScroll ),
				typeof( VampiricEmbraceScroll ),	typeof( VengefulSpiritScroll ),			typeof( WitherScroll ),					typeof( WraithFormScroll ),
				typeof( ArchCureScroll ),			typeof( ArchProtectionScroll ),			typeof( CurseScroll ),					typeof( FireFieldScroll ),
				typeof( GreaterHealScroll ),		typeof( LightningScroll ),				typeof( ManaDrainScroll ),				typeof( RecallScroll ),
				typeof( BladeSpiritsScroll ),		typeof( DispelFieldScroll ),			typeof( IncognitoScroll ),				typeof( MagicReflectScroll ),
				typeof( MindBlastScroll ),			typeof( ParalyzeScroll ),				typeof( PoisonFieldScroll ),			typeof( SummonCreatureScroll ),
				typeof( DispelScroll ),				typeof( EnergyBoltScroll ),				typeof( ExplosionScroll ),				typeof( InvisibilityScroll ),
				typeof( MarkScroll ),				typeof( MassCurseScroll ),				typeof( ParalyzeFieldScroll ),			typeof( RevealScroll ),
				typeof( ChainLightningScroll ),		typeof( EnergyFieldScroll ),			typeof( FlamestrikeScroll ),			typeof( GateTravelScroll ),
				typeof( ManaVampireScroll ),		typeof( MassDispelScroll ),				typeof( MeteorSwarmScroll ),			typeof( PolymorphScroll ),
				typeof( EarthquakeScroll ),			typeof( EnergyVortexScroll ),			typeof( ResurrectionScroll ),			typeof( SummonAirElementalScroll ),
				typeof( SummonDaemonScroll ),		typeof( SummonEarthElementalScroll ),	typeof( SummonFireElementalScroll ),	typeof( SummonWaterElementalScroll )
			};

		public static Type[] HighLevelScrolls{ get{ return m_HighLevelScrolls; } }

		public static Item RandomHighLevelScroll()
		{
			return Construct( m_HighLevelScrolls );
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private static Type[] m_DungeonWareTypes = new Type[]
			{
				typeof( Board ),				typeof( Board ),				typeof( Board ),				typeof( Board ),			typeof( Board ),			typeof( Board ),		typeof( Board ),	typeof( Board ),	typeof( Board ),	typeof( Board ),
				typeof( AshBoard ),				typeof( AshBoard ),				typeof( AshBoard ),				typeof( AshBoard ),			typeof( AshBoard ),			typeof( AshBoard ),
				typeof( CherryBoard ),			typeof( CherryBoard ),			typeof( CherryBoard ),			typeof( CherryBoard ),		typeof( CherryBoard ),
				typeof( EbonyBoard ),			typeof( EbonyBoard ),			typeof( EbonyBoard ),			typeof( EbonyBoard ),		typeof( EbonyBoard ),
				typeof( GoldenOakBoard ),		typeof( GoldenOakBoard ),		typeof( GoldenOakBoard ),		typeof( GoldenOakBoard ),
				typeof( HickoryBoard ),			typeof( HickoryBoard ),			typeof( HickoryBoard ),			typeof( HickoryBoard ),
				typeof( MahoganyBoard ),		typeof( MahoganyBoard ),		typeof( MahoganyBoard ),
				typeof( OakBoard ),				typeof( OakBoard ),				typeof( OakBoard ),
				typeof( PineBoard ),			typeof( PineBoard ),
				typeof( RosewoodBoard ),		typeof( RosewoodBoard ),
				typeof( WalnutBoard ),
				typeof( IronIngot ),			typeof( IronIngot ),			typeof( IronIngot ),			typeof( IronIngot ),		typeof( IronIngot ),	typeof( IronIngot ),	typeof( IronIngot ),	typeof( IronIngot ),	typeof( IronIngot ),
				typeof( DullCopperIngot ),		typeof( DullCopperIngot ),		typeof( DullCopperIngot ),		typeof( DullCopperIngot ),
				typeof( ShadowIronIngot ),		typeof( ShadowIronIngot ),		typeof( ShadowIronIngot ),		typeof( ShadowIronIngot ),
				typeof( CopperIngot ),			typeof( CopperIngot ),			typeof( CopperIngot ),
				typeof( BronzeIngot ),			typeof( BronzeIngot ),			typeof( BronzeIngot ),
				typeof( GoldIngot ),			typeof( GoldIngot ),
				typeof( AgapiteIngot ),			typeof( AgapiteIngot ),
				typeof( VeriteIngot ),
				typeof( ValoriteIngot ),
				typeof( Leather ),				typeof( Leather ),				typeof( Leather ),				typeof( Leather ),			typeof( Leather ),			typeof( Leather ),	typeof( Leather ),	typeof( Leather ),	typeof( Leather ),	typeof( Leather ),
				typeof( HornedLeather ),		typeof( HornedLeather ),		typeof( HornedLeather ),		typeof( HornedLeather ),
				typeof( BarbedLeather ),		typeof( BarbedLeather ),		typeof( BarbedLeather ),
				typeof( NecroticLeather ),		typeof( NecroticLeather ),
				typeof( VolcanicLeather ),		typeof( VolcanicLeather ),
				typeof( FrozenLeather ),		typeof( FrozenLeather ),
				typeof( SpinedLeather ),
				typeof( GoliathLeather ),
				typeof( DraconicLeather ),
				typeof( HellishLeather ),

				typeof( Cloth ),	typeof( BoltOfCloth ),	typeof( UncutCloth ),	typeof( Cloth ),	typeof( BoltOfCloth ),	typeof( UncutCloth ),
				typeof( Cloth ),	typeof( BoltOfCloth ),	typeof( UncutCloth ),	typeof( Cloth ),	typeof( BoltOfCloth ),	typeof( UncutCloth ),
				typeof( Cloth ),	typeof( BoltOfCloth ),	typeof( UncutCloth ),	typeof( Cloth ),	typeof( BoltOfCloth ),	typeof( UncutCloth ),
				typeof( Cloth ),	typeof( BoltOfCloth ),	typeof( UncutCloth ),	typeof( Cloth ),	typeof( BoltOfCloth ),	typeof( UncutCloth ),
				typeof( Cloth ),	typeof( BoltOfCloth ),	typeof( UncutCloth ),	typeof( Cloth ),	typeof( BoltOfCloth ),	typeof( UncutCloth ),
				typeof( Cloth ),	typeof( BoltOfCloth ),	typeof( UncutCloth ),	typeof( Cloth ),	typeof( BoltOfCloth ),	typeof( UncutCloth ),
				typeof( Cloth ),	typeof( BoltOfCloth ),	typeof( UncutCloth ),	typeof( Cloth ),	typeof( BoltOfCloth ),	typeof( UncutCloth ),

				typeof( Jar ),		typeof( Jar ),			typeof( Jar ),			typeof( Jar ),		typeof( Jar ),			typeof( Jar ),

				typeof( PolishedBone ),		typeof( PolishedBone ),			typeof( PolishedSkull ),			typeof( PolishedSkull ),

				typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),
				typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),
				typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),
				typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),
				typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),
				typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),
				typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),
				typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),
				typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),
				typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),
				typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft ),	typeof( Bottle ),	typeof( BlankScroll ),	typeof( Feather ),	typeof( Shaft )
			};

		public static Type[] DungeonWareTypes{ get{ return m_DungeonWareTypes; } }

		public static Item RandomWares()
		{
			return Construct( m_DungeonWareTypes );
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private static Type[] m_RuneMagic = new Type[]
			{
				typeof( RuneMagicBook ),	typeof( Runebook ),		typeof( RuneBag ),		typeof( RecallRune), 
				typeof( An ),				typeof( Bet ),			typeof( Corp ),			typeof( Des ),			typeof( Ex ),		
				typeof( Flam ),				typeof( Grav ),			typeof( Hur ),			typeof( In ),			typeof( Jux ),			typeof( Kal ),		
				typeof( Lor ),				typeof( Mani ),			typeof( Nox ),			typeof( Ort ),			typeof( Por ),			typeof( Quas ),		
				typeof( Rel ),				typeof( Sanct ),		typeof( Tym ),			typeof( Uus ),			typeof( Vas ),			typeof( Wis ),		
				typeof( Xen ),				typeof( Ylem ),			typeof( Zu )	
			};

		public static Type[] RuneMagic{ get{ return m_RuneMagic; } }

		public static Item RandomRuneMagic()
		{
			return Construct( m_RuneMagic );
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private static Type[] m_DungeonLoreBooks = new Type[]
			{
				typeof( Spellbook ),				typeof( NecromancerSpellbook ),				typeof( SongBook ),
				typeof( SomeRandomNote ),			typeof( SomeRandomNote ), 					typeof( SomeRandomNote ),
				typeof( LoreBook ),					typeof( LoreBook ),							typeof( LoreBook ),
				typeof( LoreBook ),					typeof( LoreBook ),							typeof( LoreBook ),
				typeof( LoreBook ),					typeof( LoreBook ),							typeof( LoreBook ),
				typeof( BlueBook ),					typeof( BlueBook ),							typeof( BlueBook ),
				typeof( BlueBook ),					typeof( BlueBook ),							typeof( BlueBook ),
				typeof( BlueBook ),					typeof( BlueBook ),							typeof( BlueBook ),
				typeof( WorldMapLodor ),			typeof( WorldMapSosaria ),					typeof( WorldMapBottle ),	
				typeof( WorldMapSerpent ),			typeof( WorldMapUmber ),					typeof( AlchemicalElixirs ), 
				typeof( WorldMapAmbrosia ),			typeof( WorldMapIslesOfDread ),				typeof( WorldMapSavage ),
				typeof( RuneMagicBook ),			typeof( MapRanger ),						typeof( GoldenRangers ),
				typeof( CBookDruidicHerbalism ),	typeof( CBookNecroticAlchemy ),				typeof( LearnWoodBook ),
				typeof( LearnTraps ),				typeof( LearnTitles ),						typeof( LearnTailorBook ),
				typeof( LearnStealingBook ),		typeof( LearnScalesBook ),					typeof( LearnReagentsBook ),
				typeof( LearnMiscBook ),			typeof( LearnMetalBook ),					typeof( LearnLeatherBook ),
				typeof( LearnGraniteBook ),			typeof( AlchemicalMixtures ),				typeof( BookOfPoisons ),
				typeof( WorkShoppes ),				typeof( SwordsAndShackles ),				typeof( QuestTake ),
				typeof( EternalWar ), 				typeof( BloodLichCult ), typeof( BeASorcerer ), typeof( BeATroubadour )
			};

		public static Type[] DungeonLoreBooks{ get{ return m_DungeonLoreBooks; } }

		public static Item RandomLoreBooks()
		{
			return Construct( m_DungeonLoreBooks );
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private static Type[] m_DungeonToolsTypes = new Type[]
			{
				typeof( FishingPole ),			typeof( Shovel ),				typeof( OreShovel ),
				typeof( Pickaxe ),				typeof( Scissors ),				typeof( Dyes ),
				typeof( DyeTub ),				typeof( FletcherTools ),		typeof( Hammer ),
				typeof( MapmakersPen ),			typeof( MortarPestle ),			typeof( Nails ),
				typeof( RollingPin ),			typeof( Saw ),					typeof( ScribesPen ),
				typeof( SewingKit ),			typeof( Skillet ),				typeof( SmithHammer ),
				typeof( Tongs ),				typeof( TinkerTools ),			typeof( Spyglass ),
				typeof( HerbalistCauldron ),	typeof( GardenTool ),			typeof( SewingKit ),
				typeof( SmithHammer ),			typeof( Skillet ),				typeof( RecallRune ),
				typeof( GraveShovel ),			typeof( MixingCauldron ),		typeof( SurgeonsKnife ),
				typeof( LumberAxe ),			typeof( InteriorDecorator ),	typeof( HousePlacementTool ), 
				typeof( SmallTent ),			typeof( CampersTent ),			typeof( TenFootPole ),
				typeof( Drums ),				typeof( LapHarp ),				typeof( Lute ),
				typeof( Tambourine ),			typeof( BambooFlute ),			typeof( PolishBoneBrush ),
				typeof( GrapplingHook ),		typeof( WoodworkingTools ),		typeof( Monocle )
			};

		public static Type[] DungeonToolsTypes{ get{ return m_DungeonToolsTypes; } }

		public static Item RandomTools()
		{
			return Construct( m_DungeonToolsTypes );
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private static Type[] m_DungeonRareTypes = new Type[]
			{
				typeof( MagicJewelryRing ),		typeof( MagicJewelryNecklace ),			typeof( MagicJewelryEarrings ),	
				typeof( MagicJewelryBracelet ),	typeof( SkeletonsKey ),					typeof( PaintCanvas ),				
				typeof( HeavySharpeningStone ),	typeof( ConsecratedSharpeningStone ),	typeof( MyCircusTentEastAddonDeed ),
				typeof( ManyArrows100 ),		typeof( ManyBolts100 ),					typeof( MyTentSouthAddonDeed ),
				typeof( RoughSharpeningStone ),	typeof( DenseSharpeningStone ),			typeof( ElementalSharpeningStone ),
				typeof( Runebook ),				typeof( Sand ),							typeof( MagicalDyes ),
				typeof( UnknownKeg ),			typeof( ArtifactManual ),				typeof( light_dragon_brazier ), 
				typeof( CrystallineJar ),		typeof( TrophyBase ),					typeof( DockingLantern ),
				typeof( MagicScissors ), 		typeof( InvulnerabilityPotion ),		typeof( MoonStone ),
				typeof( BoatBuild ),			typeof( TrapKit ),						typeof( MalletStake ), 
				typeof( MySpellbook ),			typeof( MyNecromancerSpellbook ),		typeof( MySongbook ),
				typeof( MyPaladinbook ),		typeof( MySamuraibook ),				typeof( MyNinjabook ),
				typeof( CandleLarge ),			typeof( Candelabra ),					typeof( CandelabraStand ),
				typeof( HairDyeBottle ),		typeof( GothicCandelabraA ),			typeof( GothicCandelabraB ),
				typeof( RareAnvil ),			typeof( MagicHammer ),					typeof( MasterSkeletonsKey ),
				typeof( UnusualDyes ),			typeof( MysticSpellbook ),				typeof( MagicPigment ),
				typeof( ArmsBarrel ),			typeof( AlternateRealityMap ),			typeof( GenderPotion ),
				typeof( NecromancerBarrel ),	typeof( PotionOfMight ),				typeof( PotionOfWisdom ),
                typeof( RoughWeightingStone ),  typeof( DenseWeightingStone ),          typeof( ElementalWeightingStone ),
                typeof( HeavyWeightingStone ),  typeof( ConsecratedWeightingStone ),
                typeof( PotionOfDexterity ),	typeof( CarpetBuild ),					typeof( DwarvenForge )
			};

		public static Type[] DungeonRareTypes{ get{ return m_DungeonRareTypes; } }

		public static Item RandomRare()
		{
			return Construct( m_DungeonRareTypes );
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private static Type[] m_DungeonJunkTypes = new Type[]
			{
				typeof( FurBoots ),				typeof( Boots ),				typeof( ThighBoots ),
				typeof( Shoes ),				typeof( Sandals ),				typeof( ElvenBoots ),
				typeof( FancyShirt ),			typeof( Shirt ),				typeof( ShortPants ),
				typeof( LongPants ),			typeof( Skirt ),				typeof( Kilt ),
				typeof( BodySash ),				typeof( FullApron ),			typeof( Doublet ),
				typeof( Surcoat ),				typeof( Tunic ),				typeof( FormalShirt ),
				typeof( RustyJunk ),			typeof( RustyJunk ),			typeof( RustyJunk ),
				typeof( JesterSuit ),			typeof( Cloak ),				typeof( Cloak ),
				typeof( HalfApron ),			typeof( Cloak ),				typeof( GildedDress ),
				typeof( FancyDress ),			typeof( Robe ),					typeof( Robe ),
				typeof( PlainDress ),			typeof( Cloak ),				typeof( Robe ),
				typeof( FloppyHat ),			typeof( WideBrimHat ),			typeof( Cap ),
				typeof( SkullCap ),				typeof( Bandana ),				typeof( TallStrawHat ),
				typeof( StrawHat ),				typeof( WizardsHat ),			typeof( Bonnet ),
				typeof( FeatheredHat ),			typeof( TricorneHat ),			typeof( JesterHat ),
				typeof( Bag ),					typeof( Basket ),				typeof( WoodenBox ),
				typeof( SmallCrate ),			typeof( MetalBox ),				typeof( Backpack ),			typeof( Pouch ),
				typeof( Bag ),					typeof( Basket ),				typeof( WoodenBox ),
				typeof( SmallCrate ),			typeof( MetalBox ),				typeof( Backpack ),			typeof( Pouch ),
				typeof( Bag ),					typeof( Basket ),				typeof( WoodenBox ),
				typeof( SmallCrate ),			typeof( MetalBox ),				typeof( Backpack ),			typeof( Pouch ),
				typeof( Chessboard ),			typeof( CheckerBoard ),			typeof( Backgammon ),
				typeof( Dices ),				typeof( tarotpoker ),			typeof( MahjongGame ),		typeof( TarotDeck ),
				typeof( Beeswax ),				typeof( OilCloth ),				typeof( Scales ),
				typeof( Axle ),					typeof( AxleGears ),			typeof( ClockFrame ),
				typeof( ClockParts ),			typeof( Gears ),				typeof( SextantParts ),
				typeof( Springs ),				typeof( Fork ),					typeof( ForkLeft ),
				typeof( ForkRight ),			typeof( Spoon ),				typeof( SpoonLeft ),
				typeof( SpoonRight ),			typeof( Knife ),				typeof( KnifeLeft ),
				typeof( KnifeRight ),			typeof( Plate ),				typeof( Jug ),
				typeof( CeramicMug ),			typeof( PewterMug ),			typeof( Goblet ),
				typeof( GlassMug ),				typeof( Pitcher ),				typeof( BlueBook ), 
				typeof( Candle ),				typeof( CandleLarge ),			typeof( CandleLong ), 
				typeof( CandleShort ),			typeof( CandleSkull ),			typeof( BlueBook ),			typeof( Pillows ),
				typeof( Brazier ),				typeof( BrazierTall ),			typeof( WallTorch ),		typeof( ColoredWallTorch )
			};

		public static Type[] DungeonJunkTypes{ get{ return m_DungeonJunkTypes; } }

		public static Item RandomJunk()
		{
			return Construct( m_DungeonJunkTypes );
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private static Type[] m_DungeonOrientTypes = new Type[]
			{
				typeof( WhiteHangingLantern ),	typeof( ShojiLantern ),			typeof( RoundPaperLantern ),				
				typeof( RedHangingLantern ),	typeof( PaperLantern ),			typeof( TowerLantern ),				
				typeof( OrigamiPaper ),			typeof( WindChimes ),			typeof( FancyWindChimes ),				
				typeof( BambooScreen ),			typeof( ShojiScreen ),			typeof( OrientBasket1 ),				
				typeof( OrientBasket2 ),		typeof( OrientBasket3 ),		typeof( OrientBasket4 ),				
				typeof( OrientBasket5 ),		typeof( OrientalItems )
			};

		public static Type[] DungeonOrientTypes{ get{ return m_DungeonOrientTypes; } }

		public static Item RandomOrient()
		{
			return Construct( m_DungeonOrientTypes );
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private static Type[] m_DungeonEvilTypes = new Type[]
			{
				typeof( GuillotineDeed ),					typeof( IronMaidenDeed ),			typeof( ScarecrowDeed ),	
				typeof( WoodenCoffinDeed ),					typeof( UnsettlingPortraitDeed ),	typeof( BoneCouchDeed ),	
				typeof( AwesomeDisturbingPortraitDeed ),	typeof( BoneTableDeed ),			typeof( BoneThroneDeed ),	
				typeof( CreepyPortraitDeed ),				typeof( DisturbingPortraitDeed ),	typeof( HaunterMirrorDeed ),	
				typeof( SacrificialAltarDeed ),				typeof( BedOfNailsDeed ),			typeof( ESpikeColumnDeed ),	
				typeof( ESpikePostEastDeed ),				typeof( ESpikePostSouthDeed ),		typeof( EObsidianPillarDeed ),	
				typeof( EObsidianRockDeed ),				typeof( EShadowAltarDeed ),			typeof( EShadowBannerDeed ),	
				typeof( EShadowFirePitDeed ),				typeof( EShadowFirePitCrossDeed ),	typeof( EShadowPillarDeed ),	
				typeof( BloodyPentagramDeed ),				typeof( EvilItems ),				typeof( EvilItems ),
				typeof( EvilItems ),						typeof( EvilItems ),				typeof( EvilItems ),
				typeof( BrokenArmoire ),					typeof( BrokenBookcase ),			typeof( BrokenDrawer ),
				typeof( NecromancerBanner ),				typeof( NecromancerTable ),			typeof( BloodPentagramDeed ),
				typeof( BloodyTableAddonDeed ),				typeof( DeadBodyEWDeed ),			typeof( DeadBodyNSDeed ),
				typeof( EvilFireplaceSouthFaceAddonDeed ),	typeof( HalloweenBlood ),			typeof( HalloweenBonePileDeed ),
				typeof( HalloweenChopper ),					typeof( HalloweenColumn ),			typeof( HalloweenGrave1 ),
				typeof( HalloweenGrave2 ),					typeof( HalloweenGrave3 ),			typeof( HalloweenMaiden ),
				typeof( HalloweenPylon ),					typeof( HalloweenPylonFire ),		typeof( HalloweenShrineChaosDeed ),
				typeof( HalloweenSkullPole ),				typeof( HalloweenStoneColumn ),		typeof( HalloweenStoneSpike ),
				typeof( HalloweenStoneSpike2 ),				typeof( HalloweenTortSkel ),		typeof( halloween_coffin_eastAddonDeed ),
				typeof( halloween_block_eastAddonDeed ),	typeof( LargeDyingPlant ),			typeof( DyingPlant ),
				typeof( PumpkinScarecrow ),					typeof( GrimWarning ),				typeof( SkullsOnPike ),
				typeof( BlackCatStatue ),					typeof( RuinedTapestry ),			typeof( HalloweenWeb ),
				typeof( halloween_shackles ),				typeof( halloween_ruined_bookcase ),typeof( halloween_covered_chair ),
				typeof( halloween_HauntedMirror1 ),			typeof( halloween_HauntedMirror2 ),	typeof( halloween_devil_face ),
				typeof( BurningScarecrowA )
			};

		public static Type[] DungeonEvilTypes{ get{ return m_DungeonEvilTypes; } }

		public static Item RandomEvil()
		{
			return Construct( m_DungeonEvilTypes );
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private static Type[] m_DungeonSlayerTypes = new Type[]
			{
				typeof( SlayerDeed ), 			typeof( SlayerDeed ), 			typeof( LuckyHorseShoes )
			};

		public static Type[] DungeonSlayerTypes{ get{ return m_DungeonSlayerTypes; } }

		public static Item RandomSlayer()
		{
			return Construct( m_DungeonSlayerTypes );
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private static Type[] m_SpaceJunkTypes = new Type[]
			{
				typeof( SpaceJunkA ), 			typeof( SpaceJunkB ), 				typeof( SpaceJunkC ), 
				typeof( SpaceJunkD ), 			typeof( SpaceJunkE ), 				typeof( SpaceJunkF ), 
				typeof( SpaceJunkG ), 			typeof( SpaceJunkH ), 				typeof( SpaceJunkI ), 
				typeof( SpaceJunkJ ), 			typeof( SpaceJunkK ), 				typeof( SpaceJunkL ), 
				typeof( SpaceJunkM ), 			typeof( SpaceJunkN ), 				typeof( SpaceJunkO ), 
				typeof( SpaceJunkP ), 			typeof( SpaceJunkQ ), 				typeof( SpaceJunkR ), 
				typeof( SpaceJunkS ), 			typeof( SpaceJunkT ), 				typeof( SpaceJunkU ), 
				typeof( SpaceJunkV ), 			typeof( SpaceJunkW ), 				typeof( SpaceJunkX ), 
				typeof( SpaceJunkY ), 			typeof( SpaceJunkZ ),

				typeof( SpaceJunkA ), 			typeof( SpaceJunkB ), 				typeof( SpaceJunkC ), 
				typeof( SpaceJunkD ), 			typeof( SpaceJunkE ), 				typeof( SpaceJunkF ), 
				typeof( SpaceJunkG ), 			typeof( SpaceJunkH ), 				typeof( SpaceJunkI ), 
				typeof( SpaceJunkJ ), 			typeof( SpaceJunkK ), 				typeof( SpaceJunkL ), 
				typeof( SpaceJunkM ), 			typeof( SpaceJunkN ), 				typeof( SpaceJunkO ), 
				typeof( SpaceJunkP ), 			typeof( SpaceJunkQ ), 				typeof( SpaceJunkR ), 
				typeof( SpaceJunkS ), 			typeof( SpaceJunkT ), 				typeof( SpaceJunkU ), 
				typeof( SpaceJunkV ), 			typeof( SpaceJunkW ), 				typeof( SpaceJunkX ), 
				typeof( SpaceJunkY ), 			typeof( SpaceJunkZ ),

				typeof( SpaceJunkA ), 			typeof( SpaceJunkB ), 				typeof( SpaceJunkC ), 
				typeof( SpaceJunkD ), 			typeof( SpaceJunkE ), 				typeof( SpaceJunkF ), 
				typeof( SpaceJunkG ), 			typeof( SpaceJunkH ), 				typeof( SpaceJunkI ), 
				typeof( SpaceJunkJ ), 			typeof( SpaceJunkK ), 				typeof( SpaceJunkL ), 
				typeof( SpaceJunkM ), 			typeof( SpaceJunkN ), 				typeof( SpaceJunkO ), 
				typeof( SpaceJunkP ), 			typeof( SpaceJunkQ ), 				typeof( SpaceJunkR ), 
				typeof( SpaceJunkS ), 			typeof( SpaceJunkT ), 				typeof( SpaceJunkU ), 
				typeof( SpaceJunkV ), 			typeof( SpaceJunkW ), 				typeof( SpaceJunkX ), 
				typeof( SpaceJunkY ), 			typeof( SpaceJunkZ ),

				typeof( ChickenLeg ), 			typeof( Ribs ), 					typeof( RomulanAle ),
				typeof( Bandage ),				typeof( FancyHood ),
				typeof( Lockpick ), 			typeof( Jar ), 						typeof( Bottle ),
				typeof( DataPad ), 				typeof( Boots ), 					typeof( Shoes ),
				typeof( FancyShirt ), 			typeof( Shirt ), 					typeof( LongPants ),
				typeof( Skirt ), 				typeof( Surcoat ), 					typeof( Tunic ),
				typeof( FormalShirt ), 			typeof( Fork ), 					typeof( ForkLeft ),
				typeof( ForkRight ), 			typeof( Spoon ), 					typeof( SpoonLeft ),
				typeof( SpoonRight ), 			typeof( Knife ), 					typeof( KnifeLeft ),
				typeof( KnifeRight ), 			typeof( Plate ), 					typeof( GlassMug ),
				typeof( Pillows ),				typeof( Arrow ),					typeof( Bolt ),
				typeof( Bedroll ),				typeof( DirtyWaterskin ),			typeof( Waterskin ),
				typeof( SmallTent ),			typeof( CampersTent ),				typeof( PlasmaTorch ),

				typeof( SkeletonsKey ),			typeof( MasterSkeletonsKey ),		typeof( Robe ),
				typeof( ClothHood ),			typeof( ClothCowl ),				typeof( Robe ),

				typeof( Krystal ),				typeof( KilrathiHeavyGun ),			typeof( KilrathiGun ),
				typeof( LightSword ),			typeof( Spyglass ),					typeof( ArtifactManual ),
				typeof( LandmineSetup ),		typeof( PlasmaGrenade),				typeof( DataPad ),
				typeof( PuzzleCube ),			typeof( DuctTape ),					typeof( TinkerTools ),
				typeof( FirstAidKit ),			typeof( DoubleLaserSword ),			typeof( ThermalDetonator ),
				typeof( Chainsaw ),				typeof( PortableSmelter ),


				typeof( RobotBatteries ),		typeof( RobotSheetMetal ),			typeof( RobotOil ),
				typeof( RobotGears ),			typeof( RobotEngineParts ),			typeof( RobotCircuitBoard ),
				typeof( RobotBolt ),			typeof( RobotTransistor ),			typeof( MaterialLiquifier )
			};

		public static Type[] SpaceJunkTypes{ get{ return m_SpaceJunkTypes; } }

		public static Item RandomSpaceJunk()
		{
			return Construct( m_SpaceJunkTypes );
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private static Type[] m_SpaceBagTypes = new Type[]
			{
				typeof( SpaceJunkA ), 			typeof( SpaceJunkB ), 				typeof( SpaceJunkC ), 
				typeof( SpaceJunkD ), 			typeof( SpaceJunkE ), 				typeof( SpaceJunkF ), 
				typeof( SpaceJunkG ), 			typeof( SpaceJunkH ), 				typeof( SpaceJunkI ), 
				typeof( SpaceJunkJ ), 			typeof( SpaceJunkK ), 				typeof( SpaceJunkL ), 
				typeof( SpaceJunkM ), 			typeof( SpaceJunkN ), 				typeof( SpaceJunkO ), 
				typeof( SpaceJunkP ), 			typeof( SpaceJunkQ ), 				typeof( SpaceJunkR ), 
				typeof( SpaceJunkS ), 			typeof( SpaceJunkT ), 				typeof( SpaceJunkU ), 
				typeof( SpaceJunkV ), 			typeof( SpaceJunkW ), 				typeof( SpaceJunkX ), 
				typeof( SpaceJunkY ), 			typeof( SpaceJunkZ ),

				typeof( SpaceJunkA ), 			typeof( SpaceJunkB ), 				typeof( SpaceJunkC ), 
				typeof( SpaceJunkD ), 			typeof( SpaceJunkE ), 				typeof( SpaceJunkF ), 
				typeof( SpaceJunkG ), 			typeof( SpaceJunkH ), 				typeof( SpaceJunkI ), 
				typeof( SpaceJunkJ ), 			typeof( SpaceJunkK ), 				typeof( SpaceJunkL ), 
				typeof( SpaceJunkM ), 			typeof( SpaceJunkN ), 				typeof( SpaceJunkO ), 
				typeof( SpaceJunkP ), 			typeof( SpaceJunkQ ), 				typeof( SpaceJunkR ), 
				typeof( SpaceJunkS ), 			typeof( SpaceJunkT ), 				typeof( SpaceJunkU ), 
				typeof( SpaceJunkV ), 			typeof( SpaceJunkW ), 				typeof( SpaceJunkX ), 
				typeof( SpaceJunkY ), 			typeof( SpaceJunkZ ),

				typeof( RobotBatteries ),		typeof( RobotSheetMetal ),			typeof( RobotOil ),
				typeof( RobotGears ),			typeof( RobotEngineParts ),			typeof( RobotCircuitBoard ),
				typeof( RobotBolt ),			typeof( RobotTransistor )
			};

		public static Type[] SpaceBagTypes{ get{ return m_SpaceBagTypes; } }

		public static Item RandomSpaceBag()
		{
			return Construct( m_SpaceBagTypes );
		}


		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////


		private static Type[] m_SpaceCrashTypes = new Type[]
			{
				typeof( SpaceJunkA ), 			typeof( SpaceJunkB ), 				typeof( SpaceJunkC ), 
				typeof( SpaceJunkD ), 			typeof( SpaceJunkE ), 				typeof( SpaceJunkF ), 
				typeof( SpaceJunkG ), 			typeof( SpaceJunkH ), 				typeof( SpaceJunkI ), 
				typeof( SpaceJunkJ ), 			typeof( SpaceJunkK ), 				typeof( SpaceJunkL ), 
				typeof( SpaceJunkM ), 			typeof( SpaceJunkN ), 				typeof( SpaceJunkO ), 
				typeof( SpaceJunkP ), 			typeof( SpaceJunkQ ), 				typeof( SpaceJunkR ), 
				typeof( SpaceJunkS ), 			typeof( SpaceJunkT ), 				typeof( SpaceJunkU ), 
				typeof( SpaceJunkV ), 			typeof( SpaceJunkW ), 				typeof( SpaceJunkX ), 
				typeof( SpaceJunkY ), 			typeof( SpaceJunkZ ),


				typeof( RomulanAle ),			typeof( PlasmaTorch ),
				typeof( Krystal ),				typeof( KilrathiHeavyGun ),			typeof( KilrathiGun ),
				typeof( LightSword ),			typeof( Chainsaw ),					typeof( PortableSmelter ),
				typeof( LandmineSetup ),		typeof( PlasmaGrenade),
				typeof( PuzzleCube ),			typeof( DuctTape ),					typeof( TinkerTools ),
				typeof( FirstAidKit ),			typeof( DoubleLaserSword ),			typeof( ThermalDetonator ),
				typeof( RobotBatteries ),		typeof( RobotSheetMetal ),			typeof( RobotOil ),
				typeof( RobotGears ),			typeof( RobotEngineParts ),			typeof( RobotCircuitBoard ),
				typeof( RobotBolt ),			typeof( RobotTransistor ),			typeof( MaterialLiquifier )
			};


		public static Type[] SpaceCrashTypes{ get{ return m_SpaceCrashTypes; } }


		public static Item RandomSpaceCrash()
		{
			return Construct( m_SpaceCrashTypes );
		}


		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static Item Construct( Type type )
		{
			try
			{
				return Activator.CreateInstance( type ) as Item;
			}
			catch
			{
				return null;
			}
		}

		public static Item Construct( Type[] types )
		{
			if ( types.Length > 0 )
				return Construct( types, Utility.Random( types.Length ) );

			return null;
		}

		public static Item Construct( Type[] types, int index )
		{
			if ( index >= 0 && index < types.Length )
				return Construct( types[index] );

			return null;
		}

		public static Item Construct( params Type[][] types )
		{
			int totalLength = 0;

			for ( int i = 0; i < types.Length; ++i )
				totalLength += types[i].Length;

			if ( totalLength > 0 )
			{
				int index = Utility.Random( totalLength );

				for ( int i = 0; i < types.Length; ++i )
				{
					if ( index >= 0 && index < types[i].Length )
						return Construct( types[i][index] );

					index -= types[i].Length;
				}
			}

			return null;
		}
	}
}
