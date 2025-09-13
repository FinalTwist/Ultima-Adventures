using System;
using Server;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x44D1, 0x44D2)]
    public class Crab : Item, ICommodity
    {
        [Constructable]
        public Crab() : this(1)
        {
        }

        [Constructable]
        public Crab(int amount) : base(0x44D1)
        {
            Stackable = true;
            Amount = amount;
            Weight = 1.0;
        }

        public Crab(Serial serial) : base(serial)
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

        public int DescriptionNumber
        {
            get { return LabelNumber; }
        }

        public bool IsDeedable
        {
            get { return true; }
        }
    }
}
