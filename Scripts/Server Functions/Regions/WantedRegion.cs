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
using System.Text;
using System.IO;
using Server.Network;
using Server.Misc;
using Server.Items;

namespace Server.Regions
{
	public class WantedRegion : BaseRegion
	{
		public WantedRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return false;
		}

		public override TimeSpan GetLogoutDelay( Mobile m )
		{
			return TimeSpan.Zero;
		}

		public override void AlterLightLevel( Mobile m, ref int global, ref int personal )
		{
			global = LightCycle.CaveLevel;
		}

		public override bool AllowHarmful( Mobile from, Mobile target )
		{
			if (( from is PlayerMobile ) && ( target is PlayerMobile ))
				return false;
			else
				return base.AllowHarmful( from, target );
		}

		public override bool OnBeginSpellCast( Mobile m, ISpell s )
		{
			m.SendMessage( "That does not seem to work here." );
			return false;
		}
		
		public override void OnEnter( Mobile m )
		{
			base.OnEnter( m );
			if ( m is PlayerMobile )
			{
				m.SendMessage( "You wake up in a Britain jail cell." );
				m.SendMessage( "Perhaps there is a secret way out." );
			}
			if ( m is PlayerMobile && m.AccessLevel < AccessLevel.GameMaster )
			{
				Server.Items.CharacterDatabase.SetWanted( m );
			}

			Server.Misc.RegionMusic.MusicRegion( m, this );
		}								
	}
}