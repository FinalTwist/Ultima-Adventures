using System;
using Server.Items;

namespace Server.Items
{
	[Flipable( 0x27A0, 0x27EB )]
	public class BobDoleBelt : BaseWaist
	{
		[Constructable]
		public BobDoleBelt() : this( 0 )
		{
			Name = "Bob Dole's Belt";
			Hue = 643;
		}

		[Constructable]
		public BobDoleBelt( int hue ) : base( 0x27A0, hue )
		{
			Weight = 1.0;
		}

		public BobDoleBelt( Serial serial ) : base( serial )
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
