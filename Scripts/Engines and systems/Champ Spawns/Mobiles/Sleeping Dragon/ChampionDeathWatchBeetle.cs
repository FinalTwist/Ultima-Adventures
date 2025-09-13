/**********************************************************
	Name: Champion Spawn Monster
	Scripted By: Formosa
	Version: v1.0
	Update Date: March 10, 2013

	Notes: 
	Anyone can modify/redistribute this 
	Do Not Remove/Change This Header!!	
**********************************************************/

using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
	[CorpseName( "a deathwatchbeetle champion corpse" )]
	[TypeAlias( "Server.Mobiles.DeathWatchBeetle" )]
	public class ChampionDeathwatchBeetle : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.CrushingBlow;
		}

		[Constructable]
		public ChampionDeathwatchBeetle() : base( AIType.AI_Melee, Core.ML ? FightMode.Closest : FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			ChampionHue();

			Name = "a deathwatch beetle";
			Body = 242;

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

			Fame = 1400;
			Karma = -1400;

			VirtualArmor = 16;

			if ( Utility.RandomDouble() < .5 )
				PackItem( Engines.Plants.Seed.RandomBonsaiSeed() );

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 120.0;
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
			AddLoot( LootPack.Gems );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			
			c.DropItem( new Apple( 6 ) );
			
			if ( Utility.RandomDouble() < 0.05 )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: c.DropItem( new Necklace() ); break;
					case 1: c.DropItem( new GoldRing() ); break;
				}
			}
			
			if ( Utility.RandomDouble() < 0.04 )
				c.DropItem( new GoldBracelet() );
						
			if ( Utility.RandomDouble() < 0.03 )
				c.DropItem( new GoldBeadNecklace() );
					
			if ( Utility.RandomDouble() < 0.02 )
				c.DropItem( new GoldNecklace() );
				
			if ( Utility.RandomDouble() < 0.01 )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: c.DropItem( new Beads() ); break;
					case 1: c.DropItem( new GoldEarrings() ); break;
				}
			}
		}

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

		public override int Hides{ get{ return 8; } }

		public virtual void ChampionHue()
		{
			switch ( Utility.Random ( 10 ) )
			{
				case 0: Hue = ( 11 ); break;
				case 1: Hue = ( 25 ); break;
				case 2: Hue = ( 40 ); break;
				case 3: Hue = ( 44 ); break;
				case 4: Hue = ( 33 ); break;
				case 5: Hue = ( 49 ); break;
				case 6: Hue = ( 47 ); break;
				case 7: Hue = ( 56 ); break;
				case 8: Hue = ( 76 ); break;
				case 9: Hue = ( 95 ); break;
			}
		}
	
		public ChampionDeathwatchBeetle( Serial serial ) : base( serial )
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