using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
    public class PianoAddon : BaseAddon
    {
        public override BaseAddonDeed Deed
        {
            get
            {
                return new PianoAddonDeed();
            }
        }

        [Constructable]
        public PianoAddon()
        {
            AddonComponent ac = null;
            ac = new AddonComponent(2928);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, -1, 1, 2);

            ac = new AddonComponent(5981);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, -1, 1, 6);

            ac = new AddonComponent(5984);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, -1, 1, 8);

            ac = new AddonComponent(5981);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, -1, 1, 7);

            ac = new AddonComponent(5985);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, -1, 1, 9);

            ac = new AddonComponent(5431);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, -1, 1, 10);

            ac = new AddonComponent(7933);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, -1, 1, 7);

            ac = new AddonComponent(2480);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, -1, 1, 11);

            ac = new AddonComponent(7883);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, -1, 0, 1);

            ac = new AddonComponent(2480);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, -1, -1, 2);

            ac = new AddonComponent(2924);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, -1, 0);

            ac = new AddonComponent(2925);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 0, 0);

            ac = new AddonComponent(4006);
            ac.Name = "Piano Keys";
            AddComponent(ac, 0, 0, 7);

            ac = new AddonComponent(5981);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 0, 10);

            ac = new AddonComponent(7933);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 0, 9);

            ac = new AddonComponent(5991);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 0, 9);

            ac = new AddonComponent(5988);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 0, 10);

            ac = new AddonComponent(5987);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 0, 8);

            ac = new AddonComponent(5988);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 0, 9);

            ac = new AddonComponent(2252);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 0, 11);

            ac = new AddonComponent(2923);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 1, 0);

            ac = new AddonComponent(2845);
            ac.Light = LightType.Circle225;
            ac.Name = "A Candelabra";
            AddComponent(ac, 0, 1, 17);

            ac = new AddonComponent(4006);
            ac.Name = "Piano Keys";
            AddComponent(ac, 0, 1, 7);

            ac = new AddonComponent(7031);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 1, 12);

            ac = new AddonComponent(7933);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 1, 14);

            ac = new AddonComponent(5986);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 1, 14);

            ac = new AddonComponent(5986);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 1, 12);

            ac = new AddonComponent(5991);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 1, 8);

            ac = new AddonComponent(5987);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 1, 9);

            ac = new AddonComponent(5985);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 0, 1, 10);

            ac = new AddonComponent(3774);
            ac.Name = "Sheet Music";
            AddComponent(ac, 1, 1, 15);

            ac = new AddonComponent(3772);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 1, 1, 12);

            ac = new AddonComponent(1114);
            ac.Hue = 1;
            ac.Name = "Piano";
            AddComponent(ac, 1, 0, 0);
        }

        public PianoAddon(Serial serial)
            : base(serial)
        {
        }

        public override void OnComponentUsed(AddonComponent ac, Mobile from)
        {
            if (!from.InRange(GetWorldLocation(), 1))
                from.SendMessage("You are too far away to use that!");
            else if (from.Mounted)
                from.SendMessage("You cannot play the piano while mounted!");
            else
            {
				if (!from.HasGump(typeof(SynthGump)))
					from.SendGump(new SynthGump(from));
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

    }

    public class PianoAddonDeed : BaseAddonDeed
    {
        public override BaseAddon Addon
        {
            get
            {
                return new PianoAddon();
            }
        }

        [Constructable]
        public PianoAddonDeed()
        {
            Name = "Piano";
        }

        public PianoAddonDeed(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
