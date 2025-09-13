using System;
using Server;
using Server.Items;
using System.Collections;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class WeededItem : LockableContainer
	{
		[Constructable]
		public WeededItem() : base( 0x9A8 )
		{
			Name = "weed covered item";
			Locked = true;
			LockLevel = 1000;
			MaxLockLevel = 1000;
			RequiredSkill = 1000;
			Weight = 0.1;
			Hue = 0xB87;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Wrapped in Thick Weeds");
			list.Add( 1049644, "Give to a alchemist or herbalist to remove"); // PARENTHESIS
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This item is wrapped in weeds and cannot be used." );
		}

		public WeededItem( Serial serial ) : base( serial )
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