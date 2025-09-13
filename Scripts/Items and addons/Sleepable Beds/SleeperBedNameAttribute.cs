// Script Package: Sleepable Beds
// Version: 1.0
// Author: Oak
// Servers: RunUO 2.0
// Date: 7/7/2006
// History: 
//  Written for RunUO 1.0 shard, Sylvan Dreams,  in February 2005. Based largely on work by David on his Sleepable NPCs scripts.
//  Modified for RunUO 2.0, removed shard specific customizations (wing layers, etc.)
using System;

namespace Server
{
	[AttributeUsage( AttributeTargets.Class )]
	public class SleeperBedNameAttribute : Attribute
	{
		private string m_Name;

		public string Name
		{
			get{ return m_Name; }
		}

		public SleeperBedNameAttribute( string name )
		{
			m_Name = name;
		}
	}
}