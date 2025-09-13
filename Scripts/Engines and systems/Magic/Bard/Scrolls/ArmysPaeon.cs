using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ArmysPaeonScroll : SpellScroll
	{
		[Constructable]
		public ArmysPaeonScroll() : this( 1 )
		{
		}

		[Constructable]
		public ArmysPaeonScroll( int amount ) : base( 351, 0x1F4C, amount )
		{
			Name = "army's paeon sheet music";
			Hue = 0x96;
			Stackable = true;
        }

		public ArmysPaeonScroll( Serial serial ) : base( serial )
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