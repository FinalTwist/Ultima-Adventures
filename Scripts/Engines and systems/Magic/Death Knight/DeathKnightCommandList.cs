using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.DeathKnight;
using Server.Commands;

namespace Server.Scripts.Commands
{
	public class CastDeathKnightSpells
	{
		public static void Initialize()
		{
            Properties.Initialize();

			Register( "Banish", AccessLevel.Player, new CommandEventHandler( Banish_OnCommand ) );

			Register( "DemonicTouch", AccessLevel.Player, new CommandEventHandler( DemonicTouch_OnCommand ) );

			Register( "DevilPact", AccessLevel.Player, new CommandEventHandler( DevilPact_OnCommand ) );

			Register( "GrimReaper", AccessLevel.Player, new CommandEventHandler( GrimReaper_OnCommand ) );

			Register( "HagHand", AccessLevel.Player, new CommandEventHandler( HagHand_OnCommand ) );

			Register( "Hellfire", AccessLevel.Player, new CommandEventHandler( Hellfire_OnCommand ) );

			Register( "LucifersBolt", AccessLevel.Player, new CommandEventHandler( LucifersBolt_OnCommand ) );

			Register( "OrbofOrcus", AccessLevel.Player, new CommandEventHandler( OrbofOrcus_OnCommand ) );

			Register( "ShieldofHate", AccessLevel.Player, new CommandEventHandler( ShieldofHate_OnCommand ) );

			Register( "SoulReaper", AccessLevel.Player, new CommandEventHandler( SoulReaper_OnCommand ) );
			
			Register( "StrengthofSteel", AccessLevel.Player, new CommandEventHandler( StrengthofSteel_OnCommand ) );
			
			Register( "Strike", AccessLevel.Player, new CommandEventHandler( Strike_OnCommand ) );
			
			Register( "SuccubusSkin", AccessLevel.Player, new CommandEventHandler( SuccubusSkin_OnCommand ) );
			
			Register( "Wrath", AccessLevel.Player, new CommandEventHandler( Wrath_OnCommand ) );
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

		[Usage( "Banish" )]
		[Description( "Casts Banish" )]
		public static void Banish_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 750 ) ){ new BanishSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "DemonicTouch" )]
		[Description( "Casts Demonic Touch" )]
		public static void DemonicTouch_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 751 ) ){ new DemonicTouchSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "DevilPact" )]
		[Description( "Casts Devil Pact" )]
		public static void DevilPact_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 752 ) ){ new DevilPactSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "GrimReaper" )]
		[Description( "Casts Grim Reaper" )]
		public static void GrimReaper_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 753 ) ){ new GrimReaperSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "HagHand" )]
		[Description( "Casts Hag Hand" )]
		public static void HagHand_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 754 ) ){ new HagHandSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "Hellfire" )]
		[Description( "Casts Hellfire" )]
		public static void Hellfire_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 755 ) ){ new HellfireSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "LucifersBolt" )]
		[Description( "Casts Lucifer's Bolt" )]
		public static void LucifersBolt_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 756 ) ){ new LucifersBoltSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "OrbofOrcus" )]
		[Description( "Casts Orb of Orcus" )]
		public static void OrbofOrcus_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 757 ) ){ new OrbOfOrcusSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "ShieldofHate" )]
		[Description( "Casts Shield of Hate" )]
		public static void ShieldofHate_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 758 ) ){ new ShieldOfHateSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "SoulReaper" )]
		[Description( "Casts Soul Reaper" )]
		public static void SoulReaper_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 759 ) ){ new SoulReaperSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}
				
				[Usage( "StrengthofSteel" )]
		[Description( "Casts Strength of Steel" )]
		public static void StrengthofSteel_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 760 ) ){ new StrengthOfSteelSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}
		
				[Usage( "Strike" )]
		[Description( "Casts Strike" )]
		public static void Strike_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 761 ) ){ new StrikeSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}
		
				[Usage( "SuccubusSkin" )]
		[Description( "Casts Succubus Skin" )]
		public static void SuccubusSkin_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 762 ) ){ new SuccubusSkinSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}
		
				[Usage( "Wrath" )]
		[Description( "Casts Wrath" )]
		public static void Wrath_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 763 ) ){ new WrathSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}
	}
}
