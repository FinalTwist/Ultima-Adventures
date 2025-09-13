using System;
using Server.Targeting;
using Server.Network;
using Server;

namespace Server.Spells.Undead
{
	public class UndeadEyesSpell : UndeadSpell
	{
		private static SpellInfo m_Info = new SpellInfo( "", "", 239, 9021 );
		public override double RequiredSkill{ get{ return 10.0; } }
		public override int RequiredMana{ get{ return 0; } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }

		
		public UndeadEyesSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override void OnCast()
		{
			Caster.Target = new EyesSpellTarget( this );
		}
		
		private class EyesSpellTarget : Target
		{
			private Spell m_Spell;
			
			public EyesSpellTarget( Spell spell ) : base( 10, false, TargetFlags.None )
			{
				m_Spell = spell;
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile && m_Spell.CheckSequence() )
				{
					Mobile targ = (Mobile)targeted;
					
					SpellHelper.Turn( m_Spell.Caster, targ );
					
					if ( targ.BeginAction( typeof( LightCycle ) ) )
					{
						new LightCycle.NightSightTimer( targ ).Start();
						int level = (int)Math.Abs( LightCycle.DungeonLevel * ( m_Spell.Caster.Skills[SkillName.Necromancy].Base / 100 ) );
						
						if ( level > 25 || level < 0 )
							level = 25;

						level = 12;
						
						targ.LightLevel = level;
						
						targ.FixedParticles( 0x376A, 9, 32, 5007, EffectLayer.Waist );
						targ.PlaySound( 0x37A );
					}
					else
					{
						from.SendMessage( "{0} already have horde minions eyes.", from == targ ? "You" : "They" );
					}
				}
				
				m_Spell.FinishSequence();
			}
			
			protected override void OnTargetFinish( Mobile from )
			{
				m_Spell.FinishSequence();
			}
		}
	}
}
