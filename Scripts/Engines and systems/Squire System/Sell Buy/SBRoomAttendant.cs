using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBRoomAttendant : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRoomAttendant()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "squire care book", typeof( SquireCareBook ), 50, 10, 0x22C5, 0 ) );
				Add( new GenericBuyInfo( "squire inspection spyglass", typeof( SquireStatsSpyglass ), 200, 10, 0x14F5, 0 ) );
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
