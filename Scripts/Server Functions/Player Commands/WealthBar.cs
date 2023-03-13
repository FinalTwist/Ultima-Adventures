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

namespace Server.Gumps 
{
    public class WealthBar : Gump
    {
		public int m_Origin;

		public static void Initialize()
		{
            CommandSystem.Register( "wealth", AccessLevel.Player, new CommandEventHandler( ToolBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "wealth" )]
		[Description( "Opens the Wealth Tracking Bar." )]
		public static void ToolBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( WealthBar ) );
			from.SendGump( new WealthBar( from ) );
        }

		public WealthBar ( Mobile from ) : base ( 25, 25 )
		{
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);

			AddImage(0, 0, 156);
			AddImage(30, 0, 156);
			AddImage(60, 0, 156);
			AddImage(90, 0, 156);
			AddImage(119, 0, 156);
			AddImage(149, 0, 156);
			AddImage(179, 0, 156);
			AddImage(209, 0, 156);
			AddImage(234, 0, 156);
			AddImage(264, 0, 156);

			AddImage(0, 13, 156);
			AddImage(30, 13, 156);
			AddImage(60, 13, 156);
			AddImage(90, 13, 156);
			AddImage(119, 13, 156);
			AddImage(149, 13, 156);
			AddImage(179, 13, 156);
			AddImage(209, 13, 156);
			AddImage(234, 13, 156);
			AddImage(264, 13, 156);

			AddImage(294, 0, 156);
			AddImage(294, 13, 156);
			AddItem(283, 10, 3823);

			AddItem(-5, 3, 7183);
			AddHtml( 39, 11, 100, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Server.Misc.GetPlayerInfo.GetWealth( from, 0 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddItem(155, 10, 10174);
			AddHtml( 182, 11, 100, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Server.Misc.GetPlayerInfo.GetWealth( from, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddImage(324, 0, 156);
			AddImage(324, 13, 156);
			AddImage(330, 0, 156);
			AddImage(330, 13, 156);
			AddButton(322, 10, 4020, 4020, 1, GumpButtonType.Reply, 0);
		}

		public static void RefreshWealthBar( Mobile from )
		{
			if ( from is PlayerMobile )
			{
				if( from.HasGump( typeof(WealthBar)) )
				{
					from.CloseGump( typeof(WealthBar) );
					from.SendGump( new WealthBar( from ) );
				}
			}
		}

		public void InvokeCommand( string c, Mobile from )
        {
            CommandSystem.Handle(from, String.Format("{0}{1}", CommandSystem.Prefix, c));
        }

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( info.ButtonID != 1 )
				from.SendGump( new WealthBar( from ) );
			else
				from.SendSound( 0x4A ); 
		}
    }
}