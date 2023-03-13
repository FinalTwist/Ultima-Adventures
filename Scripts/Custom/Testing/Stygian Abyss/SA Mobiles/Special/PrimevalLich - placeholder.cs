/* Based on Neira, still to get detailed information on the Primeval Lich */
using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Engines.CannedEvil;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public class PrimevalLich : BaseChampion
	{
		public override ChampionSkullType SkullType{ get{ return ChampionSkullType.Death; } }

		public override Type[] UniqueList{ get { return new Type[] {}; } } // FIXME: Doesn't work. Placeholder.

		public override Type[] SharedList{ get { return new Type[] {}; } } // FIXME: Doesn't work. Placeholder.

		public override Type[] DecorativeList{ get { return new Type[] {}; } } // FIXME: Doesn't work. Placeholder.
 
		public override MonsterStatuetteType[] StatueTypes { get { return new MonsterStatuetteType[] { }; } }
		
		[Constructable]
		public PrimevalLich() : base( AIType.AI_Mage )
		{
			Name = "The Primeval Lich";
			Body = 830;

			SetStr( 305, 425 );
			SetDex( 72, 150 );
			SetInt( 505, 950 );

			SetHits( 4800 );
			SetStam( 102, 300 );

			SetDamage( 45, 89 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 60, 70 );
			SetResistance( ResistanceType.Fire, 45, 55 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 80, 90 );
			SetResistance( ResistanceType.Energy, 50, 60 );

			SetSkill( SkillName.EvalInt, 120.0 );
			SetSkill( SkillName.Magery, 120.0 );
			SetSkill( SkillName.Meditation, 120.0 );
            SetSkill( SkillName.Necromancy, 120.0 );
            SetSkill (SkillName.SpiritSpeak, 120.0 );
			SetSkill( SkillName.MagicResist, 150.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 100.0 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 30;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 3 );
			AddLoot( LootPack.Meager );
		}

		public void ChangeCombatant()
		{
			ForceReacquire();
			BeginFlee( TimeSpan.FromSeconds( 2.5 ) );
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool Uncalmable{ get{ return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

		public override bool ShowFameTitle{ get{ return false; } }
		public override bool ClickTitle{ get{ return false; } }

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.1 >= Utility.RandomDouble() ) // 10% chance to drop or throw an unholy bone
				AddUnholyBone( defender, 0.25 );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.1 >= Utility.RandomDouble() ) // 10% chance to drop or throw an unholy bone
				AddUnholyBone( attacker, 0.25 );
		}

		public override void AlterDamageScalarFrom( Mobile caster, ref double scalar )
		{
			base.AlterDamageScalarFrom( caster, ref scalar );

			if ( 0.1 >= Utility.RandomDouble() ) // 10% chance to throw an unholy bone
				AddUnholyBone( caster, 1.0 );
		}

		public void AddUnholyBone( Mobile target, double chanceToThrow )
		{
			if( this.Map == null )
				return;

			if ( chanceToThrow >= Utility.RandomDouble() )
			{
				Direction = GetDirectionTo( target );
				MovingEffect( target, 0xF7E, 10, 1, true, false, 0x496, 0 );
				new DelayTimer( this, target ).Start();
			}
			else
			{
				new UnholyBone().MoveToWorld( Location, Map );
			}
		}

		private class DelayTimer : Timer
		{
			private Mobile m_Mobile;
			private Mobile m_Target;

			public DelayTimer( Mobile m, Mobile target ) : base( TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Mobile = m;
				m_Target = target;
			}

			protected override void OnTick()
			{
				if ( m_Mobile.CanBeHarmful( m_Target ) )
				{
					m_Mobile.DoHarmful( m_Target );
					AOS.Damage( m_Target, m_Mobile, Utility.RandomMinMax( 10, 20 ), 100, 0, 0, 0, 0 );
					new UnholyBone().MoveToWorld( m_Target.Location, m_Target.Map );
				}
			}
		}

		public PrimevalLich( Serial serial ) : base( serial )
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
