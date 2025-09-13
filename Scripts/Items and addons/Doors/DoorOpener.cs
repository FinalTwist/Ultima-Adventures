using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;

namespace Server.Items
{
	public class DoorOpener : Item
	{
		[Constructable]
		public DoorOpener() : base( 0x4336 )
		{
			ItemID = Utility.RandomList( 0x4336, 0x4337 );
			Movable = false;
			Name = "a door opener";
			Visible = false;
		}

		public DoorOpener(Serial serial) : base(serial)
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
            ArrayList list = new ArrayList();
            foreach (  Item item in m.GetItemsInRange( 2 ) )
            {
                if ( item is BaseDoor )
                    list.Add( item );
            }
            foreach ( Item item in list )
			{
				BaseDoor iDoor = (BaseDoor)item;
				iDoor.Open = true;
			}
			return true;
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