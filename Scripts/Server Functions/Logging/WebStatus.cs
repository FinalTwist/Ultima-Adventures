using System;
using System.IO;
using System.Text;
using Server;
using Server.Network;
using Server.Guilds;
using Server.Accounting;
using Server.Mobiles;

namespace Server.Misc
{
	public class StatusPage : Timer
	{
		public static bool Enabled = true;

		public static void Initialize()
		{
			if ( Enabled )
				new StatusPage().Start();
		}

		public StatusPage() : base( TimeSpan.FromSeconds( 5.0 ), TimeSpan.FromSeconds( 60.0 ) )
		{
			Priority = TimerPriority.FiveSeconds;
		}

		private static string Encode( string input )
		{
			StringBuilder sb = new StringBuilder( input );

			sb.Replace( "&", "&amp;" );
			sb.Replace( "<", "&lt;" );
			sb.Replace( ">", "&gt;" );
			sb.Replace( "\"", "&quot;" );
			sb.Replace( "'", "&apos;" );

			return sb.ToString();
		}

		protected override void OnTick()
		{
			using ( StreamWriter op = new StreamWriter( "Info/online.php" ) )
			{
				foreach ( NetState state in NetState.Instances )
				{
					Mobile m = state.Mobile;

					if ( m != null && ( m.AccessLevel < AccessLevel.GameMaster ) )
					{
						op.Write( "<br>" );
						op.Write( Encode( m.Name ) );
						op.Write( " the " );
						op.Write( GetPlayerInfo.GetSkillTitle( m ) );
					}
				}
			}

			if ( LoggingFunctions.LoggingEvents() == true )
			{
				LoggingFunctions.LogClear( "Logging Murderers" );

				// GET ALL OF THE MURDERERS ///////////////////////////////
				foreach ( Account a in Accounts.GetAccounts() )
				{
					if (a == null)
						break;

					int index = 0;

					for (int i = 0; i < a.Length; ++i)
					{
						Mobile m = a[i];

						if (m == null)
							continue;

						if ( ( m.Kills > 0 ) && (m.AccessLevel < AccessLevel.GameMaster) )
						{
							LoggingFunctions.LogKillers( m, m.Kills );
						}

						++index;
					}
				}
			}
		}
	}
}