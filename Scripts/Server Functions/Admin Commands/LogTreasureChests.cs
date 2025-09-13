using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using System.IO;

namespace Server.Misc
{
    class LogTreasureChests
    {
		public static void Initialize()
		{
            CommandSystem.Register( "logchests", AccessLevel.Administrator, new CommandEventHandler( LogChests_OnCommand ) );
		}
		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "logchests" )]
		[Description( "Logs the location of all the treasure chests in the world." )]
		public static void LogChests_OnCommand( CommandEventArgs e )
        {
			Mobile from = e.Mobile;

			if ( !Directory.Exists( "Info" ) )
				Directory.CreateDirectory( "Info" );

			string sPath = "Info/treasurechests.txt";
			string FoundBox = "";
			int nCount = 0;

			Region reg = null;
			ArrayList chests = new ArrayList();
			foreach ( Item chest in World.Items.Values )
			if ( chest is DungeonChest )
			{
				chests.Add( chest );
			}
			using (StreamWriter writer = new StreamWriter( sPath ))
			{
				for ( int i = 0; i < chests.Count; ++i )
				{
					nCount++;
					Item box = ( Item )chests[ i ];
					string sRegion = Worlds.GetMyRegion( box.Map, box.Location );
					string sMap = Worlds.GetMyWorld( box.Map, box.Location, box.X, box.Y );
					FoundBox = sMap + "_" + box.X.ToString() + "_" + box.Y.ToString() + "_" + box.Z.ToString() + "_" + sRegion + "\n" + FoundBox;
				}
				writer.WriteLine( FoundBox );
			}

			from.SendMessage( "You have exported " + nCount.ToString() + " treasure chests." );
		}
	}
}