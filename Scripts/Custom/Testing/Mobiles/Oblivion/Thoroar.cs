// Created by Neptune

using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles

{
    [CorpseName(" corpse of Oblivion")]
    public class Thoroar : BaseCreature
    {

        [Constructable]
        public Thoroar() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.1, 0.2)
        {
            Name = "Thoroar";
            Title = "Lord of Oblivion";
            Hue = 1261;
            Paralyzed = false;
            Body = 126; // Uncomment these lines and input values
            BaseSoundID = 357; // To use your own custom body and sound.
            SetStr(2000, 2500);
            SetDex(1500, 2000);
            SetInt(1500, 2000);
            SetHits(200000, 250000);
            SetDamage(45, 55);
            SetDamageType(ResistanceType.Cold, 70);
            SetDamageType(ResistanceType.Fire, 70);
            SetDamageType(ResistanceType.Energy, 70);
            SetDamageType(ResistanceType.Poison, 70);

            SetResistance(ResistanceType.Physical, 65);
            SetResistance(ResistanceType.Cold, 65);
            SetResistance(ResistanceType.Fire, 65);
            SetResistance(ResistanceType.Energy, 65);
            SetResistance(ResistanceType.Poison, 65);

            SetSkill(SkillName.EvalInt, 120.1, 130.0);
            SetSkill(SkillName.Magery, 90.1, 100.0);
            SetSkill(SkillName.Meditation, 100.1, 101.0);
            SetSkill(SkillName.Poisoning, 100.1, 101.0);
            SetSkill(SkillName.MagicResist, 75.2, 100.0);
            SetSkill(SkillName.Tactics, 90.1, 100.0);
            SetSkill(SkillName.Wrestling, 75.1, 100.0);
            SetSkill(SkillName.Swords, 75.1, 100.0);
            SetSkill(SkillName.Anatomy, 75.1, 100.0);
            SetSkill(SkillName.Parry, 75.1, 100.0);


            Fame = 40000;
            Karma = -45000;
            VirtualArmor = 70;
            //PackGold(11120, 11130);

        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.SuperBoss, 3);
            PackItem( new Gold(11120, 11130));

            switch (Utility.Random(75))
            {

                case 0: PackItem(new OblivionCap()); break;
                case 1: PackItem(new OblivionChest()); break;
                case 2: PackItem(new OblivionArms()); break;
                case 3: PackItem(new OblivionGloves()); break;
                case 4: PackItem(new OblivionLegs()); break;
                case 5: PackItem(new OblivionBlade()); break;
                case 6: PackItem(new OblivionShield()); break;
                case 7: PackItem(new OblivionGorget()); break;



            }

        }

        public override bool HasBreath { get { return true; } }
        public override int BreathFireDamage { get { return 5; } }
        //public override int BreathColdDamage { get { return 9; } }
        public override bool IsScaryToPets { get { return true; } }
        public override bool AutoDispel { get { return true; } }
        public override bool BardImmune { get { return true; } }
        public override bool Unprovokable { get { return true; } }
        public override Poison HitPoison { get { return Poison.Lethal; } }
        public override bool AlwaysMurderer { get { return true; } }




        public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
            if (from is BaseCreature)
            {
                BaseCreature bc = (BaseCreature)from;

                if (bc.Controlled || bc.BardTarget == this)
                    damage = 1; // Immune to pets and provoked creatures
            }
        }



        public Thoroar(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

        }

    }
}

