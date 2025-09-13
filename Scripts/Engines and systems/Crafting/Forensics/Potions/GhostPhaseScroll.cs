using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class GhostPhaseScroll : SpellScroll
	{
		[Constructable]
		public GhostPhaseScroll() : this( 1 )
		{
		}
		
		[Constructable]
		public GhostPhaseScroll( int amount ) : base( 144, 0x282F, amount )
		{
			Name = "ghost phase concoction";
			Hue = 0x47E;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Turns your body into ghostly matter that reappears elsewhere" );
			list.Add( 1049644, "Requires 35 Necromancy"); // PARENTHESIS
		}

		public GhostPhaseScroll( Serial serial ) : base( serial )
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
