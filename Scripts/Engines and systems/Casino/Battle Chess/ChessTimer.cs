using System;

using Server;

namespace Arya.Chess
{
	public class ChessTimer
	{
		private InternalTimer m_Timer;

		private DateTime m_LastMove = DateTime.MaxValue;
		private DateTime m_Disconnect = DateTime.MaxValue;
		private DateTime m_GameStart;
		private DateTime m_EndGame = DateTime.MaxValue;

		private ChessGame m_Game;

		public ChessTimer( ChessGame game )
		{
			m_Game = game;
			m_Timer = new InternalTimer( this );
			m_Timer.Start();

			m_GameStart = DateTime.UtcNow;
		}

		public void Stop()
		{
			if ( m_Timer != null && m_Timer.Running )
			{
				m_Timer.Stop();
				m_Timer = null;
			}
		}

		public void OnTick()
		{
			if ( m_Game == null )
			{
				m_Timer.Stop();
				return;
			}

			if ( m_GameStart != DateTime.MaxValue )
			{
				// Still starting the game
				if ( ( DateTime.UtcNow - m_GameStart ) >= ChessConfig.GameStartTimeOut )
				{
					m_Game.Cleanup();
				}
			}

			if ( m_EndGame != DateTime.MaxValue )
			{
				if ( ( DateTime.UtcNow - m_EndGame ) >= ChessConfig.EndGameTimerOut )
				{
					m_Game.Cleanup();
				}

				return; // Waiting for end game, don't bother with other checks
			}
			
			if ( m_LastMove != DateTime.MaxValue )
			{
				// Now playing
				if ( ( DateTime.UtcNow - m_LastMove ) >= ChessConfig.MoveTimeOut )
				{
					m_EndGame = DateTime.UtcNow;
					m_Game.OnMoveTimeout();
					return;
				}
			}

			if ( m_Disconnect != DateTime.MaxValue )
			{
				// A player has been disconnected
				if ( ( DateTime.UtcNow - m_Disconnect ) >= ChessConfig.DisconnectTimeOut )
				{
					m_Game.Cleanup();
				}
			}
		}

		public void OnGameStart()
		{
			m_GameStart = DateTime.MaxValue;
			m_LastMove = DateTime.UtcNow;
		}

		public void OnMoveMade()
		{
			m_LastMove = DateTime.UtcNow;
			m_EndGame = DateTime.MaxValue;
		}

		public void OnPlayerDisconnected()
		{
			if ( m_Disconnect == DateTime.MaxValue )
				m_Disconnect = DateTime.UtcNow;
		}

		public void OnPlayerConnected()
		{
			m_Disconnect = DateTime.MaxValue;
		}

		public void OnGameOver()
		{
			m_EndGame = DateTime.UtcNow;
		}

		private class InternalTimer : Timer
		{
			private ChessTimer m_Parent;

			public InternalTimer( ChessTimer parent ) : base( TimeSpan.FromSeconds( 15 ), TimeSpan.FromSeconds( 15 ) )
			{
				Priority = TimerPriority.FiveSeconds;
				m_Parent = parent;
			}

			protected override void OnTick()
			{
				if ( m_Parent != null )
					m_Parent.OnTick();
				else
					Stop();
			}
		}
	}
}
