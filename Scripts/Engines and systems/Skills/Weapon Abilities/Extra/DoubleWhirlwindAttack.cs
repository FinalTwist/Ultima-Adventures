using System;
using System.Collections;
using Server;
using Server.Spells;
using Server.Mobiles;

namespace Server.Items
{
	public class DoubleWhirlwindAttack : WeaponAbility
	{
		public DoubleWhirlwindAttack(){}

		public override int BaseMana{ get{ return 25; } }

		public override void OnHit( Mobile attacker, Mobile defender, int damage )
		{
			if ( !Validate( attacker )  ) return;
			ClearCurrentAbility( attacker );
			if (attacker.Stam < 50)
			{
				attacker.SendMessage("You are too fatigues to perform this attack!");
				return;
			}
			Map map = attacker.Map;
			if ( map == null ) return;
			BaseWeapon weapon = attacker.Weapon as BaseWeapon;
			if ( weapon == null ) return;
			if ( !CheckMana( attacker, true ) ) return;
			attacker.FixedEffect( 0x3728, 10, 15 );
			attacker.PlaySound( 0x2A1 );
			ArrayList list = new ArrayList();
			foreach ( Mobile m in attacker.GetMobilesInRange( 1 ) ) list.Add( m );
			ArrayList targets = new ArrayList();
			for ( int i = 0; i < list.Count; ++i )
			{
				Mobile m = (Mobile)list[i];
				if ( m != defender && m != attacker && SpellHelper.ValidIndirectTarget( attacker, m ) )
				{
					if ( m == null || m.Deleted || m.Map != attacker.Map || !m.Alive || !attacker.CanSee( m ) || !attacker.CanBeHarmful( m ) ) continue;
					if ( !attacker.InRange( m, weapon.MaxRange ) ) continue;
					if ( attacker.InLOS( m ) )
					{
						targets.Add( m );
					}
				}
			}

			if ( targets.Count > 0 )
			{
				double bushido = attacker.Skills.Bushido.Value;
				double damageBonus = 1.0 + Math.Pow( (targets.Count * bushido) / 60, 2 ) / 100;
				if ( damageBonus > 2.0 ) damageBonus = 2.0;
				attacker.RevealingAction();
				for ( int i = 0; i < targets.Count; ++i )
				{
					Mobile m = (Mobile)targets[i];
					attacker.SendLocalizedMessage( 1060161 );
					int stamdrain = Math.Max(10, (int)(attacker.Stam * 0.1));
					attacker.Stam  = stamdrain;
					m.SendLocalizedMessage( 1060162 );
					weapon.OnHit( attacker, m, damageBonus );
					weapon.OnHit( attacker, m, damageBonus );
				}
				weapon.OnHit( attacker, defender, damageBonus );
			}
		}
	}
}