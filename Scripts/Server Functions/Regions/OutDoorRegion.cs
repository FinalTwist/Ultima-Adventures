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
	public class OutDoorRegion : BaseRegion
	{
		public OutDoorRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			if ( from.Region.IsPartOf( "the Ranger Outpost" ) )
			{
				if ( from.Skills[SkillName.Camping].Base >= 90 || from.Skills[SkillName.Tracking].Base >= 90 )
				{
					return true;
				}
				else
				{
					from.SendMessage( "Only a master explorer or ranger can build a home here." );
					return false;
				}
			}

			return false;
		}

		public override void OnEnter( Mobile m )
		{
			base.OnEnter( m );
			if ( m is PlayerMobile )
			{
				LoggingFunctions.LogRegions( m, this.Name, "enter" );

				if ( this.Name == "the Ranger Outpost" ){ CharacterDatabase.SetKeys( m, "RangerOutpost", true ); }
			}
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