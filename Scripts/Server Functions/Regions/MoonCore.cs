using System;
using System.Xml;
using Server;
using Server.Mobiles;
using System.Text;
using System.IO;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fourth;
using Server.Spells.Sixth;
using Server.Spells.Chivalry;

namespace Server.Regions
{
	public class MoonCore : BaseRegion
	{
		public MoonCore( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return false;
		}

		public override void AlterLightLevel( Mobile m, ref int global, ref int personal )
		{
			global = LightCycle.CaveLevel;
		}

		public override bool OnBeginSpellCast( Mobile m, ISpell s )
		{
			m.SendMessage( "Magic does not seem to work here." );
			return false;
		}

		public override void OnEnter( Mobile m )
		{
			base.OnEnter( m );
			if ( m is PlayerMobile )
			{
				if ( this.Name == "the Core of the Moon" && m.Blessed == false && m.Alive && m.AccessLevel == AccessLevel.Player )
				{
					Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
					Effects.PlaySound( m.Location, m.Map, 0x225 );
					m.Damage( 10000, m );
					LoggingFunctions.LogKillTile( m, "the intense heat of the Moon's core" );
				}
				else if ( this.Name == "the Core of the Moon" )
				{
					m.SendMessage( "You can feel an intense heat!" );
				}
				else if ( m.Skills[SkillName.Magery].Base >= 80.0 || m.Skills[SkillName.Necromancy].Base >= 80.0 )
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
			if ( m is PlayerMobile && this.Name != "the Core of the Moon" )
			{
				if ( m.Skills[SkillName.Magery].Base >= 80.0 || m.Skills[SkillName.Necromancy].Base >= 80.0 )
				{
					LoggingFunctions.LogRegions( m, this.Name, "exit" );
				}
			}
		}
	}
}