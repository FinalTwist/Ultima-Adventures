using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class StandardHideHarvester : BaseHarvester
	{
		[Constructable]
		public StandardHideHarvester() : base()
		{
			Name = "Standard Hide Harvester";
			type = 3;
			quality = Utility.RandomMinMax(4, 5);
			NestSpawnType = "BasicSentry";
			Movable = true;
			
		}

		public StandardHideHarvester( Serial serial ) : base( serial )
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