using System;
using Server;

namespace Server.Items
{
	public class ConsecratedStrike : WeaponAbility
	{
		public ConsecratedStrike(){}
		public override int BaseMana { get { return 20; } }

		public override bool OnBeforeSwing(Mobile attacker, Mobile defender, bool validate)
		{
			if (validate && (!Validate(attacker) || !CheckMana(attacker, false))) return false;
			else return true;
		}

		public override void OnHit(Mobile attacker, Mobile defender, int damage)
		{
			if (!CheckMana(attacker, true)) return;
			ClearCurrentAbility(attacker);
			attacker.SendMessage("You hit them with the highest possible damage!");
			defender.PlaySound(0x56);
		}
	}
}