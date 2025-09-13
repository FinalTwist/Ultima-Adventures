using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class BalanceSpike : BaseHarvester
	{
		[Constructable]
		public BalanceSpike() : base()
		{
			Name = "A spike in the balance";
			type = 10;
			quality = 10;
			NestSpawnType = "StrongSentry";
			Movable = true;
			Weight = 75;

			
		}

		public BalanceSpike( Serial serial ) : base( serial )
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