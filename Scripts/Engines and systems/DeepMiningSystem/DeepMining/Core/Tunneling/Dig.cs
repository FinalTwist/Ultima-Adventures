/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 13/09/2014
 */

using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Regions;
using Server.Engines.Harvest;

using UltimaLive;//obviously

using DynamicGumps;

namespace Server.DeepMine
{
	public static class Dig
	{
		#region ItemID
		public static int[] Diggables;
		
		public static void Initialize()
		{
			List<int> list = new List<int>();
			
			list.AddRange(Rocks);
			list.AddRange(W);
			list.AddRange(N);
			list.AddRange(E);
			list.AddRange(S);
			list.AddRange(C);
			list.Add(cornere);
			list.Add(cornerw);
			
			list.Sort();
			
			Diggables = new int[list.Count];
			int index = 0;
			foreach(int id in list)
			{
				Diggables[index] = id;
				index++;
			}
			
			Tunneling.System.Definitions[0].Tiles = Diggables;
		}
		
		public static int blank = 585;
		public static int[] Rocks = new int[]{4963,955,4964,4965,4966,4967,4968,4969,4970,4971,4972,4973,4976,
			6003,6004,6007,6008,17886,17900,17901,17906,17907,17908,17909,18013,18047,18048,
			18118,18119,18120,18121,18122,18123,18396,18397,18398,18399,18400};
		
		public const int black = 2; //TODO something like  private int black { get return map.getlandtile(0.0).tileID }
		public static int[] W = new int[]{604,606,608,614,618}; 
		public static int[] N = new int[]{605,607,609,613,617};
		public static int[] E = new int[]{2272,2273,2277,2281};
		public static int[] S = new int[]{2272,2273,2276,2282};
		private static int[] C = new int[]{2273};
		public const int cornerw = 612;
		public const int cornere = 610;
		private const int holew = 8550;
		private const int holen = 8552;
		private static int[] holes = new int[]{holew,holen, holew+1,holen+1};
		#endregion
		
		#region Utilities
		public static int GetRandomTile(int[] list)
		{
			return list[Utility.Random(list.Length-1)];
		}
		
		public static bool Match(int[] list, int id)
		{
			for(int i=0; i<list.Length; i++)
			{
				if(list[i]==id)
					return true;
			}
			
			return false;
		}
		#endregion
		
		public static void Do(Map map, object harvested)
		{
			Point3D location;
			
			bool rock = false;
			
			if(harvested is StaticTarget)
			{
				StaticTarget item = (StaticTarget)harvested;
				
				location = item.Location;
				
				if(!Diggable(map, item))
					return;
				
				rock = Match(Rocks,item.ItemID);
				
				new DeleteStatic(map.MapID, item).DoOperation();
			}
			else
			{
				location = new Point3D( ((IPoint3D)harvested).X, ((IPoint3D)harvested).Y, 0);
			}
			
			new SetLandID(location.X, location.Y, map.MapID, blank).DoOperation();
			
			if(Utility.RandomDouble()<0.1 && !rock)
			{
				new AddStatic(map.MapID,GetRandomTile(Rocks),0,location.X,location.Y,0).DoOperation();
			}
			
			CreateWalls(map, location);
		}
		
		#region Tests
		private static bool Diggable(Map map, StaticTarget item)
		{
			StaticTile[] sides = null;
			
			sides = map.Tiles.GetStaticTiles(item.Location.X, item.Location.Y-1);
			
			if(IsHole(sides))
				return false;
			
			sides = map.Tiles.GetStaticTiles(item.Location.X, item.Location.Y+1);
			
			if(IsHole(sides))
				return false;
			
			return true;
		}
		
		private static bool IsHole(StaticTile[] sides)
		{
			if(sides!=null && sides.Length>0)
			{
				StaticTile side = sides[0];
				
				if(Match(holes, side.ID))
					return true;
			}
			
			return false;
		}
		#endregion
		
		#region CreateWalls
		public static void CreateWalls(Map map, Point3D digloc)
		{
			CreateWallWest(map, digloc);
			CreateWallNorth(map, digloc);
			CreateWallNorthWest(map, digloc);
			CreateWallSouth(map, digloc);
			CreateWallEast(map, digloc);
			CreateWallSouthEastCorner(map, digloc);
		}
		
		public static void CreateWallWest(Map map, Point3D digloc )
		{
			Point3D wallw = new Point3D(digloc.X-1,digloc.Y,0);
			
			StaticTile[] staticstiles = null;
			StaticTile statictile;
			LandTile landtile;
			
			staticstiles =  map.Tiles.GetStaticTiles(wallw.X,wallw.Y);
			
			if(staticstiles!=null && staticstiles.Length>0)//wall on west
			{
				statictile = staticstiles[0];
				
				if(Match(holes,statictile.ID) || Match(S, statictile.ID) ||Match(Rocks,statictile.ID))
					return;
				
				if(Match(N, statictile.ID))
				{
					new DeleteStatic(map.MapID,new StaticTarget(new Point3D(wallw.X,wallw.Y,0),statictile.ID)).DoOperation();
					new AddStatic(map.MapID,cornerw,0,wallw.X,wallw.Y,0).DoOperation();
				}
				else if(!Match(W, statictile.ID))
				{
					new DeleteStatic(map.MapID,new StaticTarget(new Point3D(wallw.X,wallw.Y,0),statictile.ID)).DoOperation();
					
					if(!Holes.CheckHole(map,wallw,0,-1,holew))
						new AddStatic(map.MapID,GetRandomTile(W),0,wallw.X,wallw.Y,0).DoOperation();
				}
			}
			else //no wall on west
			{
				landtile = map.Tiles.GetLandTile(wallw.X,wallw.Y);
				if(landtile.ID!=blank)
				{
					new SetLandID(wallw.X, wallw.Y, map.MapID, blank).DoOperation();
					new AddStatic(map.MapID, GetRandomTile(W), 0, wallw.X,wallw.Y,0).DoOperation();
				}
			}
		}
		
		public static void CreateWallNorth(Map map, Point3D digloc )
		{
			Point3D walln = new Point3D(digloc.X,digloc.Y-1,0);
			
			StaticTile[] staticstiles = null;
			StaticTile statictile;
			LandTile landtile;
			
			staticstiles =  map.Tiles.GetStaticTiles(walln.X,walln.Y);
			
			if(staticstiles!=null && staticstiles.Length>0)//wall on north
			{
				statictile = staticstiles[0];
				
				if(Match(holes,statictile.ID) || Match(Rocks,statictile.ID) )
					return;
				
				if(Match(W, statictile.ID))
				{
					new DeleteStatic(map.MapID,new StaticTarget(new Point3D(walln.X,walln.Y,0),statictile.ID)).DoOperation();
					new AddStatic(map.MapID,cornerw,0,walln.X,walln.Y,0).DoOperation();
				}
				else if(!Match(N, statictile.ID))
				{
					new DeleteStatic(map.MapID,new StaticTarget(new Point3D(walln.X,walln.Y,0),statictile.ID)).DoOperation();
					
					if(!Holes.CheckHole(map,walln,1,0,holen))
						new AddStatic(map.MapID,GetRandomTile(N),0,walln.X,walln.Y,0).DoOperation();
				}
			}
			else
			{
				landtile = map.Tiles.GetLandTile(walln.X,walln.Y);
				if(landtile.ID!=blank)
				{
					new SetLandID(walln.X, walln.Y, map.MapID, blank).DoOperation();
					
					if(!Holes.CheckHole(map,walln,1,0,holen))
						new AddStatic(map.MapID, GetRandomTile(N), 0, walln.X,walln.Y,0).DoOperation();
				}
			}
			
		}
		
		public static void CreateWallNorthWest(Map map, Point3D digloc )
		{
			Point3D wallnw = new Point3D(digloc.X-1,digloc.Y-1,0);
			
			StaticTile[] staticstiles = null;
			StaticTile statictile;
			LandTile landtile;
			
			staticstiles =  map.Tiles.GetStaticTiles(wallnw.X,wallnw.Y);
			
			if(staticstiles!=null && staticstiles.Length>0)//wall on northwest
			{
				statictile = staticstiles[0];
				
				if(Match(holes, statictile.ID) || Match(Rocks,statictile.ID))
					return;
				
				if(Match(E, statictile.ID) || Match(S, statictile.ID))
					return;
				
				if(statictile.ID==cornerw)
					return;
				
				if(!Match(W,statictile.ID) && !Match(N, statictile.ID) )
				{
					if(statictile.ID!=cornere)
					{
						new DeleteStatic(map.MapID,new StaticTarget(new Point3D(wallnw.X,wallnw.Y,0),statictile.ID)).DoOperation();
						new AddStatic(map.MapID,cornere,0,wallnw.X,wallnw.Y,0).DoOperation();
					}
				}
			}
			else
			{
				landtile = map.Tiles.GetLandTile(wallnw.X,wallnw.Y);
				if(landtile.ID!=blank)
				{
					new SetLandID(wallnw.X, wallnw.Y, map.MapID, blank).DoOperation();
					new AddStatic(map.MapID, cornere, 0, wallnw.X,wallnw.Y,0).DoOperation();
				}
			}
		}
		
		public static void CreateWallSouth(Map map, Point3D digloc )
		{
			Point3D walls = new Point3D(digloc.X,digloc.Y+1,0);
			Point3D wallsw = new Point3D(digloc.X-1,digloc.Y+1,0);
			
			LandTile landtile = map.Tiles.GetLandTile(digloc.X,digloc.Y+1);
			
			if(landtile.ID==black)
			{
				new SetLandID(walls.X, walls.Y, map.MapID, blank).DoOperation();
				new AddStatic(map.MapID, GetRandomTile(S), 0, walls.X,walls.Y,0).DoOperation();
			}
			
			landtile = map.Tiles.GetLandTile(digloc.X-1,digloc.Y+1);
			
			if(landtile.ID==black)
			{
				new SetLandID(wallsw.X, wallsw.Y, map.MapID, blank).DoOperation();
				new AddStatic(map.MapID, GetRandomTile(S), 0, wallsw.X,wallsw.Y,0).DoOperation();
			}
		}
		
		public static void CreateWallEast(Map map, Point3D digloc )
		{
			Point3D walle = new Point3D(digloc.X+1,digloc.Y,0);
			Point3D wallne = new Point3D(digloc.X+1,digloc.Y-1,0);
			
			StaticTile[] staticstiles = null;
			StaticTile statictile;
			
			staticstiles =  map.Tiles.GetStaticTiles(wallne.X,wallne.Y);
			
			if(staticstiles!=null && staticstiles.Length>0)
			{
				statictile = staticstiles[0];
				
				if(Match(holes, statictile.ID))
					return;
			}
			
			LandTile landtile = map.Tiles.GetLandTile(digloc.X+1,digloc.Y);
			
			if(landtile.ID==black)
			{
				new SetLandID(walle.X, walle.Y, map.MapID, blank).DoOperation();
				new AddStatic(map.MapID, GetRandomTile(E), 0, walle.X,walle.Y,0).DoOperation();
			}
			
			landtile = map.Tiles.GetLandTile(digloc.X+1,digloc.Y-1);
			
			if(landtile.ID==black)
			{
				new SetLandID(wallne.X, wallne.Y, map.MapID, blank).DoOperation();
				new AddStatic(map.MapID, GetRandomTile(E), 0, wallne.X,wallne.Y,0).DoOperation();
			}
		}
		
		public static void CreateWallSouthEastCorner(Map map, Point3D digloc )
		{
			Point3D wallse = new Point3D(digloc.X+1,digloc.Y+1,0);
			
			LandTile landtile = map.Tiles.GetLandTile(digloc.X+1,digloc.Y+1);
			
			if(landtile.ID==black)
			{
				new SetLandID(wallse.X, wallse.Y, map.MapID, blank).DoOperation();
				new AddStatic(map.MapID, GetRandomTile(C), 0, wallse.X,wallse.Y,0).DoOperation();
			}
		}
		#endregion
	}
}