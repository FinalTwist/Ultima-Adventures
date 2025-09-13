using System;

namespace Server.Items
{
	public class FistsOfFury : WeaponAbility
	{
		public FistsOfFury(){}

		public override int BaseMana { get { return 20; } }
		public override double DamageScalar { get { return 0.9; } }

		public override bool OnBeforeSwing(Mobile attacker, Mobile defender, bool validate)
		{
			if (validate && (!Validate(attacker) || !CheckMana(attacker, false))) return false;
			else return true;
		}

		public override void OnHit(Mobile attacker, Mobile defender, int damage)
		{
			if (!CheckMana(attacker, true)) return;
			ClearCurrentAbility(attacker);
			if (defender == null || defender.Deleted || attacker.Deleted || defender.Map != attacker.Map || !defender.Alive || !attacker.Alive || !attacker.CanSee(defender))
			{
				attacker.Combatant = null;
				return;
			}
			attacker.SendMessage("You attack with a series of mighty blows!");
			defender.SendMessage("You have been struck with a series of mighty blows!");
			defender.PlaySound(0x3BB);
			defender.FixedEffect(0x37B9, 244, 25);
			if (attacker.InLOS(defender))
			{
				BaseWeapon.InDoubleStrike = true;
				attacker.RevealingAction();
				int strikes = (int)(attacker.Skills.Wrestling.Value / 75) + 1;
				for ( int i = 0; i < strikes; ++i )
				{
					attacker.Weapon.OnSwing( attacker, defender );
				}
				BaseWeapon.InDoubleStrike = false;
			}
		}
	}
}