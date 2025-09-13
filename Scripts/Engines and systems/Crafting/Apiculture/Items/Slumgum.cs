using System;

namespace Server.Items
{
	public class Slumgum : Item
	{
		[Constructable]
		public Slumgum() : this( 1 )
		{
		}

		[Constructable]
		public Slumgum( int amount ) : base( 5927 )
		{
			Weight = 1.0;
			Stackable = true;
			Amount = amount;
			Hue = 1126;
			Name = "Slumgum";
		}

		public Slumgum( Serial serial ) : base( serial )
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