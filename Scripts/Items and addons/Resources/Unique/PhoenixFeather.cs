using System;
using Server;

namespace Server.Items
{
	public class PhoenixFeather : Item
	{
		[Constructable]
		public PhoenixFeather() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public PhoenixFeather( int amount ) : base( 0x4CCD )
		{
			Name = "phoenix feather";
			Stackable = true;
			Hue = 0xB71;
			Amount = amount;
		}

		public PhoenixFeather( Serial serial ) : base( serial )
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