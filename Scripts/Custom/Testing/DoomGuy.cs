using System;
using Server; 
using Server.Misc;

namespace Server.Items
{
	[Flipable( 0x555E, 0x555F )]
	public class DoomGuy : Item
	{
		[Constructable]
		public DoomGuy() : base( 0x555E )
		{
			Name = "doom guy Statue";
			//Light = LightType.Circle225;
			Weight = 100.0;
			//Hue = Misc.RandomThings.GetRandomColor(0);
		}

		public DoomGuy( Serial serial ) : base( serial )
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