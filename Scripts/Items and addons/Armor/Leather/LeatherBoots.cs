using System;

namespace Server.Items
{
	[FlipableAttribute( 0x170d, 0x170e )]
	public class LeatherSandals : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 6; } }

		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 5; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public LeatherSandals() : base( 0x170d )
		{
			Name = "leather sandals";
			Weight = 2.0;
			Layer = Layer.Shoes;
			Hue = 0x83E;
		}

		public LeatherSandals( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x170f, 0x1710 )]
	public class LeatherShoes : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 2; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 2; } }
		public override int BaseEnergyResistance{ get{ return 2; } }

		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 6; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public LeatherShoes() : base( 0x170f )
		{
			Name = "leather shoes";
			Weight = 3.0;
			Layer = Layer.Shoes;
			Hue = 0x83E;
		}

		public LeatherShoes( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x170b, 0x170c )]
	public class LeatherBoots : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 7; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public LeatherBoots() : base( 0x170b )
		{
			Name = "leather boots";
			Weight = 4.0;
			Layer = Layer.Shoes;
			Hue = 0x83E;
		}

		public LeatherBoots( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x1711, 0x1712 )]
	public class LeatherThighBoots : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 8; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public LeatherThighBoots() : base( 0x1711 )
		{
			Name = "leather thigh boots";
			Weight = 5.0;
			Layer = Layer.Shoes;
			Hue = 0x83E;
		}

		public LeatherThighBoots( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x2FC4, 0x317A )]
	public class LeatherSoftBoots : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 7; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public LeatherSoftBoots() : base( 0x2FC4 )
		{
			Name = "soft leather boots";
			Weight = 2.0;
			Layer = Layer.Shoes;
			Hue = 0x83E;

			SkillBonuses.SetValues( 1, SkillName.Stealth, 10 );
			SkillBonuses.SetValues( 0, SkillName.Hiding, 10 );
		}

		public LeatherSoftBoots( Serial serial ) : base( serial )
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
