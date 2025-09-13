using System;
using Server;
using Server.Network;
using Server.Commands;
using Server.Items;
using Server.Engines.PartySystem;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Server.Mobiles;

namespace Server.Gumps
{
    public class PartyGump : Gump
    {
        private Mobile m_Target, m_Leader;

        public PartyGump(Mobile leader, Mobile target, int page) : base(25, 25)
        {
            m_Leader = leader;
            m_Target = target;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 152);
			AddImage(285, 0, 152);
			AddImage(0, 62, 152);
			AddImage(285, 62, 152);
			AddImage(283, 2, 129);
			AddImage(283, 60, 129);
			AddImage(2, 60, 129);
			AddImage(2, 2, 129);
			AddImage(7, 7, 145);
			AddImage(167, 7, 140);
			AddImage(243, 7, 140);
			AddImage(543, 9, 143);
			AddImage(47, 294, 130);
			AddImage(174, 123, 136);
			AddImage(19, 289, 159);
			int buttonIdOne = 1;
			int buttonIdTwo = 2;
			if (page == 0) {
				AddHtml( 176, 33, 344, 28, @"<BODY><BASEFONT Color=#FBFBFB><BIG>JOIN A GROUP OF ADVENTURERS</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 176, 81, 329, 105, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + m_Leader.Name + " is asking you to join their party! If you wish to accompany them, select the appropriate button. Otherwise, you can simply cancel the request and continue on your own journey.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			} else if (page == 1 && ((PlayerMobile)target).SoulBound) {
				AddHtml( 176, 33, 344, 28, @"<BODY><BASEFONT Color=#FBFBFB><BIG>SOULBOUND PARTY HAS DIED</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				this.AddHtml( 176, 81, 329, 105, @"<BODY><BASEFONT Color=#FCFF00><BIG>Do you want to teleport to their location?</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				buttonIdOne = 3;
				buttonIdTwo = 4;
			}
			AddButton(97, 161, 4023, 4023, buttonIdOne, GumpButtonType.Reply, 0);
			AddButton(97, 233, 4020, 4020, buttonIdTwo, GumpButtonType.Reply, 0);
		
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (m_Leader == null || m_Target == null)
                return;

            switch (info.ButtonID)
            {
                case 1:
				{
					PartyCommands.Handler.OnAccept(m_Target, m_Leader);
					break;
				}
                case 2:
				{
					PartyCommands.Handler.OnDecline(m_Target, m_Leader);
					break;
				}
				case 3:
					// teleport them to the soulbound character
					Point3D pointer = new Point3D(m_Target.X, m_Target.Y, m_Target.Z);
					Map map = m_Target.Map;
					m_Leader.MoveToWorld(pointer,map);
					Effects.PlaySound( m_Leader.Location, m_Leader.Map, 0x1FC );
					m_Leader.SendMessage("You teleport to your fallen party members new location.");
					m_Leader.PlaySound( 0x214 );
				break;
				case 4:
					// do nothing
					return;
				//break;
            }
        }
    }
}