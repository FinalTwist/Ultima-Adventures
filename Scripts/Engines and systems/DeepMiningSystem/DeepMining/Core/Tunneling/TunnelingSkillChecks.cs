/*
 * Crée par SharpDevelop.
 * Gargouille 
 * Date: 14/09/2014
 */

using System;
using Server;
using Server.Mobiles;
using Server.Regions;
using Server.Engines.Harvest;

namespace Server.DeepMine
{
	public static class TunnelingSkillChecks
	{
		//Time we have to dig
		public static HarvestDefinition Altering(Mobile from, HarvestDefinition def)
		{
			int diggingTime = 1;
			
			//Time needed to dig... Here its 8 effects at 120 skill
			diggingTime = 20-((from.Skills[SkillName.Mining].BaseFixedPoint)/100);
			
			//Scaling with mine deepth
			Region region = Region.Find(from.Location, from.Map);
			if(region!=null && region is DeepMineRegion)
			{
				DeepMineRegion dr = (DeepMineRegion)region;
				
				int scale = dr.Level/2;
				
				diggingTime*=Math.Max(scale,1);
			}
			
			
			diggingTime=1;//during phase test
			
			def.EffectCounts = new int[]{diggingTime};
			
			return def;
		}
	}
}