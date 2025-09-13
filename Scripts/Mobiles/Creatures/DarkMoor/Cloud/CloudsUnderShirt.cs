using System;

namespace Server.Items
{
	[FlipableAttribute( 0x1efd, 0x1efe )]
	public class CloudsUnderShirt : BaseShirt
	{
		[Constructable]
		public CloudsUnderShirt() : this( 0 )
		{
		}

		[Constructable]
		public CloudsUnderShirt( int hue ) : base( 0x1EFD, hue )
		{
			Weight = 2.0;
			Name = "Clouds under shirt";
			Hue = 1176;
			LootType = LootType.Blessed;
		}

		public CloudsUnderShirt( Serial serial ) : base( serial )
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