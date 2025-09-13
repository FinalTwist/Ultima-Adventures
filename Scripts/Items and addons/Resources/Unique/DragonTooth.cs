using System;
using Server;

namespace Server.Items
{
	public class DragonTooth : Item
	{
		[Constructable]
		public DragonTooth() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public DragonTooth( int amount ) : base( 0xF78 )
		{
			Name = "dragon tooth";
			Stackable = true;
			Amount = amount;
			Hue = 0x481;
		}

		public DragonTooth( Serial serial ) : base( serial )
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
			Name = "dragon tooth";
		}
	}
}