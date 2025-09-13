/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 05/10/2014
 */

using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using UltimaLive;

namespace Server.DeepMine
{
	public static class DeepCaverns
	{
		public static int MinSize = 10;
		
		public static void Initialize()
		{
			Tunneling.System.Digged+=new DigEventHandler( OnDig );
		}
		
		public static void OnDig(object sender, DigEventArgs e)
		{
			if(CheckChance(e))
			{
				if(e.Object is StaticTarget)
				{
					StaticTarget wall = (StaticTarget)e.Object;
					
					if(IsWallDigged(wall))
						CreateCavern(e, wall);
				}
			}
		}
		
		public static bool CheckChance(DigEventArgs e)
		{
			return Utility.RandomDouble()<0.02;
		}
		
		public static bool IsWallDigged(StaticTarget digged)
		{
			return !(Dig.Match(Dig.Rocks,digged.ItemID));
		}
		
		public static void CreateCavern(DigEventArgs e, StaticTarget digged)
		{
			Map map = e.Map;
			DeepMineRegion region = e.DeepRegion;
			Mobile from = e.Mobile;
			Point3D entrywall = new Point3D(0,0,0);
			int dirx=0;
			int diry=0;
			
			if(FindEntryWall(from, map, digged, ref entrywall, ref dirx, ref diry))
			{
				MapOperationSeries moveSeries = new MapOperationSeries(null, map.MapID);

				int depth=GetCavernMaxDepth();
				
				if(CheckDepth(map, region, entrywall, dirx, diry, ref depth, ref moveSeries))
				{
					if(CheckWidth( map, region, entrywall, dirx, diry, depth, ref moveSeries))
					{
						Open(from, map, region, entrywall, dirx, diry, depth, moveSeries);
					}
				}
			}
		}
		
		public static bool FindEntryWall(Mobile from, Map map, StaticTarget digged, ref Point3D entrywall, ref int dirx, ref int diry)
		{
			dirx = digged.X-from.X;
			diry = digged.Y-from.Y;
			
			if(Math.Abs(dirx)+Math.Abs(diry)!=1)//only straight lines, I'm tired
				return false;
			
			int x = digged.X+dirx;
			int y = digged.Y+diry;
			
			StaticTile[] tiles = map.Tiles.GetStaticTiles(x,y);
			
			if(tiles.Length>0)
			{
				StaticTile tile = tiles[0];
				
				if( Dig.Match(Dig.Diggables,tile.ID) && !(Dig.Match(Dig.Rocks,tile.ID)) )
				{
					entrywall = new Point3D(x,y,0);
					
					return true;
				}
			}
			
			return false;
		}
		
		public static int GetCavernMaxDepth()
		{
			double chance = Utility.RandomDouble();
			
			if(chance<70)
				return MinSize;
			else if(chance<90)
				return Utility.RandomMinMax(MinSize,MinSize+(MinSize/2));
			else if(chance<98)
				return Utility.RandomMinMax(MinSize,MinSize*2);
			
			return Utility.RandomMinMax(MinSize,MinSize*3);
		}
		
		public static bool CheckDepth(Map map, DeepMineRegion region, Point3D entrywall, int dirx, int diry, ref int depth, ref MapOperationSeries moveSeries)
		{
			int size = depth;
			depth = 0;
			
			bool stop = false;
			int x = entrywall.X;
			int y = entrywall.Y;
			bool first = true;
			
			for(int i=0;i<size && !stop;i++)
			{
				if(region.Contains(new Point3D(x,y,0)))
				{
					StaticTile[] tiles = map.Tiles.GetStaticTiles(x,y);
					
					if(tiles.Length==0 | first)
					{
						depth++;
						if(first)
						{
							moveSeries = new MapOperationSeries(new SetLandID(x, y, map.MapID, Dig.blank), map.MapID);
							
							first = false;
						}
						else
							moveSeries.Add(new SetLandID(x, y, map.MapID, Dig.blank));
					}
					else
						stop = true;
				}
				else
					stop = true;
				
				x+=dirx;
				y+=diry;
			}
			
			return depth>=MinSize;
		}
		
		public static bool CheckWidth( Map map, DeepMineRegion region, Point3D entrywall, int dirx, int diry, int depth, ref MapOperationSeries moveSeries)
		{
			bool conform = true;
			
			int xentry = entrywall.X;
			int yentry = entrywall.Y;
			int x1 = xentry;
			int y1 = yentry;
			int x2 = xentry;
			int y2 = yentry;
			
			int randomside1 = Utility.RandomMinMax(2,(Utility.RandomBool()?MinSize:MinSize/2));
			int randomside2 = Utility.RandomMinMax(2,(Utility.RandomBool()?MinSize:MinSize/2));
			
			int vector1 = 1;
			int vector2 = 1;
			
			int[] id = Dig.S;
			
			for(int i=0;i<depth-1;i++)
			{
				xentry+=dirx;
				yentry+=diry;
				
				x1 = x2 = xentry;
				y1 = y2 = yentry;
				
				bool stop1 = false;
				for(int side1=1;side1<randomside1 && !stop1 && conform;side1++)
				{
					if(dirx==0)
						x1+=diry;
					else y1+=dirx;
					
					if(!CheckTile(map, region, x1, y1, ref moveSeries, (side1==randomside1-1)))
					{
						stop1 = true;
						
						if(side1<3)
							conform = false;
					}
					else
					{
						if(i==0)
						{
							if(dirx==0)
								id = (diry>0?Dig.N:Dig.S);
							else
								id = (dirx>0?Dig.W:Dig.E);
							
							AddWall(map, x1, y1, ref moveSeries, Dig.GetRandomTile(id));
						}
						else if(i==depth-2)
						{
							if(dirx==0)
								id = (diry>0?Dig.S:Dig.N);
							else
								id = (dirx>0?Dig.E:Dig.W);
							
							AddWall(map, x1, y1, ref moveSeries, Dig.GetRandomTile(id));
						}
					}
				}
				
				if(conform)
				{
					if(stop1)
					{
						if(dirx==0)
							x1-=diry;
						else y1-=dirx;
					}
					
					if(dirx==0)
						id = (diry>0?Dig.E:Dig.W);
					else
						id = (dirx>0?Dig.S:Dig.N);
					
					if(id==Dig.W && vector1<0)
					{
						moveSeries.Add(new SetLandID(x1-1, y1, map.MapID, Dig.blank));
						
						AddWall(map, x1-1, y1, ref moveSeries, Dig.cornere);
						AddWall(map, x1, y1, ref moveSeries, Dig.cornerw);
					}
					else if(id==Dig.N && vector1<0)
					{
						moveSeries.Add(new SetLandID(x1, y1-1, map.MapID, Dig.blank));
						AddWall(map, x1, y1-1, ref moveSeries, Dig.cornere);
						AddWall(map, x1, y1, ref moveSeries, Dig.cornerw);
					}
					else
						AddWall(map, x1, y1, ref moveSeries, Dig.GetRandomTile(id));
				}
				
				bool stop2 = false;
				for(int side2=1;side2<randomside2 && !stop2 && conform;side2++)
				{
					if(dirx==0)
						x2-=diry;
					else y2-=dirx;
					
					if(!CheckTile(map, region, x2, y2, ref moveSeries, (side2==randomside2-1)))
					{
						stop2 = true;
						
						if(side2<3)
							conform = false;
					}
					else
					{
						if(i==0)
						{
							if(dirx==0)
								id = (diry>0?Dig.N:Dig.S);
							else
								id = (dirx>0?Dig.W:Dig.E);
							
							AddWall(map, x2, y2, ref moveSeries, Dig.GetRandomTile(id));
						}
						else if(i==depth-2)
						{
							if(dirx==0)
								id = (diry>0?Dig.S:Dig.N);
							else
								id = (dirx>0?Dig.E:Dig.W);
							
							AddWall(map, x2, y2, ref moveSeries, Dig.GetRandomTile(id));
						}
					}
				}
				
				if(conform)
				{
					if(stop2)
					{
						if(dirx==0)
							x2+=diry;
						else y2+=dirx;
					}
					
					if(dirx==0)
						id = (diry>0?Dig.W:Dig.E);
					else
						id = (dirx>0?Dig.N:Dig.S);
					
					if(id==Dig.N && vector2>0)
					{
						AddWall(map, x2, y2, ref moveSeries, Dig.cornere);
						AddWall(map, x2+1, y2, ref moveSeries, Dig.cornerw);
						AddWall(map, x2, y2+1, ref moveSeries, Dig.cornerw);
					}
					else
						AddWall(map, x2, y2, ref moveSeries, Dig.GetRandomTile(id));
				}
				
				randomside1 += GetVector(ref vector1);
				randomside2 += GetVector(ref vector2);
				
			}
			
			return conform;
		}
		
		public static bool CheckTile(Map map, DeepMineRegion region, int x, int y,  ref MapOperationSeries moveSeries, bool border )
		{
			if(region.Contains(new Point3D(x,y,0)))
			{
				StaticTile[] tiles = map.Tiles.GetStaticTiles(x,y);
				
				if(tiles.Length==0)
				{
					moveSeries.Add(new SetLandID(x, y, map.MapID, Dig.blank));
					
					if(!border && Utility.RandomDouble()<0.1)
						moveSeries.Add(new AddStatic(map.MapID,Dig.GetRandomTile(Dig.E),0,x,y,0));
					
					return true;
				}
			}
			return false;
		}
		
		public static int GetVector(ref int vector)
		{
			vector = Utility.RandomMinMax(-1,1);
			
			return vector;
		}
		
		public static void AddWall(Map map, int x, int y, ref MapOperationSeries moveSeries, int id)
		{
			moveSeries.Add(new AddStatic(map.MapID,id,0,x,y,0));
		}
		
		public static void Open( Mobile from, Map map, DeepMineRegion region, Point3D entrywall, int dirx, int diry, int depth, MapOperationSeries moveSeries)
		{
			StaticTile[] tiles = map.Tiles.GetStaticTiles(entrywall.X,entrywall.Y);
			
			if(tiles.Length>0)
			{
				StaticTile tile = tiles[0];
				
				moveSeries.Add(new DeleteStatic(map.MapID,new StaticTarget(entrywall,tile.ID)));
			}
			
			int[] id = Dig.S;
			
			if(dirx==0)
				id=(diry>0?Dig.S:Dig.N);
			else
				id=(dirx>0?Dig.E:Dig.W);
			
			moveSeries.Add(new AddStatic(map.MapID,Dig.GetRandomTile(id),0,entrywall.X+(dirx*(depth-1)),entrywall.Y+(diry*(depth-1)),0));
			
			moveSeries.DoOperation();
			
			from.SendMessage("You find a cavern !!");
			
			from.PlaySound(0x245);
		}
	}
}