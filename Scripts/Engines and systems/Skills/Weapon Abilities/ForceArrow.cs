// $Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Items/Weapons/Abilities/ForceArrow.cs#2 $

using System;
using Server;
using System.Collections;

namespace Server.Items
{
	public class ForceArrow : WeaponAbility
	{
		public ForceArrow()
		{
		}

		public override int BaseMana { get { return 20; } }

		public override void OnHit(Mobile attacker, Mobile defender, int damage)
		{
			if (!Validate(attacker) || !CheckMana(attacker, true))
				return;

			ClearCurrentAbility(attacker);

			attacker.SendLocalizedMessage(1074381); // You fire an arrow of pure force.
			defender.SendLocalizedMessage(1074382); // You are struck by a force arrow!

			if (Utility.RandomDouble() >= attacker.Skills[SkillName.Anatomy].Value / 600)
			{
				defender.Warmode = false;
				//attacker.SendMessage("Mobile forget who are attacking.");
			}
			DoLowerDefense(attacker, defender);
		}

		public virtual void DoLowerDefense(Mobile from, Mobile defender)
		{
			if (HitLower.ApplyDefense(defender))
			{
				defender.PlaySound(0x28E);
				Effects.SendTargetEffect(defender, 0x37BE, 1, 4, 0x23, 3);
			}
		}
	}
}
