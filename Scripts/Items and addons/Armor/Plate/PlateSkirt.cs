using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1C08, 0x1C09 )]
	public class PlateSkirt : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 12; } }
		public override int BaseFireResistance{ get{ return 6; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 6; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 65; } }

		public override int AosStrReq{ get{ return 90; } }

		public override int OldStrReq{ get{ return 60; } }
		public override int OldDexBonus{ get{ return -6; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public PlateSkirt() : base( 0x1C08 )
		{
			Weight = 12.0;
			Name = "platemail skirt";
		}

		public override void OnLocationChange( Point3D oldLocation )
		{
			if ( this.Resource == CraftResource.Iron && this.Hue == 0 ){ this.Hue = 0xB70; }
		}

		public PlateSkirt( Serial serial ) : base( serial )
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