using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Spells;

namespace Server.Spells.Herbalist
{
	public class VolcanicEruptionSpell : HerbalistSpell
	{
		private static SpellInfo m_Info = new SpellInfo( "", "", 239, 9021 );
		public override int HerbalistSpellCircle{ get{ return 8; } }
		public override double CastDelay{ get{ return 2.0; } }
		public override double RequiredSkill{ get{ return 80.0; } }
		public override int RequiredMana{ get{ return 0; } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }

		public VolcanicEruptionSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckSequence() )
			{
				SpellHelper.Turn( Caster, p );

				if ( p is Item )
					p = ((Item)p).GetWorldLocation();

				double damage = Utility.Random( 27, 22 );

				ArrayList targets = new ArrayList();

				IPooledEnumerable eable = Caster.Map.GetMobilesInRange( new Point3D( p ), 1 + (int)(Caster.Skills[DamageSkill].Value / 10.0) );

				foreach ( Mobile m in eable )
				{
					if ( Caster != m )
						targets.Add( m );
				}

				eable.Free();

				if ( targets.Count > 0 )
				{
					for ( int i = 0; i < targets.Count; ++i )
					{
						Mobile m = (Mobile)targets[i];

						double toDeal = damage;

						if ( CheckResisted( m ) )
						{
							toDeal *= 0.7;
							m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
						}

						Caster.DoHarmful( m );
						SpellHelper.Damage( this, m, toDeal, 50, 100, 0, 0, 0 );

						m.FixedParticles( 0x3709, 20, 10, 5044, EffectLayer.RightFoot );
						m.PlaySound( 0x21F );
						m.FixedParticles( 0x36BD, 10, 30, 5052, EffectLayer.Head );
						m.PlaySound( 0x208 );
					}
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private VolcanicEruptionSpell m_Owner;

			public InternalTarget( VolcanicEruptionSpell owner ) : base( 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;

				if ( p != null )
					m_Owner.Target( p );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}