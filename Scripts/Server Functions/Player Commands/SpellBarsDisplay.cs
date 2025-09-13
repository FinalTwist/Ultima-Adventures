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
using Server.Spells.Mystic;
using Server.Prompts;
using Server.Gumps;

namespace Server.Gumps 
{
    public class SpellBarsMage1 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "magetool1", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "magetool1" )]
		[Description( "Opens Spell Bar For Mages - 1." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsMage1 ) );
			from.SendGump( new SpellBarsMage1( from ) );
        }
   
        public SpellBarsMage1 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 66, "SetupBarsMage1" ) > 0 )
			{
				this.AddImage(7, 0, 2234, 0);
				int dby = 53;

				if ( HasSpell( from, 0 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2240, 2240, 99, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Clumsy"); } }
				if ( HasSpell( from, 1 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2241, 2241, 1, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Create Food"); } }
				if ( HasSpell( from, 2 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2242, 2242, 2, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Feeblemind"); } }
				if ( HasSpell( from, 3 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2243, 2243, 3, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Heal"); } }
				if ( HasSpell( from, 4 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2244, 2244, 4, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Magic Arrow"); } }
				if ( HasSpell( from, 5 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2245, 2245, 5, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Night Sight"); } }
				if ( HasSpell( from, 6 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2246, 2246, 6, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Reactive Armor"); } }
				if ( HasSpell( from, 7 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2247, 2247, 7, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Weaken"); } }
				if ( HasSpell( from, 8 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2248, 2248, 8, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Agility"); } }
				if ( HasSpell( from, 9 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2249, 2249, 9, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Cunning"); } }
				if ( HasSpell( from, 10 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2250, 2250, 10, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Cure"); } }
				if ( HasSpell( from, 11 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2251, 2251, 11, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Harm"); } }
				if ( HasSpell( from, 12 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2252, 2252, 12, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Magic Trap"); } }
				if ( HasSpell( from, 13 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2253, 2253, 13, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Remove Trap"); } }
				if ( HasSpell( from, 14 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2254, 2254, 14, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Protection"); } }
				if ( HasSpell( from, 15 ) && ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2255, 2255, 15, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Strength"); } }
				if ( HasSpell( from, 16 ) && ToolBarUpdates.GetToolBarSetting( from, 17, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2256, 2256, 16, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Bless"); } }
				if ( HasSpell( from, 17 ) && ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2257, 2257, 17, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fireball"); } }
				if ( HasSpell( from, 18 ) && ToolBarUpdates.GetToolBarSetting( from, 19, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2258, 2258, 18, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"MagicLock"); } }
				if ( HasSpell( from, 19 ) && ToolBarUpdates.GetToolBarSetting( from, 20, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2259, 2259, 19, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Poison"); } }
				if ( HasSpell( from, 20 ) && ToolBarUpdates.GetToolBarSetting( from, 21, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2260, 2260, 20, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Telekinesis"); } }
				if ( HasSpell( from, 21 ) && ToolBarUpdates.GetToolBarSetting( from, 22, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2261, 2261, 21, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Teleport"); } }
				if ( HasSpell( from, 22 ) && ToolBarUpdates.GetToolBarSetting( from, 23, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2262, 2262, 22, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Unlock"); } }
				if ( HasSpell( from, 23 ) && ToolBarUpdates.GetToolBarSetting( from, 24, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2263, 2263, 23, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Wall Of Stone"); } }
				if ( HasSpell( from, 24 ) && ToolBarUpdates.GetToolBarSetting( from, 25, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2264, 2264, 24, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Arch Cure"); } }
				if ( HasSpell( from, 25 ) && ToolBarUpdates.GetToolBarSetting( from, 26, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2265, 2265, 25, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Arch Protection"); } }
				if ( HasSpell( from, 26 ) && ToolBarUpdates.GetToolBarSetting( from, 27, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2266, 2266, 26, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Curse"); } }
				if ( HasSpell( from, 27 ) && ToolBarUpdates.GetToolBarSetting( from, 28, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2267, 2267, 27, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fire Field"); } }
				if ( HasSpell( from, 28 ) && ToolBarUpdates.GetToolBarSetting( from, 29, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2268, 2268, 28, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Greater Heal"); } }
				if ( HasSpell( from, 29 ) && ToolBarUpdates.GetToolBarSetting( from, 30, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2269, 2269, 29, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Lightning"); } }
				if ( HasSpell( from, 30 ) && ToolBarUpdates.GetToolBarSetting( from, 31, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2270, 2270, 30, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mana Drain"); } }
				if ( HasSpell( from, 31 ) && ToolBarUpdates.GetToolBarSetting( from, 32, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2271, 2271, 31, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Recall"); } }
				if ( HasSpell( from, 32 ) && ToolBarUpdates.GetToolBarSetting( from, 33, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2272, 2272, 32, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Blade Spirits"); } }
				if ( HasSpell( from, 33 ) && ToolBarUpdates.GetToolBarSetting( from, 34, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2273, 2273, 33, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Dispel Field"); } }
				if ( HasSpell( from, 34 ) && ToolBarUpdates.GetToolBarSetting( from, 35, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2274, 2274, 34, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Incognito"); } }
				if ( HasSpell( from, 35 ) && ToolBarUpdates.GetToolBarSetting( from, 36, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2275, 2275, 35, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Magic Reflect"); } }
				if ( HasSpell( from, 36 ) && ToolBarUpdates.GetToolBarSetting( from, 37, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2276, 2276, 36, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mind Blast"); } }
				if ( HasSpell( from, 37 ) && ToolBarUpdates.GetToolBarSetting( from, 38, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2277, 2277, 37, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Paralyze"); } }
				if ( HasSpell( from, 38 ) && ToolBarUpdates.GetToolBarSetting( from, 39, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2278, 2278, 38, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Poison Field"); } }
				if ( HasSpell( from, 39 ) && ToolBarUpdates.GetToolBarSetting( from, 40, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2279, 2279, 39, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Summon Creature"); } }
				if ( HasSpell( from, 40 ) && ToolBarUpdates.GetToolBarSetting( from, 41, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2280, 2280, 40, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Dispel"); } }
				if ( HasSpell( from, 41 ) && ToolBarUpdates.GetToolBarSetting( from, 42, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2281, 2281, 41, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Bolt"); } }
				if ( HasSpell( from, 42 ) && ToolBarUpdates.GetToolBarSetting( from, 43, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2282, 2282, 42, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Explosion"); } }
				if ( HasSpell( from, 43 ) && ToolBarUpdates.GetToolBarSetting( from, 44, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2283, 2283, 43, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Invisibility"); } }
				if ( HasSpell( from, 44 ) && ToolBarUpdates.GetToolBarSetting( from, 45, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2284, 2284, 44, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mark"); } }
				if ( HasSpell( from, 45 ) && ToolBarUpdates.GetToolBarSetting( from, 46, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2285, 2285, 45, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mass Curse"); } }
				if ( HasSpell( from, 46 ) && ToolBarUpdates.GetToolBarSetting( from, 47, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2286, 2286, 46, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Paralyze Field"); } }
				if ( HasSpell( from, 47 ) && ToolBarUpdates.GetToolBarSetting( from, 48, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2287, 2287, 47, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Reveal"); } }
				if ( HasSpell( from, 48 ) && ToolBarUpdates.GetToolBarSetting( from, 49, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2288, 2288, 48, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Chain Lightning"); } }
				if ( HasSpell( from, 49 ) && ToolBarUpdates.GetToolBarSetting( from, 50, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2289, 2289, 49, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Field"); } }
				if ( HasSpell( from, 50 ) && ToolBarUpdates.GetToolBarSetting( from, 51, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2290, 2290, 50, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Flame Strike"); } }
				if ( HasSpell( from, 51 ) && ToolBarUpdates.GetToolBarSetting( from, 52, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2291, 2291, 51, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Gate Travel"); } }
				if ( HasSpell( from, 52 ) && ToolBarUpdates.GetToolBarSetting( from, 53, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2292, 2292, 52, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mana Vampire"); } }
				if ( HasSpell( from, 53 ) && ToolBarUpdates.GetToolBarSetting( from, 54, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2293, 2293, 53, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mass Dispel"); } }
				if ( HasSpell( from, 54 ) && ToolBarUpdates.GetToolBarSetting( from, 55, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2294, 2294, 54, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Meteor Swarm"); } }
				if ( HasSpell( from, 55 ) && ToolBarUpdates.GetToolBarSetting( from, 56, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2295, 2295, 55, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Polymorph"); } }
				if ( HasSpell( from, 56 ) && ToolBarUpdates.GetToolBarSetting( from, 57, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2296, 2296, 56, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Earthquake"); } }
				if ( HasSpell( from, 57 ) && ToolBarUpdates.GetToolBarSetting( from, 58, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2297, 2297, 57, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Vortex"); } }
				if ( HasSpell( from, 58 ) && ToolBarUpdates.GetToolBarSetting( from, 59, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2298, 2298, 58, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Resurrection"); } }
				if ( HasSpell( from, 59 ) && ToolBarUpdates.GetToolBarSetting( from, 60, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2299, 2299, 59, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Air Elemental"); } }
				if ( HasSpell( from, 60 ) && ToolBarUpdates.GetToolBarSetting( from, 61, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2300, 2300, 60, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Summon Daemon"); } }
				if ( HasSpell( from, 61 ) && ToolBarUpdates.GetToolBarSetting( from, 62, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2301, 2301, 61, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Earth Elemental"); } }
				if ( HasSpell( from, 62 ) && ToolBarUpdates.GetToolBarSetting( from, 63, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2302, 2302, 62, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fire Elemental"); } }
				if ( HasSpell( from, 63 ) && ToolBarUpdates.GetToolBarSetting( from, 64, "SetupBarsMage1" ) == 1){this.AddButton(5, dby, 2303, 2303, 63, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Water Elemental"); } }
			}			
			else			
			{			
				this.AddImage(0, 0, 2234, 0);		
				int dby = 50;		
				if ( HasSpell( from, 0 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2240, 2240, 99, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 1 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2241, 2241, 1, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 2 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2242, 2242, 2, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 3 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2243, 2243, 3, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 4 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2244, 2244, 4, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 5 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2245, 2245, 5, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 6 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2246, 2246, 6, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 7 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2247, 2247, 7, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 8 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2248, 2248, 8, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 9 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2249, 2249, 9, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 10 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2250, 2250, 10, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 11 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2251, 2251, 11, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 12 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2252, 2252, 12, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 13 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2253, 2253, 13, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 14 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2254, 2254, 14, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 15 ) && ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2255, 2255, 15, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 16 ) && ToolBarUpdates.GetToolBarSetting( from, 17, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2256, 2256, 16, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 17 ) && ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2257, 2257, 17, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 18 ) && ToolBarUpdates.GetToolBarSetting( from, 19, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2258, 2258, 18, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 19 ) && ToolBarUpdates.GetToolBarSetting( from, 20, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2259, 2259, 19, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 20 ) && ToolBarUpdates.GetToolBarSetting( from, 21, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2260, 2260, 20, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 21 ) && ToolBarUpdates.GetToolBarSetting( from, 22, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2261, 2261, 21, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 22 ) && ToolBarUpdates.GetToolBarSetting( from, 23, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2262, 2262, 22, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 23 ) && ToolBarUpdates.GetToolBarSetting( from, 24, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2263, 2263, 23, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 24 ) && ToolBarUpdates.GetToolBarSetting( from, 25, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2264, 2264, 24, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 25 ) && ToolBarUpdates.GetToolBarSetting( from, 26, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2265, 2265, 25, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 26 ) && ToolBarUpdates.GetToolBarSetting( from, 27, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2266, 2266, 26, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 27 ) && ToolBarUpdates.GetToolBarSetting( from, 28, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2267, 2267, 27, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 28 ) && ToolBarUpdates.GetToolBarSetting( from, 29, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2268, 2268, 28, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 29 ) && ToolBarUpdates.GetToolBarSetting( from, 30, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2269, 2269, 29, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 30 ) && ToolBarUpdates.GetToolBarSetting( from, 31, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2270, 2270, 30, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 31 ) && ToolBarUpdates.GetToolBarSetting( from, 32, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2271, 2271, 31, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 32 ) && ToolBarUpdates.GetToolBarSetting( from, 33, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2272, 2272, 32, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 33 ) && ToolBarUpdates.GetToolBarSetting( from, 34, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2273, 2273, 33, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 34 ) && ToolBarUpdates.GetToolBarSetting( from, 35, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2274, 2274, 34, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 35 ) && ToolBarUpdates.GetToolBarSetting( from, 36, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2275, 2275, 35, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 36 ) && ToolBarUpdates.GetToolBarSetting( from, 37, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2276, 2276, 36, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 37 ) && ToolBarUpdates.GetToolBarSetting( from, 38, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2277, 2277, 37, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 38 ) && ToolBarUpdates.GetToolBarSetting( from, 39, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2278, 2278, 38, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 39 ) && ToolBarUpdates.GetToolBarSetting( from, 40, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2279, 2279, 39, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 40 ) && ToolBarUpdates.GetToolBarSetting( from, 41, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2280, 2280, 40, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 41 ) && ToolBarUpdates.GetToolBarSetting( from, 42, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2281, 2281, 41, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 42 ) && ToolBarUpdates.GetToolBarSetting( from, 43, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2282, 2282, 42, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 43 ) && ToolBarUpdates.GetToolBarSetting( from, 44, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2283, 2283, 43, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 44 ) && ToolBarUpdates.GetToolBarSetting( from, 45, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2284, 2284, 44, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 45 ) && ToolBarUpdates.GetToolBarSetting( from, 46, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2285, 2285, 45, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 46 ) && ToolBarUpdates.GetToolBarSetting( from, 47, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2286, 2286, 46, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 47 ) && ToolBarUpdates.GetToolBarSetting( from, 48, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2287, 2287, 47, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 48 ) && ToolBarUpdates.GetToolBarSetting( from, 49, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2288, 2288, 48, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 49 ) && ToolBarUpdates.GetToolBarSetting( from, 50, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2289, 2289, 49, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 50 ) && ToolBarUpdates.GetToolBarSetting( from, 51, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2290, 2290, 50, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 51 ) && ToolBarUpdates.GetToolBarSetting( from, 52, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2291, 2291, 51, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 52 ) && ToolBarUpdates.GetToolBarSetting( from, 53, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2292, 2292, 52, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 53 ) && ToolBarUpdates.GetToolBarSetting( from, 54, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2293, 2293, 53, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 54 ) && ToolBarUpdates.GetToolBarSetting( from, 55, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2294, 2294, 54, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 55 ) && ToolBarUpdates.GetToolBarSetting( from, 56, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2295, 2295, 55, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 56 ) && ToolBarUpdates.GetToolBarSetting( from, 57, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2296, 2296, 56, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 57 ) && ToolBarUpdates.GetToolBarSetting( from, 58, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2297, 2297, 57, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 58 ) && ToolBarUpdates.GetToolBarSetting( from, 59, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2298, 2298, 58, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 59 ) && ToolBarUpdates.GetToolBarSetting( from, 60, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2299, 2299, 59, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 60 ) && ToolBarUpdates.GetToolBarSetting( from, 61, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2300, 2300, 60, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 61 ) && ToolBarUpdates.GetToolBarSetting( from, 62, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2301, 2301, 61, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 62 ) && ToolBarUpdates.GetToolBarSetting( from, 63, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2302, 2302, 62, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 63 ) && ToolBarUpdates.GetToolBarSetting( from, 64, "SetupBarsMage1" ) == 1){this.AddButton(dby, 5, 2303, 2303, 63, GumpButtonType.Reply, 1); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsMage1 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }
				case 99: { if ( HasSpell( from, 0 ) ) { new ClumsySpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 1: { if ( HasSpell( from, 1 ) ) { new CreateFoodSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 2: { if ( HasSpell( from, 2 ) ) { new FeeblemindSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 3: { if ( HasSpell( from, 3 ) ) { new HealSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 4: { if ( HasSpell( from, 4 ) ) { new MagicArrowSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 5: { if ( HasSpell( from, 5 ) ) { new NightSightSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 6: { if ( HasSpell( from, 6 ) ) { new ReactiveArmorSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 7: { if ( HasSpell( from, 7 ) ) { new WeakenSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 8: { if ( HasSpell( from, 8 ) ) { new AgilitySpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 9: { if ( HasSpell( from, 9 ) ) { new CunningSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 10: { if ( HasSpell( from, 10 ) ) { new CureSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 11: { if ( HasSpell( from, 11 ) ) { new HarmSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 12: { if ( HasSpell( from, 12 ) ) { new MagicTrapSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 13: { if ( HasSpell( from, 13 ) ) { new RemoveTrapSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 14: { if ( HasSpell( from, 14 ) ) { new ProtectionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 15: { if ( HasSpell( from, 15 ) ) { new StrengthSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 16: { if ( HasSpell( from, 16 ) ) { new BlessSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 17: { if ( HasSpell( from, 17 ) ) { new FireballSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 18: { if ( HasSpell( from, 18 ) ) { new MagicLockSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 19: { if ( HasSpell( from, 19 ) ) { new PoisonSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 20: { if ( HasSpell( from, 20 ) ) { new TelekinesisSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 21: { if ( HasSpell( from, 21 ) ) { new TeleportSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 22: { if ( HasSpell( from, 22 ) ) { new UnlockSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 23: { if ( HasSpell( from, 23 ) ) { new WallOfStoneSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 24: { if ( HasSpell( from, 24 ) ) { new ArchCureSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 25: { if ( HasSpell( from, 25 ) ) { new ArchProtectionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 26: { if ( HasSpell( from, 26 ) ) { new CurseSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 27: { if ( HasSpell( from, 27 ) ) { new FireFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 28: { if ( HasSpell( from, 28 ) ) { new GreaterHealSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 29: { if ( HasSpell( from, 29 ) ) { new LightningSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 30: { if ( HasSpell( from, 30 ) ) { new ManaDrainSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 31: { if ( HasSpell( from, 31 ) ) { new RecallSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 32: { if ( HasSpell( from, 32 ) ) { new BladeSpiritsSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 33: { if ( HasSpell( from, 33 ) ) { new DispelFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 34: { if ( HasSpell( from, 34 ) ) { new IncognitoSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 35: { if ( HasSpell( from, 35 ) ) { new MagicReflectSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 36: { if ( HasSpell( from, 36 ) ) { new MindBlastSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 37: { if ( HasSpell( from, 37 ) ) { new ParalyzeSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 38: { if ( HasSpell( from, 38 ) ) { new PoisonFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 39: { if ( HasSpell( from, 39 ) ) { new SummonCreatureSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 40: { if ( HasSpell( from, 40 ) ) { new DispelSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 41: { if ( HasSpell( from, 41 ) ) { new EnergyBoltSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 42: { if ( HasSpell( from, 42 ) ) { new ExplosionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 43: { if ( HasSpell( from, 43 ) ) { new InvisibilitySpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 44: { if ( HasSpell( from, 44 ) ) { new MarkSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 45: { if ( HasSpell( from, 45 ) ) { new MassCurseSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 46: { if ( HasSpell( from, 46 ) ) { new ParalyzeFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 47: { if ( HasSpell( from, 47 ) ) { new RevealSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 48: { if ( HasSpell( from, 48 ) ) { new ChainLightningSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 49: { if ( HasSpell( from, 49 ) ) { new EnergyFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 50: { if ( HasSpell( from, 50 ) ) { new FlameStrikeSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 51: { if ( HasSpell( from, 51 ) ) { new GateTravelSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 52: { if ( HasSpell( from, 52 ) ) { new ManaVampireSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 53: { if ( HasSpell( from, 53 ) ) { new MassDispelSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 54: { if ( HasSpell( from, 54 ) ) { new MeteorSwarmSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 55: { if ( HasSpell( from, 55 ) ) { new PolymorphSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 56: { if ( HasSpell( from, 56 ) ) { new EarthquakeSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 57: { if ( HasSpell( from, 57 ) ) { new EnergyVortexSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 58: { if ( HasSpell( from, 58 ) ) { new ResurrectionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 59: { if ( HasSpell( from, 59 ) ) { new AirElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 60: { if ( HasSpell( from, 60 ) ) { new SummonDaemonSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 61: { if ( HasSpell( from, 61 ) ) { new EarthElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 62: { if ( HasSpell( from, 62 ) ) { new FireElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
				case 63: { if ( HasSpell( from, 63 ) ) { new WaterElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage1( from ) ); } break; }
			}
		}
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Server.Gumps 
{
    public class SpellBarsMage2 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "magetool2", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "magetool2" )]
		[Description( "Opens Spell Bar For Mages - 2." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsMage2 ) );
			from.SendGump( new SpellBarsMage2( from ) );
        }
   
        public SpellBarsMage2 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 66, "SetupBarsMage2" ) > 0 )
			{
				this.AddImage(7, 0, 2234, 0);
				int dby = 53;

				if ( HasSpell( from, 0 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2240, 2240, 99, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Clumsy"); } }
				if ( HasSpell( from, 1 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2241, 2241, 1, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Create Food"); } }
				if ( HasSpell( from, 2 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2242, 2242, 2, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Feeblemind"); } }
				if ( HasSpell( from, 3 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2243, 2243, 3, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Heal"); } }
				if ( HasSpell( from, 4 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2244, 2244, 4, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Magic Arrow"); } }
				if ( HasSpell( from, 5 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2245, 2245, 5, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Night Sight"); } }
				if ( HasSpell( from, 6 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2246, 2246, 6, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Reactive Armor"); } }
				if ( HasSpell( from, 7 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2247, 2247, 7, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Weaken"); } }
				if ( HasSpell( from, 8 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2248, 2248, 8, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Agility"); } }
				if ( HasSpell( from, 9 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2249, 2249, 9, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Cunning"); } }
				if ( HasSpell( from, 10 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2250, 2250, 10, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Cure"); } }
				if ( HasSpell( from, 11 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2251, 2251, 11, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Harm"); } }
				if ( HasSpell( from, 12 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2252, 2252, 12, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Magic Trap"); } }
				if ( HasSpell( from, 13 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2253, 2253, 13, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Remove Trap"); } }
				if ( HasSpell( from, 14 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2254, 2254, 14, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Protection"); } }
				if ( HasSpell( from, 15 ) && ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2255, 2255, 15, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Strength"); } }
				if ( HasSpell( from, 16 ) && ToolBarUpdates.GetToolBarSetting( from, 17, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2256, 2256, 16, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Bless"); } }
				if ( HasSpell( from, 17 ) && ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2257, 2257, 17, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fireball"); } }
				if ( HasSpell( from, 18 ) && ToolBarUpdates.GetToolBarSetting( from, 19, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2258, 2258, 18, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"MagicLock"); } }
				if ( HasSpell( from, 19 ) && ToolBarUpdates.GetToolBarSetting( from, 20, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2259, 2259, 19, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Poison"); } }
				if ( HasSpell( from, 20 ) && ToolBarUpdates.GetToolBarSetting( from, 21, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2260, 2260, 20, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Telekinesis"); } }
				if ( HasSpell( from, 21 ) && ToolBarUpdates.GetToolBarSetting( from, 22, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2261, 2261, 21, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Teleport"); } }
				if ( HasSpell( from, 22 ) && ToolBarUpdates.GetToolBarSetting( from, 23, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2262, 2262, 22, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Unlock"); } }
				if ( HasSpell( from, 23 ) && ToolBarUpdates.GetToolBarSetting( from, 24, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2263, 2263, 23, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Wall Of Stone"); } }
				if ( HasSpell( from, 24 ) && ToolBarUpdates.GetToolBarSetting( from, 25, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2264, 2264, 24, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Arch Cure"); } }
				if ( HasSpell( from, 25 ) && ToolBarUpdates.GetToolBarSetting( from, 26, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2265, 2265, 25, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Arch Protection"); } }
				if ( HasSpell( from, 26 ) && ToolBarUpdates.GetToolBarSetting( from, 27, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2266, 2266, 26, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Curse"); } }
				if ( HasSpell( from, 27 ) && ToolBarUpdates.GetToolBarSetting( from, 28, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2267, 2267, 27, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fire Field"); } }
				if ( HasSpell( from, 28 ) && ToolBarUpdates.GetToolBarSetting( from, 29, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2268, 2268, 28, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Greater Heal"); } }
				if ( HasSpell( from, 29 ) && ToolBarUpdates.GetToolBarSetting( from, 30, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2269, 2269, 29, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Lightning"); } }
				if ( HasSpell( from, 30 ) && ToolBarUpdates.GetToolBarSetting( from, 31, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2270, 2270, 30, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mana Drain"); } }
				if ( HasSpell( from, 31 ) && ToolBarUpdates.GetToolBarSetting( from, 32, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2271, 2271, 31, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Recall"); } }
				if ( HasSpell( from, 32 ) && ToolBarUpdates.GetToolBarSetting( from, 33, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2272, 2272, 32, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Blade Spirits"); } }
				if ( HasSpell( from, 33 ) && ToolBarUpdates.GetToolBarSetting( from, 34, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2273, 2273, 33, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Dispel Field"); } }
				if ( HasSpell( from, 34 ) && ToolBarUpdates.GetToolBarSetting( from, 35, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2274, 2274, 34, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Incognito"); } }
				if ( HasSpell( from, 35 ) && ToolBarUpdates.GetToolBarSetting( from, 36, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2275, 2275, 35, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Magic Reflect"); } }
				if ( HasSpell( from, 36 ) && ToolBarUpdates.GetToolBarSetting( from, 37, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2276, 2276, 36, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mind Blast"); } }
				if ( HasSpell( from, 37 ) && ToolBarUpdates.GetToolBarSetting( from, 38, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2277, 2277, 37, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Paralyze"); } }
				if ( HasSpell( from, 38 ) && ToolBarUpdates.GetToolBarSetting( from, 39, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2278, 2278, 38, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Poison Field"); } }
				if ( HasSpell( from, 39 ) && ToolBarUpdates.GetToolBarSetting( from, 40, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2279, 2279, 39, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Summon Creature"); } }
				if ( HasSpell( from, 40 ) && ToolBarUpdates.GetToolBarSetting( from, 41, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2280, 2280, 40, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Dispel"); } }
				if ( HasSpell( from, 41 ) && ToolBarUpdates.GetToolBarSetting( from, 42, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2281, 2281, 41, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Bolt"); } }
				if ( HasSpell( from, 42 ) && ToolBarUpdates.GetToolBarSetting( from, 43, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2282, 2282, 42, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Explosion"); } }
				if ( HasSpell( from, 43 ) && ToolBarUpdates.GetToolBarSetting( from, 44, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2283, 2283, 43, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Invisibility"); } }
				if ( HasSpell( from, 44 ) && ToolBarUpdates.GetToolBarSetting( from, 45, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2284, 2284, 44, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mark"); } }
				if ( HasSpell( from, 45 ) && ToolBarUpdates.GetToolBarSetting( from, 46, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2285, 2285, 45, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mass Curse"); } }
				if ( HasSpell( from, 46 ) && ToolBarUpdates.GetToolBarSetting( from, 47, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2286, 2286, 46, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Paralyze Field"); } }
				if ( HasSpell( from, 47 ) && ToolBarUpdates.GetToolBarSetting( from, 48, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2287, 2287, 47, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Reveal"); } }
				if ( HasSpell( from, 48 ) && ToolBarUpdates.GetToolBarSetting( from, 49, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2288, 2288, 48, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Chain Lightning"); } }
				if ( HasSpell( from, 49 ) && ToolBarUpdates.GetToolBarSetting( from, 50, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2289, 2289, 49, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Field"); } }
				if ( HasSpell( from, 50 ) && ToolBarUpdates.GetToolBarSetting( from, 51, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2290, 2290, 50, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Flame Strike"); } }
				if ( HasSpell( from, 51 ) && ToolBarUpdates.GetToolBarSetting( from, 52, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2291, 2291, 51, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Gate Travel"); } }
				if ( HasSpell( from, 52 ) && ToolBarUpdates.GetToolBarSetting( from, 53, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2292, 2292, 52, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mana Vampire"); } }
				if ( HasSpell( from, 53 ) && ToolBarUpdates.GetToolBarSetting( from, 54, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2293, 2293, 53, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mass Dispel"); } }
				if ( HasSpell( from, 54 ) && ToolBarUpdates.GetToolBarSetting( from, 55, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2294, 2294, 54, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Meteor Swarm"); } }
				if ( HasSpell( from, 55 ) && ToolBarUpdates.GetToolBarSetting( from, 56, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2295, 2295, 55, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Polymorph"); } }
				if ( HasSpell( from, 56 ) && ToolBarUpdates.GetToolBarSetting( from, 57, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2296, 2296, 56, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Earthquake"); } }
				if ( HasSpell( from, 57 ) && ToolBarUpdates.GetToolBarSetting( from, 58, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2297, 2297, 57, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Vortex"); } }
				if ( HasSpell( from, 58 ) && ToolBarUpdates.GetToolBarSetting( from, 59, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2298, 2298, 58, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Resurrection"); } }
				if ( HasSpell( from, 59 ) && ToolBarUpdates.GetToolBarSetting( from, 60, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2299, 2299, 59, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Air Elemental"); } }
				if ( HasSpell( from, 60 ) && ToolBarUpdates.GetToolBarSetting( from, 61, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2300, 2300, 60, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Summon Daemon"); } }
				if ( HasSpell( from, 61 ) && ToolBarUpdates.GetToolBarSetting( from, 62, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2301, 2301, 61, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Earth Elemental"); } }
				if ( HasSpell( from, 62 ) && ToolBarUpdates.GetToolBarSetting( from, 63, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2302, 2302, 62, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fire Elemental"); } }
				if ( HasSpell( from, 63 ) && ToolBarUpdates.GetToolBarSetting( from, 64, "SetupBarsMage2" ) == 1){this.AddButton(5, dby, 2303, 2303, 63, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Water Elemental"); } }
			}			
			else			
			{			
				this.AddImage(0, 0, 2234, 0);		
				int dby = 50;		
				if ( HasSpell( from, 0 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2240, 2240, 99, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 1 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2241, 2241, 1, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 2 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2242, 2242, 2, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 3 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2243, 2243, 3, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 4 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2244, 2244, 4, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 5 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2245, 2245, 5, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 6 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2246, 2246, 6, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 7 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2247, 2247, 7, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 8 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2248, 2248, 8, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 9 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2249, 2249, 9, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 10 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2250, 2250, 10, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 11 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2251, 2251, 11, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 12 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2252, 2252, 12, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 13 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2253, 2253, 13, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 14 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2254, 2254, 14, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 15 ) && ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2255, 2255, 15, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 16 ) && ToolBarUpdates.GetToolBarSetting( from, 17, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2256, 2256, 16, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 17 ) && ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2257, 2257, 17, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 18 ) && ToolBarUpdates.GetToolBarSetting( from, 19, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2258, 2258, 18, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 19 ) && ToolBarUpdates.GetToolBarSetting( from, 20, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2259, 2259, 19, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 20 ) && ToolBarUpdates.GetToolBarSetting( from, 21, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2260, 2260, 20, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 21 ) && ToolBarUpdates.GetToolBarSetting( from, 22, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2261, 2261, 21, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 22 ) && ToolBarUpdates.GetToolBarSetting( from, 23, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2262, 2262, 22, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 23 ) && ToolBarUpdates.GetToolBarSetting( from, 24, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2263, 2263, 23, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 24 ) && ToolBarUpdates.GetToolBarSetting( from, 25, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2264, 2264, 24, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 25 ) && ToolBarUpdates.GetToolBarSetting( from, 26, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2265, 2265, 25, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 26 ) && ToolBarUpdates.GetToolBarSetting( from, 27, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2266, 2266, 26, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 27 ) && ToolBarUpdates.GetToolBarSetting( from, 28, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2267, 2267, 27, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 28 ) && ToolBarUpdates.GetToolBarSetting( from, 29, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2268, 2268, 28, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 29 ) && ToolBarUpdates.GetToolBarSetting( from, 30, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2269, 2269, 29, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 30 ) && ToolBarUpdates.GetToolBarSetting( from, 31, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2270, 2270, 30, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 31 ) && ToolBarUpdates.GetToolBarSetting( from, 32, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2271, 2271, 31, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 32 ) && ToolBarUpdates.GetToolBarSetting( from, 33, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2272, 2272, 32, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 33 ) && ToolBarUpdates.GetToolBarSetting( from, 34, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2273, 2273, 33, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 34 ) && ToolBarUpdates.GetToolBarSetting( from, 35, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2274, 2274, 34, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 35 ) && ToolBarUpdates.GetToolBarSetting( from, 36, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2275, 2275, 35, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 36 ) && ToolBarUpdates.GetToolBarSetting( from, 37, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2276, 2276, 36, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 37 ) && ToolBarUpdates.GetToolBarSetting( from, 38, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2277, 2277, 37, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 38 ) && ToolBarUpdates.GetToolBarSetting( from, 39, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2278, 2278, 38, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 39 ) && ToolBarUpdates.GetToolBarSetting( from, 40, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2279, 2279, 39, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 40 ) && ToolBarUpdates.GetToolBarSetting( from, 41, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2280, 2280, 40, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 41 ) && ToolBarUpdates.GetToolBarSetting( from, 42, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2281, 2281, 41, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 42 ) && ToolBarUpdates.GetToolBarSetting( from, 43, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2282, 2282, 42, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 43 ) && ToolBarUpdates.GetToolBarSetting( from, 44, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2283, 2283, 43, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 44 ) && ToolBarUpdates.GetToolBarSetting( from, 45, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2284, 2284, 44, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 45 ) && ToolBarUpdates.GetToolBarSetting( from, 46, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2285, 2285, 45, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 46 ) && ToolBarUpdates.GetToolBarSetting( from, 47, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2286, 2286, 46, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 47 ) && ToolBarUpdates.GetToolBarSetting( from, 48, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2287, 2287, 47, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 48 ) && ToolBarUpdates.GetToolBarSetting( from, 49, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2288, 2288, 48, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 49 ) && ToolBarUpdates.GetToolBarSetting( from, 50, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2289, 2289, 49, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 50 ) && ToolBarUpdates.GetToolBarSetting( from, 51, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2290, 2290, 50, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 51 ) && ToolBarUpdates.GetToolBarSetting( from, 52, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2291, 2291, 51, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 52 ) && ToolBarUpdates.GetToolBarSetting( from, 53, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2292, 2292, 52, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 53 ) && ToolBarUpdates.GetToolBarSetting( from, 54, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2293, 2293, 53, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 54 ) && ToolBarUpdates.GetToolBarSetting( from, 55, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2294, 2294, 54, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 55 ) && ToolBarUpdates.GetToolBarSetting( from, 56, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2295, 2295, 55, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 56 ) && ToolBarUpdates.GetToolBarSetting( from, 57, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2296, 2296, 56, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 57 ) && ToolBarUpdates.GetToolBarSetting( from, 58, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2297, 2297, 57, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 58 ) && ToolBarUpdates.GetToolBarSetting( from, 59, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2298, 2298, 58, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 59 ) && ToolBarUpdates.GetToolBarSetting( from, 60, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2299, 2299, 59, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 60 ) && ToolBarUpdates.GetToolBarSetting( from, 61, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2300, 2300, 60, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 61 ) && ToolBarUpdates.GetToolBarSetting( from, 62, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2301, 2301, 61, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 62 ) && ToolBarUpdates.GetToolBarSetting( from, 63, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2302, 2302, 62, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 63 ) && ToolBarUpdates.GetToolBarSetting( from, 64, "SetupBarsMage2" ) == 1){this.AddButton(dby, 5, 2303, 2303, 63, GumpButtonType.Reply, 1); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsMage2 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }
				case 99: { if ( HasSpell( from, 0 ) ) { new ClumsySpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 1: { if ( HasSpell( from, 1 ) ) { new CreateFoodSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 2: { if ( HasSpell( from, 2 ) ) { new FeeblemindSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 3: { if ( HasSpell( from, 3 ) ) { new HealSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 4: { if ( HasSpell( from, 4 ) ) { new MagicArrowSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 5: { if ( HasSpell( from, 5 ) ) { new NightSightSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 6: { if ( HasSpell( from, 6 ) ) { new ReactiveArmorSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 7: { if ( HasSpell( from, 7 ) ) { new WeakenSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 8: { if ( HasSpell( from, 8 ) ) { new AgilitySpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 9: { if ( HasSpell( from, 9 ) ) { new CunningSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 10: { if ( HasSpell( from, 10 ) ) { new CureSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 11: { if ( HasSpell( from, 11 ) ) { new HarmSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 12: { if ( HasSpell( from, 12 ) ) { new MagicTrapSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 13: { if ( HasSpell( from, 13 ) ) { new RemoveTrapSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 14: { if ( HasSpell( from, 14 ) ) { new ProtectionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 15: { if ( HasSpell( from, 15 ) ) { new StrengthSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 16: { if ( HasSpell( from, 16 ) ) { new BlessSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 17: { if ( HasSpell( from, 17 ) ) { new FireballSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 18: { if ( HasSpell( from, 18 ) ) { new MagicLockSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 19: { if ( HasSpell( from, 19 ) ) { new PoisonSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 20: { if ( HasSpell( from, 20 ) ) { new TelekinesisSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 21: { if ( HasSpell( from, 21 ) ) { new TeleportSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 22: { if ( HasSpell( from, 22 ) ) { new UnlockSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 23: { if ( HasSpell( from, 23 ) ) { new WallOfStoneSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 24: { if ( HasSpell( from, 24 ) ) { new ArchCureSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 25: { if ( HasSpell( from, 25 ) ) { new ArchProtectionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 26: { if ( HasSpell( from, 26 ) ) { new CurseSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 27: { if ( HasSpell( from, 27 ) ) { new FireFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 28: { if ( HasSpell( from, 28 ) ) { new GreaterHealSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 29: { if ( HasSpell( from, 29 ) ) { new LightningSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 30: { if ( HasSpell( from, 30 ) ) { new ManaDrainSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 31: { if ( HasSpell( from, 31 ) ) { new RecallSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 32: { if ( HasSpell( from, 32 ) ) { new BladeSpiritsSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 33: { if ( HasSpell( from, 33 ) ) { new DispelFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 34: { if ( HasSpell( from, 34 ) ) { new IncognitoSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 35: { if ( HasSpell( from, 35 ) ) { new MagicReflectSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 36: { if ( HasSpell( from, 36 ) ) { new MindBlastSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 37: { if ( HasSpell( from, 37 ) ) { new ParalyzeSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 38: { if ( HasSpell( from, 38 ) ) { new PoisonFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 39: { if ( HasSpell( from, 39 ) ) { new SummonCreatureSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 40: { if ( HasSpell( from, 40 ) ) { new DispelSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 41: { if ( HasSpell( from, 41 ) ) { new EnergyBoltSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 42: { if ( HasSpell( from, 42 ) ) { new ExplosionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 43: { if ( HasSpell( from, 43 ) ) { new InvisibilitySpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 44: { if ( HasSpell( from, 44 ) ) { new MarkSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 45: { if ( HasSpell( from, 45 ) ) { new MassCurseSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 46: { if ( HasSpell( from, 46 ) ) { new ParalyzeFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 47: { if ( HasSpell( from, 47 ) ) { new RevealSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 48: { if ( HasSpell( from, 48 ) ) { new ChainLightningSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 49: { if ( HasSpell( from, 49 ) ) { new EnergyFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 50: { if ( HasSpell( from, 50 ) ) { new FlameStrikeSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 51: { if ( HasSpell( from, 51 ) ) { new GateTravelSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 52: { if ( HasSpell( from, 52 ) ) { new ManaVampireSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 53: { if ( HasSpell( from, 53 ) ) { new MassDispelSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 54: { if ( HasSpell( from, 54 ) ) { new MeteorSwarmSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 55: { if ( HasSpell( from, 55 ) ) { new PolymorphSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 56: { if ( HasSpell( from, 56 ) ) { new EarthquakeSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 57: { if ( HasSpell( from, 57 ) ) { new EnergyVortexSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 58: { if ( HasSpell( from, 58 ) ) { new ResurrectionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 59: { if ( HasSpell( from, 59 ) ) { new AirElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 60: { if ( HasSpell( from, 60 ) ) { new SummonDaemonSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 61: { if ( HasSpell( from, 61 ) ) { new EarthElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 62: { if ( HasSpell( from, 62 ) ) { new FireElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
				case 63: { if ( HasSpell( from, 63 ) ) { new WaterElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage2( from ) ); } break; }
			}
		}
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Server.Gumps 
{
    public class SpellBarsMage3 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "magetool3", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "magetool3" )]
		[Description( "Opens Spell Bar For Mages - 3." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsMage3 ) );
			from.SendGump( new SpellBarsMage3( from ) );
        }
   
        public SpellBarsMage3 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 66, "SetupBarsMage3" ) > 0 )
			{
				this.AddImage(7, 0, 2234, 0);
				int dby = 53;

				if ( HasSpell( from, 0 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2240, 2240, 99, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Clumsy"); } }
				if ( HasSpell( from, 1 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2241, 2241, 1, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Create Food"); } }
				if ( HasSpell( from, 2 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2242, 2242, 2, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Feeblemind"); } }
				if ( HasSpell( from, 3 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2243, 2243, 3, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Heal"); } }
				if ( HasSpell( from, 4 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2244, 2244, 4, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Magic Arrow"); } }
				if ( HasSpell( from, 5 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2245, 2245, 5, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Night Sight"); } }
				if ( HasSpell( from, 6 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2246, 2246, 6, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Reactive Armor"); } }
				if ( HasSpell( from, 7 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2247, 2247, 7, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Weaken"); } }
				if ( HasSpell( from, 8 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2248, 2248, 8, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Agility"); } }
				if ( HasSpell( from, 9 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2249, 2249, 9, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Cunning"); } }
				if ( HasSpell( from, 10 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2250, 2250, 10, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Cure"); } }
				if ( HasSpell( from, 11 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2251, 2251, 11, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Harm"); } }
				if ( HasSpell( from, 12 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2252, 2252, 12, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Magic Trap"); } }
				if ( HasSpell( from, 13 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2253, 2253, 13, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Remove Trap"); } }
				if ( HasSpell( from, 14 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2254, 2254, 14, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Protection"); } }
				if ( HasSpell( from, 15 ) && ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2255, 2255, 15, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Strength"); } }
				if ( HasSpell( from, 16 ) && ToolBarUpdates.GetToolBarSetting( from, 17, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2256, 2256, 16, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Bless"); } }
				if ( HasSpell( from, 17 ) && ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2257, 2257, 17, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fireball"); } }
				if ( HasSpell( from, 18 ) && ToolBarUpdates.GetToolBarSetting( from, 19, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2258, 2258, 18, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"MagicLock"); } }
				if ( HasSpell( from, 19 ) && ToolBarUpdates.GetToolBarSetting( from, 20, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2259, 2259, 19, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Poison"); } }
				if ( HasSpell( from, 20 ) && ToolBarUpdates.GetToolBarSetting( from, 21, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2260, 2260, 20, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Telekinesis"); } }
				if ( HasSpell( from, 21 ) && ToolBarUpdates.GetToolBarSetting( from, 22, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2261, 2261, 21, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Teleport"); } }
				if ( HasSpell( from, 22 ) && ToolBarUpdates.GetToolBarSetting( from, 23, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2262, 2262, 22, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Unlock"); } }
				if ( HasSpell( from, 23 ) && ToolBarUpdates.GetToolBarSetting( from, 24, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2263, 2263, 23, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Wall Of Stone"); } }
				if ( HasSpell( from, 24 ) && ToolBarUpdates.GetToolBarSetting( from, 25, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2264, 2264, 24, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Arch Cure"); } }
				if ( HasSpell( from, 25 ) && ToolBarUpdates.GetToolBarSetting( from, 26, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2265, 2265, 25, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Arch Protection"); } }
				if ( HasSpell( from, 26 ) && ToolBarUpdates.GetToolBarSetting( from, 27, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2266, 2266, 26, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Curse"); } }
				if ( HasSpell( from, 27 ) && ToolBarUpdates.GetToolBarSetting( from, 28, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2267, 2267, 27, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fire Field"); } }
				if ( HasSpell( from, 28 ) && ToolBarUpdates.GetToolBarSetting( from, 29, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2268, 2268, 28, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Greater Heal"); } }
				if ( HasSpell( from, 29 ) && ToolBarUpdates.GetToolBarSetting( from, 30, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2269, 2269, 29, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Lightning"); } }
				if ( HasSpell( from, 30 ) && ToolBarUpdates.GetToolBarSetting( from, 31, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2270, 2270, 30, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mana Drain"); } }
				if ( HasSpell( from, 31 ) && ToolBarUpdates.GetToolBarSetting( from, 32, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2271, 2271, 31, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Recall"); } }
				if ( HasSpell( from, 32 ) && ToolBarUpdates.GetToolBarSetting( from, 33, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2272, 2272, 32, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Blade Spirits"); } }
				if ( HasSpell( from, 33 ) && ToolBarUpdates.GetToolBarSetting( from, 34, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2273, 2273, 33, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Dispel Field"); } }
				if ( HasSpell( from, 34 ) && ToolBarUpdates.GetToolBarSetting( from, 35, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2274, 2274, 34, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Incognito"); } }
				if ( HasSpell( from, 35 ) && ToolBarUpdates.GetToolBarSetting( from, 36, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2275, 2275, 35, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Magic Reflect"); } }
				if ( HasSpell( from, 36 ) && ToolBarUpdates.GetToolBarSetting( from, 37, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2276, 2276, 36, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mind Blast"); } }
				if ( HasSpell( from, 37 ) && ToolBarUpdates.GetToolBarSetting( from, 38, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2277, 2277, 37, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Paralyze"); } }
				if ( HasSpell( from, 38 ) && ToolBarUpdates.GetToolBarSetting( from, 39, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2278, 2278, 38, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Poison Field"); } }
				if ( HasSpell( from, 39 ) && ToolBarUpdates.GetToolBarSetting( from, 40, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2279, 2279, 39, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Summon Creature"); } }
				if ( HasSpell( from, 40 ) && ToolBarUpdates.GetToolBarSetting( from, 41, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2280, 2280, 40, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Dispel"); } }
				if ( HasSpell( from, 41 ) && ToolBarUpdates.GetToolBarSetting( from, 42, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2281, 2281, 41, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Bolt"); } }
				if ( HasSpell( from, 42 ) && ToolBarUpdates.GetToolBarSetting( from, 43, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2282, 2282, 42, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Explosion"); } }
				if ( HasSpell( from, 43 ) && ToolBarUpdates.GetToolBarSetting( from, 44, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2283, 2283, 43, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Invisibility"); } }
				if ( HasSpell( from, 44 ) && ToolBarUpdates.GetToolBarSetting( from, 45, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2284, 2284, 44, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mark"); } }
				if ( HasSpell( from, 45 ) && ToolBarUpdates.GetToolBarSetting( from, 46, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2285, 2285, 45, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mass Curse"); } }
				if ( HasSpell( from, 46 ) && ToolBarUpdates.GetToolBarSetting( from, 47, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2286, 2286, 46, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Paralyze Field"); } }
				if ( HasSpell( from, 47 ) && ToolBarUpdates.GetToolBarSetting( from, 48, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2287, 2287, 47, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Reveal"); } }
				if ( HasSpell( from, 48 ) && ToolBarUpdates.GetToolBarSetting( from, 49, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2288, 2288, 48, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Chain Lightning"); } }
				if ( HasSpell( from, 49 ) && ToolBarUpdates.GetToolBarSetting( from, 50, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2289, 2289, 49, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Field"); } }
				if ( HasSpell( from, 50 ) && ToolBarUpdates.GetToolBarSetting( from, 51, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2290, 2290, 50, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Flame Strike"); } }
				if ( HasSpell( from, 51 ) && ToolBarUpdates.GetToolBarSetting( from, 52, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2291, 2291, 51, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Gate Travel"); } }
				if ( HasSpell( from, 52 ) && ToolBarUpdates.GetToolBarSetting( from, 53, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2292, 2292, 52, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mana Vampire"); } }
				if ( HasSpell( from, 53 ) && ToolBarUpdates.GetToolBarSetting( from, 54, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2293, 2293, 53, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mass Dispel"); } }
				if ( HasSpell( from, 54 ) && ToolBarUpdates.GetToolBarSetting( from, 55, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2294, 2294, 54, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Meteor Swarm"); } }
				if ( HasSpell( from, 55 ) && ToolBarUpdates.GetToolBarSetting( from, 56, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2295, 2295, 55, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Polymorph"); } }
				if ( HasSpell( from, 56 ) && ToolBarUpdates.GetToolBarSetting( from, 57, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2296, 2296, 56, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Earthquake"); } }
				if ( HasSpell( from, 57 ) && ToolBarUpdates.GetToolBarSetting( from, 58, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2297, 2297, 57, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Vortex"); } }
				if ( HasSpell( from, 58 ) && ToolBarUpdates.GetToolBarSetting( from, 59, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2298, 2298, 58, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Resurrection"); } }
				if ( HasSpell( from, 59 ) && ToolBarUpdates.GetToolBarSetting( from, 60, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2299, 2299, 59, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Air Elemental"); } }
				if ( HasSpell( from, 60 ) && ToolBarUpdates.GetToolBarSetting( from, 61, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2300, 2300, 60, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Summon Daemon"); } }
				if ( HasSpell( from, 61 ) && ToolBarUpdates.GetToolBarSetting( from, 62, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2301, 2301, 61, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Earth Elemental"); } }
				if ( HasSpell( from, 62 ) && ToolBarUpdates.GetToolBarSetting( from, 63, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2302, 2302, 62, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fire Elemental"); } }
				if ( HasSpell( from, 63 ) && ToolBarUpdates.GetToolBarSetting( from, 64, "SetupBarsMage3" ) == 1){this.AddButton(5, dby, 2303, 2303, 63, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Water Elemental"); } }
			}			
			else			
			{			
				this.AddImage(0, 0, 2234, 0);		
				int dby = 50;		
				if ( HasSpell( from, 0 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2240, 2240, 99, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 1 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2241, 2241, 1, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 2 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2242, 2242, 2, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 3 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2243, 2243, 3, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 4 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2244, 2244, 4, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 5 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2245, 2245, 5, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 6 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2246, 2246, 6, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 7 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2247, 2247, 7, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 8 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2248, 2248, 8, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 9 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2249, 2249, 9, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 10 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2250, 2250, 10, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 11 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2251, 2251, 11, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 12 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2252, 2252, 12, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 13 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2253, 2253, 13, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 14 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2254, 2254, 14, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 15 ) && ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2255, 2255, 15, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 16 ) && ToolBarUpdates.GetToolBarSetting( from, 17, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2256, 2256, 16, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 17 ) && ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2257, 2257, 17, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 18 ) && ToolBarUpdates.GetToolBarSetting( from, 19, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2258, 2258, 18, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 19 ) && ToolBarUpdates.GetToolBarSetting( from, 20, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2259, 2259, 19, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 20 ) && ToolBarUpdates.GetToolBarSetting( from, 21, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2260, 2260, 20, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 21 ) && ToolBarUpdates.GetToolBarSetting( from, 22, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2261, 2261, 21, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 22 ) && ToolBarUpdates.GetToolBarSetting( from, 23, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2262, 2262, 22, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 23 ) && ToolBarUpdates.GetToolBarSetting( from, 24, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2263, 2263, 23, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 24 ) && ToolBarUpdates.GetToolBarSetting( from, 25, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2264, 2264, 24, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 25 ) && ToolBarUpdates.GetToolBarSetting( from, 26, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2265, 2265, 25, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 26 ) && ToolBarUpdates.GetToolBarSetting( from, 27, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2266, 2266, 26, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 27 ) && ToolBarUpdates.GetToolBarSetting( from, 28, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2267, 2267, 27, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 28 ) && ToolBarUpdates.GetToolBarSetting( from, 29, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2268, 2268, 28, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 29 ) && ToolBarUpdates.GetToolBarSetting( from, 30, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2269, 2269, 29, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 30 ) && ToolBarUpdates.GetToolBarSetting( from, 31, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2270, 2270, 30, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 31 ) && ToolBarUpdates.GetToolBarSetting( from, 32, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2271, 2271, 31, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 32 ) && ToolBarUpdates.GetToolBarSetting( from, 33, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2272, 2272, 32, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 33 ) && ToolBarUpdates.GetToolBarSetting( from, 34, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2273, 2273, 33, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 34 ) && ToolBarUpdates.GetToolBarSetting( from, 35, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2274, 2274, 34, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 35 ) && ToolBarUpdates.GetToolBarSetting( from, 36, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2275, 2275, 35, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 36 ) && ToolBarUpdates.GetToolBarSetting( from, 37, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2276, 2276, 36, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 37 ) && ToolBarUpdates.GetToolBarSetting( from, 38, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2277, 2277, 37, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 38 ) && ToolBarUpdates.GetToolBarSetting( from, 39, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2278, 2278, 38, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 39 ) && ToolBarUpdates.GetToolBarSetting( from, 40, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2279, 2279, 39, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 40 ) && ToolBarUpdates.GetToolBarSetting( from, 41, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2280, 2280, 40, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 41 ) && ToolBarUpdates.GetToolBarSetting( from, 42, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2281, 2281, 41, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 42 ) && ToolBarUpdates.GetToolBarSetting( from, 43, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2282, 2282, 42, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 43 ) && ToolBarUpdates.GetToolBarSetting( from, 44, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2283, 2283, 43, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 44 ) && ToolBarUpdates.GetToolBarSetting( from, 45, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2284, 2284, 44, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 45 ) && ToolBarUpdates.GetToolBarSetting( from, 46, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2285, 2285, 45, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 46 ) && ToolBarUpdates.GetToolBarSetting( from, 47, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2286, 2286, 46, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 47 ) && ToolBarUpdates.GetToolBarSetting( from, 48, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2287, 2287, 47, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 48 ) && ToolBarUpdates.GetToolBarSetting( from, 49, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2288, 2288, 48, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 49 ) && ToolBarUpdates.GetToolBarSetting( from, 50, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2289, 2289, 49, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 50 ) && ToolBarUpdates.GetToolBarSetting( from, 51, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2290, 2290, 50, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 51 ) && ToolBarUpdates.GetToolBarSetting( from, 52, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2291, 2291, 51, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 52 ) && ToolBarUpdates.GetToolBarSetting( from, 53, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2292, 2292, 52, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 53 ) && ToolBarUpdates.GetToolBarSetting( from, 54, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2293, 2293, 53, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 54 ) && ToolBarUpdates.GetToolBarSetting( from, 55, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2294, 2294, 54, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 55 ) && ToolBarUpdates.GetToolBarSetting( from, 56, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2295, 2295, 55, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 56 ) && ToolBarUpdates.GetToolBarSetting( from, 57, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2296, 2296, 56, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 57 ) && ToolBarUpdates.GetToolBarSetting( from, 58, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2297, 2297, 57, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 58 ) && ToolBarUpdates.GetToolBarSetting( from, 59, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2298, 2298, 58, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 59 ) && ToolBarUpdates.GetToolBarSetting( from, 60, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2299, 2299, 59, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 60 ) && ToolBarUpdates.GetToolBarSetting( from, 61, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2300, 2300, 60, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 61 ) && ToolBarUpdates.GetToolBarSetting( from, 62, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2301, 2301, 61, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 62 ) && ToolBarUpdates.GetToolBarSetting( from, 63, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2302, 2302, 62, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 63 ) && ToolBarUpdates.GetToolBarSetting( from, 64, "SetupBarsMage3" ) == 1){this.AddButton(dby, 5, 2303, 2303, 63, GumpButtonType.Reply, 1); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsMage3 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }
				case 99: { if ( HasSpell( from, 0 ) ) { new ClumsySpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 1: { if ( HasSpell( from, 1 ) ) { new CreateFoodSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 2: { if ( HasSpell( from, 2 ) ) { new FeeblemindSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 3: { if ( HasSpell( from, 3 ) ) { new HealSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 4: { if ( HasSpell( from, 4 ) ) { new MagicArrowSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 5: { if ( HasSpell( from, 5 ) ) { new NightSightSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 6: { if ( HasSpell( from, 6 ) ) { new ReactiveArmorSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 7: { if ( HasSpell( from, 7 ) ) { new WeakenSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 8: { if ( HasSpell( from, 8 ) ) { new AgilitySpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 9: { if ( HasSpell( from, 9 ) ) { new CunningSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 10: { if ( HasSpell( from, 10 ) ) { new CureSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 11: { if ( HasSpell( from, 11 ) ) { new HarmSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 12: { if ( HasSpell( from, 12 ) ) { new MagicTrapSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 13: { if ( HasSpell( from, 13 ) ) { new RemoveTrapSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 14: { if ( HasSpell( from, 14 ) ) { new ProtectionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 15: { if ( HasSpell( from, 15 ) ) { new StrengthSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 16: { if ( HasSpell( from, 16 ) ) { new BlessSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 17: { if ( HasSpell( from, 17 ) ) { new FireballSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 18: { if ( HasSpell( from, 18 ) ) { new MagicLockSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 19: { if ( HasSpell( from, 19 ) ) { new PoisonSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 20: { if ( HasSpell( from, 20 ) ) { new TelekinesisSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 21: { if ( HasSpell( from, 21 ) ) { new TeleportSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 22: { if ( HasSpell( from, 22 ) ) { new UnlockSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 23: { if ( HasSpell( from, 23 ) ) { new WallOfStoneSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 24: { if ( HasSpell( from, 24 ) ) { new ArchCureSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 25: { if ( HasSpell( from, 25 ) ) { new ArchProtectionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 26: { if ( HasSpell( from, 26 ) ) { new CurseSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 27: { if ( HasSpell( from, 27 ) ) { new FireFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 28: { if ( HasSpell( from, 28 ) ) { new GreaterHealSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 29: { if ( HasSpell( from, 29 ) ) { new LightningSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 30: { if ( HasSpell( from, 30 ) ) { new ManaDrainSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 31: { if ( HasSpell( from, 31 ) ) { new RecallSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 32: { if ( HasSpell( from, 32 ) ) { new BladeSpiritsSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 33: { if ( HasSpell( from, 33 ) ) { new DispelFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 34: { if ( HasSpell( from, 34 ) ) { new IncognitoSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 35: { if ( HasSpell( from, 35 ) ) { new MagicReflectSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 36: { if ( HasSpell( from, 36 ) ) { new MindBlastSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 37: { if ( HasSpell( from, 37 ) ) { new ParalyzeSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 38: { if ( HasSpell( from, 38 ) ) { new PoisonFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 39: { if ( HasSpell( from, 39 ) ) { new SummonCreatureSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 40: { if ( HasSpell( from, 40 ) ) { new DispelSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 41: { if ( HasSpell( from, 41 ) ) { new EnergyBoltSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 42: { if ( HasSpell( from, 42 ) ) { new ExplosionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 43: { if ( HasSpell( from, 43 ) ) { new InvisibilitySpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 44: { if ( HasSpell( from, 44 ) ) { new MarkSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 45: { if ( HasSpell( from, 45 ) ) { new MassCurseSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 46: { if ( HasSpell( from, 46 ) ) { new ParalyzeFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 47: { if ( HasSpell( from, 47 ) ) { new RevealSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 48: { if ( HasSpell( from, 48 ) ) { new ChainLightningSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 49: { if ( HasSpell( from, 49 ) ) { new EnergyFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 50: { if ( HasSpell( from, 50 ) ) { new FlameStrikeSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 51: { if ( HasSpell( from, 51 ) ) { new GateTravelSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 52: { if ( HasSpell( from, 52 ) ) { new ManaVampireSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 53: { if ( HasSpell( from, 53 ) ) { new MassDispelSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 54: { if ( HasSpell( from, 54 ) ) { new MeteorSwarmSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 55: { if ( HasSpell( from, 55 ) ) { new PolymorphSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 56: { if ( HasSpell( from, 56 ) ) { new EarthquakeSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 57: { if ( HasSpell( from, 57 ) ) { new EnergyVortexSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 58: { if ( HasSpell( from, 58 ) ) { new ResurrectionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 59: { if ( HasSpell( from, 59 ) ) { new AirElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 60: { if ( HasSpell( from, 60 ) ) { new SummonDaemonSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 61: { if ( HasSpell( from, 61 ) ) { new EarthElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 62: { if ( HasSpell( from, 62 ) ) { new FireElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
				case 63: { if ( HasSpell( from, 63 ) ) { new WaterElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage3( from ) ); } break; }
			}
		}
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Server.Gumps 
{
    public class SpellBarsMage4 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "magetool4", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "magetool4" )]
		[Description( "Opens Spell Bar For Mages - 4." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsMage4 ) );
			from.SendGump( new SpellBarsMage4( from ) );
        }
   
        public SpellBarsMage4 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 66, "SetupBarsMage4" ) > 0 )
			{
				this.AddImage(7, 0, 2234, 0);
				int dby = 53;

				if ( HasSpell( from, 0 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2240, 2240, 99, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Clumsy"); } }
				if ( HasSpell( from, 1 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2241, 2241, 1, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Create Food"); } }
				if ( HasSpell( from, 2 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2242, 2242, 2, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Feeblemind"); } }
				if ( HasSpell( from, 3 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2243, 2243, 3, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Heal"); } }
				if ( HasSpell( from, 4 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2244, 2244, 4, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Magic Arrow"); } }
				if ( HasSpell( from, 5 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2245, 2245, 5, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Night Sight"); } }
				if ( HasSpell( from, 6 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2246, 2246, 6, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Reactive Armor"); } }
				if ( HasSpell( from, 7 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2247, 2247, 7, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Weaken"); } }
				if ( HasSpell( from, 8 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2248, 2248, 8, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Agility"); } }
				if ( HasSpell( from, 9 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2249, 2249, 9, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Cunning"); } }
				if ( HasSpell( from, 10 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2250, 2250, 10, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Cure"); } }
				if ( HasSpell( from, 11 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2251, 2251, 11, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Harm"); } }
				if ( HasSpell( from, 12 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2252, 2252, 12, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Magic Trap"); } }
				if ( HasSpell( from, 13 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2253, 2253, 13, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Remove Trap"); } }
				if ( HasSpell( from, 14 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2254, 2254, 14, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Protection"); } }
				if ( HasSpell( from, 15 ) && ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2255, 2255, 15, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Strength"); } }
				if ( HasSpell( from, 16 ) && ToolBarUpdates.GetToolBarSetting( from, 17, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2256, 2256, 16, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Bless"); } }
				if ( HasSpell( from, 17 ) && ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2257, 2257, 17, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fireball"); } }
				if ( HasSpell( from, 18 ) && ToolBarUpdates.GetToolBarSetting( from, 19, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2258, 2258, 18, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"MagicLock"); } }
				if ( HasSpell( from, 19 ) && ToolBarUpdates.GetToolBarSetting( from, 20, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2259, 2259, 19, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Poison"); } }
				if ( HasSpell( from, 20 ) && ToolBarUpdates.GetToolBarSetting( from, 21, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2260, 2260, 20, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Telekinesis"); } }
				if ( HasSpell( from, 21 ) && ToolBarUpdates.GetToolBarSetting( from, 22, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2261, 2261, 21, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Teleport"); } }
				if ( HasSpell( from, 22 ) && ToolBarUpdates.GetToolBarSetting( from, 23, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2262, 2262, 22, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Unlock"); } }
				if ( HasSpell( from, 23 ) && ToolBarUpdates.GetToolBarSetting( from, 24, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2263, 2263, 23, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Wall Of Stone"); } }
				if ( HasSpell( from, 24 ) && ToolBarUpdates.GetToolBarSetting( from, 25, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2264, 2264, 24, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Arch Cure"); } }
				if ( HasSpell( from, 25 ) && ToolBarUpdates.GetToolBarSetting( from, 26, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2265, 2265, 25, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Arch Protection"); } }
				if ( HasSpell( from, 26 ) && ToolBarUpdates.GetToolBarSetting( from, 27, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2266, 2266, 26, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Curse"); } }
				if ( HasSpell( from, 27 ) && ToolBarUpdates.GetToolBarSetting( from, 28, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2267, 2267, 27, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fire Field"); } }
				if ( HasSpell( from, 28 ) && ToolBarUpdates.GetToolBarSetting( from, 29, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2268, 2268, 28, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Greater Heal"); } }
				if ( HasSpell( from, 29 ) && ToolBarUpdates.GetToolBarSetting( from, 30, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2269, 2269, 29, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Lightning"); } }
				if ( HasSpell( from, 30 ) && ToolBarUpdates.GetToolBarSetting( from, 31, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2270, 2270, 30, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mana Drain"); } }
				if ( HasSpell( from, 31 ) && ToolBarUpdates.GetToolBarSetting( from, 32, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2271, 2271, 31, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Recall"); } }
				if ( HasSpell( from, 32 ) && ToolBarUpdates.GetToolBarSetting( from, 33, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2272, 2272, 32, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Blade Spirits"); } }
				if ( HasSpell( from, 33 ) && ToolBarUpdates.GetToolBarSetting( from, 34, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2273, 2273, 33, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Dispel Field"); } }
				if ( HasSpell( from, 34 ) && ToolBarUpdates.GetToolBarSetting( from, 35, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2274, 2274, 34, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Incognito"); } }
				if ( HasSpell( from, 35 ) && ToolBarUpdates.GetToolBarSetting( from, 36, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2275, 2275, 35, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Magic Reflect"); } }
				if ( HasSpell( from, 36 ) && ToolBarUpdates.GetToolBarSetting( from, 37, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2276, 2276, 36, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mind Blast"); } }
				if ( HasSpell( from, 37 ) && ToolBarUpdates.GetToolBarSetting( from, 38, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2277, 2277, 37, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Paralyze"); } }
				if ( HasSpell( from, 38 ) && ToolBarUpdates.GetToolBarSetting( from, 39, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2278, 2278, 38, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Poison Field"); } }
				if ( HasSpell( from, 39 ) && ToolBarUpdates.GetToolBarSetting( from, 40, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2279, 2279, 39, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Summon Creature"); } }
				if ( HasSpell( from, 40 ) && ToolBarUpdates.GetToolBarSetting( from, 41, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2280, 2280, 40, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Dispel"); } }
				if ( HasSpell( from, 41 ) && ToolBarUpdates.GetToolBarSetting( from, 42, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2281, 2281, 41, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Bolt"); } }
				if ( HasSpell( from, 42 ) && ToolBarUpdates.GetToolBarSetting( from, 43, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2282, 2282, 42, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Explosion"); } }
				if ( HasSpell( from, 43 ) && ToolBarUpdates.GetToolBarSetting( from, 44, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2283, 2283, 43, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Invisibility"); } }
				if ( HasSpell( from, 44 ) && ToolBarUpdates.GetToolBarSetting( from, 45, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2284, 2284, 44, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mark"); } }
				if ( HasSpell( from, 45 ) && ToolBarUpdates.GetToolBarSetting( from, 46, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2285, 2285, 45, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mass Curse"); } }
				if ( HasSpell( from, 46 ) && ToolBarUpdates.GetToolBarSetting( from, 47, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2286, 2286, 46, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Paralyze Field"); } }
				if ( HasSpell( from, 47 ) && ToolBarUpdates.GetToolBarSetting( from, 48, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2287, 2287, 47, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Reveal"); } }
				if ( HasSpell( from, 48 ) && ToolBarUpdates.GetToolBarSetting( from, 49, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2288, 2288, 48, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Chain Lightning"); } }
				if ( HasSpell( from, 49 ) && ToolBarUpdates.GetToolBarSetting( from, 50, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2289, 2289, 49, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Field"); } }
				if ( HasSpell( from, 50 ) && ToolBarUpdates.GetToolBarSetting( from, 51, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2290, 2290, 50, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Flame Strike"); } }
				if ( HasSpell( from, 51 ) && ToolBarUpdates.GetToolBarSetting( from, 52, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2291, 2291, 51, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Gate Travel"); } }
				if ( HasSpell( from, 52 ) && ToolBarUpdates.GetToolBarSetting( from, 53, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2292, 2292, 52, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mana Vampire"); } }
				if ( HasSpell( from, 53 ) && ToolBarUpdates.GetToolBarSetting( from, 54, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2293, 2293, 53, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mass Dispel"); } }
				if ( HasSpell( from, 54 ) && ToolBarUpdates.GetToolBarSetting( from, 55, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2294, 2294, 54, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Meteor Swarm"); } }
				if ( HasSpell( from, 55 ) && ToolBarUpdates.GetToolBarSetting( from, 56, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2295, 2295, 55, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Polymorph"); } }
				if ( HasSpell( from, 56 ) && ToolBarUpdates.GetToolBarSetting( from, 57, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2296, 2296, 56, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Earthquake"); } }
				if ( HasSpell( from, 57 ) && ToolBarUpdates.GetToolBarSetting( from, 58, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2297, 2297, 57, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Vortex"); } }
				if ( HasSpell( from, 58 ) && ToolBarUpdates.GetToolBarSetting( from, 59, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2298, 2298, 58, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Resurrection"); } }
				if ( HasSpell( from, 59 ) && ToolBarUpdates.GetToolBarSetting( from, 60, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2299, 2299, 59, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Air Elemental"); } }
				if ( HasSpell( from, 60 ) && ToolBarUpdates.GetToolBarSetting( from, 61, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2300, 2300, 60, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Summon Daemon"); } }
				if ( HasSpell( from, 61 ) && ToolBarUpdates.GetToolBarSetting( from, 62, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2301, 2301, 61, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Earth Elemental"); } }
				if ( HasSpell( from, 62 ) && ToolBarUpdates.GetToolBarSetting( from, 63, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2302, 2302, 62, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fire Elemental"); } }
				if ( HasSpell( from, 63 ) && ToolBarUpdates.GetToolBarSetting( from, 64, "SetupBarsMage4" ) == 1){this.AddButton(5, dby, 2303, 2303, 63, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 65, "SetupBarsMage4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Water Elemental"); } }
			}			
			else			
			{			
				this.AddImage(0, 0, 2234, 0);		
				int dby = 50;		
				if ( HasSpell( from, 0 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2240, 2240, 99, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 1 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2241, 2241, 1, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 2 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2242, 2242, 2, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 3 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2243, 2243, 3, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 4 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2244, 2244, 4, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 5 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2245, 2245, 5, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 6 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2246, 2246, 6, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 7 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2247, 2247, 7, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 8 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2248, 2248, 8, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 9 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2249, 2249, 9, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 10 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2250, 2250, 10, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 11 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2251, 2251, 11, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 12 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2252, 2252, 12, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 13 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2253, 2253, 13, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 14 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2254, 2254, 14, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 15 ) && ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2255, 2255, 15, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 16 ) && ToolBarUpdates.GetToolBarSetting( from, 17, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2256, 2256, 16, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 17 ) && ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2257, 2257, 17, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 18 ) && ToolBarUpdates.GetToolBarSetting( from, 19, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2258, 2258, 18, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 19 ) && ToolBarUpdates.GetToolBarSetting( from, 20, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2259, 2259, 19, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 20 ) && ToolBarUpdates.GetToolBarSetting( from, 21, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2260, 2260, 20, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 21 ) && ToolBarUpdates.GetToolBarSetting( from, 22, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2261, 2261, 21, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 22 ) && ToolBarUpdates.GetToolBarSetting( from, 23, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2262, 2262, 22, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 23 ) && ToolBarUpdates.GetToolBarSetting( from, 24, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2263, 2263, 23, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 24 ) && ToolBarUpdates.GetToolBarSetting( from, 25, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2264, 2264, 24, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 25 ) && ToolBarUpdates.GetToolBarSetting( from, 26, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2265, 2265, 25, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 26 ) && ToolBarUpdates.GetToolBarSetting( from, 27, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2266, 2266, 26, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 27 ) && ToolBarUpdates.GetToolBarSetting( from, 28, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2267, 2267, 27, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 28 ) && ToolBarUpdates.GetToolBarSetting( from, 29, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2268, 2268, 28, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 29 ) && ToolBarUpdates.GetToolBarSetting( from, 30, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2269, 2269, 29, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 30 ) && ToolBarUpdates.GetToolBarSetting( from, 31, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2270, 2270, 30, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 31 ) && ToolBarUpdates.GetToolBarSetting( from, 32, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2271, 2271, 31, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 32 ) && ToolBarUpdates.GetToolBarSetting( from, 33, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2272, 2272, 32, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 33 ) && ToolBarUpdates.GetToolBarSetting( from, 34, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2273, 2273, 33, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 34 ) && ToolBarUpdates.GetToolBarSetting( from, 35, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2274, 2274, 34, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 35 ) && ToolBarUpdates.GetToolBarSetting( from, 36, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2275, 2275, 35, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 36 ) && ToolBarUpdates.GetToolBarSetting( from, 37, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2276, 2276, 36, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 37 ) && ToolBarUpdates.GetToolBarSetting( from, 38, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2277, 2277, 37, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 38 ) && ToolBarUpdates.GetToolBarSetting( from, 39, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2278, 2278, 38, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 39 ) && ToolBarUpdates.GetToolBarSetting( from, 40, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2279, 2279, 39, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 40 ) && ToolBarUpdates.GetToolBarSetting( from, 41, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2280, 2280, 40, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 41 ) && ToolBarUpdates.GetToolBarSetting( from, 42, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2281, 2281, 41, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 42 ) && ToolBarUpdates.GetToolBarSetting( from, 43, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2282, 2282, 42, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 43 ) && ToolBarUpdates.GetToolBarSetting( from, 44, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2283, 2283, 43, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 44 ) && ToolBarUpdates.GetToolBarSetting( from, 45, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2284, 2284, 44, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 45 ) && ToolBarUpdates.GetToolBarSetting( from, 46, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2285, 2285, 45, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 46 ) && ToolBarUpdates.GetToolBarSetting( from, 47, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2286, 2286, 46, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 47 ) && ToolBarUpdates.GetToolBarSetting( from, 48, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2287, 2287, 47, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 48 ) && ToolBarUpdates.GetToolBarSetting( from, 49, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2288, 2288, 48, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 49 ) && ToolBarUpdates.GetToolBarSetting( from, 50, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2289, 2289, 49, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 50 ) && ToolBarUpdates.GetToolBarSetting( from, 51, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2290, 2290, 50, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 51 ) && ToolBarUpdates.GetToolBarSetting( from, 52, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2291, 2291, 51, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 52 ) && ToolBarUpdates.GetToolBarSetting( from, 53, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2292, 2292, 52, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 53 ) && ToolBarUpdates.GetToolBarSetting( from, 54, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2293, 2293, 53, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 54 ) && ToolBarUpdates.GetToolBarSetting( from, 55, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2294, 2294, 54, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 55 ) && ToolBarUpdates.GetToolBarSetting( from, 56, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2295, 2295, 55, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 56 ) && ToolBarUpdates.GetToolBarSetting( from, 57, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2296, 2296, 56, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 57 ) && ToolBarUpdates.GetToolBarSetting( from, 58, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2297, 2297, 57, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 58 ) && ToolBarUpdates.GetToolBarSetting( from, 59, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2298, 2298, 58, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 59 ) && ToolBarUpdates.GetToolBarSetting( from, 60, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2299, 2299, 59, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 60 ) && ToolBarUpdates.GetToolBarSetting( from, 61, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2300, 2300, 60, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 61 ) && ToolBarUpdates.GetToolBarSetting( from, 62, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2301, 2301, 61, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 62 ) && ToolBarUpdates.GetToolBarSetting( from, 63, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2302, 2302, 62, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 63 ) && ToolBarUpdates.GetToolBarSetting( from, 64, "SetupBarsMage4" ) == 1){this.AddButton(dby, 5, 2303, 2303, 63, GumpButtonType.Reply, 1); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsMage4 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }
				case 99: { if ( HasSpell( from, 0 ) ) { new ClumsySpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 1: { if ( HasSpell( from, 1 ) ) { new CreateFoodSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 2: { if ( HasSpell( from, 2 ) ) { new FeeblemindSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 3: { if ( HasSpell( from, 3 ) ) { new HealSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 4: { if ( HasSpell( from, 4 ) ) { new MagicArrowSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 5: { if ( HasSpell( from, 5 ) ) { new NightSightSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 6: { if ( HasSpell( from, 6 ) ) { new ReactiveArmorSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 7: { if ( HasSpell( from, 7 ) ) { new WeakenSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 8: { if ( HasSpell( from, 8 ) ) { new AgilitySpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 9: { if ( HasSpell( from, 9 ) ) { new CunningSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 10: { if ( HasSpell( from, 10 ) ) { new CureSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 11: { if ( HasSpell( from, 11 ) ) { new HarmSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 12: { if ( HasSpell( from, 12 ) ) { new MagicTrapSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 13: { if ( HasSpell( from, 13 ) ) { new RemoveTrapSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 14: { if ( HasSpell( from, 14 ) ) { new ProtectionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 15: { if ( HasSpell( from, 15 ) ) { new StrengthSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 16: { if ( HasSpell( from, 16 ) ) { new BlessSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 17: { if ( HasSpell( from, 17 ) ) { new FireballSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 18: { if ( HasSpell( from, 18 ) ) { new MagicLockSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 19: { if ( HasSpell( from, 19 ) ) { new PoisonSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 20: { if ( HasSpell( from, 20 ) ) { new TelekinesisSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 21: { if ( HasSpell( from, 21 ) ) { new TeleportSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 22: { if ( HasSpell( from, 22 ) ) { new UnlockSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 23: { if ( HasSpell( from, 23 ) ) { new WallOfStoneSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 24: { if ( HasSpell( from, 24 ) ) { new ArchCureSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 25: { if ( HasSpell( from, 25 ) ) { new ArchProtectionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 26: { if ( HasSpell( from, 26 ) ) { new CurseSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 27: { if ( HasSpell( from, 27 ) ) { new FireFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 28: { if ( HasSpell( from, 28 ) ) { new GreaterHealSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 29: { if ( HasSpell( from, 29 ) ) { new LightningSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 30: { if ( HasSpell( from, 30 ) ) { new ManaDrainSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 31: { if ( HasSpell( from, 31 ) ) { new RecallSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 32: { if ( HasSpell( from, 32 ) ) { new BladeSpiritsSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 33: { if ( HasSpell( from, 33 ) ) { new DispelFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 34: { if ( HasSpell( from, 34 ) ) { new IncognitoSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 35: { if ( HasSpell( from, 35 ) ) { new MagicReflectSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 36: { if ( HasSpell( from, 36 ) ) { new MindBlastSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 37: { if ( HasSpell( from, 37 ) ) { new ParalyzeSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 38: { if ( HasSpell( from, 38 ) ) { new PoisonFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 39: { if ( HasSpell( from, 39 ) ) { new SummonCreatureSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 40: { if ( HasSpell( from, 40 ) ) { new DispelSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 41: { if ( HasSpell( from, 41 ) ) { new EnergyBoltSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 42: { if ( HasSpell( from, 42 ) ) { new ExplosionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 43: { if ( HasSpell( from, 43 ) ) { new InvisibilitySpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 44: { if ( HasSpell( from, 44 ) ) { new MarkSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 45: { if ( HasSpell( from, 45 ) ) { new MassCurseSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 46: { if ( HasSpell( from, 46 ) ) { new ParalyzeFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 47: { if ( HasSpell( from, 47 ) ) { new RevealSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 48: { if ( HasSpell( from, 48 ) ) { new ChainLightningSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 49: { if ( HasSpell( from, 49 ) ) { new EnergyFieldSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 50: { if ( HasSpell( from, 50 ) ) { new FlameStrikeSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 51: { if ( HasSpell( from, 51 ) ) { new GateTravelSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 52: { if ( HasSpell( from, 52 ) ) { new ManaVampireSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 53: { if ( HasSpell( from, 53 ) ) { new MassDispelSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 54: { if ( HasSpell( from, 54 ) ) { new MeteorSwarmSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 55: { if ( HasSpell( from, 55 ) ) { new PolymorphSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 56: { if ( HasSpell( from, 56 ) ) { new EarthquakeSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 57: { if ( HasSpell( from, 57 ) ) { new EnergyVortexSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 58: { if ( HasSpell( from, 58 ) ) { new ResurrectionSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 59: { if ( HasSpell( from, 59 ) ) { new AirElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 60: { if ( HasSpell( from, 60 ) ) { new SummonDaemonSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 61: { if ( HasSpell( from, 61 ) ) { new EarthElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 62: { if ( HasSpell( from, 62 ) ) { new FireElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
				case 63: { if ( HasSpell( from, 63 ) ) { new WaterElementalSpell( from, null ).Cast(); from.SendGump( new SpellBarsMage4( from ) ); } break; }
			}
		}
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Server.Gumps 
{
    public class SpellBarsNecro1 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "necrotool1", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "necrotool1" )]
		[Description( "Opens Spell Bar For Necromancers - 1." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsNecro1 ) );
			from.SendGump( new SpellBarsNecro1( from ) );
        }
   
        public SpellBarsNecro1 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 19, "SetupBarsNecro1" ) > 0 )
			{
				this.AddImage(7, 0, 11011, 0);
				int dby = 53;

				if ( HasSpell( from, 100 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20480,20480, 1, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Animate Dead"); } }
				if ( HasSpell( from, 101 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20481,20481, 2, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Blood Oath"); } }
				if ( HasSpell( from, 102 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20482,20482, 3, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Corpse Skin"); } }
				if ( HasSpell( from, 103 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20483,20483, 4, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Curse Weapon"); } }
				if ( HasSpell( from, 104 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20484,20484, 5, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Evil Omen"); } }
				if ( HasSpell( from, 105 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20485,20485, 6, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Horrific Beast"); } }
				if ( HasSpell( from, 106 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20486,20486, 7, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Lich Form"); } }
				if ( HasSpell( from, 107 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20487,20487, 8, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mind Rot"); } }
				if ( HasSpell( from, 108 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20488,20488, 9, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Pain Spike"); } }
				if ( HasSpell( from, 109 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20489,20489, 10, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Poison Strike"); } }
				if ( HasSpell( from, 110 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20490,20490, 11, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Strangle"); } }
				if ( HasSpell( from, 111 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20491,20491, 12, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Summon Familiar"); } }
				if ( HasSpell( from, 112 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20492,20492, 13, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Vampiric Embrace"); } }
				if ( HasSpell( from, 113 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20493,20493, 14, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Vengeful Spirit"); } }
				if ( HasSpell( from, 114 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20494,20494, 15, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Wither"); } }
				if ( HasSpell( from, 115 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20495,20495, 16, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Wraith Form"); } }
				if ( HasSpell( from, 116 ) && ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsNecro1" ) == 1){this.AddButton(5, dby, 20496,20496, 17, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Exorcism"); } }
			}
			else			
			{			
				this.AddImage(0, 0, 11011, 0);		
				int dby = 50;		

				if ( HasSpell( from, 100 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20480,20480, 1, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 101 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20481,20481, 2, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 102 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20482,20482, 3, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 103 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20483,20483, 4, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 104 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20484,20484, 5, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 105 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20485,20485, 6, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 106 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20486,20486, 7, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 107 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20487,20487, 8, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 108 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20488,20488, 9, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 109 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20489,20489, 10, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 110 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20490,20490, 11, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 111 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20491,20491, 12, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 112 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20492,20492, 13, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 113 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20493,20493, 14, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 114 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20494,20494, 15, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 115 ) && ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20495,20495, 16, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 116 ) && ToolBarUpdates.GetToolBarSetting( from, 17, "SetupBarsNecro1" ) == 1){this.AddButton(dby, 5, 20496,20496, 17, GumpButtonType.Reply, 1); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsNecro1 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }
				case 1: { if ( HasSpell( from, 100 ) ) { new AnimateDeadSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 2: { if ( HasSpell( from, 101 ) ) { new BloodOathSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 3: { if ( HasSpell( from, 102 ) ) { new CorpseSkinSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 4: { if ( HasSpell( from, 103 ) ) { new CurseWeaponSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 5: { if ( HasSpell( from, 104 ) ) { new EvilOmenSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 6: { if ( HasSpell( from, 105 ) ) { new HorrificBeastSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 7: { if ( HasSpell( from, 106 ) ) { new LichFormSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 8: { if ( HasSpell( from, 107 ) ) { new MindRotSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 9: { if ( HasSpell( from, 108 ) ) { new PainSpikeSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 10: { if ( HasSpell( from, 109 ) ) { new PoisonStrikeSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 11: { if ( HasSpell( from, 110 ) ) { new StrangleSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 12: { if ( HasSpell( from, 111 ) ) { new SummonFamiliarSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 13: { if ( HasSpell( from, 112 ) ) { new VampiricEmbraceSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 14: { if ( HasSpell( from, 113 ) ) { new VengefulSpiritSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 15: { if ( HasSpell( from, 114 ) ) { new WitherSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 16: { if ( HasSpell( from, 115 ) ) { new WraithFormSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
				case 17: { if ( HasSpell( from, 116 ) ) { new ExorcismSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro1( from ) ); } break; }
			}
		}
    }
}


/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Server.Gumps 
{
    public class SpellBarsNecro2 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "necrotool2", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "necrotool2" )]
		[Description( "Opens Spell Bar For Necromancers - 2." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsNecro2 ) );
			from.SendGump( new SpellBarsNecro2( from ) );
        }
   
        public SpellBarsNecro2 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 19, "SetupBarsNecro2" ) > 0 )
			{
				this.AddImage(7, 0, 11011, 0);
				int dby = 53;

				if ( HasSpell( from, 100 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20480,20480, 1, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Animate Dead"); } }
				if ( HasSpell( from, 101 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20481,20481, 2, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Blood Oath"); } }
				if ( HasSpell( from, 102 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20482,20482, 3, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Corpse Skin"); } }
				if ( HasSpell( from, 103 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20483,20483, 4, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Curse Weapon"); } }
				if ( HasSpell( from, 104 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20484,20484, 5, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Evil Omen"); } }
				if ( HasSpell( from, 105 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20485,20485, 6, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Horrific Beast"); } }
				if ( HasSpell( from, 106 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20486,20486, 7, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Lich Form"); } }
				if ( HasSpell( from, 107 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20487,20487, 8, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mind Rot"); } }
				if ( HasSpell( from, 108 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20488,20488, 9, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Pain Spike"); } }
				if ( HasSpell( from, 109 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20489,20489, 10, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Poison Strike"); } }
				if ( HasSpell( from, 110 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20490,20490, 11, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Strangle"); } }
				if ( HasSpell( from, 111 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20491,20491, 12, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Summon Familiar"); } }
				if ( HasSpell( from, 112 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20492,20492, 13, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Vampiric Embrace"); } }
				if ( HasSpell( from, 113 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20493,20493, 14, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Vengeful Spirit"); } }
				if ( HasSpell( from, 114 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20494,20494, 15, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Wither"); } }
				if ( HasSpell( from, 115 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20495,20495, 16, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Wraith Form"); } }
				if ( HasSpell( from, 116 ) && ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsNecro2" ) == 1){this.AddButton(5, dby, 20496,20496, 17, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsNecro2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Exorcism"); } }
			}
			else			
			{			
				this.AddImage(0, 0, 11011, 0);		
				int dby = 50;		

				if ( HasSpell( from, 100 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20480,20480, 1, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 101 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20481,20481, 2, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 102 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20482,20482, 3, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 103 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20483,20483, 4, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 104 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20484,20484, 5, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 105 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20485,20485, 6, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 106 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20486,20486, 7, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 107 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20487,20487, 8, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 108 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20488,20488, 9, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 109 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20489,20489, 10, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 110 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20490,20490, 11, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 111 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20491,20491, 12, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 112 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20492,20492, 13, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 113 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20493,20493, 14, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 114 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20494,20494, 15, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 115 ) && ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20495,20495, 16, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 116 ) && ToolBarUpdates.GetToolBarSetting( from, 17, "SetupBarsNecro2" ) == 1){this.AddButton(dby, 5, 20496,20496, 17, GumpButtonType.Reply, 1); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsNecro2 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }
				case 1: { if ( HasSpell( from, 100 ) ) { new AnimateDeadSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 2: { if ( HasSpell( from, 101 ) ) { new BloodOathSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 3: { if ( HasSpell( from, 102 ) ) { new CorpseSkinSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 4: { if ( HasSpell( from, 103 ) ) { new CurseWeaponSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 5: { if ( HasSpell( from, 104 ) ) { new EvilOmenSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 6: { if ( HasSpell( from, 105 ) ) { new HorrificBeastSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 7: { if ( HasSpell( from, 106 ) ) { new LichFormSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 8: { if ( HasSpell( from, 107 ) ) { new MindRotSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 9: { if ( HasSpell( from, 108 ) ) { new PainSpikeSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 10: { if ( HasSpell( from, 109 ) ) { new PoisonStrikeSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 11: { if ( HasSpell( from, 110 ) ) { new StrangleSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 12: { if ( HasSpell( from, 111 ) ) { new SummonFamiliarSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 13: { if ( HasSpell( from, 112 ) ) { new VampiricEmbraceSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 14: { if ( HasSpell( from, 113 ) ) { new VengefulSpiritSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 15: { if ( HasSpell( from, 114 ) ) { new WitherSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 16: { if ( HasSpell( from, 115 ) ) { new WraithFormSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
				case 17: { if ( HasSpell( from, 116 ) ) { new ExorcismSpell( from, null ).Cast(); from.SendGump( new SpellBarsNecro2( from ) ); } break; }
			}
		}
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Server.Gumps 
{
    public class SpellBarsChivalry1 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "chivalrytool1", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "chivalrytool1" )]
		[Description( "Opens Spell Bar For Knights - 1." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsChivalry1 ) );
			from.SendGump( new SpellBarsChivalry1( from ) );
        }
   
        public SpellBarsChivalry1 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsChivalry1" ) > 0 )
			{
				this.AddImage(7, 0, 11012, 0);
				int dby = 53;

				if ( HasSpell( from, 200 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsChivalry1" ) == 1){this.AddButton(5, dby, 20736,20736, 1, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Cleanse by Fire"); } }
				if ( HasSpell( from, 201 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsChivalry1" ) == 1){this.AddButton(5, dby, 20737,20737, 2, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Close Wounds"); } }
				if ( HasSpell( from, 202 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsChivalry1" ) == 1){this.AddButton(5, dby, 20738,20738, 3, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Consecrate Weapon"); } }
				if ( HasSpell( from, 203 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsChivalry1" ) == 1){this.AddButton(5, dby, 20739,20739, 4, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Dispel Evil"); } }
				if ( HasSpell( from, 204 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsChivalry1" ) == 1){this.AddButton(5, dby, 20740,20740, 5, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Divine Fury"); } }
				if ( HasSpell( from, 205 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsChivalry1" ) == 1){this.AddButton(5, dby, 20741,20741, 6, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Enemy of One"); } }
				if ( HasSpell( from, 206 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsChivalry1" ) == 1){this.AddButton(5, dby, 20742,20742, 7, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Holy Light"); } }
				if ( HasSpell( from, 207 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsChivalry1" ) == 1){this.AddButton(5, dby, 20743,20743, 8, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Noble Sacrifice"); } }
				if ( HasSpell( from, 208 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsChivalry1" ) == 1){this.AddButton(5, dby, 20744,20744, 9, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Remove Curse"); } }
				if ( HasSpell( from, 209 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsChivalry1" ) == 1){this.AddButton(5, dby, 20745,20745, 10, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Sacred Journey"); } }
			}
			else			
			{			
				this.AddImage(0, 0, 11012, 0);		
				int dby = 50;		

				if ( HasSpell( from, 200 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsChivalry1" ) == 1){this.AddButton(dby, 5, 20736,20736, 1, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 201 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsChivalry1" ) == 1){this.AddButton(dby, 5, 20737,20737, 2, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 202 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsChivalry1" ) == 1){this.AddButton(dby, 5, 20738,20738, 3, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 203 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsChivalry1" ) == 1){this.AddButton(dby, 5, 20739,20739, 4, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 204 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsChivalry1" ) == 1){this.AddButton(dby, 5, 20740,20740, 5, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 205 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsChivalry1" ) == 1){this.AddButton(dby, 5, 20741,20741, 6, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 206 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsChivalry1" ) == 1){this.AddButton(dby, 5, 20742,20742, 7, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 207 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsChivalry1" ) == 1){this.AddButton(dby, 5, 20743,20743, 8, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 208 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsChivalry1" ) == 1){this.AddButton(dby, 5, 20744,20744, 9, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 209 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsChivalry1" ) == 1){this.AddButton(dby, 5, 20745,20745, 10, GumpButtonType.Reply, 1); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsChivalry1 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }
				case 1 : { if ( HasSpell( from, 200 ) ) { new CleanseByFireSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry1( from ) ); } break; }
				case 2 : { if ( HasSpell( from, 201 ) ) { new CloseWoundsSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry1( from ) ); } break; }
				case 3 : { if ( HasSpell( from, 202 ) ) { new ConsecrateWeaponSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry1( from ) ); } break; }
				case 4 : { if ( HasSpell( from, 203 ) ) { new DispelEvilSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry1( from ) ); } break; }
				case 5 : { if ( HasSpell( from, 204 ) ) { new DivineFurySpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry1( from ) ); } break; }
				case 6 : { if ( HasSpell( from, 205 ) ) { new EnemyOfOneSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry1( from ) ); } break; }
				case 7 : { if ( HasSpell( from, 206 ) ) { new HolyLightSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry1( from ) ); } break; }
				case 8 : { if ( HasSpell( from, 207 ) ) { new NobleSacrificeSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry1( from ) ); } break; }
				case 9 : { if ( HasSpell( from, 208 ) ) { new RemoveCurseSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry1( from ) ); } break; }
				case 10 : { if ( HasSpell( from, 209 ) ) { new SacredJourneySpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry1( from ) ); } break; }
			}
		}
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Server.Gumps 
{
    public class SpellBarsChivalry2 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "chivalrytool2", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "chivalrytool2" )]
		[Description( "Opens Spell Bar For Knights - 2." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsChivalry2 ) );
			from.SendGump( new SpellBarsChivalry2( from ) );
        }
   
        public SpellBarsChivalry2 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsChivalry2" ) > 0 )
			{
				this.AddImage(7, 0, 11012, 0);
				int dby = 53;

				if ( HasSpell( from, 200 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsChivalry2" ) == 1){this.AddButton(5, dby, 20736,20736, 1, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Cleanse by Fire"); } }
				if ( HasSpell( from, 201 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsChivalry2" ) == 1){this.AddButton(5, dby, 20737,20737, 2, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Close Wounds"); } }
				if ( HasSpell( from, 202 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsChivalry2" ) == 1){this.AddButton(5, dby, 20738,20738, 3, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Consecrate Weapon"); } }
				if ( HasSpell( from, 203 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsChivalry2" ) == 1){this.AddButton(5, dby, 20739,20739, 4, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Dispel Evil"); } }
				if ( HasSpell( from, 204 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsChivalry2" ) == 1){this.AddButton(5, dby, 20740,20740, 5, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Divine Fury"); } }
				if ( HasSpell( from, 205 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsChivalry2" ) == 1){this.AddButton(5, dby, 20741,20741, 6, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Enemy of One"); } }
				if ( HasSpell( from, 206 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsChivalry2" ) == 1){this.AddButton(5, dby, 20742,20742, 7, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Holy Light"); } }
				if ( HasSpell( from, 207 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsChivalry2" ) == 1){this.AddButton(5, dby, 20743,20743, 8, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Noble Sacrifice"); } }
				if ( HasSpell( from, 208 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsChivalry2" ) == 1){this.AddButton(5, dby, 20744,20744, 9, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Remove Curse"); } }
				if ( HasSpell( from, 209 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsChivalry2" ) == 1){this.AddButton(5, dby, 20745,20745, 10, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsChivalry2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Sacred Journey"); } }
			}
			else			
			{			
				this.AddImage(0, 0, 11012, 0);		
				int dby = 50;		

				if ( HasSpell( from, 200 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsChivalry2" ) == 1){this.AddButton(dby, 5, 20736,20736, 1, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 201 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsChivalry2" ) == 1){this.AddButton(dby, 5, 20737,20737, 2, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 202 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsChivalry2" ) == 1){this.AddButton(dby, 5, 20738,20738, 3, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 203 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsChivalry2" ) == 1){this.AddButton(dby, 5, 20739,20739, 4, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 204 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsChivalry2" ) == 1){this.AddButton(dby, 5, 20740,20740, 5, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 205 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsChivalry2" ) == 1){this.AddButton(dby, 5, 20741,20741, 6, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 206 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsChivalry2" ) == 1){this.AddButton(dby, 5, 20742,20742, 7, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 207 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsChivalry2" ) == 1){this.AddButton(dby, 5, 20743,20743, 8, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 208 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsChivalry2" ) == 1){this.AddButton(dby, 5, 20744,20744, 9, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 209 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsChivalry2" ) == 1){this.AddButton(dby, 5, 20745,20745, 10, GumpButtonType.Reply, 1); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsChivalry2 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }
				case 1 : { if ( HasSpell( from, 200 ) ) { new CleanseByFireSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry2( from ) ); } break; }
				case 2 : { if ( HasSpell( from, 201 ) ) { new CloseWoundsSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry2( from ) ); } break; }
				case 3 : { if ( HasSpell( from, 202 ) ) { new ConsecrateWeaponSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry2( from ) ); } break; }
				case 4 : { if ( HasSpell( from, 203 ) ) { new DispelEvilSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry2( from ) ); } break; }
				case 5 : { if ( HasSpell( from, 204 ) ) { new DivineFurySpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry2( from ) ); } break; }
				case 6 : { if ( HasSpell( from, 205 ) ) { new EnemyOfOneSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry2( from ) ); } break; }
				case 7 : { if ( HasSpell( from, 206 ) ) { new HolyLightSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry2( from ) ); } break; }
				case 8 : { if ( HasSpell( from, 207 ) ) { new NobleSacrificeSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry2( from ) ); } break; }
				case 9 : { if ( HasSpell( from, 208 ) ) { new RemoveCurseSpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry2( from ) ); } break; }
				case 10 : { if ( HasSpell( from, 209 ) ) { new SacredJourneySpell( from, null ).Cast(); from.SendGump( new SpellBarsChivalry2( from ) ); } break; }
			}
		}
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Server.Gumps 
{
    public class SpellBarsBard1 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "bardtool1", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "bardtool1" )]
		[Description( "Opens Spell Bar For Bards - 1." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsBard1 ) );
			from.SendGump( new SpellBarsBard1( from ) );
        }
   
        public SpellBarsBard1 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 )
			{
				this.AddImage(7, 0, 11056, 0);
				int dby = 53;

				if ( HasSpell( from, 351 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1028,1028, 1, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Army's Paeon"); } }
				if ( HasSpell( from, 352 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1029,1029, 2, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Enchanting Etude"); } }
				if ( HasSpell( from, 353 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1030,1030, 3, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Carol"); } }
				if ( HasSpell( from, 354 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1031,1031, 4, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Threnody"); } }
				if ( HasSpell( from, 355 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1032,1032, 5, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fire Carol"); } }
				if ( HasSpell( from, 356 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1033,1033, 6, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fire Threnody"); } }
				if ( HasSpell( from, 357 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1034,1034, 7, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Foe Requiem"); } }
				if ( HasSpell( from, 358 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1035,1035, 8, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Ice Carol"); } }
				if ( HasSpell( from, 359 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1036,1036, 9, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Ice Threnody"); } }
				if ( HasSpell( from, 360 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1037,1037, 10, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Knight's Minne"); } }
				if ( HasSpell( from, 361 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1038,1038, 11, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mage's Ballad"); } }
				if ( HasSpell( from, 362 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1040,1040, 12, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Magic Finale"); } }
				if ( HasSpell( from, 363 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1041,1041, 13, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Poison Carol"); } }
				if ( HasSpell( from, 364 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1042,1042, 14, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Poison Threnody"); } }
				if ( HasSpell( from, 365 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1043,1043, 15, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Shepherd's Dance"); } }
				if ( HasSpell( from, 366 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsBard1" ) == 1){this.AddButton(5, dby, 1044,1044, 16, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Sinewy Etude"); } }
			}
			else			
			{			
				this.AddImage(0, 0, 11056, 0);		
				int dby = 50;		

				if ( HasSpell( from, 351 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1028,1028, 1, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 352 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1029,1029, 2, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 353 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1030,1030, 3, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 354 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1031,1031, 4, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 355 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1032,1032, 5, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 356 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1033,1033, 6, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 357 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1034,1034, 7, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 358 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1035,1035, 8, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 359 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1036,1036, 9, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 360 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1037,1037, 10, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 361 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1038,1038, 11, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 362 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1040,1040, 12, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 363 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1041,1041, 13, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 364 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1042,1042, 14, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 365 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1043,1043, 15, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 366 ) && ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsBard1" ) == 1){this.AddButton(dby, 5, 1044,1044, 16, GumpButtonType.Reply, 1); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsBard1 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }
				case 1: { if ( HasSpell( from, 351 ) ) { new ArmysPaeonSong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
				case 2: { if ( HasSpell( from, 352 ) ) { new EnchantingEtudeSong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
				case 3: { if ( HasSpell( from, 353 ) ) { new EnergyCarolSong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
				case 4: { if ( HasSpell( from, 354 ) ) { new EnergyThrenodySong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
				case 5: { if ( HasSpell( from, 355 ) ) { new FireCarolSong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
				case 6: { if ( HasSpell( from, 356 ) ) { new FireThrenodySong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
				case 7: { if ( HasSpell( from, 357 ) ) { new FoeRequiemSong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
				case 8: { if ( HasSpell( from, 358 ) ) { new IceCarolSong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
				case 9: { if ( HasSpell( from, 359 ) ) { new IceThrenodySong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
				case 10: { if ( HasSpell( from, 360 ) ) { new KnightsMinneSong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
				case 11: { if ( HasSpell( from, 361 ) ) { new MagesBalladSong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
				case 12: { if ( HasSpell( from, 362 ) ) { new MagicFinaleSong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
				case 13: { if ( HasSpell( from, 363 ) ) { new PoisonCarolSong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
				case 14: { if ( HasSpell( from, 364 ) ) { new PoisonThrenodySong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
				case 15: { if ( HasSpell( from, 365 ) ) { new SheepfoeMamboSong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
				case 16: { if ( HasSpell( from, 366 ) ) { new SinewyEtudeSong( from, null ).Cast(); from.SendGump( new SpellBarsBard1( from ) ); } break; }
			}
		}
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Server.Gumps 
{
    public class SpellBarsBard2 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "bardtool2", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "bardtool2" )]
		[Description( "Opens Spell Bar For Bards - 2." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsBard2 ) );
			from.SendGump( new SpellBarsBard2( from ) );
        }
   
        public SpellBarsBard2 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 )
			{
				this.AddImage(7, 0, 11056, 0);
				int dby = 53;

				if ( HasSpell( from, 351 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1028,1028, 1, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Army's Paeon"); } }
				if ( HasSpell( from, 352 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1029,1029, 2, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Enchanting Etude"); } }
				if ( HasSpell( from, 353 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1030,1030, 3, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Carol"); } }
				if ( HasSpell( from, 354 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1031,1031, 4, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Energy Threnody"); } }
				if ( HasSpell( from, 355 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1032,1032, 5, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fire Carol"); } }
				if ( HasSpell( from, 356 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1033,1033, 6, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Fire Threnody"); } }
				if ( HasSpell( from, 357 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1034,1034, 7, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Foe Requiem"); } }
				if ( HasSpell( from, 358 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1035,1035, 8, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Ice Carol"); } }
				if ( HasSpell( from, 359 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1036,1036, 9, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Ice Threnody"); } }
				if ( HasSpell( from, 360 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1037,1037, 10, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Knight's Minne"); } }
				if ( HasSpell( from, 361 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1038,1038, 11, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Mage's Ballad"); } }
				if ( HasSpell( from, 362 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1040,1040, 12, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Magic Finale"); } }
				if ( HasSpell( from, 363 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1041,1041, 13, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Poison Carol"); } }
				if ( HasSpell( from, 364 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1042,1042, 14, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Poison Threnody"); } }
				if ( HasSpell( from, 365 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1043,1043, 15, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Sheepfoe Mambo"); } }
				if ( HasSpell( from, 366 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsBard2" ) == 1){this.AddButton(5, dby, 1044,1044, 16, GumpButtonType.Reply, 1); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 18, "SetupBarsBard2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Sinewy Etude"); } }
			}
			else			
			{			
				this.AddImage(0, 0, 11056, 0);		
				int dby = 50;		

				if ( HasSpell( from, 351 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1028,1028, 1, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 352 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1029,1029, 2, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 353 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1030,1030, 3, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 354 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1031,1031, 4, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 355 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1032,1032, 5, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 356 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1033,1033, 6, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 357 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1034,1034, 7, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 358 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1035,1035, 8, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 359 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1036,1036, 9, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 360 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1037,1037, 10, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 361 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1038,1038, 11, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 362 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1040,1040, 12, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 363 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1041,1041, 13, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 364 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1042,1042, 14, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 365 ) && ToolBarUpdates.GetToolBarSetting( from, 15, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1043,1043, 15, GumpButtonType.Reply, 1); dby = dby + 45;}
				if ( HasSpell( from, 366 ) && ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsBard2" ) == 1){this.AddButton(dby, 5, 1044,1044, 16, GumpButtonType.Reply, 1); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsBard2 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }
				case 1: { if ( HasSpell( from, 351 ) ) { new ArmysPaeonSong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
				case 2: { if ( HasSpell( from, 352 ) ) { new EnchantingEtudeSong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
				case 3: { if ( HasSpell( from, 353 ) ) { new EnergyCarolSong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
				case 4: { if ( HasSpell( from, 354 ) ) { new EnergyThrenodySong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
				case 5: { if ( HasSpell( from, 355 ) ) { new FireCarolSong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
				case 6: { if ( HasSpell( from, 356 ) ) { new FireThrenodySong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
				case 7: { if ( HasSpell( from, 357 ) ) { new FoeRequiemSong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
				case 8: { if ( HasSpell( from, 358 ) ) { new IceCarolSong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
				case 9: { if ( HasSpell( from, 359 ) ) { new IceThrenodySong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
				case 10: { if ( HasSpell( from, 360 ) ) { new KnightsMinneSong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
				case 11: { if ( HasSpell( from, 361 ) ) { new MagesBalladSong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
				case 12: { if ( HasSpell( from, 362 ) ) { new MagicFinaleSong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
				case 13: { if ( HasSpell( from, 363 ) ) { new PoisonCarolSong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
				case 14: { if ( HasSpell( from, 364 ) ) { new PoisonThrenodySong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
				case 15: { if ( HasSpell( from, 365 ) ) { new SheepfoeMamboSong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
				case 16: { if ( HasSpell( from, 366 ) ) { new SinewyEtudeSong( from, null ).Cast(); from.SendGump( new SpellBarsBard2( from ) ); } break; }
			}
		}
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Server.Gumps 
{
    public class SpellBarsDeath1 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			if ( book is DeathKnightSpellbook )
			{
				DeathKnightSpellbook tome = (DeathKnightSpellbook)book;
				if ( tome.owner != from )
				{
					book = null;
				}
			}

			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "deathtool1", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "deathtool1" )]
		[Description( "Opens Spell Bar For Death Knights - 1." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsDeath1 ) );
			from.SendGump( new SpellBarsDeath1( from ) );
        }
   
        public SpellBarsDeath1 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath1" ) > 0 )
			{
				this.AddImage(7, 0, 11013, 2405);
				int dby = 53;

				if ( HasSpell( from, 750 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsDeath1" ) == 1){this.AddButton(5, dby, 0x5010,0x5010, 1, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5010, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Banish"); } }
				if ( HasSpell( from, 751 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsDeath1" ) == 1){this.AddButton(5, dby, 0x5009,0x5009, 2, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5009, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Demonic Touch"); } }
				if ( HasSpell( from, 752 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsDeath1" ) == 1){this.AddButton(5, dby, 0x5005,0x5005, 3, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5005, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Devil Pact"); } }
				if ( HasSpell( from, 753 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsDeath1" ) == 1){this.AddButton(5, dby, 0x402,0x402, 4, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x402, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Grim Reaper"); } }
				if ( HasSpell( from, 754 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsDeath1" ) == 1){this.AddButton(5, dby, 0x5002,0x5002, 5, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5002, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Hag Hand"); } }
				if ( HasSpell( from, 755 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsDeath1" ) == 1){this.AddButton(5, dby, 0x3E9,0x3E9, 6, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x3E9, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Hellfire"); } }
				if ( HasSpell( from, 756 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsDeath1" ) == 1){this.AddButton(5, dby, 0x5DC0,0x5DC0, 7, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5DC0, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Lucifer's Bolt"); } }
				if ( HasSpell( from, 757 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsDeath1" ) == 1){this.AddButton(5, dby, 0x1B,0x1B, 8, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x1B, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Orb of Orcus"); } }
				if ( HasSpell( from, 758 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsDeath1" ) == 1){this.AddButton(5, dby, 0x3EE,0x3EE, 9, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x3EE, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Shield of Hate"); } }
				if ( HasSpell( from, 759 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsDeath1" ) == 1){this.AddButton(5, dby, 0x5006,0x5006, 10, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5006, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Soul Reaper"); } }
				if ( HasSpell( from, 760 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsDeath1" ) == 1){this.AddButton(5, dby, 0x2B,0x2B, 11, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x2B, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Strength of Steel"); } }
				if ( HasSpell( from, 761 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsDeath1" ) == 1){this.AddButton(5, dby, 0x12,0x12, 12, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x12, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Strike"); } }
				if ( HasSpell( from, 762 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsDeath1" ) == 1){this.AddButton(5, dby, 0x500C,0x500C, 13, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x500C, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Succubus Skin"); } }
				if ( HasSpell( from, 763 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsDeath1" ) == 1){this.AddButton(5, dby, 0x2E,0x2E, 14, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x2E, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Wrath"); } }
			}
			else			
			{			
				this.AddImage(0, 0, 11013, 2405);		
				int dby = 50;		

				if ( HasSpell( from, 750 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsDeath1" ) == 1){this.AddButton(dby, 5, 0x5010,0x5010, 1, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5010, 2405); dby = dby + 45;}
				if ( HasSpell( from, 751 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsDeath1" ) == 1){this.AddButton(dby, 5, 0x5009,0x5009, 2, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5009, 2405); dby = dby + 45;}
				if ( HasSpell( from, 752 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsDeath1" ) == 1){this.AddButton(dby, 5, 0x5005,0x5005, 3, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5005, 2405); dby = dby + 45;}
				if ( HasSpell( from, 753 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsDeath1" ) == 1){this.AddButton(dby, 5, 0x402,0x402, 4, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x402, 2405); dby = dby + 45;}
				if ( HasSpell( from, 754 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsDeath1" ) == 1){this.AddButton(dby, 5, 0x5002,0x5002, 5, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5002, 2405); dby = dby + 45;}
				if ( HasSpell( from, 755 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsDeath1" ) == 1){this.AddButton(dby, 5, 0x3E9,0x3E9, 6, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x3E9, 2405); dby = dby + 45;}
				if ( HasSpell( from, 756 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsDeath1" ) == 1){this.AddButton(dby, 5, 0x5DC0,0x5DC0, 7, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5DC0, 2405); dby = dby + 45;}
				if ( HasSpell( from, 757 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsDeath1" ) == 1){this.AddButton(dby, 5, 0x1B,0x1B, 8, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x1B, 2405); dby = dby + 45;}
				if ( HasSpell( from, 758 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsDeath1" ) == 1){this.AddButton(dby, 5, 0x3EE,0x3EE, 9, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x3EE, 2405); dby = dby + 45;}
				if ( HasSpell( from, 759 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsDeath1" ) == 1){this.AddButton(dby, 5, 0x5006,0x5006, 10, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5006, 2405); dby = dby + 45;}
				if ( HasSpell( from, 760 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsDeath1" ) == 1){this.AddButton(dby, 5, 0x2B,0x2B, 11, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x2B, 2405); dby = dby + 45;}
				if ( HasSpell( from, 761 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsDeath1" ) == 1){this.AddButton(dby, 5, 0x12,0x12, 12, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x12, 2405); dby = dby + 45;}
				if ( HasSpell( from, 762 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsDeath1" ) == 1){this.AddButton(dby, 5, 0x500C,0x500C, 13, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x500C, 2405); dby = dby + 45;}
				if ( HasSpell( from, 763 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsDeath1" ) == 1){this.AddButton(dby, 5, 0x2E,0x2E, 14, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x2E, 2405); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsDeath2 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }

				case 1: { if ( HasSpell( from, 750 ) ) { new BanishSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath1( from ) ); } break; }
				case 2: { if ( HasSpell( from, 751 ) ) { new DemonicTouchSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath1( from ) ); } break; }
				case 3: { if ( HasSpell( from, 752 ) ) { new DevilPactSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath1( from ) ); } break; }
				case 4: { if ( HasSpell( from, 753 ) ) { new GrimReaperSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath1( from ) ); } break; }
				case 5: { if ( HasSpell( from, 754 ) ) { new HagHandSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath1( from ) ); } break; }
				case 6: { if ( HasSpell( from, 755 ) ) { new HellfireSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath1( from ) ); } break; }
				case 7: { if ( HasSpell( from, 756 ) ) { new LucifersBoltSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath1( from ) ); } break; }
				case 8: { if ( HasSpell( from, 757 ) ) { new OrbOfOrcusSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath1( from ) ); } break; }
				case 9: { if ( HasSpell( from, 758 ) ) { new ShieldOfHateSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath1( from ) ); } break; }
				case 10: { if ( HasSpell( from, 759 ) ) { new SoulReaperSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath1( from ) ); } break; }
				case 11: { if ( HasSpell( from, 760 ) ) { new StrengthOfSteelSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath1( from ) ); } break; }
				case 12: { if ( HasSpell( from, 761 ) ) { new StrikeSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath1( from ) ); } break; }
				case 13: { if ( HasSpell( from, 762 ) ) { new SuccubusSkinSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath1( from ) ); } break; }
				case 14: { if ( HasSpell( from, 763 ) ) { new WrathSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath1( from ) ); } break; }
			}
		}
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


namespace Server.Gumps 
{
    public class SpellBarsDeath2 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			if ( book is DeathKnightSpellbook )
			{
				DeathKnightSpellbook tome = (DeathKnightSpellbook)book;
				if ( tome.owner != from )
				{
					book = null;
				}
			}

			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "deathtool2", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "deathtool2" )]
		[Description( "Opens Spell Bar For Death Knights - 2." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsDeath2 ) );
			from.SendGump( new SpellBarsDeath2( from ) );
        }
   
        public SpellBarsDeath2 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath2" ) > 0 )
			{
				this.AddImage(7, 0, 11013, 2405);
				int dby = 53;

				if ( HasSpell( from, 750 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsDeath2" ) == 1){this.AddButton(5, dby, 0x5010,0x5010, 1, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5010, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Banish"); } }
				if ( HasSpell( from, 751 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsDeath2" ) == 1){this.AddButton(5, dby, 0x5009,0x5009, 2, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5009, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Demonic Touch"); } }
				if ( HasSpell( from, 752 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsDeath2" ) == 1){this.AddButton(5, dby, 0x5005,0x5005, 3, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5005, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Devil Pact"); } }
				if ( HasSpell( from, 753 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsDeath2" ) == 1){this.AddButton(5, dby, 0x402,0x402, 4, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x402, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Grim Reaper"); } }
				if ( HasSpell( from, 754 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsDeath2" ) == 1){this.AddButton(5, dby, 0x5002,0x5002, 5, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5002, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Hag Hand"); } }
				if ( HasSpell( from, 755 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsDeath2" ) == 1){this.AddButton(5, dby, 0x3E9,0x3E9, 6, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x3E9, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Hellfire"); } }
				if ( HasSpell( from, 756 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsDeath2" ) == 1){this.AddButton(5, dby, 0x5DC0,0x5DC0, 7, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5DC0, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Lucifer's Bolt"); } }
				if ( HasSpell( from, 757 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsDeath2" ) == 1){this.AddButton(5, dby, 0x1B,0x1B, 8, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x1B, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Orb of Orcus"); } }
				if ( HasSpell( from, 758 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsDeath2" ) == 1){this.AddButton(5, dby, 0x3EE,0x3EE, 9, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x3EE, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Shield of Hate"); } }
				if ( HasSpell( from, 759 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsDeath2" ) == 1){this.AddButton(5, dby, 0x5006,0x5006, 10, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5006, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Soul Reaper"); } }
				if ( HasSpell( from, 760 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsDeath2" ) == 1){this.AddButton(5, dby, 0x2B,0x2B, 11, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x2B, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Strength of Steel"); } }
				if ( HasSpell( from, 761 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsDeath2" ) == 1){this.AddButton(5, dby, 0x12,0x12, 12, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x12, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Strike"); } }
				if ( HasSpell( from, 762 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsDeath2" ) == 1){this.AddButton(5, dby, 0x500C,0x500C, 13, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x500C, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Succubus Skin"); } }
				if ( HasSpell( from, 763 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsDeath2" ) == 1){this.AddButton(5, dby, 0x2E,0x2E, 14, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x2E, 2405); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsDeath2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Wrath"); } }
			}
			else			
			{			
				this.AddImage(0, 0, 11013, 2405);		
				int dby = 50;		

				if ( HasSpell( from, 750 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsDeath2" ) == 1){this.AddButton(dby, 5, 0x5010,0x5010, 1, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5010, 2405); dby = dby + 45;}
				if ( HasSpell( from, 751 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsDeath2" ) == 1){this.AddButton(dby, 5, 0x5009,0x5009, 2, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5009, 2405); dby = dby + 45;}
				if ( HasSpell( from, 752 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsDeath2" ) == 1){this.AddButton(dby, 5, 0x5005,0x5005, 3, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5005, 2405); dby = dby + 45;}
				if ( HasSpell( from, 753 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsDeath2" ) == 1){this.AddButton(dby, 5, 0x402,0x402, 4, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x402, 2405); dby = dby + 45;}
				if ( HasSpell( from, 754 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsDeath2" ) == 1){this.AddButton(dby, 5, 0x5002,0x5002, 5, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5002, 2405); dby = dby + 45;}
				if ( HasSpell( from, 755 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsDeath2" ) == 1){this.AddButton(dby, 5, 0x3E9,0x3E9, 6, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x3E9, 2405); dby = dby + 45;}
				if ( HasSpell( from, 756 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsDeath2" ) == 1){this.AddButton(dby, 5, 0x5DC0,0x5DC0, 7, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5DC0, 2405); dby = dby + 45;}
				if ( HasSpell( from, 757 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsDeath2" ) == 1){this.AddButton(dby, 5, 0x1B,0x1B, 8, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x1B, 2405); dby = dby + 45;}
				if ( HasSpell( from, 758 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsDeath2" ) == 1){this.AddButton(dby, 5, 0x3EE,0x3EE, 9, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x3EE, 2405); dby = dby + 45;}
				if ( HasSpell( from, 759 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsDeath2" ) == 1){this.AddButton(dby, 5, 0x5006,0x5006, 10, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5006, 2405); dby = dby + 45;}
				if ( HasSpell( from, 760 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsDeath2" ) == 1){this.AddButton(dby, 5, 0x2B,0x2B, 11, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x2B, 2405); dby = dby + 45;}
				if ( HasSpell( from, 761 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsDeath2" ) == 1){this.AddButton(dby, 5, 0x12,0x12, 12, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x12, 2405); dby = dby + 45;}
				if ( HasSpell( from, 762 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsDeath2" ) == 1){this.AddButton(dby, 5, 0x500C,0x500C, 13, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x500C, 2405); dby = dby + 45;}
				if ( HasSpell( from, 763 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsDeath2" ) == 1){this.AddButton(dby, 5, 0x2E,0x2E, 14, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x2E, 2405); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsDeath2 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }

				case 1: { if ( HasSpell( from, 750 ) ) { new BanishSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath2( from ) ); } break; }
				case 2: { if ( HasSpell( from, 751 ) ) { new DemonicTouchSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath2( from ) ); } break; }
				case 3: { if ( HasSpell( from, 752 ) ) { new DevilPactSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath2( from ) ); } break; }
				case 4: { if ( HasSpell( from, 753 ) ) { new GrimReaperSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath2( from ) ); } break; }
				case 5: { if ( HasSpell( from, 754 ) ) { new HagHandSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath2( from ) ); } break; }
				case 6: { if ( HasSpell( from, 755 ) ) { new HellfireSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath2( from ) ); } break; }
				case 7: { if ( HasSpell( from, 756 ) ) { new LucifersBoltSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath2( from ) ); } break; }
				case 8: { if ( HasSpell( from, 757 ) ) { new OrbOfOrcusSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath2( from ) ); } break; }
				case 9: { if ( HasSpell( from, 758 ) ) { new ShieldOfHateSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath2( from ) ); } break; }
				case 10: { if ( HasSpell( from, 759 ) ) { new SoulReaperSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath2( from ) ); } break; }
				case 11: { if ( HasSpell( from, 760 ) ) { new StrengthOfSteelSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath2( from ) ); } break; }
				case 12: { if ( HasSpell( from, 761 ) ) { new StrikeSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath2( from ) ); } break; }
				case 13: { if ( HasSpell( from, 762 ) ) { new SuccubusSkinSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath2( from ) ); } break; }
				case 14: { if ( HasSpell( from, 763 ) ) { new WrathSpell( from, null ).Cast(); from.SendGump( new SpellBarsDeath2( from ) ); } break; }
			}
		}
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Server.Gumps 
{
    public class SpellBarsPriest1 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			if ( book is HolyManSpellbook )
			{
				HolyManSpellbook tome = (HolyManSpellbook)book;
				if ( tome.owner != from )
				{
					book = null;
				}
			}

			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "holytool1", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "holytool1" )]
		[Description( "Opens Spell Bar For Prayers - 1." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsPriest1 ) );
			from.SendGump( new SpellBarsPriest1( from ) );
        }
   
        public SpellBarsPriest1 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest1" ) > 0 )
			{
				this.AddImage(7, 0, 2420, 1071);
				int dby = 53;

				if ( HasSpell( from, 770 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsPriest1" ) == 1){this.AddButton(5, dby, 0x965,0x965, 1, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x965, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest1" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Banish"); } }
				if ( HasSpell( from, 771 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsPriest1" ) == 1){this.AddButton(5, dby, 0x966,0x966, 2, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x966, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest1" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Dampen Spirit"); } }
				if ( HasSpell( from, 772 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsPriest1" ) == 1){this.AddButton(5, dby, 0x967,0x967, 3, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x967, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest1" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Enchant"); } }
				if ( HasSpell( from, 773 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsPriest1" ) == 1){this.AddButton(5, dby, 0x968,0x968, 4, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x968, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest1" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Hammer of Faith"); } }
				if ( HasSpell( from, 774 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsPriest1" ) == 1){this.AddButton(5, dby, 0x969,0x969, 5, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x969, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest1" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Heavenly Light"); } }
				if ( HasSpell( from, 775 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsPriest1" ) == 1){this.AddButton(5, dby, 0x96A,0x96A, 6, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x96A, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest1" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Nourish"); } }
				if ( HasSpell( from, 776 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsPriest1" ) == 1){this.AddButton(5, dby, 0x96B,0x96B, 7, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x96B, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest1" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Purge"); } }
				if ( HasSpell( from, 777 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsPriest1" ) == 1){this.AddButton(5, dby, 0x96C,0x96C, 8, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x96C, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest1" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Rebirth"); } }
				if ( HasSpell( from, 778 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsPriest1" ) == 1){this.AddButton(5, dby, 0x96E,0x96E, 9, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x96E, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest1" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Sacred Boon"); } }
				if ( HasSpell( from, 779 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsPriest1" ) == 1){this.AddButton(5, dby, 0x96D,0x96D, 10, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x96D, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest1" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Sactify"); } }
				if ( HasSpell( from, 780 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsPriest1" ) == 1){this.AddButton(5, dby, 0x96F,0x96F, 11, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x96F, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest1" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Seance"); } }
				if ( HasSpell( from, 781 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsPriest1" ) == 1){this.AddButton(5, dby, 0x970,0x970, 12, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x970, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest1" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Smite"); } }
				if ( HasSpell( from, 782 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsPriest1" ) == 1){this.AddButton(5, dby, 0x971,0x971, 13, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x971, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest1" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Touch of Life"); } }
				if ( HasSpell( from, 783 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsPriest1" ) == 1){this.AddButton(5, dby, 0x972,0x972, 14, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x972, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest1" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Trial by Fire"); } }
			}
			else			
			{			
				this.AddImage(0, 0, 2420, 1071);		
				int dby = 50;		

				if ( HasSpell( from, 770 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsPriest1" ) == 1){this.AddButton(dby, 5, 0x965,0x965, 1, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x965, 1071); dby = dby + 45;}
				if ( HasSpell( from, 771 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsPriest1" ) == 1){this.AddButton(dby, 5, 0x966,0x966, 2, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x966, 1071); dby = dby + 45;}
				if ( HasSpell( from, 772 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsPriest1" ) == 1){this.AddButton(dby, 5, 0x967,0x967, 3, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x967, 1071); dby = dby + 45;}
				if ( HasSpell( from, 773 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsPriest1" ) == 1){this.AddButton(dby, 5, 0x968,0x968, 4, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x968, 1071); dby = dby + 45;}
				if ( HasSpell( from, 774 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsPriest1" ) == 1){this.AddButton(dby, 5, 0x969,0x969, 5, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x969, 1071); dby = dby + 45;}
				if ( HasSpell( from, 775 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsPriest1" ) == 1){this.AddButton(dby, 5, 0x96A,0x96A, 6, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x96A, 1071); dby = dby + 45;}
				if ( HasSpell( from, 776 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsPriest1" ) == 1){this.AddButton(dby, 5, 0x96B,0x96B, 7, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x96B, 1071); dby = dby + 45;}
				if ( HasSpell( from, 777 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsPriest1" ) == 1){this.AddButton(dby, 5, 0x96C,0x96C, 8, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x96C, 1071); dby = dby + 45;}
				if ( HasSpell( from, 778 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsPriest1" ) == 1){this.AddButton(dby, 5, 0x96E,0x96E, 9, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x96E, 1071); dby = dby + 45;}
				if ( HasSpell( from, 779 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsPriest1" ) == 1){this.AddButton(dby, 5, 0x96D,0x96D, 10, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x96D, 1071); dby = dby + 45;}
				if ( HasSpell( from, 780 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsPriest1" ) == 1){this.AddButton(dby, 5, 0x96F,0x96F, 11, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x96F, 1071); dby = dby + 45;}
				if ( HasSpell( from, 781 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsPriest1" ) == 1){this.AddButton(dby, 5, 0x970,0x970, 12, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x970, 1071); dby = dby + 45;}
				if ( HasSpell( from, 782 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsPriest1" ) == 1){this.AddButton(dby, 5, 0x971,0x971, 13, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x971, 1071); dby = dby + 45;}
				if ( HasSpell( from, 783 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsPriest1" ) == 1){this.AddButton(dby, 5, 0x972,0x972, 14, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x972, 1071); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsPriest1 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }

				case 1: { if ( HasSpell( from, 770 ) ) { new BanishEvilSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest1( from ) ); } break; }
				case 2: { if ( HasSpell( from, 771 ) ) { new DampenSpiritSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest1( from ) ); } break; }
				case 3: { if ( HasSpell( from, 772 ) ) { new EnchantSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest1( from ) ); } break; }
				case 4: { if ( HasSpell( from, 773 ) ) { new HammerOfFaithSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest1( from ) ); } break; }
				case 5: { if ( HasSpell( from, 774 ) ) { new HeavenlyLightSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest1( from ) ); } break; }
				case 6: { if ( HasSpell( from, 775 ) ) { new NourishSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest1( from ) ); } break; }
				case 7: { if ( HasSpell( from, 776 ) ) { new PurgeSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest1( from ) ); } break; }
				case 8: { if ( HasSpell( from, 777 ) ) { new RebirthSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest1( from ) ); } break; }
				case 9: { if ( HasSpell( from, 778 ) ) { new SacredBoonSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest1( from ) ); } break; }
				case 10: { if ( HasSpell( from, 779 ) ) { new SanctifySpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest1( from ) ); } break; }
				case 11: { if ( HasSpell( from, 780 ) ) { new SeanceSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest1( from ) ); } break; }
				case 12: { if ( HasSpell( from, 781 ) ) { new SmiteSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest1( from ) ); } break; }
				case 13: { if ( HasSpell( from, 782 ) ) { new TouchOfLifeSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest1( from ) ); } break; }
				case 14: { if ( HasSpell( from, 783 ) ) { new TrialByFireSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest1( from ) ); } break; }
			}
		}
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


namespace Server.Gumps 
{
    public class SpellBarsPriest2 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			if ( book is HolyManSpellbook )
			{
				HolyManSpellbook tome = (HolyManSpellbook)book;
				if ( tome.owner != from )
				{
					book = null;
				}
			}

			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "holytool2", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "holytool2" )]
		[Description( "Opens Spell Bar For Prayers - 2." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsPriest2 ) );
			from.SendGump( new SpellBarsPriest2( from ) );
        }
   
        public SpellBarsPriest2 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest2" ) > 0 )
			{
				this.AddImage(7, 0, 2420, 1071);
				int dby = 53;

				if ( HasSpell( from, 770 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsPriest2" ) == 1){this.AddButton(5, dby, 0x965,0x965, 1, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x965, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest2" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Banish"); } }
				if ( HasSpell( from, 771 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsPriest2" ) == 1){this.AddButton(5, dby, 0x966,0x966, 2, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x966, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest2" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Dampen Spirit"); } }
				if ( HasSpell( from, 772 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsPriest2" ) == 1){this.AddButton(5, dby, 0x967,0x967, 3, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x967, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest2" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Enchant"); } }
				if ( HasSpell( from, 773 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsPriest2" ) == 1){this.AddButton(5, dby, 0x968,0x968, 4, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x968, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest2" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Hammer of Faith"); } }
				if ( HasSpell( from, 774 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsPriest2" ) == 1){this.AddButton(5, dby, 0x969,0x969, 5, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x969, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest2" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Heavenly Light"); } }
				if ( HasSpell( from, 775 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsPriest2" ) == 1){this.AddButton(5, dby, 0x96A,0x96A, 6, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x96A, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest2" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Nourish"); } }
				if ( HasSpell( from, 776 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsPriest2" ) == 1){this.AddButton(5, dby, 0x96B,0x96B, 7, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x96B, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest2" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Purge"); } }
				if ( HasSpell( from, 777 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsPriest2" ) == 1){this.AddButton(5, dby, 0x96C,0x96C, 8, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x96C, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest2" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Rebirth"); } }
				if ( HasSpell( from, 778 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsPriest2" ) == 1){this.AddButton(5, dby, 0x96E,0x96E, 9, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x96E, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest2" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Sacred Boon"); } }
				if ( HasSpell( from, 779 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsPriest2" ) == 1){this.AddButton(5, dby, 0x96D,0x96D, 10, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x96D, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest2" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Sactify"); } }
				if ( HasSpell( from, 780 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsPriest2" ) == 1){this.AddButton(5, dby, 0x96F,0x96F, 11, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x96F, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest2" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Seance"); } }
				if ( HasSpell( from, 781 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsPriest2" ) == 1){this.AddButton(5, dby, 0x970,0x970, 12, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x970, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest2" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Smite"); } }
				if ( HasSpell( from, 782 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsPriest2" ) == 1){this.AddButton(5, dby, 0x971,0x971, 13, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x971, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest2" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Touch of Life"); } }
				if ( HasSpell( from, 783 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsPriest2" ) == 1){this.AddButton(5, dby, 0x972,0x972, 14, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x972, 1071); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 16, "SetupBarsPriest2" ) > 0 ){ AddLabel(59, (dby-34), 1071, @"Trial by Fire"); } }
			}
			else			
			{			
				this.AddImage(0, 0, 2420, 1071);		
				int dby = 50;		

				if ( HasSpell( from, 770 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsPriest2" ) == 1){this.AddButton(dby, 5, 0x965,0x965, 1, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x965, 1071); dby = dby + 45;}
				if ( HasSpell( from, 771 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsPriest2" ) == 1){this.AddButton(dby, 5, 0x966,0x966, 2, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x966, 1071); dby = dby + 45;}
				if ( HasSpell( from, 772 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsPriest2" ) == 1){this.AddButton(dby, 5, 0x967,0x967, 3, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x967, 1071); dby = dby + 45;}
				if ( HasSpell( from, 773 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsPriest2" ) == 1){this.AddButton(dby, 5, 0x968,0x968, 4, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x968, 1071); dby = dby + 45;}
				if ( HasSpell( from, 774 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsPriest2" ) == 1){this.AddButton(dby, 5, 0x969,0x969, 5, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x969, 1071); dby = dby + 45;}
				if ( HasSpell( from, 775 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsPriest2" ) == 1){this.AddButton(dby, 5, 0x96A,0x96A, 6, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x96A, 1071); dby = dby + 45;}
				if ( HasSpell( from, 776 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsPriest2" ) == 1){this.AddButton(dby, 5, 0x96B,0x96B, 7, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x96B, 1071); dby = dby + 45;}
				if ( HasSpell( from, 777 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsPriest2" ) == 1){this.AddButton(dby, 5, 0x96C,0x96C, 8, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x96C, 1071); dby = dby + 45;}
				if ( HasSpell( from, 778 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsPriest2" ) == 1){this.AddButton(dby, 5, 0x96E,0x96E, 9, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x96E, 1071); dby = dby + 45;}
				if ( HasSpell( from, 779 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsPriest2" ) == 1){this.AddButton(dby, 5, 0x96D,0x96D, 10, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x96D, 1071); dby = dby + 45;}
				if ( HasSpell( from, 780 ) && ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsPriest2" ) == 1){this.AddButton(dby, 5, 0x96F,0x96F, 11, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x96F, 1071); dby = dby + 45;}
				if ( HasSpell( from, 781 ) && ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsPriest2" ) == 1){this.AddButton(dby, 5, 0x970,0x970, 12, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x970, 1071); dby = dby + 45;}
				if ( HasSpell( from, 782 ) && ToolBarUpdates.GetToolBarSetting( from, 13, "SetupBarsPriest2" ) == 1){this.AddButton(dby, 5, 0x971,0x971, 13, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x971, 1071); dby = dby + 45;}
				if ( HasSpell( from, 783 ) && ToolBarUpdates.GetToolBarSetting( from, 14, "SetupBarsPriest2" ) == 1){this.AddButton(dby, 5, 0x972,0x972, 14, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x972, 1071); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsPriest2 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }

				case 1: { if ( HasSpell( from, 770 ) ) { new BanishEvilSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest2( from ) ); } break; }
				case 2: { if ( HasSpell( from, 771 ) ) { new DampenSpiritSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest2( from ) ); } break; }
				case 3: { if ( HasSpell( from, 772 ) ) { new EnchantSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest2( from ) ); } break; }
				case 4: { if ( HasSpell( from, 773 ) ) { new HammerOfFaithSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest2( from ) ); } break; }
				case 5: { if ( HasSpell( from, 774 ) ) { new HeavenlyLightSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest2( from ) ); } break; }
				case 6: { if ( HasSpell( from, 775 ) ) { new NourishSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest2( from ) ); } break; }
				case 7: { if ( HasSpell( from, 776 ) ) { new PurgeSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest2( from ) ); } break; }
				case 8: { if ( HasSpell( from, 777 ) ) { new RebirthSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest2( from ) ); } break; }
				case 9: { if ( HasSpell( from, 778 ) ) { new SacredBoonSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest2( from ) ); } break; }
				case 10: { if ( HasSpell( from, 779 ) ) { new SanctifySpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest2( from ) ); } break; }
				case 11: { if ( HasSpell( from, 780 ) ) { new SeanceSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest2( from ) ); } break; }
				case 12: { if ( HasSpell( from, 781 ) ) { new SmiteSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest2( from ) ); } break; }
				case 13: { if ( HasSpell( from, 782 ) ) { new TouchOfLifeSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest2( from ) ); } break; }
				case 14: { if ( HasSpell( from, 783 ) ) { new TrialByFireSpell( from, null ).Cast(); from.SendGump( new SpellBarsPriest2( from ) ); } break; }
			}
		}
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Server.Gumps 
{
    public class SpellBarsMonk1 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "monktool1", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "monktool1" )]
		[Description( "Opens Spell Bar For Monks - 1." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsMonk1 ) );
			from.SendGump( new SpellBarsMonk1( from ) );
        }
   
        public SpellBarsMonk1 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsMonk1" ) > 0 )
			{
				this.AddImage(7, 0, 0x973, 2422);
				int dby = 53;

				if ( HasSpell( from, 250 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsMonk1" ) == 1){this.AddButton(5, dby, 0x500E,0x500E, 1, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x500E, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Astral Projection"); } }
				if ( HasSpell( from, 251 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsMonk1" ) == 1){this.AddButton(5, dby, 0x410,0x410, 2, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x410, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Astral Travel"); } }
				if ( HasSpell( from, 252 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsMonk1" ) == 1){this.AddButton(5, dby, 0x15,0x15, 3, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x15, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Create Robe"); } }
				if ( HasSpell( from, 253 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsMonk1" ) == 1){this.AddButton(5, dby, 0x971,0x971, 4, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x971, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Gentle Touch"); } }
				if ( HasSpell( from, 254 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsMonk1" ) == 1){this.AddButton(5, dby, 0x4B2,0x4B2, 5, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x4B2, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Leap"); } }
				if ( HasSpell( from, 255 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsMonk1" ) == 1){this.AddButton(5, dby, 0x5DC2,0x5DC2, 6, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5DC2, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Psionic Blast"); } }
				if ( HasSpell( from, 256 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsMonk1" ) == 1){this.AddButton(5, dby, 0x1A,0x1A, 7, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x1A, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Psychic Wall"); } }
				if ( HasSpell( from, 257 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsMonk1" ) == 1){this.AddButton(5, dby, 0x96D,0x96D, 8, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x96D, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Purity of Body"); } }
				if ( HasSpell( from, 258 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsMonk1" ) == 1){this.AddButton(5, dby, 0x5001,0x5001, 9, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5001, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Quivering Palm"); } }
				if ( HasSpell( from, 259 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsMonk1" ) == 1){this.AddButton(5, dby, 0x19,0x19, 10, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x19, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Wind Runner"); } }
			}
			else			
			{			
				this.AddImage(0, 0, 0x973, 2422);		
				int dby = 50;		

				if ( HasSpell( from, 250 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsMonk1" ) == 1){this.AddButton(dby, 5, 0x500E,0x500E, 1, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x500E, 2422); dby = dby + 45;}
				if ( HasSpell( from, 251 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsMonk1" ) == 1){this.AddButton(dby, 5, 0x410,0x410, 2, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x410, 2422); dby = dby + 45;}
				if ( HasSpell( from, 252 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsMonk1" ) == 1){this.AddButton(dby, 5, 0x15,0x15, 3, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x15, 2422); dby = dby + 45;}
				if ( HasSpell( from, 253 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsMonk1" ) == 1){this.AddButton(dby, 5, 0x971,0x971, 4, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x971, 2422); dby = dby + 45;}
				if ( HasSpell( from, 254 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsMonk1" ) == 1){this.AddButton(dby, 5, 0x4B2,0x4B2, 5, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x4B2, 2422); dby = dby + 45;}
				if ( HasSpell( from, 255 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsMonk1" ) == 1){this.AddButton(dby, 5, 0x5DC2,0x5DC2, 6, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5DC2, 2422); dby = dby + 45;}
				if ( HasSpell( from, 256 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsMonk1" ) == 1){this.AddButton(dby, 5, 0x1A,0x1A, 7, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x1A, 2422); dby = dby + 45;}
				if ( HasSpell( from, 257 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsMonk1" ) == 1){this.AddButton(dby, 5, 0x96D,0x96D, 8, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x96D, 2422); dby = dby + 45;}
				if ( HasSpell( from, 258 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsMonk1" ) == 1){this.AddButton(dby, 5, 0x5001,0x5001, 9, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5001, 2422); dby = dby + 45;}
				if ( HasSpell( from, 259 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsMonk1" ) == 1){this.AddButton(dby, 5, 0x19,0x19, 10, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x19, 2422); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsMonk1 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }
				case 1 : { if ( HasSpell( from, 250 ) ) { new AstralProjection( from, null ).Cast(); from.SendGump( new SpellBarsMonk1( from ) ); } break; }
				case 2 : { if ( HasSpell( from, 251 ) ) { new AstralTravel( from, null ).Cast(); from.SendGump( new SpellBarsMonk1( from ) ); } break; }
				case 3 : { if ( HasSpell( from, 252 ) ) { new CreateRobe( from, null ).Cast(); from.SendGump( new SpellBarsMonk1( from ) ); } break; }
				case 4 : { if ( HasSpell( from, 253 ) ) { new GentleTouch( from, null ).Cast(); from.SendGump( new SpellBarsMonk1( from ) ); } break; }
				case 5 : { if ( HasSpell( from, 254 ) ) { new Leap( from, null ).Cast(); from.SendGump( new SpellBarsMonk1( from ) ); } break; }
				case 6 : { if ( HasSpell( from, 255 ) ) { new PsionicBlast( from, null ).Cast(); from.SendGump( new SpellBarsMonk1( from ) ); } break; }
				case 7 : { if ( HasSpell( from, 256 ) ) { new PsychicWall( from, null ).Cast(); from.SendGump( new SpellBarsMonk1( from ) ); } break; }
				case 8 : { if ( HasSpell( from, 257 ) ) { new PurityOfBody( from, null ).Cast(); from.SendGump( new SpellBarsMonk1( from ) ); } break; }
				case 9 : { if ( HasSpell( from, 258 ) ) { new QuiveringPalm( from, null ).Cast(); from.SendGump( new SpellBarsMonk1( from ) ); } break; }
				case 10 : { if ( HasSpell( from, 259 ) ) { new WindRunner( from, null ).Cast(); from.SendGump( new SpellBarsMonk1( from ) ); } break; }
			}
		}
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Server.Gumps 
{
    public class SpellBarsMonk2 : Gump
    {
		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );
			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void Initialize()
		{
            CommandSystem.Register( "monktool2", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "monktool2" )]
		[Description( "Opens Spell Bar For Monks - 2." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( SpellBarsMonk2 ) );
			from.SendGump( new SpellBarsMonk2( from ) );
        }
   
		public SpellBarsMonk2 ( Mobile from ) : base ( 10,10 )
        {
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);

			if ( ToolBarUpdates.GetToolBarSetting( from, 12, "SetupBarsMonk2" ) > 0 )
			{
				this.AddImage(7, 0, 0x973, 2422);
				int dby = 53;

				if ( HasSpell( from, 250 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsMonk2" ) == 1){this.AddButton(5, dby, 0x500E,0x500E, 1, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x500E, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Astral Projection"); } }
				if ( HasSpell( from, 251 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsMonk2" ) == 1){this.AddButton(5, dby, 0x410,0x410, 2, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x410, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Astral Travel"); } }
				if ( HasSpell( from, 252 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsMonk2" ) == 1){this.AddButton(5, dby, 0x15,0x15, 3, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x15, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Create Robe"); } }
				if ( HasSpell( from, 253 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsMonk2" ) == 1){this.AddButton(5, dby, 0x971,0x971, 4, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x971, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Gentle Touch"); } }
				if ( HasSpell( from, 254 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsMonk2" ) == 1){this.AddButton(5, dby, 0x4B2,0x4B2, 5, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x4B2, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Leap"); } }
				if ( HasSpell( from, 255 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsMonk2" ) == 1){this.AddButton(5, dby, 0x5DC2,0x5DC2, 6, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5DC2, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Psionic Blast"); } }
				if ( HasSpell( from, 256 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsMonk2" ) == 1){this.AddButton(5, dby, 0x1A,0x1A, 7, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x1A, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Psychic Wall"); } }
				if ( HasSpell( from, 257 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsMonk2" ) == 1){this.AddButton(5, dby, 0x96D,0x96D, 8, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x96D, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Purity of Body"); } }
				if ( HasSpell( from, 258 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsMonk2" ) == 1){this.AddButton(5, dby, 0x5001,0x5001, 9, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x5001, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Quivering Palm"); } }
				if ( HasSpell( from, 259 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsMonk2" ) == 1){this.AddButton(5, dby, 0x19,0x19, 10, GumpButtonType.Reply, 1); this.AddImage(5, dby, 0x19, 2422); dby = dby + 45; if ( ToolBarUpdates.GetToolBarSetting( from, 11, "SetupBarsMonk2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"Wind Runner"); } }
			}
			else			
			{			
				this.AddImage(0, 0, 0x973, 2422);		
				int dby = 50;		

				if ( HasSpell( from, 250 ) && ToolBarUpdates.GetToolBarSetting( from, 1, "SetupBarsMonk2" ) == 1){this.AddButton(dby, 5, 0x500E,0x500E, 1, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x500E, 2422); dby = dby + 45;}
				if ( HasSpell( from, 251 ) && ToolBarUpdates.GetToolBarSetting( from, 2, "SetupBarsMonk2" ) == 1){this.AddButton(dby, 5, 0x410,0x410, 2, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x410, 2422); dby = dby + 45;}
				if ( HasSpell( from, 252 ) && ToolBarUpdates.GetToolBarSetting( from, 3, "SetupBarsMonk2" ) == 1){this.AddButton(dby, 5, 0x15,0x15, 3, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x15, 2422); dby = dby + 45;}
				if ( HasSpell( from, 253 ) && ToolBarUpdates.GetToolBarSetting( from, 4, "SetupBarsMonk2" ) == 1){this.AddButton(dby, 5, 0x971,0x971, 4, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x971, 2422); dby = dby + 45;}
				if ( HasSpell( from, 254 ) && ToolBarUpdates.GetToolBarSetting( from, 5, "SetupBarsMonk2" ) == 1){this.AddButton(dby, 5, 0x4B2,0x4B2, 5, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x4B2, 2422); dby = dby + 45;}
				if ( HasSpell( from, 255 ) && ToolBarUpdates.GetToolBarSetting( from, 6, "SetupBarsMonk2" ) == 1){this.AddButton(dby, 5, 0x5DC2,0x5DC2, 6, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5DC2, 2422); dby = dby + 45;}
				if ( HasSpell( from, 256 ) && ToolBarUpdates.GetToolBarSetting( from, 7, "SetupBarsMonk2" ) == 1){this.AddButton(dby, 5, 0x1A,0x1A, 7, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x1A, 2422); dby = dby + 45;}
				if ( HasSpell( from, 257 ) && ToolBarUpdates.GetToolBarSetting( from, 8, "SetupBarsMonk2" ) == 1){this.AddButton(dby, 5, 0x96D,0x96D, 8, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x96D, 2422); dby = dby + 45;}
				if ( HasSpell( from, 258 ) && ToolBarUpdates.GetToolBarSetting( from, 9, "SetupBarsMonk2" ) == 1){this.AddButton(dby, 5, 0x5001,0x5001, 9, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x5001, 2422); dby = dby + 45;}
				if ( HasSpell( from, 259 ) && ToolBarUpdates.GetToolBarSetting( from, 10, "SetupBarsMonk2" ) == 1){this.AddButton(dby, 5, 0x19,0x19, 10, GumpButtonType.Reply, 1); this.AddImage(dby, 5, 0x19, 2422); dby = dby + 45;}
			}
		}
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			from.CloseGump( typeof( SpellBarsMonk2 ) );

			switch ( info.ButtonID ) 
			{
				case 0: { break; }
				case 1 : { if ( HasSpell( from, 250 ) ) { new AstralProjection( from, null ).Cast(); from.SendGump( new SpellBarsMonk2( from ) ); } break; }
				case 2 : { if ( HasSpell( from, 251 ) ) { new AstralTravel( from, null ).Cast(); from.SendGump( new SpellBarsMonk2( from ) ); } break; }
				case 3 : { if ( HasSpell( from, 252 ) ) { new CreateRobe( from, null ).Cast(); from.SendGump( new SpellBarsMonk2( from ) ); } break; }
				case 4 : { if ( HasSpell( from, 253 ) ) { new GentleTouch( from, null ).Cast(); from.SendGump( new SpellBarsMonk2( from ) ); } break; }
				case 5 : { if ( HasSpell( from, 254 ) ) { new Leap( from, null ).Cast(); from.SendGump( new SpellBarsMonk2( from ) ); } break; }
				case 6 : { if ( HasSpell( from, 255 ) ) { new PsionicBlast( from, null ).Cast(); from.SendGump( new SpellBarsMonk2( from ) ); } break; }
				case 7 : { if ( HasSpell( from, 256 ) ) { new PsychicWall( from, null ).Cast(); from.SendGump( new SpellBarsMonk2( from ) ); } break; }
				case 8 : { if ( HasSpell( from, 257 ) ) { new PurityOfBody( from, null ).Cast(); from.SendGump( new SpellBarsMonk2( from ) ); } break; }
				case 9 : { if ( HasSpell( from, 258 ) ) { new QuiveringPalm( from, null ).Cast(); from.SendGump( new SpellBarsMonk2( from ) ); } break; }
				case 10 : { if ( HasSpell( from, 259 ) ) { new WindRunner( from, null ).Cast(); from.SendGump( new SpellBarsMonk2( from ) ); } break; }
			}
		}
    }
}