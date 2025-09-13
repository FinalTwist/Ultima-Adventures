/////////////////////////////////////
//          SPAWN EDITOR           //
// This SpawnEditor is a huge mod  //
// of "ZenArcher's SpawnEditor v2" //
//            By Nerun             //
/////////////////////////////////////
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Regions;
using Server.Commands;

namespace Server.Gumps
{
	public class SpawnEditorGump : Gump
	{
		private int m_page;
		private ArrayList m_tempList;
		public Item m_selSpawner;

		public int page
		{
			get{ return m_page; }
			set{ m_page = value; }
		}

		public Item selSpawner
		{
			get{ return m_selSpawner; }
			set{ m_selSpawner = value; }
		}

		public ArrayList tempList
		{
			get{ return m_tempList; }
			set{ m_tempList = value; }
		}

		public static void Initialize()
		{
			CommandSystem.Register( "SpawnEditor", AccessLevel.GameMaster, new CommandEventHandler( SpawnEditor_OnCommand ) );
			CommandSystem.Register( "Editor", AccessLevel.GameMaster, new CommandEventHandler( SpawnEditor_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
			CommandSystem.Register( command, access, handler );
		}
		
		[Usage( "SpawnEditor" )]
		[Aliases( "Editor" )]
		[Description( "Used to find and edit spawns" )]
		public static void SpawnEditor_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			SpawnEditor_OnCommand( from );
		}

		public static void SpawnEditor_OnCommand( Mobile from )
		{
			ArrayList worldList = new ArrayList();
			ArrayList facetList = new ArrayList();

			Type type = ScriptCompiler.FindTypeByName( "PremiumSpawner", true );

			if ( type == typeof( Item ) || type.IsSubclassOf( typeof( Item ) ) )
			{
				bool isAbstract = type.IsAbstract;

				foreach ( Item item in World.Items.Values )
				{
					if ( isAbstract ? item.GetType().IsSubclassOf( type ) : item.GetType() == type )
						worldList.Add( item );
				}
			}

			foreach( PremiumSpawner worldSpnr in worldList )
			{
				if( worldSpnr.Map == from.Map )
					facetList.Add( worldSpnr );
			}

//TODO: Sort spawner list

			SpawnEditor_OnCommand( from, 0, facetList );
		}

		public static void SpawnEditor_OnCommand( Mobile from, int page, ArrayList currentList )
		{
			SpawnEditor_OnCommand( from, page, currentList, 0 );
		}

		public static void SpawnEditor_OnCommand( Mobile from, int page, ArrayList currentList, int selected )
		{
			SpawnEditor_OnCommand( from, page, currentList, selected, null );
		}

		public static void SpawnEditor_OnCommand( Mobile from, int page, ArrayList currentList, int selected, Item selSpawner )
		{
			from.SendGump( new SpawnEditorGump( from, page, currentList, selected, selSpawner ) );
		}

		public SpawnEditorGump( Mobile from, int page, ArrayList currentList, int selected, Item spwnr ) : base( 50, 40 )
		{
			tempList = new ArrayList();
			Mobile m = from;
			m_page = page;
			Region r = from.Region;
			Map map = from.Map;
			int buttony = 60;
			int buttonID = 1;
			int listnum = 0;

			tempList = currentList;

			selSpawner = spwnr;

			AddPage(0);

			AddBackground( 0, 0, 600, 450, 5054 );
			AddImageTiled( 8, 8, 584, 40, 2624 );
			AddAlphaRegion( 8, 8, 584, 40 );
			AddImageTiled( 8, 50, 250, 396, 2624 );
			AddAlphaRegion( 8, 50, 250, 396 );
			AddImageTiled( 260, 50, 332, 396, 2624 );
			AddAlphaRegion( 260, 50, 332, 396 );
			AddLabel( 220, 20, 52, "PREMIUM SPAWNER EDITOR" );
			AddButton( 550, 405, 0x158A, 0x158B, 10002, GumpButtonType.Reply, 1 ); //Quit Button
			AddButton( 275, 412, 0x845, 0x846, 10008, GumpButtonType.Reply, 0 );
			AddLabel( 300, 410, 52, "Refresh" );

			if( currentList.Count == 0 )
				AddLabel( 50, 210, 52, "No Premium Spawners Found" );
			else
			{
				if( page == 0 )
				{
					if( currentList.Count < 15 )
						listnum = currentList.Count;
					else
						listnum = 15;

					for( int x = 0; x < listnum; x++ )
					{
						Item spawnr = null;

						if( currentList[x] is Item )
							spawnr = currentList[x] as Item;

						string gumpMsg = "";

						Point3D spawnr3D = new Point3D( ( new Point2D( spawnr.X, spawnr.Y ) ), spawnr.Z );
						Region spawnrRegion = Region.Find( spawnr3D, map );

						if( spawnrRegion.ToString() == "" )
							gumpMsg = "PremiumSpawner at " + spawnr.X.ToString() + ", " + spawnr.Y.ToString();
						else
							gumpMsg = spawnrRegion.ToString();

						AddButton( 25, buttony, 0x845, 0x846, buttonID, GumpButtonType.Reply, 0 );
						AddLabel( 55, buttony, 52, gumpMsg );
						buttony += 25;
						buttonID += 1;
					}
				}

				else if( page > 0 )
				{
					if( currentList.Count < 15 + ( 15 * page ) )
						listnum = currentList.Count;
					else
						listnum = 15 + ( 15 * page );

					for( int x = 15 * page; x < listnum; x++ )
					{
						Item spawnr = null;
						buttonID = x+1;

						if( currentList[x] is Item )
							spawnr = currentList[x] as Item;

						string gumpMsg = "";

						Point3D spawnr3D = new Point3D( ( new Point2D( spawnr.X, spawnr.Y ) ), spawnr.Z );
						Region spawnrRegion = Region.Find( spawnr3D, map );

						if( spawnrRegion.ToString() == "" )
							gumpMsg = "PremiumSpawner at " + spawnr.X.ToString() + ", " + spawnr.Y.ToString();
						else
							gumpMsg = spawnrRegion.ToString();

						AddButton( 25, buttony, 0x845, 0x846, buttonID, GumpButtonType.Reply, 0 );
						AddLabel( 55, buttony, 52, gumpMsg );
						buttony += 25;
					}
				}
			}

			if( page == 0 && currentList.Count > 15 )
				AddButton( 450, 20, 0x15E1, 0x15E5, 10000, GumpButtonType.Reply, 0 );
			else if( page > 0 && currentList.Count > 15 + ( page * 15 ) )
				AddButton( 450, 20, 0x15E1, 0x15E5, 10000, GumpButtonType.Reply, 0 );

			if( page != 0 )
				AddButton( 150, 20, 0x15E3, 0x15E7, 10001, GumpButtonType.Reply, 0 );

			int pageNum = (int)currentList.Count / 15;
			int rem = currentList.Count % 15;
			int totPages = 0;

			string stotPages = "";

			if( rem > 0 )
			{
				totPages = pageNum + 1;
				stotPages = totPages.ToString();
			}
			else
				stotPages = pageNum.ToString();

			string pageText = "Page " + ( page + 1 ) + " of " + stotPages;

			AddLabel( 40, 20, 52, pageText );

			if( selected == 0 )
				InitializeStartingRightPanel();
			else if( selected == 1 )
				InitializeSelectedRightPanel();
		}

		public void InitializeStartingRightPanel()
		{
			AddLabel( 275, 65, 52, "Filter to current region only" );
			AddButton( 500, 65, 0x15E1, 0x15E5, 10003, GumpButtonType.Reply, 0 );

			AddTextField( 275, 140, 50, 20, 0 );
			AddLabel( 275, 115, 52, "Filter by Distance" );
			AddButton( 500, 115, 0x15E1, 0x15E5, 10004, GumpButtonType.Reply, 0 );

			AddTextField( 275, 190, 120, 20, 1 );
			AddLabel( 275, 165, 52, "Search Spawners by Creature" );
			AddButton( 500, 165, 0x15E1, 0x15E5, 10009, GumpButtonType.Reply, 0 );

			AddTextField( 275, 240, 50, 20, 2 );
			AddLabel( 275, 215, 52, "Search Spawners by SpawnID" );
			AddButton( 500, 215, 0x15E1, 0x15E5, 10010, GumpButtonType.Reply, 0 );
		}

		public void InitializeSelectedRightPanel()
		{
			string spX = selSpawner.X.ToString();
			string spY = selSpawner.Y.ToString();
			string spnText = "PremiumSpawner at " + spX + ", " + spY;

			AddLabel( 350, 65, 52, spnText );

			PremiumSpawner initSpn = selSpawner as PremiumSpawner;
			int strNum = 0;
			string spns = "Containing: ";
			string spnsNEW = "";
			string spns1 = "";
			string spns2 = "";
			string spns3 = "";

			for( int i = 0; i < initSpn.CreaturesName.Count; i++ )
			{
				if( strNum == 0 )
				{
					if( i < initSpn.CreaturesName.Count - 1 )
					{
						if( spns.Length + initSpn.CreaturesName[i].ToString().Length < 50 )
							spnsNEW += (string)initSpn.CreaturesName[i] + ", ";
						else
						{
							strNum = 1;
							spns1 += (string)initSpn.CreaturesName[i] + ", ";
						}
					}
					else
						spnsNEW += (string)initSpn.CreaturesName[i];
				}
				else if( strNum == 1 )
				{
					if( i < initSpn.CreaturesName.Count - 1 )
					{
						if( spns1.Length + initSpn.CreaturesName[i].ToString().Length < 50 )
							spns1 += (string)initSpn.CreaturesName[i] + ", ";
						else
						{
							strNum = 2;
							spns2 += (string)initSpn.CreaturesName[i] + ", ";
						}
					}
					else
					{
						if( spns1.Length + initSpn.CreaturesName[i].ToString().Length < 50 )
							spns1 += (string)initSpn.CreaturesName[i];
						else
						{
							strNum = 3;
							spns2 += (string)initSpn.CreaturesName[i];
						}
					}
				}
				else if( strNum == 2 )
				{
					if( i < initSpn.CreaturesName.Count - 1 )
					{
						if( spns2.Length + initSpn.CreaturesName[i].ToString().Length < 50 )
							spns2 += (string)initSpn.CreaturesName[i] + ", ";
						else
						{
							strNum = 3;
							spns3 += (string)initSpn.CreaturesName[i] + ", ";
						}
					}
					else
					{
						if( spns2.Length + initSpn.CreaturesName[i].ToString().Length < 50 )
							spns2 += (string)initSpn.CreaturesName[i];
						else
						{
							strNum = 4;
							spns3 += (string)initSpn.CreaturesName[i];
						}
					}
				}
				else if( strNum == 3 )	
				{
					if( i < initSpn.CreaturesName.Count - 1 )
						spns3 += (string)initSpn.CreaturesName[i] + ", ";
					else
						spns3 += (string)initSpn.CreaturesName[i];
				}
			}

			string spnsNEWa = "";
			string spns1a = "";
			string spns2a = "";
			string spns3a = "";

			for( int i = 0; i < initSpn.SubSpawnerA.Count; i++ )
			{
				if( strNum == 0 )
				{
					if( i < initSpn.SubSpawnerA.Count - 1 )
					{
						if( spns.Length + initSpn.SubSpawnerA[i].ToString().Length < 50 )
							spnsNEWa += (string)initSpn.SubSpawnerA[i] + ", ";
						else
						{
							strNum = 1;
							spns1a += (string)initSpn.SubSpawnerA[i] + ", ";
						}
					}
					else
						spnsNEWa += (string)initSpn.SubSpawnerA[i];
				}
				else if( strNum == 1 )
				{
					if( i < initSpn.SubSpawnerA.Count - 1 )
					{
						if( spns1a.Length + initSpn.SubSpawnerA[i].ToString().Length < 50 )
							spns1a += (string)initSpn.SubSpawnerA[i] + ", ";
						else
						{
							strNum = 2;
							spns2a += (string)initSpn.SubSpawnerA[i] + ", ";
						}
					}
					else
					{
						if( spns1a.Length + initSpn.SubSpawnerA[i].ToString().Length < 50 )
							spns1a += (string)initSpn.SubSpawnerA[i];
						else
						{
							strNum = 3;
							spns2a += (string)initSpn.SubSpawnerA[i];
						}
					}
				}
				else if( strNum == 2 )
				{
					if( i < initSpn.SubSpawnerA.Count - 1 )
					{
						if( spns2a.Length + initSpn.SubSpawnerA[i].ToString().Length < 50 )
							spns2a += (string)initSpn.SubSpawnerA[i] + ", ";
						else
						{
							strNum = 3;
							spns3a += (string)initSpn.SubSpawnerA[i] + ", ";
						}
					}
					else
					{
						if( spns2a.Length + initSpn.SubSpawnerA[i].ToString().Length < 50 )
							spns2a += (string)initSpn.SubSpawnerA[i];
						else
						{
							strNum = 4;
							spns3a += (string)initSpn.SubSpawnerA[i];
						}
					}
				}
				else if( strNum == 3 )	
				{
					if( i < initSpn.SubSpawnerA.Count - 1 )
						spns3a += (string)initSpn.SubSpawnerA[i] + ", ";
					else
						spns3a += (string)initSpn.SubSpawnerA[i];
				}
			}

			string spnsNEWb = "";
			string spns1b = "";
			string spns2b = "";
			string spns3b = "";

			for( int i = 0; i < initSpn.SubSpawnerB.Count; i++ )
			{
				if( strNum == 0 )
				{
					if( i < initSpn.SubSpawnerB.Count - 1 )
					{
						if( spns.Length + initSpn.SubSpawnerB[i].ToString().Length < 50 )
							spnsNEWb += (string)initSpn.SubSpawnerB[i] + ", ";
						else
						{
							strNum = 1;
							spns1b += (string)initSpn.SubSpawnerB[i] + ", ";
						}
					}
					else
						spnsNEWb += (string)initSpn.SubSpawnerB[i];
				}
				else if( strNum == 1 )
				{
					if( i < initSpn.SubSpawnerB.Count - 1 )
					{
						if( spns1b.Length + initSpn.SubSpawnerB[i].ToString().Length < 50 )
							spns1b += (string)initSpn.SubSpawnerB[i] + ", ";
						else
						{
							strNum = 2;
							spns2b += (string)initSpn.SubSpawnerB[i] + ", ";
						}
					}
					else
					{
						if( spns1b.Length + initSpn.SubSpawnerB[i].ToString().Length < 50 )
							spns1b += (string)initSpn.SubSpawnerB[i];
						else
						{
							strNum = 3;
							spns2b += (string)initSpn.SubSpawnerB[i];
						}
					}
				}
				else if( strNum == 2 )
				{
					if( i < initSpn.SubSpawnerB.Count - 1 )
					{
						if( spns2b.Length + initSpn.SubSpawnerB[i].ToString().Length < 50 )
							spns2b += (string)initSpn.SubSpawnerB[i] + ", ";
						else
						{
							strNum = 3;
							spns3b += (string)initSpn.SubSpawnerB[i] + ", ";
						}
					}
					else
					{
						if( spns2b.Length + initSpn.SubSpawnerB[i].ToString().Length < 50 )
							spns2b += (string)initSpn.SubSpawnerB[i];
						else
						{
							strNum = 4;
							spns3b += (string)initSpn.SubSpawnerB[i];
						}
					}
				}
				else if( strNum == 3 )	
				{
					if( i < initSpn.SubSpawnerB.Count - 1 )
						spns3b += (string)initSpn.SubSpawnerB[i] + ", ";
					else
						spns3b += (string)initSpn.SubSpawnerB[i];
				}
			}

			string spnsNEWc = "";
			string spns1c = "";
			string spns2c = "";
			string spns3c = "";

			for( int i = 0; i < initSpn.SubSpawnerC.Count; i++ )
			{
				if( strNum == 0 )
				{
					if( i < initSpn.SubSpawnerC.Count - 1 )
					{
						if( spns.Length + initSpn.SubSpawnerC[i].ToString().Length < 50 )
							spnsNEWc += (string)initSpn.SubSpawnerC[i] + ", ";
						else
						{
							strNum = 1;
							spns1c += (string)initSpn.SubSpawnerC[i] + ", ";
						}
					}
					else
						spnsNEWc += (string)initSpn.SubSpawnerC[i];
				}
				else if( strNum == 1 )
				{
					if( i < initSpn.SubSpawnerC.Count - 1 )
					{
						if( spns1c.Length + initSpn.SubSpawnerC[i].ToString().Length < 50 )
							spns1c += (string)initSpn.SubSpawnerC[i] + ", ";
						else
						{
							strNum = 2;
							spns2c += (string)initSpn.SubSpawnerC[i] + ", ";
						}
					}
					else
					{
						if( spns1c.Length + initSpn.SubSpawnerC[i].ToString().Length < 50 )
							spns1c += (string)initSpn.SubSpawnerC[i];
						else
						{
							strNum = 3;
							spns2c += (string)initSpn.SubSpawnerC[i];
						}
					}
				}
				else if( strNum == 2 )
				{
					if( i < initSpn.SubSpawnerC.Count - 1 )
					{
						if( spns2c.Length + initSpn.SubSpawnerC[i].ToString().Length < 50 )
							spns2c += (string)initSpn.SubSpawnerC[i] + ", ";
						else
						{
							strNum = 3;
							spns3c += (string)initSpn.SubSpawnerC[i] + ", ";
						}
					}
					else
					{
						if( spns2c.Length + initSpn.SubSpawnerC[i].ToString().Length < 50 )
							spns2c += (string)initSpn.SubSpawnerC[i];
						else
						{
							strNum = 4;
							spns3c += (string)initSpn.SubSpawnerC[i];
						}
					}
				}
				else if( strNum == 3 )	
				{
					if( i < initSpn.SubSpawnerC.Count - 1 )
						spns3c += (string)initSpn.SubSpawnerC[i] + ", ";
					else
						spns3c += (string)initSpn.SubSpawnerC[i];
				}
			}

			string spnsNEWd = "";
			string spns1d = "";
			string spns2d = "";
			string spns3d = "";

			for( int i = 0; i < initSpn.SubSpawnerD.Count; i++ )
			{
				if( strNum == 0 )
				{
					if( i < initSpn.SubSpawnerD.Count - 1 )
					{
						if( spns.Length + initSpn.SubSpawnerD[i].ToString().Length < 50 )
							spnsNEWd += (string)initSpn.SubSpawnerD[i] + ", ";
						else
						{
							strNum = 1;
							spns1d += (string)initSpn.SubSpawnerD[i] + ", ";
						}
					}
					else
						spnsNEWd += (string)initSpn.SubSpawnerD[i];
				}
				else if( strNum == 1 )
				{
					if( i < initSpn.SubSpawnerD.Count - 1 )
					{
						if( spns1d.Length + initSpn.SubSpawnerD[i].ToString().Length < 50 )
							spns1d += (string)initSpn.SubSpawnerD[i] + ", ";
						else
						{
							strNum = 2;
							spns2d += (string)initSpn.SubSpawnerD[i] + ", ";
						}
					}
					else
					{
						if( spns1d.Length + initSpn.SubSpawnerD[i].ToString().Length < 50 )
							spns1d += (string)initSpn.SubSpawnerD[i];
						else
						{
							strNum = 3;
							spns2d += (string)initSpn.SubSpawnerD[i];
						}
					}
				}
				else if( strNum == 2 )
				{
					if( i < initSpn.SubSpawnerD.Count - 1 )
					{
						if( spns2d.Length + initSpn.SubSpawnerD[i].ToString().Length < 50 )
							spns2d += (string)initSpn.SubSpawnerD[i] + ", ";
						else
						{
							strNum = 3;
							spns3d += (string)initSpn.SubSpawnerD[i] + ", ";
						}
					}
					else
					{
						if( spns2d.Length + initSpn.SubSpawnerD[i].ToString().Length < 50 )
							spns2d += (string)initSpn.SubSpawnerD[i];
						else
						{
							strNum = 4;
							spns3d += (string)initSpn.SubSpawnerD[i];
						}
					}
				}
				else if( strNum == 3 )	
				{
					if( i < initSpn.SubSpawnerD.Count - 1 )
						spns3d += (string)initSpn.SubSpawnerD[i] + ", ";
					else
						spns3d += (string)initSpn.SubSpawnerD[i];
				}
			}

			string spnsNEWe = "";
			string spns1e = "";
			string spns2e = "";
			string spns3e = "";

			for( int i = 0; i < initSpn.SubSpawnerE.Count; i++ )
			{
				if( strNum == 0 )
				{
					if( i < initSpn.SubSpawnerE.Count - 1 )
					{
						if( spns.Length + initSpn.SubSpawnerE[i].ToString().Length < 50 )
							spnsNEWe += (string)initSpn.SubSpawnerE[i] + ", ";
						else
						{
							strNum = 1;
							spns1e += (string)initSpn.SubSpawnerE[i] + ", ";
						}
					}
					else
						spnsNEWe += (string)initSpn.SubSpawnerE[i];
				}
				else if( strNum == 1 )
				{
					if( i < initSpn.SubSpawnerE.Count - 1 )
					{
						if( spns1e.Length + initSpn.SubSpawnerE[i].ToString().Length < 50 )
							spns1e += (string)initSpn.SubSpawnerE[i] + ", ";
						else
						{
							strNum = 2;
							spns2e += (string)initSpn.SubSpawnerE[i] + ", ";
						}
					}
					else
					{
						if( spns1e.Length + initSpn.SubSpawnerE[i].ToString().Length < 50 )
							spns1e += (string)initSpn.SubSpawnerE[i];
						else
						{
							strNum = 3;
							spns2e += (string)initSpn.SubSpawnerE[i];
						}
					}
				}
				else if( strNum == 2 )
				{
					if( i < initSpn.SubSpawnerE.Count - 1 )
					{
						if( spns2e.Length + initSpn.SubSpawnerE[i].ToString().Length < 50 )
							spns2e += (string)initSpn.SubSpawnerE[i] + ", ";
						else
						{
							strNum = 3;
							spns3e += (string)initSpn.SubSpawnerE[i] + ", ";
						}
					}
					else
					{
						if( spns2e.Length + initSpn.SubSpawnerE[i].ToString().Length < 50 )
							spns2e += (string)initSpn.SubSpawnerE[i];
						else
						{
							strNum = 4;
							spns3e += (string)initSpn.SubSpawnerE[i];
						}
					}
				}
				else if( strNum == 3 )	
				{
					if( i < initSpn.SubSpawnerE.Count - 1 )
						spns3e += (string)initSpn.SubSpawnerE[i] + ", ";
					else
						spns3e += (string)initSpn.SubSpawnerE[i];
				}
			}

			AddLabel( 275, 85, 52, spns );
			AddLabel( 280, 110, 52, "[1]" );
			AddLabel( 280, 180, 52, "[2]" );
			AddLabel( 280, 250, 52, "[3]" );
			AddLabel( 425, 110, 52, "[4]" );
			AddLabel( 425, 180, 52, "[5]" );
			AddLabel( 425, 250, 52, "[6]" );
			AddHtml( 300, 110, 115, 65, spnsNEW, true, true );
			AddHtml( 300, 180, 115, 65, spnsNEWa, true, true );
			AddHtml( 300, 250, 115, 65, spnsNEWb, true, true );
			AddHtml( 445, 110, 115, 65, spnsNEWc, true, true );
			AddHtml( 445, 180, 115, 65, spnsNEWd, true, true );
			AddHtml( 445, 250, 115, 65, spnsNEWe, true, true );
			if( spns1 != "" )
				AddLabel( 275, 105, 200, spns1 );

			if( spns2 != "" )
				AddLabel( 275, 125, 200, spns2 );

			if( spns3 != "" )
				AddLabel( 275, 145, 200, spns3 );

			AddLabel( 320, 320, 52, "Go to Spawner" );
			AddButton( 525, 320, 0x15E1, 0x15E5, 10005, GumpButtonType.Reply, 1 );
			AddLabel( 320, 345, 52, "Delete Selected Spawner" );
			AddButton( 525, 345, 0x15E1, 0x15E5, 10006, GumpButtonType.Reply, 0 );
			AddLabel( 320, 370, 52, "Edit Spawns" );
			AddButton( 525, 370, 0x15E1, 0x15E5, 10007, GumpButtonType.Reply, 0 );
		}

		public List<string> CreateArray( RelayInfo info, Mobile from )
		{
			List<string> creaturesName = new List<string>();

			for ( int i = 0;  i < 13; i++ )
			{
				TextRelay te = info.GetTextEntry( i );

				if ( te != null )
				{
					string str = te.Text;

					if ( str.Length > 0 )
					{
						str = str.Trim();

						Type type = SpawnerType.GetType( str );

						if ( type != null )
							creaturesName.Add( str );
						else
							AddLabel( 70, 230, 39, "Invalid Search String" );
					}
				}
			}

			return creaturesName;
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			int buttonNum = 0;
			ArrayList currentList = new ArrayList( tempList );
			int page = m_page;

			if( info.ButtonID > 0 && info.ButtonID < 10000 )
				buttonNum = 1;
			else if( info.ButtonID > 20004 )
				buttonNum = 30000;
			else
				buttonNum = info.ButtonID;

			switch( buttonNum )
			{
				case 0:
				{
					//Close
					break;
				}
				case 1:
				{
					selSpawner = currentList[ info.ButtonID - 1 ] as Item;
					SpawnEditor_OnCommand( from, page, currentList, 1, selSpawner );
					break;
				}
				case 10000:
				{
					if( m_page * 10 < currentList.Count )
					{
						page = m_page += 1;
						SpawnEditor_OnCommand( from, page, currentList );
					}
					break;
				}
				case 10001:
				{
					if( m_page != 0 )
					{
						page = m_page -= 1;
						SpawnEditor_OnCommand( from, page, currentList );
					}
					break;
				}
				case 10002:
				{
					//Close
					break;
				}
				case 10003:
				{
					FilterByRegion( from, tempList, from.Region, from.Map, page );
					break;
				}
				case 10004:
				{
					TextRelay oDis = info.GetTextEntry( 0 );
					string sDis = ( oDis == null ? "" : oDis.Text.Trim() );
					if( sDis != "" )
					{
						try
						{
							int distance = Convert.ToInt32( sDis );
							FilterByDistance( tempList, from, distance, page );						}
						catch
						{
							from.SendMessage( "Distance must be a number" );
							SpawnEditor_OnCommand( from, page, currentList );
						}
					}
					else
					{
						from.SendMessage( "You must specify a distance" );
						SpawnEditor_OnCommand( from, page, currentList );
					}
					break;
				}
				case 10005:
				{
					from.Location = new Point3D( selSpawner.X, selSpawner.Y, selSpawner.Z );
					SpawnEditor_OnCommand( from, page, currentList, 1, selSpawner );
					break;
				}
				case 10006:
				{
					selSpawner.Delete();
					SpawnEditor_OnCommand( from );
					break;
				}
				case 10007:
				{
					from.SendGump( new PremiumSpawnerGump( selSpawner as PremiumSpawner ) );
					SpawnEditor_OnCommand( from, page, currentList, 1, selSpawner );
					break;
				}
				case 10008:
				{
					SpawnEditor_OnCommand( from );
					break;
				}
				case 10009:
				{
					TextRelay oSearch = info.GetTextEntry( 1 );
					string sSearch = ( oSearch == null ? null : oSearch.Text.Trim() );
					SearchByName( tempList, from, sSearch, page );
					break;
				}
				case 10010:
				{
					TextRelay oID = info.GetTextEntry( 2 );
					string sID = ( oID == null ? "" : oID.Text.Trim() );
					if( sID != "" )
					{
						try
						{
							int SearchID = Convert.ToInt32( sID );
							SearchByID( tempList, from, SearchID, page );						}
						catch
						{
							from.SendMessage( "SpawnID must be a number" );
							SpawnEditor_OnCommand( from, page, currentList );
						}
					}
					else
					{
						from.SendMessage( "You must specify a SpawnID" );
						SpawnEditor_OnCommand( from, page, currentList );
					}
					break;
				}
				case 20000:
				{
					PremiumSpawner spawner = selSpawner as PremiumSpawner;
					spawner.CreaturesName = CreateArray( info, state.Mobile );
					break;
				}
				case 20001:
				{
					PremiumSpawner spawner = selSpawner as PremiumSpawner;
					SpawnEditor_OnCommand( from, page, currentList, 2, selSpawner );
					spawner.BringToHome();
					break;
				}
				case 20002:
				{
					PremiumSpawner spawner = selSpawner as PremiumSpawner;
					SpawnEditor_OnCommand( from, page, currentList, 2, selSpawner );
					spawner.Respawn();
					break;
				}
				case 20003:
				{
					PremiumSpawner spawner = selSpawner as PremiumSpawner;
					SpawnEditor_OnCommand( from, page, currentList, 2, selSpawner );
					state.Mobile.SendGump( new PropertiesGump( state.Mobile, spawner ) );
					break;
				}
				case 30000:
				{
					int buttonID = info.ButtonID - 20004;
					int index = buttonID / 2;
					int type = buttonID % 2;

					PremiumSpawner spawner = selSpawner as PremiumSpawner;

					TextRelay entry = info.GetTextEntry( index );

					if ( entry != null && entry.Text.Length > 0 )
					{
						if ( type == 0 ) // Spawn creature
							spawner.Spawn( entry.Text );
						else // Remove creatures
							spawner.RemoveCreatures( entry.Text );
							//spawner.RemoveCreaturesA( entry.Text );

						spawner.CreaturesName = CreateArray( info, state.Mobile );
					}

					break;
				}
			}
		}


		public static void FilterByRegion( Mobile from, ArrayList facetList, Region regr, Map regmap, int page )
		{
			ArrayList filregList = new ArrayList();
			
			foreach( Item regItem in facetList )
			{
				Point2D p2 = new Point2D( regItem.X, regItem.Y );
				Point3D p = new Point3D( p2, regItem.Z );
						
				if( Region.Find( p, regmap ) == regr )
					filregList.Add( regItem );
			}

			from.SendGump( new SpawnEditorGump( from, 0, filregList, 0, null ) );
		}

		public static void FilterByDistance( ArrayList currentList, Mobile m, int dis, int page )
		{
			ArrayList fildisList = new ArrayList();

			for( int z = 0; z < currentList.Count; z ++ )
			{
				Item disItem = currentList[z] as Item;

				if( disItem.X >= m.X - dis && disItem.X <= m.X + dis && disItem.Y >= m.Y - dis && disItem.Y <= m.Y + dis )
					fildisList.Add( disItem );
			}

			m.SendGump( new SpawnEditorGump( m, 0, fildisList, 0, null ) );
		}

		public static void SearchByName( ArrayList currentList, Mobile from, string search, int page )
		{
			ArrayList searchList = new ArrayList();

			foreach( PremiumSpawner spn in currentList )
			{
				foreach( string str in spn.CreaturesName )
				{
					if( str.ToLower().IndexOf( search ) >= 0 )
						searchList.Add( spn );
				}

				foreach( string str in spn.SubSpawnerA )
				{
					if( str.ToLower().IndexOf( search ) >= 0 )
						searchList.Add( spn );
				}

				foreach( string str in spn.SubSpawnerB )
				{
					if( str.ToLower().IndexOf( search ) >= 0 )
						searchList.Add( spn );
				}

				foreach( string str in spn.SubSpawnerC )
				{
					if( str.ToLower().IndexOf( search ) >= 0 )
						searchList.Add( spn );
				}

				foreach( string str in spn.SubSpawnerD )
				{
					if( str.ToLower().IndexOf( search ) >= 0 )
						searchList.Add( spn );
				}

				foreach( string str in spn.SubSpawnerE )
				{
					if( str.ToLower().IndexOf( search ) >= 0 )
						searchList.Add( spn );
				}
			}

			from.SendGump( new SpawnEditorGump( from, 0, searchList, 0, null ) );
		}

		public static void SearchByID( ArrayList currentList, Mobile from, int SearchID, int page )
		{
			ArrayList searchList = new ArrayList();

			foreach( PremiumSpawner spn in currentList )
			{
				if ( ((PremiumSpawner)spn).SpawnID == SearchID )
				{
					searchList.Add( spn );
				}
			}

			from.SendGump( new SpawnEditorGump( from, 0, searchList, 0, null ) );
		}

		public void AddTextField( int x, int y, int width, int height, int index )
		{
			AddBackground( x - 2, y - 2, width + 4, height + 4, 0x2486 );
			AddTextEntry( x + 2, y + 2, width - 4, height - 4, 0, index, "" );
		}
	}
}