using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
	[CorpseName( "a broken machine" )]
	public class IronBeetle : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.CrushingBlow;
		}

		[Constructable]
		public IronBeetle() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an iron beetle";
			Body = 82;

			SetStr( 300 );
			SetDex( 100 );
			SetInt( 100 );

			SetDamage( 10, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 15, 30 );
			SetResistance( ResistanceType.Cold, 15, 30 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 20, 35 );

			SetSkill( SkillName.MagicResist, 50.1, 58.0 );
			SetSkill( SkillName.Tactics, 67.1, 77.0 );
			SetSkill( SkillName.Wrestling, 50.1, 60.0 );
			SetSkill( SkillName.Anatomy, 30.1, 34.0 );

			Fame = 1800;
			Karma = -1800;

			PackItem( new IronIngot( Utility.RandomMinMax( 9, 12 ) ) );

			if ( 0.1 > Utility.RandomDouble() )
				PackItem( new PowerCrystal() );

			if ( 0.15 > Utility.RandomDouble() )
				PackItem( new ClockworkAssembly() );

			if ( 0.2 > Utility.RandomDouble() )
				PackItem( new ArcaneGem() );

			if ( 0.25 > Utility.RandomDouble() )
				PackItem( new Gears( Utility.RandomMinMax( 1, 2 ) ) );

			if ( 0.25 > Utility.RandomDouble() )
				PackItem( new BottleOil( Utility.RandomMinMax( 1, 2 ) ) );

			Tamable = false;
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

		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune { get { return !Core.SE; } }
		public override bool Unprovokable { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override void AlterMeleeDamageTo( Mobile to, ref int damage )
		{
			if ( Utility.RandomBool() && (this.Mana > 14) && to != null )
			{
				damage = (damage + (damage / 2));
				to.SendLocalizedMessage( 1060091 ); // You take extra damage from the crushing attack!
				to.PlaySound( 0x1E1 );
				to.FixedParticles( 0x377A, 1, 32, 0x26da, 0, 0, 0 );
				Mana -= 15;
			}
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			Mobile combatant = Combatant;

			if ( combatant == null || combatant.Deleted || combatant.Map != Map || !InRange( combatant, 12 ) || !CanBeHarmful( combatant ) || !InLOS( combatant ) )
				return;

			if( Utility.Random( 10 ) == 0 )
				PoisonAttack( combatant );

			base.OnDamage( amount, from, willKill );
		}

		public void PoisonAttack( Mobile m )
		{
			DoHarmful( m );
			this.MovingParticles( m, 0x36D4, 1, 0, false, false, 0x3F, 0, 0x1F73, 1, 0, (EffectLayer)255, 0x100 );
			m.ApplyPoison( this, Poison.Regular );
			m.SendLocalizedMessage( 1070821, this.Name ); // %s spits a poisonous substance at you!
		}

		public IronBeetle( Serial serial ) : base( serial )
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