using System;
using System.Collections.Generic;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	public class SBSpammerRes : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSpammerRes()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{  
				Add( new GenericBuyInfo( typeof( PowderOfTemperament ), 2000000, 20, 0x973, 0 ) );
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "Leather",typeof( Leather ), 15, 10000, 0x1BD7, 0 ) ); }
				Add( new GenericBuyInfo( "Spined Leather", typeof( SpinedLeather ), 75, 10000, 0x1081, 0 ) );
				if (Utility.RandomBool())
					Add( new GenericBuyInfo( "Horned Leather", typeof( HornedLeather ), 150, 10000, 0x1081,0 ) );
				if (Utility.RandomDouble() > 0.80)
					Add( new GenericBuyInfo( "Barbed Leather", typeof( BarbedLeather ), 200, 5000, 0x1081, 0 ) );
				if (Utility.RandomBool())
					Add( new GenericBuyInfo( typeof( IronIngot ), 15, 10000, 0x1BF2, 0 ) );	
				if (Utility.RandomBool())
					Add( new GenericBuyInfo( "DC Ingots", typeof( DullCopperIngot ), 25, 10000, 0x1BF2, 0 ) );		
				if (Utility.RandomBool())
					Add( new GenericBuyInfo( "Shadow Iron", typeof( ShadowIronIngot ), 30, 10000, 0x1BF2, 0 ) );			
				if (Utility.RandomBool())
					Add( new GenericBuyInfo( "Copper ingots", typeof( CopperIngot ), 35, 10000, 0x1BF2, 0 ) );	
				if (Utility.RandomBool())
					Add( new GenericBuyInfo( "Bronze ingots", typeof( BronzeIngot ), 40, 10000, 0x1BF2, 0 ) );	
				if (Utility.RandomDouble() > 0.70)
					Add( new GenericBuyInfo( "Gold ingots", typeof( GoldIngot ), 55, 10000, 0x1BF2, 0 ) );		
				if (Utility.RandomDouble() > 0.75)
					Add( new GenericBuyInfo( "Agapite Ingots", typeof( AgapiteIngot ), 85, 10000, 0x1BF2, 0 ) );		
				if (Utility.RandomDouble() > 0.80)
					Add( new GenericBuyInfo( "Verite Ingots", typeof( VeriteIngot ), 125, 10000, 0x1BF2, 0 ) );	
				if (Utility.RandomDouble() > 0.90)
					Add( new GenericBuyInfo( "Val Ingots", typeof( ValoriteIngot ), 250, 10000, 0x1BF2, 0 ) );	
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "Boards",typeof( Board ), 15, 10000, 0x1BD7, 0 ) ); }		
				Add( new GenericBuyInfo( typeof( ManyArrows100 ), 1500, 7, 0xF41, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyBolts100 ), 1700, 7, 0x1BFD, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyArrows1000 ), 17000, 10, 0xF41, 0 ) );
				Add( new GenericBuyInfo( typeof( ManyBolts1000 ), 20000, 10, 0x1BFD, 0 ) );	

				Add( new GenericBuyInfo( typeof( BlankScroll ), 25, 10000, 0x0E34, 0 ) );

				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( "Throwing Weapons",typeof( ThrowingWeapon ), 15, 10000, 0x1BD7, 0 ) ); }
	

				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( BlackPearl ), 15, Utility.Random( 1000,5000 ), 0x266F, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Bloodmoss ), 12, Utility.Random( 1000,5000 ), 0xF7B, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Garlic ), 10, Utility.Random( 1000,5000 ), 0xF84, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Ginseng ), 10, Utility.Random( 1000,5000 ), 0xF85, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( MandrakeRoot ), 10, Utility.Random( 1000,5000 ), 0xF86, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( Nightshade ), 15, Utility.Random( 1000,5000 ), 0xF88, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SpidersSilk ), 10, Utility.Random( 1000,5000 ), 0xF8D, 0 ) ); }
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( SulfurousAsh ), 10, Utility.Random( 1000,5000 ), 0xF8C, 0 ) ); }				
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Log ), 3 );
				Add( typeof( Board ), 4 );
				Add( typeof( IronIngot ), 5 ); 
				Add( typeof( DullCopperIngot ), 8 ); 
				Add( typeof( ShadowIronIngot ), 10 ); 
				Add( typeof( CopperIngot ), 12 ); 
				Add( typeof( BronzeIngot ), 14 ); 
				Add( typeof( GoldIngot ), 16 ); 
				Add( typeof( AgapiteIngot ), 19 ); 
				Add( typeof( VeriteIngot ), 25 ); 
				Add( typeof( ValoriteIngot ), 45 ); 
				Add( typeof( SpinedLeather ), 5 ); 
				Add( typeof( HornedLeather ), 10 ); 
				Add( typeof( BarbedLeather ), 25 ); 

			}
		}
	}
}
