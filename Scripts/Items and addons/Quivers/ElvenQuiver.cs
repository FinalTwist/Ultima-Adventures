using System;
using Server;

namespace Server.Items
{
	public class ElvenQuiver : BaseQuiver
	{
		public override int LabelNumber{ get{ return 1032657; } } // elven quiver
		
		[Constructable]
		public ElvenQuiver() : base()
		{
			WeightReduction = 50;
		}

		public ElvenQuiver( Serial serial ) : base( serial )
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
