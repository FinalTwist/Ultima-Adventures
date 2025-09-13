using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class WallOfSpikesScroll : SpellScroll
	{
		[Constructable]
		public WallOfSpikesScroll() : this( 1 )
		{
		}
		
		[Constructable]
		public WallOfSpikesScroll( int amount ) : base( 138, 0x282F, amount )
		{
			Name = "wall of spikes draught";
			Hue = 0xB8F;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}
	
        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Creates a protective wall of spikes" );
			list.Add( 1049644, "Requires 24 Necromancy"); // PARENTHESIS
		}

		public WallOfSpikesScroll( Serial serial ) : base( serial )
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
