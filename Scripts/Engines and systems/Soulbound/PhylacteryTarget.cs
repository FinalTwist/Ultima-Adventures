using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Engines.Soulbound
{
	public class PhylacteryTarget : Target
	{
		private Phylactery m_Phylactery;

		public PhylacteryTarget( Phylactery phylactery ) : base( 18, false, TargetFlags.None )
		{
			m_Phylactery = phylactery;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( m_Phylactery.Deleted || !m_Phylactery.IsChildOf( from.Backpack ) )
				return;

			m_Phylactery.EndCombine( from, targeted );
		}
	}
}