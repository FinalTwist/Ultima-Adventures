using System;
using Server;
using Server.Mobiles;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.Accounting;

namespace Server.Mobiles
{
	[CorpseName( "a chicken corpse" )]
	public class BasicChicken : BaseCreature
	{
	

		[Constructable]
		public BasicChicken( ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			this.Name = "a basic chicken";
            this.Body = 0xD0;
            this.BaseSoundID = 0x6E;

            this.SetStr(30, 35);
            this.SetDex(40, 45);
            this.SetInt(20, 25);

            this.SetHits(100);
            this.SetMana(0);

            this.SetDamage(3, 10);

            this.SetDamageType(ResistanceType.Physical, 100);

			//We want them to be protected from players casting on them.
            this.SetResistance(ResistanceType.Physical, 20);
            this.SetResistance(ResistanceType.Energy, 20);
            this.SetResistance(ResistanceType.Fire, 20);
            this.SetResistance(ResistanceType.Poison, 20);
            this.SetResistance(ResistanceType.Cold, 20);


            this.SetSkill(SkillName.MagicResist, 100.0);
            this.SetSkill(SkillName.Tactics, 5.0, 10.0);
            this.SetSkill(SkillName.Wrestling, 5.0, 10.0);

            this.Fame = 0;
            this.Karma = 0;

            this.VirtualArmor = Utility.Random(2, 10);

            this.Tamable = true;
            this.ControlSlots = 1;
		}

		public override int Meat{ get{ return 0; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay; } }
		public override int Feathers{ get{ return 0; } }
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
			base.AddNameProperties(list);
			
		}

		public override void OnThink()
		{
			base.OnThink();
			
			if (this.IsBonded && !this.CanAngerOnTame)
				this.IsBonded = false;
			
		}

		public override bool OnBeforeDeath()
		{

			return base.OnBeforeDeath();
		}
				
		public BasicChicken(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}