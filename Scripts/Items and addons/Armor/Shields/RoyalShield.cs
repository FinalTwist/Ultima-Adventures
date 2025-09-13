using System;
using Server;

namespace Server.Items
{
	public class RoyalShield : HeaterShield
	{
		[Constructable]
		public RoyalShield()
		{
			ItemID = 0x2B01;
			Name = "royal shield";
			Weight = 7.0;
		}

		public RoyalShield( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}
	}
}