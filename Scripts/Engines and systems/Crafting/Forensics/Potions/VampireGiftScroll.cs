using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class VampireGiftScroll : SpellScroll
	{
		[Constructable]
		public VampireGiftScroll() : this( 1 )
		{
		}

		[Constructable]
		public VampireGiftScroll( int amount ) : base( 139, 0x282F, amount )
		{
			Name = "vampire blood draught";
			Hue = 0xB85;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Vampire blood can resurrect others" );
			list.Add( 1049644, "Requires 80 Necromancy"); // PARENTHESIS
		}

		public VampireGiftScroll( Serial serial ) : base( serial )
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
