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
	public class MidlandFisherman : MidlandVendor
	{


		[Constructable]
		public MidlandFisherman() : base(  )
		{
			Job = JobFragment.shopkeep;
			good1 = typeof(FishSteak);
			good1name = "fishsteaks";
			good1adjust = 0.30;
			good2 = typeof(FishingPole);
			good2name = "fishing poles";
			good2adjust = 2;

			Title = "the fishsteak vendor";

			
			((BaseCreature)this).midrace = 1;

			//

		}

		public MidlandFisherman( Serial serial ) : base( serial )
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
			good1 = typeof(FishSteak);
			good2 = typeof(FishingPole);

		}
	}
}