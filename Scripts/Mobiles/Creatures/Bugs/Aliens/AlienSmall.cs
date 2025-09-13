using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
	[CorpseName( "an alien corpse" )]
	public class AlienSmall : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public AlienSmall() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an alien hatchling";
			Body = 98;

			SetStr( 136, 160 );
			SetDex( 41, 52 );
			SetInt( 31, 40 );

			SetHits( 121, 145 );
			SetMana( 20 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 15, 30 );
			SetResistance( ResistanceType.Cold, 15, 30 );
			SetResistance( ResistanceType.Poison, 50, 80 );
			SetResistance( ResistanceType.Energy, 20, 35 );

			SetSkill( SkillName.MagicResist, 50.1, 58.0 );
			SetSkill( SkillName.Tactics, 67.1, 77.0 );
			SetSkill( SkillName.Wrestling, 50.1, 60.0 );
			SetSkill( SkillName.Anatomy, 30.1, 34.0 );

			Fame = 1900;
			Karma = -1900;

			Tamable = true;
			MinTameSkill = 61.1;
			ControlSlots = 2;

			Item Venom = new VenomSack();
				Venom.Name = "greater venom sack";
				AddItem( Venom );
		}

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override Poison PoisonImmune{ get{ return Poison.Greater; } }
		public override Poison HitPoison{ get{ return (0.8 >= Utility.RandomDouble() ? Poison.Greater : Poison.Deadly); } }

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( Utility.RandomMinMax( 1, 4 ) == 1 )
			{
				int goo = 0;

				foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == "acidic ichor" ){ goo++; } }

				if ( goo == 0 )
				{
					MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "acidic ichor", 0xBAB, 0 );
				}
			}
		}

		public override bool OnBeforeDeath()
		{
			this.BaseSoundID = 278;
			this.PlaySound( 0x580 );
			
			Effects.SendLocationEffect( this.Location, this.Map, 0x23B2, 16, 10, 1166, 0 );

			int goo = 0;

			foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == "acidic ichor" ){ goo++; } }

			if ( goo == 0 )
			{
				MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "acidic ichor", 0xBAB, 0 );
			}

			return base.OnBeforeDeath();
		}

		public override int GetAngerSound()
		{
			return 0x4F3;
		}

		public override int GetIdleSound()
		{
			return 0x4F2;
		}

		public override int GetAttackSound()
		{
			return 0x4F1;
		}

		public override int GetHurtSound()
		{
			return 0x4F4;
		}

		public override int GetDeathSound()
		{
			return 0x4F0;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
		}

		public AlienSmall( Serial serial ) : base( serial )
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