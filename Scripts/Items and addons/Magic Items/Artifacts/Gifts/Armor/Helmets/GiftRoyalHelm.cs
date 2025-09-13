using System;
using Server;

namespace Server.Items
{
	public class GiftRoyalHelm : GiftPlateHelm
	{
		[Constructable]
		public GiftRoyalHelm()
		{
			ItemID = 0x2B10;
			Name = "royal helm";
			Weight = 5.0;
		}

		public GiftRoyalHelm( Serial serial ) : base( serial )
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