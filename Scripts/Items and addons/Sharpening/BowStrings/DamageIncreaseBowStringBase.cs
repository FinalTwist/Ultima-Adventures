namespace Server.Items
{
    public abstract class DamageIncreaseBowStringBase : AddDamageItemBase
    {
        public DamageIncreaseBowStringBase(Serial serial) : base(serial)
        {
        }

        protected DamageIncreaseBowStringBase(int uses) : base(uses, 0x543A)
        {
        }

        protected override void AfterItemDeleted(Mobile from)
        {
            from.SendMessage(32, "You use the last of the string");
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add("Adds damage increase");
            list.Add(1049644, "Only usable on ranged weapons"); // Parentheses
        }

        protected override bool Validate(Mobile from, BaseWeapon weapon)
        {
            if (!base.Validate(from, weapon)) return false;

            if (false == weapon is BaseRanged)
            {
                from.SendMessage(32, "You may only use this on ranged weapons");
                return false;
            }

            return true;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            if (IsLegacyItem) return;

            int version = reader.ReadInt();
        }
    }
}