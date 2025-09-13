using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;

namespace Server.Items
{
	public class DoorSwitch : Item
	{
        private ArrayList m_doors;

		[Constructable]
        public DoorSwitch() : base(0x108F)
		{
			LootType = LootType.Blessed;
			Movable = true;
			Name = "Door locker";
			Hue = 38;
            m_doors = new ArrayList();
		}

		public DoorSwitch( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{

			base.GetProperties( list );
            int count = m_doors.Count;
            list.Add("Doors controlled: {0}", count);

		}

		public override void OnDoubleClick(Mobile m)
		{
            InvalidateProperties();
            m.SendMessage("Please target a door to add/remove or this item to open/close the doors.");
			m.Target = new AddDoor(m_doors);
            InvalidateProperties();
		}

        public void switchit(){
            BaseDoor oc;
            foreach(Item i in m_doors){
                oc = i as BaseDoor;
                if (oc.Open)
                {
                    oc.Open = false;
                }
                else
                {
                    oc.Open = true;
                }
            }
        }

		public class AddDoor : Target
		{
            private ArrayList door;
            public AddDoor(ArrayList m_doors) : base(15, false, TargetFlags.None)
			{
                door = m_doors;
			}

            public void switchit()
            {
                BaseDoor oc;
                foreach (Item i in door)
                {
                    oc = i as BaseDoor;
                    if (oc.Open)
                    {
                        oc.Open = false;
                    }
                    else
                    {
                        oc.Open = true;
                    }
                }
            }

			protected override void OnTarget( Mobile from, object targ)
			{
				if (targ is DoorSwitch)
				{
					switchit();
					return;
				}
				if (!(targ is BaseDoor))
				{
					from.SendMessage("That is not a door");
					return;
				}
				BaseDoor d = targ as BaseDoor;
				Item targ1 = targ as Item;
				if (!door.Contains(targ1))
				{
					door.Add(targ1);
					from.SendMessage("Door added!");
				}
				else
				{
					door.Remove(targ1);
					from.SendMessage("Door removed!");
				}
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

            writer.WriteItemList(m_doors);


		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
            m_doors = reader.ReadItemList();

		}

	}
}