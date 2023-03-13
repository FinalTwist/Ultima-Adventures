using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.Multis;
using System.Collections;
using System.Collections.Generic;
using Server.Regions;

namespace Server.SkillHandlers
{
	public class Provocation
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Provocation].Callback = new SkillUseCallback( OnUse );
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
			from.SendLocalizedMessage( 501587 ); // Whom do you wish to incite?
			from.Target = new InternalFirstTarget( from, instrument );
		}

		private class InternalFirstTarget : Target
		{
			private BaseInstrument m_Instrument;

			public InternalFirstTarget( Mobile from, BaseInstrument instrument ) : base( BaseInstrument.GetBardRange( from, SkillName.Provocation ), false, TargetFlags.None )
			{
				m_Instrument = instrument;
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
				
				if (targeted == from && targeted is PlayerMobile && ((PlayerMobile)from).Troubadour() && ((Mobile)targeted).Map != null )
				{
						Region reg = Region.Find( from.Location, from.Map );
						if (reg.IsPartOf( typeof( PublicRegion ) ) || reg.IsPartOf( typeof( TownRegion ) ))
						{
							from.SendMessage("You can't do that here.");
							return;
						}

						int range = BaseInstrument.GetBardRange( from, SkillName.Provocation );
						
						double immunetest = ( (from.Skills[SkillName.Musicianship].Value / 250) + (from.Dex/350) ) /2;
						if (immunetest > 0.50)
							immunetest = 0.50;
						
						ArrayList targets = new ArrayList();
						ArrayList matched = new ArrayList();
						
						foreach ( Mobile m in from.GetMobilesInRange( range ) )
						{
							BaseHouse house = BaseHouse.FindHouseAt( m );
							
							if ( m == from || !from.CanBeHarmful ( m, false ) || !(m is BaseCreature) || m.Map == null || !from.CanSee( m ))
								continue;
							
							if ( (m is BaseCreature && ((BaseCreature)m).IsHitchStabled) || house != null)
								continue;
							
							if (((BaseCreature)m).Combatant == from && Utility.RandomBool()) // half chance of mobs being provoked if targetting the player
								continue;
									
							if ( m is BaseCreature && ( ((BaseCreature)m).Unprovokable || ((BaseCreature)m).BardImmune ) && !(Utility.RandomDouble() < immunetest))
							{
								((BaseCreature)m).Combatant = from;
								continue;
							}
							
							double diff = (m_Instrument.GetDifficultyFor(from, m ) * 1.25) - 5.0;
							
							if (SkillHandlers.Discordance.IsDiscorded(m))
								diff /= 1.20;
							
							if ( diff > (from.Skills[SkillName.Musicianship].Value /3) )
							{
								if (from.CheckTargetSkill( SkillName.Provocation, m, diff-25.0, diff+25.0 ) )
								{
									targets.Add( m );
								}
								else if (Utility.RandomBool()  && ((BaseCreature)m).Combatant == null)
									((BaseCreature)m).Combatant = from;

							}
							else 
							{
								targets.Add( m );
							}
						}
						
						bool onesuccess = false;
							    
						for ( int i = 0; i < targets.Count; ++i )
						{
							BaseCreature targ = ( BaseCreature )targets[ i ];
							BaseCreature targ2 = null;
							
							if (targ.Alive && targ != null && !targ.Deleted && !matched.Contains( targ ))
							{		
								for ( int ii = 0; !matched.Contains(targ) && ii < targets.Count; ++ii )
								{
									targ2 = (BaseCreature)targets[ ii ];
									
									if (targ != targ2 && targ2.Alive && targ2 != null && !targ2.Deleted && !matched.Contains( targ2 ) )
									{	
										matched.Add(targ);
										matched.Add(targ2);
										
										targ.Provoke( from, targ2, true );
										
										if (!onesuccess)
											onesuccess = true;
									}
								}
							}
						}
							
						if (onesuccess)
						{
							m_Instrument.PlayInstrumentWell( from );
							m_Instrument.ConsumeUse( from );
							from.SendMessage("You provoke the entire area!");
							from.NextSkillTime = Core.TickCount + (12 * 1000);
						}
						else
						{
							from.SendMessage("Your music cannot influence any creatures here.");
							m_Instrument.PlayInstrumentWell( from );
							m_Instrument.ConsumeUse( from );
							from.NextSkillTime = Core.TickCount + (6 * 1000);
						}

				}

				else if ( targeted is BaseCreature && from.CanBeHarmful( (Mobile)targeted, true ) )
				{
					BaseCreature creature = (BaseCreature)targeted;

					if ( !m_Instrument.IsChildOf( from.Backpack ) )
					{
						from.SendLocalizedMessage( 1062488 ); // The instrument you are trying to play is no longer in your backpack!
						from.NextSkillTime = Core.TickCount + 1000;
					}
					else if ( creature.Controlled )
					{
						from.SendLocalizedMessage( 501590 ); // They are too loyal to their master to be provoked.
					}
					else if ( ((BaseCreature)creature).IsParagon && BaseInstrument.GetBaseDifficulty( creature ) >= 160.0 && !(from is PlayerMobile && ((PlayerMobile)from).Troubadour()) )
					{
						from.SendLocalizedMessage( 1049446 ); // You have no chance of provoking those creatures.
					}
					else
					{
						from.RevealingAction();
						m_Instrument.PlayInstrumentWell( from );
						from.SendLocalizedMessage( 1008085 ); // You play your music and your target becomes angered.  Whom do you wish them to attack?
						from.Target = new InternalSecondTarget( from, m_Instrument, creature );
					}
				}
				else
				{
					from.SendLocalizedMessage( 501589 ); // You can't incite that!
				}
			}
		}

		private class InternalSecondTarget : Target
		{
			private BaseCreature m_Creature;
			private BaseInstrument m_Instrument;

			public InternalSecondTarget( Mobile from, BaseInstrument instrument, BaseCreature creature ) : base( BaseInstrument.GetBardRange( from, SkillName.Provocation ), false, TargetFlags.None )
			{
				m_Instrument = instrument;
				m_Creature = creature;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				double immunetest = ( (from.Skills[SkillName.Musicianship].Value / 250) + (from.Dex/350) ) /2;
				if (immunetest > 0.50)
					immunetest = 0.50;
				
				from.RevealingAction();

				if ( targeted is BaseCreature )
				{
					BaseCreature creature = (BaseCreature)targeted;

					if ( !m_Instrument.IsChildOf( from.Backpack ) )
					{
						from.SendLocalizedMessage( 1062488 ); // The instrument you are trying to play is no longer in your backpack!
						from.NextSkillTime = Core.TickCount + 1000;
					}
					else if ( ( m_Creature.Unprovokable || creature.BardImmune ) && !(from is PlayerMobile && ((PlayerMobile)from).Troubadour()) )
					{
						from.SendLocalizedMessage( 1049446 ); // You have no chance of provoking those creatures.
					}
					else if ( ( m_Creature.Unprovokable || creature.BardImmune ) && (from is PlayerMobile && ((PlayerMobile)from).Troubadour()) && !(Utility.RandomDouble() < immunetest))
					{
						from.SendMessage("You fail to influence this creature's mind.");
					}
					else if ( m_Creature.Map != creature.Map || !m_Creature.InRange( creature, BaseInstrument.GetBardRange( from, SkillName.Provocation ) ) )
					{
						from.SendLocalizedMessage( 1049450 ); // The creatures you are trying to provoke are too far away from each other for your music to have an effect.
					}
					else if ( m_Creature != creature )
					{
								Region reg = Region.Find( from.Location, from.Map );
								if (reg.IsPartOf( "the Basement" ) && from is PlayerMobile && ((PlayerMobile)from).SoulBound)
									from.NextSkillTime = Core.TickCount + 1000;
								if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() )
									from.NextSkillTime = Core.TickCount + (5 * 1000);
								else
									from.NextSkillTime = Core.TickCount + (10 * 1000);

						double diff = ((m_Instrument.GetDifficultyFor(from, m_Creature ) + m_Instrument.GetDifficultyFor(from, creature )) * 0.5) - 5.0;
							
						if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() && diff > 30)
						{
							diff /= 1.25;
							
							if (SkillHandlers.Discordance.IsDiscorded(m_Creature))
								diff /= 1.25;
						}
						
						double music = from.Skills[SkillName.Musicianship].Value;
						
						if (from is PlayerMobile && ((PlayerMobile)from).SoulBound && m_Creature is TrainingElemental1)
							diff = from.Skills[SkillName.Provocation].Value;

						if ( music > 100.0 )
							diff -= (music - 100.0) * 0.5;

						if ( from.CanBeHarmful( m_Creature, true ) && from.CanBeHarmful( creature, true ) )
						{
							if ( !BaseInstrument.CheckMusicianship( from ) )
							{
								if (reg.IsPartOf( "the Basement" ) && from is PlayerMobile && ((PlayerMobile)from).SoulBound)
									from.NextSkillTime = Core.TickCount + 1000;
								else if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() )
									from.NextSkillTime = Core.TickCount + (3 * 1000);
								else
									from.NextSkillTime = Core.TickCount + (5 * 1000);

								from.SendLocalizedMessage( 500612 ); // You play poorly, and there is no effect.
								m_Instrument.PlayInstrumentBadly( from );
								m_Instrument.ConsumeUse( from );
							}
							else
							{

								if ( reg.IsPartOf( "the Basement" ) && ( creature is TrainingElemental1 || creature is TrainingElemental) )
								from.CheckTargetSkill( SkillName.Provocation, creature, 0, 120 );

								if ( !from.CheckTargetSkill( SkillName.Provocation, creature, diff-25.0, diff+25.0 ) )
								{
									if (reg.IsPartOf( "the Basement" ) && from is PlayerMobile && ((PlayerMobile)from).SoulBound)
										from.NextSkillTime = Core.TickCount + 1000;
									else if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() )
										from.NextSkillTime = Core.TickCount + (5 * 1000);
									else
										from.NextSkillTime = Core.TickCount + (5 * 1000);
									
									from.SendLocalizedMessage( 501599 ); // Your music fails to incite enough anger.
									m_Instrument.PlayInstrumentBadly( from );
									m_Instrument.ConsumeUse( from );
								}
								else
								{
									from.SendLocalizedMessage( 501602 ); // Your music succeeds, as you start a fight.
									m_Instrument.PlayInstrumentWell( from );
									m_Instrument.ConsumeUse( from );
									m_Creature.Provoke( from, creature, true );
								}
							}
						}
					}
					else //provoke nuking
					{
						if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() && from.CanBeHarmful( m_Creature, true ) && from.CanBeHarmful( creature, true ) && (m_Creature is BaseCreature && !((BaseCreature)m_Creature).Controlled) )
						{
							
							double diff = m_Instrument.GetDifficultyFor(from, m_Creature )  - 5.0;
							
							if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() && diff > 30)
							{
								diff /= 1.25;

								if (SkillHandlers.Discordance.IsDiscorded(m_Creature))
									diff /= 1.25;
							}	
							
							if ( from.CheckTargetSkill( SkillName.Provocation, m_Creature, diff-25.0, diff+25.0 ) && m_Creature is BaseCreature )
							{
								BaseCreature mob = m_Creature as BaseCreature;
								
								double bonus = 2 + ((from.Skills[SkillName.Provocation].Value + from.Skills[SkillName.Musicianship].Value) / 70) + (from.Dex / 80);
								int min = (int)((double)mob.DamageMin * bonus );
								int max = (int)((double)mob.DamageMax * bonus );

								int dmg = AdventuresFunctions.DiminishingReturns( Utility.RandomMinMax(min, max) , 300, 10 );

								int stonebonus = m_Creature.Hits / 10;

								if (stonebonus > 1000)
									stonebonus = 1000;

								if (SkillHandlers.Discordance.IsDiscorded(m_Creature) && stonebonus > 0)
									dmg += stonebonus; //bonus for discorded targets								
								
								AOS.Damage( m_Creature, from, dmg, true, 100, 0, 0, 0, 0, 0, 0, false, false, false, false, true);
								
								from.SendMessage("The creature attacks itself!");
								from.SendMessage("[testing] for " + min + " min " + max + " max damage.");
								if (SkillHandlers.Discordance.IsDiscorded(m_Creature))
									from.SendMessage("Discord bonus of " + stonebonus + " applied.");
								
								m_Instrument.PlayInstrumentWell( from );
								m_Instrument.ConsumeUse( from );
							}
							else if (Utility.RandomBool())
							{
								from.SendMessage("The creature rebuffs your influence and attacks you!");
								if (m_Creature is BaseCreature)
									((BaseCreature)m_Creature).Combatant = from;
							}
							else
							{
								from.SendMessage("Your song failed to get the creature to harm itself.");
							}
							Region reg = Region.Find( from.Location, from.Map );
								if (reg.IsPartOf( "the Basement" ) && from is PlayerMobile && ((PlayerMobile)from).SoulBound)
									from.NextSkillTime = Core.TickCount + 1000;
								else
									from.NextSkillTime = Core.TickCount + (5 * 1000);
							
						}
						else 	
							from.SendLocalizedMessage( 501593 ); // You can't tell someone to attack themselves!
					}
				}
				else
				{
					from.SendLocalizedMessage( 501589 ); // You can't incite that!
				}
			}
		}
	}
}
