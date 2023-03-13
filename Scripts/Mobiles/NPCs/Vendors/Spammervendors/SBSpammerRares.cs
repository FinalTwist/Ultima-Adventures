using System;
using System.Collections.Generic;
using Server.Items;
using Server.Misc;
//using Server.Items.MusicBox;
using Server.Custom;

namespace Server.Mobiles
{
public class SBSpammerRares : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSpammerRares()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{  
				//Add (new GenericBuyInfo( typeof( DawnsMusicBox ), 150000, 20, 0x2AF9, 0 ) );
				Add (new GenericBuyInfo( typeof( RandomTranscendence ), 10000, 20, 0x14F0, 0 ) );
				Add (new GenericBuyInfo( typeof( RandomAlacrity ), 50000, 20, 0x14F0, 0 ) );

				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( EtherealLlama ), 2400000, 1, 0x20F6, 2858 ) );}
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( EtherealHorse ), 2500000, 1, 0x20DD, 2858 ) );}
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EtherealOstard ), 3500000, 1, 0x2135, 2858 ) );}
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EtherealRidgeback ), 3600000, 1, 0x2615, 2858 ) );}
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ClothingBlessDeed ), 750000, 1, 0x14F0, 0 ) );}
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ClothingBlessDeed ), 1250000, 1, 0x14F0, 0 ) );}
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PerfectMiningHarvester ), 750000, Utility.Random( 1,2 ), 0x5484, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PerfectLumberHarvester ), 550000, Utility.Random( 1,2 ), 0x5486, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PerfectHideHarvester ), 650000, Utility.Random( 1,2 ), 0x5487, 0 ) ); }
				//Add( new GenericBuyInfo( typeof(  ), 10000, 10, 0x14F0, 0 ) );
				//Add( new GenericBuyInfo( typeof( ScrollofAlacrity.CreateRandom() ), 50000, 10, 0x14F0, 0 ) );
/*20, 0x14F0, 0 )
				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( AssassinSpikeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( AxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BardicheOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BattleAxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BlackStaffOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BladedStaffOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BokutoOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BoneHarvesterOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BroadswordOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ButcherKnifeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( CleaverOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ClubOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( CompositeBowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( CrescentBladeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( CrossbowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( CutlassOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( DaggerOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( DaishoOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( DiamondMaceOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( DoubleAxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( DoubleBladedStaffOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ElvenCompositeLongbowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ElvenMacheteOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ElvenSpellbladeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ExecutionersAxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( GlacialStaffOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( GnarledStaffOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( HalberdOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( HammerPickOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( HatchetOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( HeavyCrossbowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( JukaBowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( KamaOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( KatanaOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( KryssOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( LajatangOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( LanceOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( LargeBattleAxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( LeafbladeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( LongswordOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( MaceOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( MagicalShortbowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( MaulOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( NoDachiOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( NunchakuOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( OrnateAxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( PikeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( PitchforkOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( QuarterStaffOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( RadiantScimitarOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( RepeatingCrossbowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( RuneBladeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( SaiOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ScepterOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ScimitarOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ScytheOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ShepherdsCrookOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ShortbowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ShortSpearOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( SkinningKnifeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( SpearOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( TekagiOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( TessenOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( TetsuboOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ThinLongswordOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( TribalSpearOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( TwoHandedAxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( VikingSwordOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( WakizashiOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( WarAxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( WarCleaverOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( WarForkOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( WarHammerOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( WarMaceOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( WildStaffOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( YumiOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}
				*/

			}
		}
				

	public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Aegis ), 15000 );
				Add( typeof( AlchemistsBauble ), 5000 );
				Add( typeof( Antiquity ), 15000 );
				Add( typeof( ArcaneShield ), 15000 );
				Add( typeof( ArcticDeathDealer ), 5000 );
				Add( typeof( ArmorOfFortune ), 15000 );
				Add( typeof( AxeOfTheHeavens ), 15000 );
				Add( typeof( BarbedSewingKit ), 100000 );
				Add( typeof( BladeOfInsanity ), 15000 );
				Add( typeof( BlazeOfDeath ), 5000 );
				Add( typeof( BloodTrail ), 15000 );
				Add( typeof( BoneCrusher ), 15000 );
				Add( typeof( BowOfHarps ), 15000 );
				Add( typeof( BraceletOfHealth ), 15000 );
				Add( typeof( BreathOfTheDead ), 15000 );
				Add( typeof( BurglarsBandana ), 5000 );
				Add( typeof( CandelabraOfSouls ), 5000 );
				Add( typeof( CavortingClub ), 5000 );
				Add( typeof( ChildOfDeath ), 15000 );
				Add( typeof( ColdBlood ), 5000 );
				Add( typeof( CrystallineBlackrock ), 1500 );
				Add( typeof( DivineCountenance ), 15000 );
				Add( typeof( DreadPirateHat ), 5000 );
				Add( typeof( EnchantedTitanLegBone ), 5000 );
				Add( typeof( Erotica ), 15000 );
				Add( typeof( FeverFall ), 15000 );
				Add( typeof( Frostbringer ), 15000 );
				Add( typeof( GauntletsOfNobility ), 15000 );
				Add( typeof( GlovesOfTheHardWorker ), 15000 );
				Add( typeof( GoldBricks ), 5000 );
				Add( typeof( GwennosHarp ), 5000 );
				Add( typeof( HandsofTabulature ), 15000 );
				Add( typeof( HatOfTheMagi ), 15000 );
				Add( typeof( HeartOfTheLion ), 5000 );
				Add( typeof( HelmOfInsight ), 15000 );
				Add( typeof( HolyKnightsBreastplate ), 15000 );
				Add( typeof( HornedSewingKit ), 10000 );
				Add( typeof( HuntersHeaddress ), 15000 );
				Add( typeof( IolosLute ), 5000 );
				Add( typeof( JackalsCollar ), 15000 );
				Add( typeof( Kamadon ), 25000 );
				Add( typeof( LaFemme ), 15000 );
				Add( typeof( LeggingsOfBane ), 15000 );
				Add( typeof( LunaLance ), 5000 );
				Add( typeof( MidnightBracers ), 15000 );
				Add( typeof( MonsterStatuette ), 20000 ); 
				Add( typeof( NightsKiss ), 5000 );
				Add( typeof( NoxRangersHeavyCrossbow ), 5000 );
				Add( typeof( OrnamentOfTheMagician ), 15000 );
				Add( typeof( OrnateCrownOfTheHarrower ), 15000 );
				Add( typeof( PhillipsWoodenSteed ), 5000 );
				Add( typeof( PolarBearMask ), 5000 );
				Add( typeof( PurposeOfPain ), 20000 );
				Add( typeof( Revenge ), 15000 );
				Add( typeof( RingOfTheElements ), 15000 );
				Add( typeof( RingOfTheVile ), 15000 );
				Add( typeof( SatanicHelm ), 15000 );
				Add( typeof( SerpentsFang ), 15000 );
				Add( typeof( ShadowDancerLeggings ), 10000 );
				Add( typeof( SpinedSewingKit ), 1000 );
				Add( typeof( SpiritOfTheTotem ), 15000 );
				Add( typeof( StaffOfTheMagi ), 10000 );
				Add( typeof( StandStill ), 15000 );
				Add( typeof( TacticalMask ), 15000 );
				Add( typeof( TheBeserkersMaul ), 10000 );
				Add( typeof( TheDragonSlayer ), 15000 );
				Add( typeof( TheDryadBow ), 10000 );
				Add( typeof( ThickNeck ), 15000 );
				Add( typeof( TunicOfFire ), 15000 );
				Add( typeof( ValasCompromise ), 15000 );
				Add( typeof( Valicious ), 15000 );
				Add( typeof( VioletCourage ), 5000 );
				Add( typeof( VoiceOfTheFallenKing ), 15000 );
				Add( typeof( WizardsStrongArm ), 15000 );
				Add( typeof( AbysmalGloves ), 12000 );
				Add( typeof( AchillesShield ), 12000 );
				Add( typeof( AchillesSpear ), 12000 );
				Add( typeof( AcidProofRobe ), 12000 );
				Add( typeof( AegisOfGrace ), 12000 );
				Add( typeof( AilricsLongbow ), 12000 );
				Add( typeof( AngelicEmbrace ), 12000 );
				Add( typeof( AngeroftheGods ), 12000 );
				Add( typeof( Annihilation ), 12000 );
				Add( typeof( ArcaneArms ), 12000 );
				Add( typeof( ArcaneCap ), 12000 );
				Add( typeof( ArcaneGloves ), 12000 );
				Add( typeof( ArcaneGorget ), 12000 );
				Add( typeof( ArcaneLeggings ), 12000 );
				Add( typeof( ArcaneTunic ), 12000 );
				Add( typeof( ArcanicRobe ), 12000 );
				Add( typeof( ArcticBeacon ), 12000 );
				Add( typeof( ArmorOfInsight ), 12000 );
				Add( typeof( ArmorOfNobility ), 12000 );
				Add( typeof( ArmsOfAegis ), 12000 );
				Add( typeof( ArmsOfFortune ), 12000 );
				Add( typeof( ArmsOfInsight ), 12000 );
				Add( typeof( ArmsOfNobility ), 12000 );
				Add( typeof( ArmsOfTheFallenKing ), 12000 );
				Add( typeof( ArmsOfTheHarrower ), 12000 );
				Add( typeof( ArmsOfToxicity ), 12000 );
				Add( typeof( AuraOfShadows ), 12000 );
				Add( typeof( AxeoftheMinotaur ), 12000 );
				Add( typeof( BagOfHolding ), 12000 );
				Add( typeof( BalancingDeed ), 12000 );
				Add( typeof( BeggarsRobe ), 12000 );
				Add( typeof( BeltofHercules ), 12000 );
				Add( typeof( BladeDance ), 12000 );
				Add( typeof( BladeOfTheRighteous ), 12000 );
				Add( typeof( BlightGrippedLongbow ), 12000 );
				Add( typeof( BloodwoodSpirit ), 12000 );
				Add( typeof( Bonesmasher ), 12000 );
				Add( typeof( BookOfKnowledge ), 12000 );
				Add( typeof( Boomstick ), 12000 );
				Add( typeof( BootsofHermes ), 12000 );
				Add( typeof( BottomlessBargainBucket ), 12000 );
				Add( typeof( BowOfTheJukaKing ), 12000 );
				Add( typeof( BowofthePhoenix ), 12000 );
				Add( typeof( BraceletOfTheElements ), 12000 );
				Add( typeof( BraceletOfTheVile ), 12000 );
				Add( typeof( BurglarsBandana ), 12000 );
				Add( typeof( Calm ), 12000 );
				Add( typeof( CandleCold ), 12000 );
				Add( typeof( CandleEnergy ), 12000 );
				Add( typeof( CandleFire ), 12000 );
				Add( typeof( CandleNecromancer ), 12000 );
				Add( typeof( CandlePoison ), 12000 );
				Add( typeof( CandleWizard ), 12000 );
				Add( typeof( CapOfFortune ), 12000 );
				Add( typeof( CapOfTheFallenKing ), 12000 );
				Add( typeof( CaptainQuacklebushsCutlass ), 12000 );
				Add( typeof( CircletOfTheSorceress ), 12000 );
				Add( typeof( CoifOfBane ), 12000 );
				Add( typeof( CoifOfFire ), 12000 );
				Add( typeof( ColdForgedBlade ), 12000 );
				Add( typeof( ColoringBook ), 12000 );
				Add( typeof( ConansHelm ), 12000 );
				Add( typeof( ConansLoinCloth ), 12000 );
				Add( typeof( ConansSword ), 12000 );
				Add( typeof( CrimsonCincture ), 12000 );
				Add( typeof( CrownOfTalKeesh ), 12000 );
				Add( typeof( DaggerOfVenom ), 12000 );
				Add( typeof( DarkGuardiansChest ), 12000 );
				Add( typeof( DarkLordsPitchfork ), 12000 );
				Add( typeof( DarkNeck ), 12000 );
				Add( typeof( DeathsMask ), 12000 );
				Add( typeof( DivineArms ), 12000 );
				Add( typeof( DivineGloves ), 12000 );
				Add( typeof( DivineGorget ), 12000 );
				Add( typeof( DivineLeggings ), 12000 );
				Add( typeof( DivineTunic ), 12000 );
				Add( typeof( DjinnisRing ), 12000 );
				Add( typeof( DoubletOfPower ), 12000 );
				Add( typeof( NordicVikingSword), 12000 );
				Add( typeof( DupresCollar ), 12000 );
				Add( typeof( DupresShield ), 12000 );
				Add( typeof( EarringBoxSet ), 12000 );
				Add( typeof( EarringsOfHealth ), 12000 );
				Add( typeof( EarringsOfProtection ), 12000 );
				Add( typeof( EarringsOfTheElements ), 12000 );
				Add( typeof( EarringsOfTheMagician ), 12000 );
				Add( typeof( EarringsOfTheVile ), 12000 );
				Add( typeof( ElvenQuiver ), 12000 );
				Add( typeof( EmbroideredOakLeafCloak ), 12000 );
				Add( typeof( EssenceOfBattle ), 12000 );
				Add( typeof( EternalFlame ), 12000 );
				Add( typeof( EverlastingBottle ), 12000 );
				Add( typeof( EverlastingLoaf ), 12000 );
				Add( typeof( EvilMageGloves ), 12000 );
				Add( typeof( Excalibur ), 12000 );
				Add( typeof( FalseGodsScepter ), 12000 );
				Add( typeof( FangOfRactus ), 12000 );
				Add( typeof( FesteringWound ), 12000 );
				Add( typeof( FeyLeggings ), 12000 );
				Add( typeof( FleshRipper ), 12000 );
				Add( typeof( Fortifiedarms ), 12000 );
				Add( typeof( FortunateBlades ), 12000 );
				Add( typeof( FurCapeOfTheSorceress ), 12000 );
				Add( typeof( Fury ), 12000 );
				Add( typeof( GandalfsHat ), 12000 );
				Add( typeof( GandalfsRobe ), 12000 );
				Add( typeof( GandalfsStaff ), 12000 );
				Add( typeof( GeishasObi ), 12000 );
				Add( typeof( GemOfSeeing ), 12000 );
				Add( typeof( GiantBlackjack ), 12000 );
				Add( typeof( GladiatorsCollar ), 12000 );
				Add( typeof( GlassSword ), 12000 );
				Add( typeof( GlovesOfAegis ), 12000 );
				Add( typeof( GlovesOfCorruption ), 12000 );
				Add( typeof( GlovesOfDexterity ), 12000 );
				Add( typeof( GlovesOfFortune ), 12000 );
				Add( typeof( GlovesOfInsight ), 12000 );
				Add( typeof( GlovesOfRegeneration ), 12000 );
				Add( typeof( GlovesOfTheFallenKing ), 12000 );
				Add( typeof( GlovesOfTheHarrower ), 12000 );
				Add( typeof( GlovesOfThePugilist ), 12000 );
				Add( typeof( GorgetOfAegis ), 12000 );
				Add( typeof( GorgetOfFortune ), 12000 );
				Add( typeof( GorgetOfInsight ), 12000 );
				Add( typeof( GrayMouserCloak ), 12000 );
				Add( typeof( GrimReapersMask ), 12000 );
				Add( typeof( GrimReapersRobe ), 12000 );
				Add( typeof( GrimReapersScythe ), 12000 );
				Add( typeof( GuantletsOfAnger ), 12000 );
				Add( typeof( HammerofThor ), 12000 );
				Add( typeof( HellForgedArms ), 12000 );
				Add( typeof( HelmOfAegis ), 12000 );
				Add( typeof( HelmOfBrilliance ), 12000 );
				Add( typeof( HelmOfSwiftness ), 12000 );
				Add( typeof( HolyKnightsArmPlates ), 12000 );
				Add( typeof( HolyKnightsGloves ), 12000 );
				Add( typeof( HolyKnightsGorget ), 12000 );
				Add( typeof( HolyKnightsLegging ), 12000 );
				Add( typeof( HolyKnightsPlateHelm ), 12000 );
				Add( typeof( HolySword ), 12000 );
				Add( typeof( HuntersArms ), 12000 );
				Add( typeof( HuntersGloves ), 12000 );
				Add( typeof( HuntersGorget ), 12000 );
				Add( typeof( HuntersLeggings ), 12000 );
				Add( typeof( HuntersTunic ), 12000 );
				Add( typeof( Indecency ), 12000 );
				Add( typeof( InquisitorsArms ), 12000 );
				Add( typeof( InquisitorsGorget ), 12000 );
				Add( typeof( InquisitorsHelm ), 12000 );
				Add( typeof( InquisitorsLeggings ), 12000 );
				Add( typeof( InquisitorsResolution ), 12000 );
				Add( typeof( InquisitorsTunic ), 12000 );
				Add( typeof( JackalsArms ), 12000 );
				Add( typeof( JackalsGloves ), 12000 );
				Add( typeof( JackalsHelm ), 12000 );
				Add( typeof( JackalsLeggings ), 12000 );
				Add( typeof( JackalsTunic ), 12000 );
				Add( typeof( JadeScimitar ), 12000 );
				Add( typeof( JesterHatofChuckles ), 12000 );
				Add( typeof( JinBaoriOfGoodFortune ), 12000 );
				Add( typeof( KamiNarisIndestructableDoubleAxe ), 12000 );
				Add( typeof( KodiakBearMask ), 12000 );
				Add( typeof( LargeBagofHolding ), 12000 );
				Add( typeof( LegacyOfTheDreadLord ), 12000 );
				Add( typeof( LeggingsOfAegis ), 12000 );
				Add( typeof( LeggingsOfDeceit ), 12000 );
				Add( typeof( LeggingsOfEnlightenment ), 12000 );
				Add( typeof( LeggingsOfFire ), 12000 );
				Add( typeof( LegsOfFortune ), 12000 );
				Add( typeof( LegsOfInsight ), 12000 );
				Add( typeof( LegsOfNobility ), 12000 );
				Add( typeof( LegsOfTheFallenKing ), 12000 );
				Add( typeof( LegsOfTheHarrower ), 12000 );
				Add( typeof( LieutenantOfTheBritannianRoyalGuard ), 12000 );
				Add( typeof( LongShot ), 12000 );
				Add( typeof( LuckyEarrings ), 12000 );
				Add( typeof( LuckyNecklace ), 12000 );
				Add( typeof( LuminousRuneBlade ), 12000 );
				Add( typeof( MadmansHatchet ), 12000 );
				Add( typeof( MagesBand ), 12000 );
				Add( typeof( MagiciansIllusion ), 12000 );
				Add( typeof( MagiciansMempo ), 12000 );
				Add( typeof( MagusShirt ), 12000 );
				Add( typeof( MarbleShield ), 12000 );
				Add( typeof( MauloftheBeast ), 12000 );
				Add( typeof( MaulOfTheTitans ), 12000 );
				Add( typeof( MediumBagofHolding ), 12000 );
				Add( typeof( MelisandesCorrodedHatchet ), 12000 );
				Add( typeof( MIBHunter ), 12000 );
				Add( typeof( MidnightGloves ), 12000 );
				Add( typeof( MidnightHelm ), 12000 );
				Add( typeof( MidnightLegs ), 12000 );
				Add( typeof( MidnightTunic ), 12000 );
				Add( typeof( MinersPickaxe ), 12000 );
				Add( typeof( NordicVikingSword ), 12000 );
				Add( typeof( NoxBow ), 12000 );
				Add( typeof( NoxNightlight ), 12000 );
				Add( typeof( OblivionsNeedle ), 12000 );
				Add( typeof( OrcChieftainHelm ), 12000 );
				Add( typeof( OrcishVisage ), 12000 );
				Add( typeof( OverseerSunderedBlade ), 12000 );
				Add( typeof( Pacify ), 12000 );
				Add( typeof( PandorasBox ), 12000 );
				Add( typeof( PendantOfTheMagi ), 12000 );
				Add( typeof( Pestilence ), 12000 );
				Add( typeof( PhantomStaff ), 12000 );
				Add( typeof( PixieSwatter ), 12000 );
				Add( typeof( PolarBearBoots ), 12000 );
				Add( typeof( PolarBearCape ), 12000 );
				Add( typeof( PowerSurge ), 12000 );
				Add( typeof( PrincessIllusion ), 12000 );
				Add( typeof( Quell ), 12000 );
				Add( typeof( QuiGonsLightSword ), 12000 );
				Add( typeof( QuiverOfBlight ), 12000 );
				Add( typeof( QuiverOfElements ), 12000 );
				Add( typeof( QuiverOfFire ), 12000 );
				Add( typeof( QuiverOfIce ), 12000 );
				Add( typeof( QuiverOfInfinity ), 12000 );
				Add( typeof( QuiverOfLightning ), 12000 );
				Add( typeof( QuiverOfRage ), 12000 );
				Add( typeof( RaedsGlory ), 12000 );
				Add( typeof( RamusNecromanticScalpel ), 12000 );
				Add( typeof( ResilientBracer ), 12000 );
				Add( typeof( Retort ), 12000 );
				Add( typeof( RighteousAnger ), 12000 );
				Add( typeof( RingOfHealth ), 12000 );
				Add( typeof( RingOfTheMagician ), 12000 );
				Add( typeof( RobeOfTeleportation ), 12000 );
				Add( typeof( RobeOfTheEclipse ), 12000 );
				Add( typeof( RobeOfTheEquinox ), 12000 );
				Add( typeof( RobeOfTreason ), 12000 );
				Add( typeof( RobinHoodsBow ), 12000 );
				Add( typeof( RobinHoodsFeatheredHat ), 12000 );
				Add( typeof( RodOfResurrection ), 12000 );
				Add( typeof( RoyalArchersBow ), 12000 );
				Add( typeof( RoyalGuardsChestplate ), 12000 );
				Add( typeof( RoyalGuardsGorget ), 12000 );
				Add( typeof( RoyalGuardSurvivalKnife ), 12000 );
				Add( typeof( RuneCarvingKnife ), 12000 );
				Add( typeof( SamaritanRobe ), 12000 );
				Add( typeof( SamuraiHelm ), 12000 );
				Add( typeof( ShadowBlade ), 12000 );
				Add( typeof( ShadowDancerArms ), 12000 );
				Add( typeof( ShadowDancerCap ), 12000 );
				Add( typeof( ShadowDancerGloves ), 12000 );
				Add( typeof( ShadowDancerGorget ), 12000 );
				Add( typeof( ShadowDancerTunic ), 12000 );
				Add( typeof( ShaminoCrossbow ), 12000 );
				Add( typeof( ShardThrasher ), 12000 );
				Add( typeof( ShieldOfIce ), 12000 );
				Add( typeof( ShieldOfInvulnerability ), 12000 );
				Add( typeof( ShimmeringTalisman ), 12000 );
				Add( typeof( ShroudOfDeciet ), 12000 );
				Add( typeof( SilvanisFeywoodBow ), 12000 );
				Add( typeof( SinbadsSword ), 12000 );
				Add( typeof( SmallBagofHolding ), 12000 );
				Add( typeof( sotedeathstarrelic ), 12000 );
				Add( typeof( soter2d2relic ), 12000 );
				Add( typeof( SoulSeeker ), 12000 );
				Add( typeof( SprintersSandals ), 12000 );
				Add( typeof( StaffOfPower ), 12000 );
				Add( typeof( StaffofSnakes ), 12000 );
				Add( typeof( Stormbringer ), 12000 );
				Add( typeof( StreetFightersVest ), 12000 );
				Add( typeof( Subdue ), 12000 );
				Add( typeof( SwiftStrike ), 12000 );
				Add( typeof( SwordOfIce ), 12000 );
				Add( typeof( TalonBite ), 12000 );
				Add( typeof( TenguHakama ), 12000 );
				Add( typeof( TheNightReaper ), 12000 );
				Add( typeof( TheRobeOfBritanniaAri ), 12000 );
				Add( typeof( TheTaskmaster ), 12000 );
				Add( typeof( ThinkingMansKilt ), 12000 );
				Add( typeof( TitansHammer ), 12000 );
				Add( typeof( TorchOfTrapFinding ), 12000 );
				Add( typeof( TotemArms ), 12000 );
				Add( typeof( TotemGloves ), 12000 );
				Add( typeof( TotemGorget ), 12000 );
				Add( typeof( TotemLeggings ), 12000 );
				Add( typeof( TotemOfVoid ), 12000 );
				Add( typeof( TotemTunic ), 12000 );
				Add( typeof( TownGuardsHalberd ), 12000 );
				Add( typeof( TunicOfAegis ), 12000 );
				Add( typeof( TunicOfBane ), 12000 );
				Add( typeof( TunicOfTheFallenKing ), 12000 );
				Add( typeof( TunicOfTheHarrower ), 12000 );
				Add( typeof( VampiresRobe ), 12000 );
				Add( typeof( VampiricDaisho ), 12000 );
				Add( typeof( VeryFancyShirt ), 12000 );
				Add( typeof( WarriorsClasp ), 12000 );
				Add( typeof( WeaponRenamingTool ), 12000 );
				Add( typeof( WildfireBow ), 12000 );
				Add( typeof( Windsong ), 12000 );
				Add( typeof( WindSpirit ), 12000 );
				Add( typeof( WizardsPants ), 12000 );
				Add( typeof( WrathOfTheDryad ), 12000 );
				Add( typeof( YashimotosHatsuburi ), 12000 );
				Add( typeof( ZyronicClaw ), 12000 );
			}
		}
	}
}
	