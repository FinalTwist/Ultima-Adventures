using System;
using Server.Items;

namespace Server.Custom
{
    public class ArmorEnhancementDeed : EnhancementDeed
    {
        [Constructable]
        public ArmorEnhancementDeed()
            : base(EnhancementType.Armor, 0)
        {
            Name = "an Armor Enhancement Deed";
        }

        public ArmorEnhancementDeed(Serial serial)
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