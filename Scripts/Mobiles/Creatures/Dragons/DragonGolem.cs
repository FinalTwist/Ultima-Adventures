using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a broken machine" )]
	public class DragonGolem : BaseCreature
	{
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 9 ); }

		[Constructable]
		public DragonGolem () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a dragon golem";
			Body = 59;
			Hue = 2500;

			SetStr( 796, 825 );
			SetDex( 86, 105 );
			SetInt( 436, 475 );

			SetHits( 478, 495 );

			SetDamage( 16, 22 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 75, 85 );
			SetResistance( ResistanceType.Energy, 15, 20 );

			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 60;

			if ( 0.1 > Utility.RandomDouble() )
				PackItem( new PowerCrystal() );

			if ( 0.15 > Utility.RandomDouble() )
				PackItem( new ClockworkAssembly() );

			if ( 0.2 > Utility.RandomDouble() )
				PackItem( new ArcaneGem() );

			if ( 0.25 > Utility.RandomDouble() )
				PackItem( new Gears( Utility.RandomMinMax( 1, 5 ) ) );

			if ( 0.25 > Utility.RandomDouble() )
				PackItem( new BottleOil( Utility.RandomMinMax( 1, 5 ) ) );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			
			if (!this.Controlled)
			{
			Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "iron scales" );
   			c.DropItem(scale);
			}
		}

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ reflect = true; } // 25% spells are reflected back to the caster
			else { reflect = false; }
		}

		public override bool OnBeforeDeath()
		{
			this.Body = 752;
			return base.OnBeforeDeath();
		}

		public override int GetAngerSound()
		{
			return 541;
		}

		public override int GetIdleSound()
		{
			if ( !Controlled )
				return 542;

			return base.GetIdleSound();
		}

		public override int GetDeathSound()
		{
			if ( !Controlled )
				return 545;

			return base.GetDeathSound();
		}

		public override int GetAttackSound()
		{
			return 562;
		}

		public override int GetHurtSound()
		{
			if ( Controlled )
				return 320;

			return base.GetHurtSound();
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 8 );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune { get { return !Core.SE; } }
		public override bool Unprovokable { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public DragonGolem( Serial serial ) : base( serial )
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
