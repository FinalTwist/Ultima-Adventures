using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;

namespace Server.Engines.Quests
{
	public class QuestButton
	{
		public static void Initialize()
		{
			EventSink.QuestGumpRequest += new QuestGumpRequestHandler( EventSink_QuestGumpRequest );
		}

		public static void EventSink_QuestGumpRequest( QuestGumpRequestArgs e )
		{
			Mobile from = e.Mobile;

			if (Misc.AdventuresFunctions.IsPuritain((object)from))
				return;

			from.CloseGump( typeof( StatsGump ) );
			from.SendGump( new StatsGump( from, 0 ) );
        }
	}
}