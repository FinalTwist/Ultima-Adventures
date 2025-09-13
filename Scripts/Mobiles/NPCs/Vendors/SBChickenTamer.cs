using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBChickenFarmer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBChickenFarmer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{  
				Add( new AnimalBuyInfo( 1, typeof( BasicChicken ), 1000, 20, 204, 0 ) );

				

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
