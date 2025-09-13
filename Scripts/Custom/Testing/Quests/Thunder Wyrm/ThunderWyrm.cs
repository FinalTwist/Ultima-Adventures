using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
    [CorpseName("a thunder wyrm corpse")]
    public class ThunderWyrm : BaseCreature
    {
        [Constructable]
        public ThunderWyrm() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a thunder wyrm";
            Body = 46;
            Hue = 1154;
            BaseSoundID = 362;


            SetStr(721, 760);
            SetDex(101, 130);
            SetInt(386, 425);

            SetHits(800, 950);

            SetDamage(25, 45);

            SetDamageType(ResistanceType.Energy, 100);

            SetResistance(ResistanceType.Physical, 15, 25);
            SetResistance(ResistanceType.Fire, 35, 45);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 80, 90);

            SetSkill(SkillName.EvalInt, 99.1, 100.0);
            SetSkill(SkillName.Magery, 99.1, 100.0);
            SetSkill(SkillName.MagicResist, 99.1, 100.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 18000;
            Karma = -18000;

            VirtualArmor = 64;

            PackGold(1500, 2000);
            switch (Utility.Random(25)) //Rarity 10 
            {
                case 0:
                    PackItem(new WQStonebow());
                    break;
                case 1:
                    PackItem(new WQStonekryss());
                    break;
                case 2:
                    PackItem(new WQStonefork());
                    break;
                case 3:
                    PackItem(new WQStoneshield());
                    break;
                case 4:
                    PackItem(new WQStonemace());
                    break;
                case 5:
                    PackItem(new WQStonekatana());
                    break;
            }


        }

        public override bool HasBreath { get { return true; } }
        public override int BreathFireDamage { get { return 0; } }
        public override int BreathEnergyDamage { get { return 100; } }
        public override int BreathEffectHue { get { return 1154; } }
        public override bool AutoDispel { get { return true; } }
        public override bool BardImmune { get { return true; } }
        public override bool Unprovokable { get { return true; } }
        public override bool Uncalmable { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Deadly; } }

        public ThunderWyrm(Serial serial) : base(serial)
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
