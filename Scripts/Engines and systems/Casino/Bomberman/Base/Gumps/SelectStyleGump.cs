using System;
using Server;
using Server.Items;
using Server.Network;
using Solaris.BoardGames;

namespace Server.Gumps
{
	public class SelectStyleGump : Gump
	{
		public virtual int Height{ get{ return 150; } }
		public virtual int Width{ get{ return 200; } }
		
		protected int _Y = 30;
		protected int _X = 20;
		
		protected BoardGameControlItem _ControlItem;
		
		public SelectStyleGump( Mobile owner, BoardGameControlItem controlitem ) : base( 450, 80 )
		{
			Closable = false;
			
			owner.CloseGump( typeof( SelectStyleGump ) );
			
			_ControlItem = controlitem;
			
			if( _ControlItem.Players.IndexOf( owner ) == -1 )
			{
				return;
			}
			
			AddPage( 0 );
			AddBackground( 0, 0, Width, Height, 0x1400 );
			
			AddLabel( 20, 60, 1152, "# of players (" + _ControlItem.MinPlayers.ToString() + "-" + _ControlItem.MaxPlayers.ToString() + "):" );
			
			int minplayers = Math.Max( _ControlItem.MinPlayers, _ControlItem.Players.Count );
			
			if( _ControlItem.MaxPlayers != _ControlItem.MinPlayers && !_ControlItem.SettingsReady )
			{
				AddLabel( 20, 40, 1172, "Pick the number of players" );
				AddTextField( 150, 60, 30, 20, 0, _ControlItem.CurrentMaxPlayers.ToString() );
				AddButton( 182, 62, 0x4B9, 0x4BA, 500, GumpButtonType.Reply, 0 );
			}
			else
			{
				AddLabel( 150, 60, 1152, _ControlItem.CurrentMaxPlayers.ToString() );
			}
			

			//AddButton( Width - 15, 0, 3, 4, 0, GumpButtonType.Reply, 0 );	
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
		
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			if( _ControlItem.Players.IndexOf( from ) != 0 )
			{
				return;
			}
			
			
			try
			{
				if( !_ControlItem.SettingsReady )
				{
					_ControlItem.CurrentMaxPlayers = Math.Max( Int32.Parse( GetTextField( info, 0 ) ), _ControlItem.Players.Count );
				
					if( _ControlItem.CurrentMaxPlayers > _ControlItem.MaxPlayers || _ControlItem.CurrentMaxPlayers < _ControlItem.MinPlayers )
					{
						throw( new Exception() );
					}
				}
			}
			catch
			{
				from.SendMessage( "Invalid number of players selected.  Please try again." );
			}
			
		}
	}
}
