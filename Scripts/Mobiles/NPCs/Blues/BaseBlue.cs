using System; 
using System.Collections; 
using Server;
using Server.Misc; 
using Server.Items; 
using Server.Mobiles;
using Server.Gumps;
using Server.Targeting;

namespace Server.Mobiles 
{ 
	public class BaseBlue : BaseConvo
	{ 

		private bool m_Bandaging;

		private bool m_Threwpot;

		private bool m_attacking;

		private bool m_revealing;
		public BaseBlue(AIType ai, FightMode fm, int PR, int FR, double AS, double PS) : base( ai, fm, PR, FR, AS, PS )
		{
			SpeechHue = Utility.RandomDyedHue(); 
			Hue = Utility.RandomSkinHue();
			RangePerception = BaseCreature.DefaultRangePerception;
			AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
		}

		public override bool ReacquireOnMovement{ get { return false; } }
		public override TimeSpan ReacquireDelay{ get { return TimeSpan.FromSeconds( 3.0 ); } }
		public override bool BardImmune{ get{ return false; } }

		public override bool IsEnemy( Mobile m )
		{


			Region reg = Region.Find( this.Location, this.Map );

			if ( reg.IsPartOf( "the Basement" ))
				return false;

			if ( ( DisguiseTimers.IsDisguised( m ) || Server.Spells.Fifth.IncognitoSpell.m_Timers.Contains(m)) && !m.Criminal )
				return false;

			if ((this is MidlandGuard) && !m.Criminal && AdventuresFunctions.IsPuritain((object)this))
				return Server.Misc.IntelligentAction.GetMidlandEnemies( m, this, false );
				
			if (this is MercenaryGuard && !m.Criminal )
				return false;

			if ( m is BaseBlue || m is BaseChild || m is BaseVendor || m is BasePerson || m is Citizens || m is PlayerVendor || m is TownHerald || m is Townsperson )
				return false;

			if ( this is BlueGuard && m is PlayerMobile && (Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "Lamut County") && !m.Criminal )
				return false;

			if (this is BlueGuard && m is MonsterNestEntity && ((MonsterNestEntity)m).MonstersNest is PortalGood )
				return false;

			if (this is BlueGuard && m is MonsterNestEntity && ((MonsterNestEntity)m).MonstersNest is PortalEvil )
				return true;

			if (this is BlueGuard && m.Combatant is MonsterNestEntity )
			{
				Mobile nest = m.Combatant;
				if (((MonsterNestEntity)nest).MonstersNest is PortalGood)
					return true;
			}

			if ( this is BlueGuard && m is PlayerMobile && ( m.Criminal || m.Karma <= -5000 || m.Kills >= 2 ) )
				return true;

			if ( m is PlayerMobile && ( m.Karma >= -6500 && !m.Criminal ) )
				return false;

			if ( m is BaseCreature )
			{
			    BaseCreature c = (BaseCreature)m;

			    if( c.Controlled && c.ControlMaster is PlayerMobile )
			    {
					PlayerMobile d = (PlayerMobile)c.ControlMaster;
					
					if ( this is BlueGuard && d is PlayerMobile && (Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "Lamut County") && !d.Criminal && d.Kills < 5)
						return false;
					
					if ( this is BlueGuard && d is PlayerMobile && ( d.Criminal || d.Karma <= -5000 || d.Kills >= 2 ) )
						return true;				
					
					if (d.Kills <= 2 && !d.Criminal && d.Karma >= -7500)
						return false;
			    }

			    if ( c.Karma >= 0 || c.FightMode == FightMode.None )
				return false;

			}	
			
			return true;
		}

		public virtual bool HealsYoungPlayers{ get{ return true; } }

		public virtual bool CheckResurrect( Mobile m )
		{
			if (this is BlueGuard && (m.Kills >= 2 || m.Karma < -5000))
				return false;
			if (m.Karma < -6500)
				return false;
			if (this is MercenaryGuard && m.Criminal)
				return false;

			return true;
		}

		public DateTime m_NextResurrect;
		public static TimeSpan ResurrectDelay = TimeSpan.FromSeconds( 20.0 );

		public virtual void OfferResurrection( Mobile m )
		{
			if ( this is BlueGuard && (m.Criminal || m.Kills >= 2 || m.Karma <= -5000) )
				return;

			if (this is MercenaryGuard && m.Criminal)
				return;

			if (this is BlueGuard)
			{
				Direction = GetDirectionTo( m );

				m.PlaySound( 0x214 );
				m.FixedEffect( 0x376A, 10, 16 );

				if ( (m is PlayerMobile && !((PlayerMobile)m).SbResTimer)  )
				{
					m.CloseGump( typeof( ResurrectCostGump ) );
					m.SendGump( new ResurrectCostGump( m, 1 ) );
				}
			}
			else if ( (m is PlayerMobile && !((PlayerMobile)m).SbResTimer)  )
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
			if ( DateTime.UtcNow >= m_NextResurrect && !m.Criminal && m.Kills <= 2 && !m.Hidden)
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
				if ( !m.Alive && !IsEnemy(m) )
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
				else if ( this.HealsYoungPlayers && m.Hits < m.HitsMax && m is PlayerMobile || m is BaseBlue )
				{
					OfferHeal( (PlayerMobile) m );
				}
			}
		}

		public override void OnThink()
		{
			base.OnThink();
			
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
			if (m_attacking == false && this.Combatant != null) // new target found, just started attacking
			{
				m_attacking = true;
				CallOthers( this.Combatant );
			}
			if (m_attacking && this.Combatant == null)
				m_attacking = false;

			//only certain routines apply to certain mobs
			if ( this is BlueGuard || this is SpawnHelper || this is SpawnHelperMage || this is MercenaryGuard)
			{
				/*if ( this.Combatant != null && this.Combatant is BaseCreature )
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
				if (this.Combatant != null && this.Combatant is PlayerMobile )
				{
					PlayerMobile pm = (PlayerMobile)this.Combatant;
					if (pm.Hidden && InRange( pm, 10 ) && !m_revealing )
					{
						if (Utility.RandomBool())
							Say("Hah! You will not evade me that easy!");
						else 
							Say("We've seen the likes of you before!");

						Say("Wis Quas");
						m_revealing = true;
						Timer.DelayCall( TimeSpan.FromSeconds( 2 ), new TimerStateCallback ( RevealHim ), new object[]{ pm }  );
					}
					else if ( DateTime.UtcNow >= m_NextResurrect && (int)GetDistanceToSqrt( this.Combatant ) > 2 && (int)GetDistanceToSqrt( this.Combatant ) < 12 && InLOS( this.Combatant ) && !m_teleporting )
					{
						m_NextResurrect = DateTime.UtcNow + ResurrectDelay;
						m_teleporting = true;
						this.Frozen = true;

						Say("Rel Por");
							
						Timer.DelayCall( TimeSpan.FromSeconds( 1 ), new TimerStateCallback ( TeleportTo ), new object[]{ this.Combatant }  );

					}
				}

			}
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{

			if ( !(this is BluePlayer) && attacker is BaseCreature && !m_freezing && !attacker.Frozen && Utility.RandomDouble() > 0.75 && InLOS( attacker ) &&  (int)GetDistanceToSqrt( attacker ) < 10)
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
				if ( mob is BaseBlue && InLOS( mob ) && mob.Combatant == null )
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
					this.Say("Found a villain!");	
				else
					this.Say("This way!");
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


		private class BandageTimer : Timer 
		{ 
			private BaseBlue pk;

			public BandageTimer( BaseBlue o ) : base( TimeSpan.FromSeconds( Utility.RandomMinMax(5,8) ) ) 
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
				if ( murderer.Karma < 0 && this.Map == Map.Ilshenar && this.X <= 1007 && this.Y <= 1280) // killing blues in darkmoor
				{
				}
				else
				{
					murderer.Criminal = true;
					murderer.Kills += 1;
					Server.Items.DisguiseTimers.RemoveDisguise( murderer );
				}
				if (Utility.RandomMinMax(1, 250) == 69 && (this is SpawnHelper || this is SpawnHelperMage) )
				{
					Container pack = Backpack;
					pack.DropItem( new EtherealHorse() );
				}

		    }

		    return base.OnBeforeDeath();
		}

		public BaseBlue( Serial serial ) : base( serial ) 
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
