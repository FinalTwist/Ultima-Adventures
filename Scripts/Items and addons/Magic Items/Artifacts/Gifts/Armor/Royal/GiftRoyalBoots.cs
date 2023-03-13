using System;
using Server.Items;

namespace Server.Items
{
	public class GiftRoyalBoots : GiftPlateGorget
	{
		[Constructable]
		public GiftRoyalBoots()
		{
			ItemID = 0x2B12;
			Name = "royal boots";
			Layer = Layer.Shoes;
			Weight = 3.0;
		}

		public GiftRoyalBoots( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}