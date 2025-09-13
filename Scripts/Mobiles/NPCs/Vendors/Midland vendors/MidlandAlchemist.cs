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
	public class MidlandAlchemist : MidlandVendor
	{


		[Constructable]
		public MidlandAlchemist() : base(  )
		{
			Job = JobFragment.shopkeep;
			good1 = typeof(Bottle);
			good1name = "bottles";
			good1adjust = 1;
			good2 = typeof(BlankScroll);
			good2name = "scrolls";
			good2adjust = 1;

			Title = "the alchemy supplier";
			
			((BaseCreature)this).midrace = 1;
		}

		public MidlandAlchemist( Serial serial ) : base( serial )
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
			good1 = typeof(Bottle);
			good2 = typeof(BlankScroll);

		}
	}
}