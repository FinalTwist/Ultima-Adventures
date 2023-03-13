using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class CiderBarrel : BaseHarvester
	{
		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }
		
		[Constructable]
		public CiderBarrel() : base()
		{
			ItemID = 0x3DB9;
			Name = "A cider-making barrel";
			type = 4;
			quality = Utility.RandomMinMax(4, 5);
			NestSpawnType = "";
			Movable = true;
			
		}

		public CiderBarrel( Serial serial ) : base( serial )
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
