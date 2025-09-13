using System;
using Server;

namespace Server.Items
{
	public class CandleLarge : BaseLight
	{
		public override int LitItemID{ get { return 0x3032; } }
		public override int UnlitItemID{ get { return 0x3031; } }

		[Constructable]
		public CandleLarge() : base( 0x3031 )
		{
			Name = "candle";
			Duration = TimeSpan.Zero;
			Burning = false;
			Light = LightType.Circle150;
			Weight = 2.0;
		}

		public CandleLarge( Serial serial ) : base( serial )
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