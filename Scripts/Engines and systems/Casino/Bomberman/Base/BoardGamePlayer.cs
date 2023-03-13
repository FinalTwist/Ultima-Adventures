using System;
using Server;
using Server.Accounting;

namespace Solaris.BoardGames
{
	//this class defines all data access for tracking player scores within boardgames
	public class BoardGamePlayer
	{
		
		//current plan is to use account tags.  
		//TODO: explore XML tags and such to better integrate with XML quest systems?
		
		//this sets the players score for a particular game type
		public static bool SetScore( Mobile mobile, string gametype, int score )
		{
			if( mobile.Account == null )
			{
				return false;
			}
			
			Account acct = (Account)mobile.Account;
			
			acct.SetTag( "BoardGames-" + gametype, score.ToString() );
			return true;
		}
		
		public static int GetScore( Mobile mobile, string gametype )
		{
			try
			{
				Account acct = (Account)mobile.Account;
				
				return Convert.ToInt32( acct.GetTag( "BoardGames-" + gametype ) );
			}
			catch
			{
				return -1;
			}
			
		}
	}
}