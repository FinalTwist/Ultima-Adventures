using System;

namespace Server.Items
{
	public class Pipes : BaseInstrument
	{
		[Constructable]
		public Pipes() : base( 0x3965, 0x5B8, 0x5B7 )
		{
			Name = "pipes";
			Weight = 5.0;
		}

		public Pipes( Serial serial ) : base( serial )
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

			if ( Weight == 3.0 )
				Weight = 5.0;
		}
	}
}