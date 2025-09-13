using System;
using Server;

namespace Server.Items
{
	[Flipable]
	public class TallLamp : BaseLight
	{
		public override int LitItemID
		{
			get
			{
				if ( ItemID == 0x4C52 )//1
					return 0x4C53;//2
				else
					return 0x4C55;//4
			}
		}
		
		public override int UnlitItemID
		{
			get
			{
				if ( ItemID == 0x4C453 )//2
					return 0x4C52;//1
				else
					return 0x4C54;//3
			}
		}
		
		[Constructable]
		public TallLamp() : base( 0x4C52 )//1
		{
			Movable = true;
			Duration = TimeSpan.Zero; // Never burnt out
			Burning = false;
			Light = LightType.Circle300;
			Weight = 3.0;
		}

		public TallLamp( Serial serial ) : base( serial )
		{
		}

		public void Flip()
		{
			Light = LightType.Circle300;

			switch ( ItemID )
			{
				case 0x4C52: ItemID = 0x4C54; break;//unlit
				case 0x4C53: ItemID = 0x4C55; break;//lit

				case 0x4C54: ItemID = 0x4C52; break;//unlit
				case 0x4C55: ItemID = 0x4C53; break;//lit
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