using System;
using Server.Items;

namespace Server.Items
{
	public class GiftRoyalArms : GiftPlateArms
	{
		[Constructable]
		public GiftRoyalArms()
		{
			ItemID = 0x2B0A;
			Name = "royal mantle";
			Weight = 5.0;
		}

		public GiftRoyalArms( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 5.0;
		}
	}
}