using Server;
using System;
using System.Collections.Generic;
using System.Collections;
using Server.Items;
using Server.Multis;
using Server.Guilds;
using Server.Misc;

namespace Server.Mobiles
{
	public class EtherealDealer : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public virtual bool IsInvulnerable{ get{ return true; } }

		[Constructable]
		public EtherealDealer() : base( "" )
		{
			Job = JobFragment.shopkeep;
			Karma = Utility.RandomMinMax( 13, -45 );
			InitStats(31, 41, 51);

			Body = 175;
			Blessed = true;
			Direction = Direction.East;
			Name = "Ethereal Guide";
			CantWalk = true;

			SetSkill( SkillName.Magery, 64.0, 100.0 );
			SetSkill( SkillName.Meditation, 60.0, 83.0 );
			SetSkill( SkillName.MagicResist, 65.0, 88.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBEthereal() );
		}

		public EtherealDealer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class SBEthereal: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBEthereal()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( EtherealLlama ), 2000000, 1, 0x20F6, 2858 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( EtherealHorse ), 2200000, 1, 0x20DD, 2858 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( EtherealOstard ), 2400000, 1, 0x2135, 2858 ) );}
				if ( MyServerSettings.SellChance() ){Add( new GenericBuyInfo( typeof( EtherealRidgeback ), 2600000, 1, 0x2615, 2858 ) );}
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( EtherealBeetle ), 2800000, 1, 0x260F, 2858 ) );}
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( EtherealCuSidhe ), 3000000, 1, 0x2D96, 2858 ) );}
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( RideablePolarBear ), 3200000, 1, 0x20E1, 2858 ) );}
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( EtherealHiryu ), 3400000, 1, 0x276A, 2858 ) );}
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( EtherealKirin ), 3600000, 1, 0x25A0, 2858 ) );}
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EtherealUnicorn ), 3800000, 1, 0x25CE, 2858 ) );}
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EtherealSwampDragon ), 4000000, 1, 0x2619, 2858 ) );}
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ChargerOfTheFallen ), 4200000, 1, 0x0499, 2858 ) );}
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EtherealReptalon ), 4400000, 1, 0x2d95, 2858 ) );}
				
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
