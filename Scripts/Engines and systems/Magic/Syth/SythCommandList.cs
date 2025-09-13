using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.Syth;
using Server.Commands;

namespace Server.Scripts.Commands
{
	public class CastSythSpells
	{
		public static void Initialize()
		{
            Properties.Initialize();

			Register( "Psychokinesis", AccessLevel.Player, new CommandEventHandler( Psychokinesis_OnCommand ) );

			Register( "DeathGrip", AccessLevel.Player, new CommandEventHandler( DeathGrip_OnCommand ) );

			Register( "Projection", AccessLevel.Player, new CommandEventHandler( Projection_OnCommand ) );

			Register( "ThrowSword", AccessLevel.Player, new CommandEventHandler( ThrowSword_OnCommand ) );

			Register( "SythSpeed", AccessLevel.Player, new CommandEventHandler( SythSpeed_OnCommand ) );

			Register( "SythLightning", AccessLevel.Player, new CommandEventHandler( SythLightning_OnCommand ) );

			Register( "Absorption", AccessLevel.Player, new CommandEventHandler( Absorption_OnCommand ) );

			Register( "PsychicBlast", AccessLevel.Player, new CommandEventHandler( PsychicBlast_OnCommand ) );

			Register( "DrainLife", AccessLevel.Player, new CommandEventHandler( DrainLife_OnCommand ) );

			Register( "CloneBody", AccessLevel.Player, new CommandEventHandler( Clone_OnCommand ) );
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

		[Usage( "Psychokinesis" )]
		[Description( "Casts Psychokinesis" )]
		public static void Psychokinesis_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 270 ) ){ new Psychokinesis( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "DeathGrip" )]
		[Description( "Casts Death Grip" )]
		public static void DeathGrip_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 271 ) ){ new DeathGrip( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "Projection" )]
		[Description( "Casts Projection" )]
		public static void Projection_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 272 ) ){ new Projection( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "ThrowSword" )]
		[Description( "Casts Throw Sword" )]
		public static void ThrowSword_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 273 ) ){ new ThrowSword( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "SythSpeed" )]
		[Description( "Casts Speed" )]
		public static void SythSpeed_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 274 ) ){ new SythSpeed( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "SythLightning" )]
		[Description( "Casts Syth Lightning" )]
		public static void SythLightning_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 275 ) ){ new SythLightning( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "Absorption" )]
		[Description( "Casts Absorption" )]
		public static void Absorption_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 276 ) ){ new Absorption( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "PsychicBlast" )]
		[Description( "Casts Psychic Blast" )]
		public static void PsychicBlast_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 277 ) ){ new PsychicBlast( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "DrainLife" )]
		[Description( "Casts Drain Life" )]
		public static void DrainLife_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 278 ) ){ new DrainLife( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "CloneBody" )]
		[Description( "Casts Clone" )]
		public static void Clone_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 279 ) ){ new CloneBody( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}
	}
}
