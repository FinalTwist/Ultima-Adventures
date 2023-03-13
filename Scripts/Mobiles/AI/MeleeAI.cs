using System;
using System.Collections;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	public class MeleeAI : BaseAI
	{
		public MeleeAI(BaseCreature m) : base (m)
		{
		}

		public void RunTo( Mobile m )
		{
			if( !SmartAI )
			{
				if( !MoveTo( m, true, m_Mobile.RangeFight ) )
					OnFailedMove();

				return;
			}

			if( !m_Mobile.InRange( m, m_Mobile.RangeFight ) )
			{
					if( !MoveTo( m, true, 1 ) )
						OnFailedMove();
			}
		}
		
		public void OnFailedMove()
		{
			/*
			if( !m_Mobile.DisallowAllMoves && !Server.Mobiles.BasePirate.IsSailor( m_Mobile ) && ( SmartAI ? Utility.Random( 4 ) == 0 : ScaleByMagery( TeleportChance ) > Utility.RandomDouble() ) )
			{
				if( m_Mobile.Target != null )
					m_Mobile.Target.Cancel( m_Mobile, TargetCancelType.Canceled );

				new TeleportSpell( m_Mobile, null ).Cast();

				m_Mobile.DebugSay( "I am stuck, I'm going to try teleporting away" );
			}
			else */if( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				if( m_Mobile.Debug )
					m_Mobile.DebugSay( "My move is blocked, so I am going to attack {0}", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;
			}
			else
			{
				WalkRandom(0, 1, 1); // Final, can no longer be cornered! will try a random move anywhere
				m_Mobile.DebugSay( "I am stuck" );
			}
		}

		public override bool DoActionWander()
		{
			m_Mobile.DebugSay( "I have no combatant" );

			if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				if ( m_Mobile.Debug )
					m_Mobile.DebugSay( "I have detected {0}, attacking", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;
			}
			else
			{
				base.DoActionWander();
			}

			return true;
		}

		public override bool DoActionCombat()
		{
				/*if (Insensitive.Contains(strategy, "many") && CheckCanCastMagery(6))
				{
					return new MassCurse( m_Mobile, null );
				}*/
			

			if ( m_Mobile.Combatant == null || m_Mobile.Combatant.Deleted || m_Mobile.Combatant.Map != m_Mobile.Map || !m_Mobile.Combatant.Alive || m_Mobile.Combatant.IsDeadBondedPet )
			{
				if (m_Mobile.Combatant.Alive && !m_Mobile.Combatant.Deleted && m_Mobile.Combatant != null)
				{
					FindEnemy(m_Mobile.Combatant);
				}
				if (m_Mobile.Combatant == null || m_Mobile.Combatant.Deleted || m_Mobile.Combatant.Map != m_Mobile.Map || !m_Mobile.Combatant.Alive || m_Mobile.Combatant.IsDeadBondedPet)
				{
					m_Mobile.DebugSay( "My combatant is gone, so my guard is up" );

					Action = ActionType.Guard;
					return true;
				}
			}

			Mobile combatant = m_Mobile.Combatant;
			string strategy = AssessStrategy(m_Mobile.Combatant);
			int distance = (int)m_Mobile.GetDistanceToSqrt( m_Mobile.Combatant );
			m_Mobile.Warmode = true;

			if ( !m_Mobile.InRange( combatant, m_Mobile.RangePerception ) )
			{
				//Okay so someone went out of range, should we move to another nearby, or try and follow the combatant?
				if (SmartAI && distance >= (m_Mobile.RangePerception + 3) && m_Mobile.InLOS(combatant) ) 
				{
					//they are still closeby, could charge
					if ( m_Mobile is BaseCreature && m_Mobile.Stam > (m_Mobile.StamMax / 2) && Utility.RandomDouble() > 0.80)
						CustomAbility.Charge.DoEffects((BaseCreature)m_Mobile, combatant, m_Mobile.DamageMin, m_Mobile.DamageMax);
					//didnt charge, can maybe move there if the thing has low health?
					else if ( Utility.RandomBool() && combatant.Hits < (combatant.Hits / 3) && Utility.RandomDouble() > 0.75 )
					{

					}//Runto combatant
				}
				
				
				// They are somewhat far away, can we find something else?

				if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
				{
					m_Mobile.Combatant = m_Mobile.FocusMob;
					m_Mobile.FocusMob = null;
				}
				else if ( !m_Mobile.InRange( combatant, m_Mobile.RangePerception * 3 ) )
				{
					m_Mobile.Combatant = null;
				}

				combatant = m_Mobile.Combatant;

				if ( combatant == null )
				{
					m_Mobile.DebugSay( "My combatant has fled, so I am on guard" );
					Action = ActionType.Guard;

					return true;
				}
			}

			/*if ( !m_Mobile.InLOS( combatant ) )
			{
				if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
				{
					m_Mobile.Combatant = combatant = m_Mobile.FocusMob;
					m_Mobile.FocusMob = null;
				}
			}*/

			if ( MoveTo( combatant, true, m_Mobile.RangeFight ) )
			{
				m_Mobile.Direction = m_Mobile.GetDirectionTo( combatant );
			}
			else if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				if ( m_Mobile.Debug )
					m_Mobile.DebugSay( "My move is blocked, so I am going to attack {0}", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;

				return true;
			}
			else if ( m_Mobile.GetDistanceToSqrt( combatant ) > m_Mobile.RangePerception + 1 )
			{
				if ( m_Mobile.Debug )
					m_Mobile.DebugSay( "I cannot find {0}, so my guard is up", combatant.Name );

				Action = ActionType.Guard;

				return true;
			}
			else
			{
				if ( m_Mobile.Debug )
					m_Mobile.DebugSay( "I should be closer to {0}", combatant.Name );
			}

			if ( !m_Mobile.Controlled && !m_Mobile.Summoned && !m_Mobile.IsParagon && !(m_Mobile is FrankenFighter) && !(m_Mobile is Robot) && !(m_Mobile is GolemFighter) && !(m_Mobile is HenchmanMonster) && !(m_Mobile is HenchmanArcher) && !(m_Mobile is HenchmanWizard) && !(m_Mobile is HenchmanFighter) ) // WIZARD ADDED FOR HENCHMAN
			{
				if ( m_Mobile.Hits < m_Mobile.HitsMax * 20/100 )
				{
					// We are low on health, should we flee?

					bool flee = false;

					if ( m_Mobile.Hits < combatant.Hits )
					{
						// We are more hurt than them

						int diff = combatant.Hits - m_Mobile.Hits;

						flee = ( Utility.Random( 0, 100 ) < (10 + diff) ); // (10 + diff)% chance to flee
					}
					else
					{
						flee = Utility.Random( 0, 100 ) < 10; // 10% chance to flee
					}

					if ( flee && m_Mobile is BaseCreature && ( ((BaseCreature)m_Mobile).CanInfect || m_Mobile is OphidianWarrior || m_Mobile is OphidianMatriarch || m_Mobile is OphidianMage || m_Mobile is OphidianKnight || m_Mobile is OphidianArchmage || m_Mobile is wOphidianWarrior || m_Mobile is wOphidianMatriarch || m_Mobile is wOphidianMage || m_Mobile is wOphidianKnight || m_Mobile is wOphidianArchmage ) )
						flee = false;

					if ( flee )
					{
						if ( m_Mobile.Debug )
							m_Mobile.DebugSay( "I am going to flee from {0}", combatant.Name );

						Action = ActionType.Flee;
					}
				}
			}

			return true;
			
			/* 
			can add CustomAbility
			Charge based on dex
			ImpaleAOE
			Ambush based on hiding
			ThrowBoulder (rock?)
			toxic spores (poisoning)
			if shield, knockback, stun
			takedown (if mounted)
			*/
			
			
		}

		public override bool DoActionGuard()
		{
			if ( AcquireFocusMob(m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				if ( m_Mobile.Debug )
					m_Mobile.DebugSay( "I have detected {0}, attacking", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;
			}
			else
			{
				base.DoActionGuard();
			}

			return true;
		}

		public override bool DoActionFlee()
		{
			if ( m_Mobile.Hits > m_Mobile.HitsMax/2 )
			{
				m_Mobile.DebugSay( "I am stronger now, so I will continue fighting" );
				Action = ActionType.Combat;
			}
			else
			{
				m_Mobile.FocusMob = m_Mobile.Combatant;
				base.DoActionFlee();
			}

			return true;
		}
	}
}
