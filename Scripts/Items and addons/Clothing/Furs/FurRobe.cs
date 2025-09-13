using System;

namespace Server.Items
{
	[Flipable( 0x1F03, 0x1F04 )]
	public class FurRobe : Robe
	{
		[Constructable]
		public FurRobe() : base( 0x1F03 )
		{
			Name = "fur robe";
			Hue = 0x907;
			Weight = 6.0;
			Resistances.Cold = 20;
		}

		public FurRobe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}