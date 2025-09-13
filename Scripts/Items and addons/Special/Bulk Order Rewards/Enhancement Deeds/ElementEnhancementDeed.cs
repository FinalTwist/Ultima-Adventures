using System;
using Server.Items;

namespace Server.Custom
{
    public class ElementEnhancementDeed : EnhancementDeed
    {
        [Constructable]
        public ElementEnhancementDeed()
            : base(EnhancementType.Element, 0)
        {
            Name = "an Element Enhancement Deed";
        }

        public ElementEnhancementDeed(Serial serial)
            : base(serial)
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

    }
}