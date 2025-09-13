using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class BloodPactScroll : SpellScroll
	{
		[Constructable]
		public BloodPactScroll() : this( 1 )
		{
		}
		
		[Constructable]
		public BloodPactScroll( int amount ) : base( 140, 0x282F, amount )
		{
			Name = "blood pact elixir";
			Hue = 0x5B5;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Gives some of your life to another" );
			list.Add( 1049644, "Requires 25 Necromancy"); // PARENTHESIS
		}

		public BloodPactScroll( Serial serial ) : base( serial )
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
