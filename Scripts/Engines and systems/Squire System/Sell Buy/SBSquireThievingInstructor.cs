using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBSquireThievingInstructor : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSquireThievingInstructor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "a hiding training contract", typeof( SHidingContract ), 500, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "a stealing training contract", typeof( SStealingContract ), 500, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "a lockpicking training contract", typeof( SLockpickingContract ), 500, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "a poisoning training contract", typeof( SPoisoningContract ), 500, 10, 0x14F0, 0 ) ); // Added 1.9.5
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
