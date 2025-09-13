using System;
using Server.Network;
using Server.Mobiles;
using Server;

namespace Server.Misc
{
	// Create the timer that monitors the current state of thirst
	public class StamDecayTimer : Timer
	{
		public static void Initialize()
		{
			new StamDecayTimer().Start();
		}
		// Based on the same timespan used in RegenRates.cs
		public StamDecayTimer() : base( TimeSpan.FromSeconds( 7 ), TimeSpan.FromSeconds( 7 ) )
		{
			Priority = TimerPriority.OneSecond;
		}
		
		protected override void OnTick()
		{
			StamDecay();
		}
		// Check the NetState and call the decaying function
		public static void StamDecay()
		{
			foreach ( NetState state in NetState.Instances )
			{
				StamDecaying( state.Mobile );
			}
		}
		
		// Check thirst level if below the value set take away 1 point of stam
		public static void StamDecaying( Mobile m )
		{
			if ( m is PlayerMobile )
			{
				if ( m != null && m.Thirst < 5 && m.Stam > 25 )
				{
					switch (m.Thirst)
					{
						case 4: m.Stam -= 5; break;
						case 3: m.Stam -= 10; break;
						case 2: m.Stam -= 15; break;
						case 1: m.Stam -= 20; break;
						case 0:
						{
							m.Stam -= 25;
							m.SendMessage( "You are exhausted from thirst!" );
							break;
						}
					}
				}
				if ( m != null && m.Thirst < 5 && m.Mana > 10 )
				{
					switch (m.Thirst)
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
