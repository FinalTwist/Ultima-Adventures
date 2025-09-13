/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 18/09/2014
 */

using System;
using System.Collections;

namespace Server.DeepMine
{
	public delegate void DigEventHandler(object sender, DigEventArgs e);
	
	public delegate void HoleFoundEventHandler(object sender, DigEventArgs e);
	
	public delegate void LevelDownEventHandler(object sender, DigEventArgs e);
	
	public delegate void LevelUpEventHandler(object sender, DigEventArgs e);
	
	public class DigEventArgs : EventArgs
	{
		private Mobile m_Mobile;
		private Map m_Map;
		private Item m_Tool;
		private object m_Object;
		
		public Map Map
		{
			get { return m_Map; }
		}
		
		public Mobile Mobile
		{
			get { return m_Mobile; }
		}
		
		public Item Tool
		{
			get { return m_Tool; }
		}
		
		public object Object
		{
			get { return m_Object; }
		}
		
		private DeepMineRegion m_DeepRegion;
		public DeepMineRegion DeepRegion
		{
			get
			{
				return m_DeepRegion;
			}
		}
		
		public DigEventArgs( Mobile mobile, Map map, DeepMineRegion region, Item tool, object harvested)
		{
			m_Mobile = mobile;
			m_Map = map;
			m_Tool = tool;
			m_Object = harvested;
			m_DeepRegion = region;
		}
	}
}