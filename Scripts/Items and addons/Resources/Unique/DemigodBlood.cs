using System;
using Server;

namespace Server.Items
{
	public class DemigodBlood : Item
	{
		[Constructable]
		public DemigodBlood() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public DemigodBlood( int amount ) : base( 0xF0E )
		{
			Name = "demigod blood";
			Stackable = true;
			Amount = amount;
			Hue = 0x4AA;
		}

		public DemigodBlood( Serial serial ) : base( serial )
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