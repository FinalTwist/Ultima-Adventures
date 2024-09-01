using Server.Targeting;

namespace Server.Items
{
    public abstract class SharpeningStoneBase : Item
    {
        protected readonly double RequiredBlacksmithSkillLevel;
        protected readonly int MaxDamageBonus;

        private int _uses;
        [CommandProperty(AccessLevel.GameMaster)]
        public int Uses { get { return _uses; } set { _uses = value; InvalidateProperties(); } }

        public SharpeningStoneBase(int uses, int maxDamageBonus, double requiredBlacksmithSkillLevel) : base(0x1F14)
        {
            Weight = 1.0;
            Hue = 0x38C;
            Uses = uses;
            MaxDamageBonus = maxDamageBonus;
            RequiredBlacksmithSkillLevel = requiredBlacksmithSkillLevel;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1060584, Uses.ToString()); // uses remaining: ~1_val~
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendMessage("This must be in your backpack to use.");
                return;
            }

            if (Uses < 1)
            {
                Delete();
                from.SendMessage(32, "The stone crumbles in your hands");
                return;
            }

            from.SendMessage("Which weapon you want to try to sharpen?");
            from.Target = new InternalTarget(this);
        }

        protected abstract int GetBonus(Mobile from);

        protected abstract void AfterBonusApplied(Mobile from, BaseWeapon weapon, int damageIncrease);

        protected void ApplyValues(Mobile from, BaseWeapon weapon)
        {
            var bonus = GetBonus(from);
            if (0 < bonus)
            {
                int weaponDamage = weapon.Attributes.WeaponDamage;
                if (MaxDamageBonus < weaponDamage + bonus) bonus = MaxDamageBonus - weaponDamage;

                AfterBonusApplied(from, weapon, bonus);
            }
            else
            {
                from.SendMessage(32, "You fail to sharpen the weapon");
            }
        }

        public void Apply(Mobile from, object targeted)
        {
            if (Deleted) { return; }

            var weapon = targeted as BaseWeapon;
            if (!Validate(from, weapon)) return;

            if (from.Skills[SkillName.Blacksmith].Value < RequiredBlacksmithSkillLevel)
            {
                from.SendMessage(32, "Your Blacksmithing must be higher to use this stone");
                return;
            }

            ApplyValues(from, weapon);

            if (--Uses <= 0)
            {
                from.SendMessage(32, "The stone crumbles in your hand");
                Delete();
            }


            if (this.Amount <= 0)
                this.Delete();
        }

        protected virtual bool Validate(Mobile from, BaseWeapon weapon)
        {
            if (weapon == null || weapon.Deleted) return false;

            if (!weapon.IsChildOf(from.Backpack))
            {
                from.SendMessage(32, "This must be in your backpack");
                return false;
            }

            if (false == (weapon is BaseSword || weapon is BaseKnife || weapon is BaseAxe || weapon is BaseSpear))
            {
                from.SendMessage(32, "You may only use this on edged weapons");
                return false;
            }

            int i_DI = weapon.Attributes.WeaponDamage;
            if (weapon.Quality == WeaponQuality.Exceptional) i_DI += 15;

            if (i_DI >= MaxDamageBonus)
            {
                from.SendMessage(32, "This weapon cannot be improved any further");
                return false;
            }

            return true;
        }

        public SharpeningStoneBase(Serial serial) : base(serial)
        {
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (int) _uses );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			_uses = reader.ReadInt();
			if ( version == 0 ) { Serial sr_Owner = reader.ReadInt(); }
        }

        private class InternalTarget : Target
        {
            private SharpeningStoneBase _stone;

            public InternalTarget(SharpeningStoneBase stone) : base(18, false, TargetFlags.None)
            {
                _stone = stone;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                _stone.Apply(from, targeted);
            }
        }
    }
}