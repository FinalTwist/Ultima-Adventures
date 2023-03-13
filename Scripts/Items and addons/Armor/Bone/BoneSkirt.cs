using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1C08, 0x1C09 )]
	public class BoneSkirt : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 7; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 7; } }
		public override int BaseEnergyResistance{ get{ return 7; } }

		public override int InitMinHits{ get{ return 25; } }
		public override int InitMaxHits{ get{ return 30; } }

		public override int AosStrReq{ get{ return 55; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int OldDexBonus{ get{ return -4; } }

		public override int ArmorBase{ get{ return 30; } }
		public override int RevertArmorBase{ get{ return 7; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Bone; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public BoneSkirt() : base( 0x1C08 )
		{
			Weight = 8.0;
			Name = "bone skirt";
		}

		public override void OnLocationChange( Point3D oldLocation )
		{
			if ( this.Hue == 0 ){ this.Hue = 0xB4D; }
		}

		public BoneSkirt( Serial serial ) : base( serial )
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