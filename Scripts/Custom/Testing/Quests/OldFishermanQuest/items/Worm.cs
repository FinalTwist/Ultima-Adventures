using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class Worm : Item
	{
		[Constructable]
		public Worm() : base( 0x1085 )
		{
                  Name = "a worm";
                  Hue = 1114;
			
		}

		public Worm( Serial serial ) : base( serial )
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