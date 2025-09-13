using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Gumps;
using Server.OneTime;

namespace Server.Mobiles
{
	public class MidlandLeather : MidlandVendor
	{


		[Constructable]
		public MidlandLeather() : base(  )
		{
			Job = JobFragment.shopkeep;
			good1 = typeof(Leather);
			good1name = "leather";
			good1adjust = 0.30;
			good2 = typeof(SkinningKnife);
			good2name = "skinning knives";
			good2adjust = 2;

			Title = "the leather vendor";
			
			((BaseCreature)this).midrace = 1;

			//

		}

		public MidlandLeather( Serial serial ) : base( serial )
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
			good1 = typeof(Leather);
			good2 = typeof(SkinningKnife);

		}
	}
}