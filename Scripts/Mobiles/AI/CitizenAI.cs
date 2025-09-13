using System;
using System.Collections;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	public class CitizenAI : BaseAI
	{
		public CitizenAI(BaseCreature m) : base (m)
		{
		}

		public override bool DoActionWander()
		{
			return false;
		}

		public override bool DoActionCombat()
		{
			return false;
		}

		public override bool DoActionBackoff()
		{
			return false;
		}

		public override bool DoActionFlee()
		{
			return false;
		}
	}
}