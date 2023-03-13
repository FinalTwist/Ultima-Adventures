using System;
using Server;
using Server.Items;
using Server.Network;
using Solaris.BoardGames;

namespace Server.Gumps
{
	//offers a new game to a player
	public class AwaitRecruitmentGump : BoardGameGump
	{
		
		public override int Height{ get{ return 200; } }
		public override int Width{ get{ return 400; } }
		
		public AwaitRecruitmentGump( Mobile owner, BoardGameControlItem controlitem ) : base( owner, controlitem )
		{
			//force it so players can't close this gump
			Closable = false;
			
			AddLabel( 40, 20, 1152, "Game:" );
			
			AddLabel( 140, 20, 1172, _ControlItem.GameName );
			
			AddHtml( 40, 50, Width - 80, 80, "You are waiting for more players to join this game.  When there are enough, this window will automatically close and the game will start.  If you wish to cancel waiting, click the Cancel button.", true, false );
			
			AddButton( 160, 160, 0xF1, 0xF2, 1, GumpButtonType.Reply, 0 );
		}
		
		protected override void DeterminePageLayout()
		{
		}
		
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			int buttonid = info.ButtonID;
			
			//cancel button
			if( buttonid == 1 )
			{
				_Owner.CloseGump( typeof( SelectStyleGump ) );
				_ControlItem.RemovePlayer( _Owner );
				
				_Owner.SendMessage( "You are no longer waiting to play this game." );
			}
		}
	}
}