using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.HolyMan;
using Server.Commands;

namespace Server.Scripts.Commands
{
	public class CastHolyManSpells
	{
		public static void Initialize()
		{
            Properties.Initialize();

			Register( "BanishEvil", AccessLevel.Player, new CommandEventHandler( BanishEvil_OnCommand ) );

			Register( "DampenSpirit", AccessLevel.Player, new CommandEventHandler( DampenSpirit_OnCommand ) );

			Register( "Enchant", AccessLevel.Player, new CommandEventHandler( Enchant_OnCommand ) );

			Register( "HammerOfFaith", AccessLevel.Player, new CommandEventHandler( HammerOfFaith_OnCommand ) );

			Register( "HeavenlyLight", AccessLevel.Player, new CommandEventHandler( HeavenlyLight_OnCommand ) );

			Register( "Nourish", AccessLevel.Player, new CommandEventHandler( Nourish_OnCommand ) );

			Register( "Purge", AccessLevel.Player, new CommandEventHandler( Purge_OnCommand ) );

			Register( "Rebirth", AccessLevel.Player, new CommandEventHandler( Rebirth_OnCommand ) );

			Register( "SacredBoon", AccessLevel.Player, new CommandEventHandler( SacredBoon_OnCommand ) );

			Register( "Sanctify", AccessLevel.Player, new CommandEventHandler( Sanctify_OnCommand ) );
			
			Register( "Seance", AccessLevel.Player, new CommandEventHandler( Seance_OnCommand ) );
			
			Register( "Smite", AccessLevel.Player, new CommandEventHandler( Smite_OnCommand ) );
			
			Register( "TouchOfLife", AccessLevel.Player, new CommandEventHandler( TouchOfLife_OnCommand ) );
			
			Register( "TrialByFire", AccessLevel.Player, new CommandEventHandler( TrialByFire_OnCommand ) );
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

		[Usage( "BanishEvil" )]
		[Description( "Casts Banish Evil" )]
		public static void BanishEvil_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 770 ) ){ new BanishEvilSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "DampenSpirit" )]
		[Description( "Casts Dampen Spirit" )]
		public static void DampenSpirit_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 771 ) ){ new DampenSpiritSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "Enchant" )]
		[Description( "Casts Enchant Weapon" )]
		public static void Enchant_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 772 ) ){ new EnchantSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "HammerOfFaith" )]
		[Description( "Casts Hammer of Faith" )]
		public static void HammerOfFaith_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 773 ) ){ new HammerOfFaithSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "HeavenlyLight" )]
		[Description( "Casts Heavenly Light" )]
		public static void HeavenlyLight_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 774 ) ){ new HeavenlyLightSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "Nourish" )]
		[Description( "Casts Nourish" )]
		public static void Nourish_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 775 ) ){ new NourishSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "Purge" )]
		[Description( "Casts Purge" )]
		public static void Purge_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 776 ) ){ new PurgeSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "Rebirth" )]
		[Description( "Casts Rebirth" )]
		public static void Rebirth_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 777 ) ){ new RebirthSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "SacredBoon" )]
		[Description( "Casts Sacred Boon" )]
		public static void SacredBoon_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 778 ) ){ new SacredBoonSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}

		[Usage( "Sanctify" )]
		[Description( "Casts Sabctify" )]
		public static void Sanctify_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 779 ) ){ new SanctifySpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}
				
				[Usage( "Seance" )]
		[Description( "Casts Seance" )]
		public static void Seance_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 780 ) ){ new SeanceSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}
		
				[Usage( "Smite" )]
		[Description( "Casts Smite" )]
		public static void Smite_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 781 ) ){ new SmiteSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}
		
				[Usage( "TouchOfLife" )]
		[Description( "Casts Touch of Life" )]
		public static void TouchOfLife_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 782 ) ){ new TouchOfLifeSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}
		
				[Usage( "TrialByFire" )]
		[Description( "Casts Trial By Fire" )]
		public static void TrialByFire_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( !Multis.DesignContext.Check( e.Mobile ) ){ return; } // They are customizing
			if ( HasSpell( from, 783 ) ){ new TrialByFireSpell( e.Mobile, null ).Cast(); } else { from.SendLocalizedMessage( 500015 ); } // You do not have that spell!
		}
	}
}
