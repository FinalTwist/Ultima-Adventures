using System;
using Server.Items;

namespace Server.Custom
{
    public class AosEnhancementDeed : EnhancementDeed
    {
        [Constructable]
        public AosEnhancementDeed()
            : base(EnhancementType.Aos, 0)
        {
            Name = "an Aos Enhancement Deed";
        }

        public AosEnhancementDeed(Serial serial)
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