using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class ManaLeechScroll : SpellScroll
	{
		[Constructable]
		public ManaLeechScroll() : this( 1 )
		{
		}
		
		[Constructable]
		public ManaLeechScroll( int amount ) : base( 132, 0x282F, amount )
		{
			Name = "lich leech mixture";
			Hue = 0xB87;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Absorbs mana from the target" );
			list.Add( 1049644, "Requires 20 Necromancy"); // PARENTHESIS
		}

		public ManaLeechScroll( Serial serial ) : base( serial )
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
