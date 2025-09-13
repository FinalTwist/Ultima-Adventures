using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class CurseItem : LockableContainer
	{
		[Constructable]
		public CurseItem() : base( 0x9A8 )
		{
			Name = "cursed item";
			Locked = true;
			LockLevel = 1000;
			MaxLockLevel = 1000;
			RequiredSkill = 1000;
			Weight = 40.0;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Cursed Item");
			list.Add( 1049644, "Give to a Wizard or Knight to Remove the Curse"); // PARENTHESIS
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This item is cursed and cannot be used." );
		}

		public CurseItem( Serial serial ) : base( serial )
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
		}
	}
}