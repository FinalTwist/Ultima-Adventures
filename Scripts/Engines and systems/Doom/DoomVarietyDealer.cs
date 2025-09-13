using System;
using System.Collections.Generic;
using Server;
using Server.Engines.BulkOrders;
using System.Collections;
using Server.Targeting; 
using Server.Items; 
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class DoomVarietyDealer : BaseVendor
	{

		
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public DoomVarietyDealer() : base( "the variety dealer" )
		{
			Job = JobFragment.shopkeep;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Camping, 65.0, 88.0 );
			SetSkill( SkillName.ItemID, 65.0, 88.0 );
			Blessed = true;
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBDoomVarietyDealer() );
		
		}

		public DoomVarietyDealer( Serial serial ) : base( serial )
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
}