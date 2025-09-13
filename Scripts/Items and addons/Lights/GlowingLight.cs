using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public class GlowingLight : Item
	{
		[Constructable]
		public GlowingLight() : base( 0x1647 )
		{
			Name = "glowing light";
			Light = LightType.Circle300;
			Movable = false;
		}

		public GlowingLight( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}