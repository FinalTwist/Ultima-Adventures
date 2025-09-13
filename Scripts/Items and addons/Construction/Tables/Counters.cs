using System;

namespace Server.Items
{
    [Furniture]
    [Flipable(0xB3F, 0xB40)]
    public class CounterStained : Item
    {
        [Constructable]
        public CounterStained() : base(0xB3F)
        {
            Weight = 1.0;
        }

        public CounterStained(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Weight == 4.0)
                Weight = 1.0;
        }
    }
    
    [Furniture]
    [Flipable(0xB3D, 0xB3E)]
    public class CounterStainedMiddle : Item
    {
        [Constructable]
        public CounterStainedMiddle() : base(0xB3D)
        {
            Weight = 1.0;
        }

        public CounterStainedMiddle(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Weight == 4.0)
                Weight = 1.0;
        }
    }

    [Furniture]
    [Flipable(0x118F, 0x1191)]
    public class CounterStainedCloth : Item
    {
        [Constructable]
        public CounterStainedCloth() : base(0x1191)
        {
            Weight = 1.0;
        }

        public CounterStainedCloth(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Weight == 4.0)
                Weight = 1.0;
        }
    }

    [Furniture]
    [Flipable(0x1190, 0x1192)]
    public class CounterStainedMiddleCloth : Item
    {
        [Constructable]
        public CounterStainedMiddleCloth() : base(0x1192)
        {
            Weight = 1.0;
        }

        public CounterStainedMiddleCloth(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Weight == 4.0)
                Weight = 1.0;
        }
    }

    [Furniture]
    [Flipable(0x1667, 0x1669, 0x166A, 0x166C)]
    public class CounterCloth : Item
    {
        [Constructable]
        public CounterCloth() : base(0x1667)
        {
            Weight = 1.0;
        }

        public CounterCloth(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Weight == 4.0)
                Weight = 1.0;
        }
    }

    [Furniture]
    [Flipable(0x1668, 0x166B)]
    public class CounterMiddleCloth : Item
    {
        [Constructable]
        public CounterMiddleCloth() : base(0x1668)
        {
            Weight = 1.0;
        }

        public CounterMiddleCloth(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Weight == 4.0)
                Weight = 1.0;
        }
    }
}