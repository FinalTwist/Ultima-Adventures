namespace Server.Items
{
    public class ElementalWeightingStone : SplitElementalItemBase
    {
        [Constructable]
        public ElementalWeightingStone() : base(0x1F14)
        {
            Name = "Elemental Weighting Stone";
        }

        public ElementalWeightingStone(Serial serial) : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add("Only usable on blunt weapons");
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.Skills[SkillName.Blacksmith].Value < 100.0 || from.Skills[SkillName.Magery].Value < 100.0)
            {
                from.SendMessage(32, "You need at least 100 Blacksmithing and 100 Magery to use this");
                return;
            }

            base.OnDoubleClick(from);
        }

        protected override bool Validate(Mobile from, BaseWeapon weapon)
        {
            if (!base.Validate(from, weapon)) return false;

            if (false == (weapon is BaseBashing || weapon is BaseStaff || weapon is IPugilistGloves))
            {
                from.SendMessage(32, "You may only use this on blunt weapons");
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