using System;

namespace Server.Items
{
	public class Fiddle : BaseInstrument
	{
		[Constructable]
		public Fiddle() : base( 0x3966, 0x5B1, 0x5B0 )
		{
			Name = "fiddle";
			Weight = 5.0;
		}

		public Fiddle( Serial serial ) : base( serial )
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