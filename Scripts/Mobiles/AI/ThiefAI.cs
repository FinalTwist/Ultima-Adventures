using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Items;

//
// This is a first simple AI
//
//

namespace Server.Mobiles
{
	public class ThiefAI : BaseAI
	{
		public ThiefAI(BaseCreature m) : base (m)
		{
		}

		private Item m_toDisarm;
		public override bool DoActionWander()
		{
			m_Mobile.DebugSay( "I have no combatant" );

			if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
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
			Mobile combatant = m_Mobile.Combatant;

			if ( combatant == null || combatant.Deleted || combatant.Map != m_Mobile.Map )
			{
				m_Mobile.DebugSay( "My combatant is gone, so my guard is up" );

				Action = ActionType.Guard;

				return true;
			}

			if ( WalkMobileRange( combatant, 1, true, m_Mobile.RangeFight, m_Mobile.RangeFight ) )
			{
				m_Mobile.Direction = m_Mobile.GetDirectionTo( combatant );
				if ( m_toDisarm == null )
					m_toDisarm = combatant.FindItemOnLayer( Layer.OneHanded );

				if ( m_toDisarm == null )
					m_toDisarm = combatant.FindItemOnLayer( Layer.TwoHanded );

				if ( m_toDisarm != null && m_toDisarm.IsChildOf( m_Mobile.Backpack ) )
				{
					m_toDisarm = combatant.FindItemOnLayer( Layer.OneHanded );
					if ( m_toDisarm == null )
						m_toDisarm = combatant.FindItemOnLayer( Layer.TwoHanded );
				}
				if ( !m_Mobile.DisarmReady && m_Mobile.Skills[SkillName.Wrestling].Value >= 80.0 && m_Mobile.Skills[SkillName.ArmsLore].Value >= 80.0 && m_toDisarm != null )
					EventSink.InvokeDisarmRequest( new DisarmRequestEventArgs( m_Mobile ) );

				if ( m_toDisarm != null && m_toDisarm.IsChildOf( combatant.Backpack ) && Core.TickCount - m_Mobile.NextSkillTime >= 0 && (m_toDisarm.LootType != LootType.Blessed && m_toDisarm.LootType != LootType.Newbied) )
				{
					m_Mobile.DebugSay( "Trying to steal from combatant." );
					m_Mobile.UseSkill( SkillName.Stealing );
					if ( m_Mobile.Target != null )
						m_Mobile.Target.Invoke( m_Mobile, m_toDisarm );
				}
				else if ( m_toDisarm == null && Core.TickCount - m_Mobile.NextSkillTime >= 0 )
				{
					Container cpack = combatant.Backpack;

					if ( cpack != null )
					{
						Item steal1 = cpack.FindItemByType( typeof ( Bandage ) );
						if ( steal1 != null ) 
						{
							m_Mobile.DebugSay( "Trying to steal from combatant." );
							m_Mobile.UseSkill( SkillName.Stealing );
							if ( m_Mobile.Target != null )
								m_Mobile.Target.Invoke( m_Mobile, steal1 );
						}
						Item steal2 = cpack.FindItemByType( typeof ( Nightshade ) );
						if ( steal2 != null ) 
						{
							m_Mobile.DebugSay( "Trying to steal from combatant." );
							m_Mobile.UseSkill( SkillName.Stealing );
							if ( m_Mobile.Target != null )
								m_Mobile.Target.Invoke( m_Mobile, steal2 );
						}
						Item steal3 = cpack.FindItemByType( typeof ( BlackPearl ) );
						if ( steal3 != null ) 
						{
							m_Mobile.DebugSay( "Trying to steal from combatant." );
							m_Mobile.UseSkill( SkillName.Stealing );
							if ( m_Mobile.Target != null )
								m_Mobile.Target.Invoke( m_Mobile, steal3 );
						}

						Item steal4 = cpack.FindItemByType( typeof ( MandrakeRoot ) );
						if ( steal4 != null ) 
						{
							m_Mobile.DebugSay( "Trying to steal from combatant." );
							m_Mobile.UseSkill( SkillName.Stealing );
							if ( m_Mobile.Target != null )
								m_Mobile.Target.Invoke( m_Mobile, steal4 );
						}

						Item steal5 = cpack.FindItemByType( typeof ( Spellbook ) );
						if ( steal5 != null ) 
						{
							m_Mobile.DebugSay( "Trying to steal from combatant." );
							m_Mobile.UseSkill( SkillName.Stealing );
							if ( m_Mobile.Target != null )
								m_Mobile.Target.Invoke( m_Mobile, steal5 );
						}

						Item steal6 = cpack.FindItemByType( typeof ( Runebook ) );
						if ( steal6 != null ) 
						{
							m_Mobile.DebugSay( "Trying to steal from combatant." );
							m_Mobile.UseSkill( SkillName.Stealing );
							if ( m_Mobile.Target != null )
								m_Mobile.Target.Invoke( m_Mobile, steal6 );
						}

						Item steal7 = cpack.FindItemByType( typeof ( BasePotion ) );
						if ( steal7 != null ) 
						{
							m_Mobile.DebugSay( "Trying to steal from combatant." );
							m_Mobile.UseSkill( SkillName.Stealing );
							if ( m_Mobile.Target != null )
								m_Mobile.Target.Invoke( m_Mobile, steal7 );
						}

						Item steal8 = cpack.FindItemByType( typeof ( SpellScroll ) );
						if ( steal8 != null ) 
						{
							m_Mobile.DebugSay( "Trying to steal from combatant." );
							m_Mobile.UseSkill( SkillName.Stealing );
							if ( m_Mobile.Target != null )
								m_Mobile.Target.Invoke( m_Mobile, steal8 );
						}

						Item steal9 = cpack.FindItemByType( typeof ( BaseMagicStaff ) );
						if ( steal9 != null ) 
						{
							m_Mobile.DebugSay( "Trying to steal from combatant." );
							m_Mobile.UseSkill( SkillName.Stealing );
							if ( m_Mobile.Target != null )
								m_Mobile.Target.Invoke( m_Mobile, steal9 );
						}

						Item steal10 = cpack.FindItemByType( typeof ( Gold ) );
						if ( steal10 != null ) 
						{
							m_Mobile.DebugSay( "Trying to steal from combatant." );
							m_Mobile.UseSkill( SkillName.Stealing );
							if ( m_Mobile.Target != null )
								m_Mobile.Target.Invoke( m_Mobile, steal10 );
						}

						if (	steal1 == null && 
								steal2 == null && 
								steal3 == null && 
								steal4 == null && 
								steal5 == null && 
								steal6 == null && 
								steal7 == null && 
								steal8 == null && 
								steal9 == null && 
								steal10 == null )
						{
							m_Mobile.DebugSay( "I am going to flee from {0}", combatant.Name );

							Action = ActionType.Flee;
						}
					}
				}
			}
			else
			{
				m_Mobile.DebugSay( "I should be closer to {0}", combatant.Name );
			}

			if ( m_Mobile.Hits < m_Mobile.HitsMax * 20/100 && !m_Mobile.IsParagon )
			{
				// We are low on health, should we flee?

				bool flee = false;

				if ( m_Mobile.Hits < combatant.Hits )
				{
					// We are more hurt than them

					int diff = combatant.Hits - m_Mobile.Hits;

					flee = ( Utility.Random( 0, 100 ) > (10 + diff) ); // (10 + diff)% chance to flee
				}
				else
				{
					flee = Utility.Random( 0, 100 ) > 10; // 10% chance to flee
				}

				if ( flee )
				{
					m_Mobile.DebugSay( "I am going to flee from {0}", combatant.Name );

					Action = ActionType.Flee;
				}
			}

			return true;
		}

		public override bool DoActionGuard()
		{
			if ( AcquireFocusMob(m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
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