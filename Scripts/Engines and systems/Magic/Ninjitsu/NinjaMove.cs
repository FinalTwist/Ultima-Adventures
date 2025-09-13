using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Spells
{
	public class NinjaMove : SpecialMove
	{
		public override SkillName MoveSkill{ get{ return SkillName.Ninjitsu; } }

		public override void CheckGain( Mobile m )
		{
			m.CheckSkill( MoveSkill, RequiredSkill - 12.5, RequiredSkill + 37.5 );
		}
	}
}