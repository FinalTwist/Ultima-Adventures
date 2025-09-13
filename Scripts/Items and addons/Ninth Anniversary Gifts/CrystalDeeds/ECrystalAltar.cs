using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class ECrystalAltarComponent : AddonComponent
    {
        [Constructable]
        public ECrystalAltarComponent(int itemID)
            : base(itemID)
        {
            Weight = 100.0;
            Movable = false;
        }

        public override int LabelNumber { get { return 1076672; } }
        public ECrystalAltarComponent(Serial serial)
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

    public class ECrystalAltarAddon : BaseAddon
    {
        public override BaseAddonDeed Deed { get { return new ECrystalAltarDeed(); } }

        [Constructable]
        public ECrystalAltarAddon()
        {
            AddComponent(new ECrystalAltarComponent(0x3DA2), 0, -1, 0);
            AddComponent(new ECrystalAltarComponent(0x3603), 0, 0, 0);
            AddComponent(new ECrystalAltarComponent(0x3602), 1, 0, 0);
            AddComponent(new ECrystalAltarComponent(0x3604), 1, -1, 0);
        }

        public ECrystalAltarAddon(Serial serial)
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

    public class ECrystalAltarDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new ECrystalAltarAddon(); } }
        public override int LabelNumber { get { return 1076672; } }

        [Constructable]
        public ECrystalAltarDeed()
        {
            ItemID = 0x14EF;
            Hue = 0x495;
            Weight = 1.0;
            LootType = LootType.Blessed;
        }

        public ECrystalAltarDeed(Serial serial)
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