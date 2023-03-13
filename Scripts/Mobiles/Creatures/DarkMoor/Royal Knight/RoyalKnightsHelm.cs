using System;
using Server.Items;

namespace Server.Items
{
	public class Royalknightshelm : BaseArmor
	{
                public override int ArtifactRarity{ get{ return 20; } } 

		public override int BasePhysicalResistance{ get{ return 15; } }
		public override int BaseFireResistance{ get{ return 15; } }
		public override int BaseColdResistance{ get{ return 15; } }
		public override int BasePoisonResistance{ get{ return 15; } }
		public override int BaseEnergyResistance{ get{ return 15; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		public override int AosStrReq{ get{ return 70; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int OldDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public Royalknightshelm() : base( 0x1412 )
		{
			Weight = 5.0;
                        Name = "Royal Knights Helm";
            		Hue = 1351;

			Attributes.BonusMana = 10;
			Attributes.DefendChance = 10;
			Attributes.Luck = 100;
		}

		public Royalknightshelm( Serial serial ) : base( serial )
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

			if ( Weight == 1.0 )
				Weight = 5.0;
		}
	}
}