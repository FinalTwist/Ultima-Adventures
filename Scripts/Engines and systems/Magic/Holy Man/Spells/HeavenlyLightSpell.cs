using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Spells;

namespace Server.Spells.HolyMan
{
	public class HeavenlyLightSpell : HolyManSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Heavenly Light", "Caelesti Lumine",
				266,
				9040
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }
		public override int RequiredTithing{ get{ return 20; } }
		public override double RequiredSkill{ get{ return 10.0; } }
		public override int RequiredMana{ get{ return 5; } }

		public HeavenlyLightSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new NightSightTarget( this );
		}

		private class NightSightTarget : Target
		{
			private Spell m_Spell;

			public NightSightTarget( Spell spell ) : base( 12, false, TargetFlags.Beneficial )
			{
				m_Spell = spell;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile && m_Spell.CheckBSequence( (Mobile) targeted ) )
				{
					Mobile targ = (Mobile)targeted;

					SpellHelper.Turn( m_Spell.Caster, targ );

					if ( targ.BeginAction( typeof( LightCycle ) ) )
					{
						new LightCycle.NightSightTimer( targ ).Start();
						int level = (int)( LightCycle.DungeonLevel * ( (Core.AOS ? targ.Skills[SkillName.Healing].Value : from.Skills[SkillName.Healing].Value )/ 100 ) );

						if ( level < 0 )
							level = 0;

						targ.LightLevel = level;

						targ.FixedParticles( 0x376A, 9, 32, 5007, EffectLayer.Waist );
						targ.PlaySound( 0x1E3 );

						BuffInfo.AddBuff( targ, new BuffInfo( BuffIcon.NightSight, 1075643 ) );	//Night Sight/You ignore lighting effects
					}
					else
					{
						from.SendMessage( "{0} already have nightsight.", from == targ ? "You" : "They" );
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
