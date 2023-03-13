// Created by Neptune

using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles

{
    [CorpseName(" corpse of Idium")]
    public class Idium : BaseCreature
    {

        [Constructable]
        public Idium() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.1, 0.2)
        {
            Name = "Idium";
            Hue = 2255;
            Body = 46; // Uncomment these lines and input values
            BaseSoundID = 357; // To use your own custom body and sound.
            SetStr(700, 800);
            SetDex(500, 800);
            SetInt(700, 800);
            SetHits(10000, 15000);
            SetDamage(30, 35);
            SetDamageType(ResistanceType.Cold, 60);
            SetDamageType(ResistanceType.Fire, 60);
            SetDamageType(ResistanceType.Poison, 60);

            SetResistance(ResistanceType.Physical, 60);
            SetResistance(ResistanceType.Cold, 60);
            SetResistance(ResistanceType.Fire, 60);
            SetResistance(ResistanceType.Energy, 60);
            SetResistance(ResistanceType.Poison, 60);

            SetSkill(SkillName.EvalInt, 90, 100.0);
            SetSkill(SkillName.Magery, 90.1, 100.0);
            SetSkill(SkillName.Meditation, 90.1, 101.0);
            SetSkill(SkillName.Poisoning, 90.1, 101.0);
            SetSkill(SkillName.MagicResist, 95.2, 100.0);
            SetSkill(SkillName.Tactics, 90.1, 100.0);
            SetSkill(SkillName.Wrestling, 95.1, 100.0);
            SetSkill(SkillName.Swords, 95.1, 100.0);
            SetSkill(SkillName.Anatomy, 95.1, 100.0);
            SetSkill(SkillName.Parry, 80.1, 100.0);


            Fame = 40000;
            Karma = -45000;
            VirtualArmor = 65;
            PackGold(15000, 20000);

        }
        public override void GenerateLoot()
        {
            switch (Utility.Random(75))
            {
                case 0: PackItem(new DragonArmsOfEvolution()); break;
                case 1: PackItem(new DragonChestOfEvolution()); break;
                case 2: PackItem(new DragonGlovesOfEvolution()); break;
                case 3: PackItem(new DragonLegsOfEvolution()); break;
                case 4: PackItem(new OrderShieldOfEvolution()); break;
                case 5: PackItem(new DragonHelmOfEvolution()); break;
            }
        }
        
        public override bool BardImmune { get { return true; } }
        public override bool Unprovokable { get { return true; } }
        public override bool AlwaysMurderer { get { return true; } }
        
        public Idium(Serial serial) : base(serial)
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

