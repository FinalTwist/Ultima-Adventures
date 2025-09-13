using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a drake corpse" )]
	public class TestDrake : BaseCreature
	{
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 17 ); }

		[Constructable]
		public TestDrake () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a drake";
			Body = 335;
			BaseSoundID = 362;

			SetStr( 401, 430 );
			SetDex( 133, 152 );
			SetInt( 101, 140 );

			SetHits( 241, 258 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Fire, 20 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 65.1, 80.0 );
			SetSkill( SkillName.Tactics, 65.1, 90.0 );
			SetSkill( SkillName.Wrestling, 65.1, 80.0 );

			Fame = 5500;
			Karma = -5500;

			VirtualArmor = 46;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 84.3;

			PackReg( 3 );

		//Final Set Karma/Fame

				double modifier = 0;
				
				if (this.AI == AIType.AI_Melee)
					modifier= 1 + (( this.Skills[SkillName.Wrestling].Value + this.Skills[SkillName.Tactics].Value ) / 200.0);
				else if (this.AI == AIType.AI_Mage || this.AI == AIType.AI_Mage)
					modifier= 1 + (( this.Skills[SkillName.Magery].Value + this.Skills[SkillName.EvalInt].Value ) / 200.0);
				else 
					modifier= 1 + (( this.Skills[SkillName.Wrestling].Value + this.Skills[SkillName.Tactics].Value ) / 200.0);
		
				if (this.CanHeal)
					modifier += this.Skills[SkillName.Healing].Value / 200.0;
				
				if (this.HasBreath)
					modifier += (double)this.BreathComputeDamage() / 100.0;
					
				if (this.HitPoison != null)
				{
					if (this.HitPoison == Poison.Lethal)
						modifier += 0.4;
					else if (this.HitPoison == Poison.Greater)
						modifier += 0.25;
					else 
						modifier += 0.10;
				}
					
				double resistances = ( ((double)this.PoisonResistSeed + (double)this.PhysicalResistanceSeed + (double)this.FireResistSeed + (double)this.ColdResistSeed + (double)this.EnergyResistSeed) / 350.0 );
				double thisprice =  ( ( ( ( (this.RawStr + this.RawDex + this.RawInt + this.HitsMax) * this.DamageMax) / 10 ) * resistances  ) * modifier * 3); 
				
				if (this.IsParagon)
					thisprice *= 2.5;
				
				if (this.Karma < 0 )
					this.Karma = -((int)thisprice);
				else 
					this.Karma = (int)thisprice;
				
				this.Fame = (int)thisprice;

			

		
	}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override int TreasureMapLevel{ get{ return 2; } }
		public override int Meat{ get{ return 10; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override int Scales{ get{ return 2; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Red ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }

		public TestDrake( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}