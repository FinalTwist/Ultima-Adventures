using System;
using Server;
using Server.Items;

namespace Server.Targeting
{
	public class MagicStaffTarget : Target
	{
		private BaseMagicStaff m_Item;

		public MagicStaffTarget( BaseMagicStaff item ) : base( 6, false, TargetFlags.None )
		{
			m_Item = item;
		}

		private static int GetOffset( Mobile caster )
		{
			return 5 + (int)(caster.Skills[SkillName.Magery].Value * 0.02 );
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			m_Item.DoMagicStaffTarget( from, targeted );
		}
	}
}