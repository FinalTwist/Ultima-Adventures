using System;
using Server;

namespace Server.Items
{
	public class ArcherQuiver : BaseQuiver
	{
		[Constructable]
		public ArcherQuiver() : base()
		{
			Name = "quiver";
			WeightReduction = 50;
			DamageIncrease = 10;
		}

		public ArcherQuiver( Serial serial ) : base( serial )
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
