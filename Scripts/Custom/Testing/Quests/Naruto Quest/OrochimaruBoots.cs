using System;

namespace Server.Items
{

	public class OrochimaruBoots : BaseShoes
	{
		[Constructable]
		public OrochimaruBoots() : this( 0 )
		{
		}

		[Constructable]
		public OrochimaruBoots( int hue ) : base( 0x2307, hue )
		{
			Weight = 3.0;
			Name = "Orochimaru boots";
			Hue = 1;
			LootType = LootType.Blessed;
		}

		public OrochimaruBoots( Serial serial ) : base( serial )
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