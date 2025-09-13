using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class NecroPoisonScroll : SpellScroll
	{
		[Constructable]
		public NecroPoisonScroll() : this( 1 )
		{
		}
		
		[Constructable]
		public NecroPoisonScroll( int amount ) : base( 141, 0x282F, amount )
		{
			Name = "disease draught";
			Hue = 0x4F8;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Causes one to become diseased" );
			list.Add( 1049644, "Requires 15 Necromancy"); // PARENTHESIS
		}

		public NecroPoisonScroll( Serial serial ) : base( serial )
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
