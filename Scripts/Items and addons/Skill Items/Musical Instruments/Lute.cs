using System;

namespace Server.Items
{
	public class Lute : BaseInstrument
	{
		[Constructable]
		public Lute() : base( 0xEB3, 0x4C, 0x4D )
		{
			Name = "lute";
			Weight = 5.0;
		}

		public Lute( Serial serial ) : base( serial )
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