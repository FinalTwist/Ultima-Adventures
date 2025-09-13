using System;

namespace Server.Items
{
	[FlipableAttribute( 0x1F7B, 0x1F7C )]
	public class CloudsOutterShirt : BaseMiddleTorso
	{
		[Constructable]
		public CloudsOutterShirt() : this( 0 )
		{
		}

		[Constructable]
		public CloudsOutterShirt( int hue ) : base( 0x1F7B, hue )
		{
			Weight = 2.0;
			Name = "Clouds outter shirt";
			Hue = 1176;
			LootType = LootType.Blessed;
		}

		public CloudsOutterShirt( Serial serial ) : base( serial )
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