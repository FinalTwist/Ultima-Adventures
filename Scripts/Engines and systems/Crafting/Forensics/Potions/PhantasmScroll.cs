using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class PhantasmScroll : SpellScroll
	{
		[Constructable]
		public PhantasmScroll() : this( 1 )
		{
		}
		
		[Constructable]
		public PhantasmScroll( int amount ) : base( 146, 0x282F, amount )
		{
			Name = "phantasm elixir";
			Hue = 0x6DE;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Summons a spirit to disable a trap" );
			list.Add( 1049644, "Requires 15 Necromancy"); // PARENTHESIS
		}

		public PhantasmScroll( Serial serial ) : base( serial )
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
