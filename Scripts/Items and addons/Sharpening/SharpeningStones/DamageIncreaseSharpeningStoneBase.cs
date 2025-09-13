namespace Server.Items
{
    public abstract class DamageIncreaseSharpeningStoneBase : AddDamageItemBase
    {
        public DamageIncreaseSharpeningStoneBase(Serial serial) : base(serial)
        {
        }

        protected DamageIncreaseSharpeningStoneBase(int uses) : base(uses, 0x1F14)
        {
        }

        protected override void AfterItemDeleted(Mobile from)
        {
            from.SendMessage(32, "You use the last of the sharpening stone");
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add("Adds damage increase");
            list.Add(1049644, "Only usable on bladed weapons"); // Parentheses
        }

        protected override bool Validate(Mobile from, BaseWeapon weapon)
        {
            if (!base.Validate(from, weapon)) return false;

            if (false == (weapon is BaseSword || weapon is BaseKnife || weapon is BaseAxe || weapon is BaseSpear))
            {
                from.SendMessage(32, "You may only use this on bladed weapons");
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