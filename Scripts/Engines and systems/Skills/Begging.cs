using System;
using Server;
using Server.Misc;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Regions;

namespace Server.SkillHandlers
{
	public class Begging
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Begging].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
			m.RevealingAction();

			m.Target = new InternalTarget();
			m.RevealingAction();

			m.SendLocalizedMessage( 500397 ); // To whom do you wish to grovel?

			return TimeSpan.FromHours( 6.0 );
		}

		public static bool IsGonnaAttack( Mobile m )
		{
			if ( m is BasePerson || m is BaseVendor || m is PlayerBarkeeper || m is PlayerVendor || m is Citizens )

				return false;

			if ( m is PlayerMobile && !m.Criminal && m.Kills<1 )

				return false;

			if (m is BaseCreature)
			{
				BaseCreature c = (BaseCreature)m;

				if( c.Controlled || c.FightMode == FightMode.Aggressor || c.FightMode == FightMode.None )

					return false;
			}
			return true;
		}

		public static bool IsMageryCreature( BaseCreature bc )
		{
			return ( bc != null && bc.AI == AIType.AI_Mage && bc.Skills[SkillName.Magery].Base > 5.0 );
		}

		public static bool IsFireBreathingCreature( BaseCreature bc )
		{
			if ( bc == null )
				return false;

			return bc.HasBreath;
		}

		public static bool IsPoisonImmune( BaseCreature bc )
		{
			return ( bc != null && bc.PoisonImmune != null );
		}

		public static int GetPoisonLevel( BaseCreature bc )
		{
			if ( bc == null )
				return 0;

			Poison p = bc.HitPoison;

			if ( p == null )
				return 0;

			return p.Level + 1;
		}

		public static double GetBaseDifficulty( Mobile targ )
		{
			double val = targ.Fame / 180; //Adventures dynamicfame does all this already.

			return val;
		}

		private class InternalTarget : Target
		{
			private bool m_SetSkillTime = true;

			public InternalTarget() :  base ( 12, false, TargetFlags.None )
			{
			}

			protected override void OnTargetFinish( Mobile from )
			{
				if ( m_SetSkillTime )
					from.NextSkillTime = Core.TickCount;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				from.RevealingAction();

				int number = -1;

				if ( targeted is Mobile )
				{
					Mobile targ = (Mobile)targeted;
					Region reg = Region.Find( targ.Location, targ.Map );

					if ( ( targ == from ) && ( from is PlayerMobile ) )
					{
						CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );

						if ( DB.CharacterBegging == 0 )
						{
							DB.CharacterBegging = 1;
							from.SendMessage(68, "You set your demeanor to begging.");
						}
						else
						{
							DB.CharacterBegging = 0;
							from.SendMessage(38, "You cease your demeanor of begging.");
						}
					}
					else if ( IsGonnaAttack(targ) && from != targ ) // BEG ENEMIES TO STOP ATTACKING YOU ////////////////////////////////////////
					{
						from.CheckSkill( SkillName.Begging, 0, 100 );
						switch( Utility.RandomMinMax( 0, 8 ) )
						{
							case 0: from.Say( "Leave me alone!" ); break;
							case 1: from.Say( "Have mercy!" ); break;
							case 2: from.Say( "Please, I am but a puny worm!" ); break;
							case 3: from.Say( "Go away!" ); break;
							case 4: from.Say( "I submit to your might!" ); break;
							case 5: from.Say( "Your power has me scared!" ); break;
							case 6: from.Say( "Leave me be!" ); break;
							case 7: from.Say( "I didn't want to hurt you!" ); break;
							case 8: from.Say( "Don't hurt me!" ); break;
						}

						if ( targ is BaseCreature && ((BaseCreature)targ).Uncalmable )
						{
							from.SendMessage("You had no chance at begging this creature from hurting you.");
						}
						else if ( targ is BaseCreature && ((BaseCreature)targ).BardPacified )
						{
							from.SendMessage("This creature is already leaving you alone.");
						}
						else
						{
							double diff = GetBaseDifficulty( targ ) - 10.0;
							double beggar = from.Skills[SkillName.Begging].Value;

							if ( beggar > 100.0 )
								diff -= (beggar - 100.0) * 0.5;

							if ( !from.CheckTargetSkill( SkillName.Begging, targ, diff - 25.0, diff + 25.0 ) )
							{
								from.SendMessage("You fail to convince them to leave you alone.");
							}
							else
							{
								from.NextSkillTime = Core.TickCount + (5 * 1000);
								if ( targ is BaseCreature )
								{
									BaseCreature bc = (BaseCreature)targ;

									from.SendMessage("You beg and plead enough for them to leave you alone.");

									targ.Combatant = null;
									targ.Warmode = false;

									double seconds = 100 - (diff / 1.5);

									if ( seconds > 120 )
										seconds = 120;
									else if ( seconds < 10 )
										seconds = 10;

									bc.Pacify( from, DateTime.UtcNow + TimeSpan.FromSeconds( seconds ) );
								}
								else
								{
									from.SendMessage("You beg and plead enough for them to leave you alone.");
									targ.SendMessage("They somehow begged and pleaded, convincing you to leave them alone.");
									targ.Combatant = null;
									targ.Warmode = false;
								}
							}
						}
						from.NextSkillTime = Core.TickCount + (10 * 1000);
						m_SetSkillTime = false;
						if (from is PlayerMobile && !((PlayerMobile)from).SoulBound)
						{
							if ( from.Karma > -2459 ){ Titles.AwardKarma( from, -40, true ); }
							if ( from.Fame > -2459 ){ Titles.AwardFame( from, -40, true ); }
						}
					}
					else if ( targ.Player && (reg.IsPartOf( typeof( SafeRegion )) || reg.IsPartOf( typeof ( PublicRegion )) || reg.IsPartOf( typeof ( PublicRegion )) ||  reg.IsPartOf( typeof ( ProtectedRegion )) || reg.IsPartOf( typeof ( HouseRegion ))) )
					{
						number = 500398; // Perhaps just asking would work better.
					} 
					else if ( !targ.Body.IsHuman ) // Make sure the NPC is human
					{
						number = 500399; // There is little chance of getting money from that!
					}
					else if ( !from.InRange( targ, 2 ) )
					{
						if ( !targ.Female )
							number = 500401; // You are too far away to beg from him.
						else
							number = 500402; // You are too far away to beg from her.
					}
					else
					{
						// Face eachother
						from.Direction = from.GetDirectionTo( targ );
						targ.Direction = targ.GetDirectionTo( from );

						from.Animate( 32, 5, 1, true, false, 0 ); // Bow

						new InternalTimer( from, targ ).Start();

						m_SetSkillTime = false;
					}
				}
				else // Not a Mobile
				{
					number = 500399; // There is little chance of getting money from that!
				}

				if ( number != -1 )
					from.SendLocalizedMessage( number );
			}

			private class InternalTimer : Timer
			{
				private Mobile m_From, m_Target;

				public InternalTimer( Mobile from, Mobile target ) : base( TimeSpan.FromSeconds( 2.0 ) )
				{
					m_From = from;
					m_Target = target;
					Priority = TimerPriority.TwoFiftyMS;
				}

				protected override void OnTick()
				{
					Container theirPack = m_Target.Backpack;

					double badKarmaChance = 0.5 - ((double)m_From.Karma / 8570);


					if ( theirPack == null )
					{
						m_From.SendLocalizedMessage( 500404 ); // They seem unwilling to give you any money.
					}
					else if ( m_From.GetDistanceToSqrt( m_Target ) > 10 )
					{
						int mesg = 0;
						if ( !m_Target.Female )
							mesg = 500401; // You are too far away to beg from him.
						else
							mesg = 500402; // You are too far away to beg from her.

						m_From.SendLocalizedMessage( mesg );
					}
					else if ( m_From.Karma < 0 && badKarmaChance > Utility.RandomDouble() )
					{
						m_Target.PublicOverheadMessage( MessageType.Regular, m_Target.SpeechHue, 500406 ); // Thou dost not look trustworthy... no gold for thee today!
					}
					else if ( m_From.CheckTargetSkill( SkillName.Begging, m_Target, 0.0, 100.0 ) )
					{
						int toConsume = theirPack.GetAmount( typeof( Gold ) ) / 2;
						
						int fameandkarma = (m_From.Fame + m_From.Karma ) / 15;
						int max = 1000 - Math.Abs(fameandkarma);

						if ( max < 10 )
							max = 10;

						if ( toConsume > max )
							toConsume = max;

						if ( toConsume > 0 )
						{
							int consumed = theirPack.ConsumeUpTo( typeof( Gold ), toConsume );

							if ( consumed > 0 )
							{
								if ( ( m_Target is PlayerMobile && ( Utility.RandomDouble() < ( ( ( (double)max / 600.0 ) + (m_From.Skills[SkillName.Begging].Value/100) ) /2) ) ) || !(m_Target is PlayerMobile) )
								{
									m_Target.PublicOverheadMessage( MessageType.Regular, m_Target.SpeechHue, 500405 ); // I feel sorry for thee...

									Gold gold = new Gold( consumed );

									m_From.AddToBackpack( gold );
									m_From.PlaySound( gold.GetDropSound() );

									if ( m_From.Karma > -2459 ){ Titles.AwardKarma( m_From, -40, true ); }
								}
							
							}
							else
							{
								m_Target.PublicOverheadMessage( MessageType.Regular, m_Target.SpeechHue, 500407 ); // I have not enough money to give thee any!
							}
						}
						else
						{
							m_Target.PublicOverheadMessage( MessageType.Regular, m_Target.SpeechHue, 500407 ); // I have not enough money to give thee any!
						}
					}
					else
					{
						m_From.SendLocalizedMessage( 500404 ); // They seem unwilling to give you any money.
						m_Target.SendMessage(m_From.Name + " tried to beg gold from you!");
					}

					m_From.NextSkillTime = Core.TickCount + (10 * 1000);
				}
			}
		}
	}
}
