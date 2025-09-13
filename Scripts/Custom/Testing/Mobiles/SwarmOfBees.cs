using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    public class SwarmOfBees : BaseCreature
    {
        [Constructable]
        public SwarmOfBees() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0, 0)
        {
            Name = "a Swarm of Bees";
            Body = 0x91B;
            BaseSoundID = 268;
            //Hue = 0x21;
            //CantWalk = true;
            MinTameSkill = 100;

            SetStr(70, 120);
            SetDex(175, 250);
            SetInt(50, 100);

            SetHits(300, 500);

            SetDamage(10, 15);

            SetDamageType(ResistanceType.Physical, 25);
            SetDamageType(ResistanceType.Fire, 25);
            SetDamageType(ResistanceType.Cold, 25);
            SetDamageType(ResistanceType.Poison, 25);
            SetDamageType(ResistanceType.Energy, 25);

            SetResistance(ResistanceType.Physical, 150);
            SetResistance(ResistanceType.Fire, 150);
            SetResistance(ResistanceType.Cold, 150);
            SetResistance(ResistanceType.Poison, 150);
            SetResistance(ResistanceType.Energy, 150);

            SetSkill(SkillName.MagicResist, 120.0);
            SetSkill(SkillName.Tactics, 120.0);
            SetSkill(SkillName.Wrestling, 100.0);

            Fame = 0;
            Karma = 0;

            VirtualArmor = 100;
            ControlSlots = 2;
        }

        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.AosFilthyRich, 5);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            c.DropItem(new Beeswax(3));

            //if (Utility.RandomDouble() < 1)
                //c.DropItem(new Beeswax(3));
        }

        public SwarmOfBees(Serial serial)
            : base(serial)
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