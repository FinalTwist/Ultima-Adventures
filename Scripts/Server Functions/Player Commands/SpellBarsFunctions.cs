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

namespace Server.Misc
{
    class ToolBarUpdates
    {
		public static void UpdateToolBar( Mobile m, int nChange, string ToolBar, int nTotal )
		{
			ToolBarUpdates.InitializeToolBar( m, ToolBar );

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			string ToolBarSetting = "";

			if ( ToolBar == "SetupBarsMage1" ){ ToolBarSetting = DB.SpellBarsMage1; }
			else if ( ToolBar == "SetupBarsMage2" ){ ToolBarSetting = DB.SpellBarsMage2; }
			else if ( ToolBar == "SetupBarsMage3" ){ ToolBarSetting = DB.SpellBarsMage3; }
			else if ( ToolBar == "SetupBarsMage4" ){ ToolBarSetting = DB.SpellBarsMage4; }
			else if ( ToolBar == "SetupBarsNecro1" ){ ToolBarSetting = DB.SpellBarsNecro1; }
			else if ( ToolBar == "SetupBarsNecro2" ){ ToolBarSetting = DB.SpellBarsNecro2; }
			else if ( ToolBar == "SetupBarsChivalry1" ){ ToolBarSetting = DB.SpellBarsChivalry1; }
			else if ( ToolBar == "SetupBarsChivalry2" ){ ToolBarSetting = DB.SpellBarsChivalry2; }
			else if ( ToolBar == "SetupBarsDeath1" ){ ToolBarSetting = DB.SpellBarsDeath1; }
			else if ( ToolBar == "SetupBarsDeath2" ){ ToolBarSetting = DB.SpellBarsDeath2; }
			else if ( ToolBar == "SetupBarsBard1" ){ ToolBarSetting = DB.SpellBarsBard1; }
			else if ( ToolBar == "SetupBarsBard2" ){ ToolBarSetting = DB.SpellBarsBard2; }
			else if ( ToolBar == "SetupBarsPriest1" ){ ToolBarSetting = DB.SpellBarsPriest1; }
			else if ( ToolBar == "SetupBarsPriest2" ){ ToolBarSetting = DB.SpellBarsPriest2; }
			else if ( ToolBar == "SetupBarsMonk1" ){ ToolBarSetting = DB.SpellBarsMonk1; }
			else if ( ToolBar == "SetupBarsMonk2" ){ ToolBarSetting = DB.SpellBarsMonk2; }

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

			if ( ToolBar == "SetupBarsMage1" ){ DB.SpellBarsMage1 = newSettings; }
			else if ( ToolBar == "SetupBarsMage2" ){ DB.SpellBarsMage2 = newSettings; }
			else if ( ToolBar == "SetupBarsMage3" ){ DB.SpellBarsMage3 = newSettings; }
			else if ( ToolBar == "SetupBarsMage4" ){ DB.SpellBarsMage4 = newSettings; }
			else if ( ToolBar == "SetupBarsNecro1" ){ DB.SpellBarsNecro1 = newSettings; }
			else if ( ToolBar == "SetupBarsNecro2" ){ DB.SpellBarsNecro2 = newSettings; }
			else if ( ToolBar == "SetupBarsChivalry1" ){ DB.SpellBarsChivalry1 = newSettings; }
			else if ( ToolBar == "SetupBarsChivalry2" ){ DB.SpellBarsChivalry2 = newSettings; }
			else if ( ToolBar == "SetupBarsDeath1" ){ DB.SpellBarsDeath1 = newSettings; }
			else if ( ToolBar == "SetupBarsDeath2" ){ DB.SpellBarsDeath2 = newSettings; }
			else if ( ToolBar == "SetupBarsBard1" ){ DB.SpellBarsBard1 = newSettings; }
			else if ( ToolBar == "SetupBarsBard2" ){ DB.SpellBarsBard2 = newSettings; }
			else if ( ToolBar == "SetupBarsPriest1" ){ DB.SpellBarsPriest1 = newSettings; }
			else if ( ToolBar == "SetupBarsPriest2" ){ DB.SpellBarsPriest2 = newSettings; }
			else if ( ToolBar == "SetupBarsMonk1" ){ DB.SpellBarsMonk1 = newSettings; }
			else if ( ToolBar == "SetupBarsMonk2" ){ DB.SpellBarsMonk2 = newSettings; }
		}

		public static void InitializeToolBar( Mobile m, string ToolBar )
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if ( ToolBar == "SetupBarsMage1" && DB.SpellBarsMage1 == null ){ DB.SpellBarsMage1 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
			else if ( ToolBar == "SetupBarsMage2" && DB.SpellBarsMage2 == null ){ DB.SpellBarsMage2 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
			else if ( ToolBar == "SetupBarsMage3" && DB.SpellBarsMage3 == null ){ DB.SpellBarsMage3 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
			else if ( ToolBar == "SetupBarsMage4" && DB.SpellBarsMage4 == null ){ DB.SpellBarsMage4 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
			else if ( ToolBar == "SetupBarsNecro1" && DB.SpellBarsNecro1 == null ){ DB.SpellBarsNecro1 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
			else if ( ToolBar == "SetupBarsNecro2" && DB.SpellBarsNecro2 == null ){ DB.SpellBarsNecro2 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
			else if ( ToolBar == "SetupBarsChivalry1" && DB.SpellBarsChivalry1 == null ){ DB.SpellBarsChivalry1 = "0#0#0#0#0#0#0#0#0#0#0#0#"; }
			else if ( ToolBar == "SetupBarsChivalry2" && DB.SpellBarsChivalry2 == null ){ DB.SpellBarsChivalry2 = "0#0#0#0#0#0#0#0#0#0#0#0#"; }
			else if ( ToolBar == "SetupBarsDeath1" && DB.SpellBarsDeath1 == null ){ DB.SpellBarsDeath1 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
			else if ( ToolBar == "SetupBarsDeath2" && DB.SpellBarsDeath2 == null ){ DB.SpellBarsDeath2 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
			else if ( ToolBar == "SetupBarsBard1" && DB.SpellBarsBard1 == null ){ DB.SpellBarsBard1 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
			else if ( ToolBar == "SetupBarsBard2" && DB.SpellBarsBard2 == null ){ DB.SpellBarsBard2 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
			else if ( ToolBar == "SetupBarsPriest1" && DB.SpellBarsPriest1 == null ){ DB.SpellBarsPriest1 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
			else if ( ToolBar == "SetupBarsPriest2" && DB.SpellBarsPriest2 == null ){ DB.SpellBarsPriest2 = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
			else if ( ToolBar == "SetupBarsMonk1" && DB.SpellBarsMonk1 == null ){ DB.SpellBarsMonk1 = "0#0#0#0#0#0#0#0#0#0#0#0#"; }
			else if ( ToolBar == "SetupBarsMonk2" && DB.SpellBarsMonk2 == null ){ DB.SpellBarsMonk2 = "0#0#0#0#0#0#0#0#0#0#0#0#"; }
		}

		public static int GetToolBarSetting( Mobile m, int nSetting, string ToolBar )
		{
			PlayerMobile pm = (PlayerMobile)m;
			string sSetting = "0";

			ToolBarUpdates.InitializeToolBar( m, ToolBar );

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			string ToolBarSetting = "";

			if ( ToolBar == "SetupBarsMage1" ){ ToolBarSetting = DB.SpellBarsMage1; }
			else if ( ToolBar == "SetupBarsMage2" ){ ToolBarSetting = DB.SpellBarsMage2; }
			else if ( ToolBar == "SetupBarsMage3" ){ ToolBarSetting = DB.SpellBarsMage3; }
			else if ( ToolBar == "SetupBarsMage4" ){ ToolBarSetting = DB.SpellBarsMage4; }
			else if ( ToolBar == "SetupBarsNecro1" ){ ToolBarSetting = DB.SpellBarsNecro1; }
			else if ( ToolBar == "SetupBarsNecro2" ){ ToolBarSetting = DB.SpellBarsNecro2; }
			else if ( ToolBar == "SetupBarsChivalry1" ){ ToolBarSetting = DB.SpellBarsChivalry1; }
			else if ( ToolBar == "SetupBarsChivalry2" ){ ToolBarSetting = DB.SpellBarsChivalry2; }
			else if ( ToolBar == "SetupBarsDeath1" ){ ToolBarSetting = DB.SpellBarsDeath1; }
			else if ( ToolBar == "SetupBarsDeath2" ){ ToolBarSetting = DB.SpellBarsDeath2; }
			else if ( ToolBar == "SetupBarsBard1" ){ ToolBarSetting = DB.SpellBarsBard1; }
			else if ( ToolBar == "SetupBarsBard2" ){ ToolBarSetting = DB.SpellBarsBard2; }
			else if ( ToolBar == "SetupBarsPriest1" ){ ToolBarSetting = DB.SpellBarsPriest1; }
			else if ( ToolBar == "SetupBarsPriest2" ){ ToolBarSetting = DB.SpellBarsPriest2; }
			else if ( ToolBar == "SetupBarsMonk1" ){ ToolBarSetting = DB.SpellBarsMonk1; }
			else if ( ToolBar == "SetupBarsMonk2" ){ ToolBarSetting = DB.SpellBarsMonk2; }

			string[] eachSetting = ToolBarSetting.Split('#');
			int nLine = 1;

			foreach (string eachSettings in eachSetting)
			{
				if ( nLine == nSetting ){ sSetting = eachSettings; }
				nLine++;
			}

			int nValue = Convert.ToInt32(sSetting);

			return nValue;
		}
	}
}