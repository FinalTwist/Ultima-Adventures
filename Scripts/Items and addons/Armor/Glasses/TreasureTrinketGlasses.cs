using System;
using Server;

namespace Server.Items
{
	public class TreasureTrinketGlasses : ElvenGlasses
	{
		public override int LabelNumber{ get{ return 1073373; } } //Treasures and Trinkets Reading Glasses

		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public TreasureTrinketGlasses()
		{
			Attributes.BonusInt = 10;
			Attributes.BonusHits = 5;
			Attributes.SpellDamage = 10;
		}
		public TreasureTrinketGlasses( Serial serial ) : base( serial )
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
