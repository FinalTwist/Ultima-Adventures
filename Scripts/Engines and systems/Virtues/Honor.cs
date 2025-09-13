using System;
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Targeting;
using Server.Regions;

namespace Server
{
	public class HonorVirtue
	{
		
		private static readonly TimeSpan UseDelay = TimeSpan.FromMinutes( 5.0 ); 

		public static void Initialize()
		{
			VirtueGump.Register( 107, new OnVirtueUsed( OnVirtueUsed ) );
		}

		private static void OnVirtueUsed( Mobile from )
		{
			if ( from.Alive )
			{
				from.SendLocalizedMessage( 1063160 ); // Target what you wish to honor.
				from.Target = new InternalTarget();
			}
		}

		private class InternalTarget : Target
		{
			public InternalTarget() : base( 12, false, TargetFlags.None )
			{
				CheckLOS = true;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				PlayerMobile pm = from as PlayerMobile;
				if ( pm == null )
					return;

				if ( targeted == pm )
				{
					EmbraceHonor( pm );
				}
				else if ( targeted is Mobile )
					Honor( pm, (Mobile) targeted );
			}

			protected override void OnTargetOutOfRange( Mobile from, object targeted )
			{
				from.SendLocalizedMessage( 1063232 ); // You are too far away to honor your opponent
			}
		}

		private static int GetHonorDuration( PlayerMobile from )
		{
			switch ( VirtueHelper.GetLevel( from, VirtueName.Honor ) )
			{
				case VirtueLevel.Seeker: return 30;
				case VirtueLevel.Follower: return 90;
				case VirtueLevel.Knight: return 300;

				default: return 0 ;
			}
		}

		private static void EmbraceHonor( PlayerMobile pm )
		{
			if ( pm.HonorActive )
			{
				pm.SendLocalizedMessage( 1063230 ); // You must wait awhile before you can embrace honor again.
				return;
			}

			if ( GetHonorDuration( pm ) == 0 )
			{
				pm.SendLocalizedMessage( 1063234 ); // You do not have enough honor to do that
				return;
			}

			TimeSpan waitTime = DateTime.UtcNow - pm.LastHonorUse;
			if ( waitTime < UseDelay )
			{
				TimeSpan remainingTime = UseDelay - waitTime;
				int remainingMinutes = (int) Math.Ceiling( remainingTime.TotalMinutes );

				pm.SendLocalizedMessage( 1063240, remainingMinutes.ToString() ); // You must wait ~1_HONOR_WAIT~ minutes before embracing honor again
				return;
			}
			
			pm.SendGump( new HonorSelf( pm ) );
			
		}

		public static void ActivateEmbrace( PlayerMobile pm )
		{
			int duration = GetHonorDuration( pm );
			int usedPoints;

			if ( pm.Virtues.Honor < 4399)
               			 usedPoints = 400;
			else if ( pm.Virtues.Honor < 10599 )
                		usedPoints = 600;
			else
                		usedPoints = 1000;

			VirtueHelper.Atrophy( pm, VirtueName.Honor, usedPoints );

			pm.HonorActive = true;
			pm.SendLocalizedMessage( 1063235 ); // You embrace your honor

			Timer.DelayCall( TimeSpan.FromSeconds( duration ),
				delegate() {
					pm.HonorActive = false;
					pm.LastHonorUse = DateTime.UtcNow;
					pm.SendLocalizedMessage( 1063236 ); // You no longer embrace your honor
				} );
		}

		private static void Honor( PlayerMobile source, Mobile target )
		{
			IHonorTarget honorTarget = target as IHonorTarget;
			GuardedRegion reg = (GuardedRegion) source.Region.GetRegion( typeof( GuardedRegion ) );
			Map map = source.Map;

			if ( honorTarget == null )
				return;

			if ( honorTarget.ReceivedHonorContext != null )
			{
				if ( honorTarget.ReceivedHonorContext.Source == source )
					return;

				if ( honorTarget.ReceivedHonorContext.CheckDistance() )
				{
					source.SendLocalizedMessage( 1063233 ); // Somebody else is honoring this opponent
					return;
				}
			}

			if ( target.Hits < target.HitsMax )
			{
				source.SendLocalizedMessage( 1063166 ); // You cannot honor this monster because it is too damaged.
				return;
			}

			BaseCreature cret = target as BaseCreature;
			if ( target.Body.IsHuman && (cret == null || (!cret.AlwaysAttackable && !cret.AlwaysMurderer)) )
			{
				if ( map != null && (map.Rules & MapRules.HarmfulRestrictions) == 0 )
				{
					//Allow honor on blue if in Fel
				}
				else
				{
					source.SendLocalizedMessage( 1001018 ); // You cannot perform negative acts
					return;					//cannot honor in trammel town on blue
				}
			}

			if( Core.ML && target is PlayerMobile )
			{
				source.SendLocalizedMessage( 1075614 ); // You cannot honor other players.
				return;
			}

			if ( source.SentHonorContext != null )
				source.SentHonorContext.Cancel();

			new HonorContext( source, target );

			source.Direction = source.GetDirectionTo( target );

			if ( !source.Mounted )
				source.Animate( 32, 5, 1, true, true, 0 );

		}
	}

	public interface IHonorTarget
	{
		HonorContext ReceivedHonorContext{ get; set; }
	}

	public class HonorContext
	{
		private PlayerMobile m_Source;
		private Mobile m_Target;

		private double m_HonorDamage;
		private int m_TotalDamage;

		private int m_Perfection;

		private enum FirstHit
		{
			NotDelivered,
			Delivered,
			Granted
		}

		private FirstHit m_FirstHit;
		private bool m_Poisoned;
		private Point3D m_InitialLocation;
		private Map m_InitialMap;

		private InternalTimer m_Timer;

		public PlayerMobile Source{ get{ return m_Source; } }
		public Mobile Target{ get{ return m_Target; } }

		public HonorContext( PlayerMobile source, Mobile target )
		{
			m_Source = source;
			m_Target = target;

			m_FirstHit = FirstHit.NotDelivered;
			m_Poisoned = false;

			m_InitialLocation = source.Location;
			m_InitialMap = source.Map;

			source.SentHonorContext = this;
			((IHonorTarget)target).ReceivedHonorContext = this;

			m_Timer = new InternalTimer( this );
			m_Timer.Start();
			source.m_hontime = (DateTime.UtcNow + TimeSpan.FromMinutes( 40 ));

			Timer.DelayCall( TimeSpan.FromMinutes( 40 ),
				delegate() {
					if (source.m_hontime < DateTime.UtcNow && source.SentHonorContext != null)
					{
						Cancel();
					}
				} );
		}

		public void OnSourceDamaged( Mobile from, int amount )
		{
			if ( from != m_Target )
				return;

			if ( m_FirstHit == FirstHit.NotDelivered )
				m_FirstHit = FirstHit.Granted;
		}

		public void OnTargetPoisoned()
		{
			m_Poisoned = true; // Set this flag for OnTargetDamaged which will be called next
		}

		public void OnTargetDamaged( Mobile from, int amount )
		{
			if ( m_FirstHit == FirstHit.NotDelivered )
				m_FirstHit = FirstHit.Delivered;

			if ( m_Poisoned )
			{
				m_HonorDamage += amount * 0.8;
				m_Poisoned = false; // Reset the flag

				return;
			}

			m_TotalDamage += amount;

			if ( from == m_Source )
			{
				if ( m_Target.CanSee( m_Source ) && m_Target.InLOS( m_Source ) && ( m_Source.InRange( m_Target, 1 )
					|| ( m_Source.Location == m_InitialLocation && m_Source.Map == m_InitialMap ) ) )
				{
					m_HonorDamage += amount;
				}
				else
				{
					m_HonorDamage += amount * 0.8;
				}
			}
			else if ( from is BaseCreature && ((BaseCreature)from).GetMaster() == m_Source )
			{
				m_HonorDamage += amount * 0.8;
			}
		}

		public void OnTargetHit( Mobile from )
		{
			if ( from != m_Source || m_Perfection == 100 )
				return;

			int bushido = (int) from.Skills.Bushido.Value;
			if ( bushido < 50 )
				return;

			m_Perfection += bushido / 10;

			if ( m_Perfection >= 100 )
			{
				m_Perfection = 100;
				m_Source.SendLocalizedMessage( 1063254 ); // You have Achieved Perfection in inflicting damage to this opponent!
			}
			else
			{
				m_Source.SendLocalizedMessage( 1063255 ); // You gain in Perfection as you precisely strike your opponent.
			}
		}

		public void OnTargetMissed( Mobile from )
		{
			if ( from != m_Source || m_Perfection == 0 )
				return;

			m_Perfection -= 25;

			if ( m_Perfection <= 0 )
			{
				m_Perfection = 0;
				m_Source.SendLocalizedMessage( 1063256 ); // You have lost all Perfection in fighting this opponent.
			}
			else
			{
				m_Source.SendLocalizedMessage( 1063257 ); // You have lost some Perfection in fighting this opponent.
			}
		}

		public void OnSourceBeneficialAction( Mobile to )
		{
			if ( to != m_Target )
				return;

			if ( m_Perfection >= 0 )
			{
				m_Perfection = 0;
				m_Source.SendLocalizedMessage( 1063256 ); // You have lost all Perfection in fighting this opponent.
			}
		}

		public void OnSourceKilled()
		{
			return;
		}

		public void OnTargetKilled()
		{
			Cancel();

			int targetFame = m_Target.Fame;

			if ( m_Perfection > 0 )
			{
				int restore = Math.Min( m_Perfection * ( targetFame + 5000 ) / 25000, 10 );

				m_Source.Hits += restore;
				m_Source.Stam += restore;
				m_Source.Mana += restore;
			}

			if ( m_Source.Virtues.Honor > targetFame )
				return;

			double dGain = ( targetFame / 100 ) * (m_HonorDamage / m_TotalDamage );	//Initial honor gain is 100th of the monsters honor

			if ( m_HonorDamage == m_TotalDamage && m_FirstHit == FirstHit.Granted)
				dGain = dGain * 1.5;							//honor gain is increased alot more if the combat was fully honorable
			else
				dGain = dGain * 0.9;

			int gain = Math.Min( (int)dGain, 200 );

			if ( gain < 1 )
				gain=1;		//Minimum gain of 1 honor when the honor is under the monsters fame

			if ( VirtueHelper.IsHighestPath( m_Source, VirtueName.Honor ) )
			{
				m_Source.SendLocalizedMessage( 1063228 ); // You cannot gain more Honor.
				return;
			}

			bool gainedPath = false;
			if ( VirtueHelper.Award( m_Source, VirtueName.Honor, (int) gain, ref gainedPath ) )
			{
				if ( gainedPath )
					m_Source.SendLocalizedMessage( 1063226 ); // You have gained a path in Honor!
				else
					m_Source.SendLocalizedMessage( 1063225 ); // You have gained in Honor.
			}
		}

		public int PerfectionDamageBonus
		{
			get { return m_Perfection; }
		}

		public int PerfectionLuckBonus
		{
			get{ return (m_Perfection * m_Perfection) / 10; }
		}

		public bool CheckDistance()
		{
			return true;
		}

		public void Cancel()
		{
			m_Source.SentHonorContext = null;
			((IHonorTarget)m_Target).ReceivedHonorContext = null;

			m_Timer.Stop();
		}

		private class InternalTimer : Timer
		{
			private HonorContext m_Context;

			public InternalTimer( HonorContext context ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Context = context;
			}

			protected override void OnTick()
			{
				m_Context.CheckDistance();
			}
		}
	}
}