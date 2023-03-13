using System;
using Server;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.Commands;
using Server.Commands.Generic;
using Server.Spells;
using Server.Spells.First;
using Server.Spells.Second;
using Server.Spells.Third;
using Server.Spells.Fourth;
using Server.Spells.Fifth;
using Server.Spells.Sixth;
using Server.Spells.Seventh;
using Server.Spells.Eighth;
using Server.Spells.Necromancy;
using Server.Spells.Chivalry;
using Server.Spells.DeathKnight; 
using Server.Spells.Song;
using Server.Spells.HolyMan;
using Server.Prompts;
using Server.Gumps;

namespace Server.Items
{
    class MageClose1
    {
		public static void Initialize()
		{
            CommandSystem.Register( "mageclose1", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "mageclose1" )]
		[Description( "Close Spell Bar Windows For Mages - 1." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsMage1 ) );
			from.CloseGump( typeof( SpellBarsMage1 ) );
        }
    }

    class MageClose2
    {
		public static void Initialize()
		{
            CommandSystem.Register( "mageclose2", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "mageclose2" )]
		[Description( "Close Spell Bar Windows For Mages - 2." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsMage2 ) );
			from.CloseGump( typeof( SpellBarsMage2 ) );
        }
    }

    class MageClose3
    {
		public static void Initialize()
		{
            CommandSystem.Register( "mageclose3", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "mageclose3" )]
		[Description( "Close Spell Bar Windows For Mages - 3." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsMage3 ) );
			from.CloseGump( typeof( SpellBarsMage3 ) );
        }
    }

    class MageClose4
    {
		public static void Initialize()
		{
            CommandSystem.Register( "mageclose4", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "mageclose4" )]
		[Description( "Close Spell Bar Windows For Mages - 4." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsMage4 ) );
			from.CloseGump( typeof( SpellBarsMage4 ) );
        }
    }

    class NecroClose1
    {
		public static void Initialize()
		{
            CommandSystem.Register( "necroclose1", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "necroclose1" )]
		[Description( "Close Spell Bar Windows For Necromancers - 1." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsNecro1 ) );
			from.CloseGump( typeof( SpellBarsNecro1 ) );
        }
    }

    class NecroClose2
    {
		public static void Initialize()
		{
            CommandSystem.Register( "necroclose2", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "necroclose2" )]
		[Description( "Close Spell Bar Windows For Necromancers - 2." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsNecro2 ) );
			from.CloseGump( typeof( SpellBarsNecro2 ) );
        }
    }

	class DeathClose1
    {
		public static void Initialize()
		{
            CommandSystem.Register( "deathclose1", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "deathclose1" )]
		[Description( "Close Spell Bar Windows For Death Knights - 1." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsDeath1 ) );
			from.CloseGump( typeof( SpellBarsDeath1 ) );
        }
    }

    class DeathClose2
    {
		public static void Initialize()
		{
            CommandSystem.Register( "deathclose2", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "deathclose2" )]
		[Description( "Close Spell Bar Windows For Death Knights - 2." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsDeath2 ) );
			from.CloseGump( typeof( SpellBarsDeath2 ) );
        }
    }

	class PriestClose1
    {
		public static void Initialize()
		{
            CommandSystem.Register( "holyclose1", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "holyclose1" )]
		[Description( "Close Spell Bar Windows For Prayers - 1." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsPriest1 ) );
			from.CloseGump( typeof( SpellBarsPriest1 ) );
        }
    }

    class PriestClose2
    {
		public static void Initialize()
		{
            CommandSystem.Register( "holyclose2", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "holyclose2" )]
		[Description( "Close Spell Bar Windows For Prayers - 2." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsPriest2 ) );
			from.CloseGump( typeof( SpellBarsPriest2 ) );
        }
    }

	class ChivalryClose1
    {
		public static void Initialize()
		{
            CommandSystem.Register( "chivalryclose1", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "chivalryclose1" )]
		[Description( "Close Spell Bar Windows For Knights - 1." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsChivalry1 ) );
			from.CloseGump( typeof( SpellBarsChivalry1 ) );
        }
    }

    class ChivalryClose2
    {
		public static void Initialize()
		{
            CommandSystem.Register( "chivalryclose2", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "chivalryclose2" )]
		[Description( "Close Spell Bar Windows For Knights - 2." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsChivalry2 ) );
			from.CloseGump( typeof( SpellBarsChivalry2 ) );
        }
    }

    class BardClose1
    {
		public static void Initialize()
		{
            CommandSystem.Register( "bardclose1", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "bardclose1" )]
		[Description( "Close Spell Bar Windows For Bards - 1." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsBard1 ) );
			from.CloseGump( typeof( SpellBarsBard1 ) );
        }
    }

    class BardClose2
    {
		public static void Initialize()
		{
            CommandSystem.Register( "bardclose2", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "bardclose2" )]
		[Description( "Close Spell Bar Windows For Bards - 2." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsBard2 ) );
			from.CloseGump( typeof( SpellBarsBard2 ) );
        }
    }

    class MonkClose1
    {
		public static void Initialize()
		{
            CommandSystem.Register( "monkclose1", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "monkclose1" )]
		[Description( "Close Spell Bar Windows For Monks - 1." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsMonk1 ) );
			from.CloseGump( typeof( SpellBarsMonk1 ) );
        }
    }

    class MonkClose2
    {
		public static void Initialize()
		{
            CommandSystem.Register( "monkclose2", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "monkclose2" )]
		[Description( "Close Spell Bar Windows For Monks - 2." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SetupBarsMonk2 ) );
			from.CloseGump( typeof( SpellBarsMonk2 ) );
        }
    }
}