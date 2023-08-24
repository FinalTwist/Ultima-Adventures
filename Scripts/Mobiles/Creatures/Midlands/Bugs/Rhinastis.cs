using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a insectoid corpse" )]
	public class Rhinastis : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		public override int BreathPhysicalDamage{ get{ return 100; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectItemID{ get{ return 0; } }
		public override bool HasBreath{ get{ return true; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 4 ); }

		[Constructable]
		public Rhinastis () : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a rhinastis";
			Body = 82;
			Hue = 628;

			SetStr( 496, 525 );
			SetDex( 86, 105 );
			SetInt( 40, 60 );

			SetHits( 378, 395 );

			SetDamage( 16, 22 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Poison, 25 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 35, 45 );

			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 60;

			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 83.9;
		}

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public override void OnDeath( Container c )
        {
            if (AdventuresFunctions.IsPuritain((object)this))
            {
                //set these for each mob
                double odds = 1; // 0 - 1 max (e.g. 0.50 is 50%)
                int amount = Utility.RandomMinMax(10, 10); // the min/max for the amount of the reg

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

                    Item reg2 = Loot.RandomMixerReagent(); 
                    reg2.Amount = amount;

                    if (reg2 != null)
                        this.Backpack.DropItem(reg2);
                }
            }


            base.OnDeath(c);
        }

		public Rhinastis(Serial serial) : base(serial)
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