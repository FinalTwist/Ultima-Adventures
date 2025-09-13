using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{    
    public class ESpikePostEastComponent : AddonComponent
    {
        [Constructable]
        public ESpikePostEastComponent(int itemID)
            : base(itemID)
        {
            Weight = 100.0;
            Movable = false;
        }

        public override int LabelNumber { get { return 1076676; } }
        public ESpikePostEastComponent(Serial serial)
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
        
    public class ESpikePostEastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed { get { return new ESpikePostEastDeed(); } }

        [Constructable]
        public ESpikePostEastAddon()
        {
            AddComponent(new ESpikePostEastComponent(0x369C), 0, 0, 0);
        }

        public ESpikePostEastAddon(Serial serial)
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

    public class ESpikePostEastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new ESpikePostEastAddon(); } }

        [Constructable]
        public ESpikePostEastDeed()
        {
			Name = "box containing a spike post facing east";
            ItemID = Utility.RandomList( 0x3420, 0x3425 );
            Hue = Server.Misc.RandomThings.GetRandomEvilColor();
            Weight = 5.0;
        }

        public ESpikePostEastDeed(Serial serial)
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