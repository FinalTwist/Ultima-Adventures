namespace Server.Items
{
    public class ConsecratedWeightingStone : ConsecrateItemBase
    {
        public ConsecratedWeightingStone(Serial serial) : base(serial)
        {
        }

        [Constructable]
        public ConsecratedWeightingStone() : this(5)
        {
        }

        [Constructable]
        public ConsecratedWeightingStone(int uses) : base(uses, 0x1F14)
        {
            Name = "Consecrated Weighting Stone";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1049644, "Only usable on blunt weapons"); // Parentheses
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.Skills[SkillName.Blacksmith].Value < 100.0 || from.Skills[SkillName.Chivalry].Value < 80.0)
            {
                from.SendMessage(32, "You need at least 100 Blacksmithing and 80 Chivalry to use this");
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