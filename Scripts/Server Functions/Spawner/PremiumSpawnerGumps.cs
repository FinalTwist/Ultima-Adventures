// By Nerun
// Engine r25
using System;
using System.Collections;
using System.IO;
using Server;
using Server.Mobiles; 
using Server.Items;
using Server.Commands; 
using Server.Network;
using Server.Gumps;
using Server.Regions;

namespace Server.Commands
{
	public class OptionsGumps
	{
		public OptionsGumps()
		{
		}

		public static void Initialize() 
		{ 
			CommandSystem.Register( "GumpSaveRegion", AccessLevel.Administrator, new CommandEventHandler( OptionsGumps1_OnCommand ) );
			CommandSystem.Register( "GumpSaveCoordinate", AccessLevel.Administrator, new CommandEventHandler( OptionsGumps2_OnCommand ) );
			CommandSystem.Register( "GumpRemoveID", AccessLevel.Administrator, new CommandEventHandler( OptionsGumps3_OnCommand ) );
			CommandSystem.Register( "GumpRemoveCoordinate", AccessLevel.Administrator, new CommandEventHandler( OptionsGumps4_OnCommand ) );
			CommandSystem.Register( "GumpRemoveRegion", AccessLevel.Administrator, new CommandEventHandler( OptionsGumps5_OnCommand ) );
		}

		[Usage( "[GumpSaveRegion" )]
		[Description( "Gump to Save inside Region" )] 
		private static void OptionsGumps1_OnCommand( CommandEventArgs e )
		{ 
			e.Mobile.SendGump( new GumpSaveRegion( e ) );
		}

		[Usage( "[GumpSaveCoordinate" )]
		[Description( "Gump to save by coordinates" )] 
		private static void OptionsGumps2_OnCommand( CommandEventArgs e )
		{ 
			e.Mobile.SendGump( new GumpSaveCoordinate( e ) );
		}

		[Usage( "[GumpRemoveID" )]
		[Description( "Gump to remove by ID" )] 
		private static void OptionsGumps3_OnCommand( CommandEventArgs e )
		{ 
			e.Mobile.SendGump( new GumpRemoveID( e ) );
		}

		[Usage( "[GumpRemoveCoordinate" )]
		[Description( "Gump to remove by coordinates" )] 
		private static void OptionsGumps4_OnCommand( CommandEventArgs e )
		{ 
			e.Mobile.SendGump( new GumpRemoveCoordinate( e ) );
		}

		[Usage( "[GumpRemoveRegion" )]
		[Description( "Gump to remove inside region" )] 
		private static void OptionsGumps5_OnCommand( CommandEventArgs e )
		{ 
			e.Mobile.SendGump( new GumpRemoveRegion( e ) );
		}
	}
}

namespace Server.Gumps
{
	public class GumpSaveRegion : Gump
	{
		private CommandEventArgs m_CommandEventArgs;

		public GumpSaveRegion( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;
			Mobile from = e.Mobile;

			AddPage(1);
			//x, y, width, hight
			AddBackground( 0, 0, 232, 210, 5054 );

			AddImageTiled( 15, 30, 120, 20, 3004 );
			AddTextEntry( 15, 30, 120, 20, 0, 0, @"region to save");
			AddLabel( 15, 10, 52, "Enter a Region:" );
			AddButton( 140, 32, 0x15E1, 0x15E5, 101, GumpButtonType.Reply, 0 );

			AddLabel( 15, 60, 52, "Tip:" );
			AddHtml( 15, 80, 200, 110, "This will SAVE the spawners, in a specified region, to Data/Monsters/'region name'.map. Type [where if you don't know the region you are. Copy to the text box the name of the region. You also can open Data/Regions.xml to a full list of regions.<BR>Example: you type [where and appear 'your region is town of Britain'. Type 'Britain' in text box.", true, true );
		}

		public override void OnResponse(NetState state, RelayInfo info)
		{
			Mobile from = state.Mobile;
			
			switch ( info.ButtonID )
			{
				case 0: // close the gump
				{
					break;
				}
					
				case 101:
				{
					TextRelay oRegion = info.GetTextEntry( 0 );
					string sRegion = oRegion.Text;
					if( sRegion != "" )
					{
						string prefix = Server.Commands.CommandSystem.Prefix;
						CommandSystem.Handle( from, String.Format( "{0}Spawngen save {1}", prefix, sRegion ) );
					}
					else
					{
						from.SendMessage( "You must specify a region!" );
						string prefix = Server.Commands.CommandSystem.Prefix;
						CommandSystem.Handle( from, String.Format( "{0}GumpSaveRegion", prefix ) );
					}
					break;
				}
			}
		}
	}

	public class GumpRemoveRegion : Gump
	{
		private CommandEventArgs m_CommandEventArgs;

		public GumpRemoveRegion( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;
			Mobile from = e.Mobile;

			AddPage(1);

			AddBackground( 0, 0, 232, 210, 5054 );

			AddImageTiled( 15, 30, 120, 20, 3004 );
			AddTextEntry( 15, 30, 120, 20, 0, 0, @"region to remove");
			AddLabel( 15, 10, 52, "Enter a Region:" );
			AddButton( 140, 32, 0x15E1, 0x15E5, 101, GumpButtonType.Reply, 0 );

			AddLabel( 15, 60, 52, "Tip:" );
			AddHtml( 15, 80, 200, 110, "This will REMOVE the spawners, in a specified region. Type [where if you don't know the region you are. Copy to the text box the name of the region. You also can open Data/Regions.xml to a full list of regions.<BR>Example: you type [where and appear 'your region is town of Britain'. Type 'Britain' in text box.", true, true );
		}

		public override void OnResponse(NetState state, RelayInfo info)
		{
			Mobile from = state.Mobile;
			
			switch ( info.ButtonID )
			{
				case 0: // close the gump
				{
					break;
				}
					
				case 101:
				{
					TextRelay oRegion = info.GetTextEntry( 0 );
					string sRegion = oRegion.Text;
					if( sRegion != "" )
					{
						string prefix = Server.Commands.CommandSystem.Prefix;
						CommandSystem.Handle( from, String.Format( "{0}Spawngen remove {1}", prefix, sRegion ) );
					}
					else
					{
						from.SendMessage( "You must specify a region!" );
						string prefix = Server.Commands.CommandSystem.Prefix;
						CommandSystem.Handle( from, String.Format( "{0}GumpRemoveRegion", prefix ) );
					}
					break;
				}
			}
		}
	}

	public class GumpRemoveID : Gump
	{
		private CommandEventArgs m_CommandEventArgs;

		public GumpRemoveID( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;
			Mobile from = e.Mobile;

			AddPage(1);

			AddBackground( 0, 0, 232, 210, 5054 );

			AddImageTiled( 15, 30, 120, 20, 3004 );
			AddTextEntry( 15, 30, 120, 20, 0, 0, @"SpawnID to remove");
			AddLabel( 15, 10, 52, "Enter a SpawnID:" );
			AddButton( 140, 32, 0x15E1, 0x15E5, 101, GumpButtonType.Reply, 0 );

			AddLabel( 15, 60, 52, "Tip:" );
			AddHtml( 15, 80, 200, 110, "This command was made to UNLOAD your own custom maps. This will REMOVE the spawners with the specified ID. Type '[get spawnid' in a spawner to know your ID. Remember: 'By Hand' spawns, i.e., those done with '[add premiumspawner' have ID = 1.", true, true );
		}

		public override void OnResponse(NetState state, RelayInfo info)
		{
			Mobile from = state.Mobile;
			
			switch ( info.ButtonID )
			{
				case 0: // close the gump
				{
					break;
				}
					
				case 101:
				{
					TextRelay oID = info.GetTextEntry( 0 );
					string sID = oID.Text;
					if( sID != "" )
					{
						try
						{
							int UnloadID = Convert.ToInt32( sID );
							string prefix = Server.Commands.CommandSystem.Prefix;
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload {1}", prefix, UnloadID ) );
						}
						catch
						{
							from.SendMessage( "SpawnID must be a number!" );
							string prefix = Server.Commands.CommandSystem.Prefix;
							CommandSystem.Handle( from, String.Format( "{0}GumpRemoveID", prefix ) );
						}
					}

					else
					{
						from.SendMessage( "You must specify an SpawnID!" );
						string prefix = Server.Commands.CommandSystem.Prefix;
						CommandSystem.Handle( from, String.Format( "{0}GumpRemoveID", prefix ) );
					}
					break;
				}
			}
		}
	}

	public class GumpSaveCoordinate : Gump
	{
		private CommandEventArgs m_CommandEventArgs;

		public GumpSaveCoordinate( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;
			Mobile from = e.Mobile;

			AddPage(1);

			AddBackground( 0, 0, 232, 235, 5054 );

			AddImageTiled( 15, 30, 37, 20, 3004 );
			AddTextEntry( 15, 30, 37, 20, 0, 0, @"X1");

			AddImageTiled( 57, 30, 37, 20, 3004 );
			AddTextEntry( 57, 30, 37, 20, 0, 1, @"Y1");

			AddImageTiled( 15, 55, 37, 20, 3004 );
			AddTextEntry( 15, 55, 37, 20, 0, 2, @"X2");

			AddImageTiled( 57, 55, 37, 20, 3004 );
			AddTextEntry( 57, 55, 37, 20, 0, 3, @"Y2");

			AddLabel( 15, 10, 52, "Enter Coordinates:" );
			AddButton( 140, 32, 0x15E1, 0x15E5, 101, GumpButtonType.Reply, 0 );

			AddLabel( 15, 85, 52, "Tip:" );
			AddHtml( 15, 105, 200, 110, "This will SAVE spawners inside specified coordinates. You can use [where in the first point and again [where in the second point to get the X and Y coordinates. You need 2: X1, Y1 for first point and X2, Y2 for the second point. The objective is determine a 'box'. This command will save all spawners inside this box.", true, true );
		}

		public override void OnResponse(NetState state, RelayInfo info)
		{
			Mobile from = state.Mobile;
			
			switch ( info.ButtonID )
			{
				case 0: // close the gump
				{
					break;
				}
					
				case 101:
				{
					TextRelay oX1 = info.GetTextEntry( 0 );
					TextRelay oY1 = info.GetTextEntry( 1 );
					TextRelay oX2 = info.GetTextEntry( 2 );
					TextRelay oY2 = info.GetTextEntry( 3 );
					string sX1 = oX1.Text;
					string sY1 = oY1.Text;
					string sX2 = oX2.Text;
					string sY2 = oY2.Text;
					if( sX1 != "" && sY1 != "" && sX2 != "" && sY2 != "" )
					{
						try
						{
							int iX1 = Convert.ToInt32( sX1 );
							int iY1 = Convert.ToInt32( sY1 );
							int iX2 = Convert.ToInt32( sX2 );
							int iY2 = Convert.ToInt32( sY2 );
							string prefix = Server.Commands.CommandSystem.Prefix;
							CommandSystem.Handle( from, String.Format( "{0}Spawngen save {1} {2} {3} {4}", prefix, iX1, iY1, iX2, iY2 ) );
						}
						catch
						{
							from.SendMessage( "Coordinates must be numbers!" );
							string prefix = Server.Commands.CommandSystem.Prefix;
							CommandSystem.Handle( from, String.Format( "{0}GumpSaveCoordinate", prefix ) );
						}
					}

					else
					{
						from.SendMessage( "You must specify all coordinates!" );
						string prefix = Server.Commands.CommandSystem.Prefix;
						CommandSystem.Handle( from, String.Format( "{0}GumpSaveCoordinate", prefix ) );
					}
					break;
				}
			}
		}
	}

	public class GumpRemoveCoordinate : Gump
	{
		private CommandEventArgs m_CommandEventArgs;

		public GumpRemoveCoordinate( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;
			Mobile from = e.Mobile;

			AddPage(1);

			AddBackground( 0, 0, 232, 235, 5054 );

			AddImageTiled( 15, 30, 37, 20, 3004 );
			AddTextEntry( 15, 30, 37, 20, 0, 0, @"X1");

			AddImageTiled( 57, 30, 37, 20, 3004 );
			AddTextEntry( 57, 30, 37, 20, 0, 1, @"Y1");

			AddImageTiled( 15, 55, 37, 20, 3004 );
			AddTextEntry( 15, 55, 37, 20, 0, 2, @"X2");

			AddImageTiled( 57, 55, 37, 20, 3004 );
			AddTextEntry( 57, 55, 37, 20, 0, 3, @"Y2");

			AddLabel( 15, 10, 52, "Enter Coordinates:" );
			AddButton( 140, 32, 0x15E1, 0x15E5, 101, GumpButtonType.Reply, 0 );

			AddLabel( 15, 85, 52, "Tip:" );
			AddHtml( 15, 105, 200, 110, "This will REMOVE spawners inside specified coordinates. You can use [where in the first point and again [where in the second point to get the X and Y coordinates. You need 2: X1, Y1 for first point and X2, Y2 for the second point. The objective is determine a 'box'. This command will remove all spawners inside this box.", true, true );
		}

		public override void OnResponse(NetState state, RelayInfo info)
		{
			Mobile from = state.Mobile;
			
			switch ( info.ButtonID )
			{
				case 0: // close the gump
				{
					break;
				}
					
				case 101:
				{
					TextRelay oX1 = info.GetTextEntry( 0 );
					TextRelay oY1 = info.GetTextEntry( 1 );
					TextRelay oX2 = info.GetTextEntry( 2 );
					TextRelay oY2 = info.GetTextEntry( 3 );
					string sX1 = oX1.Text;
					string sY1 = oY1.Text;
					string sX2 = oX2.Text;
					string sY2 = oY2.Text;
					if( sX1 != "" && sY1 != "" && sX2 != "" && sY2 != "" )
					{
						try
						{
							int iX1 = Convert.ToInt32( sX1 );
							int iY1 = Convert.ToInt32( sY1 );
							int iX2 = Convert.ToInt32( sX2 );
							int iY2 = Convert.ToInt32( sY2 );
							string prefix = Server.Commands.CommandSystem.Prefix;
							CommandSystem.Handle( from, String.Format( "{0}Spawngen remove {1} {2} {3} {4}", prefix, iX1, iY1, iX2, iY2 ) );
						}
						catch
						{
							from.SendMessage( "Coordinates must be numbers!" );
							string prefix = Server.Commands.CommandSystem.Prefix;
							CommandSystem.Handle( from, String.Format( "{0}GumpRemoveCoordinate", prefix ) );
						}
					}

					else
					{
						from.SendMessage( "You must specify all coordinates!" );
						string prefix = Server.Commands.CommandSystem.Prefix;
						CommandSystem.Handle( from, String.Format( "{0}GumpRemoveCoordinate", prefix ) );
					}
					break;
				}
			}
		}
	}
}