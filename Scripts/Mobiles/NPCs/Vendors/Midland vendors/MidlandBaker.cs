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
	public class MidlandBaker : MidlandVendor
	{


		[Constructable]
		public MidlandBaker() : base(  )
		{
			Job = JobFragment.shopkeep;
			good1 = typeof(BreadLoaf);
			good1name = "bread loaves";
			good1adjust = 1.5;
			good2 = typeof(Muffins);
			good2name = "muffins";
			good2adjust = 1.5;
			good3 = typeof(SackFlour);
			good3name = "floursacks";
			good3adjust = 0.75;
			
			Title = "the bread vendor";
			((BaseCreature)this).midrace = 1;

			//

		}

		public MidlandBaker( Serial serial ) : base( serial )
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
			good1 = typeof(BreadLoaf);
			good2 = typeof(Muffins);
			good3 = typeof(SackFlour);

		}
	}
}