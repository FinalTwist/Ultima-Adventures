using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class IceCarolScroll : SpellScroll
	{
		[Constructable]
		public IceCarolScroll() : this( 1 )
		{
		}

		[Constructable]
		public IceCarolScroll( int amount ) : base( 358, 0x1F34, amount )
		{
			Name = "ice carol sheet music";
			Hue = 0x96;
			Stackable = true;
        }

		public IceCarolScroll( Serial serial ) : base( serial )
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