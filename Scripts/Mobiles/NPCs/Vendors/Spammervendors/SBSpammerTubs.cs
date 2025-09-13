using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBSpammerTubs : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSpammerTubs()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{  
				Add (new GenericBuyInfo( "Black Tub", typeof( RewardBlackDyeTub ), 250000, 20, 0xFAB, 0 ) );
				Add (new GenericBuyInfo( "Blaze Tub", typeof(  BlazeDyeTub ), 500000, 20, 0xFAB, 0 ) );
				Add (new GenericBuyInfo( "Furniture Tub", typeof( FurnitureDyeTub ), 150000, 20, 0xFAB, 0 ) );
				Add (new GenericBuyInfo( "Leather Tub", typeof( LeatherDyeTub ), 150000, 20, 0xFAB, 0 ) );
				Add (new GenericBuyInfo( "Runebook Tub", typeof( RunebookDyeTub ), 190000, 20, 0xFAB, 0 ) );
				Add (new GenericBuyInfo( "Special Tub", typeof( SpecialDyeTub ), 150000, 20, 0xFAB, 0 ) );
				Add (new GenericBuyInfo( "Statue Tub", typeof( StatuetteDyeTub ), 400000, 20, 0xFAB, 0 ) );
				if (Utility.RandomDouble() > 0.90)
					Add (new GenericBuyInfo( "Dye anything Tub", typeof( AwesomeDyeTub ), 5000000, 1, 0xFAB, 0 ) );
				Add( new GenericBuyInfo( typeof( PetDyeTub ), 1500000, Utility.Random( 1,3 ), 0x0012, 0 ) ); 
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{

			}
		}
	}
}
