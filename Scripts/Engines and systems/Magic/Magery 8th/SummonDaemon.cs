using System;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Eighth
{
	public class SummonDaemonSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Summon Daemon", "Kal Vas Xen Corp",
				269,
				9050,
				false,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }

		public SummonDaemonSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if ( (Caster.Followers + (Core.SE ? 4 : 5)) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if ( CheckSequence() )
			{	
				TimeSpan duration = TimeSpan.FromSeconds( (Caster.Skills[SkillName.Magery].Value + Caster.Skills[SkillName.EvalInt].Value) * 9 );

				double checkvalue = 120;
				
				if (Caster is PlayerMobile && ((PlayerMobile)Caster).Sorcerer() )
					checkvalue = 90;
					
				if ( Caster.CheckTargetSkill( SkillName.EvalInt, Caster, 0.0, checkvalue ) )
				{
					BaseCreature m_Daemon = new SummonedDaemonGreater();
					SpellHelper.Summon( m_Daemon, Caster, 0x216, duration, false, false );
					m_Daemon.FixedParticles(0x3728, 8, 20, 5042, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.Head );
				}
				else
				{
					BaseCreature m_Daemon = new SummonedDaemon();
					SpellHelper.Summon( m_Daemon, Caster, 0x216, duration, false, false );
					m_Daemon.FixedParticles(0x3728, 8, 20, 5042, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.Head );
				}
			}

			FinishSequence();
		}
	}
}
