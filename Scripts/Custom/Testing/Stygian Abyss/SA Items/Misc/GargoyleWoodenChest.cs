using System;
using System.Collections.Generic;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;
using Server.Engines.Craft;

namespace Server.Items
{
    [Furniture]
    [Flipable(0x4025, 0x4026)]
    public class GargoyleWoodenChest : LockableContainer
    {
        [Constructable]
        public GargoyleWoodenChest()
            : base(0x4025)
        {
            Weight = 2.0;
            GumpID = 0x42;
        }

        public GargoyleWoodenChest(Serial serial)
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

            if (Weight == 15.0)
                Weight = 2.0;
        }
    }
}