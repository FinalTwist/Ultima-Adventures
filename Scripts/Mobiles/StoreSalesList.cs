using System;
using System.Collections.Generic;
using System.Collections;
using Server.Engines.Apiculture;
using Server.Items;
using Server.Misc;
using Server.Custom;
using Server.Multis;
using Server.Guilds;
using Server.Engines.Mahjong;

namespace Server.Mobiles
{
	public abstract class SBInfo
	{
		public SBInfo()
		{
		}

		public abstract IShopSellInfo SellInfo { get; }

        public abstract List<GenericBuyInfo> BuyInfo { get; }
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	public class SBDoomVarietyDealer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBDoomVarietyDealer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Bandage ), 10, 999, 0xE21, 0 ) );

				Add( new GenericBuyInfo( typeof( BlankScroll ), 5, 999, 0x0E34, 0 ) );
				Add( new GenericBuyInfo( typeof( HoodedShroudOfShadows ), 50000, 1, 0x455, 0 ) );
				Add( new GenericBuyInfo( typeof( ChargerOfTheFallen ), 2000000, 1, 0x2D9C, 0 ) );

				Add( new GenericBuyInfo( typeof( NightSightPotion ), 500, Utility.RandomMinMax(50,200), 0xF06, 0 ) );
				Add( new GenericBuyInfo( typeof( AgilityPotion ), 500, Utility.RandomMinMax(50,200), 0xF08, 0 ) );
				Add( new GenericBuyInfo( typeof( StrengthPotion ), 500, Utility.RandomMinMax(50,200), 0xF09, 0 ) );
				Add( new GenericBuyInfo( typeof( RefreshPotion ), 500, Utility.RandomMinMax(50,200), 0xF0B, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserCurePotion ), 500, Utility.RandomMinMax(50,200), 0xF07, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserHealPotion ), 500, Utility.RandomMinMax(50,200), 0xF0C, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserPoisonPotion ), 500, Utility.RandomMinMax(50,200), 0xF0A, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserExplosionPotion ), 500, Utility.RandomMinMax(50,200), 0xF0D, 0 ) );
				Add( new GenericBuyInfo( typeof( GreaterExplosionPotion ), 1500, Utility.RandomMinMax(50,200), 0xF0D, 0 ) );

				Add( new GenericBuyInfo( typeof( Bolt ), 50, Utility.Random( 2000, 5000 ), 0x1BFB, 0 ) );
				Add( new GenericBuyInfo( typeof( Arrow ), 50, Utility.Random( 2000, 5000 ), 0xF3F, 0 ) );

				Add( new GenericBuyInfo( typeof( BlackPearl ), 150, Utility.Random( 2000, 5000 ), 0xF7A, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Bloodmoss ), 150, Utility.Random( 2000, 5000 ), 0xF7B, 0 ) ); 
				Add( new GenericBuyInfo( typeof( MandrakeRoot ), 150, Utility.Random( 2000, 5000 ), 0xF86, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Garlic ), 150, Utility.Random( 2000, 5000 ), 0xF84, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Ginseng ), 150, Utility.Random( 2000, 5000 ), 0xF85, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Nightshade ), 150, Utility.Random( 2000, 5000 ), 0xF88, 0 ) ); 
				Add( new GenericBuyInfo( typeof( SpidersSilk ), 150, Utility.Random( 2000, 5000 ), 0xF8D, 0 ) ); 
				Add( new GenericBuyInfo( typeof( SulfurousAsh ), 150, Utility.Random( 2000, 5000 ), 0xF8C, 0 ) ); 

				Add( new GenericBuyInfo( typeof( BreadLoaf ), 1000, Utility.RandomMinMax(50,200), 0x103B, 0 ) );
				Add( new GenericBuyInfo( typeof( MeatPie ), 1500, Utility.RandomMinMax(50,200), 0x103B, 0 ) );
				Add( new GenericBuyInfo( typeof( Backpack ), 150, 20, 0x9B2, 0 ) );

				Type[] types = Loot.RegularScrollTypes;

				int circles = 3;

				for ( int i = 0; i < circles*8 && i < types.Length; ++i )
				{
					int itemID = 0x1F2E + i;

					if ( i == 6 )
						itemID = 0x1F2D;
					else if ( i > 6 )
						--itemID;

					Add( new GenericBuyInfo( types[i], 100 + ((i / 8) * 10), 20, itemID, 0 ) );
				}
				
					Add( new GenericBuyInfo( typeof( BatWing ), 130, 999, 0xF78, 0 ) );
					Add( new GenericBuyInfo( typeof( GraveDust ), 130, 999, 0xF8F, 0 ) );
					Add( new GenericBuyInfo( typeof( DaemonBlood ), 160, 999, 0xF7D, 0 ) );
					Add( new GenericBuyInfo( typeof( NoxCrystal ), 160, 999, 0xF8E, 0 ) );
					Add( new GenericBuyInfo( typeof( PigIron ), 150, 999, 0xF8A, 0 ) );

				Add( new GenericBuyInfo( typeof( RecallRune ), 750, 10, 0x1f14, 0 ) );
				Add( new GenericBuyInfo( typeof( FullMagerySpellbook ), 250000, 10, 0xEFA, 0 ) );
				Add( new GenericBuyInfo( typeof( FullNecroSpellbook ), 350000, 10, 0xEFA, 0 ) );

				Add( new GenericBuyInfo( "1041072", typeof( MagicWizardsHat ), 110, 10, 0x1718, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Bandage ), 3 );

				Add( typeof( BlankScroll ), 3 );

				Add( typeof( NightSightPotion ), 70 );
				Add( typeof( AgilityPotion ), 70 );
				Add( typeof( StrengthPotion ), 70 );
				Add( typeof( RefreshPotion ), 70 );
				Add( typeof( LesserCurePotion ), 70 );
				Add( typeof( LesserHealPotion ), 70 );
				Add( typeof( LesserPoisonPotion ), 70 );
				Add( typeof( LesserExplosionPotion ), 100 );

				Add( typeof( Bolt ), 5 );
				Add( typeof( Arrow ), 4 );

				Add( typeof( BlackPearl ), 9 );
				Add( typeof( Bloodmoss ), 9 );
				Add( typeof( MandrakeRoot ), 9 );
				Add( typeof( Garlic ), 9 );
				Add( typeof( Ginseng ), 9 );
				Add( typeof( Nightshade ), 9 );
				Add( typeof( SpidersSilk ), 9 );
				Add( typeof( SulfurousAsh ), 9 );

				Add( typeof( BreadLoaf ), 30 );
				Add( typeof( Backpack ), 70 );
				Add( typeof( RecallRune ), 30 );
				Add( typeof( Spellbook ), 90 );
				Add( typeof( BlankScroll ), 3 );

				if ( Core.AOS )
				{
					Add( typeof( BatWing ), 9 );
					Add( typeof( GraveDust ), 9 );
					Add( typeof( DaemonBlood ), 9 );
					Add( typeof( NoxCrystal ), 9 );
					Add( typeof( PigIron ), 9 );
				}

				Type[] types = Loot.RegularScrollTypes;

				for ( int i = 0; i < types.Length; ++i )
					Add( types[i], ((i / 8) + 2) * 5 );
			}
		}
	}	
	
	public class SBElfRares: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBElfRares()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( Futon ), Utility.Random( 50000,150000 ), 1, 0x295C, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ArtifactVase ), Utility.Random( 50000,150000 ), 1, 0x0B48, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ArtifactLargeVase ), Utility.Random( 50000,150000 ), 1, 0x0B47, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BrokenBookcaseDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BrokenBedDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BrokenArmoireDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StandingBrokenChairDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BrokenVanityDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BrokenFallenChairDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BrokenCoveredChairDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BrokenChestOfDrawersDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TapestryOfSosaria ), Utility.Random( 50000,150000 ), 1, 0x234E, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RoseOfTrinsic ), Utility.Random( 50000,150000 ), 1, 0x234C, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HearthOfHomeFireDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( VanityDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BlueDecorativeRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BlueFancyRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BluePlainRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BoilingCauldronDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( CinnamonFancyRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( CurtainsDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( FountainDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GoldenDecorativeRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HangingAxesDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HangingSwordsDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HouseLadderDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( LargeFishingNetDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( PinkFancyRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RedPlainRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ScarecrowDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SmallFishingNetDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TableWithBlueClothDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TableWithOrangeClothDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TableWithPurpleClothDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TableWithRedClothDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( UnmadeBedDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( WallBannerDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreeStumpDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DecorativeShieldDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MiningCartDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( PottedCactusDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StoneAnkhDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BannerDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( Tub ), Utility.Random( 50000,150000 ), 1, 0xe83, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( WaterBarrel ), Utility.Random( 50000,150000 ), 1, 0xe77, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ClosedBarrel ), Utility.Random( 50000,150000 ), 1, 0x0FAE, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( Bucket ), Utility.Random( 50000,150000 ), 1, 0x14e0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DecoTray ), Utility.Random( 50000,150000 ), 1, 0x992, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DecoTray2 ), Utility.Random( 50000,150000 ), 1, 0x991, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DecoBottlesOfLiquor ), Utility.Random( 50000,150000 ), 1, 0x99E, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( Checkers ), Utility.Random( 50000,150000 ), 1, 0xE1A, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( Chessmen3 ), Utility.Random( 50000,150000 ), 1, 0xE14, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( Chessmen2 ), Utility.Random( 50000,150000 ), 1, 0xE12, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( Chessmen ), Utility.Random( 50000,150000 ), 1, 0xE13, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( Checkers2 ), Utility.Random( 50000,150000 ), 1, 0xE1B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DecoHay2 ), Utility.Random( 50000,150000 ), 1, 0xF34, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DecoBridle2 ), Utility.Random( 50000,150000 ), 1, 0x1375, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DecoBridle ), Utility.Random( 50000,150000 ), 1, 0x1374, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBChainmailArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBChainmailArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ChainCoif ), 17, Utility.Random( 1,100 ), 0x13BB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ChainChest ), 143, Utility.Random( 1,100 ), 0x13BF, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( ChainLegs ), 149, Utility.Random( 1,100 ), 0x13BE, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( ChainCoif ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ChainChest ), 71 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ChainLegs ), 74 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ChainSkirt ), 74 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBHelmetArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHelmetArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateHelm ), 21, Utility.Random( 1,100 ), 0x1412, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CloseHelm ), 18, Utility.Random( 1,100 ), 0x1408, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CloseHelm ), 18, Utility.Random( 1,100 ), 0x1409, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Helmet ), 31, Utility.Random( 1,100 ), 0x140A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Helmet ), 18, Utility.Random( 1,100 ), 0x140B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NorseHelm ), 18, Utility.Random( 1,100 ), 0x140E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NorseHelm ), 18, Utility.Random( 1,100 ), 0x140F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bascinet ), 18, Utility.Random( 1,100 ), 0x140C, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( PlateHelm ), 21, Utility.Random( 1,100 ), 0x1419, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DreadHelm ), 21, Utility.Random( 1,100 ), 0x2FBB, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bascinet ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CloseHelm ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Helmet ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( NorseHelm ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateHelm ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DreadHelm ), 10 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBLeatherArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBLeatherArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherArms ), 80, Utility.Random( 1,100 ), 0x13CD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherChest ), 101, Utility.Random( 1,100 ), 0x13CC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherGloves ), 60, Utility.Random( 1,100 ), 0x13C6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherGorget ), 74, Utility.Random( 1,100 ), 0x13C7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherLegs ), 80, Utility.Random( 1,100 ), 0x13cb, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherCap ), 10, Utility.Random( 1,100 ), 0x1DB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FemaleLeatherChest ), 116, Utility.Random( 1,100 ), 0x1C06, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherBustierArms ), 97, Utility.Random( 1,100 ), 0x1C0A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherShorts ), 86, Utility.Random( 1,100 ), 0x1C00, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LeatherSkirt ), 87, Utility.Random( 1,100 ), 0x1C08, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherCloak ), 120, Utility.Random( 1,100 ), 0x1515, 0x83F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherRobe ), 160, Utility.Random( 1,100 ), 0x1F03, 0x83F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PugilistMits ), 18, Utility.Random( 1,100 ), 0x13C6, 0x966 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ThrowingGloves ), 26, Utility.Random( 1,100 ), 0x13C6, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDragonArms ), 43200, 1, 0x13CD, Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDragonChest ), 44000, 1, 0x13CC, Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDragonGloves ), 42900, 1, 0x13C6, Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDragonGorget ), 42700, 1, 0x13C7, Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDragonLegs ), 43200, 1, 0x13cb, Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDragonHelm ), 42800, 1, 0x1DB9, Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinNightmareArms ), 43200, 1, 0x13CD, Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinNightmareChest ), 44000, 1, 0x13CC, Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinNightmareGloves ), 42900, 1, 0x13C6, Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinNightmareGorget ), 42700, 1, 0x13C7, Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinNightmareLegs ), 43200, 1, 0x13cb, Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinNightmareHelm ), 42800, 1, 0x1DB9, Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinSerpentArms ), 43200, 1, 0x13CD, Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinSerpentChest ), 44000, 1, 0x13CC, Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinSerpentGloves ), 42900, 1, 0x13C6, Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinSerpentGorget ), 42700, 1, 0x13C7, Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinSerpentLegs ), 43200, 1, 0x13cb, Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinSerpentHelm ), 42800, 1, 0x1DB9, Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinTrollArms ), 43200, 1, 0x13CD, Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinTrollChest ), 44000, 1, 0x13CC, Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinTrollGloves ), 42900, 1, 0x13C6, Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinTrollGorget ), 42700, 1, 0x13C7, Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinTrollLegs ), 43200, 1, 0x13cb, Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinTrollHelm ), 42800, 1, 0x1DB9, Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinUnicornArms ), 43200, 1, 0x13CD, Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinUnicornChest ), 44000, 1, 0x13CC, Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinUnicornGloves ), 42900, 1, 0x13C6, Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinUnicornGorget ), 42700, 1, 0x13C7, Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinUnicornLegs ), 43200, 1, 0x13cb, Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinUnicornHelm ), 42800, 1, 0x1DB9, Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDemonArms ), 43200, 1, 0x13CD, Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDemonChest ), 44000, 1, 0x13CC, Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDemonGloves ), 42900, 1, 0x13C6, Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDemonGorget ), 42700, 1, 0x13C7, Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDemonLegs ), 43200, 1, 0x13cb, Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SkinDemonHelm ), 42800, 1, 0x1DB9, Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 ) ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherArms ), 40 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherChest ), 52 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherGloves ), 30 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherGorget ), 37 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherLegs ), 40 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherCap ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FemaleLeatherChest ), 18 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FemaleStuddedChest ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherShorts ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherSkirt ), 11 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherBustierArms ), 11 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherCloak ), 60 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherRobe ), 80 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PugilistMits ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedBustierArms ), 27 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDragonArms ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDragonChest ), 500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDragonGloves ), 300 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDragonGorget ), 370 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDragonLegs ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDragonHelm ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinNightmareArms ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinNightmareChest ), 500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinNightmareGloves ), 300 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinNightmareGorget ), 370 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinNightmareLegs ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinNightmareHelm ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinSerpentArms ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinSerpentChest ), 500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinSerpentGloves ), 300 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinSerpentGorget ), 370 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinSerpentLegs ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinSerpentHelm ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinTrollArms ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinTrollChest ), 500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinTrollGloves ), 300 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinTrollGorget ), 370 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinTrollLegs ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinTrollHelm ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinUnicornArms ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinUnicornChest ), 500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinUnicornGloves ), 300 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinUnicornGorget ), 370 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinUnicornLegs ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinUnicornHelm ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDemonArms ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDemonChest ), 500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDemonGloves ), 300 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDemonGorget ), 370 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDemonLegs ), 400 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinDemonHelm ), 100 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMetalShields : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMetalShields()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BronzeShield ), 66, Utility.Random( 1,100 ), 0x1B72, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Buckler ), 50, Utility.Random( 1,100 ), 0x1B73, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MetalKiteShield ), 123, Utility.Random( 1,100 ), 0x1B74, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HeaterShield ), 231, Utility.Random( 1,100 ), 0x1B76, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenKiteShield ), 70, Utility.Random( 1,100 ), 0x1B78, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( MetalShield ), 121, Utility.Random( 1,100 ), 0x1B7B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GuardsmanShield ), 231, Utility.Random( 1,100 ), 0x2FCB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ElvenShield ), 231, Utility.Random( 1,100 ), 0x2FCA, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DarkShield ), 231, Utility.Random( 1,100 ), 0x2FC8, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( CrestedShield ), 231, Utility.Random( 1,100 ), 0x2FC9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ChampionShield ), 231, Utility.Random( 1,100 ), 0x2B74, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JeweledShield ), 231, Utility.Random( 1,100 ), 0x2B75, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Buckler ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BronzeShield ), 33 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MetalShield ), 60 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MetalKiteShield ), 62 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HeaterShield ), 115 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenKiteShield ), 35 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GuardsmanShield ), 115 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ElvenShield ), 115 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DarkShield ), 115 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CrestedShield ), 115 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ChampionShield ), 115 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JeweledShield ), 115 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBPlateArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBPlateArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateGorget ), 104, Utility.Random( 1,100 ), 0x1413, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateChest ), 243, Utility.Random( 1,100 ), 0x1415, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateLegs ), 218, Utility.Random( 1,100 ), 0x1411, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateArms ), 188, Utility.Random( 1,100 ), 0x1410, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( PlateGloves ), 155, Utility.Random( 1,100 ), 0x1414, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FemalePlateChest ), 207, Utility.Random( 1,100 ), 0x1C04, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateArms ), 94 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateChest ), 121 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateGloves ), 72 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateGorget ), 52 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateLegs ), 109 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateSkirt ), 109 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FemalePlateChest ), 113 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBLotsOfArrows: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBLotsOfArrows()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( ManyArrows100 ), 3000, Utility.RandomMinMax(10,50), 0xF41, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyBolts100 ), 3000, Utility.RandomMinMax(10,50), 0x1BFD, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyArrows1000 ), 30000, Utility.RandomMinMax(10,50), 0xF41, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyBolts1000 ), 30000, Utility.RandomMinMax(10,50), 0x1BFD, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBRingmailArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRingmailArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RingmailChest ), 121, Utility.Random( 1,100 ), 0x13ec, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RingmailLegs ), 90, Utility.Random( 1,100 ), 0x13F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RingmailArms ), 85, Utility.Random( 1,100 ), 0x13EE, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( RingmailGloves ), 93, Utility.Random( 1,100 ), 0x13eb, 0 ) ); }

			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( RingmailArms ), 42 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RingmailChest ), 60 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RingmailGloves ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RingmailLegs ), 45 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RingmailSkirt ), 45 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBStuddedArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBStuddedArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedArms ), 87, Utility.Random( 1,100 ), 0x13DC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedChest ), 128, Utility.Random( 1,100 ), 0x13DB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedGloves ), 79, Utility.Random( 1,100 ), 0x13D5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedGorget ), 73, Utility.Random( 1,100 ), 0x13D6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedLegs ), 103, Utility.Random( 1,100 ), 0x13DA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FemaleStuddedChest ), 142, Utility.Random( 1,100 ), 0x1C02, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( StuddedBustierArms ), 120, Utility.Random( 1,100 ), 0x1c0c, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedArms ), 43 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedChest ), 64 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedGloves ), 39 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedGorget ), 36 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedLegs ), 51 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedSkirt ), 51 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FemaleStuddedChest ), 71 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedBustierArms ), 60 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBWoodenShields: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBWoodenShields()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( WoodenShield ), 150, Utility.Random( 1,10 ), 0x1B7A, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenShield ), 80 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSEArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSEArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateHatsuburi ), 76, Utility.Random( 1,100 ), 0x2775, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HeavyPlateJingasa ), 76, Utility.Random( 1,100 ), 0x2777, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DecorativePlateKabuto ), 95, Utility.Random( 1,100 ), 0x2778, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateDo ), 310, Utility.Random( 1,100 ), 0x277D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateHiroSode ), 222, Utility.Random( 1,100 ), 0x2780, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateSuneate ), 224, Utility.Random( 1,100 ), 0x2788, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateHaidate ), 235, Utility.Random( 1,100 ), 0x278D, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( ChainHatsuburi ), 76, Utility.Random( 1,100 ), 0x2774, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateHatsuburi ), 38 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HeavyPlateJingasa ), 38 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DecorativePlateKabuto ), 47 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateDo ), 155 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateHiroSode ), 111 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateSuneate ), 112 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlateHaidate), 117 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ChainHatsuburi ), 38 ); } 

			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSEBowyer: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSEBowyer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Yumi ), 53, Utility.Random( 1,100 ), 0x27A5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Fukiya ), 20, Utility.Random( 1,100 ), 0x27AA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Nunchaku ), 35, Utility.Random( 1,100 ), 0x27AE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FukiyaDarts ), 3, Utility.Random( 1,100 ), 0x2806, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Bokuto ), 21, Utility.Random( 1,100 ), 0x27A8, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Yumi ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Fukiya ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Nunchaku ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FukiyaDarts ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bokuto ), 10 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSECarpenter: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSECarpenter()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bokuto ), 21, Utility.Random( 1,100 ), 0x27A8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tetsubo ), 43, Utility.Random( 1,100 ), 0x27A6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Fukiya ), 20, Utility.Random( 1,100 ), 0x27AA, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tetsubo ), 21 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Fukiya ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bokuto ), 10 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSEFood: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSEFood()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Wasabi ), 200, Utility.Random( 1,100 ), 0x24E8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Wasabi ), 200, Utility.Random( 1,100 ), 0x24E9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BentoBox ), 600, Utility.Random( 1,100 ), 0x2836, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BentoBox ), 600, Utility.Random( 1,100 ), 0x2837, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( GreenTeaBasket ), 200, Utility.Random( 1,100 ), 0x284B, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Wasabi ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BentoBox ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreenTeaBasket ), 1 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSELeatherArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSELeatherArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherJingasa ), 11, Utility.Random( 1,100 ), 0x2776, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherDo ), 87, Utility.Random( 1,100 ), 0x277B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherHiroSode ), 49, Utility.Random( 1,100 ), 0x277E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherSuneate ), 55, Utility.Random( 1,100 ), 0x2786, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherHaidate), 54, Utility.Random( 1,100 ), 0x278A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherNinjaPants ), 49, Utility.Random( 1,100 ), 0x2791, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherNinjaJacket ), 51, Utility.Random( 1,100 ), 0x2793, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedMempo ), 61, Utility.Random( 1,100 ), 0x279D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedDo ), 130, Utility.Random( 1,100 ), 0x277C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedHiroSode ), 73, Utility.Random( 1,100 ), 0x277F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedSuneate ), 78, Utility.Random( 1,100 ), 0x2787, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( StuddedHaidate ), 76, Utility.Random( 1,100 ), 0x278B, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherJingasa ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherDo ), 42 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherHiroSode ), 23 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherSuneate ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherHaidate), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherNinjaPants ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherNinjaJacket ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedMempo ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedDo ), 66 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedHiroSode ), 32 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedSuneate ), 40 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedHaidate ), 37 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSEWeapons: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSEWeapons()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NoDachi ), 82, Utility.Random( 1,100 ), 0x27A2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tessen ), 83, Utility.Random( 1,100 ), 0x27A3, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Wakizashi ), 38, Utility.Random( 1,100 ), 0x27A4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tetsubo ), 43, Utility.Random( 1,100 ), 0x27A6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lajatang ), 108, Utility.Random( 1,100 ), 0x27A7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Daisho ), 66, Utility.Random( 1,100 ), 0x27A9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tekagi ), 55, Utility.Random( 1,100 ), 0x27AB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Shuriken ), 18, Utility.Random( 1,100 ), 0x27AC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Kama ), 61, Utility.Random( 1,100 ), 0x27AD, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Sai ), 56, Utility.Random( 1,100 ), 0x27AF, 0 ) ); }		
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( NoDachi ), 41 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tessen ), 41 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Wakizashi ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tetsubo ), 21 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lajatang ), 54 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Daisho ), 33 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tekagi ), 22 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Shuriken), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Kama ), 30 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Sai ), 28 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBAxeWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBAxeWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ExecutionersAxe ), 30, Utility.Random( 1,100 ), 0xF45, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BattleAxe ), 26, Utility.Random( 1,100 ), 0xF47, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TwoHandedAxe ), 32, Utility.Random( 1,100 ), 0x1443, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Axe ), 40, Utility.Random( 1,100 ), 0xF49, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DoubleAxe ), 52, Utility.Random( 1,100 ), 0xF4B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pickaxe ), 22, Utility.Random( 1,100 ), 0xE86, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LargeBattleAxe ), 33, Utility.Random( 1,100 ), 0x13FB, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( WarAxe ), 29, Utility.Random( 1,100 ), 0x13B0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( OrnateAxe ), 42, Utility.Random( 1,100 ), 0x2D28, 0 ) ); }

			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( OrnateAxe ),21 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BattleAxe ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DoubleAxe ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ExecutionersAxe ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LargeBattleAxe ),16 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pickaxe ), 11 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TwoHandedAxe ), 16 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WarAxe ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Axe ), 20 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBKnifeWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBKnifeWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ButcherKnife ), 14, Utility.Random( 1,100 ), 0x13F6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Dagger ), 21, Utility.Random( 1,100 ), 0xF52, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cleaver ), 100, Utility.Random( 1,100 ), 0xEC3, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( SkinningKnife ), 14, Utility.Random( 1,100 ), 0xEC4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AssassinSpike ), 21, Utility.Random( 1,100 ), 0x2D21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Leafblade ), 21, Utility.Random( 1,100 ), 0x2D22, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WarCleaver ), 25, Utility.Random( 1,100 ), 0x2D2F, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( ButcherKnife ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cleaver ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Dagger ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinningKnife ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AssassinSpike ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Leafblade ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WarCleaver ), 12 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMaceWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMaceWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DiamondMace ), 31, Utility.Random( 1,100 ), 0x2D24, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HammerPick ), 26, Utility.Random( 1,100 ), 0x143D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Club ), 16, Utility.Random( 1,100 ), 0x13B4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Mace ), 28, Utility.Random( 1,100 ), 0xF5C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Maul ), 21, Utility.Random( 1,100 ), 0x143B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WarHammer ), 25, Utility.Random( 1,100 ), 0x1439, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( WarMace ), 31, Utility.Random( 1,100 ), 0x1407, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Club ), 8 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HammerPick ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Mace ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Maul ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WarHammer ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WarMace ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DiamondMace ), 100 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBPoleArmWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBPoleArmWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bardiche ), 60, Utility.Random( 1,100 ), 0xF4D, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Halberd ), 42, Utility.Random( 1,100 ), 0x143E, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bardiche ), 30 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Halberd ), 21 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBRangedWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRangedWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bow ), 40, Utility.Random( 1,100 ), 0x13B2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Crossbow ), 55, Utility.Random( 1,100 ), 0xF50, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HeavyCrossbow ), 55, Utility.Random( 1,100 ), 0x13FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RepeatingCrossbow ), 46, Utility.Random( 1,100 ), 0x26C3, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CompositeBow ), 45, Utility.Random( 1,100 ), 0x26C2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MagicalShortbow ), 42, Utility.Random( 1,100 ), 0x2D2B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ElvenCompositeLongbow ), 42, Utility.Random( 1,100 ), 0x2D1E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bolt ), 4, Utility.Random( 200, 500 ), 0x1BFB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Arrow ), 4, Utility.Random( 200, 500 ), 0xF3F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Feather ), 2, Utility.Random( 200, 1000 ), 0x4CCD, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Shaft ), 1, Utility.Random( 200, 1000 ), 0x1BD4, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bolt ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Arrow ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Shaft ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Feather ), 1 ); } 			
				if ( MyServerSettings.BuyChance() ){Add( typeof( HeavyCrossbow ), 27 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bow ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Crossbow ), 25 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( CompositeBow ), 23 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RepeatingCrossbow ), 22 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicalShortbow ), 18 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ElvenCompositeLongbow ), 18 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSpearForkWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSpearForkWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pitchfork ), 19, Utility.Random( 1,100 ), 0xE87, 0xB3A ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ShortSpear ), 23, Utility.Random( 1,100 ), 0x1403, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Spear ), 31, Utility.Random( 1,100 ), 0xF62, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Spear ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pitchfork ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShortSpear ), 11 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBStavesWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBStavesWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlackStaff ), 22, Utility.Random( 1,100 ), 0xDF1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WildStaff ), 20, Utility.Random( 1,100 ), 0x2D25, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GnarledStaff ), 16, Utility.Random( 1,100 ), 0x13F8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( QuarterStaff ), 19, Utility.Random( 1,100 ), 0xE89, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( ShepherdsCrook ), 20, Utility.Random( 1,100 ), 0xE81, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlackStaff ), 11 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GnarledStaff ), 8 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuarterStaff ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShepherdsCrook ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WildStaff ), 10 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSwordWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSwordWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cutlass ), 24, Utility.Random( 1,100 ), 0x1441, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Katana ), 33, Utility.Random( 1,100 ), 0x13FF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Kryss ), 32, Utility.Random( 1,100 ), 0x1401, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Broadsword ), 35, Utility.Random( 1,100 ), 0xF5E, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Longsword ), 55, Utility.Random( 1,100 ), 0xF61, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ThinLongsword ), 27, Utility.Random( 1,100 ), 0x13B8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( VikingSword ), 55, Utility.Random( 1,100 ), 0x13B9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Scimitar ), 36, Utility.Random( 1,100 ), 0x13B6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoneHarvester ), 35, Utility.Random( 1,100 ), 0x26BB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CrescentBlade ), 37, Utility.Random( 1,100 ), 0x26C1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DoubleBladedStaff ), 35, Utility.Random( 1,100 ), 0x26BF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lance ), 34, Utility.Random( 1,100 ), 0x26C0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pike ), 39, Utility.Random( 1,100 ), 0x26BE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Scythe ), 39, Utility.Random( 1,100 ), 0x26BA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RuneBlade ), 55, Utility.Random( 1,100 ), 0x2D32, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RadiantScimitar ), 35, Utility.Random( 1,100 ), 0x2D33, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ElvenSpellblade ), 33, Utility.Random( 1,100 ), 0x2D20, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ElvenMachete ), 35, Utility.Random( 1,100 ), 0x2D35, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Broadsword ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cutlass ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Katana ), 16 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Kryss ), 16 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Longsword ), 27 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Scimitar ), 18 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ThinLongsword ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( VikingSword ), 27 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Scythe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoneHarvester ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Scepter ), 18 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BladedStaff ), 16 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pike ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DoubleBladedStaff ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lance ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CrescentBlade ), 18 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RuneBlade ), 27 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RadiantScimitar ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ElvenSpellblade ), 16 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ElvenMachete ), 17 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBElfWizard : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBElfWizard()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{  
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BagOfSending ), 10000, Utility.Random( 1,10 ), 0xE76, 0x8AD ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BallOfSummoning ), 3000, Utility.Random( 1,10 ), 0xE2E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BraceletOfBinding ), 3500, Utility.Random( 1,10 ), 0x4CF1, 0x489 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PowderOfTranslocation ), 1000, Utility.Random( 5,20 ), 0x26B8, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBElfHealer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBElfHealer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{  
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FountainOfLifeDeed ), 100000, 1, 0x14F0, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBUndertaker: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBUndertaker()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( PowerCoil ), Utility.Random( 10000,20000 ), Utility.Random( 1,5 ), 0x8A7, 0 ) );
				Add( new GenericBuyInfo( typeof( EmbalmingFluid ), Utility.Random( 100,200 ), Utility.Random( 15,55 ), 0xE0F, 0xBA1 ) );
            }
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( FrankenArmRight ), Utility.Random( 100,350 ) );
				Add( typeof( FrankenHead ), Utility.Random( 100,350 ) );
				Add( typeof( FrankenLegLeft ), Utility.Random( 100,350 ) );
				Add( typeof( FrankenLegRight ), Utility.Random( 100,350 ) );
				Add( typeof( FrankenTorso ), Utility.Random( 100,350 ) );
				Add( typeof( FrankenArmLeft ), Utility.Random( 100,350 ) );
				Add( typeof( FrankenBrain ), Utility.Random( 100,350 ) );
				Add( typeof( FrankenJournal ), Utility.Random( 300,750 ) );
				Add( typeof( PowerCoil ), Utility.Random( 3500,4500 ) );
				Add( typeof( CorpseSailor ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( CorpseChest ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BuriedBody ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BoneContainer ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( LeftLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( TastyHeart ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( BodyPart ), Utility.RandomMinMax( 30, 90 ) );
				Add( typeof( Head ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( LeftArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Torso ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bone ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RibCage ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( BonePile ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bones ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( GraveChest ), Utility.RandomMinMax( 100, 500 ) );
				Add( typeof( EmbalmingFluid ), Utility.RandomMinMax( 25, 45 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( DracolichSkull ), Utility.Random( 500,1000 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBAlchemist : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBAlchemist()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{  
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MortarPestle ), 8, Utility.Random( 1,100 ), 0x4CE9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingCauldron ), 247, 1, 0x269C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 1,100 ), 0x1E27, 0x979 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CBookNecroticAlchemy ), 50, Utility.Random( 1,100 ), 0x2253, 0x4AA ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AlchemicalElixirs ), 50, Utility.Random( 1,100 ), 0x2219, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AlchemicalMixtures ), 50, Utility.Random( 1,100 ), 0x2223, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BookOfPoisons ), 50, Utility.Random( 1,100 ), 0x2253, 0x4F8 ) ); }

				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Bottle ), 5, Utility.Random( 1,100 ), 0xF0E, 0 ) ); } 
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HeatingStand ), 2, Utility.Random( 1,100 ), 0x1849, 0 ) ); } 

				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlackPearl ), 5, Utility.Random( 1,100 ), 0x266F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bloodmoss ), 5, Utility.Random( 1,100 ), 0xF7B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 1,100 ), 0xF84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 1,100 ), 0xF85, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MandrakeRoot ), 3, Utility.Random( 1,100 ), 0xF86, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Nightshade ), 3, Utility.Random( 1,100 ), 0xF88, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SpidersSilk ), 3, Utility.Random( 1,100 ), 0xF8D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SulfurousAsh ), 3, Utility.Random( 1,100 ), 0xF8C, 0 ) ); }

				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Brimstone ), 6, Utility.Random( 1,100 ), 0x2FD3, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ButterflyWings ), 6, Utility.Random( 1,100 ), 0x3002, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( EyeOfToad ), 6, Utility.Random( 1,100 ), 0x2FDA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FairyEgg ), 6, Utility.Random( 1,100 ), 0x2FDB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GargoyleEar ), 6, Utility.Random( 1,100 ), 0x2FD9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BeetleShell ), 6, Utility.Random( 1,100 ), 0x2FF8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MoonCrystal ), 6, Utility.Random( 1,100 ), 0x3003, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PixieSkull ), 6, Utility.Random( 1,100 ), 0x2FE1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RedLotus ), 6, Utility.Random( 1,100 ), 0x2FE8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SeaSalt ), 6, Utility.Random( 1,100 ), 0x2FE9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SilverWidow ), 6, Utility.Random( 1,100 ), 0x2FF7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SwampBerries ), 6, Utility.Random( 1,100 ), 0x2FE0, 0 ) ); }

				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BottleOfAcid ), 600, Utility.Random( 1,100 ), 0x180F, 1167 ) ); }


				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RefreshPotion ), 100, Utility.Random( 1,100 ), 0xF0B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AgilityPotion ), 100, Utility.Random( 1,100 ), 0xF08, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NightSightPotion ), 100, Utility.Random( 1,100 ), 0xF06, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LesserHealPotion ), 100, Utility.Random( 1,100 ), 0x25FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StrengthPotion ), 100, Utility.Random( 1,100 ), 0xF09, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LesserPoisonPotion ), 100, Utility.Random( 1,100 ), 0x2600, 0 ) ); }
 				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LesserCurePotion ), 100, Utility.Random( 1,100 ), 0x233B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LesserExplosionPotion ), 21, Utility.Random( 1,100 ), 0x2407, 0 ) ); }

				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HealPotion ), 30, Utility.Random( 1,100 ), 0xF0C, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PoisonPotion ), 30, Utility.Random( 1,100 ), 0xF0A, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( CurePotion ), 30, Utility.Random( 1,100 ), 0xF07, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ExplosionPotion ), 42, Utility.Random( 1,100 ), 0xF0D, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ConflagrationPotion ), 30, Utility.Random( 1,100 ), 0x180F, 0xAD8 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ConfusionBlastPotion ), 30, Utility.Random( 1,100 ), 0x180F, 0x48D ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( FrostbitePotion ), 30, Utility.Random( 1,100 ), 0x180F, 0xAF3 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( TotalRefreshPotion ), 30, Utility.Random( 1,100 ), 0x25FF, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GreaterAgilityPotion ), 60, Utility.Random( 1,100 ), 0x256A, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GreaterConflagrationPotion ), 60, Utility.Random( 1,100 ), 0x2406, 0xAD8 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GreaterConfusionBlastPotion ), 60, Utility.Random( 1,100 ), 0x2406, 0x48D ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GreaterCurePotion ), 60, Utility.Random( 1,100 ), 0x24EA, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GreaterExplosionPotion ), 60, Utility.Random( 1,100 ), 0x2408, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GreaterFrostbitePotion ), 60, Utility.Random( 1,100 ), 0x2406, 0xAF3 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GreaterHealPotion ), 60, Utility.Random( 1,100 ), 0x25FE, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GreaterPoisonPotion ), 60, Utility.Random( 1,100 ), 0x2601, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GreaterStrengthPotion ), 60, Utility.Random( 1,100 ), 0x25F7, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( DeadlyPoisonPotion ), 60, Utility.Random( 1,100 ), 0x2669, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( LesserInvisibilityPotion ), 860, Utility.Random( 1,3 ), 0x23BD, 0x490 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( LesserManaPotion ), 860, Utility.Random( 1,3 ), 0x23BD, 0x48D ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( LesserRejuvenatePotion ), 860, Utility.Random( 1,3 ), 0x23BD, 0x48E ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( InvisibilityPotion ), 890, Utility.Random( 1,3 ), 0x180F, 0x490 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ManaPotion ), 890, Utility.Random( 1,3 ), 0x180F, 0x48D ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RejuvenatePotion ), 890, Utility.Random( 1,3 ), 0x180F, 0x48E ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GreaterInvisibilityPotion ), 8120, 1, 0x2406, 0x490 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GreaterManaPotion ), 8120, 1, 0x2406, 0x48D ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GreaterRejuvenatePotion ), 8120, 1, 0x2406, 0x48E ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( InvulnerabilityPotion ), 8300, 1, 0x180F, 0x48F ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AutoResPotion ), 8600, 1, 0x0E0F, 0x494 ) ); }

				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) ); } 
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( AlchemyPouch ), Utility.Random( 2900,3500 ), Utility.Random( 1,2 ), 0x1C10, 0x89F ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullMinotaur ), Utility.Random( 50,150 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullWyrm ), Utility.Random( 200,400 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullGreatDragon ), Utility.Random( 300,600 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullDragon ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullDemon ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullGiant ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( CanopicJar ), Utility.Random( 50,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( MixingCauldron ), 123 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( MixingSpoon ), 17 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DragonTooth ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( EnchantedSeaweed ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( GhostlyDust ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldenSerpentVenom ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( LichDust ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverSerpentVenom ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( UnicornHorn ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DemigodBlood ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DemonClaw ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DragonBlood ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlackPearl ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bloodmoss ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( MandrakeRoot ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Garlic ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ginseng ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Nightshade ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpidersSilk ), 2 ); }  
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SulfurousAsh ), 1 ); }  

				if ( MyServerSettings.BuyChance() ){Add( typeof( Brimstone ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( ButterflyWings ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( EyeOfToad ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FairyEgg ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( GargoyleEar ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( BeetleShell ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( MoonCrystal ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( PixieSkull ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( RedLotus ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SeaSalt ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverWidow ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SwampBerries ), 3 ); }  

				if ( MyServerSettings.BuyChance() ){Add( typeof( Bottle ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Jar ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MortarPestle ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AgilityPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AutoResPotion ), 94 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BottleOfAcid ), 32 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ConflagrationPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FrostbitePotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ConfusionBlastPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CurePotion ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DeadlyPoisonPotion ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ExplosionPotion ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreaterAgilityPotion ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreaterConflagrationPotion ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreaterFrostbitePotion ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreaterConfusionBlastPotion ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreaterCurePotion ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreaterExplosionPotion ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreaterHealPotion ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreaterInvisibilityPotion ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreaterManaPotion ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreaterPoisonPotion ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreaterRejuvenatePotion ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreaterStrengthPotion ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HealPotion ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( InvisibilityPotion ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( InvulnerabilityPotion ), 53 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PotionOfWisdom ), Utility.Random( 250,500 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PotionOfDexterity ), Utility.Random( 250,500 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PotionOfMight ), Utility.Random( 250,500 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LesserCurePotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LesserExplosionPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LesserHealPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LesserInvisibilityPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LesserManaPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LesserPoisonPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LesserRejuvenatePotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ManaPotion ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( NightSightPotion ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PoisonPotion ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RefreshPotion ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RejuvenatePotion ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StrengthPotion ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TotalRefreshPotion ), 28 ); } 

				Add( typeof( BottleOfParts ), Utility.Random( 10, 30 ) );
				Add( typeof( SpecialSeaweed ), Utility.Random( 15, 35 ) );
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMixologist : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMixologist()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( ElixirAlchemy ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirAnatomy ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirAnimalLore ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirAnimalTaming ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirArchery ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirArmsLore ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirBegging ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirBlacksmith ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirCamping ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirCarpentry ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirCartography ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirCooking ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirDetectHidden ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirDiscordance ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirEvalInt ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirFencing ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirFishing ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirFletching ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirFocus ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirForensics ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirHealing ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirHerding ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirHiding ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirInscribe ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirItemID ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirLockpicking ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirLumberjacking ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirMacing ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirMagicResist ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirMeditation ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirMining ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirMusicianship ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirParry ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirPeacemaking ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirPoisoning ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirProvocation ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirRemoveTrap ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirSnooping ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirSpiritSpeak ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirStealing ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirStealth ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirSwords ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirTactics ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirTailoring ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirTasteID ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirTinkering ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirTracking ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirVeterinary ), Utility.Random( 14, 35 ) );
				Add( typeof( ElixirWrestling ), Utility.Random( 14, 35 ) );
				Add( typeof( MixtureSlime ), Utility.Random( 14, 35 ) );
				Add( typeof( MixtureIceSlime ), Utility.Random( 14, 35 ) );
				Add( typeof( MixtureFireSlime ), Utility.Random( 14, 35 ) );
				Add( typeof( MixtureDiseasedSlime ), Utility.Random( 14, 35 ) );
				Add( typeof( MixtureRadiatedSlime ), Utility.Random( 14, 35 ) );
				Add( typeof( LiquidFire ), Utility.Random( 14, 35 ) );
				Add( typeof( LiquidGoo ), Utility.Random( 14, 35 ) );
				Add( typeof( LiquidIce ), Utility.Random( 14, 35 ) );
				Add( typeof( LiquidRot ), Utility.Random( 14, 35 ) );
				Add( typeof( LiquidPain ), Utility.Random( 14, 35 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBAnimalTrainer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBAnimalTrainer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new AnimalBuyInfo( 1, typeof( Cat ), 132, 10, 201, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new AnimalBuyInfo( 1, typeof( Dog ), 170, 10, 217, 0 ) ); }
				if ( 1 > 0 ){Add( new AnimalBuyInfo( 1, typeof( Rabbit ), 106, 10, 205, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new AnimalBuyInfo( 1, typeof( Eagle ), 402, 10, 5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new AnimalBuyInfo( 1, typeof( BrownBear ), 855, 10, 167, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new AnimalBuyInfo( 1, typeof( GrizzlyBearRiding ), 1767, 10, 212, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new AnimalBuyInfo( 1, typeof( Panther ), 1271, 10, 214, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new AnimalBuyInfo( 1, typeof( TimberWolf ), 768, 10, 225, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new AnimalBuyInfo( 1, typeof( Rat ), 107, 10, 238, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HitchingPost ), 20000, Utility.Random( 1,3 ), 0x14E7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TamingBODBook ), 10000, Utility.Random( 1,3 ), 0x2259, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PetDyeTub ), 1250000, Utility.Random( 1,3 ), 0x0012, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( PetTrainer ), 2500, 4, 0x166E, 0 ) ); }
			}
		}


		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( HitchingPost ), 2500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AlienEgg ), Utility.Random( 500,1000 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DragonEgg ), Utility.Random( 500,1000 ) ); } 
			}
		}
	}
	public class SBHumanAnimalTrainer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHumanAnimalTrainer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new AnimalBuyInfo( 1, typeof( Horse ), 550, 10, 204, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( PackHorse ), 631, 10, 291, 0 ) );
				if ( MyServerSettings.SellVeryRareChance() ){Add( new AnimalBuyInfo( 5, typeof( PackMule ), 10000, 1, 291, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PetTrainer ), 2500, 4, 0x166E, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	public class SBGargoyleAnimalTrainer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGargoyleAnimalTrainer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new AnimalBuyInfo( 1, typeof( SwampDragon ), 1700, 10, 0x31A, 0 ) );
				if ( MyServerSettings.SellVeryRareChance() ){Add( new AnimalBuyInfo( 5, typeof( PackTurtle ), 14500, 1, 134, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PetTrainer ), 2500, 4, 0x166E, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	public class SBElfAnimalTrainer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBElfAnimalTrainer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new AnimalBuyInfo( 1, typeof( ForestOstard ), 700, 10, 171, 0x89f ) );
				if ( MyServerSettings.SellChance() ){Add( new AnimalBuyInfo( 1, typeof( DesertOstard ), 700, 10, 171, 1701 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new AnimalBuyInfo( 1, typeof( SnowOstard ), 700, 10, 171, 0x481 ) ); }
				Add( new AnimalBuyInfo( 1, typeof( PackLlama ), 565, 10, 292, 0 ) );
				if ( MyServerSettings.SellVeryRareChance() ){Add( new AnimalBuyInfo( 1, typeof( WhiteWolf ), 1000, 10, 277, 0x47E ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new AnimalBuyInfo( 1, typeof( BlackWolf ), 1000, 10, 277, 0x76B ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PetTrainer ), 2500, 4, 0x166E, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new AnimalBuyInfo( 5, typeof( PackMule ), 10000, 1, 291, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	public class SBBarbarianAnimalTrainer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBarbarianAnimalTrainer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new AnimalBuyInfo( 1, typeof( Horse ), 550, 10, 204, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( PackHorse ), 631, 10, 291, 0 ) );
				if ( MyServerSettings.SellVeryRareChance() ){Add( new AnimalBuyInfo( 5, typeof( PackMule ), 10000, 1, 291, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new AnimalBuyInfo( 1, typeof( GreatBear ), 1500, 10, 213, 0x908 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new AnimalBuyInfo( 1, typeof( KodiakBear ), 1500, 10, 213, 0x76B ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new AnimalBuyInfo( 5, typeof( PackBear ), 12500, 1, 213, 0x908 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	public class SBOrkAnimalTrainer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBOrkAnimalTrainer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new AnimalBuyInfo( 1, typeof( Ridgeback ), 1500, 10, 187, 0x7D1 ) );
				if ( MyServerSettings.SellVeryRareChance() ){Add( new AnimalBuyInfo( 5, typeof( PackStegosaurus ), 15500, 1, 134, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBArchitect : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBArchitect()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( InteriorDecorator ), 1000, Utility.Random( 1,100 ), 0x1EBA, 0 ) );
				Add( new GenericBuyInfo( typeof( HousePlacementTool ), 500, Utility.Random( 1,100 ), 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "house teleporter", typeof( PlayersHouseTeleporter ), 25000, Utility.Random( 1,10 ), 0x181D, 0 ) );
				Add( new GenericBuyInfo( "house high teleporter", typeof( PlayersZTeleporter ), 15000, Utility.Random( 1,10 ), 0x181D, 0 ) );
				if ( Server.Items.MovingBox.IsEnabled() ){ Add( new GenericBuyInfo( typeof( MovingBox ), 1500, Utility.Random( 1,100 ), 0xE3D, 0xAC0 ) ); }
				//if ( Server.Items.BasementDoor.IsEnabled() ){ Add( new GenericBuyInfo( typeof( BasementDoor ), 2500, Utility.Random( 1,100 ), 0x02C1, 0 ) ); }
				Add( new GenericBuyInfo( typeof( house_sign_sign_post_a ), 500, Utility.Random( 1,100 ), 2967, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_post_b ), 500, Utility.Random( 1,100 ), 2970, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_merc ), 1000, Utility.Random( 1,100 ), 3082, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_armor ), 1000, Utility.Random( 1,100 ), 3008, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bake ), 1000, Utility.Random( 1,100 ), 2980, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bank ), 1000, Utility.Random( 1,100 ), 3084, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bard ), 1000, Utility.Random( 1,100 ), 3004, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_smith ), 1000, Utility.Random( 1,100 ), 3016, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bow ), 1000, Utility.Random( 1,100 ), 3022, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_ship ), 1000, Utility.Random( 1,100 ), 2998, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_fletch ), 1000, Utility.Random( 1,100 ), 3006, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_heal ), 1000, Utility.Random( 1,100 ), 2988, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_inn ), 1000, Utility.Random( 1,100 ), 2996, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_gem ), 1000, Utility.Random( 1,100 ), 3010, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_book ), 1000, Utility.Random( 1,100 ), 2966, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_mage ), 1000, Utility.Random( 1,100 ), 2990, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_necro ), 1000, Utility.Random( 1,100 ), 2811, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_supply ), 1000, Utility.Random( 1,100 ), 3020, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_herb ), 1000, Utility.Random( 1,100 ), 3014, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_pen ), 1000, Utility.Random( 1,100 ), 3000, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_sew ), 1000, Utility.Random( 1,100 ), 2982, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_tavern ), 1000, Utility.Random( 1,100 ), 3012, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_tinker ), 1000, Utility.Random( 1,100 ), 2984, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_wood ), 1000, Utility.Random( 1,100 ), 2992, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StoneWellDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RedWellDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MarbleWellDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BrownWellDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlackWellDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodWellDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MedCaseSouthAddonDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MedCaseEastAddonDeed ), 25000, 1, 0xF3A, 0xB97 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmallCaseAddonDeed ), 22500, 1, 0xF3A, 0xB97 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( InteriorDecorator ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HousePlacementTool ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlayersHouseTeleporter ), 2500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlayersZTeleporter ), 1000 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_post_a ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_post_b ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_merc ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_armor ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_bake ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_bank ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_bard ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_smith ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_bow ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_ship ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_fletch ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_heal ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_inn ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_gem ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_book ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_mage ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_necro ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_supply ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_herb ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_pen ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_sew ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_tavern ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_tinker ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( house_sign_sign_wood ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StoneWellDeed ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RedWellDeed ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarbleWellDeed ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BrownWellDeed ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlackWellDeed ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodWellDeed ), 250 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSailor : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSailor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Harpoon ), 40, Utility.Random( 3,31 ), 0xF63, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( HarpoonRope ), 2, Utility.Random( 50,250 ), 0x52B1, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( FishingPole ), 100, Utility.Random( 3,31 ), 0xDC0, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( SwordsAndShackles ), 50, Utility.Random( 1,100 ), 0x529D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoatStain ), 26, Utility.Random( 1,100 ), 0x14E0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Sextant ), 13, Utility.Random( 1,100 ), 0x1057, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GrapplingHook ), 58, Utility.Random( 1,100 ), 0x4F40, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DockingLantern ), 580, Utility.Random( 3,31 ), 0x40FF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041205", typeof( SmallBoatDeed ), 19500, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) ); }
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Harpoon ), 20 ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HarpoonRope ), 1 ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SeaShell ), 58 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DockingLantern ), 29 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RawFishSteak ), 3 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Fish ), 5 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FishingPole ), 7 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Sextant ), 6 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GrapplingHook ), 29 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PirateChest ), Utility.RandomMinMax( 200, 800 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SunkenChest ), Utility.RandomMinMax( 200, 800 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FishingNet ), Utility.RandomMinMax( 20, 40 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SpecialFishingNet ), Utility.RandomMinMax( 60, 80 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FabledFishingNet ), Utility.RandomMinMax( 100, 120 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( NeptunesFishingNet ), Utility.RandomMinMax( 140, 160 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PrizedFish ), Utility.RandomMinMax( 60, 120 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WondrousFish ), Utility.RandomMinMax( 60, 120 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TrulyRareFish ), Utility.RandomMinMax( 60, 120 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PeculiarFish ), Utility.RandomMinMax( 60, 120 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SpecialSeaweed ), Utility.RandomMinMax( 40, 160 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SunkenBag ), Utility.RandomMinMax( 100, 500 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShipwreckedItem ), Utility.RandomMinMax( 20, 60 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HighSeasRelic ), Utility.RandomMinMax( 20, 60 ) );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BoatStain ), 13 );}
				if ( 1 > 0 ){Add( typeof( SwordsAndShackles ), 25 ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MegalodonTooth ), Utility.RandomMinMax( 500, 2000 ) );}
			} 
		} 
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBKungFu: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBKungFu()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BookOfBushido), 750, 20, 0x238C, 0 ) );
				Add( new GenericBuyInfo( typeof( BookOfNinjitsu ), 750, 20, 0x23A0, 0 ) );
				Add( new GenericBuyInfo( typeof( MysticSpellbook ), 750, 20, 0x1A97, 0xB61 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateHatsuburi ), 76, 20, 0x2775, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HeavyPlateJingasa ), 76, 20, 0x2777, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DecorativePlateKabuto ), 95, 20, 0x2778, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateDo ), 310, 20, 0x277D, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateHiroSode ), 222, 20, 0x2780, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateSuneate ), 224, 20, 0x2788, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlateHaidate ), 235, 20, 0x278D, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ChainHatsuburi ), 76, 20, 0x2774, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Yumi ), 53, 20, 0x27A5, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Fukiya ), 20, 20, 0x27AA, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Nunchaku ), 35, 20, 0x27AE, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FukiyaDarts ), 3, 20, 0x2806, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bokuto ), 21, 20, 0x27A8, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bokuto ), 21, 20, 0x27A8, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tetsubo ), 43, 20, 0x27A6, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BambooFlute ), 21, 20, 0x2805, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BambooFlute ), 21, 20, 0x2805, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherJingasa ), 11, 20, 0x2776, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherDo ), 87, 20, 0x277B, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherHiroSode ), 49, 20, 0x277E, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherSuneate ), 55, 20, 0x2786, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherHaidate), 54, 20, 0x278A, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherNinjaPants ), 49, 20, 0x2791, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherNinjaJacket ), 51, 20, 0x2793, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedMempo ), 61, 20, 0x279D, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedDo ), 130, 20, 0x277C, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedHiroSode ), 73, 20, 0x277F, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedSuneate ), 78, 20, 0x2787, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedHaidate ), 76, 20, 0x278B, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NoDachi ), 82, 20, 0x27A2, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tessen ), 83, 20, 0x27A3, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Wakizashi ), 38, 20, 0x27A4, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lajatang ), 108, 20, 0x27A7, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Daisho ), 66, 20, 0x27A9, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tekagi ), 55, 20, 0x27AB, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Shuriken ), 18, 20, 0x27AC, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Kama ), 61, 20, 0x27AD, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Sai ), 56, 20, 0x27AF, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Kasa ), 31, 20, 0x2798, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ThrowingWeapon ), 2, Utility.Random( 20, 120 ), 0x52B2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherJingasa ), 11, 20, 0x2776, 0 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ClothNinjaHood ), 33, 20, 0x278F, 0 ) );}
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BookOfBushido ), 70 );
				Add( typeof( BookOfNinjitsu ), 70 );
				Add( typeof( MysticSpellbook ), 70 );
				if ( MyServerSettings.BuyChance() ){Add( typeof( MySamuraibook ), Utility.Random( 50, 200 ) );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( MyNinjabook ), Utility.Random( 50, 200 ) );}
				Add( typeof( PlateHatsuburi ), 38 );
				Add( typeof( HeavyPlateJingasa ), 38 );
				Add( typeof( DecorativePlateKabuto ), 47 );
				Add( typeof( PlateDo ), 155 );
				Add( typeof( PlateHiroSode ), 111 );
				Add( typeof( PlateSuneate ), 112 );
				Add( typeof( PlateHaidate), 117 );
				Add( typeof( ChainHatsuburi ), 38 );
				Add( typeof( Yumi ), 26 );
				Add( typeof( Fukiya ), 10 );
				Add( typeof( Nunchaku ), 17 );
				Add( typeof( FukiyaDarts ), 1 );
				Add( typeof( Bokuto ), 10 );
				Add( typeof( Tetsubo ), 21 );
				Add( typeof( Fukiya ), 10 );
				Add( typeof( BambooFlute ), 10 );
				Add( typeof( Bokuto ), 10 );
				Add( typeof( LeatherJingasa ), 5 );
				Add( typeof( LeatherDo ), 42 );
				Add( typeof( LeatherHiroSode ), 23 );
				Add( typeof( LeatherSuneate ), 26 );
				Add( typeof( LeatherHaidate), 28 );
				Add( typeof( LeatherNinjaPants ), 25 );
				Add( typeof( LeatherNinjaJacket ), 26 );
				Add( typeof( StuddedMempo ), 28 );
				Add( typeof( StuddedDo ), 66 );
				Add( typeof( StuddedHiroSode ), 32 );
				Add( typeof( StuddedSuneate ), 40 );
				Add( typeof( StuddedHaidate ), 37 );
				Add( typeof( NoDachi ), 41 );
				Add( typeof( Tessen ), 41 );
				Add( typeof( Wakizashi ), 19 );
				Add( typeof( Tetsubo ), 21 );
				Add( typeof( Lajatang ), 54 );
				Add( typeof( Daisho ), 33 );
				Add( typeof( Tekagi ), 22 );
				Add( typeof( Shuriken), 9 );
				Add( typeof( Kama ), 30 );
				Add( typeof( Sai ), 28 );
				Add( typeof( Kasa ), 100 );
				Add( typeof( ThrowingWeapon ), 1 );
				Add( typeof( LeatherJingasa ), 5 );
				Add( typeof( ClothNinjaHood ), 16 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBBaker : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBBaker() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( BreadLoaf ), 60, Utility.Random( 1,100 ), 0x103B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BreadLoaf ), 50, Utility.Random( 1,100 ), 0x103C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ApplePie ), 170, Utility.Random( 1,100 ), 0x1041, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cake ), 1530, Utility.Random( 1,100 ), 0x9E9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Muffins ), 800, Utility.Random( 1,100 ), 0x9EA, 0 ) ); }
				Add( new GenericBuyInfo( typeof( SackFlour ), 50, Utility.Random( 1,100 ), 0x1039, 0 ) ); 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FrenchBread ), 50, Utility.Random( 1,100 ), 0x98C, 0 ) ); }
             	if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cookies ), 930, Utility.Random( 1,100 ), 0x160b, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CheesePizza ), 80, 10, 0x1040, 0 ) ); } // OSI just has Pizza
				if ( MyServerSettings.SellChance() ){Add (new GenericBuyInfo( typeof( BowlFlour ), 70, Utility.Random( 1,100 ), 0xA1E, 0 ) ); }
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BreadLoaf ), 15 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FrenchBread ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cake ), 700 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cookies ), 350 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Muffins ), 300 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( CheesePizza ), 20 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( ApplePie ), 25 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( PeachCobbler ), 25 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Quiche ), 30 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Dough ), 20 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pitcher ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SackFlour ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Eggs ), 5 ); }  
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBBanker : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBanker()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "1041243", typeof( ContractOfEmployment ), 1252, Utility.Random( 1,100 ), 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "Interest Bag (Good)", typeof( InterestBag ), 1500, 1, 0xE76, 0xABE ) ); 
				Add( new GenericBuyInfo( "Interest Bag (Evil)", typeof( EvilInterestBag ), 1500, 1, 0xE76, 0xABE ) ); 
				Add( new GenericBuyInfo( "1062332", typeof( VendorRentalContract ), 1252, Utility.Random( 1,100 ), 0x14F0, 0x672 ) );
				Add( new GenericBuyInfo( "1047016", typeof( CommodityDeed ), 5, Utility.Random( 1,100 ), 0x14F0, 0x47 ) );
				Add (new GenericBuyInfo( typeof( MetalVault ), 15000, Utility.Random( 1,5 ), 0x4FE3, 0 ) );
				Add (new GenericBuyInfo( typeof( MetalSafe ), 15000, Utility.Random( 1,5 ), 0x436, 0 ) );
				Add (new GenericBuyInfo( typeof( IronSafe ), 15000, Utility.Random( 1,5 ), 0x5329, 0 ) );
				Add (new GenericBuyInfo( typeof( Safe ), 500000, Utility.Random( 1,5 ), 0x436, 0 ) );
				Add (new GenericBuyInfo( typeof( DDCopper ), 1, Utility.Random( 70000,200000 ), 0xEF0, 0 ) );
				Add (new GenericBuyInfo( typeof( DDSilver ), 2, Utility.Random( 70000,200000 ), 0xEF0, 0 ) );
				Add (new GenericBuyInfo( typeof( SilverDeed ), 2000, Utility.Random( 200, 500 ), 0x14F0, 0 ) );

			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( TreasurePile05AddonDeed ), Utility.Random( 200,600 ) );
				Add( typeof( TreasurePile04AddonDeed ), Utility.Random( 200,600 ) );
				Add( typeof( TreasurePile3AddonDeed ), Utility.Random( 200,600 ) );
				Add( typeof( TreasurePile03AddonDeed ), Utility.Random( 200,600 ) );
				Add( typeof( TreasurePile2AddonDeed ), Utility.Random( 200,600 ) );
				Add( typeof( TreasurePile02AddonDeed ), Utility.Random( 200,600 ) );
				Add( typeof( TreasurePile01AddonDeed ), Utility.Random( 200,600 ) );
				Add( typeof( TreasurePileAddonDeed ), Utility.Random( 200,600 ) );
				Add( typeof( MetalVault ), Utility.Random( 1000,2000 ) );
				Add( typeof( MetalSafe ), Utility.Random( 1000,2000 ) );
				Add( typeof( IronSafe ), Utility.Random( 1000,2000 ) );
				Add( typeof( Safe ), Utility.Random( 100000,200000 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBBard: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBBard() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Drums ), 210, Utility.Random( 1,100 ), 0x0E9C, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tambourine ), 210, Utility.Random( 1,100 ), 0x0E9E, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LapHarp ), 210, Utility.Random( 1,100 ), 0x0EB2, 0 ) ); } 
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Lute ), 210, Utility.Random( 1,100 ), 0x0EB3, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BambooFlute ), 210, Utility.Random( 1,100 ), 0x2805, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SongBook ), 240, Utility.Random( 1,5 ), 0x225A, 0 ) ); } 
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( EnergyCarolScroll ), 320, 1, 0x1F48, 0x96 ) ); } 
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( FireCarolScroll ), 320, 1, 0x1F49, 0x96 ) ); } 
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( IceCarolScroll ), 320, 1, 0x1F34, 0x96 ) ); } 
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( KnightsMinneScroll ), 320, 1, 0x1F31, 0x96 ) ); } 
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PoisonCarolScroll ), 320, 1, 0x1F33, 0x96 ) ); } 
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( JarsOfWaxInstrument ), 1600, Utility.Random( 1,5 ), 0x1005, 0x845 ) ); } 
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( JarsOfWaxInstrument ), 80 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( LapHarp ), 10 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lute ), 10 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Drums ), 10 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Harp ), 10 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tambourine ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BambooFlute ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MySongbook ), Utility.Random( 50,200 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SongBook ), 12 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( EnergyCarolScroll ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FireCarolScroll ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( IceCarolScroll ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( KnightsMinneScroll ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( PoisonCarolScroll ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( ArmysPaeonScroll ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagesBalladScroll ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( EnchantingEtudeScroll ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SheepfoeMamboScroll ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SinewyEtudeScroll ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( EnergyThrenodyScroll ), 8 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FireThrenodyScroll ), 8 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( IceThrenodyScroll ), 8 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( PoisonThrenodyScroll ), 8 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FoeRequiemScroll ), 9 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicFinaleScroll ), 10 ); }  
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBBarkeeper : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBarkeeper()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Ale, 70, Utility.Random( 1,100 ), 0x99F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Wine, 70, Utility.Random( 1,100 ), 0x9C7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Liquor, 70, Utility.Random( 1,100 ), 0x99B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Jug ), BeverageType.Cider, 130, Utility.Random( 1,100 ), 0x9C8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Milk, 180, Utility.Random( 1,100 ), 0x9F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Ale, 110, Utility.Random( 1,100 ), 0x1F95, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Cider, 110, Utility.Random( 1,100 ), 0x1F97, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Liquor, 110, Utility.Random( 1,100 ), 0x1F99, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Wine, 110, Utility.Random( 1,100 ), 0x1F9B, 0 ) ); }
				if ( 1 > 0 ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Water, 90, Utility.Random( 1,100 ), 0x1F9D, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( BreadLoaf ), 60, Utility.Random( 1,100 ), 0x103B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CheeseWheel ), 410, Utility.Random( 1,100 ), 0x97E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CookedBird ), 170, Utility.Random( 1,100 ), 0x9B7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LambLeg ), 80, Utility.Random( 1,100 ), 0x160A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfCarrots ), 30, Utility.Random( 1,100 ), 0x15F9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfCorn ), 30, Utility.Random( 1,100 ), 0x15FA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfLettuce ), 30, Utility.Random( 1,100 ), 0x15FB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfPeas ), 30, Utility.Random( 1,100 ), 0x15FC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( EmptyPewterBowl ), 5, Utility.Random( 1,100 ), 0x15FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfCorn ), 30, Utility.Random( 1,100 ), 0x15FE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfLettuce ), 30, Utility.Random( 1,100 ), 0x15FF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfPeas ), 30, Utility.Random( 1,100 ), 0x1600, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfFoodPotatos ), 30, Utility.Random( 1,100 ), 0x1601, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfStew ), 30, Utility.Random( 1,100 ), 0x1604, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfTomatoSoup ), 30, Utility.Random( 1,100 ), 0x1606, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ApplePie ), 100, Utility.Random( 1,100 ), 0x1041, 0 ) ); } //OSI just has Pie, not Apple/Fruit/Meat
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( tarotpoker ), 115, Utility.Random( 1,100 ), 0x12AB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016450", typeof( Chessboard ), 211, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016449", typeof( CheckerBoard ), 211, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Engines.Mahjong.MahjongGame ), 6000, Utility.Random( 1,100 ), 0xFAA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backgammon ), 200, Utility.Random( 1,100 ), 0xE1C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Dices ), 20, Utility.Random( 1,100 ), 0xFA7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Waterskin ), 50, Utility.Random( 1,100 ), 0xA21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HenchmanFighterItem ), 5000, Utility.Random( 1,100 ), 0x1419, 0xB96 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HenchmanArcherItem ), 6000, Utility.Random( 1,100 ), 0xF50, 0xB96 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HenchmanWizardItem ), 7000, Utility.Random( 1,100 ), 0xE30, 0xB96 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041243", typeof( ContractOfEmployment ), 1252, Utility.Random( 1,100 ), 0x14F0, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( "a barkeep contract", typeof( BarkeepContract ), 1252, Utility.Random( 1,100 ), 0x14F0, 0 ) ); }
				if ( Multis.BaseHouse.NewVendorSystem )
					if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1062332", typeof( VendorRentalContract ), 1252, Utility.Random( 1,100 ), 0x14F0, 0x672 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfCarrots ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfCorn ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfLettuce ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfPeas ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmptyPewterBowl ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfCorn ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfLettuce ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfPeas ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfFoodPotatos ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfStew ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfTomatoSoup ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BeverageBottle ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Jug ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pitcher ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GlassMug ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BreadLoaf ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CheeseWheel ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ribs ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Peach ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pear ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Grapes ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Apple ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Banana ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Candle ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Chessboard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CheckerBoard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( tarotpoker ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MahjongGame ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Backgammon ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Dices ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ContractOfEmployment ), 626 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Waterskin ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RomulanAle ), Utility.Random( 200,1000 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBBeekeeper : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBeekeeper()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( JarHoney ), 600, Utility.Random( 1,100 ), 0x9EC, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( Beeswax ), 1000, Utility.Random( 1,100 ), 0x1422, 0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){Add( new GenericBuyInfo( typeof( apiBeeHiveDeed ), 2000, Utility.Random( 1,10 ), 2330, 0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){Add( new GenericBuyInfo( typeof( HiveTool ), 100, Utility.Random( 1,100 ), 2549, 0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){Add( new GenericBuyInfo( typeof( apiSmallWaxPot ), 250, Utility.Random( 1,100 ), 2532, 0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){Add( new GenericBuyInfo( typeof( apiLargeWaxPot ), 400, Utility.Random( 1,100 ), 2541, 0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){Add( new GenericBuyInfo( typeof( WaxingPot ), 50, Utility.Random( 1,100 ), 0x142B, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( JarHoney ), 300 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Beeswax ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( apiBeeHiveDeed ), 1000 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HiveTool ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( apiSmallWaxPot ), 125 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( apiLargeWaxPot ), 200 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WaxingPot ), 25 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( ColorCandleShort ), 85 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( ColorCandleLong ), 90 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( Candle ), 3 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( CandleLarge ), 70 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( Candelabra ), 140 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( CandelabraStand ), 210 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( CandleLong ), 80 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( CandleShort ), 75 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( CandleSkull ), 95 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( CandleReligious ), 120 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WallSconce ), 60 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( JarsOfWaxMetal ), 80 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( JarsOfWaxLeather ), 80 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( JarsOfWaxInstrument ), 80 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBBlacksmith : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBBlacksmith() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 	
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( IronIngot ), 5, Utility.Random( 1,100 ), 0x1BF2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tongs ), 13, Utility.Random( 1,100 ), 0xFBB, 0 ) ); }
				Add( new GenericBuyInfo( typeof( RandomLetter ), 250, Utility.Random( 25,100 ), 0x55BF, 0 ) ); 

				
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( SmithHammer ), 21, Utility.Random( 1,100 ), 0x0FB4, 0 ) ); }
		
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazIngot ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShinySilverIngot ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystIngot ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldIngot ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetIngot ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( IceIngot ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadeIngot ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarbleIngot ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxIngot ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzIngot ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyIngot ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphireIngot ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelIngot ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyIngot ), 120 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tongs ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( IronIngot ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SmithHammer ), 10 ); } 
				Add( typeof( MagicHammer ), Utility.Random( 300,400 ) );
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBBowyer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBowyer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( FletcherTools ), 2, Utility.Random( 1,100 ), 0x1F2C, 0 ) );
				Add( new GenericBuyInfo( typeof( ArcherQuiver ), 32, Utility.Random( 1,5 ), 0x2B02, 0 ) );
				Add( new GenericBuyInfo( typeof( ArcherPoonBag ), 32, Utility.Random( 1,5 ), 0x2B02, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( FletcherTools ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ArcherQuiver ), 16 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBButcher : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBButcher() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bacon ), 70, Utility.Random( 1,100 ), 0x979, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ham ), 260, Utility.Random( 1,100 ), 0x9C9, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Sausage ), 180, Utility.Random( 1,100 ), 0x9C0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RawChickenLeg ), 30, Utility.Random( 1,100 ), 0x1607, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RawBird ), 30, Utility.Random( 1,100 ), 0x9B9, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RawLambLeg ), 30, Utility.Random( 1,100 ), 0x1609, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( RawRibs ), 20, Utility.Random( 1,100 ), 0x9F1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ButcherKnife ), 130, Utility.Random( 1,100 ), 0x13F6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cleaver ), 130, Utility.Random( 1,100 ), 0xEC3, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SkinningKnife ), 130, Utility.Random( 1,100 ), 0xEC4, 0 ) ); } 
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RawRibs ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( RawLambLeg ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( RawChickenLeg ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( RawBird ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bacon ), 15 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Sausage ), 45 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ham ), 65 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( ButcherKnife ), 35 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cleaver ), 35 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinningKnife ), 35 ); }  
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBCarpenter: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBCarpenter()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Hatchet ), 25, Utility.Random( 1,100 ), 0xF44, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LumberAxe ), 27, Utility.Random( 1,100 ), 0xF43, 0x96D ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Nails ), 3, Utility.Random( 1,100 ), 0x102E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Axle ), 2, Utility.Random( 1,100 ), 0x105B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Board ), 3, Utility.Random( 1,100 ), 0x1BD7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DrawKnife ), 10, Utility.Random( 1,100 ), 0x10E4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Froe ), 10, Utility.Random( 1,100 ), 0x10E5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Scorp ), 10, Utility.Random( 1,100 ), 0x10E7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Inshave ), 10, Utility.Random( 1,100 ), 0x10E6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DovetailSaw ), 12, Utility.Random( 1,100 ), 0x1028, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Saw ), 100, Utility.Random( 1,100 ), 0x1034, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Hammer ), 17, Utility.Random( 1,100 ), 0x102A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MouldingPlane ), 11, Utility.Random( 1,100 ), 0x102C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmoothingPlane ), 10, Utility.Random( 1,100 ), 0x1032, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( JointingPlane ), 11, Utility.Random( 1,100 ), 0x1030, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( WoodworkingTools ), 10, Utility.Random( 10,30 ), 0x4F52, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SawMillEastAddonDeed ), 500, Utility.Random( 1,100 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SawMillSouthAddonDeed ), 500, Utility.Random( 1,100 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AdventurerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F9B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AlchemyCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F91, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ArmsCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F9E, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BakerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F92, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BeekeeperCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F95, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BlacksmithCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BowyerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F97, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ButcherCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F89, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( CarpenterCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8A, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( FletcherCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F88, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HealerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F98, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HugeCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F86, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JewelerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( LibrarianCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F96, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MusicianCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F94, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NecromancerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F9A, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ProvisionerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8E, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SailorCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F9C, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StableCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F87, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SupplyCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F9D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TailorCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TavernCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F99, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TinkerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F90, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasureCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F93, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( WizardryCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8C, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C43, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C45, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C47, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C89, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x38B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x38D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CC9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireH ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CCB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireI ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CCD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireJ ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D26, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmorShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BF1, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmorShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C31, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmorShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C63, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmorShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CAD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmorShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CEF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C3B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C65, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C67, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CBF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CC1, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CF1, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBlacksmithShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBlacksmithShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C4B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBlacksmithShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C6B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBlacksmithShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CC5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBlacksmithShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CF7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C15, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C2B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C2D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C33, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C5F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C61, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C79, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfH ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CA5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfI ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CA7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfJ ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CAF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfK ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CEB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfL ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CED, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfM ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D05, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBowyerShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C29, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBowyerShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C5D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBowyerShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CA3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBowyerShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CE9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewCarpenterShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C6F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewCarpenterShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CD7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewCarpenterShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CFB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C51, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C53, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C75, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C77, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CDD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CDF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CFF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfH ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D01, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDarkBookShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BF9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDarkBookShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BFB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDarkShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BFD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C7F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C81, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C83, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C85, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C87, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CB5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CB7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersH ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CB9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersI ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CBB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersJ ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CBD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersK ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D0B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersL ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D20, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersM ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D22, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersN ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D24, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrinkShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C27, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrinkShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C5B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrinkShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CA1, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrinkShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CE7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrinkShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C1B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewHelmShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BFF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewHunterShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C4D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewKitchenShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C19, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewKitchenShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C39, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewOldBookShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x19FF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewPotionShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewRuinedBookShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0xC14, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C35, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C3D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C69, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C7B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CB1, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CC3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfH ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D07, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShoeShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C37, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShoeShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C7D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShoeShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CB3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShoeShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D09, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSorcererShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C4F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSorcererShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C73, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSorcererShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CDB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSorcererShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CFD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSupplyShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C57, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSupplyShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C9D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSupplyShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CE3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTailorShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C3F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTailorShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C6D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTailorShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CC7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTailorShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CF9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTannerShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C23, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTannerShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C49, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTavernShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C25, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTavernShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C59, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTavernShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C9F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTavernShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CE5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTinkerShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C71, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTinkerShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CD9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTinkerShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D03, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTortureShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C2F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C17, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C1D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C21, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C55, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C9B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CE1, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Hatchet ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LumberAxe ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBox ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SmallCrate ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MediumCrate ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LargeCrate ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenChest ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LargeTable ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Nightstand ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( YewWoodTable ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Throne ), 24 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenThrone ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Stool ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FootStool ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FancyWoodenChairCushion ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenChairCushion ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenChair ), 8 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BambooChair ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBench ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Saw ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Scorp ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SmoothingPlane ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DrawKnife ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Froe ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Hammer ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Inshave ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodworkingTools ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JointingPlane ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MouldingPlane ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DovetailSaw ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Axle ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Club ), 13 ); } 
				
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenPlateArms ), 90 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenPlateChest ), 119 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenPlateGloves ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenPlateGorget ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenPlateLegs ), 106 ); } 
				
				if ( MyServerSettings.BuyChance() ){Add( typeof( Log ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AshLog ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CherryLog ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EbonyLog ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldenOakLog ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HickoryLog ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MahoganyLog ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DriftwoodLog ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OakLog ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PineLog ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GhostLog ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RosewoodLog ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WalnutLog ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ElvenLog ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PetrifiedLog ), 7 ); } 

				if ( MyServerSettings.BuyChance() ){Add( typeof( Board ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AshBoard ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CherryBoard ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EbonyBoard ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldenOakBoard ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HickoryBoard ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MahoganyBoard ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DriftwoodBoard ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OakBoard ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PineBoard ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GhostBoard ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RosewoodBoard ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WalnutBoard ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ElvenBoard ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PetrifiedBoard ), 8 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBCobbler : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBCobbler() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 	
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ThighBoots ), 100, Utility.Random( 1,100 ), 0x1711, Utility.RandomNeutralHue() ) ); } 
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Shoes ), 8, Utility.Random( 1,100 ), 0x170f, Utility.RandomNeutralHue() ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Boots ), 10, Utility.Random( 1,100 ), 0x170b, Utility.RandomNeutralHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Sandals ), 5, Utility.Random( 1,100 ), 0x170d, Utility.RandomNeutralHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherSandals ), 60, Utility.Random( 1,100 ), 0x170d, 0x83E ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherShoes ), 75, Utility.Random( 1,100 ), 0x170f, 0x83E ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherBoots ), 90, Utility.Random( 1,100 ), 0x170b, 0x83E ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherThighBoots ), 105, Utility.Random( 1,100 ), 0x1711, 0x83E ) ); }  
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicBoots ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Shoes ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Boots ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( ThighBoots ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Sandals ), 2 ); }  
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagicBoots ), 25 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherSandals ), 30 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherShoes ), 37 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherBoots ), 45 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherThighBoots ), 52 ); }  
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LeatherSoftBoots ), 60 ); }  
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBCook : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBCook() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BreadLoaf ), 50, Utility.Random( 1,100 ), 0x103B, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( BreadLoaf ), 50, Utility.Random( 1,100 ), 0x103C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ApplePie ), 170, Utility.Random( 1,100 ), 0x1041, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cake ), 1530, Utility.Random( 1,100 ), 0x9E9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Muffins ), 1300, Utility.Random( 1,100 ), 0x9EA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CheeseWheel ), 310, Utility.Random( 1,100 ), 0x97E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CookedBird ), 170, Utility.Random( 1,100 ), 0x9B7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LambLeg ), 80, Utility.Random( 1,100 ), 0x160A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ChickenLeg ), 50, Utility.Random( 1,100 ), 0x1608, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfCarrots ), 30, Utility.Random( 1,100 ), 0x15F9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfCorn ), 30, Utility.Random( 1,100 ), 0x15FA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfLettuce ), 30, Utility.Random( 1,100 ), 0x15FB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfPeas ), 30, Utility.Random( 1,100 ), 0x15FC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( EmptyPewterBowl ), 5, Utility.Random( 1,100 ), 0x15FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfCorn ), 30, Utility.Random( 1,100 ), 0x15FE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfLettuce ), 30, Utility.Random( 1,100 ), 0x15FF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfPeas ), 30, Utility.Random( 1,100 ), 0x1600, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfFoodPotatos ), 30, Utility.Random( 1,100 ), 0x1601, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfStew ), 30, Utility.Random( 1,100 ), 0x1604, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfTomatoSoup ), 30, Utility.Random( 1,100 ), 0x1606, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RoastPig ), 1060, Utility.Random( 1,100 ), 0x9BB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SackFlour ), 15, Utility.Random( 1,100 ), 0x1039, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RollingPin ), 25, Utility.Random( 1,100 ), 0x1043, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FlourSifter ), 25, Utility.Random( 1,100 ), 0x103E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1044567", typeof( Skillet ), 25, Utility.Random( 25,100 ), 0x97F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GardenTool ), 12, Utility.Random( 1,100 ), 0xDFD, 0x84F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HerbalistCauldron ), 247, 1, 0x2676, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 1,100 ), 0x1E27, 0x979 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( CBookDruidicHerbalism ), 50, Utility.Random( 1,100 ), 0x2D50, 0 ) ); }
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CheeseWheel ), 60 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CookedBird ), 40 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RoastPig ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cake ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SackFlour ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BreadLoaf ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ChickenLeg ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LambLeg ), 20 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Skillet ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FlourSifter ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RollingPin ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Muffins ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ApplePie ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfCarrots ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfCorn ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfLettuce ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfPeas ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmptyPewterBowl ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfCorn ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfLettuce ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfPeas ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfFoodPotatos ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfStew ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfTomatoSoup ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GardenTool ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HerbalistCauldron ), 123 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MixingSpoon ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlantHerbalism_Leaf ), Utility.Random( 3,7 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlantHerbalism_Flower ), Utility.Random( 3,7 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlantHerbalism_Mushroom ), Utility.Random( 3,7 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlantHerbalism_Lilly ), Utility.Random( 3,7 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlantHerbalism_Cactus ), Utility.Random( 3,7 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlantHerbalism_Grass ), Utility.Random( 3,7 ) ); } 
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBFarmer : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBFarmer() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cabbage ), 5, Utility.Random( 1,100 ), 0xC7B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cantaloupe ), 6, Utility.Random( 1,100 ), 0xC79, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Carrot ), 3, Utility.Random( 1,100 ), 0xC78, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HoneydewMelon ), 7, Utility.Random( 1,100 ), 0xC74, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Squash ), 3, Utility.Random( 1,100 ), 0xC72, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lettuce ), 5, Utility.Random( 1,100 ), 0xC70, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Onion ), 3, Utility.Random( 1,100 ), 0xC6D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pumpkin ), 11, Utility.Random( 1,100 ), 0xC6A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GreenGourd ), 3, Utility.Random( 1,100 ), 0xC66, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( YellowGourd ), 3, Utility.Random( 1,100 ), 0xC64, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Watermelon ), 7, Utility.Random( 1,100 ), 0xC5C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Eggs ), 3, Utility.Random( 1,100 ), 0x9B5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Milk, 120, Utility.Random( 1,100 ), 0x9AD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Peach ), 3, Utility.Random( 1,100 ), 0x9D2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pear ), 3, Utility.Random( 1,100 ), 0x994, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lemon ), 3, Utility.Random( 1,100 ), 0x1728, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lime ), 3, Utility.Random( 1,100 ), 0x172A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Grapes ), 8, Utility.Random( 1,1000 ), 0x9D1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Apple ), 5, Utility.Random( 10,1000 ), 0x9D0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WheatSheaf ), 5, Utility.Random( 50,1000 ), 0xF36, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Hops ), 5, Utility.Random( 10,1000 ), 0x1727, 0 ) ); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(PumpkinPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(CabbagePatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(MelonPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(TurnipPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(GourdPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(OnionPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(LettucePatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(SquashPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(HoneydewPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(PumpkinPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(CarrotPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(CantaloupePatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(CornPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(PotatoPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(BananaPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(CoconutPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DatePatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(GarlicPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(NightshadePatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(GinsengPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(MandrakePatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(FlaxPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(TomatoPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(GreenTeaPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(PeaPatchDeed), 2500, 10, 0x14F0, 0)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(HayPatchDeed), 2500, 10, 0x14F0, 0)); }
                if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CattleHaybale ), 17000, Utility.Random( 1,4 ), 0x50AE, 0 ) ); }

            } 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pitcher ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Eggs ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Apple ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Grapes ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Watermelon ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( YellowGourd ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreenGourd ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pumpkin ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Onion ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lettuce ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Squash ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Carrot ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HoneydewMelon ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cantaloupe ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cabbage ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lemon ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lime ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Peach ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pear ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WheatSheaf ), 1 ); }
				if ( MyServerSettings.BuyChance() ){Add( typeof( Hops ), 1 ); }  
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBFisherman : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBFisherman() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RawFishSteak ), 10, Utility.Random( 1,100 ), 0x97A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Fish ), 30, Utility.Random( 1,100 ), 0x9CC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Fish ), 30, Utility.Random( 1,100 ), 0x9CD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Fish ), 30, Utility.Random( 1,100 ), 0x9CE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Fish ), 30, Utility.Random( 1,100 ), 0x9CF, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( FishingPole ), 100, Utility.Random( 1,100 ), 0xDC0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GrapplingHook ), 58, Utility.Random( 1,100 ), 0x4F40, 0 ) ); }
				Add( new GenericBuyInfo( typeof( MovableTrashChest ), 5000, Utility.Random( 1,5 ), 0x2811, 0 ) );
				Add( new GenericBuyInfo( typeof( ArcherPoonBag ), 32, Utility.Random( 1,5 ), 0x2B02, 0 ) );
				
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RawFishSteak ), 5 ); } 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Fish ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FishingPole ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GrapplingHook ), 29 ); } 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MegalodonTooth ), Utility.RandomMinMax( 500, 2000 ) ); } 
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBFortuneTeller : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBFortuneTeller()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Bandage ), 2, Utility.Random( 10,60 ), 0xE21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LesserHealPotion ), 100, Utility.Random( 1,100 ), 0x25FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 1,100 ), 0xF85, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 1,100 ), 0xF84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RefreshPotion ), 100, Utility.Random( 1,100 ), 0xF0B, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( LesserHealPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RefreshPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Garlic ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ginseng ), 2 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBFurtrader : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBFurtrader() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Hides ), 4, Utility.Random( 1,100 ), 0x1078, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Leather ), 4, Utility.Random( 1,100 ), 0x1081, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Furs ), 5, Utility.Random( 1,25 ), 0x11F4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FursWhite ), 8, Utility.Random( 1,25 ), 0x11F4, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurCape ), 16, Utility.Random( 1,5 ), 0x230A, 0x907 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurRobe ), 20, Utility.Random( 1,5 ), 0x1F03, 0x907 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurBoots ), 10, Utility.Random( 1,5 ), 0x2307, 0x907 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurCap ), 8, Utility.Random( 1,5 ), 0x1DB9, 0x907 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurSarong ), 14, Utility.Random( 1,5 ), 0x230C, 0x907 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurArms ), 100, Utility.Random( 1,100 ), 0x2B77, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurTunic ), 121, Utility.Random( 1,100 ), 0x2B79, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FurLegs ), 100, Utility.Random( 1,100 ), 0x2B78, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurCape ), 18, Utility.Random( 1,5 ), 0x230A, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurRobe ), 24, Utility.Random( 1,5 ), 0x1F03, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurBoots ), 12, Utility.Random( 1,5 ), 0x2307, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurCap ), 16, Utility.Random( 1,5 ), 0x1DB9, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurSarong ), 16, Utility.Random( 1,5 ), 0x230C, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurArms ), 100, Utility.Random( 1,100 ), 0x2B77, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurTunic ), 121, Utility.Random( 1,100 ), 0x2B79, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteFurLegs ), 100, Utility.Random( 1,100 ), 0x2B78, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BearMask ), Utility.Random( 28,50 ), Utility.Random( 1,5 ), 0x1545, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DeerMask ), Utility.Random( 28,50 ), Utility.Random( 1,5 ), 0x1547, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WolfMask ), Utility.Random( 28,50 ), Utility.Random( 1,5 ), 0x2B6D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DemonSkin ), 1235, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DragonSkin ), 1235, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NightmareSkin ), 1228, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SerpentSkin ), 1214, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TrollSkin ), 1221, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( UnicornSkin ), 1228, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 ) ) ); }
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyChance() ){Add( typeof( UnicornSkin ), 30 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Furs ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FursWhite ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DemonSkin ), 40 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DragonSkin ), 50 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( NightmareSkin ), 30 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SerpentSkin ), 10 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( TrollSkin ), 20 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurCape ), 8 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurCape ), 9 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurRobe ), 10 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurRobe ), 12 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurBoots ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurBoots ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurSarong ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurSarong ), 8 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurCap ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurCap ), 8 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurArms ), 50 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurTunic ), 60 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FurLegs ), 50 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurArms ), 50 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurTunic ), 60 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteFurLegs ), 50 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( BearMask ), 14 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DeerMask ), 14 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WolfMask ), 14 ); }  

				if ( MyServerSettings.BuyChance() ){Add( typeof( Hides ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinedHides ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( HornedHides ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( BarbedHides ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( NecroticHides ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( VolcanicHides ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FrozenHides ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoliathHides ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DraconicHides ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( HellishHides ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DinosaurHides ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( AlienHides ), 7 ); }  

				if ( MyServerSettings.BuyChance() ){Add( typeof( Leather ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinedLeather ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( HornedLeather ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( BarbedLeather ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( NecroticLeather ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( VolcanicLeather ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FrozenLeather ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoliathLeather ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DraconicLeather ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( HellishLeather ), 8 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DinosaurLeather ), 8 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( AlienLeather ), 8 ); }  
			} 
		} 
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBGlassblower : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGlassblower()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bottle ), 5, Utility.Random( 1,100 ), 0xF0E, 0 ) ); } 
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); } 
				if ( 1 > 0 ){Add( new GenericBuyInfo( "Crafting Glass With Glassblowing", typeof( GlassblowingBook ), 20637, Utility.Random( 1,100 ), 0xFF4, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( "Finding Glass-Quality Sand", typeof( SandMiningBook ), 20637, Utility.Random( 1,100 ), 0xFF4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1044608", typeof( Blowpipe ), 21, Utility.Random( 1,100 ), 0xE8A, 0x3B9 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Monocle ), 24, Utility.Random( 1,25 ), 0x2C84, 0 ) ); } 
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bottle ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Jar ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GlassblowingBook ), 5000 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SandMiningBook ), 5000 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Blowpipe ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Monocle ), 12 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBHairStylist : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBHairStylist() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( "special beard dye", typeof( SpecialBeardDye ), 1500, Utility.Random( 1,100 ), 0xE26, 0 ) );
				Add( new GenericBuyInfo( "special hair dye", typeof( SpecialHairDye ), 1500, Utility.Random( 1,100 ), 0xE26, 0 ) );
				Add( new GenericBuyInfo( "1041060", typeof( HairDye ), 1000, Utility.Random( 1,100 ), 0xEFF, 0 ) );
				Add( new GenericBuyInfo( "hair dye bottle", typeof( HairDyeBottle ), 1000, Utility.Random( 1,100 ), 0xE0F, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DisguiseKit ), 700, Utility.Random( 1,5 ), 0xE05, 0 ) ); }
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				Add( typeof( HairDye ), 50 );
				Add( typeof( SpecialBeardDye ), 250 );
				Add( typeof( SpecialHairDye ), 250 );
				Add( typeof( HairDyeBottle ), 300 );
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBHealer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHealer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Bandage ), 3, Utility.Random( 100,250 ), 0xE21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LesserHealPotion ), 100, Utility.Random( 1,100 ), 0x25FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 1,100 ), 0xF85, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 1,100 ), 0xF84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RefreshPotion ), 100, Utility.Random( 1,100 ), 0xF0B, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GraveShovel ), 12, Utility.Random( 1,100 ), 0xF39, 0x966 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( SurgeonsKnife ), 14, Utility.Random( 1,100 ), 0xEC4, 0x1B0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HealingDragonJar ), 2500, Utility.Random( 1,5 ), 0xF39, 0x966 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( LesserHealPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RefreshPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Garlic ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ginseng ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SurgeonsKnife ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FirstAidKit ), Utility.Random( 100,250 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBDruid : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBDruid()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Bandage ), 3, Utility.Random( 100,250 ), 0xE21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LesserHealPotion ), 100, Utility.Random( 1,100 ), 0x25FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 1,100 ), 0xF85, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 1,100 ), 0xF84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RefreshPotion ), 100, Utility.Random( 1,100 ), 0xF0B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GardenTool ), 12, Utility.Random( 1,100 ), 0xDFD, 0x84F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HerbalistCauldron ), 247, 1, 0x2676, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 1,100 ), 0x1E27, 0x979 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( CBookDruidicHerbalism ), 50, Utility.Random( 1,100 ), 0x2D50, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AppleTreeDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CherryBlossomTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DarkBrownTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GreyTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LightBrownTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PeachTreeDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PearTreeDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( VinePatchAddonDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HopsPatchDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) ); } 
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( AlchemyPouch ), Utility.Random( 2900,3500 ), Utility.Random( 1,2 ), 0x1C10, 0x89F ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HealingDragonJar ), 500, Utility.Random( 1,5 ), 0xF39, 0x966 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( LesserHealPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RefreshPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Garlic ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ginseng ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GardenTool ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HerbalistCauldron ), 123 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MixingSpoon ), 17 ); } 
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBDruidTree : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBDruidTree()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Bandage ), 2, Utility.Random( 10,150 ), 0xE21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LesserHealPotion ), 100, Utility.Random( 1,100 ), 0x25FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 1,100 ), 0xF85, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 1,100 ), 0xF84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RefreshPotion ), 100, Utility.Random( 1,100 ), 0xF0B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GardenTool ), 12, Utility.Random( 1,100 ), 0xDFD, 0x84F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HerbalistCauldron ), 247, 1, 0x2676, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 1,100 ), 0x1E27, 0x979 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( CBookDruidicHerbalism ), 50, Utility.Random( 1,100 ), 0x2D50, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) ); } 
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( AlchemyPouch ), Utility.Random( 2900,3500 ), Utility.Random( 1,2 ), 0x1C10, 0x89F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AppleTreeDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CherryBlossomTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DarkBrownTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GreyTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LightBrownTreeDeed ), 15400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PeachTreeDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PearTreeDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( VinePatchAddonDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HopsPatchDeed ), 16400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ShieldOfEarthPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x300 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( WoodlandProtectionPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x7E2 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ProtectiveFairyPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x9FF ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HerbalHealingPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x279 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GraspingRootsPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x83F ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( BlendWithForestPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x59C ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( SwarmOfInsectsPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xA70 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( VolcanicEruptionPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x54E ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( TreefellowPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x223 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( StoneCirclePotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x396 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( DruidicRunePotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x487 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( LureStonePotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x967 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NaturesPassagePotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x48B ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( MushroomGatewayPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x3B7 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( RestorativeSoilPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x479 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( FireflyPotion ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x491 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( LesserHealPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RefreshPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Garlic ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ginseng ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GardenTool ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HerbalistCauldron ), 123 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MixingSpoon ), 17 ); } 
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBHerbalist : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBHerbalist() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 1,100 ), 0xF85, 0 ) ); } 
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 1,100 ), 0xF84, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MandrakeRoot ), 3, Utility.Random( 1,100 ), 0xF86, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Nightshade ), 3, Utility.Random( 1,100 ), 0xF88, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bloodmoss ), 5, Utility.Random( 1,100 ), 0xF7B, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MortarPestle ), 8, Utility.Random( 1,100 ), 0x4CE9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GardenTool ), 12, Utility.Random( 1,100 ), 0xDFD, 0x84F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HerbalistCauldron ), 247, 1, 0x2676, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 1,100 ), 0x1E27, 0x979 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( CBookDruidicHerbalism ), 50, Utility.Random( 1,100 ), 0x2D50, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) ); } 
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( AlchemyPouch ), Utility.Random( 2900,3500 ), Utility.Random( 1,2 ), 0x1C10, 0x89F ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HangingPlantA ), Utility.Random( 5000,10000 ), 1, 0x113F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HangingPlantB ), Utility.Random( 5000,10000 ), 1, 0x1151, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HangingPlantC ), Utility.Random( 5000,10000 ), 1, 0x1164, 0 ) ); }
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bloodmoss ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( MandrakeRoot ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Garlic ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ginseng ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Nightshade ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Jar ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( MortarPestle ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GardenTool ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HerbalistCauldron ), 123 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MixingSpoon ), 17 ); } 
				Add( typeof( PlantHerbalism_Flower ), Utility.Random( 2, 6 ) );
				Add( typeof( PlantHerbalism_Leaf ), Utility.Random( 2, 6 ) );
				Add( typeof( PlantHerbalism_Mushroom ), Utility.Random( 2, 6 ) );
				Add( typeof( PlantHerbalism_Lilly ), Utility.Random( 2, 6 ) );
				Add( typeof( PlantHerbalism_Cactus ), Utility.Random( 2, 6 ) );
				Add( typeof( HomePlants_Flower ), Utility.Random( 100, 300 ) );
				Add( typeof( HomePlants_Leaf ), Utility.Random( 100, 300 ) );
				Add( typeof( HomePlants_Mushroom ), Utility.Random( 100, 300 ) );
				Add( typeof( HomePlants_Cactus ), Utility.Random( 100, 300 ) );
				Add( typeof( HomePlants_Lilly ), Utility.Random( 100, 300 ) );
				Add( typeof( HomePlants_Grass ), Utility.Random( 100, 300 ) );
				Add( typeof( SpecialSeaweed ), Utility.Random( 15, 35 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( HangingPlantA ), Utility.Random( 10, 100 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( HangingPlantB ), Utility.Random( 10, 100 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( HangingPlantC ), Utility.Random( 10, 100 ) ); }  
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBHolyMage : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHolyMage()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Spellbook ), 18, Utility.Random( 1,100 ), 0xEFA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ScribesPen ), 8, Utility.Random( 1,100 ), 0x2051, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlankScroll ), 5, Utility.Random( 1,100 ), 0x0E34, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041072", typeof( MagicWizardsHat ), 11, Utility.Random( 1,100 ), 0x1718, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WitchHat ), 11, Utility.Random( 1,100 ), 0x2FC3, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RecallRune ), 100, Utility.Random( 1,100 ), 0x1f14, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlackPearl ), 5, Utility.Random( 1,100 ), 0x266F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bloodmoss ), 5, Utility.Random( 1,100 ), 0xF7B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 1,100 ), 0xF84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 1,100 ), 0xF85, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MandrakeRoot ), 3, Utility.Random( 1,100 ), 0xF86, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Nightshade ), 3, Utility.Random( 1,100 ), 0xF88, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SpidersSilk ), 3, Utility.Random( 1,100 ), 0xF8D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SulfurousAsh ), 3, Utility.Random( 1,100 ), 0xF8C, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( reagents_magic_jar1 ), 2000, Utility.Random( 1,100 ), 0x1007, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardStaff ), 40, Utility.Random( 1,5 ), 0x0908, 0xB3A ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardStick ), 38, Utility.Random( 1,5 ), 0xDF2, 0xB3A ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MageEye ), 2, Utility.Random( 10,150 ), 0xF19, 0xB78 ) ); }


				Type[] types = Loot.RegularScrollTypes;

				for ( int i = 0; i < types.Length && i < 8; ++i )
				{
					int itemID = 0x1F2E + i;

					if ( i == 6 )
						itemID = 0x1F2D;
					else if ( i > 6 )
						--itemID;

					if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( types[i], 12 + ((i / 8) * 10), Utility.Random( 1,100 ), itemID, 0 ) ); }
				}
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicTalisman ), Utility.Random( 50,100 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlackPearl ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bloodmoss ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( MandrakeRoot ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Garlic ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ginseng ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Nightshade ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpidersSilk ), 2 ); }  
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SulfurousAsh ), 1 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( RecallRune ), 8 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Spellbook ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlankScroll ), 3 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( MysticalPearl ), 250 ); } 

				Type[] types = Loot.RegularScrollTypes;

				for ( int i = 0; i < types.Length; ++i )
					if ( MyServerSettings.BuyChance() ){Add( types[i], ((i / 8) + 2) * 5 ); } 

				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ClumsyMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CreateFoodMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FeebleMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( HealMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicArrowMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( NightSightMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ReactiveArmorMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WeaknessMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( AgilityMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CunningMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CureMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( HarmMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicTrapMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicUntrapMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ProtectionMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( StrengthMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( BlessMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireballMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicLockMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicUnlockMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PoisonMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( TelekinesisMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( TeleportMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WallofStoneMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ArchCureMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ArchProtectionMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CurseMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireFieldMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( GreaterHealMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( LightningMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ManaDrainMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( RecallMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( BladeSpiritsMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( DispelFieldMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( IncognitoMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicReflectionMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MindBlastMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ParalyzeMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PoisonFieldMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( SummonCreatureMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( DispelMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyBoltMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ExplosionMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( InvisibilityMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MarkMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MassCurseMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ParalyzeFieldMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( RevealMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ChainLightningMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyFieldMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FlameStrikeMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( GateTravelMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ManaVampireMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MassDispelMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MeteorSwarmMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PolymorphMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( AirElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EarthElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EarthquakeMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyVortexMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ResurrectionMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( SummonDaemonMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WaterElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MySpellbook ), Utility.Random( 250,1000 ) ); } 
				Add( typeof( TomeOfWands ), Utility.Random( 100,400 ) );
				if ( MyServerSettings.BuyChance() ){ Add( typeof( WizardStaff ), 20 ); } 
				if ( MyServerSettings.BuyChance() ){ Add( typeof( WizardStick ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){ Add( typeof( MageEye ), 1 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBRuneCasting: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRuneCasting()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( RuneMagicBook ), 500 );
				Add( typeof( RuneBag ), 200 );
				Add( typeof( An ), 50 );
				Add( typeof( Bet ), 50 );
				Add( typeof( Corp ), 50 );
				Add( typeof( Des ), 50 );
				Add( typeof( Ex ), 50 );
				Add( typeof( Flam ), 50 );
				Add( typeof( Grav ), 50 );
				Add( typeof( Hur ), 50 );
				Add( typeof( In ), 50 );
				Add( typeof( Jux ), 50 );
				Add( typeof( Kal ), 50 );
				Add( typeof( Lor ), 50 );
				Add( typeof( Mani ), 50 );
				Add( typeof( Nox ), 50 );
				Add( typeof( Ort ), 50 );
				Add( typeof( Por ), 50 );
				Add( typeof( Quas ), 50 );
				Add( typeof( Rel ), 50 );
				Add( typeof( Sanct ), 50 );
				Add( typeof( Tym ), 50 );
				Add( typeof( Uus ), 50 );
				Add( typeof( Vas ), 50 );
				Add( typeof( Wis ), 50 );
				Add( typeof( Xen ), 50 );
				Add( typeof( Ylem ), 50 );
				Add( typeof( Zu ), 50 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBEnchanter : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBEnchanter()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ClumsyMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CreateFoodMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( FeebleMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HealMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( MagicArrowMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( NightSightMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ReactiveArmorMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( WeaknessMagicStaff ), Utility.Random( 100,200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }

				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( AgilityMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CunningMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CureMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HarmMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( MagicTrapMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( MagicUntrapMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ProtectionMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( StrengthMagicStaff ), Utility.Random( 200,400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }

				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( BlessMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( FireballMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( MagicLockMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( MagicUnlockMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( PoisonMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( TelekinesisMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( TeleportMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( WallofStoneMagicStaff ), Utility.Random( 300,600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }

				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( ArchCureMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( ArchProtectionMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( CurseMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( FireFieldMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( GreaterHealMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( LightningMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( ManaDrainMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( RecallMagicStaff ), Utility.Random( 400,800 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }

				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( BladeSpiritsMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( DispelFieldMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( IncognitoMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( MagicReflectionMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( MindBlastMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( ParalyzeMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( PoisonFieldMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( SummonCreatureMagicStaff ), Utility.Random( 500,1000 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }

				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( DispelMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( EnergyBoltMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( ExplosionMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( InvisibilityMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( MarkMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( MassCurseMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( ParalyzeFieldMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( RevealMagicStaff ), Utility.Random( 600,1200 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }

				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( ChainLightningMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( EnergyFieldMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( FlameStrikeMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( GateTravelMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( ManaVampireMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( MassDispelMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( MeteorSwarmMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( PolymorphMagicStaff ), Utility.Random( 700,1400 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }

				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( AirElementalMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( EarthElementalMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( EarthquakeMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( EnergyVortexMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( FireElementalMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( ResurrectionMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( SummonDaemonMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( typeof( WaterElementalMagicStaff ), Utility.Random( 800,1600 ), 1, Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ), 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBHouseDeed: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHouseDeed()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBInnKeeper : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBInnKeeper()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Ale, 70, Utility.Random( 1,100 ), 0x99F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Wine, 70, Utility.Random( 1,100 ), 0x9C7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Liquor, 70, Utility.Random( 1,100 ), 0x99B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Jug ), BeverageType.Cider, 130, Utility.Random( 1,100 ), 0x9C8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Milk, 150, Utility.Random( 1,100 ), 0x9F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Ale, 110, Utility.Random( 1,100 ), 0x1F95, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Cider, 110, Utility.Random( 1,100 ), 0x1F97, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Liquor, 110, Utility.Random( 1,100 ), 0x1F99, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Wine, 110, Utility.Random( 1,100 ), 0x1F9B, 0 ) ); }
				if ( 1 > 0 ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Water, 90, Utility.Random( 1,100 ), 0x1F9D, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( BreadLoaf ), 60, Utility.Random( 1,100 ), 0x103B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CheeseWheel ), 410, Utility.Random( 1,100 ), 0x97E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CookedBird ), 120, Utility.Random( 1,100 ), 0x9B7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LambLeg ), 90, Utility.Random( 1,100 ), 0x160A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ChickenLeg ), 90, Utility.Random( 1,100 ), 0x1608, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ribs ), 90, Utility.Random( 1,100 ), 0x9F2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfCarrots ), 30, Utility.Random( 1,100 ), 0x15F9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfCorn ), 30, Utility.Random( 1,100 ), 0x15FA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfLettuce ), 30, Utility.Random( 1,100 ), 0x15FB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfPeas ), 30, Utility.Random( 1,100 ), 0x15FC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( EmptyPewterBowl ), 5, Utility.Random( 1,100 ), 0x15FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfCorn ), 30, Utility.Random( 1,100 ), 0x15FE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfLettuce ), 30, Utility.Random( 1,100 ), 0x15FF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfPeas ), 30, Utility.Random( 1,100 ), 0x1600, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfFoodPotatos ), 30, Utility.Random( 1,100 ), 0x1601, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfStew ), 30, Utility.Random( 1,100 ), 0x1604, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfTomatoSoup ), 30, Utility.Random( 1,100 ), 0x1606, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ApplePie ), 270, Utility.Random( 1,100 ), 0x1041, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Peach ), 30, Utility.Random( 1,100 ), 0x9D2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pear ), 30, Utility.Random( 1,100 ), 0x994, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Grapes ), 30, Utility.Random( 1,100 ), 0x9D1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Apple ), 30, Utility.Random( 1,100 ), 0x9D0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Banana ), 20, Utility.Random( 1,100 ), 0x171F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Torch ), 7, Utility.Random( 1,100 ), 0xF6B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Candle ), 6, Utility.Random( 1,100 ), 0xA28, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backpack ), 100, Utility.Random( 1,100 ), 0x53D5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( tarotpoker ), 5, Utility.Random( 1,100 ), 0x12AB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016450", typeof( Chessboard ), 2, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016449", typeof( CheckerBoard ), 2, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backgammon ), 2, Utility.Random( 1,100 ), 0xE1C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Dices ), 2, Utility.Random( 1,100 ), 0xFA7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Engines.Mahjong.MahjongGame ), 6, Utility.Random( 1,100 ), 0xFAA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HenchmanFighterItem ), 5000, Utility.Random( 1,100 ), 0x1419, 0xB96 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HenchmanArcherItem ), 6000, Utility.Random( 1,100 ), 0xF50, 0xB96 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HenchmanWizardItem ), 7000, Utility.Random( 1,100 ), 0xE30, 0xB96 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041243", typeof( ContractOfEmployment ), 1252, Utility.Random( 1,100 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "a barkeep contract", typeof( BarkeepContract ), 1252, Utility.Random( 1,100 ), 0x14F0, 0 ) ); }
				if ( Multis.BaseHouse.NewVendorSystem )
					if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1062332", typeof( VendorRentalContract ), 1252, Utility.Random( 1,100 ), 0x14F0, 0x672 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( BeverageBottle ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Jug ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pitcher ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GlassMug ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BreadLoaf ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CheeseWheel ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ribs ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Peach ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pear ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Grapes ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Apple ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Banana ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Torch ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Candle ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( tarotpoker ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MahjongGame ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Chessboard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CheckerBoard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Backgammon ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Dices ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ContractOfEmployment ), 626 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfCarrots ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfCorn ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfLettuce ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfPeas ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmptyPewterBowl ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfCorn ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfLettuce ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfPeas ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfFoodPotatos ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfStew ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfTomatoSoup ), 5 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBJewel: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBJewel()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldRing ), 27, Utility.Random( 1,100 ), 0x4CFA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Necklace ), 26, Utility.Random( 1,100 ), 0x4CFE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldNecklace ), 27, Utility.Random( 1,100 ), 0x4CFF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldBeadNecklace ), 27, Utility.Random( 1,100 ), 0x4CFD, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Beads ), 27, Utility.Random( 1,100 ), 0x4CFE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldBracelet ), 27, Utility.Random( 1,100 ), 0x4CF1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldEarrings ), 27, Utility.Random( 1,100 ), 0x4CFB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1060740", typeof( BroadcastCrystal ),  68, Utility.Random( 1,100 ), 0x1ED0, 0, new object[] {  500 } ) ); } // 500 charges
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1060740", typeof( BroadcastCrystal ), 131, Utility.Random( 1,100 ), 0x1ED0, 0, new object[] { 1000 } ) ); } // 1000 charges
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1060740", typeof( BroadcastCrystal ), 256, Utility.Random( 1,100 ), 0x1ED0, 0, new object[] { 2000 } ) ); } // 2000 charges
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1060740", typeof( ReceiverCrystal ), 6, Utility.Random( 1,100 ), 0x1ED0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StarSapphire ), 125, Utility.Random( 1,100 ), 0xF21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Emerald ), 100, Utility.Random( 1,100 ), 0xF10, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Sapphire ), 100, Utility.Random( 1,100 ), 0xF19, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ruby ), 75, Utility.Random( 1,100 ), 0xF13, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Citrine ), 50, Utility.Random( 1,100 ), 0xF15, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Amethyst ), 100, Utility.Random( 1,100 ), 0xF16, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tourmaline ), 75, Utility.Random( 1,100 ), 0xF2D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Amber ), 50, Utility.Random( 1,100 ), 0xF25, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Diamond ), 200, Utility.Random( 1,100 ), 0xF26, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MageEye ), 2, Utility.Random( 10,150 ), 0xF19, 0xB78 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Crystals ), 5 );
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amber ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amethyst ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Citrine ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Diamond ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Emerald ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ruby ), 37 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Sapphire ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphire ), 62 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tourmaline ), 47 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Krystal ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MageEye ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldRing ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverRing ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Necklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBeadNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBeadNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Beads ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBracelet ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBracelet ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldEarrings ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverEarrings ), 10 ); } 
				if ( MyServerSettings.SellChance() ){Add( typeof( MysticalPearl ), 500 ); } 
				if ( MyServerSettings.SellChance() ){Add( typeof( LargeCrystal ), Utility.Random( 500,1000 ) ); } 
				Add( typeof( MagicJewelryRing ), Utility.Random( 50,300 ) );
				Add( typeof( MagicJewelryCirclet ), Utility.Random( 50,300 ) );
				Add( typeof( MagicJewelryNecklace ), Utility.Random( 50,300 ) );
				Add( typeof( MagicJewelryEarrings ), Utility.Random( 50,300 ) );
				Add( typeof( MagicJewelryBracelet ), Utility.Random( 50,300 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( AgapiteAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AgapiteBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AgapiteRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AgapiteEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmberAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmberBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmberRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmberEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BrassAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BrassBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BrassRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BrassEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BronzeAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BronzeBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BronzeRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BronzeEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CaddelliteAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CaddelliteBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CaddelliteRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CaddelliteEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CopperAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CopperBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CopperRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CopperEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DiamondAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DiamondBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DiamondRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DiamondEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DullCopperAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DullCopperBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DullCopperRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DullCopperEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldenAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldenBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldenRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldenEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadeAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadeBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadeRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadeEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MithrilAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MithrilBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MithrilRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MithrilEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( NepturiteAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( NepturiteBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( NepturiteRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( NepturiteEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ObsidianAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ObsidianBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ObsidianRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ObsidianEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PearlAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PearlBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PearlRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PearlEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphireAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphireBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphireRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphireEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShadowIronAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShadowIronBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShadowIronRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShadowIronEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilveryAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilveryBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilveryRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilveryEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphireAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphireBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphireRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphireEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SteelAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SteelBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SteelRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SteelEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TourmalineAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TourmalineBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TourmalineRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TourmalineEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ValoriteAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ValoriteBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ValoriteRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ValoriteEarrings ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( VeriteAmulet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( VeriteBracelet ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( VeriteRing ), Utility.Random( 11,16 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( VeriteEarrings ), Utility.Random( 11,16 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBWarriorGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBWarriorGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "warhorse", typeof( Warhorse ), 250000, Utility.Random( 1,10 ), 0x55DC, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBKeeperOfChivalry : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBKeeperOfChivalry()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BookOfChivalry ), 140, Utility.Random( 1,100 ), 0x2252, 0 ) );
				Add( new GenericBuyInfo( "silver griffon", typeof( PaladinWarhorse ), 500000, Utility.Random( 1,10 ), 0x4C59, 0x99B ) );
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GwennoGraveAddonDeed ), Utility.Random( 5000,10000 ), 1, 0x14F0, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( MyPaladinbook ), Utility.Random( 50,200 ) ); } 
				Add( typeof( BookOfChivalry ), 70 );
				Add( typeof( HolyManSpellbook ), Utility.Random( 50,200 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBLeatherWorker: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBLeatherWorker() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Hides ), 4, Utility.Random( 1,100 ), 0x1078, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Leather ), 4, Utility.Random( 1,100 ), 0x1081, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Waterskin ), 5, Utility.Random( 1,100 ), 0xA21, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DemonSkin ), 1235, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DragonSkin ), 1235, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NightmareSkin ), 1228, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SerpentSkin ), 1214, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TrollSkin ), 1221, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( UnicornSkin ), 1228, Utility.Random( 1,10 ), 0x1081, Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 ) ) ); }
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( ThighBoots ), 28 ); } 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagicBoots ), 25 ); }  
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagicBelt ), 100 ); }  
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagicSash ), 100 ); }  
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ThrowingGloves ), 10 ); } 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PugilistGlove ), 10 ); } 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PugilistGloves ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( UnicornSkin ), 30 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DemonSkin ), 40 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DragonSkin ), 50 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( NightmareSkin ), 30 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SerpentSkin ), 10 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( TrollSkin ), 20 ); }  

				if ( MyServerSettings.BuyChance() ){Add( typeof( Hides ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinedHides ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( HornedHides ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( BarbedHides ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( NecroticHides ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( VolcanicHides ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FrozenHides ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoliathHides ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DraconicHides ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( HellishHides ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DinosaurHides ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( AlienHides ), 7 ); }  

				if ( MyServerSettings.BuyChance() ){Add( typeof( Leather ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinedLeather ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( HornedLeather ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( BarbedLeather ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( NecroticLeather ), 5 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( VolcanicLeather ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( FrozenLeather ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoliathLeather ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DraconicLeather ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( HellishLeather ), 8 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DinosaurLeather ), 8 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( AlienLeather ), 8 ); }  

				if ( MyServerSettings.BuyChance() ){Add( typeof( Waterskin ), 2 ); } 
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMapmaker : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMapmaker()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlankMap ), 5, Utility.Random( 1,100 ), 0x14EC, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( MapmakersPen ), 8, Utility.Random( 1,100 ), 0x2052, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( BlankScroll ), 12, Utility.Random( 50,200 ), 0xEF3, 0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){Add( new GenericBuyInfo( typeof( MasterSkeletonsKey ), Utility.Random( 750,1000 ), 25, 0x410B, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlankScroll ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MapmakersPen ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlankMap ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CityMap ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LocalMap ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WorldMap ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PresetMapEntry ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WorldMapLodor ), Utility.Random( 10,150 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WorldMapSosaria ), Utility.Random( 10,150 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WorldMapBottle ), Utility.Random( 10,150 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WorldMapSerpent ), Utility.Random( 10,150 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WorldMapUmber ), Utility.Random( 10,150 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WorldMapAmbrosia ), Utility.Random( 10,150 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WorldMapIslesOfDread ), Utility.Random( 10,150 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WorldMapSavage ), Utility.Random( 10,150 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WorldMapUnderworld ), Utility.Random( 20,300 ) ); } 
				Add( typeof( AlternateRealityMap ), Utility.Random( 500,1000 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMiller : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBMiller() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( typeof( SackFlour ), 6, Utility.Random( 1,100 ), 0x1039, 0 ) );
				Add( new GenericBuyInfo( typeof( WheatSheaf ), 6, Utility.Random( 1,100 ), 0xF36, 0 ) );
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SackFlour ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WheatSheaf ), 2 ); } 
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMiner: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMiner()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bag ), 6, Utility.Random( 1,100 ), 0xE76, 0xABE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Candle ), 6, Utility.Random( 1,100 ), 0xA28, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Torch ), 8, Utility.Random( 1,100 ), 0xF6B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lantern ), 2, Utility.Random( 1,100 ), 0xA25, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pickaxe ), 25, Utility.Random( 1,100 ), 0xE86, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Shovel ), 12, Utility.Random( 1,100 ), 0xF39, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( OreShovel ), 10, Utility.Random( 1,100 ), 0xF39, 0x96D ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pickaxe ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Shovel ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OreShovel ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lantern ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Torch ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bag ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Candle ), 3 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMonk : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMonk()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( MonkRobe ), 136, Utility.Random( 1,100 ), 0x204E, 0x21E ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBPlayerBarkeeper : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBPlayerBarkeeper()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Ale, 7, Utility.Random( 1,100 ), 0x99F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Wine, 7, Utility.Random( 1,100 ), 0x9C7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Liquor, 7, Utility.Random( 1,100 ), 0x99B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Jug ), BeverageType.Cider, 13, Utility.Random( 1,100 ), 0x9C8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Milk, 180, Utility.Random( 1,100 ), 0x9F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Ale, 11, Utility.Random( 1,100 ), 0x1F95, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Cider, 11, Utility.Random( 1,100 ), 0x1F97, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Liquor, 11, Utility.Random( 1,100 ), 0x1F99, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Wine, 11, Utility.Random( 1,100 ), 0x1F9B, 0 ) ); }
				if ( 1 > 0 ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Water, 11, Utility.Random( 1,100 ), 0x1F9D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( tarotpoker ), 5, Utility.Random( 1,100 ), 0x12AB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016450", typeof( Chessboard ), 2, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016449", typeof( CheckerBoard ), 2, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backgammon ), 2, Utility.Random( 1,100 ), 0xE1C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Dices ), 2, Utility.Random( 1,100 ), 0xFA7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Engines.Mahjong.MahjongGame ), 6, Utility.Random( 1,100 ), 0xFAA, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBProvisioner : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBProvisioner()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1060834", typeof( Engines.Plants.PlantBowl ), 2, Utility.Random( 1,100 ), 0x15FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Arrow ), 6, Utility.Random( 1,100 ), 0xF3F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bolt ), 7, Utility.Random( 1,100 ), 0x1BFB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backpack ), 100, Utility.Random( 1,100 ), 0x53D5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pouch ), 6, Utility.Random( 1,100 ), 0xE79, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Bag ), 6, Utility.Random( 1,100 ), 0xE76, 0xABE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Candle ), 6, Utility.Random( 1,100 ), 0xA28, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Torch ), 8, Utility.Random( 1,100 ), 0xF6B, 0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){Add( new GenericBuyInfo( typeof( TenFootPole ), Utility.Random( 500,1000 ), Utility.Random( 1,100 ), 0xE8A, 0x972 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lantern ), 2, Utility.Random( 1,100 ), 0xA25, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lockpick ), 12, Utility.Random( 1,100 ), 0x14FC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FloppyHat ), 7, Utility.Random( 1,100 ), 0x1713, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WideBrimHat ), 8, Utility.Random( 1,100 ), 0x1714, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cap ), 10, Utility.Random( 1,100 ), 0x1715, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TallStrawHat ), 8, Utility.Random( 1,100 ), 0x1716, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StrawHat ), 7, Utility.Random( 1,100 ), 0x1717, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardsHat ), 11, Utility.Random( 1,100 ), 0x1718, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WitchHat ), 11, Utility.Random( 1,100 ), 0x2FC3, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherCap ), 10, Utility.Random( 1,100 ), 0x1DB9, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FeatheredHat ), 10, Utility.Random( 1,100 ), 0x171A, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TricorneHat ), 8, Utility.Random( 1,100 ), 0x171B, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PirateHat ), 8, Utility.Random( 1,100 ), 0x2FBC, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bandana ), 6, Utility.Random( 1,100 ), 0x1540, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SkullCap ), 7, Utility.Random( 1,100 ), 0x1544, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ThrowingWeapon ), 2, Utility.Random( 20, 120 ), 0x52B2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BreadLoaf ), 60, Utility.Random( 1,100 ), 0x103B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LambLeg ), 80, Utility.Random( 1,100 ), 0x160A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ChickenLeg ), 100, Utility.Random( 1,100 ), 0x1608, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CookedBird ), 170, Utility.Random( 1,100 ), 0x9B7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Ale, 70, Utility.Random( 1,100 ), 0x99F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Wine, 70, Utility.Random( 1,100 ), 0x9C7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Liquor, 70, Utility.Random( 1,100 ), 0x99B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Jug ), BeverageType.Cider, 130, Utility.Random( 1,100 ), 0x9C8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pear ), 30, Utility.Random( 1,100 ), 0x994, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Apple ), 30, Utility.Random( 1,100 ), 0x9D0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 1,100 ), 0xF84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 1,100 ), 0xF85, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bottle ), 5, Utility.Random( 1,100 ), 0xF0E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Waterskin ), 5, Utility.Random( 1,100 ), 0xA21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RedBook ), 100, Utility.Random( 1,100 ), 0xFF1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlueBook ), 100, Utility.Random( 1,100 ), 0xFF2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TanBook ), 100, Utility.Random( 1,100 ), 0xFF0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBox ), 14, Utility.Random( 1,100 ), 0xE7D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Key ), 2, Utility.Random( 1,100 ), 0x100E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MerchantCrate ), 500, 1, 0xE3D, 0x83F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bedroll ), 5, Utility.Random( 1,100 ), 0xA59, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmallTent ), 200, Utility.Random( 1,5 ), 0x1914, Utility.RandomList( 0x96D, 0x96E, 0x96F, 0x970, 0x971, 0x972, 0x973, 0x974, 0x975, 0x976, 0x977, 0x978, 0x979, 0x97A, 0x97B, 0x97C, 0x97D, 0x97E ) ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CampersTent ), 500, Utility.Random( 1,5 ), 0x0A59, Utility.RandomList( 0x96D, 0x96E, 0x96F, 0x970, 0x971, 0x972, 0x973, 0x974, 0x975, 0x976, 0x977, 0x978, 0x979, 0x97A, 0x97B, 0x97C, 0x97D, 0x97E ) ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Kindling ), 2, Utility.Random( 1,100 ), 0xDE1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( tarotpoker ), 5, Utility.Random( 1,100 ), 0x12AB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016450", typeof( Chessboard ), 2, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016449", typeof( CheckerBoard ), 2, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backgammon ), 2, Utility.Random( 1,100 ), 0xE1C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Engines.Mahjong.MahjongGame ), 6, Utility.Random( 1,100 ), 0xFAA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Dices ), 2, Utility.Random( 1,100 ), 0xFA7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmallBagBall ), 3, Utility.Random( 1,100 ), 0x2256, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LargeBagBall ), 3, Utility.Random( 1,100 ), 0x2257, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BagOfNecroReagents ), 2000, 25, 0xE76, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BagOfReagents ), 2000, 25, 0xE76, 0 ) ); }
			

				if( !Guild.NewGuildSystem )
					if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041055", typeof( GuildDeed ), 12450, Utility.Random( 1,100 ), 0x14F0, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( IvoryTusk ), Utility.Random( 50,250 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MerchantCrate ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SmallTent ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CampersTent ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Arrow ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bolt ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Backpack ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pouch ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bag ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Candle ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Torch ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lantern ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lockpick ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FloppyHat ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WideBrimHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cap ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TallStrawHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StrawHat ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WizardsHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WitchHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherCap ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FeatheredHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TricorneHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PirateHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bandana ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullCap ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ThrowingWeapon ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Waterskin ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bottle ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Jar ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RedBook ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlueBook ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TanBook ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBox ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Kindling ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( tarotpoker ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MahjongGame ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Chessboard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CheckerBoard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Backgammon ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Dices ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amber ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amethyst ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Citrine ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Diamond ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Emerald ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ruby ), 37 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Sapphire ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphire ), 62 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tourmaline ), 47 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldRing ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverRing ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Necklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBeadNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBeadNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Beads ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBracelet ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBracelet ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldEarrings ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverEarrings ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryRing ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryCirclet ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryNecklace ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryEarrings ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryBracelet ), Utility.Random( 50,300 ) ); } 

				if( !Guild.NewGuildSystem )
					if ( MyServerSettings.BuyChance() ){Add( typeof( GuildDeed ), 6225 ); } 
			}
		}
	}
///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBBasement : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBasement()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1060834", typeof( Engines.Plants.PlantBowl ), 2, Utility.Random( 1,100 ), 0x15FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Arrow ), 3, Utility.Random( 50,100 ), 0xF3F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bolt ), 3, Utility.Random( 50,100 ), 0x1BFB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backpack ), 100, Utility.Random( 1,100 ), 0x53D5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pouch ), 6, Utility.Random( 1,100 ), 0xE79, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Bag ), 6, Utility.Random( 1,100 ), 0xE76, 0xABE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Candle ), 6, Utility.Random( 1,100 ), 0xA28, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Torch ), 8, Utility.Random( 1,100 ), 0xF6B, 0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){Add( new GenericBuyInfo( typeof( TenFootPole ), Utility.Random( 500,1000 ), Utility.Random( 1,100 ), 0xE8A, 0x972 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lantern ), 2, Utility.Random( 1,100 ), 0xA25, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lockpick ), 12, Utility.Random( 1,100 ), 0x14FC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FloppyHat ), 7, Utility.Random( 1,100 ), 0x1713, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WideBrimHat ), 8, Utility.Random( 1,100 ), 0x1714, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cap ), 10, Utility.Random( 1,100 ), 0x1715, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TallStrawHat ), 8, Utility.Random( 1,100 ), 0x1716, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StrawHat ), 7, Utility.Random( 1,100 ), 0x1717, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardsHat ), 11, Utility.Random( 1,100 ), 0x1718, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WitchHat ), 11, Utility.Random( 1,100 ), 0x2FC3, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherCap ), 10, Utility.Random( 1,100 ), 0x1DB9, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FeatheredHat ), 10, Utility.Random( 1,100 ), 0x171A, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TricorneHat ), 8, Utility.Random( 1,100 ), 0x171B, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PirateHat ), 8, Utility.Random( 1,100 ), 0x2FBC, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bandana ), 6, Utility.Random( 1,100 ), 0x1540, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SkullCap ), 7, Utility.Random( 1,100 ), 0x1544, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ThrowingWeapon ), 2, Utility.Random( 20, 120 ), 0x52B2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BreadLoaf ), 60, Utility.Random( 1,100 ), 0x103B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LambLeg ), 80, Utility.Random( 1,100 ), 0x160A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ChickenLeg ), 50, Utility.Random( 1,100 ), 0x1608, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CookedBird ), 170, Utility.Random( 1,100 ), 0x9B7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Ale, 70, Utility.Random( 1,100 ), 0x99F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Wine, 70, Utility.Random( 1,100 ), 0x9C7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Liquor, 70, Utility.Random( 1,100 ), 0x99B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Jug ), BeverageType.Cider, 130, Utility.Random( 1,100 ), 0x9C8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pear ), 30, Utility.Random( 1,100 ), 0x994, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Apple ), 30, Utility.Random( 1,100 ), 0x9D0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 1,100 ), 0xF84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 1,100 ), 0xF85, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bottle ), 5, Utility.Random( 100,1000 ), 0xF0E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Waterskin ), 5, Utility.Random( 1,100 ), 0xA21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RedBook ), 100, Utility.Random( 1,100 ), 0xFF1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlueBook ), 100, Utility.Random( 1,100 ), 0xFF2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TanBook ), 100, Utility.Random( 1,100 ), 0xFF0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBox ), 14, Utility.Random( 1,100 ), 0xE7D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Key ), 2, Utility.Random( 1,100 ), 0x100E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MerchantCrate ), 500, 1, 0xE3D, 0x83F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bedroll ), 5, Utility.Random( 1,100 ), 0xA59, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmallTent ), 200, Utility.Random( 1,5 ), 0x1914, Utility.RandomList( 0x96D, 0x96E, 0x96F, 0x970, 0x971, 0x972, 0x973, 0x974, 0x975, 0x976, 0x977, 0x978, 0x979, 0x97A, 0x97B, 0x97C, 0x97D, 0x97E ) ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CampersTent ), 500, Utility.Random( 1,5 ), 0x0A59, Utility.RandomList( 0x96D, 0x96E, 0x96F, 0x970, 0x971, 0x972, 0x973, 0x974, 0x975, 0x976, 0x977, 0x978, 0x979, 0x97A, 0x97B, 0x97C, 0x97D, 0x97E ) ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Kindling ), 2, Utility.Random( 1,100 ), 0xDE1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( tarotpoker ), 5, Utility.Random( 1,100 ), 0x12AB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016450", typeof( Chessboard ), 2, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016449", typeof( CheckerBoard ), 2, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backgammon ), 2, Utility.Random( 1,100 ), 0xE1C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Engines.Mahjong.MahjongGame ), 6, Utility.Random( 1,100 ), 0xFAA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Dices ), 2, Utility.Random( 1,100 ), 0xFA7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmallBagBall ), 3, Utility.Random( 1,100 ), 0x2256, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LargeBagBall ), 3, Utility.Random( 1,100 ), 0x2257, 0 ) ); }
				Add( new GenericBuyInfo( typeof( BagOfNecroReagents ), 500, 10, 0xE76, 0 ) ); 
				Add( new GenericBuyInfo( typeof( BagOfReagents ), 500, 10, 0xE76, 0 ) ); 
				Add( new GenericBuyInfo( typeof( SBArmorDeed ), 800, 100, 0x14F0, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Lute ), 21, Utility.Random( 10,20 ), 0x0EB3, 0 ) ); 
				
			
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( IvoryTusk ), Utility.Random( 50,250 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MerchantCrate ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SmallTent ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CampersTent ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Arrow ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bolt ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Backpack ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pouch ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bag ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Candle ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Torch ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lantern ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lockpick ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FloppyHat ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WideBrimHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cap ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TallStrawHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StrawHat ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WizardsHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WitchHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherCap ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FeatheredHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TricorneHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PirateHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bandana ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullCap ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ThrowingWeapon ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Waterskin ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bottle ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Jar ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RedBook ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlueBook ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TanBook ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBox ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Kindling ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( tarotpoker ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MahjongGame ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Chessboard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CheckerBoard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Backgammon ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Dices ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amber ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amethyst ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Citrine ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Diamond ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Emerald ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ruby ), 37 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Sapphire ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphire ), 62 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tourmaline ), 47 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldRing ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverRing ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Necklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBeadNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBeadNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Beads ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBracelet ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBracelet ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldEarrings ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverEarrings ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryRing ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryCirclet ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryNecklace ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryEarrings ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryBracelet ), Utility.Random( 50,300 ) ); } 

			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBRancher : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRancher()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new AnimalBuyInfo( 1, typeof( PackHorse ), 631, Utility.Random( 1,100 ), 291, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBRanger : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRanger()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new AnimalBuyInfo( 1, typeof( Cat ), 138, Utility.Random( 1,100 ), 201, 0 ) ); }
				if ( 1 > 0 ){Add( new AnimalBuyInfo( 1, typeof( Dog ), 181, Utility.Random( 1,100 ), 217, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new AnimalBuyInfo( 1, typeof( PackLlama ), 491, Utility.Random( 1,100 ), 292, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new AnimalBuyInfo( 1, typeof( PackHorse ), 606, Utility.Random( 1,100 ), 291, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new AnimalBuyInfo( 5, typeof( PackMule ), 10000, 1, 291, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bandage ), 2, Utility.Random( 10,60 ), 0xE21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Crossbow ), 55, Utility.Random( 1,100 ), 0xF50, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HeavyCrossbow ), 55, Utility.Random( 1,100 ), 0x13FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RepeatingCrossbow ), 46, Utility.Random( 1,100 ), 0x26C3, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CompositeBow ), 45, Utility.Random( 1,100 ), 0x26C2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bolt ), 2, Utility.Random( 30, 60 ), 0x1BFB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bow ), 40, Utility.Random( 1,100 ), 0x13B2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Arrow ), 2, Utility.Random( 30, 60 ), 0xF3F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Feather ), 2, Utility.Random( 30, 60 ), 0x4CCD, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Shaft ), 3, Utility.Random( 30, 60 ), 0x1BD4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ArcherQuiver ), 32, Utility.Random( 1,5 ), 0x2B02, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RangerArms ), 87, Utility.Random( 1,100 ), 0x13DC, 0x59C ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RangerChest ), 128, Utility.Random( 1,100 ), 0x13DB, 0x59C ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RangerGloves ), 79, Utility.Random( 1,100 ), 0x13D5, 0x59C ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RangerGorget ), 73, Utility.Random( 1,100 ), 0x13D6, 0x59C ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RangerLegs ), 103, Utility.Random( 1,100 ), 0x13DA, 0x59C ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmallTent ), 200, Utility.Random( 1,5 ), 0x1914, Utility.RandomList( 0x96D, 0x96E, 0x96F, 0x970, 0x971, 0x972, 0x973, 0x974, 0x975, 0x976, 0x977, 0x978, 0x979, 0x97A, 0x97B, 0x97C, 0x97D, 0x97E ) ) ); }
				if ( MyServerSettings.SellCommonChance() ){Add( new GenericBuyInfo( typeof( CampersTent ), 500, Utility.Random( 1,5 ), 0x0A59, Utility.RandomList( 0x96D, 0x96E, 0x96F, 0x970, 0x971, 0x972, 0x973, 0x974, 0x975, 0x976, 0x977, 0x978, 0x979, 0x97A, 0x97B, 0x97C, 0x97D, 0x97E ) ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( MyTentEastAddonDeed ), 1000, 1, 0xA58, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( MyTentSouthAddonDeed ), 1000, 1, 0xA59, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TrapKit ), 420, Utility.Random( 1,5 ), 0x1EBB, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( MyTentEastAddonDeed ), 200 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MyTentSouthAddonDeed ), 200 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SmallTent ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CampersTent ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Crossbow ), 27 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HeavyCrossbow ), 28 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RepeatingCrossbow ), 23 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CompositeBow ), 22 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bolt ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Arrow ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bow ), 20 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Feather ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Shaft ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Arrow ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ArcherQuiver ), 16 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RangerArms ), 43 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RangerChest ), 64 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RangerGloves ), 40 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RangerLegs ), 51 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RangerGorget ), 36 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TrapKit ), 210 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBRealEstateBroker : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRealEstateBroker()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( InteriorDecorator ), 1000, Utility.Random( 1,100 ), 0x1EBA, 0 ) );
				Add( new GenericBuyInfo( typeof( HousePlacementTool ), 500, Utility.Random( 1,100 ), 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "house teleporter", typeof( PlayersHouseTeleporter ), 25000, Utility.Random( 1,10 ), 0x181D, 0 ) );
				Add( new GenericBuyInfo( "house high teleporter", typeof( PlayersZTeleporter ), 15000, Utility.Random( 1,10 ), 0x181D, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlankScroll ), 5, Utility.Random( 1,100 ), 0x0E34, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ScribesPen ), 8,  Utility.Random( 1,100 ), 0x2051, 0 ) ); }
				Add( new GenericBuyInfo( typeof( house_sign_sign_post_a ), 500, Utility.Random( 1,100 ), 2967, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_post_b ), 500, Utility.Random( 1,100 ), 2970, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_merc ), 1000, Utility.Random( 1,100 ), 3082, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_armor ), 1000, Utility.Random( 1,100 ), 3008, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bake ), 1000, Utility.Random( 1,100 ), 2980, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bank ), 1000, Utility.Random( 1,100 ), 3084, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bard ), 1000, Utility.Random( 1,100 ), 3004, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_smith ), 1000, Utility.Random( 1,100 ), 3016, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_bow ), 1000, Utility.Random( 1,100 ), 3022, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_ship ), 1000, Utility.Random( 1,100 ), 2998, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_fletch ), 1000, Utility.Random( 1,100 ), 3006, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_heal ), 1000, Utility.Random( 1,100 ), 2988, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_inn ), 1000, Utility.Random( 1,100 ), 2996, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_gem ), 1000, Utility.Random( 1,100 ), 3010, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_book ), 1000, Utility.Random( 1,100 ), 2966, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_mage ), 1000, Utility.Random( 1,100 ), 2990, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_necro ), 1000, Utility.Random( 1,100 ), 2811, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_supply ), 1000, Utility.Random( 1,100 ), 3020, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_herb ), 1000, Utility.Random( 1,100 ), 3014, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_pen ), 1000, Utility.Random( 1,100 ), 3000, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_sew ), 1000, Utility.Random( 1,100 ), 2982, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_tavern ), 1000, Utility.Random( 1,100 ), 3012, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_tinker ), 1000, Utility.Random( 1,100 ), 2984, 0 ) );
				Add( new GenericBuyInfo( typeof( house_sign_sign_wood ), 1000, Utility.Random( 1,100 ), 2992, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( ScribesPen ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlankScroll ), 2 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBScribe: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBScribe()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ScribesPen ), 16, Utility.Random( 1,100 ), 0x2051, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlankScroll ), 5, Utility.Random( 1,100 ), 0x0E34, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Monocle ), 24, Utility.Random( 1,25 ), 0x2C84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BrownBook ), 100, Utility.Random( 1,100 ), 0xFEF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TanBook ), 100, Utility.Random( 1,100 ), 0xFF0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlueBook ), 100, Utility.Random( 1,100 ), 0xFF2, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( "1041267", typeof( Runebook ), 3500, Utility.Random( 1,3 ), 0x0F3D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Mailbox ), 158, Utility.Random( 1,5 ), 0x4142, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( ScribesPen ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Monocle ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BrownBook ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TanBook ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlueBook ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlankScroll ), 3 ); } 
				Add( typeof( JokeBook ), Utility.Random( 750,1500 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( DynamicBook ), Utility.Random( 10,150 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DataPad ), Utility.Random( 5, 150 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( NecromancerSpellbook ), 55 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BookOfBushido ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BookOfNinjitsu ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MysticSpellbook ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DeathKnightSpellbook ), Utility.Random( 100,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Runebook ), Utility.Random( 100,350 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BookOfChivalry ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BookOfChivalry ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HolyManSpellbook ), Utility.Random( 50,200 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSage: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSage()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LoreGuidetoAdventure ), 5, Utility.Random( 5,100 ), 0x1C11, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( WeaponAbilityBook ), 50, Utility.Random( 1,100 ), 0x2254, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnLeatherBook ), 50, Utility.Random( 1,100 ), 0x4C60, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnMiscBook ), 50, Utility.Random( 1,100 ), 0x4C5D, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnMetalBook ), 50, Utility.Random( 1,100 ), 0x4C5B, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnWoodBook ), 50, Utility.Random( 1,100 ), 0x4C5E, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnReagentsBook ), 50, Utility.Random( 1,100 ), 0x4C5E, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnGraniteBook ), 50, Utility.Random( 1,100 ), 0x4C5C, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnScalesBook ), 50, Utility.Random( 1,100 ), 0x4C60, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnTailorBook ), 50, Utility.Random( 1,100 ), 0x4C5E, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnTraps ), 50, Utility.Random( 1,100 ), 0xFF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BeATroubadour ), 500, Utility.Random( 1,2 ), 0x4C5C, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BeASorcerer ), 500, Utility.Random( 1,2 ), 0x4C5C, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( SwordsAndShackles ), 500, Utility.Random( 1,100 ), 0x529D, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( CBookDruidicHerbalism ), 500, Utility.Random( 1,100 ), 0x2D50, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( CBookNecroticAlchemy ), 500, Utility.Random( 1,100 ), 0x2253, 0x4AA ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( AlchemicalElixirs ), 500, Utility.Random( 1,100 ), 0x2219, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( AlchemicalMixtures ), 500, Utility.Random( 1,100 ), 0x2223, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( BookOfPoisons ), 500, Utility.Random( 1,100 ), 0x2253, 0x4F8 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( WorkShoppes ), 500, Utility.Random( 1,100 ), 0x2259, 0xB50 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnTitles ), 50, Utility.Random( 1,100 ), 0xFF2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ScribesPen ), 8, Utility.Random( 1,100 ), 0x2051, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlankScroll ), 7, Utility.Random( 50,500 ), 0x0E34, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Monocle ), 24, Utility.Random( 1,25 ), 0x2C84, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( "1041267", typeof( Runebook ), 3500, Utility.Random( 1,3 ), 0x0F3D, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( ScribesPen ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlankScroll ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Monocle ), 12 ); } 
				Add( typeof( TomeOfWands ), Utility.Random( 100,400 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( NecromancerSpellbook ), 55 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BookOfBushido ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BookOfNinjitsu ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MysticSpellbook ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DeathKnightSpellbook ), Utility.Random( 100,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Runebook ), Utility.Random( 100,350 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BookOfChivalry ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BookOfChivalry ), 70 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HolyManSpellbook ), Utility.Random( 50,200 ) ); } 
				if ( 1 > 0 ){Add( typeof( SwordsAndShackles ), 25 ); }
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSECook: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSECook()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Wasabi ), 20, Utility.Random( 1,100 ), 0x24E8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SushiRolls ), 30, Utility.Random( 1,100 ), 0x283E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SushiPlatter ), 30, Utility.Random( 1,100 ), 0x2840, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GreenTea ), 30, Utility.Random( 1,100 ), 0x284C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MisoSoup ), 30, Utility.Random( 1,100 ), 0x284D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WhiteMisoSoup ), 30, Utility.Random( 1,100 ), 0x284E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RedMisoSoup ), 30, Utility.Random( 1,100 ), 0x284F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AwaseMisoSoup ), 30, Utility.Random( 1,100 ), 0x2850, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BentoBox ), 60, Utility.Random( 1,100 ), 0x2836, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( BentoBox ), 6, Utility.Random( 1,100 ), 0x2837, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Wasabi ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BentoBox ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GreenTea ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SushiRolls ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SushiPlatter ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MisoSoup ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RedMisoSoup ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WhiteMisoSoup ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AwaseMisoSoup ), 5 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSEHats: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSEHats()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Kasa ), 31, Utility.Random( 1,100 ), 0x2798, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherJingasa ), 11, Utility.Random( 1,100 ), 0x2776, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ClothNinjaHood ), 33, Utility.Random( 1,100 ), 0x278F, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Kasa ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherJingasa ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ClothNinjaHood ), 16 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBShipwright : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBShipwright()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041205", typeof( SmallBoatDeed ), 10000, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041206", typeof( SmallDragonBoatDeed ), 13000, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041207", typeof( MediumBoatDeed ), 18000, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041208", typeof( MediumDragonBoatDeed ), 20000, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041209", typeof( LargeBoatDeed ), 30000, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041210", typeof( LargeDragonBoatDeed ), 40000, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( DockingLantern ), 580, Utility.Random( 1,100 ), 0x40FF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Sextant ), 13, Utility.Random( 1,100 ), 0x1057, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GrapplingHook ), 58, Utility.Random( 1,100 ), 0x4F40, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoatStain ), 26, Utility.Random( 1,100 ), 0x14E0, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( SeaShell ), 58 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DockingLantern ), 29 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Sextant ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GrapplingHook ), 29 ); } 
				Add( typeof( PirateChest ), Utility.RandomMinMax( 100, 500 ) );
				Add( typeof( SunkenChest ), Utility.RandomMinMax( 100, 500 ) );
				Add( typeof( FishingNet ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( SpecialFishingNet ), Utility.RandomMinMax( 30, 40 ) );
				Add( typeof( FabledFishingNet ), Utility.RandomMinMax( 50, 60 ) );
				Add( typeof( NeptunesFishingNet ), Utility.RandomMinMax( 70, 80 ) );
				Add( typeof( PrizedFish ), Utility.RandomMinMax( 30, 60 ) );
				Add( typeof( WondrousFish ), Utility.RandomMinMax( 30, 60 ) );
				Add( typeof( TrulyRareFish ), Utility.RandomMinMax( 30, 60 ) );
				Add( typeof( PeculiarFish ), Utility.RandomMinMax( 30, 60 ) );
				Add( typeof( SpecialSeaweed ), Utility.RandomMinMax( 20, 80 ) );
				Add( typeof( SunkenBag ), Utility.RandomMinMax( 50, 250 ) );
				Add( typeof( ShipwreckedItem ), Utility.RandomMinMax( 10, 50 ) );
				Add( typeof( HighSeasRelic ), Utility.RandomMinMax( 10, 50 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoatStain ), 13 ); } 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MegalodonTooth ), Utility.RandomMinMax( 500, 2000 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBDevon : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBDevon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( MagicSextant ), Utility.Random( 500,1000 ), Utility.Random( 5,100 ), 0x26A0, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSmithTools: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBSmithTools() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( IronIngot ), 5, Utility.Random( 1,100 ), 0x1BF2, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Tongs ), 13, Utility.Random( 1,100 ), 0xFBB, 0 ) ); } 

			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tongs ), 7 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( IronIngot ), 4 ); }  
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBStoneCrafter : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBStoneCrafter()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Nails ), 3, Utility.Random( 1,100 ), 0x102E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Axle ), 2, Utility.Random( 1,100 ), 0x105B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Board ), 3, Utility.Random( 1,100 ), 0x1BD7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DrawKnife ), 10, Utility.Random( 1,100 ), 0x10E4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Froe ), 10, Utility.Random( 1,100 ), 0x10E5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Scorp ), 10, Utility.Random( 1,100 ), 0x10E7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Inshave ), 10, Utility.Random( 1,100 ), 0x10E6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DovetailSaw ), 12, Utility.Random( 1,100 ), 0x1028, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Saw ), 100, Utility.Random( 1,100 ), 0x1034, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Hammer ), 17, Utility.Random( 1,100 ), 0x102A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MouldingPlane ), 11, Utility.Random( 1,100 ), 0x102C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmoothingPlane ), 10, Utility.Random( 1,100 ), 0x1032, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( JointingPlane ), 11, Utility.Random( 1,100 ), 0x1030, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( WoodworkingTools ), 10, Utility.Random( 10,30 ), 0x4F52, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( "Making Valuables With Stonecrafting", typeof( MasonryBook ), 50625, Utility.Random( 1,100 ), 0xFBE, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( "Mining For Quality Stone", typeof( StoneMiningBook ), 25625, Utility.Random( 1,100 ), 0xFBE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1044515", typeof( MalletAndChisel ), 3, Utility.Random( 1,100 ), 0x12B3, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "Jade Statue Maker", typeof( JadeStatueMaker ), 50000, 1, 0x32F2, 0xB93 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "Marble Statue Maker", typeof( MarbleStatueMaker ), 50000, 1, 0x32F2, 0xB8F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "Bronze Statue Maker", typeof( BronzeStatueMaker ), 50000, 1, 0x32F2, 0xB97 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( MasonryBook ), 5000 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StoneMiningBook ), 5000 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MalletAndChisel ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBox ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SmallCrate ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MediumCrate ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LargeCrate ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenChest ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LargeTable ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Nightstand ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( YewWoodTable ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Throne ), 24 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenThrone ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Stool ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FootStool ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FancyWoodenChairCushion ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenChairCushion ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenChair ), 8 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BambooChair ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBench ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Saw ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Scorp ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SmoothingPlane ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DrawKnife ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Froe ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Hammer ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Inshave ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodworkingTools ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JointingPlane ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MouldingPlane ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DovetailSaw ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Board ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Axle ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenShield ), 31 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlackStaff ), 24 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GnarledStaff ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuarterStaff ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShepherdsCrook ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Club ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Log ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RockUrn ), 30 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RockVase ), 30 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBTailor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBTailor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SewingKit ), 3, Utility.Random( 1,15 ), 0x4C81, 0 ) ); } 
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Scissors ), 11, Utility.Random( 1,15 ), 0xF9F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DyeTub ), 8, Utility.Random( 1,15 ), 0xFAB, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Dyes ), 8, Utility.Random( 1,15 ), 0xFA9, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Shirt ), 12, Utility.Random( 1,15 ), 0x1517, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ShortPants ), 7, Utility.Random( 1,15 ), 0x152E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FancyShirt ), 21, Utility.Random( 1,15 ), 0x1EFD, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RoyalCoat ), 21, Utility.Random( 1,15 ), 0x307, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RoyalShirt ), 21, Utility.Random( 1,15 ), 0x30B, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RusticShirt ), 21, Utility.Random( 1,15 ), 0x30D, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SquireShirt ), 21, Utility.Random( 1,15 ), 0x311, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FormalCoat ), 21, Utility.Random( 1,15 ), 0x403, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardShirt ), 21, Utility.Random( 1,15 ), 0x407, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BeggarVest ), 12, Utility.Random( 1,15 ), 0x308, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RoyalVest ), 12, Utility.Random( 1,15 ), 0x30C, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RusticVest ), 12, Utility.Random( 1,15 ), 0x30E, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SailorPants ), 7, Utility.Random( 1,15 ), 0x309, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PiratePants ), 10, Utility.Random( 1,15 ), 0x404, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RoyalSkirt ), 11, Utility.Random( 1,15 ), 0x30A, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Skirt ), 12, Utility.Random( 1,15 ), 0x1516, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RoyalLongSkirt ), 12, Utility.Random( 1,15 ), 0x408, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LongPants ), 10, Utility.Random( 1,15 ), 0x1539, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FancyDress ), 26, Utility.Random( 1,15 ), 0x1EFF, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PlainDress ), 13, Utility.Random( 1,15 ), 0x1F01, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Kilt ), 11, Utility.Random( 1,15 ), 0x1537, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HalfApron ), 10, Utility.Random( 1,15 ), 0x153b, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LoinCloth ), 10, Utility.Random( 1,15 ), 0x2B68, 637 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RoyalLoinCloth ), 10, Utility.Random( 1,15 ), 0x55DB, 637 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Robe ), 18, Utility.Random( 1,15 ), 0x1F03, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( JokerRobe ), 40, Utility.Random( 1,5 ), 0x2B6B, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AssassinRobe ), 40, Utility.Random( 1,5 ), 0x2B69, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FancyRobe ), 40, Utility.Random( 1,5 ), 0x2B6A, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GildedRobe ), 40, Utility.Random( 1,5 ), 0x2B6C, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( OrnateRobe ), 40, Utility.Random( 1,5 ), 0x2B6E, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MagistrateRobe ), 40, Utility.Random( 1,5 ), 0x2B70, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RoyalRobe ), 40, Utility.Random( 1,5 ), 0x2B73, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SorcererRobe ), 40, Utility.Random( 1,5 ), 0x3175, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AssassinShroud ), 40, Utility.Random( 1,5 ), 0x2FB9, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NecromancerRobe ), 40, Utility.Random( 1,5 ), 0x2FBA, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SpiderRobe ), 40, Utility.Random( 1,5 ), 0x2FC6, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( VagabondRobe ), 40, Utility.Random( 1,5 ), 0x567D, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PirateCoat ), 40, Utility.Random( 1,5 ), 0x567E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ExquisiteRobe ), 40, Utility.Random( 1,5 ), 0x283, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ProphetRobe ), 40, Utility.Random( 1,5 ), 0x284, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ElegantRobe ), 40, Utility.Random( 1,5 ), 0x285, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FormalRobe ), 40, Utility.Random( 1,5 ), 0x286, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ArchmageRobe ), 40, Utility.Random( 1,5 ), 0x287, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PriestRobe ), 40, Utility.Random( 1,5 ), 0x288, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CultistRobe ), 40, Utility.Random( 1,5 ), 0x289, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GildedDarkRobe ), 40, Utility.Random( 1,5 ), 0x28A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GildedLightRobe ), 40, Utility.Random( 1,5 ), 0x301, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SageRobe ), 40, Utility.Random( 1,5 ), 0x302, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cloak ), 8, Utility.Random( 1,15 ), 0x1515, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Doublet ), 13, Utility.Random( 1,15 ), 0x1F7B, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tunic ), 18, Utility.Random( 1,15 ), 0x1FA1, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( JesterSuit ), 26, Utility.Random( 1,15 ), 0x1F9F, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( JesterHat ), 12, Utility.Random( 1,15 ), 0x171C, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FloppyHat ), 7, Utility.Random( 1,15 ), 0x1713, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WideBrimHat ), 8, Utility.Random( 1,15 ), 0x1714, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cap ), 10, Utility.Random( 1,15 ), 0x1715, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TallStrawHat ), 8, Utility.Random( 1,15 ), 0x1716, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StrawHat ), 7, Utility.Random( 1,15 ), 0x1717, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardsHat ), 11, Utility.Random( 1,15 ), 0x1718, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WitchHat ), 11, Utility.Random( 1,15 ), 0x2FC3, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherCap ), 10, Utility.Random( 1,15 ), 0x1DB9, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FeatheredHat ), 10, Utility.Random( 1,15 ), 0x171A, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TricorneHat ), 8, Utility.Random( 1,15 ), 0x171B, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PirateHat ), 8, Utility.Random( 1,15 ), 0x2FBC, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bandana ), 6, Utility.Random( 1,15 ), 0x1540, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SkullCap ), 7, Utility.Random( 1,15 ), 0x1544, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ClothHood ), 12, Utility.Random( 1,15 ), 0x2B71, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ClothCowl ), 12, Utility.Random( 1,15 ), 0x3176, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardHood ), 12, Utility.Random( 1,15 ), 0x310, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FancyHood ), 12, Utility.Random( 1,15 ), 0x4D09, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoltOfCloth ), 100, Utility.Random( 1,15 ), 0xf95, Utility.RandomDyedHue() ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cloth ), 2, Utility.Random( 1,15 ), 0x1766, Utility.RandomDyedHue() ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( UncutCloth ), 2, Utility.Random( 1,15 ), 0x1767, Utility.RandomDyedHue() ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Cotton ), 102, Utility.Random( 1,15 ), 0xDF9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Wool ), 62, Utility.Random( 1,15 ), 0xDF8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Flax ), 102, Utility.Random( 1,15 ), 0x1A9C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SpoolOfThread ), 18, Utility.Random( 1,15 ), 0x543A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FemaleMannequinDeed ), Utility.Random( 5000,15000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MaleMannequinDeed ), Utility.Random( 5000,15000 ), 1, 0x14F0, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( JokerRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AssassinRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FancyRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GildedRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OrnateRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagistrateRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RoyalRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SorcererRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AssassinShroud ), 29 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( NecromancerRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpiderRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( VagabondRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PirateCoat ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ExquisiteRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ProphetRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ElegantRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FormalRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ArchmageRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PriestRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CultistRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GildedDarkRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GildedLightRobe ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SageRobe ), 19 ); } 

				if ( MyServerSettings.BuyChance() ){Add( typeof( Scissors ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SewingKit ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Dyes ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DyeTub ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoltOfCloth ), 35 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FancyShirt ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Shirt ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ShortPants ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LongPants ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cloak ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FancyDress ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Robe ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlainDress ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Skirt ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RoyalCoat ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RoyalShirt ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RusticShirt ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SquireShirt ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FormalCoat ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WizardShirt ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BeggarVest ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RoyalVest ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RusticVest ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SailorPants ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PiratePants ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RoyalSkirt ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Skirt ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RoyalLongSkirt ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Kilt ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Doublet ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tunic ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JesterSuit ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FullApron ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HalfApron ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LoinCloth ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JesterHat ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FloppyHat ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WideBrimHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cap ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullCap ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ClothCowl ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WizardHood ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ClothHood ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FancyHood ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bandana ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TallStrawHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StrawHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WizardsHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WitchHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bonnet ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FeatheredHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TricorneHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PirateHat ), 4 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpoolOfThread ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Flax ), 51 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Cotton ), 51 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Wool ), 31 ); } 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagicRobe ), 30 ); }  
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagicHat ), 20 ); }  
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagicCloak ), 30 ); }  
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagicBelt ), 20 ); }  
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagicSash ), 20 ); }  
				Add( typeof( MagicScissors ), Utility.Random( 300,400 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBJester: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBJester()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BagOfTricks ), 200, Utility.Random( 1,100 ), 0x1E3F, 0 ) );
				Add( new GenericBuyInfo( typeof( JesterHat ), 12, Utility.Random( 1,100 ), 0x171C, 0 ) );
				Add( new GenericBuyInfo( typeof( JokerHat ), 12, Utility.Random( 1,100 ), 0x171C, 0 ) );
				Add( new GenericBuyInfo( typeof( JesterSuit ), 26, Utility.Random( 1,100 ), 0x1F9F, 0 ) );
				Add( new GenericBuyInfo( typeof( JesterGarb ), 26, Utility.Random( 1,100 ), 0x1F9F, 0 ) );
				Add( new GenericBuyInfo( typeof( FoolsCoat ), 26, Utility.Random( 1,100 ), 0x1F9F, 0 ) );
				Add( new GenericBuyInfo( typeof( JokerRobe ), 26, Utility.Random( 1,100 ), 0x1F9F, 0 ) );
				Add( new GenericBuyInfo( typeof( JesterShoes ), 26, Utility.Random( 1,100 ), 0x170f, 0 ) );
				Add( new GenericBuyInfo( typeof( ThrowingGloves ), 26, Utility.Random( 1,100 ), 0x13C6, 0 ) );
				Add( new GenericBuyInfo( typeof( ThrowingWeapon ), 2, Utility.Random( 20, 200 ), 0x52B2, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MyCircusTentEastAddonDeed ), 1000, 1, 0xA58, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MyCircusTentSouthAddonDeed ), 1000, 1, 0xA59, Utility.RandomDyedHue() ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BagOfTricks ), 100 );
				Add( typeof( JesterHat ), 6 );
				Add( typeof( JokerHat ), 6 );
				Add( typeof( JesterSuit ), 13 );
				Add( typeof( JesterGarb ), 13 );
				Add( typeof( FoolsCoat ), 13 );
				Add( typeof( JokerRobe ), 13 );
				Add( typeof( JesterShoes ), 13 );
				Add( typeof( ThrowingGloves ), 13 );
				Add( typeof( ThrowingWeapon ), 1 );
				Add( typeof( MyCircusTentEastAddonDeed ), 200 );
				Add( typeof( MyCircusTentSouthAddonDeed ), 200 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBTanner : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBTanner()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FemaleStuddedChest ), 62, Utility.Random( 1,100 ), 0x1C02, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FemalePlateChest ), 207, Utility.Random( 1,100 ), 0x1C04, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FemaleLeatherChest ), 36, Utility.Random( 1,100 ), 0x1C06, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherShorts ), 28, Utility.Random( 1,100 ), 0x1C00, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherSkirt ), 25, Utility.Random( 1,100 ), 0x1C08, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LeatherBustierArms ), 30, Utility.Random( 1,100 ), 0x1C0B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StuddedBustierArms ), 50, Utility.Random( 1,100 ), 0x1C0C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bag ), 6, Utility.Random( 1,100 ), 0xE76, 0xABE ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pouch ), 6, Utility.Random( 1,100 ), 0xE79, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Backpack ), 100, Utility.Random( 1,100 ), 0x53D5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SkinningKnife ), 100, Utility.Random( 1,100 ), 0xEC4, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bag ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pouch ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Backpack ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkinningKnife ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FemaleStuddedChest ), 31 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StuddedBustierArms ), 23 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FemalePlateChest), 103 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FemaleLeatherChest ), 18 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherBustierArms ), 12 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherShorts ), 14 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LeatherSkirt ), 12 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBTavernKeeper : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBTavernKeeper()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Ale, 70, Utility.Random( 1,100 ), 0x99F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Wine, 70, Utility.Random( 1,100 ), 0x9C7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Liquor, 70, Utility.Random( 1,100 ), 0x99B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Jug ), BeverageType.Cider, 130, Utility.Random( 1,100 ), 0x9C8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Milk, 180, Utility.Random( 1,100 ), 0x9F0, 0 ) ); }
				if ( 1 > 0 ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Ale, 110, Utility.Random( 1,100 ), 0x1F95, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Cider, 110, Utility.Random( 1,100 ), 0x1F97, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Liquor, 110, Utility.Random( 1,100 ), 0x1F99, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Wine, 110, Utility.Random( 1,100 ), 0x1F9B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Water, 110, Utility.Random( 1,100 ), 0x1F9D, 0 ) ); }
				Add( new GenericBuyInfo( typeof( Pitcher ), 3, Utility.Random( 50,100 ), 0x1F97, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BreadLoaf ), 60, Utility.Random( 1,100 ), 0x103B, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( CheeseWheel ), 410, Utility.Random( 1,100 ), 0x97E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CookedBird ), 170, Utility.Random( 1,100 ), 0x9B7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LambLeg ), 80, Utility.Random( 1,100 ), 0x160A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ChickenLeg ), 80, Utility.Random( 1,100 ), 0x1608, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ribs ), 90, Utility.Random( 1,100 ), 0x9F2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfCarrots ), 30, Utility.Random( 1,100 ), 0x15F9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfCorn ), 30, Utility.Random( 1,100 ), 0x15FA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfLettuce ), 30, Utility.Random( 1,100 ), 0x15FB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfPeas ), 30, Utility.Random( 1,100 ), 0x15FC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( EmptyPewterBowl ), 20, Utility.Random( 1,100 ), 0x15FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfCorn ), 30, Utility.Random( 1,100 ), 0x15FE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfLettuce ), 30, Utility.Random( 1,100 ), 0x15FF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfPeas ), 30, Utility.Random( 1,100 ), 0x1600, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfFoodPotatos ), 30, Utility.Random( 1,100 ), 0x1601, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfStew ), 30, Utility.Random( 1,100 ), 0x1604, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfTomatoSoup ), 30, Utility.Random( 1,100 ), 0x1606, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ApplePie ), 70, Utility.Random( 1,100 ), 0x1041, 0 ) ); } //OSI just has Pie, not Apple/Fruit/Meat
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( tarotpoker ), 50, Utility.Random( 1,100 ), 0x12AB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016450", typeof( Chessboard ), 20, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1016449", typeof( CheckerBoard ), 20, Utility.Random( 1,100 ), 0xFA6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backgammon ), 20, Utility.Random( 1,100 ), 0xE1C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Dices ), 20, Utility.Random( 1,100 ), 0xFA7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Engines.Mahjong.MahjongGame ), 600, Utility.Random( 1,100 ), 0xFAA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Waterskin ), 5, Utility.Random( 1,100 ), 0xA21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HenchmanFighterItem ), 5000, Utility.Random( 1,100 ), 0x1419, 0xB96 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HenchmanArcherItem ), 6000, Utility.Random( 1,100 ), 0xF50, 0xB96 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HenchmanWizardItem ), 7000, Utility.Random( 1,100 ), 0xE30, 0xB96 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041243", typeof( ContractOfEmployment ), 1252, Utility.Random( 1,100 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "a barkeep contract", typeof( BarkeepContract ), 1252, Utility.Random( 1,100 ), 0x14F0, 0 ) ); }

				if ( Multis.BaseHouse.NewVendorSystem )
					if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1062332", typeof( VendorRentalContract ), 1252, Utility.Random( 1,100 ), 0x14F0, 0x672 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfCarrots ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfCorn ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfLettuce ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfPeas ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmptyPewterBowl ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfCorn ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfLettuce ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfPeas ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PewterBowlOfFoodPotatos ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfStew ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBowlOfTomatoSoup ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BeverageBottle ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Waterskin ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Jug ), 30 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pitcher ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GlassMug ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BreadLoaf ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CheeseWheel ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ribs ), 15 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Peach ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pear ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Grapes ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Apple ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Banana ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Candle ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( tarotpoker ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MahjongGame ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Chessboard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CheckerBoard ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Backgammon ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Dices ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ContractOfEmployment ), 626 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RomulanAle ), Utility.Random( 20,100 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBThief : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBThief()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Backpack ), 100, Utility.Random( 1,100 ), 0x53D5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pouch ), 6, Utility.Random( 1,100 ), 0xE79, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Torch ), 8, Utility.Random( 1,100 ), 0xF6B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lantern ), 2, Utility.Random( 1,100 ), 0xA25, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnStealingBook ), 5, Utility.Random( 1,100 ), 0x4C5C, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LearnTraps ), 5, Utility.Random( 1,100 ), 0xFF2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lockpick ), 15, Utility.Random( 1,100 ), 0x14FC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SkeletonsKey ), Utility.Random( 100,500 ), 25, 0x410A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBox ), 14, Utility.Random( 1,100 ), 0x9AA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Key ), 2, Utility.Random( 1,100 ), 0x100E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1041060", typeof( HairDye ), 100, Utility.Random( 1,100 ), 0xEFF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "hair dye bottle", typeof( HairDyeBottle ), 1000, Utility.Random( 1,100 ), 0xE0F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DisguiseKit ), 700, Utility.Random( 1,5 ), 0xE05, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PackGrenade ), 100, Utility.Random( 1,100 ), 0x25FD, 0 ) ); }
				Add( new GenericBuyInfo( typeof( CorruptedMoonStone ), 5000, 5, 0xF8B, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Backpack ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pouch ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Torch ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lantern ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lockpick ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenBox ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HairDye ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HairDyeBottle ), 300 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkeletonsKey ), 10 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBTinker: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBTinker() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Clock ), 22, Utility.Random( 1,100 ), 0x104B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Nails ), 3, Utility.Random( 1,100 ), 0x102E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ClockParts ), 3, Utility.Random( 1,100 ), 0x104F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AxleGears ), 3, Utility.Random( 1,100 ), 0x1051, 0 ) ); } 
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Gears ), 2, Utility.Random( 1,100 ), 0x1053, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Hinge ), 2, Utility.Random( 1,100 ), 0x1055, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Sextant ), 13, Utility.Random( 1,100 ), 0x1057, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SextantParts ), 5, Utility.Random( 1,100 ), 0x1059, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Axle ), 2, Utility.Random( 1,100 ), 0x105B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Springs ), 3, Utility.Random( 1,100 ), 0x105D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1024111", typeof( Key ), 8, Utility.Random( 1,100 ), 0x100F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1024112", typeof( Key ), 8, Utility.Random( 1,100 ), 0x1010, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "1024115", typeof( Key ), 8, Utility.Random( 1,100 ), 0x1013, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( KeyRing ), 8, Utility.Random( 1,100 ), 0x1010, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Lockpick ), 12, Utility.Random( 1,100 ), 0x14FC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SkeletonsKey ), Utility.Random( 200,750 ), 10, 0x410A, 0 ) ); }
				Add( new GenericBuyInfo( typeof( TinkersTools ), 750, Utility.Random( 1,10 ), 0x1EBC, 0 ) ); 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Board ), 3, Utility.Random( 1,100 ), 0x1BD7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( IronIngot ), 5, Utility.Random( 1,100 ), 0x1BF2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SewingKit ), 3, Utility.Random( 1,100 ), 0x4C81, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DrawKnife ), 10, Utility.Random( 1,100 ), 0x10E4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Froe ), 10, Utility.Random( 1,100 ), 0x10E5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Scorp ), 10, Utility.Random( 1,100 ), 0x10E7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Inshave ), 10, Utility.Random( 1,100 ), 0x10E6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ButcherKnife ), 13, Utility.Random( 1,100 ), 0x13F6, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Scissors ), 11, Utility.Random( 1,100 ), 0xF9F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tongs ), 13, Utility.Random( 1,100 ), 0xFBB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DovetailSaw ), 12, Utility.Random( 1,100 ), 0x1028, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Saw ), 100, Utility.Random( 1,100 ), 0x1034, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Hammer ), 17, Utility.Random( 1,100 ), 0x102A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmithHammer ), 23, Utility.Random( 1,100 ), 0x0FB4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Shovel ), 12, Utility.Random( 1,100 ), 0xF39, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( OreShovel ), 10, Utility.Random( 1,100 ), 0xF39, 0x96D ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MouldingPlane ), 11, Utility.Random( 1,100 ), 0x102C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( JointingPlane ), 10, Utility.Random( 1,100 ), 0x1030, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SmoothingPlane ), 11, Utility.Random( 1,100 ), 0x1032, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( WoodworkingTools ), 10, Utility.Random( 10,30 ), 0x4F52, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Pickaxe ), 25, Utility.Random( 1,100 ), 0xE86, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ThrowingWeapon ), 2, Utility.Random( 20, 120 ), 0x52B2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WallTorch ), 50, Utility.Random( 5,20 ), 0xA07, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ColoredWallTorch ), 100, Utility.Random( 5,20 ), 0x3D89, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( light_dragon_brazier ), 750, Utility.Random( 1,100 ), 0x194E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TrapKit ), 420, Utility.Random( 1,5 ), 0x1EBB, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PianoAddonDeed ), Utility.Random( 100000,200000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PoorMiningHarvester ), 15000, Utility.Random( 1,2 ), 0x5484, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StandardMiningHarvester ), 75000, Utility.Random( 1,2 ), 0x5484, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GoodMiningHarvester ), 225000, Utility.Random( 1,2 ), 0x5484, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PoorLumberHarvester ), 15000, Utility.Random( 1,2 ), 0x5486, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( StandardLumberHarvester ), 75000, Utility.Random( 1,2 ), 0x5486, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PoorHideHarvester ), 15000, Utility.Random( 1,2 ), 0x5487, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( StandardHideHarvester ), 75000, Utility.Random( 1,2 ), 0x5487, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HarvesterRepairKit ), 7500, Utility.Random( 1,2 ), 0x4C2C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CiderBarrel ), 20000, Utility.Random( 1,4 ), 0x3DB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AleBarrel ), 20000, Utility.Random( 1,4 ), 0x3DB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LiquorBarrel ), 20000, Utility.Random( 1,4 ), 0x3DB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CheesePress ), 20000, Utility.Random( 1,4 ), 0x3DB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DeviceKit), 1000, Utility.Random( 1,2 ), 0x4F86, 0 ) ); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(GogglesofScience), 1000, Utility.Random(1, 2), 0x3172, 0)); }

            } 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( LootChest ), 600 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Shovel ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OreShovel ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SewingKit ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Scissors ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tongs ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Key ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DovetailSaw ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MouldingPlane ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Nails ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JointingPlane ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SmoothingPlane ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Saw ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Clock ), 11 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ClockParts ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AxleGears ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Gears ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Hinge ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Sextant ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SextantParts ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Axle ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Springs ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DrawKnife ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Froe ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Inshave ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodworkingTools ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Scorp ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Lockpick ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkeletonsKey ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TinkerTools ), 5 ); }
				if ( MyServerSettings.BuyChance() ){Add( typeof( Board ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Log ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Pickaxe ), 16 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Hammer ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SmithHammer ), 11 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ButcherKnife ), 6 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CrystalScales ), Utility.Random( 250,500 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GolemManual ), Utility.Random( 500,750 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PowerCrystal ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ArcaneGem ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ClockworkAssembly ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BottleOil ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ThrowingWeapon ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TrapKit ), 210 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkA ), Utility.Random( 5, 10 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkB ), Utility.Random( 10, 20 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkC ), Utility.Random( 15, 30 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkD ), Utility.Random( 20, 40 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkE ), Utility.Random( 25, 50 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkF ), Utility.Random( 30, 60 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkG ), Utility.Random( 35, 70 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkH ), Utility.Random( 40, 80 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkI ), Utility.Random( 45, 90 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkJ ), Utility.Random( 50, 100 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkK ), Utility.Random( 55, 110 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkL ), Utility.Random( 60, 120 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkM ), Utility.Random( 65, 130 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkN ), Utility.Random( 70, 140 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkO ), Utility.Random( 75, 150 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkP ), Utility.Random( 80, 160 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkQ ), Utility.Random( 85, 170 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkR ), Utility.Random( 90, 180 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkS ), Utility.Random( 95, 190 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkT ), Utility.Random( 100, 200 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkU ), Utility.Random( 105, 210 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkV ), Utility.Random( 110, 220 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkW ), Utility.Random( 115, 230 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkX ), Utility.Random( 120, 240 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkY ), Utility.Random( 125, 250 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpaceJunkZ ), Utility.Random( 130, 260 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LandmineSetup ), Utility.Random( 100, 300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlasmaGrenade ), Utility.Random( 28, 38 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( ThermalDetonator ), Utility.Random( 28, 38 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PuzzleCube ), Utility.Random( 45, 90 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PlasmaTorch ), Utility.Random( 45, 90 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DuctTape ), Utility.Random( 45, 90 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RobotBatteries ), Utility.Random( 5, 100 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RobotSheetMetal ), Utility.Random( 5, 100 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RobotOil ), Utility.Random( 5, 100 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RobotGears ), Utility.Random( 5, 100 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RobotEngineParts ), Utility.Random( 5, 100 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RobotCircuitBoard ), Utility.Random( 5, 100 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RobotBolt ), Utility.Random( 5, 100 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RobotTransistor ), Utility.Random( 5, 100 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RobotSchematics ), Utility.Random( 500,750 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DataPad ), Utility.Random( 5, 150 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MaterialLiquifier ), Utility.Random( 100, 300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Chainsaw ), Utility.Random( 130, 260 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PortableSmelter ), Utility.Random( 130, 260 ) ); } 
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBVagabond : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBVagabond()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldRing ), 27, Utility.Random( 1,100 ), 0x4CFA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Necklace ), 26, Utility.Random( 1,100 ), 0x4CFE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldNecklace ), 27, Utility.Random( 1,100 ), 0x4CFF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldBeadNecklace ), 27, Utility.Random( 1,100 ), 0x4CFD, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Beads ), 27, Utility.Random( 1,100 ), 0x4CFE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldBracelet ), 27, Utility.Random( 1,100 ), 0x4CF1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GoldEarrings ), 27, Utility.Random( 1,100 ), 0x4CFB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Board ), 3, Utility.Random( 1,100 ), 0x1BD7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( IronIngot ), 6, Utility.Random( 1,100 ), 0x1BF2, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StarSapphire ), 125, Utility.Random( 1,100 ), 0xF21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Emerald ), 100, Utility.Random( 1,100 ), 0xF10, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Sapphire ), 100, Utility.Random( 1,100 ), 0xF19, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ruby ), 75, Utility.Random( 1,100 ), 0xF13, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Citrine ), 50, Utility.Random( 1,100 ), 0xF15, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Amethyst ), 100, Utility.Random( 1,100 ), 0xF16, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Tourmaline ), 75, Utility.Random( 1,100 ), 0xF2D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Amber ), 50, Utility.Random( 1,100 ), 0xF25, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Diamond ), 200, Utility.Random( 1,100 ), 0xF26, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MageEye ), 2, Utility.Random( 10,150 ), 0xF19, 0xB78 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Board ), 3, Utility.Random( 1,100 ), 0x1BD7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( IronIngot ), 6, Utility.Random( 1,100 ), 0x1BF2, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new AnimalBuyInfo( 1, typeof( TrainingElemental ), 50000, 14, 214, 0 ) ); }				
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( Board ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IronIngot ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amber ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Amethyst ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Citrine ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Diamond ), 100 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Emerald ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ruby ), 37 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Sapphire ), 50 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarSapphire ), 62 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Tourmaline ), 47 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MageEye ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldRing ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverRing ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Necklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBeadNecklace ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBeadNecklace ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Beads ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldBracelet ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverBracelet ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GoldEarrings ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverEarrings ), 10 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryRing ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryCirclet ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryNecklace ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryEarrings ), Utility.Random( 50,300 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicJewelryBracelet ), Utility.Random( 50,300 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBVarietyDealer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBVarietyDealer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( ArtifactVase ), Utility.Random( 50000,150000 ), 1, 0x0B48, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ArtifactLargeVase ), Utility.Random( 50000,150000 ), 1, 0x0B47, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TapestryOfSosaria ), Utility.Random( 50000,150000 ), 1, 0x234E, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BlueDecorativeRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BlueFancyRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BluePlainRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( CinnamonFancyRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( CurtainsDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( FountainDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GoldenDecorativeRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HangingAxesDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HangingSwordsDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( PinkFancyRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( RedPlainRugDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( WallBannerDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DecorativeShieldDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StoneAnkhDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BannerDeed ), Utility.Random( 50000,150000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DecoTray ), Utility.Random( 50000,150000 ), 1, 0x992, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DecoTray2 ), Utility.Random( 50000,150000 ), 1, 0x991, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePile01AddonDeed ), Utility.Random( 80000,120000 ), 1, 0x0E41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePile02AddonDeed ), Utility.Random( 80000,120000 ), 1, 0x0E41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePile03AddonDeed ), Utility.Random( 80000,120000 ), 1, 0x0E41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePile04AddonDeed ), Utility.Random( 80000,120000 ), 1, 0x0E41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePile05AddonDeed ), Utility.Random( 80000,120000 ), 1, 0x0E41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePileAddonDeed ), Utility.Random( 120000,200000 ), 1, 0x0E41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePile2AddonDeed ), Utility.Random( 120000,200000 ), 1, 0x0E41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasurePile3AddonDeed ), Utility.Random( 120000,200000 ), 1, 0x0E41, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( LootChest ), 600 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPainting ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPaintingA ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPaintingB ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPaintingC ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPaintingD ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPaintingE ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPaintingF ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxPaintingG ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxSculptors ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxSculptorsA ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxSculptorsB ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxSculptorsC ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxSculptorsD ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( WaxSculptorsE ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( DragonLamp ), Utility.Random( 50,500 ) ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( DragonPedStatue ), Utility.Random( 50,500 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBVeterinarian : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBVeterinarian()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Bandage ), 2, Utility.Random( 10,150 ), 0xE21, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LesserHealPotion ), 100, Utility.Random( 1,100 ), 0x25FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 1,100 ), 0xF85, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 1,100 ), 0xF84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RefreshPotion ), 100, Utility.Random( 1,100 ), 0xF0B, 0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){Add( new GenericBuyInfo( typeof( HitchingPost ), 10000, Utility.Random( 1,3 ), 0x14E7, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( LesserHealPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RefreshPotion ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Garlic ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ginseng ), 2 ); } 
				if ( MyServerSettings.BuyCommonChance() ){Add( typeof( HitchingPost ), 2500 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AlienEgg ), Utility.Random( 500,1000 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DragonEgg ), Utility.Random( 500,1000 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( FirstAidKit ), Utility.Random( 100,250 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBWaiter : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBWaiter()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Ale, 70, Utility.Random( 1,100 ), 0x99F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Wine, 70, Utility.Random( 1,100 ), 0x9C7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Liquor, 70, Utility.Random( 1,100 ), 0x99B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Jug ), BeverageType.Cider, 130, Utility.Random( 1,100 ), 0x9C8, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Milk, 180, Utility.Random( 1,100 ), 0x9F0, 0 ) ); }
				if ( 1 > 0 ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Ale, 110, Utility.Random( 1,100 ), 0x1F95, 0 ) ); }
				Add( new GenericBuyInfo( typeof( Pitcher ), 3, Utility.Random( 50,100 ), 0x1F97, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Cider, 110, Utility.Random( 1,100 ), 0x1F97, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Liquor, 110, Utility.Random( 1,100 ), 0x1F99, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Wine, 110, Utility.Random( 1,100 ), 0x1F9B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new BeverageBuyInfo( typeof( Pitcher ), BeverageType.Water, 90, Utility.Random( 1,100 ), 0x1F9D, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( BreadLoaf ), 60, Utility.Random( 1,100 ), 0x103B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CheeseWheel ), 410, Utility.Random( 1,100 ), 0x97E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CookedBird ), 170, Utility.Random( 1,100 ), 0x9B7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LambLeg ), 80, Utility.Random( 1,100 ), 0x160A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfCarrots ), 30, Utility.Random( 1,100 ), 0x15F9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfCorn ), 30, Utility.Random( 1,100 ), 0x15FA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfLettuce ), 30, Utility.Random( 1,100 ), 0x15FB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfPeas ), 30, Utility.Random( 1,100 ), 0x15FC, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( EmptyPewterBowl ), 5, Utility.Random( 1,100 ), 0x15FD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfCorn ), 30, Utility.Random( 1,100 ), 0x15FE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfLettuce ), 30, Utility.Random( 1,100 ), 0x15FF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfPeas ), 30, Utility.Random( 1,100 ), 0x1600, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PewterBowlOfFoodPotatos ), 30, Utility.Random( 1,100 ), 0x1601, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfStew ), 30, Utility.Random( 1,100 ), 0x1604, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WoodenBowlOfTomatoSoup ), 30, Utility.Random( 1,100 ), 0x1606, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ApplePie ), 70, Utility.Random( 1,100 ), 0x1041, 0 ) ); } //OSI just has Pie, not Apple/Fruit/Meat
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBWeaponSmith: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBWeaponSmith() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WeaponAbilityBook ), 5, Utility.Random( 1,100 ), 0x2254, 0 ) ); }
			}
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( RareAnvil ), Utility.Random( 200,1000 ) ); } 
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBWeaver: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBWeaver() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Dyes ), 8, Utility.Random( 1,100 ), 0xFA9, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DyeTub ), 8, Utility.Random( 1,100 ), 0xFAB, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( UncutCloth ), 3, Utility.Random( 1,100 ), 0x1761, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( UncutCloth ), 3, Utility.Random( 1,100 ), 0x1762, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( UncutCloth ), 3, Utility.Random( 1,100 ), 0x1763, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( UncutCloth ), 3, Utility.Random( 1,100 ), 0x1764, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoltOfCloth ), 100, Utility.Random( 1,100 ), 0xf9B, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoltOfCloth ), 100, Utility.Random( 1,100 ), 0xf9C, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoltOfCloth ), 100, Utility.Random( 1,100 ), 0xf96, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoltOfCloth ), 100, Utility.Random( 1,100 ), 0xf97, 0 ) ); } 
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DarkYarn ), 18, Utility.Random( 1,100 ), 0xE1D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LightYarn ), 18, Utility.Random( 1,100 ), 0xE1E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LightYarnUnraveled ), 18, Utility.Random( 1,100 ), 0xE1F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( PaintCanvas ), 500, Utility.Random( 1,100 ), 0xA6C, 0x47E ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( Scissors ), 11, Utility.Random( 1,100 ), 0xF9F, 0 ) ); }
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Scissors ), 6 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Dyes ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( DyeTub ), 4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( UncutCloth ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoltOfCloth ), 35 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( LightYarnUnraveled ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LightYarn ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DarkYarn ), 9 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PaintCanvas ), 250 ); } 
			} 
		} 
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBNecroMage : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBNecroMage()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BatWing ), 3, 20, 0xF78, 0 ) );
				Add( new GenericBuyInfo( typeof( DaemonBlood ), 6, 20, 0xF7D, 0 ) );
				Add( new GenericBuyInfo( typeof( PigIron ), 5, 20, 0xF8A, 0 ) );
				Add( new GenericBuyInfo( typeof( NoxCrystal ), 6, 20, 0xF8E, 0 ) );
				Add( new GenericBuyInfo( typeof( GraveDust ), 3, 20, 0xF8F, 0 ) );
				Add( new GenericBuyInfo( typeof( BloodOathScroll ), 25, 5, 0x2263, 0 ) );
				Add( new GenericBuyInfo( typeof( CorpseSkinScroll ), 28, 5, 0x2263, 0 ) );
				Add( new GenericBuyInfo( typeof( CurseWeaponScroll ), 12, 5, 0x2263, 0 ) );
				Add( new GenericBuyInfo( typeof( PolishBoneBrush ), 12, 10, 0x1371, 0 ) );
				Add( new GenericBuyInfo( typeof( GraveShovel ), 12, Utility.Random( 1,100 ), 0xF39, 0x966 ) );
				Add( new GenericBuyInfo( typeof( SurgeonsKnife ), 14, Utility.Random( 1,100 ), 0xEC4, 0x1B0 ) );
				Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) );
				Add( new GenericBuyInfo( typeof( MixingCauldron ), 247, 1, 0x269C, 0 ) );
				Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 1,100 ), 0x1E27, 0x979 ) );
				Add( new GenericBuyInfo( typeof( CBookNecroticAlchemy ), 50, Utility.Random( 1,100 ), 0x2253, 0x4AA ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) ); } 
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( AlchemyPouch ), Utility.Random( 2900,3500 ), Utility.Random( 1,2 ), 0x1C10, 0x89F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardStaff ), 40, Utility.Random( 1,5 ), 0x0908, 0xB3A ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardStick ), 38, Utility.Random( 1,5 ), 0xDF2, 0xB3A ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MageEye ), 2, Utility.Random( 10,150 ), 0xF19, 0xB78 ) ); }
				Add( new GenericBuyInfo( typeof( CorruptedMoonStone ), 5000, 5, 0xF8B, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BatWing ), 1 );
				Add( typeof( DaemonBlood ), 3 );
				Add( typeof( PigIron ), 2 );
				Add( typeof( NoxCrystal ), 3 );
				Add( typeof( GraveDust ), 1 );
				Add( typeof( ExorcismScroll ), 1 );
				Add( typeof( AnimateDeadScroll ), 26 );
				Add( typeof( BloodOathScroll ), 26 );
				Add( typeof( CorpseSkinScroll ), 26 );
				Add( typeof( CurseWeaponScroll ), 26 );
				Add( typeof( EvilOmenScroll ), 26 );
				Add( typeof( PainSpikeScroll ), 26 );
				Add( typeof( SummonFamiliarScroll ), 26 );
				Add( typeof( HorrificBeastScroll ), 27 );
				Add( typeof( MindRotScroll ), 39 );
				Add( typeof( PoisonStrikeScroll ), 39 );
				Add( typeof( WraithFormScroll ), 51 );
				Add( typeof( LichFormScroll ), 64 );
				Add( typeof( StrangleScroll ), 64 );
				Add( typeof( WitherScroll ), 64 );
				Add( typeof( VampiricEmbraceScroll ), 101 );
				Add( typeof( VengefulSpiritScroll ), 114 );
				Add( typeof( PolishBoneBrush ), 6 );
				Add( typeof( PolishedSkull ), 3 );
				Add( typeof( PolishedBone ), 3 );
				if ( MyServerSettings.BuyChance() ){Add( typeof( MixingCauldron ), 123 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( MixingSpoon ), 17 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SurgeonsKnife ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullMinotaur ), Utility.Random( 50,150 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullWyrm ), Utility.Random( 200,400 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullGreatDragon ), Utility.Random( 300,600 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullDragon ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullDemon ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullGiant ), Utility.Random( 100,300 ) ); }  
				Add( typeof( CorpseSailor ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( CorpseChest ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BuriedBody ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BoneContainer ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( LeftLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( TastyHeart ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( BodyPart ), Utility.RandomMinMax( 30, 90 ) );
				Add( typeof( Head ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( LeftArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Torso ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bone ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RibCage ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( BonePile ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bones ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( GraveChest ), Utility.RandomMinMax( 100, 500 ) );
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenCoffin ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenCasket ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StoneCoffin ), 45 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StoneCasket ), 45 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DracolichSkull ), Utility.Random( 500,1000 ) ); } 
				if ( MyServerSettings.BuyChance() ){ Add( typeof( WizardStaff ), 20 ); } 
				if ( MyServerSettings.BuyChance() ){ Add( typeof( WizardStick ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){ Add( typeof( MageEye ), 1 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBNecromancer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBNecromancer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( NecromancerSpellbook ), 115, 1, 0x2253, 0 ) );
				Add( new GenericBuyInfo( typeof( NecroSkinPotion ), 1000, 1, 0x1006, 0 ) );
				Add( new GenericBuyInfo( typeof( BookofDead ), 25000, 1,  0x1C11, 2500 ) );
				Add( new GenericBuyInfo( typeof( DarkHeart ), 500, 5, 0xF91, 0x386 ) );
				Add( new GenericBuyInfo( typeof( BatWing ), 3, 20, 0xF78, 0 ) );
				Add( new GenericBuyInfo( typeof( DaemonBlood ), 6, 20, 0xF7D, 0 ) );
				Add( new GenericBuyInfo( typeof( PigIron ), 5, 20, 0xF8A, 0 ) );
				Add( new GenericBuyInfo( typeof( NoxCrystal ), 6, 20, 0xF8E, 0 ) );
				Add( new GenericBuyInfo( typeof( GraveDust ), 3, 20, 0xF8F, 0 ) );
				Add( new GenericBuyInfo( typeof( BloodOathScroll ), 25, 5, 0x2263, 0 ) );
				Add( new GenericBuyInfo( typeof( CorpseSkinScroll ), 28, 5, 0x2263, 0 ) );
				Add( new GenericBuyInfo( typeof( CurseWeaponScroll ), 12, 5, 0x2263, 0 ) );
				Add( new GenericBuyInfo( typeof( PolishBoneBrush ), 12, 10, 0x1371, 0 ) );
				Add( new GenericBuyInfo( typeof( GraveShovel ), 12, Utility.Random( 1,100 ), 0xF39, 0x966 ) );
				Add( new GenericBuyInfo( typeof( SurgeonsKnife ), 14, Utility.Random( 1,100 ), 0xEC4, 0x1B0 ) );
				Add( new GenericBuyInfo( typeof( MixingCauldron ), 247, 1, 0x269C, 0 ) );
				Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 1,100 ), 0x1E27, 0x979 ) );
				Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) );
				Add( new GenericBuyInfo( typeof( CBookNecroticAlchemy ), 50, Utility.Random( 1,100 ), 0x2253, 0x4AA ) );
				Add( new GenericBuyInfo( "undead horse", typeof( NecroHorse ), 500000, 5, 0x2617, 0xB97 ) );
				Add( new GenericBuyInfo( "daemon servant", typeof( DaemonMount ), 350000, 5, 11669, 0x4AA ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) ); } 
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( AlchemyPouch ), Utility.Random( 2900,3500 ), Utility.Random( 1,2 ), 0x1C10, 0x89F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardStaff ), 40, Utility.Random( 1,5 ), 0x0908, 0xB3A ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardStick ), 38, Utility.Random( 1,5 ), 0xDF2, 0xB3A ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MageEye ), 2, Utility.Random( 10,150 ), 0xF19, 0xB78 ) ); }
				Add( new GenericBuyInfo( typeof( CorruptedMoonStone ), 5000, 5, 0xF8B, 0 ) );
				Add( new GenericBuyInfo( typeof( BagOfNecroReagents ), 500, 10, 0xE76, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BoneGrinder ), 25000, Utility.Random( 1,4 ), 0x3DB9, 0 ) ); }

				
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BatWing ), 1 );
				Add( typeof( DaemonBlood ), 3 );
				Add( typeof( PigIron ), 2 );
				Add( typeof( NoxCrystal ), 3 );
				Add( typeof( GraveDust ), 1 );
				Add( typeof( ExorcismScroll ), 1 );
				Add( typeof( AnimateDeadScroll ), 26 );
				Add( typeof( BloodOathScroll ), 26 );
				Add( typeof( CorpseSkinScroll ), 26 );
				Add( typeof( CurseWeaponScroll ), 26 );
				Add( typeof( EvilOmenScroll ), 26 );
				Add( typeof( PainSpikeScroll ), 26 );
				Add( typeof( SummonFamiliarScroll ), 26 );
				Add( typeof( HorrificBeastScroll ), 27 );
				Add( typeof( MindRotScroll ), 39 );
				Add( typeof( PoisonStrikeScroll ), 39 );
				Add( typeof( WraithFormScroll ), 51 );
				Add( typeof( LichFormScroll ), 64 );
				Add( typeof( StrangleScroll ), 64 );
				Add( typeof( WitherScroll ), 64 );
				Add( typeof( VampiricEmbraceScroll ), 101 );
				Add( typeof( VengefulSpiritScroll ), 114 );
				Add( typeof( PolishBoneBrush ), 6 );
				Add( typeof( PolishedSkull ), 3 );
				Add( typeof( PolishedBone ), 3 );
				Add( typeof( NecromancerSpellbook ), 55 );
				Add( typeof( DeathKnightSpellbook ), Utility.Random( 100,300 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullDragon ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullDemon ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullGiant ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( MixingCauldron ), 123 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( MixingSpoon ), 17 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SurgeonsKnife ), 7 ); } 
				if ( MyServerSettings.BuyChance() ){ Add( typeof( BoneContainer ), 250 ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ClumsyMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CreateFoodMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FeebleMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( HealMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicArrowMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( NightSightMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ReactiveArmorMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WeaknessMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( AgilityMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CunningMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CureMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( HarmMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicTrapMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicUntrapMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ProtectionMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( StrengthMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( BlessMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireballMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicLockMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicUnlockMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PoisonMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( TelekinesisMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( TeleportMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WallofStoneMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ArchCureMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ArchProtectionMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CurseMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireFieldMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( GreaterHealMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( LightningMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ManaDrainMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( RecallMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( BladeSpiritsMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( DispelFieldMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( IncognitoMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicReflectionMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MindBlastMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ParalyzeMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PoisonFieldMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( SummonCreatureMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( DispelMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyBoltMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ExplosionMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( InvisibilityMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MarkMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MassCurseMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ParalyzeFieldMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( RevealMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ChainLightningMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyFieldMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FlameStrikeMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( GateTravelMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ManaVampireMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MassDispelMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MeteorSwarmMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PolymorphMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( AirElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EarthElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EarthquakeMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyVortexMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ResurrectionMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( SummonDaemonMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WaterElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MyNecromancerSpellbook ), Utility.Random( 250,1000 ) ); } 
				Add( typeof( CorpseSailor ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( CorpseChest ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BodyPart ), Utility.RandomMinMax( 30, 90 ) );
				Add( typeof( BuriedBody ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BoneContainer ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( LeftLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( TastyHeart ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( Head ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( LeftArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Torso ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bone ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RibCage ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( BonePile ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bones ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( GraveChest ), Utility.RandomMinMax( 1000, 5000 ) );
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenCoffin ), 205 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenCasket ), 250 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StoneCoffin ), 450 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StoneCasket ), 450 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DemonPrison ), Utility.Random( 500,1000 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DracolichSkull ), Utility.Random( 500,1000 ) ); } 
				if ( MyServerSettings.BuyChance() ){ Add( typeof( WizardStaff ), 20 ); } 
				if ( MyServerSettings.BuyChance() ){ Add( typeof( WizardStick ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){ Add( typeof( MageEye ), 1 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBWitches : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBWitches()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NecromancerSpellbook ), 115, 1, 0x2253, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BatWing ), 3, Utility.Random( 10,100 ), 0xF78, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DaemonBlood ), 6, Utility.Random( 10,100 ), 0xF7D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PigIron ), 5, Utility.Random( 10,100 ), 0xF8A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NoxCrystal ), 6, Utility.Random( 10,100 ), 0xF8E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GraveDust ), 3, Utility.Random( 10,100 ), 0xF8F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BloodOathScroll ), 25, 5, 0x2263, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CorpseSkinScroll ), 28, 5, 0x2263, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CurseWeaponScroll ), 12, 5, 0x2263, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( TarotDeck ), 5, Utility.Random( 1,100 ), 0x12AB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PolishBoneBrush ), 12, 10, 0x1371, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GraveShovel ), 12, Utility.Random( 1,100 ), 0xF39, 0x966 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( SurgeonsKnife ), 14, Utility.Random( 1,100 ), 0xEC4, 0x1B0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingCauldron ), 247, 1, 0x269C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 1,100 ), 0x1E27, 0x979 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CBookNecroticAlchemy ), 50, Utility.Random( 1,100 ), 0x2253, 0x4AA ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BlackDyeTub ), 5000, 1, 0xFAB, 0x1 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( reagents_magic_jar2 ), 1500, Utility.Random( 1,100 ), 0x1007, 0xB97 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HellsGateScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x54F ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ManaLeechScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xB87 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NecroCurePoisonScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x8A2 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NecroPoisonScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x4F8 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NecroUnlockScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x493 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PhantasmScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x6DE ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( RetchedAirScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xA97 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( SpectreShadowScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x17E ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( UndeadEyesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x491 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( VampireGiftScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xB85 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( WallOfSpikesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xB8F ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( BloodPactScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x5B5 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GhostlyImagesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xBF ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GhostPhaseScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x47E ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GraveyardGatewayScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x2EA ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HellsBrandScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x54C ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) ); } 
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( AlchemyPouch ), Utility.Random( 2900,3500 ), Utility.Random( 1,2 ), 0x1C10, 0x89F ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardStaff ), 40, Utility.Random( 1,5 ), 0x0908, 0xB3A ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardStick ), 38, Utility.Random( 1,5 ), 0xDF2, 0xB3A ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MageEye ), 2, Utility.Random( 10,150 ), 0xF19, 0xB78 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BatWing ), 1 );
				Add( typeof( DaemonBlood ), 3 );
				Add( typeof( PigIron ), 2 );
				Add( typeof( NoxCrystal ), 3 );
				Add( typeof( GraveDust ), 1 );
				Add( typeof( ExorcismScroll ), 1 );
				Add( typeof( AnimateDeadScroll ), 26 );
				Add( typeof( BloodOathScroll ), 26 );
				Add( typeof( CorpseSkinScroll ), 26 );
				Add( typeof( CurseWeaponScroll ), 26 );
				Add( typeof( EvilOmenScroll ), 26 );
				Add( typeof( PainSpikeScroll ), 26 );
				Add( typeof( SummonFamiliarScroll ), 26 );
				Add( typeof( HorrificBeastScroll ), 27 );
				Add( typeof( MindRotScroll ), 39 );
				Add( typeof( PoisonStrikeScroll ), 39 );
				Add( typeof( WraithFormScroll ), 51 );
				Add( typeof( LichFormScroll ), 64 );
				Add( typeof( StrangleScroll ), 64 );
				Add( typeof( WitherScroll ), 64 );
				Add( typeof( VampiricEmbraceScroll ), 101 );
				Add( typeof( VengefulSpiritScroll ), 114 );
				Add( typeof( PolishBoneBrush ), 6 );
				Add( typeof( PolishedSkull ), 3 );
				Add( typeof( PolishedBone ), 3 );
				Add( typeof( NecromancerSpellbook ), 55 );
				Add( typeof( DeathKnightSpellbook ), Utility.Random( 100,300 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullMinotaur ), Utility.Random( 50,150 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullWyrm ), Utility.Random( 200,400 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullGreatDragon ), Utility.Random( 300,600 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullDragon ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullDemon ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullGiant ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){ Add( typeof( MixingCauldron ), 123 ); }  
				if ( MyServerSettings.BuyChance() ){ Add( typeof( MixingSpoon ), 17 ); }  
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MyNecromancerSpellbook ), Utility.Random( 250,1000 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( BlackDyeTub ), 2500 ); }  
				Add( typeof( CorpseSailor ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( CorpseChest ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BuriedBody ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BodyPart ), Utility.RandomMinMax( 30, 90 ) );
				Add( typeof( BoneContainer ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( LeftLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( TastyHeart ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( Head ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( LeftArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Torso ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bone ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RibCage ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( BonePile ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bones ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( GraveChest ), Utility.RandomMinMax( 100, 500 ) );
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
				if ( MyServerSettings.BuyChance() ){Add( typeof( DemonPrison ), Utility.Random( 500,1000 ) ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DracolichSkull ), Utility.Random( 500,1000 ) ); } 
				if ( MyServerSettings.BuyChance() ){ Add( typeof( WizardStaff ), 20 ); } 
				if ( MyServerSettings.BuyChance() ){ Add( typeof( WizardStick ), 19 ); } 
				if ( MyServerSettings.BuyChance() ){ Add( typeof( MageEye ), 1 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMortician : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMortician()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GraveShovel ), 12, Utility.Random( 1,100 ), 0xF39, 0x966 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( SurgeonsKnife ), 14, Utility.Random( 1,100 ), 0xEC4, 0x1B0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingCauldron ), 247, 1, 0x269C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 1,100 ), 0x1E27, 0x979 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 1,100 ), 0x10B4, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PolishBoneBrush ), 12, 10, 0x1371, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CBookNecroticAlchemy ), 50, Utility.Random( 1,100 ), 0x2253, 0x4AA ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HellsGateScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x54F ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ManaLeechScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xB87 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NecroCurePoisonScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x8A2 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NecroPoisonScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x4F8 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NecroUnlockScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x493 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PhantasmScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x6DE ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( RetchedAirScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xA97 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( SpectreShadowScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x17E ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( UndeadEyesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x491 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( VampireGiftScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xB85 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( WallOfSpikesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xB8F ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( BloodPactScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x5B5 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GhostlyImagesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0xBF ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GhostPhaseScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x47E ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GraveyardGatewayScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x2EA ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HellsBrandScroll ), Utility.Random( 10,100 ), Utility.Random( 1,5 ), 0x1007, 0x54C ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( AlchemyPouch ), Utility.Random( 2900,3500 ), Utility.Random( 1,2 ), 0x1C10, 0x89F ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( WoodenCoffin ), 100, 1, 0x2800, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( WoodenCasket ), 100, 1, 0x27E9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StoneCoffin ), 180, 1, 0x27E0, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StoneCasket ), 180, 1, 0x2802, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullMinotaur ), Utility.Random( 50,150 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullWyrm ), Utility.Random( 200,400 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullGreatDragon ), Utility.Random( 300,600 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullDragon ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullDemon ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SkullGiant ), Utility.Random( 100,300 ) ); }  
				if ( MyServerSettings.BuyChance() ){ Add( typeof( MixingCauldron ), 123 ); }  
				if ( MyServerSettings.BuyChance() ){ Add( typeof( MixingSpoon ), 17 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoneArms ), 94 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoneChest ), 121 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoneGloves ), 72 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoneLegs ), 109 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BoneSkirt ), 109 ); }				
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MyNecromancerSpellbook ), Utility.Random( 250,1000 ) ); } 
				Add( typeof( CorpseSailor ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( CorpseChest ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BuriedBody ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BodyPart ), Utility.RandomMinMax( 30, 90 ) );
				Add( typeof( BoneContainer ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( LeftLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( TastyHeart ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( Head ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( LeftArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Torso ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bone ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RibCage ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( BonePile ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bones ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( GraveChest ), Utility.RandomMinMax( 100, 500 ) );
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
				Add( typeof( PolishBoneBrush ), 6 );
				Add( typeof( PolishedSkull ), 3 );
				Add( typeof( PolishedBone ), 3 );
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenCoffin ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WoodenCasket ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StoneCoffin ), 45 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StoneCasket ), 45 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DracolichSkull ), Utility.Random( 500,1000 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMage : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMage()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Spellbook ), 18, Utility.Random( 1,100 ), 0xEFA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NecromancerSpellbook ), 115, Utility.Random( 1,100 ), 0x2253, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ScribesPen ), 8, Utility.Random( 1,100 ), 0x2051, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlankScroll ), 5, Utility.Random( 1,100 ), 0x0E34, 0 ) ); }
				if ( 1 > 0 ){Add( new GenericBuyInfo( "1041072", typeof( MagicWizardsHat ), 11, Utility.Random( 1,100 ), 0x1718, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WitchHat ), 11, Utility.Random( 1,100 ), 0x2FC3, Utility.RandomDyedHue() ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( RecallRune ), 100, Utility.Random( 1,100 ), 0x1F14, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlackPearl ), 5, Utility.Random( 1,100 ), 0x266F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bloodmoss ), 5, Utility.Random( 1,100 ), 0xF7B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 1,100 ), 0xF84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 1,100 ), 0xF85, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MandrakeRoot ), 3, Utility.Random( 1,100 ), 0xF86, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Nightshade ), 3, Utility.Random( 1,100 ), 0xF88, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SpidersSilk ), 3, Utility.Random( 1,100 ), 0xF8D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SulfurousAsh ), 3, Utility.Random( 1,100 ), 0xF8C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BatWing ), 3, Utility.Random( 1,100 ), 0xF78, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DaemonBlood ), 6, Utility.Random( 1,100 ), 0xF7D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PigIron ), 5, Utility.Random( 1,100 ), 0xF8A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( NoxCrystal ), 6, Utility.Random( 1,100 ), 0xF8E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GraveDust ), 3, Utility.Random( 1,100 ), 0xF8F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BloodOathScroll ), 25, Utility.Random( 1,100 ), 0x2263, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CorpseSkinScroll ), 28, Utility.Random( 1,100 ), 0x2263, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CurseWeaponScroll ), 12, Utility.Random( 1,100 ), 0x2263, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( IronFlask ), 500, Utility.Random( 1,100 ), 0x282E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardStaff ), 40, Utility.Random( 1,5 ), 0x0908, 0xB3A ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( WizardStick ), 38, Utility.Random( 1,5 ), 0xDF2, 0xB3A ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MageEye ), 2, Utility.Random( 10,150 ), 0xF19, 0xB78 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "Stable Gates", typeof( LinkedGateBag ), 300000, 1, 0xE76, 0xABE ) ); }
				Add( new GenericBuyInfo( typeof( BagOfReagents ), 500, 25, 0xE76, 0 ) ); 


				Type[] types = Loot.RegularScrollTypes;

				int circles = 3;

				for ( int i = 0; i < circles*8 && i < types.Length; ++i )
				{
					int itemID = 0x1F2E + i;

					if ( i == 6 )
						itemID = 0x1F2D;
					else if ( i > 6 )
						--itemID;

					if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( types[i], 12 + ((i / 8) * 10), 20, itemID, 0 ) ); }
				}
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( MagicTalisman ), Utility.Random( 50,100 ) ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( WizardsHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WitchHat ), 5 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BlackPearl ), 3 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Bloodmoss ),4 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( MandrakeRoot ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Garlic ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Ginseng ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( Nightshade ), 2 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpidersSilk ), 2 ); }  
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SulfurousAsh ), 1 ); }  
				if ( MyServerSettings.BuyChance() ){Add( typeof( BatWing ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( DaemonBlood ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PigIron ), 2 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( NoxCrystal ), 3 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GraveDust ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RecallRune ), 13 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( Spellbook ), 25 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( NecromancerSpellbook ), 55 ); } 
				if ( MyServerSettings.SellCommonChance() ){Add( typeof( MysticalPearl ), 250 ); } 

				Type[] types = Loot.RegularScrollTypes;

				for ( int i = 0; i < types.Length; ++i )
					if ( MyServerSettings.BuyChance() ){Add(types[i], i + 3 + (i / 4)); } 
			
				if ( MyServerSettings.BuyChance() ){Add( typeof( ExorcismScroll ), 1 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AnimateDeadScroll ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( BloodOathScroll ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CorpseSkinScroll ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( CurseWeaponScroll ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EvilOmenScroll ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PainSpikeScroll ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SummonFamiliarScroll ), 26 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( HorrificBeastScroll ), 27 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MindRotScroll ), 39 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( PoisonStrikeScroll ), 39 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WraithFormScroll ), 51 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( LichFormScroll ), 64 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StrangleScroll ), 64 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( WitherScroll ), 64 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( VampiricEmbraceScroll ), 101 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( VengefulSpiritScroll ), 114 ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ClumsyMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CreateFoodMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FeebleMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( HealMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicArrowMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( NightSightMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ReactiveArmorMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WeaknessMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( AgilityMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CunningMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CureMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( HarmMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicTrapMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicUntrapMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ProtectionMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( StrengthMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( BlessMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireballMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicLockMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicUnlockMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PoisonMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( TelekinesisMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( TeleportMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WallofStoneMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ArchCureMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ArchProtectionMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CurseMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireFieldMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( GreaterHealMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( LightningMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ManaDrainMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( RecallMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( BladeSpiritsMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( DispelFieldMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( IncognitoMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicReflectionMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MindBlastMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ParalyzeMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PoisonFieldMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( SummonCreatureMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( DispelMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyBoltMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ExplosionMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( InvisibilityMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MarkMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MassCurseMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ParalyzeFieldMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( RevealMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ChainLightningMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyFieldMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FlameStrikeMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( GateTravelMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ManaVampireMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MassDispelMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MeteorSwarmMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PolymorphMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( AirElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EarthElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EarthquakeMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyVortexMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ResurrectionMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( SummonDaemonMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WaterElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MyNecromancerSpellbook ), Utility.Random( 250,1000 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MySpellbook ), Utility.Random( 250,1000 ) ); } 
				Add( typeof( TomeOfWands ), Utility.Random( 100,400 ) );
				if ( MyServerSettings.SellChance() ){Add( typeof( DemonPrison ), Utility.Random( 500,1000 ) ); } 
				if ( MyServerSettings.SellChance() ){ Add( typeof( WizardStaff ), 20 ); } 
				if ( MyServerSettings.SellChance() ){ Add( typeof( WizardStick ), 19 ); } 
				if ( MyServerSettings.SellChance() ){ Add( typeof( MageEye ), 1 ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBGodlySewing: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGodlySewing()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( GodSewing ), 1000, 20, 0x0F9F, 0x501 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBGodlySmithing: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGodlySmithing()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( GodSmithing ), 1000, 20, 0x0FB5, 0x501 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBGodlyBrewing: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGodlyBrewing()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( GodBrewing ), 1000, 20, 0x0E28, 0x501 ) );
				Add( new GenericBuyInfo( typeof( reagents_magic_jar1 ), 2000, Utility.Random( 1,100 ), 0x1007, 0 ) );
				Add( new GenericBuyInfo( typeof( reagents_magic_jar2 ), 1500, Utility.Random( 1,100 ), 0x1007, 0xB97 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMazeStore: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMazeStore()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( reagents_magic_jar1 ), 2000, 20, 0x1007, 0 ) );
				Add( new GenericBuyInfo( typeof( reagents_magic_jar2 ), 1500, 20, 0x1007, 0xB97 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBBuyArtifacts: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBuyArtifacts()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
								if ( MyServerSettings.BuyRareChance() ){Add( typeof( AbysmalGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AchillesShield ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AchillesSpear ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AcidProofRobe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Aegis ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AilricsLongbow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Antiquity ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AngelicEmbrace ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AngeroftheGods ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Annihilation ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcaneArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcaneCap ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcaneGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcaneGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcaneLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcaneShield ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcaneTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcanicRobe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcticBeacon ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArcticDeathDealer ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmorOfFortune ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmorOfInsight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmorOfNobility ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmsOfAegis ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmsOfFortune ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmsOfInsight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmsOfNobility ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmsOfTheFallenKing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmsOfTheHarrower ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ArmsOfToxicity ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AuraOfShadows ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AxeoftheMinotaur ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BagOfHolding ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BalancingDeed ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BeltofHercules ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BlazeOfDeath ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BookOfKnowledge ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BootsofHermes ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BowOfTheJukaKing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BowofthePhoenix ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BraceletOfHealth ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BraceletOfTheElements ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BraceletOfTheVile ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BreathOfTheDead ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BurglarsBandana ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Calm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CandelabraOfSouls ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CandleCold ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CandleEnergy ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CandleFire ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CandleNecromancer ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CandlePoison ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CandleWizard ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CapOfFortune ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CapOfTheFallenKing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CavortingClub ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CircletOfTheSorceress ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CoifOfBane ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CoifOfFire ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ColdBlood ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ColoringBook ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ConansHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ConansSword ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CrimsonCincture ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CrownOfTalKeesh ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DaggerOfVenom ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DarkGuardiansChest ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DarkLordsPitchfork ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DarkNeck ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DeathsMask ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DivineArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DivineCountenance ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DivineGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DivineGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DivineLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DivineTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DjinnisRing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DreadPirateHat ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DupresCollar ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DupresShield ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EarringBoxSet ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EarringsOfHealth ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EarringsOfProtection ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EarringsOfTheElements ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EarringsOfTheMagician ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EarringsOfTheVile ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ElvenQuiver ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EmbroideredOakLeafCloak ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EnchantedTitanLegBone ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EternalFlame ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EverlastingBottle ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EverlastingLoaf ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EvilMageGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Excalibur ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FalseGodsScepter ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FangOfRactus ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FesteringWound ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Fortifiedarms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FortunateBlades ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Frostbringer ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FurCapeOfTheSorceress ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Fury ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GandalfsHat ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GandalfsRobe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GandalfsStaff ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GauntletsOfNobility ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GeishasObi ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GemOfSeeing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GiantBlackjack ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GladiatorsCollar ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlassSword ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfAegis ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfCorruption ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfDexterity ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfFortune ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfInsight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfRegeneration ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfTheFallenKing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfTheHarrower ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfThePugilist ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GorgetOfAegis ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GorgetOfFortune ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GorgetOfInsight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GuantletsOfAnger ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GwennosHarp ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HammerofThor ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HatOfTheMagi ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HeartOfTheLion ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HellForgedArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HelmOfAegis ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HelmOfBrilliance ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HelmOfInsight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HolyKnightsArmPlates ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HolyKnightsBreastplate ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HolyKnightsGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HolyKnightsGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HolyKnightsLegging ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HolyKnightsPlateHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HolySword ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HuntersArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HuntersGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HuntersGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HuntersHeaddress ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HuntersLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HuntersTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Indecency ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( InquisitorsArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( InquisitorsGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( InquisitorsHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( InquisitorsLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( InquisitorsResolution ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( InquisitorsTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( IolosLute ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JackalsArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JackalsCollar ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JackalsGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JackalsHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JackalsLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JackalsTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JadeScimitar ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JesterHatofChuckles ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( JinBaoriOfGoodFortune ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( KamiNarisIndestructableDoubleAxe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( KodiakBearMask ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LargeBagofHolding ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LegacyOfTheDreadLord ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LeggingsOfAegis ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LeggingsOfBane ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LeggingsOfDeceit ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LeggingsOfEnlightenment ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LeggingsOfFire ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LegsOfFortune ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LegsOfInsight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LegsOfNobility ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LegsOfTheFallenKing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LegsOfTheHarrower ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LieutenantOfTheBritannianRoyalGuard ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LongShot ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LuckyEarrings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LuckyNecklace ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LunaLance ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MadmansHatchet ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagesBand ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagiciansIllusion ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagiciansMempo ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MarbleShield ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MauloftheBeast ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MaulOfTheTitans ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MediumBagofHolding ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MidnightBracers ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MidnightGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MidnightHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MidnightLegs ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MidnightTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MinersPickaxe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( NightsKiss ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( NordicVikingSword ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( NoxBow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( NoxNightlight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( NoxRangersHeavyCrossbow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( OblivionsNeedle ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( OrcChieftainHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( OrcishVisage ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( OrnamentOfTheMagician ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( OrnateCrownOfTheHarrower ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Pestilence ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PixieSwatter ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PolarBearBoots ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PolarBearCape ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PolarBearMask ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PowerSurge ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Quell ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiverOfBlight ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiverOfElements ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiverOfFire ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiverOfIce ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiverOfInfinity ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiverOfLightning ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiverOfRage ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RamusNecromanticScalpel ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Retort ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RighteousAnger ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RingOfHealth ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RingOfTheElements ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RingOfTheMagician ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RingOfTheVile ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RobeOfTeleportation ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RobeOfTheEclipse ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RobeOfTheEquinox ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RobeOfTreason ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RobinHoodsBow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RobinHoodsFeatheredHat ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RodOfResurrection ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RoyalArchersBow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RoyalGuardsChestplate ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RoyalGuardsGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RoyalGuardSurvivalKnife ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SamaritanRobe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SamuraiHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SerpentsFang ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShadowBlade ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShadowDancerArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShadowDancerCap ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShadowDancerGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShadowDancerGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShadowDancerLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShadowDancerTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShaminoCrossbow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShieldOfInvulnerability ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShimmeringTalisman ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShroudOfDeciet ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SinbadsSword ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SmallBagofHolding ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SoulSeeker ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SpiritOfTheTotem ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SprintersSandals ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( StaffOfPower ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( StaffofSnakes ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( StaffOfTheMagi ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Stormbringer ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Subdue ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SwiftStrike ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TalonBite ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TheBeserkersMaul ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TheDragonSlayer ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TheDryadBow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TheRobeOfBritanniaAri ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TheTaskmaster ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TitansHammer ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TorchOfTrapFinding ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TotemArms ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TotemGloves ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TotemGorget ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TotemLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TotemOfVoid ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TotemTunic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TownGuardsHalberd ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TunicOfAegis ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TunicOfBane ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TunicOfFire ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TunicOfTheFallenKing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TunicOfTheHarrower ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( VampiricDaisho ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( VioletCourage ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( VoiceOfTheFallenKing ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WarriorsClasp ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WeaponRenamingTool ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WildfireBow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Windsong ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WizardsPants ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WrathOfTheDryad ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( YashimotosHatsuburi ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ZyronicClaw ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AegisOfGrace ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AlchemistsBauble ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( AxeOfTheHeavens ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BeggarsRobe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BladeDance ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BladeOfInsanity ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BladeOfTheRighteous ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BlightGrippedLongbow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BloodwoodSpirit ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BoneCrusher ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Bonesmasher ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Boomstick ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BottomlessBargainBucket ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( CaptainQuacklebushsCutlass ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ColdForgedBlade ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ConansLoinCloth ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DoubletOfPower ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( EssenceOfBattle ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FeyLeggings ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FleshRipper ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GrayMouserCloak ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GrimReapersMask ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GrimReapersRobe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GrimReapersScythe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HelmOfSwiftness ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( LuminousRuneBlade ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MagusShirt ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MelisandesCorrodedHatchet ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( OverseerSunderedBlade ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Pacify ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PandorasBox ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PendantOfTheMagi ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PhantomStaff ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PrincessIllusion ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RaedsGlory ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ResilientBracer ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( RuneCarvingKnife ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShardThrasher ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SilvanisFeywoodBow ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( StreetFightersVest ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TenguHakama ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TheNightReaper ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ThinkingMansKilt ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( VampiresRobe ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( VeryFancyShirt ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WindSpirit ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( QuiGonsLightSword ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( sotedeathstarrelic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( soter2d2relic ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BloodTrail ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( BowOfHarps ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ChildOfDeath ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Erotica ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( FeverFall ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( GlovesOfTheHardWorker ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( HandsofTabulature ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Kamadon ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( MIBHunter ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( PurposeOfPain ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Revenge ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SatanicHelm ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ShieldOfIce ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( StandStill ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( SwordOfIce ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( TacticalMask ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ThickNeck ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( ValasCompromise ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( Valicious ), Utility.Random( 20000,30000 ) ); }
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( WizardsStrongArm ), Utility.Random( 20000,30000 ) ); }
				
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBGemArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGemArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AmethystShield ), 25425, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldPlateChest ),25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EmeraldShield ), 25425, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GarnetShield ), 25425, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IceFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IcePlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IcePlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IcePlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IcePlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IcePlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IcePlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IceShield ), 25425, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadeFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadePlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadePlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadePlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadePlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadePlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadePlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JadeShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarbleFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarblePlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarblePlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarblePlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarblePlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarblePlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarblePlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarbleShields ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( OnyxShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( QuartzShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RubyShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphireFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphirePlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphirePlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphirePlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphirePlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphirePlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphirePlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SapphireShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SilverShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SpinelShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StarRubyShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazFemalePlateChest ), 25513, 1, 0x1C04, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazPlateArms ), 25494, 1, 0x1410, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazPlateChest ), 25521, 1, 0x1415, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazPlateGloves ), 25372, 1, 0x1414, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazPlateGorget ), 25352, 1, 0x1413, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazPlateHelm ), 25320, 1, 0x1419, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazPlateLegs ), 25509, 1, 0x1411, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TopazShield ), 25415, 1, 0x1B76, Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( AmethystShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( EmeraldShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( GarnetShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IceFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IcePlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IcePlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IcePlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IcePlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IcePlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IcePlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( IceShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadeFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadePlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadePlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadePlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadePlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadePlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadePlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( JadeShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarbleFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarblePlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarblePlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarblePlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarblePlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarblePlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarblePlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( MarbleShields ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( OnyxShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzPlateGloves  ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( QuartzShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RubyShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphirePlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphirePlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphirePlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphirePlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphirePlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphireFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphirePlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SapphireShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SilverShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( SpinelShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( StarRubyShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazPlateArms ), 470 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazPlateGloves ), 360 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazPlateGorget ), 260 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazPlateLegs ), 545 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazPlateChest ), 605 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazFemalePlateChest ), 565 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazPlateHelm ), 330 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( TopazShield ), 575 ); } 
				if ( MyServerSettings.BuyChance() ){Add( typeof( RareAnvil ), Utility.Random( 200,1000 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBRoscoe: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRoscoe()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( 1 > 0 ){Add( new GenericBuyInfo( typeof( LesserManaPotion ), 20, Utility.Random( 1,100 ), 0x23BD, 0x48D ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( ManaPotion ), 40, Utility.Random( 1,100 ), 0x180F, 0x48D ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( GreaterManaPotion ), 80, Utility.Random( 1,100 ), 0x2406, 0x48D ) ); }

				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ClumsyMagicStaff ), 500, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( CreateFoodMagicStaff ), 500, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( FeebleMagicStaff ), 500, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HealMagicStaff ), 500, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( MagicArrowMagicStaff ), 500, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( NightSightMagicStaff ), 500, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ReactiveArmorMagicStaff ), 500, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( WeaknessMagicStaff ), 500, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( AgilityMagicStaff ), 1000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( CunningMagicStaff ), 1000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( CureMagicStaff ), 1000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( HarmMagicStaff ), 1000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( MagicTrapMagicStaff ), 1000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( MagicUntrapMagicStaff ), 1000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ProtectionMagicStaff ), 1000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( StrengthMagicStaff ), 1000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( BlessMagicStaff ), 2000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( FireballMagicStaff ), 2000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( MagicLockMagicStaff ), 2000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( MagicUnlockMagicStaff ), 2000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PoisonMagicStaff ), 2000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( TelekinesisMagicStaff ), 2000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( TeleportMagicStaff ), 2000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( WallofStoneMagicStaff ), 2000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ArchCureMagicStaff ), 4000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ArchProtectionMagicStaff ), 4000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( CurseMagicStaff ), 4000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( FireFieldMagicStaff ), 4000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GreaterHealMagicStaff ), 4000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( LightningMagicStaff ), 4000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ManaDrainMagicStaff ), 4000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RecallMagicStaff ), 4000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BladeSpiritsMagicStaff ), 8000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DispelFieldMagicStaff ), 8000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( IncognitoMagicStaff ), 8000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MagicReflectionMagicStaff ), 8000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MindBlastMagicStaff ), 8000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ParalyzeMagicStaff ), 8000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( PoisonFieldMagicStaff ), 8000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SummonCreatureMagicStaff ), 8000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( DispelMagicStaff ), 16000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EnergyBoltMagicStaff ), 16000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ExplosionMagicStaff ), 16000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( InvisibilityMagicStaff ), 16000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MarkMagicStaff ), 16000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MassCurseMagicStaff ), 16000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ParalyzeFieldMagicStaff ), 16000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( RevealMagicStaff ), 16000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ChainLightningMagicStaff ), 24000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EnergyFieldMagicStaff ), 24000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( FlameStrikeMagicStaff ), 24000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( GateTravelMagicStaff ), 24000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ManaVampireMagicStaff ), 24000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MassDispelMagicStaff ), 24000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MeteorSwarmMagicStaff ), 24000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( PolymorphMagicStaff ), 24000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AirElementalMagicStaff ), 32000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EarthElementalMagicStaff ), 32000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EarthquakeMagicStaff ), 32000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EnergyVortexMagicStaff ), 32000, 1, 0xDF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( FireElementalMagicStaff ), 32000, 1, 0xDF2, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ResurrectionMagicStaff ), 32000, 1, 0xDF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SummonDaemonMagicStaff ), 32000, 1, 0xDF4, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( WaterElementalMagicStaff ), 32000, 1, 0xDF5, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( LesserManaPotion ), 10 ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ManaPotion ), 20 ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( GreaterManaPotion ), 40 ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ClumsyMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CreateFoodMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FeebleMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( HealMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicArrowMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( NightSightMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ReactiveArmorMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WeaknessMagicStaff ), Utility.Random( 10,20 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( AgilityMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CunningMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CureMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( HarmMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicTrapMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicUntrapMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ProtectionMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( StrengthMagicStaff ), Utility.Random( 20,40 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( BlessMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireballMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicLockMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicUnlockMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PoisonMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( TelekinesisMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( TeleportMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WallofStoneMagicStaff ), Utility.Random( 30,60 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ArchCureMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ArchProtectionMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( CurseMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireFieldMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( GreaterHealMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( LightningMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ManaDrainMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( RecallMagicStaff ), Utility.Random( 40,80 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( BladeSpiritsMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( DispelFieldMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( IncognitoMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MagicReflectionMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MindBlastMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ParalyzeMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PoisonFieldMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( SummonCreatureMagicStaff ), Utility.Random( 50,100 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( DispelMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyBoltMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ExplosionMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( InvisibilityMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MarkMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MassCurseMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ParalyzeFieldMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( RevealMagicStaff ), Utility.Random( 60,120 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ChainLightningMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyFieldMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FlameStrikeMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( GateTravelMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ManaVampireMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MassDispelMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( MeteorSwarmMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( PolymorphMagicStaff ), Utility.Random( 70,140 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( AirElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EarthElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EarthquakeMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( EnergyVortexMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( FireElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( ResurrectionMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( SummonDaemonMagicStaff ), Utility.Random( 80,160 ) ); } 
				if ( MyServerSettings.BuyRareChance() ){ Add( typeof( WaterElementalMagicStaff ), Utility.Random( 80,160 ) ); } 
			}
		}
	}

/////----------------------------------------------------------------------------------------------------------------------------------------------------/////

	public class SBTinkerGuild: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBTinkerGuild() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{
				Add( new GenericBuyInfo( typeof( GuildTinkering ), 500, Utility.Random( 1,5 ), 0x1EBB, 0x430 ) );
				Add( new GenericBuyInfo( typeof( Clock ), 22, Utility.Random( 50,200 ), 0x104B, 0 ) );
				Add( new GenericBuyInfo( typeof( Nails ), 3, Utility.Random( 50,200 ), 0x102E, 0 ) );
				Add( new GenericBuyInfo( typeof( ClockParts ), 3, Utility.Random( 50,200 ), 0x104F, 0 ) );
				Add( new GenericBuyInfo( typeof( AxleGears ), 3, Utility.Random( 50,200 ), 0x1051, 0 ) );
				Add( new GenericBuyInfo( typeof( Gears ), 2, Utility.Random( 50,200 ), 0x1053, 0 ) );
				Add( new GenericBuyInfo( typeof( Hinge ), 2, Utility.Random( 50,200 ), 0x1055, 0 ) );
				Add( new GenericBuyInfo( typeof( Sextant ), 13, Utility.Random( 50,200 ), 0x1057, 0 ) );
				Add( new GenericBuyInfo( typeof( SextantParts ), 5, Utility.Random( 50,200 ), 0x1059, 0 ) );
				Add( new GenericBuyInfo( typeof( Axle ), 2, Utility.Random( 50,200 ), 0x105B, 0 ) );
				Add( new GenericBuyInfo( typeof( Springs ), 3, Utility.Random( 50,200 ), 0x105D, 0 ) );
				Add( new GenericBuyInfo( "1024111", typeof( Key ), 8, Utility.Random( 50,200 ), 0x100F, 0 ) );
				Add( new GenericBuyInfo( "1024112", typeof( Key ), 8, Utility.Random( 50,200 ), 0x1010, 0 ) );
				Add( new GenericBuyInfo( "1024115", typeof( Key ), 8, Utility.Random( 50,200 ), 0x1013, 0 ) );
				Add( new GenericBuyInfo( typeof( KeyRing ), 8, Utility.Random( 50,200 ), 0x1010, 0 ) );
				Add( new GenericBuyInfo( typeof( Lockpick ), 12, Utility.Random( 50,200 ), 0x14FC, 0 ) );
				Add( new GenericBuyInfo( typeof( SkeletonsKey ), Utility.Random( 150,250 ), 20, 0x410A, 0 ) );
				Add( new GenericBuyInfo( typeof( TinkersTools ), 7, Utility.Random( 50,200 ), 0x1EBC, 0 ) );
				Add( new GenericBuyInfo( typeof( Board ), 3, Utility.Random( 50,200 ), 0x1BD7, 0 ) );
				Add( new GenericBuyInfo( typeof( IronIngot ), 5, Utility.Random( 50,200 ), 0x1BF2, 0 ) );
				Add( new GenericBuyInfo( typeof( SewingKit ), 3, Utility.Random( 50,200 ), 0x4C81, 0 ) );
				Add( new GenericBuyInfo( typeof( DrawKnife ), 10, Utility.Random( 50,200 ), 0x10E4, 0 ) );
				Add( new GenericBuyInfo( typeof( Froe ), 10, Utility.Random( 50,200 ), 0x10E5, 0 ) );
				Add( new GenericBuyInfo( typeof( Scorp ), 10, Utility.Random( 50,200 ), 0x10E7, 0 ) );
				Add( new GenericBuyInfo( typeof( Inshave ), 10, Utility.Random( 50,200 ), 0x10E6, 0 ) );
				Add( new GenericBuyInfo( typeof( ButcherKnife ), 13, Utility.Random( 50,200 ), 0x13F6, 0 ) );
				Add( new GenericBuyInfo( typeof( Scissors ), 11, Utility.Random( 50,200 ), 0xF9F, 0 ) );
				Add( new GenericBuyInfo( typeof( Tongs ), 13, Utility.Random( 50,200 ), 0xFBB, 0 ) );
				Add( new GenericBuyInfo( typeof( DovetailSaw ), 12, Utility.Random( 50,200 ), 0x1028, 0 ) );
				Add( new GenericBuyInfo( typeof( Saw ), 100, Utility.Random( 50,200 ), 0x1034, 0 ) );
				Add( new GenericBuyInfo( typeof( Hammer ), 17, Utility.Random( 50,200 ), 0x102A, 0 ) );
				Add( new GenericBuyInfo( typeof( SmithHammer ), 23, Utility.Random( 50,200 ), 0x0FB4, 0 ) );
				Add( new GenericBuyInfo( typeof( Shovel ), 12, Utility.Random( 50,200 ), 0xF39, 0 ) );
				Add( new GenericBuyInfo( typeof( OreShovel ), 10, Utility.Random( 50,200 ), 0xF39, 0x96D ) );
				Add( new GenericBuyInfo( typeof( MouldingPlane ), 11, Utility.Random( 50,200 ), 0x102C, 0 ) );
				Add( new GenericBuyInfo( typeof( JointingPlane ), 10, Utility.Random( 50,200 ), 0x1030, 0 ) );
				Add( new GenericBuyInfo( typeof( SmoothingPlane ), 11, Utility.Random( 50,200 ), 0x1032, 0 ) );
				Add( new GenericBuyInfo( typeof( WoodworkingTools ), 10, Utility.Random( 10,50 ), 0x4F52, 0 ) );
				Add( new GenericBuyInfo( typeof( Pickaxe ), 25, Utility.Random( 50,200 ), 0xE86, 0 ) );
				Add( new GenericBuyInfo( typeof( ThrowingWeapon ), 2, Utility.Random( 20, 120 ), 0x52B2, 0 ) );
				Add( new GenericBuyInfo( typeof( light_wall_torch ), 50, Utility.Random( 50,200 ), 0xA07, 0 ) );
				Add( new GenericBuyInfo( typeof( light_dragon_brazier ), 750, Utility.Random( 50,200 ), 0x194E, 0 ) );
				Add( new GenericBuyInfo( typeof( TrapKit ), 420, Utility.Random( 1,3 ), 0x1EBB, 0 ) );
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PianoAddonDeed ), Utility.Random( 95000,190000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PoorMiningHarvester ),12000, Utility.Random( 1,2 ), 0x5484, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( StandardMiningHarvester ), 70000, Utility.Random( 1,2 ), 0x5484, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( GoodMiningHarvester ), 200000, Utility.Random( 1,2 ), 0x5484, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PoorLumberHarvester ), 12000, Utility.Random( 1,2 ), 0x5486, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( StandardLumberHarvester ), 70000, Utility.Random( 1,2 ), 0x5486, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( PoorHideHarvester ), 12000, Utility.Random( 1,2 ), 0x5487, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( StandardHideHarvester ), 70000, Utility.Random( 1,2 ), 0x5487, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HarvesterRepairKit ), 6500, Utility.Random( 1,2 ), 0x4C2C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CiderBarrel ), 17000, Utility.Random( 1,4 ), 0x3DB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( AleBarrel ), 17000, Utility.Random( 1,4 ), 0x3DB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( LiquorBarrel ), 17000, Utility.Random( 1,4 ), 0x3DB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( CheesePress ), 17000, Utility.Random( 1,4 ), 0x3DB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DeviceKit), 900, Utility.Random( 1,2 ), 0x4F86, 0 ) ); }
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{
				Add( typeof( LootChest ), 600 );
				Add( typeof( Shovel ), 6 );
				Add( typeof( OreShovel ), 5 );
				Add( typeof( SewingKit ), 1 );
				Add( typeof( Scissors ), 6 );
				Add( typeof( Tongs ), 7 );
				Add( typeof( Key ), 1 );
				Add( typeof( DovetailSaw ), 6 );
				Add( typeof( MouldingPlane ), 6 );
				Add( typeof( Nails ), 1 );
				Add( typeof( JointingPlane ), 6 );
				Add( typeof( SmoothingPlane ), 6 );
				Add( typeof( Saw ), 7 );
				Add( typeof( Clock ), 11 );
				Add( typeof( ClockParts ), 1 );
				Add( typeof( AxleGears ), 1 );
				Add( typeof( Gears ), 1 );
				Add( typeof( Hinge ), 1 );
				Add( typeof( Sextant ), 6 );
				Add( typeof( SextantParts ), 2 );
				Add( typeof( Axle ), 1 );
				Add( typeof( Springs ), 1 );
				Add( typeof( DrawKnife ), 5 );
				Add( typeof( Froe ), 5 );
				Add( typeof( Inshave ), 5 );
				Add( typeof( WoodworkingTools ), 5 );
				Add( typeof( Scorp ), 5 );
				Add( typeof( Lockpick ), 6 );
				Add( typeof( SkeletonsKey ), 10 );
				Add( typeof( TinkerTools ), 3 );
				Add( typeof( Board ), 1 );
				Add( typeof( Log ), 1 );
				Add( typeof( Pickaxe ), 16 );
				Add( typeof( Hammer ), 3 );
				Add( typeof( SmithHammer ), 11 );
				Add( typeof( ButcherKnife ), 6 );
				Add( typeof( CrystalScales ), Utility.Random( 300,600 ) );
				Add( typeof( GolemManual ), Utility.Random( 500,750 ) );
				Add( typeof( PowerCrystal ), 100 );
				Add( typeof( ArcaneGem ), 10 );
				Add( typeof( ClockworkAssembly ), 100 );
				Add( typeof( BottleOil ), 5 );
				Add( typeof( ThrowingWeapon ), 1 );
				Add( typeof( TrapKit ), 210 );
				Add( typeof( SpaceJunkA ), Utility.Random( 5, 10 ) );
				Add( typeof( SpaceJunkB ), Utility.Random( 10, 20 ) );
				Add( typeof( SpaceJunkC ), Utility.Random( 15, 30 ) );
				Add( typeof( SpaceJunkD ), Utility.Random( 20, 40 ) );
				Add( typeof( SpaceJunkE ), Utility.Random( 25, 50 ) );
				Add( typeof( SpaceJunkF ), Utility.Random( 30, 60 ) );
				Add( typeof( SpaceJunkG ), Utility.Random( 35, 70 ) );
				Add( typeof( SpaceJunkH ), Utility.Random( 40, 80 ) );
				Add( typeof( SpaceJunkI ), Utility.Random( 45, 90 ) );
				Add( typeof( SpaceJunkJ ), Utility.Random( 50, 100 ) );
				Add( typeof( SpaceJunkK ), Utility.Random( 55, 110 ) );
				Add( typeof( SpaceJunkL ), Utility.Random( 60, 120 ) );
				Add( typeof( SpaceJunkM ), Utility.Random( 65, 130 ) );
				Add( typeof( SpaceJunkN ), Utility.Random( 70, 140 ) );
				Add( typeof( SpaceJunkO ), Utility.Random( 75, 150 ) );
				Add( typeof( SpaceJunkP ), Utility.Random( 80, 160 ) );
				Add( typeof( SpaceJunkQ ), Utility.Random( 85, 170 ) );
				Add( typeof( SpaceJunkR ), Utility.Random( 90, 180 ) );
				Add( typeof( SpaceJunkS ), Utility.Random( 95, 190 ) );
				Add( typeof( SpaceJunkT ), Utility.Random( 100, 200 ) );
				Add( typeof( SpaceJunkU ), Utility.Random( 105, 210 ) );
				Add( typeof( SpaceJunkV ), Utility.Random( 110, 220 ) );
				Add( typeof( SpaceJunkW ), Utility.Random( 115, 230 ) );
				Add( typeof( SpaceJunkX ), Utility.Random( 120, 240 ) );
				Add( typeof( SpaceJunkY ), Utility.Random( 125, 250 ) );
				Add( typeof( SpaceJunkZ ), Utility.Random( 130, 260 ) );
				Add( typeof( LandmineSetup ), Utility.Random( 100, 300 ) );
				Add( typeof( PlasmaGrenade ), Utility.Random( 28, 38 ) );
				Add( typeof( ThermalDetonator ), Utility.Random( 28, 38 ) );
				Add( typeof( PuzzleCube ), Utility.Random( 45, 90 ) );
				Add( typeof( PlasmaTorch ), Utility.Random( 45, 90 ) );
				Add( typeof( DuctTape ), Utility.Random( 45, 90 ) );
				Add( typeof( RobotBatteries ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotSheetMetal ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotOil ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotGears ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotEngineParts ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotCircuitBoard ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotBolt ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotTransistor ), Utility.Random( 5, 100 ) );
				Add( typeof( RobotSchematics ), Utility.Random( 500,750 ) );
				Add( typeof( DataPad ), Utility.Random( 5, 150 ) );
				Add( typeof( MaterialLiquifier ), Utility.Random( 100, 300 ) );
				Add( typeof( Chainsaw ), Utility.Random( 130, 260 ) );
				Add( typeof( PortableSmelter ), Utility.Random( 130, 260 ) );
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBThiefGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBThiefGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Backpack ), 100, Utility.Random( 50,200 ), 0x53D5, 0 ) );
				Add( new GenericBuyInfo( typeof( Pouch ), 6, Utility.Random( 50,200 ), 0xE79, 0 ) );
				Add( new GenericBuyInfo( typeof( Torch ), 8, Utility.Random( 50,200 ), 0xF6B, 0 ) );
				Add( new GenericBuyInfo( typeof( Lantern ), 2, Utility.Random( 50,200 ), 0xA25, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnStealingBook ), 5, Utility.Random( 50,200 ), 0x4C5C, 0 ) );
				Add( new GenericBuyInfo( typeof( Lockpick ), 12, Utility.Random( 50,500 ), 0x14FC, 0 ) );
				Add( new GenericBuyInfo( typeof( SkeletonsKey ), Utility.Random( 50,200 ), 25, 0x410A, 0 ) );
				Add( new GenericBuyInfo( typeof( MasterSkeletonsKey ), Utility.Random( 500,2000 ), 10, 0x410A, 0 ) );
				Add( new GenericBuyInfo( typeof( WoodenBox ), 14, Utility.Random( 50,200 ), 0x9AA, 0 ) );
				Add( new GenericBuyInfo( typeof( Key ), 2, Utility.Random( 50,200 ), 0x100E, 0 ) );
				Add( new GenericBuyInfo( "1041060", typeof( HairDye ), 100, Utility.Random( 1,100 ), 0xEFF, 0 ) );
				Add( new GenericBuyInfo( "hair dye bottle", typeof( HairDyeBottle ), 1000, Utility.Random( 1,100 ), 0xE0F, 0 ) );
				Add( new GenericBuyInfo( typeof( DisguiseKit ), 700, Utility.Random( 1,5 ), 0xE05, 0 ) );
				Add( new GenericBuyInfo( typeof( CorruptedMoonStone ), 2500, 5, 0xF8B, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Backpack ), 7 );
				Add( typeof( Pouch ), 3 );
				Add( typeof( Torch ), 3 );
				Add( typeof( Lantern ), 1 );
				Add( typeof( Lockpick ), 6 );
				Add( typeof( WoodenBox ), 7 );
				Add( typeof( HairDye ), 50 );
				Add( typeof( HairDyeBottle ), 300 );
				Add( typeof( SkeletonsKey ), 10 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBTailorGuild: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBTailorGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( GuildSewing ), 500, Utility.Random( 1,5 ), 0x4C81, 0x430 ) );
				Add( new GenericBuyInfo( typeof( SewingKit ), 3, Utility.Random( 3,31 ), 0x4C81, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Scissors ), 11, Utility.Random( 3,31 ), 0xF9F, 0 ) );
				Add( new GenericBuyInfo( typeof( DyeTub ), 8, Utility.Random( 3,31 ), 0xFAB, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Dyes ), 8, Utility.Random( 3,31 ), 0xFA9, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Shirt ), 12, Utility.Random( 3,31 ), 0x1517, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( ShortPants ), 7, Utility.Random( 3,31 ), 0x152E, 0 ) );
				Add( new GenericBuyInfo( typeof( FancyShirt ), 21, Utility.Random( 3,31 ), 0x1EFD, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( LongPants ), 10, Utility.Random( 3,31 ), 0x1539, 0 ) );
				Add( new GenericBuyInfo( typeof( FancyDress ), 26, Utility.Random( 3,31 ), 0x1EFF, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( PlainDress ), 13, Utility.Random( 3,31 ), 0x1F01, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( Kilt ), 11, Utility.Random( 3,31 ), 0x1537, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( HalfApron ), 10, Utility.Random( 3,31 ), 0x153b, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( LoinCloth ), 10, Utility.Random( 3,31 ), 0x2B68, 637 ) );
				Add( new GenericBuyInfo( typeof( RoyalLoinCloth ), 10, Utility.Random( 3,31 ), 0x55DB, 637 ) );
				Add( new GenericBuyInfo( typeof( Robe ), 18, Utility.Random( 3,31 ), 0x1F03, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( JokerRobe ), 40, Utility.Random( 1,5 ), 0x2B6B, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( AssassinRobe ), 40, Utility.Random( 1,5 ), 0x2B69, 0 ) );
				Add( new GenericBuyInfo( typeof( FancyRobe ), 40, Utility.Random( 1,5 ), 0x2B6A, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( GildedRobe ), 40, Utility.Random( 1,5 ), 0x2B6C, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( OrnateRobe ), 40, Utility.Random( 1,5 ), 0x2B6E, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( MagistrateRobe ), 40, Utility.Random( 1,5 ), 0x2B70, 0 ) );
				Add( new GenericBuyInfo( typeof( RoyalRobe ), 40, Utility.Random( 1,5 ), 0x2B73, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( SorcererRobe ), 40, Utility.Random( 1,5 ), 0x3175, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( AssassinShroud ), 40, Utility.Random( 1,5 ), 0x2FB9, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( NecromancerRobe ), 40, Utility.Random( 1,5 ), 0x2FBA, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( SpiderRobe ), 40, Utility.Random( 1,5 ), 0x2FC6, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( VagabondRobe ), 40, Utility.Random( 1,5 ), 0x567D, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( PirateCoat ), 40, Utility.Random( 1,5 ), 0x567E, 0 ) );
				Add( new GenericBuyInfo( typeof( ExquisiteRobe ), 40, Utility.Random( 1,5 ), 0x283, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( ProphetRobe ), 40, Utility.Random( 1,5 ), 0x284, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( ElegantRobe ), 40, Utility.Random( 1,5 ), 0x285, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( FormalRobe ), 40, Utility.Random( 1,5 ), 0x286, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( ArchmageRobe ), 40, Utility.Random( 1,5 ), 0x287, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( PriestRobe ), 40, Utility.Random( 1,5 ), 0x288, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( CultistRobe ), 40, Utility.Random( 1,5 ), 0x289, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( GildedDarkRobe ), 40, Utility.Random( 1,5 ), 0x28A, 0 ) );
				Add( new GenericBuyInfo( typeof( GildedLightRobe ), 40, Utility.Random( 1,5 ), 0x301, 0 ) );
				Add( new GenericBuyInfo( typeof( SageRobe ), 40, Utility.Random( 1,5 ), 0x302, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( Cloak ), 8, Utility.Random( 3,31 ), 0x1515, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( Doublet ), 13, Utility.Random( 3,31 ), 0x1F7B, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( Tunic ), 18, Utility.Random( 3,31 ), 0x1FA1, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( JesterSuit ), 26, Utility.Random( 3,31 ), 0x1F9F, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( JesterHat ), 12, Utility.Random( 3,31 ), 0x171C, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( FloppyHat ), 7, Utility.Random( 3,31 ), 0x1713, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( WideBrimHat ), 8, Utility.Random( 3,31 ), 0x1714, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( Cap ), 10, Utility.Random( 3,31 ), 0x1715, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( TallStrawHat ), 8, Utility.Random( 3,31 ), 0x1716, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( StrawHat ), 7, Utility.Random( 3,31 ), 0x1717, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( WizardsHat ), 11, Utility.Random( 3,31 ), 0x1718, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( WitchHat ), 11, Utility.Random( 1,15 ), 0x2FC3, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( LeatherCap ), 10, Utility.Random( 3,31 ), 0x1DB9, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( FeatheredHat ), 10, Utility.Random( 3,31 ), 0x171A, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( TricorneHat ), 8, Utility.Random( 3,31 ), 0x171B, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( PirateHat ), 8, Utility.Random( 3,31 ), 0x2FBC, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( Bandana ), 6, Utility.Random( 3,31 ), 0x1540, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( SkullCap ), 7, Utility.Random( 3,31 ), 0x1544, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( ClothHood ), 12, Utility.Random( 1,15 ), 0x2B71, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( ClothCowl ), 12, Utility.Random( 1,15 ), 0x3176, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( WizardHood ), 12, Utility.Random( 1,15 ), 0x310, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( FancyHood ), 12, Utility.Random( 1,15 ), 0x4D09, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( BoltOfCloth ), 100, Utility.Random( 3,31 ), 0xf95, Utility.RandomDyedHue() ) ); 
				Add( new GenericBuyInfo( typeof( Cloth ), 2, Utility.Random( 3,31 ), 0x1766, Utility.RandomDyedHue() ) ); 
				Add( new GenericBuyInfo( typeof( UncutCloth ), 2, Utility.Random( 3,31 ), 0x1767, Utility.RandomDyedHue() ) ); 
				Add( new GenericBuyInfo( typeof( Cotton ), 102, Utility.Random( 3,31 ), 0xDF9, 0 ) );
				Add( new GenericBuyInfo( typeof( Wool ), 62, Utility.Random( 3,31 ), 0xDF8, 0 ) );
				Add( new GenericBuyInfo( typeof( Flax ), 102, Utility.Random( 3,31 ), 0x1A9C, 0 ) );
				Add( new GenericBuyInfo( typeof( SpoolOfThread ), 18, Utility.Random( 3,31 ), 0x543A, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( FemaleMannequinDeed ), Utility.Random( 5000,10000 ), 1, 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MaleMannequinDeed ), Utility.Random( 5000,10000 ), 1, 0x14F0, 0 ) ); }
				
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Scissors ), 6 );
				Add( typeof( SewingKit ), 1 );
				Add( typeof( Dyes ), 4 );
				Add( typeof( DyeTub ), 4 );
				Add( typeof( BoltOfCloth ), 35 );
				Add( typeof( FancyShirt ), 10 );
				Add( typeof( Shirt ), 6 );
				Add( typeof( ShortPants ), 3 );
				Add( typeof( LongPants ), 5 );
				Add( typeof( Cloak ), 4 );
				Add( typeof( FancyDress ), 12 );
				Add( typeof( Robe ), 9 );
				Add( typeof( PlainDress ), 7 );
				Add( typeof( Skirt ), 5 );
				Add( typeof( Kilt ), 5 );
				Add( typeof( Doublet ), 7 );
				Add( typeof( Tunic ), 9 );
				Add( typeof( JesterSuit ), 13 );
				Add( typeof( FullApron ), 5 );
				Add( typeof( HalfApron ), 5 );
				Add( typeof( LoinCloth ), 5 );
				Add( typeof( JesterHat ), 6 );
				Add( typeof( FloppyHat ), 3 );
				Add( typeof( WideBrimHat ), 4 );
				Add( typeof( Cap ), 5 );
				Add( typeof( SkullCap ), 3 );
				Add( typeof( ClothCowl ), 6 );
				Add( typeof( ClothHood ), 6 );
				Add( typeof( WizardHood ), 6 );
				Add( typeof( FancyHood ), 6 );
				Add( typeof( Bandana ), 3 );
				Add( typeof( TallStrawHat ), 4 );
				Add( typeof( StrawHat ), 4 );
				Add( typeof( WizardsHat ), 5 );
				Add( typeof( WitchHat ), 5 );
				Add( typeof( Bonnet ), 4 );
				Add( typeof( FeatheredHat ), 5 );
				Add( typeof( TricorneHat ), 4 );
				Add( typeof( PirateHat ), 4 );
				Add( typeof( SpoolOfThread ), 9 );
				Add( typeof( Flax ), 51 );
				Add( typeof( Cotton ), 51 );
				Add( typeof( Wool ), 31 );
				Add( typeof( MagicRobe ), 30 ); 
				Add( typeof( MagicHat ), 20 ); 
				Add( typeof( MagicCloak ), 30 ); 
				Add( typeof( MagicBelt ), 20 ); 
				Add( typeof( MagicSash ), 20 ); 
				Add( typeof( JokerRobe ), 19 );
				Add( typeof( AssassinRobe ), 19 );
				Add( typeof( FancyRobe ), 19 );
				Add( typeof( GildedRobe ), 19 );
				Add( typeof( OrnateRobe ), 19 );
				Add( typeof( MagistrateRobe ), 19 );
				Add( typeof( VagabondRobe ), 19 );
				Add( typeof( PirateCoat ), 19 );
				Add( typeof( RoyalRobe ), 19 );
				Add( typeof( SorcererRobe ), 19 );
				Add( typeof( AssassinShroud ), 29 );
				Add( typeof( NecromancerRobe ), 19 );
				Add( typeof( SpiderRobe ), 19 );
				Add( typeof( ExquisiteRobe ), 19 );
				Add( typeof( ProphetRobe ), 19 );
				Add( typeof( ElegantRobe ), 19 );
				Add( typeof( FormalRobe ), 19 );
				Add( typeof( ArchmageRobe ), 19 );
				Add( typeof( PriestRobe ), 19 );
				Add( typeof( CultistRobe ), 19 );
				Add( typeof( GildedDarkRobe ), 19 );
				Add( typeof( GildedLightRobe ), 19 );
				Add( typeof( SageRobe ), 19 );
				Add( typeof( MagicScissors ), Utility.Random( 300,400 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMinerGuild: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMinerGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Bag ), 6, Utility.Random( 50,200 ), 0xE76, 0xABE ) );
				Add( new GenericBuyInfo( typeof( Candle ), 6, Utility.Random( 50,200 ), 0xA28, 0 ) );
				Add( new GenericBuyInfo( typeof( Torch ), 8, Utility.Random( 50,200 ), 0xF6B, 0 ) );
				Add( new GenericBuyInfo( typeof( Lantern ), 2, Utility.Random( 50,200 ), 0xA25, 0 ) );
				Add( new GenericBuyInfo( typeof( Pickaxe ), 25, Utility.Random( 50,200 ), 0xE86, 0 ) );
				Add( new GenericBuyInfo( typeof( Shovel ), 12, Utility.Random( 50,200 ), 0xF39, 0 ) );
				Add( new GenericBuyInfo( typeof( OreShovel ), 10, Utility.Random( 50,200 ), 0xF39, 0x96D ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Pickaxe ), 12 );
				Add( typeof( Shovel ), 6 );
				Add( typeof( OreShovel ), 5 );
				Add( typeof( Lantern ), 1 );
				Add( typeof( Torch ), 3 );
				Add( typeof( Bag ), 3 );
				Add( typeof( Candle ), 3 );
				Add( typeof( IronIngot ), 4 ); 
				Add( typeof( DullCopperIngot ), 8 ); 
				Add( typeof( ShadowIronIngot ), 12 ); 
				Add( typeof( CopperIngot ), 16 ); 
				Add( typeof( BronzeIngot ), 20 ); 
				Add( typeof( GoldIngot ), 24 ); 
				Add( typeof( AgapiteIngot ), 28 ); 
				Add( typeof( VeriteIngot ), 32 ); 
				Add( typeof( ValoriteIngot ), 36 ); 
				Add( typeof( NepturiteIngot ), 36 ); 
				Add( typeof( ObsidianIngot ), 36 ); 
				Add( typeof( SteelIngot ), 40 ); 
				Add( typeof( BrassIngot ), 44 ); 
				Add( typeof( MithrilIngot ), 48 ); 
				Add( typeof( XormiteIngot ), 48 ); 
				Add( typeof( DwarvenIngot ), 96 ); 
				Add( typeof( Amber ), 25 );
				Add( typeof( Amethyst ), 50 );
				Add( typeof( Citrine ), 25 );
				Add( typeof( Diamond ), 100 );
				Add( typeof( Emerald ), 50 );
				Add( typeof( Ruby ), 37 );
				Add( typeof( Sapphire ), 50 );
				Add( typeof( StarSapphire ), 62 );
				Add( typeof( Tourmaline ), 47 );
				if ( MyServerSettings.BuyChance() ){Add( typeof( RareAnvil ), Utility.Random( 200,1000 ) ); } 
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBMageGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMageGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Spellbook ), 18, Utility.Random( 50,200 ), 0xEFA, 0 ) );
				Add( new GenericBuyInfo( typeof( ScribesPen ), 8, Utility.Random( 50,200 ), 0x2051, 0 ) );
				Add( new GenericBuyInfo( typeof( BlankScroll ), 5, Utility.Random( 50,200 ), 0x0E34, 0 ) );
				Add( new GenericBuyInfo( "1041072", typeof( MagicWizardsHat ), 11, Utility.Random( 50,200 ), 0x1718, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( WitchHat ), 11, Utility.Random( 1,100 ), 0x2FC3, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( RecallRune ), 100, Utility.Random( 50,200 ), 0x1F14, 0 ) );
				Add( new GenericBuyInfo( typeof( BlackPearl ), 5, Utility.Random( 250,1000 ), 0x266F, 0 ) );
				Add( new GenericBuyInfo( typeof( Bloodmoss ), 5, Utility.Random( 250,1000 ), 0xF7B, 0 ) );
				Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 250,1000 ), 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 250,1000 ), 0xF85, 0 ) );
				Add( new GenericBuyInfo( typeof( MandrakeRoot ), 3, Utility.Random( 250,1000 ), 0xF86, 0 ) );
				Add( new GenericBuyInfo( typeof( Nightshade ), 3, Utility.Random( 250,1000 ), 0xF88, 0 ) );
				Add( new GenericBuyInfo( typeof( SpidersSilk ), 3, Utility.Random( 250,1000 ), 0xF8D, 0 ) );
				Add( new GenericBuyInfo( typeof( SulfurousAsh ), 3, Utility.Random( 250,1000 ), 0xF8C, 0 ) );
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( reagents_magic_jar1 ), 2000, Utility.Random( 50,200 ), 0x1007, 0 ) ); }
				Add( new GenericBuyInfo( typeof( WizardStaff ), 40, Utility.Random( 1,5 ), 0x0908, 0xB3A ) );
				Add( new GenericBuyInfo( typeof( WizardStick ), 38, Utility.Random( 1,5 ), 0xDF2, 0xB3A ) );
				Add( new GenericBuyInfo( typeof( MageEye ), 2, Utility.Random( 10,150 ), 0xF19, 0xB78 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "Stable Gates", typeof( LinkedGateBag ), 250000, 1, 0xE76, 0xABE ) ); }


				Type[] types = Loot.RegularScrollTypes;

				int circles = 4;

				for ( int i = 0; i < circles*8 && i < types.Length; ++i )
				{
					int itemID = 0x1F2E + i;

					if ( i == 6 )
						itemID = 0x1F2D;
					else if ( i > 6 )
						--itemID;

					Add( new GenericBuyInfo( types[i], 12 + ((i / 8) * 10), 20, itemID, 0 ) );
				}
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( MagicTalisman ), Utility.Random( 50,100 ) ); 
				Add( typeof( WizardsHat ), 100 );
				Add( typeof( WitchHat ), 5 );
				Add( typeof( BlackPearl ), 3 ); 
				Add( typeof( Bloodmoss ),4 ); 
				Add( typeof( MandrakeRoot ), 2 ); 
				Add( typeof( Garlic ), 2 ); 
				Add( typeof( Ginseng ), 2 ); 
				Add( typeof( Nightshade ), 2 ); 
				Add( typeof( SpidersSilk ), 2 ); 
				Add( typeof( SulfurousAsh ), 1 ); 
				Add( typeof( RecallRune ), 13 );
				Add( typeof( Spellbook ), 25 );
				Add( typeof( MysticalPearl ), 250 );

				Type[] types = Loot.RegularScrollTypes;

				for ( int i = 0; i < types.Length; ++i )
					Add(types[i], i + 3 + (i / 4));
			
				Add( typeof( ClumsyMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( CreateFoodMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( FeebleMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( HealMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( MagicArrowMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( NightSightMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( ReactiveArmorMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( WeaknessMagicStaff ), Utility.Random( 10,20 ) );
				Add( typeof( AgilityMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( CunningMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( CureMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( HarmMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( MagicTrapMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( MagicUntrapMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( ProtectionMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( StrengthMagicStaff ), Utility.Random( 20,40 ) );
				Add( typeof( BlessMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( FireballMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( MagicLockMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( MagicUnlockMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( PoisonMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( TelekinesisMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( TeleportMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( WallofStoneMagicStaff ), Utility.Random( 30,60 ) );
				Add( typeof( ArchCureMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( ArchProtectionMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( CurseMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( FireFieldMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( GreaterHealMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( LightningMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( ManaDrainMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( RecallMagicStaff ), Utility.Random( 40,80 ) );
				Add( typeof( BladeSpiritsMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( DispelFieldMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( IncognitoMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( MagicReflectionMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( MindBlastMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( ParalyzeMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( PoisonFieldMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( SummonCreatureMagicStaff ), Utility.Random( 50,100 ) );
				Add( typeof( DispelMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( EnergyBoltMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( ExplosionMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( InvisibilityMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( MarkMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( MassCurseMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( ParalyzeFieldMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( RevealMagicStaff ), Utility.Random( 60,120 ) );
				Add( typeof( ChainLightningMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( EnergyFieldMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( FlameStrikeMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( GateTravelMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( ManaVampireMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( MassDispelMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( MeteorSwarmMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( PolymorphMagicStaff ), Utility.Random( 70,140 ) );
				Add( typeof( AirElementalMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( EarthElementalMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( EarthquakeMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( EnergyVortexMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( FireElementalMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( ResurrectionMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( SummonDaemonMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( WaterElementalMagicStaff ), Utility.Random( 80,160 ) );
				Add( typeof( MySpellbook ), 500 );
				Add( typeof( TomeOfWands ), Utility.Random( 100,400 ) );
				Add( typeof( DemonPrison ), Utility.Random( 500,1000 ) );
				Add( typeof( WizardStaff ), 20 );
				Add( typeof( WizardStick ), 19 );
				Add( typeof( MageEye ), 1 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBHealerGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHealerGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Bandage ), 2, Utility.Random( 250,1000 ), 0xE21, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserHealPotion ), 100, Utility.Random( 50,200 ), 0x25FD, 0 ) );
				Add( new GenericBuyInfo( typeof( HealPotion ), 30, Utility.Random( 50,200 ), 0xF0C, 0 ) );
				Add( new GenericBuyInfo( typeof( GreaterHealPotion ), 60, Utility.Random( 50,200 ), 0x25FE, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 50,200 ), 0xF85, 0 ) );
				Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 50,200 ), 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( RefreshPotion ), 100, Utility.Random( 50,200 ), 0xF0B, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( LesserHealPotion ), 7 );
				Add( typeof( HealPotion ), 14 );
				Add( typeof( GreaterHealPotion ), 28 );
				Add( typeof( RefreshPotion ), 7 );
				Add( typeof( Garlic ), 2 );
				Add( typeof( Ginseng ), 2 );
				Add( typeof( FirstAidKit ), Utility.Random( 100,250 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBSailorGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSailorGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "1041205", typeof( SmallBoatDeed ), 9500, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) );
				Add( new GenericBuyInfo( "1041206", typeof( SmallDragonBoatDeed ), 10500, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) );
				Add( new GenericBuyInfo( "1041207", typeof( MediumBoatDeed ), 11500, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) );
				Add( new GenericBuyInfo( "1041208", typeof( MediumDragonBoatDeed ), 12500, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) );
				Add( new GenericBuyInfo( "1041209", typeof( LargeBoatDeed ), 13500, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) );
				Add( new GenericBuyInfo( "1041210", typeof( LargeDragonBoatDeed ), 14500, Utility.Random( 1,100 ), 0x14F3, 0x5BE ) );
				Add( new GenericBuyInfo( typeof( DockingLantern ), 58, Utility.Random( 50,200 ), 0x40FF, 0 ) );
				Add( new GenericBuyInfo( typeof( RawFishSteak ), 10, Utility.Random( 50,200 ), 0x97A, 0 ) );
				Add( new GenericBuyInfo( typeof( Fish ), 30, Utility.Random( 50,200 ), 0x9CC, 0 ) );
				Add( new GenericBuyInfo( typeof( Fish ), 30, Utility.Random( 50,200 ), 0x9CD, 0 ) );
				Add( new GenericBuyInfo( typeof( Fish ), 30, Utility.Random( 50,200 ), 0x9CE, 0 ) );
				Add( new GenericBuyInfo( typeof( Fish ), 30, Utility.Random( 50,200 ), 0x9CF, 0 ) );
				Add( new GenericBuyInfo( typeof( FishingPole ), 100, Utility.Random( 50,200 ), 0xDC0, 0 ) );
				Add( new GenericBuyInfo( typeof( BoatStain ), 26, Utility.Random( 1,100 ), 0x14E0, 0 ) );
				Add( new GenericBuyInfo( typeof( Sextant ), 13, Utility.Random( 1,100 ), 0x1057, 0 ) );
				Add( new GenericBuyInfo( typeof( GrapplingHook ), 58, Utility.Random( 1,100 ), 0x4F40, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( SeaShell ), 58 );
				Add( typeof( DockingLantern ), 29 );
				Add( typeof( RawFishSteak ), 5 );
				Add( typeof( Fish ), 3 );
				Add( typeof( FishingPole ), 7 );
				Add( typeof( Sextant ), 6 );
				Add( typeof( GrapplingHook ), 29 );
				Add( typeof( PirateChest ), Utility.RandomMinMax( 200, 800 ) );
				Add( typeof( SunkenChest ), Utility.RandomMinMax( 200, 800 ) );
				Add( typeof( FishingNet ), Utility.RandomMinMax( 20, 40 ) );
				Add( typeof( SpecialFishingNet ), Utility.RandomMinMax( 60, 80 ) );
				Add( typeof( FabledFishingNet ), Utility.RandomMinMax( 100, 120 ) );
				Add( typeof( NeptunesFishingNet ), Utility.RandomMinMax( 140, 160 ) );
				Add( typeof( PrizedFish ), Utility.RandomMinMax( 60, 120 ) );
				Add( typeof( WondrousFish ), Utility.RandomMinMax( 60, 120 ) );
				Add( typeof( TrulyRareFish ), Utility.RandomMinMax( 60, 120 ) );
				Add( typeof( PeculiarFish ), Utility.RandomMinMax( 60, 120 ) );
				Add( typeof( SpecialSeaweed ), Utility.RandomMinMax( 40, 160 ) );
				Add( typeof( SunkenBag ), Utility.RandomMinMax( 100, 500 ) );
				Add( typeof( ShipwreckedItem ), Utility.RandomMinMax( 20, 60 ) );
				Add( typeof( HighSeasRelic ), Utility.RandomMinMax( 20, 60 ) );
				Add( typeof( BoatStain ), 13 );
				Add( typeof( MegalodonTooth ), Utility.RandomMinMax( 1000, 2000 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBBlacksmithGuild : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBBlacksmithGuild() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{
				Add( new GenericBuyInfo( typeof( GuildHammer ), 500, Utility.Random( 1,5 ), 0xFB5, 0x430 ) );
				Add( new GenericBuyInfo( typeof( IronIngot ), 5, Utility.Random( 10,1000 ), 0x1BF2, 0 ) );
				Add( new GenericBuyInfo( typeof( Tongs ), 13, Utility.Random( 50,200 ), 0xFBB, 0 ) ); 
				Add( new GenericBuyInfo( typeof( BronzeShield ), 66, Utility.Random( 50,200 ), 0x1B72, 0 ) );
				Add( new GenericBuyInfo( typeof( Buckler ), 50, Utility.Random( 50,200 ), 0x1B73, 0 ) );
				Add( new GenericBuyInfo( typeof( MetalKiteShield ), 123, Utility.Random( 50,200 ), 0x1B74, 0 ) );
				Add( new GenericBuyInfo( typeof( HeaterShield ), 231, Utility.Random( 50,200 ), 0x1B76, 0 ) );
				Add( new GenericBuyInfo( typeof( MetalShield ), 121, Utility.Random( 50,200 ), 0x1B7B, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateGorget ), 104, Utility.Random( 50,200 ), 0x1413, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateChest ), 243, Utility.Random( 50,200 ), 0x1415, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateLegs ), 218, Utility.Random( 50,200 ), 0x1411, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateArms ), 188, Utility.Random( 50,200 ), 0x1410, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateGloves ), 155, Utility.Random( 50,200 ), 0x1414, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateHelm ), 21, Utility.Random( 50,200 ), 0x1412, 0 ) );
				Add( new GenericBuyInfo( typeof( CloseHelm ), 18, Utility.Random( 50,200 ), 0x1408, 0 ) );
				Add( new GenericBuyInfo( typeof( CloseHelm ), 18, Utility.Random( 50,200 ), 0x1409, 0 ) );
				Add( new GenericBuyInfo( typeof( Helmet ), 31, Utility.Random( 50,200 ), 0x140A, 0 ) );
				Add( new GenericBuyInfo( typeof( Helmet ), 18, Utility.Random( 50,200 ), 0x140B, 0 ) );
				Add( new GenericBuyInfo( typeof( NorseHelm ), 18, Utility.Random( 50,200 ), 0x140E, 0 ) );
				Add( new GenericBuyInfo( typeof( NorseHelm ), 18, Utility.Random( 50,200 ), 0x140F, 0 ) );
				Add( new GenericBuyInfo( typeof( Bascinet ), 18, Utility.Random( 50,200 ), 0x140C, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateHelm ), 21, Utility.Random( 50,200 ), 0x1419, 0 ) );
				Add( new GenericBuyInfo( typeof( DreadHelm ), 21, Utility.Random( 50,200 ), 0x2FBB, 0 ) );
				Add( new GenericBuyInfo( typeof( ChainCoif ), 17, Utility.Random( 50,200 ), 0x13BB, 0 ) );
				Add( new GenericBuyInfo( typeof( ChainChest ), 143, Utility.Random( 50,200 ), 0x13BF, 0 ) );
				Add( new GenericBuyInfo( typeof( ChainLegs ), 149, Utility.Random( 50,200 ), 0x13BE, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailChest ), 121, Utility.Random( 50,200 ), 0x13ec, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailLegs ), 90, Utility.Random( 50,200 ), 0x13F0, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailArms ), 85, Utility.Random( 50,200 ), 0x13EE, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailGloves ), 93, Utility.Random( 50,200 ), 0x13eb, 0 ) );
				Add( new GenericBuyInfo( typeof( ExecutionersAxe ), 30, Utility.Random( 50,200 ), 0xF45, 0 ) );
				Add( new GenericBuyInfo( typeof( Bardiche ), 60, Utility.Random( 50,200 ), 0xF4D, 0 ) );
				Add( new GenericBuyInfo( typeof( BattleAxe ), 26, Utility.Random( 50,200 ), 0xF47, 0 ) );
				Add( new GenericBuyInfo( typeof( TwoHandedAxe ), 32, Utility.Random( 50,200 ), 0x1443, 0 ) );
				Add( new GenericBuyInfo( typeof( ButcherKnife ), 14, Utility.Random( 50,200 ), 0x13F6, 0 ) );
				Add( new GenericBuyInfo( typeof( Cutlass ), 24, Utility.Random( 50,200 ), 0x1441, 0 ) );
				Add( new GenericBuyInfo( typeof( Dagger ), 21, Utility.Random( 50,200 ), 0xF52, 0 ) );
				Add( new GenericBuyInfo( typeof( Halberd ), 42, Utility.Random( 50,200 ), 0x143E, 0 ) );
				Add( new GenericBuyInfo( typeof( HammerPick ), 26, Utility.Random( 50,200 ), 0x143D, 0 ) );
				Add( new GenericBuyInfo( typeof( Katana ), 33, Utility.Random( 50,200 ), 0x13FF, 0 ) );
				Add( new GenericBuyInfo( typeof( Kryss ), 32, Utility.Random( 50,200 ), 0x1401, 0 ) );
				Add( new GenericBuyInfo( typeof( Broadsword ), 35, Utility.Random( 50,200 ), 0xF5E, 0 ) );
				Add( new GenericBuyInfo( typeof( Longsword ), 55, Utility.Random( 50,200 ), 0xF61, 0 ) );
				Add( new GenericBuyInfo( typeof( ThinLongsword ), 27, Utility.Random( 50,200 ), 0x13B8, 0 ) );
				Add( new GenericBuyInfo( typeof( VikingSword ), 55, Utility.Random( 50,200 ), 0x13B9, 0 ) );
				Add( new GenericBuyInfo( typeof( Cleaver ), 100, Utility.Random( 50,200 ), 0xEC3, 0 ) );
				Add( new GenericBuyInfo( typeof( Axe ), 40, Utility.Random( 50,200 ), 0xF49, 0 ) );
				Add( new GenericBuyInfo( typeof( DoubleAxe ), 52, Utility.Random( 50,200 ), 0xF4B, 0 ) );
				Add( new GenericBuyInfo( typeof( Pickaxe ), 22, Utility.Random( 50,200 ), 0xE86, 0 ) );
				Add( new GenericBuyInfo( typeof( Pitchfork ), 19, Utility.Random( 50,200 ), 0xE87, 0xB3A ) );
				Add( new GenericBuyInfo( typeof( Scimitar ), 36, Utility.Random( 50,200 ), 0x13B6, 0 ) );
				Add( new GenericBuyInfo( typeof( SkinningKnife ), 14, Utility.Random( 50,200 ), 0xEC4, 0 ) );
				Add( new GenericBuyInfo( typeof( LargeBattleAxe ), 33, Utility.Random( 50,200 ), 0x13FB, 0 ) );
				Add( new GenericBuyInfo( typeof( WarAxe ), 29, Utility.Random( 50,200 ), 0x13B0, 0 ) );
				Add( new GenericBuyInfo( typeof( BoneHarvester ), 35, Utility.Random( 50,200 ), 0x26BB, 0 ) );
				Add( new GenericBuyInfo( typeof( CrescentBlade ), 37, Utility.Random( 50,200 ), 0x26C1, 0 ) );
				Add( new GenericBuyInfo( typeof( DoubleBladedStaff ), 35, Utility.Random( 50,200 ), 0x26BF, 0 ) );
				Add( new GenericBuyInfo( typeof( Lance ), 34, Utility.Random( 50,200 ), 0x26C0, 0 ) );
				Add( new GenericBuyInfo( typeof( Pike ), 39, Utility.Random( 50,200 ), 0x26BE, 0 ) );
				Add( new GenericBuyInfo( typeof( Scythe ), 39, Utility.Random( 50,200 ), 0x26BA, 0 ) );
				Add( new GenericBuyInfo( typeof( Mace ), 28, Utility.Random( 50,200 ), 0xF5C, 0 ) );
				Add( new GenericBuyInfo( typeof( Maul ), 21, Utility.Random( 50,200 ), 0x143B, 0 ) );
				Add( new GenericBuyInfo( typeof( SmithHammer ), 21, Utility.Random( 50,200 ), 0x0FB4, 0 ) );
				Add( new GenericBuyInfo( typeof( ShortSpear ), 23, Utility.Random( 50,200 ), 0x1403, 0 ) );
				Add( new GenericBuyInfo( typeof( Spear ), 31, Utility.Random( 50,200 ), 0xF62, 0 ) );
				Add( new GenericBuyInfo( typeof( WarHammer ), 25, Utility.Random( 50,200 ), 0x1439, 0 ) );
				Add( new GenericBuyInfo( typeof( WarMace ), 31, Utility.Random( 50,200 ), 0x1407, 0 ) );
				Add( new GenericBuyInfo( typeof( Scepter ), 39, Utility.Random( 50,200 ), 0x26BC, 0 ) );
				Add( new GenericBuyInfo( typeof( BladedStaff ), 40, Utility.Random( 50,200 ), 0x26BD, 0 ) );
				Add( new GenericBuyInfo( typeof( GuardsmanShield ), 231, Utility.Random( 1,100 ), 0x2FCB, 0 ) );
				Add( new GenericBuyInfo( typeof( ElvenShield ), 231, Utility.Random( 1,100 ), 0x2FCA, 0 ) );
				Add( new GenericBuyInfo( typeof( DarkShield ), 231, Utility.Random( 1,100 ), 0x2FC8, 0 ) );
				Add( new GenericBuyInfo( typeof( CrestedShield ), 231, Utility.Random( 1,100 ), 0x2FC9, 0 ) );
				Add( new GenericBuyInfo( typeof( ChampionShield ), 231, Utility.Random( 1,100 ), 0x2B74, 0 ) );
				Add( new GenericBuyInfo( typeof( JeweledShield ), 231, Utility.Random( 1,100 ), 0x2B75, 0 ) );
				Add( new GenericBuyInfo( typeof( AssassinSpike ), 21, Utility.Random( 1,100 ), 0x2D21, 0 ) );
				Add( new GenericBuyInfo( typeof( Leafblade ), 21, Utility.Random( 1,100 ), 0x2D22, 0 ) );
				Add( new GenericBuyInfo( typeof( WarCleaver ), 25, Utility.Random( 1,100 ), 0x2D2F, 0 ) );
				Add( new GenericBuyInfo( typeof( DiamondMace ), 31, Utility.Random( 1,100 ), 0x2D24, 0 ) );
				Add( new GenericBuyInfo( typeof( OrnateAxe ), 42, Utility.Random( 1,100 ), 0x2D28, 0 ) );
				Add( new GenericBuyInfo( typeof( RuneBlade ), 55, Utility.Random( 1,100 ), 0x2D32, 0 ) );
				Add( new GenericBuyInfo( typeof( RadiantScimitar ), 35, Utility.Random( 1,100 ), 0x2D33, 0 ) );
				Add( new GenericBuyInfo( typeof( ElvenSpellblade ), 33, Utility.Random( 1,100 ), 0x2D20, 0 ) );
				Add( new GenericBuyInfo( typeof( ElvenMachete ), 35, Utility.Random( 1,100 ), 0x2D35, 0 ) );
				Add( new GenericBuyInfo( typeof( RandomLetter ), 150, Utility.Random( 25,100 ), 0x55BF, 0 ) ); 

			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{
				Add( typeof( GuardsmanShield ), 115 );
				Add( typeof( ElvenShield ), 115 );
				Add( typeof( DarkShield ), 115 );
				Add( typeof( CrestedShield ), 115 );
				Add( typeof( ChampionShield ), 115 );
				Add( typeof( JeweledShield ), 115 );
				Add( typeof( TopazIngot ), 120 ); 
				Add( typeof( ShinySilverIngot ), 120 ); 
				Add( typeof( AmethystIngot ), 120 ); 
				Add( typeof( EmeraldIngot ), 120 ); 
				Add( typeof( GarnetIngot ), 120 ); 
				Add( typeof( IceIngot ), 120 ); 
				Add( typeof( JadeIngot ), 120 ); 
				Add( typeof( MarbleIngot ), 120 ); 
				Add( typeof( OnyxIngot ), 120 ); 
				Add( typeof( QuartzIngot ), 120 ); 
				Add( typeof( RubyIngot ), 120 ); 
				Add( typeof( SapphireIngot ), 120 ); 
				Add( typeof( SpinelIngot ), 120 ); 
				Add( typeof( StarRubyIngot ), 120 ); 
				Add( typeof( Tongs ), 7 ); 
				Add( typeof( IronIngot ), 4 ); 
				Add( typeof( DullCopperIngot ), 8 ); 
				Add( typeof( ShadowIronIngot ), 12 ); 
				Add( typeof( CopperIngot ), 16 ); 
				Add( typeof( BronzeIngot ), 20 ); 
				Add( typeof( GoldIngot ), 24 ); 
				Add( typeof( AgapiteIngot ), 28 ); 
				Add( typeof( VeriteIngot ), 32 ); 
				Add( typeof( ValoriteIngot ), 36 ); 
				Add( typeof( NepturiteIngot ), 36 ); 
				Add( typeof( SteelIngot ), 40 ); 
				Add( typeof( BrassIngot ), 44 ); 
				Add( typeof( MithrilIngot ), 48 ); 
				Add( typeof( XormiteIngot ), 48 ); 
				Add( typeof( DwarvenIngot ), 96 ); 
				Add( typeof( ObsidianIngot ), 36 ); 
				Add( typeof( Buckler ), 25 );
				Add( typeof( BronzeShield ), 33 );
				Add( typeof( MetalShield ), 60 );
				Add( typeof( MetalKiteShield ), 62 );
				Add( typeof( HeaterShield ), 115 );
				Add( typeof( PlateArms ), 94 );
				Add( typeof( PlateChest ), 121 );
				Add( typeof( PlateGloves ), 72 );
				Add( typeof( PlateGorget ), 52 );
				Add( typeof( PlateLegs ), 109 );
				Add( typeof( PlateSkirt ), 109 );
				Add( typeof( FemalePlateChest ), 113 );
				Add( typeof( Bascinet ), 9 );
				Add( typeof( CloseHelm ), 9 );
				Add( typeof( Helmet ), 9 );
				Add( typeof( NorseHelm ), 9 );
				Add( typeof( PlateHelm ), 10 );
				Add( typeof( DreadHelm ), 10 );
				Add( typeof( ChainCoif ), 6 );
				Add( typeof( ChainChest ), 71 );
				Add( typeof( ChainLegs ), 74 );
				Add( typeof( ChainSkirt ), 74 );
				Add( typeof( RingmailArms ), 42 );
				Add( typeof( RingmailChest ), 60 );
				Add( typeof( RingmailGloves ), 26 );
				Add( typeof( RingmailLegs ), 45 );
				Add( typeof( RingmailSkirt ), 45 );
				Add( typeof( BattleAxe ), 13 );
				Add( typeof( DoubleAxe ), 26 );
				Add( typeof( ExecutionersAxe ), 10 );
				Add( typeof( LargeBattleAxe ),16 );
				Add( typeof( Pickaxe ), 11 );
				Add( typeof( TwoHandedAxe ), 16 );
				Add( typeof( WarAxe ), 14 );
				Add( typeof( Axe ), 20 );
				Add( typeof( Bardiche ), 30 );
				Add( typeof( Halberd ), 21 );
				Add( typeof( ButcherKnife ), 7 );
				Add( typeof( Cleaver ), 7 );
				Add( typeof( Dagger ), 10 );
				Add( typeof( SkinningKnife ), 7 );
				Add( typeof( HammerPick ), 13 );
				Add( typeof( Mace ), 14 );
				Add( typeof( Maul ), 10 );
				Add( typeof( WarHammer ), 12 );
				Add( typeof( WarMace ), 10 );
				Add( typeof( Scepter ), 20 );
				Add( typeof( BladedStaff ), 20 );
				Add( typeof( Scythe ), 19 );
				Add( typeof( BoneHarvester ), 17 );
				Add( typeof( Scepter ), 18 );
				Add( typeof( BladedStaff ), 16 );
				Add( typeof( Pike ), 19 );
				Add( typeof( DoubleBladedStaff ), 17 );
				Add( typeof( Lance ), 17 );
				Add( typeof( CrescentBlade ), 18 );
				Add( typeof( Spear ), 10 );
				Add( typeof( Pitchfork ), 9 );
				Add( typeof( ShortSpear ), 11 );
				Add( typeof( SmithHammer ), 10 );
				Add( typeof( Broadsword ), 17 );
				Add( typeof( Cutlass ), 12 );
				Add( typeof( Katana ), 16 );
				Add( typeof( Kryss ), 16 );
				Add( typeof( Longsword ), 27 );
				Add( typeof( Scimitar ), 18 );
				Add( typeof( ThinLongsword ), 13 );
				Add( typeof( VikingSword ), 27 );
				Add( typeof( AssassinSpike ), 10 );
				Add( typeof( Leafblade ), 10 );
				Add( typeof( WarCleaver ), 12 );
				Add( typeof( DiamondMace ), 10 );
				Add( typeof( OrnateAxe ),21 );
				Add( typeof( RuneBlade ), 27 );
				Add( typeof( RadiantScimitar ), 17 );
				Add( typeof( ElvenSpellblade ), 16 );
				Add( typeof( ElvenMachete ), 17 );
				Add( typeof( RareAnvil ), Utility.Random( 400,1500 ) );
				Add( typeof( MagicHammer ), Utility.Random( 300,400 ) );
			} 
		} 
	} 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBBardGuild: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBBardGuild() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{
				Add( new GenericBuyInfo( typeof( Drums ), 21, Utility.Random( 50,200 ), 0x0E9C, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Tambourine ), 21, Utility.Random( 50,200 ), 0x0E9E, 0 ) ); 
				Add( new GenericBuyInfo( typeof( LapHarp ), 21, Utility.Random( 50,200 ), 0x0EB2, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Lute ), 21, Utility.Random( 50,200 ), 0x0EB3, 0 ) ); 
				Add( new GenericBuyInfo( typeof( BambooFlute ), 21, Utility.Random( 1,100 ), 0x2805, 0 ) );
				Add( new GenericBuyInfo( typeof( SongBook ), 24, Utility.Random( 1,5 ), 0x225A, 0 ) ); 
				Add( new GenericBuyInfo( typeof( EnergyCarolScroll ), 32, Utility.Random( 1,5 ), 0x1F48, 0x96 ) ); 
				Add( new GenericBuyInfo( typeof( FireCarolScroll ), 32, Utility.Random( 1,5 ), 0x1F49, 0x96 ) ); 
				Add( new GenericBuyInfo( typeof( IceCarolScroll ), 32, Utility.Random( 1,5 ), 0x1F34, 0x96 ) ); 
				Add( new GenericBuyInfo( typeof( KnightsMinneScroll ), 32, Utility.Random( 1,5 ), 0x1F31, 0x96 ) ); 
				Add( new GenericBuyInfo( typeof( PoisonCarolScroll ), 32, Utility.Random( 1,5 ), 0x1F33, 0x96 ) ); 
				Add( new GenericBuyInfo( typeof( JarsOfWaxInstrument ), 160, Utility.Random( 1,5 ), 0x1005, 0x845 ) );
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{
				Add( typeof( JarsOfWaxInstrument ), 80 );
				Add( typeof( BambooFlute ), 10 );
				Add( typeof( LapHarp ), 10 ); 
				Add( typeof( Lute ), 10 ); 
				Add( typeof( Drums ), 10 ); 
				Add( typeof( Harp ), 10 ); 
				Add( typeof( Tambourine ), 10 );
				Add( typeof( MySongbook ), 200 );
				Add( typeof( SongBook ), 12 ); 
				Add( typeof( EnergyCarolScroll ), 5 ); 
				Add( typeof( FireCarolScroll ), 5 ); 
				Add( typeof( IceCarolScroll ), 5 ); 
				Add( typeof( KnightsMinneScroll ), 5 ); 
				Add( typeof( PoisonCarolScroll ), 5 ); 
				Add( typeof( ArmysPaeonScroll ), 6 ); 
				Add( typeof( MagesBalladScroll ), 6 ); 
				Add( typeof( EnchantingEtudeScroll ), 7 ); 
				Add( typeof( SheepfoeMamboScroll ), 7 ); 
				Add( typeof( SinewyEtudeScroll ), 7 ); 
				Add( typeof( EnergyThrenodyScroll ), 8 ); 
				Add( typeof( FireThrenodyScroll ), 8 ); 
				Add( typeof( IceThrenodyScroll ), 8 ); 
				Add( typeof( PoisonThrenodyScroll ), 8 ); 
				Add( typeof( FoeRequiemScroll ), 9 ); 
				Add( typeof( MagicFinaleScroll ), 10 ); 
			} 
		} 
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBHolidayXmas : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHolidayXmas()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( WreathDeed ), 30000, Utility.Random( 1,3 ), 0x14EF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreenStocking ), 11000, Utility.Random( 1,3 ), 0x2bd9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( RedStocking ), 11000, Utility.Random( 1,3 ), 0x2bdb, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( SnowPileDeco ), 8000, Utility.Random( 1,3 ), 0x8E2, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( Snowman ), 23000, Utility.Random( 1,3 ), 0x2328, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BlueSnowflake ), 10000, Utility.Random( 1,3 ), 0x232E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( WhiteSnowflake ), 10000, Utility.Random( 1,3 ), 0x232F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( RedPoinsettia ), 12000, Utility.Random( 1,3 ), 0x2330, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( WhitePoinsettia ), 12000, Utility.Random( 1,3 ), 0x2331, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( IcyPatch ), 6000, Utility.Random( 1,3 ), 0x122F, 0x481 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( IcicleLargeSouth ), 8000, Utility.Random( 1,3 ), 0x4572, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( IcicleMedSouth ), 7000, Utility.Random( 1,3 ), 0x4573, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( IcicleSmallSouth ), 6000, Utility.Random( 1,3 ), 0x4574, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( IcicleLargeEast ), 8000, Utility.Random( 1,3 ), 0x4575, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( IcicleMedEast ), 7000, Utility.Random( 1,3 ), 0x4576, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( IcicleSmallEast ), 6000, Utility.Random( 1,3 ), 0x4577, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GingerBreadHouseDeed ), 45000, Utility.Random( 1,3 ), 0x14EF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CandyCane ), 2000, Utility.Random( 1,3 ), 0x2bdd, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GingerBreadCookie ), 2000, Utility.Random( 1,3 ), 0x2be1, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HolidayBell ), 28000, Utility.Random( 1,3 ), 0x1C12, 0xA ) ); }
			if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HolidayBells ), 15000, Utility.Random( 1,3 ), 0x544F, 0 ) ); }

				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GiftBoxRectangle ), 14000, Utility.Random( 1,3 ), 0x46A6, Utility.RandomList( 0x672, 0x454, 0x507, 0x4ac, 0x504, 0x84b, 0x495, 0x97c, 0x493, 0x4a8, 0x494, 0x4aa, 0xb8b, 0x84f, 0x491, 0x851, 0x503, 0xb8c, 0x4ab, 0x84B ) ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GiftBoxCube ), 14000, Utility.Random( 1,3 ), 0x46A2, Utility.RandomList( 0x672, 0x454, 0x507, 0x4ac, 0x504, 0x84b, 0x495, 0x97c, 0x493, 0x4a8, 0x494, 0x4aa, 0xb8b, 0x84f, 0x491, 0x851, 0x503, 0xb8c, 0x4ab, 0x84B ) ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GiftBoxCylinder ), 14000, Utility.Random( 1,3 ), 0x46A3, Utility.RandomList( 0x672, 0x454, 0x507, 0x4ac, 0x504, 0x84b, 0x495, 0x97c, 0x493, 0x4a8, 0x494, 0x4aa, 0xb8b, 0x84f, 0x491, 0x851, 0x503, 0xb8c, 0x4ab, 0x84B ) ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GiftBoxOctogon ), 14000, Utility.Random( 1,3 ), 0x46A4, Utility.RandomList( 0x672, 0x454, 0x507, 0x4ac, 0x504, 0x84b, 0x495, 0x97c, 0x493, 0x4a8, 0x494, 0x4aa, 0xb8b, 0x84f, 0x491, 0x851, 0x503, 0xb8c, 0x4ab, 0x84B ) ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GiftBoxAngel ), 14000, Utility.Random( 1,3 ), 0x46A7, Utility.RandomList( 0x672, 0x454, 0x507, 0x4ac, 0x504, 0x84b, 0x495, 0x97c, 0x493, 0x4a8, 0x494, 0x4aa, 0xb8b, 0x84f, 0x491, 0x851, 0x503, 0xb8c, 0x4ab, 0x84B ) ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GiftBoxNeon ), 14000, Utility.Random( 1,3 ), 0x232A, Utility.RandomList( 0x438, 0x424, 0x433, 0x445, 0x42b, 0x448 ) ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GiftBox ), 14000, Utility.Random( 1,3 ), 0x232A, Utility.RandomDyedHue() ) ); }
				Add( new GenericBuyInfo( typeof( HolidayTreeDeed ), 8600, Utility.Random( 1,3 ), 0x0CC8, 0 ) );
				Add( new GenericBuyInfo( typeof( HolidayTreeFlatDeed ), 8600, Utility.Random( 1,3 ), 0x0CC8, 0 ) );
				Add( new GenericBuyInfo( typeof( NewHolidayTree ), 19800, Utility.Random( 1,3 ), 0x4C7D, 0 ) );
				Add( new GenericBuyInfo( typeof( RibbonTreeSmall ), 7000, Utility.Random( 1,3 ), 0x5448, 0 ) );
				Add( new GenericBuyInfo( typeof( RibbonTree ), 18000, Utility.Random( 1,3 ), 0x5447, 0 ) );
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ChristmasRobe ), 500, Utility.Random( 1,3 ), 0x2684, 1153 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBHolidayHalloween : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHolidayHalloween()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BloodyTableAddonDeed ), 17500, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BloodPentagramDeed ), 75000, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( MongbatDartBoardEastDeed ), 1200, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( MongbatDartBoardSouthDeed ), 1200, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenTree6 ), 10800, Utility.Random( 1,3 ), 0xCCD, 0x2C1 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenTree5 ), 10800, Utility.Random( 1,3 ), 0x224D, 0x2C1 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenTree4 ), 10800, Utility.Random( 1,3 ), 0xCD3, 0x2C1 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenTree3 ), 10800, Utility.Random( 1,3 ), 0x224A, 0x2C5 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenTree2 ), 10800, Utility.Random( 1,3 ), 0xD94, 0x2C5 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenTree1 ), 10800, Utility.Random( 1,3 ), 0xCE3, 0x2C5 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenTortSkel ), 7450, Utility.Random( 1,3 ), 0x1A03, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenStoneSpike2 ), 7600, Utility.Random( 1,3 ), 0x2202, 0x322 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenStoneSpike ), 10600, Utility.Random( 1,3 ), 0x2201, 0x322 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenStoneColumn ), 10500, Utility.Random( 1,3 ), 0x77, 0x322 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenSkullPole ), 10540, Utility.Random( 1,3 ), 0x2204, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenShrineChaosDeed ), 200380, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenPylonFire ), 22100, Utility.Random( 1,3 ), 0x19AF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenPylon ), 11800, Utility.Random( 1,3 ), 0x1ECB, 0x322 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenMaiden ), 22780, Utility.Random( 1,3 ), 0x124B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenGrave3 ), 3350, Utility.Random( 1,3 ), 0xEDE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenGrave2 ), 3350, Utility.Random( 1,3 ), 0x116E, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenGrave1 ), 3350, Utility.Random( 1,3 ), 0x1168, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenColumn ), 8100, Utility.Random( 1,3 ), 0x196, 0x322 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenChopper ), 8760, Utility.Random( 1,3 ), 0x1245, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenBonePileDeed ), 5680, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenBlood ), 990, Utility.Random( 1,3 ), 0x122A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( AppleBobbingBarrel ), 570, Utility.Random( 1,3 ), 0x0F33, 0 ) ); }
				Add( new GenericBuyInfo( typeof( CarvedPumpkin ), 580, Utility.Random( 1,3 ), 0x4694, 0 ) );
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin2 ), 580, Utility.Random( 1,3 ), 0x4698, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin3 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5457, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin4 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x545B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin5 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x545F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin6 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5464, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin7 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5468, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin8 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x546C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin9 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5470, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin10 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5474, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin11 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5478, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin12 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x547C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin13 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5480, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin14 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5544, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin15 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5547, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin16 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5549, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin17 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x554D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin18 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5551, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin19 ), Utility.Random( 1500,5000 ), Utility.Random( 1,3 ), 0x5555, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CarvedPumpkin20 ), Utility.Random( 15000,50000 ), Utility.Random( 1,3 ), 0x5451, 0 ) ); }				
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( DeadBodyEWDeed ), 7345, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( DeadBodyNSDeed ), 7345, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( EvilFireplaceSouthFaceAddonDeed ), 66800, Utility.Random( 1,3 ), 0x14EF, 1175 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( EvilFireplaceEastFaceAddonDeed ), 66800, Utility.Random( 1,3 ), 0x14EF, 1175 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_coffin_eastAddonDeed ), 9470, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_coffin_southAddonDeed ), 9470, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_block_southAddonDeed ), 9430, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_block_eastAddonDeed ), 9430, Utility.Random( 1,3 ), 0x14EF, 0x96C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( LargeDyingPlant ), 225, Utility.Random( 1,3 ), 0x42B9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( DyingPlant ), 175, Utility.Random( 1,3 ), 0x42BA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( PumpkinScarecrow ), 2240, Utility.Random( 1,3 ), 0x469B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GrimWarning ), 3120, Utility.Random( 1,3 ), 0x42BD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( SkullsOnPike ), 3120, Utility.Random( 1,3 ), 0x42B5, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BlackCatStatue ), 3100, Utility.Random( 1,3 ), 0x4688, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( RuinedTapestry ), 15135, Utility.Random( 1,3 ), 0x4699, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HalloweenWeb ), 5185, Utility.Random( 1,3 ), 0xEE3, Utility.RandomList( 43, 1175, 1151 ) ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_shackles ), 125, Utility.Random( 1,3 ), 5696, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_ruined_bookcase ), 5340, Utility.Random( 1,3 ), 0x0C14, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_covered_chair ), 520, Utility.Random( 1,3 ), 3095, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_HauntedMirror1 ), 3270, Utility.Random( 1,3 ), 10875, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_HauntedMirror2 ), 3270, Utility.Random( 1,3 ), 10876, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( halloween_devil_face ), 350150, Utility.Random( 1,3 ), 4348, 0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){ Add( new GenericBuyInfo( typeof( PackedCostume ), 2530, Utility.Random( 1,3 ), 0x46A3, 0x5E0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){ Add( new GenericBuyInfo( typeof( WrappedCandy ), 120, Utility.Random( 1,3 ), 0x469E, 0 ) ); }
				if ( MyServerSettings.SellCommonChance() ){ Add( new GenericBuyInfo( typeof( HalloweenPack ), 2530, Utility.Random( 1,3 ), 0x46A3, 0x5E0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( NecromancerTable ), 15520, Utility.Random( 1,3 ), 0x149D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( NecromancerBanner ), 15350, Utility.Random( 1,3 ), 0x149B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BurningScarecrowA ), 5290, Utility.Random( 1,3 ), 0x23A9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GothicCandelabraA ), 5280, Utility.Random( 1,3 ), 0x052D, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBNecroGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBNecroGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BatWing ), 3, Utility.Random( 250,1000 ), 0xF78, 0 ) );
				Add( new GenericBuyInfo( typeof( DaemonBlood ), 6, Utility.Random( 250,1000 ), 0xF7D, 0 ) );
				Add( new GenericBuyInfo( typeof( PigIron ), 5, Utility.Random( 250,1000 ), 0xF8A, 0 ) );
				Add( new GenericBuyInfo( typeof( NoxCrystal ), 6, Utility.Random( 250,1000 ), 0xF8E, 0 ) );
				Add( new GenericBuyInfo( typeof( GraveDust ), 3, Utility.Random( 250,1000 ), 0xF8F, 0 ) );
				Add( new GenericBuyInfo( typeof( BloodOathScroll ), 25, Utility.Random( 50,200 ), 0x2263, 0 ) );
				Add( new GenericBuyInfo( typeof( CorpseSkinScroll ), 28, Utility.Random( 50,200 ), 0x2263, 0 ) );
				Add( new GenericBuyInfo( typeof( CurseWeaponScroll ), 12, Utility.Random( 50,200 ), 0x2263, 0 ) );
				Add( new GenericBuyInfo( typeof( PolishBoneBrush ), 12, 10, 0x1371, 0 ) );
				Add( new GenericBuyInfo( typeof( GraveShovel ), 12, Utility.Random( 50,200 ), 0xF39, 0x966 ) );
				Add( new GenericBuyInfo( typeof( SurgeonsKnife ), 14, Utility.Random( 50,200 ), 0xEC4, 0x1B0 ) );
				Add( new GenericBuyInfo( typeof( MixingCauldron ), 247, Utility.Random( 50,200 ), 0x269C, 0 ) );
				Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 50,200 ), 0x1E27, 0x979 ) );
				Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 15,30 ), 0x10B4, 0 ) );
				Add( new GenericBuyInfo( typeof( CBookNecroticAlchemy ), 50, Utility.Random( 50,200 ), 0x2253, 0x4AA ) );
				Add( new GenericBuyInfo( typeof( NecromancerSpellbook ), 115, Utility.Random( 50,200 ), 0x2253, 0 ) );
				Add( new GenericBuyInfo( typeof( TarotDeck ), 5, Utility.Random( 50,200 ), 0x12AB, 0 ) ); 
				Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) );
				Add( new GenericBuyInfo( typeof( AlchemyPouch ), Utility.Random( 2900,3500 ), Utility.Random( 1,2 ), 0x1C10, 0x89F ) );
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BlackDyeTub ), 5000, Utility.Random( 1,1 ), 0xFAB, 0x1 ) ); }
				Add( new GenericBuyInfo( typeof( reagents_magic_jar2 ), 1500, Utility.Random( 250,1000 ), 0x1007, 0xB97 ) );
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HellsGateScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x54F ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ManaLeechScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0xB87 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( NecroCurePoisonScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x8A2 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( NecroPoisonScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x4F8 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( NecroUnlockScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x493 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( PhantasmScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x6DE ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( RetchedAirScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0xA97 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( SpectreShadowScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x17E ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( UndeadEyesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x491 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( VampireGiftScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0xB85 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( WallOfSpikesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0xB8F ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BloodPactScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x5B5 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GhostlyImagesScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0xBF ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GhostPhaseScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x47E ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GraveyardGatewayScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x2EA ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HellsBrandScroll ), Utility.Random( 10,100 ), Utility.Random( 1,3 ), 0x1007, 0x54C ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ReaperHood ), 28, Utility.Random( 1,10 ), 0x4CDB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ReaperCowl ), 28, Utility.Random( 1,10 ), 0x4CDD, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( DeadMask ), 28, Utility.Random( 1,10 ), 0x405, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( NecromancerRobe ), 50, Utility.Random( 1,10 ), 0x2FBA, 0 ) ); }
				Add( new GenericBuyInfo( typeof( WizardStaff ), 40, Utility.Random( 1,5 ), 0x0908, 0xB3A ) );
				Add( new GenericBuyInfo( typeof( WizardStick ), 38, Utility.Random( 1,5 ), 0xDF2, 0xB3A ) );
				Add( new GenericBuyInfo( typeof( MageEye ), 2, Utility.Random( 10,150 ), 0xF19, 0xB78 ) );
				Add( new GenericBuyInfo( typeof( CorruptedMoonStone ), 1000, 5, 0xF8B, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BatWing ), 1 );
				Add( typeof( DaemonBlood ), 3 );
				Add( typeof( PigIron ), 2 );
				Add( typeof( NoxCrystal ), 3 );
				Add( typeof( GraveDust ), 1 );
				Add( typeof( NecromancerSpellbook ), 55 );
				Add( typeof( DeathKnightSpellbook ), Utility.Random( 100,300 ) );
				Add( typeof( ExorcismScroll ), 1 );
				Add( typeof( AnimateDeadScroll ), 26 );
				Add( typeof( BloodOathScroll ), 26 );
				Add( typeof( CorpseSkinScroll ), 26 );
				Add( typeof( CurseWeaponScroll ), 26 );
				Add( typeof( EvilOmenScroll ), 26 );
				Add( typeof( PainSpikeScroll ), 26 );
				Add( typeof( SummonFamiliarScroll ), 26 );
				Add( typeof( HorrificBeastScroll ), 27 );
				Add( typeof( MindRotScroll ), 39 );
				Add( typeof( PoisonStrikeScroll ), 39 );
				Add( typeof( WraithFormScroll ), 51 );
				Add( typeof( LichFormScroll ), 64 );
				Add( typeof( StrangleScroll ), 64 );
				Add( typeof( WitherScroll ), 64 );
				Add( typeof( VampiricEmbraceScroll ), 101 );
				Add( typeof( VengefulSpiritScroll ), 114 );
				Add( typeof( MixingCauldron ), 123 ); 
				Add( typeof( MixingSpoon ), 17 ); 
				Add( typeof( MyNecromancerSpellbook ), 500 );
				Add( typeof( BlackDyeTub ), 2500 ); 
				Add( typeof( PolishBoneBrush ), 6 );
				Add( typeof( PolishedSkull ), 3 );
				Add( typeof( PolishedBone ), 3 );
				Add( typeof( BoneContainer ), Utility.RandomMinMax( 100, 400 ) );
				Add( typeof( CorpseSailor ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BodyPart ), Utility.RandomMinMax( 30, 90 ) );
				Add( typeof( CorpseChest ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( BuriedBody ), Utility.RandomMinMax( 50, 300 ) );
				Add( typeof( LeftLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightLeg ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( TastyHeart ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( Head ), Utility.RandomMinMax( 10, 20 ) );
				Add( typeof( LeftArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RightArm ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Torso ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bone ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( RibCage ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( BonePile ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( Bones ), Utility.RandomMinMax( 5, 10 ) );
				Add( typeof( GraveChest ), Utility.RandomMinMax( 100, 500 ) );
				Add( typeof( SkullMinotaur ), Utility.Random( 50,150 ) );
				Add( typeof( SkullWyrm ), Utility.Random( 200,400 ) );
				Add( typeof( SkullGreatDragon ), Utility.Random( 300,600 ) );
				Add( typeof( SkullDragon ), Utility.Random( 100,300 ) );
				Add( typeof( SkullDemon ), Utility.Random( 100,300 ) );
				Add( typeof( SkullGiant ), Utility.Random( 100,300 ) );
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
				Add( typeof( WoodenCoffin ), 25 );
				Add( typeof( WoodenCasket ), 25 );
				Add( typeof( StoneCoffin ), 45 );
				Add( typeof( StoneCasket ), 45 );
				Add( typeof( DemonPrison ), Utility.Random( 500,1000 ) );
				Add( typeof( ReaperHood ), 11 );
				Add( typeof( ReaperCowl ), 11 );
				Add( typeof( DeadMask ), 11 );
				Add( typeof( NecromancerRobe ), 19 );
				if ( MyServerSettings.BuyChance() ){Add( typeof( DracolichSkull ), Utility.Random( 500,1000 ) ); } 
				Add( typeof( WizardStaff ), 20 );
				Add( typeof( WizardStick ), 19 );
				Add( typeof( MageEye ), 1 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBArcherGuild: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBArcherGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( GuildFletching ), 500, Utility.Random( 1,5 ), 0x1EB8, 0x430 ) );
				Add( new GenericBuyInfo( typeof( ArcherQuiver ), 32, Utility.Random( 1,5 ), 0x2B02, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyArrows100 ), 300, Utility.Random( 1,10 ), 0xF41, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyBolts100 ), 300, Utility.Random( 1,10 ), 0x1BFD, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyArrows1000 ), 3000, Utility.Random( 1,10 ), 0xF41, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyBolts1000 ), 3000, Utility.Random( 1,10 ), 0x1BFD, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( ArcherQuiver ), 16 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBAlchemistGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBAlchemistGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{  
				Add( new GenericBuyInfo( typeof( MortarPestle ), 8, Utility.Random( 50,200 ), 0x4CE9, 0 ) );
				Add( new GenericBuyInfo( typeof( MixingCauldron ), 247, Utility.Random( 50,200 ), 0x269C, 0 ) );
				Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 50,200 ), 0x1E27, 0x979 ) );
				Add( new GenericBuyInfo( typeof( CBookNecroticAlchemy ), 50, Utility.Random( 50,200 ), 0x2253, 0x4AA ) );
				Add( new GenericBuyInfo( typeof( AlchemicalElixirs ), 50, Utility.Random( 1,100 ), 0x2219, 0 ) );
				Add( new GenericBuyInfo( typeof( AlchemicalMixtures ), 50, Utility.Random( 1,100 ), 0x2223, 0 ) );
				Add( new GenericBuyInfo( typeof( BookOfPoisons ), 50, Utility.Random( 1,100 ), 0x2253, 0x4F8 ) );

				Add( new GenericBuyInfo( typeof( Bottle ), 5, Utility.Random( 50,200 ), 0xF0E, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Jar ), 5, Utility.Random( 15,30 ), 0x10B4, 0 ) );
				Add( new GenericBuyInfo( typeof( HeatingStand ), 2, Utility.Random( 1,100 ), 0x1849, 0 ) ); 

				Add( new GenericBuyInfo( typeof( BlackPearl ), 5, Utility.Random( 250,1000 ), 0x266F, 0 ) );
				Add( new GenericBuyInfo( typeof( Bloodmoss ), 5, Utility.Random( 250,1000 ), 0xF7B, 0 ) );
				Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 250,1000 ), 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 250,1000 ), 0xF85, 0 ) );
				Add( new GenericBuyInfo( typeof( MandrakeRoot ), 3, Utility.Random( 250,1000 ), 0xF86, 0 ) );
				Add( new GenericBuyInfo( typeof( Nightshade ), 3, Utility.Random( 250,1000 ), 0xF88, 0 ) );
				Add( new GenericBuyInfo( typeof( SpidersSilk ), 3, Utility.Random( 250,1000 ), 0xF8D, 0 ) );
				Add( new GenericBuyInfo( typeof( SulfurousAsh ), 3, Utility.Random( 250,1000 ), 0xF8C, 0 ) );

				Add( new GenericBuyInfo( typeof( Brimstone ), 6, Utility.Random( 250,1000 ), 0x2FD3, 0 ) );
				Add( new GenericBuyInfo( typeof( ButterflyWings ), 6, Utility.Random( 250,1000 ), 0x3002, 0 ) );
				Add( new GenericBuyInfo( typeof( EyeOfToad ), 6, Utility.Random( 250,1000 ), 0x2FDA, 0 ) );
				Add( new GenericBuyInfo( typeof( FairyEgg ), 6, Utility.Random( 250,1000 ), 0x2FDB, 0 ) );
				Add( new GenericBuyInfo( typeof( GargoyleEar ), 6, Utility.Random( 250,1000 ), 0x2FD9, 0 ) );
				Add( new GenericBuyInfo( typeof( BeetleShell ), 6, Utility.Random( 250,1000 ), 0x2FF8, 0 ) );
				Add( new GenericBuyInfo( typeof( MoonCrystal ), 6, Utility.Random( 250,1000 ), 0x3003, 0 ) );
				Add( new GenericBuyInfo( typeof( PixieSkull ), 6, Utility.Random( 250,1000 ), 0x2FE1, 0 ) );
				Add( new GenericBuyInfo( typeof( RedLotus ), 6, Utility.Random( 250,1000 ), 0x2FE8, 0 ) );
				Add( new GenericBuyInfo( typeof( SeaSalt ), 6, Utility.Random( 250,1000 ), 0x2FE9, 0 ) );
				Add( new GenericBuyInfo( typeof( SilverWidow ), 6, Utility.Random( 250,1000 ), 0x2FF7, 0 ) );
				Add( new GenericBuyInfo( typeof( SwampBerries ), 6, Utility.Random( 250,1000 ), 0x2FE0, 0 ) );

				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( reagents_magic_jar1 ), 2000, Utility.Random( 50,200 ), 0x1007, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){ Add( new GenericBuyInfo( typeof( reagents_magic_jar2 ), 1500, Utility.Random( 50,200 ), 0x1007, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( reagents_magic_jar3 ), 5000, Utility.Random( 250,1000 ), 0x1007, 0x488 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( BottleOfAcid ), 600, Utility.Random( 3,21 ), 0x180F, 1167 ) ); }


				Add( new GenericBuyInfo( typeof( RefreshPotion ), 100, Utility.Random( 10,25 ), 0xF0B, 0 ) );
				Add( new GenericBuyInfo( typeof( AgilityPotion ), 100, Utility.Random( 10,25 ), 0xF08, 0 ) );
				Add( new GenericBuyInfo( typeof( NightSightPotion ), 100, Utility.Random( 10,25 ), 0xF06, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserHealPotion ), 100, Utility.Random( 10,25 ), 0x25FD, 0 ) );
				Add( new GenericBuyInfo( typeof( StrengthPotion ), 100, Utility.Random( 10,25 ), 0xF09, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserPoisonPotion ), 100, Utility.Random( 10,25 ), 0x2600, 0 ) );
 				Add( new GenericBuyInfo( typeof( LesserCurePotion ), 100, Utility.Random( 10,25 ), 0x233B, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserExplosionPotion ), 21, Utility.Random( 10,25 ), 0x2407, 0 ) );

				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( HealPotion ), 30, Utility.Random( 1,100 ), 0xF0C, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( PoisonPotion ), 30, Utility.Random( 1,100 ), 0xF0A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CurePotion ), 30, Utility.Random( 1,100 ), 0xF07, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ExplosionPotion ), 42, Utility.Random( 1,100 ), 0xF0D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ConflagrationPotion ), 30, Utility.Random( 1,100 ), 0x180F, 0xAD8 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ConfusionBlastPotion ), 30, Utility.Random( 1,100 ), 0x180F, 0x48D ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( FrostbitePotion ), 30, Utility.Random( 1,100 ), 0x180F, 0xAF3 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( TotalRefreshPotion ), 30, Utility.Random( 1,100 ), 0x25FF, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterAgilityPotion ), 60, Utility.Random( 1,100 ), 0x256A, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterConflagrationPotion ), 60, Utility.Random( 1,100 ), 0x2406, 0xAD8 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterConfusionBlastPotion ), 60, Utility.Random( 1,100 ), 0x2406, 0x48D ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterCurePotion ), 60, Utility.Random( 1,100 ), 0x24EA, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterExplosionPotion ), 60, Utility.Random( 1,100 ), 0x2408, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterFrostbitePotion ), 60, Utility.Random( 1,100 ), 0x2406, 0xAF3 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterHealPotion ), 60, Utility.Random( 1,100 ), 0x25FE, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterPoisonPotion ), 60, Utility.Random( 1,100 ), 0x2601, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterStrengthPotion ), 60, Utility.Random( 1,100 ), 0x25F7, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( DeadlyPoisonPotion ), 60, Utility.Random( 1,100 ), 0x2669, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( LesserInvisibilityPotion ), 860, Utility.Random( 1,3 ), 0x23BD, 0x490 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( LesserManaPotion ), 860, Utility.Random( 1,3 ), 0x23BD, 0x48D ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( LesserRejuvenatePotion ), 860, Utility.Random( 1,3 ), 0x23BD, 0x48E ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( InvisibilityPotion ), 890, Utility.Random( 1,3 ), 0x180F, 0x490 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ManaPotion ), 890, Utility.Random( 1,3 ), 0x180F, 0x48D ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( RejuvenatePotion ), 890, Utility.Random( 1,3 ), 0x180F, 0x48E ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterInvisibilityPotion ), 8120, 1, 0x2406, 0x490 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterManaPotion ), 8120, 1, 0x2406, 0x48D ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreaterRejuvenatePotion ), 8120, 1, 0x2406, 0x48E ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( InvulnerabilityPotion ), 8300, 1, 0x180F, 0x48F ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( AutoResPotion ), 8600, 1, 0x0E0F, 0x494 ) ); }
				Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) );
				Add( new GenericBuyInfo( typeof( AlchemyPouch ), Utility.Random( 2900,3500 ), Utility.Random( 1,2 ), 0x1C10, 0x89F ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( SkullMinotaur ), Utility.Random( 50,150 ) );
				Add( typeof( SkullWyrm ), Utility.Random( 200,400 ) );
				Add( typeof( SkullGreatDragon ), Utility.Random( 300,600 ) );
				Add( typeof( SkullDragon ), Utility.Random( 100,300 ) );
				Add( typeof( SkullDemon ), Utility.Random( 100,300 ) );
				Add( typeof( SkullGiant ), Utility.Random( 100,300 ) );
				Add( typeof( CanopicJar ), Utility.Random( 50,300 ) );
				Add( typeof( MixingCauldron ), 123 );
				Add( typeof( MixingSpoon ), 17 );
				Add( typeof( DragonTooth ), 120 );
				Add( typeof( EnchantedSeaweed ), 120 );
				Add( typeof( GhostlyDust ), 120 );
				Add( typeof( GoldenSerpentVenom ), 120 );
				Add( typeof( LichDust ), 120 );
				Add( typeof( SilverSerpentVenom ), 120 );
				Add( typeof( UnicornHorn ), 120 );
				Add( typeof( DemigodBlood ), 120 );
				Add( typeof( DemonClaw ), 120 );
				Add( typeof( DragonBlood ), 120 );
				Add( typeof( BlackPearl ), 3 );
				Add( typeof( Bloodmoss ), 3 );
				Add( typeof( MandrakeRoot ), 2 );
				Add( typeof( Garlic ), 2 );
				Add( typeof( Ginseng ), 2 );
				Add( typeof( Nightshade ), 2 );
				Add( typeof( SpidersSilk ), 2 );
				Add( typeof( SulfurousAsh ), 1 );
				Add( typeof( Brimstone ), 3 );
				Add( typeof( ButterflyWings ), 3 );
				Add( typeof( EyeOfToad ), 3 );
				Add( typeof( FairyEgg ), 3 );
				Add( typeof( GargoyleEar ), 3 );
				Add( typeof( BeetleShell ), 3 );
				Add( typeof( MoonCrystal ), 3 );
				Add( typeof( PixieSkull ), 3 );
				Add( typeof( RedLotus ), 3 );
				Add( typeof( SeaSalt ), 3 );
				Add( typeof( SilverWidow ), 3 );
				Add( typeof( SwampBerries ), 3 );
				Add( typeof( Bottle ), 3 );
				Add( typeof( Jar ), 3 );
				Add( typeof( MortarPestle ), 4 );
				Add( typeof( AgilityPotion ), 7 );
				Add( typeof( AutoResPotion ), 94 );
				Add( typeof( BottleOfAcid ), 32 );
				Add( typeof( ConflagrationPotion ), 7 );
				Add( typeof( FrostbitePotion ), 7 );
				Add( typeof( ConfusionBlastPotion ), 7 );
				Add( typeof( CurePotion ), 14 );
				Add( typeof( DeadlyPoisonPotion ), 28 );
				Add( typeof( ExplosionPotion ), 14 );
				Add( typeof( GreaterAgilityPotion ), 28 );
				Add( typeof( GreaterConflagrationPotion ), 28 );
				Add( typeof( GreaterFrostbitePotion ), 28 );
				Add( typeof( GreaterConfusionBlastPotion ), 28 );
				Add( typeof( GreaterCurePotion ), 28 );
				Add( typeof( GreaterExplosionPotion ), 28 );
				Add( typeof( GreaterHealPotion ), 28 );
				Add( typeof( GreaterInvisibilityPotion ), 28 );
				Add( typeof( GreaterManaPotion ), 28 );
				Add( typeof( GreaterPoisonPotion ), 28 );
				Add( typeof( GreaterRejuvenatePotion ), 28 );
				Add( typeof( GreaterStrengthPotion ), 28 );
				Add( typeof( HealPotion ), 14 );
				Add( typeof( InvisibilityPotion ), 14 );
				Add( typeof( InvulnerabilityPotion ), 53 );
				Add( typeof( PotionOfWisdom ), Utility.Random( 250,500 ) );
				Add( typeof( PotionOfDexterity ), Utility.Random( 250,500 ) );
				Add( typeof( PotionOfMight ), Utility.Random( 250,500 ) );
				Add( typeof( LesserCurePotion ), 7 );
				Add( typeof( LesserExplosionPotion ), 7 );
				Add( typeof( LesserHealPotion ), 7 );
				Add( typeof( LesserInvisibilityPotion ), 7 );
				Add( typeof( LesserManaPotion ), 7 );
				Add( typeof( LesserPoisonPotion ), 7 );
				Add( typeof( LesserRejuvenatePotion ), 7 );
				Add( typeof( ManaPotion ), 14 );
				Add( typeof( NightSightPotion ), 14 );
				Add( typeof( PoisonPotion ), 14 );
				Add( typeof( RefreshPotion ), 14 );
				Add( typeof( RejuvenatePotion ), 28 );
				Add( typeof( StrengthPotion ), 14 );
				Add( typeof( TotalRefreshPotion ), 28 );
				Add( typeof( BottleOfParts ), Utility.Random( 10, 30 ) );
				Add( typeof( SpecialSeaweed ), Utility.Random( 20, 40 ) );
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBLibraryGuild: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBLibraryGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( GuildScribe ), 500, Utility.Random( 1,5 ), 0x2051, 0x430 ) );
				Add( new GenericBuyInfo( typeof( LoreGuidetoAdventure ), 5, Utility.Random( 5,100 ), 0x1C11, 0 ) );
				Add( new GenericBuyInfo( typeof( WeaponAbilityBook ), 5, Utility.Random( 1,100 ), 0x2254, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnLeatherBook ), 5, Utility.Random( 1,100 ), 0x4C60, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnMiscBook ), 5, Utility.Random( 1,100 ), 0x4C5D, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnMetalBook ), 5, Utility.Random( 1,100 ), 0x4C5B, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnWoodBook ), 5, Utility.Random( 1,100 ), 0x4C5E, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnReagentsBook ), 5, Utility.Random( 1,100 ), 0x4C5E, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnTailorBook ), 5, Utility.Random( 1,100 ), 0x4C5E, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnGraniteBook ), 5, Utility.Random( 1,100 ), 0x4C5C, 0 ) );
				Add( new GenericBuyInfo( typeof( LearnScalesBook ), 5, Utility.Random( 1,100 ), 0x4C60, 0 ) );
				Add( new GenericBuyInfo( typeof( CBookDruidicHerbalism ), 50, Utility.Random( 1,100 ), 0x2D50, 0 ) );
				Add( new GenericBuyInfo( typeof( CBookNecroticAlchemy ), 50, Utility.Random( 1,100 ), 0x2253, 0x4AA ) );
				Add( new GenericBuyInfo( typeof( AlchemicalElixirs ), 50, Utility.Random( 1,100 ), 0x2219, 0 ) );
				Add( new GenericBuyInfo( typeof( AlchemicalMixtures ), 50, Utility.Random( 1,100 ), 0x2223, 0 ) );
				Add( new GenericBuyInfo( typeof( BookOfPoisons ), 50, Utility.Random( 1,100 ), 0x2253, 0x4F8 ) );
				Add( new GenericBuyInfo( typeof( WorkShoppes ), 50, Utility.Random( 1,100 ), 0x2259, 0xB50 ) );
				Add( new GenericBuyInfo( typeof( LearnTitles ), 5, Utility.Random( 1,100 ), 0xFF2, 0 ) );
				Add( new GenericBuyInfo( typeof( ScribesPen ), 8, Utility.Random( 50,200 ), 0x2051, 0 ) );
				Add( new GenericBuyInfo( typeof( BlankScroll ), 5, Utility.Random( 250,1000 ), 0x0E34, 0 ) );
				Add( new GenericBuyInfo( typeof( Monocle ), 24, Utility.Random( 3,30 ), 0x2C84, 0 ) );
				if ( MyServerSettings.SellVeryRareChance() ){ Add( new GenericBuyInfo( "1041267", typeof( Runebook ), 3500, Utility.Random( 1,3 ), 0x0F3D, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( ScribesPen ), 4 );
				Add( typeof( BlankScroll ), 3 );
				Add( typeof( Monocle ), 12 );
				Add( typeof( DynamicBook ), Utility.Random( 10,150 ) );
				Add( typeof( TomeOfWands ), Utility.Random( 100,400 ) );
				Add( typeof( JokeBook ), Utility.Random( 750,1500 ) );
				Add( typeof( DataPad ), Utility.Random( 5, 150 ) );
				Add( typeof( NecromancerSpellbook ), 55 );
				Add( typeof( BookOfBushido ), 70 );
				Add( typeof( BookOfNinjitsu ), 70 );
				Add( typeof( MysticSpellbook ), 70 );
				Add( typeof( DeathKnightSpellbook ), Utility.Random( 100,300 ) );
				Add( typeof( Runebook ), Utility.Random( 100,350 ) );
				Add( typeof( BookOfChivalry ), 70 );
				Add( typeof( BookOfChivalry ), 70 );
				Add( typeof( HolyManSpellbook ), Utility.Random( 50,200 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBDruidGuild : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBDruidGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Bandage ), 2, Utility.Random( 250,1000 ), 0xE21, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserHealPotion ), 100, Utility.Random( 50,200 ), 0x25FD, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 3, Utility.Random( 50,200 ), 0xF85, 0 ) );
				Add( new GenericBuyInfo( typeof( Garlic ), 3, Utility.Random( 50,200 ), 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( RefreshPotion ), 100, Utility.Random( 50,200 ), 0xF0B, 0 ) );
				Add( new GenericBuyInfo( typeof( GardenTool ), 12, Utility.Random( 50,200 ), 0xDFD, 0x84F ) );
				Add( new GenericBuyInfo( typeof( HerbalistCauldron ), 247, Utility.Random( 50,200 ), 0x2676, 0 ) );
				Add( new GenericBuyInfo( typeof( MixingSpoon ), 34, Utility.Random( 50,200 ), 0x1E27, 0x979 ) );
				Add( new GenericBuyInfo( typeof( CBookDruidicHerbalism ), 50, Utility.Random( 1,100 ), 0x2D50, 0 ) );
				Add( new GenericBuyInfo( typeof( AlchemyTub ), 2400, Utility.Random( 1,5 ), 0x126A, 0 ) );
				Add( new GenericBuyInfo( typeof( AlchemyPouch ), Utility.Random( 2900,3500 ), Utility.Random( 1,2 ), 0x1C10, 0x89F ) );
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( AppleTreeDeed ), 10640, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( CherryBlossomTreeDeed ), 10540, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( DarkBrownTreeDeed ), 10540, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( GreyTreeDeed ), 10540, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( LightBrownTreeDeed ), 10540, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( PeachTreeDeed ), 10640, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( PearTreeDeed ), 10640, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( VinePatchAddonDeed ), 10400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( HopsPatchDeed ), 10400, Utility.Random( 1,2 ), 0x14F0, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( LesserHealPotion ), 7 );
				Add( typeof( RefreshPotion ), 7 );
				Add( typeof( Garlic ), 2 );
				Add( typeof( Ginseng ), 2 );
				Add( typeof( GardenTool ), 6 );
				Add( typeof( HerbalistCauldron ), 123 );
				Add( typeof( MixingSpoon ), 17 );
				Add( typeof( AlchemyTub ), Utility.Random( 200, 500 ) );
				Add( typeof( FirstAidKit ), Utility.Random( 100,250 ) );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBCarpenterGuild: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBCarpenterGuild()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Hatchet ), 20, Utility.Random( 1,100 ), 0xF44, 0 ) );
				Add( new GenericBuyInfo( typeof( LumberAxe ), 22, Utility.Random( 1,100 ), 0xF43, 0x96D ) );
				Add( new GenericBuyInfo( typeof( GuildCarpentry ), 500, Utility.Random( 1,5 ), 0x1EBA, 0x430 ) );
				Add( new GenericBuyInfo( typeof( Nails ), 3, Utility.Random( 50,200 ), 0x102E, 0 ) );
				Add( new GenericBuyInfo( typeof( Axle ), 2, Utility.Random( 50,200 ), 0x105B, 0 ) );
				Add( new GenericBuyInfo( typeof( Board ), 3, Utility.Random( 50,200 ), 0x1BD7, 0 ) );
				Add( new GenericBuyInfo( typeof( DrawKnife ), 10, Utility.Random( 50,200 ), 0x10E4, 0 ) );
				Add( new GenericBuyInfo( typeof( Froe ), 10, Utility.Random( 50,200 ), 0x10E5, 0 ) );
				Add( new GenericBuyInfo( typeof( Scorp ), 10, Utility.Random( 50,200 ), 0x10E7, 0 ) );
				Add( new GenericBuyInfo( typeof( Inshave ), 10, Utility.Random( 50,200 ), 0x10E6, 0 ) );
				Add( new GenericBuyInfo( typeof( DovetailSaw ), 12, Utility.Random( 50,200 ), 0x1028, 0 ) );
				Add( new GenericBuyInfo( typeof( Saw ), 100, Utility.Random( 50,200 ), 0x1034, 0 ) );
				Add( new GenericBuyInfo( typeof( Hammer ), 17, Utility.Random( 50,200 ), 0x102A, 0 ) );
				Add( new GenericBuyInfo( typeof( MouldingPlane ), 11, Utility.Random( 50,200 ), 0x102C, 0 ) );
				Add( new GenericBuyInfo( typeof( SmoothingPlane ), 10, Utility.Random( 50,200 ), 0x1032, 0 ) );
				Add( new GenericBuyInfo( typeof( JointingPlane ), 11, Utility.Random( 50,200 ), 0x1030, 0 ) );
				Add( new GenericBuyInfo( typeof( WoodworkingTools ), 10, Utility.Random( 10,50 ), 0x4F52, 0 ) );
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AdventurerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F9B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( AlchemyCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F91, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ArmsCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F9E, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BakerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F92, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BeekeeperCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F95, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BlacksmithCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( BowyerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F97, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ButcherCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F89, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( CarpenterCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8A, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( FletcherCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F88, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HealerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F98, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( HugeCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F86, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( JewelerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( LibrarianCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F96, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( MusicianCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F94, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NecromancerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F9A, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ProvisionerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8E, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SailorCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F9C, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( StableCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F87, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( SupplyCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F9D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TailorCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TavernCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F99, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TinkerCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F90, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( TreasureCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F93, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( WizardryCrate ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x4F8C, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C43, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C45, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C47, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C89, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x38B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x38D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CC9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireH ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CCB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireI ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CCD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmoireJ ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D26, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmorShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BF1, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmorShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C31, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmorShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C63, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmorShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CAD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewArmorShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CEF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C3B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C65, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C67, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CBF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CC1, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CF1, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBakerShelfG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBlacksmithShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C41, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBlacksmithShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C4B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBlacksmithShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C6B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBlacksmithShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CC5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBlacksmithShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CF7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C15, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C2B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C2D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C33, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C5F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C61, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C79, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfH ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CA5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfI ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CA7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfJ ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CAF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfK ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CEB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfL ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CED, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBookShelfM ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D05, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBowyerShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C29, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBowyerShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C5D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBowyerShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CA3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewBowyerShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CE9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewCarpenterShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C6F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewCarpenterShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CD7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewCarpenterShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CFB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C51, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C53, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C75, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C77, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CDD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CDF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CFF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewClothShelfH ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D01, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDarkBookShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BF9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDarkBookShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BFB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDarkShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BFD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C7F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C81, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C83, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C85, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C87, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CB5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CB7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersH ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CB9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersI ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CBB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersJ ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CBD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersK ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D0B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersL ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D20, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersM ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D22, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrawersN ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D24, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrinkShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C27, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrinkShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C5B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrinkShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CA1, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrinkShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CE7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewDrinkShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C1B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewHelmShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BFF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewHunterShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C4D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewKitchenShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C19, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewKitchenShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C39, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewOldBookShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x19FF, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewPotionShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3BF3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewRuinedBookShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0xC14, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C35, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C3D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C69, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C7B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CB1, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CC3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfG ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CF5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShelfH ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D07, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShoeShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C37, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShoeShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C7D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShoeShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CB3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewShoeShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D09, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSorcererShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C4F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSorcererShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C73, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSorcererShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CDB, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSorcererShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CFD, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSupplyShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C57, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSupplyShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C9D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewSupplyShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CE3, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTailorShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C3F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTailorShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C6D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTailorShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CC7, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTailorShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CF9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTannerShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C23, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTannerShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C49, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTavernShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C25, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTavernShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C59, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTavernShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C9F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTavernShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CE5, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTinkerShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C71, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTinkerShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CD9, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTinkerShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3D03, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewTortureShelf ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C2F, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfA ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C17, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfB ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C1D, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfC ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C21, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfD ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C55, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfE ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3C9B, 0 ) ); }
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( NewWizardShelfF ), Utility.Random( 200,400 ), Utility.Random( 1,5 ), 0x3CE1, 0 ) ); }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Hatchet ), 100 );
				Add( typeof( LumberAxe ), 16 );
				Add( typeof( WoodenBox ), 7 );
				Add( typeof( SmallCrate ), 5 );
				Add( typeof( MediumCrate ), 6 );
				Add( typeof( LargeCrate ), 7 );
				Add( typeof( WoodenChest ), 100 );
				Add( typeof( LargeTable ), 10 );
				Add( typeof( Nightstand ), 7 );
				Add( typeof( YewWoodTable ), 10 );
				Add( typeof( Throne ), 24 );
				Add( typeof( WoodenThrone ), 6 );
				Add( typeof( Stool ), 6 );
				Add( typeof( FootStool ), 6 );
				Add( typeof( FancyWoodenChairCushion ), 12 );
				Add( typeof( WoodenChairCushion ), 10 );
				Add( typeof( WoodenChair ), 8 );
				Add( typeof( BambooChair ), 6 );
				Add( typeof( WoodenBench ), 6 );
				Add( typeof( Saw ), 9 );
				Add( typeof( Scorp ), 6 );
				Add( typeof( SmoothingPlane ), 6 );
				Add( typeof( DrawKnife ), 6 );
				Add( typeof( Froe ), 6 );
				Add( typeof( Hammer ), 14 );
				Add( typeof( Inshave ), 6 );
				Add( typeof( WoodworkingTools ), 6 );
				Add( typeof( JointingPlane ), 6 );
				Add( typeof( MouldingPlane ), 6 );
				Add( typeof( DovetailSaw ), 7 );
				Add( typeof( Axle ), 1 );
				Add( typeof( Club ), 13 );

				Add( typeof( Log ), 1 );
				Add( typeof( AshLog ), 2 );
				Add( typeof( CherryLog ), 2 );
				Add( typeof( EbonyLog ), 3 );
				Add( typeof( GoldenOakLog ), 3 );
				Add( typeof( HickoryLog ), 4 );
				Add( typeof( MahoganyLog ), 4 );
				Add( typeof( DriftwoodLog ), 4 );
				Add( typeof( OakLog ), 5 );
				Add( typeof( PineLog ), 5 );
				Add( typeof( GhostLog ), 5 );
				Add( typeof( RosewoodLog ), 6 );
				Add( typeof( WalnutLog ), 6 );
				Add( typeof( ElvenLog ), 12 );
				Add( typeof( PetrifiedLog ), 7 );

				Add( typeof( Board ), 2 );
				Add( typeof( AshBoard ), 3 );
				Add( typeof( CherryBoard ), 3 );
				Add( typeof( EbonyBoard ), 4 );
				Add( typeof( GoldenOakBoard ), 4 );
				Add( typeof( HickoryBoard ), 5 );
				Add( typeof( MahoganyBoard ), 5 );
				Add( typeof( DriftwoodBoard ), 5 );
				Add( typeof( OakBoard ), 6 );
				Add( typeof( PineBoard ), 6 );
				Add( typeof( GhostBoard ), 6 );
				Add( typeof( RosewoodBoard ), 7 );
				Add( typeof( WalnutBoard ), 7 );
				Add( typeof( ElvenBoard ), 14 );
				Add( typeof( PetrifiedBoard ), 8 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBAssassin : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBAssassin()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( LesserPoisonPotion ), 100, Utility.Random( 10,50 ), 0x2600, 0 ) );
				Add( new GenericBuyInfo( typeof( PoisonPotion ), 30, Utility.Random( 10,50 ), 0xF0A, 0 ) );
				Add( new GenericBuyInfo( typeof( GreaterPoisonPotion ), 60, Utility.Random( 10,50 ), 0x2601, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( DeadlyPoisonPotion ), 120, Utility.Random( 1,100 ), 0x2669, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( LethalPoisonPotion ), 320, Utility.Random( 1,100 ), 0x266A, 0 ) ); }
				Add( new GenericBuyInfo( typeof( Nightshade ), 4, Utility.Random( 250,1000 ), 0xF88, 0 ) );
				Add( new GenericBuyInfo( typeof( Dagger ), 21, Utility.Random( 10,50 ), 0xF52, 0 ) );
				Add( new GenericBuyInfo( typeof( AssassinSpike ), 21, Utility.Random( 1,100 ), 0x2D21, 0 ) );
				Add( new GenericBuyInfo( "1041060", typeof( HairDye ), 100, Utility.Random( 1,100 ), 0xEFF, 0 ) );
				Add( new GenericBuyInfo( "hair dye bottle", typeof( HairDyeBottle ), 1000, Utility.Random( 1,100 ), 0xE0F, 0 ) );
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( DisguiseKit ), 700, Utility.Random( 1,5 ), 0xE05, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( AssassinRobe ), 38, Utility.Random( 1,10 ), 0x2B69, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( AssassinShroud ), 50, Utility.Random( 1,10 ), 0x2FB9, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ReaperHood ), 28, Utility.Random( 1,10 ), 0x4CDB, 0 ) ); }
				if ( MyServerSettings.SellChance() ){ Add( new GenericBuyInfo( typeof( ReaperCowl ), 28, Utility.Random( 1,10 ), 0x4CDD, 0 ) ); }
				Add( new GenericBuyInfo( typeof( BookOfPoisons ), 50, Utility.Random( 1,100 ), 0x2253, 0x4F8 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( LesserPoisonPotion ), 7 );
				Add( typeof( PoisonPotion ), 14 );
				Add( typeof( GreaterPoisonPotion ), 28 );
				Add( typeof( DeadlyPoisonPotion ), 56 );
				Add( typeof( LethalPoisonPotion ), 128 );
				Add( typeof( Nightshade ), 2 );
				Add( typeof( Dagger ), 10 );
				Add( typeof( HairDye ), 50 );
				Add( typeof( HairDyeBottle ), 300 );
				Add( typeof( SilverSerpentVenom ), 140 );
				Add( typeof( GoldenSerpentVenom ), 210 );
				Add( typeof( AssassinSpike ), 10 );
				Add( typeof( AssassinRobe ), 19 );
				Add( typeof( AssassinShroud ), 29 );
				Add( typeof( ReaperHood ), 11 );
				Add( typeof( ReaperCowl ), 11 );
			}
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SBCartographer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBCartographer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BlankMap ), 5, Utility.Random( 3,31 ), 0x14EC, 0 ) );
				Add( new GenericBuyInfo( typeof( MapmakersPen ), 8, Utility.Random( 3,31 ), 0x2052, 0 ) );
				Add( new GenericBuyInfo( typeof( BlankScroll ), 12, Utility.Random( 30,310 ), 0xEF3, 0 ) );
				Add( new GenericBuyInfo( typeof( MasterSkeletonsKey ), Utility.Random( 250,1000 ), Utility.Random( 3,5 ), 0x410B, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BlankScroll ), 6 );
				Add( typeof( MapmakersPen ), 4 );
				Add( typeof( BlankMap ), 2 );
				Add( typeof( CityMap ), 3 );
				Add( typeof( LocalMap ), 3 );
				Add( typeof( WorldMap ), 3 );
				Add( typeof( PresetMapEntry ), 3 );
				Add( typeof( WorldMapLodor ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapSosaria ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapBottle ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapSerpent ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapUmber ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapAmbrosia ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapIslesOfDread ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapSavage ), Utility.Random( 10,150 ) );
				Add( typeof( WorldMapUnderworld ), Utility.Random( 20,300 ) );
				Add( typeof( AlternateRealityMap ), Utility.Random( 500,1000 ) );
			}
		}
	}
}
