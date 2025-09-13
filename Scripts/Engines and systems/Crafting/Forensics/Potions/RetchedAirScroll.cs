using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class RetchedAirScroll : SpellScroll
	{
		[Constructable]
		public RetchedAirScroll() : this( 1 )
		{
		}
		
		[Constructable]
		public RetchedAirScroll( int amount ) : base( 136, 0x282F, amount )
		{
			Name = "retched air elixir";
			Hue = 0xA97;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Creates a burst of harmful gas" );
			list.Add( 1049644, "Requires 20 Necromancy"); // PARENTHESIS
		}

		public RetchedAirScroll( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
}


