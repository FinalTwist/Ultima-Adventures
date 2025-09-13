using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class CheesePress : BaseHarvester
	{
		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }
		
		[Constructable]
		public CheesePress() : base()
		{
			ItemID = 0x142A;
			Name = "A cheese press";
			type = 8;
			quality = Utility.RandomMinMax(4, 5);
			NestSpawnType = "";
			Movable = true;
			
		}

		public CheesePress( Serial serial ) : base( serial )
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
