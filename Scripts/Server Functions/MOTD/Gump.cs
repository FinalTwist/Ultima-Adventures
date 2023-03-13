using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Misc;

namespace Joeku.MOTD
{
	public class MOTD_Gump : Gump
	{
		public int m_Origin;
		public Mobile User;
		public bool Help;
		public int Index;

		public MOTD_Gump( Mobile user, bool help, int index, int origin ) : base( 25, 25 )
		{
			m_Origin = origin;

			this.User = user;
			this.Help = help;
			this.Index = index;

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( user );

			int button = 4018;
			if( DB.CharacterMOTD == 1 )
				button = 3609;

            this.Closable=true;
			this.Disposable=true;
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
			AddImage(7, 7, 145);
			AddImage(7, 354, 142);
			AddImage(167, 7, 140);
			AddImage(264, 564, 140);
			AddImage(274, 7, 140);
			AddImage(558, 566, 143);
			AddImage(558, 9, 143);
			AddImage(286, 540, 156);
			AddHtml( 177, 37, 230, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>MESSAGE OF THE DAY</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddBody( user );
			AddItem(38, 269, 7775);
			AddHtml( 419, 531, 154, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>SHOW AT LOGIN</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(384, 531, button, button, 1, GumpButtonType.Reply, 0);
			AddImage(209, 100, 140);
			AddImage(367, 66, 134);
			AddItem(542, 36, 733);
			AddImage(181, 102, 159);
		}

		public void AddBody( Mobile m )
		{
			AddHtml(101, 152, 473, 353, MOTD_Main.Info[this.Index].Body, false, true); // Text - Main - Category - Body
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			int button = info.ButtonID;

			if ( info.ButtonID == 1 )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );

				if( DB.CharacterMOTD == 1 )
					DB.CharacterMOTD = 0;
				else
					DB.CharacterMOTD = 1;

				MOTD_Utility.SendGump( from, false, this.Index, m_Origin );

				from.SendSound( 0x4A ); 
			}
			else if ( m_Origin > 0 )
			{
				from.SendSound( 0x4A ); 
				from.SendGump( new Server.Engines.Help.HelpGump( from, 1 ) );
			}
		}
	}
}
