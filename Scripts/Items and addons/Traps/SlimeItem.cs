using System;
using Server;
using Server.Items;
using System.Collections;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class SlimeItem : LockableContainer
	{
		[Constructable]
		public SlimeItem() : base( 0x9A8 )
		{
			Name = "slime covered item";
			Locked = true;
			LockLevel = 1000;
			MaxLockLevel = 1000;
			RequiredSkill = 1000;
			Weight = 0.1;
			//Hue = 0xB85;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Covered in Thick Slime");
			list.Add( 1049644, "Give to an inn or tavern servant to clean"); // PARENTHESIS
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This item is covered in slime and cannot be used." );
		}

		public SlimeItem( Serial serial ) : base( serial )
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