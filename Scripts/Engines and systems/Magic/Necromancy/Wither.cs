using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Misc;

namespace Server.Spells.Necromancy
{
	public class WitherSpell : NecromancerSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Wither", "Kal Vas An Flam",
				203,
				9031,
				Reagent.NoxCrystal,
				Reagent.GraveDust,
				Reagent.PigIron
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.5 ); } }

		public override double RequiredSkill { get { return 60.0; } }

		public override int RequiredMana { get { return 23; } }

		public WitherSpell( Mobile caster, Item scroll )
			: base( caster, scroll, m_Info )
		{
		}

		public override bool DelayedDamage { get { return false; } }

		public override void OnCast()
		{
			if( CheckSequence() )
			{
				/* Creates a withering frost around the Caster,
				 * which deals Cold Damage to all valid targets in a radius of 5 tiles.
				 */

				Map map = Caster.Map;

				if( map != null )
				{
					List<Mobile> targets = new List<Mobile>();

					foreach( Mobile m in Caster.GetMobilesInRange( Core.ML ? 4 : 5 ) )
						if( Caster != m && Caster.InLOS( m ) && SpellHelper.ValidIndirectTarget( Caster, m ) && Caster.CanBeHarmful( m, false ) )
						{
							bool friendlyfire = false;
							if (Caster is BaseCreature && m is BaseCreature && ( (Caster.Karma < 0 && m.Karma <0) || (Caster.Karma > 0 && m.Karma > 0) ) )
								friendlyfire = true;
							
							if (!friendlyfire)
								targets.Add( m );
						}

					Effects.PlaySound( Caster.Location, map, 0x1FB );
					Effects.PlaySound( Caster.Location, map, 0x10B );
					Effects.SendLocationParticles( EffectItem.Create( Caster.Location, map, EffectItem.DefaultDuration ), 0x37CC, 1, 40, 97, 3, 9917, 0 );

					for( int i = 0; i < targets.Count; ++i )
					{
						Mobile m = targets[ i ];

						Caster.DoHarmful( m );
						m.FixedParticles( 0x374A, 1, 15, 9502, 97, 3, (EffectLayer)255 );

						double damage = Utility.RandomMinMax( 30, 35 );

						damage *= ( 300 + ( m.Karma / 100 ) + ( GetDamageSkill( Caster ) * 10 ) );
						damage /= 1000;
						int SDICap = MyServerSettings.RealSpellDamageCap();
						int sdiBonus = AosAttributes.GetValue( Caster, AosAttribute.SpellDamage );
						if (sdiBonus > SDICap) {
							sdiBonus = SDICap;
						}
						// PvP spell damage increase cap of 15% from an item�s magic property in Publish 33(SE)
						if( Core.SE && m.Player && Caster.Player && sdiBonus > 15 )
							sdiBonus = 15;

						damage *= ( 100 + sdiBonus );
						damage /= 100;

						int nBenefit = 0;
						if ( Caster is PlayerMobile ) // WIZARD
						{
							nBenefit = (int)(Caster.Skills[SkillName.Necromancy].Value / 5);
						}

						damage = damage + nBenefit;

						SpellHelper.Damage( this, m, damage, 0, 0, 100, 0, 0 );
					}
				}
			}

			FinishSequence();
		}
	}
}