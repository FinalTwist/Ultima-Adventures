using System;
using System.IO;
using Server;
using Server.Commands;

namespace Joeku.MOTD
{
	public class MOTD_Main
	{
		public const int Version = 100;
		public const string ReleaseDate = "September 1st, 2012";

		public static readonly string FilePath = Path.Combine( Core.BaseDirectory, @"Info" );
		public static MOTD_Info[] Info = new MOTD_Info[]
		{
			new MOTD_Info( "News" ),
		};
		public static MOTD_HelpInfo[] HelpInfo = new MOTD_HelpInfo[]
		{
			new MOTD_HelpInfo( "Help" ),
			new MOTD_HelpInfo( "Preferences" )
		};

		public static void Initialize()
		{
			EventSink.Login += new LoginEventHandler( MOTD_Utility.EventSink_OnLogin );
			CommandSystem.Register( "MOTD", AccessLevel.Player, new CommandEventHandler( MOTD_Utility.EventSink_OnCommand ) );
			MOTD_Utility.CheckFiles( false );
		}
	}
}
