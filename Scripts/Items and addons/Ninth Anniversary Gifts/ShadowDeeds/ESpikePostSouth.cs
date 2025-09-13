using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{    
    public class ESpikePostSouthComponent : AddonComponent
    {
        [Constructable]
        public ESpikePostSouthComponent(int itemID)
            : base(itemID)
        {
            Weight = 100.0;
            Movable = false;
        }

        public override int LabelNumber { get { return 1076676; } }
        public ESpikePostSouthComponent(Serial serial)
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

    public class ESpikePostSouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed { get { return new ESpikePostSouthDeed(); } }

        [Constructable]
        public ESpikePostSouthAddon()
        {
            AddComponent(new ESpikePostSouthComponent(0x364D), 0, 0, 0);
        }

        public ESpikePostSouthAddon(Serial serial)
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

    public class ESpikePostSouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new ESpikePostSouthAddon(); } }
        public override int LabelNumber { get { return 1076676; } }

        [Constructable]
        public ESpikePostSouthDeed()
        {
			Name = "box containing a spike post facing south";
            ItemID = Utility.RandomList( 0x3420, 0x3425 );
            Hue = Server.Misc.RandomThings.GetRandomEvilColor();
            Weight = 5.0;
        }

        public ESpikePostSouthDeed(Serial serial)
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