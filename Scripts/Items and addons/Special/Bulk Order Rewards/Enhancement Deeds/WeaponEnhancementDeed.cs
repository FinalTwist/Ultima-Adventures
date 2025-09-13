using System;
using Server.Items;

namespace Server.Custom
{
    public class WeaponEnhancementDeed : EnhancementDeed
    {
        [Constructable]
        public WeaponEnhancementDeed()
            : base(EnhancementType.Weapon, 0)
        {
            Name = "a Weapon Enhancement Deed";
        }

        public WeaponEnhancementDeed(Serial serial)
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