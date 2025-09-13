using System;
using Server.Items;

namespace Server.Items
{
	[Flipable( 0x279B, 0x27E6 )]
	public class CloudsPants : BasePants
	{
		[Constructable]
		public CloudsPants() : this( 0 )
		{
		}

		[Constructable]
		public CloudsPants( int hue ) : base( 0x279B, hue )
		{
			Weight = 2.0;
			Hue = 1176;
			Name = "Clouds Pants";
			LootType = LootType.Blessed;
		}

		public CloudsPants( Serial serial ) : base( serial )
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