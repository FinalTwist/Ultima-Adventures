/*
 * Crée par SharpDevelop.
 * Gargouille 
 * Date: 03/10/2014
 */

using System;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.DeepMine
{
	public static class DeepMineMobileSpawn
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
				if(DeepMineInfos.MapDefs[e.Map].MineDefs[e.DeepRegion.Mine].MobileInfo.MobileTypes.Count>0)
				{
					if(CheckChance())
					{
						Type t = GetSpawnType(e.DeepRegion);
						
						if(t!=null)
						{
							Point3D loc = e.Mobile.Location;
							Server.Spells.SpellHelper.FindValidSpawnLocation(e.Map, ref loc, false);
							
							if(loc!=e.Mobile.Location)
							{
								Mobile mob = (Mobile)Activator.CreateInstance(t);
								
								mob.MoveToWorld(loc,e.Map);
							}
						}
					}
				}
			}
		}
		
		public static bool CheckChance()
		{
			return Utility.RandomDouble()<ChanceToCheck;
		}
		
		public static Type GetSpawnType(DeepMineRegion region)
		{
			Map map = region.Map;
			
			List<MobileDetails> list = new List<MobileDetails>(DeepMineInfos.MapDefs[map].MineDefs[region.Mine].MobileInfo.MobileTypes);
			if(list!=null && list.Count>0)
			{
				List<Type> leveledlist = new List<Type>();
				foreach(MobileDetails detail in list)
				{
					if(detail.FirstLevel<=region.Level)
						leveledlist.Add(detail.MobileType);
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
