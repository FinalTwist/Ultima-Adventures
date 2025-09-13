using System;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells.Necromancy
{
	public class PoisonStrikeSpell : NecromancerSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Poison Strike", "In Vas Nox",
				203,
				9031,
				Reagent.NoxCrystal
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( (Core.ML ? 1.75 : 1.5) ); } }

		public override double RequiredSkill { get { return 50.0; } }
		public override int RequiredMana { get { return 17; } }

		public PoisonStrikeSpell( Mobile caster, Item scroll )
			: base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage { get { return false; } }

		public void Target( Mobile m )
		{
			if( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				/* Creates a blast of poisonous energy centered on the target.
				 * The main target is inflicted with a large amount of Poison damage, and all valid targets in a radius of 2 tiles around the main target are inflicted with a lesser effect.
				 * One tile from main target receives 50% damage, two tiles from target receives 33% damage.
				 */

				//CheckResisted( m ); // Check magic resist for skill, but do not use return value	//reports from OSI:  Necro spells don't give Resist gain

				Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 63, 7, 9915, 0 );
				Effects.PlaySound( m.Location, m.Map, 0x229 );

				int nBenefit = 0;
				if ( Caster is PlayerMobile ) // WIZARD
				{
					nBenefit = (int)(Caster.Skills[SkillName.Necromancy].Value / 5);
				}

				nBenefit += (int)(Caster.Skills[SkillName.Poisoning].Value / 4);

				double damage = Utility.RandomMinMax( (Core.ML ? 32 : 36), 40 ) * ((300 + (GetDamageSkill( Caster ) * 9)) / 2500) + nBenefit;
				
				int sdiBonusPVM = AosAttributes.GetValue( Caster, AosAttribute.SpellDamage );
					if ( Server.Misc.MyServerSettings.SpellDamageIncreaseVsMonsters() > 0 && sdiBonusPVM > Server.Misc.MyServerSettings.SpellDamageIncreaseVsMonsters() )
					{
						sdiBonusPVM = Server.Misc.MyServerSettings.SpellDamageIncreaseVsMonsters();
					}

				int sdiBonusPVP = AosAttributes.GetValue( Caster, AosAttribute.SpellDamage );
				if ( Server.Misc.MyServerSettings.SpellDamageIncreaseVsPlayers() > 0 && sdiBonusPVP > Server.Misc.MyServerSettings.SpellDamageIncreaseVsPlayers() )
				{
					sdiBonusPVP = Server.Misc.MyServerSettings.SpellDamageIncreaseVsPlayers();
				}

				sdiBonusPVM = sdiBonusPVM / 25;
				sdiBonusPVP = sdiBonusPVP / 25;

				double pvmDamage = damage * (1 + sdiBonusPVM);
				double pvpDamage = damage * (1 + sdiBonusPVP);

				Map map = m.Map;

				if( map != null )
				{
					List<Mobile> targets = new List<Mobile>();
			
					if ( Caster.CanBeHarmful(m, false ) )
						targets.Add( m );

					foreach( Mobile targ in m.GetMobilesInRange( 2 ) )
						if(!(Caster is BaseCreature && targ is BaseCreature ))
							if( ( targ != Caster && m != targ ) && ( SpellHelper.ValidIndirectTarget( Caster, targ ) && Caster.CanBeHarmful( targ, false) ) )
								targets.Add( targ );

					for( int i = 0; i < targets.Count; ++i )
					{
						Mobile targ = targets[i];
						int num;

						if( targ.InRange( m.Location, 0 ) )
							num = 1;
						else if( targ.InRange( m.Location, 1 ) )
							num = 2;
						else
							num = 3;

						Caster.DoHarmful( targ );
						SpellHelper.Damage( this, targ, ((m.Player && Caster.Player) ? pvpDamage : pvmDamage) / num, 0, 0, 0, 100, 0 );
					}
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private PoisonStrikeSpell m_Owner;

			public InternalTarget( PoisonStrikeSpell owner )
				: base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if( o is Mobile )
					m_Owner.Target( (Mobile)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}