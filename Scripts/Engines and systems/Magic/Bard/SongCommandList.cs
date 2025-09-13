using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.Song;
using Server.Commands;

namespace Server.Scripts.Commands
{
	public class CastSongSpells
	{
		public static void Initialize()
		{
            Properties.Initialize();

			Register( "ArmysPaeon", AccessLevel.Player, new CommandEventHandler( ArmysPaeon_OnCommand ) );

			Register( "EnchantingEtude", AccessLevel.Player, new CommandEventHandler( EnchantingEtude_OnCommand ) );

			Register( "EnergyCarol", AccessLevel.Player, new CommandEventHandler( EnergyCarol_OnCommand ) );

			Register( "EnergyThrenody", AccessLevel.Player, new CommandEventHandler( EnergyThrenody_OnCommand ) );

			Register( "FireCarol", AccessLevel.Player, new CommandEventHandler( FireCarol_OnCommand ) );

			Register( "FireThrenody", AccessLevel.Player, new CommandEventHandler( FireThrenody_OnCommand ) );

			Register( "FoeRequiem", AccessLevel.Player, new CommandEventHandler( FoeRequiem_OnCommand ) );

			Register( "IceCarol", AccessLevel.Player, new CommandEventHandler( IceCarol_OnCommand ) );

			Register( "IceThrenody", AccessLevel.Player, new CommandEventHandler( IceThrenody_OnCommand ) );

			Register( "KnightsMinne", AccessLevel.Player, new CommandEventHandler( KnightsMinne_OnCommand ) );

			Register( "MagesBallad", AccessLevel.Player, new CommandEventHandler( MagesBallad_OnCommand ) );

			Register( "MagicFinale", AccessLevel.Player, new CommandEventHandler( MagicFinale_OnCommand ) );

			Register( "PoisonCarol", AccessLevel.Player, new CommandEventHandler( PoisonCarol_OnCommand ) );

			Register( "PoisonThrenody", AccessLevel.Player, new CommandEventHandler( PoisonThrenody_OnCommand ) );

			Register( "ShephardsDance", AccessLevel.Player, new CommandEventHandler( SheepfoeMambo_OnCommand ) );

			Register( "SinewyEtude", AccessLevel.Player, new CommandEventHandler( SinewyEtude_OnCommand ) );

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

		[Usage( "ArmysPaeon" )]
		[Description( "Casts ArmysPaeon" )]
		public static void ArmysPaeon_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 351 ) )
					{
					new ArmysPaeonSong( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
                    }
        } 			

		
		[Usage( "EnchantingEtude" )]
		[Description( "Casts EnchantingEtude" )]
		public static void EnchantingEtude_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;
			
         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 352 ) )
					{
					new EnchantingEtudeSong( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "EnergyCarol" )]
		[Description( "Casts EnergyCarol" )]
		public static void EnergyCarol_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 353 ) )
					{
					new EnergyCarolSong( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "EnergyThrenody" )]
		[Description( "Casts EnergyThrenody" )]
		public static void EnergyThrenody_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 354 ) )
					{
					new EnergyThrenodySong( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "FireCarol" )]
		[Description( "Casts FireCarol" )]
		public static void FireCarol_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 355 ) )
					{
					new FireCarolSong( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "FireThrenody" )]
		[Description( "Casts FireThrenody" )]
		public static void FireThrenody_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 356 ) )
					{
					new  FireThrenodySong( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "FoeRequiem" )]
		[Description( "Casts FoeRequiem" )]
		public static void FoeRequiem_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 357 ) )
					{
					new FoeRequiemSong( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "IceCarol" )]
		[Description( "Casts IceCarol" )]
		public static void IceCarol_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 358 ) )
					{
					new IceCarolSong( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "IceThrenody" )]
		[Description( "Casts IceThrenody" )]
		public static void IceThrenody_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 359 ) )
					{
					new IceThrenodySong( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "KnightsMinne" )]
		[Description( "Casts KnightsMinne" )]
		public static void KnightsMinne_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 360 ) )
					{
					new KnightsMinneSong( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "MagesBallad" )]
		[Description( "Casts MagesBallad" )]
		public static void MagesBallad_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 361 ) )
					{
					new MagesBalladSong( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "MagicFinale" )]
		[Description( "Casts MagicFinale" )]
		public static void MagicFinale_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 362 ) )
					{
					new MagicFinaleSong( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "PoisonCarol" )]
		[Description( "Casts PoisonCarol" )]
		public static void PoisonCarol_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 363 ) )
					{
					new PoisonCarolSong( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "PoisonThrenody" )]
		[Description( "Casts PoisonThrenody" )]
		public static void PoisonThrenody_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 364 ) )
					{
					new PoisonThrenodySong( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}

		[Usage( "ShephardsDance" )]
		[Description( "Casts ShepherdDance" )]
		public static void SheepfoeMambo_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 365 ) )
					{
					new SheepfoeMamboSong( e.Mobile, null ).Cast(); 
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 			

		}
		[Usage( "SinewyEtude" )]
		[Description( "Casts SinewyEtude" )]
		public static void SinewyEtude_OnCommand( CommandEventArgs e )
		{
				Mobile from = e.Mobile;

         			if ( !Multis.DesignContext.Check( e.Mobile ) )
            				return; // They are customizing

				if ( HasSpell( from, 366 ) )
					{

					new SinewyEtudeSong( e.Mobile, null ).Cast();
					}
				else
					{
									from.SendLocalizedMessage( 500015 ); // You do not have that spell!
					} 

		}
	}
}
