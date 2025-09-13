using System;
using Server;

namespace Server.Items
{
	public class FoldedSteelGlasses : ElvenGlasses
	{
		public override int LabelNumber{ get{ return 1073380; } } //Folded Steel Reading Glasses

		public override int BasePhysicalResistance{ get{ return 20; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public FoldedSteelGlasses()
		{
			Attributes.BonusStr = 8;
			Attributes.NightSight = 1;
			Attributes.DefendChance = 15;
		}
		public FoldedSteelGlasses( Serial serial ) : base( serial )
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
