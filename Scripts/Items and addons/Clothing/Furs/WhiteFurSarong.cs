using System;

namespace Server.Items
{
	public class WhiteFurSarong : Kilt
	{
		[Constructable]
		public WhiteFurSarong() : base( 0x230C )
		{
			Name = "fur sarong";
			Hue = 0x481;
			Weight = 3.0;
			Resistances.Cold = 10;
		}

		public WhiteFurSarong( Serial serial ) : base( serial )
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

			if ( Weight == 4.0 )
				Weight = 3.0;
		}
	}
}