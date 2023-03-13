using System;
using Server.Network;
using Server.Gumps;  // UNIQUE NAMING SYSTEM

namespace Server.Misc
{
	public class LoginStats
	{
		public static void Initialize()
		{
			// Register our event handler
			EventSink.Login += new LoginEventHandler( EventSink_Login );
		}

		private static void EventSink_Login( LoginEventArgs args )
		{
			int userCount = NetState.Instances.Count;
			int itemCount = World.Items.Count;
			int mobileCount = World.Mobiles.Count;

			Mobile m = args.Mobile;

			if ( m.AccessLevel >= AccessLevel.GameMaster )
				m.SendMessage( "You can type '[helpadmin' to learn the commands for this server." );
			else
				m.SendMessage( "You can use the 'Help' button on your paperdoll for more information." );

			//Unique Naming System//
			#region CheckName
			if ( ( m.Name == CharacterCreation.GENERIC_NAME || !CharacterCreation.CheckDupe(m, m.Name) ) && m.AccessLevel < AccessLevel.GameMaster )
			{
				m.CantWalk = true;
				m.SendGump( new NameChangeGump( m) );
			}
			#endregion
			//Unique Naming System//
		}
	}
}