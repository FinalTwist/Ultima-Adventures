
using System;
using System.Collections;
using Server.Items;

namespace Server.Items
{
	/// <summary>
	/// Gain a defensive advantage over your primary opponent for a short time.
	/// </summary>
	public class Feint : WeaponAbility
	{
		private static Hashtable m_Registry = new Hashtable();
		public static Hashtable Registry { get { return m_Registry; } }

		public Feint()
		{
		}

		public override int BaseMana { get { return 25; } }

		public override bool CheckSkills( Mobile from )
		{
			return base.CheckSkills( from );
		}

		public override void OnHit( Mobile attacker, Mobile defender, int damage )
		{
			if( !Validate( attacker ) || !CheckMana( attacker, true ) )
				return;

			if( Registry.Contains( defender ) )
			{
				FeintTimer existingtimer = (FeintTimer)Registry[defender];
				existingtimer.Stop();
				Registry.Remove( defender );
			}

			ClearCurrentAbility( attacker );

			attacker.SendLocalizedMessage( 1063360 ); // You baffle your target with a feint!
			defender.SendLocalizedMessage( 1063361 ); // You were deceived by an attacker's feint!

			attacker.FixedParticles( 0x3728, 1, 13, 0x7F3, 0x962, 0, EffectLayer.Waist );

			Timer t = new FeintTimer( defender, (int)(20.0 + 3.0 * (Math.Max( attacker.Skills[SkillName.Tactics].Value, attacker.Skills[SkillName.Anatomy].Value ) - 50.0) / 7.0) );	//20-50 % decrease

			t.Start();
			Registry.Add( defender, t );
		}

		public class FeintTimer : Timer
		{
			private Mobile m_Defender;
			private int m_SwingSpeedReduction;

			public int SwingSpeedReduction { get { return m_SwingSpeedReduction; } }

			public FeintTimer( Mobile defender, int swingSpeedReduction )
				: base( TimeSpan.FromSeconds( 6.0 ) )
			{
				m_Defender = defender;
				m_SwingSpeedReduction = swingSpeedReduction;
				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				Registry.Remove( m_Defender );
			}
		}
	}
}