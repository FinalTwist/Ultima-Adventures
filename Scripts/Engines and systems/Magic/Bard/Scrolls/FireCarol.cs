using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class FireCarolScroll : SpellScroll
	{
		[Constructable]
		public FireCarolScroll() : this( 1 )
		{
		}

		[Constructable]
		public FireCarolScroll( int amount ) : base( 355, 0x1F49, amount )
		{
			Name = "fire carol sheet music";
			Hue = 0x96;
			Stackable = true;
        }

		public FireCarolScroll( Serial serial ) : base( serial )
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