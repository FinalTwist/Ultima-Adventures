using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using System.Collections.Generic;
using Server.Misc;
using System.Collections;
using Server.Targeting;

namespace Server.SkillHandlers
{
	public class ArmsLore
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.ArmsLore].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse(Mobile m)
		{
			m.Target = new InternalTarget();

			m.SendLocalizedMessage( 500349 ); // What item do you wish to get information about?

			return TimeSpan.FromSeconds( 1.0 );
		}

		[PlayerVendorTarget]
		private class InternalTarget : Target
		{
			public InternalTarget() : base( 2, false, TargetFlags.None )
			{
				AllowNonlocal = true;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{
					Item examine = (Item)targeted;
					IDItem( from, examine, targeted, false );
				}
			}
		}

		public static void IDItem( Mobile from, Item examine, object targeted, bool automatic )
		{
			if ( !examine.Movable )
			{
				from.SendMessage( "That cannot move so you cannot identify it." );
			}
			else if ( !from.InRange( examine.GetWorldLocation(), 3 ) )
			{
				from.SendMessage( "You will need to get closer to identify that." );
			}
			else if ( !(examine.IsChildOf( from.Backpack )) && Server.Misc.MyServerSettings.IdentifyItemsOnlyInPack() ) 
			{
				from.SendMessage( "This must be in your backpack to identify." );
			}
			else if ( targeted is UnidentifiedItem )
			{
				UnidentifiedItem relic = (UnidentifiedItem)targeted;

				if ( relic.IDAttempt > 5 && !automatic )
				{
					from.SendMessage("Only a vendor can identify this item now as too many attempts were made.");
				}
				else if ( relic.SkillRequired != "ArmsLore" && !automatic )
				{
					from.SendMessage("You are using the wrong skill to figure this out.");
				}
				else if ( from.CheckTargetSkill( SkillName.ArmsLore, targeted, (Utility.RandomMinMax(-5, 70)), 120 ) || automatic )
				{
					Container pack = (Container)relic;
						List<Item> items = new List<Item>();
						foreach (Item item in pack.Items)
						{
							items.Add(item);
						}
						foreach (Item item in items)
						{
							from.AddToBackpack ( item );
						}

						from.SendMessage("You successfully identify the item.");
					relic.Delete();
				}
				else
				{
					relic.IDAttempt = relic.IDAttempt + 1;
					from.SendMessage("You can't seem to identify this item.");
				}
			}
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			else if ( Server.Misc.RelicItems.IsRelicItem( examine ) == true )
			{
				from.SendMessage( Server.Misc.RelicItems.IdentifyRelicValue( from, from, examine ) );
			}
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			else if ( targeted is BaseWeapon )
			{
				if ( ((BaseWeapon)targeted).Identified == true )
				{
					BaseWeapon weap = (BaseWeapon)targeted;

					if ( weap.MaxHitPoints != 0 )
					{
						int hp = (int)((weap.HitPoints / (double)weap.MaxHitPoints) * 10);

						if ( hp < 0 )
							hp = 0;
						else if ( hp > 9 )
							hp = 9;

						from.SendLocalizedMessage( 1038285 + hp );
					}

					int damage = (weap.MaxDamage + weap.MinDamage) / 2;
					int hand = (weap.Layer == Layer.OneHanded ? 0 : 1);

					if ( damage < 3 )
						damage = 0;
					else
						damage = (int)Math.Ceiling( Math.Min( damage, 30 ) / 5.0 );

					WeaponType type = weap.Type;

					if ( type == WeaponType.Ranged )
						from.SendLocalizedMessage( 1038224 + (damage * 9) );
					else if ( type == WeaponType.Piercing )
						from.SendLocalizedMessage( 1038218 + hand + (damage * 9) );
					else if ( type == WeaponType.Slashing )
						from.SendLocalizedMessage( 1038220 + hand + (damage * 9) );
					else if ( type == WeaponType.Bashing )
						from.SendLocalizedMessage( 1038222 + hand + (damage * 9) );
					else
						from.SendLocalizedMessage( 1038216 + hand + (damage * 9) );

					if ( weap.Poison != null && weap.PoisonCharges > 0 )
						from.SendLocalizedMessage( 1038284 ); // It appears to have poison smeared on it.
				}
				else if ( from.CheckTargetSkill( SkillName.ArmsLore, targeted, (Utility.RandomMinMax(0, 70)), 100 ) )
				{
					((BaseWeapon)targeted).Identified = true;
					BaseWeapon weap = (BaseWeapon)targeted;

					if ( weap.MaxHitPoints != 0 )
					{
						int hp = (int)((weap.HitPoints / (double)weap.MaxHitPoints) * 10);

						if ( hp < 0 )
							hp = 0;
						else if ( hp > 9 )
							hp = 9;

						from.SendLocalizedMessage( 1038285 + hp );
					}

					int damage = (weap.MaxDamage + weap.MinDamage) / 2;
					int hand = (weap.Layer == Layer.OneHanded ? 0 : 1);

					if ( damage < 3 )
						damage = 0;
					else
						damage = (int)Math.Ceiling( Math.Min( damage, 30 ) / 5.0 );

					WeaponType type = weap.Type;

					if ( type == WeaponType.Ranged )
						from.SendLocalizedMessage( 1038224 + (damage * 9) );
					else if ( type == WeaponType.Piercing )
						from.SendLocalizedMessage( 1038218 + hand + (damage * 9) );
					else if ( type == WeaponType.Slashing )
						from.SendLocalizedMessage( 1038220 + hand + (damage * 9) );
					else if ( type == WeaponType.Bashing )
						from.SendLocalizedMessage( 1038222 + hand + (damage * 9) );
					else
						from.SendLocalizedMessage( 1038216 + hand + (damage * 9) );

					if ( weap.Poison != null && weap.PoisonCharges > 0 )
						from.SendLocalizedMessage( 1038284 ); // It appears to have poison smeared on it.
				}
				else
				{
					from.SendLocalizedMessage( 500353 ); // You are not certain...
				}
			}
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			else if(targeted is BaseArmor)
			{
				if ( ((BaseArmor)targeted).Identified == true )
				{
					BaseArmor arm = (BaseArmor)targeted;

					if ( arm.MaxHitPoints != 0 )
					{
						int hp = (int)((arm.HitPoints / (double)arm.MaxHitPoints) * 10);

						if ( hp < 0 )
							hp = 0;
						else if ( hp > 9 )
							hp = 9;

						from.SendLocalizedMessage( 1038285 + hp );
					}
				}
				else if( from.CheckTargetSkill(SkillName.ArmsLore, targeted, (Utility.RandomMinMax(0, 70)), 100) )
				{
					((BaseArmor)targeted).Identified = true;
					BaseArmor arm = (BaseArmor)targeted;

					if ( arm.MaxHitPoints != 0 )
					{
						int hp = (int)((arm.HitPoints / (double)arm.MaxHitPoints) * 10);

						if ( hp < 0 )
							hp = 0;
						else if ( hp > 9 )
							hp = 9;

						from.SendLocalizedMessage( 1038285 + hp );
					}

					from.SendLocalizedMessage( 1038295 + (int)Math.Ceiling( Math.Min( arm.ArmorRating, 35 ) / 5.0 ) );
					/*
					if ( arm.ArmorRating < 1 )
						from.SendLocalizedMessage( 1038295 ); // This armor offers no defense against attackers.
					else if ( arm.ArmorRating < 6 )
						from.SendLocalizedMessage( 1038296 ); // This armor provides almost no protection.
					else if ( arm.ArmorRating < 11 )
						from.SendLocalizedMessage( 1038297 ); // This armor provides very little protection.
					else if ( arm.ArmorRating < 16 )
						from.SendLocalizedMessage( 1038298 ); // This armor offers some protection against blows.
					else if ( arm.ArmorRating < 21 )
						from.SendLocalizedMessage( 1038299 ); // This armor serves as sturdy protection.
					else if ( arm.ArmorRating < 26 )
						from.SendLocalizedMessage( 1038300 ); // This armor is a superior defense against attack.
					else if ( arm.ArmorRating < 31 )
						from.SendLocalizedMessage( 1038301 ); // This armor offers excellent protection.
					else
						from.SendLocalizedMessage( 1038302 ); // This armor is superbly crafted to provide maximum protection.
					 * */
				}
				else
				{
					from.SendLocalizedMessage( 500353 ); // You are not certain...
				}
			}
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			else if ( targeted is SwampDragon && ((SwampDragon)targeted).HasBarding )
			{
				SwampDragon pet = (SwampDragon)targeted;

				if ( from.CheckTargetSkill( SkillName.ArmsLore, targeted, (Utility.RandomMinMax(0, 50)), 100 ) )
				{
					int perc = (4 * pet.BardingHP) / pet.BardingMaxHP;

					if ( perc < 0 )
						perc = 0;
					else if ( perc > 4 )
						perc = 4;

					pet.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1053021 - perc, from.NetState );
				}
				else
				{
					from.SendLocalizedMessage( 500353 ); // You are not certain...
				}
			}
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			else
			{
				from.SendLocalizedMessage( 500352 ); // This is neither weapon nor armor.
			}
		}
	}
}