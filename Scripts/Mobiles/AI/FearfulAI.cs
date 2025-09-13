using System;
using System.Collections;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
    public class FearfulAI : BaseAI
    {
        public FearfulAI(BaseCreature m) : base(m) { }

        private int RangeFocusMob { get { return m_Mobile.RangePerception * 4; } }
        public override bool DoActionWander()
        {
            Mobile controller = m_Mobile.ControlMaster;

            if (m_Mobile.Combatant != null)
                Action = ActionType.Flee;
            else if (controller != null && controller.Combatant != null && controller.GetDistanceToSqrt(m_Mobile) < 5)
                Action = ActionType.Flee;
            else return base.DoActionWander();

            return true;
        }

        public override bool DoActionCombat()
        {
                Action = ActionType.Wander;
                return true;
        }

        public override bool DoActionBackoff()
        {
            double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;

            if ( !m_Mobile.Summoned && !m_Mobile.Controlled && hitPercent < 0.1 )  Action = ActionType.Flee;
            else
            {
                if (AcquireFocusMob(RangeFocusMob, FightMode.Closest, true, false, true))
                {
                    if (WalkMobileRange(m_Mobile.FocusMob, 1, false, m_Mobile.RangePerception, RangeFocusMob)) 
                        Action = ActionType.Wander;               
                }
                else Action = ActionType.Wander;
            }

            return true;
        }

        public override bool DoActionFlee()
        {
            AcquireFocusMob(RangeFocusMob, m_Mobile.FightMode, true, false, true);

            if ( m_Mobile.FocusMob == null ) m_Mobile.FocusMob = m_Mobile.Combatant;
            return base.DoActionFlee();
        }
    }
}