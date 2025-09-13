using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class IceThrenodyScroll : SpellScroll
	{
		[Constructable]
		public IceThrenodyScroll() : this( 1 )
		{
		}

		[Constructable]
		public IceThrenodyScroll( int amount ) : base( 359, 0x1F45, amount )
		{
			Name = "ice threnody sheet music";
			Hue = 0x96;
			Stackable = true;
        }

		public IceThrenodyScroll( Serial serial ) : base( serial )
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