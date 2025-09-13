using System;

namespace Server.Items
{
	public class WhiteDyeTub : DyeTub
	{
		[Constructable]
		public WhiteDyeTub()
		{
			DyedHue = 0x0481;
			Name = "white dye tub";
			Redyable = false;
			Hue = 0x3BC;
		}

		public WhiteDyeTub( Serial serial ) : base( serial )
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