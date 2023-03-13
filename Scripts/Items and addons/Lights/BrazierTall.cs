using System;
using Server;

namespace Server.Items
{
	public class BrazierTall : BaseLight
	{
		public override int LitItemID{ get { return 0x19AA; } }
		public override int UnlitItemID{ get { return 0x54D6; } }
		
		[Constructable]
		public BrazierTall() : base( 0x19AA )
		{
			Movable = true;
			Duration = TimeSpan.Zero; // Never burnt out
			Burning = true;
			Light = LightType.Circle300;
			Weight = 25.0;
		}

		public BrazierTall( Serial serial ) : base( serial )
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