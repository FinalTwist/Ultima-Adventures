using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "an alien corpse" )]
	public class Xenomutant : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 100; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x3F; } }
		public override int BreathEffectSound{ get{ return 0x658; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 18 ); }

		[Constructable]
		public Xenomutant() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a xenomutant";
			Body = 737;
			BaseSoundID = 0x5A;

			SetStr( 326, 450 );
			SetDex( 256, 275 );
			SetInt( 211, 220 );

			SetHits( 276, 290 );
			SetMana( 0 );

			SetDamage( 18, 28 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 5, 15 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.DetectHidden, 80.1, 100.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 100.0 );

			Fame = 7000;
			Karma = -7000;

			VirtualArmor = 50;
		}

		public override HideType HideType{ get{ return HideType.Alien; } }
		public override int Hides{ get{ return 12; } }
		public override bool BleedImmune{ get{ return true; } }
		public override int GetAttackSound(){ return 0x642; }	// A
		public override int GetDeathSound(){ return 0x643; }	// D
		public override int GetHurtSound(){ return 0x644; }		// H

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( Utility.RandomMinMax( 1, 2 ) == 1 )
			{
				int goo = 0;

				foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == "alien blood" ){ goo++; } }

				if ( goo == 0 )
				{
					MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "alien blood", 0x48E, 0 );
				}
			}
		}

		public override void OnDamage( int amount, Mobile m, bool willKill )
		{
			if ( this != null && this.Hits > 0 && Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				Region myReg = Region.Find( this.Location, this.Map );
				Region foeReg = Region.Find( m.Location, m.Map );

				bool isNearby = false;
				foreach ( Mobile foe in this.GetMobilesInRange( 1 ) )
				{
					if ( foe == m )
					{
						isNearby = true;
					}
				}

				if ( isNearby == false && myReg == foeReg )
				{
					Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5024 );
					Effects.PlaySound( this, this.Map, 0x64A );
					this.Location = m.Location;
					this.Combatant = m;
					this.Warmode = true;
					Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5024 );
					Effects.PlaySound( this, this.Map, 0x64A );
				}
			}
			base.OnDamage( amount, this, willKill );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( Utility.RandomMinMax( 1, 2 ) == 1 )
			{
				Item acid = new GreaterConflagrationPotion();
				acid.Name = "botle of alien blood";
				acid.Hue = 0x48E;
				c.DropItem( acid );
			}
		}

		public Xenomutant(Serial serial) : base(serial)
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