using System;
using Server;

namespace Server.Items
{
	public class WyrmSoulShield : OrderShield 
	{
		public override int ArtifactRarity{ get{ return 11; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 100; } }

		[Constructable]
		public WyrmSoulShield()
		{
			Weight = 5.0;
            		Name = "a Wyrm Soul's Shield";
            		Hue = 1154;
                         
	//		Attributes.AttackChance = x;
	//		Attributes.BonusDex = 7;
	//		Attributes.BonusHits = 10;
			Attributes.BonusInt = 15;
	//		Attributes.BonusMana = 10;
	//		Attributes.BonusStam = 10;
	//		Attributes.BonusStr = 7;
			Attributes.CastRecovery = 2;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 25;
	//		Attributes.EnhancePotions = x;
			Attributes.LowerManaCost = 20;
	//		Attributes.LowerRegCost = 10;
			Attributes.Luck = 120;
			Attributes.ReflectPhysical = 17;
	//		Attributes.RegenHits = 2;
	//		Attributes.RegenMana = 2;
	//		Attributes.RegenStam = 2;
			Attributes.SpellChanneling = 1; // 1 for true, 0 for false
	//		Attributes.SpellDamage = x;
	//		Attributes.WeaponDamage = x;
	//		Attributes.WeaponSpeed = x;
	//		Attributes.ReflectPhysical = 20;
	//		Attributes.RegenHits = x;
	//		WeaponAttributes.DurabilityBonus = x; 
	//		WeaponAttributes.HitColdArea = x;
	//		WeaponAttributes.HitDispel = x;
	//		WeaponAttributes.HitEnergyArea = x;
	//		WeaponAttributes.HitFireArea = x;
	//		WeaponAttributes.HitFireball = x;
	//		WeaponAttributes.HitHarm = x;
	//		WeaponAttributes.HitLeechHits = x;
	//		WeaponAttributes.HitLeechMana = x;
	//		WeaponAttributes.HitLeechStam = x;
	//		WeaponAttributes.HitLightning = x;
	//		WeaponAttributes.HitLowerAttack = x;
	//		WeaponAttributes.HitLowerDefend = x;
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
			ArmorAttributes.SelfRepair = 5;
	//		WeaponAttributes.UseBestSkill = x; // 1 for true, 0 for false.
	//		PhysicalBonus = 20;
	//		FireBonus = -5;
	//		ColdBonus = 10;
	//		PoisonBonus = -10;
	//		EnergyBonus = 10;

			LootType = LootType.Blessed;
		}

		public WyrmSoulShield( Serial serial ) : base( serial )
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