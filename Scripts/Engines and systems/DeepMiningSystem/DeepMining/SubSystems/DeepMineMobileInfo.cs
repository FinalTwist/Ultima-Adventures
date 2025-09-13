/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 04/10/2014
 */

using System;
using System.Collections.Generic;
using System.Xml;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.DeepMine
{
	public class DeepMineMobileInfo
	{
		public DeepMineMobileInfo()
		{
			
		}
		
		public List<MobileDetails> MobileTypes = new List<MobileDetails>();
		
		public void Serialize(XmlWriter writer)
		{
			writer.WriteStartElement("MobileDetails");
			
			foreach(MobileDetails detail in MobileTypes)
			{
				detail.Serialize(writer);
			}
			
			writer.WriteEndElement();
		}
		
		public void Deserialize(XmlNode node)
		{
			/*foreach(XmlNode child in node.ChildNodes)
			{
				if(child.Name=="Detail")
				{
					MobileDetails detail = new MobileDetails();
					detail.Deserialize(child);
					MobileTypes.Add(detail);
				}
			}*/
			
			XmlNodeList nodes = node.SelectNodes("Detail");
			
			foreach(XmlNode child in nodes)
			{
				MobileDetails detail = new MobileDetails();
				detail.Deserialize(child);
				MobileTypes.Add(detail);
			}
		}
	}
	
	public class MobileDetails
	{
		private Type m_MobileType;
		public Type MobileType
		{
			get { return m_MobileType; }
			set { m_MobileType = value; }
		}
		
		private int m_FirstLevel;
		public int FirstLevel
		{
			get { return m_FirstLevel; }
			set { m_FirstLevel = value; }
		}
		
		public MobileDetails(Type t, int level)
		{
			m_MobileType = t;
			m_FirstLevel = level;
		}
		
		public MobileDetails()
		{
			
		}
		
		public void Serialize(XmlWriter writer)
		{
			writer.WriteStartElement("Detail");
			
			writer.WriteElementString("MobileType",m_MobileType.FullName);
			writer.WriteElementString("FirstLevel",m_FirstLevel.ToString());
			
			writer.WriteEndElement();
		}
		
		public void Deserialize(XmlNode node)
		{
			XmlNode type = node.SelectSingleNode("MobileType");
			m_MobileType = ScriptCompiler.FindTypeByFullName(type.InnerText);
			
			XmlNode level = node.SelectSingleNode("FirstLevel");
			m_FirstLevel = Convert.ToInt32(level.InnerText);
		}
	}
}