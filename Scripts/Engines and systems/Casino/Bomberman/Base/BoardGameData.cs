using System;
using System.IO;
using System.Collections.Generic;
using Server;

namespace Solaris.BoardGames
{
	//this data class is used to keep track of player scores for various boardgames
	public class BoardGameData
	{
		public const string SAVE_PATH = @"Saves/BoardGame Data";
		public const string FILENAME = "boardgames.bin";
		
		protected static List<BoardGameData> _GameData;
		
		public static List<BoardGameData> GameData
		{
			get
			{
				if( _GameData == null )
				{
					_GameData = new List<BoardGameData>();
				}
				return _GameData;
			}
		}
		
		
		protected string _GameName;
		protected List<BoardGamePlayerScore> _Scores;
		
		public string GameName{ get{ return _GameName; } }
		
		public List<BoardGamePlayerScore> Scores
		{
			get
			{
				if( _Scores == null )
				{
					_Scores = new List<BoardGamePlayerScore>();
				}
				return _Scores;
			}
		}
		
		protected BoardGameData( string gamename )
		{
			_GameName = gamename;
		}
		
		protected BoardGameData( GenericReader reader )
		{
			Deserialize( reader );
		}
		
		protected virtual void Serialize( GenericWriter writer )
		{
			writer.Write( 0 );
			
			writer.Write( _GameName );
			
			writer.Write( Scores.Count );
			
			foreach( BoardGamePlayerScore score in Scores )
			{
				score.Serialize( writer );
			}
		}
		
		protected virtual void Deserialize( GenericReader reader )
		{
			int version = reader.ReadInt();
			
			_GameName = reader.ReadString();
			
			int count = reader.ReadInt();
			
			for( int i = 0; i < count; i++ )
			{
				BoardGamePlayerScore playerscore = new BoardGamePlayerScore( reader );
				
				if( playerscore.Player != null && !playerscore.Player.Deleted )
				{
					Scores.Add( playerscore );
				}
			}
		}
		
		protected static BoardGamePlayerScore GetScoreData( string gamename, Mobile player )
		{
			List<BoardGamePlayerScore> scores = GetScores( gamename );
			
			if( scores == null )
			{
				BoardGameData gamedata = new BoardGameData( gamename );
				GameData.Add( gamedata );
				scores = gamedata.Scores;
			}
		
			int index = BoardGamePlayerScore.IndexOf( scores, player );
			
			if( index == -1 )
			{
				BoardGamePlayerScore newscore = new BoardGamePlayerScore( player );
				scores.Add( newscore );
				return newscore;
			}
			else
			{
				return scores[ index ];
			}
		}
		
		public static List<BoardGamePlayerScore> GetScores( string gamename )
		{
			int gameindex = IndexOf( gamename );
			
			if( gameindex == -1 )
			{
				return null;
			}
			else
			{
				return GameData[ gameindex ].Scores;
			}
		}
		
		public static void SetScore( string gamename, Mobile player, int score )
		{
			BoardGamePlayerScore scoredata = GetScoreData( gamename, player );
			
			if( scoredata != null )
			{
				scoredata.Score = score;
			}
			else
			{
				
			}
		}
		
		public static int GetScore( string gamename, Mobile player )
		{
			BoardGamePlayerScore scoredata = GetScoreData( gamename, player );
			
			if( scoredata != null )
			{
				return scoredata.Score;
			}
			else
			{
				return 0;
			}
		}
		
		public static void ChangeScore( string gamename, Mobile player, int delta )
		{
			SetScore( gamename, player, Math.Max( 0, GetScore( gamename, player ) + delta ) );
		}
		
		public static void AddWin( string gamename, Mobile player )
		{
			BoardGamePlayerScore playerscore = GetScoreData( gamename, player );
			
			playerscore.Wins += 1;
		}
		
		public static void AddLose( string gamename, Mobile player )
		{
			BoardGamePlayerScore playerscore = GetScoreData( gamename, player );
			
			playerscore.Losses += 1;
			
		}
		
		public static void ResetScores( string gamename )
		{
			int gameindex = IndexOf( gamename );
			
			if( gameindex > -1 )
			{
				GameData.RemoveAt( gameindex );
			}
		}
		
		
		public static int IndexOf( string gamename )
		{
			for( int i = 0; i < GameData.Count; i++ )
			{
				if( GameData[i].GameName == gamename )
				{
					return i;
				}
			}
			return -1;
		}
		
		
		public static void Configure()
		{
			EventSink.WorldLoad += new WorldLoadEventHandler( OnLoad );
			EventSink.WorldSave += new WorldSaveEventHandler( OnSave );
		}
		
		public static void OnSave( WorldSaveEventArgs e )
		{
			if( !Directory.Exists( SAVE_PATH ) )
			{
				Directory.CreateDirectory( SAVE_PATH );
			}

			GenericWriter writer = new BinaryFileWriter( Path.Combine( SAVE_PATH, FILENAME ), true );

			writer.Write( 0 );
			
			writer.Write( GameData.Count );
			
			foreach( BoardGameData data in GameData )
			{
				data.Serialize( writer );
			}
			
			writer.Close();
			
		}
		
		public static void OnLoad()
		{
			//don't load the file if it don't exist!
			if( !File.Exists( Path.Combine( SAVE_PATH, FILENAME ) ) )
			{
				return;
			}

			using( FileStream bin = new FileStream( Path.Combine( SAVE_PATH, FILENAME ), FileMode.Open, FileAccess.Read, FileShare.Read ) )
			{
				GenericReader reader = new BinaryFileReader( new BinaryReader( bin ) );
				
				int version = reader.ReadInt();
				
				int count = reader.ReadInt();

				for( int i = 0; i < count; i++ )
				{
					GameData.Add( new BoardGameData( reader ) );
				}
				
				reader.End();
			}
		}
		
	}
	
	public class BoardGamePlayerScore : IComparable
	{
		protected Mobile _Player;
		
		
		public Mobile Player{ get{ return _Player; } }
		
		public int Score;
		public int Wins;
		public int Losses;
		
		public BoardGamePlayerScore( Mobile player ) : this( player, 0 )
		{
		}
		
		public BoardGamePlayerScore( Mobile player, int score )
		{
			_Player = player;
			Score = score;
		}
		
		//deserialize constructor
		public BoardGamePlayerScore( GenericReader reader )
		{
			Deserialize( reader );
		}
		
		public int CompareTo( object obj )
		{
			if( !( obj is BoardGamePlayerScore ) )
			{
				return 0;
			}
			
			BoardGamePlayerScore comparescore = (BoardGamePlayerScore)obj;
			
			return -Score.CompareTo( comparescore.Score );
		}
		
		public virtual void Serialize( GenericWriter writer )
		{
			writer.Write( 0 );
			
			writer.Write( _Player );
			writer.Write( Score );
			writer.Write( Wins );
			writer.Write( Losses );
			
		}
		
		public virtual void Deserialize( GenericReader reader )
		{
			int version = reader.ReadInt();
			
			_Player = reader.ReadMobile();
			Score = reader.ReadInt();
			Wins = reader.ReadInt();
			Losses = reader.ReadInt();
		}
		
		public static int IndexOf( List<BoardGamePlayerScore> scores, Mobile player )
		{
			if( scores == null )
			{
				return -1;
			}
			
			for( int i = 0; i < scores.Count; i++ )
			{
				if( scores[i].Player == player )
				{
					return i;
				}
			}
			
			return -1;
		}
	}
}
