using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class EShadowFirePitCrossComponent : AddonComponent
    {
        [Constructable]
        public EShadowFirePitCrossComponent(int itemID)
            : base(itemID)
        {
            Weight = 100.0;
            Movable = false;
        }

        public override int LabelNumber { get { return 1076680; } }
        public EShadowFirePitCrossComponent(Serial serial)
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

    public class EShadowFirePitCrossAddon : BaseAddon
    {
        public override BaseAddonDeed Deed { get { return new EShadowFirePitCrossDeed(); } }

        [Constructable]
        public EShadowFirePitCrossAddon()
        {
	    AddonComponent ac;
	    ac = new AddonComponent( 0x3651 );
	    ac.Light = LightType.Circle225;
	    AddComponent( ac, 1, 0, 0 );
            AddComponent(new EShadowFirePitCrossComponent(0x3652), 0, 0, 0);
            AddComponent(new EShadowFirePitCrossComponent(0x3653), 1, -1, 0);
        }

        public EShadowFirePitCrossAddon(Serial serial)
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

    public class EShadowFirePitCrossDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new EShadowFirePitCrossAddon(); } }

        [Constructable]
        public EShadowFirePitCrossDeed()
        {
			Name = "box containing a crossed shadow fire pit";
            ItemID = Utility.RandomList( 0x3420, 0x3425 );
            Hue = Server.Misc.RandomThings.GetRandomEvilColor();
            Weight = 5.0;
        }

        public EShadowFirePitCrossDeed(Serial serial)
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