//Engine r55
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Regions;

namespace Server
{
	public class SpawnGenerator
	{
		private static int m_Count;
		private static int m_MapOverride = -1;
		private static int m_IDOverride = -1;
		private static double m_MinTimeOverride = -1;
		private static double m_MaxTimeOverride = -1;
		private const bool TotalRespawn = true;
		private const int Team = 0;

		public static void Initialize()
		{
			CommandSystem.Register( "SpawnGen", AccessLevel.Administrator, new CommandEventHandler( SpawnGen_OnCommand ) );
		}

		[Usage( "SpawnGen [<filename>]|[unload <id>]|[remove <region>|<rect>]|[save <region>|<rect>][savebyhand][cleanfacet]" )]
		[Description( "Complex command, it generate and remove spawners." )]
		private static void SpawnGen_OnCommand( CommandEventArgs e )
		{
			//wrog use
			if ( e.ArgString == null || e.ArgString == "" )
			{
				e.Mobile.SendMessage( "Usage: SpawnGen [<filename>]|[remove <region>|<rect>|<ID>]|[save <region>|<rect>|<ID>]" );
			}
			//[spawngen remove and [spawngen remove region
			else if ( e.Arguments[0].ToLower() == "remove" && e.Arguments.Length == 2 )
			{
				Remove( e.Mobile, e.Arguments[1].ToLower() );
			}
			//[spawngen remove x1 y1 x2 y2
			else if ( e.Arguments[0].ToLower() == "remove" && e.Arguments.Length == 5 )
			{
				int x1 = Utility.ToInt32( e.Arguments[1] );
				int y1 = Utility.ToInt32( e.Arguments[2] );
				int x2 = Utility.ToInt32( e.Arguments[3] );
				int y2 = Utility.ToInt32( e.Arguments[4] );
				RemoveByCoord( e.Mobile, x1, y1, x2, y2 );
			}
			//[spawngen remove
			else if ( e.ArgString.ToLower() == "remove" )
			{
				Remove( e.Mobile, ""  );
			}
			//[spawngen save and [spawngen save region
			else if ( e.Arguments[0].ToLower() == "save" && e.Arguments.Length == 2 )
			{
				Save( e.Mobile, e.Arguments[1].ToLower() );
			}
			//[spawngen unload SpawnID 
			else if ( e.Arguments[0].ToLower() == "unload" && e.Arguments.Length == 2 )
			{
				int ID = Utility.ToInt32( e.Arguments[1] );
				Unload( ID );
			}
			//[spawngen savebyhand
			else if ( e.Arguments[0].ToLower() == "savebyhand" )
			{
				SaveByHand();
			}
			//[spawngen cleanfacet
			else if ( e.Arguments[0].ToLower() == "cleanfacet" )
			{
				CleanFacet( e.Mobile );
			}
			////[spawngen save x1 y1 x2 y2
			else if ( e.Arguments[0].ToLower() == "save" && e.Arguments.Length == 5 )
			{
				int x1 = Utility.ToInt32( e.Arguments[1] );
				int y1 = Utility.ToInt32( e.Arguments[2] );
				int x2 = Utility.ToInt32( e.Arguments[3] );
				int y2 = Utility.ToInt32( e.Arguments[4] );
				SaveByCoord( e.Mobile, x1, y1, x2, y2 );
			}
			//[spawngen save
			else if ( e.ArgString.ToLower() == "save" )
			{
				Save( e.Mobile, "" );
			}
			else
			{
				Parse( e.Mobile, e.ArgString );
			}
		}

		public static void Talk( string alfa )
		{
			World.Broadcast( 0x35, true, "Spawns are being {0}, please wait.", alfa );
		}
		
		public static string GetRegion(Item item)
		{
			Region re = Region.Find(item.Location, item.Map);
			string regname = re.ToString().ToLower();
			return regname;
		}

		//[spawngen remove and [spawngen remove region
		private static void Remove( Mobile from, string region )
		{
			DateTime aTime = DateTime.UtcNow;
			int count = 0;
			List<Item> itemtodo = new List<Item>();

			string prefix = Server.Commands.CommandSystem.Prefix;
		
			if( region == null || region == "" )
			{
				CommandSystem.Handle( from, String.Format( "{0}Global remove where premiumspawner", prefix ) );
			}
			else
			{
				foreach( Item itemdel in World.Items.Values )
				{
					if( itemdel is PremiumSpawner && itemdel.Map == from.Map )
					{
						if( GetRegion(itemdel) == region )
						{
							itemtodo.Add(itemdel);
							count += 1;
						}
					}
				}

				GenericRemove( itemtodo, count, aTime);
			}
		}

		//[spawngen unload SpawnID
		private static void Unload( int ID )
		{
			DateTime aTime = DateTime.UtcNow;
			int count = 0;
			List<Item> itemtodo = new List<Item>();

			foreach ( Item itemremove in World.Items.Values )
			{ 
				if ( itemremove is PremiumSpawner && ((PremiumSpawner)itemremove).SpawnID == ID )
				{
					itemtodo.Add( itemremove );
					count +=1;
				}
			}

			GenericRemove( itemtodo, count, aTime);
		}

		//[spawngen remove x1 y1 x2 y2
		private static void RemoveByCoord( Mobile from, int x1, int y1, int x2, int y2 )
		{
			DateTime aTime = DateTime.UtcNow;
			int count = 0;
			List<Item> itemtodo = new List<Item>();

			foreach ( Item itemremove in World.Items.Values )
			{ 
				if ( itemremove is PremiumSpawner && ( ( itemremove.X >= x1 && itemremove.X <= x2 ) && ( itemremove.Y >= y1 && itemremove.Y <= y2 ) && itemremove.Map == from.Map ) )
				{
					itemtodo.Add( itemremove );
					count +=1;
				}
			}

			GenericRemove( itemtodo, count, aTime);
		}

		//[spawngen cleanfacet
		//this is the old [SpawnRem
		public static void CleanFacet( Mobile from )
		{
			DateTime aTime = DateTime.UtcNow;
			int count = 0;
			List<Item> itemtodo = new List<Item>();

			foreach ( Item itemremove in World.Items.Values )
			{ 
				if ( itemremove is PremiumSpawner && itemremove.Map == from.Map && itemremove.Parent == null )
				{
					itemtodo.Add( itemremove );
					count +=1;
				}
			}

			GenericRemove( itemtodo, count, aTime);
		}

		private static void GenericRemove( List<Item> colecao, int count, DateTime aTime )
		{
			if( colecao.Count == 0 )
			{
				World.Broadcast( 0x35, true, "There are no PremiumSpawners to be removed." );
			}
			else
			{
				Talk("removed");
				
				foreach ( Item item in colecao )
				{
					item.Delete();
				}
				
				DateTime bTime = DateTime.UtcNow;
				World.Broadcast( 0x35, true, "{0} PremiumSpawners have been removed in {1:F1} seconds.", count, (bTime - aTime).TotalSeconds );
			}
		}

		//[spawngen save and [spawngen save region
		private static void Save( Mobile from, string region )
		{
			DateTime aTime = DateTime.UtcNow;
			int count = 0;
			List<Item> itemtodo = new List<Item>();
			string mapanome = region;

			if( region == "" )
				mapanome = "Spawns";

			foreach ( Item itemsave in World.Items.Values )
			{
				if ( itemsave is PremiumSpawner && ( region == null || region == "" ) )
				{
					itemtodo.Add( itemsave );
					count +=1;
				}

				else if ( itemsave is PremiumSpawner && itemsave.Map == from.Map )
				{
					if ( GetRegion(itemsave) == region )
					{
						itemtodo.Add( itemsave );
						count += 1;
					}
				}
			}

			GenericSave( itemtodo, mapanome, count, aTime );
		}

		//[spawngen SaveByHand
		private static void SaveByHand()
		{
			DateTime aTime = DateTime.UtcNow;
			int count = 0;
			List<Item> itemtodo = new List<Item>();
			string mapanome = "SpawnsByHand";

			foreach ( Item itemsave in World.Items.Values )
			{ 
				if ( itemsave is PremiumSpawner && ((PremiumSpawner)itemsave).SpawnID == 1 )
				{
					itemtodo.Add( itemsave );
					count +=1;
				}
			}

			GenericSave( itemtodo, mapanome, count, aTime );
		}

		//[spawngen save x1 y1 x2 y2
		private static void SaveByCoord( Mobile from, int x1, int y1, int x2, int y2 )
		{
			DateTime aTime = DateTime.UtcNow;
			int count = 0;
			List<Item> itemtodo = new List<Item>();
			string mapanome = "SpawnsByCoords";

			foreach ( Item itemsave in World.Items.Values )
			{ 
				if ( itemsave is PremiumSpawner && ( ( itemsave.X >= x1 && itemsave.X <= x2 ) && ( itemsave.Y >= y1 && itemsave.Y <= y2 ) && itemsave.Map == from.Map ) )
				{
					itemtodo.Add( itemsave );
					count +=1;
				}
			}

			GenericSave( itemtodo, mapanome, count, aTime );
		}

		private static void GenericSave( List<Item> colecao, string mapa, int count, DateTime startTime )
		{
			List<Item> itemssave = new List<Item>( colecao );
			string mapanome = mapa;
			
			if( itemssave.Count == 0 )
			{
				World.Broadcast( 0x35, true, "There are no PremiumSpawners to be saved." );
			}
			else
			{
				Talk("saved");
				
				if ( !Directory.Exists( "Data/Monsters" ) )
					Directory.CreateDirectory( "Data/Monsters" );

				string escreva = "Data/Monsters/" + mapanome + ".map";

				using ( StreamWriter op = new StreamWriter( escreva ) )
				{
					foreach ( PremiumSpawner itemsave2 in itemssave )
					{
						int mapnumber = 0;
						switch ( itemsave2.Map.ToString() )
						{
							case "Felucca":
								mapnumber = 1;
								break;
							case "Trammel":
								mapnumber = 2;
								break;
							case "Ilshenar":
								mapnumber = 3;
								break;
							case "Malas":
								mapnumber = 4;
								break;
							case "Tokuno":
								mapnumber = 5;
								break;
							case "TerMur":
								mapnumber = 6;
								break;
							default:
								mapnumber = 7;
								Console.WriteLine( "Monster Parser: Warning, unknown map {0}", itemsave2.Map );
								break;
						}

						string timer1a = itemsave2.MinDelay.ToString();
						string[] timer1b = timer1a.Split( ':' ); //Broke the string hh:mm:ss in an array (hh, mm, ss)
						int timer1c = ( Utility.ToInt32( timer1b[0] ) * 60 ) + Utility.ToInt32( timer1b[1] ); //multiply hh * 60 to find mm, then add mm
						string timer1d = timer1c.ToString();
						if ( Utility.ToInt32( timer1b[0] ) == 0 && Utility.ToInt32( timer1b[1] ) == 0 ) //If hh and mm are 0, use seconds, else drop ss
							timer1d = Utility.ToInt32( timer1b[2] ) + "s";
						
						string timer2a = itemsave2.MaxDelay.ToString();
						string[] timer2b = timer2a.Split( ':' );
						int timer2c = ( Utility.ToInt32( timer2b[0] ) * 60 ) + Utility.ToInt32( timer2b[1] );
						string timer2d = timer2c.ToString();
						if ( Utility.ToInt32( timer2b[0] ) == 0 && Utility.ToInt32( timer2b[1] ) == 0 )
							timer2d = Utility.ToInt32( timer2b[2] ) + "s";
						
						string towrite = "";
						string towriteA = "";
						string towriteB = "";
						string towriteC = "";
						string towriteD = "";
						string towriteE = "";

						if ( itemsave2.CreaturesName.Count > 0 )
							towrite = itemsave2.CreaturesName[0].ToString();

						if ( itemsave2.SubSpawnerA.Count > 0 )
							towriteA = itemsave2.SubSpawnerA[0].ToString();

						if ( itemsave2.SubSpawnerB.Count > 0 )
							towriteB = itemsave2.SubSpawnerB[0].ToString();

						if ( itemsave2.SubSpawnerC.Count > 0 )
							towriteC = itemsave2.SubSpawnerC[0].ToString();

						if ( itemsave2.SubSpawnerD.Count > 0 )
							towriteD = itemsave2.SubSpawnerD[0].ToString();

						if ( itemsave2.SubSpawnerE.Count > 0 )
							towriteE = itemsave2.SubSpawnerE[0].ToString();

						for ( int i = 1; i < itemsave2.CreaturesName.Count; ++i )
						{
							if ( itemsave2.CreaturesName.Count > 0 )
								towrite = towrite + ":" + itemsave2.CreaturesName[i].ToString();
						}

						for ( int i = 1; i < itemsave2.SubSpawnerA.Count; ++i )
						{
							if ( itemsave2.SubSpawnerA.Count > 0 )
								towriteA = towriteA + ":" + itemsave2.SubSpawnerA[i].ToString();
						}

						for ( int i = 1; i < itemsave2.SubSpawnerB.Count; ++i )
						{
							if ( itemsave2.SubSpawnerB.Count > 0 )
								towriteB = towriteB + ":" + itemsave2.SubSpawnerB[i].ToString();
						}

						for ( int i = 1; i < itemsave2.SubSpawnerC.Count; ++i )
						{
							if ( itemsave2.SubSpawnerC.Count > 0 )
								towriteC = towriteC + ":" + itemsave2.SubSpawnerC[i].ToString();
						}

						for ( int i = 1; i < itemsave2.SubSpawnerD.Count; ++i )
						{
							if ( itemsave2.SubSpawnerD.Count > 0 )
								towriteD = towriteD + ":" + itemsave2.SubSpawnerD[i].ToString();
						}

						for ( int i = 1; i < itemsave2.SubSpawnerE.Count; ++i )
						{
							if ( itemsave2.SubSpawnerE.Count > 0 )
								towriteE = towriteE + ":" + itemsave2.SubSpawnerE[i].ToString();
						}

						op.WriteLine( "*|{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}|{20}", towrite, towriteA, towriteB, towriteC, towriteD, towriteE, itemsave2.X, itemsave2.Y, itemsave2.Z, mapnumber, timer1d, timer2d, itemsave2.WalkingRange, itemsave2.HomeRange, itemsave2.SpawnID, itemsave2.Count, itemsave2.CountA, itemsave2.CountB, itemsave2.CountC, itemsave2.CountD, itemsave2.CountE );
					}
				}

				DateTime endTime = DateTime.UtcNow;
				World.Broadcast( 0x35, true, "{0} spawns have been saved. The entire process took {1:F1} seconds.", count, (endTime - startTime).TotalSeconds );
			}
		}

		public static void Parse( Mobile from, string filename )
		{
			string monster_path1 = Path.Combine( Core.BaseDirectory, "Data/Monsters" );
			string monster_path = Path.Combine( monster_path1, filename );
			m_Count = 0;

			if ( File.Exists( monster_path ) )
			{
				from.SendMessage( "Spawning {0}...", filename );
				m_MapOverride = -1;
				m_IDOverride = -1;
				m_MinTimeOverride = -1;
				m_MaxTimeOverride = -1;

				using ( StreamReader ip = new StreamReader( monster_path ) )
				{
					string line;

					while ( (line = ip.ReadLine()) != null )
					{
						string[] split = line.Split( '|' );
						string[] splitA = line.Split( ' ' );

						if ( splitA.Length == 2  )
						{
							if ( splitA[0].ToLower() == "overridemap" )
								m_MapOverride = Utility.ToInt32( splitA[1] );
							if ( splitA[0].ToLower() == "overrideid" )
								m_IDOverride = Utility.ToInt32( splitA[1] );
							if ( splitA[0].ToLower() == "overridemintime" )
								m_MinTimeOverride = Utility.ToDouble( splitA[1] );
							if ( splitA[0].ToLower() == "overridemaxtime" )
								m_MaxTimeOverride = Utility.ToDouble( splitA[1] );
						}

						if ( split.Length < 19 )
							continue;

						switch( split[0].ToLower() ) 
						{
							//Comment Line
							case "##":
								break;
							//Place By class
							case "*":
								PlaceNPC( split[2].Split(':'), split[3].Split(':'), split[4].Split(':'), split[5].Split(':'), split[6].Split(':'), split[7], split[8], split[9], split[10], split[11], split[12], split[14], split[13], split[15], split[16], split[17], split[18], split[19], split[20], split[21], split[1].Split(':') );
								break;
							//Place By Type
							case "r":
								PlaceNPC( split[2].Split(':'), split[3].Split(':'), split[4].Split(':'), split[5].Split(':'), split[6].Split(':'), split[7], split[8], split[9], split[10], split[11], split[12], split[14], split[13], split[15], split[16], split[17], split[18], split[19], split[20], split[1], "bloodmoss", "sulfurousash", "spiderssilk", "mandrakeroot", "gravedust", "nightshade", "ginseng", "garlic", "batwing", "pigiron", "noxcrystal", "daemonblood", "blackpearl");
								break;
						}
					}
				}

				m_MapOverride = -1;
				m_IDOverride = -1;
				m_MinTimeOverride = -1;
				m_MaxTimeOverride = -1;

				from.SendMessage( "Done, added {0} spawners",m_Count );
			}
			else
			{
				from.SendMessage( "{0} not found!", monster_path );
			}
		}

		public static void PlaceNPC( string[] fakespawnsA, string[] fakespawnsB, string[] fakespawnsC, string[] fakespawnsD, string[] fakespawnsE, string sx, string sy, string sz, string sm, string smintime, string smaxtime, string swalkingrange, string shomerange, string sspawnid, string snpccount, string sfakecountA, string sfakecountB, string sfakecountC, string sfakecountD, string sfakecountE, params string[] types )
		{
			if ( types.Length == 0 )
				return;

			int x = Utility.ToInt32( sx );
			int y = Utility.ToInt32( sy );
			int z = Utility.ToInt32( sz );
			int map = Utility.ToInt32( sm );
			
			//MinTime
			string samintime = smintime;
			
			if ( smintime.Contains("s") || smintime.Contains("m") || smintime.Contains("h") )
				samintime = smintime.Remove(smintime.Length - 1);
			
			double dmintime = Utility.ToDouble( samintime );
			
			if ( m_MinTimeOverride != -1 )
				dmintime = m_MinTimeOverride;

			TimeSpan mintime = TimeSpan.FromMinutes( dmintime );
			
			if ( smintime.Contains("s") )
				mintime = TimeSpan.FromSeconds( dmintime );
			else if ( smintime.Contains("m") )
				mintime = TimeSpan.FromMinutes( dmintime );
			else if ( smintime.Contains("h") )
				mintime = TimeSpan.FromHours( dmintime );
			
			//MaxTime
			
			string samaxtime = smaxtime;
			
			if ( smaxtime.Contains("s") || smaxtime.Contains("m") || smaxtime.Contains("h") )
				samaxtime = smaxtime.Remove(smaxtime.Length - 1);
			
			double dmaxtime = Utility.ToDouble( samaxtime );

			if ( m_MaxTimeOverride != -1 )
			{
				if ( m_MaxTimeOverride < dmintime )
					dmaxtime = dmintime;
				else
					dmaxtime = m_MaxTimeOverride;
			}

			TimeSpan maxtime = TimeSpan.FromMinutes( dmaxtime );
			
			if ( smaxtime.Contains("s") )
				maxtime = TimeSpan.FromSeconds( dmaxtime );
			else if ( smaxtime.Contains("m") )
				maxtime = TimeSpan.FromMinutes( dmaxtime );
			else if ( smaxtime.Contains("h") )
				maxtime = TimeSpan.FromHours( dmaxtime );
			
			//
			int homerange = Utility.ToInt32( shomerange );
	        int walkingrange = Utility.ToInt32( swalkingrange );
			int spawnid = Utility.ToInt32( sspawnid );
			int npccount = Utility.ToInt32( snpccount );
			int fakecountA = Utility.ToInt32( sfakecountA );
			int fakecountB = Utility.ToInt32( sfakecountB );
			int fakecountC = Utility.ToInt32( sfakecountC );
			int fakecountD = Utility.ToInt32( sfakecountD );
			int fakecountE = Utility.ToInt32( sfakecountE );

			if ( m_MapOverride != -1 )
				map = m_MapOverride;

			if ( m_IDOverride != -1 )
				spawnid = m_IDOverride;

			switch ( map )
			{
				case 0://Trammel and Felucca
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Felucca, mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Trammel, mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					break;
				case 1://Felucca
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Felucca, mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					break;
				case 2://Trammel
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Trammel, mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					break;
				case 3://Ilshenar
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Ilshenar, mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					break;
				case 4://Malas
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Malas, mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					break;
				case 5://Tokuno
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Maps[4], mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					break;
				case 6://TerMur
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Maps[5], mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					break;

				default:
					Console.WriteLine( "Spawn Parser: Warning, unknown map {0}", map );
					break;
			}
		}

		private static void MakeSpawner( string[] types, string[] fakespawnsA, string[] fakespawnsB, string[] fakespawnsC, string[] fakespawnsD, string[] fakespawnsE, int x, int y, int z, Map map, TimeSpan mintime, TimeSpan maxtime, int walkingrange, int homerange, int spawnid, int npccount, int fakecountA, int fakecountB, int fakecountC, int fakecountD, int fakecountE )
		{
			if ( types.Length == 0 )
				return;

			List<string> tipos = new List<string>( types );
			List<string> noneA = new List<string>();
			List<string> noneB = new List<string>();
			List<string> noneC = new List<string>();
			List<string> noneD = new List<string>();
			List<string> noneE = new List<string>();

			if ( fakespawnsA[0] != "" )
				noneA = new List<string>( fakespawnsA );

			if ( fakespawnsB[0] != "" )
				noneB = new List<string>( fakespawnsB );

			if ( fakespawnsC[0] != "" )
				noneC = new List<string>( fakespawnsC );

			if ( fakespawnsD[0] != "" )
				noneD = new List<string>( fakespawnsD );

			if ( fakespawnsE[0] != "" )
				noneE = new List<string>( fakespawnsE );

			PremiumSpawner spawner = new PremiumSpawner( npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE, spawnid, mintime, maxtime, Team, walkingrange, homerange, tipos, noneA, noneB, noneC, noneD, noneE );

			spawner.MoveToWorld( new Point3D( x, y, z ), map );
			
			if ( spawner.SpawnID == 9999 )
			{
				spawner.MinDelay = TimeSpan.FromSeconds( 1.0 );
				spawner.MaxDelay = TimeSpan.FromSeconds( 1.0 );
				spawner.NextSpawn = TimeSpan.FromSeconds( 1.0 );
				spawner.Respawn();
			}
			else if ( spawner.SpawnID == 8888 )
			{
				spawner.MinDelay = TimeSpan.FromMinutes( 10.0 );
				spawner.MaxDelay = TimeSpan.FromMinutes( 15.0 );
				spawner.NextSpawn = TimeSpan.FromSeconds( 1.0 );
				Server.Mobiles.PremiumSpawner.Reconfigure( spawner, true );
			}
			else if ( TotalRespawn && spawner.Running == true )
			{
				spawner.Respawn();
			}
			
			m_Count++;
		}
	}
}
