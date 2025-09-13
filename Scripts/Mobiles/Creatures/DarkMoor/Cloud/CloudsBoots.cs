using System;

namespace Server.Items
{
	[FlipableAttribute( 0x170b, 0x170c )]
	public class CloudsBoots : BaseShoes
	{
		[Constructable]
		public CloudsBoots() : this( 0 )
		{
		}

		[Constructable]
		public CloudsBoots( int hue ) : base( 0x170B, hue )
		{
			Weight = 3.0;
			Name = "Clouds boots";
			Hue = 1176;
			LootType = LootType.Blessed;
		}

		public CloudsBoots( Serial serial ) : base( serial )
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