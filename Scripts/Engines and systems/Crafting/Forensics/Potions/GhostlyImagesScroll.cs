using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class GhostlyImagesScroll : SpellScroll
	{
		[Constructable]
		public GhostlyImagesScroll() : this( 1 )
		{
		}

		[Constructable]
		public GhostlyImagesScroll( int amount ) : base( 143, 0x282F, amount )
		{
			Name = "ghostly images draught";
			Hue = 0xBF;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Creates an illusionary image of the user" );
			list.Add( 1049644, "Requires 45 Necromancy"); // PARENTHESIS
		}

		public GhostlyImagesScroll( Serial serial ) : base( serial )
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
