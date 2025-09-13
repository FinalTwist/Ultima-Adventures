using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Gumps;
using Server.OneTime;
using Server.Items.Crops;

namespace Server.Mobiles
{
	public class MidlandDealer : MidlandVendor
	{


		[Constructable]
		public MidlandDealer() : base(  )
		{
			Job = JobFragment.shopkeep;
			good1 = typeof(SmokeweedLeaves);
			good1name = "Smokeweed leaves";
			good1adjust = 0.75;
			good2 = typeof(SmokeweekJoint);
			good2name = "joints";
			good2adjust = 5;

			Title = "the shady dealer";
			
			((BaseCreature)this).midrace = 1;

			//

		}

		public MidlandDealer( Serial serial ) : base( serial )
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
			good1 = typeof(SmokeweedLeaves);
			good2 = typeof(SmokeweekJoint);

		}
	}
}