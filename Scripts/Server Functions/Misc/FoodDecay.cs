using System;
using Server.Network;
using Server;
using Server.Mobiles;

namespace Server.Misc
{
	public class FoodDecayTimer : Timer
	{
		public static void Initialize()
		{
			new FoodDecayTimer().Start();
		}

		public FoodDecayTimer() : base( TimeSpan.FromMinutes( 7 ), TimeSpan.FromMinutes( 10 ) )
		{
			Priority = TimerPriority.OneMinute;
		}

		protected override void OnTick()
		{
			FoodDecay();			
		}

		public static void FoodDecay()
		{
			foreach ( NetState state in NetState.Instances )
			{
				HungerDecay( state.Mobile );
				ThirstDecay( state.Mobile );
			}
		}

		public static void HungerDecay( Mobile m )
		{
			if ( m != null  )
			{
				if ( m is PlayerMobile && !Server.Commands.AFK.m_AFK.Contains(m.Serial.Value) && m.AccessLevel == AccessLevel.Player && m.Alive )
				{
					if ( m.Skills[SkillName.Camping].Value <= Utility.RandomMinMax( 1, 400 ) )
					{
						if ( m.Hunger >= 1 )
						{
							if ( (m.Direction & Direction.Running) != 0 )
								m.Hunger -= Utility.RandomMinMax(1,3);
							else 
								m.Hunger --;
								
							if (m is PlayerMobile && ((PlayerMobile)m).THC > 0 && (Utility.RandomDouble() < (((PlayerMobile)m).THC / 100) ))
								m.Hunger -= Utility.RandomMinMax(1,2);
								
							if (m.Hunger < 0)
								m.Hunger = 0;
								
							if ( m.Hunger < 5 ){ m.SendMessage( "You are extremely hungry." ); m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "I am extremely hungry."); }
							else if ( m.Hunger < 10 ){ m.SendMessage( "You are getting very hungry." ); m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "I am getting very hungry."); }
						}	
						else
						{
							if ( m.Hits > 5 )
								m.Hits -= 5;
							if ( m.Mana > 2 )
								m.Mana -= 2;

							m.SendMessage( "You are starving to death!" );
							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "I am starving to death!");
						}
					}
				}
				else if ( m is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)m;

					if ( bc.Controlled && m.Hunger >= 1 )
					{
						m.Hunger -= 1;
					}
				}
			}
		}

		public static void ThirstDecay( Mobile m )
		{
			if ( m != null )
			{
				if ( m is PlayerMobile && !Server.Commands.AFK.m_AFK.Contains(m.Serial.Value) && m.AccessLevel == AccessLevel.Player && m.Alive)
				{
					if ( m.Skills[SkillName.Camping].Value >= Utility.RandomMinMax( 1, 400 ) ){}
					else
					{
						if ( m.Thirst >= 1 )
						{
							if ( (m.Direction & Direction.Running) != 0 )
								m.Thirst -= Utility.RandomMinMax(2,3);
							else if ( m.Direction != 0 )
								m.Thirst -= Utility.RandomMinMax(1,2);
							else
								m.Thirst --;

							if (m.Thirst < 0)
								m.Thirst = 0;
								
							if ( m.Thirst < 5 ){ m.SendMessage( "You are extremely thirsty." ); m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "I am extremely thirsty."); }
							else if ( m.Thirst < 10 ){ m.SendMessage( "You are getting thirsty." ); m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "I am getting thirsty."); }
						}
						else
						{
							if ( m.Stam > 5 )
								m.Stam -= 5;
							if ( m.Mana > 2 )
								m.Mana -= 2;

							m.SendMessage( "You are exhausted from thirst" );
							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "I am exhausted from thirst!");
						}
					}
				}
				else if ( m is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)m;

					if ( bc.Controlled && m.Thirst >= 1 )
					{
						m.Thirst -= 1;
					}
				}
			}
		}
	}
}
