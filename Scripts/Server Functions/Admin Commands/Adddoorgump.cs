using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
	public class AddDoorGump : Gump
	{
		private int m_Type;

		public AddDoorGump() : this( -1 )
		{
		}

		public void AddBlueBack( int width, int height )
		{
			AddBackground (  0,  0, width-00, height-00, 0xE10 );
			AddBackground (  8,  5, width-16, height-11, 0x053 );
			AddImageTiled ( 15, 14, width-29, height-29, 0xE14 );
			AddAlphaRegion( 15, 14, width-29, height-29 );
		}

		public AddDoorGump( int type ) : base( 50, 40 )
		{
			m_Type = type;

			AddPage( 0 );

			if ( m_Type >= 0 && m_Type < m_Types.Length )
			{
				AddBlueBack( 155, 174 );

				int baseID = m_Types[m_Type].m_BaseID;

				AddItem( 25, 24, baseID );
				AddButton( 26, 37, 0x5782, 0x5782, 1, GumpButtonType.Reply, 0 );

				AddItem( 47, 45, baseID + 2 );
				AddButton( 43, 57, 0x5783, 0x5783, 2, GumpButtonType.Reply, 0 );

				AddItem( 87, 22, baseID + 10 );
				AddButton( 116, 35, 0x5785, 0x5785, 6, GumpButtonType.Reply, 0 );

				AddItem( 65, 45, baseID + 8 );
				AddButton( 96, 55, 0x5784, 0x5784, 5, GumpButtonType.Reply, 0 );

				AddButton( 73, 36, 0x2716, 0x2716, 9, GumpButtonType.Reply, 0 );
			}
			else
			{
				AddBlueBack( 950, 145 );

				for ( int i = 0; i < m_Types.Length; ++i )
				{
					AddButton( 30 + (i * 49), 13, 0x2624, 0x2625, i + 1, GumpButtonType.Reply, 0 );
					AddItem( 22 + (i * 49), 20, m_Types[i].m_BaseID );
				}
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			int button = info.ButtonID - 1;

			if ( m_Type == -1 )
			{
				if ( button >= 0 && button < m_Types.Length )
					from.SendGump( new AddDoorGump( button ) );
			}
			else
			{
				if ( button >= 0 && button < 8 )
				{
					from.SendGump( new AddDoorGump( m_Type ) );
					CommandSystem.Handle( from, String.Format( "{0}Add {1} {2}", CommandSystem.Prefix, m_Types[m_Type].m_Type.Name, (DoorFacing) button ) );
				}
				else if ( button == 8 )
				{
					from.SendGump( new AddDoorGump( m_Type ) );
					CommandSystem.Handle( from, String.Format( "{0}Link", CommandSystem.Prefix ) );
				}
				else
				{
					from.SendGump( new AddDoorGump() );
				}
			}
		}

		public static void Initialize()
		{
			CommandSystem.Register( "AddDoor", AccessLevel.GameMaster, new CommandEventHandler( AddDoor_OnCommand ) );
		}

		[Usage( "AddDoor" )]
		[Description( "Displays a menu from which you can interactively add doors." )]
		public static void AddDoor_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new AddDoorGump() );
		}

		public static DoorInfo[] m_Types = new DoorInfo[]
			{
				new DoorInfo( typeof( MetalDoor ), 0x675 ),
                new DoorInfo( typeof( MetalDoor2), 0x6C5 ),
				new DoorInfo( typeof( RattanDoor ), 0x695 ),
				new DoorInfo( typeof( DarkWoodDoor ), 0x6A5 ),
				new DoorInfo( typeof( LightWoodDoor ), 0x6D5 ),
				new DoorInfo( typeof( StrongWoodDoor ), 0x6E5 ),
				new DoorInfo( typeof( LightWoodGate ), 0x839 ),
				new DoorInfo( typeof( DarkWoodGate ), 0x866 ),
				new DoorInfo( typeof( MediumWoodDoor ), 0x6B5 ),
				new DoorInfo( typeof( SecretLightWoodDoor ), 0x344 ),
				new DoorInfo( typeof( SecretWoodenDoor ), 0x334 ),
				new DoorInfo( typeof( SecretDungeonDoor), 0x314 ),
				new DoorInfo( typeof( SecretStoneDoor1 ), 0xE8 ),
				new DoorInfo( typeof( SecretStoneDoor2 ), 0x324 ),
				new DoorInfo( typeof( SecretStoneDoor3 ), 0x354 ),
				new DoorInfo( typeof( IronGateShort ), 0x84C ),
                new DoorInfo( typeof( IronGate ), 0x824 ),
				new DoorInfo( typeof( BarredMetalDoor ), 0x685 ),
				new DoorInfo( typeof( BarredMetalDoor2 ), 0x1FED )
			};
	}

	public class DoorInfo
	{
		public Type m_Type;
		public int m_BaseID;

		public DoorInfo( Type type, int baseID )
		{
			m_Type = type;
			m_BaseID = baseID;
		}
	}
}