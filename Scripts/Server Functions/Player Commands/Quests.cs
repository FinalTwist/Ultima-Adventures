using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
    public class QuestsGump : Gump
    {
        public QuestsGump( Mobile from ) : base( 25, 25 )
        {
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(300, 300, 153);
			AddImage(0, 300, 153);
			AddImage(0, 0, 153);
			AddImage(300, 0, 153);
			AddImage(600, 0, 153);
			AddImage(600, 300, 153);
			AddImage(2, 2, 129);
			AddImage(302, 2, 129);
			AddImage(2, 298, 129);
			AddImage(598, 2, 129);
			AddImage(598, 298, 129);
			AddImage(301, 298, 129);
			AddImage(8, 7, 133);
			AddImage(28, 367, 128);
			AddImage(238, 46, 132);
			AddImage(373, 46, 132);
			AddImage(678, 7, 134);
			AddImage(432, 538, 130);
			AddImage(836, 338, 131);
			AddImage(537, 538, 130);

			AddHtml( 109, 80, 716, 349, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + Server.Engines.Help.HelpGump.MyQuests( from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);

			AddHtml( 507, 471, 300, 29, @"<BODY><BASEFONT Color=#FBFBFB><BIG>QUESTS & DISCOVERIES</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
        }
    }
}