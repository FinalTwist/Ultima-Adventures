using System;
using Server;

namespace Server.Items
{
	public class GiftRoyalShield : GiftHeaterShield
	{
		[Constructable]
		public GiftRoyalShield()
		{
			ItemID = 0x2B01;
			Name = "royal shield";
			Weight = 7.0;
		}

		public GiftRoyalShield( Serial serial ) : base(serial)
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
