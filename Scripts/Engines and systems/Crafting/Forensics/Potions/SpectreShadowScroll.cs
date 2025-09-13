using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class SpectreShadowScroll : SpellScroll
	{
		[Constructable]
		public SpectreShadowScroll() : this( 1 )
		{
		}
		
		[Constructable]
		public SpectreShadowScroll( int amount ) : base( 131, 0x282F, amount )
		{
			Name = "spectre shadow elixir";
			Hue = 0x17E;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Can turn anyone invisible" );
			list.Add( 1049644, "Requires 28 Necromancy"); // PARENTHESIS
		}

		public SpectreShadowScroll( Serial serial ) : base( serial )
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