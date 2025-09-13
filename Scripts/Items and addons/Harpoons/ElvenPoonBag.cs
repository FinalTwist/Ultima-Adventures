using System;
using Server;

namespace Server.Items
{
	public class ElvenPoonBag	: BasePoon
	{
		//public override int LabelNumber{ get{ return 1032657; } } // elven quiver
		
		[Constructable]
		public ElvenPoonBag() : base()
		{
			Name = "Elvish High Quality Harpoon Quiver";
			WeightReduction = 50;
		}

		public ElvenPoonBag( Serial serial ) : base( serial )
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
