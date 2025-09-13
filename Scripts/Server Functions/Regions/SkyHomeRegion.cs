using System;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Gumps;
using System.Text;
using System.IO;
using Server.Misc;

namespace Server.Regions
{
	public class SkyHomeDwelling : BaseRegion
	{
		public SkyHomeDwelling( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return false;
		}

		public override void OnEnter( Mobile m )
		{
			base.OnEnter( m );
			LoggingFunctions.LogRegions( m, "a Dwelling in the Sky", "enter" );
		}

		public override void OnExit( Mobile m )
		{
			base.OnExit( m );
			LoggingFunctions.LogRegions( m, "a Dwelling in the Sky", "exit" );
		}
	}
}