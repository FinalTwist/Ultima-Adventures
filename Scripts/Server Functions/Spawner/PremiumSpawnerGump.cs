//////////////////
// Engine 5.2.5 //
//////////////////
using System;
using System.Collections;
using Server.Network;
using Server.Gumps;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public class PremiumSpawnerGump : Gump
	{
		private PremiumSpawner m_Spawner;

		public void AddBlackAlpha( int x, int y, int width, int height )
		{
			AddImageTiled( x, y, width, height, 2624 );
			AddAlphaRegion( x, y, width, height );
		}

		public PremiumSpawnerGump( PremiumSpawner spawner ) : base( 50, 50 )
		{
			m_Spawner = spawner;

			AddPage( 1 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 1" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 100, GumpButtonType.Reply, 0 );
			AddLabel( 260, 60, 52, "Okay" );

			AddButton( 260, 90, 0xFB4, 0xFB6, 200, GumpButtonType.Reply, 0 );
			AddLabel( 232, 110, 52, "Bring to Home" );

			AddButton( 260, 140, 0xFA8, 0xFAA, 300, GumpButtonType.Reply, 0 );
			AddLabel( 232, 160, 52, "Total Respawn" );

			AddButton( 260, 190, 0xFAB, 0xFAD, 400, GumpButtonType.Reply, 0 );
			AddLabel( 245, 210, 52, "Properties" );

			AddButton( 260, 240, 0xFB1, 0xFB3, 0, GumpButtonType.Reply, 0 );
			AddLabel( 256, 260, 52, "Cancel" );

			AddButton( 230, 320, 5603, 5607, 0, GumpButtonType.Page, 6 );
			AddButton( 302, 320, 5601, 5605, 0, GumpButtonType.Page, 2 );
			AddLabel( 258, 320, 52, "- 1 -" );

			for ( int i = 0;  i < 15; i++ )
			{
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, 4 + (i * 2), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, 5 + (i * 2), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.CreaturesName.Count )
				{
					str = (string)spawner.CreaturesName[i];
					int count = m_Spawner.CountCreatures( str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 100 + i, str );
			}

			AddPage( 2 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 2" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 101, GumpButtonType.Reply, 0 );
			AddLabel( 260, 60, 52, "Okay" );

			AddButton( 260, 90, 0xFB4, 0xFB6, 200, GumpButtonType.Reply, 0 );
			AddLabel( 232, 110, 52, "Bring to Home" );

			AddButton( 260, 140, 0xFA8, 0xFAA, 300, GumpButtonType.Reply, 0 );
			AddLabel( 232, 160, 52, "Total Respawn" );

			AddButton( 260, 190, 0xFAB, 0xFAD, 400, GumpButtonType.Reply, 0 );
			AddLabel( 245, 210, 52, "Properties" );

			AddButton( 260, 240, 0xFB1, 0xFB3, 0, GumpButtonType.Reply, 0 );
			AddLabel( 256, 260, 52, "Cancel" );

			AddButton( 230, 320, 5603, 5607, 0, GumpButtonType.Page, 1 );
			AddButton( 302, 320, 5601, 5605, 0, GumpButtonType.Page, 3 );
			AddLabel( 258, 320, 52, "- 2 -" );

			for ( int i = 0;  i < 15; i++ )
			{
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, 4 + (i * 2), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, 5 + (i * 2), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.SubSpawnerA.Count )
				{
					str = (string)spawner.SubSpawnerA[i];
					int count = m_Spawner.CountCreaturesA( str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 200 + i, str );
			}

			AddPage( 3 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 3" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 102, GumpButtonType.Reply, 0 );
			AddLabel( 260, 60, 52, "Okay" );

			AddButton( 260, 90, 0xFB4, 0xFB6, 200, GumpButtonType.Reply, 0 );
			AddLabel( 232, 110, 52, "Bring to Home" );

			AddButton( 260, 140, 0xFA8, 0xFAA, 300, GumpButtonType.Reply, 0 );
			AddLabel( 232, 160, 52, "Total Respawn" );

			AddButton( 260, 190, 0xFAB, 0xFAD, 400, GumpButtonType.Reply, 0 );
			AddLabel( 245, 210, 52, "Properties" );

			AddButton( 260, 240, 0xFB1, 0xFB3, 0, GumpButtonType.Reply, 0 );
			AddLabel( 256, 260, 52, "Cancel" );

			AddButton( 230, 320, 5603, 5607, 0, GumpButtonType.Page, 2 );
			AddButton( 302, 320, 5601, 5605, 0, GumpButtonType.Page, 4 );
			AddLabel( 258, 320, 52, "- 3 -" );

			for ( int i = 0;  i < 15; i++ )
			{
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, 4 + (i * 2), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, 5 + (i * 2), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.SubSpawnerB.Count )
				{
					str = (string)spawner.SubSpawnerB[i];
					int count = m_Spawner.CountCreaturesB( str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 300 + i, str );
			}

			AddPage( 4 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 4" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 103, GumpButtonType.Reply, 0 );
			AddLabel( 260, 60, 52, "Okay" );

			AddButton( 260, 90, 0xFB4, 0xFB6, 200, GumpButtonType.Reply, 0 );
			AddLabel( 232, 110, 52, "Bring to Home" );

			AddButton( 260, 140, 0xFA8, 0xFAA, 300, GumpButtonType.Reply, 0 );
			AddLabel( 232, 160, 52, "Total Respawn" );

			AddButton( 260, 190, 0xFAB, 0xFAD, 400, GumpButtonType.Reply, 0 );
			AddLabel( 245, 210, 52, "Properties" );

			AddButton( 260, 240, 0xFB1, 0xFB3, 0, GumpButtonType.Reply, 0 );
			AddLabel( 256, 260, 52, "Cancel" );

			AddButton( 230, 320, 5603, 5607, 0, GumpButtonType.Page, 3 );
			AddButton( 302, 320, 5601, 5605, 0, GumpButtonType.Page, 5 );
			AddLabel( 258, 320, 52, "- 4 -" );

			for ( int i = 0;  i < 15; i++ )
			{
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, 4 + (i * 2), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, 5 + (i * 2), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.SubSpawnerC.Count )
				{
					str = (string)spawner.SubSpawnerC[i];
					int count = m_Spawner.CountCreaturesC( str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 400 + i, str );
			}

			AddPage( 5 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 5" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 104, GumpButtonType.Reply, 0 );
			AddLabel( 260, 60, 52, "Okay" );

			AddButton( 260, 90, 0xFB4, 0xFB6, 200, GumpButtonType.Reply, 0 );
			AddLabel( 232, 110, 52, "Bring to Home" );

			AddButton( 260, 140, 0xFA8, 0xFAA, 300, GumpButtonType.Reply, 0 );
			AddLabel( 232, 160, 52, "Total Respawn" );

			AddButton( 260, 190, 0xFAB, 0xFAD, 400, GumpButtonType.Reply, 0 );
			AddLabel( 245, 210, 52, "Properties" );

			AddButton( 260, 240, 0xFB1, 0xFB3, 0, GumpButtonType.Reply, 0 );
			AddLabel( 256, 260, 52, "Cancel" );

			AddButton( 230, 320, 5603, 5607, 0, GumpButtonType.Page, 4 );
			AddButton( 302, 320, 5601, 5605, 0, GumpButtonType.Page, 6 );
			AddLabel( 258, 320, 52, "- 5 -" );

			for ( int i = 0;  i < 15; i++ )
			{
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, 4 + (i * 2), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, 5 + (i * 2), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.SubSpawnerD.Count )
				{
					str = (string)spawner.SubSpawnerD[i];
					int count = m_Spawner.CountCreaturesD( str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 500 + i, str );
			}

			AddPage( 6 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 6" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 105, GumpButtonType.Reply, 0 );
			AddLabel( 260, 60, 52, "Okay" );

			AddButton( 260, 90, 0xFB4, 0xFB6, 200, GumpButtonType.Reply, 0 );
			AddLabel( 232, 110, 52, "Bring to Home" );

			AddButton( 260, 140, 0xFA8, 0xFAA, 300, GumpButtonType.Reply, 0 );
			AddLabel( 232, 160, 52, "Total Respawn" );

			AddButton( 260, 190, 0xFAB, 0xFAD, 400, GumpButtonType.Reply, 0 );
			AddLabel( 245, 210, 52, "Properties" );

			AddButton( 260, 240, 0xFB1, 0xFB3, 0, GumpButtonType.Reply, 0 );
			AddLabel( 256, 260, 52, "Cancel" );

			AddButton( 230, 320, 5603, 5607, 0, GumpButtonType.Page, 5 );
			AddButton( 302, 320, 5601, 5605, 0, GumpButtonType.Page, 1 );
			AddLabel( 258, 320, 52, "- 6 -" );

			for ( int i = 0;  i < 15; i++ )
			{
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, 4 + (i * 2), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, 5 + (i * 2), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.SubSpawnerE.Count )
				{
					str = (string)spawner.SubSpawnerE[i];
					int count = m_Spawner.CountCreaturesE( str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 600 + i, str );
			}

		}

		public List<string> CreateArray( RelayInfo info, Mobile from )
		{
			List<string> creaturesName = new List<string>();

			for ( int i = 0;  i < 15; i++ )
			{
				TextRelay te = info.GetTextEntry( 100 + i );

				if ( te != null )
				{
					string str = te.Text;

					if ( str.Length > 0 )
					{
						str = str.Trim();

						string t = Spawner.ParseType( str );

						Type type = ScriptCompiler.FindTypeByName( t );

						if ( type != null )
							creaturesName.Add( str );
						else
							from.SendMessage( "{0} is not a valid type name.", t );
					}
				}
			}

			return creaturesName;
		}

		public List<string> CreateArrayA( RelayInfo info, Mobile from )
		{
			List<string> creatureNameAA = new List<string>();

			for ( int i = 0;  i < 15; i++ )
			{
				TextRelay te = info.GetTextEntry( 200 + i );

				if ( te != null )
				{
					string str = te.Text;

					if ( str.Length > 0 )
					{
						str = str.Trim();

						string t = Spawner.ParseType( str );

						Type type = ScriptCompiler.FindTypeByName( t );

						if ( type != null )
							creatureNameAA.Add( str );
						else
							from.SendMessage( "{0} is not a valid type name.", t );
					}
				}
			}

			return creatureNameAA;
		}

		public List<string> CreateArrayB( RelayInfo info, Mobile from )
		{
			List<string> creatureNameBB = new List<string>();

			for ( int i = 0;  i < 15; i++ )
			{
				TextRelay te = info.GetTextEntry( 300 + i );

				if ( te != null )
				{
					string str = te.Text;

					if ( str.Length > 0 )
					{
						str = str.Trim();

						string t = Spawner.ParseType( str );

						Type type = ScriptCompiler.FindTypeByName( t );

						if ( type != null )
							creatureNameBB.Add( str );
						else
							from.SendMessage( "{0} is not a valid type name.", t );
					}
				}
			}

			return creatureNameBB;
		}

		public List<string> CreateArrayC( RelayInfo info, Mobile from )
		{
			List<string> creatureNameCC = new List<string>();

			for ( int i = 0;  i < 15; i++ )
			{
				TextRelay te = info.GetTextEntry( 400 + i );

				if ( te != null )
				{
					string str = te.Text;

					if ( str.Length > 0 )
					{
						str = str.Trim();

						string t = Spawner.ParseType( str );

						Type type = ScriptCompiler.FindTypeByName( t );

						if ( type != null )
							creatureNameCC.Add( str );
						else
							from.SendMessage( "{0} is not a valid type name.", t );
					}
				}
			}

			return creatureNameCC;
		}

		public List<string> CreateArrayD( RelayInfo info, Mobile from )
		{
			List<string> creatureNameDD = new List<string>();

			for ( int i = 0;  i < 15; i++ )
			{
				TextRelay te = info.GetTextEntry( 500 + i );

				if ( te != null )
				{
					string str = te.Text;

					if ( str.Length > 0 )
					{
						str = str.Trim();

						string t = Spawner.ParseType( str );

						Type type = ScriptCompiler.FindTypeByName( t );

						if ( type != null )
							creatureNameDD.Add( str );
						else
							from.SendMessage( "{0} is not a valid type name.", t );
					}
				}
			}

			return creatureNameDD;
		}

		public List<string> CreateArrayE( RelayInfo info, Mobile from )
		{
			List<string> creatureNameEE = new List<string>();

			for ( int i = 0;  i < 15; i++ )
			{
				TextRelay te = info.GetTextEntry( 600 + i );

				if ( te != null )
				{
					string str = te.Text;

					if ( str.Length > 0 )
					{
						str = str.Trim();

						string t = Spawner.ParseType( str );

						Type type = ScriptCompiler.FindTypeByName( t );

						if ( type != null )
							creatureNameEE.Add( str );
						else
							from.SendMessage( "{0} is not a valid type name.", t );
					}
				}
			}

			return creatureNameEE;
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( m_Spawner.Deleted )
				return;

			switch ( info.ButtonID )
			{
				case 0: // Closed
				{
					break;
				}
				case 100: // Okay
				{
					m_Spawner.CreaturesName = CreateArray( info, state.Mobile );
					break;
				}
				case 101: // Okay
				{
					m_Spawner.SubSpawnerA = CreateArrayA( info, state.Mobile );
					break;
				}
				case 102: // Okay
				{
					m_Spawner.SubSpawnerB = CreateArrayB( info, state.Mobile );
					break;
				}
				case 103: // Okay
				{
					m_Spawner.SubSpawnerC = CreateArrayC( info, state.Mobile );
					break;
				}
				case 104: // Okay
				{
					m_Spawner.SubSpawnerD = CreateArrayD( info, state.Mobile );
					break;
				}
				case 105: // Okay
				{
					m_Spawner.SubSpawnerE = CreateArrayE( info, state.Mobile );
					break;
				}
				case 200: // Bring everything home
				{
					m_Spawner.BringToHome();

					break;
				}
				case 300: // Complete respawn
				{
					m_Spawner.Respawn();

					break;
				}
				case 400: // Props
				{
					state.Mobile.SendGump( new PropertiesGump( state.Mobile, m_Spawner ) );
					state.Mobile.SendGump( new PremiumSpawnerGump( m_Spawner ) );

					break;
				}
				default:
				{
					int buttonID = info.ButtonID - 4;
					int index = buttonID / 2;
					int type = buttonID % 2;

					TextRelay entry = info.GetTextEntry( index );

					if ( entry != null && entry.Text.Length > 0 )
					{
						if ( type == 0 ) // Spawn creature
							m_Spawner.Spawn( entry.Text );
						else // Remove creatures
							m_Spawner.RemoveCreatures( entry.Text );

						m_Spawner.CreaturesName = CreateArray( info, state.Mobile );
						m_Spawner.SubSpawnerA = CreateArrayA( info, state.Mobile );
						m_Spawner.SubSpawnerB = CreateArrayB( info, state.Mobile );
						m_Spawner.SubSpawnerC = CreateArrayC( info, state.Mobile );
						m_Spawner.SubSpawnerD = CreateArrayD( info, state.Mobile );
						m_Spawner.SubSpawnerE = CreateArrayE( info, state.Mobile );
					}

					break;
				}
			}
		}
	}
}