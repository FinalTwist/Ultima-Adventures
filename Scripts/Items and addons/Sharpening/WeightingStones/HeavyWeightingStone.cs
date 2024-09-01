namespace Server.Items
{
    public class HeavyWeightingStone : WeightingStoneBase
    {
        [Constructable]
        public HeavyWeightingStone() : this(5)
        {
        }

        [Constructable]
        public HeavyWeightingStone(int uses) : base(uses, 70, 80)
        {
            Name = "Heavy Weighting Stone";
        }

        protected override int GetBonus(Mobile from)
        {
            return Utility.Random((int)(from.Skills[SkillName.Blacksmith].Value / 20));
        }

        protected override void AfterBonusApplied(Mobile from, BaseWeapon weapon, int damageIncrease)
        {
            weapon.Attributes.WeaponDamage += damageIncrease;

            from.SendMessage(88, "The damage increases by {0}%", damageIncrease);
        }

        protected override bool Validate(Mobile from, BaseWeapon weapon)
        {
            if (!base.Validate(from, weapon)) return false;

            if (from.Skills[SkillName.Blacksmith].Value < 80)
            {
                from.SendMessage(32, "You need at least 80 Blacksmith to weight weapons with this stone");
                return false;
            }

            return true;
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);
            list.Add(1070722, "Can Greatly Increase Mace/Pugilist Glove/Stave's Damage");
        }

        public HeavyWeightingStone(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
}