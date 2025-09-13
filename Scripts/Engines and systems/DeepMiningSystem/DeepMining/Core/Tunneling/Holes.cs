/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 18/09/2014
 */

using System;
using Server;
using Server.Items;
using Server.Regions;
using Server.Targeting;
using UltimaLive;

namespace Server.DeepMine
{
	public static class Holes
	{
		public static bool CheckHole( Map map, Point3D location, int offsetx, int offsety, int id )
		{
			DeepMineRegion reg = null;
			
			if(!FindDeepMine(map, location, ref reg))
				return false;
			
			if(IsBottom(reg))
				return false;
			
			if(HoleNearby(map, location))
				return false;
			
			int secondid = 0;
			
			if(!EligibleUp(map, location, offsetx, offsety, ref secondid))
				return false;
			
			Point3D dest = new Point3D(0,0,0);
			
			if(!EligibleDown(map, location, offsetx, offsety, reg, ref dest))
				return false;
			
			if(!GetChance())
				return false;
			
			DigHole(map,location, offsetx, offsety, id, secondid, dest);
			
			return true;
			
		}
		
		private static bool FindDeepMine(Map map, Point3D location, ref DeepMineRegion reg)
		{
			Region region = Region.Find(location, map);
			
			if(region!=null && region is DeepMineRegion)
			{
				reg = (DeepMineRegion)region;
				
				return true;
			}
			
			return false;
		}
		
		private static bool IsBottom(DeepMineRegion region)
		{
			if(region!=null)
			{
				if(region.Level<region.MaxLevel)
					return false;
			}
			
			return true;
		}
		
		private static bool HoleNearby(Map map, Point3D location)
		{
			IPooledEnumerable holes = map.GetItemsInRange(location, 20);
			
			foreach(object o in holes)
			{
				if(o is Hole)
					return true;
			}
			
			holes.Free();
			
			return false;
		}
		
		private static bool EligibleUp(Map map, Point3D location, int offsetx, int offsety, ref int secondid)
		{
			StaticTile[] sides = map.Tiles.GetStaticTiles(location.X+offsetx,location.Y+offsety);
			
			if(sides!=null && sides.Length>0)
			{
				StaticTile side = sides[0];
				
				secondid = side.ID;
				
				int[] wall = Dig.W;
				if(offsetx==1)
					wall = Dig.N;
				
				
				return Dig.Match(wall,secondid);
			}
			
			return false;
		}
		
		private static bool EligibleDown(Map map, Point3D location, int offsetx, int offsety, DeepMineRegion region, ref Point3D dest)
		{
			dest = new Point3D(location.X+DeepMineParsing.Border+region.Area[0].Width, location.Y+DeepMineParsing.Border+region.Area[0].Height, 0);
			
			for(int i=-2;i<3;i++)
			{
				for(int j=-2;j<3;j++)
				{
					LandTile tile = map.Tiles.GetLandTile(dest.X+i,dest.Y+j);
					if(tile.ID!=2)
						return false;
				}
			}
			
			return true;
		}
		
		private static bool GetChance()
		{
			return true;
		}
		
		private static void DigHole(Map map, Point3D location, int offsetx, int offsety, int id, int secondid, Point3D dest)
		{
			new AddStatic(map.MapID,id,0,location.X,location.Y,0).DoOperation();
			new DeleteStatic(map.MapID,new StaticTarget(new Point3D(location.X+offsetx,location.Y+offsetx,0),secondid)).DoOperation();
			new AddStatic(map.MapID,id+1,0,location.X+offsetx,location.Y+offsety,0).DoOperation();
			
			int dx = 0;
			int dy = 0;
			Direction facing = Direction.Running;
			
			if(offsetx==0)
			{
				dx++;
				facing = Direction.East;
			}
			else
			{
				dy++;
				facing = Direction.South;
			}
			
			Point3D hole1 = new Point3D(location.X+dx,location.Y+dy,1);
			Point3D hole2 = new Point3D(location.X+offsetx+dx,location.Y+offsety+dy,1);
			Point3D hole3 = new Point3D(dest.X+dx,dest.Y+dy,1);
			Point3D hole4 = new Point3D(dest.X+offsetx+dx,dest.Y+offsety+dy,1);
			
			new Hole(hole3,map, facing, false).MoveToWorld(hole1, map);
			new Hole(hole4,map, facing, false).MoveToWorld(hole2, map);
			
			new AddStatic(map.MapID,id,0,dest.X,dest.Y,0).DoOperation();
			new AddStatic(map.MapID,id+1,0,dest.X+offsetx,dest.Y+offsety,0).DoOperation();
			
			new Hole(hole1,map, facing, true).MoveToWorld(hole3, map);
			new Hole(hole2,map, facing, true).MoveToWorld(hole4, map);
			
			Dig.Do(map,hole3);
			Dig.Do(map,hole4);
		}
	}
	
	public class Hole : Teleporter
	{
		public Hole(Point3D pointDest, Map mapDest, Direction facing, bool up) : base( pointDest, mapDest, false )
		{
			Active = true;
			Direction = facing;
			Up = up;
			Name = "Hole";
		}
		
		private bool Up;
		
		public Hole( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( (bool) Up );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			Up = reader.ReadBool();
		}
		
		public override void DoTeleport( Mobile m )
		{
			base.DoTeleport(m);
			
			Region reg = Region.Find(m.Location,m.Map);
			if(reg!=null && reg is DeepMineRegion)
			{
				if(Up)
					OnLevelUp(new DigEventArgs(m, m.Map,(DeepMineRegion)reg, null, null));
				else
					OnLevelDown(new DigEventArgs(m, m.Map,(DeepMineRegion)reg, null, null));
			}
		}
		
		//Event for external use
		public event LevelDownEventHandler LevelDown;
		protected virtual void OnLevelDown(DigEventArgs e)
		{
			if (LevelDown != null)
				LevelDown(this, e);
		}
		
		public event LevelUpEventHandler LevelUp;
		protected virtual void OnLevelUp(DigEventArgs e)
		{
			if (LevelUp != null)
				LevelUp(this, e);
		}
		
		public event HoleFoundEventHandler HoleFound;
		protected virtual void OnHoleFound(DigEventArgs e)
		{
			if (HoleFound != null)
				HoleFound(this, e);
		}
	}
}