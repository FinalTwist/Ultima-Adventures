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
using Server.Spells.Magical;
using System.Text;
using System.IO;
using Server.Misc;
using Server.Network;

namespace Server.Regions
{
	public class BardTownRegion : BaseRegion
	{
		public BardTownRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return false;
		}

		public override bool AllowHarmful( Mobile from, Mobile target )
		{
			int CanHarm = 0;

			if ( from is BaseCreature )
			{
				BaseCreature enemy = (BaseCreature)from;

				if ( enemy.Summoned )
				{
					if ( enemy.SummonMaster != null )
						CanHarm = CanHarm + 1;
				}
				else if ( enemy.Controlled )
				{
					if ( enemy.ControlMaster != null )
						CanHarm = CanHarm + 1;
				}
				else if ( enemy.BardProvoked )
				{
					if ( enemy.BardMaster != null )
						CanHarm = CanHarm + 1;
				}
			}

			if ( target is BaseCreature )
			{
				BaseCreature enemy = (BaseCreature)target;

				if ( enemy.Summoned )
				{
					if ( enemy.SummonMaster != null )
						CanHarm = CanHarm + 1;
				}
				else if ( enemy.Controlled )
				{
					if ( enemy.ControlMaster != null )
						CanHarm = CanHarm + 1;
				}
				else if ( enemy.BardProvoked )
				{
					if ( enemy.BardMaster != null )
						CanHarm = CanHarm + 1;
				}
			}

			if ( from is PlayerMobile || from is BaseVendor || from is BaseNPC || from is BasePerson )
				CanHarm = CanHarm + 1;

			if ( target is PlayerMobile || target is BaseVendor || target is BaseNPC || target is BasePerson )
				CanHarm = CanHarm + 1;

			if ( CanHarm > 1 )
				return false;
			else
				return base.AllowHarmful( from, target );
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