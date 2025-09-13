using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class AleBarrel : BaseHarvester
	{
		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }
		
		[Constructable]
		public AleBarrel() : base()
		{
			ItemID = 0x3DB8;
			Name = "An ale-making barrel";
			type = 5;
			quality = Utility.RandomMinMax(4, 5);
			NestSpawnType = "";
			Movable = true;
			
		}

		public AleBarrel( Serial serial ) : base( serial )
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
