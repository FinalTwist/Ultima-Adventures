using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class EObsidianRockComponent : AddonComponent
    {
        [Constructable]
        public EObsidianRockComponent(int itemID)
            : base(itemID)
        {
            Weight = 100.0;
            Movable = false;
        }

        public override int LabelNumber { get { return 1076677; } }
        public EObsidianRockComponent(Serial serial)
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

    public class EObsidianRockAddon : BaseAddon
    {
        public override BaseAddonDeed Deed { get { return new EObsidianRockDeed(); } }

        [Constructable]
        public EObsidianRockAddon()
        {
            AddComponent(new EObsidianRockComponent(0x364E), 0, 0, 0);
        }

        public EObsidianRockAddon(Serial serial)
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

    public class EObsidianRockDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new EObsidianRockAddon(); } }

        [Constructable]
        public EObsidianRockDeed()
        {
			Name = "box containing an obsidian rock";
            ItemID = Utility.RandomList( 0x3420, 0x3425 );
            Hue = Server.Misc.RandomThings.GetRandomEvilColor();
            Weight = 5.0;
        }

        public EObsidianRockDeed(Serial serial)
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