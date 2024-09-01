namespace Server.Items
{
    public class ElementalSharpeningStone : SharpeningStoneBase
    {
        [Constructable]
        public ElementalSharpeningStone() : this(5)
        {
        }

        [Constructable]
        public ElementalSharpeningStone(int uses) : base(uses, 70, 100)
        {
            Name = "Elemental Sharpening Stone";
        }

        protected override int GetBonus(Mobile from)
        {
            return Utility.Random((int)(from.Skills[SkillName.Blacksmith].Value / 20));
        }

        protected override void AfterBonusApplied(Mobile from, BaseWeapon weapon, int damageIncrease)
        {
            weapon.Attributes.WeaponDamage += damageIncrease;
            weapon.AosElementDamages.Fire
                = weapon.AosElementDamages.Cold
                = weapon.AosElementDamages.Poison
                = weapon.AosElementDamages.Energy
                = weapon.AosElementDamages.Physical
                = 20;

            from.SendMessage(88, "You weighted the weapon with {0} elemental damage increase", damageIncrease);
        }

        protected override bool Validate(Mobile from, BaseWeapon weapon)
        {
            if (!base.Validate(from, weapon)) return false;

            if (from.Skills[SkillName.Blacksmith].Value < 100.0 || from.Skills[SkillName.Magery].Value < 100.0)
            {
                from.SendMessage(32, "You need at least 100 Blacksmith and 100 Magery to sharpen weapons with this stone");
                return false;
            }


            return true;
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);
			list.Add(1070722, "Can Increase A Bladed Weapon's Damage");
            list.Add(1049644, "Even Damage To All Defenses"); // PARENTHESIS
        }

        public ElementalSharpeningStone(Serial serial) : base(serial)
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