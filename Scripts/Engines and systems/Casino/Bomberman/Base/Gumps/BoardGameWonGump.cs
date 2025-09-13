using System;
using Server;
using Server.Items;
using Server.Network;
using Solaris.BoardGames;

namespace Server.Gumps
{
	public class BoardGameWonGump : BoardGameGump
	{
		public override int Height{ get{ return 140; } }
		public override int Width{ get{ return 300; } }
		
		public BoardGameWonGump( Mobile owner, BoardGameControlItem controlitem ) : base( owner, controlitem )
		{
			AddLabel( 40, 20, 1152, "Game:" );
			
			AddLabel( 140, 20, 1172, _ControlItem.GameName );
			
			AddLabel( 40, 50, 1152, "Congratulations, you won the game!!" );
			
			//TODO: add info about points earned?
			
			AddButton( 100, 80, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 );
		}
		
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			_ControlItem.EndGame();
		}
	}
}