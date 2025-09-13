namespace Server.Items
{
    [FlipableAttribute(0xF43, 0xF44)]
    public class GargoylesAxe : BaseAxe
    {
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.Disarm; } }
        public override WeaponAbility ThirdAbility { get { return WeaponAbility.MagicProtection2; } }
        public override WeaponAbility FourthAbility { get { return WeaponAbility.FrenziedWhirlwind; } }
        public override WeaponAbility FifthAbility { get { return WeaponAbility.MeleeProtection2; } }

        public override int AosStrengthReq { get { return 20; } }
        public override int AosMinDamage { get { return 13; } }
        public override int AosMaxDamage { get { return 15; } }
        public override int AosSpeed { get { return 41; } }
        public override float MlSpeed { get { return 2.75f; } }

        public override int OldStrengthReq { get { return 15; } }
        public override int OldMinDamage { get { return 2; } }
        public override int OldMaxDamage { get { return 17; } }
        public override int OldSpeed { get { return 40; } }


        [Constructable]
        public GargoylesAxe() : this(Utility.RandomMinMax(50, 75))
        {
        }

        [Constructable]
        public GargoylesAxe(int uses) : base(0xF43)
        {
            Name = "A Gargoyle's Axe";
            Hue = 0xB73;
            Weight = 11.0;
            HitPoints = MaxHitPoints = uses;
        }

        public GargoylesAxe(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}