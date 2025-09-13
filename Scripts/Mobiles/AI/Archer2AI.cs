using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
    public class Archer2AI : ArcherAI
    {
	public Archer2AI(BaseCreature m) : base (m)
	{
	}

	public override bool DoActionCombat()
	{
	    m_Mobile.Warmode = true;

	    if ( m_Mobile.Combatant == null || m_Mobile.Combatant.Deleted || !m_Mobile.Combatant.Alive || m_Mobile.Combatant.IsDeadBondedPet )
	    {
		m_Mobile.DebugSay("My combatant is deleted");
		Action = ActionType.Guard;
		return true;
	    }

	    if ((Core.TickCount - m_Mobile.LastMoveTime) > 1000 || m_Mobile.AIFullSpeedActive )
	    {
		if (WalkMobileRange(m_Mobile.Combatant, 4, true, m_Mobile.Weapon.MaxRange - 1, m_Mobile.Weapon.MaxRange))
		{
		    // Be sure to face the combatant
		    m_Mobile.Direction = m_Mobile.GetDirectionTo(m_Mobile.Combatant.Location);
		}
		else
		{
		    if ( m_Mobile.Combatant != null )
		    {
			if ( m_Mobile.Debug )
			    m_Mobile.DebugSay( "I am still not in range of {0}", m_Mobile.Combatant.Name);

			if ( (int) m_Mobile.GetDistanceToSqrt( m_Mobile.Combatant ) > m_Mobile.RangePerception + 1 )
			{
			    if ( m_Mobile.Debug )
				m_Mobile.DebugSay( "I have lost {0}", m_Mobile.Combatant.Name);

			    m_Mobile.Combatant = null;
			    Action = ActionType.Guard;
			    return true;
			}
		    }
		}
	    }

	    // When we have no ammo, we flee
	    Container pack = m_Mobile.Backpack;

	    if ( pack == null || pack.FindItemByType( typeof( Arrow ) ) == null )
	    {
		Action = ActionType.Flee;
		return true;
	    }


	    // At 20% we should check if we must leave
	    if ( m_Mobile.Hits < m_Mobile.HitsMax*20/100 )
	    {
		bool bFlee = false;
		// if my current hits are more than my opponent, i don't care
		if ( m_Mobile.Combatant != null && m_Mobile.Hits < m_Mobile.Combatant.Hits)
		{
		    int iDiff = m_Mobile.Combatant.Hits - m_Mobile.Hits;

		    if ( Utility.Random(0, 100) > 10 + iDiff) // 10% to flee + the diff of hits
		    {
			bFlee = true;
		    }
		}
		else if ( m_Mobile.Combatant != null && m_Mobile.Hits >= m_Mobile.Combatant.Hits)
		{
		    if ( Utility.Random(0, 100) > 10 ) // 10% to flee
		    {
			bFlee = true;
		    }
		}

		if (bFlee)
		{
		    Action = ActionType.Flee; 
		}
	    }

	    return true;
	}

	private void WalkBack(int iChanceToNotMove, int iChanceToDir, int iSteps)
	{
	    if (m_Mobile.Deleted || m_Mobile.DisallowAllMoves)
		return;

	    // If I'm not standing by my spawn point, take "iSteps" steps towards it...
	    if (m_Mobile.Home != Point3D.Zero)
	    {
		// m_Mobile.GetDistanceToSqrt(m_Mobile.Home): Distance to Spawner
		// m_Mobile.RangeHome: Defined home range of the spawner, i.e. circle around spawner that is considered "home".
		// If more than 10 steps away from home range:
		if ( (int)m_Mobile.GetDistanceToSqrt(m_Mobile.Home) > (m_Mobile.RangeHome + 15) )
		{
		    int i = 0;
		    while (i < iSteps)
		    {
			if (Utility.Random(10) > 3)
			{
			    DoMove(m_Mobile.GetDirectionTo(m_Mobile.Home));
			}
			else
			{
			    WalkRandom(iChanceToNotMove, iChanceToDir, 1);
			}

			i = i + 1;
		    }
		}
	    }
	}
    }
}
