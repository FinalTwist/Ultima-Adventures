using System;
using Server;
using Server.Network;
using Server.Items;

namespace Server.Gumps
{
	public class SounderGump : Gump
	{
		private Mobile m_Owner;
		private Sounder m_Sounder;

		public SounderGump( Mobile owner, Sounder device ) : this( owner, device, device.Sounds )
		{
		}

		public SounderGump( Mobile owner, Sounder device, int[] sounds ) : base( 25, 25 )
		{
			owner.CloseGump( typeof( SounderGump ) );

			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			m_Owner = owner;
			m_Sounder = device;

			int[] g_Sounds = new int[10];
			g_Sounds = sounds;

			string str;

			AddPage( 0 );

			AddBackground( 0, 0, 200, 320, 0x13BE );

			AddImageTiled( 46, 28, 108, 230, 0x98D );

			AddLabel( 50, 292, 0, "Cancel" );
			AddButton( 18, 290, 0xFB1, 0xFB3, 0, GumpButtonType.Reply, 0 );

			AddLabel( 136, 292, 0, "Accept" );
			AddButton( 104, 290, 0xFB7, 0xFB9, 1, GumpButtonType.Reply, 0 );

			AddLabel( 50, 267, 0, "Props" );
			AddButton( 18, 265, 0xFAB, 0xFAD, 2, GumpButtonType.Reply, 0 );

			AddLabel( 136, 267, 0, "Sort" );
			AddButton( 104, 265, 0xFA8, 0xFAA, 3, GumpButtonType.Reply, 0 );

			AddLabel( 12, 6, 0, "Del" );
			AddLabel( 65, 6, 0, "Sound List" );
			AddLabel( 160, 6, 0, "Play" );

			for ( int i = 0;  i < 10; i++ )
			{
				if ( g_Sounds[i] >= 0 )
					str = g_Sounds[i].ToString();
				else
					str = null;

				AddButton( 9, ( 23 * i ) + 30, 0xFB4, 0xFB6, 10 + i, GumpButtonType.Reply, 0 );
				AddTextEntry( 54, ( 23 * i ) + 30, 80, 19, 0, i, str );
				AddButton( 160, ( 23 * i ) + 30, 0xFAE, 0xFB0, 20 + i, GumpButtonType.Reply, 0 );
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			int snd;
			int[] g_Sounds = new int[10];

			for ( int i = 0;  i < 10; i++ )
			{
				TextRelay entry = info.GetTextEntry( i );

				if ( entry != null && entry.Text.Length > 0 )
					g_Sounds[i] = Utility.ToInt32( entry.Text );
				else
					g_Sounds[i] = -1;
			}

			switch( info.ButtonID )
			{
				case 0: // Cancel
					break; 
				case 1: // Okay
				{
					m_Sounder.Sounds = g_Sounds;
					break;
				}
				case 2: // Show Properties Page
				{
					if ( from.AccessLevel >= AccessLevel.GameMaster )
						from.SendGump( new PropertiesGump( from, m_Sounder ) );
					
					break; 
				}
				case 3: // Sort the Array
				{
					for ( int i = 0;  i < 10; i++ )
						if ( g_Sounds[i] < 0 ) g_Sounds[i] = Int32.MaxValue;

					Array.Sort( g_Sounds );

					for ( int i = 0;  i < 10; i++ )
						if ( g_Sounds[i] == Int32.MaxValue ) g_Sounds[i] = -1;

					from.SendGump( new SounderGump( m_Owner, m_Sounder, g_Sounds ) );
					break; 
				}
				default:
				{
					int index = info.ButtonID;

					if ( index >= 20 ) // Play sound for Designer only
					{
						index -= 20;
						TextRelay entry = info.GetTextEntry( index );

						if ( entry != null && entry.Text.Length > 0 )
						{
							snd = g_Sounds[index];

							Packet p = new PlaySound( snd, m_Sounder.Location );
							m_Owner.Send( p );
						}
					}
					else // Delete an entry
					{
						index -= 10;
						g_Sounds[index] = -1;
					}

					from.SendGump( new SounderGump( m_Owner, m_Sounder, g_Sounds ) );
					break;
				}
			}
		}
	}
}
