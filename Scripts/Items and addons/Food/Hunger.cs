using System;
using Server;
using Server.Commands;
using Server.Items;
using Server.Network;
using Server.Targeting;
using CPA = Server.CommandPropertyAttribute;

namespace Server.Scripts.Commands
{
	public class MyHunger
	{
		public static void Initialize()
		{
			CommandSystem.Register ( "mhgr", AccessLevel.Player, new CommandEventHandler ( MyHunger_OnCommand ) );
			CommandSystem.Register ( "myhunger", AccessLevel.Player, new CommandEventHandler ( MyHunger_OnCommand ) );
		}
		public static void MyHunger_OnCommand( CommandEventArgs e )
		{
			int h = e.Mobile.Hunger; // Variable to hold the hunger value of the player
			// these values are taken from Food.cs and relate directly to the message
			// you get when you eat.
			if (h <= 0 )
				e.Mobile.SendMessage( "You are starving to death." );
			else if ( h <= 5 )
			       	e.Mobile.SendMessage( "You are extremely hungry." );
			else if ( h <= 10 )
				e.Mobile.SendMessage( "You are very hungry." );
			else if ( h <= 15 )
				e.Mobile.SendMessage( "You are slightly hungry." );
			else if ( h <= 19 )
				e.Mobile.SendMessage( "You are not really hungry." );
			else if ( h > 19 )
				e.Mobile.SendMessage( "You are quite full." );
			else
				e.Mobile.SendMessage( "Error: Please report this error: hunger not found." );

			int t = e.Mobile.Thirst; // Variable to hold the thirst value of the player
			// read the comments above to see where these values came from
			if ( t <= 0 )
				e.Mobile.SendMessage( "You are exhausted from thirst." );
			else if ( t <= 5 )
			       	e.Mobile.SendMessage( "You are extremely thirsty." );
			else if ( t <= 10 )
				e.Mobile.SendMessage( "You are very thirsty." );
			else if ( t <= 15 )
				e.Mobile.SendMessage( "You are slightly thirsty." );
			else if ( t <= 19 )
				e.Mobile.SendMessage( "You are not really thirsty." );
			else if ( t > 19 )
				e.Mobile.SendMessage( "You are not thirsty." );
			else
				e.Mobile.SendMessage( "Error: Please report this error: thirst not found." );
		}
	}
}
