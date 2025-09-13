using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class GoodMiningHarvester : BaseHarvester
	{
		[Constructable]
		public GoodMiningHarvester() : base()
		{
			Name = "Good Mining Harvester";
			type = 1;
			quality = Utility.RandomMinMax(6, 8);
			NestSpawnType = "SolidSentry";
			Movable = true;
			
		}

		public GoodMiningHarvester( Serial serial ) : base( serial )
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