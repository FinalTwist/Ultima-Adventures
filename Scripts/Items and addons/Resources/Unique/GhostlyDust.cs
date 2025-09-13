using System;
using Server;

namespace Server.Items
{
	public class GhostlyDust : Item
	{
		[Constructable]
		public GhostlyDust() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public GhostlyDust( int amount ) : base( 0xF8C )
		{
			Name = "ghostly dust";
			Stackable = true;
			Amount = amount;
			Hue = 1150;
		}

		public GhostlyDust( Serial serial ) : base( serial )
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