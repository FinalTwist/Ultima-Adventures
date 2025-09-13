using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Solaris.BoardGames;

namespace Server.Gumps
{
	public class BoardGameScoresGump : BoardGameGump
	{
		public const int ENTRIES_PER_PAGE = 10;
		
		public override int Height{ get{ return 440; } }
		public override int Width{ get{ return 350; } }
		
		protected List<BoardGamePlayerScore> _PlayerScores;
		
		protected int _X;
		protected int _Y;
		
		//maximum entry listing height, for multi-page calculation
		public int MaxEntryDisplayHeight{ get{ return 300; } }
		
		//line spacing between entries
		public int EntryLineSpacing{ get{ return 20; } }
		
		protected int _Page;
		
		//this is determined based on the number of entries and the maximum number to display per page
		protected int _MaxPages;

		public BoardGameScoresGump( Mobile owner, BoardGameControlItem controlitem ) : this( owner, controlitem, 0 )
		{
		}
		
		public BoardGameScoresGump( Mobile owner, BoardGameControlItem controlitem, int page ) : base( owner, controlitem )
		{
			_Page = page;
			
			AddLabel( 40, 20, 1152, "Game:" );
			
			AddLabel( 140, 20, 1172, _ControlItem.GameName );
			
			AddLabel( 40, 50, 1152, "Scores" );
			
			_PlayerScores = BoardGameData.GetScores( controlitem.GameName );
			
			if( _PlayerScores == null || _PlayerScores.Count == 0 )
			{
				AddLabel( 40, 80, 1152, "- NO SCORES SET YET -" );
				return;
			}
			
			_PlayerScores.Sort();
			
			_X = 20;
			_Y = 80;
			
			_MaxPages = _PlayerScores.Count / ENTRIES_PER_PAGE + 1;
			
			if( _PlayerScores.Count % ENTRIES_PER_PAGE == 0 )
			{
				_MaxPages -= 1;
			}
			
			_Page = Math.Max( 0, Math.Min( _Page, _MaxPages ) );
			
			int listingstart = _Page * ENTRIES_PER_PAGE;
			int listingend = Math.Min( _PlayerScores.Count, (_Page + 1 ) * ENTRIES_PER_PAGE );
			
			AddLabel( _X, _Y, 1152, "Name" );
			AddLabel( _X + 150, _Y, 1152, "Score" );
			AddLabel( _X+ 200, _Y, 1152, "Wins" );
			AddLabel( _X + 250, _Y, 1152, "Losses" );
			
			for( int i = listingstart; i < listingend; i++ )
			{
				AddLabel( _X, _Y += 20, 1152, _PlayerScores[i].Player.Name );
				AddLabel( _X + 150, _Y, 1152, _PlayerScores[i].Score.ToString() );
				AddLabel( _X + 200, _Y, 1152, _PlayerScores[i].Wins.ToString() );
				AddLabel( _X + 250, _Y, 1152, _PlayerScores[i].Losses.ToString() );
			}
			
			AddPageButtons();
			
			AddButton( 60, Height - 40, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 );
		}
		
		protected void AddPageButtons()
		{
			//page buttons
			_Y = Height - 90;
			
			if ( _Page > 0 ) 
			{
				AddButton( 20, _Y, 0x15E3, 0x15E7, 4, GumpButtonType.Reply, 0 ); 
			}
			else 
			{
				AddImage( 20, _Y, 0x25EA ); 
			}
			AddLabel( 40, _Y, 88, "Previous Page" );
			
			
			if ( _Page < _MaxPages - 1 ) 
			{
				AddButton( Width - 40, _Y, 0x15E1, 0x15E5, 5, GumpButtonType.Reply, 0 ); 
			}
			else 
			{
				AddImage( Width - 40, _Y, 0x25E6 ); 
			}
			AddLabel( Width - 120, _Y, 88, "Next Page" );
			
			AddLabel( Width / 2 - 10, _Y, 88, String.Format( "({0}/{1})", _Page + 1, _MaxPages ) );
			
		}

		
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			int buttonid = info.ButtonID;
			
			if( buttonid == 4 )
			{
				_Owner.SendGump( new BoardGameScoresGump( _Owner, _ControlItem, _Page - 1 ) );
			}
			else if( buttonid == 5 )
			{
				_Owner.SendGump( new BoardGameScoresGump( _Owner, _ControlItem, _Page + 1 ) );
			}
		}
	}
}