/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 26/09/2014
 */

using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;
using Server.Regions;

namespace Server.DeepMine
{
	//Parse maps into mines and levels
	//Called when server starts
	public abstract class DeepMineParsing
	{
		public const int Border = 12;//No man's land beetween each region
		
		public static void ParseMap(MapRegister mapregister)
		{
			int mines = mapregister.Mines;
			int levels = mapregister.Levels;
			
			Map map = Map.Maps[mapregister.MapID];
			
			if(map!=null)
			{
				if(!DeepMineInfos.MapDefs.ContainsKey(map))
				{
					Utility.PushColor(ConsoleColor.Cyan);
					Console.WriteLine("Parsing map "+map.Name+" for mines...");
					Utility.PopColor();
					
					DeepMapInfo mapinfo = new DeepMapInfo();
					DeepMineInfos.MapDefs.Add(map, mapinfo);
					
					int x = Border;
					int y = Border;
					
					#region Sanity
					if(mines<1)mines=1;
					if(levels<1)levels=1;
					if(mines>50)mines=50;
					if(levels>50)levels=50;
					#endregion
					
					string name = map.Name;
					
					Rectangle2D[] m_Areas;
					
					int width = (map.Width-(Border*mines))/mines;
					int height = (map.Height-(Border*levels))/levels;
					
					for(int i=1;i<mines+1;i++)
					{
						DeepMineInfo mineinfo = new DeepMineInfo();
						mapinfo.MineDefs.Add(i,mineinfo);
						
						for(int j=1;j<levels+1;j++)
						{							
							DeepLevelInfo levelinfo = new DeepLevelInfo();
							mineinfo.LevelDefs.Add(j,levelinfo);
							
							m_Areas = new Rectangle2D[1];
							
							m_Areas[0] =  new Rectangle2D( x,y,width,height );
							
							string regname = name+"_"+map.MapID.ToString()+"_"+i.ToString()+"/"+mines.ToString()+"_"+j.ToString()+"/"+levels.ToString();
							
							DeepMineRegion m_Region = (DeepMineRegion)Activator.CreateInstance( typeof(DeepMineRegion), new object[]{regname,map,50,m_Areas} );
							
							m_Region.Register();
							
							y+=height+Border;
							
							levelinfo.Region = m_Region;
						}
						
						x+=width+Border;
						y = Border;
					}
				}
				
				Utility.PushColor(ConsoleColor.Cyan);
				Console.WriteLine("Done...");
				Utility.PopColor();
			}
		}
	}
}
