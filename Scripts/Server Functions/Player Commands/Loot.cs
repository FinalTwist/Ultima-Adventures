using System;
using Server;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.Commands;
using Server.Commands.Generic;
using Server.Prompts;
using Server.Gumps;

namespace Server.Misc
{
    class LootChoiceUpdates
    {
		public static void UpdateLootChoice( Mobile m, int nChange )
		{
			LootChoiceUpdates.InitializeLootChoice( m );

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			string LootChoiceSetting = DB.CharacterLoot;

			string[] eachSetting = LootChoiceSetting.Split('#');
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
				else if ( nLine > 17 )
				{
				}
				else
				{
					newSettings = newSettings + eachSettings + "#";
				}
				nLine++;
			}

			DB.CharacterLoot = newSettings; 
		}

		public static void InitializeLootChoice( Mobile m )
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			if ( DB.CharacterLoot == null ){ DB.CharacterLoot = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#"; }
		}
	}
}

namespace Server.Gumps 
{
    public class LootChoices : Gump
    {
		public int m_Origin;

		public static void Initialize()
		{
            CommandSystem.Register( "loot", AccessLevel.Player, new CommandEventHandler( LootChoice_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "loot" )]
		[Description( "Allows you to setup automatic looting." )]
		public static void LootChoice_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( LootChoices ) );
			from.SendGump( new LootChoices( from, 0 ) );
        }

        public LootChoices ( Mobile from, int origin ) : base ( 25,25 )
        {
			m_Origin = origin;

			LootChoiceUpdates.InitializeLootChoice( from );
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );
			string MySettings = DB.CharacterLoot;

			this.Closable=true;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 151);
			AddImage(300, 0, 151);
			AddImage(0, 300, 151);
			AddImage(300, 300, 151);
			AddImage(2, 2, 129);
			AddImage(298, 2, 129);
			AddImage(2, 298, 129);
			AddImage(298, 298, 129);
			AddImage(362, 8, 138);
			AddImage(8, 8, 133);
			AddImage(7, 378, 148);
			AddImage(251, 378, 144);
			AddImage(380, 534, 159);
			AddImage(185, 534, 143);
			AddImage(188, 542, 156);
			AddImage(376, 544, 156);
			AddImage(213, 542, 156);
			AddImage(240, 552, 156);
			AddImage(349, 544, 156);
			AddImage(320, 550, 156);
			AddImage(294, 556, 156);
			AddImage(269, 559, 156);

			AddHtml( 253, 43, 94, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>LOOT</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 102, 85, 396, 124, @"<BODY><BASEFONT Color=#FBFBFB>Check the categories of items to automatically take from common dungeon chests or corpses and put them in your backpack. Magery and necromancer reagents are those used specifically by those characters. Alchemic reagents are unique to alchemy only. Herbalist reagents are plants that one may find, used in druidic herbalism.</BASEFONT></BODY>", (bool)false, (bool)false);
			AddItem(257, 504, 2169);

			string[] eachLoot = MySettings.Split('#');
			int nLine = 1;

			AddHtml( 145, 225, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Coins & Nuggets</BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 145, 265, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Gems & Jewels</BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 145, 300, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Arrows</BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 145, 340, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Bolts</BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 145, 380, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Bandages</BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 145, 420, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Magery Scrolls</BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 145, 460, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Necromancer Scrolls</BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 145, 500, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Unknown Scrolls</BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 320, 225, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Magery Reagents</BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 320, 265, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Necromancy Reagents</BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 320, 300, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Alchemic Reagents</BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 320, 340, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Herbalist Reagents</BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 320, 380, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Unknown Reagents</BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 320, 420, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Potions</BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 320, 460, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Unknown Potions</BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 320, 500, 130, 21, @"<BODY><BASEFONT Color=#FCFF00>Bard Songs</BASEFONT></BODY>", (bool)false, (bool)false);

			foreach (string eachLoots in eachLoot)
			{
				if (nLine == 1 && eachLoots == "0"){ AddButton(105, 225, 3609, 3609, 99, GumpButtonType.Reply, 1); } else if (nLine == 1){  AddButton(105, 225, 4018, 4018, 99, GumpButtonType.Reply, 1); }
				if (nLine == 2 && eachLoots == "0"){ AddButton(105, 265, 3609, 3609, 1, GumpButtonType.Reply, 1); } else if (nLine == 2){ AddButton(105, 265, 4018, 4018, 1, GumpButtonType.Reply, 1); }
				if (nLine == 3 && eachLoots == "0"){ AddButton(105, 300, 3609, 3609, 2, GumpButtonType.Reply, 1); } else if (nLine == 3){ AddButton(105, 300, 4018, 4018, 2, GumpButtonType.Reply, 1); }
				if (nLine == 4 && eachLoots == "0"){ AddButton(105, 340, 3609, 3609, 3, GumpButtonType.Reply, 1); } else if (nLine == 4){ AddButton(105, 340, 4018, 4018, 3, GumpButtonType.Reply, 1); }
				if (nLine == 5 && eachLoots == "0"){ AddButton(105, 380, 3609, 3609, 4, GumpButtonType.Reply, 1); } else if (nLine == 5){ AddButton(105, 380, 4018, 4018, 4, GumpButtonType.Reply, 1); }
				if (nLine == 6 && eachLoots == "0"){ AddButton(105, 420, 3609, 3609, 5, GumpButtonType.Reply, 1); } else if (nLine == 6){ AddButton(105, 420, 4018, 4018, 5, GumpButtonType.Reply, 1); }
				if (nLine == 7 && eachLoots == "0"){ AddButton(105, 460, 3609, 3609, 6, GumpButtonType.Reply, 1); } else if (nLine == 7){ AddButton(105, 460, 4018, 4018, 6, GumpButtonType.Reply, 1); }
				if (nLine == 14 && eachLoots == "0"){ AddButton(105, 500, 3609, 3609, 13, GumpButtonType.Reply, 1); } else if (nLine == 14){ AddButton(105, 500, 4018, 4018, 13, GumpButtonType.Reply, 1); }

				if (nLine == 8 && eachLoots == "0"){ AddButton(465, 225, 3609, 3609, 7, GumpButtonType.Reply, 1); } else if (nLine == 8){ AddButton(465, 225, 4018, 4018, 7, GumpButtonType.Reply, 1); }
				if (nLine == 9 && eachLoots == "0"){ AddButton(465, 265, 3609, 3609, 8, GumpButtonType.Reply, 1); } else if (nLine == 9){ AddButton(465, 265, 4018, 4018, 8, GumpButtonType.Reply, 1); }
				if (nLine == 15 && eachLoots == "0"){ AddButton(465, 300, 3609, 3609, 14, GumpButtonType.Reply, 1); } else if (nLine == 15){ AddButton(465, 300, 4018, 4018, 14, GumpButtonType.Reply, 1); }
				if (nLine == 16 && eachLoots == "0"){ AddButton(465, 340, 3609, 3609, 15, GumpButtonType.Reply, 1); } else if (nLine == 16){ AddButton(465, 340, 4018, 4018, 15, GumpButtonType.Reply, 1); }
				if (nLine == 10 && eachLoots == "0"){ AddButton(465, 380, 3609, 3609, 9, GumpButtonType.Reply, 1); } else if (nLine == 10){ AddButton(465, 380, 4018, 4018, 9, GumpButtonType.Reply, 1); }
				if (nLine == 11 && eachLoots == "0"){ AddButton(465, 420, 3609, 3609, 10, GumpButtonType.Reply, 1); } else if (nLine == 11){ AddButton(465, 420, 4018, 4018, 10, GumpButtonType.Reply, 1); }
				if (nLine == 12 && eachLoots == "0"){ AddButton(465, 460, 3609, 3609, 11, GumpButtonType.Reply, 1); } else if (nLine == 12){ AddButton(465, 460, 4018, 4018, 11, GumpButtonType.Reply, 1); }
				if (nLine == 13 && eachLoots == "0"){ AddButton(465, 500, 3609, 3609, 12, GumpButtonType.Reply, 1); } else if (nLine == 13){ AddButton(465, 500, 4018, 4018, 12, GumpButtonType.Reply, 1); }

				nLine++;
			}
        }
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( info.ButtonID == 99 ){ LootChoiceUpdates.UpdateLootChoice( from, 1 ); }
			else if ( info.ButtonID == 1 ){ LootChoiceUpdates.UpdateLootChoice( from, 2 ); }
			else if ( info.ButtonID == 2 ){ LootChoiceUpdates.UpdateLootChoice( from, 3 ); }
			else if ( info.ButtonID == 3 ){ LootChoiceUpdates.UpdateLootChoice( from, 4 ); }
			else if ( info.ButtonID == 4 ){ LootChoiceUpdates.UpdateLootChoice( from, 5 ); }
			else if ( info.ButtonID == 5 ){ LootChoiceUpdates.UpdateLootChoice( from, 6 ); }
			else if ( info.ButtonID == 6 ){ LootChoiceUpdates.UpdateLootChoice( from, 7 ); }
			else if ( info.ButtonID == 7 ){ LootChoiceUpdates.UpdateLootChoice( from, 8 ); }
			else if ( info.ButtonID == 8 ){ LootChoiceUpdates.UpdateLootChoice( from, 9 ); }
			else if ( info.ButtonID == 9 ){ LootChoiceUpdates.UpdateLootChoice( from, 10 ); }
			else if ( info.ButtonID == 10 ){ LootChoiceUpdates.UpdateLootChoice( from, 11 ); }
			else if ( info.ButtonID == 11 ){ LootChoiceUpdates.UpdateLootChoice( from, 12 ); }
			else if ( info.ButtonID == 12 ){ LootChoiceUpdates.UpdateLootChoice( from, 13 ); }
			else if ( info.ButtonID == 13 ){ LootChoiceUpdates.UpdateLootChoice( from, 14 ); }
			else if ( info.ButtonID == 14 ){ LootChoiceUpdates.UpdateLootChoice( from, 15 ); }
			else if ( info.ButtonID == 15 ){ LootChoiceUpdates.UpdateLootChoice( from, 16 ); }
			else if ( info.ButtonID == 16 ){ LootChoiceUpdates.UpdateLootChoice( from, 17 ); }

			if ( info.ButtonID < 1 && m_Origin > 0 ){ from.SendSound( 0x4A ); from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) ); }
			else if ( info.ButtonID < 1 ){ }
			else { from.SendGump( new LootChoices( from, m_Origin ) ); from.SendSound( 0x4A ); }
		}
    }
}