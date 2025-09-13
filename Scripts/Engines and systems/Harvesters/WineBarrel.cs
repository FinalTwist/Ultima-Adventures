using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class WineBarrel : BaseHarvester
	{
		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }
		
		[Constructable]
		public WineBarrel() : base()
		{
			ItemID = 0x2DDC;
			Name = "An ale-making barrel";
			type = 6;
			quality = Utility.RandomMinMax(4, 5);
			NestSpawnType = "";
			Movable = true;
			
		}

		public WineBarrel( Serial serial ) : base( serial )
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
