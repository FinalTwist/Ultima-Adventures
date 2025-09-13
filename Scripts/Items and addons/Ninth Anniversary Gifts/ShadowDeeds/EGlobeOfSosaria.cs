using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class EGlobeOfSosariaComponent : AddonComponent
    {
        [Constructable]
        public EGlobeOfSosariaComponent(int itemID)
            : base(itemID)
        {
            Weight = 100.0;
            Movable = false;
        }

        public override int LabelNumber { get { return 1076681; } }
        public EGlobeOfSosariaComponent(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(this.GetWorldLocation(), 2))
            {
                from.LocalOverheadMessage(MessageType.Regular, 906, 1019045); // I can't reach that.
            }
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

    public class EGlobeOfSosariaAddon : BaseAddon
    {
        public override BaseAddonDeed Deed { get { return new EGlobeOfSosariaDeed(); } }

        [Constructable]
        public EGlobeOfSosariaAddon()
        {
            AddComponent(new EGlobeOfSosariaComponent(0x3657), 1, 0, 0);
            AddComponent(new EGlobeOfSosariaComponent(0x3658), 0, 0, 0);
            AddComponent(new EGlobeOfSosariaComponent(0x3661), 1, 0, 0);
            AddComponent(new EGlobeOfSosariaComponent(0x3659), 1, -1, 0);
        }

        public EGlobeOfSosariaAddon(Serial serial)
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

    public class EGlobeOfSosariaDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new EGlobeOfSosariaAddon(); } }
        public override int LabelNumber { get { return 1076681; } }

        [Constructable]
        public EGlobeOfSosariaDeed()
        {
            ItemID = 0x14EF;
            Hue = 0x774;
            Weight = 1.0;
            LootType = LootType.Blessed;
        }

        public EGlobeOfSosariaDeed(Serial serial)
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