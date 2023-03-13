using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.Mystic;
using Server.Commands;

namespace Server.Scripts.Commands
{
	public class CastMonkSpells
	{
		public static void Initialize()
		{
            Properties.Initialize();

			Register( "AstralProjection", AccessLevel.Player, new CommandEventHandler( AstralProjection_OnCommand ) );

			Register( "AstralTravel", AccessLevel.Player, new CommandEventHandler( AstralTravel_OnCommand ) );

			Register( "CreateRobe", AccessLevel.Player, new CommandEventHandler( CreateRobe_OnCommand ) );

			Register( "GentleTouch", AccessLevel.Player, new CommandEventHandler( GentleTouch_OnCommand ) );

			Register( "Leap", AccessLevel.Player, new CommandEventHandler( Leap_OnCommand ) );

			Register( "PsionicBlast", AccessLevel.Player, new CommandEventHandler( PsionicBlast_OnCommand ) );

			Register( "PsychicWall", AccessLevel.Player, new CommandEventHandler( PsychicWall_OnCommand ) );

			Register( "PurityOfBody", AccessLevel.Player, new CommandEventHandler( PurityOfBody_OnCommand ) );

			Register( "QuiveringPalm", AccessLevel.Player, new CommandEventHandler( QuiveringPalm_OnCommand ) );

			Register( "WindRunner", AccessLevel.Player, new CommandEventHandler( WindRunner_OnCommand ) );
		}

	    public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );

			return ( book != null && book.HasSpell( spellID ) );
		}

		[Usage( "AstralProjection" )]
		[Description( "Casts AstralProjection" )]
		public static void AstralProjection_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 250 ) )
					{
					new AstralProjection( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
                    }
        }

		[Usage( "AstralTravel" )]
		[Description( "Casts AstralTravel" )]
		public static void AstralTravel_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;
			
         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 251 ) )
					{
					new AstralTravel( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "CreateRobe" )]
		[Description( "Casts CreateRobe" )]
		public static void CreateRobe_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 252 ) )
					{
					new CreateRobe( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "GentleTouch" )]
		[Description( "Casts GentleTouch" )]
		public static void GentleTouch_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 253 ) )
					{
					new GentleTouch( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "Leap" )]
		[Description( "Casts Leap" )]
		public static void Leap_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 254 ) )
					{
					new Leap( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "PsionicBlast" )]
		[Description( "Casts PsionicBlast" )]
		public static void PsionicBlast_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 255 ) )
					{
					new  PsionicBlast( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "PsychicWall" )]
		[Description( "Casts PsychicWall" )]
		public static void PsychicWall_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 256 ) )
					{
					new PsychicWall( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "PurityOfBody" )]
		[Description( "Casts PurityOfBody" )]
		public static void PurityOfBody_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 257 ) )
					{
					new PurityOfBody( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "QuiveringPalm" )]
		[Description( "Casts QuiveringPalm" )]
		public static void QuiveringPalm_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 258 ) )
					{
					new QuiveringPalm( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "WindRunner" )]
		[Description( "Casts WindRunner" )]
		public static void WindRunner_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 259 ) )
					{
					new WindRunner( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}
	}
}
