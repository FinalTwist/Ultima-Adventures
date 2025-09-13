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
using Server.Misc;
using Server.Network;

namespace Server.Regions
{
	public class DawnRegion : BaseRegion
	{
		public DawnRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return false;
		}

		public override void OnEnter( Mobile m )
		{
			base.OnEnter( m );
			if ( m is PlayerMobile )
			{
				if ( m.Skills[SkillName.Magery].Base >= 80.0 || m.Skills[SkillName.Necromancy].Base >= 80.0 )
				{
					LoggingFunctions.LogRegions( m, this.Name, "enter" );
				}
				else
				{
					BaseCreature.TeleportPets( m, new Point3D(3696, 523, 5), Map.Trammel, false );
					m.MoveToWorld (new Point3D(3696, 523, 5), Map.Trammel);
					m.PlaySound( 0x1FE );
					m.SendMessage("You lack the magical essence to remain on the moon.");
				}
			}

			Server.Misc.RegionMusic.MusicRegion( m, this );
		}

		public override void OnExit( Mobile m )
		{
			base.OnExit( m );
			if ( m is PlayerMobile )
			{
				if ( m.Skills[SkillName.Magery].Base >= 80.0 || m.Skills[SkillName.Necromancy].Base >= 80.0 )
				{
					LoggingFunctions.LogRegions( m, this.Name, "exit" );
				}
			}
		}
	}
}