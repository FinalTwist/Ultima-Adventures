using System;
using Server;

namespace Server.Items
{
	public class LichDust : Item
	{
		[Constructable]
		public LichDust() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public LichDust( int amount ) : base( 0xF8F )
		{
			Name = "lich dust";
			Stackable = true;
			Amount = amount;
			Hue = 0xB85;
		}

		public LichDust( Serial serial ) : base( serial )
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