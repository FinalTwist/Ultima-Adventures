using System;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
    public class LockpickingChest10 : LockableContainer
    {
        private bool m_Locked;

        [Constructable]
        public LockpickingChest10(): base(0x9AA)
        {
            Locked = true;
            LockLevel = 10;
		Name = "Delapitated Lockpicking Chest";
            RequiredSkill = 10;
            Weight = 4.0;

        }

        //public override void OnSingleClick(Mobile from)
        //{
        //    if (!m_Locked)
        //        m_Locked = true;
        //}

        public override void LockPick(Mobile from)
        {
            this.Locked = true;
            from.SendMessage("The container magically relocks it self.");
        }
        public LockpickingChest10(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}