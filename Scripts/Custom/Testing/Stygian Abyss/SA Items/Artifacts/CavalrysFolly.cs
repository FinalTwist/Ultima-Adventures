using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{

	public class CavalrysFolly : BaseSpear
	{

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.EarthStrike; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.MagicProtection; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.Disarm; } }
		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		public override int AosStrengthReq{ get{ return 40; } }
		public override int AosMinDamage{ get{ return 14; } }
		public override int AosMaxDamage{ get{ return 16; } }
		public override int AosSpeed{ get{ return 37; } }
		public override float MlSpeed{ get{ return 3.00f; } }

		[Constructable]
		public CavalrysFolly() : base( 0x26BD )
		{
		
			Name = ("Cavalry's Folly");
		
			Hue = 1165;
		
			Weight = 4.0;
			Attributes.BonusHits = 2;
			Attributes.AttackChance = 10;
			Attributes.WeaponDamage = 45;
			Attributes.WeaponSpeed = 35;
			WeaponAttributes.HitLowerDefend = 40;	
			WeaponAttributes.HitFireball = 40;
			
		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public CavalrysFolly( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}