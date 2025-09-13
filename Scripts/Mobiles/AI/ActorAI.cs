using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	public class PlayActorAI : BaseAI
	{
		public PlayActorAI(BaseCreature m) : base (m)
		{
		}

		public override bool DoActionWander()
		{

			if( m_Mobile.CurrentWayPoint != null )
			{
				WayPoint point = m_Mobile.CurrentWayPoint;
				if( (point.X != m_Mobile.Location.X || point.Y != m_Mobile.Location.Y) && point.Map == m_Mobile.Map && point.Parent == null && !point.Deleted )
				{
					m_Mobile.DebugSay( "I will move towards my waypoint." );
					DoMove( m_Mobile.GetDirectionTo( m_Mobile.CurrentWayPoint ) );
				}
				else if( OnAtWayPoint() )
				{
					m_Mobile.DebugSay( "I will go to the next waypoint" );
					m_Mobile.CurrentWayPoint = point.NextPoint;
					if( point.NextPoint != null && point.NextPoint.Deleted )
						m_Mobile.CurrentWayPoint = point.NextPoint = point.NextPoint.NextPoint;
				}
			}

			return true;
		} 

		public override bool DoActionCombat()
		{

			return false;
		}

		public override bool DoActionGuard()
		{

			return false;
		}
	}
}