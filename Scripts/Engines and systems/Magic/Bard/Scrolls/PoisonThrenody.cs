using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class PoisonThrenodyScroll : SpellScroll
	{
		[Constructable]
		public PoisonThrenodyScroll() : this( 1 )
		{
		}

		[Constructable]
		public PoisonThrenodyScroll( int amount ) : base( 364, 0x1F32, amount )
		{
			Name = "poison threnody sheet music";
			Hue = 0x96;
			Stackable = true;
        }

		public PoisonThrenodyScroll( Serial serial ) : base( serial )
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