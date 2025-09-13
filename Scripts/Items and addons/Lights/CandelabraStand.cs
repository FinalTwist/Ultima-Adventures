using System;
using Server;

namespace Server.Items
{
	public class CandelabraStand : BaseLight
	{
		public override int LitItemID{ get { return 0x3036; } }
		public override int UnlitItemID{ get { return 0x3035; } }

		[Constructable]
		public CandelabraStand() : base( 0x3035 )
		{
			Name = "candelabra";
			Duration = TimeSpan.Zero;
			BurntOut = false;
			Burning = false;
			Light = LightType.Circle225;
			Weight = 20.0;
		}

		public CandelabraStand( Serial serial ) : base( serial )
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