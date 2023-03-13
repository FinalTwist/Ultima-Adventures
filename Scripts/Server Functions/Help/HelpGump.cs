using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Menus;
using Server.Menus.Questions;
using Server.Accounting;
using Server.Multis;
using Server.Mobiles;
using Server.Regions;
using System.Collections;
using System.Collections.Generic;
using Server.Commands;
using Server.Misc;
using Server.Items;
using System.Globalization;

namespace Server.Engines.Help
{
	public class ContainedMenu : QuestionMenu
	{
		private Mobile m_From;

		public ContainedMenu( Mobile from ) : base( "You already have an open help request. We will have someone assist you as soon as possible.  What would you like to do?", new string[]{ "Leave my old help request like it is.", "Remove my help request from the queue." } )
		{
			m_From = from;
		}

		public override void OnCancel( NetState state )
		{
			m_From.SendLocalizedMessage( 1005306, "", 0x35 ); // Help request unchanged.
		}

		public override void OnResponse( NetState state, int index )
		{
			m_From.SendSound( 0x4A );
			if ( index == 0 )
			{
				m_From.SendLocalizedMessage( 1005306, "", 0x35 ); // Help request unchanged.
			}
			else if ( index == 1 )
			{
				PageEntry entry = PageQueue.GetEntry( m_From );

				if ( entry != null && entry.Handler == null )
				{
					m_From.SendLocalizedMessage( 1005307, "", 0x35 ); // Removed help request.
					entry.AddResponse( entry.Sender, "[Canceled]" );
					PageQueue.Remove( entry );
				}
				else
				{
					m_From.SendLocalizedMessage( 1005306, "", 0x35 ); // Help request unchanged.
				}
			}
		}
	}

	public class HelpGump : Gump
	{
		public static void Initialize()
		{
			EventSink.HelpRequest += new HelpRequestEventHandler( EventSink_HelpRequest );
		}

		private static void EventSink_HelpRequest( HelpRequestEventArgs e )
		{
			foreach ( Gump g in e.Mobile.NetState.Gumps )
			{
				if ( g is HelpGump )
					return;
			}

			if ( !PageQueue.CheckAllowedToPage( e.Mobile ) )
				return;

			if ( PageQueue.Contains( e.Mobile ) )
				e.Mobile.SendMenu( new ContainedMenu( e.Mobile ) );
			else
				e.Mobile.SendGump( new HelpGump( e.Mobile, 1 ) );
		}

		private static bool IsYoung( Mobile m )
		{
			if ( m is PlayerMobile )
				return ((PlayerMobile)m).Young;

			return false;
		}

		public static bool CheckCombat( Mobile m )
		{
			for ( int i = 0; i < m.Aggressed.Count; ++i )
			{
				AggressorInfo info = m.Aggressed[i];

				if ( DateTime.UtcNow - info.LastCombatTime < TimeSpan.FromSeconds( 30.0 ) )
					return true;
			}

			return false;
		}

		public HelpGump( Mobile from, int page ) : base( 25, 25 )
		{
			string HelpText = MyHelp();

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 153);
			AddImage(300, 0, 153);
			AddImage(600, 0, 153);
			AddImage(900, 0, 153);
			AddImage(0, 300, 153);
			AddImage(300, 300, 153);
			AddImage(600, 300, 153);
			AddImage(900, 300, 153);
			AddImage(2, 2, 129);
			AddImage(300, 2, 129);
			AddImage(600, 2, 129);
			AddImage(898, 2, 129);
			AddImage(2, 298, 129);
			AddImage(300, 298, 129);
			AddImage(600, 298, 129);
			AddImage(898, 298, 129);
			AddImage(0, 600, 153);
			AddImage(300, 600, 153);
			AddImage(600, 600, 153);
			AddImage(900, 600, 153);
			AddImage(2, 598, 129);
			AddImage(300, 598, 129);
			AddImage(600, 598, 129);
			AddImage(898, 598, 129);
			AddImage(6, 7, 145);
			AddImage(8, 664, 128);
			AddImage(962, 8, 138);
			AddImage(9, 333, 137);
			AddImage(853, 419, 144);
			AddImage(773, 399, 129);
			AddImage(52, 516, 156);
			AddImage(1052, 549, 156);
			AddImage(662, 47, 132);
			AddImage(410, 835, 130);
			AddImage(548, 794, 134);
			AddImage(784, 863, 140);
			AddImage(868, 654, 147);
			AddImage(754, 865, 159);
			AddImage(368, 47, 132);
			AddImage(212, 47, 132);
			AddImage(170, 8, 139);
			AddImage(166, 7, 156);
			AddImage(204, 9, 156);
			AddImage(184, 9, 156);

			AddImage(282, 159, 161);
			AddImage(282, 439, 161);
			AddImage(284, 143, 158);
			AddImage(283, 719, 157);

			int MainAdd = 32;
			int MainSet = 181;

			AddHtml( 181, 95, 618, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>ULTIMA ODYSSEY - HELP OPTIONS</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			string colorMenu = "#FCFF00";

			AddButton(100, 146, 4005, 4005, 1, GumpButtonType.Reply, 0);
				colorMenu = "#FCFF00";
				if ( page == 1 )
				{
					colorMenu = "#FFA200";
					AddHtml( 316, 177, 755, 536, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + HelpText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
				}
				AddHtml( 141, 146, 150, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>Main</BIG></BASEFONT></BODY>", (bool)false, (bool)false);


				int button_afk = 3609;
				HelpText = "Your 'Away From Keyboard' Settings Are Disabled.";
				colorMenu = "#FCFF00";
				if ( page == 2 )
				{
					colorMenu = "#FFA200";
					if ( Server.Commands.AFK.m_AFK.Contains( from.Serial.Value ) ){ button_afk = 4018; HelpText = "Your 'Away From Keyboard' Settings Are Enabled."; }
					AddHtml( 316, 177, 755, 536, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + HelpText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
				}
			AddButton(100, MainSet, button_afk, button_afk, 2, GumpButtonType.Reply, 0);
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>AFK</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			MainSet += MainAdd;

			AddButton(100, MainSet, 4005, 4005, 3, GumpButtonType.Reply, 0);
				colorMenu = "#FCFF00"; if ( page == 3 ){ colorMenu = "#FFA200"; }
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>Chat</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			MainSet += MainAdd;

			AddButton(100, MainSet, 4005, 4005, 4, GumpButtonType.Reply, 0);
				colorMenu = "#FCFF00";
				if ( page == 4 )
				{
					colorMenu = "#FFA200";
					AddHtml( 316, 177, 755, 536, @"<BODY><BASEFONT Color=#FCFF00><BIG>Your empty corpses have been removed.</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
				}
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>Corpse Clear</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			MainSet += MainAdd;

			AddButton(100, MainSet, 4005, 4005, 5, GumpButtonType.Reply, 0);
				colorMenu = "#FCFF00"; if ( page == 5 ){ colorMenu = "#FFA200"; }
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>Corpse Search</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			MainSet += MainAdd;

			AddButton(100, MainSet, 4005, 4005, 6, GumpButtonType.Reply, 0);
				colorMenu = "#FCFF00"; if ( page == 6 ){ colorMenu = "#FFA200"; }
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>Emote</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			MainSet += MainAdd;

			AddButton(100, MainSet, 4005, 4005, 7, GumpButtonType.Reply, 0);
				colorMenu = "#FCFF00";
				if ( page == 7 )
				{
					colorMenu = "#FFA200";

					AddButton(310, 155, 4005, 4005, 66, GumpButtonType.Reply, 0);
					AddButton(350, 155, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 155, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Bard Songs Bar I</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 155, 4005, 4005, 266, GumpButtonType.Reply, 0);
						AddHtml( 675, 155, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 155, 4017, 4017, 366, GumpButtonType.Reply, 0);
						AddHtml( 905, 155, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(310, 190, 4005, 4005, 67, GumpButtonType.Reply, 0);
					AddButton(350, 190, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 190, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Bard Songs Bar II</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 190, 4005, 4005, 267, GumpButtonType.Reply, 0);
						AddHtml( 675, 190, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 190, 4017, 4017, 367, GumpButtonType.Reply, 0);
						AddHtml( 905, 190, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(310, 225, 4005, 4005, 68, GumpButtonType.Reply, 0);
					AddButton(350, 225, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 225, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Chivalry Spell Bar I</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 225, 4005, 4005, 268, GumpButtonType.Reply, 0);
						AddHtml( 675, 225, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 225, 4017, 4017, 368, GumpButtonType.Reply, 0);
						AddHtml( 905, 225, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(310, 260, 4005, 4005, 69, GumpButtonType.Reply, 0);
					AddButton(350, 260, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 260, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Chivalry Spell Bar II</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 260, 4005, 4005, 269, GumpButtonType.Reply, 0);
						AddHtml( 675, 260, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 260, 4017, 4017, 369, GumpButtonType.Reply, 0);
						AddHtml( 905, 260, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(310, 295, 4005, 4005, 70, GumpButtonType.Reply, 0);
					AddButton(350, 295, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 295, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Death Knight Spell Bar I</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 295, 4005, 4005, 270, GumpButtonType.Reply, 0);
						AddHtml( 675, 295, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 295, 4017, 4017, 370, GumpButtonType.Reply, 0);
						AddHtml( 905, 295, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(310, 330, 4005, 4005, 71, GumpButtonType.Reply, 0);
					AddButton(350, 330, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 330, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Death Knight Spell Bar II</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 330, 4005, 4005, 271, GumpButtonType.Reply, 0);
						AddHtml( 675, 330, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 330, 4017, 4017, 371, GumpButtonType.Reply, 0);
						AddHtml( 905, 330, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(310, 365, 4005, 4005, 72, GumpButtonType.Reply, 0);
					AddButton(350, 365, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 365, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Magery Spell Bar I</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 365, 4005, 4005, 272, GumpButtonType.Reply, 0);
						AddHtml( 675, 365, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 365, 4017, 4017, 372, GumpButtonType.Reply, 0);
						AddHtml( 905, 365, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(310, 400, 4005, 4005, 73, GumpButtonType.Reply, 0);
					AddButton(350, 400, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 400, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Magery Spell Bar II</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 400, 4005, 4005, 273, GumpButtonType.Reply, 0);
						AddHtml( 675, 400, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 400, 4017, 4017, 373, GumpButtonType.Reply, 0);
						AddHtml( 905, 400, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(310, 435, 4005, 4005, 74, GumpButtonType.Reply, 0);
					AddButton(350, 435, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 435, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Magery Spell Bar III</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 435, 4005, 4005, 274, GumpButtonType.Reply, 0);
						AddHtml( 675, 435, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 435, 4017, 4017, 374, GumpButtonType.Reply, 0);
						AddHtml( 905, 435, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(310, 470, 4005, 4005, 75, GumpButtonType.Reply, 0);
					AddButton(350, 470, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 470, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Magery Spell Bar IV</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 470, 4005, 4005, 275, GumpButtonType.Reply, 0);
						AddHtml( 675, 470, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 470, 4017, 4017, 375, GumpButtonType.Reply, 0);
						AddHtml( 905, 470, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(310, 505, 4005, 4005, 980, GumpButtonType.Reply, 0);
					AddButton(350, 505, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 505, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Monk Ability Bar I</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 505, 4005, 4005, 280, GumpButtonType.Reply, 0);
						AddHtml( 675, 505, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 505, 4017, 4017, 380, GumpButtonType.Reply, 0);
						AddHtml( 905, 505, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(310, 540, 4005, 4005, 981, GumpButtonType.Reply, 0);
					AddButton(350, 540, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 540, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Monk Ability Bar II</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 540, 4005, 4005, 281, GumpButtonType.Reply, 0);
						AddHtml( 675, 540, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 540, 4017, 4017, 381, GumpButtonType.Reply, 0);
						AddHtml( 905, 540, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(310, 575, 4005, 4005, 76, GumpButtonType.Reply, 0);
					AddButton(350, 575, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 575, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Necromancer Spell Bar I</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 575, 4005, 4005, 276, GumpButtonType.Reply, 0);
						AddHtml( 675, 575, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 575, 4017, 4017, 376, GumpButtonType.Reply, 0);
						AddHtml( 905, 575, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(310, 610, 4005, 4005, 77, GumpButtonType.Reply, 0);
					AddButton(350, 610, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 610, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Necromancer Spell Bar II</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 610, 4005, 4005, 277, GumpButtonType.Reply, 0);
						AddHtml( 675, 610, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 610, 4017, 4017, 377, GumpButtonType.Reply, 0);
						AddHtml( 905, 610, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(310, 645, 4005, 4005, 78, GumpButtonType.Reply, 0);
					AddButton(350, 645, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 645, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Priest Prayers Bar I</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 645, 4005, 4005, 278, GumpButtonType.Reply, 0);
						AddHtml( 675, 645, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 645, 4017, 4017, 378, GumpButtonType.Reply, 0);
						AddHtml( 905, 645, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(310, 680, 4005, 4005, 79, GumpButtonType.Reply, 0);
					AddButton(350, 680, 4011, 4011, 95, GumpButtonType.Reply, 0);
					AddHtml( 390, 680, 300, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Priest Prayers Bar II</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(635, 680, 4005, 4005, 279, GumpButtonType.Reply, 0);
						AddHtml( 675, 680, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Open Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddButton(865, 680, 4017, 4017, 379, GumpButtonType.Reply, 0);
						AddHtml( 905, 680, 110, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Close Toolbar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>Magic Toolbars</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			MainSet += MainAdd;

			AddButton(100, MainSet, 4005, 4005, 8, GumpButtonType.Reply, 0);
				colorMenu = "#FCFF00"; if ( page == 8 ){ colorMenu = "#FFA200"; }
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>Moongate Search</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			MainSet += MainAdd;

			AddButton(100, MainSet, 4005, 4005, 9, GumpButtonType.Reply, 0);
				colorMenu = "#FCFF00"; if ( page == 9 ){ colorMenu = "#FFA200"; }
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>MOTD</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			MainSet += MainAdd;

			AddButton(100, MainSet, 4005, 4005, 10, GumpButtonType.Reply, 0);
				colorMenu = "#FCFF00";
				if ( page == 10 )
				{
					colorMenu = "#FFA200";

					AddHtml( 316, 158, 755, 377, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + MyQuests( from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
					AddHtml( 316, 556, 726, 217, @"<BODY><BASEFONT Color=#FFA200><BIG>Throughout your journey, you may come across particular events that appear in your quest log. They may be a simple achievement of finding a strange land, or they may reference an item you must find. Quests are handled in a 'virtual' manner. What this means is that any achievements are real, but any references to items found are not. If your quest log states that you found an ebony key, you will not have an ebony key in your backpack...but you will 'virtually' have the item. The quest will keep track of this fact for you. Because of this, you will never lose that ebony key and it remains unique to your character's questing. The quest knows you found it and have it. You may be tasked to find an item in a dungeon. When there is an indication you found it, it will be 'virtually' in your possession. You will often hear a sound of victory when a quest event is reached, along with a message about it. You still may miss it, however. So check your quest log from time to time. One way to get quests is to visit taverns or inns. If you see a bulletin board called 'Seeking Brave Adventurers', single click on it to begin your life questing for fame and fortune.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>Quests</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			MainSet += MainAdd;

			AddButton(100, MainSet, 4005, 4005, 11, GumpButtonType.Reply, 0);
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Quick Bar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			MainSet += MainAdd;

			AddButton(100, MainSet, 4005, 4005, 12, GumpButtonType.Reply, 0);
				colorMenu = "#FCFF00";
				if ( page == 12 )
				{
					int setB = 3609;
					int adjs = -4;
					int adjm = -4;
					colorMenu = "#FFA200";

					CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );

					// ------------------------------------------------------------------------------------------------------------------------------

					if ( DB.CharacterWepAbNames == 1 ){ setB = 4018; } else { setB = 3609; }
					AddButton(310, 155+adjs, setB, setB, 51, GumpButtonType.Reply, 0);
					AddButton(350, 155+adjs, 4011, 4011, 99, GumpButtonType.Reply, 0);
					AddHtml( 390, 155+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Ability Names</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					if ( DB.CharacterSheath == 1 ){ setB = 4018; } else { setB = 3609; }
					AddButton(310, 190+adjs, setB, setB, 52, GumpButtonType.Reply, 0);
					AddButton(350, 190+adjs, 4011, 4011, 100, GumpButtonType.Reply, 0);
					AddHtml( 390, 190+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Auto Sheath</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					if ( DB.ClassicPoisoning == 1 ){ setB = 4018; } else { setB = 3609; }
					AddButton(310, 225+adjs, setB, setB, 64, GumpButtonType.Reply, 0);
					AddButton(350, 225+adjs, 4011, 4011, 86, GumpButtonType.Reply, 0);
					AddHtml( 390, 225+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Classic Poisoning</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					if ( DB.Hue == 2 ){ setB = 4018; } else { setB = 3609; }
					AddButton(310, 260+adjs, setB, setB, 80, GumpButtonType.Reply, 0);
					AddButton(350, 260+adjs, 4011, 4011, 97, GumpButtonType.Reply, 0);
					AddHtml( 390, 260+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Container Fix</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					if ( DB.Hue == 3 ){ setB = 4018; } else { setB = 3609; }
					AddButton(310, 295+adjs, setB, setB, 81, GumpButtonType.Reply, 0);
					AddButton(350, 295+adjs, 4011, 4011, 98, GumpButtonType.Reply, 0);
					AddHtml( 390, 295+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Large Containers</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					AddButton(310, 330+adjs, 4005, 4005, 55, GumpButtonType.Reply, 0);
					AddButton(350, 330+adjs, 4011, 4011, 85, GumpButtonType.Reply, 0);
					AddHtml( 390, 330+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Loot Options</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					AddButton(310, 365+adjs, 4005, 4005, 65, GumpButtonType.Reply, 0);
					AddButton(350, 365+adjs, 4011, 4011, 83, GumpButtonType.Reply, 0);
					AddHtml( 390, 365+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Music Playlist</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					if ( DB.CharMusical == "Forest" ){ setB = 4018; } else { setB = 3609; }
					AddButton(310, 400+adjs, setB, setB, 53, GumpButtonType.Reply, 0);
					AddButton(350, 400+adjs, 4011, 4011, 82, GumpButtonType.Reply, 0);
					AddHtml( 390, 400+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Music Tone</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					/*if ( ((PlayerMobile)from).WimpPlayer == false ){ setB = 4018; } else { */ setB = 3609; //}
					AddButton(310, 435+adjs, setB, setB, 54, GumpButtonType.Reply, 0);
					AddButton(350, 435+adjs, 4011, 4011, 84, GumpButtonType.Reply, 0);
					AddHtml( 390, 435+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Private</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					AddButton(310, 470+adjs, 4005, 4005, 56, GumpButtonType.Reply, 0);
					AddButton(350, 470+adjs, 4011, 4011, 87, GumpButtonType.Reply, 0);
					AddHtml( 390, 470+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Skill Title</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					string skillLocks = "Skill List (Show Up)"; 
					if ( DB.SkillDisplay == 1 ){ setB = 4018; skillLocks = "Skill List (Show Up and Locked)"; } else { setB = 4017; }
					AddButton(310, 505+adjs, setB, setB, 982, GumpButtonType.Reply, 0);
					AddButton(350, 505+adjs, 4011, 4011, 199, GumpButtonType.Reply, 0);
					AddHtml( 390, 505+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>" + skillLocks + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					// ------------------------------------------------------------------------------------------------------------------------------

					AddButton(310, 540+adjs, 4005, 4005, 63, GumpButtonType.Reply, 0);
					AddButton(350, 540+adjs, 4011, 4011, 88, GumpButtonType.Reply, 0);
					AddHtml( 390, 540+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Reagent Bar - Alchemy</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					AddButton(310, 575+adjs, 4005, 4005, 60, GumpButtonType.Reply, 0);
					AddButton(350, 575+adjs, 4011, 4011, 89, GumpButtonType.Reply, 0);
					AddHtml( 390, 575+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Reagent Bar - Magery</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					AddButton(310, 610+adjs, 4005, 4005, 61, GumpButtonType.Reply, 0);
					AddButton(350, 610+adjs, 4011, 4011, 90, GumpButtonType.Reply, 0);
					AddHtml( 390, 610+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Reagent Bar - Necromancy</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					AddButton(310, 645+adjs, 4005, 4005, 62, GumpButtonType.Reply, 0);
					AddButton(350, 645+adjs, 4011, 4011, 91, GumpButtonType.Reply, 0);
					AddHtml( 390, 645+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Reagent Bar - Close All</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					// ------------------------------------------------------------------------------------------------------------------------------

					if ( DB.CharacterEvil == 0 && DB.CharacterOriental == 0 && DB.CharacterBarbaric == 0 ){ setB = 4018; } else { setB = 3609; }
					AddButton(310, 680+adjs, setB, setB, 57, GumpButtonType.Reply, 0);
					AddButton(350, 680+adjs, 4011, 4011, 92, GumpButtonType.Reply, 0);
					AddHtml( 390, 680+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Play Style - Normal</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					if ( DB.CharacterEvil == 1 ){ setB = 4018; } else { setB = 3609; }
					AddButton(310, 715+adjs, setB, setB, 58, GumpButtonType.Reply, 0);
					AddButton(350, 715+adjs, 4011, 4011, 93, GumpButtonType.Reply, 0);
					AddHtml( 390, 715+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Play Style - Evil</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					if ( DB.CharacterOriental == 1 ){ setB = 4018; } else { setB = 3609; }
					AddButton(310, 750+adjs, setB, setB, 59, GumpButtonType.Reply, 0);
					AddButton(350, 750+adjs, 4011, 4011, 94, GumpButtonType.Reply, 0);
					AddHtml( 390, 750+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Play Style - Oriental</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					string amazon = "";
					if ( DB.CharacterBarbaric == 1 ){ setB = 4018; } 
					else if ( DB.CharacterBarbaric == 2 ){ setB = 4003; amazon = " with Amazon Fighting Titles"; } 
					else { setB = 3609; }
					AddButton(310, 785+adjs, setB, setB, 984, GumpButtonType.Reply, 0);
					AddButton(350, 785+adjs, 4011, 4011, 198, GumpButtonType.Reply, 0);
					AddHtml( 390, 785+adjs, 300, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Play Style - Barbaric" + amazon + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					// ------------------------------------------------------------------------------------------------------------------------------

					AddButton(350, 820+adjs, 4011, 4011, 96, GumpButtonType.Reply, 0);
					AddHtml( 390, 820+adjs, 160, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Magery Spell Color</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					if ( DB.MagerySpellHue == 0x47E ){ setB = 4018; } else { setB = 3609; }
					AddButton(570, 820+adjs, setB, setB, 500, GumpButtonType.Reply, 0);
					AddHtml( 610, 820+adjs, 64, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>White</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					if ( DB.MagerySpellHue == 0x94E ){ setB = 4018; } else { setB = 3609; }
					AddButton(700, 820+adjs, setB, setB, 501, GumpButtonType.Reply, 0);
					AddHtml( 740, 820+adjs, 64, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Black</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					if ( DB.MagerySpellHue == 0x48D ){ setB = 4018; } else { setB = 3609; }
					AddButton(830, 820+adjs, setB, setB, 502, GumpButtonType.Reply, 0);
					AddHtml( 870, 820+adjs, 64, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Blue</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					if ( DB.MagerySpellHue == 0x48E ){ setB = 4018; } else { setB = 3609; }
					AddButton(960, 820+adjs, setB, setB, 503, GumpButtonType.Reply, 0);
					AddHtml( 1000, 820+adjs, 64, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Red</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					adjs = adjs + adjm;

					if ( DB.MagerySpellHue == 0x48F ){ setB = 4018; } else { setB = 3609; }
					AddButton(570, 855+adjs, setB, setB, 504, GumpButtonType.Reply, 0);
					AddHtml( 610, 855+adjs, 64, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Green</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					if ( DB.MagerySpellHue == 0x490 ){ setB = 4018; } else { setB = 3609; }
					AddButton(700, 855+adjs, setB, setB, 505, GumpButtonType.Reply, 0);
					AddHtml( 740, 855+adjs, 64, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Purple</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					if ( DB.MagerySpellHue == 0x491 ){ setB = 4018; } else { setB = 3609; }
					AddButton(830, 855+adjs, setB, setB, 506, GumpButtonType.Reply, 0);
					AddHtml( 870, 855+adjs, 64, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Yellow</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			
					if ( DB.MagerySpellHue == 0 ){ setB = 4018; } else { setB = 3609; }
					AddButton(960, 855+adjs, setB, setB, 507, GumpButtonType.Reply, 0);
					AddHtml( 1000, 855+adjs, 64, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Default</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>Settings</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			MainSet += MainAdd;

			AddButton(100, MainSet, 4005, 4005, 13, GumpButtonType.Reply, 0);
				colorMenu = "#FCFF00"; if ( page == 13 ){ colorMenu = "#FFA200"; }
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>Skills</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			MainSet += MainAdd;

			AddButton(100, MainSet, 4005, 4005, 983, GumpButtonType.Reply, 0);
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Skill List</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			MainSet += MainAdd;

			AddButton(100, MainSet, 4005, 4005, 14, GumpButtonType.Reply, 0);
				colorMenu = "#FCFF00"; if ( page == 14 ){ colorMenu = "#FFA200"; }
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>Statistics</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			MainSet += MainAdd;

			bool house = false;
			if ( from.Region is HouseRegion )
			    if (((HouseRegion)from.Region).House.IsOwner(from))
					house = true;
			if ( from.Region.GetLogoutDelay( from ) != TimeSpan.Zero && house == false )
			{
				AddButton(100, MainSet, 4005, 4005, 15, GumpButtonType.Reply, 0);
				colorMenu = "#FCFF00"; if ( page == 15 ){ colorMenu = "#FFA200"; }
					AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>Stuck in World</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}

			MainSet += MainAdd;

			AddButton(100, MainSet, 4005, 4005, 17, GumpButtonType.Reply, 0);
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Wealth Bar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			MainSet += MainAdd;

			AddButton(100, MainSet, 4005, 4005, 16, GumpButtonType.Reply, 0);
				colorMenu = "#FCFF00"; if ( page == 16 ){ colorMenu = "#FFA200"; }
				AddHtml( 141, MainSet, 150, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>Weapon Abilities</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
		}

        public void InvokeCommand( string c, Mobile from )
        {
            CommandSystem.Handle(from, String.Format("{0}{1}", CommandSystem.Prefix, c));
        }

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			from.SendSound( 0x4A ); 

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );

			PageType type = (PageType)(-1);

			from.CloseGump( typeof(Server.Engines.Help.HelpGump) );

			if ( info.ButtonID > 81 && info.ButtonID < 200 ) // SMALL INFO HELP WINDOWS
			{
				from.CloseGump( typeof( InfoHelpGump ) );
				from.SendGump( new InfoHelpGump( info.ButtonID, 12 ) );
			}
			if ( info.ButtonID >= 200 && info.ButtonID <= 400 ) // MAGIC BARS OPEN AND CLOSE
			{
				from.SendGump( new Server.Engines.Help.HelpGump( from, 7 ) );

				if ( info.ButtonID == 266 ){ InvokeCommand( "bardtool1", from ); }
				else if ( info.ButtonID == 366 ){ InvokeCommand( "bardclose1", from ); }
				else if ( info.ButtonID == 267 ){ InvokeCommand( "bardtool2", from ); }
				else if ( info.ButtonID == 367 ){ InvokeCommand( "bardclose2", from ); }
				else if ( info.ButtonID == 268 ){ InvokeCommand( "chivalrytool1", from ); }
				else if ( info.ButtonID == 368 ){ InvokeCommand( "chivalryclose1", from ); }
				else if ( info.ButtonID == 269 ){ InvokeCommand( "chivalrytool2", from ); }
				else if ( info.ButtonID == 369 ){ InvokeCommand( "chivalryclose2", from ); }
				else if ( info.ButtonID == 270 ){ InvokeCommand( "deathtool1", from ); }
				else if ( info.ButtonID == 370 ){ InvokeCommand( "deathclose1", from ); }
				else if ( info.ButtonID == 271 ){ InvokeCommand( "deathtool2", from ); }
				else if ( info.ButtonID == 371 ){ InvokeCommand( "deathclose2", from ); }
				else if ( info.ButtonID == 272 ){ InvokeCommand( "magetool1", from ); }
				else if ( info.ButtonID == 372 ){ InvokeCommand( "mageclose1", from ); }
				else if ( info.ButtonID == 273 ){ InvokeCommand( "magetool2", from ); }
				else if ( info.ButtonID == 373 ){ InvokeCommand( "mageclose2", from ); }
				else if ( info.ButtonID == 274 ){ InvokeCommand( "magetool3", from ); }
				else if ( info.ButtonID == 374 ){ InvokeCommand( "mageclose3", from ); }
				else if ( info.ButtonID == 275 ){ InvokeCommand( "magetool4", from ); }
				else if ( info.ButtonID == 375 ){ InvokeCommand( "mageclose4", from ); }
				else if ( info.ButtonID == 276 ){ InvokeCommand( "necrotool1", from ); }
				else if ( info.ButtonID == 376 ){ InvokeCommand( "necroclose1", from ); }
				else if ( info.ButtonID == 277 ){ InvokeCommand( "necrotool2", from ); }
				else if ( info.ButtonID == 377 ){ InvokeCommand( "necroclose2", from ); }
				else if ( info.ButtonID == 278 ){ InvokeCommand( "holytool1", from ); }
				else if ( info.ButtonID == 378 ){ InvokeCommand( "holyclose1", from ); }
				else if ( info.ButtonID == 279 ){ InvokeCommand( "holytool2", from ); }
				else if ( info.ButtonID == 379 ){ InvokeCommand( "holyclose2", from ); }
				else if ( info.ButtonID == 280 ){ InvokeCommand( "monktool1", from ); }
				else if ( info.ButtonID == 380 ){ InvokeCommand( "monkclose1", from ); }
				else if ( info.ButtonID == 281 ){ InvokeCommand( "monktool2", from ); }
				else if ( info.ButtonID == 381 ){ InvokeCommand( "monkclose2", from ); }
			}
			else
			{
				switch ( info.ButtonID )
				{
					case 0: // Close/Cancel
					{
						//from.SendLocalizedMessage( 501235, "", 0x35 ); // Help request aborted.
						break;
					}
					case 1: // MAIN
					{
						from.SendGump( new Server.Engines.Help.HelpGump( from, info.ButtonID ) );
						break;
					}
					case 2: // AFK
					{
						InvokeCommand( "afk", from );
						from.SendGump( new Server.Engines.Help.HelpGump( from, info.ButtonID ) );
						break;
					}
					case 3: // Chat
					{
						InvokeCommand( "c", from );
						break;
					}
					case 4: // Corpse Clear
					{
						InvokeCommand( "corpseclear", from );
						from.SendGump( new Server.Engines.Help.HelpGump( from, info.ButtonID ) );
						break;
					}
					case 5: // Corpse Search
					{
						InvokeCommand( "corpse", from );
						break;
					}
					case 6: // Emote
					{
						InvokeCommand( "emote", from );
						break;
					}
					case 7: // Magic
					{
						from.SendGump( new Server.Engines.Help.HelpGump( from, 7 ) );
						break;
					}
					case 8: // Moongate
					{
						InvokeCommand( "magicgate", from );
						break;
					}
					case 9: // MOTD
					{
						from.CloseGump( typeof( Joeku.MOTD.MOTD_Gump ) );
						Joeku.MOTD.MOTD_Utility.SendGump( from, false, 0, 1 );
						break;
					}
					case 10: // Quests
					{
						from.SendGump( new Server.Engines.Help.HelpGump( from, info.ButtonID ) );
						break;
					}
					case 11: // Quick Bar
					{
						from.CloseGump( typeof( QuickBar ) );
						from.SendGump( new QuickBar( from ) );
						break;
					}
					case 12: // Settings
					{
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 13: // Skills
					{
						from.CloseGump( typeof( NewSkillsGump ) );
						from.SendGump( new NewSkillsGump( 1 ) );
						break;
					}
					case 14: // Statistics
					{
						from.CloseGump( typeof( Server.Statistics.StatisticsGump ) );
						from.SendGump( new Server.Statistics.StatisticsGump( 1 ) );
						break;
					}
					case 15: // Stuck
					{
						BaseHouse house = BaseHouse.FindHouseAt( from );

						if ( house != null && house.IsAosRules )
						{
							from.Location = house.BanLocation;
						}
						else if ( from.Region.IsPartOf( typeof( Server.Regions.Jail ) ) )
						{
							from.SendLocalizedMessage( 1041530, "", 0x35 ); // You'll need a better jailbreak plan then that!
						}
						else if ( from.CanUseStuckMenu() && from.Region.CanUseStuckMenu( from ) && !CheckCombat( from ) && !from.Frozen && !from.Criminal && (Core.AOS || from.Kills < 5) )
						{
							StuckMenu menu = new StuckMenu( from, from, true );

							menu.BeginClose();

							from.SendGump( menu );
						}
						else
						{
							type = PageType.Stuck;
						}

						break;
					}
					case 16: // Weapon Abilities
					{
						InvokeCommand( "sad", from );
						break;
					}
					case 17: // Wealth Bar
					{
						from.CloseGump( typeof( WealthBar ) );
						from.SendGump( new WealthBar( from ) );
						break;
					}
					case 51: // Weapon Ability Names
					{
						if ( DB.CharacterWepAbNames != 1 )
						{
							DB.CharacterWepAbNames = 1;
						}
						else
						{
							DB.CharacterWepAbNames = 0;
						}
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 52: // Auto Sheathe
					{
						if ( DB.CharacterSheath == 1 )
						{
							DB.CharacterSheath = 0;
						}
						else
						{
							DB.CharacterSheath = 1;
						}
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 53: // Musical
					{
						string tunes = DB.CharMusical;

						if ( tunes == "Forest" )
						{
							DB.CharMusical = "Dungeon";
						}
						else
						{
							DB.CharMusical = "Forest";
						}
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 54: // Private
					{
						/*
						PlayerMobile pm = (PlayerMobile)from;

						if ( pm.WimpPlayer == false )
						{
							pm.WimpPlayer = true;
						}
						else
						{
							pm.WimpPlayer = false;
						}
						*/
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 55: // Loot
					{
						from.CloseGump( typeof( LootChoices ) );
						from.SendGump( new LootChoices( from, 1 ) );
						break;
					}
					case 56: // Skill Titles
					{
						from.CloseGump( typeof( SkillTitleGump ) );
						from.SendGump( new SkillTitleGump( from ) );
						break;
					}
					case 982: // Skill List
					{
						if ( DB.SkillDisplay > 0 ){ DB.SkillDisplay = 0; } else { DB.SkillDisplay = 1; }
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						Server.Gumps.SkillListingGump.RefreshSkillList( from );
						break;
					}
					case 983: // Open Skill List
					{
						Server.Gumps.SkillListingGump.OpenSkillList( from );
						break;
					}
					case 57: // Normal Play
					{
						DB.CharacterEvil = 0;
						DB.CharacterOriental = 0;
						DB.CharacterBarbaric = 0;
						Server.Items.BarbaricSatchel.GetRidOf( from );
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 58: // Evil Play
					{
						DB.CharacterEvil = 1;
						DB.CharacterOriental = 0;
						DB.CharacterBarbaric = 0;
						Server.Items.BarbaricSatchel.GetRidOf( from );
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 59: // Oriental Play
					{
						DB.CharacterEvil = 0;
						DB.CharacterOriental = 1;
						DB.CharacterBarbaric = 0;
						Server.Items.BarbaricSatchel.GetRidOf( from );
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 984: // Barbaric Play
					{
						if ( DB.CharacterBarbaric == 1 && from.Female )
						{
							DB.CharacterBarbaric = 2;
						}
						else if ( DB.CharacterBarbaric > 0 )
						{
							DB.CharacterBarbaric = 0;
							Server.Items.BarbaricSatchel.GetRidOf( from );
						}
						else
						{
							DB.CharacterEvil = 0;
							DB.CharacterOriental = 0;
							DB.CharacterBarbaric = 1;
							Server.Items.BarbaricSatchel.GivePack( from );
						}
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 60: // Mage Reagents
					{
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						InvokeCommand( "mreagents", from );
						break;
					}
					case 61: // Necromancer Reagents
					{
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						InvokeCommand( "nreagents", from );
						break;
					}
					case 62: // Close Reagents
					{
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						InvokeCommand( "creagents", from );
						break;
					}
					case 63: // Alchemy Reagents
					{
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						InvokeCommand( "areagents", from );
						break;
					}
					case 64: // Poisoning
					{
						if ( DB.ClassicPoisoning == 1 )
						{
							DB.ClassicPoisoning = 0;
						}
						else
						{
							DB.ClassicPoisoning = 1;
						}
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 65: // Music Playlist
					{
						from.CloseGump( typeof( MusicPlaylist ) );
						from.SendGump( new MusicPlaylist( from ) );
						break;
					}
					case 66: // SPELL BARS BELOW ---------------------------------------
					{
						from.CloseGump( typeof( SetupBarsBard1 ) );
						from.SendGump( new SetupBarsBard1( from, 1 ) );
						break;
					}
					case 67:
					{
						from.CloseGump( typeof( SetupBarsBard2 ) );
						from.SendGump( new SetupBarsBard2( from, 1 ) );
						break;
					}
					case 68:
					{
						from.CloseGump( typeof( SetupBarsChivalry1 ) );
						from.SendGump( new SetupBarsChivalry1( from, 1 ) );
						break;
					}
					case 69:
					{
						from.CloseGump( typeof( SetupBarsChivalry2 ) );
						from.SendGump( new SetupBarsChivalry2( from, 1 ) );
						break;
					}
					case 70:
					{
						from.CloseGump( typeof( SetupBarsDeath1 ) );
						from.SendGump( new SetupBarsDeath1( from, 1 ) );
						break;
					}
					case 71:
					{
						from.CloseGump( typeof( SetupBarsDeath2 ) );
						from.SendGump( new SetupBarsDeath2( from, 1 ) );
						break;
					}
					case 72:
					{
						from.CloseGump( typeof( SetupBarsMage1 ) );
						from.SendGump( new SetupBarsMage1( from, 1 ) );
						break;
					}
					case 73:
					{
						from.CloseGump( typeof( SetupBarsMage2 ) );
						from.SendGump( new SetupBarsMage2( from, 1 ) );
						break;
					}
					case 74:
					{
						from.CloseGump( typeof( SetupBarsMage3 ) );
						from.SendGump( new SetupBarsMage3( from, 1 ) );
						break;
					}
					case 75:
					{
						from.CloseGump( typeof( SetupBarsMage4 ) );
						from.SendGump( new SetupBarsMage4( from, 1 ) );
						break;
					}
					case 76:
					{
						from.CloseGump( typeof( SetupBarsNecro1 ) );
						from.SendGump( new SetupBarsNecro1( from, 1 ) );
						break;
					}
					case 77:
					{
						from.CloseGump( typeof( SetupBarsNecro2 ) );
						from.SendGump( new SetupBarsNecro2( from, 1 ) );
						break;
					}
					case 78:
					{
						from.CloseGump( typeof( SetupBarsPriest1 ) );
						from.SendGump( new SetupBarsPriest1( from, 1 ) );
						break;
					}
					case 79:
					{
						from.CloseGump( typeof( SetupBarsPriest2 ) );
						from.SendGump( new SetupBarsPriest2( from, 1 ) );
						break;
					}
					case 80:
					{
						if ( DB.Hue == 2 ){ DB.Hue = 0; } else { DB.Hue = 2; }
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 81:
					{
						if ( DB.Hue == 3 ){ DB.Hue = 0; } else { DB.Hue = 3; }
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 980:
					{
						from.CloseGump( typeof( SetupBarsMonk1 ) );
						from.SendGump( new SetupBarsMonk1( from, 1 ) );
						break;
					}
					case 981:
					{
						from.CloseGump( typeof( SetupBarsMonk2 ) );
						from.SendGump( new SetupBarsMonk2( from, 1 ) );
						break;
					}
					case 500:
					{
						DB.MagerySpellHue = 0x47E;
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 501:
					{
						DB.MagerySpellHue = 0x94E;
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 502:
					{
						DB.MagerySpellHue = 0x48D;
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 503:
					{
						DB.MagerySpellHue = 0x48E;
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 504:
					{
						DB.MagerySpellHue = 0x48F;
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 505:
					{
						DB.MagerySpellHue = 0x490;
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 506:
					{
						DB.MagerySpellHue = 0x491;
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
					case 507:
					{
						DB.MagerySpellHue = 0;
						from.SendGump( new Server.Engines.Help.HelpGump( from, 12 ) );
						break;
					}
				}
			}
		}

		public static string MyQuests( Mobile from )
        {
			PlayerMobile pm = (PlayerMobile)from;

			string sQuests = "Below is a brief list of current quests, along with achievements in specific discoveries. These are owned quests, which are specific to your character. Other quests (like messages in a bottle, treasure maps, or scribbled notes) are not listed here.<br><br>";

			string ContractQuest = CharacterDatabase.GetQuestInfo( from, "StandardQuest" );
			if ( CharacterDatabase.GetQuestState( from, "StandardQuest" ) ){ string sAdventurer = StandardQuestFunctions.QuestStatus( from ); sQuests = sQuests + "-" + sAdventurer + ".<br><br>"; }

			string ContractKiller = CharacterDatabase.GetQuestInfo( from, "AssassinQuest" );
			if ( CharacterDatabase.GetQuestState( from, "AssassinQuest" ) ){ string sAssassin = AssassinFunctions.QuestStatus( from ); sQuests = sQuests + "-" + sAssassin + ".<br><br>"; }

			string ContractSailor = CharacterDatabase.GetQuestInfo( from, "FishingQuest" );
			if ( CharacterDatabase.GetQuestState( from, "FishingQuest" ) ){ string sSailor = FishingQuestFunctions.QuestStatus( from ); sQuests = sQuests + "-" + sSailor + ".<br><br>"; }

			sQuests = sQuests + OtherQuests( from );

			if ( CharacterDatabase.GetKeys( from, "UndermountainKey" ) ){ sQuests = sQuests + "-Found a key made of dwarven steel.<br><br>"; }
			if ( CharacterDatabase.GetKeys( from, "BlackKnightKey" ) ){ sQuests = sQuests + "-Found the Black Knight's key.<br><br>"; }
			if ( CharacterDatabase.GetKeys( from, "SkullGate" ) ){ sQuests = sQuests + "-Discovered the secret of Skull Gate.<br>   One is in the Undercity of Umbra in Sosaria.<br>   The other is in the Ravendark Woods.<br><br>"; }
			if ( CharacterDatabase.GetKeys( from, "SerpentPillars" ) ){ sQuests = sQuests + "-Discovered the secret of the Serpent Pillars.<br>   Sosaria: 86° 41'S, 124° 39'E<br>   Lodoria: 35° 36'S, 65° 2'E<br><br>"; }
			if ( CharacterDatabase.GetKeys( from, "RangerOutpost" ) ){ sQuests = sQuests + "-Discovered the Ranger Outpost.<br><br>"; }

			if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleMadGodName" ) ){ sQuests = sQuests + "-Learned about the Mad God Tarjan.<br><br>"; }
			if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleCatacombKey" ) ){ sQuests = sQuests + "-The priest from the Mad God Temple gave me the key to the Catacombs.<br><br>"; }
			if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleSpectreEye" ) ){ sQuests = sQuests + "-Found a mysterious eye from the Catacombs.<br><br>"; }
			if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleHarkynKey" ) ){ sQuests = sQuests + "-Found a key with a symbol of a dragon on it.<br><br>"; }
			if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleDragonKey" ) ){ sQuests = sQuests + "-Found a rusty key from around a gray dragon's neck.<br><br>"; }
			if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleCrystalSword" ) ){ sQuests = sQuests + "-Found a crystal sword.<br><br>"; }
			if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleSilverSquare" ) ){ sQuests = sQuests + "-Found a silver square.<br><br>"; }
			if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleKylearanKey" ) ){ sQuests = sQuests + "-Found a key with a symbol of a unicorn on it.<br><br>"; }
			if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleBedroomKey" ) ){ sQuests = sQuests + "-Found a key with a symbol of a tree on it.<br><br>"; }
			if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleSilverTriangle" ) ){ sQuests = sQuests + "-Found a silver triangle.<br><br>"; }
			if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleCrystalGolem" ) ){ sQuests = sQuests + "-Destroyed the crystal golem and found a golden key.<br><br>"; }
			if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleEbonyKey" ) ){ sQuests = sQuests + "-Kylearan gave me an ebony key with a demon symbol on it.<br><br>"; }
			if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleSilverCircle" ) ){ sQuests = sQuests + "-Found a silver circle.<br><br>"; }
			if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleWin" ) && ((PlayerMobile)from).Profession != 1 ){ sQuests = sQuests + "-Defeated the evil wizard Mangar and escaped Skara Brae.<br><br>"; }

			if ( CharacterDatabase.GetDiscovered( from, "the Land of Sosaria" ) ){ sQuests = sQuests + "-Discovered the World of Sosaria.<br><br>"; }
			if ( CharacterDatabase.GetDiscovered( from, "the Island of Umber Veil" ) ){ sQuests = sQuests + "-Discovered Umber Veil.<br><br>"; }
			if ( CharacterDatabase.GetDiscovered( from, "the Land of Ambrosia" ) ){ sQuests = sQuests + "-Discovered Ambrosia.<br><br>"; }
			if ( CharacterDatabase.GetDiscovered( from, "the Land of Lodoria" ) ){ sQuests = sQuests + "-Discovered the Elven World of Lodoria.<br><br>"; }
			if ( CharacterDatabase.GetDiscovered( from, "the Serpent Island" ) ){ sQuests = sQuests + "-Discovered the Serpent Island.<br><br>"; }
			if ( CharacterDatabase.GetDiscovered( from, "the Isles of Dread" ) ){ sQuests = sQuests + "-Discovered the Isles of Dread.<br><br>"; }
			if ( CharacterDatabase.GetDiscovered( from, "the Savaged Empire" ) ){ sQuests = sQuests + "-Discovered the Valley of the Savaged Empire.<br><br>"; }
			if ( CharacterDatabase.GetDiscovered( from, "the Bottle World of Kuldar" ) ){ sQuests = sQuests + "-Discovered the Bottle World of Kuldar.<br><br>"; }
			if ( CharacterDatabase.GetDiscovered( from, "the Underworld" ) ){ sQuests = sQuests + "-Discovered the Underworld.<br><br>"; }

			return "Quests For " + from.Name + "<br><br>" + sQuests;
        }

		public static string MyHelp()
        {
			string allowSave = "";
				if ( MyServerSettings.AllowSaveFunction() ){ allowSave = "[save - Saves the game server and world.<br><br>"; }

			string HelpText = "If you are looking for help exploring this world, you can learn about almost anything within the game world you travel. Some merchants sell scrolls or books that will explain how some skills can be performed, resources gathered, and even how elements of the world can be manipulated. A sage often sells many tomes of useful information on skills, weapon abilities, or various types of magics available. If you are totally new to this game, buy yourself a Guide to Adventure book from a sage if you lost the one you started with. This book explains how to navigate and play the game. You will also learn some things about how the world behaves such as merchant interactions, how to use items, and what to do when your character dies. Talk to the townsfolk to learn whatever you can. On this screen there are many options, information, and settings that can assist in your journey. Many of the options here have keyboard commands that are listed below. Make sure to check out the 'Info' section on your character's paperdoll as it has some vital information about your character.<br><br>"
				+ "Common Commands: Below are the commands you can use for various things in the game.<br><br>"
				+ "[abilitynames - Turns on/off the special weapon ability names next to the appropriate icons.<br><br>"
				+ "[afk - Turns on/off the notification to others that you are away from keyboard.<br><br>"
				+ "[bandother - Bandage other command.<br><br>"
				+ "[bandself - Bandage self command.<br><br>"
				+ "[barbaric - Turns on/off the barbaric flavor the game provides (see end).<br><br>"
				+ "[c - Initiates the chat system.<br><br>"
				+ "[corpse - Helps one find their remains.<br><br>"
				+ "[corpseclear - Removes your corpse from a ship's deck.<br><br>"
				+ "[e - Opens the emote mini window.<br><br>"
				+ "[emote - Opens the emote window.<br><br>"
				+ "[evil - Turns on/off the evil flavor the game provides (see end).<br><br>"
				+ "[loot - Automatically take certain items from common dungeon chests or corpses and put them in your backpack. The unknown items are those that will need identification, but you may decide to take them anyway. The reagent options have a few categories. Magery and necromancer reagents are those used specifically by those characters. Alchemic reagents are those that fall outside the category of magery and necromancer reagents, and only alchemists use them. Herbalist reagents are plants that one may find, used in druidic herbalism.<br><br>"
				+ "[magicgate - Helps one find the nearest magical gate.<br><br>"
				+ "[motd - Opens the message of the day.<br><br>"
				+ "[oriental - Turns on/off the oriental flavor the game provides (see end).<br><br>"
				+ "[password - Change your account password.<br><br>"
				+ "[poisons - This changes how poisoned weapons work, which can be for either precise control with special weapon infectious strikes (default) or with hits of a one-handed slashing or piercing weapon.<br><br>"
				+ "[private - Turns on/off detailed messages of your journey for the town crier and local citizen chatter.<br><br>"
				+ "[quests - Opens a scroll to show certain quest events.<br><br>"
				+ "[quickbar - Opens a small, vertical bar with common game functions for easier use.<br><br>"
				+ "[sad - Opens the weapon's special abilities.<br><br>"
				+ allowSave
				+ "[sheathe - Turns on/off the feature to sheathe your weapon when not in battle.<br><br>"
				+ "[skill - Shows you what each skill is used for.<br><br>"
				+ "[skilllist - Displays a more condensed list of skills you have set to 'up' and perhaps 'locked'.<br><br>"
				+ "[spellhue ## - This command, following by a color reference hue number, will change all of your magery spell effects to that color. A value of '1' will normally render as '0' so avoid that setting as it will not produce the result you may want.<br><br>"
				+ "[statistics - Shows you some statistics of the server.<br><br>"
				+ "[wealth - Opens a small, horizontal bar showing your gold value for the various forms of currency and gold in your bank and backpack. Currency are items you would have a banker convert to gold for you (silver, copper, xormite, jewels, and crystals). If you put these items in your bank, you can update the values on the wealth bar by right clicking on it.<br><br>"

				+ "<br><br>"

				+ "Area Difficulty Levels: When you enter many dangerous areas, there will be a message to you that you entered a particular area. There may be a level of difficulty shown in parenthesis, that will give you an indication on the difficulty of the area. Below are the descriptions for each level.<br><br>"
				+ " - Easy (Not much of a challenge)<br><br>"
				+ " - Normal (An average level of<br>"
				+ "        challenge)<br><br>"
				+ " - Difficult (A tad more difficult)<br><br>"
				+ " - Challenging (You will probably<br>"
				+ "        run away alot)<br><br>"
				+ " - Hard (You will probably die alot)<br><br>"
				+ " - Deadly (I dare you)<br><br>"

				+ "<br><br>"

				+ "Skill Titles: You can set your default title for your character. Although you may be a Grandmaster Driven, you may want your title to reflect your Apprentice Wizard title instead. This is how you set it...<br><br>"
				+ "Type the '[SkillName' command followed by the name of the skill you want to set as your default. Make sure you surround the skill name in quotes and all lowercase. Example...<br>"
				+ "  [SkillName \"animal taming\"<br><br>"
				+ "If you want the game to manage your character's title, simply use the same command with a skill name of \"clear\".<br><br>"

				+ "<br><br>"

				+ "Reagent Counters: Below are the commands you can use to watch your reagent quantities as you cast spells. These are horizontal bars that will show the quantities of the reagents you are carrying. These will show updated quantities of reagents whenever you cast a spell or make a potion that uses them. Otherwise you can make a macro to these commands and use them to refresh the amounts manually, or you can can simply right click on the bar to refresh the quantities as well.<br><br>"
				+ "[mreagents - Opens the magery reagent bar.<br><br>"
				+ "[nreagents - Opens the necromaner reagent bar.<br><br>"
				+ "[areagents - Opens the alchemy reagent bar.<br><br>"
				+ "[creagents - Closes all reagent bars.<br><br>"

				+ "<br><br>"

				+ "Magic Toolbars: Below are the commands you can use to manage magic toolbars that might help you play better.<br><br>"
				+ "[bardsong1 - Opens the 1st bard song bar editor.<br><br>"
				+ "[bardsong2 - Opens the 2nd bard song bar editor.<br><br>"
				+ "[chivalryspell1 - Opens the 1st chivalry spell bar editor.<br><br>"
				+ "[chivalryspell2 - Opens the 2nd chivalry spell bar editor.<br><br>"
				+ "[deathspell1 - Opens the 1st death knight spell bar editor.<br><br>"
				+ "[deathspell2 - Opens the 2nd death knight spell bar editor.<br><br>"
				+ "[holyspell1 - Opens the 1st priest prayer bar editor.<br><br>"
				+ "[holyspell2 - Opens the 2nd priest prayer bar editor.<br><br>"
				+ "[magespell1 - Opens the 1st mage spell bar editor.<br><br>"
				+ "[magespell2 - Opens the 2nd mage spell bar editor.<br><br>"
				+ "[magespell3 - Opens the 3rd mage spell bar editor.<br><br>"
				+ "[magespell4 - Opens the 4th mage spell bar editor.<br><br>"
				+ "[monkspell1 - Opens the 1st monk ability bar editor.<br><br>"
				+ "[monkspell2 - Opens the 2nd monk ability bar editor.<br><br>"
				+ "[necrospell1 - Opens the 1st necromancer spell bar editor.<br><br>"
				+ "[necrospell2 - Opens the 2nd necromancer spell bar editor.<br><br>"

				+ "<br><br>"

				+ "[bardtool1 - Opens the 1st bard song bar.<br><br>"
				+ "[bardtool2 - Opens the 2nd bard song bar.<br><br>"
				+ "[chivalrytool1 - Opens the 1st chivalry spell bar.<br><br>"
				+ "[chivalrytool2 - Opens the 2nd chivalry spell bar.<br><br>"
				+ "[deathtool1 - Opens the 1st death knight spell bar.<br><br>"
				+ "[deathtool2 - Opens the 2nd death knight spell bar.<br><br>"
				+ "[holytool1 - Opens the 1st priest prayer bar.<br><br>"
				+ "[holytool2 - Opens the 2nd priest prayer bar.<br><br>"
				+ "[magetool1 - Opens the 1st mage spell bar.<br><br>"
				+ "[magetool2 - Opens the 2nd mage spell bar.<br><br>"
				+ "[magetool3 - Opens the 3rd mage spell bar.<br><br>"
				+ "[magetool4 - Opens the 4th mage spell bar.<br><br>"
				+ "[monktool1 - Opens the 1st monk ability bar.<br><br>"
				+ "[monktool2 - Opens the 2nd monk ability bar.<br><br>"
				+ "[necrotool1 - Opens the 1st necromancer spell bar.<br><br>"
				+ "[necrotool2 - Opens the 2nd necromancer spell bar.<br><br>"

				+ "<br><br>"

				+ "[bardclose1 - Closes the 1st bard song bar.<br><br>"
				+ "[bardclose2 - Closes the 2nd bard song bar.<br><br>"
				+ "[chivalryclose1 - Closes the 1st chivalry spell bar.<br><br>"
				+ "[chivalryclose2 - Closes the 2nd chivalry spell bar.<br><br>"
				+ "[deathclose1 - Closes the 1st death knight spell bar.<br><br>"
				+ "[deathclose2 - Closes the 2nd death knight spell bar.<br><br>"
				+ "[holyclose1 - Closes the 1st priest prayer bar.<br><br>"
				+ "[holyclose2 - Closes the 2nd priest prayer bar.<br><br>"
				+ "[mageclose1 - Closes the 1st mage spell bar.<br><br>"
				+ "[mageclose2 - Closes the 2nd mage spell bar.<br><br>"
				+ "[mageclose3 - Closes the 3rd mage spell bar.<br><br>"
				+ "[mageclose4 - Closes the 4th mage spell bar.<br><br>"
				+ "[monkclose1 - Closes the 1st monk ability bar.<br><br>"
				+ "[monkclose2 - Closes the 2nd monk ability bar.<br><br>"
				+ "[necroclose1 - Closes the 1st necromancer spell bar.<br><br>"
				+ "[necroclose2 - Closes the 2nd necromancer spell bar.<br><br>"

				+ "<br><br>"

				+ "Music: There is many different pieces of classic music in the game, and they play depending on areas you visit. Some of the music is from the original game, but there are some pieces from the older Ultima games. There are also some pieces from computer games in the 1990's, but they really fit the theme when traveling the land. You can choose to listen to them, or change the music you are listening to when exploring the world. Keep in mind that when you change the music, and you enter a new area, the default music for that area will play and you may have to change your music again. Also keep in mind that your game client will want to play the song for a few seconds before allowing a switch of new music. You can use the below command to open a window that allows you to choose a song to play. Almost all of them play in a loop, where there are three that do not and are marked with an asterisk. There are two pages of songs to choose from so use the top arrow to go back and forth to each screen. When your music begins to play, then press the OKAY button to exit the screen. Although an unnecessary function, it does give you some control over the music in the game.<br><br>"
				+ "[music - Opens the music player.<br><br>"
				+ "The below command will simply toggle your music preference to play a different set of music in the dungeons. When turned on, it will play music you normally hear when traveling the land, instead of the music commonly played in dungeons.<br><br>"
				+ "[musical - Sets the default dungeon music.<br><br>"

				+ "<br><br>"

				+ "Evil Style: There is an evil element to the game that some want to participate in. With classes such as Necromancers, some players may want to travel a world with this flavor added. This particular setting allows you to toggle between regular and evil flavors. When in the evil mode, some of the treasure you will find will often have a name that fits in the evil style. When you stay within negative karma, skill titles will change for you as well, but not all. Look over the book of skill titles (found within the game world) to see which titles will change based on karma. Some of the relics you will find may also have this style, to perhaps decorate a home in this fashion. This option can be turned off and on at any time. You can only have one type of play style active at any one time.<br><br>"
				+ "[evil - Turns on/off the evil flavor the game provides.<br><br>"

				+ "<br><br>"

				+ "Oriental Style: There is an oriental element to the game that most do not want to participate in. With classes such as Ninja and Samurai, some players may want to travel a world with this flavor added. This particular setting allows you to toggle between fantasy and oriental. When in the oriental mode, much of the treasure you will find will be of Chinese or Japanese historical origins. These types of items will most times be named to match the style. Items that once belonged to someone, will often have a name that fits in the oriental style. Some of the skill titles will change for you as well, but not all. Look over the book of skill titles (found within the game world) to see which titles will change based on this play style. Some of the relics and artwork you will find will also have this style, to perhaps decorate a home in this fashion. This option can be turned off and on at any time. You can only have one type of play style active at any one time.<br><br>"
				+ "[oriental - Turns on/off the oriental flavor the game provides.<br><br>"

				+ "<br><br>"

				+ "Barbaric Style: The default game does not lend itself to a sword and sorcery experience. This means that it is not the most optimal play experience to be a loin cloth wearing barbarian that roams the land with a huge axe. Characters generally get as much equipment as they can in order to maximize their rate of survivability. This particular play style can help in this regard. Choosing to play in this style will have a satchel appear in your main pack. You cannot store anything in this satchel, as its purpose is to change certain pieces of equipment you place into it. It will change shields, hats, helms, tunics, sleeves, leggings, boots, gorgets, gloves, necklaces, cloaks, and robes. When these items get changed, they will become something that appears differently but behave in the same way the previous item did. These different items can be equipped but may not appear on your character. Also note that when you wear robes, they cover your character's tunics and sleeves. Wearing a sword and sorcery robe will do the same thing so you will have to remove the robe in order to get to the sleeves and/or tunic. This play style has their own set of skill titles for many skills as well. If you are playing a female character, pressing the button further will convert any 'Barbarian' titles to 'Amazon'. You can open your satchel to learn more about this play style. This option can be turned off and on at any time. You can only have one type of play style active at any one time.<br><br>"
				+ "[barbaric - Turns on/off the barbaric flavor the game provides.<br><br>"

			+ "";

			return HelpText;
		}

		public static string OtherQuests( Mobile from )
        {
			string quests = "";

			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			{
				if ( item is ThiefNote )
				{
					if ( ((ThiefNote)item).NoteOwner == from )
					{
						if ( Server.Items.ThiefNote.ThiefAllowed( from ) == null )
						{
							quests = quests + "-" + ((ThiefNote)item).NoteStory + "<br><br>";
						}
						else
						{
							quests = quests + "-You have a secret note instructing you to steal something, but you will take a break from thieving and read it in about " + Server.Items.ThiefNote.ThiefAllowed( from ) + " minutes.<br><br>";
						}
					}
				}
				else if ( item is CourierMail )
				{
					if ( ((CourierMail)item).Owner == from )
					{
						quests = quests + "-You need to find " + ((CourierMail)item).SearchItem + " for " + ((CourierMail)item).ForWho + ". They said in their letter that you should search in " + ((CourierMail)item).SearchDungeon + " in " + ((CourierMail)item).SearchWorld + ".<br><br>";
					}
				}
				else if ( item is SearchPage )
				{
					if ( ((SearchPage)item).Owner == from )
					{
						quests = quests + "-You want to find " + ((SearchPage)item).SearchItem + " in " + ((SearchPage)item).SearchDungeon + " in " + ((SearchPage)item).SearchWorld + ".<br><br>";
					}
				}
				else if ( item is SummonPrison )
				{
					if ( ((SummonPrison)item).owner == from )
					{
						quests = quests + "-You currently have " + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(((SummonPrison)item).Prisoner.ToLower()) + " in a Magical Prison.<br><br>";
					}
				}
				else if ( item is VortexCube )
				{
					if ( ((VortexCube)item).CubeOwner == from )
					{
						VortexCube cube = (VortexCube)item;
						quests = quests + "-You are searching for the Codex of Ultimate Wisdom.<br>";

						if ( cube.HasConvexLense > 0 ){ quests = quests + "   -You have the Convex Lense.<br>"; }
						if ( cube.HasConcaveLense > 0 ){ quests = quests + "   -You have the Concave Lense.<br>"; }

						if ( cube.HasKeyLaw > 0 ){ quests = quests + "   -You have the Key of Law.<br>"; }
						if ( cube.HasKeyBalance > 0 ){ quests = quests + "   -You have the Key of Balance.<br>"; }
						if ( cube.HasKeyChaos > 0 ){ quests = quests + "   -You have the Key of Chaos.<br>"; }

						if ( cube.HasCrystalRed > 0 ){ quests = quests + "   -You have the Red Void Crystal.<br>"; }
						if ( cube.HasCrystalBlue > 0 ){ quests = quests + "   -You have the Blue Void Crystal.<br>"; }
						if ( cube.HasCrystalGreen > 0 ){ quests = quests + "   -You have the Green Void Crystal.<br>"; }
						if ( cube.HasCrystalYellow > 0 ){ quests = quests + "   -You have the Yellow Void Crystal.<br>"; }
						if ( cube.HasCrystalWhite > 0 ){ quests = quests + "   -You have the White Void Crystal.<br>"; }
						if ( cube.HasCrystalPurple > 0 ){ quests = quests + "   -You have the Purple Void Crystal.<br>"; }

						quests = quests + "<br>";
					}
				}
				else if ( item is ObeliskTip )
				{
					if ( ((ObeliskTip)item).ObeliskOwner == from )
					{
						ObeliskTip obelisk = (ObeliskTip)item;
						quests = quests + "-You are trying to become a Titan of Ether.<br>";
						quests = quests + "   -You have the Obelisk Tip.<br>"; 

						if ( obelisk.WonAir > 0 ){ quests = quests + "   -You have defeated Stratos, the Titan of Air.<br>"; }
						else if ( obelisk.HasAir > 0 ){ quests = quests + "   -You have the Breath of Air.<br>"; }
						if ( obelisk.WonFire > 0 ){ quests = quests + "   -You have defeated Pyros, the Titan of Fire.<br>"; }
						else if ( obelisk.HasFire > 0 ){ quests = quests + "   -You have the Tongue of Flame.<br>"; }
						if ( obelisk.WonEarth > 0 ){ quests = quests + "   -You have defeated Lithos, the Titan of Earth.<br>"; }
						else if ( obelisk.HasEarth > 0 ){ quests = quests + "   -You have the Heart of Earth.<br>"; }
						if ( obelisk.WonWater > 0 ){ quests = quests + "   -You have defeated Hydros, the Titan of Water.<br>"; }
						else if ( obelisk.HasWater > 0 ){ quests = quests + "   -You have the Tear of the Seas.<br>"; }

						quests = quests + "<br>";
					}
				}
				else if ( item is MuseumBook )
				{
					if ( ((MuseumBook)item).ArtOwner == from )
					{
						quests = quests + "-You have found " + MuseumBook.GetTotal( (MuseumBook)item ) + " out of 60 antiques for the museum.<br><br>";
					}
				}
				else if ( item is RuneBox )
				{
					if ( ((RuneBox)item).RuneBoxOwner == from )
					{
						int runes = ((RuneBox)item).HasCompassion + ((RuneBox)item).HasHonesty + ((RuneBox)item).HasHonor + ((RuneBox)item).HasHumility + ((RuneBox)item).HasJustice + ((RuneBox)item).HasSacrifice + ((RuneBox)item).HasSpirituality + ((RuneBox)item).HasValor;
						quests = quests + "-You have found " + runes + " out of 8 runes of virtue.<br><br>";
					}
				}
				else if ( item is QuestTome )
				{
					if ( ((QuestTome)item).QuestTomeOwner == from )
					{
						quests = quests + "-You are on a quest to find " + ((QuestTome)item).GoalItem4 + ".<br><br>";
					}
				}
			}

			if ( CharacterDatabase.GetKeys( from, "Museums" ) )
			{
				quests = quests + "-You have found all of the antiques for the Museum.<br><br>";
			}
			if ( CharacterDatabase.GetKeys( from, "Gygax" ) )
			{
				quests = quests + "-You have obtained the Statue of Gygax.<br><br>";
			}
			if ( CharacterDatabase.GetKeys( from, "Virtues" ) )
			{
				quests = quests + "-You have cleansed all of the Runes of Virtue.<br><br>";
			}
			else if ( CharacterDatabase.GetKeys( from, "Corrupt" ) )
			{
				quests = quests + "-You have corrupted all of the Runes of Virtue.<br><br>";
			}
			else if ( CharacterDatabase.GetKeys( from, "Runes" ) )
			{
				quests = quests + "-You are searching for the Runes of Virtue.<br><br>";
			}

			return quests;
		}
	}
}

namespace Server.Gumps
{
    public class InfoHelpGump : Gump
    {
		public int m_Origin;

        public InfoHelpGump( int page, int origin ) : base( 25, 25 )
        {
			m_Origin = origin;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			string title = "";
			string info = "";
			bool scrollbar = true;

			if ( page == 82 )
			{
				scrollbar = false;
				title = "Music Tone";
				info = "This option will simply toggle your music preference to play a different set of music in the dungeons. When turned on, it will play music you normally hear when traveling the land, instead of the music commonly played in dungeons.";
			}
			else if ( page == 83 )
			{
				title = "Music Playlist";
				info = "This gives you a complete list of the in-game music. You can select the music you like and those choices will randomly play as you go from region to region. To listen to a song for review, select the blue gem icon. Note that the client has a delay time when you can start another song so selecting the blue gem may not respond if you started a song too soon before that. Wait for a few seconds and try clicking the blue gem again to see if that song starts to play. Playlists are disabled by default, so if you want your playlist to function, make sure to enable it.";
			}
			else if ( page == 84 )
			{
				scrollbar = false;
				title = "Private";
				info = "This option turns on or off the detailed messages of your journey for the town crier and local citizen chatter. This keeps your activities private so others will not see where you are traveling the world.";
			}
			else if ( page == 85 )
			{
				title = "Loot Options";
				info = "This lets you select from a list of categories, where they will automatically take those types of items from common dungeon chests or corpses and put them in your backpack. If you select coins, you will take wealth in the form of currency or gold nuggets. If you take gems and jewels, this will consist of gems, gemstones, jewelry, jewels, and crystals. The unknown items are those that will need identification, but you may decide to take them anyway. The reagent options have a few categories. Magery and necromancer reagents are those used specifically by those characters. Alchemic reagents are those that fall outside the category of magery and necromancer reagents, and only alchemists use them. Herbalist reagents are plants that one may find, used in druidic herbalism.";
			}
			else if ( page == 86 )
			{
				title = "Classic Poisoning";
				info = "There are two methods that assassins use to handle poisoned weapons. One is the simple method of soaking the blade and having it poison whenever it strikes their opponent. With this method, known as classic poisoning, there is little control on the dosage given but it is easier to maneuver. When this option is turned off, it has the newer and more tactical method, where only certain weapons can be poisoned and the assassin can control when the poison is administered with the hit. Although the tactical method requires more thought, it does have the potential to allow an assassin to poison certain arrows, for example. The choice of methods can be switched at any time, but only one method can be in use at a given time.";
			}
			else if ( page == 87 )
			{
				title = "Skill Title";
				info = "When you don't set your skill title here, the game will take your highest skill and make that into your character's title. Choosing a skill here will force your title to that profession. So if you always want to be known as a wizard, then select the 'Magery' option (for example). You can let the game manage this at any time by setting it back to 'Auto Title'. Be warned when choosing a skill, if you have zero skill points in it, you will be titled 'the Village Idiot'. If you get at least 0.1, you will at least be 'Aspiring'.";
			}
			else if ( page == 88 )
			{
				title = "Reagent Bar - Alchemy";
				info = "These reagent bars show the number of each reagent you currently have in your pack. Although alchemy uses many different types of reagents, this reagent bar only has those reagents that alchemists use (like silver widow or red lotus). Whenever you mix a potion or cast a spell, the values will change to reflect your new inventory amounts. You can force a refresh of these bars by right-clicking on them as well. If you want to ever close these reagent bars, you will need to use the '[creagents' command or the 'Reagent Bar - Close All' option in the 'Settings' section of the 'Help' screen.";
			}
			else if ( page == 89 )
			{
				title = "Reagent Bar - Magery";
				info = "These reagent bars show the number of each reagent you currently have in your pack. This reagent bar only has those reagents that mages use (like black pearl or sulfurous ash). Whenever you mix a potion or cast a spell, the values will change to reflect your new inventory amounts. You can force a refresh of these bars by right-clicking on them as well. If you want to ever close these reagent bars, you will need to use the '[creagents' command or the 'Reagent Bar - Close All' option in the 'Settings' section of the 'Help' screen.";
			}
			else if ( page == 90 )
			{
				title = "Reagent Bar - Necromancy";
				info = "These reagent bars show the number of each reagent you currently have in your pack. This reagent bar only has those reagents that necromancers use (like grave dust or pig iron). Whenever you mix a potion or cast a spell, the values will change to reflect your new inventory amounts. You can force a refresh of these bars by right-clicking on them as well. If you want to ever close these reagent bars, you will need to use the '[creagents' command or the 'Reagent Bar - Close All' option in the 'Settings' section of the 'Help' screen.";
			}
			else if ( page == 91 )
			{
				scrollbar = false;
				title = "Reagent Bar - Close All";
				info = "This option is used when you want to close any reagent bars you have open as they are locked in an open state to avoid accidental closure. You can also use the '[creagents' command do this as well.";
			}
			else if ( page == 92 )
			{
				title = "Play Style - Normal";
				info = "This is the default play style for " + Server.Misc.ServerList.ServerName + ". It is designed for a classic fantasy world experience for the players. There are two other play styles available, evil and oriental. Play styles do not change the mechanics of the game playing experience, but it does change the flavor of the treasure you find and the henchman you hire. For example, you can set your play style to an 'evil' style of play. What happens is you will find treasure geared toward that play style. Where you would normally find a blue 'mace of might', the evil style would have you find a black 'mace of ghostly death'. They are simply a way to tweak your character's experience in the game.";
			}
			else if ( page == 93 )
			{
				title = "Play Style - Evil";
				info = "There is an evil element to the game that some want to participate in. With classes such as Necromancers, some players may want to travel a world with this flavor added. This particular setting allows you to toggle between regular and evil flavors. When in the evil mode, some of the treasure you will find will often have a name that fits in the evil style. When you stay within negative karma, skill titles will change for you as well, but not all. Look over the book of skill titles (found within the game world) to see which titles will change based on karma. Some of the relics you will find may also have this style, to perhaps decorate a home in this fashion. This option can be turned off and on at any time. You can only have one type of play style active at any one time.<br><br>"
				+ "[evil - Turns on/off the evil flavor the game provides.";
			}
			else if ( page == 94 )
			{
				title = "Play Style - Oriental";
				info = "There is an oriental element to the game that most do not want to participate in. With classes such as Ninja and Samurai, some players may want to travel a world with this flavor added. This particular setting allows you to toggle between fantasy and oriental. When in the oriental mode, much of the treasure you will find will be of Chinese or Japanese historical origins. These types of items will most times be named to match the style. Items that once belonged to someone, will often have a name that fits in the oriental style. Some of the skill titles will change for you as well, but not all. Look over the book of skill titles (found within the game world) to see which titles will change based on this play style. Some of the relics and artwork you will find will also have this style, to perhaps decorate a home in this fashion. This option can be turned off and on at any time. You can only have one type of play style active at any one time.";
			}
			else if ( page == 95 )
			{
				m_Origin = 7;
				title = "Magic Toolbars";
				info = "These toolbars can be configured for all areas of magical-style spells in the game. Each school of magic has two separate toolbars you can customize, except for magery which has four available. The large number of spells for magery benefit from the extra two toolbars. These toolbars allow you to select spells that you like to cast often, and set whether the bar will appear vertical or horizontal. If you choose to have the toolbar appear vertical, you have the additional option of showing the spell names next to the icons. These toolbars can be moved around and you need only single click the appropriate icon to cast the spell. If you have spells selected for a toolbar, but lack the spell in your spellbook, the icon will not appear when you open the toolbar. These toolbars cannot be closed by normal means, to avoid the chance you close them by accident when in combat. You can either use the command button available in the 'Help' section, or the appropriate typed keyboard command.";
			}
			else if ( page == 96 )
			{
				scrollbar = false;
				title = "Magery Spell Color";
				info = "You can change the color for all of your magery spell effects here. There are a limited amount of choices given here. Once set, your spells will be that color for every effect. If you want to set it back to normal, then select the 'Default' option. You can also use the '[spellhue' command followed by a number of any color you want to set it to.";
			}
			else if ( page == 97 )
			{
				title = "Container Fix";
				info = "This game uses some brand new containers that do not exist in the original game. Along with that, there is a client limitation on where you can place items in a non-standard container. There are newly developed game clients that do not readily suffer from this limitation. That is why this option exists for you to choose. If you apply this fix to your game, then non-standard containers will have a different appearance when you open them so you can visualize clearly where items can be placed within the container. If you use a more modernized and developed client, you may want to leave this setting alone. If you do apply this fix, some containers will not be affected by the change where corpses and dungeon containers will remain as is. NOTE: Some of the non-standard containers are the urns, vases, stone chests, coffins, sarcophagi, and metal crates. There are also new shelves where many show items on the shelves that are non-standard. Sacks and satchels are also non-standard. The wooden crates for home organization are another example. This cannot be used in conjunction with the 'Large Containers' option.";
			}
			else if ( page == 98 )
			{
				title = "Large Containers";
				info = "This option should only be used if you are using the modern client called ClassicUO. To use this option, you need to copy the 'containers.txt' file from your client's directory and overwrite the file in your ClassicUO 'Data/Client' directory. If you are not sure about what client you are using, then you should never check this option. It cannot be used in conjunction with the 'Container Fix' option. When enabled, this will give you larger container areas to organize your items. It will affect almost all containers from those in your home, bank, treasure, and corpses. Some containers do not scale with this like trash barrels. Keep in mind that there is another option built into ClassicUO that can scale your containers and even the items within them, so you can try to use that option instead of this one. You can even use them in conjunction.";
			}
			else if ( page == 99 )
			{
				scrollbar = false;
				title = "Ability Names";
				info = "When you get good enough with tactics and a weapon type, you will get special abilities that they can perform. These usually appear as simple icons you can select to do the action, but this option will turn on or off the special weapon ability names next to the appropriate icons.";
			}
			else if ( page == 100 )
			{
				scrollbar = false;
				title = "Auto Sheath";
				info = "This option turns on or off the feature to sheathe your weapon when not in battle. When you put your character back into war mode, they will draw the weapon.";
			}
			else if ( page == 198 )
			{
				title = "Play Style - Barbaric";
				info = "The default game does not lend itself to a sword and sorcery experience. This means that it is not the most optimal play experience to be a loin cloth wearing barbarian that roams the land with a huge axe. Characters generally get as much equipment as they can in order to maximize their rate of survivability. This particular play style can help in this regard. Choosing to play in this style will have a satchel appear in your main pack. You cannot store anything in this satchel, as its purpose is to change certain pieces of equipment you place into it. It will change shields, hats, helms, tunics, sleeves, leggings, boots, gorgets, gloves, necklaces, cloaks, and robes. When these items get changed, they will become something that appears differently but behave in the same way the previous item did. These different items can be equipped but may not appear on your character. Also note that when you wear robes, they cover your character's tunics and sleeves. Wearing a sword and sorcery robe will do the same thing so you will have to remove the robe in order to get to the sleeves and/or tunic. This play style has their own set of skill titles for many skills as well. If you are playing a female character, pressing the button further will convert any 'Barbarian' titles to 'Amazon'. You can open your satchel to learn more about this play style. This option can be turned off and on at any time. You can only have one type of play style active at any one time.";
			}
			else if ( page == 199 )
			{
				title = "Skill Lists";
				info = "Skill lists are an alternative to the normal skill lists you can get from clicking the appropriate button on the paper doll. Although you still need to use that for skill management (up, down, lock), skill lists have a more condensed appearance for when you play the game. In order for skills to appear in this alternate list, they have to either be set to 'up', or they can be set to 'locked'. The 'locked' skills will only display in this list if you change your settings here to reflect that. The list does not refresh in real time, but it will often refresh itself to show your skill status in both real and enhanced values. Any skill that appears in orange indicates a skill that you have locked. You can open this list with the '[skilllist' command, or the appropriate button on the main screen.";
			}
			else if ( page == 999 )
			{
				title = "Quick Bar";
				info = "This toolbar provides a quick and convenient way to keep an eye on certain inventory items, invoke commands, and access information. Since this toolbar is condensed, images are used to represent the function of the various buttons and will appear in the following order:";
				info = info + "<br><br>-Bandage Count";
				info = info + "<br><br>-Bandage Self Command";
				info = info + "<br><br>-Bandage Other Command";
				info = info + "<br><br>-Arrow Count";
				info = info + "<br><br>-Bolt Count";
				info = info + "<br><br>-Throwing Weapon Count";
				info = info + "<br><br>-Mage Eye Count";
				info = info + "<br><br>-View Message of the Day";
				info = info + "<br><br>-View Weapon Ability Toolbar";
				info = info + "<br><br>-Change Song Command";
				info = info + "<br><br>-AFK";
				info = info + "<br><br>-View Quests";
				info = info + "<br><br>-Find Nearest Moongate";
				info = info + "<br><br>-Chat Command";
				info = info + "<br><br>-View Emote Toolbar";
				info = info + "<br><br>-Find Your Corpse";
				info = info + "<br><br>-View Magery Reagent Toolbar";
				info = info + "<br><br>-View Necromancer Reagent Toolbar";
				info = info + "<br><br>-View Alchemy Reagent Toolbar";
				info = info + "<br><br>-Open Spell Toolbars";
				info = info + "<br>    -Close Spell Toolbars";
				info = info + "<br><br>-Close the Quick Bar";
				info = info + "<br><br>Some items on the quick bar will not appear unless certain requirements are met. You won't see options for spell toolbars unless you have the appropriate spellbook and at least a skill level of 5 in the associated skill. You won't see bandage, arrow, or bolt counts or functions without having at least one of such items in your back pack. The reagent counter buttons won't appear unless you have some skill in magery, necromancy, or alchemy. This design's purpose is to keep the quick bar as condensed as possible and omit icons that your character will not be using. This bar will update itself from time to time, but selecting an option, or right clicking the bar, will update it as well.";
			}
			else if ( page == 1000 )
			{
				title = "Flip Deed";
				info = "This option allows you to flip some deeds that can come in one of two direction facings. So if a deed states that furniture faces east, then you can set the deed on the floor of your house and flip it to face south instead. This can flip almost any deed-like items in this manner, but not all items are called 'deeds' or look like deeds. Some items behave as deeds and those can be flipped in the same manner. Tents or bear rugs, for example, have a facing and you can flip those with this command..";
			}

			AddPage(0);
			AddImage(0, 0, 155);
			AddImage(300, 0, 155);
			AddImage(2, 2, 163);
			AddImage(302, 2, 163);
			AddImage(97, 2, 163);
			AddImage(6, 6, 150);
			AddImage(42, 253, 140);
			AddImage(254, 71, 144);
			AddImage(6, 218, 139);
			AddImage(156, 19, 156);
			AddImage(182, 22, 156);
			AddImage(154, 34, 162);
			AddImage(65, 110, 2183);
			AddHtml( 185, 10, 403, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + title + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 189, 46, 316, 168, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + info + "</BIG></BASEFONT></BODY>", (bool)false, (bool)scrollbar);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
			from.SendSound( 0x4A );
			from.CloseGump( typeof( Server.Engines.Help.HelpGump ) );
			if ( m_Origin != 999 ){ from.SendGump( new Server.Engines.Help.HelpGump( from, m_Origin ) ); }
        }
    }
}