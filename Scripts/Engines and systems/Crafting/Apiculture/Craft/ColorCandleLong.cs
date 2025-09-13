using System;
using Server;

namespace Server.Items
{
	public class ColorCandleLong : BaseLight
	{
		public override int LitItemID{ get { return 0x1430; } }
		public override int UnlitItemID{ get { return 0x1433; } }

		[Constructable]
		public ColorCandleLong() : base( 0x1433 )
		{
			Name = "colored candle";
			Duration = TimeSpan.Zero;
			Burning = false;
			Light = LightType.Circle150;
			Weight = 1.0;
		}

		public ColorCandleLong( Serial serial ) : base( serial )
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