using System;
using Server.Items;

namespace Server.Items
{
	public class RoyalBoots : PlateGorget
	{
		[Constructable]
		public RoyalBoots()
		{
			ItemID = 0x2B12;
			Name = "royal boots";
			Layer = Layer.Shoes;

		}

		public RoyalBoots( Serial serial ) : base( serial )
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