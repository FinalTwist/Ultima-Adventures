using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;
namespace Server.Items
{
	public class UndeadEyesScroll : SpellScroll
	{
		[Constructable]
		public UndeadEyesScroll() : this( 1 )
		{
		}
		
		[Constructable]
		public UndeadEyesScroll( int amount ) : base( 137, 0x282F, amount )
		{
			Name = "eyes of the dead mixture";
			Hue = 0x491;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Gives one site of the dead, seeing in the dark" );
			list.Add( 1049644, "Requires 10 Necromancy"); // PARENTHESIS
		}

		public UndeadEyesScroll( Serial ser ) : base(ser)
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
