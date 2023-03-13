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
	public class MidlandClothier : MidlandVendor
	{


		[Constructable]
		public MidlandClothier() : base(  )
		{
			Job = JobFragment.shopkeep;
			good1 = typeof(Cloth);
			good1name = "cloth";
			good1adjust = 0.75;
			good2 = typeof(Cotton);
			good2name = "cotton";
			good2adjust = 0.25;
			good3 = typeof(Flax);
			good3name = "flax";
			good3adjust = 0.25;
			good4 = typeof(SewingKit);
			good4name = "sewing kits";
			good4adjust = 2;

			Title = "the clothier";
			
			((BaseCreature)this).midrace = 1;

			//

		}

		public MidlandClothier( Serial serial ) : base( serial )
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
			good1 = typeof(Cloth);
			good2 = typeof(Cotton);
			good3 = typeof(Flax);
			good4 = typeof(SewingKit);

		}
	}
}