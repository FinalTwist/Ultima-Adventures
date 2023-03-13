using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Undead
{
	public class UndeadCurePoisonSpell : UndeadSpell
	{
		private static SpellInfo m_Info = new SpellInfo( "", "", 239, 9021 );
		public override double RequiredSkill{ get{ return 25.0; } }
		public override int RequiredMana{ get{ return 0; } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }

		public UndeadCurePoisonSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
			else if ( CheckBSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );
				
				Poison p = m.Poison;
				
				if ( p != null )
				{
					int chanceToCure = 10000 + (int)(Caster.Skills[SkillName.Necromancy].Value * 75) - ((p.Level + 1) * 1750);
					chanceToCure /= 100;
					
					if ( chanceToCure > Utility.Random( 100 ) )
					{
						if ( m.CurePoison( Caster ) )
						{
							if ( Caster != m )
								Caster.SendLocalizedMessage( 1010058 ); // You have cured the target of all poisons!
							
							m.SendLocalizedMessage( 1010059 ); // You have been cured of all poisons.
						}
					}
					else
					{
						m.SendLocalizedMessage( 1010060 ); // You have failed to cure your target!
					}
				}
				
				m.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
				m.PlaySound( 0x19C );
			}
			
			FinishSequence();
		}
		
		public class InternalTarget : Target
		{
			private UndeadCurePoisonSpell m_Owner;
			
			public InternalTarget( UndeadCurePoisonSpell owner ) : base( 12, false, TargetFlags.Beneficial )
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
