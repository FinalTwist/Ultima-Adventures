using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MagicFinaleScroll : SpellScroll
	{
		[Constructable]
		public MagicFinaleScroll() : this( 1 )
		{
		}

		[Constructable]
		public MagicFinaleScroll( int amount ) : base( 362, 0x1F2E, amount )
		{
			Name = "magic finale sheet music";
			Hue = 0x96;
			Stackable = true;
        }

		public MagicFinaleScroll( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "The sheet music must be in your music book." );
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