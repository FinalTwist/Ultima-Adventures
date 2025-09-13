using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class GraveyardGatewayScroll : SpellScroll
	{
		[Constructable]
		public GraveyardGatewayScroll() : this( 1 )
		{
		}
		
		[Constructable]
		public GraveyardGatewayScroll( int amount ) : base( 135, 0x282F, amount )
		{
			Name = "black gate draught";
			Hue = 0x2EA;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Creates a horrific black gate to another location" );
			list.Add( 1049644, "Requires 80 Necromancy"); // PARENTHESIS
		}

		public GraveyardGatewayScroll( Serial serial ) : base( serial )
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
