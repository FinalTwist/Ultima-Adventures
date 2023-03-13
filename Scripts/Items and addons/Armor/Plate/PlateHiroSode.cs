using System;
using Server.Items;

namespace Server.Items
{
	public class PlateHiroSode : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 12; } }
		public override int BaseFireResistance{ get{ return 7; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 7; } }
		public override int BaseEnergyResistance{ get{ return 6; } }

		public override int InitMinHits{ get{ return 55; } }
		public override int InitMaxHits{ get{ return 75; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 75; } }

		public override int ArmorBase{ get{ return 3; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public PlateHiroSode() : base( 0x2780 )
		{
			Weight = 5.0;
		}

		public PlateHiroSode( Serial serial ) : base( serial )
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