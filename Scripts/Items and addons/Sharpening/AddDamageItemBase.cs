using Server.Targeting;
using System;

namespace Server.Items
{
    public abstract class AddDamageItemBase : Item
    {
        protected abstract int MaxDamageBonus { get; }
        private int _uses;
        protected bool IsLegacyItem;

        public AddDamageItemBase(Serial serial) : base(serial)
        {
        }

        protected AddDamageItemBase(int uses, int itemID) : base(itemID)
        {
            Weight = 1.0;
            Hue = 0x38C;
            Uses = uses;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Uses
        {
            get { return _uses; }
            set { _uses = value; InvalidateProperties(); }
        }

        protected abstract void AfterItemDeleted(Mobile from);

        public void Apply(Mobile from, object targeted)
        {
            if (Deleted) { return; }

            var weapon = targeted as BaseWeapon;
            if (!Validate(from, weapon)) return;
        
            var currentDamage = weapon.Attributes.WeaponDamage;
            if (weapon.Quality == WeaponQuality.Exceptional) currentDamage += 15; // Because apparently Exceptional is worth 15% DI
            if (MaxDamageBonus <= currentDamage)
            {
                from.SendMessage(32, "This weapon cannot be strengthened any further");
                return;
            }

            int randomBonus = GetBonus(from);
            if (randomBonus < 1)
            {
                from.SendMessage(32, "You fail to strengthen your weapon.");
            }
            else
            {
                var damageBonus = Math.Min(randomBonus, MaxDamageBonus - currentDamage);
                weapon.Attributes.WeaponDamage += damageBonus;
                from.SendMessage(88, "The damage increases by {0}%", damageBonus);
            }

            if (--Uses < 1)
            {
                Delete();
                AfterItemDeleted(from);
            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1060584, Uses.ToString()); // uses remaining: ~1_val~
        }

        public override void OnDoubleClick(Mobile from)
        {
            PromptForTarget(from, "Which weapon would you like to strengthen?");
        }

        protected abstract int GetBonus(Mobile from);

        protected void PromptForTarget(Mobile from, string message)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendMessage("This must be in your backpack to use");
                return;
            }

            from.SendMessage(message);
            from.Target = new InternalTarget(this);
        }

        protected virtual bool Validate(Mobile from, BaseWeapon weapon)
        {
            if (weapon == null || weapon.Deleted) return false;

            if (!weapon.IsChildOf(from.Backpack))
            {
                from.SendMessage(32, "This must be in your backpack");
                return false;
            }

            return true;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2); // version
            writer.Write((int)_uses);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            IsLegacyItem = version < 2;
            _uses = reader.ReadInt();
        }

        private class InternalTarget : Target
        {
            private readonly AddDamageItemBase _itemBase;

            public InternalTarget(AddDamageItemBase itemBase) : base(1, false, TargetFlags.None)
            {
                _itemBase = itemBase;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                _itemBase.Apply(from, targeted);
            }
        }
    }
}