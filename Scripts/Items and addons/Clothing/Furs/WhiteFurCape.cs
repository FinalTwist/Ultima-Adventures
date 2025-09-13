using System;

namespace Server.Items
{
	[Flipable( 0x2B76, 0x316D )]
	public class WhiteFurCape : Cloak
	{
		[Constructable]
		public WhiteFurCape() : base( 0x2B76 )
		{
			Name = "fur cape";
			Hue = 0x481;
			Weight = 4.0;
			Resistances.Cold = 15;
		}

		public WhiteFurCape( Serial serial ) : base( serial )
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