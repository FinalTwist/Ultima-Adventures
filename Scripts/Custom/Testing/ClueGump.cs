using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Accounting;
using Server.Mobiles;
using Server.Regions;
using Server.Commands;
using Server.Misc;

namespace Server.Gumps
{
	public class ClueGump : Gump
	{
		public ClueGump( string text, string title ) : base( 25, 25 )
		{
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 153);
			AddImage(2, 2, 163);
			AddImage(8, 9, 5553);
			AddButton(261, 9, 4017, 4017, 0, GumpButtonType.Reply, 0);
			AddHtml( 80, 40, 208, 26, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + title + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 11, 80, 277, 207, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + text + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
		}

        public override void OnResponse(NetState sender, RelayInfo info)
        {
        }
	}
}