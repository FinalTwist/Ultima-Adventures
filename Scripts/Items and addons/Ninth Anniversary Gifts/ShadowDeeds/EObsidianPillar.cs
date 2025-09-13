using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class EObsidianPillarComponent : AddonComponent
    {
        [Constructable]
        public EObsidianPillarComponent(int itemID)
            : base(itemID)
        {
            Weight = 100.0;
            Movable = false;
        }

        public override int LabelNumber { get { return 1076678; } }
        public EObsidianPillarComponent(Serial serial)
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

    public class EObsidianPillarAddon : BaseAddon
    {
        public override BaseAddonDeed Deed { get { return new EObsidianPillarDeed(); } }

        [Constructable]
        public EObsidianPillarAddon()
        {
            AddComponent(new EObsidianPillarComponent(0x364F), 0, 0, 0);
        }

        public EObsidianPillarAddon(Serial serial)
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

    public class EObsidianPillarDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new EObsidianPillarAddon(); } }

        [Constructable]
        public EObsidianPillarDeed()
        {
			Name = "box containing an obsidian pillar";
            ItemID = Utility.RandomList( 0x3420, 0x3425 );
            Hue = Server.Misc.RandomThings.GetRandomEvilColor();
            Weight = 5.0;
        }

        public EObsidianPillarDeed(Serial serial)
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