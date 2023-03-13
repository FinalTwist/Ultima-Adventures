using System;
using Server;

namespace Server.Items
{
	public class PegLeghook : BoneHarvester 
	{
		public override int ArtifactRarity{ get{ return 11; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override float MlSpeed{ get{ return 2.75f; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public PegLeghook()
		{
			Weight = 5.0;
            		Name = "Peg Leg Hook";
            		Hue = 1167;

			WeaponAttributes.HitLightning = 50;
			WeaponAttributes.HitLeechMana = 50;
			WeaponAttributes.HitLowerDefend = 50;
			Attributes.WeaponDamage = 60;
			Attributes.Luck = 300;
			Attributes.SpellChanneling = 1;
			Attributes.WeaponSpeed = 40;

			StrRequirement = 70;

			LootType = LootType.Regular;
		}

		public PegLeghook( Serial serial ) : base( serial )
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