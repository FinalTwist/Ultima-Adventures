using System;
using Server; 
using Server.Misc;

namespace Server.Items
{
	[Flipable( 0x544B, 0x544C )]
	public class WizardsStatue : Item
	{
		[Constructable]
		public WizardsStatue() : base( 0x544B )
		{
			Name = "Statue of " + NameList.RandomName( "evil mage" );
			Light = LightType.Circle225;
			Weight = 100.0;
			Hue = RandomThings.GetRandomMetalColor();
		}

		public WizardsStatue( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}