using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Multis
{
	public class ConfirmDryDockGump : Gump
	{
		private Mobile m_From;
		private BaseBoat m_Boat;
		private int m_Hue;

		public ConfirmDryDockGump( Mobile from, BaseBoat boat, int hue ) : base( 25, 25 )
		{
			m_From = from;
			m_Boat = boat;
			m_Hue = hue;


			string msg = "Do you want to dry dock your ship now?";
			int sign = 2998;
			if ( BaseBoat.isCarpet( m_Boat ) )
			{
				msg = "Do you want to roll up your carpet now?";
				sign = 2990;
			}


			m_From.CloseGump( typeof( ConfirmDryDockGump ) );

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 153);
			AddImage(4, 0, 153);
			AddImage(0, 4, 153);
			AddImage(4, 4, 153);
			AddImage(2, 2, 129);
			AddImage(2, 261, 130);
			AddImage(8, 61, 137);
			AddImage(2, 259, 162);
			AddImage(16, 254, 162);
			AddImage(272, 253, 156);
			AddImage(255, 256, 143);
			AddImage(102, 6, 146);
			AddImage(287, 35, 162);
			AddImage(2, 26, 156);
			AddImage(74, 30, 159);
			AddImage(109, 10, 156);
			AddItem(7, 14, sign);
			if ( !BaseBoat.isCarpet( m_Boat ) )
			{
			AddItem(129, 237, 5364);
			AddItem(85, 60, 2199);
				AddItem(60, 104, 5368);
			}
			AddButton(54, 237, 4023, 4023, 2, GumpButtonType.Reply, 0);
			AddButton(228, 237, 4020, 4020, 1, GumpButtonType.Reply, 0);
			AddHtml( 63, 137, 177, 87, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + msg + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( info.ButtonID == 2 )
				m_Boat.EndDryDock( m_From, m_Hue );
		}
	}
}