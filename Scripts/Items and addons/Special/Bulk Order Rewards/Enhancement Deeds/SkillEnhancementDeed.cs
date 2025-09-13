using System;
using Server.Items;

namespace Server.Custom
{
    public class SkillEnhancementDeed : EnhancementDeed
    {
        [Constructable]
        public SkillEnhancementDeed()
            : base(EnhancementType.Skill, 0)
        {
            Name = "a Skill Enhancement Deed";
        }

        public SkillEnhancementDeed(Serial serial)
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