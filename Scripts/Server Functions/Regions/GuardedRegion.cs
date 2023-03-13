using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Spells;

namespace Server.Regions
{
	public class GuardedRegion : BaseRegion
	{
		public GuardedRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
			// NOT USED
		}
	}
}
