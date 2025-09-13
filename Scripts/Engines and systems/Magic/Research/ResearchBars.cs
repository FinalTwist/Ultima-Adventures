using System;
using Server;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Misc;
using Server.Commands;
using Server.Commands.Generic;
using Server.Spells;
using Server.Spells.Research;
using Server.Prompts;
using Server.Gumps;
using Server.Items;

namespace Server.Misc
{
    class ResearchClose1
    {
		public static void Initialize()
		{
            CommandSystem.Register( "researchclose1", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "researchclose1" )]
		[Description( "Close Spell Bar Windows For Research - 1." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SetupBarsResearch1 ) );
				from.CloseGump( typeof( SpellBarsResearch1 ) );
			}
        }
    }

    class ResearchClose2
    {
		public static void Initialize()
		{
            CommandSystem.Register( "researchclose2", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "researchclose2" )]
		[Description( "Close Spell Bar Windows For Research - 2." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SetupBarsResearch2 ) );
				from.CloseGump( typeof( SpellBarsResearch2 ) );
			}
        }
    }

    class ResearchClose3
    {
		public static void Initialize()
		{
            CommandSystem.Register( "researchclose3", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "researchclose3" )]
		[Description( "Close Spell Bar Windows For Research - 3." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SetupBarsResearch3 ) );
				from.CloseGump( typeof( SpellBarsResearch3 ) );
			}
        }
    }

    class ResearchClose4
    {
		public static void Initialize()
		{
            CommandSystem.Register( "researchclose4", AccessLevel.Player, new CommandEventHandler( CloseBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "researchclose4" )]
		[Description( "Close Spell Bar Windows For Research - 4." )]
		public static void CloseBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SetupBarsResearch4 ) );
				from.CloseGump( typeof( SpellBarsResearch4 ) );
			}
        }
    }

    class ResearchBarSettings
    {
		public static Item ResearchMaterials( Mobile m )
		{
			Item bag = null;

			if ( m.Backpack.FindItemByType( typeof ( ResearchBag ) ) != null )
			{
				Item sack = m.Backpack.FindItemByType( typeof ( ResearchBag ) );
				ResearchBag pouch = (ResearchBag)sack;
				if ( pouch.BagOwner == m )
				{
					bag = sack;
				}
			}

			return bag;
		}

		public static void UpdateToolBar( Mobile m, int nChange, string ToolBar, int nTotal )
		{
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( m ) != null )
			{
				ResearchBarSettings.InitializeToolBar( m, ToolBar );
				ResearchBag bag = (ResearchBag)( Server.Misc.ResearchBarSettings.ResearchMaterials( m ) );

				string ToolBarSetting = "";

				if ( ToolBar == "SetupBarsResearch1" ){ ToolBarSetting = bag.BarsCast1; }
				else if ( ToolBar == "SetupBarsResearch2" ){ ToolBarSetting = bag.BarsCast2; }
				else if ( ToolBar == "SetupBarsResearch3" ){ ToolBarSetting = bag.BarsCast3; }
				else if ( ToolBar == "SetupBarsResearch4" ){ ToolBarSetting = bag.BarsCast4; }

				string[] eachSetting = ToolBarSetting.Split('#');
				int nLine = 1;
				string newSettings = "";

				foreach (string eachSettings in eachSetting)
				{
					if ( nLine == nChange )
					{
						string sChange = "0";
						if ( eachSettings == "0" ){ sChange = "1"; }
						newSettings = newSettings + sChange + "#";
					}
					else if ( nLine > nTotal )
					{
					}
					else
					{
						newSettings = newSettings + eachSettings + "#";
					}
					nLine++;
				}

				if ( ToolBar == "SetupBarsResearch1" ){ bag.BarsCast1 = newSettings; }
				else if ( ToolBar == "SetupBarsResearch2" ){ bag.BarsCast2 = newSettings; }
				else if ( ToolBar == "SetupBarsResearch3" ){ bag.BarsCast3 = newSettings; }
				else if ( ToolBar == "SetupBarsResearch4" ){ bag.BarsCast4 = newSettings; }
			}
		}

		public static void InitializeToolBar( Mobile m, string ToolBar )
		{
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( m ) != null )
			{
				ResearchBag bag = (ResearchBag)( Server.Misc.ResearchBarSettings.ResearchMaterials( m ) );
				if ( ToolBar == "SetupBarsResearch1" && bag.BarsCast1 == null ){ bag.BarsCast1 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
				else if ( ToolBar == "SetupBarsResearch2" && bag.BarsCast2 == null ){ bag.BarsCast2 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
				else if ( ToolBar == "SetupBarsResearch3" && bag.BarsCast3 == null ){ bag.BarsCast3 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
				else if ( ToolBar == "SetupBarsResearch4" && bag.BarsCast4 == null ){ bag.BarsCast4 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
			}
		}

		public static int GetToolBarSetting( Mobile m, int nSetting, string ToolBar )
		{
			int nValue = 0;
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( m ) != null )
			{
				string sSetting = "0";

				ResearchBarSettings.InitializeToolBar( m, ToolBar );
				ResearchBag bag = (ResearchBag)( Server.Misc.ResearchBarSettings.ResearchMaterials( m ) );

				string ToolBarSetting = "";

				if ( ToolBar == "SetupBarsResearch1" ){ ToolBarSetting = bag.BarsCast1; }
				else if ( ToolBar == "SetupBarsResearch2" ){ ToolBarSetting = bag.BarsCast2; }
				else if ( ToolBar == "SetupBarsResearch3" ){ ToolBarSetting = bag.BarsCast3; }
				else if ( ToolBar == "SetupBarsResearch4" ){ ToolBarSetting = bag.BarsCast4; }

				string[] eachSetting = ToolBarSetting.Split('#');
				int nLine = 1;

				foreach (string eachSettings in eachSetting)
				{
					if ( nLine == nSetting ){ sSetting = eachSettings; }
					nLine++;
				}

				nValue = Convert.ToInt32(sSetting);
			}
			return nValue;
		}

		public static bool HasSpell( Mobile from, int spellID )
		{
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				ResearchBag bag = (ResearchBag)( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) );
				return Server.Misc.Research.GetResearch( bag, spellID );
			}
			return false;
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
    public class SpellBarsResearch1 : Gump
    {
		public static void Initialize()
		{
            CommandSystem.Register( "researchtool1", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "researchtool1" )]
		[Description( "Opens Spell Bar For Researchers - 1." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SpellBarsResearch1 ) );
				from.SendGump( new SpellBarsResearch1( from ) );
			}
        }

        public SpellBarsResearch1 ( Mobile from ) : base ( 10,10 )
        {
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				this.Closable=false;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;
				this.AddPage(0);

				if ( ResearchBarSettings.GetToolBarSetting( from, 66, "SetupBarsResearch1" ) > 0 )
				{
					this.AddImage(7, 0, 11194, 0);
					int dby = 53;
					int index = 0;
					index = 1; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 99, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 2; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 1, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 3; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 2, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 4; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 3, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 5; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 4, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 6; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 5, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 7; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 6, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 8; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 7, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 9; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 8, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 10; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 9, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 11; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 10, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 12; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 11, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 13; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 12, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 14; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 13, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 15; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 14, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 16; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 15, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 17; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 16, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 18; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 17, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 19; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 18, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 20; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 19, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 21; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 20, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 22; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 21, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 23; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 22, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 24; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 23, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 25; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 24, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 26; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 25, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 27; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 26, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 28; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 27, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 29; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 28, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 30; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 29, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 31; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 30, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 32; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 31, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 33; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 32, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 34; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 33, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 35; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 34, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 36; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 35, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 37; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 36, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 38; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 37, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 39; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 38, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 40; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 39, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 41; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 40, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 42; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 41, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 43; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 42, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 44; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 43, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 45; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 44, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 46; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 45, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 47; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 46, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 48; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 47, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 49; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 48, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 50; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 49, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 51; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 50, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 52; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 51, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 53; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 52, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 54; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 53, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 55; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 54, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 56; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 55, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 57; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 56, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 58; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 57, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 59; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 58, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 60; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 59, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 61; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 60, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 62; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 61, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 63; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 62, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 64; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 63, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch1" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
				}
				else
				{
					this.AddImage(0, 0, 11194, 0);
					int dby = 50;
					int index = 0;
					index = 1; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 99, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 2; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 1, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 3; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 2, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 4; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 3, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 5; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 4, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 6; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 5, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 7; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 6, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 8; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 7, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 9; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 8, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 10; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 9, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 11; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 10, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 12; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 11, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 13; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 12, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 14; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 13, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 15; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 14, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 16; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 15, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 17; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 16, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 18; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 17, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 19; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 18, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 20; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 19, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 21; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 20, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 22; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 21, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 23; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 22, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 24; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 23, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 25; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 24, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 26; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 25, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 27; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 26, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 28; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 27, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 29; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 28, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 30; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 29, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 31; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 30, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 32; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 31, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 33; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 32, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 34; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 33, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 35; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 34, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 36; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 35, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 37; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 36, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 38; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 37, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 39; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 38, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 40; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 39, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 41; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 40, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 42; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 41, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 43; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 42, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 44; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 43, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 45; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 44, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 46; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 45, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 47; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 46, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 48; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 47, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 49; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 48, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 50; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 49, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 51; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 50, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 52; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 51, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 53; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 52, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 54; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 53, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 55; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 54, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 56; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 55, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 57; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 56, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 58; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 57, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 59; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 58, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 60; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 59, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 61; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 60, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 62; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 61, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 63; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 62, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 64; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch1" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 63, GumpButtonType.Reply, 1); dby = dby + 45;}
				}
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SpellBarsResearch1 ) );

				switch ( info.ButtonID )
				{
					case 0: { break; }
					case 99: { if ( ResearchBarSettings.HasSpell( from, 1 ) ) { new ResearchConjure( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 1: { if ( ResearchBarSettings.HasSpell( from, 2 ) ) { new ResearchDeathSpeak( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 2: { if ( ResearchBarSettings.HasSpell( from, 3 ) ) { new ResearchSneak( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 3: { if ( ResearchBarSettings.HasSpell( from, 4 ) ) { new ResearchCreateFire( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 4: { if ( ResearchBarSettings.HasSpell( from, 5 ) ) { new ResearchSummonElectricalElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 5: { if ( ResearchBarSettings.HasSpell( from, 6 ) ) { new ResearchConfusionBlast( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 6: { if ( ResearchBarSettings.HasSpell( from, 7 ) ) { new ResearchSeeTruth( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 7: { if ( ResearchBarSettings.HasSpell( from, 8 ) ) { new ResearchIcicle( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 8: { if ( ResearchBarSettings.HasSpell( from, 9 ) ) { new ResearchExtinguish( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 9: { if ( ResearchBarSettings.HasSpell( from, 10 ) ) { new ResearchRockFlesh( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 10: { if ( ResearchBarSettings.HasSpell( from, 11 ) ) { new ResearchMassMight( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 11: { if ( ResearchBarSettings.HasSpell( from, 12 ) ) { new ResearchEndureCold( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 12: { if ( ResearchBarSettings.HasSpell( from, 13 ) ) { new ResearchSummonWeedElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 13: { if ( ResearchBarSettings.HasSpell( from, 14 ) ) { new ResearchSummonCreature( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 14: { if ( ResearchBarSettings.HasSpell( from, 15 ) ) { new ResearchHealingTouch( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 15: { if ( ResearchBarSettings.HasSpell( from, 16 ) ) { new ResearchSnowBall( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 16: { if ( ResearchBarSettings.HasSpell( from, 17 ) ) { new ResearchClone( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 17: { if ( ResearchBarSettings.HasSpell( from, 18 ) ) { new ResearchGrantPeace( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 18: { if ( ResearchBarSettings.HasSpell( from, 19 ) ) { new ResearchSleep( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 19: { if ( ResearchBarSettings.HasSpell( from, 20 ) ) { new ResearchEndureHeat( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 20: { if ( ResearchBarSettings.HasSpell( from, 21 ) ) { new ResearchSummonIceElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 21: { if ( ResearchBarSettings.HasSpell( from, 22 ) ) { new ResearchEtherealTravel( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 22: { if ( ResearchBarSettings.HasSpell( from, 23 ) ) { new ResearchWizardEye( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 23: { if ( ResearchBarSettings.HasSpell( from, 24 ) ) { new ResearchFrostField( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 24: { if ( ResearchBarSettings.HasSpell( from, 25 ) ) { new ResearchCreateGold( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 25: { if ( ResearchBarSettings.HasSpell( from, 26 ) ) { new ResearchSummonDead( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 26: { if ( ResearchBarSettings.HasSpell( from, 27 ) ) { new ResearchCauseFear( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 27: { if ( ResearchBarSettings.HasSpell( from, 28 ) ) { new ResearchIgnite( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 28: { if ( ResearchBarSettings.HasSpell( from, 29 ) ) { new ResearchSummonMudElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 29: { if ( ResearchBarSettings.HasSpell( from, 30 ) ) { new ResearchBanishDaemon( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 30: { if ( ResearchBarSettings.HasSpell( from, 31 ) ) { new ResearchFadefromSight( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 31: { if ( ResearchBarSettings.HasSpell( from, 32 ) ) { new ResearchGasCloud( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 32: { if ( ResearchBarSettings.HasSpell( from, 33 ) ) { new ResearchSwarm( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 33: { if ( ResearchBarSettings.HasSpell( from, 34 ) ) { new ResearchMaskofDeath( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 34: { if ( ResearchBarSettings.HasSpell( from, 35 ) ) { new ResearchEnchant( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 35: { if ( ResearchBarSettings.HasSpell( from, 36 ) ) { new ResearchFlameBolt( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 36: { if ( ResearchBarSettings.HasSpell( from, 37 ) ) { new ResearchSummonGemElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 37: { if ( ResearchBarSettings.HasSpell( from, 38 ) ) { new ResearchCallDestruction( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 38: { if ( ResearchBarSettings.HasSpell( from, 39 ) ) { new ResearchDivination( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 39: { if ( ResearchBarSettings.HasSpell( from, 40 ) ) { new ResearchFrostStrike( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 40: { if ( ResearchBarSettings.HasSpell( from, 41 ) ) { new ResearchMagicSteed( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 41: { if ( ResearchBarSettings.HasSpell( from, 42 ) ) { new ResearchCreateGolem( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 42: { if ( ResearchBarSettings.HasSpell( from, 43 ) ) { new ResearchSleepField( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 43: { if ( ResearchBarSettings.HasSpell( from, 44 ) ) { new ResearchConflagration( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 44: { if ( ResearchBarSettings.HasSpell( from, 45 ) ) { new ResearchSummonAcidElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 45: { if ( ResearchBarSettings.HasSpell( from, 46 ) ) { new ResearchMeteorShower( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 46: { if ( ResearchBarSettings.HasSpell( from, 47 ) ) { new ResearchIntervention( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 47: { if ( ResearchBarSettings.HasSpell( from, 48 ) ) { new ResearchHailStorm( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 48: { if ( ResearchBarSettings.HasSpell( from, 49 ) ) { new ResearchAerialServant( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 49: { if ( ResearchBarSettings.HasSpell( from, 50 ) ) { new ResearchOpenGround( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 50: { if ( ResearchBarSettings.HasSpell( from, 51 ) ) { new ResearchCharm( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 51: { if ( ResearchBarSettings.HasSpell( from, 52 ) ) { new ResearchExplosion( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 52: { if ( ResearchBarSettings.HasSpell( from, 53 ) ) { new ResearchSummonPoisonElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
          case 53: { if ( ResearchBarSettings.HasSpell( from, 54 ) ) { new ResearchSummonDevil( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 54: { if ( ResearchBarSettings.HasSpell( from, 55 ) ) { new ResearchAirWalk( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 55: { if ( ResearchBarSettings.HasSpell( from, 56 ) ) { new ResearchAvalanche( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 56: { if ( ResearchBarSettings.HasSpell( from, 57 ) ) { new ResearchDeathVortex( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 57: { if ( ResearchBarSettings.HasSpell( from, 58 ) ) { new ResearchWithstandDeath( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 58: { if ( ResearchBarSettings.HasSpell( from, 59 ) ) { new ResearchMassSleep( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 59: { if ( ResearchBarSettings.HasSpell( from, 60 ) ) { new ResearchRingofFire( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 60: { if ( ResearchBarSettings.HasSpell( from, 61 ) ) { new ResearchSummonBloodElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 61: { if ( ResearchBarSettings.HasSpell( from, 62 ) ) { new ResearchDevastation( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 62: { if ( ResearchBarSettings.HasSpell( from, 63 ) ) { new ResearchRestoration( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
					case 63: { if ( ResearchBarSettings.HasSpell( from, 64 ) ) { new ResearchMassDeath( from, null ).Cast(); from.SendGump( new SpellBarsResearch1( from ) ); } break; }
				}
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
    public class SpellBarsResearch2 : Gump
    {
		public static void Initialize()
		{
            CommandSystem.Register( "researchtool2", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "researchtool2" )]
		[Description( "Opens Spell Bar For Researchers - 2." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SpellBarsResearch2 ) );
				from.SendGump( new SpellBarsResearch2( from ) );
			}
        }

        public SpellBarsResearch2 ( Mobile from ) : base ( 10,10 )
        {
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				this.Closable=false;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;
				this.AddPage(0);

				if ( ResearchBarSettings.GetToolBarSetting( from, 66, "SetupBarsResearch2" ) > 0 )
				{
					this.AddImage(7, 0, 11194, 0);
					int dby = 53;
					int index = 0;
					index = 1; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 99, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 2; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 1, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 3; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 2, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 4; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 3, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 5; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 4, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 6; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 5, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 7; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 6, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 8; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 7, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 9; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 8, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 10; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 9, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 11; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 10, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 12; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 11, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 13; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 12, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 14; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 13, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 15; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 14, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 16; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 15, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 17; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 16, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 18; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 17, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 19; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 18, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 20; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 19, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 21; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 20, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 22; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 21, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 23; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 22, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 24; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 23, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 25; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 24, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 26; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 25, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 27; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 26, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 28; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 27, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 29; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 28, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 30; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 29, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 31; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 30, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 32; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 31, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 33; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 32, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 34; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 33, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 35; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 34, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 36; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 35, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 37; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 36, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 38; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 37, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 39; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 38, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 40; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 39, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 41; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 40, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 42; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 41, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 43; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 42, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 44; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 43, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 45; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 44, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 46; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 45, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 47; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 46, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 48; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 47, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 49; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 48, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 50; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 49, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 51; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 50, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 52; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 51, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 53; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 52, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 54; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 53, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 55; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 54, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 56; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 55, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 57; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 56, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 58; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 57, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 59; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 58, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 60; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 59, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 61; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 60, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 62; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 61, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 63; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 62, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 64; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 63, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch2" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
				}
				else
				{
					this.AddImage(0, 0, 11194, 0);
					int dby = 50;
					int index = 0;
					index = 1; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 99, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 2; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 1, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 3; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 2, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 4; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 3, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 5; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 4, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 6; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 5, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 7; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 6, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 8; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 7, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 9; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 8, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 10; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 9, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 11; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 10, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 12; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 11, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 13; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 12, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 14; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 13, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 15; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 14, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 16; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 15, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 17; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 16, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 18; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 17, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 19; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 18, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 20; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 19, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 21; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 20, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 22; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 21, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 23; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 22, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 24; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 23, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 25; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 24, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 26; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 25, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 27; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 26, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 28; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 27, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 29; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 28, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 30; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 29, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 31; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 30, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 32; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 31, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 33; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 32, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 34; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 33, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 35; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 34, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 36; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 35, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 37; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 36, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 38; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 37, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 39; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 38, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 40; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 39, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 41; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 40, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 42; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 41, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 43; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 42, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 44; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 43, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 45; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 44, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 46; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 45, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 47; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 46, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 48; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 47, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 49; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 48, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 50; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 49, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 51; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 50, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 52; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 51, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 53; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 52, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 54; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 53, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 55; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 54, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 56; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 55, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 57; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 56, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 58; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 57, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 59; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 58, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 60; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 59, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 61; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 60, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 62; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 61, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 63; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 62, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 64; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch2" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 63, GumpButtonType.Reply, 1); dby = dby + 45;}
				}
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SpellBarsResearch2 ) );

				switch ( info.ButtonID )
				{
					case 0: { break; }
					case 99: { if ( ResearchBarSettings.HasSpell( from, 1 ) ) { new ResearchConjure( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 1: { if ( ResearchBarSettings.HasSpell( from, 2 ) ) { new ResearchDeathSpeak( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 2: { if ( ResearchBarSettings.HasSpell( from, 3 ) ) { new ResearchSneak( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 3: { if ( ResearchBarSettings.HasSpell( from, 4 ) ) { new ResearchCreateFire( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 4: { if ( ResearchBarSettings.HasSpell( from, 5 ) ) { new ResearchSummonElectricalElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 5: { if ( ResearchBarSettings.HasSpell( from, 6 ) ) { new ResearchConfusionBlast( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 6: { if ( ResearchBarSettings.HasSpell( from, 7 ) ) { new ResearchSeeTruth( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 7: { if ( ResearchBarSettings.HasSpell( from, 8 ) ) { new ResearchIcicle( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 8: { if ( ResearchBarSettings.HasSpell( from, 9 ) ) { new ResearchExtinguish( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 9: { if ( ResearchBarSettings.HasSpell( from, 10 ) ) { new ResearchRockFlesh( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 10: { if ( ResearchBarSettings.HasSpell( from, 11 ) ) { new ResearchMassMight( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 11: { if ( ResearchBarSettings.HasSpell( from, 12 ) ) { new ResearchEndureCold( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 12: { if ( ResearchBarSettings.HasSpell( from, 13 ) ) { new ResearchSummonWeedElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 13: { if ( ResearchBarSettings.HasSpell( from, 14 ) ) { new ResearchSummonCreature( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 14: { if ( ResearchBarSettings.HasSpell( from, 15 ) ) { new ResearchHealingTouch( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 15: { if ( ResearchBarSettings.HasSpell( from, 16 ) ) { new ResearchSnowBall( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 16: { if ( ResearchBarSettings.HasSpell( from, 17 ) ) { new ResearchClone( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 17: { if ( ResearchBarSettings.HasSpell( from, 18 ) ) { new ResearchGrantPeace( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 18: { if ( ResearchBarSettings.HasSpell( from, 19 ) ) { new ResearchSleep( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 19: { if ( ResearchBarSettings.HasSpell( from, 20 ) ) { new ResearchEndureHeat( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 20: { if ( ResearchBarSettings.HasSpell( from, 21 ) ) { new ResearchSummonIceElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 21: { if ( ResearchBarSettings.HasSpell( from, 22 ) ) { new ResearchEtherealTravel( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 22: { if ( ResearchBarSettings.HasSpell( from, 23 ) ) { new ResearchWizardEye( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 23: { if ( ResearchBarSettings.HasSpell( from, 24 ) ) { new ResearchFrostField( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 24: { if ( ResearchBarSettings.HasSpell( from, 25 ) ) { new ResearchCreateGold( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 25: { if ( ResearchBarSettings.HasSpell( from, 26 ) ) { new ResearchSummonDead( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 26: { if ( ResearchBarSettings.HasSpell( from, 27 ) ) { new ResearchCauseFear( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 27: { if ( ResearchBarSettings.HasSpell( from, 28 ) ) { new ResearchIgnite( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 28: { if ( ResearchBarSettings.HasSpell( from, 29 ) ) { new ResearchSummonMudElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 29: { if ( ResearchBarSettings.HasSpell( from, 30 ) ) { new ResearchBanishDaemon( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 30: { if ( ResearchBarSettings.HasSpell( from, 31 ) ) { new ResearchFadefromSight( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 31: { if ( ResearchBarSettings.HasSpell( from, 32 ) ) { new ResearchGasCloud( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 32: { if ( ResearchBarSettings.HasSpell( from, 33 ) ) { new ResearchSwarm( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 33: { if ( ResearchBarSettings.HasSpell( from, 34 ) ) { new ResearchMaskofDeath( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 34: { if ( ResearchBarSettings.HasSpell( from, 35 ) ) { new ResearchEnchant( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 35: { if ( ResearchBarSettings.HasSpell( from, 36 ) ) { new ResearchFlameBolt( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 36: { if ( ResearchBarSettings.HasSpell( from, 37 ) ) { new ResearchSummonGemElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
          case 37: { if ( ResearchBarSettings.HasSpell( from, 38 ) ) { new ResearchCallDestruction( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 38: { if ( ResearchBarSettings.HasSpell( from, 39 ) ) { new ResearchDivination( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 39: { if ( ResearchBarSettings.HasSpell( from, 40 ) ) { new ResearchFrostStrike( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 40: { if ( ResearchBarSettings.HasSpell( from, 41 ) ) { new ResearchMagicSteed( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 41: { if ( ResearchBarSettings.HasSpell( from, 42 ) ) { new ResearchCreateGolem( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 42: { if ( ResearchBarSettings.HasSpell( from, 43 ) ) { new ResearchSleepField( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 43: { if ( ResearchBarSettings.HasSpell( from, 44 ) ) { new ResearchConflagration( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 44: { if ( ResearchBarSettings.HasSpell( from, 45 ) ) { new ResearchSummonAcidElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
          case 45: { if ( ResearchBarSettings.HasSpell( from, 46 ) ) { new ResearchMeteorShower( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 46: { if ( ResearchBarSettings.HasSpell( from, 47 ) ) { new ResearchIntervention( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 47: { if ( ResearchBarSettings.HasSpell( from, 48 ) ) { new ResearchHailStorm( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 48: { if ( ResearchBarSettings.HasSpell( from, 49 ) ) { new ResearchAerialServant( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 49: { if ( ResearchBarSettings.HasSpell( from, 50 ) ) { new ResearchOpenGround( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 50: { if ( ResearchBarSettings.HasSpell( from, 51 ) ) { new ResearchCharm( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 51: { if ( ResearchBarSettings.HasSpell( from, 52 ) ) { new ResearchExplosion( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 52: { if ( ResearchBarSettings.HasSpell( from, 53 ) ) { new ResearchSummonPoisonElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
          case 53: { if ( ResearchBarSettings.HasSpell( from, 54 ) ) { new ResearchSummonDevil( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 54: { if ( ResearchBarSettings.HasSpell( from, 55 ) ) { new ResearchAirWalk( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 55: { if ( ResearchBarSettings.HasSpell( from, 56 ) ) { new ResearchAvalanche( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 56: { if ( ResearchBarSettings.HasSpell( from, 57 ) ) { new ResearchDeathVortex( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 57: { if ( ResearchBarSettings.HasSpell( from, 58 ) ) { new ResearchWithstandDeath( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 58: { if ( ResearchBarSettings.HasSpell( from, 59 ) ) { new ResearchMassSleep( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 59: { if ( ResearchBarSettings.HasSpell( from, 60 ) ) { new ResearchRingofFire( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 60: { if ( ResearchBarSettings.HasSpell( from, 61 ) ) { new ResearchSummonBloodElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 61: { if ( ResearchBarSettings.HasSpell( from, 62 ) ) { new ResearchDevastation( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 62: { if ( ResearchBarSettings.HasSpell( from, 63 ) ) { new ResearchRestoration( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
					case 63: { if ( ResearchBarSettings.HasSpell( from, 64 ) ) { new ResearchMassDeath( from, null ).Cast(); from.SendGump( new SpellBarsResearch2( from ) ); } break; }
				}
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
    public class SpellBarsResearch3 : Gump
    {
		public static void Initialize()
		{
            CommandSystem.Register( "researchtool3", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "researchtool3" )]
		[Description( "Opens Spell Bar For Researchers - 3." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SpellBarsResearch3 ) );
				from.SendGump( new SpellBarsResearch3( from ) );
			}
        }

        public SpellBarsResearch3 ( Mobile from ) : base ( 10,10 )
        {
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				this.Closable=false;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;
				this.AddPage(0);

				if ( ResearchBarSettings.GetToolBarSetting( from, 66, "SetupBarsResearch3" ) > 0 )
				{
					this.AddImage(7, 0, 11194, 0);
					int dby = 53;
					int index = 0;
					index = 1; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 99, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 2; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 1, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 3; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 2, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 4; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 3, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 5; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 4, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 6; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 5, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 7; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 6, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 8; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 7, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 9; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 8, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 10; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 9, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 11; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 10, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 12; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 11, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 13; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 12, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 14; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 13, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 15; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 14, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 16; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 15, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 17; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 16, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 18; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 17, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 19; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 18, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 20; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 19, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 21; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 20, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 22; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 21, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 23; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 22, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 24; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 23, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 25; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 24, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 26; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 25, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 27; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 26, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 28; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 27, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 29; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 28, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 30; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 29, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 31; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 30, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 32; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 31, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 33; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 32, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 34; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 33, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 35; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 34, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 36; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 35, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 37; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 36, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 38; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 37, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 39; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 38, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 40; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 39, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 41; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 40, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 42; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 41, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 43; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 42, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 44; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 43, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 45; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 44, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 46; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 45, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 47; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 46, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 48; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 47, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 49; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 48, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 50; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 49, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 51; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 50, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 52; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 51, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 53; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 52, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 54; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 53, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 55; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 54, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 56; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 55, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 57; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 56, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 58; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 57, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 59; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 58, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 60; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 59, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 61; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 60, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 62; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 61, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 63; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 62, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 64; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 63, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch3" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
				}
				else
				{
					this.AddImage(0, 0, 11194, 0);
					int dby = 50;
					int index = 0;
					index = 1; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 99, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 2; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 1, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 3; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 2, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 4; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 3, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 5; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 4, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 6; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 5, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 7; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 6, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 8; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 7, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 9; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 8, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 10; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 9, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 11; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 10, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 12; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 11, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 13; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 12, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 14; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 13, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 15; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 14, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 16; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 15, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 17; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 16, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 18; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 17, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 19; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 18, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 20; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 19, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 21; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 20, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 22; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 21, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 23; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 22, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 24; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 23, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 25; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 24, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 26; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 25, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 27; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 26, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 28; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 27, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 29; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 28, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 30; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 29, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 31; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 30, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 32; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 31, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 33; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 32, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 34; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 33, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 35; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 34, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 36; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 35, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 37; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 36, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 38; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 37, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 39; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 38, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 40; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 39, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 41; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 40, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 42; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 41, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 43; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 42, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 44; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 43, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 45; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 44, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 46; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 45, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 47; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 46, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 48; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 47, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 49; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 48, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 50; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 49, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 51; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 50, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 52; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 51, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 53; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 52, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 54; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 53, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 55; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 54, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 56; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 55, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 57; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 56, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 58; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 57, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 59; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 58, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 60; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 59, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 61; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 60, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 62; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 61, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 63; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 62, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 64; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch3" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 63, GumpButtonType.Reply, 1); dby = dby + 45;}
				}
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SpellBarsResearch3 ) );

				switch ( info.ButtonID )
				{
					case 0: { break; }
					case 99: { if ( ResearchBarSettings.HasSpell( from, 1 ) ) { new ResearchConjure( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 1: { if ( ResearchBarSettings.HasSpell( from, 2 ) ) { new ResearchDeathSpeak( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 2: { if ( ResearchBarSettings.HasSpell( from, 3 ) ) { new ResearchSneak( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 3: { if ( ResearchBarSettings.HasSpell( from, 4 ) ) { new ResearchCreateFire( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 4: { if ( ResearchBarSettings.HasSpell( from, 5 ) ) { new ResearchSummonElectricalElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 5: { if ( ResearchBarSettings.HasSpell( from, 6 ) ) { new ResearchConfusionBlast( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 6: { if ( ResearchBarSettings.HasSpell( from, 7 ) ) { new ResearchSeeTruth( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 7: { if ( ResearchBarSettings.HasSpell( from, 8 ) ) { new ResearchIcicle( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 8: { if ( ResearchBarSettings.HasSpell( from, 9 ) ) { new ResearchExtinguish( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 9: { if ( ResearchBarSettings.HasSpell( from, 10 ) ) { new ResearchRockFlesh( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 10: { if ( ResearchBarSettings.HasSpell( from, 11 ) ) { new ResearchMassMight( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 11: { if ( ResearchBarSettings.HasSpell( from, 12 ) ) { new ResearchEndureCold( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 12: { if ( ResearchBarSettings.HasSpell( from, 13 ) ) { new ResearchSummonWeedElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 13: { if ( ResearchBarSettings.HasSpell( from, 14 ) ) { new ResearchSummonCreature( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 14: { if ( ResearchBarSettings.HasSpell( from, 15 ) ) { new ResearchHealingTouch( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 15: { if ( ResearchBarSettings.HasSpell( from, 16 ) ) { new ResearchSnowBall( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 16: { if ( ResearchBarSettings.HasSpell( from, 17 ) ) { new ResearchClone( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 17: { if ( ResearchBarSettings.HasSpell( from, 18 ) ) { new ResearchGrantPeace( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 18: { if ( ResearchBarSettings.HasSpell( from, 19 ) ) { new ResearchSleep( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 19: { if ( ResearchBarSettings.HasSpell( from, 20 ) ) { new ResearchEndureHeat( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 20: { if ( ResearchBarSettings.HasSpell( from, 21 ) ) { new ResearchSummonIceElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 21: { if ( ResearchBarSettings.HasSpell( from, 22 ) ) { new ResearchEtherealTravel( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 22: { if ( ResearchBarSettings.HasSpell( from, 23 ) ) { new ResearchWizardEye( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 23: { if ( ResearchBarSettings.HasSpell( from, 24 ) ) { new ResearchFrostField( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 24: { if ( ResearchBarSettings.HasSpell( from, 25 ) ) { new ResearchCreateGold( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 25: { if ( ResearchBarSettings.HasSpell( from, 26 ) ) { new ResearchSummonDead( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 26: { if ( ResearchBarSettings.HasSpell( from, 27 ) ) { new ResearchCauseFear( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 27: { if ( ResearchBarSettings.HasSpell( from, 28 ) ) { new ResearchIgnite( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 28: { if ( ResearchBarSettings.HasSpell( from, 29 ) ) { new ResearchSummonMudElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 29: { if ( ResearchBarSettings.HasSpell( from, 30 ) ) { new ResearchBanishDaemon( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 30: { if ( ResearchBarSettings.HasSpell( from, 31 ) ) { new ResearchFadefromSight( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 31: { if ( ResearchBarSettings.HasSpell( from, 32 ) ) { new ResearchGasCloud( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 32: { if ( ResearchBarSettings.HasSpell( from, 33 ) ) { new ResearchSwarm( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 33: { if ( ResearchBarSettings.HasSpell( from, 34 ) ) { new ResearchMaskofDeath( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 34: { if ( ResearchBarSettings.HasSpell( from, 35 ) ) { new ResearchEnchant( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 35: { if ( ResearchBarSettings.HasSpell( from, 36 ) ) { new ResearchFlameBolt( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 36: { if ( ResearchBarSettings.HasSpell( from, 37 ) ) { new ResearchSummonGemElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
          case 37: { if ( ResearchBarSettings.HasSpell( from, 38 ) ) { new ResearchCallDestruction( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 38: { if ( ResearchBarSettings.HasSpell( from, 39 ) ) { new ResearchDivination( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 39: { if ( ResearchBarSettings.HasSpell( from, 40 ) ) { new ResearchFrostStrike( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 40: { if ( ResearchBarSettings.HasSpell( from, 41 ) ) { new ResearchMagicSteed( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 41: { if ( ResearchBarSettings.HasSpell( from, 42 ) ) { new ResearchCreateGolem( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 42: { if ( ResearchBarSettings.HasSpell( from, 43 ) ) { new ResearchSleepField( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 43: { if ( ResearchBarSettings.HasSpell( from, 44 ) ) { new ResearchConflagration( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 44: { if ( ResearchBarSettings.HasSpell( from, 45 ) ) { new ResearchSummonAcidElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
          case 45: { if ( ResearchBarSettings.HasSpell( from, 46 ) ) { new ResearchMeteorShower( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 46: { if ( ResearchBarSettings.HasSpell( from, 47 ) ) { new ResearchIntervention( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 47: { if ( ResearchBarSettings.HasSpell( from, 48 ) ) { new ResearchHailStorm( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 48: { if ( ResearchBarSettings.HasSpell( from, 49 ) ) { new ResearchAerialServant( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 49: { if ( ResearchBarSettings.HasSpell( from, 50 ) ) { new ResearchOpenGround( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 50: { if ( ResearchBarSettings.HasSpell( from, 51 ) ) { new ResearchCharm( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 51: { if ( ResearchBarSettings.HasSpell( from, 52 ) ) { new ResearchExplosion( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 52: { if ( ResearchBarSettings.HasSpell( from, 53 ) ) { new ResearchSummonPoisonElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
          case 53: { if ( ResearchBarSettings.HasSpell( from, 54 ) ) { new ResearchSummonDevil( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 54: { if ( ResearchBarSettings.HasSpell( from, 55 ) ) { new ResearchAirWalk( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 55: { if ( ResearchBarSettings.HasSpell( from, 56 ) ) { new ResearchAvalanche( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 56: { if ( ResearchBarSettings.HasSpell( from, 57 ) ) { new ResearchDeathVortex( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 57: { if ( ResearchBarSettings.HasSpell( from, 58 ) ) { new ResearchWithstandDeath( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 58: { if ( ResearchBarSettings.HasSpell( from, 59 ) ) { new ResearchMassSleep( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 59: { if ( ResearchBarSettings.HasSpell( from, 60 ) ) { new ResearchRingofFire( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 60: { if ( ResearchBarSettings.HasSpell( from, 61 ) ) { new ResearchSummonBloodElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 61: { if ( ResearchBarSettings.HasSpell( from, 62 ) ) { new ResearchDevastation( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 62: { if ( ResearchBarSettings.HasSpell( from, 63 ) ) { new ResearchRestoration( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
					case 63: { if ( ResearchBarSettings.HasSpell( from, 64 ) ) { new ResearchMassDeath( from, null ).Cast(); from.SendGump( new SpellBarsResearch3( from ) ); } break; }
				}
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
    public class SpellBarsResearch4 : Gump
    {
		public static void Initialize()
		{
            CommandSystem.Register( "researchtool4", AccessLevel.Player, new CommandEventHandler( ToolBars_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "researchtool4" )]
		[Description( "Opens Spell Bar For Researchers - 4." )]
		public static void ToolBars_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SpellBarsResearch4 ) );
				from.SendGump( new SpellBarsResearch4( from ) );
			}
        }

        public SpellBarsResearch4 ( Mobile from ) : base ( 10,10 )
        {
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				this.Closable=false;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;
				this.AddPage(0);

				if ( ResearchBarSettings.GetToolBarSetting( from, 66, "SetupBarsResearch4" ) > 0 )
				{
					this.AddImage(7, 0, 11194, 0);
					int dby = 53;
					int index = 0;
					index = 1; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 99, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 2; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 1, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 3; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 2, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 4; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 3, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 5; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 4, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 6; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 5, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 7; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 6, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 8; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 7, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 9; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 8, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 10; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 9, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 11; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 10, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 12; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 11, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 13; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 12, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 14; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 13, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 15; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 14, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 16; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 15, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 17; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 16, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 18; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 17, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 19; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 18, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 20; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 19, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 21; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 20, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 22; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 21, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 23; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 22, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 24; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 23, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 25; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 24, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 26; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 25, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 27; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 26, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 28; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 27, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 29; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 28, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 30; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 29, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 31; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 30, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 32; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 31, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 33; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 32, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 34; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 33, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 35; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 34, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 36; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 35, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 37; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 36, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 38; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 37, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 39; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 38, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 40; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 39, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 41; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 40, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 42; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 41, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 43; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 42, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 44; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 43, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 45; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 44, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 46; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 45, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 47; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 46, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 48; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 47, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 49; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 48, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 50; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 49, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 51; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 50, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 52; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 51, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 53; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 52, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 54; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 53, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 55; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 54, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 56; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 55, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 57; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 56, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 58; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 57, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 59; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 58, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 60; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 59, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 61; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 60, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 62; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 61, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 63; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 62, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
					index = 64; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(5, dby, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 63, GumpButtonType.Reply, 1); dby = dby + 45; if ( ResearchBarSettings.GetToolBarSetting( from, 65, "SetupBarsResearch4" ) > 0 ){ AddLabel(59, (dby-34), 0x481, @"" + Server.Misc.Research.SpellInformation( index, 2 ) + ""); } }
				}
				else
				{
					this.AddImage(0, 0, 11194, 0);
					int dby = 50;
					int index = 0;
					index = 1; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 99, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 2; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 1, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 3; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 2, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 4; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 3, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 5; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 4, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 6; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 5, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 7; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 6, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 8; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 7, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 9; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 8, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 10; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 9, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 11; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 10, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 12; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 11, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 13; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 12, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 14; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 13, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 15; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 14, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 16; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 15, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 17; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 16, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 18; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 17, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 19; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 18, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 20; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 19, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 21; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 20, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 22; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 21, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 23; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 22, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 24; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 23, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 25; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 24, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 26; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 25, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 27; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 26, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 28; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 27, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 29; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 28, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 30; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 29, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 31; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 30, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 32; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 31, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 33; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 32, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 34; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 33, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 35; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 34, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 36; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 35, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 37; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 36, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 38; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 37, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 39; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 38, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 40; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 39, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 41; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 40, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 42; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 41, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 43; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 42, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 44; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 43, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 45; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 44, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 46; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 45, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 47; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 46, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 48; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 47, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 49; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 48, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 50; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 49, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 51; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 50, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 52; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 51, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 53; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 52, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 54; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 53, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 55; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 54, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 56; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 55, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 57; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 56, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 58; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 57, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 59; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 58, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 60; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 59, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 61; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 60, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 62; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 61, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 63; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 62, GumpButtonType.Reply, 1); dby = dby + 45;}
					index = 64; if ( ResearchBarSettings.HasSpell( from, index ) && ResearchBarSettings.GetToolBarSetting( from, index, "SetupBarsResearch4" ) == 1){this.AddButton(dby, 5, Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), Int32.Parse( Server.Misc.Research.SpellInformation( index, 11 ) ), 63, GumpButtonType.Reply, 1); dby = dby + 45;}
				}
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SpellBarsResearch4 ) );

				switch ( info.ButtonID )
				{
					case 0: { break; }
					case 99: { if ( ResearchBarSettings.HasSpell( from, 1 ) ) { new ResearchConjure( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 1: { if ( ResearchBarSettings.HasSpell( from, 2 ) ) { new ResearchDeathSpeak( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 2: { if ( ResearchBarSettings.HasSpell( from, 3 ) ) { new ResearchSneak( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 3: { if ( ResearchBarSettings.HasSpell( from, 4 ) ) { new ResearchCreateFire( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 4: { if ( ResearchBarSettings.HasSpell( from, 5 ) ) { new ResearchSummonElectricalElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 5: { if ( ResearchBarSettings.HasSpell( from, 6 ) ) { new ResearchConfusionBlast( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 6: { if ( ResearchBarSettings.HasSpell( from, 7 ) ) { new ResearchSeeTruth( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 7: { if ( ResearchBarSettings.HasSpell( from, 8 ) ) { new ResearchIcicle( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 8: { if ( ResearchBarSettings.HasSpell( from, 9 ) ) { new ResearchExtinguish( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 9: { if ( ResearchBarSettings.HasSpell( from, 10 ) ) { new ResearchRockFlesh( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 10: { if ( ResearchBarSettings.HasSpell( from, 11 ) ) { new ResearchMassMight( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 11: { if ( ResearchBarSettings.HasSpell( from, 12 ) ) { new ResearchEndureCold( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 12: { if ( ResearchBarSettings.HasSpell( from, 13 ) ) { new ResearchSummonWeedElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 13: { if ( ResearchBarSettings.HasSpell( from, 14 ) ) { new ResearchSummonCreature( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 14: { if ( ResearchBarSettings.HasSpell( from, 15 ) ) { new ResearchHealingTouch( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 15: { if ( ResearchBarSettings.HasSpell( from, 16 ) ) { new ResearchSnowBall( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 16: { if ( ResearchBarSettings.HasSpell( from, 17 ) ) { new ResearchClone( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 17: { if ( ResearchBarSettings.HasSpell( from, 18 ) ) { new ResearchGrantPeace( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 18: { if ( ResearchBarSettings.HasSpell( from, 19 ) ) { new ResearchSleep( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 19: { if ( ResearchBarSettings.HasSpell( from, 20 ) ) { new ResearchEndureHeat( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 20: { if ( ResearchBarSettings.HasSpell( from, 21 ) ) { new ResearchSummonIceElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 21: { if ( ResearchBarSettings.HasSpell( from, 22 ) ) { new ResearchEtherealTravel( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 22: { if ( ResearchBarSettings.HasSpell( from, 23 ) ) { new ResearchWizardEye( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 23: { if ( ResearchBarSettings.HasSpell( from, 24 ) ) { new ResearchFrostField( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 24: { if ( ResearchBarSettings.HasSpell( from, 25 ) ) { new ResearchCreateGold( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 25: { if ( ResearchBarSettings.HasSpell( from, 26 ) ) { new ResearchSummonDead( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 26: { if ( ResearchBarSettings.HasSpell( from, 27 ) ) { new ResearchCauseFear( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 27: { if ( ResearchBarSettings.HasSpell( from, 28 ) ) { new ResearchIgnite( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 28: { if ( ResearchBarSettings.HasSpell( from, 29 ) ) { new ResearchSummonMudElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 29: { if ( ResearchBarSettings.HasSpell( from, 30 ) ) { new ResearchBanishDaemon( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 30: { if ( ResearchBarSettings.HasSpell( from, 31 ) ) { new ResearchFadefromSight( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 31: { if ( ResearchBarSettings.HasSpell( from, 32 ) ) { new ResearchGasCloud( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 32: { if ( ResearchBarSettings.HasSpell( from, 33 ) ) { new ResearchSwarm( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 33: { if ( ResearchBarSettings.HasSpell( from, 34 ) ) { new ResearchMaskofDeath( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 34: { if ( ResearchBarSettings.HasSpell( from, 35 ) ) { new ResearchEnchant( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 35: { if ( ResearchBarSettings.HasSpell( from, 36 ) ) { new ResearchFlameBolt( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 36: { if ( ResearchBarSettings.HasSpell( from, 37 ) ) { new ResearchSummonGemElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
          case 37: { if ( ResearchBarSettings.HasSpell( from, 38 ) ) { new ResearchCallDestruction( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 38: { if ( ResearchBarSettings.HasSpell( from, 39 ) ) { new ResearchDivination( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 39: { if ( ResearchBarSettings.HasSpell( from, 40 ) ) { new ResearchFrostStrike( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 40: { if ( ResearchBarSettings.HasSpell( from, 41 ) ) { new ResearchMagicSteed( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 41: { if ( ResearchBarSettings.HasSpell( from, 42 ) ) { new ResearchCreateGolem( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 42: { if ( ResearchBarSettings.HasSpell( from, 43 ) ) { new ResearchSleepField( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 43: { if ( ResearchBarSettings.HasSpell( from, 44 ) ) { new ResearchConflagration( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 44: { if ( ResearchBarSettings.HasSpell( from, 45 ) ) { new ResearchSummonAcidElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
          case 45: { if ( ResearchBarSettings.HasSpell( from, 46 ) ) { new ResearchMeteorShower( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 46: { if ( ResearchBarSettings.HasSpell( from, 47 ) ) { new ResearchIntervention( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 47: { if ( ResearchBarSettings.HasSpell( from, 48 ) ) { new ResearchHailStorm( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 48: { if ( ResearchBarSettings.HasSpell( from, 49 ) ) { new ResearchAerialServant( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 49: { if ( ResearchBarSettings.HasSpell( from, 50 ) ) { new ResearchOpenGround( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 50: { if ( ResearchBarSettings.HasSpell( from, 51 ) ) { new ResearchCharm( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 51: { if ( ResearchBarSettings.HasSpell( from, 52 ) ) { new ResearchExplosion( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 52: { if ( ResearchBarSettings.HasSpell( from, 53 ) ) { new ResearchSummonPoisonElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
          case 53: { if ( ResearchBarSettings.HasSpell( from, 54 ) ) { new ResearchSummonDevil( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 54: { if ( ResearchBarSettings.HasSpell( from, 55 ) ) { new ResearchAirWalk( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 55: { if ( ResearchBarSettings.HasSpell( from, 56 ) ) { new ResearchAvalanche( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 56: { if ( ResearchBarSettings.HasSpell( from, 57 ) ) { new ResearchDeathVortex( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 57: { if ( ResearchBarSettings.HasSpell( from, 58 ) ) { new ResearchWithstandDeath( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 58: { if ( ResearchBarSettings.HasSpell( from, 59 ) ) { new ResearchMassSleep( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 59: { if ( ResearchBarSettings.HasSpell( from, 60 ) ) { new ResearchRingofFire( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 60: { if ( ResearchBarSettings.HasSpell( from, 61 ) ) { new ResearchSummonBloodElemental( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 61: { if ( ResearchBarSettings.HasSpell( from, 62 ) ) { new ResearchDevastation( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 62: { if ( ResearchBarSettings.HasSpell( from, 63 ) ) { new ResearchRestoration( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
					case 63: { if ( ResearchBarSettings.HasSpell( from, 64 ) ) { new ResearchMassDeath( from, null ).Cast(); from.SendGump( new SpellBarsResearch4( from ) ); } break; }
				}
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
    public class SetupBarsResearch1 : Gump
    {
		public int m_Origin;

		public static void Initialize()
		{
            CommandSystem.Register( "researchspell1", AccessLevel.Player, new CommandEventHandler( ToolBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "researchspell1" )]
		[Description( "Opens Spell Bar Editor For Researchers - 1." )]
		public static void ToolBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SetupBarsResearch1 ) );
				from.SendGump( new SetupBarsResearch1( from, 0 ) );
			}
        }

		public SetupBarsResearch1 ( Mobile from, int origin ) : base ( 25,25 )
		{
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				m_Origin = origin;

				Closable=true;
				Disposable=true;
				Dragable=true;
				Resizable=false;

				AddPage(0);
				AddImage(0, 0, 153);
				AddImage(600, 0, 153);
				AddImage(300, 0, 153);
				AddImage(0, 300, 153);
				AddImage(300, 300, 153);
				AddImage(600, 300, 153);
				AddImage(2, 2, 129);
				AddImage(300, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(300, 298, 129);
				AddImage(598, 298, 129);
				AddImage(7, 7, 150);
				AddImage(679, 5, 134);
				AddImage(178, 22, 156);
				AddImage(165, 19, 156);
				AddImage(155, 36, 162);
				AddImage(376, 44, 132);
				AddImage(210, 44, 132);
				AddImage(185, 41, 159);
				AddImage(343, 560, 140);
				AddImage(564, 560, 140);
				AddImage(853, 562, 143);
				AddImage(16, 248, 137);
				AddImage(6, 378, 148);
				AddImage(40, 331, 156);
				AddImage(33, 356, 156);
				AddImage(32, 342, 156);

				AddItem(20, 215, 3570);
				AddItem(22, 374, 3629);

				AddHtml( 196, 79, 337, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>SPELL BAR - RESEARCH - I</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				ResearchBarSettings.InitializeToolBar( from, "SetupBarsResearch1" );
				ResearchBag bag = (ResearchBag)( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) );
				string MySettings = bag.BarsCast1;

				int button1 = 3609;
				int button2 = 3609;
				int button3 = 3609;
				int button4 = 3609;
				int button5 = 3609;
				int button6 = 3609;
				int button7 = 3609;
				int button8 = 3609;
				int button9 = 3609;
				int button10 = 3609;
				int button11 = 3609;
				int button12 = 3609;
				int button13 = 3609;
				int button14 = 3609;
				int button15 = 3609;
				int button16 = 3609;
				int button17 = 3609;
				int button18 = 3609;
				int button19 = 3609;
				int button20 = 3609;
				int button21 = 3609;
				int button22 = 3609;
				int button23 = 3609;
				int button24 = 3609;
				int button25 = 3609;
				int button26 = 3609;
				int button27 = 3609;
				int button28 = 3609;
				int button29 = 3609;
				int button30 = 3609;
				int button31 = 3609;
				int button32 = 3609;
				int button33 = 3609;
				int button34 = 3609;
				int button35 = 3609;
				int button36 = 3609;
				int button37 = 3609;
				int button38 = 3609;
				int button39 = 3609;
				int button40 = 3609;
				int button41 = 3609;
				int button42 = 3609;
				int button43 = 3609;
				int button44 = 3609;
				int button45 = 3609;
				int button46 = 3609;
				int button47 = 3609;
				int button48 = 3609;
				int button49 = 3609;
				int button50 = 3609;
				int button51 = 3609;
				int button52 = 3609;
				int button53 = 3609;
				int button54 = 3609;
				int button55 = 3609;
				int button56 = 3609;
				int button57 = 3609;
				int button58 = 3609;
				int button59 = 3609;
				int button60 = 3609;
				int button61 = 3609;
				int button62 = 3609;
				int button63 = 3609;
				int button64 = 3609;
				int button65 = 3609;
				int button66 = 3609;
				int button67 = 3609;

				string[] eachSpell = MySettings.Split('#');
				int nLine = 1;
				foreach (string eachSpells in eachSpell)
				{
					if ( nLine == 1 && eachSpells == "1"){ button1 = 4018; }
					if ( nLine == 2 && eachSpells == "1"){ button2 = 4018; }
					if ( nLine == 3 && eachSpells == "1"){ button3 = 4018; }
					if ( nLine == 4 && eachSpells == "1"){ button4 = 4018; }
					if ( nLine == 5 && eachSpells == "1"){ button5 = 4018; }
					if ( nLine == 6 && eachSpells == "1"){ button6 = 4018; }
					if ( nLine == 7 && eachSpells == "1"){ button7 = 4018; }
					if ( nLine == 8 && eachSpells == "1"){ button8 = 4018; }
					if ( nLine == 9 && eachSpells == "1"){ button9 = 4018; }
					if ( nLine == 10 && eachSpells == "1"){ button10 = 4018; }
					if ( nLine == 11 && eachSpells == "1"){ button11 = 4018; }
					if ( nLine == 12 && eachSpells == "1"){ button12 = 4018; }
					if ( nLine == 13 && eachSpells == "1"){ button13 = 4018; }
					if ( nLine == 14 && eachSpells == "1"){ button14 = 4018; }
					if ( nLine == 15 && eachSpells == "1"){ button15 = 4018; }
					if ( nLine == 16 && eachSpells == "1"){ button16 = 4018; }
					if ( nLine == 17 && eachSpells == "1"){ button17 = 4018; }
					if ( nLine == 18 && eachSpells == "1"){ button18 = 4018; }
					if ( nLine == 19 && eachSpells == "1"){ button19 = 4018; }
					if ( nLine == 20 && eachSpells == "1"){ button20 = 4018; }
					if ( nLine == 21 && eachSpells == "1"){ button21 = 4018; }
					if ( nLine == 22 && eachSpells == "1"){ button22 = 4018; }
					if ( nLine == 23 && eachSpells == "1"){ button23 = 4018; }
					if ( nLine == 24 && eachSpells == "1"){ button24 = 4018; }
					if ( nLine == 25 && eachSpells == "1"){ button25 = 4018; }
					if ( nLine == 26 && eachSpells == "1"){ button26 = 4018; }
					if ( nLine == 27 && eachSpells == "1"){ button27 = 4018; }
					if ( nLine == 28 && eachSpells == "1"){ button28 = 4018; }
					if ( nLine == 29 && eachSpells == "1"){ button29 = 4018; }
					if ( nLine == 30 && eachSpells == "1"){ button30 = 4018; }
					if ( nLine == 31 && eachSpells == "1"){ button31 = 4018; }
					if ( nLine == 32 && eachSpells == "1"){ button32 = 4018; }
					if ( nLine == 33 && eachSpells == "1"){ button33 = 4018; }
					if ( nLine == 34 && eachSpells == "1"){ button34 = 4018; }
					if ( nLine == 35 && eachSpells == "1"){ button35 = 4018; }
					if ( nLine == 36 && eachSpells == "1"){ button36 = 4018; }
					if ( nLine == 37 && eachSpells == "1"){ button37 = 4018; }
					if ( nLine == 38 && eachSpells == "1"){ button38 = 4018; }
					if ( nLine == 39 && eachSpells == "1"){ button39 = 4018; }
					if ( nLine == 40 && eachSpells == "1"){ button40 = 4018; }
					if ( nLine == 41 && eachSpells == "1"){ button41 = 4018; }
					if ( nLine == 42 && eachSpells == "1"){ button42 = 4018; }
					if ( nLine == 43 && eachSpells == "1"){ button43 = 4018; }
					if ( nLine == 44 && eachSpells == "1"){ button44 = 4018; }
					if ( nLine == 45 && eachSpells == "1"){ button45 = 4018; }
					if ( nLine == 46 && eachSpells == "1"){ button46 = 4018; }
					if ( nLine == 47 && eachSpells == "1"){ button47 = 4018; }
					if ( nLine == 48 && eachSpells == "1"){ button48 = 4018; }
					if ( nLine == 49 && eachSpells == "1"){ button49 = 4018; }
					if ( nLine == 50 && eachSpells == "1"){ button50 = 4018; }
					if ( nLine == 51 && eachSpells == "1"){ button51 = 4018; }
					if ( nLine == 52 && eachSpells == "1"){ button52 = 4018; }
					if ( nLine == 53 && eachSpells == "1"){ button53 = 4018; }
					if ( nLine == 54 && eachSpells == "1"){ button54 = 4018; }
					if ( nLine == 55 && eachSpells == "1"){ button55 = 4018; }
					if ( nLine == 56 && eachSpells == "1"){ button56 = 4018; }
					if ( nLine == 57 && eachSpells == "1"){ button57 = 4018; }
					if ( nLine == 58 && eachSpells == "1"){ button58 = 4018; }
					if ( nLine == 59 && eachSpells == "1"){ button59 = 4018; }
					if ( nLine == 60 && eachSpells == "1"){ button60 = 4018; }
					if ( nLine == 61 && eachSpells == "1"){ button61 = 4018; }
					if ( nLine == 62 && eachSpells == "1"){ button62 = 4018; }
					if ( nLine == 63 && eachSpells == "1"){ button63 = 4018; }
					if ( nLine == 64 && eachSpells == "1"){ button64 = 4018; }

					if ( nLine == 65 && eachSpells == "1" ) { button65 = 4018; }

					if ( nLine == 66 && eachSpells == "0" ) { button66 = 3609; }
					else if ( nLine == 66 && eachSpells == "1" ) { button66 = 4018; }

					if ( nLine == 66 && eachSpells == "1" ) { button67 = 3609; }
					else if ( nLine == 66 && eachSpells == "0" ) { button67 = 4018; }

					nLine++;
				}

				AddButton(582, 82, button65, button65, 90, GumpButtonType.Reply, 0);
				AddHtml( 624, 81, 261, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Show Spell Names When Vertical</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddButton(377, 540, button66, button66, 91, GumpButtonType.Reply, 0);
				AddHtml( 417, 539, 125, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Vertical Bar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddButton(681, 537, button67, button67, 91, GumpButtonType.Reply, 0);
				AddHtml( 721, 536, 125, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Horizontal Bar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				// ------------------------------------------------------------------------------------ 1

				int x1 = 135;
				int x2 = 95;
				int y1 = 120;
				int y2 = 130;
				int rp = 0;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button1, button1, 99, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button2, button2, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button3, button3, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button4, button4, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button5, button5, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button6, button6, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button7, button7, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button8, button8, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 2

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button9, button9, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button10, button10, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button11, button11, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button12, button12, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button13, button13, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button14, button14, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button15, button15, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button16, button16, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 3

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button17, button17, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button18, button18, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button19, button19, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button20, button20, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button21, button21, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button22, button22, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button23, button23, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button24, button24, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 4

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button25, button25, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button26, button26, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button27, button27, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button28, button28, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button29, button29, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button30, button30, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button31, button31, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button32, button32, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 5

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button33, button33, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button34, button34, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button35, button35, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button36, button36, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button37, button37, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button38, button38, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button39, button39, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button40, button40, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 6

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button41, button41, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button42, button42, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button43, button43, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button44, button44, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button45, button45, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button46, button46, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button47, button47, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button48, button48, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 7

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button49, button49, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button50, button50, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button51, button51, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button52, button52, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button53, button53, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button54, button54, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button55, button55, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button56, button56, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 8

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button57, button57, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button58, button58, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button59, button59, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button60, button60, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button61, button61, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button62, button62, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button63, button63, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button64, button64, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				if ( info.ButtonID == 99 ){ ResearchBarSettings.UpdateToolBar( from, 1, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 1 ){ ResearchBarSettings.UpdateToolBar( from, 2, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 2 ){ ResearchBarSettings.UpdateToolBar( from, 3, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 3 ){ ResearchBarSettings.UpdateToolBar( from, 4, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 4 ){ ResearchBarSettings.UpdateToolBar( from, 5, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 5 ){ ResearchBarSettings.UpdateToolBar( from, 6, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 6 ){ ResearchBarSettings.UpdateToolBar( from, 7, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 7 ){ ResearchBarSettings.UpdateToolBar( from, 8, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 8 ){ ResearchBarSettings.UpdateToolBar( from, 9, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 9 ){ ResearchBarSettings.UpdateToolBar( from, 10, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 10 ){ ResearchBarSettings.UpdateToolBar( from, 11, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 11 ){ ResearchBarSettings.UpdateToolBar( from, 12, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 12 ){ ResearchBarSettings.UpdateToolBar( from, 13, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 13 ){ ResearchBarSettings.UpdateToolBar( from, 14, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 14 ){ ResearchBarSettings.UpdateToolBar( from, 15, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 15 ){ ResearchBarSettings.UpdateToolBar( from, 16, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 16 ){ ResearchBarSettings.UpdateToolBar( from, 17, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 17 ){ ResearchBarSettings.UpdateToolBar( from, 18, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 18 ){ ResearchBarSettings.UpdateToolBar( from, 19, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 19 ){ ResearchBarSettings.UpdateToolBar( from, 20, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 20 ){ ResearchBarSettings.UpdateToolBar( from, 21, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 21 ){ ResearchBarSettings.UpdateToolBar( from, 22, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 22 ){ ResearchBarSettings.UpdateToolBar( from, 23, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 23 ){ ResearchBarSettings.UpdateToolBar( from, 24, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 24 ){ ResearchBarSettings.UpdateToolBar( from, 25, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 25 ){ ResearchBarSettings.UpdateToolBar( from, 26, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 26 ){ ResearchBarSettings.UpdateToolBar( from, 27, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 27 ){ ResearchBarSettings.UpdateToolBar( from, 28, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 28 ){ ResearchBarSettings.UpdateToolBar( from, 29, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 29 ){ ResearchBarSettings.UpdateToolBar( from, 30, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 30 ){ ResearchBarSettings.UpdateToolBar( from, 31, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 31 ){ ResearchBarSettings.UpdateToolBar( from, 32, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 32 ){ ResearchBarSettings.UpdateToolBar( from, 33, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 33 ){ ResearchBarSettings.UpdateToolBar( from, 34, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 34 ){ ResearchBarSettings.UpdateToolBar( from, 35, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 35 ){ ResearchBarSettings.UpdateToolBar( from, 36, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 36 ){ ResearchBarSettings.UpdateToolBar( from, 37, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 37 ){ ResearchBarSettings.UpdateToolBar( from, 38, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 38 ){ ResearchBarSettings.UpdateToolBar( from, 39, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 39 ){ ResearchBarSettings.UpdateToolBar( from, 40, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 40 ){ ResearchBarSettings.UpdateToolBar( from, 41, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 41 ){ ResearchBarSettings.UpdateToolBar( from, 42, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 42 ){ ResearchBarSettings.UpdateToolBar( from, 43, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 43 ){ ResearchBarSettings.UpdateToolBar( from, 44, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 44 ){ ResearchBarSettings.UpdateToolBar( from, 45, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 45 ){ ResearchBarSettings.UpdateToolBar( from, 46, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 46 ){ ResearchBarSettings.UpdateToolBar( from, 47, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 47 ){ ResearchBarSettings.UpdateToolBar( from, 48, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 48 ){ ResearchBarSettings.UpdateToolBar( from, 49, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 49 ){ ResearchBarSettings.UpdateToolBar( from, 50, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 50 ){ ResearchBarSettings.UpdateToolBar( from, 51, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 51 ){ ResearchBarSettings.UpdateToolBar( from, 52, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 52 ){ ResearchBarSettings.UpdateToolBar( from, 53, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 53 ){ ResearchBarSettings.UpdateToolBar( from, 54, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 54 ){ ResearchBarSettings.UpdateToolBar( from, 55, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 55 ){ ResearchBarSettings.UpdateToolBar( from, 56, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 56 ){ ResearchBarSettings.UpdateToolBar( from, 57, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 57 ){ ResearchBarSettings.UpdateToolBar( from, 58, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 58 ){ ResearchBarSettings.UpdateToolBar( from, 59, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 59 ){ ResearchBarSettings.UpdateToolBar( from, 60, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 60 ){ ResearchBarSettings.UpdateToolBar( from, 61, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 61 ){ ResearchBarSettings.UpdateToolBar( from, 62, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 62 ){ ResearchBarSettings.UpdateToolBar( from, 63, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 63 ){ ResearchBarSettings.UpdateToolBar( from, 64, "SetupBarsResearch1", 66 ); }

				else if ( info.ButtonID == 90 ){ ResearchBarSettings.UpdateToolBar( from, 65, "SetupBarsResearch1", 66 ); }
				else if ( info.ButtonID == 91 ){ ResearchBarSettings.UpdateToolBar( from, 66, "SetupBarsResearch1", 66 ); }

				if ( info.ButtonID < 1 && m_Origin > 0 )
				{
					ResearchBag bag = (ResearchBag)( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) );
					from.SendGump( new Server.Items.ResearchBag.ResearchGump( bag ) );
					from.SendSound( 0x4A );
				}
				else if ( info.ButtonID < 1 ){}
				else { from.SendGump( new SetupBarsResearch1( from, m_Origin ) ); from.SendSound( 0x4A ); }
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
    public class SetupBarsResearch2 : Gump
    {
		public int m_Origin;

		public static void Initialize()
		{
            CommandSystem.Register( "researchspell2", AccessLevel.Player, new CommandEventHandler( ToolBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "researchspell2" )]
		[Description( "Opens Spell Bar Editor For Researchers - 2." )]
		public static void ToolBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SetupBarsResearch2 ) );
				from.SendGump( new SetupBarsResearch2( from, 0 ) );
			}
        }

 		public SetupBarsResearch2 ( Mobile from, int origin ) : base ( 25,25 )
		{
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				m_Origin = origin;

				Closable=true;
				Disposable=true;
				Dragable=true;
				Resizable=false;

				AddPage(0);
				AddImage(0, 0, 153);
				AddImage(600, 0, 153);
				AddImage(300, 0, 153);
				AddImage(0, 300, 153);
				AddImage(300, 300, 153);
				AddImage(600, 300, 153);
				AddImage(2, 2, 129);
				AddImage(300, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(300, 298, 129);
				AddImage(598, 298, 129);
				AddImage(7, 7, 150);
				AddImage(679, 5, 134);
				AddImage(178, 22, 156);
				AddImage(165, 19, 156);
				AddImage(155, 36, 162);
				AddImage(376, 44, 132);
				AddImage(210, 44, 132);
				AddImage(185, 41, 159);
				AddImage(343, 560, 140);
				AddImage(564, 560, 140);
				AddImage(853, 562, 143);
				AddImage(16, 248, 137);
				AddImage(6, 378, 148);
				AddImage(40, 331, 156);
				AddImage(33, 356, 156);
				AddImage(32, 342, 156);

				AddItem(20, 215, 3570);
				AddItem(22, 374, 3629);

				AddHtml( 196, 79, 337, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>SPELL BAR - RESEARCH - II</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				ResearchBarSettings.InitializeToolBar( from, "SetupBarsResearch2" );
				ResearchBag bag = (ResearchBag)( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) );
				string MySettings = bag.BarsCast2;

				int button1 = 3609;
				int button2 = 3609;
				int button3 = 3609;
				int button4 = 3609;
				int button5 = 3609;
				int button6 = 3609;
				int button7 = 3609;
				int button8 = 3609;
				int button9 = 3609;
				int button10 = 3609;
				int button11 = 3609;
				int button12 = 3609;
				int button13 = 3609;
				int button14 = 3609;
				int button15 = 3609;
				int button16 = 3609;
				int button17 = 3609;
				int button18 = 3609;
				int button19 = 3609;
				int button20 = 3609;
				int button21 = 3609;
				int button22 = 3609;
				int button23 = 3609;
				int button24 = 3609;
				int button25 = 3609;
				int button26 = 3609;
				int button27 = 3609;
				int button28 = 3609;
				int button29 = 3609;
				int button30 = 3609;
				int button31 = 3609;
				int button32 = 3609;
				int button33 = 3609;
				int button34 = 3609;
				int button35 = 3609;
				int button36 = 3609;
				int button37 = 3609;
				int button38 = 3609;
				int button39 = 3609;
				int button40 = 3609;
				int button41 = 3609;
				int button42 = 3609;
				int button43 = 3609;
				int button44 = 3609;
				int button45 = 3609;
				int button46 = 3609;
				int button47 = 3609;
				int button48 = 3609;
				int button49 = 3609;
				int button50 = 3609;
				int button51 = 3609;
				int button52 = 3609;
				int button53 = 3609;
				int button54 = 3609;
				int button55 = 3609;
				int button56 = 3609;
				int button57 = 3609;
				int button58 = 3609;
				int button59 = 3609;
				int button60 = 3609;
				int button61 = 3609;
				int button62 = 3609;
				int button63 = 3609;
				int button64 = 3609;
				int button65 = 3609;
				int button66 = 3609;
				int button67 = 3609;

				string[] eachSpell = MySettings.Split('#');
				int nLine = 1;
				foreach (string eachSpells in eachSpell)
				{
					if ( nLine == 1 && eachSpells == "1"){ button1 = 4018; }
					if ( nLine == 2 && eachSpells == "1"){ button2 = 4018; }
					if ( nLine == 3 && eachSpells == "1"){ button3 = 4018; }
					if ( nLine == 4 && eachSpells == "1"){ button4 = 4018; }
					if ( nLine == 5 && eachSpells == "1"){ button5 = 4018; }
					if ( nLine == 6 && eachSpells == "1"){ button6 = 4018; }
					if ( nLine == 7 && eachSpells == "1"){ button7 = 4018; }
					if ( nLine == 8 && eachSpells == "1"){ button8 = 4018; }
					if ( nLine == 9 && eachSpells == "1"){ button9 = 4018; }
					if ( nLine == 10 && eachSpells == "1"){ button10 = 4018; }
					if ( nLine == 11 && eachSpells == "1"){ button11 = 4018; }
					if ( nLine == 12 && eachSpells == "1"){ button12 = 4018; }
					if ( nLine == 13 && eachSpells == "1"){ button13 = 4018; }
					if ( nLine == 14 && eachSpells == "1"){ button14 = 4018; }
					if ( nLine == 15 && eachSpells == "1"){ button15 = 4018; }
					if ( nLine == 16 && eachSpells == "1"){ button16 = 4018; }
					if ( nLine == 17 && eachSpells == "1"){ button17 = 4018; }
					if ( nLine == 18 && eachSpells == "1"){ button18 = 4018; }
					if ( nLine == 19 && eachSpells == "1"){ button19 = 4018; }
					if ( nLine == 20 && eachSpells == "1"){ button20 = 4018; }
					if ( nLine == 21 && eachSpells == "1"){ button21 = 4018; }
					if ( nLine == 22 && eachSpells == "1"){ button22 = 4018; }
					if ( nLine == 23 && eachSpells == "1"){ button23 = 4018; }
					if ( nLine == 24 && eachSpells == "1"){ button24 = 4018; }
					if ( nLine == 25 && eachSpells == "1"){ button25 = 4018; }
					if ( nLine == 26 && eachSpells == "1"){ button26 = 4018; }
					if ( nLine == 27 && eachSpells == "1"){ button27 = 4018; }
					if ( nLine == 28 && eachSpells == "1"){ button28 = 4018; }
					if ( nLine == 29 && eachSpells == "1"){ button29 = 4018; }
					if ( nLine == 30 && eachSpells == "1"){ button30 = 4018; }
					if ( nLine == 31 && eachSpells == "1"){ button31 = 4018; }
					if ( nLine == 32 && eachSpells == "1"){ button32 = 4018; }
					if ( nLine == 33 && eachSpells == "1"){ button33 = 4018; }
					if ( nLine == 34 && eachSpells == "1"){ button34 = 4018; }
					if ( nLine == 35 && eachSpells == "1"){ button35 = 4018; }
					if ( nLine == 36 && eachSpells == "1"){ button36 = 4018; }
					if ( nLine == 37 && eachSpells == "1"){ button37 = 4018; }
					if ( nLine == 38 && eachSpells == "1"){ button38 = 4018; }
					if ( nLine == 39 && eachSpells == "1"){ button39 = 4018; }
					if ( nLine == 40 && eachSpells == "1"){ button40 = 4018; }
					if ( nLine == 41 && eachSpells == "1"){ button41 = 4018; }
					if ( nLine == 42 && eachSpells == "1"){ button42 = 4018; }
					if ( nLine == 43 && eachSpells == "1"){ button43 = 4018; }
					if ( nLine == 44 && eachSpells == "1"){ button44 = 4018; }
					if ( nLine == 45 && eachSpells == "1"){ button45 = 4018; }
					if ( nLine == 46 && eachSpells == "1"){ button46 = 4018; }
					if ( nLine == 47 && eachSpells == "1"){ button47 = 4018; }
					if ( nLine == 48 && eachSpells == "1"){ button48 = 4018; }
					if ( nLine == 49 && eachSpells == "1"){ button49 = 4018; }
					if ( nLine == 50 && eachSpells == "1"){ button50 = 4018; }
					if ( nLine == 51 && eachSpells == "1"){ button51 = 4018; }
					if ( nLine == 52 && eachSpells == "1"){ button52 = 4018; }
					if ( nLine == 53 && eachSpells == "1"){ button53 = 4018; }
					if ( nLine == 54 && eachSpells == "1"){ button54 = 4018; }
					if ( nLine == 55 && eachSpells == "1"){ button55 = 4018; }
					if ( nLine == 56 && eachSpells == "1"){ button56 = 4018; }
					if ( nLine == 57 && eachSpells == "1"){ button57 = 4018; }
					if ( nLine == 58 && eachSpells == "1"){ button58 = 4018; }
					if ( nLine == 59 && eachSpells == "1"){ button59 = 4018; }
					if ( nLine == 60 && eachSpells == "1"){ button60 = 4018; }
					if ( nLine == 61 && eachSpells == "1"){ button61 = 4018; }
					if ( nLine == 62 && eachSpells == "1"){ button62 = 4018; }
					if ( nLine == 63 && eachSpells == "1"){ button63 = 4018; }
					if ( nLine == 64 && eachSpells == "1"){ button64 = 4018; }

					if ( nLine == 65 && eachSpells == "1" ) { button65 = 4018; }

					if ( nLine == 66 && eachSpells == "0" ) { button66 = 3609; }
					else if ( nLine == 66 && eachSpells == "1" ) { button66 = 4018; }

					if ( nLine == 66 && eachSpells == "1" ) { button67 = 3609; }
					else if ( nLine == 66 && eachSpells == "0" ) { button67 = 4018; }

					nLine++;
				}

				AddButton(582, 82, button65, button65, 90, GumpButtonType.Reply, 0);
				AddHtml( 624, 81, 261, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Show Spell Names When Vertical</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddButton(377, 540, button66, button66, 91, GumpButtonType.Reply, 0);
				AddHtml( 417, 539, 125, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Vertical Bar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddButton(681, 537, button67, button67, 91, GumpButtonType.Reply, 0);
				AddHtml( 721, 536, 125, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Horizontal Bar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				// ------------------------------------------------------------------------------------ 1

				int x1 = 135;
				int x2 = 95;
				int y1 = 120;
				int y2 = 130;
				int rp = 0;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button1, button1, 99, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button2, button2, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button3, button3, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button4, button4, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button5, button5, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button6, button6, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button7, button7, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button8, button8, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 2

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button9, button9, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button10, button10, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button11, button11, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button12, button12, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button13, button13, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button14, button14, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button15, button15, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button16, button16, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 3

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button17, button17, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button18, button18, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button19, button19, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button20, button20, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button21, button21, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button22, button22, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button23, button23, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button24, button24, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 4

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button25, button25, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button26, button26, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button27, button27, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button28, button28, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button29, button29, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button30, button30, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button31, button31, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button32, button32, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 5

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button33, button33, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button34, button34, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button35, button35, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button36, button36, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button37, button37, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button38, button38, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button39, button39, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button40, button40, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 6

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button41, button41, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button42, button42, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button43, button43, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button44, button44, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button45, button45, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button46, button46, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button47, button47, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button48, button48, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 7

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button49, button49, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button50, button50, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button51, button51, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button52, button52, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button53, button53, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button54, button54, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button55, button55, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button56, button56, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 8

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button57, button57, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button58, button58, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button59, button59, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button60, button60, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button61, button61, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button62, button62, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button63, button63, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button64, button64, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				if ( info.ButtonID == 99 ){ ResearchBarSettings.UpdateToolBar( from, 1, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 1 ){ ResearchBarSettings.UpdateToolBar( from, 2, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 2 ){ ResearchBarSettings.UpdateToolBar( from, 3, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 3 ){ ResearchBarSettings.UpdateToolBar( from, 4, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 4 ){ ResearchBarSettings.UpdateToolBar( from, 5, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 5 ){ ResearchBarSettings.UpdateToolBar( from, 6, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 6 ){ ResearchBarSettings.UpdateToolBar( from, 7, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 7 ){ ResearchBarSettings.UpdateToolBar( from, 8, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 8 ){ ResearchBarSettings.UpdateToolBar( from, 9, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 9 ){ ResearchBarSettings.UpdateToolBar( from, 10, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 10 ){ ResearchBarSettings.UpdateToolBar( from, 11, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 11 ){ ResearchBarSettings.UpdateToolBar( from, 12, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 12 ){ ResearchBarSettings.UpdateToolBar( from, 13, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 13 ){ ResearchBarSettings.UpdateToolBar( from, 14, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 14 ){ ResearchBarSettings.UpdateToolBar( from, 15, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 15 ){ ResearchBarSettings.UpdateToolBar( from, 16, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 16 ){ ResearchBarSettings.UpdateToolBar( from, 17, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 17 ){ ResearchBarSettings.UpdateToolBar( from, 18, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 18 ){ ResearchBarSettings.UpdateToolBar( from, 19, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 19 ){ ResearchBarSettings.UpdateToolBar( from, 20, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 20 ){ ResearchBarSettings.UpdateToolBar( from, 21, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 21 ){ ResearchBarSettings.UpdateToolBar( from, 22, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 22 ){ ResearchBarSettings.UpdateToolBar( from, 23, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 23 ){ ResearchBarSettings.UpdateToolBar( from, 24, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 24 ){ ResearchBarSettings.UpdateToolBar( from, 25, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 25 ){ ResearchBarSettings.UpdateToolBar( from, 26, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 26 ){ ResearchBarSettings.UpdateToolBar( from, 27, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 27 ){ ResearchBarSettings.UpdateToolBar( from, 28, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 28 ){ ResearchBarSettings.UpdateToolBar( from, 29, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 29 ){ ResearchBarSettings.UpdateToolBar( from, 30, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 30 ){ ResearchBarSettings.UpdateToolBar( from, 31, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 31 ){ ResearchBarSettings.UpdateToolBar( from, 32, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 32 ){ ResearchBarSettings.UpdateToolBar( from, 33, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 33 ){ ResearchBarSettings.UpdateToolBar( from, 34, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 34 ){ ResearchBarSettings.UpdateToolBar( from, 35, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 35 ){ ResearchBarSettings.UpdateToolBar( from, 36, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 36 ){ ResearchBarSettings.UpdateToolBar( from, 37, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 37 ){ ResearchBarSettings.UpdateToolBar( from, 38, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 38 ){ ResearchBarSettings.UpdateToolBar( from, 39, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 39 ){ ResearchBarSettings.UpdateToolBar( from, 40, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 40 ){ ResearchBarSettings.UpdateToolBar( from, 41, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 41 ){ ResearchBarSettings.UpdateToolBar( from, 42, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 42 ){ ResearchBarSettings.UpdateToolBar( from, 43, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 43 ){ ResearchBarSettings.UpdateToolBar( from, 44, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 44 ){ ResearchBarSettings.UpdateToolBar( from, 45, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 45 ){ ResearchBarSettings.UpdateToolBar( from, 46, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 46 ){ ResearchBarSettings.UpdateToolBar( from, 47, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 47 ){ ResearchBarSettings.UpdateToolBar( from, 48, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 48 ){ ResearchBarSettings.UpdateToolBar( from, 49, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 49 ){ ResearchBarSettings.UpdateToolBar( from, 50, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 50 ){ ResearchBarSettings.UpdateToolBar( from, 51, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 51 ){ ResearchBarSettings.UpdateToolBar( from, 52, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 52 ){ ResearchBarSettings.UpdateToolBar( from, 53, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 53 ){ ResearchBarSettings.UpdateToolBar( from, 54, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 54 ){ ResearchBarSettings.UpdateToolBar( from, 55, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 55 ){ ResearchBarSettings.UpdateToolBar( from, 56, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 56 ){ ResearchBarSettings.UpdateToolBar( from, 57, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 57 ){ ResearchBarSettings.UpdateToolBar( from, 58, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 58 ){ ResearchBarSettings.UpdateToolBar( from, 59, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 59 ){ ResearchBarSettings.UpdateToolBar( from, 60, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 60 ){ ResearchBarSettings.UpdateToolBar( from, 61, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 61 ){ ResearchBarSettings.UpdateToolBar( from, 62, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 62 ){ ResearchBarSettings.UpdateToolBar( from, 63, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 63 ){ ResearchBarSettings.UpdateToolBar( from, 64, "SetupBarsResearch2", 66 ); }

				else if ( info.ButtonID == 90 ){ ResearchBarSettings.UpdateToolBar( from, 65, "SetupBarsResearch2", 66 ); }
				else if ( info.ButtonID == 91 ){ ResearchBarSettings.UpdateToolBar( from, 66, "SetupBarsResearch2", 66 ); }

				if ( info.ButtonID < 1 && m_Origin > 0 )
				{
					ResearchBag bag = (ResearchBag)( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) );
					from.SendGump( new Server.Items.ResearchBag.ResearchGump( bag ) );
					from.SendSound( 0x4A );
				}
				else if ( info.ButtonID < 1 ){}
				else { from.SendGump( new SetupBarsResearch2( from, m_Origin ) ); from.SendSound( 0x4A ); }
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
    public class SetupBarsResearch3 : Gump
    {
		public int m_Origin;

		public static void Initialize()
		{
            CommandSystem.Register( "researchspell3", AccessLevel.Player, new CommandEventHandler( ToolBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "researchspell3" )]
		[Description( "Opens Spell Bar Editor For Researchers - 3." )]
		public static void ToolBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SetupBarsResearch3 ) );
				from.SendGump( new SetupBarsResearch3( from, 0 ) );
			}
        }

 		public SetupBarsResearch3 ( Mobile from, int origin ) : base ( 25,25 )
		{
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				m_Origin = origin;

				Closable=true;
				Disposable=true;
				Dragable=true;
				Resizable=false;

				AddPage(0);
				AddImage(0, 0, 153);
				AddImage(600, 0, 153);
				AddImage(300, 0, 153);
				AddImage(0, 300, 153);
				AddImage(300, 300, 153);
				AddImage(600, 300, 153);
				AddImage(2, 2, 129);
				AddImage(300, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(300, 298, 129);
				AddImage(598, 298, 129);
				AddImage(7, 7, 150);
				AddImage(679, 5, 134);
				AddImage(178, 22, 156);
				AddImage(165, 19, 156);
				AddImage(155, 36, 162);
				AddImage(376, 44, 132);
				AddImage(210, 44, 132);
				AddImage(185, 41, 159);
				AddImage(343, 560, 140);
				AddImage(564, 560, 140);
				AddImage(853, 562, 143);
				AddImage(16, 248, 137);
				AddImage(6, 378, 148);
				AddImage(40, 331, 156);
				AddImage(33, 356, 156);
				AddImage(32, 342, 156);

				AddItem(20, 215, 3570);
				AddItem(22, 374, 3629);

				AddHtml( 196, 79, 337, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>SPELL BAR - RESEARCH - III</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				ResearchBarSettings.InitializeToolBar( from, "SetupBarsResearch3" );
				ResearchBag bag = (ResearchBag)( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) );
				string MySettings = bag.BarsCast3;

				int button1 = 3609;
				int button2 = 3609;
				int button3 = 3609;
				int button4 = 3609;
				int button5 = 3609;
				int button6 = 3609;
				int button7 = 3609;
				int button8 = 3609;
				int button9 = 3609;
				int button10 = 3609;
				int button11 = 3609;
				int button12 = 3609;
				int button13 = 3609;
				int button14 = 3609;
				int button15 = 3609;
				int button16 = 3609;
				int button17 = 3609;
				int button18 = 3609;
				int button19 = 3609;
				int button20 = 3609;
				int button21 = 3609;
				int button22 = 3609;
				int button23 = 3609;
				int button24 = 3609;
				int button25 = 3609;
				int button26 = 3609;
				int button27 = 3609;
				int button28 = 3609;
				int button29 = 3609;
				int button30 = 3609;
				int button31 = 3609;
				int button32 = 3609;
				int button33 = 3609;
				int button34 = 3609;
				int button35 = 3609;
				int button36 = 3609;
				int button37 = 3609;
				int button38 = 3609;
				int button39 = 3609;
				int button40 = 3609;
				int button41 = 3609;
				int button42 = 3609;
				int button43 = 3609;
				int button44 = 3609;
				int button45 = 3609;
				int button46 = 3609;
				int button47 = 3609;
				int button48 = 3609;
				int button49 = 3609;
				int button50 = 3609;
				int button51 = 3609;
				int button52 = 3609;
				int button53 = 3609;
				int button54 = 3609;
				int button55 = 3609;
				int button56 = 3609;
				int button57 = 3609;
				int button58 = 3609;
				int button59 = 3609;
				int button60 = 3609;
				int button61 = 3609;
				int button62 = 3609;
				int button63 = 3609;
				int button64 = 3609;
				int button65 = 3609;
				int button66 = 3609;
				int button67 = 3609;

				string[] eachSpell = MySettings.Split('#');
				int nLine = 1;
				foreach (string eachSpells in eachSpell)
				{
					if ( nLine == 1 && eachSpells == "1"){ button1 = 4018; }
					if ( nLine == 2 && eachSpells == "1"){ button2 = 4018; }
					if ( nLine == 3 && eachSpells == "1"){ button3 = 4018; }
					if ( nLine == 4 && eachSpells == "1"){ button4 = 4018; }
					if ( nLine == 5 && eachSpells == "1"){ button5 = 4018; }
					if ( nLine == 6 && eachSpells == "1"){ button6 = 4018; }
					if ( nLine == 7 && eachSpells == "1"){ button7 = 4018; }
					if ( nLine == 8 && eachSpells == "1"){ button8 = 4018; }
					if ( nLine == 9 && eachSpells == "1"){ button9 = 4018; }
					if ( nLine == 10 && eachSpells == "1"){ button10 = 4018; }
					if ( nLine == 11 && eachSpells == "1"){ button11 = 4018; }
					if ( nLine == 12 && eachSpells == "1"){ button12 = 4018; }
					if ( nLine == 13 && eachSpells == "1"){ button13 = 4018; }
					if ( nLine == 14 && eachSpells == "1"){ button14 = 4018; }
					if ( nLine == 15 && eachSpells == "1"){ button15 = 4018; }
					if ( nLine == 16 && eachSpells == "1"){ button16 = 4018; }
					if ( nLine == 17 && eachSpells == "1"){ button17 = 4018; }
					if ( nLine == 18 && eachSpells == "1"){ button18 = 4018; }
					if ( nLine == 19 && eachSpells == "1"){ button19 = 4018; }
					if ( nLine == 20 && eachSpells == "1"){ button20 = 4018; }
					if ( nLine == 21 && eachSpells == "1"){ button21 = 4018; }
					if ( nLine == 22 && eachSpells == "1"){ button22 = 4018; }
					if ( nLine == 23 && eachSpells == "1"){ button23 = 4018; }
					if ( nLine == 24 && eachSpells == "1"){ button24 = 4018; }
					if ( nLine == 25 && eachSpells == "1"){ button25 = 4018; }
					if ( nLine == 26 && eachSpells == "1"){ button26 = 4018; }
					if ( nLine == 27 && eachSpells == "1"){ button27 = 4018; }
					if ( nLine == 28 && eachSpells == "1"){ button28 = 4018; }
					if ( nLine == 29 && eachSpells == "1"){ button29 = 4018; }
					if ( nLine == 30 && eachSpells == "1"){ button30 = 4018; }
					if ( nLine == 31 && eachSpells == "1"){ button31 = 4018; }
					if ( nLine == 32 && eachSpells == "1"){ button32 = 4018; }
					if ( nLine == 33 && eachSpells == "1"){ button33 = 4018; }
					if ( nLine == 34 && eachSpells == "1"){ button34 = 4018; }
					if ( nLine == 35 && eachSpells == "1"){ button35 = 4018; }
					if ( nLine == 36 && eachSpells == "1"){ button36 = 4018; }
					if ( nLine == 37 && eachSpells == "1"){ button37 = 4018; }
					if ( nLine == 38 && eachSpells == "1"){ button38 = 4018; }
					if ( nLine == 39 && eachSpells == "1"){ button39 = 4018; }
					if ( nLine == 40 && eachSpells == "1"){ button40 = 4018; }
					if ( nLine == 41 && eachSpells == "1"){ button41 = 4018; }
					if ( nLine == 42 && eachSpells == "1"){ button42 = 4018; }
					if ( nLine == 43 && eachSpells == "1"){ button43 = 4018; }
					if ( nLine == 44 && eachSpells == "1"){ button44 = 4018; }
					if ( nLine == 45 && eachSpells == "1"){ button45 = 4018; }
					if ( nLine == 46 && eachSpells == "1"){ button46 = 4018; }
					if ( nLine == 47 && eachSpells == "1"){ button47 = 4018; }
					if ( nLine == 48 && eachSpells == "1"){ button48 = 4018; }
					if ( nLine == 49 && eachSpells == "1"){ button49 = 4018; }
					if ( nLine == 50 && eachSpells == "1"){ button50 = 4018; }
					if ( nLine == 51 && eachSpells == "1"){ button51 = 4018; }
					if ( nLine == 52 && eachSpells == "1"){ button52 = 4018; }
					if ( nLine == 53 && eachSpells == "1"){ button53 = 4018; }
					if ( nLine == 54 && eachSpells == "1"){ button54 = 4018; }
					if ( nLine == 55 && eachSpells == "1"){ button55 = 4018; }
					if ( nLine == 56 && eachSpells == "1"){ button56 = 4018; }
					if ( nLine == 57 && eachSpells == "1"){ button57 = 4018; }
					if ( nLine == 58 && eachSpells == "1"){ button58 = 4018; }
					if ( nLine == 59 && eachSpells == "1"){ button59 = 4018; }
					if ( nLine == 60 && eachSpells == "1"){ button60 = 4018; }
					if ( nLine == 61 && eachSpells == "1"){ button61 = 4018; }
					if ( nLine == 62 && eachSpells == "1"){ button62 = 4018; }
					if ( nLine == 63 && eachSpells == "1"){ button63 = 4018; }
					if ( nLine == 64 && eachSpells == "1"){ button64 = 4018; }

					if ( nLine == 65 && eachSpells == "1" ) { button65 = 4018; }

					if ( nLine == 66 && eachSpells == "0" ) { button66 = 3609; }
					else if ( nLine == 66 && eachSpells == "1" ) { button66 = 4018; }

					if ( nLine == 66 && eachSpells == "1" ) { button67 = 3609; }
					else if ( nLine == 66 && eachSpells == "0" ) { button67 = 4018; }

					nLine++;
				}

				AddButton(582, 82, button65, button65, 90, GumpButtonType.Reply, 0);
				AddHtml( 624, 81, 261, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Show Spell Names When Vertical</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddButton(377, 540, button66, button66, 91, GumpButtonType.Reply, 0);
				AddHtml( 417, 539, 125, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Vertical Bar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddButton(681, 537, button67, button67, 91, GumpButtonType.Reply, 0);
				AddHtml( 721, 536, 125, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Horizontal Bar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				// ------------------------------------------------------------------------------------ 1

				int x1 = 135;
				int x2 = 95;
				int y1 = 120;
				int y2 = 130;
				int rp = 0;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button1, button1, 99, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button2, button2, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button3, button3, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button4, button4, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button5, button5, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button6, button6, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button7, button7, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button8, button8, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 2

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button9, button9, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button10, button10, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button11, button11, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button12, button12, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button13, button13, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button14, button14, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button15, button15, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button16, button16, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 3

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button17, button17, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button18, button18, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button19, button19, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button20, button20, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button21, button21, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button22, button22, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button23, button23, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button24, button24, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 4

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button25, button25, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button26, button26, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button27, button27, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button28, button28, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button29, button29, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button30, button30, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button31, button31, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button32, button32, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 5

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button33, button33, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button34, button34, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button35, button35, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button36, button36, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button37, button37, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button38, button38, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button39, button39, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button40, button40, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 6

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button41, button41, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button42, button42, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button43, button43, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button44, button44, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button45, button45, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button46, button46, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button47, button47, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button48, button48, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 7

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button49, button49, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button50, button50, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button51, button51, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button52, button52, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button53, button53, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button54, button54, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button55, button55, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button56, button56, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 8

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button57, button57, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button58, button58, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button59, button59, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button60, button60, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button61, button61, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button62, button62, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button63, button63, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button64, button64, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				if ( info.ButtonID == 99 ){ ResearchBarSettings.UpdateToolBar( from, 1, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 1 ){ ResearchBarSettings.UpdateToolBar( from, 2, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 2 ){ ResearchBarSettings.UpdateToolBar( from, 3, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 3 ){ ResearchBarSettings.UpdateToolBar( from, 4, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 4 ){ ResearchBarSettings.UpdateToolBar( from, 5, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 5 ){ ResearchBarSettings.UpdateToolBar( from, 6, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 6 ){ ResearchBarSettings.UpdateToolBar( from, 7, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 7 ){ ResearchBarSettings.UpdateToolBar( from, 8, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 8 ){ ResearchBarSettings.UpdateToolBar( from, 9, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 9 ){ ResearchBarSettings.UpdateToolBar( from, 10, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 10 ){ ResearchBarSettings.UpdateToolBar( from, 11, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 11 ){ ResearchBarSettings.UpdateToolBar( from, 12, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 12 ){ ResearchBarSettings.UpdateToolBar( from, 13, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 13 ){ ResearchBarSettings.UpdateToolBar( from, 14, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 14 ){ ResearchBarSettings.UpdateToolBar( from, 15, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 15 ){ ResearchBarSettings.UpdateToolBar( from, 16, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 16 ){ ResearchBarSettings.UpdateToolBar( from, 17, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 17 ){ ResearchBarSettings.UpdateToolBar( from, 18, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 18 ){ ResearchBarSettings.UpdateToolBar( from, 19, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 19 ){ ResearchBarSettings.UpdateToolBar( from, 20, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 20 ){ ResearchBarSettings.UpdateToolBar( from, 21, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 21 ){ ResearchBarSettings.UpdateToolBar( from, 22, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 22 ){ ResearchBarSettings.UpdateToolBar( from, 23, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 23 ){ ResearchBarSettings.UpdateToolBar( from, 24, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 24 ){ ResearchBarSettings.UpdateToolBar( from, 25, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 25 ){ ResearchBarSettings.UpdateToolBar( from, 26, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 26 ){ ResearchBarSettings.UpdateToolBar( from, 27, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 27 ){ ResearchBarSettings.UpdateToolBar( from, 28, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 28 ){ ResearchBarSettings.UpdateToolBar( from, 29, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 29 ){ ResearchBarSettings.UpdateToolBar( from, 30, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 30 ){ ResearchBarSettings.UpdateToolBar( from, 31, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 31 ){ ResearchBarSettings.UpdateToolBar( from, 32, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 32 ){ ResearchBarSettings.UpdateToolBar( from, 33, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 33 ){ ResearchBarSettings.UpdateToolBar( from, 34, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 34 ){ ResearchBarSettings.UpdateToolBar( from, 35, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 35 ){ ResearchBarSettings.UpdateToolBar( from, 36, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 36 ){ ResearchBarSettings.UpdateToolBar( from, 37, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 37 ){ ResearchBarSettings.UpdateToolBar( from, 38, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 38 ){ ResearchBarSettings.UpdateToolBar( from, 39, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 39 ){ ResearchBarSettings.UpdateToolBar( from, 40, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 40 ){ ResearchBarSettings.UpdateToolBar( from, 41, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 41 ){ ResearchBarSettings.UpdateToolBar( from, 42, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 42 ){ ResearchBarSettings.UpdateToolBar( from, 43, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 43 ){ ResearchBarSettings.UpdateToolBar( from, 44, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 44 ){ ResearchBarSettings.UpdateToolBar( from, 45, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 45 ){ ResearchBarSettings.UpdateToolBar( from, 46, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 46 ){ ResearchBarSettings.UpdateToolBar( from, 47, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 47 ){ ResearchBarSettings.UpdateToolBar( from, 48, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 48 ){ ResearchBarSettings.UpdateToolBar( from, 49, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 49 ){ ResearchBarSettings.UpdateToolBar( from, 50, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 50 ){ ResearchBarSettings.UpdateToolBar( from, 51, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 51 ){ ResearchBarSettings.UpdateToolBar( from, 52, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 52 ){ ResearchBarSettings.UpdateToolBar( from, 53, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 53 ){ ResearchBarSettings.UpdateToolBar( from, 54, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 54 ){ ResearchBarSettings.UpdateToolBar( from, 55, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 55 ){ ResearchBarSettings.UpdateToolBar( from, 56, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 56 ){ ResearchBarSettings.UpdateToolBar( from, 57, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 57 ){ ResearchBarSettings.UpdateToolBar( from, 58, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 58 ){ ResearchBarSettings.UpdateToolBar( from, 59, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 59 ){ ResearchBarSettings.UpdateToolBar( from, 60, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 60 ){ ResearchBarSettings.UpdateToolBar( from, 61, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 61 ){ ResearchBarSettings.UpdateToolBar( from, 62, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 62 ){ ResearchBarSettings.UpdateToolBar( from, 63, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 63 ){ ResearchBarSettings.UpdateToolBar( from, 64, "SetupBarsResearch3", 66 ); }

				else if ( info.ButtonID == 90 ){ ResearchBarSettings.UpdateToolBar( from, 65, "SetupBarsResearch3", 66 ); }
				else if ( info.ButtonID == 91 ){ ResearchBarSettings.UpdateToolBar( from, 66, "SetupBarsResearch3", 66 ); }

				if ( info.ButtonID < 1 && m_Origin > 0 )
				{
					ResearchBag bag = (ResearchBag)( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) );
					from.SendGump( new Server.Items.ResearchBag.ResearchGump( bag ) );
					from.SendSound( 0x4A );
				}
				else if ( info.ButtonID < 1 ){}
				else { from.SendGump( new SetupBarsResearch3( from, m_Origin ) ); from.SendSound( 0x4A ); }
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
    public class SetupBarsResearch4 : Gump
    {
		public int m_Origin;

		public static void Initialize()
		{
            CommandSystem.Register( "researchspell4", AccessLevel.Player, new CommandEventHandler( ToolBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "researchspell4" )]
		[Description( "Opens Spell Bar Editor For Researchers - 4." )]
		public static void ToolBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				from.CloseGump( typeof( SetupBarsResearch4 ) );
				from.SendGump( new SetupBarsResearch4( from, 0 ) );
			}
        }

		public SetupBarsResearch4 ( Mobile from, int origin ) : base ( 25,25 )
		{
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				m_Origin = origin;

				Closable=true;
				Disposable=true;
				Dragable=true;
				Resizable=false;

				AddPage(0);
				AddImage(0, 0, 153);
				AddImage(600, 0, 153);
				AddImage(300, 0, 153);
				AddImage(0, 300, 153);
				AddImage(300, 300, 153);
				AddImage(600, 300, 153);
				AddImage(2, 2, 129);
				AddImage(300, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(300, 298, 129);
				AddImage(598, 298, 129);
				AddImage(7, 7, 150);
				AddImage(679, 5, 134);
				AddImage(178, 22, 156);
				AddImage(165, 19, 156);
				AddImage(155, 36, 162);
				AddImage(376, 44, 132);
				AddImage(210, 44, 132);
				AddImage(185, 41, 159);
				AddImage(343, 560, 140);
				AddImage(564, 560, 140);
				AddImage(853, 562, 143);
				AddImage(16, 248, 137);
				AddImage(6, 378, 148);
				AddImage(40, 331, 156);
				AddImage(33, 356, 156);
				AddImage(32, 342, 156);

				AddItem(20, 215, 3570);
				AddItem(22, 374, 3629);

				AddHtml( 196, 79, 337, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>SPELL BAR - RESEARCH - IV</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				ResearchBarSettings.InitializeToolBar( from, "SetupBarsResearch4" );
				ResearchBag bag = (ResearchBag)( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) );
				string MySettings = bag.BarsCast4;

				int button1 = 3609;
				int button2 = 3609;
				int button3 = 3609;
				int button4 = 3609;
				int button5 = 3609;
				int button6 = 3609;
				int button7 = 3609;
				int button8 = 3609;
				int button9 = 3609;
				int button10 = 3609;
				int button11 = 3609;
				int button12 = 3609;
				int button13 = 3609;
				int button14 = 3609;
				int button15 = 3609;
				int button16 = 3609;
				int button17 = 3609;
				int button18 = 3609;
				int button19 = 3609;
				int button20 = 3609;
				int button21 = 3609;
				int button22 = 3609;
				int button23 = 3609;
				int button24 = 3609;
				int button25 = 3609;
				int button26 = 3609;
				int button27 = 3609;
				int button28 = 3609;
				int button29 = 3609;
				int button30 = 3609;
				int button31 = 3609;
				int button32 = 3609;
				int button33 = 3609;
				int button34 = 3609;
				int button35 = 3609;
				int button36 = 3609;
				int button37 = 3609;
				int button38 = 3609;
				int button39 = 3609;
				int button40 = 3609;
				int button41 = 3609;
				int button42 = 3609;
				int button43 = 3609;
				int button44 = 3609;
				int button45 = 3609;
				int button46 = 3609;
				int button47 = 3609;
				int button48 = 3609;
				int button49 = 3609;
				int button50 = 3609;
				int button51 = 3609;
				int button52 = 3609;
				int button53 = 3609;
				int button54 = 3609;
				int button55 = 3609;
				int button56 = 3609;
				int button57 = 3609;
				int button58 = 3609;
				int button59 = 3609;
				int button60 = 3609;
				int button61 = 3609;
				int button62 = 3609;
				int button63 = 3609;
				int button64 = 3609;
				int button65 = 3609;
				int button66 = 3609;
				int button67 = 3609;

				string[] eachSpell = MySettings.Split('#');
				int nLine = 1;
				foreach (string eachSpells in eachSpell)
				{
					if ( nLine == 1 && eachSpells == "1"){ button1 = 4018; }
					if ( nLine == 2 && eachSpells == "1"){ button2 = 4018; }
					if ( nLine == 3 && eachSpells == "1"){ button3 = 4018; }
					if ( nLine == 4 && eachSpells == "1"){ button4 = 4018; }
					if ( nLine == 5 && eachSpells == "1"){ button5 = 4018; }
					if ( nLine == 6 && eachSpells == "1"){ button6 = 4018; }
					if ( nLine == 7 && eachSpells == "1"){ button7 = 4018; }
					if ( nLine == 8 && eachSpells == "1"){ button8 = 4018; }
					if ( nLine == 9 && eachSpells == "1"){ button9 = 4018; }
					if ( nLine == 10 && eachSpells == "1"){ button10 = 4018; }
					if ( nLine == 11 && eachSpells == "1"){ button11 = 4018; }
					if ( nLine == 12 && eachSpells == "1"){ button12 = 4018; }
					if ( nLine == 13 && eachSpells == "1"){ button13 = 4018; }
					if ( nLine == 14 && eachSpells == "1"){ button14 = 4018; }
					if ( nLine == 15 && eachSpells == "1"){ button15 = 4018; }
					if ( nLine == 16 && eachSpells == "1"){ button16 = 4018; }
					if ( nLine == 17 && eachSpells == "1"){ button17 = 4018; }
					if ( nLine == 18 && eachSpells == "1"){ button18 = 4018; }
					if ( nLine == 19 && eachSpells == "1"){ button19 = 4018; }
					if ( nLine == 20 && eachSpells == "1"){ button20 = 4018; }
					if ( nLine == 21 && eachSpells == "1"){ button21 = 4018; }
					if ( nLine == 22 && eachSpells == "1"){ button22 = 4018; }
					if ( nLine == 23 && eachSpells == "1"){ button23 = 4018; }
					if ( nLine == 24 && eachSpells == "1"){ button24 = 4018; }
					if ( nLine == 25 && eachSpells == "1"){ button25 = 4018; }
					if ( nLine == 26 && eachSpells == "1"){ button26 = 4018; }
					if ( nLine == 27 && eachSpells == "1"){ button27 = 4018; }
					if ( nLine == 28 && eachSpells == "1"){ button28 = 4018; }
					if ( nLine == 29 && eachSpells == "1"){ button29 = 4018; }
					if ( nLine == 30 && eachSpells == "1"){ button30 = 4018; }
					if ( nLine == 31 && eachSpells == "1"){ button31 = 4018; }
					if ( nLine == 32 && eachSpells == "1"){ button32 = 4018; }
					if ( nLine == 33 && eachSpells == "1"){ button33 = 4018; }
					if ( nLine == 34 && eachSpells == "1"){ button34 = 4018; }
					if ( nLine == 35 && eachSpells == "1"){ button35 = 4018; }
					if ( nLine == 36 && eachSpells == "1"){ button36 = 4018; }
					if ( nLine == 37 && eachSpells == "1"){ button37 = 4018; }
					if ( nLine == 38 && eachSpells == "1"){ button38 = 4018; }
					if ( nLine == 39 && eachSpells == "1"){ button39 = 4018; }
					if ( nLine == 40 && eachSpells == "1"){ button40 = 4018; }
					if ( nLine == 41 && eachSpells == "1"){ button41 = 4018; }
					if ( nLine == 42 && eachSpells == "1"){ button42 = 4018; }
					if ( nLine == 43 && eachSpells == "1"){ button43 = 4018; }
					if ( nLine == 44 && eachSpells == "1"){ button44 = 4018; }
					if ( nLine == 45 && eachSpells == "1"){ button45 = 4018; }
					if ( nLine == 46 && eachSpells == "1"){ button46 = 4018; }
					if ( nLine == 47 && eachSpells == "1"){ button47 = 4018; }
					if ( nLine == 48 && eachSpells == "1"){ button48 = 4018; }
					if ( nLine == 49 && eachSpells == "1"){ button49 = 4018; }
					if ( nLine == 50 && eachSpells == "1"){ button50 = 4018; }
					if ( nLine == 51 && eachSpells == "1"){ button51 = 4018; }
					if ( nLine == 52 && eachSpells == "1"){ button52 = 4018; }
					if ( nLine == 53 && eachSpells == "1"){ button53 = 4018; }
					if ( nLine == 54 && eachSpells == "1"){ button54 = 4018; }
					if ( nLine == 55 && eachSpells == "1"){ button55 = 4018; }
					if ( nLine == 56 && eachSpells == "1"){ button56 = 4018; }
					if ( nLine == 57 && eachSpells == "1"){ button57 = 4018; }
					if ( nLine == 58 && eachSpells == "1"){ button58 = 4018; }
					if ( nLine == 59 && eachSpells == "1"){ button59 = 4018; }
					if ( nLine == 60 && eachSpells == "1"){ button60 = 4018; }
					if ( nLine == 61 && eachSpells == "1"){ button61 = 4018; }
					if ( nLine == 62 && eachSpells == "1"){ button62 = 4018; }
					if ( nLine == 63 && eachSpells == "1"){ button63 = 4018; }
					if ( nLine == 64 && eachSpells == "1"){ button64 = 4018; }

					if ( nLine == 65 && eachSpells == "1" ) { button65 = 4018; }

					if ( nLine == 66 && eachSpells == "0" ) { button66 = 3609; }
					else if ( nLine == 66 && eachSpells == "1" ) { button66 = 4018; }

					if ( nLine == 66 && eachSpells == "1" ) { button67 = 3609; }
					else if ( nLine == 66 && eachSpells == "0" ) { button67 = 4018; }

					nLine++;
				}

				AddButton(582, 82, button65, button65, 90, GumpButtonType.Reply, 0);
				AddHtml( 624, 81, 261, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Show Spell Names When Vertical</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddButton(377, 540, button66, button66, 91, GumpButtonType.Reply, 0);
				AddHtml( 417, 539, 125, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Vertical Bar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddButton(681, 537, button67, button67, 91, GumpButtonType.Reply, 0);
				AddHtml( 721, 536, 125, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Horizontal Bar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				// ------------------------------------------------------------------------------------ 1

				int x1 = 135;
				int x2 = 95;
				int y1 = 120;
				int y2 = 130;
				int rp = 0;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button1, button1, 99, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button2, button2, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button3, button3, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button4, button4, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button5, button5, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button6, button6, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button7, button7, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button8, button8, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 2

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button9, button9, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button10, button10, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button11, button11, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button12, button12, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button13, button13, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button14, button14, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button15, button15, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button16, button16, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 3

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button17, button17, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button18, button18, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button19, button19, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button20, button20, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button21, button21, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button22, button22, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button23, button23, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button24, button24, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 4

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button25, button25, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button26, button26, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button27, button27, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button28, button28, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button29, button29, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button30, button30, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button31, button31, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button32, button32, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 5

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button33, button33, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button34, button34, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button35, button35, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button36, button36, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button37, button37, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button38, button38, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button39, button39, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button40, button40, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 6

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button41, button41, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button42, button42, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button43, button43, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button44, button44, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button45, button45, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button46, button46, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button47, button47, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button48, button48, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 7

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button49, button49, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button50, button50, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button51, button51, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button52, button52, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button53, button53, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button54, button54, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button55, button55, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button56, button56, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				// ------------------------------------------------------------------------------------ 8

				x1 = x1+100;
				x2 = x2+100;
				y1 = 120;
				y2 = 130;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button57, button57, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button58, button58, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button59, button59, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button60, button60, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button61, button61, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button62, button62, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button63, button63, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;

				AddImage(x1, y1, Int32.Parse( Server.Misc.Research.SpellInformation( rp+1, 11 ) ));
				AddButton(x2, y2, button64, button64, rp, GumpButtonType.Reply, 0);
					y1=y1+45;	y2=y2+45;	rp++;
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				if ( info.ButtonID == 99 ){ ResearchBarSettings.UpdateToolBar( from, 1, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 1 ){ ResearchBarSettings.UpdateToolBar( from, 2, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 2 ){ ResearchBarSettings.UpdateToolBar( from, 3, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 3 ){ ResearchBarSettings.UpdateToolBar( from, 4, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 4 ){ ResearchBarSettings.UpdateToolBar( from, 5, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 5 ){ ResearchBarSettings.UpdateToolBar( from, 6, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 6 ){ ResearchBarSettings.UpdateToolBar( from, 7, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 7 ){ ResearchBarSettings.UpdateToolBar( from, 8, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 8 ){ ResearchBarSettings.UpdateToolBar( from, 9, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 9 ){ ResearchBarSettings.UpdateToolBar( from, 10, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 10 ){ ResearchBarSettings.UpdateToolBar( from, 11, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 11 ){ ResearchBarSettings.UpdateToolBar( from, 12, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 12 ){ ResearchBarSettings.UpdateToolBar( from, 13, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 13 ){ ResearchBarSettings.UpdateToolBar( from, 14, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 14 ){ ResearchBarSettings.UpdateToolBar( from, 15, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 15 ){ ResearchBarSettings.UpdateToolBar( from, 16, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 16 ){ ResearchBarSettings.UpdateToolBar( from, 17, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 17 ){ ResearchBarSettings.UpdateToolBar( from, 18, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 18 ){ ResearchBarSettings.UpdateToolBar( from, 19, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 19 ){ ResearchBarSettings.UpdateToolBar( from, 20, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 20 ){ ResearchBarSettings.UpdateToolBar( from, 21, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 21 ){ ResearchBarSettings.UpdateToolBar( from, 22, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 22 ){ ResearchBarSettings.UpdateToolBar( from, 23, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 23 ){ ResearchBarSettings.UpdateToolBar( from, 24, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 24 ){ ResearchBarSettings.UpdateToolBar( from, 25, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 25 ){ ResearchBarSettings.UpdateToolBar( from, 26, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 26 ){ ResearchBarSettings.UpdateToolBar( from, 27, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 27 ){ ResearchBarSettings.UpdateToolBar( from, 28, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 28 ){ ResearchBarSettings.UpdateToolBar( from, 29, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 29 ){ ResearchBarSettings.UpdateToolBar( from, 30, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 30 ){ ResearchBarSettings.UpdateToolBar( from, 31, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 31 ){ ResearchBarSettings.UpdateToolBar( from, 32, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 32 ){ ResearchBarSettings.UpdateToolBar( from, 33, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 33 ){ ResearchBarSettings.UpdateToolBar( from, 34, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 34 ){ ResearchBarSettings.UpdateToolBar( from, 35, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 35 ){ ResearchBarSettings.UpdateToolBar( from, 36, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 36 ){ ResearchBarSettings.UpdateToolBar( from, 37, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 37 ){ ResearchBarSettings.UpdateToolBar( from, 38, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 38 ){ ResearchBarSettings.UpdateToolBar( from, 39, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 39 ){ ResearchBarSettings.UpdateToolBar( from, 40, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 40 ){ ResearchBarSettings.UpdateToolBar( from, 41, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 41 ){ ResearchBarSettings.UpdateToolBar( from, 42, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 42 ){ ResearchBarSettings.UpdateToolBar( from, 43, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 43 ){ ResearchBarSettings.UpdateToolBar( from, 44, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 44 ){ ResearchBarSettings.UpdateToolBar( from, 45, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 45 ){ ResearchBarSettings.UpdateToolBar( from, 46, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 46 ){ ResearchBarSettings.UpdateToolBar( from, 47, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 47 ){ ResearchBarSettings.UpdateToolBar( from, 48, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 48 ){ ResearchBarSettings.UpdateToolBar( from, 49, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 49 ){ ResearchBarSettings.UpdateToolBar( from, 50, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 50 ){ ResearchBarSettings.UpdateToolBar( from, 51, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 51 ){ ResearchBarSettings.UpdateToolBar( from, 52, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 52 ){ ResearchBarSettings.UpdateToolBar( from, 53, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 53 ){ ResearchBarSettings.UpdateToolBar( from, 54, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 54 ){ ResearchBarSettings.UpdateToolBar( from, 55, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 55 ){ ResearchBarSettings.UpdateToolBar( from, 56, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 56 ){ ResearchBarSettings.UpdateToolBar( from, 57, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 57 ){ ResearchBarSettings.UpdateToolBar( from, 58, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 58 ){ ResearchBarSettings.UpdateToolBar( from, 59, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 59 ){ ResearchBarSettings.UpdateToolBar( from, 60, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 60 ){ ResearchBarSettings.UpdateToolBar( from, 61, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 61 ){ ResearchBarSettings.UpdateToolBar( from, 62, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 62 ){ ResearchBarSettings.UpdateToolBar( from, 63, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 63 ){ ResearchBarSettings.UpdateToolBar( from, 64, "SetupBarsResearch4", 66 ); }

				else if ( info.ButtonID == 90 ){ ResearchBarSettings.UpdateToolBar( from, 65, "SetupBarsResearch4", 66 ); }
				else if ( info.ButtonID == 91 ){ ResearchBarSettings.UpdateToolBar( from, 66, "SetupBarsResearch4", 66 ); }

				if ( info.ButtonID < 1 && m_Origin > 0 )
				{
					ResearchBag bag = (ResearchBag)( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) );
					from.SendGump( new Server.Items.ResearchBag.ResearchGump( bag ) );
					from.SendSound( 0x4A );
				}
				else if ( info.ButtonID < 1 ){}
				else { from.SendGump( new SetupBarsResearch4( from, m_Origin ) ); from.SendSound( 0x4A ); }
			}
		}
    }
}
