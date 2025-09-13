using System;

namespace Server.Items
{
	public class Krystal : Item
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public Krystal() : this( 1 )
		{
		}

		[Constructable]
		public Krystal( int amount ) : base( 0xF13 )
		{
			Hue = 0x489;
			Name = "krystal";
			Stackable = true;
			Amount = amount;
			Light = LightType.Circle150;
		}

		public Krystal( Serial serial ) : base( serial )
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