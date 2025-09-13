using System;
using Server;

namespace Server.Items
{
	public class PolishedBone : Item
	{
		[Constructable]
		public PolishedBone() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public PolishedBone( int amount ) : base( 0x22C3 )
		{
			Name = "polished bone";
			Stackable = true;
			Amount = amount;
		}

		public PolishedBone( Serial serial ) : base( serial )
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