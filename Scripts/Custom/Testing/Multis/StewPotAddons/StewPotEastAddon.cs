/* Created by Hammerhand*/

using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class StewPotEastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed
        {
            get
            {
                return new StewPotEastAddonDeed();
            }
        }

        [Constructable]
        public StewPotEastAddon()
        {
            AddonComponent ac;
            ac = new AddonComponent(2416);
            ac.Name = "Stew";

            AddComponent(ac, 0, 0, 8);

            ac = new AddonComponent(2421);
            AddComponent(ac, 0, 0, 0);


            ac = new AddonComponent(4012);
            ac.Light = LightType.Circle150;
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

        public StewPotEastAddon(Serial serial)
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

    public class StewPotEastAddonDeed : BaseAddonDeed
    {
        public override BaseAddon Addon
        {
            get
            {
                return new StewPotEastAddon();
            }
        }

        [Constructable]
        public StewPotEastAddonDeed()
        {
            Name = "a StewPot deed";
        }

        public StewPotEastAddonDeed(Serial serial)
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