using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class TomeOfWands : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefWands.CraftSystem; } }

		public override int Hue{ get { return 0xB7E; } }

		[Constructable]
		public TomeOfWands() : base( 0x1AA3 )
		{
			Name = "tome of wands";
			Weight = 1.0;
			Hue = 0xB7E;
		}

		[Constructable]
		public TomeOfWands( int uses ) : base( uses, 0x1AA3 )
		{
			Weight = 1.0;
		}

		public TomeOfWands( Serial serial ) : base( serial )
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

			if ( Weight == 2.0 )
				Weight = 1.0;
		}
	}
}