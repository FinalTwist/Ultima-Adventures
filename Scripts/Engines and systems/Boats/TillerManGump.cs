using Server.Mobiles;
using Server.Multis;
using Server.Network;

namespace Server.Gumps 
{
    public class TillerManGump : Gump
    {
		private Mobile m_From;
		private BaseBoat m_Boat;
		private bool ToggleOneStep;
		
        public TillerManGump ( Mobile from, BaseBoat boat, bool onestep ) : base ( 0, 0 )
        {
			m_From = from;
			m_Boat = boat;

			ToggleOneStep = onestep;
			
			Closable=true;
			Disposable=false;
			Dragable=true;
			Resizable=false;
			AddPage(0);


			int image = 10920;
			int color = 10006;
			if ( BaseBoat.isCarpet( m_Boat ) ){ image = 10923; color = 10924; }


			AddImage(0, 0, image);


			AddButton(11, 106, 10006, 10006, 11, GumpButtonType.Reply, 0);	// TURN LEFT
			AddButton(147, 106, 10006, 10006, 12, GumpButtonType.Reply, 0);	// TURN RIGHT
			AddButton(79, 165, 10006, 10006, 13, GumpButtonType.Reply, 0);	// COME ABOUT


			AddButton(108, 57, color, color, 1, GumpButtonType.Reply, 0);	// N
			AddButton(124, 91, color, color, 2, GumpButtonType.Reply, 0);	// NE
			AddButton(111, 123, color, color, 3, GumpButtonType.Reply, 0);	// E
			AddButton(79, 138, color, color, 4, GumpButtonType.Reply, 0);	// SE
			AddButton(46, 124, color, color, 5, GumpButtonType.Reply, 0);	// S
			AddButton(32, 91, color, color, 6, GumpButtonType.Reply, 0);	// SW
			AddButton(44, 60, color, color, 7, GumpButtonType.Reply, 0);	// W
			AddButton(78, 45, color, color, 8, GumpButtonType.Reply, 0);	// NW


			AddButton(78, 16, 10006, 10006, 10, GumpButtonType.Reply, 0);	// ANCHOR
			AddButton(75, 89, 11410, 11410, 100, GumpButtonType.Reply, 0);	// STOP
			AddButton(38, 159, 2103, 2103, 9, GumpButtonType.Reply, 0);		// ONE STEP
			AddButton(120, 158, 2103, 2103, 99, GumpButtonType.Reply, 0);	// RENAME SHIP
        }

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if( m_Boat == null || m_From == null )
				return;
				
			if( !m_Boat.Contains( m_From ) )
			{
				if ( BaseBoat.isCarpet( m_Boat ) ){ m_From.SendMessage( "You have to be on your carpet to do that!" ); }
				else { m_From.SendMessage( "You have to be on the boat to do that!" ); }
				m_From.CloseGump( typeof( TillerManGump ) );
				return;
			}

			switch ( info.ButtonID )
			{
				case 100: m_Boat.StopMove( true ); break;
				case 99: m_Boat.BeginRename( m_From ); break;

				case 1: // N
				{
					if ( m_Boat.Facing == Direction.North ){      if( !ToggleOneStep ){ m_Boat.StartMove( Direction.North, true ); } else { m_Boat.OneMove( Direction.North ); }	}
					else if ( m_Boat.Facing == Direction.South ){ if( !ToggleOneStep ){ m_Boat.StartMove( Direction.South, true ); } else { m_Boat.OneMove( Direction.South ); }	}
					else if ( m_Boat.Facing == Direction.East ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.West, true ); } else { m_Boat.OneMove( Direction.West ); }	}
					else if ( m_Boat.Facing == Direction.West ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.East, true ); } else { m_Boat.OneMove( Direction.East ); }	}
					break;
				}
				case 2: // NE
				{
					if ( m_Boat.Facing == Direction.North ){      if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Right, true ); } else { m_Boat.OneMove( Direction.Right ); }	}
					else if ( m_Boat.Facing == Direction.South ){ if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Left, true ); } else { m_Boat.OneMove( Direction.Left ); }	}
					else if ( m_Boat.Facing == Direction.East ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Up, true ); } else { m_Boat.OneMove( Direction.Up ); }	}
					else if ( m_Boat.Facing == Direction.West ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Down, true ); } else { m_Boat.OneMove( Direction.Down ); }	}
					break;
				}
				case 3: // E
				{
					if ( m_Boat.Facing == Direction.North ){      if( !ToggleOneStep ){ m_Boat.StartMove( Direction.East, true ); } else { m_Boat.OneMove( Direction.East ); }	}
					else if ( m_Boat.Facing == Direction.South ){ if( !ToggleOneStep ){ m_Boat.StartMove( Direction.West, true ); } else { m_Boat.OneMove( Direction.West ); }	}
					else if ( m_Boat.Facing == Direction.East ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.North, true ); } else { m_Boat.OneMove( Direction.North ); }	}
					else if ( m_Boat.Facing == Direction.West ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.South, true ); } else { m_Boat.OneMove( Direction.South ); }	}
					break;
				}
				case 4: // SE
				{
					if ( m_Boat.Facing == Direction.North ){      if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Down, true ); } else { m_Boat.OneMove( Direction.Down ); }	}
					else if ( m_Boat.Facing == Direction.South ){ if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Up, true ); } else { m_Boat.OneMove( Direction.Up ); }	}
					else if ( m_Boat.Facing == Direction.East ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Right, true ); } else { m_Boat.OneMove( Direction.Right ); }	}
					else if ( m_Boat.Facing == Direction.West ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Left, true ); } else { m_Boat.OneMove( Direction.Left ); }	}
					break;
				}
				case 5: // S
				{
					if ( m_Boat.Facing == Direction.North ){      if( !ToggleOneStep ){ m_Boat.StartMove( Direction.South, true ); } else { m_Boat.OneMove( Direction.South ); }	}
					else if ( m_Boat.Facing == Direction.South ){ if( !ToggleOneStep ){ m_Boat.StartMove( Direction.North, true ); } else { m_Boat.OneMove( Direction.North ); }	}
					else if ( m_Boat.Facing == Direction.East ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.East, true ); } else { m_Boat.OneMove( Direction.East ); }	}
					else if ( m_Boat.Facing == Direction.West ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.West, true ); } else { m_Boat.OneMove( Direction.West ); }	}
					break;
				}
				case 6: // SW
				{
					if ( m_Boat.Facing == Direction.North ){      if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Left, true ); } else { m_Boat.OneMove( Direction.Left ); }	}
					else if ( m_Boat.Facing == Direction.South ){ if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Right, true ); } else { m_Boat.OneMove( Direction.Right ); }	}
					else if ( m_Boat.Facing == Direction.East ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Down, true ); } else { m_Boat.OneMove( Direction.Down ); }	}
					else if ( m_Boat.Facing == Direction.West ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Up, true ); } else { m_Boat.OneMove( Direction.Up ); }	}
					break;
				}
				case 7: // W
				{
					if ( m_Boat.Facing == Direction.North ){      if( !ToggleOneStep ){ m_Boat.StartMove( Direction.West, true ); } else { m_Boat.OneMove( Direction.West ); }	}
					else if ( m_Boat.Facing == Direction.South ){ if( !ToggleOneStep ){ m_Boat.StartMove( Direction.East, true ); } else { m_Boat.OneMove( Direction.East ); }	}
					else if ( m_Boat.Facing == Direction.East ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.South, true ); } else { m_Boat.OneMove( Direction.South ); }	}
					else if ( m_Boat.Facing == Direction.West ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.North, true ); } else { m_Boat.OneMove( Direction.North ); }	}
					break;
				}
				case 8: // NW
				{
					if ( m_Boat.Facing == Direction.North ){      if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Up, true ); } else { m_Boat.OneMove( Direction.Up ); }	}
					else if ( m_Boat.Facing == Direction.South ){ if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Down, true ); } else { m_Boat.OneMove( Direction.Down ); }	}
					else if ( m_Boat.Facing == Direction.East ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Left, true ); } else { m_Boat.OneMove( Direction.Left ); }	}
					else if ( m_Boat.Facing == Direction.West ){  if( !ToggleOneStep ){ m_Boat.StartMove( Direction.Right, true ); } else { m_Boat.OneMove( Direction.Right ); }	}
					break;
				}

				case 9:	// TOGGLE ONE STEP
				{
					if( !ToggleOneStep )
						ToggleOneStep = true;
					else ToggleOneStep = false;
					break;
				}
				case 10:	// RAISE / DROP ANCHOR
				{
					if( m_Boat.Anchored )
						m_Boat.RaiseAnchor( true );
					else 
						m_Boat.LowerAnchor( true );
					break;
				}
				case 11:	// TURN LEFT/RIGHT/AROUND
				{
					m_Boat.StartTurn(  -2, true );	// LEFT		
					break;
				}
				case 12:
				{
					m_Boat.StartTurn(  2, true );	// RIGHT
					break;
				}
				case 13:
				{
					m_Boat.StartTurn(  -4, true );	// AROUND
					break;
				}
			}

			m_From.CloseGump( typeof( TillerManGump ) );
			m_From.SendGump( new TillerManGump( m_From, m_Boat, ToggleOneStep ) );
		}	
    }
}