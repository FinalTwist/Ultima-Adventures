using System;
using Server;

namespace Server.Items
{
	public class ArcherPoonBag : BasePoon
	{
		[Constructable]
		public ArcherPoonBag() : base()
		{
			Name = "Harpoon Quiver";
			WeightReduction = 50;
			DamageIncrease = 10;
		}

		public ArcherPoonBag( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}
