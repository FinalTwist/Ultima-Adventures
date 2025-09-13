using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    public class GibberHead : Item
    {
        [Constructable]
        public GibberHead()
            : base(7960)
        {
            Weight = 5.0;
            Name = "a gibberling queen head";
            Hue = 0;
        }

        public override void OnDoubleClick(Mobile from)
        {
        }

        public GibberHead(Serial serial)
            : base(serial)
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
        }
    }
}