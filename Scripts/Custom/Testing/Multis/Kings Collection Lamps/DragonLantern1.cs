using System;
using Server;

namespace Server.Items
{
	[Flipable]
	public class DragonLantern : BaseLight
	{
		public override int LitItemID
		{
			get
			{
				if ( ItemID == 0x4C40 )//1
					return 0x4C41;//2
				else
					return 0x4C43;//4
			}
		}
		
		public override int UnlitItemID
		{
			get
			{
				if ( ItemID == 0x4C41 )//2
					return 0x4C40;//1
				else
					return 0x4C42;//3
			}
		}
		
		[Constructable]
		public DragonLantern() : base( 0x4C40 )//1
		{
			Movable = true;
			Duration = TimeSpan.Zero; // Never burnt out
			Burning = false;
			Light = LightType.Circle300;
			Weight = 3.0;
		}

		public DragonLantern( Serial serial ) : base( serial )
		{
		}

		public void Flip()
		{
			Light = LightType.Circle300;

			switch ( ItemID )
			{
				case 0x4C40: ItemID = 0x4C42; break;//unlit
				case 0x4C41: ItemID = 0x4C43; break;//lit

				case 0x4C42: ItemID = 0x4C40; break;//unlit
				case 0x4C43: ItemID = 0x4C41; break;//lit
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