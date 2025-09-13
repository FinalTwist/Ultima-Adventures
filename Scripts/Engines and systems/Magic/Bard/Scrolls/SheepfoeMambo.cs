using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SheepfoeMamboScroll : SpellScroll
	{
		[Constructable]
		public SheepfoeMamboScroll() : this( 1 )
		{
		}

		[Constructable]
		public SheepfoeMamboScroll( int amount ) : base( 365, 0x1F2D, amount )
		{
			Name = "shepherd's dance sheet music";
			Hue = 0x96;
			Stackable = true;
        }

		public SheepfoeMamboScroll( Serial serial ) : base( serial )
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