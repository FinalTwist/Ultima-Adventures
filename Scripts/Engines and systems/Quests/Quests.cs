using System;
using Server;
using System.Collections.Generic;
using Server.Commands;
using Server.Mobiles;
using Server.Misc;
using Server.Gumps;

namespace Server.Items
{
	public class Quests
	{
        public static void Initialize()
        {
            CommandSystem.Register("quests", AccessLevel.Player, new CommandEventHandler( MyQuests_OnCommand ));
        }

		[Usage( "quests" )]
		[Description( "Opens Quest Gump." )]
        private static void MyQuests_OnCommand( CommandEventArgs e )
        {
			Mobile from = e.Mobile;
			from.CloseGump( typeof( QuestsGump ) );
			from.SendGump( new QuestsGump( from ) );
        }
    }
}