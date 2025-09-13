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
	public class MidlandFarmer : MidlandVendor
	{


		[Constructable]
		public MidlandFarmer() : base(  )
		{
			Job = JobFragment.shopkeep;
			good1 = typeof(Corn);
			good1name = "corn";
			good1adjust = 0.25;
			good2 = typeof(Onion);
			good2name = "onions";
			good2adjust = 0.25;
			good3 = typeof(Carrot);
			good3name = "carrots";
			good3adjust = 0.25;
			good4 = typeof(Pumpkin);
			good4name = "pumpkins";
			good4adjust = 0.25;
			
			Title = "the produce vendor";
			((BaseCreature)this).midrace = 1;

			//

		}

		public MidlandFarmer( Serial serial ) : base( serial )
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
			good1 = typeof(Corn);
			good2 = typeof(Onion);
			good3 = typeof(Carrot);
			good4 = typeof(Pumpkin);

		}
	}
}