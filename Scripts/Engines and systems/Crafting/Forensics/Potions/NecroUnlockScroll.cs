using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class NecroUnlockScroll : SpellScroll
	{
		[Constructable]
		public NecroUnlockScroll() : this( 1 )
		{
		}
		
		[Constructable]
		public NecroUnlockScroll( int amount ) : base( 145, 0x282F, amount )
		{
			Name = "tomb raiding concoction";
			Hue = 0x493;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{

		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Summons the spirits to unlock something" );
			list.Add( 1049644, "Requires 15 Necromancy"); // PARENTHESIS
		}

		public NecroUnlockScroll( Serial serial ) : base( serial )
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
