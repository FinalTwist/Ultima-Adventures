/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 26/09/2014
 */

/// <summary>
/// Summary tree of Maps/Mines/Levels/values
///
/// Serialized in DeepMineXMLSave
///
/// At Initialize it check foreach map if XMLSave exists, and if it checks with what is register in DeepMineMapRegistry (number of mines and number of levels)
///
/// If not, call DeepMineParsing to parse map as registred
///
/// </summary>

using System;
using System.Collections.Generic;
using System.Xml;
using Server;
using Server.Mobiles;

namespace Server.DeepMine
{
	public static class DeepMineInfos
	{
		public static Dictionary<Map,DeepMapInfo> MapDefs = new Dictionary<Map, DeepMapInfo>();
		
		public static void Initialize()
		{
			foreach(MapRegister entry in DeepMineMapRegistry.MapEntries)
			{
				if(!DeepMineXMLSave.Load(entry))
					DeepMineParsing.ParseMap(entry);
			}
		}
		
		public static void Serialize(XmlWriter writer)
		{
			writer.WriteStartElement("AllMaps");
			
			foreach(KeyValuePair<Map,DeepMapInfo> kvp in MapDefs)
			{
				writer.WriteStartElement("Map");
				
				writer.WriteElementString("MapID",kvp.Key.MapID.ToString());
				writer.WriteElementString("Mines", kvp.Value.MineDefs.Count.ToString());
				writer.WriteElementString("Levels", kvp.Value.MineDefs[1].LevelDefs.Count.ToString());
				
				kvp.Value.Serialize(writer);
				
				writer.WriteEndElement();
			}
			
			writer.WriteEndElement();
		}
	}
	
	public class DeepMapInfo
	{
		public DeepMapInfo()
		{
			
		}
		
		public Dictionary<int,DeepMineInfo> MineDefs = new Dictionary<int, DeepMineInfo>();
		
		public void Serialize(XmlWriter writer)
		{
			foreach(KeyValuePair<int,DeepMineInfo> kvp in MineDefs)
			{
				kvp.Value.Serialize(writer);
			}
		}
		
		public void Deserialize(XmlNode node)
		{
			foreach(XmlNode child in node)
			{
				if(child.Name=="DeepMineInfo")
				{
					DeepMineInfo info = new DeepMineInfo();
					
					info.Deserialize(child);
					
					MineDefs.Add(MineDefs.Count,info);
				}
			}
		}
	}
	
	public class DeepMineInfo
	{
		public DeepMineInfo()
		{
			
		}
		
		public Dictionary<int,DeepLevelInfo> LevelDefs = new Dictionary<int, DeepLevelInfo>();
		
		private DeepMineHarvestInfo m_HarvestInfo = new DeepMineHarvestInfo();
		public DeepMineHarvestInfo HarvestInfo
		{
			get { return m_HarvestInfo; }
			set { m_HarvestInfo = value; }
		}
		
		private DeepMineMobileInfo m_MobileInfo = new DeepMineMobileInfo();
		public DeepMineMobileInfo MobileInfo
		{
			get { return m_MobileInfo; }
		}
	
		public void Serialize(XmlWriter writer)
		{
			writer.WriteStartElement("DeepMineInfo");
			
			foreach(KeyValuePair<int,DeepLevelInfo> kvp in LevelDefs)
			{
				kvp.Value.Serialize(writer);
			}
			
			m_HarvestInfo.Serialize(writer);
			
			m_MobileInfo.Serialize(writer);
			
			writer.WriteEndElement();
		}
		
		public void Deserialize(XmlNode node)
		{
			foreach(XmlNode child in node)
			{
				if(child.Name=="DeepLevelInfo")
				{
					DeepLevelInfo info = new DeepLevelInfo();
					
					info.Deserialize(child);
					
					LevelDefs.Add(LevelDefs.Count,info);
				}
			}
			
			m_HarvestInfo = new DeepMineHarvestInfo();
			m_HarvestInfo.Deserialize(node.SelectSingleNode("HarvestInfo"));
			
			m_MobileInfo = new DeepMineMobileInfo();
			m_MobileInfo.Deserialize(node.SelectSingleNode("MobileDetails"));
		}
	}
	
	public class DeepLevelInfo
	{
		public DeepLevelInfo()
		{
			
		}
		
		private DeepMineRegion m_Region;
		public DeepMineRegion Region
		{
			get { return m_Region; }
			set { m_Region = value; }
		}
		
		public List<DeepHarvestSpot> Spots = new List<DeepHarvestSpot>();
		
		public void Serialize(XmlWriter writer)
		{
			writer.WriteStartElement("DeepLevelInfo");
			
			Rectangle3D rect = m_Region.Area[0];
			
			writer.WriteElementString("Region",m_Region.Name);
			writer.WriteElementString("Map",m_Region.Map.MapID.ToString());
			writer.WriteElementString("X",rect.Start.X.ToString());
			writer.WriteElementString("Y",rect.Start.Y.ToString());
			writer.WriteElementString("Width",rect.Width.ToString());
			writer.WriteElementString("Height",rect.Height.ToString());
			
			writer.WriteStartElement("Spots");
			foreach(DeepHarvestSpot spot in Spots)
			{
				spot.Serialize(writer);
			}
			writer.WriteEndElement();
			
			writer.WriteEndElement();
		}
		
		public void Deserialize(XmlNode node)
		{
			string name = "";
			int map = 0;
			int X = 0;
			int Y = 0;
			int Width = 0;
			int Height = 0;
			
			foreach(XmlNode data in node)
			{
				if(data.Name=="Region")
					name = data.InnerText;
				else if(data.Name=="Map")
					map = Convert.ToInt32(data.InnerText);
				else if(data.Name=="X")
					X = Convert.ToInt32(data.InnerText);
				else if(data.Name=="Y")
					Y = Convert.ToInt32(data.InnerText);
				else if(data.Name=="Width")
					Width = Convert.ToInt32(data.InnerText);
				else if(data.Name=="Height")
					Height = Convert.ToInt32(data.InnerText);
				else if(data.Name=="Spots")
				{
					foreach(XmlNode spot in data.ChildNodes)
					{
						if(spot.Name=="Spot")
						{
							int x = Convert.ToInt32(spot.SelectSingleNode("X").InnerText);
							int y = Convert.ToInt32(spot.SelectSingleNode("Y").InnerText);
							Type ore = ScriptCompiler.FindTypeByFullName(spot.SelectSingleNode("OreType").InnerText);
							
							Spots.Add(new DeepHarvestSpot(x,y,ore));
						}
					}
				}
				
			}
			
			m_Region = new DeepMineRegion(name, Map.Maps[map],50, new Rectangle2D[]{new Rectangle2D(X,Y,Width,Height)});
			
			m_Region.Register();
		}
	}
}