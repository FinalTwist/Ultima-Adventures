using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class LiquorBarrel : BaseHarvester
	{
		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }
		
		[Constructable]
		public LiquorBarrel() : base()
		{
			ItemID = 0x3DBA;
			Name = "An liquor-making barrel";
			type = 7;
			quality = Utility.RandomMinMax(4, 5);
			NestSpawnType = "";
			Movable = true;
			
		}

		public LiquorBarrel( Serial serial ) : base( serial )
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
