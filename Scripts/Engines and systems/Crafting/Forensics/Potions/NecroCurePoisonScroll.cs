using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class NecroCurePoisonScroll : SpellScroll
	{
		[Constructable]
		public NecroCurePoisonScroll() : this( 1 )
		{
		}
		
		[Constructable]
		public NecroCurePoisonScroll( int amount ) : base( 133, 0x282F, amount )
		{
			Name = "disease curing concoction";
			Hue = 0x8A2;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Cures one of diseases" );
			list.Add( 1049644, "Requires 25 Necromancy"); // PARENTHESIS
		}

		public NecroCurePoisonScroll( Serial serial ) : base( serial )
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
