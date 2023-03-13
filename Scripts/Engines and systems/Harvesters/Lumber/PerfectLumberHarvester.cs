using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class PerfectLumberHarvester : BaseHarvester
	{
		[Constructable]
		public PerfectLumberHarvester() : base()
		{
			Name = "Perfect Lumber Harvester";
			type = 2;
			quality = Utility.RandomMinMax(9, 10);
			NestSpawnType = "StrongSentry";
			Movable = true;

			
		}

		public PerfectLumberHarvester( Serial serial ) : base( serial )
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