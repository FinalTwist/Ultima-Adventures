using System;
using Server;
using Server.Misc;
using Server.Items;
using Server.Mobiles;

namespace Server.Misc
{
    class MagicCastingItem
    {
		public static bool CastNoSkill( Item item )
		{
			if ( item is BaseMagicStaff )
				return true;

			if ( item is RobeOfTeleportation )
				return true;

			if ( item is BaseMagicObject )
				return true;

			if ( item is SoulShard )
				return true;

			return false;
		}
	}
}