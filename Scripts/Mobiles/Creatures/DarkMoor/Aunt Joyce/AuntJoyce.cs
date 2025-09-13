//Scripted By James4245
using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Targeting;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("corpse of a crazy cat lady")]
    public class AuntJoyce : BaseCreature
    {
        private static bool m_Talked;

        string[] kfcsay = new string[]
        {
            "OMFG, I CAN'T AFFORD ANYTHING!",
            "FIND A HOME FOR YOUR F*CKING CAT!",
            "I HAVE NOTHING!  I CAN'T AFFORD ANYTHING!  NOBODY LOVES ME!  I'M NOT A BI†CH!",
            "YOU F*CKING DOG!  GET OFF MY F*CKING FEET BEFORE I KICK YOU!",
            "YOU F*CKING PICKY CAT!  NOW YOU GET NOTHING!",
            "YOU DON'T LIKE WHAT I PUT DOWN, FINE!  YOU CAN F*CKING STARVE!  SEE IF I CARE!",
            "HOW AM I GOING TO GET TO THE STORE?!?  HOW AM I GOING TO GET TO THE DOCTOR?!?  TAKE ME TO THE VET!",
            "I HATE THESE F*CKING NEIGHBORS!  SHUT THE F*CK UP YOU DAMN MEXICANS!",

        };

        [Constructable]
        public AuntJoyce() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.15, 0.4)
        {
            Body = 0x191;
            Name = "Aunt Joyce";
            Title = "the Evil, Racial, Offensive Aunt";

            SetStr(1800, 1900);
            SetDex(1510, 1750);
            SetInt(1710, 2200);

            SetHits(8000, 10000);

            SetDamage(34, 136);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Cold, 50);

            SetResistance(ResistanceType.Physical, 45);
            SetResistance(ResistanceType.Fire, 65);
            SetResistance(ResistanceType.Cold, 35);
            SetResistance(ResistanceType.Poison, 25);
            SetResistance(ResistanceType.Energy, 35);

            SetSkill(SkillName.EvalInt, 670.0);
            SetSkill(SkillName.Magery, 670.0);
            SetSkill(SkillName.Meditation, 670.0);
            SetSkill(SkillName.MagicResist, 630.0);
            SetSkill(SkillName.Tactics, 620.0);
            SetSkill(SkillName.Wrestling, 620.0);
            SetSkill(SkillName.Swords, 475.0, 1300.0);
            SetSkill(SkillName.Parry, 475.0, 1300.0);


            Fame = 10000;
            Karma = 10000;

            Item stick = new BitchStick();
            stick.LootType = LootType.Blessed;
            stick.Movable = false;
            AddItem(stick);



            Item legs = new JoyceDress();
            legs.LootType = LootType.Blessed;
            legs.Movable = false;
            AddItem(legs);

            Item gloves = new JoyceGloves();
            gloves.LootType = LootType.Blessed;
            gloves.Movable = false;
            AddItem(gloves);

            Item neck = new JoyceNeck();
            neck.LootType = LootType.Blessed;
            neck.Movable = false;
            AddItem(neck);

            //            Item helm = new CapOfHorus();
            //     		helm.LootType = LootType.Blessed;
            //     		AddItem(helm);

            Item chest = new JoyceChest();
            chest.LootType = LootType.Blessed;
            chest.Movable = false;
            AddItem(chest);
        }
        //public override bool ShowFameTitle { get { return false; } }
        public override bool AutoDispel { get { return true; } }
        public override bool BardImmune { get { return true; } }
        public override bool Unprovokable { get { return true; } }
        public override bool Uncalmable { get { return true; } }
        public override bool AlwaysMurderer { get { return true; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.SuperBoss, 2);

            switch (Utility.Random(10)) //Minor Artifacts
            {
                case 0: PackItem(new BitchStick()); break;
                case 1: PackItem(new JoyceDress()); break;
                case 2: PackItem(new JoyceGloves()); break;
                case 3: PackItem(new JoyceChest()); break;
                case 4: PackItem(new JoyceDress()); break;
                case 5: PackItem(new Spam()); break;
                case 6: PackItem(new Spam()); break;
                case 7: PackItem(new Spam()); break;

            }
            base.GenerateLoot();
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m_Talked == false)
            {
                if (m.InRange(this, 4))
                {
                    m_Talked = true;
                    SayRandom(kfcsay, this);
                    this.Move(GetDirectionTo(m.Location));
                    SpamTimer t = new SpamTimer();
                    t.Start();
                }
            }
        }

        public AuntJoyce(Serial serial) : base(serial)
        {
        }

        private class SpamTimer : Timer
        {
            public SpamTimer() : base(TimeSpan.FromSeconds(8))
            {
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Talked = false;
            }
        }

        private static void SayRandom(string[] say, Mobile m)
        {
            m.Say(say[Utility.Random(say.Length)]);
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

