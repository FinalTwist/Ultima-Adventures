using System;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Spells;
using System.Text;
using System.IO;
using Server.Network;
using System.Collections;
using Server.Misc;
using Server.Items;

namespace Server.Regions
{
	public class SafeRegion : BaseRegion
	{
		public SafeRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return false;
		}

		public override void AlterLightLevel( Mobile m, ref int global, ref int personal )
		{
			if ( this.Name == "the Fort of Tenebrae" ){ global = LightCycle.DungeonLevel; }
		}

		public override bool AllowHarmful( Mobile from, Mobile target )
		{
			return false;
		}

		public override bool OnBeginSpellCast( Mobile m, ISpell s )
		{
			if ( Worlds.IsAllowedSpell( m, s ) || this.Name == "the Forgotten Lighthouse" || this.Name == "the Fort of Tenebrae" || this.Name == "Savage Sea Docks" || this.Name == "Serpent Sail Docks" || this.Name == "Anchor Rock Docks" || this.Name == "Kraken Reef Docks" )
			{
				return base.OnBeginSpellCast( m, s );
			}
			else
			{
				m.SendMessage( "This magic does not seem to work here." );
				return false;
			}
		}

		public override void OnEnter( Mobile m )
		{
			base.OnEnter( m );
			if ( m is PlayerMobile )
			{
				LoggingFunctions.LogRegions( m, this.Name, "enter" );
			}
			else if ( m is BaseCreature && !(m is BaseVendor) && !(m is Citizens) ) // WATER CREATURES GO UNDER THE SURFACE
			{
				BaseCreature bc = (BaseCreature)m;
				if ( bc.ControlMaster == null && 
				 ( this.Name == "the Forgotten Lighthouse" || 
				 this.Name == "Anchor Rock Docks" || 
				 this.Name == "Kraken Reef Docks" || 
				 this.Name == "Savage Sea Docks" || 
				 this.Name == "Serpent Sail Docks" ) )
				{
					m.PlaySound( 0x026 );
					Effects.SendLocationEffect( m.Location, m.Map, 0x352D, 16, 4 );
					m.Delete();
				}
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