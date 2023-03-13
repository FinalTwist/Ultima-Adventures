using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1C08, 0x1C09 )]
	public class RingmailSkirt : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 9; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 2; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 2; } }

		public override int InitMinHits{ get{ return 40; } }
		public override int InitMaxHits{ get{ return 50; } }

		public override int AosStrReq{ get{ return 40; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int OldDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 22; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Ringmail; } }

		[Constructable]
		public RingmailSkirt() : base( 0x1C08 )
		{
			Weight = 12.0;
			Name = "banded mail skirt";
		}

		public override void OnLocationChange( Point3D oldLocation )
		{
			if ( this.Resource == CraftResource.Iron && this.Hue == 0 ){ this.Hue = 0xAC0; }
		}

		public RingmailSkirt( Serial serial ) : base( serial )
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