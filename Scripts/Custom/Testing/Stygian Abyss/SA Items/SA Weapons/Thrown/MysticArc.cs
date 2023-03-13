using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Spells;

namespace Server.Items
{
	// The thrown projectile will arc to a second target after hitting the primary target. Chaos energy will burst from the projectile at each target. 
	// This will only hit targets that are in combat with the user.
	public class MysticArc : WeaponAbility
	{
		public override int BaseMana{ get{ return 15; } }
		private Mobile m_Target;
		private int m_Damage = 15;

		public override void OnHit( Mobile attacker, Mobile defender, int damage )
		{
			if ( !CheckMana( attacker, true ) && defender != null )
				return;

			BaseThrown weapon = attacker.Weapon as BaseThrown;

			if ( weapon == null )
				return;

			List<Mobile> targets = new List<Mobile>();

			foreach ( Mobile m in attacker.GetMobilesInRange( weapon.MaxRange ) )
			{
				if ( m == defender )
					continue;

				if ( m.Combatant != attacker )
					continue;

				targets.Add( m );
			}

			if ( targets.Count > 0 )
				m_Target = targets[Utility.Random( targets.Count )];

			/*
			Mobile m = null;

			foreach( DamageEntry de in attacker.DamageEntries )
			{
				m = Mobile.GetDamagerFrom( de );

				if ( m != null )
				{
					if ( defender != m && defender.InRange( m, 3 ) )
					{
						m_Target = m;
						break;
					}
				}
			}
			*/

			AOS.Damage( defender, attacker, m_Damage, 0, 0, 0, 0, 100 );

			if ( m_Target != null )
			{
				defender.MovingEffect( m_Target, weapon.ItemID, 18, 1, false, false );
				Timer.DelayCall( TimeSpan.FromMilliseconds( 333.0 ), new TimerCallback( ThrowAgain ) );
				m_Mobile = attacker;
			}

			ClearCurrentAbility( attacker );
		}

		private Mobile m_Mobile;

		public void ThrowAgain()
		{
			if ( m_Target != null && m_Mobile != null )
			{
				BaseThrown weapon = m_Mobile.Weapon as BaseThrown;

				if ( weapon == null )
					return;

				if ( WeaponAbility.GetCurrentAbility( m_Mobile ) is MysticArc )
					ClearCurrentAbility( m_Mobile );

				if ( weapon.CheckHit( m_Mobile, m_Target ) )
				{
					weapon.OnHit( m_Mobile, m_Target, 0.0 );
					AOS.Damage( m_Target, m_Mobile, m_Damage, 0, 0, 0, 0, 100 );
				}
			}
		}
	}
}