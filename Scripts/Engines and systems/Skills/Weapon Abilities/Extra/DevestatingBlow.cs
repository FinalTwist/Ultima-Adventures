using System;

namespace Server.Items
{
	public class DevastatingBlow : WeaponAbility
	{
		public DevastatingBlow(){}

		public override int BaseMana { get { return 30; } }
		public override double DamageScalar { get { return 2.0; } }

		public override bool OnBeforeSwing(Mobile attacker, Mobile defender, bool validate)
		{
			if (validate && (!Validate(attacker) || !CheckMana(attacker, false))) return false;
			else return true;
		}

		public override void OnHit(Mobile attacker, Mobile defender, int damage)
		{
			if (!CheckMana(attacker, true)) return;
			ClearCurrentAbility(attacker);

			attacker.SendMessage("You strike a devastating blow!");
			defender.SendMessage("You were struck with a devastating blow!");
			defender.PlaySound(0x1E1);
			defender.FixedParticles(0, 1, 0, 9946, EffectLayer.Head);
			Effects.SendMovingParticles(new Entity(Serial.Zero, new Point3D(defender.X, defender.Y, defender.Z + 50), defender.Map), new Entity(Serial.Zero, new Point3D(defender.X, defender.Y, defender.Z + 20), defender.Map), 0xFB4, 1, 0, false, false, 0, 3, 9501, 1, 0, EffectLayer.Head, 0x100);
		}
	}
}