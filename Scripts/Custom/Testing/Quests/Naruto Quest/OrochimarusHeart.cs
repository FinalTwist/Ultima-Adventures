using System;
using Server.Items;

namespace Server.Items
{
	public class OrochimarusHeart : Item
	{
		[Constructable]
		public OrochimarusHeart() : base( 7405 )
		{
			Movable = true;
			Name = "Heart of Orochimaru";
                  Hue = 1000;
		}

		public OrochimarusHeart( Serial serial ) : base( serial )
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
