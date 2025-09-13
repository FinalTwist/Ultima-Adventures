using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class HellsBrandScroll : SpellScroll
	{
		[Constructable]
		public HellsBrandScroll() : this( 1 )
		{
		}

		[Constructable]
		public HellsBrandScroll( int amount ) : base( 134, 0x282F, amount )
		{
			Name = "hellish branding ooze";
			Hue = 0x54C;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Marks a rune location with symbols of evil" );
			list.Add( 1049644, "Requires 55 Necromancy"); // PARENTHESIS
		}

		public HellsBrandScroll( Serial serial ) : base( serial )
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
