using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class StandardMiningHarvester : BaseHarvester
	{
		[Constructable]
		public StandardMiningHarvester() : base()
		{
			Name = "Standard Mining Harvester";
			type = 1;
			quality = Utility.RandomMinMax(4, 5);
			NestSpawnType = "BasicSentry";
			Movable = true;
			
		}

		public StandardMiningHarvester( Serial serial ) : base( serial )
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