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
	public class ProtectedRegion : BaseRegion
	{
		public ProtectedRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return false;
		}

		public override void AlterLightLevel( Mobile m, ref int global, ref int personal )
		{
			if ( this.Name == "the Cabin" ){ global = LightCycle.CaveLevel; }
			else if ( this.Name == "the Chamber of the Codex" ){ global = LightCycle.DungeonLevel; }
			else if ( this.Name == "the Chamber of Virtue" ){ global = LightCycle.DungeonLevel; }
			else if ( this.Name == "the Chamber of Corruption" ){ global = LightCycle.DungeonLevel; }
		}

		public override bool AllowHarmful( Mobile from, Mobile target )
		{
			if (target is TrainingElemental || target is TrainingElemental1 || from is TrainingElemental || from is TrainingElemental1)
				return true;

			return false;
		}

		public override bool OnBeginSpellCast( Mobile m, ISpell s )
		{
			if ( this.Name == "the Chamber of the Codex" )
			{
				m.SendMessage( "Magic does not work within the Chamber of the Codex." );
				return false;
			}
			else if ( this.Name == "the Basement" )
			{
				return true;
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

			Server.Misc.RegionMusic.MusicRegion( m, this );
		}

		public override void OnExit( Mobile m )
		{
			base.OnExit( m );
			if ( m is PlayerMobile )
			{
				LoggingFunctions.LogRegions( m, this.Name, "exit" );
			}

			if ( this.Name == "the Lodoria Forest" )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
				CharacterDatabase.SetDiscovered( m, "the Land of Lodoria", true );
			}
			else if ( this.Name == "the Chamber of the Codex" )
			{
				ArrayList targets = new ArrayList();
				foreach ( Item item in World.Items.Values )
				{
					if ( item is CodexWisdom )
					{
						if ( ((CodexWisdom)item).CodexOwner == m )
						{
							targets.Add( item );
						}
					}
				}
				for ( int i = 0; i < targets.Count; ++i )
				{
					Item item = ( Item )targets[ i ];

					if ( item is CodexWisdom )
					{
						CodexWisdom codex = (CodexWisdom)item;

						bool DestroyLense = true;

						if ( ( codex.SkillFirst == codex.PreviousFirst || codex.SkillFirst == codex.PreviousSecond ) && ( codex.SkillSecond == codex.PreviousFirst || codex.SkillSecond == codex.PreviousSecond ) )
							DestroyLense = false;

						if ( DestroyLense )
						{
							codex.PreviousFirst = codex.SkillFirst;
							codex.PreviousSecond = codex.SkillSecond;
							codex.HasConvexLense = 0;
							codex.HasConcaveLense = 0;
						}
					}
				}
			}
		}
	}
}