using System;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fourth;
using Server.Spells.Sixth;
using Server.Spells.Chivalry;
using Server.Spells.Undead;
using Server.Spells.Herbalist;
using System.Text;
using System.IO;
using Server.Network;
using Server.Misc;
using System.Collections.Generic;
using System.Collections;
using Server.Items;

namespace Server.Regions
{
	public class DungeonHomeRegion : BaseRegion
	{
		public DungeonHomeRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return false;
		}

		public override void AlterLightLevel( Mobile m, ref int global, ref int personal )
		{
			global = LightCycle.JailLevel;
		}

		public override void OnEnter( Mobile m )
		{
			base.OnEnter( m );

			LoggingFunctions.LogRegions( m, "a Dungeon Dwelling", "enter" );

			Server.Misc.RegionMusic.MusicRegion( m, this );
		}

		public override void OnExit( Mobile m )
		{
			base.OnExit( m );
			LoggingFunctions.LogRegions( m, "a Dungeon Dwelling", "exit" );
		}
	}
}