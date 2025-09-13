using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Server;
using Server.Accounting;
using Server.Commands;
using Server.Engines.Help;
using Server.Network;
using Server.Regions;
using Server.Mobiles;

namespace Server.Misc
{
	public enum PasswordProtection
	{
		None,
		Crypt,
		NewCrypt
	}

	public class AccountHandler
	{
		private static int MaxAccountsPerIP = 10;

		private static TimeSpan DeleteDelay = TimeSpan.FromDays( 7.0 );

		public static PasswordProtection ProtectPasswords = PasswordProtection.NewCrypt;

		private static AccessLevel m_LockdownLevel;

		public static AccessLevel LockdownLevel
		{
			get{ return m_LockdownLevel; }
			set{ m_LockdownLevel = value; }
		}

		public static bool AutoAccountCreation()
		{
			return true;
		}

		private static CityInfo[] StartingCities = new CityInfo[]
			{
				new CityInfo( "New Haven",	"New Haven Bank",	1150168, 3667,	2625,	0  ),
				new CityInfo( "Yew",		"The Empath Abbey",	1075072, 633,	858,	0  ),
				new CityInfo( "Minoc",		"The Barnacle",		1075073, 2476,	413,	15 ),
				new CityInfo( "Britain",	"The Wayfarer's Inn",	1075074, 1602,	1591,	20 ),
				new CityInfo( "Moonglow",	"The Scholars Inn",	1075075, 4408,	1168,	0  ),
				new CityInfo( "Trinsic",	"The Traveler's Inn",	1075076, 1845,	2745,	0  ),
				new CityInfo( "Jhelom",		"The Mercenary Inn",	1075078, 1374,	3826,	0  ),
				new CityInfo( "Skara Brae",	"The Falconer's Inn",	1075079, 618,	2234,	0  ),
				new CityInfo( "Vesper",		"The Ironwood Inn",	1075080, 2771,	976,	0  )
			};

		private static bool PasswordCommandEnabled = true;

		public static void Initialize()
		{
			EventSink.DeleteRequest += new DeleteRequestEventHandler( EventSink_DeleteRequest );
			EventSink.AccountLogin += new AccountLoginEventHandler( EventSink_AccountLogin );
			EventSink.GameLogin += new GameLoginEventHandler( EventSink_GameLogin );

			if ( PasswordCommandEnabled )
				CommandSystem.Register( "Password", AccessLevel.Player, new CommandEventHandler( Password_OnCommand ) );
		}

		[Usage( "Password <newPassword> <repeatPassword>" )]
		[Description( "Changes the password of the commanding players account." )]
		public static void Password_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			Account acct = from.Account as Account;

			if ( acct == null )
				return;

			IPAddress[] accessList = acct.LoginIPs;

			if ( accessList.Length == 0 )
				return;

			NetState ns = from.NetState;

			if ( ns == null )
				return;

			if ( e.Length == 0 )
			{
				from.SendMessage( "You must specify the new password." );
				return;
			}
			else if ( e.Length == 1 )
			{
				from.SendMessage( "To prevent potential typing mistakes, you must type the password twice. Use the format:" );
				from.SendMessage( "Password \"(newPassword)\" \"(repeated)\"" );
				return;
			}

			string pass = e.GetString( 0 );
			string pass2 = e.GetString( 1 );

			if ( pass != pass2 )
			{
				from.SendMessage( "The passwords do not match." );
				return;
			}

			bool isSafe = true;

			for ( int i = 0; isSafe && i < pass.Length; ++i )
				isSafe = ( pass[i] >= 0x20 && pass[i] < 0x80 );

			if ( !isSafe )
			{
				from.SendMessage( "That is not a valid password." );
				return;
			}

			try
			{
				acct.SetPassword( pass );
				from.SendMessage( "The password to your account has changed." );
			}
			catch
			{
			}
		}

		private static void EventSink_DeleteRequest( DeleteRequestEventArgs e )
		{
			NetState state = e.State;
			int index = e.Index;

			Account acct = state.Account as Account;

			if ( acct == null )
			{
				state.Dispose();
			}
			else if ( index < 0 || index >= acct.Length )
			{
				state.Send( new DeleteResult( DeleteResultType.BadRequest ) );
				state.Send( new CharacterListUpdate( acct ) );
			}
			else
			{
				Mobile m = acct[index];

				if ( m == null )
				{
					state.Send( new DeleteResult( DeleteResultType.CharNotExist ) );
					state.Send( new CharacterListUpdate( acct ) );
				}
				else if ( m.NetState != null )
				{
					state.Send( new DeleteResult( DeleteResultType.CharBeingPlayed ) );
					state.Send( new CharacterListUpdate( acct ) );
				}
				else if ( DateTime.UtcNow < (m.CreationTime + DeleteDelay) )
				{
					state.Send( new DeleteResult( DeleteResultType.CharTooYoung ) );
					state.Send( new CharacterListUpdate( acct ) );
				}
				else if ( m.AccessLevel == AccessLevel.Player && Region.Find( m.LogoutLocation, m.LogoutMap ).GetRegion( typeof( Jail ) ) != null )	//Don't need to check current location, if netstate is null, they're logged out
				{
					state.Send( new DeleteResult( DeleteResultType.BadRequest ) );
					state.Send( new CharacterListUpdate( acct ) );
				}
				else
				{
					Console.WriteLine( "Client: {0}: Deleting character {1} (0x{2:X})", state, index, m.Serial.Value );

					acct.Comments.Add( new AccountComment( "System", String.Format( "Character #{0} {1} deleted by {2}", index + 1, m, state ) ) );

					m.Delete();

					state.Send( new CharacterListUpdate( acct ) );
				}
			}
		}

		public static bool CanCreate( IPAddress ip )
		{
			if ( !IPTable.ContainsKey( ip ) )
				return true;

			return ( IPTable[ip] < MaxAccountsPerIP );
		}

		private static Dictionary<IPAddress, Int32> m_IPTable;

		public static Dictionary<IPAddress, Int32> IPTable
		{
			get
			{
				if ( m_IPTable == null )
				{
					m_IPTable = new Dictionary<IPAddress, Int32>();

					foreach ( Account a in Accounts.GetAccounts() )
						if ( a.LoginIPs.Length > 0 )
						{
							IPAddress ip = a.LoginIPs[0];

							if ( m_IPTable.ContainsKey( ip ) )
								m_IPTable[ip]++;
							else
								m_IPTable[ip] = 1;
						}
				}

				return m_IPTable;
			}
		}	

		private static Account CreateAccount( NetState state, string un, string pw )
		{
			if ( un.Length == 0 || pw.Length == 0 )
				return null;

			bool isSafe = true;

			for ( int i = 0; isSafe && i < un.Length; ++i )
				isSafe = ( un[i] >= 0x20 && un[i] < 0x80 );

			for ( int i = 0; isSafe && i < pw.Length; ++i )
				isSafe = ( pw[i] >= 0x20 && pw[i] < 0x80 );

			if ( !isSafe )
				return null;

			if ( !CanCreate( state.Address ) )
			{
				Console.WriteLine( "Login: {0}: Account '{1}' not created, ip already has {2} account{3}.", state, un, MaxAccountsPerIP, MaxAccountsPerIP == 1 ? "" : "s" );
				return null;
			}

			Console.WriteLine( "Login: {0}: Creating new account '{1}'", state, un );

			Account a = new Account( un, pw );

			return a;
		}

		public static void EventSink_AccountLogin( AccountLoginEventArgs e )
		{
			if ( !IPLimiter.SocketBlock && !IPLimiter.Verify( e.State.Address ) )
			{
				e.Accepted = false;
				e.RejectReason = ALRReason.InUse;

				Console.WriteLine( "Login: {0}: Past IP limit threshold", e.State );

				using ( StreamWriter op = new StreamWriter( "ipLimits.log", true ) )
					op.WriteLine( "{0}\tPast IP limit threshold\t{1}", e.State, DateTime.UtcNow );

				return;
			}

			string un = e.Username;
			string pw = e.Password;

			e.Accepted = false;
			Account acct = Accounts.GetAccount( un ) as Account;

			if ( acct == null )
			{
				if ( AutoAccountCreation() && un.Trim().Length > 0 )	//To prevent someone from making an account of just '' or a bunch of meaningless spaces 
				{
					e.State.Account = acct = CreateAccount( e.State, un, pw );
					e.Accepted = acct == null ? false : acct.CheckAccess( e.State );

					if ( !e.Accepted )
						e.RejectReason = ALRReason.BadComm;
				}
				else
				{
					Console.WriteLine( "Login: {0}: Invalid username '{1}'", e.State, un );
					e.RejectReason = ALRReason.Invalid;
				}
			}
			else if ( !acct.HasAccess( e.State ) )
			{
				Console.WriteLine( "Login: {0}: Access denied for '{1}'", e.State, un );
				e.RejectReason = ( m_LockdownLevel > AccessLevel.Player ? ALRReason.BadComm : ALRReason.BadPass );
			}
			else if ( !acct.CheckPassword( pw ) )
			{
				Console.WriteLine( "Login: {0}: Invalid password for '{1}'", e.State, un );
				e.RejectReason = ALRReason.BadPass;
			}
			else if ( acct.Banned )
			{
				Console.WriteLine( "Login: {0}: Banned account '{1}'", e.State, un );
				e.RejectReason = ALRReason.Blocked;
			}
			else
			{
				Console.WriteLine( "Login: {0}: Valid credentials for '{1}'", e.State, un );
				e.State.Account = acct;
				e.Accepted = true;

				acct.LogAccess( e.State );
			}

			if ( !e.Accepted )
				AccountAttackLimiter.RegisterInvalidAccess( e.State );
		}

		public static void EventSink_GameLogin( GameLoginEventArgs e )
		{
			if ( !IPLimiter.SocketBlock && !IPLimiter.Verify( e.State.Address ) )
			{
				e.Accepted = false;

				Console.WriteLine( "Login: {0}: Past IP limit threshold", e.State );

				using ( StreamWriter op = new StreamWriter( "ipLimits.log", true ) )
					op.WriteLine( "{0}\tPast IP limit threshold\t{1}", e.State, DateTime.UtcNow );

				return;
			}

			string un = e.Username;
			string pw = e.Password;

			Account acct = Accounts.GetAccount( un ) as Account;

			if ( acct == null )
			{
				e.Accepted = false;
			}
			else if ( !acct.HasAccess( e.State ) )
			{
				Console.WriteLine( "Login: {0}: Access denied for '{1}'", e.State, un );
				e.Accepted = false;
			}
			else if ( !acct.CheckPassword( pw ) )
			{
				Console.WriteLine( "Login: {0}: Invalid password for '{1}'", e.State, un );
				e.Accepted = false;
			}
			else if ( acct.Banned )
			{
				Console.WriteLine( "Login: {0}: Banned account '{1}'", e.State, un );
				e.Accepted = false;
			}
			else
			{
				acct.LogAccess( e.State );

				Console.WriteLine( "Login: {0}: Account '{1}' at character list", e.State, un );
				e.State.Account = acct;
				e.Accepted = true;
				e.CityInfo = StartingCities;
			}

			if ( !e.Accepted )
				AccountAttackLimiter.RegisterInvalidAccess( e.State );
		}
	}
}