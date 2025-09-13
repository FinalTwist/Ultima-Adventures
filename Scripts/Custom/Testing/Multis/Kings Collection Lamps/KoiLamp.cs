using System;
using Server;

namespace Server.Items
{
	[Flipable]
	public class KoiLamp : BaseLight
	{
		public override int LitItemID
		{
			get
			{
				if ( ItemID == 0x4C48 )//1
					return 0x4C49;//2
				else
					return 0x4C4B;//4
			}
		}
		
		public override int UnlitItemID
		{
			get
			{
				if ( ItemID == 0x4C49 )//2
					return 0x4C48;//1
				else
					return 0x4C4A;//3
			}
		}
		
		[Constructable]
		public KoiLamp() : base( 0x4C48 )//1
		{
			Movable = true;
			Duration = TimeSpan.Zero; // Never burnt out
			Burning = false;
			Light = LightType.Circle300;
			Weight = 3.0;
		}

		public KoiLamp( Serial serial ) : base( serial )
		{
		}

		public void Flip()
		{
			Light = LightType.Circle300;

			switch ( ItemID )
			{
				case 0x4C48: ItemID = 0x4C4A; break;//unlit
				case 0x4C49: ItemID = 0x4C4B; break;//lit

				case 0x4C4A: ItemID = 0x4C48; break;//unlit
				case 0x4C4B: ItemID = 0x4C49; break;//lit
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