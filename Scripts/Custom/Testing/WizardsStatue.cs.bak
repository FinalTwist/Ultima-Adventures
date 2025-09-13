using System;
using Server; 
using Server.Misc;

namespace Server.Items
{
	[Flipable( 0x5465, 0x5466 )]
	public class WizardsStatue : Item
	{
		[Constructable]
		public WizardsStatue() : base( 0x5465 )
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