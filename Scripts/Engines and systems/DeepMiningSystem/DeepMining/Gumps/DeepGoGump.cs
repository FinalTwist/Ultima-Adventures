/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 26/09/2014
 */

using System;
using System.Collections.Generic;
using Server;
using Server.Commands;
using DynamicGumps;

namespace Server.DeepMine
{
	public enum DeepGoPage { None,Map,Mine,Level}
	
	[GumpHeader(Profile.Paper, IsClosable.True, "DeepMines Go Gump")]
	public class DeepGoGump
	{
		#region DeepGo command
		public static void Initialize()
		{
			CommandSystem.Register( "DeepGo", AccessLevel.GameMaster, new CommandEventHandler( DeepGo_OnCommand ) );
		}
		
		private static void DeepGo_OnCommand( CommandEventArgs e )
		{
			Mobile m = e.Mobile;
			
			m.SendGump(new DynamicGump(m, new DeepGoGump()));
		}
		#endregion
		
		public DeepGoGump()
		{
			foreach(Map map in DeepMineInfos.MapDefs.Keys)
			{
				if(!DeepMaps.Contains(map))
					DeepMaps.Add(map);
			}
		}
		
		private DeepGoPage CurrentPage = DeepGoPage.Map;
		
		private List<Map> DeepMaps = new List<Map>();
		
		private Map CurrentMap = null;
		
		private int CurrentMine = 0;
		
		[GumpElement(DisplayMode.Blank)]
		public int blank;
		
		#region SelectMap
		
		[GumpElement(DisplayMode.List)]
		public int MapIndex =0;
		public bool IsActive_MapIndex()
		{
			return CurrentPage== DeepGoPage.Map && DeepMaps.Count>0;
		}
		public string GetData_MapIndex()
		{
			return DeepMaps[MapIndex].Name;
		}
		public void CallBack_MapIndex_Up(Mobile from)
		{
			if(MapIndex<DeepMaps.Count-1)
				MapIndex++;
			
			from.SendGump(new DynamicGump(from, this));
		}
		public void CallBack_MapIndex_Down(Mobile from)
		{
			if(MapIndex>0)
				MapIndex--;
			
			from.SendGump(new DynamicGump(from, this));
		}
		public void CallBack_MapIndex(Mobile from)
		{
			CurrentMap = DeepMaps[MapIndex];
			
			CurrentPage = DeepGoPage.Mine;
			
			from.SendGump(new DynamicGump(from, this));
		}
		#endregion
				
		#region SelectMine
		[GumpElement(DisplayMode.LabelData)]
		public int SelectedMap;
		public bool IsActive_SelectedMap()
		{
			return CurrentPage==DeepGoPage.Mine || CurrentPage==DeepGoPage.Level;
		}
		public string GetData_SelectedMap()
		{
			return "Map : "+CurrentMap.Name;
		}
		
		[GumpElement(DisplayMode.List)]
		public int Mines=1;
		public bool IsActive_Mines()
		{
			return CurrentPage==DeepGoPage.Mine;
		}
		public string GetData_Mines()
		{
			return "Mine "+Mines.ToString();
		}
		public void CallBack_Mines_Up(Mobile from)
		{
			if(Mines<DeepMineInfos.MapDefs[CurrentMap].MineDefs.Count)
				Mines++;
			
			from.SendGump(new DynamicGump(from,this));
		}
		public void CallBack_Mines_Down(Mobile from)
		{
			if(Mines>1)
				Mines--;
			
			from.SendGump(new DynamicGump(from,this));
		}
		public void CallBack_Mines(Mobile from)
		{
			CurrentPage = DeepGoPage.Level;
			
			CurrentMine = Mines;
			
			from.SendGump(new DynamicGump(from,this));
		}
		#endregion
		
		#region SelectLevel
		[GumpElement(DisplayMode.LabelData)]
		public int SelectedMine;
		public bool IsActive_SelectedMine()
		{
			return CurrentPage==DeepGoPage.Level;
		}
		public string GetData_SelectedMine()
		{
			return "Mine : "+CurrentMine.ToString();
		}
		
		[GumpElement(DisplayMode.List)]
		public int Levels=1;
		public bool IsActive_Levels()
		{
			return CurrentPage==DeepGoPage.Level;
		}
		public string GetData_Levels()
		{
			return "Level "+Levels.ToString();
		}
		public void CallBack_Levels_Up(Mobile from)
		{
			if(Levels<DeepMineInfos.MapDefs[CurrentMap].MineDefs[0].LevelDefs.Count)
				Levels++;
			
			from.SendGump(new DynamicGump(from,this));
		}
		public void CallBack_Levels_Down(Mobile from)
		{
			if(Levels>1)
				Levels--;
			
			from.SendGump(new DynamicGump(from,this));
		}
/*		public void CallBack_Levels(Mobile from)
		{
			DeepMineRegion region = DeepMineInfos.MapDefs[CurrentMap].MineDefs[CurrentMine-1].LevelDefs[Levels-1].Region;
			
			region.Bring(from);
			
			from.SendGump( new DynamicGump(from, this));
		}*/
		public void CallBack_Levels(Mobile from)
		{
			if(DeepMineInfos.MapDefs.ContainsKey(CurrentMap))
			{
				if(DeepMineInfos.MapDefs[CurrentMap].MineDefs.ContainsKey(CurrentMine-1))
				{
					if(DeepMineInfos.MapDefs[CurrentMap].MineDefs[CurrentMine-1].LevelDefs.ContainsKey(Levels-1))
					{
						DeepMineRegion region = DeepMineInfos.MapDefs[CurrentMap].MineDefs[CurrentMine-1].LevelDefs[Levels-1].Region;
						
						region.Bring(from);
					}
					else
						Console.WriteLine("Level index error");
				}
				else
					Console.WriteLine("Mine index error");
			}
			else
				Console.WriteLine("map index error");
			
			from.SendGump( new DynamicGump(from, this));
		}
		#endregion
		
		#region SetMine
		[GumpElement(DisplayMode.Blank)]
		public int SetMineBlank;
		public bool IsActive_SetMineBlank()
		{
			return CurrentPage==DeepGoPage.Level;
		}
		
		[GumpElement(DisplayMode.LabelButton,"Edit Mine properties at this level")]
		public int SetMine;
		public bool IsActive_SetMine()
		{
			return CurrentPage==DeepGoPage.Level;
		}
		public void CallBack_SetMine(Mobile from)
		{
			from.CloseGump(typeof(DynamicGump));
			
			from.SendGump( new DynamicGump(from, new DeepSetHarvestGump(DeepMineInfos.MapDefs[CurrentMap].MineDefs[CurrentMine-1])));
			
			from.SendGump( new DynamicGump(from, this));
		}
		#endregion
	}
}