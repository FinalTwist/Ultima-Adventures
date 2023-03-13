using System;
using Server;
using Server.Items;
using Server.Network;
using Solaris.BoardGames;

namespace Server.Gumps
{
	//main gump class for boardgames
	public class BoardGameGump : Gump
	{
		public virtual int Height{ get{ return 100; } }
		public virtual int Width{ get{ return 100; } }
		
		//reference to the control system for the boardgame
		protected BoardGameControlItem _ControlItem;
		protected Mobile _Owner;
		
		public BoardGameGump( Mobile owner, BoardGameControlItem controlitem ) : base( 50, 50 )
		{
			_Owner = owner;
			_ControlItem = controlitem;
			
			_Owner.CloseGump( typeof( BoardGameGump ) );
			
			
			DrawBackground();
			
			
			
		}
		
		protected virtual void DrawBackground()
		{
			AddPage(0);
			
			//determine page layout, sizes, and what gets displayed where
			DeterminePageLayout();
			
			
			AddBackground( 0, 0, Width, Height, 9270 );
			AddImageTiled( 11, 10, Width - 22, Height - 20, 2624 );
			
			AddAlphaRegion( 11, 10, Width - 22, Height - 20 );
			
			
		}
		
		protected virtual void DeterminePageLayout()
		{
		}
		
		public void AddTextField( int x, int y, int width, int height, int index, string text )
		{
			AddImageTiled( x - 2, y - 2, width + 4, height + 4, 0xA2C );
			AddAlphaRegion( x -2, y - 2, width + 4, height + 4 );
			AddTextEntry( x + 2, y + 2, width - 4, height - 4, 1153, index, text );
		}
		
		public string GetTextField( RelayInfo info, int index )
		{
			TextRelay relay = info.GetTextEntry( index );
			return ( relay == null ? null : relay.Text.Trim() );
		}


		
		
		
		public override void OnResponse( NetState sender, RelayInfo info )
		{
		}
		
		
	}
	
}