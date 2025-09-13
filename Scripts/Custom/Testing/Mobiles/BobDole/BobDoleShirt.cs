using System;

namespace Server.Items
{
	[FlipableAttribute( 0x1efd, 0x1efe )]
	public class BobDoleShirt : BaseShirt
	{
		[Constructable]
		public BobDoleShirt() : this( 0 )
		{
		}

		[Constructable]
		public BobDoleShirt( int hue ) : base( 0x1EFD, hue )
		{
			Weight = 2.0;
			Name = "Bob Dole's Shirt";
			Hue = 602;
		}

		public BobDoleShirt( Serial serial ) : base( serial )
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
