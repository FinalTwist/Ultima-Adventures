using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.Jedi;
using Server.Commands;

namespace Server.Scripts.Commands
{
	public class CastJediSpells
	{
		public static void Initialize()
		{
            Properties.Initialize();

			Register( "ForceGrip", AccessLevel.Player, new CommandEventHandler( ForceGrip_OnCommand ) );

			Register( "MindsEye", AccessLevel.Player, new CommandEventHandler( MindsEye_OnCommand ) );

			Register( "Mirage", AccessLevel.Player, new CommandEventHandler( Mirage_OnCommand ) );

			Register( "ThrowSabre", AccessLevel.Player, new CommandEventHandler( ThrowSabre_OnCommand ) );

			Register( "Celerity", AccessLevel.Player, new CommandEventHandler( Celerity_OnCommand ) );

			Register( "PsychicAura", AccessLevel.Player, new CommandEventHandler( PsychicAura_OnCommand ) );

			Register( "Deflection", AccessLevel.Player, new CommandEventHandler( Deflection_OnCommand ) );

			Register( "SoothingTouch", AccessLevel.Player, new CommandEventHandler( SoothingTouch_OnCommand ) );

			Register( "StasisField", AccessLevel.Player, new CommandEventHandler( StasisField_OnCommand ) );

			Register( "Replicate", AccessLevel.Player, new CommandEventHandler( Clone_OnCommand ) );
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

		[Usage( "ForceGrip" )]
		[Description( "Casts Force Grip" )]
		public static void ForceGrip_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 280 ) ){ new ForceGrip( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "MindsEye" )]
		[Description( "Casts Minds Eye" )]
		public static void MindsEye_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 281 ) ){ new MindsEye( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "Mirage" )]
		[Description( "Casts Mirage" )]
		public static void Mirage_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 282 ) ){ new Mirage( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "ThrowSabre" )]
		[Description( "Casts Throw Sabre" )]
		public static void ThrowSabre_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 283 ) ){ new ThrowSabre( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "Celerity" )]
		[Description( "Casts Celerity" )]
		public static void Celerity_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 284 ) ){ new Celerity( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "PsychicAura" )]
		[Description( "Casts Psychic Aura" )]
		public static void PsychicAura_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 285 ) ){ new PsychicAura( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "Deflection" )]
		[Description( "Casts Deflection" )]
		public static void Deflection_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 286 ) ){ new Deflection( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "SoothingTouch" )]
		[Description( "Casts Soothing Touch" )]
		public static void SoothingTouch_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 287 ) ){ new SoothingTouch( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "StasisField" )]
		[Description( "Casts Stasis Field" )]
		public static void StasisField_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 288 ) ){ new StasisField( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "Replicate" )]
		[Description( "Casts Replicate" )]
		public static void Clone_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 289 ) ){ new Replicate( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}
	}
}
