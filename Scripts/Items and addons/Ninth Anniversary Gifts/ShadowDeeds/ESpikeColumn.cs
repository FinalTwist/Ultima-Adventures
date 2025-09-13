using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class ESpikeColumnComponent : AddonComponent
    {
        [Constructable]
        public ESpikeColumnComponent(int itemID)
            : base(itemID)
        {
            Weight = 100.0;
            Movable = false;
        }

        public override int LabelNumber { get { return 1076675; } }
        public ESpikeColumnComponent(Serial serial)
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

    public class ESpikeColumnAddon : BaseAddon
    {
        public override BaseAddonDeed Deed { get { return new ESpikeColumnDeed(); } }

        [Constructable]
        public ESpikeColumnAddon()
        {
            AddComponent(new ESpikeColumnComponent(0x364C), 0, 0, 0);
        }

        public ESpikeColumnAddon(Serial serial)
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

    public class ESpikeColumnDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new ESpikeColumnAddon(); } }
        public override int LabelNumber { get { return 1076675; } }

        [Constructable]
        public ESpikeColumnDeed()
        {
			Name = "box containing a spike column";
            ItemID = Utility.RandomList( 0x3420, 0x3425 );
            Hue = Server.Misc.RandomThings.GetRandomEvilColor();
            Weight = 5.0;
        }

        public ESpikeColumnDeed(Serial serial)
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

			if ( ItemID != 0x3420 && ItemID != 0x3425 ){ ItemID = 0x3425; }
        }
    }
}