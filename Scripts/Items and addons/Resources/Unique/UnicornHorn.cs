using System;
using Server;

namespace Server.Items
{
	public class UnicornHorn : Item
	{
		[Constructable]
		public UnicornHorn() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public UnicornHorn( int amount ) : base( 0x2DB7 )
		{
			Name = "unicorn horn";
			Amount = amount;
			Hue = 0x47E;
			Stackable = true;
		}

		public UnicornHorn( Serial serial ) : base( serial )
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