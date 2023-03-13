using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class EShadowFirePitComponent : AddonComponent
    {
        [Constructable]
        public EShadowFirePitComponent(int itemID)
            : base(itemID)
        {
            Weight = 100.0;
            Movable = false;
        }

        public override int LabelNumber { get { return 1076680; } }
        public EShadowFirePitComponent(Serial serial)
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

    public class EShadowFirePitAddon : BaseAddon
    {
        public override BaseAddonDeed Deed { get { return new EShadowFirePitDeed(); } }

        [Constructable]
        public EShadowFirePitAddon()
        {
	    AddonComponent ac;
	    ac = new AddonComponent( 0x3654 );
	    ac.Light = LightType.Circle225;
	    AddComponent( ac, 1, 0, 0 );
            AddComponent(new EShadowFirePitComponent(0x3655), 0, 0, 0);
            AddComponent(new EShadowFirePitComponent(0x3656), 1, -1, 0);
        }

        public EShadowFirePitAddon(Serial serial)
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

    public class EShadowFirePitDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new EShadowFirePitAddon(); } }

        [Constructable]
        public EShadowFirePitDeed()
        {
			Name = "box containing a shadow fire pit";
            ItemID = Utility.RandomList( 0x3420, 0x3425 );
            Hue = Server.Misc.RandomThings.GetRandomEvilColor();
            Weight = 5.0;
        }

        public EShadowFirePitDeed(Serial serial)
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