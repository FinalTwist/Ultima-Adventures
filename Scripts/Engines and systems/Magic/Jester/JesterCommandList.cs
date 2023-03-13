using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.Jester;
using Server.Commands;

namespace Server.Scripts.Commands
{
	public class CastJesterSpells
	{
		public static void Initialize()
		{
            Properties.Initialize();

			Register( "CanOfSnakes", AccessLevel.Player, new CommandEventHandler( CanOfSnakes_OnCommand ) );

			Register( "Clowns", AccessLevel.Player, new CommandEventHandler( Clowns_OnCommand ) );

			Register( "FlowerPower", AccessLevel.Player, new CommandEventHandler( FlowerPower_OnCommand ) );

			Register( "Hilarity", AccessLevel.Player, new CommandEventHandler( Hilarity_OnCommand ) );

			Register( "Insult", AccessLevel.Player, new CommandEventHandler( Insult_OnCommand ) );

			Register( "JumpAround", AccessLevel.Player, new CommandEventHandler( JumpAround_OnCommand ) );

			Register( "PoppingBalloon", AccessLevel.Player, new CommandEventHandler( PoppingBalloon_OnCommand ) );

			Register( "RabbitInAHat", AccessLevel.Player, new CommandEventHandler( RabbitInAHat_OnCommand ) );

			Register( "SeltzerBottle", AccessLevel.Player, new CommandEventHandler( SeltzerBottle_OnCommand ) );

			Register( "SurpriseGift", AccessLevel.Player, new CommandEventHandler( SurpriseGift_OnCommand ) );
		}

	    public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "CanOfSnakes" )]
		[Description( "Casts Can Of Nuts" )]
		public static void CanOfSnakes_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !Multis.DesignContext.Check( e.Mobile ) )
				return; // They are customizing

			if ( Server.Misc.GetPlayerInfo.isJester( from ) )
			{
				new CanOfSnakes( e.Mobile, null ).Cast(); 
			}
			else
			{
				from.SendMessage( "You are not a jester!" );
			}
        }

		[Usage( "Clowns" )]
		[Description( "Casts Clowns" )]
		public static void Clowns_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !Multis.DesignContext.Check( e.Mobile ) )
				return; // They are customizing

			if ( Server.Misc.GetPlayerInfo.isJester( from ) )
			{
				new Clowns( e.Mobile, null ).Cast(); 
			}
			else
			{
				from.SendMessage( "You are not a jester!" );
			}
		}

		[Usage( "FlowerPower" )]
		[Description( "Casts Flower Power" )]
		public static void FlowerPower_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !Multis.DesignContext.Check( e.Mobile ) )
				return; // They are customizing

			if ( Server.Misc.GetPlayerInfo.isJester( from ) )
			{
				new FlowerPower( e.Mobile, null ).Cast(); 
			}
			else
			{
				from.SendMessage( "You are not a jester!" );
			}
		}

		[Usage( "Hilarity" )]
		[Description( "Casts Hilarity" )]
		public static void Hilarity_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !Multis.DesignContext.Check( e.Mobile ) )
				return; // They are customizing

			if ( Server.Misc.GetPlayerInfo.isJester( from ) )
			{
				new Hilarity( e.Mobile, null ).Cast(); 
			}
			else
			{
				from.SendMessage( "You are not a jester!" );
			}
		}

		[Usage( "Insult" )]
		[Description( "Casts Insult" )]
		public static void Insult_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !Multis.DesignContext.Check( e.Mobile ) )
				return; // They are customizing

			if ( Server.Misc.GetPlayerInfo.isJester( from ) )
			{
				new Insult( e.Mobile, null ).Cast(); 
			}
			else
			{
				from.SendMessage( "You are not a jester!" );
			}
		}

		[Usage( "JumpAround" )]
		[Description( "Casts Jump Around" )]
		public static void JumpAround_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !Multis.DesignContext.Check( e.Mobile ) )
				return; // They are customizing

			if ( Server.Misc.GetPlayerInfo.isJester( from ) )
			{
				new JumpAround( e.Mobile, null ).Cast(); 
			}
			else
			{
				from.SendMessage( "You are not a jester!" );
			}
		}

		[Usage( "PoppingBalloon" )]
		[Description( "Casts Popping Balloon" )]
		public static void PoppingBalloon_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !Multis.DesignContext.Check( e.Mobile ) )
				return; // They are customizing

			if ( Server.Misc.GetPlayerInfo.isJester( from ) )
			{
				new PoppingBalloon( e.Mobile, null ).Cast(); 
			}
			else
			{
				from.SendMessage( "You are not a jester!" );
			}
		}

		[Usage( "RabbitInAHat" )]
		[Description( "Casts Rabbit In A Hat" )]
		public static void RabbitInAHat_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !Multis.DesignContext.Check( e.Mobile ) )
				return; // They are customizing

			if ( Server.Misc.GetPlayerInfo.isJester( from ) )
			{
				new RabbitInAHat( e.Mobile, null ).Cast(); 
			}
			else
			{
				from.SendMessage( "You are not a jester!" );
			}
		}

		[Usage( "SeltzerBottle" )]
		[Description( "Casts Seltzer Bottle" )]
		public static void SeltzerBottle_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !Multis.DesignContext.Check( e.Mobile ) )
				return; // They are customizing

			if ( Server.Misc.GetPlayerInfo.isJester( from ) )
			{
				new SeltzerBottle( e.Mobile, null ).Cast(); 
			}
			else
			{
				from.SendMessage( "You are not a jester!" );
			}
		}

		[Usage( "SurpriseGift" )]
		[Description( "Casts Surprise Gift" )]
		public static void SurpriseGift_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !Multis.DesignContext.Check( e.Mobile ) )
				return; // They are customizing

			if ( Server.Misc.GetPlayerInfo.isJester( from ) )
			{
				new SurpriseGift( e.Mobile, null ).Cast(); 
			}
			else
			{
				from.SendMessage( "You are not a jester!" );
			}
		}
	}
}
