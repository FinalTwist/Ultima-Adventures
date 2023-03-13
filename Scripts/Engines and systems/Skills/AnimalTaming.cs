using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

using Server.Spells;
using Server.Misc;

namespace Server.SkillHandlers
{
	public class AnimalTaming
	{
		private static Hashtable m_BeingTamed = new Hashtable();

		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.AnimalTaming].Callback = new SkillUseCallback( OnUse );
		}

		private static bool m_DisableMessage;

		public static bool DisableMessage
		{
			get{ return m_DisableMessage; }
			set{ m_DisableMessage = value; }
		}

		public static TimeSpan OnUse( Mobile m )
		{
			m.RevealingAction();

			m.Target = new InternalTarget();
			m.RevealingAction();

			if ( !m_DisableMessage )
				m.SendLocalizedMessage( 502789 ); // Tame which animal?

			return TimeSpan.FromHours( 6.0 );
		}

		public static bool CheckMastery( Mobile tamer, BaseCreature creature )
		{
			BaseCreature familiar = (BaseCreature)Spells.Necromancy.SummonFamiliarSpell.Table[tamer];

			if ( familiar != null && !familiar.Deleted && familiar is DarkWolfFamiliar )
			{
				if ( creature is DireWolf || creature is GreyWolf || creature is TimberWolf || creature is WhiteWolf || creature is MysticalFox )
					return true;
			}

			return false;
		}

		public static bool MustBeSubdued( BaseCreature bc )
		{
            if (bc.Owners.Count > 0) { return false; } //Checks to see if the animal has been tamed before
			return bc.SubdueBeforeTame && (bc.Hits > (bc.HitsMax / 10));
		}

		public static void ScaleStats( BaseCreature bc, double scalar )
		{
			if ( bc.RawStr > 0 )
				bc.RawStr = (int)Math.Max( 1, bc.RawStr * scalar );

			if ( bc.RawDex > 0 )
				bc.RawDex = (int)Math.Max( 1, bc.RawDex * scalar );

			if ( bc.RawInt > 0 )
				bc.RawInt = (int)Math.Max( 1, bc.RawInt * scalar );

			if ( bc.HitsMaxSeed > 0 )
			{
				bc.HitsMaxSeed = (int)Math.Max( 1, bc.HitsMaxSeed * scalar );
				bc.Hits = bc.Hits;
				}

			if ( bc.StamMaxSeed > 0 )
			{
				bc.StamMaxSeed = (int)Math.Max( 1, bc.StamMaxSeed * scalar );
				bc.Stam = bc.Stam;
			}
		}

		public static void ScaleSkills( BaseCreature bc, double scalar )
		{
			ScaleSkills( bc, scalar, scalar );
		}

		public static void ScaleSkills( BaseCreature bc, double scalar, double capScalar )
		{
			for ( int i = 0; i < bc.Skills.Length; ++i )
			{
				bc.Skills[i].Base *= scalar;

				bc.Skills[i].Cap = Math.Max( 100.0, bc.Skills[i].Cap * capScalar );

				if ( bc.Skills[i].Base > bc.Skills[i].Cap )
				{
					bc.Skills[i].Cap = bc.Skills[i].Base;
				}
			}
		}

		private class InternalTarget : Target
		{
			private bool m_SetSkillTime = true;

			public InternalTarget() :  base ( 2, false, TargetFlags.None )
			{
			}

			protected override void OnTargetFinish( Mobile from )
			{
				if ( m_SetSkillTime )
					from.NextSkillTime = Core.TickCount;
			}

			public virtual void ResetPacify( object obj )
			{
				if( obj is BaseCreature )
				{
					((BaseCreature)obj).BardPacified = true;
				}
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				from.RevealingAction();

				if (from.Blessed)
				{
					from.PublicOverheadMessage( MessageType.Regular, 0x3B2, false, string.Format ( "You are unable to do this in this state."  ) );
					return;
				}
				if ( targeted is Mobile )
				{
					if ( targeted is BaseCreature )
					{
						BaseCreature creature = (BaseCreature)targeted;

						if ( !creature.Tamable )
						{
							creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1049655, from.NetState ); // That creature cannot be tamed.
						}
						else if ( creature.Controlled )
						{
							creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 502804, from.NetState ); // That animal looks tame already.
						}
						//else if ( from.Female && !creature.AllowFemaleTamer )
						//{
						//	creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1049653, from.NetState ); // That creature can only be tamed by males.
						//}
						//else if ( !from.Female && !creature.AllowMaleTamer )
						//{
						//	creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1049652, from.NetState ); // That creature can only be tamed by females.
						//}
						else if ( from.Followers + creature.ControlSlots > from.FollowersMax )
						{
							from.SendLocalizedMessage( 1049611 ); // You have too many followers to tame that creature.
						}
						else if ( creature.Owners.Count >= BaseCreature.MaxOwners && !creature.Owners.Contains( from ) )
						{
							creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1005615, from.NetState ); // This animal has had too many owners and is too upset for you to tame.
						}
						else if ( MustBeSubdued( creature ) )
						{
							creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1054025, from.NetState ); // You must subdue this creature before you can tame it!
						}
						else if ( CheckMastery( from, creature ) || from.Skills[SkillName.AnimalTaming].Value >= creature.MinTameSkill )
						{

							double angerodds = creature.MinTameSkill / from.Skills[SkillName.AnimalTaming].Value; // final

							if ( m_BeingTamed.Contains( targeted ) )
							{
								creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 502802, from.NetState ); // Someone else is already taming this.
							}
							else if ( creature.CanAngerOnTame && angerodds >= Utility.RandomDouble() && from.AccessLevel == AccessLevel.Player) // final
							{
								creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 502805, from.NetState ); // You seem to anger the beast!
								creature.PlaySound( creature.GetAngerSound() );
								creature.Direction = creature.GetDirectionTo( from );

								if( creature.BardPacified && Utility.RandomDouble() > .24)
								{
									Timer.DelayCall( TimeSpan.FromSeconds( 2.0 ), new TimerStateCallback( ResetPacify ), creature );
								}
								else
								{
									creature.BardEndTime = DateTime.UtcNow;
								}
		
								creature.BardPacified = false;

								creature.Move( creature.Direction );

								if ( from is PlayerMobile )
									creature.Combatant = from;
							}
							else
							{
								m_BeingTamed[targeted] = from;

								from.LocalOverheadMessage( MessageType.Emote, 0x59, 1010597 ); // You start to tame the creature.
								from.NonlocalOverheadMessage( MessageType.Emote, 0x59, 1010598 ); // *begins taming a creature.*

								new InternalTimer( from, creature, Utility.Random( 4, 6 ) ).Start();

								m_SetSkillTime = false;
							}
						}
						else
						{
							creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 502806, from.NetState ); // You have no chance of taming this creature.
							
							double diff = creature.MinTameSkill - from.Skills[SkillName.AnimalTaming].Value  ;
							if (diff <= 0.5)
								from.PublicOverheadMessage( MessageType.Regular, 0x3B2, false, string.Format ( "you can almost grasp this creature's habits"  ) );
							else if (diff <= 2.5)					
								from.PublicOverheadMessage( MessageType.Regular, 0x3B2, false, string.Format ( "you are close to being able to tame this"  ) );
							else if (diff <= 5)					
								from.PublicOverheadMessage( MessageType.Regular, 0x3B2, false, string.Format ( "you will be able to tame this with more effort"  ) );
							else if (diff <= 10)					
								from.PublicOverheadMessage( MessageType.Regular, 0x3B2, false, string.Format ( "you have a long ways to go before you can tame that"  ) );
							else if (diff > 15)					
								from.PublicOverheadMessage( MessageType.Regular, 0x3B2, false, string.Format ( "this creature is much too difficult for you now"  ) );

						}
					}
					else
					{
						((Mobile)targeted).PrivateOverheadMessage( MessageType.Regular, 0x3B2, 502469, from.NetState ); // That being cannot be tamed.
					}
				}
				else
				{
					from.SendLocalizedMessage( 502801 ); // You can't tame that!
				}
			}

			private class InternalTimer : Timer
			{
				private Mobile m_Tamer;
				private BaseCreature m_Creature;
				private int m_MaxCount;
				private int m_Count;
				private bool m_Paralyzed;
				private DateTime m_StartTime;

				public InternalTimer( Mobile tamer, BaseCreature creature, int count ) : base( TimeSpan.FromSeconds( 3.0 ), TimeSpan.FromSeconds( 3.0 ), count )
				{
					m_Tamer = tamer;
					m_Creature = creature;
					m_MaxCount = count;
					m_Paralyzed = creature.Paralyzed;
					m_StartTime = DateTime.UtcNow;
					Priority = TimerPriority.TwoFiftyMS;
				}

				protected override void OnTick()
				{
					m_Count++;

					DamageEntry de = m_Creature.FindMostRecentDamageEntry( false );
					bool alreadyOwned = m_Creature.Owners.Contains( m_Tamer );

					if ( !m_Tamer.InRange( m_Creature, 6 ) )
					{
						m_BeingTamed.Remove( m_Creature );
						m_Tamer.NextSkillTime = Core.TickCount;
						m_Creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 502795, m_Tamer.NetState ); // You are too far away to continue taming.
						Stop();
					}
					else if ( !m_Tamer.CheckAlive() )
					{
						m_BeingTamed.Remove( m_Creature );
						m_Tamer.NextSkillTime = Core.TickCount;
						m_Creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 502796, m_Tamer.NetState ); // You are dead, and cannot continue taming.
						Stop();
					}
					else if ( !m_Tamer.CanSee( m_Creature ) || !m_Tamer.InLOS( m_Creature ) || !CanPath() )
					{
						m_BeingTamed.Remove( m_Creature );
						m_Tamer.NextSkillTime = Core.TickCount;
						m_Creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1049654, m_Tamer.NetState ); // You do not have a clear path to the animal you are taming, and must cease your attempt.
						Stop();
					}
					else if ( !m_Creature.Tamable )
					{
						m_BeingTamed.Remove( m_Creature );
						m_Tamer.NextSkillTime = Core.TickCount;
						m_Creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1049655, m_Tamer.NetState ); // That creature cannot be tamed.
						Stop();
					}
					else if ( m_Creature.Controlled )
					{
						m_BeingTamed.Remove( m_Creature );
						m_Tamer.NextSkillTime = Core.TickCount;
						m_Creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 502804, m_Tamer.NetState ); // That animal looks tame already.
						Stop();
					}
					else if ( m_Creature.Owners.Count >= BaseCreature.MaxOwners && !m_Creature.Owners.Contains( m_Tamer ) )
					{
						m_BeingTamed.Remove( m_Creature );
						m_Tamer.NextSkillTime = Core.TickCount;
						m_Creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1005615, m_Tamer.NetState ); // This animal has had too many owners and is too upset for you to tame.
						Stop();
					}
					else if ( MustBeSubdued( m_Creature ) )
					{
						m_BeingTamed.Remove( m_Creature );
						m_Tamer.NextSkillTime = Core.TickCount;
						m_Creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1054025, m_Tamer.NetState ); // You must subdue this creature before you can tame it!
						Stop();
					}
					else if ( de != null && de.LastDamage > m_StartTime )
					{
						m_BeingTamed.Remove( m_Creature );
						m_Tamer.NextSkillTime = Core.TickCount;
						m_Creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 502794, m_Tamer.NetState ); // The animal is too angry to continue taming.
						Stop();
					}
					else if ( m_Count < m_MaxCount )
					{
						m_Tamer.RevealingAction();
		
						if (Utility.RandomDouble() < 0.85)	
						{
							switch ( Utility.Random( 3 ) )
							{
								case 0: m_Tamer.PublicOverheadMessage( MessageType.Regular, 0x3B2, Utility.Random( 502790, 4 ) ); break;
								case 1: m_Tamer.PublicOverheadMessage( MessageType.Regular, 0x3B2, Utility.Random( 1005608, 6 ) ); break;
								case 2: m_Tamer.PublicOverheadMessage( MessageType.Regular, 0x3B2, Utility.Random( 1010593, 4 ) ); break;
							}
						}
						else 
						{
							string speech = "";
							if (m_Tamer.Karma < 0)
							{
								switch ( Utility.Random( 13 ) )
								{
									case 0: speech = "Come here, you little mongrel."; break;
									case 1: speech = "Come on, tame already." ; break;
									case 2: speech = "Consider yourself lucky I don't hit TAB..."; break;
									case 3: speech = "I'm just going to rename you and kill you."; break;
									case 4: speech = "I bet the broker will pay well for you."; break;
									case 5: speech = "Come... kill my enemies and make me rich."; break;
									case 6: speech = "I'm too lazy to kill enemies myself, I need you to do it for me see?"; break;
									case 7: speech = "I was just joking when I asked you to travel with me."; break;
									case 8: speech = "If you don't give me a gain you'll pay."; break;
									case 9: speech = "If you don't tame now I'll starve you for making me wait."; break;
									case 10: speech = "You might be worth more in hides and meat."; break;
									case 11: speech = "My name's " + m_Tamer.Name + " and I'm your daddy."; break;
									case 12: speech = "You're the weakest example of " + m_BeingTamed + " I've ever seen."; break;
									
								}
							}
							else
							{
								switch ( Utility.Random( 13 ) )
								{
									case 0: speech =  "Take your time!  I want you to trust me."; break;
									case 1: speech = "I will find you a nice home to live in."; break;
									case 2: speech = "What a beautiful creature you are!"; break;
									case 3: speech = "Can you make that cute noise again?"; break;
									case 4: speech = "I will care for you and grow with you."; break;
									case 5: speech = "Together we will explore marvelous places!"; break;
									case 6: speech = "You and me, sitting on a tree... wait what?"; break;
									case 7: speech = "I bet your fur is soft... can I touch it?"; break;
									case 8: speech = "Consider coming with me, friend."; break;
									case 9: speech = "Out of all the creatures around me, I picked you my friend."; break;
									case 10: speech = "I will train you to become a better creature."; break;
									case 11: speech = "Oooh that's cute, you just licked your privates."; break;
									case 12: speech = "Hey there buddy, I'm " + m_Tamer.Name + " and I think you're really cute."; break;
									
								}
							}
							
							
												
							if (m_Tamer is PlayerMobile && ((PlayerMobile)m_Tamer).BAC > 0 && Utility.RandomDouble() < ((double)((PlayerMobile)m_Tamer).BAC/200)) //is drunk!
							{
								// lets have fun
								string[] said = speech.Split(' ');
								speech = "";

								for( int i = 0; i < said.Length; i++ )
								{
									if (Utility.RandomDouble() > 0.85)
									{
										string junk = "";
										switch (Utility.Random(7))
										{
											case 0: speech += "ssmbb" + " "; break; //Sticks Stones May Break Bones
											case 1: speech += "bwchm" + " "; break; //But words can't hurt me
											case 2: speech += "umgat" + " "; break; //Unless my giant angry tames
											case 3: speech += "mmcfi" + " "; break; //Mistake my commands for insults
											case 4: speech += "taemg" + " "; break; //turn and eat my gear
											case 5: speech += "aqkmb" + " "; break; //and quickly kill me before
											case 6: speech += "istmh" + " "; break; //I stop their mangy hides
										}
									}
									else
										speech += said[Utility.Random(said.Length)]+ " ";
								}
							}
							
							m_Tamer.PublicOverheadMessage( MessageType.Regular, 0x3B2, false, speech );
							
							//m_BeingTamed.Remove( m_Creature );
							//m_Tamer.NextSkillTime = Core.TickCount;
							//Stop();
							//return;
								
						}

						if ( !alreadyOwned ) // Passively check animal lore for gain // FInal added taming too!
						{
							switch ( Utility.Random( 2 ) )
							{
								case 0: m_Tamer.CheckTargetSkill( SkillName.AnimalTaming, m_Creature, m_Creature.MinTameSkill - 25, m_Creature.MinTameSkill + 25 ); break;
								case 1: break;
							}
							m_Tamer.CheckTargetSkill( SkillName.AnimalLore, m_Creature, m_Creature.MinTameSkill - 25, m_Creature.MinTameSkill + 25 ); //+++
						}

						if ( m_Creature.Paralyzed )
							m_Paralyzed = true;
					}
					else
					{
						m_Tamer.RevealingAction();
						m_Tamer.NextSkillTime = Core.TickCount;
						m_BeingTamed.Remove( m_Creature );

						double minSkill = m_Creature.MinTameSkill + (m_Creature.Owners.Count * 6.0);

						if ( minSkill > 24.9 && CheckMastery( m_Tamer, m_Creature ) )
							minSkill = 0; // 50% at 0.0?

						if ( m_Creature.Paralyzed )
							m_Paralyzed = true;

						if ( !alreadyOwned ) // Passively check animal lore for gain // FInal added taming too!
						{
							switch ( Utility.Random( 2 ) )
							{
								case 0: m_Tamer.CheckTargetSkill( SkillName.AnimalTaming, m_Creature, minSkill - 25, minSkill + 25 ); break;
								case 1: break;
							}
							m_Tamer.CheckTargetSkill( SkillName.AnimalLore, m_Creature, minSkill - 25, minSkill + 25); //+++
						}


						if ( CheckMastery( m_Tamer, m_Creature ) || alreadyOwned || m_Tamer.CheckTargetSkill( SkillName.AnimalTaming, m_Creature, minSkill - 25.0, minSkill + 25.0 ) )
						{
							if ( m_Creature.Owners.Count == 0 ) // First tame
							{
								if ( m_Paralyzed )
									ScaleSkills( m_Creature, 0.65 ); // 86% of original skills if they were paralyzed during the taming
								else
									ScaleSkills( m_Creature, 0.80 ); // 90% of original skills

								if ( m_Creature.StatLossAfterTame )
									ScaleStats( m_Creature, 0.50 );
							}

							if ( alreadyOwned )
							{
								m_Tamer.SendLocalizedMessage( 502797 ); // That wasn't even challenging.
							}
							else
							{
								m_Creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 502799, m_Tamer.NetState ); // It seems to accept you as master.
								m_Creature.Owners.Add( m_Tamer );
							}

							if (m_Creature.Title != null)
							{
								if ( m_Creature.Title.Contains("*Enraged*") || m_Creature.Title.Contains("*Righteous*") )
								{
									m_Creature.RawStr /= 2;
									m_Creature.RawDex /= 2;
									m_Creature.RawInt /= 2;
									m_Creature.Hue = -1;
									m_Creature.HitsMaxSeed /= 2;
									m_Creature.Hits = m_Creature.HitsMaxSeed;
									m_Creature.AIFullSpeedActive = false;
								}
							}

							if (m_Creature.Map != null)
							{
								int Heat = MyServerSettings.GetDifficultyLevel( m_Creature.Location, m_Creature.Map );
								if (Heat > 0 )
									Server.Mobiles.BaseCreature.BeefDown(m_Creature, Heat); //final beefdown to adjust for beefup in dungeons
							}
							
							m_Creature.SetControlMaster( m_Tamer );

							m_Creature.RangeHome = -1;
							m_Creature.Home = new Point3D(0, 0, 0);

							m_Creature.IsBonded = false;

						}
						else
						{
							m_Creature.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 502798, m_Tamer.NetState ); // You fail to tame the creature.
						}
					}
				}

				private bool CanPath()
				{
					IPoint3D p = m_Tamer as IPoint3D;

					if ( p == null )
						return false;

					if( m_Creature.InRange( new Point3D( p ), 1 ) )
						return true;

					MovementPath path = new MovementPath( m_Creature, new Point3D( p ) );
					return path.Success;
				}
			}
		}
	}
}
