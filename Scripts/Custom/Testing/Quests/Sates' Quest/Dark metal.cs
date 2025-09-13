using System;
using Server;

namespace Server.Items
{
	public class DarkMetal : Item
	{
		[Constructable]
		public DarkMetal() : this( 1 )
		{
		}

		[Constructable]
		public DarkMetal( int amount ) : base( 0xF13 )
		{
			Name = "Dark Metal";
			Hue = 1175;
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
		}

		public DarkMetal( Serial serial ) : base( serial )
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