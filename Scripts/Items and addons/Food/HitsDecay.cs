using System;
using Server.Network;
using Server;
using Server.Mobiles;

namespace Server.Misc
{
	// Create the timer that monitors the current state of hunger
	public class HitsDecayTimer : Timer
	{
		public static void Initialize()
		{
			new HitsDecayTimer().Start();
		}
		// Based on the same timespan used in RegenRates.cs
		public HitsDecayTimer() : base( TimeSpan.FromSeconds( 11 ), TimeSpan.FromSeconds( 11 ) )
		{
			Priority = TimerPriority.OneSecond;
		}
		
		protected override void OnTick()
		{
			HitsDecay();
		}
		// Check the NetState and call the decaying function
		public static void HitsDecay()
		{
			foreach ( NetState state in NetState.Instances )
			{
				HitsDecaying( state.Mobile );
			}
		}

		// Check hunger level if below the value set take away 1 hit
		public static void HitsDecaying( Mobile m )
		{
			if ( m is PlayerMobile )
			{
				if ( m != null && m.Hunger < 5 && m.Hits > 18 )
				{
					switch (m.Hunger)
					{
						case 4: m.Hits -= 5; break;
						case 3: m.Hits -= 8; break;
						case 2: m.Hits -= 11; break;
						case 1: m.Hits -= 14; break;
						case 0:
						{
							m.Hits -= 17;
							m.SendMessage( "You are starving to death!" );
							m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "I am starving to death!");
							break;
						}
					}
				}
				if ( m != null && m.Hunger < 5 && m.Mana > 10 )
				{
					switch (m.Hunger)
					{
						case 4: m.Mana -= 2; break;
						case 3: m.Mana -= 4; break;
						case 2: m.Mana -= 6; break;
						case 1: m.Mana -= 8; break;
						case 0: m.Mana -= 10; break;
					}
				}
			}
		}
	}
}
