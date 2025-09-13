
using System;
using Server;

namespace Server.Items
{
	public class HellSword : ThinLongsword  // your name of weapon(no spaces) : name of the base weapon you want to use. ie:Katana (make sure you capitalize both names)
	{
		public override int ArtifactRarity{ get{ return 666; } } // Set the artifact rarity here.

		//public override int EffectID{ get{ return nn; } } // 0x1BFE (bolt) or 0xF42 (arrow)
		//public override Type AmmoType{ get{ return typeof( nn ); } } // Bolt or Arrow
		//public override Item Ammo{ get{ return new nn(); } } // Bolt or Arrow

		//public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ShadowStrike; } } //Only select one primary
		//public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }


		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		//public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }
		//public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }

		public override int InitMinHits{ get{ return 25; } } // Set the minium amount of hit points for the weapon.
		public override int InitMaxHits{ get{ return 50; } } // Set the Maxium amount of hit points for the weapon.

		[Constructable]
		public HellSword() // your name of weapon(no spaces)
		{
			Weight = 4.0; // Weight in stones for your weapon
            		Name = "Bogus Killer";  // Name of your weapon with spaces.
            		Hue = 2117;     // The color of your weapon.

			//WeaponAttributes.DurabilityBonus = 0; // Pick and choose the attributes for your weapon (remember to remove the // before the ones you entend to use)
			//WeaponAttributes.HitColdArea = 0;
			WeaponAttributes.HitDispel = 10;
			//WeaponAttributes.HitEnergyArea = 0;
			//WeaponAttributes.HitFireArea = 0;
			WeaponAttributes.HitFireball = 25;
			//WeaponAttributes.HitHarm = 0;
			WeaponAttributes.HitLeechHits = 20;
			WeaponAttributes.HitLeechMana = 20;
			WeaponAttributes.HitLeechStam = 20;                                   
			//WeaponAttributes.HitLightning = 50;
			//WeaponAttributes.HitLowerAttack = 0;
			//WeaponAttributes.HitLowerDefence = 0;
			//WeaponAttributes.HitMagicArrow = 0;
			//WeaponAttributes.HitPhysicalArea = 10;
			//WeaponAttributes.HitPoisonArea = 0;
			//WeaponAttributes.LowerStatReq = 0;
			//WeaponAttributes.MageWeapon = 1;    
			//WeaponAttributes.ResistColdBonus = 12;
			WeaponAttributes.ResistEnergyBonus = 2;
			WeaponAttributes.ResistPhysicalBonus = 4;
			WeaponAttributes.ResistFireBonus = 15;
			WeaponAttributes.ResistPoisonBonus = 2;
			//WeaponAttributes.SelfRepair = 1;

			Attributes.AttackChance = 15;
			Attributes.BonusDex = 10;
			//Attributes.BonusHits = 20;
			//Attributes.BonusInt = 20;
			//Attributes.BonusMana = 10;
			Attributes.BonusStam = 10;
			Attributes.BonusStr = 5;
			//Attributes.CastRecovery = 2;
			//Attributes.CastSpeed = 2;
			//Attributes.DefendChance = 2;
			//Attributes.EnhancePotions = 0;
			//Attributes.LowerRegCost = 100;
			Attributes.Luck = 100;
			Attributes.SpellChanneling = 1;
			//Attributes.SpellDamage = 10;

	

			//LootType = LootType.Blessed; //Blessed, Newbied or Cursed
		}

		public HellSword( Serial serial ) : base( serial ) // your name of weapon(no spaces)
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