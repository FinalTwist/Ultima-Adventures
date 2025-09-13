using System;
using Server;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x44D3, 0x44D4)]
    public class Lobster : Item, ICommodity
    {
        [Constructable]
        public Lobster() : this(1)
        {
        }

        [Constructable]
        public Lobster(int amount) : base(0x44D3)
        {
            Stackable = true;
            Amount = amount;
            Weight = 1.0;
        }

        public Lobster(Serial serial) : base(serial)
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
