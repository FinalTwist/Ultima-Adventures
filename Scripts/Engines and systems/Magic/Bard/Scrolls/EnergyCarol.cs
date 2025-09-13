using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EnergyCarolScroll : SpellScroll
	{
		[Constructable]
		public EnergyCarolScroll() : this( 1 )
		{
		}

		[Constructable]
		public EnergyCarolScroll( int amount ) : base( 353, 0x1F48, amount )
		{
			Name = "energy carol sheet music";
			Hue = 0x96;
			Stackable = true;
        }

		public EnergyCarolScroll( Serial serial ) : base( serial )
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