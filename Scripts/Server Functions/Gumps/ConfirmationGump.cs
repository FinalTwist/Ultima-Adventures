using System;
using Server.Mobiles;

namespace Server.Gumps
{
	public class ConfirmationGump : Gump
	{
		private readonly Action OnConfirmed;

		public ConfirmationGump( Mobile from, string message, Action onConfirmed ) : base( 25, 25 )
		{
            OnConfirmed = onConfirmed;

			from.CloseGump( typeof( ConfirmReleaseGump ) );

			AddPage(0);
			AddImage(0, 0, 154);
			AddImage(181, 0, 154);
			AddImage(2, 2, 163);
			AddImage(183, 2, 163);
			AddImage(7, 8, 145);
			AddImage(282, 9, 146);
			AddImage(202, 9, 156);
			AddImage(167, 7, 156);
			AddImage(184, 10, 156);
			AddHtml( 97, 152, 322, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + message + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(100, 200, 4005, 4005, 2, GumpButtonType.Reply, 0);
			AddHtml( 137, 200, 58, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Yes</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(308, 200, 4020, 4020, 1, GumpButtonType.Reply, 0);
			AddHtml( 346, 200, 58, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>No</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddImage(278, 20, 156);
			AddImage(296, 21, 156);
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			if ( info.ButtonID == 2 )
			{
                OnConfirmed();
			}
		}
	}
}