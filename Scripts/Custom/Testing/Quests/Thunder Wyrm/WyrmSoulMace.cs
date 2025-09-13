using System;
using Server;

namespace Server.Items
{
	public class WyrmSoulMace : Mace 
	{
		public override int ArtifactRarity{ get{ return 11; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.BleedAttack; } }

		public override int AosMinDamage{ get{ return 18; } }
		public override int AosMaxDamage{ get{ return 22; } }
		public override int AosSpeed{ get{ return 40; } }
		public override float MlSpeed{ get{ return 2.50f; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 100; } }

		[Constructable]
		public WyrmSoulMace()
		{
			Weight = 5.0;
            		Name = "a Wyrm Soul's Mace";
            		Hue = 1154;
                         
			WeaponAttributes.HitLightning = 60;

			Attributes.AttackChance = 25;
	//		Attributes.BonusDex = 7;
			Attributes.BonusHits = 10;
	//		Attributes.BonusInt = 7;
	//		Attributes.BonusMana = 10;
	//		Attributes.BonusStam = 10;
			Attributes.BonusStr = 15;
	//		Attributes.CastRecovery = x;
	//		Attributes.CastSpeed = x;
			Attributes.DefendChance = 10;
	//		Attributes.EnhancePotions = x;
	//		Attributes.LowerManaCost = 10;
	//		Attributes.LowerRegCost = 10;
	//		Attributes.Luck =120;
	//		Attributes.ReflectPhysical = x;
	//		Attributes.RegenHits = 2;
	//		Attributes.RegenMana = 2;
	//		Attributes.RegenStam = 2;
			Attributes.SpellChanneling = 1; // 1 for true, 0 for false
			Attributes.SpellDamage = 10;
			Attributes.WeaponDamage = 70;
			Attributes.WeaponSpeed = 40;
	//		Attributes.ReflectPhysical = x;
	//		Attributes.RegenHits = x;
	//		WeaponAttributes.DurabilityBonus = x; 
	//		WeaponAttributes.HitColdArea = x;
	//		WeaponAttributes.HitDispel = x;
	//		WeaponAttributes.HitEnergyArea = x;
	//		WeaponAttributes.HitFireArea = x;
			WeaponAttributes.HitFireball = 52;
	//		WeaponAttributes.HitHarm = x;
			WeaponAttributes.HitLeechHits = 48;
	//		WeaponAttributes.HitLeechMana = 52;
			WeaponAttributes.HitLeechStam = 52;
	//		WeaponAttributes.HitLightning = x;
			WeaponAttributes.HitLowerAttack = 45;
			WeaponAttributes.HitLowerDefend = 55;
	//		WeaponAttributes.HitMagicArrow = x;
	//		WeaponAttributes.HitPhysicalArea = x;
	//		WeaponAttributes.HitPoisonArea = x;
	//		WeaponAttributes.LowerStatReq = x;
	//		WeaponAttributes.MageWeapon = x; // 1 for true, 0 for false.
	//		WeaponAttributes.ResistColdBonus = x;
	//		WeaponAttributes.ResistEnergyBonus = x;
	//		WeaponAttributes.ResistFireBonus = x;
	//		WeaponAttributes.ResistPhysicalBonus = x;
	//		WeaponAttributes.ResistPoisonBonus = x;
			WeaponAttributes.SelfRepair = 15;
	//		WeaponAttributes.UseBestSkill = x; // 1 for true, 0 for false.
	//		PhysicalBonus = 20;
	//		FireBonus = -5;
	//		ColdBonus = 10;
	//		PoisonBonus = -10;
	//		EnergyBonus = 10;

			DexRequirement = 35;

			LootType = LootType.Blessed;
		}

		public WyrmSoulMace( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}