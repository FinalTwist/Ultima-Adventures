using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class PoorLumberHarvester : BaseHarvester
	{
		[Constructable]
		public PoorLumberHarvester() : base()
		{
			Name = "Poor Lumber Harvester";
			type = 2;
			quality = Utility.RandomMinMax(1, 3);
			NestSpawnType = "WeakSentry";
			Movable = true;
			
		}

		public PoorLumberHarvester( Serial serial ) : base( serial )
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