using System;
using Server.Items;

namespace Server.Items
{
	public class GiftRoyalGorget : GiftPlateGorget
	{
		[Constructable]
		public GiftRoyalGorget()
		{
			ItemID = 0x2B0E;
			Name = "royal gorget";
			Weight = 2.0;
		}

		public GiftRoyalGorget( Serial serial ) : base( serial )
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
		}
	}
}