/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 12/09/2014
 */

using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Regions;

namespace Server.DeepMine
{
	public class DeepMineRegion : BaseRegion
	{
		public DeepMineRegion(string name, Map map, int priority, Rectangle2D[] area) : base( name, map, priority, area)
		{
			
		}
		
		public int Mine { get { return (Convert.ToInt32((Name.Split('_')[2]).Split('/')[0]))-1; } }
		public int Level { get { return (Convert.ToInt32((Name.Split('_')[3]).Split('/')[0]))-1; } }
		public int MaxLevel { get { return Convert.ToInt32((Name.Split('_')[3]).Split('/')[1]); } }
		
		#region Methods
		public void Bring(Mobile from)
		{
			int x = Area[0].Start.X + (Area[0].Width/2);
			int y = Area[0].Start.Y + Area[0].Height -2;
			
			Point3D entrance = new Point3D(x,y,0);
			
			LandTile tile = Map.Tiles.GetLandTile(x,y);
			if(tile.ID==2)
			Dig.Do(Map, entrance);
			
			
			from.MoveToWorld(entrance, Map);
		}
		
		#endregion
	}
}