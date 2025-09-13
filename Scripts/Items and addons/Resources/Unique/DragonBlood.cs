using System;
using Server;

namespace Server.Items
{
	public class DragonBlood : Item
	{
		[Constructable]
		public DragonBlood() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public DragonBlood( int amount ) : base( 0xF7D )
		{
			Name = "dragon blood";
			Stackable = true;
			Amount = amount;
			Hue = 0x4AA;
		}

		public DragonBlood( Serial serial ) : base( serial )
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