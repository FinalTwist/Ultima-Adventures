using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class CombativePlateGorget : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 2; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 2; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 65; } }

		public override int AosStrReq{ get{ return 45; } }
		public override int OldStrReq{ get{ return 30; } }

		public override int OldDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public CombativePlateGorget() : base( 0x1413 )
		{
			Weight = 2.0;
			Name = "Combative Plate Gorget";
			ArmorAttributes.MageArmor = 0;
			ArmorAttributes.SelfRepair = 0;
			Attributes.ReflectPhysical = 1;
			Attributes.WeaponDamage = 1;
			Attributes.WeaponSpeed = 1;
			Attributes.DefendChance = 1;
			Attributes.Luck = 10;
		}

		public override bool OnEquip( Mobile from )
		{
			if (from is PlayerMobile)
			{
				PlayerMobile p_PlayerMobile = from as PlayerMobile;
				BaseArmor legsB = this;
				BaseArmor chestB = this;
				BaseArmor armsB = this;
				BaseArmor helmB = this;
				BaseArmor glovesB = this;
				BaseArmor gorgetB = this;

				Item legsA = p_PlayerMobile.FindItemOnLayer( Layer.LastUserValid );
				if (legsA != null) legsB = legsA as BaseArmor;
				Item chestA = p_PlayerMobile.FindItemOnLayer( Layer.InnerTorso );
				if (chestA != null) chestB = chestA as BaseArmor;
				Item armsA = p_PlayerMobile.FindItemOnLayer( Layer.Arms );
				if (armsA != null) armsB = armsA as BaseArmor;
				Item helmA = p_PlayerMobile.FindItemOnLayer( Layer.Helm );
				if (helmA != null) helmB = helmA as BaseArmor;
				Item glovesA = p_PlayerMobile.FindItemOnLayer( Layer.Gloves );
				if (glovesA != null) glovesB = glovesA as BaseArmor;
				Item gorgetA = p_PlayerMobile.FindItemOnLayer( Layer.Neck );
				if (gorgetA != null) gorgetB = gorgetA as BaseArmor;

				int count = 0;
				int bonus = 0;
				if ( legsB != null && ( legsB is CombativePlateLegs || legsB is CombativePlateSkirt) && legsB != this ) count += 1;
				if ( chestB != null && ( chestB is CombativePlateChest || chestB is FemaleCombativePlateChest) && chestB != this ) count += 1;
				if ( armsB != null && armsB is CombativePlateArms && armsB != this ) count += 1;
				if ( helmB != null && helmB is CombativeNorseHelm && helmB != this ) count += 1;
				if ( glovesB != null && glovesB is CombativePlateGloves && glovesB != this ) count += 1;
				if ( gorgetB != null && gorgetB is CombativePlateGorget && gorgetB != this ) count += 1;
				count += 1; //this is here because item does not count until after the return of true at the end ;)
				bonus = count;
				if (count >= 3) bonus +=5;
				if (count >= 6) bonus *= 3;

				if ( legsB != null && ( legsB is CombativePlateLegs || legsB is CombativePlateSkirt) && legsB != this )
				{
					if (count >= 4) legsB.ArmorAttributes.MageArmor = 1;
						else legsB.ArmorAttributes.MageArmor = 0;
					if (count >= 6) legsB.ArmorAttributes.SelfRepair = 2;
						else  if (count >= 3) legsB.ArmorAttributes.SelfRepair = 1;
						else  legsB.ArmorAttributes.SelfRepair = 0;
					legsB.Attributes.ReflectPhysical = bonus;
					legsB.Attributes.WeaponDamage = bonus;
					legsB.Attributes.WeaponSpeed = bonus;
					legsB.Attributes.DefendChance = bonus;
					legsB.Attributes.Luck = 10 * bonus;
				}
				if ( chestB != null && ( chestB is CombativePlateChest || chestB is FemaleCombativePlateChest) && chestB != this )
				{
					if (count >= 4) chestB.ArmorAttributes.MageArmor = 1;
						else chestB.ArmorAttributes.MageArmor = 0;
					if (count >= 6) chestB.ArmorAttributes.SelfRepair = 2;
						else  if (count >= 3) chestB.ArmorAttributes.SelfRepair = 1;
						else  chestB.ArmorAttributes.SelfRepair = 0;
					chestB.Attributes.ReflectPhysical = bonus;
					chestB.Attributes.WeaponDamage = bonus;
					chestB.Attributes.WeaponSpeed = bonus;
					chestB.Attributes.DefendChance = bonus;
					chestB.Attributes.Luck = 10 * bonus;
				}
				if ( armsB != null && armsB is CombativePlateArms && armsB != this )
				{
					if (count >= 4) armsB.ArmorAttributes.MageArmor = 1;
						else armsB.ArmorAttributes.MageArmor = 0;
					if (count >= 6) armsB.ArmorAttributes.SelfRepair = 2;
						else  if (count >= 3) armsB.ArmorAttributes.SelfRepair = 1;
						else  armsB.ArmorAttributes.SelfRepair = 0;
					armsB.Attributes.ReflectPhysical = bonus;
					armsB.Attributes.WeaponDamage = bonus;
					armsB.Attributes.WeaponSpeed = bonus;
					armsB.Attributes.DefendChance = bonus;
					armsB.Attributes.Luck = 10 * bonus;
				}
				if ( helmB != null && helmB is CombativeNorseHelm && helmB != this )
				{
					if (count >= 4) helmB.ArmorAttributes.MageArmor = 1;
						else helmB.ArmorAttributes.MageArmor = 0;
					if (count >= 6) helmB.ArmorAttributes.SelfRepair = 2;
						else  if (count >= 3) helmB.ArmorAttributes.SelfRepair = 1;
						else  helmB.ArmorAttributes.SelfRepair = 0;
					helmB.Attributes.ReflectPhysical = bonus;
					helmB.Attributes.WeaponDamage = bonus;
					helmB.Attributes.WeaponSpeed = bonus;
					helmB.Attributes.DefendChance = bonus;
					helmB.Attributes.Luck = 10 * bonus;
				}
				if ( glovesB != null && glovesB is CombativePlateGloves && glovesB != this )
				{
					if (count >= 4) glovesB.ArmorAttributes.MageArmor = 1;
						else glovesB.ArmorAttributes.MageArmor = 0;
					if (count >= 6) glovesB.ArmorAttributes.SelfRepair = 2;
						else  if (count >= 3) glovesB.ArmorAttributes.SelfRepair = 1;
						else  glovesB.ArmorAttributes.SelfRepair = 0;
					glovesB.Attributes.ReflectPhysical = bonus;
					glovesB.Attributes.WeaponDamage = bonus;
					glovesB.Attributes.WeaponSpeed = bonus;
					glovesB.Attributes.DefendChance = bonus;
					glovesB.Attributes.Luck = 10 * bonus;
				}
				if ( gorgetB != null && gorgetB is CombativePlateGorget && gorgetB != this )
				{
					if (count >= 4) gorgetB.ArmorAttributes.MageArmor = 1;
						else gorgetB.ArmorAttributes.MageArmor = 0;
					if (count >= 6) gorgetB.ArmorAttributes.SelfRepair = 2;
						else  if (count >= 3) gorgetB.ArmorAttributes.SelfRepair = 1;
						else  gorgetB.ArmorAttributes.SelfRepair = 0;
					gorgetB.Attributes.ReflectPhysical = bonus;
					gorgetB.Attributes.WeaponDamage = bonus;
					gorgetB.Attributes.WeaponSpeed = bonus;
					gorgetB.Attributes.DefendChance = bonus;
					gorgetB.Attributes.Luck = 10 * bonus;
				}
				if (count >= 4) this.ArmorAttributes.MageArmor = 1;
					else this.ArmorAttributes.MageArmor = 0;
				if (count >= 6) this.ArmorAttributes.SelfRepair = 2;
					else if (count >= 3) this.ArmorAttributes.SelfRepair = 1;
					else this.ArmorAttributes.SelfRepair = 0;
				this.Attributes.ReflectPhysical = bonus;
				this.Attributes.WeaponDamage = bonus;
				this.Attributes.WeaponSpeed = bonus;
				this.Attributes.DefendChance = bonus;
				this.Attributes.Luck = 10 * bonus;
			}
			return base.OnEquip( from );
		}

		public override void OnRemoved(IEntity parent )
		{
			if (parent is PlayerMobile)
			{
				PlayerMobile p_PlayerMobile = parent as PlayerMobile;
				BaseArmor legsB = this;
				BaseArmor chestB = this;
				BaseArmor armsB = this;
				BaseArmor helmB = this;
				BaseArmor glovesB = this;
				BaseArmor gorgetB = this;

				Item legsA = p_PlayerMobile.FindItemOnLayer( Layer.LastUserValid );
				if (legsA != null) legsB = legsA as BaseArmor;
				Item chestA = p_PlayerMobile.FindItemOnLayer( Layer.InnerTorso );
				if (chestA != null) chestB = chestA as BaseArmor;
				Item armsA = p_PlayerMobile.FindItemOnLayer( Layer.Arms );
				if (armsA != null) armsB = armsA as BaseArmor;
				Item helmA = p_PlayerMobile.FindItemOnLayer( Layer.Helm );
				if (helmA != null) helmB = helmA as BaseArmor;
				Item glovesA = p_PlayerMobile.FindItemOnLayer( Layer.Gloves );
				if (glovesA != null) glovesB = glovesA as BaseArmor;
				Item gorgetA = p_PlayerMobile.FindItemOnLayer( Layer.Neck );
				if (gorgetA != null) gorgetB = gorgetA as BaseArmor;

				int count = 0;
				int bonus = 0;
				if ( legsB != null && ( legsB is CombativePlateLegs || legsB is CombativePlateSkirt) && legsB != this ) count += 1;
				if ( chestB != null && ( chestB is CombativePlateChest || chestB is FemaleCombativePlateChest) && chestB != this ) count += 1;
				if ( armsB != null && armsB is CombativePlateArms && armsB != this ) count += 1;
				if ( helmB != null && helmB is CombativeNorseHelm && helmB != this ) count += 1;
				if ( glovesB != null && glovesB is CombativePlateGloves && glovesB != this ) count += 1;
				if ( gorgetB != null && gorgetB is CombativePlateGorget && gorgetB != this ) count += 1;
				//count -= 1; //not needed because this item does not get counted (because it is "this" - so correct count of what is left
				bonus = count;
				if (count >= 3) bonus +=5;
				if (count >= 6) bonus *= 3;

				if ( legsB != null && ( legsB is CombativePlateLegs || legsB is CombativePlateSkirt) && legsB != this )
				{
					if (count >= 4) legsB.ArmorAttributes.MageArmor = 1;
						else legsB.ArmorAttributes.MageArmor = 0;
					if (count >= 6) legsB.ArmorAttributes.SelfRepair = 2;
						else  if (count >= 3) legsB.ArmorAttributes.SelfRepair = 1;
						else  legsB.ArmorAttributes.SelfRepair = 0;
					legsB.Attributes.ReflectPhysical = bonus;
					legsB.Attributes.WeaponDamage = bonus;
					legsB.Attributes.WeaponSpeed = bonus;
					legsB.Attributes.DefendChance = bonus;
					legsB.Attributes.Luck = 10 * bonus;
				}
				if ( chestB != null && ( chestB is CombativePlateChest || chestB is FemaleCombativePlateChest) && chestB != this )
				{
					if (count >= 4) chestB.ArmorAttributes.MageArmor = 1;
						else chestB.ArmorAttributes.MageArmor = 0;
					if (count >= 6) chestB.ArmorAttributes.SelfRepair = 2;
						else  if (count >= 3) chestB.ArmorAttributes.SelfRepair = 1;
						else  chestB.ArmorAttributes.SelfRepair = 0;
					chestB.Attributes.ReflectPhysical = bonus;
					chestB.Attributes.WeaponDamage = bonus;
					chestB.Attributes.WeaponSpeed = bonus;
					chestB.Attributes.DefendChance = bonus;
					chestB.Attributes.Luck = 10 * bonus;
				}
				if ( armsB != null && armsB is CombativePlateArms && armsB != this )
				{
					if (count >= 4) armsB.ArmorAttributes.MageArmor = 1;
						else armsB.ArmorAttributes.MageArmor = 0;
					if (count >= 6) armsB.ArmorAttributes.SelfRepair = 2;
						else  if (count >= 3) armsB.ArmorAttributes.SelfRepair = 1;
						else  armsB.ArmorAttributes.SelfRepair = 0;
					armsB.Attributes.ReflectPhysical = bonus;
					armsB.Attributes.WeaponDamage = bonus;
					armsB.Attributes.WeaponSpeed = bonus;
					armsB.Attributes.DefendChance = bonus;
					armsB.Attributes.Luck = 10 * bonus;
				}
				if ( helmB != null && helmB is CombativeNorseHelm && helmB != this )
				{
					if (count >= 4) helmB.ArmorAttributes.MageArmor = 1;
						else helmB.ArmorAttributes.MageArmor = 0;
					if (count >= 6) helmB.ArmorAttributes.SelfRepair = 2;
						else  if (count >= 3) helmB.ArmorAttributes.SelfRepair = 1;
						else  helmB.ArmorAttributes.SelfRepair = 0;
					helmB.Attributes.ReflectPhysical = bonus;
					helmB.Attributes.WeaponDamage = bonus;
					helmB.Attributes.WeaponSpeed = bonus;
					helmB.Attributes.DefendChance = bonus;
					helmB.Attributes.Luck = 10 * bonus;
				}
				if ( glovesB != null && glovesB is CombativePlateGloves && glovesB != this )
				{
					if (count >= 4) glovesB.ArmorAttributes.MageArmor = 1;
						else glovesB.ArmorAttributes.MageArmor = 0;
					if (count >= 6) glovesB.ArmorAttributes.SelfRepair = 2;
						else  if (count >= 3) glovesB.ArmorAttributes.SelfRepair = 1;
						else  glovesB.ArmorAttributes.SelfRepair = 0;
					glovesB.Attributes.ReflectPhysical = bonus;
					glovesB.Attributes.WeaponDamage = bonus;
					glovesB.Attributes.WeaponSpeed = bonus;
					glovesB.Attributes.DefendChance = bonus;
					glovesB.Attributes.Luck = 10 * bonus;
				}
				if ( gorgetB != null && gorgetB is CombativePlateGorget && gorgetB != this )
				{
					if (count >= 4) gorgetB.ArmorAttributes.MageArmor = 1;
						else gorgetB.ArmorAttributes.MageArmor = 0;
					if (count >= 6) gorgetB.ArmorAttributes.SelfRepair = 2;
						else  if (count >= 3) gorgetB.ArmorAttributes.SelfRepair = 1;
						else  gorgetB.ArmorAttributes.SelfRepair = 0;
					gorgetB.Attributes.ReflectPhysical = bonus;
					gorgetB.Attributes.WeaponDamage = bonus;
					gorgetB.Attributes.WeaponSpeed = bonus;
					gorgetB.Attributes.DefendChance = bonus;
					gorgetB.Attributes.Luck = 10 * bonus;
				}
				this.ArmorAttributes.MageArmor = 0;
				this.ArmorAttributes.SelfRepair = 0;
				this.Attributes.ReflectPhysical = 1;
				this.Attributes.WeaponDamage = 1;
				this.Attributes.WeaponSpeed = 1;
				this.Attributes.DefendChance = 1;
				this.Attributes.Luck = 10;
			}
			return;
		}

		public CombativePlateGorget( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize(GenericReader reader) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}