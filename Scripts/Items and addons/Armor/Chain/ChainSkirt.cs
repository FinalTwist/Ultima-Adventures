using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1C08, 0x1C09 )]
	public class ChainSkirt : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 1; } }
		public override int BaseEnergyResistance{ get{ return 2; } }

		public override int InitMinHits{ get{ return 45; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override int AosStrReq{ get{ return 60; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int OldDexBonus{ get{ return -3; } }

		public override int ArmorBase{ get{ return 28; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Chainmail; } }

		[Constructable]
		public ChainSkirt() : base( 0x1C08 )
		{
			Weight = 12.0;
			Name = "metal skirt";
		}

		public override void OnLocationChange( Point3D oldLocation )
		{
			if ( this.Resource == CraftResource.Iron && this.Hue == 0 ){ this.Hue = 0x430; }
		}

		public ChainSkirt( Serial serial ) : base( serial )
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