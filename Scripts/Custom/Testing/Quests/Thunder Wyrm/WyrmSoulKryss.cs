using System;
using Server;

namespace Server.Items
{
	public class WyrmSoulKryss : Kryss 
	{
		public override int ArtifactRarity{ get{ return 11; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.BleedAttack; } }

		public override int AosMinDamage{ get{ return 18; } }
		public override int AosMaxDamage{ get{ return 22; } }
		public override int AosSpeed{ get{ return 56; } }
		public override float MlSpeed{ get{ return 2.50f; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 100; } }

		[Constructable]
		public WyrmSoulKryss()
		{
			Weight = 5.0;
            		Name = "a Wyrm Soul's Kryss";
            		Hue = 1154;
                         
			WeaponAttributes.HitLightning = 60;
			WeaponAttributes.HitLeechHits = 52;
			WeaponAttributes.HitLeechMana = 46;
			WeaponAttributes.HitLightning = 58;
			WeaponAttributes.HitLowerDefend = 47;
			WeaponAttributes.SelfRepair = 5;

			Attributes.SpellChanneling = 1;
			Attributes.BonusStr = 15;
			Attributes.Luck = 120;
			Attributes.WeaponDamage = 65;
			Attributes.WeaponSpeed = 40;

			DexRequirement = 35;

			LootType = LootType.Blessed;
		}

		public WyrmSoulKryss( Serial serial ) : base( serial )
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