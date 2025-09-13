using System;
using Server.Items;

namespace Server.Items
{
	public class GiftRoyalGloves : GiftPlateGloves
	{
		[Constructable]
		public GiftRoyalGloves()
		{
			ItemID = 0x2B0C;
			Name = "royal bracers";
			Weight = 2.0;
		}

		public GiftRoyalGloves( Serial serial ) : base( serial )
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
				Weight = 2.0;
		}
	}
}