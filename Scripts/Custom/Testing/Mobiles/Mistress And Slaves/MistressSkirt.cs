using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1c08, 0x1c09 )]
	public class MistressSkirt : BaseArmor
	{
		public override int ArtifactRarity{ get{ return 58; } }

		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 9; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		public override int AosStrReq{ get{ return 20; } }

		public override int ArmorBase{ get{ return 15; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		public override bool AllowMaleWearer{ get{ return false; } }

		[Constructable]
		public MistressSkirt() : base( 0x1C08 )
		{
			Weight = 1.0;
			Name = "Mistress Skirt";
			Hue = 1950;

			Attributes.AttackChance = 5;
			Attributes.DefendChance = 5;
			Attributes.EnhancePotions = 10;
			Attributes.LowerManaCost = 5;
			Attributes.LowerRegCost = 20;
			Attributes.Luck = 150;
			Attributes.NightSight = 5;
			Attributes.BonusMana = 15;
			Attributes.SpellDamage = 10;
			Attributes.WeaponSpeed = 5;
		}

		public MistressSkirt( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			if ( Weight == 3.0 )
				Weight = 1.0;
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}