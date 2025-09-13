using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EnchantingEtudeScroll : SpellScroll
	{
		[Constructable]
		public EnchantingEtudeScroll() : this( 1 )
		{
		}

		[Constructable]
		public EnchantingEtudeScroll( int amount ) : base( 352, 0x1F4A, amount )
		{
			Name = "enchanting etude sheet music";
			Hue = 0x96;
			Stackable = true;
        }

		public EnchantingEtudeScroll( Serial serial ) : base( serial )
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