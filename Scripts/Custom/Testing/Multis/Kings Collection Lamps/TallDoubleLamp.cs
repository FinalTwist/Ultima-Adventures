using System;
using Server;

namespace Server.Items
{
	[Flipable]
	public class TallDoubleLamp : BaseLight
	{
		public override int LitItemID
		{
			get
			{
				if ( ItemID == 0x4C56 )//1
					return 0x4C57;//2
				else
					return 0x4C59;//4
			}
		}
		
		public override int UnlitItemID
		{
			get
			{
				if ( ItemID == 0x4C457 )//2
					return 0x4C56;//1
				else
					return 0x4C58;//3
			}
		}
		
		[Constructable]
		public TallDoubleLamp() : base( 0x4C56 )//1
		{
			Movable = true;
			Duration = TimeSpan.Zero; // Never burnt out
			Burning = false;
			Light = LightType.Circle300;
			Weight = 3.0;
		}

		public TallDoubleLamp( Serial serial ) : base( serial )
		{
		}

		public void Flip()
		{
			Light = LightType.Circle300;

			switch ( ItemID )
			{
				case 0x4C56: ItemID = 0x4C58; break;//unlit
				case 0x4C57: ItemID = 0x4C59; break;//lit

				case 0x4C58: ItemID = 0x4C56; break;//unlit
				case 0x4C59: ItemID = 0x4C57; break;//lit
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