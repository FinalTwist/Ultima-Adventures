using System;

namespace Server.Items
{
	[FlipableAttribute( 0x1F7B, 0x1F7C )]
	public class OrochimaruShirt : BaseMiddleTorso
	{
		[Constructable]
		public OrochimaruShirt() : this( 0 )
		{
		}

		[Constructable]
		public OrochimaruShirt( int hue ) : base( 0x1F7B, hue )
		{
			Weight = 2.0;
			Name = "Orochimaru shirt";
			Hue = 1;
			LootType = LootType.Blessed;
		}

		public OrochimaruShirt( Serial serial ) : base( serial )
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