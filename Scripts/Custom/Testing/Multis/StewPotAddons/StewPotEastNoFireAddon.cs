/* Created by Hammerhand*/

using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class StewPotEastNoFireAddon : BaseAddon
    {
        public override BaseAddonDeed Deed
        {
            get
            {
                return new StewPotEastNoFireAddonDeed();
            }
        }

        [Constructable]
        public StewPotEastNoFireAddon()
        {
            AddonComponent ac;
            ac = new AddonComponent(2416);
            ac.Name = "Stew";

            AddComponent(ac, 0, 0, 8);

            ac = new AddonComponent(2421);
            AddComponent(ac, 0, 0, 0);

        }

        public override void OnComponentUsed(AddonComponent ac, Mobile from)
        {
            if (!from.InRange(GetWorldLocation(), 2))
                from.SendMessage("You are too far away.");
            else
            {
                {
                    from.SendMessage("You dish up a nice hot bowl of hearty stew");
                    from.AddToBackpack(new WoodenBowlOfStew());
                }
            }
        }

        public StewPotEastNoFireAddon(Serial serial)
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

    public class StewPotEastNoFireAddonDeed : BaseAddonDeed
    {
        public override BaseAddon Addon
        {
            get
            {
                return new StewPotEastNoFireAddon();
            }
        }

        [Constructable]
        public StewPotEastNoFireAddonDeed()
        {
            Name = "a StewPot deed";
        }

        public StewPotEastNoFireAddonDeed(Serial serial)
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
            switch (version)
            {
                case 0:
                    {
                        break;
                    }
            }
        }
    }
}