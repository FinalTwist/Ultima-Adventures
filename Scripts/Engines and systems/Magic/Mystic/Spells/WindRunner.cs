using System;
using Server;
using System.Collections;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Mystic
{
	public class WindRunner : MysticSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Wind Runner", "Beh Ra Mu Ahm",
				269,
				0
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }
		public override int RequiredTithing{ get{ return 250; } }
		public override double RequiredSkill{ get{ return 70.0; } }
		public override int RequiredMana{ get{ return 50; } }
		public override int MysticSpellCircle{ get{ return 2; } }

		public WindRunner( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

        private static Hashtable m_Timers = new Hashtable();

		public static bool UnderEffect( Mobile m )
		{
			return m_Timers.Contains( m );
		}

		public static void RemoveEffect( Mobile m )
		{
			if (StopTimer( m ))
			{
				m.Send(SpeedControl.Disable);
				m.EndAction( typeof( WindRunner ) );
				m.PlaySound( 0x64C ); // Cleansing winds
				m.SendMessage("You feel the wind around you dissipate");
			}
		}

        public static bool StopTimer(Mobile m)
        {
            Timer t = (Timer)m_Timers[m];

            if (t != null)
            {
                t.Stop();
                m_Timers.Remove(m);
            }

            return (t != null);
        }

		public override void OnCast()
		{
			Item shoes = Caster.FindItemOnLayer( Layer.Shoes );

            if ( Caster.Mounted )
            {
                Caster.SendMessage( "You cannot use this ability while on a mount!" );
            }
			else if ( shoes is BootsofHermes )
			{
                Caster.SendMessage( "You cannot use this ability while wearing those magical boots!" );
			}
			else if (Caster is PlayerMobile && ((PlayerMobile)Caster).SoulBound && ((PlayerMobile)Caster).sbmasterspeed)
			{
                Caster.SendMessage( "You are already moving with speed!" );
			}
			else
			{
				if ( !Caster.CanBeginAction( typeof( WindRunner ) ) )
				{
					StopTimer( Caster );
				}

				int TotalTime = (int)( Caster.Skills[SkillName.Wrestling].Value * 5 );
                InternalTimer timer = new InternalTimer( Caster, TimeSpan.FromSeconds( TotalTime ) );

				Caster.Send(SpeedControl.MountSpeed);
				Caster.BeginAction( typeof( WindRunner ) );
				Point3D air = new Point3D( ( Caster.X+1 ), ( Caster.Y+1 ), ( Caster.Z+5 ) );
				Effects.SendLocationParticles(EffectItem.Create(air, Caster.Map, EffectItem.DefaultDuration), 0x2007, 9, 32, 0, 0, 5022, 0);
				Caster.PlaySound( 0x64F ); // Hailstorm

				m_Timers[Caster] = timer;
				timer.Start();
			}

            FinishSequence();
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;

			public InternalTimer( Mobile Caster, TimeSpan duration ) : base( duration )
			{
				Priority = TimerPriority.OneSecond;
				m_m = Caster;
			}

			protected override void OnTick()
			{
				RemoveEffect( m_m );
			}
		}
	}
}