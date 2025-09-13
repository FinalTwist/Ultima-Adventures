using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a pile of metal" )]
	public class Exodus : BaseCreature
	{
		private bool m_FieldActive;
		public bool FieldActive{ get{ return m_FieldActive; } }
		public bool CanUseField{ get{ return Hits >= HitsMax * 9 / 10; } } // TODO: an OSI bug prevents to verify this
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }

		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 100; } }
		public override int BreathEffectHue{ get{ return 0x9C2; } }
		public override int BreathEffectSound{ get{ return 0x665; } }
		public override int BreathEffectItemID{ get{ return 0x3818; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 14 ); }

		[Constructable]
		public Exodus () : base( AIType.AI_Melee, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{
			Name = "Exodus";
			Hue = 0x9C4;
			BaseSoundID = 0x300;
			Body = 191;

			SetStr( 900, 1200 );
			SetDex( 177, 255 );
			SetInt( 500, 750 );

			SetHits( 8000, 10000 );

			SetDamage( 30, 60 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 45, 60 );
			SetResistance( ResistanceType.Fire, 20, 40 );
			SetResistance( ResistanceType.Cold, 20, 40 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 20, 40 );

			SetSkill( SkillName.Anatomy, 110.0 );
			SetSkill( SkillName.EvalInt, 110.0 );
			SetSkill( SkillName.MagicResist, 110.0 );
			SetSkill( SkillName.Tactics, 110.0 );
			SetSkill( SkillName.Wrestling, 110.0 );

			Fame = 28000;
			Karma = -28000;

			VirtualArmor = 60;

			PackItem( new IronIngot( Utility.RandomMinMax( 20, 50 ) ) );
			PackItem( new PowerCrystal() );
			PackItem( new ArcaneGem() );
			PackItem( new ClockworkAssembly() );
			PackItem( new BottleOil( Utility.RandomMinMax( 3, 8 ) ) );
			PackItem( new Gears( Utility.RandomMinMax( 3, 8 ) ) );

			m_FieldActive = CanUseField;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich );
		}

		public override bool AutoDispel{ get{ return true; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 6; } }
		public override bool BardImmune { get { return true; } }

		public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			if ( m_FieldActive )
				damage = (int)( damage * 0.25 ); // no melee damage when the field is up
		}

		public override void AlterSpellDamageFrom( Mobile caster, ref int damage )
		{
			if ( !m_FieldActive )
				damage = (int)( damage * 0.25 ); // no spell damage when the field is down
		}

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ reflect = true; } // 25% spells are reflected back to the caster
			else { reflect = false; }
		}

		public override void OnDamagedBySpell( Mobile from )
		{
			if( from != null && from.Alive && 0.4 > Utility.RandomDouble() )
			{
				SendEBolt( from );
			}

			if ( !m_FieldActive )
			{
				this.FixedParticles( 0, 10, 0, 0x2522, EffectLayer.Waist );
			}
			else if ( m_FieldActive && !CanUseField )
			{
				m_FieldActive = false;

				this.FixedParticles( 0x3735, 1, 30, 0x251F, EffectLayer.Waist );
			}
		}

        public override void OnAfterSpawn()
        {
			base.OnAfterSpawn();
			Worlds.MoveToRandomDungeon( this );
			Server.Misc.IntelligentAction.BurnAway( this );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( m_FieldActive )
			{
				this.FixedParticles( 0x376A, 20, 10, 0x2530, EffectLayer.Waist );

				PlaySound( 0x2F4 );

				attacker.SendAsciiMessage( "Your weapon is less effective with the creature's magical barrier up!" );

				if ( attacker is BaseCreature ) // KILL ANY PETS PLAYERS FOOLISHLY BROUGHT WITH THEM
				{
					BaseCreature pet = (BaseCreature)attacker;
					if( ( pet.Summoned || pet.Controlled ) && !(pet is FrankenFighter) && !(pet is Robot) && !(pet is GolemFighter) && !(pet is HenchmanMonster) && !(pet is HenchmanWizard) && !(pet is HenchmanArcher) && !(pet is HenchmanFighter) )
					{
						SendEBoltOnPet( attacker );
					}
				}
			}

			if( attacker != null && attacker.Alive && attacker.Weapon is BaseRanged && 0.4 > Utility.RandomDouble() )
			{
				SendEBolt( attacker );
			}
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
   			c.DropItem( new DarkCoreExodus() );
		}

		public override bool OnBeforeDeath()
		{
			Item reward = new SummonReward();
			reward.Hue = 0x835;
			reward.ItemID = 0x2105;
			reward.Name = "Statue of Exodus";
			this.PackItem( reward );

			this.Body = 752;
			Server.Misc.IntelligentAction.BurnAway( this );
			Server.Misc.LoggingFunctions.LogSlayingLord( this.LastKiller, this.Name );
			return base.OnBeforeDeath();
		}

		public override void OnThink()
		{
			base.OnThink();

			if ( !m_FieldActive && !IsHurt() )
				m_FieldActive = true;
		}

		public override bool Move( Direction d )
		{
			bool move = base.Move( d );

			if ( move && m_FieldActive && this.Combatant != null )
				this.FixedParticles( 0, 10, 0, 0x2530, EffectLayer.Waist );

			return move;
		}		

		public void SendEBolt( Mobile to )
		{
			this.MovingParticles( to, 0x3818, 7, 0, false, true, 0xBE3, 0xFCB, 0x211 );
			to.PlaySound( 0x229 );
			this.DoHarmful( to );
			AOS.Damage( to, this, 60, 0, 0, 0, 0, 100 );
		}

		public void SendEBoltOnPet( Mobile to )
		{
			this.MovingParticles( to, 0x3818, 7, 0, false, true, 0xBE3, 0xFCB, 0x211 );
			to.PlaySound( 0x229 );
			this.DoHarmful( to );
			AOS.Damage( to, this, 1000, 0, 0, 0, 0, 100 );
		}

		public Exodus( Serial serial ) : base( serial )
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

			m_FieldActive = CanUseField;
		}
	}
}

namespace Server.Items
{
	public class DarkCoreExodus : Item
	{
		[Constructable]
		public DarkCoreExodus() : base( 0x1CD )
		{
			Hue = 0x497;
			Name = "dark core of Exodus";
		}

		public DarkCoreExodus( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}