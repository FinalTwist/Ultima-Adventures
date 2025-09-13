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
	public class MidlandLumberjack : MidlandVendor
	{


		[Constructable]
		public MidlandLumberjack() : base(  )
		{
			Job = JobFragment.shopkeep;
			good1 = typeof(Board);
			good1name = "boards";
			good1adjust = 0.30;
			good2 = typeof(Hatchet);
			good2name = "hatchets";
			good2adjust = 2;

			Title = "the lumber vendor";
			
			((BaseCreature)this).midrace = 1;

			//

		}

		public MidlandLumberjack( Serial serial ) : base( serial )
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
			good1 = typeof(Board);
			good2 = typeof(Hatchet);

		}
	}
}