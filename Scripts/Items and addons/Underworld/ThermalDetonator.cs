using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Spells;

namespace Server.Items
{
	public class ThermalDetonator : Item
	{
		[Constructable]
		public ThermalDetonator() : base( 0x4C14 )
		{
			Name = "thermal detonator";
			Weight = 0.1;
			Stackable = true;
		}

		public ThermalDetonator( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		private static bool LeveledExplosion = false;
		private static bool InstantExplosion = true;
		private static bool RelativeLocation = false;
		private const int ExplosionRange = 3;

		public virtual object FindParent( Mobile from )
		{
			Mobile m = this.HeldBy;

			if ( m != null && m.Holding == this )
				return m;

			object obj = this.RootParent;

			if ( obj != null )
				return obj;

			if ( Map == Map.Internal )
				return from;

			return this;
		}

		private Timer m_Timer;

		private ArrayList m_Users;

		public override void OnDoubleClick( Mobile from )
		{
			if ( Core.AOS && (from.Paralyzed || from.Blessed || from.Frozen || (from.Spell != null && from.Spell.IsCasting)) )
			{
				from.SendMessage( "You cannot do that yet." );
				return;
			}
			else if ( !from.Region.AllowHarmful( from, from ) )
			{
				from.SendMessage( "That doesn't feel like a good idea." ); 
				return;
			}

			ThrowTarget targ = from.Target as ThrowTarget;
			this.Stackable = false; // Scavenged explosion grenades won't stack with those ones in backpack, and still will explode.

			if ( targ != null && targ.Grenade == this )
				return;

			from.RevealingAction();

			if ( m_Users == null )
				m_Users = new ArrayList();

			if ( !m_Users.Contains( from ) )
				m_Users.Add( from );

			from.Target = new ThrowTarget( this );

			if ( m_Timer == null )
			{
				from.SendLocalizedMessage( 500236 ); // You should throw it now!

				if( Core.ML )
					m_Timer = Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.25 ), 5, new TimerStateCallback( Detonate_OnTick ), new object[]{ from, 3 } ); // 3.6 seconds explosion delay
				else
					m_Timer = Timer.DelayCall( TimeSpan.FromSeconds( 0.75 ), TimeSpan.FromSeconds( 1.0 ), 4, new TimerStateCallback( Detonate_OnTick ), new object[]{ from, 3 } ); // 2.6 seconds explosion delay
			}
		}

		private void Detonate_OnTick( object state )
		{
			if ( Deleted )
				return;

			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];
			int timer = (int)states[1];

			object parent = FindParent( from );

			if ( timer == 0 )
			{
				Point3D loc;
				Map map;

				if ( parent is Item )
				{
					Item item = (Item)parent;

					loc = item.GetWorldLocation();
					map = item.Map;
				}
				else if ( parent is Mobile )
				{
					Mobile m = (Mobile)parent;

					loc = m.Location;
					map = m.Map;
				}
				else
				{
					return;
				}

				Explode( from, true, loc, map );
				m_Timer = null;
			}
			else
			{
				if ( parent is Item )
					((Item)parent).PublicOverheadMessage( MessageType.Regular, 0x22, false, timer.ToString() );
				else if ( parent is Mobile )
					((Mobile)parent).PublicOverheadMessage( MessageType.Regular, 0x22, false, timer.ToString() );

				states[1] = timer - 1;
			}
		}

		private void Reposition_OnTick( object state )
		{
			if ( Deleted )
				return;

			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];
			IPoint3D p = (IPoint3D)states[1];
			Map map = (Map)states[2];

			Point3D loc = new Point3D( p );

			if ( InstantExplosion )
				Explode( from, true, loc, map );
			else
				MoveToWorld( loc, map );
		}

		private class ThrowTarget : Target
		{
			private ThermalDetonator m_Grenade;

			public ThermalDetonator Grenade
			{
				get{ return m_Grenade; }
			}

			public ThrowTarget( ThermalDetonator grenade ) : base( 12, true, TargetFlags.None )
			{
				m_Grenade = grenade;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Grenade.Deleted || m_Grenade.Map == Map.Internal )
					return;

				IPoint3D p = targeted as IPoint3D;

				if ( p == null )
					return;

				Map map = from.Map;

				if ( map == null )
					return;

				SpellHelper.GetSurfaceTop( ref p );

				from.RevealingAction();

				IEntity to;

				to = new Entity( Serial.Zero, new Point3D( p ), map );

				if( p is Mobile )
				{
					if( !RelativeLocation ) // explosion location = current mob location. 
						p = ((Mobile)p).Location;
					else
						to = (Mobile)p;
				}

				Effects.SendMovingEffect( from, to, 0x23B, 7, 0, false, false, 0, 0 );

				if( m_Grenade.Amount > 1 )
				{
					Mobile.LiftItemDupe( m_Grenade, 1 );
				}

				m_Grenade.Internalize();
				Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( m_Grenade.Reposition_OnTick ), new object[]{ from, p, map } );
			}
		}

		public void Explode( Mobile from, bool direct, Point3D loc, Map map )
		{
			if ( Deleted )
				return;

			Consume();

			for ( int i = 0; m_Users != null && i < m_Users.Count; ++i )
			{
				Mobile m = (Mobile)m_Users[i];
				ThrowTarget targ = m.Target as ThrowTarget;

				if ( targ != null && targ.Grenade == this )
					Target.Cancel( m );
			}

			if ( map == null )
				return;

			Effects.PlaySound(loc, map, 0x307);

			Effects.SendLocationEffect(loc, map, 0x36B0, 9, 10, 0, 0);

			IPooledEnumerable eable;// = LeveledExplosion ? map.GetObjectsInRange(loc, ExplosionRange) : map.GetMobilesInRange(loc, ExplosionRange);

			if (LeveledExplosion)
			{
				eable = map.GetObjectsInRange(loc, ExplosionRange);
			}
			else
			{
				eable = map.GetMobilesInRange(loc, ExplosionRange);
			}
			
			ArrayList toExplode = new ArrayList();

			int toDamage = 0;

			foreach ( object o in eable )
			{
				if ( o is Mobile ) // WIZARD FIX FOR HURTING ALL
				{
					toExplode.Add( o );
					++toDamage;
				}
			}

			eable.Free();

			int min = 60;
			int max = 90;

			for ( int i = 0; i < toExplode.Count; ++i )
			{
				object o = toExplode[i];

				if ( o is Mobile )
				{
					Mobile m = (Mobile)o;

					if ( from != null )
						from.DoHarmful( m );

					int damage = Utility.RandomMinMax( min, max );
	
					if ( !Core.AOS && damage > 40 )
						damage = 40;
					else if ( Core.AOS && toDamage > 2 )
						damage /= toDamage - 1;

					AOS.Damage( m, from, damage, 0, 100, 0, 0, 0 );
				}
				else if ( o is ThermalDetonator )
				{
					ThermalDetonator pot = (ThermalDetonator)o;

					pot.Explode( from, false, pot.GetWorldLocation(), pot.Map );
				}
			}
		}
	}
}