using System;
using Server;

namespace Server.Items
{
	public class EnchantedSeaweed : Item
	{
		[Constructable]
		public EnchantedSeaweed() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public EnchantedSeaweed( int amount ) : base( 0x0A96 )
		{
			Name = "enchanted seaweed";
			Stackable = true;
			Amount = amount;
			Hue = 0x84E;
		}

		public EnchantedSeaweed( Serial serial ) : base( serial )
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
			ItemID = 0x0A96;
		}
	}
}