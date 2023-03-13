using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class PerfectHideHarvester : BaseHarvester
	{
		[Constructable]
		public PerfectHideHarvester() : base()
		{
			Name = "Perfect Hide Harvester";
			type = 3;
			quality = Utility.RandomMinMax(9, 10);
			NestSpawnType = "StrongSentry";
			Movable = true;

			
		}

		public PerfectHideHarvester( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}