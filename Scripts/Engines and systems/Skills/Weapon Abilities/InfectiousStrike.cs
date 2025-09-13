using System;

namespace Server.Items
{
	/// <summary>
	/// This special move represents a significant change to the use of poisons in Age of Shadows.
	/// Now, only certain weapon types � those that have Infectious Strike as an available special move � will be able to be poisoned.
	/// Targets will no longer be poisoned at random when hit by poisoned weapons.
	/// Instead, the wielder must use this ability to deliver the venom.
	/// While no skill in Poisoning is directly required to use this ability, being knowledgeable in the application and use of toxins
	/// will allow a character to use Infectious Strike at reduced mana cost and with a chance to inflict more deadly poison on his victim.
	/// With this change, weapons will no longer be corroded by poison.
	/// Level 5 poison will be possible when using this special move.
	/// </summary>
	public class InfectiousStrike : WeaponAbility
	{
		public InfectiousStrike()
		{
		}

		public override int BaseMana{ get{ return 15; } }

		public override bool RequiresTactics( Mobile from )
		{
			return false;
		}

		public override void OnHit( Mobile attacker, Mobile defender, int damage )
		{
			if ( !Validate( attacker ) )
				return;

			ClearCurrentAbility( attacker );

			BaseWeapon weapon = attacker.Weapon as BaseWeapon;

			if ( weapon == null )
				return;

			Poison p = weapon.Poison;

			int ClassicPoisons = 0;
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( attacker );
			if ( DB != null )
			{
				ClassicPoisons = DB.ClassicPoisoning;
			}

			if ( ClassicPoisons > 0 )
			{
				attacker.SendMessage( "You cannot use this attack with your current poison settings!" );
				return;
			}
			if ( p == null || weapon.PoisonCharges <= 0 )
			{
				attacker.SendLocalizedMessage( 1061141 ); // Your weapon must have a dose of poison to perform an infectious strike!
				return;
			}

			if ( !CheckMana( attacker, true ) )
				return;

			--weapon.PoisonCharges;

			// Infectious strike special move now uses poisoning skill to help determine potency 
			int maxLevel = attacker.Skills[SkillName.Poisoning].Fixed / 200;
			if ( maxLevel < 0 ) maxLevel = 0;
			if ( p.Level > maxLevel ) p = Poison.GetPoison( maxLevel );

			if ( (attacker.Skills[SkillName.Poisoning].Value / 100.0) > Utility.RandomDouble() )
			{
				int level = p.Level + 1;
				Poison newPoison = Poison.GetPoison( level );

				if ( newPoison != null )
				{
					p = newPoison;

					attacker.SendLocalizedMessage( 1060080 ); // Your precise strike has increased the level of the poison by 1
					defender.SendLocalizedMessage( 1060081 ); // The poison seems extra effective!
				}
			}

			defender.PlaySound( 0xDD );
			defender.FixedParticles( 0x3728, 244, 25, 9941, 1266, 0, EffectLayer.Waist );

			if ( defender.ApplyPoison( attacker, p ) != ApplyPoisonResult.Immune )
			{
				Misc.Titles.AwardKarma( attacker, -20, true );
				attacker.SendLocalizedMessage( 1008096, true, defender.Name ); // You have poisoned your target : 
				defender.SendLocalizedMessage( 1008097, false, attacker.Name ); //  : poisoned you!
			}
		}
	}
}