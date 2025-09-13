using System;
using Server;
using Server.Items;

namespace Server.Targeting
{
	public class MagicObjectTarget : Target
	{
		private BaseMagicObject m_Item;

		public MagicObjectTarget( BaseMagicObject item ) : base( 6, false, TargetFlags.None )
		{
			m_Item = item;
		}

		private static int GetOffset( Mobile caster )
		{
			return 5 + (int)(100 * 0.02 );
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			m_Item.DoMagicObjectTarget( from, targeted );
		}
	}
}