/*
 All credit for this particular script goes to Haazen for an awesome job!
*/
using System;
using System.Text;
using Server.Gumps;
using Server.Multis;
using Server.Mobiles;
using Server.Movement;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class MysticalSextant : Item
	{

		[Constructable]
		public MysticalSextant() : base( 0x1057 )
		{
			Movable = true;
			Hue = 1283;
			Name = "Mystical Sextant of Control";
		}

		public override void OnDoubleClick( Mobile from )
		{
		from.SendGump( new NavigationGump( (PlayerMobile)from ) );	
		}

		public MysticalSextant( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

		}

	}
}

namespace Server.Gumps

{

	public class NavigationGump : Gump
	{
		private PlayerMobile m_From;

		public NavigationGump( PlayerMobile from ) : base( 40, 40 )
		{
			from.CloseGump( typeof( NavigationGump ) ); 
			m_From = from;

			AddPage( 0 );
			AddBackground( 0, 0, 140, 155, 83 );//5054 83
			AddImageTiled(8, 9, 126, 135, 1416);
			AddAlphaRegion(8, 9, 126, 135);

		AddButton( 20, 124, 0x15E6, 0x15E6, 1, GumpButtonType.Reply, 0 );//Drop 0x985
		AddLabel( 50, 120, 0x34, "Anchor" );

		AddButton( 107, 124, 0x15E0, 0x15E0, 2, GumpButtonType.Reply, 0 );//Raise 0x983

		AddButton( 60, 10, 0x26AC, 0x26AC, 3, GumpButtonType.Reply, 0 ); //Forward
		AddButton( 60, 90, 0x26B2, 0x26B2, 4, GumpButtonType.Reply, 0 ); //Back
		AddButton( 20, 50, 0x26B5, 0x26B5, 5, GumpButtonType.Reply, 0 ); //Left
		AddButton( 100, 50, 0x26AF, 0x26AF, 6, GumpButtonType.Reply, 0 ); //Right
		AddButton( 62, 53, 0x2C93, 0x2C93, 7, GumpButtonType.Reply, 0 ); //Stop

		AddButton( 20, 90, 0x5786, 0x5786, 8, GumpButtonType.Reply, 0 ); //TurnLeft
		//AddLabel( 55, 125, 0x34, "Turn" );
		AddButton( 100, 90, 0x5781, 0x5781, 9, GumpButtonType.Reply, 0 ); //TurnRight
		AddButton( 62, 31, 0x2621, 0x2621, 10, GumpButtonType.Reply, 0 ); //OneForward
		AddButton( 62, 73, 0x2625, 0x2625, 11, GumpButtonType.Reply, 0 ); //OneBack
		AddButton( 40, 52, 0x2627, 0x2627, 12, GumpButtonType.Reply, 0 ); //OneLeft
		AddButton( 83, 52, 0x2623, 0x2623, 13, GumpButtonType.Reply, 0 ); //OneRight

		AddButton( 39, 29, 0x24C0, 0x24C0, 14, GumpButtonType.Reply, 0 ); //LeftForward 0x13F4
		AddButton( 92, 29, 0x24BE, 0x24BE, 15, GumpButtonType.Reply, 0 ); //RightForward 0x13F2
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{

			BaseBoat boat = BaseBoat.FindBoatAt( m_From, m_From.Map );
			Container pack = m_From.Backpack;
			if ( boat == null )
				return;
			if ( pack != null ) // && pack.ConsumeTotal( typeof( Gold ), 2 ) )
			{
			   switch ( info.ButtonID )
			   {
				case 1: // Drop Anchor
				{
					//boat.LowerAnchor( true );
				//m_From.SendGump( new NavigationGump( (PlayerMobile)m_From ) );
					break;
				}
				case 2: // Raise Anchor
				{
					//boat.RaiseAnchor( true );
				//m_From.SendGump( new NavigationGump( (PlayerMobile)m_From ) );
					break;
				}
				case 3: // Forward
				{
					boat.StartMove( Direction.North, true );
				m_From.SendGump( new NavigationGump( (PlayerMobile)m_From ) );
					break;
				}
				case 4: // Back
				{
					boat.StartMove( Direction.South, true );
				m_From.SendGump( new NavigationGump( (PlayerMobile)m_From ) );
					break;
				}
				case 5: // Left
				{
					boat.StartMove( Direction.West, true );
				m_From.SendGump( new NavigationGump( (PlayerMobile)m_From ) );
					break;
				}
				case 6: // Right
				{
					boat.StartMove( Direction.East, true );
				m_From.SendGump( new NavigationGump( (PlayerMobile)m_From ) );
					break;
				}
				case 7: // Stop
				{
					boat.StopMove( true );
				m_From.SendGump( new NavigationGump( (PlayerMobile)m_From ) );
					break;
				}
				case 8: // TurnLeft
				{
					boat.StartTurn( -2, true );
				m_From.SendGump( new NavigationGump( (PlayerMobile)m_From ) );
					break;
				}
				case 9: // TurnRight
				{
					boat.StartTurn( 2, true );
				m_From.SendGump( new NavigationGump( (PlayerMobile)m_From ) );
					break;
				}
				case 10: // OneForward
				{
					boat.OneMove( Direction.North );
				m_From.SendGump( new NavigationGump( (PlayerMobile)m_From ) );
					break;
				}
				case 11: // OneBack
				{
					boat.OneMove( Direction.South );
				m_From.SendGump( new NavigationGump( (PlayerMobile)m_From ) );
					break;
				}
				case 12: // OneLeft
				{
					boat.OneMove( Direction.West );
				m_From.SendGump( new NavigationGump( (PlayerMobile)m_From ) );
					break;
				}
				case 13: // OneRight
				{
					boat.OneMove( Direction.East );
				m_From.SendGump( new NavigationGump( (PlayerMobile)m_From ) );
					break;
				}
				case 14: // LeftForward
				{
					boat.StartMove( Direction.Up, true );
				m_From.SendGump( new NavigationGump( (PlayerMobile)m_From ) );
					break;
				}
				case 15: // RightForward
				{
					boat.StartMove( Direction.Right, true );
				m_From.SendGump( new NavigationGump( (PlayerMobile)m_From ) );
					break;
				}
			   }
			}
          	}
  	}
}