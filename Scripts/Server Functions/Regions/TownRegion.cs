using System;
using System.Xml;
using Server;

namespace Server.Regions
{
	public class TownRegion : GuardedRegion
	{
		public TownRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
			// NOT USED
		}

		public override void AlterLightLevel( Mobile m, ref int global, ref int personal )
		{
			global = LightCycle.DayLevel;
		}
	}
}