using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;

namespace Server.Spells.HolyMan
{
	public class SacredBoonSpell : HolyManSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Sacred Boon", "Sacrum Munus",
				266,
				9040
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 4 ); } }
		public override int RequiredTithing{ get{ return 100; } }
		public override double RequiredSkill{ get{ return 60.0; } }
		public override int RequiredMana{ get{ return 40; } }

		private static Hashtable m_Table = new Hashtable();

		public SacredBoonSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public static bool HasEffect( Mobile m )
		{
			return ( m_Table[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			Timer t = (Timer)m_Table[m];

			if ( t != null )
			{
				t.Stop();
				m_Table.Remove( m );
			}
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

			if ( m_Table.Contains( m ) )
			{
				Caster.LocalOverheadMessage( MessageType.Regular, 0x481, false, "That target already has this affect." );
			}

			else if ( CheckBSequence( m, false ) )
			{
				SpellHelper.Turn( Caster, m );

				Timer t = new InternalTimer( m, Caster );
				t.Start();
				m_Table[m] = t;
				m.PlaySound( 0x202 );
				m.FixedParticles( 0x376A, 1, 62, 9923, 3, 3, EffectLayer.Waist );
				m.FixedParticles( 0x3779, 1, 46, 9502, 5, 3, EffectLayer.Waist );
				m.SendMessage( "A holy aura surrounds you causing your wounds to heal faster." );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private SacredBoonSpell m_Owner;

			public InternalTarget( SacredBoonSpell owner ) : base( 12, false, TargetFlags.Beneficial )
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

		private class InternalTimer : Timer
		{
			private Mobile dest, source;
			private DateTime NextTick;
			private DateTime Expire;

			public InternalTimer( Mobile m, Mobile from ) : base( TimeSpan.FromSeconds( 0.1 ), TimeSpan.FromSeconds( 0.1 ) )
			{
				dest = m;
				source = from;
				Priority = TimerPriority.FiftyMS;
				Expire = DateTime.UtcNow + TimeSpan.FromSeconds( 30.0 );
			}

			protected override void OnTick()
			{
				if ( !dest.CheckAlive() )
				{
					Stop();
					m_Table.Remove( dest );
				}

				if ( DateTime.UtcNow < NextTick )
					return;

				if ( DateTime.UtcNow >= NextTick )
				{
					double heal = 5 + ( source.Skills[SkillName.Healing].Value / 15.0 ) + ( source.Skills[SkillName.SpiritSpeak].Value / 15.0 );

					dest.Heal( Server.Misc.MyServerSettings.PlayerLevelMod( (int)heal, dest ) );

					dest.PlaySound( 0x202 );
					dest.FixedParticles( 0x376A, 1, 62, 9923, 3, 3, EffectLayer.Waist );
					dest.FixedParticles( 0x3779, 1, 46, 9502, 5, 3, EffectLayer.Waist );
					NextTick = DateTime.UtcNow + TimeSpan.FromSeconds( 4 );
				}

				if ( DateTime.UtcNow >= Expire )
				{
					Stop();
					if ( m_Table.Contains( dest ) )
						m_Table.Remove( dest );
				}
			}
		}
	}
}
