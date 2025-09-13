using System;
using Server;
using Server.Items;
using Server.Network;
using Solaris.BoardGames;

namespace Server.Gumps
{
	public class ConfirmResetGameScoreGump : BoardGameGump
	{
		public override int Height{ get{ return 200; } }
		public override int Width{ get{ return 400; } }
		
		protected int _Y = 30;
		protected int _X = 20;
		
		public ConfirmResetGameScoreGump( Mobile owner, BoardGameControlItem controlitem ) : base( owner, controlitem )
		{
			AddLabel( 40, 20, 1152, "Game:" );
			
			AddLabel( 140, 20, 1172, _ControlItem.GameName );
			
			AddHtml( 40, 50, Width - 80, 80, "You are about to reset all the score data for " + _ControlItem.GameName + ".  Once you do this, it cannot be undone.  Are you sure you wish to do this?", true, false );
			
			AddButton( 30, 160, 0xF7, 0xF8, 1, GumpButtonType.Reply, 0 );
			
			AddButton( 160, 160, 0xF1, 0xF2, 0, GumpButtonType.Reply, 0 );
		}
		
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			
			if( info.ButtonID == 1 )
			{
				BoardGameData.ResetScores( _ControlItem.GameName );
				
				_Owner.SendMessage( "You have now reset all the scores." );
				
			}
		}
	}
}
