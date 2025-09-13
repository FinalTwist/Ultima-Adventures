using System;
using Server;

namespace Server.Items
{
	public class LargeCrystal : Item
	{
		[Constructable]
		public LargeCrystal() : base( 0x1444 )
		{
			Name = "crystal";
			Light = LightType.Circle225;
			Weight = 20;
		}

		public LargeCrystal( Serial serial ) : base( serial )
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