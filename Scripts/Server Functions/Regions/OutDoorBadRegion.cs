using System;
using System.Xml;
using Server;
using Server.Mobiles;
using System.Text;
using System.IO;
using Server.Network;
using Server.Misc;
using Server.Items;

namespace Server.Regions
{
	public class OutDoorBadRegion : BaseRegion
	{
		public OutDoorBadRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return false;
		}

		public override void AlterLightLevel( Mobile m, ref int global, ref int personal )
		{
			if ( this.Map == Map.Ilshenar ){ global = LightCycle.DungeonLevel; }
		}

		public override void OnEnter( Mobile m )
		{
			base.OnEnter( m );
			if ( m is PlayerMobile )
			{
				LoggingFunctions.LogRegions( m, this.Name, "enter" );
			}

			Server.Misc.RegionMusic.MusicRegion( m, this );
		}

		public override void OnExit( Mobile m )
		{
			base.OnExit( m );
			if ( m is PlayerMobile )
			{
				LoggingFunctions.LogRegions( m, this.Name, "exit" );
			}
		}
	}
}