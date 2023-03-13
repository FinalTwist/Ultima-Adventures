using System;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Misc;

namespace Server.Regions
{
	public class SeaSpawnRegion : BaseRegion
	{
		public SeaSpawnRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
		}
	}
}