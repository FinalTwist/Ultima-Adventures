using Server.Engines.BulkOrders;
using Server.Targeting;
using System;

namespace Server.Items
{
    public class BodMaterialRandomizerDeed : Item
    {
        [Constructable]
        public BodMaterialRandomizerDeed() : base(0x14F0)
        {
            Weight = 1.0;
            Name = "Bulk Order Deed Material Randomizer";
        }

        public BodMaterialRandomizerDeed(Serial serial) : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add("Randomizes the resource rarity");
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendMessage("This must be in your backpack to use");
                return;
            }

            from.SendMessage("Target a Bulk Order Deed to change the required resource");
            from.Target = new InternalTarget(this);
        }

        public void Upgrade(Mobile from, Item item)
        {
            if (item == null) return;

            if (TryRandomize(item as SmallBOD) || TryUpgrade(item as LargeBOD))
            {
                Delete();
                return;
            }

            from.SendMessage("You can't upgrade that.");
        }

        private BulkMaterialType Randomize(BulkMaterialType current, BulkMaterialType min, BulkMaterialType max)
        {
            if (current == min && current == max) return current;

            int i = 0;
            while (true && ++i < 50)
            {
                var random = (BulkMaterialType)Utility.RandomMinMax((int)min, (int)max);
                if (random != current) return random;
            }

            Console.WriteLine("Failed to randomize BOD material. {0}, {1}, {2}", current, min, max);

            return current;
        }

        private bool TryRandomize(SmallBOD bod)
        {
            if (bod == null) return false;
            if (bod.Material == BulkMaterialType.None) return false;

            if (bod is SmallSmithBOD)
            {
                bod.Material = Randomize(bod.Material, BulkMaterialType.DullCopper, BulkMaterialType.Dwarven);
                return true;
            }

            if (bod is SmallTailorBOD)
            {
                bod.Material = Randomize(bod.Material, BulkMaterialType.Horned, BulkMaterialType.Alien);
                return true;
            }

            if (bod is SmallCarpenterBOD || bod is SmallFletcherBOD)
            {
                bod.Material = Randomize(bod.Material, BulkMaterialType.Ash, BulkMaterialType.Elven);
                return true;
            }

            return false;
        }

        private bool TryUpgrade(LargeBOD bod)
        {
            if (bod == null) return false;
            if (bod.Material == BulkMaterialType.None) return false;

            if (bod is LargeSmithBOD)
            {
                bod.Material = Randomize(bod.Material, BulkMaterialType.DullCopper, BulkMaterialType.Dwarven);
                return true;
            }

            if (bod is LargeTailorBOD)
            {
                bod.Material = Randomize(bod.Material, BulkMaterialType.Horned, BulkMaterialType.Alien);
                return true;
            }

            if (bod is LargeCarpenterBOD || bod is LargeFletcherBOD)
            {
                bod.Material = Randomize(bod.Material, BulkMaterialType.Ash, BulkMaterialType.Elven);
                return true;
            }

            return false;
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

        private class InternalTarget : Target
        {
            private readonly BodMaterialRandomizerDeed _deed;

            public InternalTarget(BodMaterialRandomizerDeed deed) : base(1, false, TargetFlags.None)
            {
                _deed = deed;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                _deed.Upgrade(from, targeted as Item);
            }
        }
    }
}