using System;
using System.Collections.Generic;
using System.Reflection;
using Server;
using Server.ContextMenus;
using Server.Gumps;
using Solaris.BoardGames;

namespace Server.Items
{
	public enum BoardGameState
	{
		Disabled = 0,
		Inactive = 1,
		Pending = 2,
		Recruiting = 3,
		Active = 4,
		GameOver = 5
	}
	
	//the main control object for a boardgame system.  Each board game needs exactly one control object 
	public abstract class BoardGameControlItem : Item
	{
		public virtual string GameName{ get{ return "-UNDEFINED-"; } }
		public virtual string GameDescription{ get{ return "-UNDEFINED-"; } }
		public virtual string GameRules{ get{ return "-UNDEFINED-"; } }
		
		public virtual bool CanCastSpells{ get{ return true; } }
		public virtual bool CanUseSkills{ get{ return true; } }
		public virtual bool CanUsePets{ get{ return true; } }
		
		public virtual bool UseFromBackpack{ get{ return true; } }
		
		public virtual TimeSpan WinDelay{ get{ return TimeSpan.Zero; } }
		
		protected BoardGameState _State;
		
		protected WinnerTimer _WinnerTimer;
		protected EndGameTimer _EndGameTimer;
		
		//valid distance from the control item
		public virtual int UseRange{ get{ return 10; } }
		
		public virtual int MinPlayers{ get{ return 0; } }
		public virtual int MaxPlayers{ get{ return 0; } }
		
		public int CurrentMaxPlayers;
		
		protected int _CostToPlay;
		
		//the list of all players participating in the game
		protected List<Mobile> _Players;
		
		//the list of all players pending a decision when interacting with the boardgame controller
		protected List<Mobile> _PendingPlayers;
		
		//the defined region where the game is taking place
		public Rectangle3D GameZone;
		
		protected Point3D _BoardOffset;
		
		public Point3D BoardOffset{ get{ return _BoardOffset; } }
		
		protected bool _SettingsReady;
		
		public bool SettingsReady
		{
			get{ return _SettingsReady; }
			set
			{
				_SettingsReady = value;
				if( _SettingsReady && Players.Count == CurrentMaxPlayers && _State < BoardGameState.Active && _State != BoardGameState.Disabled )
				{
					InitializeGame();
				}
			}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D BoardLocation
		{
			get
			{
				return new Point3D( X + _BoardOffset.X, Y + _BoardOffset.Y, Z + _BoardOffset.Z );
			}
			set
			{
				Point3D location = value;
				_BoardOffset = new Point3D( location.X - X, location.Y - Y, location.Z - Z );
				UpdatePosition();
			}
		}
		
		protected Map _BoardMap;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Map BoardMap
		{
			get{ return _BoardMap; }
			set
			{
				_BoardMap = value;
				UpdatePosition();
			}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool ForceGameOver
		{
			get{ return false; }
			set
			{
				if( value )
				{
					EndGame();
				}
			}
		}
		
		protected bool _AllowPlayerConfiguration;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowPlayerConfiguration
		{ 
			get{ return _AllowPlayerConfiguration; } 
			set
			{ 
				_AllowPlayerConfiguration = value; 
				
				if( _State != BoardGameState.Recruiting )
				{
					_SettingsReady = !value;
				}
			}
		}
		
		
		
		//these hold the height and width of the game board
		protected int _BoardWidth;
		protected int _BoardHeight;

		[CommandProperty( AccessLevel.GameMaster )]
		public virtual int BoardWidth
		{
			get
			{
				return _BoardWidth;
			}
			set
			{
				
				if( (int)_State <= (int)BoardGameState.Recruiting )
				{
					_BoardWidth = value;
					ResetBoard();
				}
			}
		}
		
			
		[CommandProperty( AccessLevel.GameMaster )]
		public virtual int BoardHeight
		{
			get
			{
				return _BoardHeight;
			}
			set
			{
				if( (int)_State <= (int)BoardGameState.Recruiting )
				{
					_BoardHeight = value;
					ResetBoard();
				}
			}
		}
		
		//the list of all items used as background for the game board
		protected List<GamePiece> _BackgroundItems;
		
		protected BoardGameRegion _BoardGameRegion;

		
		
		[CommandProperty( AccessLevel.GameMaster )]
		public BoardGameState State
		{
			get
			{
				return _State; 
			}
			set
			{
				_State = value;
				InvalidateProperties();
			}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int CostToPlay
		{
			get{ return _CostToPlay; }
			set
			{
				_CostToPlay = value;
				InvalidateProperties();
			}
			
		}
		
				
		public List<Mobile> Players
		{
			get
			{
				if( _Players == null )
				{
					_Players = new List<Mobile>();
				}
				return _Players; 
			}
		}
		
		public List<Mobile> PendingPlayers
		{
			get
			{
				if( _PendingPlayers == null )
				{
					_PendingPlayers = new List<Mobile>();
				}
				return _PendingPlayers; 
			}
		}
		
		
		public List<GamePiece> BackgroundItems
		{
			get
			{
				if( _BackgroundItems == null )
				{
					_BackgroundItems = new List<GamePiece>();
				}
				return _BackgroundItems;
			}
		}
			
		
		
		//main constructor
		public BoardGameControlItem() : base( 4006 )		//default itemid 4006: checkerboard
		{
			_AllowPlayerConfiguration = true;
			CurrentMaxPlayers = MinPlayers;
			InitializeControl();

		}
		
		//deserialization constructor
		public BoardGameControlItem( Serial serial ) : base( serial )
		{
		}
		
		//this method initializes the game control and connects it with this item
		protected virtual void InitializeControl()
		{
			ResetBoard();
			Movable = UseFromBackpack;	
		}
		
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			
			list.Add( new ViewBoardGameScoresEntry( from, this, 1 ) );
			
			if( from.AccessLevel >= AccessLevel.GameMaster )
			{
				list.Add( new ResetBoardGameScoresEntry( from, this, 2 ) );
				
			}
		}

		
		
		//this method builds the game board
		public virtual void BuildBoard()
		{
		}
		
		//this resets the gameboard and refreshes it
		public virtual void ResetBoard()
		{
			WipeBoard();
			
			BuildBoard();
		}
		
		protected virtual void PrimePlayers()
		{
			foreach( Mobile player in Players )
			{
				
				player.CloseGump( typeof( AwaitRecruitmentGump ) );
				player.CloseGump( typeof( SelectStyleGump ) );
			}
		}
		
		public virtual void WipeBoard()
		{
			foreach( GamePiece piece in BackgroundItems )
			{
				piece.BoardGameControlItem = null;		//detach reference to this boardgame so that it doesn't wipe the board itself
				piece.Delete();
			}

			if( _BoardGameRegion != null )
			{
				_BoardGameRegion.Unregister();
			}
			
			_BackgroundItems = null;
		}
		
		
		//this moves the players into the board, gives them the equipment they need, and starts the game
		protected virtual void StartGame()
		{
			//define and clear the game field, then rebuild it
			ResetBoard();
			
			//move players into the board, give them game interface items, or otherwise get them set up
			PrimePlayers();
			
		}
		
		public virtual void EndGame()
		{
			if( _WinnerTimer != null )
			{
				_WinnerTimer.Stop();
				_WinnerTimer = null;
			}
			
			_PendingPlayers = null;
			_SettingsReady = !_AllowPlayerConfiguration;
			InvalidateProperties();
			
			foreach( Mobile player in Players )
			{
				player.CloseGump( typeof( BoardGameGump ) );
			}
		}
		
		protected void StartEndGameTimer( TimeSpan delay )
		{
			if( _EndGameTimer != null )
			{
				_EndGameTimer.Stop();
				
				_EndGameTimer = null;
			}
			
			_EndGameTimer = new EndGameTimer( this, delay );
			_EndGameTimer.Start();
		}

		//this triggers the actual engame detection
		protected virtual void OnEndGameTimer()
		{
			if( _EndGameTimer != null )
			{
				_EndGameTimer.Stop();
				_EndGameTimer = null;
			}
		}
		
		
		protected virtual void AnnounceWinner()
		{
			_State = BoardGameState.GameOver;
			if( _WinnerTimer == null )
			{
				_WinnerTimer = new WinnerTimer( this, WinDelay );
				_WinnerTimer.Start();
			}
		}
		
		
		//this method is called by the control item when a player doubleclicks it
		public override void OnDoubleClick( Mobile from )
		{
			if( CanUse( from ) )
			{
				OnUse( from );
			}
		}
		
		public virtual bool CanUse( Mobile from )
		{
			//if they've logged out
			if( from.NetState == null )
			{
				return false;
			}
			
			if( UseFromBackpack )
			{
				if( !IsChildOf( from.Backpack ) )
				{
					from.SendMessage( "This must be in your backpack to use." );
					return false;
				}
			}
			else
			{
				if( !from.InRange( this, UseRange ) )
				{
					from.SendMessage( "You are out of range." );
					return false;
				}
			}
			
			return CheckRequirements( from );
		}
		
		public virtual bool CheckRequirements( Mobile from )
		{
			if( !CanUsePets && from.Followers > 0 )
			{
				from.SendMessage( "You are not allowed to have pets in this game." );
				return false;
			}
			
			if( !CheckCost( from, false ) )
			{
				from.SendMessage( "You lack the gold to play this game." );
				return false;
			}
			return true;
		}
		
		//this checks for money, and withdraws it if necessary
		public bool CheckCost( Mobile from, bool withdraw )
		{
			if( CostToPlay == 0 )
			{
				return true;
			}
			
			Gold gold = (Gold)from.Backpack.FindItemByType( typeof( Gold ) );
			
			if( gold == null || gold.Amount < CostToPlay )
			{
				Container bankbox = from.FindBankNoCreate();
				
				if( bankbox != null )
				{
					gold = (Gold)bankbox.FindItemByType( typeof( Gold ) );
					
					if( gold != null && gold.Amount >= CostToPlay )
					{
						if( withdraw )
						{
							bankbox.ConsumeTotal( typeof( Gold ), CostToPlay );
						}
						return true;
					}
				}
				return false;
			}
			
			if( withdraw )
			{
				from.Backpack.ConsumeTotal( typeof( Gold ), CostToPlay );
			}
			
			return true;
		}
		
		
		//updates all game pieces position 
		public virtual void UpdatePosition()
		{
			if( _BoardMap == null || _BoardMap == Map.Internal )
			{
				_BoardMap = Map;
			}
			
			foreach( GamePiece piece in BackgroundItems )
			{
				piece.UpdatePosition();
			}
			
			if( _BoardGameRegion != null )
			{
				_BoardGameRegion.Unregister();
			}
			
			_BoardGameRegion = new BoardGameRegion( this );
			_BoardGameRegion.Register();
		}
		
		//mouse-over properties info
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1070722, "Status: " + Enum.GetName( typeof( BoardGameState ), _State ) ); //~1_NOTHING~
			list.Add( 1060658, "Cost to play\t{0}", CostToPlay.ToString() ); // ~1_val~: ~2_val~
		}

		
		
		//this method is called when a successful OnDoubleClick method is performed
		protected virtual void OnUse( Mobile from )
		{
			switch( _State )
			{
				case BoardGameState.Disabled:
				{
					DisabledGame( from );
					break;
				}
				case BoardGameState.Inactive:
				{
					OfferNewGame( from );
					break;
				}
				case BoardGameState.Pending:
				{
					GamePending( from );
					break;
				}
				case BoardGameState.Recruiting:
				{
					OfferRecruiting( from );
					break;
				}
				case BoardGameState.Active:
				{
					GameActive( from );
					break;
				}
				case BoardGameState.GameOver:
				{
					GameOver( from );
					break;	
				}
			}
			
		}
		
		protected virtual void DisabledGame( Mobile from )
		{
			if( from.AccessLevel < AccessLevel.GameMaster )
			{
				from.SendMessage( "That game has been disabled by staff." );
			}
		}
		
		protected virtual void OfferNewGame( Mobile from )
		{
			PendingPlayers.Add( from );
			
			State = BoardGameState.Pending;
			
			from.SendGump( new OfferNewGameGump( from, this, true ) );
		}
		
		protected virtual void GamePending( Mobile from )
		{
			from.SendMessage( "This game is pending use from another player.  Please try again later." );
		}
		
		protected virtual void OfferRecruiting( Mobile from )
		{
			if( PendingPlayers.IndexOf( from ) == -1 )
			{
				if( PendingPlayers.Count < CurrentMaxPlayers )
				{
					PendingPlayers.Add( from );
					from.SendGump( new OfferNewGameGump( from, this, false ) );
				}
				else
				{
					from.SendMessage( "This game has enough players attempting to start a game.  Please try again later." );
				}
			}
			else
			{
				from.SendGump( new AwaitRecruitmentGump( from, this ) );
			}
		}
		
		protected virtual void GameActive( Mobile from )
		{
			if( Players.IndexOf( from ) == -1 )
			{
				from.SendMessage( "A game is already in progess.  Please try again later." );
			}
		}
		
		protected virtual void GameOver( Mobile from )
		{
			from.SendMessage( "The last game has just ended.  Please try again later." );
		}
		
		//this is called by the recruitment gump when a player agrees to play the game
		public virtual void AddPlayer( Mobile from )
		{
			Players.Add( from );
			from.SendGump( new AwaitRecruitmentGump( from, this ) );
			
			if( Players.Count == CurrentMaxPlayers && SettingsReady )
			{
				InitializeGame();
			}
			else
			{
				State = BoardGameState.Recruiting;
			}
		}
		
		public void InitializeGame()
		{
			//perform final check on all players to make sure they're still good to play
				
			Mobile toboot = null;
			foreach( Mobile player in Players )
			{
				if( !CanUse( player ) )
				{
					player.SendMessage( "You can no longer enter the game, and have been removed from the list." );
					toboot = player;
					break;
				}
			}
				
			if( toboot != null )
			{
				if( Players.IndexOf( toboot ) == 0 )
				{
					_SettingsReady = !_AllowPlayerConfiguration;
				}
				RemovePlayer( toboot );
				return;
			}
				
			_PendingPlayers = null;
			State = BoardGameState.Active;
			
			foreach( Mobile player in Players )
			{
				CheckCost( player, true );
				player.SendMessage( "Game on!" );
			}
			
			//Start the game!
			StartGame();
		}
		
		//this is called by the await recruitment gump when the player chooses to cancel waiting
		public virtual void RemovePlayer( Mobile from )
		{
			from.CloseGump( typeof( AwaitRecruitmentGump ) );
			from.CloseGump( typeof( SelectStyleGump ) );
			
			Players.Remove( from );
			PendingPlayers.Remove( from );
			
			if( Players.Count == 0 )
			{
				State = BoardGameState.Inactive;
				_SettingsReady = !_AllowPlayerConfiguration;
			}
		}
		
				
		//these are used to update all movable addon components
		public override void OnLocationChange( Point3D oldLoc )
		{
			if ( Deleted )
			{
				return;
			}
			
			UpdatePosition();
		}
		
		//these are used to update all movable addon components		
		public override void OnMapChange()
		{
			if ( Deleted )
			{
				return;
			}
			
			UpdatePosition();
		}
		
		public override void Delete()
		{
			if( _WinnerTimer != null )
			{
				_WinnerTimer.Stop();
				_WinnerTimer = null;
			}
			if( _EndGameTimer != null )
			{
				_EndGameTimer.Stop();
				_EndGameTimer = null;
			}
			
			base.Delete();
		}

		//this cleans up all movable addon components, and removes the reference to this addon in the key item
		public override void OnAfterDelete()
		{
			base.OnAfterDelete();
			
			if( _WinnerTimer != null )
			{
				_WinnerTimer.Stop();
				_WinnerTimer = null;
			}

			foreach( GamePiece piece in BackgroundItems )
			{
				if( piece != null && !piece.Deleted )
				{
					piece.Delete();
				}
			}
			
			if( _BoardGameRegion != null )
			{
				_BoardGameRegion.Unregister();
			}

		}
		
		
		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 2 );
			
			writer.Write( BoardMap );
			
			writer.Write( _AllowPlayerConfiguration );
			
			writer.Write( (int)_State );
			writer.Write( _CostToPlay );
			
			writer.Write( CurrentMaxPlayers );
			
			writer.Write( _BoardWidth );
			writer.Write( _BoardHeight );
			
			writer.Write( GameZone.Start.X );
			writer.Write( GameZone.Start.Y );
			writer.Write( GameZone.Start.Z );
			writer.Write( GameZone.End.X );
			writer.Write( GameZone.End.Y );
			writer.Write( GameZone.End.Z );
			
			writer.Write( _BoardOffset.X );
			writer.Write( _BoardOffset.Y );
			writer.Write( _BoardOffset.Z );
			
			
			
			writer.Write( Players.Count );
			foreach( Mobile mobile in Players )
			{
				writer.Write( mobile );
			}
			
			writer.Write( BackgroundItems.Count );
			foreach( GamePiece piece in BackgroundItems )
			{
				writer.Write( (Item)piece );
			}
			
			
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			if( version >= 2 )
			{
				_BoardMap = reader.ReadMap();
			}
			else
			{
				_BoardMap = Map;
			}
			
			if( version >= 1 )
			{
				_AllowPlayerConfiguration = reader.ReadBool();
			}
			else
			{
				_AllowPlayerConfiguration = true;
			}
			
			State = (BoardGameState)reader.ReadInt();
			_CostToPlay = reader.ReadInt();
			
			CurrentMaxPlayers = reader.ReadInt();
			
			_BoardWidth = reader.ReadInt();
			_BoardHeight = reader.ReadInt();
			
			GameZone = new Rectangle3D( new Point3D( reader.ReadInt(), reader.ReadInt(), reader.ReadInt() ), new Point3D( reader.ReadInt(), reader.ReadInt(), reader.ReadInt() ) );
			
			
			_BoardOffset = new Point3D( reader.ReadInt(), reader.ReadInt(), reader.ReadInt() );
			
			int count = reader.ReadInt();
			for( int i = 0; i < count; i++ )
			{
				Players.Add( reader.ReadMobile() );
			}
			
			count = reader.ReadInt();
			for( int i = 0; i < count; i++ )
			{
				BackgroundItems.Add( (GamePiece)reader.ReadItem() );
			}
			
			if( _State == BoardGameState.Pending || _State == BoardGameState.Recruiting )
			{
				_State = BoardGameState.Inactive;
				_Players = null;
			}
			
			_BoardGameRegion = new BoardGameRegion( this );
			_BoardGameRegion.Register();

			_SettingsReady = !_AllowPlayerConfiguration;
		}
		
		protected class EndGameTimer : Timer
		{
			private BoardGameControlItem _ControlItem;

			public EndGameTimer( BoardGameControlItem controlitem, TimeSpan delay ) : base( delay, TimeSpan.FromSeconds( 1.0 ) )
			{
				_ControlItem = controlitem;
			}

			protected override void OnTick()
			{
				_ControlItem.OnEndGameTimer();
			}
		}
		
		//this timer delays the game a bit, so the winner can stand proud over the game
		protected class WinnerTimer : Timer
		{
			private BoardGameControlItem _ControlItem;

			public WinnerTimer( BoardGameControlItem controlitem, TimeSpan delay ) : base( delay, TimeSpan.FromSeconds( 1.0 ) )
			{
				_ControlItem = controlitem;
			}

			protected override void OnTick()
			{
				_ControlItem.EndGame();
				Stop();
			}
		}
		
	}
}