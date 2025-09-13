// $Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Items/Weapons/Abilities/ForceofNature.cs#2 $

using System;
using Server;
using System.Collections;

namespace Server.Items
{
	public class ForceOfNature : WeaponAbility
	{
		public ForceOfNature()
		{
		}

		public override int BaseMana { get { return 40; } }

		public override void OnHit(Mobile attacker, Mobile defender, int damage)
		{
			if (!Validate(attacker) || !CheckMana(attacker, true))
				return;

			ClearCurrentAbility(attacker);

			attacker.SendLocalizedMessage(1074374); // You attack your enemy with the force of nature!
			defender.SendLocalizedMessage(1074375); // You are assaulted with great force!

			defender.PlaySound(0x22F);
			defender.FixedParticles(0x36CB, 1, 9, 9911, 67, 5, EffectLayer.Head);
			defender.FixedParticles(0x374A, 1, 17, 9502, 1108, 4, (EffectLayer)255);

			if (!m_Table.Contains(defender))
			{
				Timer t = new InternalTimer(defender, attacker);
				t.Start();

				m_Table[defender] = t;
			}
		}
		private static Hashtable m_Table = new Hashtable();

		public static bool RemoveCurse(Mobile m)
		{
			Timer t = (Timer)m_Table[m];

			if (t == null)
				return false;

			t.Stop();
			m.SendLocalizedMessage(1061687); // You can breath normally again.

			m_Table.Remove(m);
			return true;
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Target, m_From;
			private double m_MinBaseDamage, m_MaxBaseDamage;

			private DateTime m_NextHit;
			private int m_HitDelay;

			private int m_Count, m_MaxCount;

			public InternalTimer(Mobile target, Mobile from)
				: base(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(0.1))
			{
				Priority = TimerPriority.FiftyMS;

				m_Target = target;
				m_From = from;

				double spiritLevel = from.Skills[SkillName.SpiritSpeak].Value / 15;

				m_MinBaseDamage = spiritLevel - 2;
				m_MaxBaseDamage = spiritLevel + 1;

				m_HitDelay = 5;
				m_NextHit = DateTime.UtcNow + TimeSpan.FromSeconds(m_HitDelay);

				m_Count = (int)spiritLevel;

				if (m_Count < 4)
					m_Count = 4;

				m_MaxCount = m_Count;
			}

			protected override void OnTick()
			{
				if (!m_Target.Alive)
				{
					m_Table.Remove(m_Target);
					Stop();
				}

				if (!m_Target.Alive || DateTime.UtcNow < m_NextHit)
					return;

				--m_Count;

				if (m_HitDelay > 1)
				{
					if (m_MaxCount < 5)
					{
						--m_HitDelay;
					}
					else
					{
						int delay = (int)(Math.Ceiling((1.0 + (5 * m_Count)) / m_MaxCount));

						if (delay <= 5)
							m_HitDelay = delay;
						else
							m_HitDelay = 5;
					}
				}

				if (m_Count == 0)
				{
					m_Target.SendLocalizedMessage(1061687); // You can breath normally again.
					m_Table.Remove(m_Target);
					Stop();
				}
				else
				{
					m_NextHit = DateTime.UtcNow + TimeSpan.FromSeconds(m_HitDelay);

					double damage = m_MinBaseDamage + (Utility.RandomDouble() * (m_MaxBaseDamage - m_MinBaseDamage));

					damage *= (3 - (((double)m_Target.Stam / m_Target.StamMax) * 2));

					if (damage < 1)
						damage = 1;

					if (!m_Target.Player)
						damage *= 1.75;

					AOS.Damage(m_Target, m_From, (int)damage, 0, 0, 0, 100, 0);
				}
			}
		}
	}
}
