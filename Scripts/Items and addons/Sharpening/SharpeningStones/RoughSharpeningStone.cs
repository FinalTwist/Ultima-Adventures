namespace Server.Items
{
    public class RoughSharpeningStone : SharpeningStoneBase
    {
        [Constructable]
        public RoughSharpeningStone() : this(5)
        {
        }

        [Constructable]
        public RoughSharpeningStone(int uses) : base(uses, 60, 60)
        {
            Name = "Rough Sharpening Stone";
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

            if (from.Skills[SkillName.Blacksmith].Value < 60)
            {
                from.SendMessage(32, "You need at least 60 Blacksmith to sharpen weapons with this stone");
                return false;
            }

            return true;
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);
			list.Add(1070722, "Can Slightly Increase Bladed Weapons Damage");
        }

        public RoughSharpeningStone(Serial serial) : base(serial)
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