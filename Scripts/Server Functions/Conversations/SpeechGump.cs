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
	public class SpeechGump : Gump
	{
        public SpeechGump( string sTitle, string sText ) : base( 25, 25 )
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(500, 300, 155);
			AddImage(500, 0, 155);
			AddImage(0, 0, 155);
			AddImage(300, 0, 155);
			AddImage(0, 300, 155);
			AddImage(300, 300, 155);
			AddImage(2, 2, 129);
			AddImage(300, 2, 129);
			AddImage(2, 298, 129);
			AddImage(300, 298, 129);
			AddImage(7, 8, 145);
			AddImage(167, 20, 132);
			AddImage(94, 535, 130);
			AddImage(498, 2, 129);
			AddImage(466, 20, 132);
			AddHtml( 175, 47, 544, 26, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + sTitle + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddImage(498, 298, 129);
			AddImage(388, 364, 136);
			AddImage(758, 17, 143);
			AddImage(81, 518, 141);
			AddImage(6, 345, 148);
			AddHtml( 175, 89, 545, 329, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			AddImage(353, 524, 156);
			AddImage(346, 527, 156);
			AddImage(349, 529, 159);
        }
    }
}