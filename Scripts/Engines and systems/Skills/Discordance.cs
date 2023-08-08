using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Multis;
using Server.Regions;

namespace Server.SkillHandlers
{
	public class Discordance
	{
		public static int Effect(Mobile from, Mobile targ) {
			int effect = (int)(from.Skills[SkillName.Discordance].Value / 5.0 );
			if (from is PlayerMobile && ((PlayerMobile)from).Troubadour())
				effect = (int)(from.Skills[SkillName.Discordance].Value / 3.0 );
			if ( Core.SE && BaseInstrument.GetBaseDifficulty( targ ) >= 160.0 )
				effect /= 2;
			return effect;
		}
		public static double Scalar(int effect) {
			return (effect * 0.01);
		}
		public static int ReduceValue(Mobile from, Mobile targ, int value) {
			int effect = Discordance.Effect(from, targ);
			double scalar = Discordance.Scalar(effect);
			int reduced = (int)(0 - (value*scalar));
			return reduced;
		}
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Discordance].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
				if (m is PlayerMobile && ((PlayerMobile)m).Troubadour() )
				{
					double odds = (m.Skills[SkillName.Musicianship].Value + m.Skills[SkillName.Stealth].Value / 500) + (m.Dex/500);
					if (odds > 0.95)
						odds = 0.95;

					if (Utility.RandomDouble() > odds)
					{
						m.RevealingAction();
						m.SendMessage("[testing] odds of staying hidden was " + (int)(odds*100));
					}
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
					{
						from.RevealingAction();
						from.SendMessage("[testing] odds of staying hidden was " + (int)(odds*100));
					}
				}
				else
					from.RevealingAction();
			
			from.SendLocalizedMessage( 1049541 ); // Choose the target for your song of discordance.
			from.Target = new DiscordanceTarget( from, instrument );
			Region reg = Region.Find( from.Location, from.Map );
			if (reg.IsPartOf( "the Basement" ) && from is PlayerMobile && ((PlayerMobile)from).SoulBound)
				from.NextSkillTime = Core.TickCount + 1000;
			else if (from is PlayerMobile && ((PlayerMobile)from).Troubadour())
				from.NextSkillTime = Core.TickCount + 4000;
			else 
				from.NextSkillTime = Core.TickCount + 6000;
		}

		public class DiscordanceInfo
		{
			public Mobile m_From;
			public Mobile m_Creature;
			public DateTime m_EndTime;
			public bool m_Ending;
			public Timer m_Timer;
			public int m_Effect;
			public ArrayList m_Mods;

			public DiscordanceInfo( Mobile from, Mobile creature, int effect, ArrayList mods )
			{
				m_From = from;
				m_Creature = creature;
				m_EndTime = DateTime.UtcNow;
				m_Ending = false;
				m_Effect = effect;
				m_Mods = mods;

				Apply();
			}

			public void Apply()
			{
				for ( int i = 0; i < m_Mods.Count; ++i )
				{
					object mod = m_Mods[i];

					if ( mod is ResistanceMod )
						m_Creature.AddResistanceMod( (ResistanceMod) mod );
					else if ( mod is StatMod )
						m_Creature.AddStatMod( (StatMod) mod );
					else if ( mod is SkillMod )
						m_Creature.AddSkillMod( (SkillMod) mod );
				}
				
				if (m_Creature is BaseCreature)
					((BaseCreature)m_Creature).IsDiscorded = true;
			}

			public void Clear()
			{
				for ( int i = 0; i < m_Mods.Count; ++i )
				{
					object mod = m_Mods[i];

					if ( mod is ResistanceMod )
						m_Creature.RemoveResistanceMod( (ResistanceMod) mod );
					else if ( mod is StatMod )
						m_Creature.RemoveStatMod( ((StatMod) mod).Name );
					else if ( mod is SkillMod )
						m_Creature.RemoveSkillMod( (SkillMod) mod );
				}

				if (m_Creature is BaseCreature)
					((BaseCreature)m_Creature).IsDiscorded = false;
			}
		}

		public static Hashtable m_Table = new Hashtable();

		public static bool GetEffect( Mobile targ, ref int effect )
		{
			DiscordanceInfo info = m_Table[targ] as DiscordanceInfo;

			if ( info == null )
				return false;

			effect = info.m_Effect;
			return true;
		}

		public static bool IsDiscorded(Mobile targ)
		{
			if (targ == null || targ.Deleted)
				return false;
			
			if (targ is BaseCreature && ((BaseCreature)targ).IsDiscorded)
				return true;

			if (!(targ is BaseCreature))
			{
				DiscordanceInfo info = m_Table[targ] as DiscordanceInfo;

				if ( info != null || m_Table.Contains( targ ))
					return true;
			}

			return false;
		}

		public static void ProcessDiscordance( DiscordanceInfo info )
		{
			Mobile from = info.m_From;
			Mobile targ = info.m_Creature;
			bool ends = false;

			// According to uoherald bard must remain alive, visible, and 
			// within range of the target or the effect ends in 15 seconds.
			if ( !targ.Alive || targ.Deleted || !from.Alive || from.Hidden || from is TrainingElemental1 || from is TrainingElemental)
				ends = true;
			else
			{
				int range = (int) targ.GetDistanceToSqrt( from );
				int maxRange = BaseInstrument.GetBardRange( from, SkillName.Discordance );

				if ( from.Map != targ.Map || range > maxRange )
					ends = true;
			}

			if ( ends && info.m_Ending && info.m_EndTime < DateTime.UtcNow )
			{
				if ( info.m_Timer != null )
					info.m_Timer.Stop();

				info.Clear();
				m_Table.Remove( targ );
			}
			else
			{
				if ( ends && !info.m_Ending )
				{
					info.m_Ending = true;
					if (targ is TrainingElemental1 || targ is TrainingElemental)
						info.m_EndTime = DateTime.UtcNow + TimeSpan.FromSeconds( 1 );
					else if (from is PlayerMobile && ((PlayerMobile)from).Troubadour())
						info.m_EndTime = DateTime.UtcNow + TimeSpan.FromSeconds( 20.0 );
					else
						info.m_EndTime = DateTime.UtcNow + TimeSpan.FromSeconds( 15.0 );
				}
				else if ( !ends )
				{
					info.m_Ending = false;
					info.m_EndTime = DateTime.UtcNow;
				}

				targ.FixedEffect( 0x376A, 1, 32 );
			}
		}

		public class DiscordanceTarget : Target
		{
			private BaseInstrument m_Instrument;

			public DiscordanceTarget( Mobile from, BaseInstrument inst ) : base( BaseInstrument.GetBardRange( from, SkillName.Discordance ), false, TargetFlags.None )
			{
				m_Instrument = inst;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() )
				{
					double odds = (from.Skills[SkillName.Musicianship].Value + from.Skills[SkillName.Stealth].Value / 500) + (from.Dex/500);
					if (odds > 0.95)
						odds = 0.95;

					if (Utility.RandomDouble() > odds)
					{
						from.RevealingAction();
						from.SendMessage("[testing] odds of staying hidden was " + (int)(odds*100));
					}
				}
				else
					from.RevealingAction();
					
				from.NextSkillTime = Core.TickCount + 1000;

				if ( m_Instrument.Parent != from && !m_Instrument.IsChildOf( from.Backpack ) )
				{
					from.SendLocalizedMessage( 1062488 ); // The instrument you are trying to play is no longer in your backpack!
				}
				else if ( target is Mobile )
				{
					Mobile targ = (Mobile)target;

					if ( (targ is BaseCreature && !from.CanBeHarmful( targ, false ) && ((BaseCreature)targ).ControlMaster != from) )
					{
						from.SendLocalizedMessage( 1049535 ); // A song of discord would have no effect on that.
					}
					else if ( IsDiscorded(targ) && !(targ is TrainingElemental1)) //Already discorded
					{
						from.SendLocalizedMessage( 1049537 );// Your target is already in discord.
					}
					else if ( IsDiscorded(targ) && targ is TrainingElemental1 && from is PlayerMobile && ((PlayerMobile)from).SoulBound) //Already discorded
					{
						m_Table.Remove( targ );
						((BaseCreature)targ).IsDiscorded = false;
						from.SendMessage("You remove the discord from this beast.");
					}
					else if ( !targ.Player )
					{
						ProcessDiscordance(from, targ);
					}
					if (targ == from && targ is PlayerMobile && ((PlayerMobile)from).Troubadour() && ((Mobile)targ).Map != null )
					{

						Region reg = Region.Find( from.Location, from.Map );
						if (reg.IsPartOf( typeof( PublicRegion ) ) || reg.IsPartOf( typeof( TownRegion ) ))
						{
							from.SendMessage("You can't do that here.");
							return;
						}

						int range = BaseInstrument.GetBardRange( from, SkillName.Discordance );
						
						double immunetest = ( (from.Skills[SkillName.Musicianship].Value / 250) + (from.Dex/350) ) /2;
						if (immunetest > 0.50)
							immunetest = 0.50;
						
						bool onesuccess = false;
						
						foreach ( Mobile m in from.GetMobilesInRange( range ) )
						{
							BaseHouse house = BaseHouse.FindHouseAt( m );
							
							if ( m == from || !from.CanBeHarmful ( m, false ) || !(m is BaseCreature) || m.Map == null || !from.CanSee( m ) )
								continue;
									
							if ( m is BaseCreature && ((BaseCreature)m).BardImmune && !(Utility.RandomDouble() < immunetest))
							{
								((BaseCreature)m).Combatant = from;
								continue;
							}
							
							if ( (m is BaseCreature && ((BaseCreature)m).IsHitchStabled) || house != null || SkillHandlers.Discordance.IsDiscorded(m))
								continue;
							
							double diff = (m_Instrument.GetDifficultyFor(from, m ) * 1.15) - 5.0;
							
							if ( diff > (from.Skills[SkillName.Musicianship].Value /3) )
							{
								if (from.CheckTargetSkill( SkillName.Discordance, m, diff-25.0, diff+25.0 ) )
								{
									ProcessDiscordance(from, m, false);
									onesuccess = true;
									//m.Say("[Added to area target list]"); debug
								}
								else if (Utility.RandomBool()  && ((BaseCreature)m).Combatant == null)
									((BaseCreature)m).Combatant = from;

							}
							else 
							{
								ProcessDiscordance(from, m, false);
								onesuccess = true;
								//m.Say("[Added to area target list]");
							}
						}		
						if (onesuccess)
						{
							m_Instrument.PlayInstrumentWell( from );
							m_Instrument.ConsumeUse( from );
							from.SendMessage("You put the area in discord!");
							from.NextSkillTime = Core.TickCount + (13 * 1000);
						}
						else
						{
							from.SendMessage("Your music cannot influence any creatures here.");
							from.NextSkillTime = Core.TickCount + (5 * 1000);
						}

					}
					
					else
					{
						m_Instrument.PlayInstrumentBadly( from );
					}
				}
				else
				{
					from.SendLocalizedMessage( 1049535 ); // A song of discord would have no effect on that.
				}
			}

			private void ProcessDiscordance( Mobile from, Mobile targ)
			{
				ProcessDiscordance(from, targ, true);
			}

			private void ProcessDiscordance( Mobile from, Mobile targ, bool msg)
			{
						double diff = m_Instrument.GetDifficultyFor(from, targ ) - 10.0;
						
						if (from is PlayerMobile && ((PlayerMobile)from).Troubadour() )
							diff /= 1.25;
						
						double music = from.Skills[SkillName.Musicianship].Value;
						Region reg = Region.Find( targ.Location, targ.Map );

						if ( music > 100.0 )
							diff -= (music - 100.0) * 0.5;

						if ( !BaseInstrument.CheckMusicianship( from ) )
						{
							if (msg)
								from.SendLocalizedMessage( 500612 ); // You play poorly, and there is no effect.
							m_Instrument.PlayInstrumentBadly( from );
							m_Instrument.ConsumeUse( from );
						}
						else if ( from.CheckTargetSkill( SkillName.Discordance, targ, diff-25.0, diff+25.0 ) )
						{
							if ( reg.IsPartOf( "the Basement" ) && ( targ is TrainingElemental1 || targ is TrainingElemental) )
								from.CheckTargetSkill( SkillName.Discordance, targ, 0, 120 );

							if (msg)
								from.SendLocalizedMessage( 1049539 ); // You play the song surpressing your targets strength
							m_Instrument.PlayInstrumentWell( from );
							m_Instrument.ConsumeUse( from );

							ArrayList mods = new ArrayList();

							mods.Add( new ResistanceMod( ResistanceType.Physical, Discordance.ReduceValue(from, targ, from.GetResistance(ResistanceType.Physical)) ) );
							mods.Add( new ResistanceMod( ResistanceType.Fire, Discordance.ReduceValue(from, targ, from.GetResistance(ResistanceType.Fire)) ) );
							mods.Add( new ResistanceMod( ResistanceType.Cold, Discordance.ReduceValue(from, targ, from.GetResistance(ResistanceType.Cold)) ) );
							mods.Add( new ResistanceMod( ResistanceType.Poison, Discordance.ReduceValue(from, targ, from.GetResistance(ResistanceType.Poison)) ) );
							mods.Add( new ResistanceMod( ResistanceType.Energy, Discordance.ReduceValue(from, targ, from.GetResistance(ResistanceType.Energy)) ) );

							mods.Add( new StatMod( StatType.Str, "DiscordanceStr", Discordance.ReduceValue(from, targ, targ.RawStr), TimeSpan.Zero ) );
							mods.Add( new StatMod( StatType.Int, "DiscordanceInt", Discordance.ReduceValue(from, targ, targ.RawInt), TimeSpan.Zero ) );
							mods.Add( new StatMod( StatType.Dex, "DiscordanceDex", Discordance.ReduceValue(from, targ, targ.RawDex), TimeSpan.Zero ) );

							for ( int i = 0; i < targ.Skills.Length; ++i )
							{
								if ( targ.Skills[i].Value > 0 )
									mods.Add( new DefaultSkillMod( (SkillName)i, true, targ.Skills[i].Value *  Discordance.Scalar(Discordance.Effect(from, targ)) ) );
							}
							DiscordanceInfo info = new DiscordanceInfo( from, targ, Math.Abs(  Discordance.Effect(from, targ) ), mods );
							info.m_Timer = Timer.DelayCall<DiscordanceInfo>( TimeSpan.Zero, TimeSpan.FromSeconds( 1.25 ), new TimerStateCallback<DiscordanceInfo>( Server.SkillHandlers.Discordance.ProcessDiscordance ), info );

						}
						else
						{
							if (msg)
								from.SendLocalizedMessage( 1049540 );// You fail to disrupt your target
							m_Instrument.PlayInstrumentBadly( from );
							m_Instrument.ConsumeUse( from );
						}

								if (reg.IsPartOf( "the Basement" ) && from is PlayerMobile && ((PlayerMobile)from).SoulBound)
									from.NextSkillTime = Core.TickCount + 1000;
								else if (from is PlayerMobile && ((PlayerMobile)from).Troubadour())
									from.NextSkillTime = Core.TickCount + 5000;
								else 
									from.NextSkillTime = Core.TickCount + (12 * 1000);
			
			
			
			
			}
		}
	}
}
