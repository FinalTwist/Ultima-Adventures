using System;

namespace Server.Items
{
	[FlipableAttribute( 0x1efd, 0x1efe )]
	public class OrochimaruUnderShirt : BaseShirt
	{
		[Constructable]
		public OrochimaruUnderShirt() : this( 0 )
		{
		}

		[Constructable]
		public OrochimaruUnderShirt( int hue ) : base( 0x1EFD, hue )
		{
			Weight = 2.0;
			Name = "Orochimaru under shirt";
			Hue = 1;
			LootType = LootType.Blessed;
		}

		public OrochimaruUnderShirt( Serial serial ) : base( serial )
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