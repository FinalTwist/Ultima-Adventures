/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 27/09/2014
 */

using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Commands;
using DynamicGumps;

namespace Server.DeepMine
{
	[GumpHeader(Profile.Paper,IsClosable.True,"MINE HARVEST SETTING")]
	public class DeepSetHarvestGump
	{
		#region SetMine command
		public static void Initialize()
		{
			CommandSystem.Register( "SetMine", AccessLevel.GameMaster, new CommandEventHandler( SetMine_OnCommand ) );
		}
		
		private static void SetMine_OnCommand( CommandEventArgs e )
		{
			Mobile m = e.Mobile;
			
			Region region = Region.Find(m.Location, m.Map);
			if(region!=null && region is DeepMineRegion)
			{
				DeepMineRegion dr = (DeepMineRegion)region;
				
				m.SendGump(new DynamicGump(m, new DeepSetHarvestGump(DeepMineInfos.MapDefs[dr.Map].MineDefs[dr.Mine])));
			}
			else m.SendMessage("Command only works in DeepMine regions...");
			
		}
		#endregion
		
		public DeepSetHarvestGump(DeepMineInfo info)
		{
			m_Mine = info;
		}
		
		private DeepMineInfo m_Mine;
		
		[GumpElement(DisplayMode.LabelData)]
		public int Title;
		public string GetData_Title()
		{
			string[] str = m_Mine.LevelDefs[1].Region.Name.Split('_');
			
			return str[0]+" "+str[1]+" "+str[2];
		}
		
		#region Ore
		[GumpElement(DisplayMode.Label,"OreTypes :")]
		public int ListLabel;
		
		[GumpElement(DisplayMode.List)]
		public int Index=-1;
		public string GetData_Index()
		{
			if(Index<0)
				return "Add a new type";
			
			HarvestDetails detail = m_Mine.HarvestInfo.OreTypes[Index];
			
			return FormatName.ToOre(detail.OreType.FullName)+" starting at level "+(detail.FirstLevel+1).ToString();
		}
		public void CallBack_Index_Up(Mobile  from)
		{
			if(Index<m_Mine.HarvestInfo.OreTypes.Count-1)
				Index++;
			
			from.SendGump(new DynamicGump(from, this));
		}
		public void CallBack_Index_Down(Mobile  from)
		{
			if(Index>=0)
				Index--;
			
			from.SendGump(new DynamicGump(from, this));
		}
		public void CallBack_Index(Mobile  from)
		{
			if(Index<0)
			{
				from.SendMessage("Target a new ore ...");
				
				from.Target = new CallBackTarget(this, "OnOreTargeted",new object[]{});
			}
			else
			{
				from.SendMessage("Do you want to remove that ore from the HarvestDefinition ? (Y/N) ");
				
				from.Prompt = new CallBackPrompt(this,"OnDeleted");
			}
		}
		public void OnOreTargeted(Mobile from, IPoint3D targeted, object[] data)
		{
			if(targeted is Item)
			{
				Item  it = (Item)targeted;
				Type ore =it.GetType();
				
				if(DeepMineHarvestInfo.BaseType.IsAssignableFrom(ore))
				{
					foreach(HarvestDetails detail in m_Mine.HarvestInfo.OreTypes)
					{
						if(detail.OreType==ore)
						{
							from.SendMessage(FormatName.ToOre(ore.FullName)+" is already set for that mine...");
							
							return;
						}
					}
					
					int lvl = 1;
					
					Region region = Region.Find(from.Location, from.Map);
					if(region!=null && region is DeepMineRegion)
					{
						lvl = ((DeepMineRegion)region).Level;
					}
					
					m_Mine.HarvestInfo.OreTypes.Add(new HarvestDetails(ore,lvl));
					
					from.SendMessage(FormatName.ToOre(ore.FullName)+" added to HarvestDefinition, starting at level "+(lvl+1).ToString());
					
					Index = m_Mine.HarvestInfo.OreTypes.Count-1;
				}
				else from.SendMessage("This is not ore...");
			}
			from.SendGump(new DynamicGump(from, this));
		}
		public void OnDeleted(Mobile from, string text)
		{
			if(text.ToLower()=="y")
			{
				from.SendMessage(FormatName.ToOre(m_Mine.HarvestInfo.OreTypes[Index].OreType.FullName)+" removed from HarvestDefinition");
				
				m_Mine.HarvestInfo.OreTypes.RemoveAt(Index);
				
				Index--;
			}
			from.SendGump(new DynamicGump(from, this));
		}
		#endregion
		
		#region Mobile
		[GumpElement(DisplayMode.Label,"MobileTypes :")]
		public int ListLabel2;
		
		[GumpElement(DisplayMode.List)]
		public int Index2=-1;
		public string GetData_Index2()
		{
			if(Index2<0)
				return "Add a new type";
			
			MobileDetails detail = m_Mine.MobileInfo.MobileTypes[Index2];
			
			return FormatName.ToMobile(detail.MobileType.FullName)+" starting at level "+(detail.FirstLevel+1).ToString();
		}
		public void CallBack_Index2_Up(Mobile  from)
		{
			if(Index2<m_Mine.MobileInfo.MobileTypes.Count-1)
				Index2++;
			
			from.SendGump(new DynamicGump(from, this));
		}
		public void CallBack_Index2_Down(Mobile  from)
		{
			if(Index2>=0)
				Index2--;
			
			from.SendGump(new DynamicGump(from, this));
		}
		public void CallBack_Index2(Mobile  from)
		{
			if(Index2<0)
			{
				from.SendMessage("Target a new mobile ...");
				
				from.Target = new CallBackTarget(this, "OnMobileTargeted",new object[]{});
			}
			else
			{
				from.SendMessage("Do you want to remove that mobile from the HarvestDefinition ? (Y/N) ");
				
				from.Prompt = new CallBackPrompt(this,"OnDeleted2");
			}
		}
		public void OnMobileTargeted(Mobile from, IPoint3D targeted, object[] data)
		{
			if(targeted is Mobile)
			{
				Mobile  m = (Mobile)targeted;
				Type mob =m.GetType();
								
				foreach(MobileDetails detail in m_Mine.MobileInfo.MobileTypes)
				{
					if(detail.MobileType==mob)
					{
						from.SendMessage(FormatName.ToMobile(mob.FullName)+" is already set for that mine...");
						
						return;
					}
				}
				
				int lvl = 1;
				
				Region region = Region.Find(from.Location, from.Map);
				if(region!=null && region is DeepMineRegion)
				{
					lvl = ((DeepMineRegion)region).Level;
				}
				
				m_Mine.MobileInfo.MobileTypes.Add(new MobileDetails(mob,lvl));
				
				from.SendMessage(FormatName.ToMobile(mob.FullName)+" added to HarvestDefinition, starting at level "+(lvl+1).ToString());
				
				Index2 = m_Mine.MobileInfo.MobileTypes.Count-1;
				
			}
			from.SendGump(new DynamicGump(from, this));
		}
		public void OnDeleted2(Mobile from, string text)
		{
			if(text.ToLower()=="y")
			{
				from.SendMessage(FormatName.ToMobile(m_Mine.MobileInfo.MobileTypes[Index2].MobileType.FullName)+" removed from HarvestDefinition");
				
				m_Mine.MobileInfo.MobileTypes.RemoveAt(Index2);
				
				Index2--;
			}
			from.SendGump(new DynamicGump(from, this));
		}
		#endregion
	}
}
