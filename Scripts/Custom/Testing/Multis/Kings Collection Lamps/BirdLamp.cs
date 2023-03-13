using System;
using Server;

namespace Server.Items
{
	[Flipable]
	public class BirdLamp : BaseLight
	{
		public override int LitItemID
		{
			get
			{
				if ( ItemID == 0x4C44 )//1
					return 0x4C45;//2
				else
					return 0x4C47;//4
			}
		}
		
		public override int UnlitItemID
		{
			get
			{
				if ( ItemID == 0x4C45 )//2
					return 0x4C44;//1
				else
					return 0x4C46;//3
			}
		}
		
		[Constructable]
		public BirdLamp() : base( 0x4C44 )//1
		{
			Movable = true;
			Duration = TimeSpan.Zero; // Never burnt out
			Burning = false;
			Light = LightType.Circle300;
			Weight = 3.0;
		}

		public BirdLamp( Serial serial ) : base( serial )
		{
		}

		public void Flip()
		{
			Light = LightType.Circle300;

			switch ( ItemID )
			{
				case 0x4C44: ItemID = 0x4C46; break;//unlit
				case 0x4C45: ItemID = 0x4C47; break;//lit

				case 0x4C46: ItemID = 0x4C44; break;//unlit
				case 0x4C47: ItemID = 0x4C45; break;//lit
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