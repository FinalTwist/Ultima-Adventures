using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.SkillHandlers
{
	public class Peacemaking
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Peacemaking].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
			if (m is PlayerMobile && ((PlayerMobile)m).Troubadour() )
			{
				double odds = (m.Skills[SkillName.Musicianship].Value + m.Skills[SkillName.Stealth].Value / 500) + (m.Dex/350);
				if (odds > 0.90)
					odds = 0.90;
				
				if (Utility.RandomDouble() > odds)
					m.RevealingAction();
			}
			else
				m.RevealingAction();

			BaseInstrument.PickInstrument( m, new InstrumentPickedCallback( OnPickedInstrument ) );

			return TimeSpan.FromSeconds( 1.0 ); // Cannot use another skill for 1 second
		}

		public static void OnPickedInstrument( Mobile from, BaseInstrument instrument )
		{
			if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() )
			{
				double odds = (from.Skills[SkillName.Musicianship].Value + from.Skills[SkillName.Stealth].Value / 500) + (from.Dex/350);
				if (odds > 0.90)
					odds = 0.90;
				
				if (Utility.RandomDouble() > odds)
					from.RevealingAction();
			}
			else
				from.RevealingAction();
			//from.SendLocalizedMessage( 1049525 ); // Whom do you wish to calm?
			from.SendMessage( "Choose someone to calm or choose yourself to calm everyone in the nearby area." );
			from.Target = new InternalTarget( from, instrument );
			
			if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() )
				from.NextSkillTime = Core.TickCount + (4 * 1000);
			else
				from.NextSkillTime = Core.TickCount + (6 * 1000);
		}

		private class InternalTarget : Target
		{
			private BaseInstrument m_Instrument;
			private bool m_SetSkillTime = true;

			public InternalTarget( Mobile from, BaseInstrument instrument ) :  base( BaseInstrument.GetBardRange( from, SkillName.Peacemaking ), false, TargetFlags.None )
			{
				m_Instrument = instrument;
			}

			protected override void OnTargetFinish( Mobile from )
			{
				if ( m_SetSkillTime )
					from.NextSkillTime = Core.TickCount;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() )
				{
					double odds = (from.Skills[SkillName.Musicianship].Value + from.Skills[SkillName.Stealth].Value / 500) + (from.Dex/350);
					if (odds > 0.90)
						odds = 0.90;

					if (Utility.RandomDouble() > odds)
						from.RevealingAction();
				}
				else
					from.RevealingAction();

				if ( !(targeted is Mobile) )
				{
					from.SendLocalizedMessage( 1049528 ); // You cannot calm that!
				}
				else if ( m_Instrument.Parent != from && !m_Instrument.IsChildOf( from.Backpack ) )
				{
					from.SendLocalizedMessage( 1062488 ); // The instrument you are trying to play is no longer in your backpack!
				}
				else if ( targeted is Mobile )
				{
					m_SetSkillTime = false;
					if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() )
						from.NextSkillTime = Core.TickCount + (5 * 1000);
					else
						from.NextSkillTime = Core.TickCount + (10 * 1000);
					
					double immunetest = ( (from.Skills[SkillName.Musicianship].Value / 250) + (from.Dex/350) ) /2;
					if (immunetest > 0.50)
						immunetest = 0.50;

					if ( targeted == from )
					{
						double testvalue = 120;
						
						if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() )
							testvalue = 90;
						
						// Standard mode : reset combatants for everyone in the area

						if ( !BaseInstrument.CheckMusicianship( from ) )
						{
							from.SendLocalizedMessage( 500612 ); // You play poorly, and there is no effect.
							m_Instrument.PlayInstrumentBadly( from );
							m_Instrument.ConsumeUse( from );
						}
						else if ( !from.CheckSkill( SkillName.Peacemaking, 0.0, testvalue ) )
						{
							from.SendLocalizedMessage( 500613 ); // You attempt to calm everyone, but fail.
							m_Instrument.PlayInstrumentBadly( from );
							m_Instrument.ConsumeUse( from );
						}
						else
						{
							
							Region reg = Region.Find( from.Location, from.Map );
							
							if (reg.IsPartOf( "the Basement" ) && from is PlayerMobile && ((PlayerMobile)from).SoulBound)
								from.NextSkillTime = Core.TickCount + 1000;
							else if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() )
								from.NextSkillTime = Core.TickCount + (3 * 1000);
							else
								from.NextSkillTime = Core.TickCount + (5 * 1000);
							
							m_Instrument.PlayInstrumentWell( from );
							m_Instrument.ConsumeUse( from );

							double seconds = ( from.Skills[SkillName.Musicianship].Value ) / 10;
							
							if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() )
								seconds += 8 * (from.Dex / 500);

							Map map = from.Map;

							if ( map != null )
							{
								int range = BaseInstrument.GetBardRange( from, SkillName.Peacemaking );

								bool calmed = false;

								foreach ( Mobile m in from.GetMobilesInRange( range ) )
								{
									if ( m == from || !from.CanBeHarmful ( m, false ))
										continue;
									
									if (((m is BaseCreature && ((BaseCreature)m).Uncalmable) || (m is BaseCreature && ((BaseCreature)m).AreaPeaceImmune)) && !(from is PlayerMobile && ((PlayerMobile)from).Troubadour()))
										continue;
								
									
									
									if (((m is BaseCreature && ((BaseCreature)m).Uncalmable) || (m is BaseCreature && ((BaseCreature)m).AreaPeaceImmune)) && from is PlayerMobile && ((PlayerMobile)from).Troubadour() && !(Utility.RandomDouble() < immunetest) )
										continue;
										
									calmed = true;

									m.SendLocalizedMessage( 500616 ); // You hear lovely music, and forget to continue battling!
									m.Combatant = null;
									m.Warmode = false;

									if ( m is BaseCreature && !((BaseCreature)m).BardPacified )
										((BaseCreature)m).Pacify( from, DateTime.UtcNow + TimeSpan.FromSeconds( seconds ) );
								}

								if ( !calmed )
									from.SendLocalizedMessage( 1049648 ); // You play hypnotic music, but there is nothing in range for you to calm.
								else
									from.SendLocalizedMessage( 500615 ); // You play your hypnotic music, stopping the battle.
							}
						}
					}
					else
					{
						// Target mode : pacify a single target for a longer duration

						Mobile targ = (Mobile)targeted;

						if ( !from.CanBeHarmful( targ, false ) )
						{
							from.SendLocalizedMessage( 1049528 );
							m_SetSkillTime = true;
						}
						else if ( targ is BaseCreature && ((BaseCreature)targ).Uncalmable && !(from is PlayerMobile && ((PlayerMobile)from).Troubadour()) )
						{
							from.SendLocalizedMessage( 1049526 ); // You have no chance of calming that creature.
							m_SetSkillTime = true;
						}
						else if ( targ is BaseCreature && ((BaseCreature)targ).Uncalmable && (from is PlayerMobile && ((PlayerMobile)from).Troubadour()) && !(Utility.RandomDouble() < immunetest) )
						{
							from.SendMessage("You fail to influence this creature's mind.");
							m_SetSkillTime = true;
						}
						else if ( targ is BaseCreature && ((BaseCreature)targ).BardPacified )
						{
							from.SendLocalizedMessage( 1049527 ); // That creature is already being calmed.
							m_SetSkillTime = true;
						}
						else if ( !BaseInstrument.CheckMusicianship( from ) )
						{
							from.SendLocalizedMessage( 500612 ); // You play poorly, and there is no effect.
							Region reg = Region.Find( from.Location, from.Map );
							if (reg.IsPartOf( "the Basement" ) && from is PlayerMobile && ((PlayerMobile)from).SoulBound)
								from.NextSkillTime = Core.TickCount + 1000;
							else if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() )
								from.NextSkillTime = Core.TickCount + 3000;
							else
								from.NextSkillTime = Core.TickCount + (5 * 1000);
							m_Instrument.PlayInstrumentBadly( from );
							m_Instrument.ConsumeUse( from );
						}
						else
						{
							double diff = m_Instrument.GetDifficultyFor(from, targ ) - 10.0;
							
							if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() && diff > 30)
								diff /= 1.25;
								
							double music = from.Skills[SkillName.Musicianship].Value;

							if ( music > 100.0 )
								diff -= (music - 100.0) * 0.5;

							if ( !from.CheckTargetSkill( SkillName.Peacemaking, targ, diff - 25.0, diff + 25.0 ) )
							{
								from.SendLocalizedMessage( 1049531 ); // You attempt to calm your target, but fail.
								m_Instrument.PlayInstrumentBadly( from );
								m_Instrument.ConsumeUse( from );
							}
							else
							{
								m_Instrument.PlayInstrumentWell( from );
								m_Instrument.ConsumeUse( from );

								Region reg = Region.Find( from.Location, from.Map );
								if (reg.IsPartOf( "the Basement" ) && from is PlayerMobile && ((PlayerMobile)from).SoulBound)
									from.NextSkillTime = Core.TickCount + 1000;
								else if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() )
									from.NextSkillTime = Core.TickCount + 3000;
								else
									from.NextSkillTime = Core.TickCount + (5 * 1000);

								if ( targ is BaseCreature )
								{
									BaseCreature bc = (BaseCreature)targ;

									from.SendLocalizedMessage( 1049532 ); // You play hypnotic music, calming your target.

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
									from.SendLocalizedMessage( 1049532 ); // You play hypnotic music, calming your target.

									targ.SendLocalizedMessage( 500616 ); // You hear lovely music, and forget to continue battling!
									targ.Combatant = null;
									targ.Warmode = false;
								}
							}
						}
					}
				}
				else 
				{
					from.SendLocalizedMessage( 1049528 ); // You cannot calm that!
				}
			}
		}
	}
}
