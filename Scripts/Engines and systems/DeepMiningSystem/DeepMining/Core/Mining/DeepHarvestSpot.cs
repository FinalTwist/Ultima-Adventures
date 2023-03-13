/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 30/09/2014
 */

using System;
using System.Collections.Generic;
using System.Xml;
using Server;
using Server.Engines.Harvest;

namespace Server.DeepMine
{
	public class DeepHarvestSpot
	{
		private int m_X;
		public int X
		{
			get { return m_X; }
			set { m_X = value; }
		}
		
		private int m_Y;
		public int Y
		{
			get { return m_Y; }
			set { m_Y = value; }
		}
		
		private Type m_OreType;
		public Type OreType
		{
			get { return m_OreType; }
			set { m_OreType = value; }
		}
		
		public DeepHarvestSpot(int x, int y, Type type)
		{
			m_X = x;
			m_Y = y;
			m_OreType = type;
		}
		
		private HarvestSystem m_HarvestSystem;
		public HarvestSystem HarvestSystem
		{
			get
			{
				if(m_HarvestSystem==null)
				{
					DeepMining system = new DeepMining();
					
					system.Ore.Resources = new HarvestResource[]
					{
						new HarvestResource( 0, 0, 120, m_HarvestMessage, m_OreType )
					};
					
					system.Ore.Veins = new HarvestVein[]
					{
						new HarvestVein( 100, 0.0, system.Ore.Resources[0], null   )
					};
					
					m_HarvestSystem = system;
				}
				return m_HarvestSystem;
				
			}
		}
		
		private string m_HarvestMessage
		{
			get
			{
				//can use cliloc cf 1007072 if ores not custom
				string ore = FormatName.ToOre(m_OreType.FullName);
				
				return "You place some "+ore+ " in your backpack";
			}
		}
		
		public void Serialize(XmlWriter writer)
		{
			writer.WriteStartElement("Spot");
			
			writer.WriteElementString("X",m_X.ToString());
			writer.WriteElementString("Y",m_Y.ToString());
			writer.WriteElementString("OreType",m_OreType.FullName);
			
			writer.WriteEndElement();
		}
	}
}