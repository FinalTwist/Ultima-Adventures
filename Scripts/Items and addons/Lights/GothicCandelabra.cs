using System;
using Server;

namespace Server.Items
{
	public class GothicCandelabraA : BaseLight
	{
		public override int LitItemID{ get { return 0x1CC1; } }
		public override int UnlitItemID{ get { return 0x052D; } }

		[Constructable]
		public GothicCandelabraA() : base( 0x052D )
		{
			Name = "gothic candelabra";
			Duration = TimeSpan.Zero;
			BurntOut = false;
			Burning = false;
			Light = LightType.Circle225;
			Weight = 20.0;
		}

		public GothicCandelabraA( Serial serial ) : base( serial )
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
	public class GothicCandelabraB : BaseLight
	{
		public override int LitItemID{ get { return 0x1CC3; } }
		public override int UnlitItemID{ get { return 0x052E; } }

		[Constructable]
		public GothicCandelabraB() : base( 0x052E )
		{
			Name = "gothic candelabra";
			Duration = TimeSpan.Zero;
			BurntOut = false;
			Burning = false;
			Light = LightType.Circle225;
			Weight = 20.0;
		}

		public GothicCandelabraB( Serial serial ) : base( serial )
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