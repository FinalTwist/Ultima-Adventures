using System;
using Server;

namespace Server.Items
{
	[Flipable]
	public class CandleReligious : BaseLight 
	{
		public override int LitItemID
		{
			get
			{
				Light = LightType.Circle225;
				if ( ItemID == 0x2370 )
					return 0x236E;
				else
					return 0x2371;
			}
		}
		
		public override int UnlitItemID
		{
			get
			{
				Light = LightType.Empty;
				if ( ItemID == 0x236E )
					return 0x2370;
				else
					return 0x2373;
			}
		}
		
		[Constructable]
		public CandleReligious() : base( 0x2370 )
		{
			Name = "religious candle";
			Movable = true;
			Duration = TimeSpan.Zero; // Never burnt out
			Burning = false;
			Light = LightType.Empty;
			Weight = 4.0;
		}

		public CandleReligious( Serial serial ) : base( serial )
		{
		}

		public void Flip()
		{
			switch ( ItemID )
			{
				case 0x236E: ItemID = 0x2371; break;
				case 0x2371: ItemID = 0x236E; break;

				case 0x2370: ItemID = 0x2373; break;
				case 0x2373: ItemID = 0x2370; break;
			}
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