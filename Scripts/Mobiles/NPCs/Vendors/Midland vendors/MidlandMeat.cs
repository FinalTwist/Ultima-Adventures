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
	public class MidlandMeat : MidlandVendor
	{


		[Constructable]
		public MidlandMeat() : base(  )
		{
			Job = JobFragment.shopkeep;
			good1 = typeof(Ribs);
			good1name = "Ribs";
			good1adjust = 1.5;
			good2 = typeof(CookedBird);
			good2name = "Cooked Chickens";
			good2adjust = 1.5;
			good3 = typeof(Skillet);
			good3name = "Skillets";
			good3adjust = 2.5;
			
			Title = "the meat vendor";
			((BaseCreature)this).midrace = 1;

			//

		}

		public MidlandMeat( Serial serial ) : base( serial )
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

			good1 = typeof(Ribs);
			good2 = typeof(CookedBird);
			good3 = typeof(Skillet);

		}
	}
}