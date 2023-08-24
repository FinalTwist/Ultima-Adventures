using System;
using Server.Mobiles;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a insectoid corpse" )]
	public class Selatis : BaseCreature
	{
		public override bool CanChew { get{return true;}}
		[Constructable]
		public Selatis() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			Name = "a greater selatis";
			Body = 99;
			Hue = 196;

			SetStr( 112, 260 );
			SetDex( 120, 290 );
			SetInt( 50, 76 );

			SetHits( 64, 188 );
			SetMana( 0 );

			SetDamage( 8, 25 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 55 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 25 );
			SetResistance( ResistanceType.Poison, 5, 10 );

			SetSkill( SkillName.MagicResist, 15.1, 30.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 1250;
			Karma = 0;

			VirtualArmor = 22;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 71.1;
		}
		else
		{
			Name = "a selatis";
			Body = 99;
			Hue = 196;

			SetStr( 112, 160 );
			SetDex( 120, 190 );
			SetInt( 50, 76 );

			SetHits( 64, 88 );
			SetMana( 0 );

			SetDamage( 8, 16 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 5, 10 );

			SetSkill( SkillName.MagicResist, 15.1, 30.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 750;
			Karma = 0;

			VirtualArmor = 22;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 61.1;
		}
	}

		public override int Meat{ get{ return 1; } }


		
		public override bool OnBeforeDeath( )
        {
            if (AdventuresFunctions.IsPuritain((object)this))
            {
                //set these for each mob
                double odds = 0.5; // 0 - 1 max (e.g. 0.50 is 50%)
                int amount = Utility.RandomMinMax(5, 10); // the min/max for the amount of the reg

                if (odds >= Utility.RandomDouble() && this.Backpack != null)
                {
                    //change the reg type here available are 
                    //RandomNecromancyReagent()
                    //RandomReagent()
                    //RandomMixerReagent()
                    //RandomMixerReagent()
                    //RandomHerbReagent()
                    Item reg = Loot.RandomReagent(); 
                    reg.Amount = amount;

                    if (reg != null)
                        this.Backpack.DropItem(reg);

                    Item reg2 = Loot.RandomNecromancyReagent(); 
                    reg2.Amount = amount;

                    if (reg2 != null)
                        this.Backpack.DropItem(reg2);
                }
            }


            return base.OnBeforeDeath();
        }

		public Selatis(Serial serial) : base(serial)
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