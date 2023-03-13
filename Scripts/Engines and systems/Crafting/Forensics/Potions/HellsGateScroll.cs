using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class HellsGateScroll : SpellScroll
	{
		[Constructable]
		public HellsGateScroll() : this( 1 )
		{
		}
		
		[Constructable]
		public HellsGateScroll( int amount ) : base( 142, 0x282F, amount )
		{
			Name = "demonic fire ooze";
			Hue = 0x54F;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Ignites a marked rune with power to transport one to that location" );
			list.Add( 1049644, "Requires 40 Necromancy"); // PARENTHESIS
		}

		public HellsGateScroll( Serial serial ) : base( serial )
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
