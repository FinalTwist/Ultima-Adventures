/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 29/09/2014
 */

using System;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.DeepMine
{
	public static class DeepMineOreSpawn
	{
		private static int MinDistance = 10;
		private static double ChanceToCheck = 0.3;
		
		public static void Initialize()
		{
			Tunneling.System.Digged+=new DigEventHandler( OnDig );
		}
		
		public static void OnDig(object sender, DigEventArgs e)
		{
			if(DeepMineInfos.MapDefs.ContainsKey(e.Map) && DeepMineInfos.MapDefs[e.Map].MineDefs.ContainsKey(e.DeepRegion.Mine) && DeepMineInfos.MapDefs[e.Map].MineDefs[e.DeepRegion.Mine].LevelDefs.ContainsKey(e.DeepRegion.Level))
			{
				if(DeepMineInfos.MapDefs[e.Map].MineDefs[e.DeepRegion.Mine].HarvestInfo.OreTypes.Count>0)
				{
					if(CheckChance())
					{
						DeepHarvestSpot spot = null;
						if(!CheckSpotNearBy(e.Map,e.Mobile.Location,ref spot))
						{
							SetSpot(e);
						}
					}
				}
			}
		}
		
		public static bool CheckChance()
		{
			return Utility.RandomDouble()<ChanceToCheck;
		}
		
		public static bool CheckSpotNearBy(Map map, Point3D loc, ref DeepHarvestSpot spot)
		{
			Region region = Region.Find(loc, map);
			if(region!=null && region is DeepMineRegion)
			{
				DeepMineRegion dr = (DeepMineRegion)region;
				
				DeepMineInfo info = DeepMineInfos.MapDefs[map].MineDefs[dr.Mine];
				
				int X = loc.X;
				int Y = loc.Y;
				
				if(info!=null)
				{
					foreach(DeepHarvestSpot sp in info.LevelDefs[dr.Level].Spots)
					{
						if(Math.Abs(X-sp.X)<MinDistance)
						{
							if(Math.Abs(Y-sp.Y)<MinDistance)
							{
								if(spot!=null)
									spot = sp;
								
								return true;
							}
						}
					}
				}
			}
			
			return false;
		}
		
		public static void SetSpot(DigEventArgs e)
		{
			Point3D loc = e.Mobile.Location;
			Map map = e.Map;
			
			Console.Write("Try to set spot ");
			
			for(int i=0;i<MinDistance*2;i++)
			{
				Console.Write(".");
				
				int x = Utility.RandomMinMax(5,15)*(Utility.RandomBool()?1:-1);
				int y = Utility.RandomMinMax(5,15)*(Utility.RandomBool()?1:-1);
				
				if(map.Tiles.GetLandTile(loc.X+x,loc.Y+y).ID==Dig.black)
				{
					DeepMineInfos.MapDefs[map].MineDefs[e.DeepRegion.Mine].LevelDefs[e.DeepRegion.Level].Spots.Add(new DeepHarvestSpot(loc.X+x,loc.Y+y,GetSpawnType(e.DeepRegion)));
					
					Console.WriteLine("Spot found in "+e.DeepRegion.Name+" at "+(loc.X+x).ToString()+":"+(loc.Y+y).ToString());
					
					return;
				}
			}
		}
		
		public static Type GetSpawnType(DeepMineRegion region)
		{
			Map map = region.Map;
			
			List<HarvestDetails> list = new List<HarvestDetails>(DeepMineInfos.MapDefs[map].MineDefs[region.Mine].HarvestInfo.OreTypes);
			if(list!=null && list.Count>0)
			{
				List<Type> leveledlist = new List<Type>();
				foreach(HarvestDetails detail in list)
				{
					if(detail.FirstLevel<=region.Level)
						leveledlist.Add(detail.OreType);
				}
				
				if(leveledlist.Count>0)
				{
					Type t = leveledlist[Utility.RandomMinMax(0,leveledlist.Count-1)];
					
					return t;
				}
			}
			
			return null;
		}
	}
}
