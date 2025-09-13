using System;
using Server;

namespace Server.Items
{
	public class PaintCanvas : Item
	{
		[Constructable]
		public PaintCanvas() : base( 0xA6C )
		{
			Name = "painting canvas";
			Hue = 0x47E;
		}

		public PaintCanvas( Serial serial ) : base( serial )
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