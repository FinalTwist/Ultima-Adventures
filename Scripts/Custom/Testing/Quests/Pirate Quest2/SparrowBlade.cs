using System;
using Server;

namespace Server.Items
{
	public class SparrowBlade : Katana 
	{
		public override int ArtifactRarity{ get{ return 6; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override float MlSpeed{ get{ return 2.25f; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public SparrowBlade()
		{
			Weight = 5.0;
            		Name = "Captian Sparrow's Blade";
            		Hue = 1157;

			WeaponAttributes.HitLightning = 54;
			WeaponAttributes.HitHarm = 48;
			WeaponAttributes.HitLowerDefend = 57;

			Attributes.Luck = 300;
			Attributes.SpellChanneling = 1;
			Attributes.WeaponSpeed = 55;

			StrRequirement = 70;

			LootType = LootType.Regular;
		}

		public SparrowBlade( Serial serial ) : base( serial )
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