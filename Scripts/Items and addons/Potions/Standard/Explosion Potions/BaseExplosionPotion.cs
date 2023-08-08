using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Spells;
using Server.Mobiles;

namespace Server.Items
{
	public abstract class BaseExplosionPotion : BasePotion
	{
		public abstract int MinDamage { get; }
		public abstract int MaxDamage { get; }

		//public override int Hue{ get { return 0; } }

		public override bool RequireFreeHand{ get{ return false; } }

		private static bool LeveledExplosion = false; // Should explosion potions explode other nearby potions?
		private static bool InstantExplosion = true; // Should explosion potions explode on impact?
		private static bool RelativeLocation = false; // Is the explosion target location relative for mobiles?
		private const int ExplosionRange = 2; // How long is the blast radius?





        public BaseExplosionPotion( PotionEffect effect ) : base( 0xF0D, effect )
		{
		}

		public BaseExplosionPotion( Serial serial ) : base( serial )
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

		public override void Drink( Mobile from )
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
			this.Stackable = false; // Scavenged explosion potions won't stack with those ones in backpack, and still will explode.

			if ( targ != null && targ.Potion == this )
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
			private BaseExplosionPotion m_Potion;

			public BaseExplosionPotion Potion
			{
				get{ return m_Potion; }
			}

			public ThrowTarget( BaseExplosionPotion potion ) : base( 12, true, TargetFlags.None )
			{
				m_Potion = potion;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Potion.Deleted || m_Potion.Map == Map.Internal )
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

				Effects.SendMovingEffect( from, to, m_Potion.ItemID, 7, 0, false, false, m_Potion.Hue, 0 );

				if( m_Potion.Amount > 1 )
				{
					Mobile.LiftItemDupe( m_Potion, 1 );
				}

				m_Potion.Internalize();
				Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( m_Potion.Reposition_OnTick ), new object[]{ from, p, map } );
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

				if ( targ != null && targ.Potion == this )
					Target.Cancel( m );
			}

			if ( map == null )
				return;

			Effects.PlaySound(loc, map, 0x307);

			Effects.SendLocationEffect(loc, map, 0x3822, 20, 10, 0, 0);
            int alchemyBonus = 0;

			if ( direct )
				alchemyBonus = (int)(from.Skills.Alchemy.Value / (Core.AOS ? 5 : 10));

			IPooledEnumerable eable;

            //Explosion Potions have a wider range based on EnhancePotions for Chemists

            int scalar = 0;
            double scalarx = 0;
                if (from is PlayerMobile && ((PlayerMobile)from).Alchemist())
                    scalarx = 1.0 + (0.02 * EnhancePotions(from));
                    scalar = Convert.ToInt32(scalarx);


            if (LeveledExplosion)
			{
				eable = map.GetObjectsInRange(loc, ExplosionRange + scalar);
			}
			else
			{
				eable = map.GetMobilesInRange(loc, ExplosionRange + scalar);
			}			ArrayList toExplode = new ArrayList();

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

			int min = Scale( from, MinDamage );
			int max = Scale( from, MaxDamage );

			for ( int i = 0; i < toExplode.Count; ++i )
			{
				object o = toExplode[i];

				if ( o is Mobile )
				{
					Mobile m = (Mobile)o;

					if ( from != null )
						from.DoHarmful( m );

					int damage = Utility.RandomMinMax( min, max );
					
					damage += alchemyBonus;

					if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() && SkillHandlers.Discordance.IsDiscorded(m))
						damage += (int)((double)damage * 0.5);

					if ( Core.AOS && toDamage > 2 )
						damage /= toDamage - 1;

					AOS.Damage( true, m, from, damage, 0, 100, 0, 0, 0 );
				}
				else if ( o is BaseExplosionPotion )
				{
					BaseExplosionPotion pot = (BaseExplosionPotion)o;

					pot.Explode( from, false, pot.GetWorldLocation(), pot.Map );
				}
			}
		}
	}
}