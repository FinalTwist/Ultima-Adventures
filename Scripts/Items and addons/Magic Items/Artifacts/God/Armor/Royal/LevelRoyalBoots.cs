using System;
using Server.Items;

namespace Server.Items
{
	public class LevelRoyalBoots : LevelPlateGorget
	{
		[Constructable]
		public LevelRoyalBoots()
		{
			ItemID = 0x2B12;
			Name = "royal boots";
			Layer = Layer.Shoes;
			Weight = 3.0;
		}

		public LevelRoyalBoots( Serial serial ) : base( serial )
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