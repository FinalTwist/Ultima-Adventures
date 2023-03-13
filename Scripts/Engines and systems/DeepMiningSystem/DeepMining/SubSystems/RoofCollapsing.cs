/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 05/10/2014
 */

using System;
using Server;
using Server.Targeting;
using UltimaLive;

namespace Server.DeepMine
{
	public static class RoofCollapsing
	{
		public static int BaseDamage = 10;
		
		public static void Initialize()
		{
			Tunneling.System.Digged+=new DigEventHandler( OnDig );
		}
		
		public static void OnDig(object sender, DigEventArgs e)
		{
			if(CheckChance(e.Mobile))
				Collapse(e);
		}
		
		public static bool CheckChance(Mobile from)
		{
			double chance = 0.5-(from.Skills[SkillName.Mining].Base/400);
			
			return Utility.RandomDouble()<chance;
		}
		
		public static void Collapse(DigEventArgs e)
		{
			Mobile from = e.Mobile;
			Map map = e.Map;
			
			int fx = from.Location.X;
			int fy = from.Location.Y;
			
			int damage = BaseDamage;
			
			from.PlaySound(0x20E);
			
			for(int i=0;i<offsets.Length;i+=2)
			{
				if(Utility.RandomBool())
				{
					int x = fx+offsets[i];
					int y = fy+offsets[i+1];
					
					LandTile tile = map.Tiles.GetLandTile(x,y);
					
					if(tile.ID==Dig.blank)
					{
						StaticTile[] stc = map.Tiles.GetStaticTiles(x,y);
						if(stc.Length==0 || Dig.Match(Dig.Rocks,stc[0].ID))
						{
							int rock = Dig.GetRandomTile(Dig.Rocks);
							
							damage+=BaseDamage;
							
							Point3D startLoc = new Point3D( x, y, 30 );
							Point3D endLoc = new Point3D( x,y,0);
							
							Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc, map ), new Entity( Serial.Zero, endLoc, map ),
							                         rock, 5, 0, false, false );
							
							Effects.SendLocationEffect(endLoc,map,0x3728,1);
							
							if((offsets[i]+offsets[i+1])==0)
								from.Damage(Utility.RandomMinMax(BaseDamage,damage));
							
							if(stc.Length>0 && stc[0].ID!=Dig.Rocks[0])
								new DeleteStatic(map.MapID,new StaticTarget(new Point3D(x,y,0),stc[0].ID)).DoOperation();
							
							new AddStatic(map.MapID,rock,0,x,y,0).DoOperation();
							
						}
					}
				}
			}
		}
		
		public static int[] offsets = new int[]
		{
			-1,-1,
			-1,0,
			-1,1,
			0,-1,
			0,1,
			1,-1,
			1,0,
			1,1,
			0,0
		};
	}
}