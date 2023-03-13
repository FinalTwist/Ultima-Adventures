using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Undead
{
	public class NecroPoisonSpell : UndeadSpell
	{
		private static SpellInfo m_Info = new SpellInfo( "", "", 239, 9021 );
		public override double RequiredSkill{ get{ return 15.0; } }
		public override int RequiredMana{ get{ return 0; } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }

		public NecroPoisonSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );
				
				SpellHelper.CheckReflect( 3, Caster, ref m );
				
				if ( m.Spell != null )
					m.Spell.OnCasterHurt();
				
				m.Paralyzed = false;
				
				if ( CheckResisted( m ) )
				{
					m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
				}
				else
				{
					int level;
					
					if ( Core.AOS )
					{
						if ( Caster.InRange( m, 2 ) && Caster.Map == m.Map )
						{
							int total = (Caster.Skills.Necromancy.Fixed + Caster.Skills.Poisoning.Fixed) / 2;
							
							if ( total >= 1000 )
								level = 3;
							else if ( total > 850 )
								level = 2;
							else if ( total > 650 )
								level = 1;
							else
								level = 0;
						}
						else
						{
							level = 0;
						}
					}
					else
					{
						double total = Caster.Skills[SkillName.Necromancy].Value + Caster.Skills[SkillName.Poisoning].Value;
						double dist = Caster.GetDistanceToSqrt( m );
						
						if ( dist >= 3.0 )
							total -= (dist - 3.0) * 10.0;
						
						if ( total >= 200.0 && 1 > Utility.Random( 10 ) )
							level = 3;
						else if ( total > (Core.AOS ? 170.1 : 170.0) )
							level = 2;
						else if ( total > (Core.AOS ? 130.1 : 130.0) )
							level = 1;
						else
							level = 0;
					}
					
					m.ApplyPoison( Caster, Poison.GetPoison( level ) );
				}
				
				m.FixedParticles( 0x374A, 10, 15, 5021, EffectLayer.Waist );
				m.PlaySound( 0x474 );
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
			private NecroPoisonSpell m_Owner;
			
			public InternalTarget( NecroPoisonSpell owner ) : base( 12, false, TargetFlags.Harmful )
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
