/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 27/09/2014
 */

using System;
using System.Collections.Generic;
using System.Xml;
using Server;
using Server.Items;

namespace Server.DeepMine
{
	public class DeepMineHarvestInfo
	{
		public DeepMineHarvestInfo()
		{
			
		}
		
		public static Type BaseType = typeof(BaseOre);//Common
		//public static Type BaseType = typeof(MineraiBase);//LD7A
		
		public List<HarvestDetails> OreTypes = new List<HarvestDetails>();
		
		public void Serialize(XmlWriter writer)
		{
			writer.WriteStartElement("HarvestInfo");
			
			foreach(HarvestDetails detail in OreTypes)
			{
				detail.Serialize(writer);
			}
			
			writer.WriteEndElement();
		}
		
		public void Deserialize(XmlNode node)
		{
			foreach(XmlNode child in node.ChildNodes)
			{
				if(child.Name=="Detail")
				{
					HarvestDetails detail = new HarvestDetails();
					detail.Deserialize(child);
					OreTypes.Add(detail);
				}
			}
		}
	}
	
	public class HarvestDetails
	{
		private Type m_OreType;
		public Type OreType
		{
			get { return m_OreType; }
			set { m_OreType = value; }
		}
		
		private int m_FirstLevel;
		public int FirstLevel
		{
			get { return m_FirstLevel; }
			set { m_FirstLevel = value; }
		}
		
		public HarvestDetails(Type t, int level)
		{
			m_OreType = t;
			m_FirstLevel = level;
		}
		
		public HarvestDetails()
		{
			
		}
		
		public void Serialize(XmlWriter writer)
		{
			writer.WriteStartElement("Detail");
			
			writer.WriteElementString("OreType",m_OreType.FullName);
			writer.WriteElementString("FirstLevel",m_FirstLevel.ToString());
			
			writer.WriteEndElement();
		}
		
		public void Deserialize(XmlNode node)
		{
			XmlNode type = node.SelectSingleNode("OreType");
			m_OreType = ScriptCompiler.FindTypeByFullName(type.InnerText);
			
			XmlNode level = node.SelectSingleNode("FirstLevel");
			m_FirstLevel = Convert.ToInt32(level.InnerText);
		}
	}
}