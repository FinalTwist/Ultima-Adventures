using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items
{
    class OxidizingPowder : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063590;
            }
        }

        [Constructable]
        public OxidizingPowder() : base(0x0F8F)
        {
            Stackable = true;
            Weight = 1.0;
            Hue = 0xD7;
            Name = "oxidizing powder";
        }

        public OxidizingPowder(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); //version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

    }
}
