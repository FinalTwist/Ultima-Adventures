using System;
using System.Xml;
using Server;
using System.Text;
using Server.Mobiles;
using System.IO;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Spells;

namespace Server.Regions
{
	public class VillageRegion : BaseRegion
	{
		public VillageRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			if ( from.Region.IsPartOf( "the Village of Barako" ) )
			{
				return true;
			}

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