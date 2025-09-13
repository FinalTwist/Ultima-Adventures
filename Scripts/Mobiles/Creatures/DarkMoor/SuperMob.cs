using System;

namespace Server.Mobiles
{
    [CorpseName("a super mob corpse")]
    public class SuperMob : BaseCreature
    {
        private int StoredDamage;
        private int DamageChange = GetRandomDamage();
        private string Weakness;
        private string Strength;

        [Constructable]
        public SuperMob() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a super mob";

            switch (Utility.Random(4))
            {
                case 0:
                        Body = 59;
                        BaseSoundID = 362;
                        break;

                case 1:
                        Body = 303;
                        BaseSoundID = 357;
                        break;

                case 2:
                        Body = 40;
                        BaseSoundID = 357;
                        break;

                case 3:
                        Body = 57;
                        BaseSoundID = 451;
                        break;
            }

            SetStr((DamageChange / 10) + 75);
            SetDex((DamageChange / 10) + 75);
            SetInt((DamageChange / 10) + 75);

            SetHits(DamageChange * 20);

            SetDamage(DamageChange / 10, (DamageChange / 10) + 25);

            SetSkill(SkillName.MagicResist, (DamageChange/10) + 50);

            ChangeWeakness();

            Fame = DamageChange * 20;
            Karma = DamageChange * -20;

            VirtualArmor = ((DamageChange / 10) + 10);
        }

        public SuperMob(Serial serial) : base(serial)
        {
        }

        public static int GetRandomDamage()
        {
            Random rnd = new Random();
            return rnd.Next(250, 500);
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            StoredDamage = StoredDamage + amount;

            if (StoredDamage >= DamageChange)
            {
                ChangeWeakness();

                StoredDamage = 0;
                DamageChange = GetRandomDamage();
            }

            base.OnDamage(amount, from, willKill);
        }

        public void ChangeWeakness()
        {
            Random rnd = new Random();
            int getRnd = rnd.Next(1, 5);

            SetAllStr();

            ChangeStr();

            if (getRnd == 1)
            {
                SetResistance(ResistanceType.Physical, 0);
                Weakness = "Physical";
                Hue = 1175;
            }
            if (getRnd == 2)
            {
                SetResistance(ResistanceType.Fire, 0);
                Weakness = "Fire";
                Hue = 1260;
            }
            if (getRnd == 3)
            {
                SetResistance(ResistanceType.Cold, 0);
                Weakness = "Cold";
                Hue = 1266;
            }
            if (getRnd == 4)
            {
                SetResistance(ResistanceType.Poison, 0);
                Weakness = "Poison";
                Hue = 1272;
            }
            if (getRnd == 5)
            {
                SetResistance(ResistanceType.Energy, 0);
                Weakness = "Energy";
                Hue = 1278;
            }
        }

        public void ChangeStr()
        {
            Random rnd = new Random();
            int getRnd = rnd.Next(1, 5);

            if (getRnd == 1)
            {
                SetDamageType(ResistanceType.Physical, 100);
                Strength = "Physical";
            }
            if (getRnd == 2)
            {
                SetDamageType(ResistanceType.Fire, 100);
                Strength = "Fire";
            }
            if (getRnd == 3)
            {
                SetDamageType(ResistanceType.Cold, 100);
                Strength = "Cold";
            }
            if (getRnd == 4)
            {
                SetDamageType(ResistanceType.Poison, 100);
                Strength = "Poison";
            }
            if (getRnd == 5)
            {
                SetDamageType(ResistanceType.Energy, 100);
                Strength = "Energy";
            }
        }

        public void SetAllStr()
        {
            SetDamageType(ResistanceType.Physical, DamageChange / 10);
            SetResistance(ResistanceType.Physical, 100);
            
            SetDamageType(ResistanceType.Fire, DamageChange / 10);
            SetResistance(ResistanceType.Fire, 100);

            SetDamageType(ResistanceType.Cold, DamageChange / 10);
            SetResistance(ResistanceType.Cold, 100);

            SetDamageType(ResistanceType.Poison, DamageChange / 10);
            SetResistance(ResistanceType.Poison, 100);

            SetDamageType(ResistanceType.Energy, DamageChange / 10);
            SetResistance(ResistanceType.Energy, 100);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel >= AccessLevel.GameMaster)
                from.SendMessage(StoredDamage + "/" + DamageChange + " : <Attack> " + Strength + " : <Weak> " + Weakness);

            base.OnDoubleClick(from);
        }

        public override bool IgnoreYoungProtection
        {
            get
            {
                return true;
            }
        }
        public override bool BardImmune
        {
            get
            {
                return true;
            }
        }
        public override bool Unprovokable
        {
            get
            {
                return true;
            }
        }
        public override bool AreaPeaceImmune
        {
            get
            {
                return true;
            }
        }
        public override bool BleedImmune
        {
            get
            {
                return true;
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);

            writer.Write(DamageChange);
            writer.Write(Weakness);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            DamageChange = reader.ReadInt();
            Weakness = reader.ReadString();
            
            ChangeWeakness();
            StoredDamage = 0;
        }
    }
}
