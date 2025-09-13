using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Herbalist
{
	public class FireflySpell : HerbalistSpell
	{
		private static SpellInfo m_Info = new SpellInfo( "", "", 239, 9021 );
		public override int HerbalistSpellCircle{ get{ return 4; } }
		public override double CastDelay{ get{ return 1.0; } }
      	public override double RequiredSkill{ get{ return 55.0; } }
      	public override int RequiredMana{ get{ return 0; } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }
		
		public FireflySpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}
		
		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( !(m is Mobile) )
			{
				Caster.SendMessage( "Fireflies will never distract that" );
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );
				
				SpellHelper.CheckReflect( 3, Caster, ref m );
				
				if ( m.Spell != null )
					m.Spell.OnCasterHurt();
				
				m.Paralyzed = false;

				if ( !Caster.CanBeHarmful( m, false ) )
				{
					Caster.SendLocalizedMessage( 1049528 );
				}
				else if ( m is BaseCreature && ((BaseCreature)m).Uncalmable )
				{
					Caster.SendMessage( "Fireflies will never distract that" );
				}
				else if ( CheckResisted( m ) )
				{
					Caster.SendMessage( "They ignore the fireflies" );
					m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
				}
				else
				{
					if ( m is BaseCreature )
					{
						BaseCreature bc = (BaseCreature)m;

						Caster.SendMessage( "The fireflies dazzle them out of battle" );

						m.Combatant = null;
						m.Warmode = false;
						double seconds = Caster.Skills[SkillName.AnimalLore].Value;

						bc.Pacify( Caster, DateTime.UtcNow + TimeSpan.FromSeconds( seconds ) );
					}
					else
					{
						Caster.SendMessage( "The fireflies dazzle them out of battle" );
						m.SendMessage( "You forget you were fighting while surrounded by fireflies" );
						m.Combatant = null;
						m.Warmode = false;
					}
					m.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
					m.PlaySound( 0x1E0 );
				}
			}
			FinishSequence();
		}

		public virtual bool CheckResisted( Mobile target )
		{
			double n = GetResistPercent( target );

			n /= 100.0;

			if( n <= 0.0 )
				return false;

			if( n >= 1.0 )
				return true;

			int maxSkill = (1 + 8) * 10;
			maxSkill += (1 + (8 / 6)) * 25;

			if( target.Skills[SkillName.MagicResist].Value < maxSkill )
				target.CheckSkill( SkillName.MagicResist, 0.0, 120.0 );

			return (n >= Utility.RandomDouble());
		}

		public virtual double GetResistPercentForCircle( Mobile target )
		{
			double firstPercent = target.Skills[SkillName.MagicResist].Value / 5.0;
			double secondPercent = target.Skills[SkillName.MagicResist].Value - (((Caster.Skills[CastSkill].Value - 20.0) / 5.0) + (1 + 8) * 5.0);

			return (firstPercent > secondPercent ? firstPercent : secondPercent) / 2.0; // Seems should be about half of what stratics says.
		}

		public virtual double GetResistPercent( Mobile target )
		{
			return GetResistPercentForCircle( target );
		}

		private class InternalTarget : Target
		{
			private FireflySpell m_Owner;
			
			public InternalTarget( FireflySpell owner ) : base( 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}
			
			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}
			
			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}