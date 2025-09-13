using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "an alien corpse" )]
	public class Alien : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public Alien() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an alien";
			Body = 97;

			SetStr( 360, 550 );
			SetDex( 102, 150 );
			SetInt( 152, 200 );

			SetHits( 282, 385 );

			SetDamage( 7, 14 );

			SetResistance( ResistanceType.Physical, 40, 60 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.Wrestling, 90.1, 100.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 120.4, 160.0 );
			SetSkill( SkillName.Anatomy, 50.5, 100.0 );

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 50;

			Tamable = true;
			MinTameSkill = 91.1;
			ControlSlots = 3;

			Item Venom = new VenomSack();
				Venom.Name = "lethal venom sack";
				AddItem( Venom );
		}

		public override int Meat{ get{ return 5; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return (0.8 >= Utility.RandomDouble() ? Poison.Deadly : Poison.Lethal); } }

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( Utility.RandomMinMax( 1, 4 ) == 1 )
			{
				Item acid = new BottleOfAcid();
				acid.Name = "jar of acidic ichor";
				acid.ItemID = 0x1007;
				acid.Hue = 0xBAB;
				c.DropItem( acid );
			}

			Mobile killer = this.LastKiller;
			if ( killer != null && !Controlled )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( Utility.RandomMinMax( 1, 100 ) == 1 )
					{
						c.DropItem( new AlienEgg() );
					}
				}
			}
		}

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
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.Rich );
		}

		public Alien( Serial serial ) : base( serial )
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