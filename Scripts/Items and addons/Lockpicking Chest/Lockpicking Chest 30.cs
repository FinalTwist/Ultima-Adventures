using System;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
    public class LockpickingChest30 : LockableContainer
    {
        private bool m_Locked;

        [Constructable]
        public LockpickingChest30(): base(0x9AA)
        {
            Locked = true;
            LockLevel = 30;
            RequiredSkill = 30;
		Name = "Rusted Lockpicking Chest";
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
        public LockpickingChest30(Serial serial)
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