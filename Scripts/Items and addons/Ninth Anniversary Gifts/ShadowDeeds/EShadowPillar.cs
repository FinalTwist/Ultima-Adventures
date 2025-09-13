using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class EShadowPillarComponent : AddonComponent
    {
        [Constructable]
        public EShadowPillarComponent(int itemID)
            : base(itemID)
        {
            Weight = 100.0;
            Movable = false;
        }

        public override int LabelNumber { get { return 1076679; } }
        public EShadowPillarComponent(Serial serial)
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

    public class EShadowPillarAddon : BaseAddon
    {
        public override BaseAddonDeed Deed { get { return new EShadowPillarDeed(); } }

        [Constructable]
        public EShadowPillarAddon()
        {
            AddComponent(new EShadowPillarComponent(0x3650), 0, 0, 0);
        }

        public EShadowPillarAddon(Serial serial)
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

    public class EShadowPillarDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new EShadowPillarAddon(); } }

        [Constructable]
        public EShadowPillarDeed()
        {
			Name = "box containing a shadow pillar";
            ItemID = Utility.RandomList( 0x3420, 0x3425 );
            Hue = Server.Misc.RandomThings.GetRandomEvilColor();
            Weight = 5.0;
        }

        public EShadowPillarDeed(Serial serial)
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