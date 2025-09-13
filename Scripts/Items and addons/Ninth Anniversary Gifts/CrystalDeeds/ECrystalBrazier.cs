using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class ECrystalBrazierComponent : AddonComponent
    {
        [Constructable]
        public ECrystalBrazierComponent(int itemID)
            : base(itemID)
        {
            Weight = 100.0;
            Movable = false;
            Light = LightType.Circle225;
        }

        public override int LabelNumber { get { return 1076667; } }
        public ECrystalBrazierComponent(Serial serial)
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

    public class ECrystalBrazierAddon : BaseAddon
    {
        public override BaseAddonDeed Deed { get { return new ECrystalBrazierDeed(); } }

        [Constructable]
        public ECrystalBrazierAddon()
        {
            AddComponent(new ECrystalBrazierComponent(0x35EF), 0, 0, 0);
        }

        public ECrystalBrazierAddon(Serial serial)
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

    public class ECrystalBrazierDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new ECrystalBrazierAddon(); } }
        public override int LabelNumber { get { return 1076667; } }

        [Constructable]
        public ECrystalBrazierDeed()
        {
            ItemID = 0x14EF;
            Hue = 0x495;
            Weight = 1.0;
            LootType = LootType.Blessed;
        }

        public ECrystalBrazierDeed(Serial serial)
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