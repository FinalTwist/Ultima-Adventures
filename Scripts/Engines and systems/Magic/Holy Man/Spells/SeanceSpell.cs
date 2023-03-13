using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Spells;

namespace Server.Spells.HolyMan
{
	public class SeanceSpell : HolyManSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Seance", "Spiritus Mundi",
				266,
				9040
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }
		public override int RequiredTithing{ get{ return 120; } }
		public override double RequiredSkill{ get{ return 60.0; } }
		public override int RequiredMana{ get{ return 30; } }

        private int m_NewBody;
        private int m_OldBody;
        private int m_NewHue;
        private int m_OldHue;

        public SeanceSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override bool CheckCast(Mobile caster)
        {
            if (Caster.Mounted)
            {
                Caster.SendLocalizedMessage(1042561); //Please dismount first.
                return false;
            }
            else if (TransformationSpellHelper.UnderTransformation(Caster))
            {
                Caster.SendMessage("You cannot enter the realm of the dead while in that form.");
                return false;
            }
			else if ( DisguiseTimers.IsDisguised( Caster ) )
			{
                Caster.SendMessage("You cannot enter the realm of the dead while disguised.");
                return false;
            }
            else if (Caster.BodyMod == 183 || Caster.BodyMod == 184)
            {
                Caster.SendMessage("You cannot enter the realm of the dead without removing your paint.");
                return false;
            }
            else if (!Caster.CanBeginAction(typeof(SeanceSpell)))
            {
                Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
                return false;
            }
            else
            {
				m_NewBody = 970;
				m_NewHue = 0x9C4;
            }
            m_OldBody = Caster.Body;
            m_OldHue = Caster.Hue;
            return true;
        }

        public override void OnCast()
        {
            if (!CheckSequence())
            {
                return;
            }
            else if (!Caster.CanBeginAction(typeof(SeanceSpell)))
            {
                Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
            }
            else if (TransformationSpellHelper.UnderTransformation(Caster))
            {
                Caster.SendMessage("You cannot enter the realm of the dead while in that form.");
            }
			else if ( DisguiseTimers.IsDisguised( Caster ) )
			{
                Caster.SendMessage("You cannot enter the realm of the dead while disguised.");
            }
            else if (Caster.BodyMod == 183 || Caster.BodyMod == 184)
            {
                Caster.SendMessage("You cannot enter the realm of the dead without removing your paint.");
            }
            else if (!Caster.CanBeginAction(typeof(Server.Spells.Fifth.IncognitoSpell)) || Caster.IsBodyMod)
            {
                DoFizzle();
            }
            else if (CheckSequence())
            {
                if (Caster.BeginAction(typeof(SeanceSpell)))
                {
                    if (m_NewBody != 0)
                    {
                        if (this.Scroll != null)
                            Scroll.Consume();

                        Caster.PlaySound(0x379);
                        Caster.BodyValue = m_NewBody;
                        Caster.Hue = m_NewHue;
                        Caster.SendMessage("You enter the realm of the dead.");
                        Caster.Blessed = true;

                        StopTimer(Caster);

                        Timer t = new InternalTimer(Caster, m_OldBody, m_OldHue);

                        m_Timers[Caster] = t;

                        t.Start();
                    }
                }
                else
                {
                    Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
                }
            }

            FinishSequence();
        }

        private static Hashtable m_Timers = new Hashtable();

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

        private class InternalTimer : Timer
        {
            private Mobile m_Owner;
            private int m_OldBody;
            private int m_OldHue;

            public InternalTimer(Mobile owner, int body, int hue) : base(TimeSpan.FromSeconds(0))
            {
                m_Owner = owner;
                m_OldBody = body;
                m_OldHue = hue;

                int val = (int)( owner.Skills[SkillName.Healing].Value + ( owner.Skills[SkillName.SpiritSpeak].Value / 2 ) );

                if (val > 100)
                    val = 100;

                Delay = TimeSpan.FromSeconds(val);
                Priority = TimerPriority.TwoFiftyMS;
            }

            protected override void OnTick()
            {
                if (!m_Owner.CanBeginAction(typeof(SeanceSpell)))
                {
                    m_Owner.BodyValue = m_OldBody;
                    m_Owner.Hue = m_OldHue;
					m_Owner.Blessed = false;
                    m_Owner.EndAction(typeof(SeanceSpell));
                }
            }
        }
    }
}
