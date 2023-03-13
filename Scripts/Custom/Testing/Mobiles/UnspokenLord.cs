using System;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a glowing unspoken corpse")]
    public class UnspokenLord : BaseCreature
    {
        [Constructable]
        public UnspokenLord()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.4, 0.8)
        {
            this.Name = "an unspoken lord";
            this.Body = 400;
            this.Hue = 2019;
			this.ActiveSpeed = .15;

            this.SetStr(500, 510);
            this.SetDex(350, 375);
            this.SetInt(850, 900);

            this.SetHits(650, 725);

            this.SetDamage(25, 35);

            this.SetDamageType(ResistanceType.Energy, 40);


            this.SetResistance(ResistanceType.Physical, 80);
            this.SetResistance(ResistanceType.Fire, 60 );
            this.SetResistance(ResistanceType.Cold, 75);
            this.SetResistance(ResistanceType.Poison, 85);
            this.SetResistance(ResistanceType.Energy, 70);

            this.SetSkill(SkillName.MagicResist, 120.1, 130.0);
            this.SetSkill(SkillName.Swords, 120.1, 130.0);
            this.SetSkill(SkillName.Tactics, 100.1, 130.0);
            this.SetSkill(SkillName.Wrestling, 120.1, 130.0);
			this.SetSkill(SkillName.EvalInt, 120.1, 130.0);
			this.SetSkill(SkillName.Necromancy, 150.1, 165.0);
			this.SetSkill(SkillName.Magery, 150.0, 165.0);
			this.SetSkill(SkillName.SpiritSpeak, 150.0, 165.0);
			this.SetSkill(SkillName.Meditation, 150.0);
		

            this.Fame = 15200;
            this.Karma = -12500;

			UnspokenLordsScythe weapon = new UnspokenLordsScythe(); 
			weapon.Movable = false;
			AddItem( weapon );
			
			HoodedShroudOfShadows chest = new HoodedShroudOfShadows(); 
			chest.Hue = 2019;
			chest.Movable = false;
			AddItem( chest );
			


            this.VirtualArmor = 30;
		   {	
			new ChargerOfTheFallen().Rider = this;
		   } 
        }
		
		 

        public UnspokenLord(Serial serial)
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
		
		public override Poison PoisonImmune
        {
            get
            {
                return Poison.Lethal;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.FilthyRich, 4 );
			
        }
		
		public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            if (Utility.RandomDouble() < 0.20) 
               c.DropItem(new UnspokenLordsScythe()); 

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