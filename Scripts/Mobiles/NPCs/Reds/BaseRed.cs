using System; 
using System.Collections; 
using Server;
using Server.Misc; 
using Server.Items; 
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Targeting;

namespace Server.Mobiles 
{ 
	public class BaseRed : BaseCreature 
	{ 

		private bool m_Bandaging;

		private bool m_Threwpot;

		private bool m_attacking;

		private bool m_revealing;
		public BaseRed(AIType ai, FightMode fm, int PR, int FR, double AS, double PS) : base( ai, fm, PR, FR, AS, PS )
		{
			SpeechHue = Utility.RandomDyedHue(); 
			Hue = Utility.RandomSkinHue();
			RangePerception = BaseCreature.DefaultRangePerception;
			Criminal = true;
			AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
			m_revealing = false;
			m_teleporting = false;
			m_Threwpot = false;
			m_freezing = false;
			m_attacking = false;
		}

		public override bool ReacquireOnMovement{ get { return true; } }
		public override TimeSpan ReacquireDelay{ get { return TimeSpan.FromSeconds( 1 ); } }
		public override bool BardImmune{ get{ return false; } }
		public override bool AlwaysMurderer{ get{ return true; } }

		public override bool IsEnemy( Mobile m )
		{

			Region reg = Region.Find( this.Location, this.Map );

			if ( reg.IsPartOf( "the Basement" ))
				return false;

			if (m is PlayerMobile && (m.Str + m.Int + m.Dex) < 125 && this.Map == Map.Trammel)
				return false;

		    if ( m is HarvesterEntity || m is BaseRed || m is Citizens || m is PlayerVendor || m is TownHerald || m is BaseVendor )
			return false;

		    if ( m is BaseCreature && ((BaseCreature)m).ControlMaster != null && m.Combatant != this)
		    {
				Mobile owner = ((BaseCreature)m).ControlMaster;
				if (owner is PlayerMobile && owner.Kills >= 2)
					return false;
		    } 
			
			//if ( (Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "The Pit") && (m.Kills >= 5 && m.Criminal) )
			//	return true;
			
		    if ( m is PlayerMobile && m.Criminal || m.Kills >= 2 )
				return false;

		    return true;
		}

		public virtual bool HealsYoungPlayers{ get{ return false; } }

		public virtual bool CheckResurrect( Mobile m )
		{
			return true;
		}

		public DateTime m_NextResurrect;
		public static TimeSpan ResurrectDelay = TimeSpan.FromSeconds( 6.0 );

		public virtual void OfferResurrection( Mobile m )
		{
			
			if (m.Criminal || m.Kills >= 2)
			{
				Direction = GetDirectionTo( m );
				Say("An Corp");

				m.PlaySound(0x1F2);
				m.FixedEffect( 0x376A, 10, 16 );
				
				m.Resurrect();

			}
		}

		public virtual void OfferHeal( PlayerMobile m )
		{
			Direction = GetDirectionTo( m );

			//if ( m.CheckYoungHealTime() )
			if ( DateTime.UtcNow >= m_NextResurrect && m.Criminal && m.Kills >= 2 && !m.Hidden)
			{
				Say("Here's some help"); // You look like you need some healing my child.
				Say("In Vas Mani");

				m.PlaySound( 0x1F2 );
				m.FixedEffect( 0x376A, 9, 32 );

				m.Hits = (m.Hits + 50);
				m_NextResurrect = DateTime.UtcNow + ResurrectDelay;
			}
			else
			{
				Say("You should be good"); // I can do no more for you at this time.
			}

		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( !m.Frozen && DateTime.UtcNow >= m_NextResurrect && InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) )
			{
				if ( !m.Alive && (m is PlayerMobile && (m.Criminal || m.Kills >= 2)) )
				{
					m_NextResurrect = DateTime.UtcNow + ResurrectDelay;

					if ( m.Map == null || !m.Map.CanFit( m.Location, 16, false, false ) )
					{
						m.SendLocalizedMessage( 502391 ); // Thou can not be resurrected there!
					}
					else if ( CheckResurrect( m ) )
					{
						OfferResurrection( m );
					}
				}
				else if ( m.Hits < m.HitsMax && (m is PlayerMobile && m.Criminal || m.Kills >= 5) )
				{
					OfferHeal( (PlayerMobile) m );
				}
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			if ( Utility.RandomDouble() < (this.Fame / 250000) && (this.AI == AIType.AI_Melee) ) //odds of doing a special
			{
				BleedThem(defender);
			}

			base.OnGaveMeleeAttack( defender );
		}

        private void BleedThem(Mobile m)
        {
            if (m == null) return;
            m.SendLocalizedMessage(1060160); // You are bleeding!
            m.PlaySound(0x133);
            m.FixedParticles(0x377A, 244, 25, 9950, 31, 0, EffectLayer.Waist);
            BleedAttack.BeginBleed(m, m);
        }

		public override void OnThink()
		{
			base.OnThink();

			//speed change
			if (!Mounted)
			{
				AIFullSpeedActive = AIFullSpeedPassive = false;
			}
			else 
				AIFullSpeedActive = AIFullSpeedPassive = true;


			// Chug pots
			if ( this.Poisoned )
			{
				GreaterCurePotion m_CPot = (GreaterCurePotion)this.Backpack.FindItemByType( typeof ( GreaterCurePotion ) );
				if ( m_CPot != null )
					m_CPot.Drink( this );
			}

			if ( this.Hits <= (this.HitsMax * .7) ) // Will try to use heal pots if he's at or below 70% health
			{
				GreaterHealPotion m_HPot = (GreaterHealPotion)this.Backpack.FindItemByType( typeof ( GreaterHealPotion ) );
				if ( m_HPot != null )
					m_HPot.Drink( this );
			}
			
			if ( this.Stam <= (this.StamMax * .25) ) // Will use a refresh pot if he's at or below 25% stam
			{
				TotalRefreshPotion m_RPot = (TotalRefreshPotion)this.Backpack.FindItemByType( typeof ( TotalRefreshPotion) );
				if ( m_RPot != null )
					m_RPot.Drink( this );
			}

				    // Use bandages
			if ( (this.IsHurt() || this.Poisoned) && m_Bandaging == false )
			{
				Bandage m_Band = (Bandage)this.Backpack.FindItemByType( typeof ( Bandage ) );

				if ( m_Band != null )
				{
					m_Bandaging = true;

					if ( BandageContext.BeginHeal( this, this ) != null )
					m_Band.Consume();
					BandageTimer bt = new BandageTimer( this );
					bt.Start();
				}
			}
			/*if (this.Combatant != null && this.Combatant is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)this.Combatant;

					if (!m_freezing && !bc.Paralyzed && !bc.Frozen && Utility.RandomDouble() > 0.75)
					{
						m_freezing = true;
						this.Frozen = true;

						Say("An Ex Por");
						
						Timer.DelayCall( TimeSpan.FromSeconds( 2 ), new TimerStateCallback ( FreezeIt ), new object[]{ this.Combatant }  );
					}
					

				if (bc.Controlled && bc.ControlMaster != null && InLOS( bc.ControlMaster ) &&  (int)GetDistanceToSqrt( bc.ControlMaster ) < 10 )
				{
					this.Combatant = bc.ControlMaster;
				}
			}*/
			//situational awareness
			if (m_attacking == false && this.Combatant != null) // new target found, just started attacking
			{
				m_attacking = true;
				CallOthers( this.Combatant );
			}
			if (m_attacking && this.Combatant == null)
				m_attacking = false;

			if (this.Combatant != null && this.Combatant is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)this.Combatant;
				if (pm.Hidden && InRange( pm, 10 ) && !m_revealing )
				{
					if (Utility.RandomBool())
						Say("Hah! Nice try you noob.");
					else 
						Say("Playing Hide and go seek?  What do you think I am!");

					Say("Wis Quas");
					m_revealing = true;
					Timer.DelayCall( TimeSpan.FromSeconds( 2 ), new TimerStateCallback ( RevealHim ), new object[]{ pm }  );
				}
				else if ( DateTime.UtcNow >= m_NextResurrect && (this is SpawnRed || this is DreadLord  ) && (int)GetDistanceToSqrt( this.Combatant ) > 2 && (int)GetDistanceToSqrt( this.Combatant ) < 12 && InLOS( this.Combatant ) && !m_teleporting )
				{
					m_NextResurrect = DateTime.UtcNow + ResurrectDelay;
					m_teleporting = true;
					this.Frozen = true;

					Say("Rel Por");
						
					Timer.DelayCall( TimeSpan.FromSeconds( 1 ), new TimerStateCallback ( TeleportTo ), new object[]{ this.Combatant }  );

				}
			}
			if (this.Combatant != null && this is RedPlayer && InLOS( this.Combatant ) && (int)GetDistanceToSqrt( this.Combatant ) > 2 && (int)GetDistanceToSqrt( this.Combatant ) < 15 && !m_Threwpot)
			{
					ExplosionPotion m_Expot = (ExplosionPotion)this.Backpack.FindItemByType( typeof ( ExplosionPotion ) );

					if ( m_Expot != null && m_potTimer == null )
					{
						this.Frozen = true;
						m_Threwpot = true;
						Timer.DelayCall( TimeSpan.FromSeconds( 1 ), new TimerStateCallback ( Throwpot ), new object[]{ this } );
					}
			}
			
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{

			if ( !(this is RedPlayer) && attacker is BaseCreature && !m_freezing && !attacker.Frozen && Utility.RandomDouble() > 0.75 && InLOS( attacker ) &&  (int)GetDistanceToSqrt( attacker ) < 10)
			{
				BaseCreature bc = (BaseCreature)attacker;

				m_freezing = true;
				this.Frozen = true;

				Say("An Ex Por");
						
				Timer.DelayCall( TimeSpan.FromSeconds( 2 ), new TimerStateCallback ( FreezeIt ), new object[]{ attacker }  );

				if (bc.Controlled && bc.ControlMaster != null && InLOS( bc.ControlMaster ) &&  (int)GetDistanceToSqrt( bc.ControlMaster ) < 10 )
				{
					this.Combatant = bc.ControlMaster;
				}
			}
			base.OnGotMeleeAttack( attacker );
		}

		public override void CallOthers( Mobile target )
		{
			bool callout = false;
			foreach ( Mobile mob in this.GetMobilesInRange( 13 ) )
			{
				if ( mob is BaseRed && InLOS( mob ) && mob.Combatant == null )
				{
					if (!callout)
						callout = true;

					mob.Combatant = target;
					mob.Warmode = true;
				}
			}	

			if (callout)
			{
				if (Utility.RandomBool() )
					this.Say("Found a Noob!");	
				else
					this.Say("This Way!");
			}	
		}

		public void RevealHim( object state )
		{
			m_revealing = false;
			if (this.Deleted || this == null)
				return;
				
			object[] states = (object[])state;

			PlayerMobile from = (PlayerMobile)states[0];
			Map map = from.Map;

			if ( map == null || map == Map.Internal || from == null )
				return;

			if ( InLOS( from ) )
			{
				from.RevealingAction();
			}
			
		}

		private bool m_freezing;

		public void FreezeIt( object state )
		{

			this.m_freezing = false;
			this.Frozen = false;

			if (this.Deleted || this == null)
				return;

			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];

			if (from == null )
				return;

			if ( InLOS( from ) )
			{
						from.Paralyze( TimeSpan.FromSeconds( Utility.RandomMinMax(3, 5) ) );

						from.PlaySound( 0x204 );
						from.FixedEffect( 0x376A, 10, 16, 0, 0 );
			}
		}

		private bool m_teleporting;

		public void TeleportTo( object state )
		{
			this.m_teleporting = false;
			this.Frozen = false;

			if (this.Deleted || this == null)
				return;

			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];

			if (from == null || from.Map == null )
				return;

 			if ( (int)GetDistanceToSqrt( from ) > 2 && (int)GetDistanceToSqrt( from ) < 12 && InLOS( from ))
			{
				from.PlaySound( 0x1FE );
				this.MoveToWorld( from.Location, from.Map );
			}
		}

		private Timer m_potTimer;		
		public void Throwpot( object state )
		{
			m_Threwpot = false;

			if (this.Deleted || this == null )
				return;

			this.Frozen = false;
			object[] states = (object[])state;

			BaseRed from = (BaseRed)states[0];

			if (!InLOS( from ))
				return;

			if (from == null || from.Map == null || from.Combatant == null || from.Backpack == null)
				return;

			Map map = from.Map;
			Mobile target = from.Combatant;
			Map targetmap = target.Map;
			Point3D targetloc = target.Location;

			if (  target == null || targetmap == Map.Internal || targetmap == null )
				return;

		    BaseExplosionPotion explosion = (BaseExplosionPotion)from.Backpack.FindItemByType( typeof( BaseExplosionPotion ) );
		    if( explosion != null && explosion.Amount > 0)
		    {

				if( explosion.Amount > 1 )
				{
					Mobile.LiftItemDupe( explosion, 1 );
				}

				explosion.Internalize();

				Effects.SendMovingEffect( from, target, 0x2408, 7, 0, false, false, 0, 0 );

				Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( Reposition_OnTick ), new object[]{ explosion, targetloc, targetmap } );
				
			}
		}

		private void Reposition_OnTick( object state )
		{
			if ( Deleted )
				return;

			object[] states = (object[])state;
			Item pot = (Item)states[0];
			Point3D p = (Point3D)states[1];
			Map map = (Map)states[2];

			pot.MoveToWorld( p, map );

			m_potTimer = Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.25 ), 5, new TimerStateCallback( Detonate_OnTick ), new object[]{ pot, p, map, 3 } ); // 3.6 seconds explosion delay


		}

		private void Detonate_OnTick( object state )
		{

			object[] states = (object[])state;
			BaseExplosionPotion pot = (BaseExplosionPotion)states[0];
			Point3D loc = (Point3D)states[1];
			Map map = (Map)states[2];
			int timer = (int)states[3];

			if (pot.Deleted)
				return;

			if ( timer == 0 )
			{
				if ( pot.Deleted )
					return;

				pot.Consume();

				if ( map == null )
					return;

				Effects.PlaySound(loc, map, 0x307);

				Effects.SendLocationEffect(loc, map, 0x36B0, 9, 10, 0, 0);

				IPooledEnumerable eable =  map.GetObjectsInRange( loc, 2 ) ;
				ArrayList toExplode = new ArrayList();

				int toDamage = 0;

				foreach ( object o in eable )
				{
					if ( o is Mobile && !(o is BaseRed) ) 
					{
						toExplode.Add( o );
						++toDamage;
					}
				}

				eable.Free();

				for ( int i = 0; i < toExplode.Count; ++i )
				{
					object o = toExplode[i];

					if ( o is Mobile )
					{
						Mobile m = (Mobile)o;

						int damage = Utility.RandomMinMax( 20, 80 );

						AOS.Damage( m, this, damage, 0, 100, 0, 0, 0 );
					}
					else if ( o is BaseExplosionPotion )
					{
						BaseExplosionPotion pots = (BaseExplosionPotion)o;

						pots.Explode( this, false, pots.GetWorldLocation(), pots.Map );
					}
				}

				m_potTimer = null;
			}
			else
			{
				pot.PublicOverheadMessage( MessageType.Regular, 0x22, false, timer.ToString() );
				states[3] = timer - 1;
			}
		}

		private class BandageTimer : Timer 
		{ 
			private BaseRed pk;

			public BandageTimer( BaseRed o ) : base( TimeSpan.FromSeconds( Utility.RandomMinMax(3,6) ) ) 
			{ 
			pk = o;
			Priority = TimerPriority.OneSecond; 
			} 

			protected override void OnTick() 
			{ 
			pk.m_Bandaging = false; 
			} 
		}

		public override bool OnBeforeDeath()
		{

		    Mobile murderer = this.LastKiller;

		    if ( murderer is PlayerMobile )
		    {
				if (Utility.RandomMinMax(1, 250) == 69 && (this is SpawnRedMage || this is SpawnRed || this is DreadLord) )
				{
					Container pack = Backpack;
					pack.DropItem( new EtherealHorse() );
				}
			}

			return base.OnBeforeDeath();
		}

		public BaseRed( Serial serial ) : base( serial ) 
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
			AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
		} 
	} 
}   
