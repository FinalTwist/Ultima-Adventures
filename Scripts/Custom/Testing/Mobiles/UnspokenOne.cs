using System;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an unspoken ones corpse")]
    public class UnspokenOne : BaseCreature
    {
        [Constructable]
        public UnspokenOne()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.4, 0.8)
        {
            this.Name = "an unspoken one";
            this.Body = 400;
            this.Hue = 2019;

            this.SetStr(500, 510);
            this.SetDex(350, 375);
            this.SetInt(650, 655);

            this.SetHits(350, 385);

            this.SetDamage(25, 35);

            this.SetDamageType(ResistanceType.Energy, 40);
			

            this.SetResistance(ResistanceType.Physical, 50, 55);
            this.SetResistance(ResistanceType.Fire, 50, 65);
            this.SetResistance(ResistanceType.Cold, 75, 80);
            this.SetResistance(ResistanceType.Poison, 85, 90);
            this.SetResistance(ResistanceType.Energy, 70, 80);

            this.SetSkill(SkillName.MagicResist, 120.1, 130.0);
            this.SetSkill(SkillName.Swords, 120.1, 130.0);
            this.SetSkill(SkillName.Tactics, 100.1, 130.0);
            this.SetSkill(SkillName.Wrestling, 120.1, 130.0);
			this.SetSkill(SkillName.EvalInt, 120.1, 130.0);
			this.SetSkill(SkillName.Necromancy, 120.1, 130.0);
			this.SetSkill(SkillName.Magery, 120.0, 130.0);
			this.SetSkill(SkillName.Meditation, 120.0, 130.0);
			this.SetSkill(SkillName.SpiritSpeak, 120.0, 130.0);
		

            this.Fame = 9500;
            this.Karma = -12500;

			Scythe weapon = new Scythe(); 
			weapon.Hue = 2019;
			weapon.Movable = false;
			AddItem( weapon );
			
			DeathShroud chest = new DeathShroud(); 
			chest.Hue = 2019;
			chest.Movable = false;
			AddItem( chest );

            this.VirtualArmor = 35;
		   
        }

        public UnspokenOne(Serial serial)
            : base(serial)
        {
        }
		public override bool ClickTitle
        {
            get
            {
                return false;
            }
        }

        public override bool AlwaysAttackable
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
            this.AddLoot(LootPack.Rich, 3 );
        }

        public override void DisplayPaperdollTo(Mobile to)
        {
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i] is ContextMenus.PaperdollEntry)
                    list.RemoveAt(i--);
            }
        }

        public override int GetIdleSound()
        {
            return 0x107;
        }

        public override int GetAngerSound()
        {
            return 0x1BF;
        }

        public override int GetDeathSound()
        {
            return 0xFD;
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