using System;
using Server;

namespace Server.Items
{
	public class ColorCandleShort : BaseLight
	{
		public override int LitItemID{ get { return 0x142C; } }
		public override int UnlitItemID{ get { return 0x142F; } }

		[Constructable]
		public ColorCandleShort() : base( 0x142F )
		{
			Name = "colored candle";
			Duration = TimeSpan.Zero;
			Burning = false;
			Light = LightType.Circle150;
			Weight = 1.0;
		}

		public ColorCandleShort( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}