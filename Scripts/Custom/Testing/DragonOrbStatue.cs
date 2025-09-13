using System;
using Server; 
using Server.Misc;

namespace Server.Items
{
	[Flipable( 0x544D, 0x544E )]
	public class DragonOrbStatue : Item
	{
		[Constructable]
		public DragonOrbStatue() : base( 0x544D )
		{
			Name = "Statue of " + NameList.RandomName( "dragon" );
			Light = LightType.Circle225;
			Weight = 100.0;
			Hue = Misc.RandomThings.GetRandomColor(0);
		}

		public DragonOrbStatue( Serial serial ) : base( serial )
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