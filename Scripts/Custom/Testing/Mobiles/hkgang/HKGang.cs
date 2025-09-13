//
// Hunter Killer Gang v1.0b
// jm (aka x-ray aka �������) 
// jm99[at]mail333.com
//
// Some code fixes from MarkC777 and KillerBeeZ,  
// thanks guys !
//

using System;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using Server.Regions;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Engines.HunterKiller
{
	public enum HKState
	{
		Waiting,
		Pursuit,
		Ambush,
		Returning
	}

	public class HKGangSpawn : Item
	{
		private Mobile Leader;
		private ArrayList Killers;
		private int maxRange		= 200;
		private HKState state		= HKState.Waiting;
		private Mobile target		= null;
		private WayPoint waypoint	= null;
		private Timer timer;
		private DateTime nextActionTime;
		private DateTime nextRefreshTime;

		[Constructable]
		public HKGangSpawn() : base( 0x1f13 )
		{
			Visible = false;
			Movable = false;

			Name = "HunterKillerSpawn";

			Timer.DelayCall( TimeSpan.Zero, new TimerCallback( AddComponents ) );
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxRange
		{
			get
			{
				return maxRange;
			}
			set
			{
				maxRange = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public HKState State
		{
			get
			{
				return state;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime NextAction
		{
			get
			{
				return nextActionTime;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Target
		{
			get
			{
				return target;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public WayPoint Waypoint
		{
			get
			{
				return waypoint;
			}
		}

		public HKGangSpawn( Serial serial ) : base( serial )
		{
		}

		public void AddRandomMobile()
		{
			switch ( Utility.Random( 3 )) 
			{
			case 0:	AddMobile( false, new HKArcher() ); break; 
			case 1:	AddMobile( false, new HKMage() ); break; 
			case 2:	AddMobile( false, new HKWarrior() ); break; 
			}
		}

		public void AddComponents()
		{
			if (Deleted) return;

			Killers = new ArrayList();

			AddMobile( true,  new HKLeader()  );
			AddMobile( false, new HKWarrior() );

			AddRandomMobile();
			AddRandomMobile();

			timer = new SliceTimer( this );

			timer.Start();
		}

		public void AddMobile( bool isLeader, Mobile m )
		{
			if (isLeader)
				Leader = m;
			else
				Killers.Add( m );			

			Point3D loc = new Point3D( X , Y , Z );

			BaseCreature bc = m as BaseCreature;

			if ( bc != null )
			{
				bc.RangeHome = 4; 

				bc.Home = loc; 
			}

			m.Location = loc;
			m.Map = Map;
		}

		public void ClearWaypoint()
		{
			if (waypoint != null)
			{
				if (Leader != null && !Leader.Deleted && Leader.Alive) ((HKLeader)Leader).CurrentWayPoint = null;

				foreach( Mobile m in Killers )
				{	
					if(!m.Deleted && m.Alive)
					{
						((HKMobile)m).CurrentWayPoint = null;
					}
				}			

				waypoint.Delete();

				waypoint = null;
			}
		}

		public void AssignWaypoint()
		{
			((HKLeader)Leader).CurrentWayPoint = waypoint;

			foreach( Mobile m in Killers )
			{	
				if(!m.Deleted && m.Alive)
				{
					((HKMobile)m).CurrentWayPoint = waypoint;
				}
			}				
		}

		public void RefreshWaypoint(bool speak)
		{
			if (Leader != null && !Leader.Deleted && Leader.Alive) 
			{
				ClearWaypoint();

				if (speak) ((HKLeader)Leader).Speak(0);

				if (Leader.GetDistanceToSqrt( target ) > maxRange || Map != target.Map)
				{
					StateReturning();

					return;
				}

				waypoint = new WayPoint();

				waypoint.Location = target.Location;
				waypoint.Map = Map;

				AssignWaypoint();
			}
			else
			{
				StateWaiting();

//				System.Console.WriteLine("DEBUG: Leader dead");
			}
		}

		public void FindTarget()
		{
			if (Leader == null || Leader.Deleted || !Leader.Alive) return;

			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( maxRange ) )
			{
				if( m != null && m.Player && !m.Deleted && m.Alive && !m.Hidden && m.AccessLevel == AccessLevel.Player )
				{
					list.Add( m );
				}
			}

			if (list.Count == 0) return;

			target = (Mobile)list[ Utility.Random( 0, list.Count ) ];

			state = HKState.Pursuit;

			nextActionTime = DateTime.UtcNow + TimeSpan.FromMinutes( 15 );

			RefreshWaypoint(true);

//			System.Console.WriteLine("DEBUG: Pursuit, target: {0}", target.Name);
		}

		public void SetHome(Point3D loc)
		{
			((HKLeader)Leader).Home = loc;

			foreach( Mobile m in Killers )
			{	
				if(!m.Deleted && m.Alive)
				{
					((HKMobile)m).Home = loc;
				}
			}				
		}

		public void SetHome2This()
		{
			foreach( Mobile m in Killers )
			{	
				if(!m.Deleted && m.Alive)
				{
					((HKMobile)m).Home = m.Location;
				}
			}				
		}

		public bool CheckTarget()
		{
			if (target != null && !target.Deleted)
			{
				if (!target.Alive)
				{
					if (Leader != null && !Leader.Deleted && Leader.Alive) 
					{
						ClearWaypoint();

						SetHome(Leader.Location);

						((HKLeader)Leader).Speak(1);

						state = HKState.Ambush;

						nextActionTime = DateTime.UtcNow + TimeSpan.FromMinutes( 8 );

//						System.Console.WriteLine("DEBUG: Target dead, Ambush");
					}
					else
					{
//						System.Console.WriteLine("DEBUG: Leader dead");
						
						StateWaiting();
					}					

					return true;
				}

				return false;
			}

			return true;
		}

		public void StateReturning()
		{
			state = HKState.Returning;

			ClearWaypoint();

			SetHome( new Point3D(X, Y, Z) );

			nextActionTime = DateTime.UtcNow + TimeSpan.FromMinutes( 10 );			

//			System.Console.WriteLine("DEBUG: Returning");
		}

		public void StateWaiting()
		{
			state = HKState.Waiting;

			ClearWaypoint();

			SetHome2This();

//			System.Console.WriteLine("DEBUG: Waiting");
		}

		public int GetAlive()
		{
			int count = 0;

			if (Leader != null && !Leader.Deleted && Leader.Alive) count++;

			foreach( Mobile m in Killers )
			{	
				if(!m.Deleted && m.Alive)
				{
					count++;
				}
			}			

			return count;
		}

		public void OnSlice()
		{
//			System.Console.WriteLine("DEBUG: Tick !");

			if (GetAlive() == 0)
			{
				ClearWaypoint();

				Delete();

//				System.Console.WriteLine("DEBUG: Removing spawn");

				return;
			}

			switch(state)
			{
			case HKState.Waiting:

				if ( 0.05 < Utility.RandomDouble() ) return;

				FindTarget();

				break;

			case HKState.Pursuit:

				if ( DateTime.UtcNow >= nextActionTime)
				{
					if (Leader != null && !Leader.Deleted && Leader.Alive)
					{
						StateReturning();
					}
					else
					{
						StateWaiting();
					}

					return;
				}

				if ( CheckTarget() ) break;

				if ( DateTime.UtcNow >= nextRefreshTime )
				{
					RefreshWaypoint(false);

					nextRefreshTime = DateTime.UtcNow + TimeSpan.FromSeconds( 10 );
				}

				break;

			case HKState.Ambush:

				if ( DateTime.UtcNow >= nextActionTime )
				{
					if (Leader != null && !Leader.Deleted && Leader.Alive) 
					{
						((HKLeader)Leader).Speak(2);

						StateReturning();
					}
					else
					{
						StateWaiting();
					}
				}
			
				break;

			case HKState.Returning:

				if ( DateTime.UtcNow >= nextActionTime )
				{
					StateWaiting();
				}

				break;
			}
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			if (Leader != null && !Leader.Deleted) Leader.Delete();

			if (Killers != null)
			{
				foreach( Mobile m in Killers )
				{	
					if(!m.Deleted)
					{
						m.Delete();
					}
				}		
			}

			if (timer != null) timer.Stop();
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendGump( new PropertiesGump( from, this ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( Leader );
			writer.WriteMobileList( Killers , true);
			writer.Write( (int) state );
			writer.Write( target );
			writer.Write( waypoint );
			writer.WriteDeltaTime( nextActionTime );
			writer.WriteDeltaTime( nextRefreshTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			Leader = reader.ReadMobile();
			Killers = reader.ReadMobileList();
			state = (HKState)reader.ReadInt();
			target = reader.ReadMobile();
			waypoint = reader.ReadItem() as WayPoint;
			nextActionTime = reader.ReadDeltaTime();
			nextRefreshTime = reader.ReadDeltaTime();

			timer = new SliceTimer( this );

			timer.Start();
		}
	}
}
