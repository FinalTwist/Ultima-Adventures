namespace Server.Items
{
    public class DenseWeightingStone : WeightingStoneBase
    {
        [Constructable]
        public DenseWeightingStone() : this(5)
        {
        }

        [Constructable]
        public DenseWeightingStone(int uses) : base(uses, 70, 100)
        {
            Name = "Dense Weighting Stone";
        }

        protected override int GetBonus(Mobile from)
        {
            return Utility.Random((int)(from.Skills[SkillName.Blacksmith].Value / 10));
        }

        protected override void AfterBonusApplied(Mobile from, BaseWeapon weapon, int damageIncrease)
        {
            weapon.Attributes.WeaponDamage += damageIncrease;
            from.SendMessage(88, "The damage increases by {0}%", damageIncrease);
        }

        protected override bool Validate(Mobile from, BaseWeapon weapon)
        {
            if (!base.Validate(from, weapon)) return false;

            if (from.Skills[SkillName.Blacksmith].Value < 100.0)
            {
                from.SendMessage(32, "You need at least 100.0 Blacksmith to weight weapons with this stone");
                return false;
            }

            return true;
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);
            list.Add(1070722, "Can Wondrously Increase Mace/Pugilist Glove/Stave's Damage");
        }

        public DenseWeightingStone(Serial serial) : base(serial)
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