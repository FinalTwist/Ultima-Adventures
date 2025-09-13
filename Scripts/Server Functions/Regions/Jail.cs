using System;
using System.Xml;
using Server;
using Server.Spells;

namespace Server.Regions
{
	public class Jail : BaseRegion
	{
		public Jail( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
			// NOT USED
		}
	}
}