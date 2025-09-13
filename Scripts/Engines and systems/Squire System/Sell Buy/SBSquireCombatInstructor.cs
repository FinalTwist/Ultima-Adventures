using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBSquireCombatInstructor : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSquireCombatInstructor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "an wrestling training contract", typeof( SWrestlingContract ), 500, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "an tactics training contract", typeof( STacticsContract ), 500, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "an magic resistance training contract", typeof( SMagicResistContract ), 500, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "an anatomy training contract", typeof( SAnatomyContract ), 500, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "an swordsmanship training contract", typeof( SSwordsContract ), 500, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "an macefighting training contract", typeof( SMacingContract ), 500, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "an fencing training contract", typeof( SFencingContract ), 500, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "an archery training contract", typeof( SArcheryContract ), 500, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "an parrying training contract", typeof( SParryContract ), 500, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "an evaluate intelligence training contract", typeof( SEvalIntContract ), 500, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "an meditation training contract", typeof( SMeditationContract ), 500, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "an focus training contract", typeof( SFocusContract ), 500, 10, 0x14F0, 0 ) );
				Add( new GenericBuyInfo( "an chivalry training contract", typeof( SChivalryContract ), 500, 10, 0x14F0, 0 ) ); // Added 1.9.6
                Add(new GenericBuyInfo("a Bushido training contract", typeof(SBushidoContract), 300, 10, 0x14F0, 0)); // Added Rafa
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
