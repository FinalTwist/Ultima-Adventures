using System;
using Server;

namespace Server.Items
{
	public class PegasusFeather : Item
	{
		[Constructable]
		public PegasusFeather() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public PegasusFeather( int amount ) : base( 0x4CCD )
		{
			Name = "pegasus feather";
			Stackable = true;
			Amount = amount;
			Hue = 0xB5C;
		}

		public PegasusFeather( Serial serial ) : base( serial )
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