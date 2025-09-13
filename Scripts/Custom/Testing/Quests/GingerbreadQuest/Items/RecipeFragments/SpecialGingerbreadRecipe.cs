/*Created by Hammerhand*/

using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class SpecialGingerbreadRecipe : Item
    {
        public override string DefaultName
        {
            get { return "SpecialGingerbreadRecipe"; }
        }

        [Constructable]
        public SpecialGingerbreadRecipe()  : base(0xFF4)
        {
            base.Weight = 1.0;
            Hue = 0;
        }

        public SpecialGingerbreadRecipe(Serial serial)
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