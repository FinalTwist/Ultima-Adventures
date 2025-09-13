using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Sixth
{
	public class ExplosionSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Explosion", "Vas Ort Flam",
				230,
				9041,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot
			);

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

		public ExplosionSpell( Mobile caster, Item scroll )
			: base( caster, scroll, m_Info )
		{
		}

		public override bool DelayedDamageStacking { get { return !Core.AOS; } }

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage { get { return false; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( Caster.CanBeHarmful( m ) && CheckSequence() )
			{
				Mobile attacker = Caster, defender = m;

				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int) this.Circle, Caster, ref m );

				InternalTimer t = new InternalTimer( this, attacker, defender, m );
				t.Start();
			}

			FinishSequence();
		}

		private class InternalTimer : Timer
		{
			private MagerySpell m_Spell;
			private Mobile m_Target;
			private Mobile m_Attacker, m_Defender;

			public InternalTimer( MagerySpell spell, Mobile attacker, Mobile defender, Mobile target )
				: base( TimeSpan.FromSeconds( Core.AOS ? 3.0 : 2.5 ) )
			{
				m_Spell = spell;
				m_Attacker = attacker;
				m_Defender = defender;
				m_Target = target;

				if ( m_Spell != null )
					m_Spell.StartDelayedDamageContext( attacker, this );

				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				if ( m_Attacker.HarmfulCheck( m_Defender ) )
				{
					double damage;

					int nBenefit = 0;
					if ( m_Attacker is PlayerMobile ) // WIZARD
					{
						nBenefit = (int)(m_Attacker.Skills[SkillName.Magery].Value / 5);
					}

					if ( Core.AOS )
					{
						damage = m_Spell.GetNewAosDamage( 40, 1, 5, m_Defender ) + nBenefit;
					}
					else
					{
						damage = Utility.Random( 23, 22 ) + nBenefit;

						if ( m_Spell.CheckResisted( m_Target ) )
						{
							damage *= 0.75;

							m_Target.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
						}

						damage *= m_Spell.GetDamageScalar( m_Target );
					}

					if ( Utility.RandomBool() )
					{
						m_Target.FixedParticles( 0x36BD, 20, 10, 5044, Server.Items.CharacterDatabase.GetMySpellHue( m_Attacker, 0 ), 0, EffectLayer.Head );
					}
					else
					{
						Effects.SendLocationEffect( m_Target.Location, m_Target.Map, 0x3822, 60, 10, Server.Items.CharacterDatabase.GetMySpellHue( m_Attacker, 0 ), 0 );
					}
					m_Target.PlaySound( 0x307 );

					SpellHelper.Damage( m_Spell, m_Target, damage, 0, 100, 0, 0, 0 );

					if ( m_Spell != null )
						m_Spell.RemoveDelayedDamageContext( m_Attacker );
				}
			}
		}

		private class InternalTarget : Target
		{
			private ExplosionSpell m_Owner;

			public InternalTarget( ExplosionSpell owner )
				: base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile) o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}