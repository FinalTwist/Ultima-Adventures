using System;
using Server;
using Server.Misc;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Items;
using System.Text;
using Server.Mobiles;
using System.Collections;
using Server.Commands.Generic;

namespace Server.Gumps
{
	public class LoggingGumpCrier : Gump
	{
        public LoggingGumpCrier( Mobile from, int page ) : base( 25, 25 )
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 155);
			AddImage(300, 0, 155);
			AddImage(600, 0, 155);
			AddImage(0, 300, 155);
			AddImage(300, 300, 155);
			AddImage(600, 300, 155);
			AddImage(2, 2, 129);
			AddImage(298, 2, 129);
			AddImage(598, 2, 129);
			AddImage(2, 298, 129);
			AddImage(298, 298, 129);
			AddImage(598, 298, 129);
			AddImage(7, 8, 145);
			AddImage(553, 378, 144);
			AddImage(698, 8, 146);
			AddImage(8, 353, 142);
			AddImage(167, 8, 129);
			AddImage(270, 291, 129);
			AddImage(471, 289, 129);
			AddImage(271, 565, 143);
			AddImage(741, 525, 159);
			AddImage(730, 38, 162);
			AddImage(712, 24, 162);
			AddImage(722, 37, 162);
			AddImage(707, 37, 162);
			AddImage(695, 36, 162);
			AddButton(100, 155, 4005, 4005, 1, GumpButtonType.Reply, 0);
			AddButton(100, 204, 4005, 4005, 2, GumpButtonType.Reply, 0);
			AddButton(100, 253, 4005, 4005, 3, GumpButtonType.Reply, 0);
			AddButton(100, 302, 4005, 4005, 6, GumpButtonType.Reply, 0);
			AddButton(100, 351, 4005, 4005, 4, GumpButtonType.Reply, 0);
			AddButton(100, 400, 4005, 4005, 5, GumpButtonType.Reply, 0);
			AddHtml( 177, 22, 518, 31, @"<BODY><BASEFONT Color=#FBFBFB><BIG>FROM THE NEWS OF THE TOWN CRIER</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			string colorMenuA = "#FCFF00"; if ( page == 2 ){ colorMenuA = "#FFA200"; }
			string colorMenuB = "#FCFF00"; if ( page == 3 ){ colorMenuB = "#FFA200"; }
			string colorMenuC = "#FCFF00"; if ( page == 4 ){ colorMenuC = "#FFA200"; }
			string colorMenuD = "#FCFF00"; if ( page == 5 ){ colorMenuD = "#FFA200"; }
			string colorMenuE = "#FCFF00"; if ( page == 6 ){ colorMenuE = "#FFA200"; }
			string colorMenuF = "#FCFF00"; if ( page == 7 ){ colorMenuF = "#FFA200"; }


			AddHtml( 145, 155, 222, 24, @"<BODY><BASEFONT Color=" + colorMenuA + "><BIG>Deeds in the Realm</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 145, 204, 222, 24, @"<BODY><BASEFONT Color=" + colorMenuB + "><BIG>Exploration in the Realm</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 145, 253, 222, 24, @"<BODY><BASEFONT Color=" + colorMenuC + "><BIG>Victories in Battle</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 145, 302, 222, 24, @"<BODY><BASEFONT Color=" + colorMenuF + "><BIG>Gossip in the Realm</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 145, 351, 222, 24, @"<BODY><BASEFONT Color=" + colorMenuD + "><BIG>Recent Deaths</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 145, 400, 222, 24, @"<BODY><BASEFONT Color=" + colorMenuE + "><BIG>Wanted Murderers</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddItem(45, 317, 7186);
			AddItem(41, 277, 5914);
			AddItem(255, 458, 19105);
			AddItem(173, 61, 2221);
			AddItem(99, 452, 19319);
			AddItem(332, 89, 4458);

			if ( page == 2 )
			{
				string sEvents = "Deeds In The Realm<br><br>" + LoggingFunctions.LogRead( "Logging Quests", from );
				AddHtml( 394, 117, 403, 390, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sEvents + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			}
			else if ( page == 3 )
			{
				string sEvents = "Exploration In The Realm<br><br>" + LoggingFunctions.LogRead( "Logging Journies", from );
				AddHtml( 394, 117, 403, 390, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sEvents + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			}
			else if ( page == 4 )
			{
				string sEvents = "Victories In The Realm<br><br>" + LoggingFunctions.LogRead( "Logging Battles", from );
				AddHtml( 394, 117, 403, 390, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sEvents + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			}
			else if ( page == 5 )
			{
				string sEvents = "Recent Deaths In The Realm<br><br>" + LoggingFunctions.LogRead( "Logging Deaths", from );
				AddHtml( 394, 117, 403, 390, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sEvents + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			}
			else if ( page == 6 )
			{
				string sEvents = "Murderers In The Realm<br><br>" + LoggingFunctions.LogRead( "Logging Murderers", from );
				AddHtml( 394, 117, 403, 390, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sEvents + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			}
			else if ( page == 7 )
			{
				string sEvents = "Gossip In The Realm<br><br>" + LoggingFunctions.LogRead( "Logging Adventures", from );
				AddHtml( 394, 117, 403, 390, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sEvents + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			}
        }

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			switch ( info.ButtonID )
			{
				case 1:
				{
					from.CloseGump( typeof( LoggingGumpCrier ) );
					from.SendGump( new LoggingGumpCrier( from, 2 ) );
					break;
				}
				case 2:
				{
					from.CloseGump( typeof( LoggingGumpCrier ) );
					from.SendGump( new LoggingGumpCrier( from, 3 ) );
					break;
				}
				case 3:
				{
					from.CloseGump( typeof( LoggingGumpCrier ) );
					from.SendGump( new LoggingGumpCrier( from, 4 ) );
					break;
				}
				case 4:
				{
					from.CloseGump( typeof( LoggingGumpCrier ) );
					from.SendGump( new LoggingGumpCrier( from, 5 ) );
					break;
				}
				case 5:
				{
					from.CloseGump( typeof( LoggingGumpCrier ) );
					from.SendGump( new LoggingGumpCrier( from, 6 ) );
					break;
				}
				case 6:
				{
					from.CloseGump( typeof( LoggingGumpCrier ) );
					from.SendGump( new LoggingGumpCrier( from, 7 ) );
					break;
				}
			}
		}
    }
}