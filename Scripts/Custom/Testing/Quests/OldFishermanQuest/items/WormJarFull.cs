using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class WormJarFull : Item
	{
		[Constructable]
		public WormJarFull() : base( 0x1007 )
		{
                  Name = "Bait Jar (It's Full)";
                  Hue = 1113;
		  Weight = 1.0;
			
		}

		public WormJarFull( Serial serial ) : base( serial )
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