using System;
using Server;

namespace Server.Items
{
	public class MaritimeGlasses : ElvenGlasses
	{
		public override int LabelNumber{ get{ return 1073364; } } //Maritime Reading Glasses

		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 30; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public MaritimeGlasses()
		{
			Attributes.Luck = 150;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 20;
		}
		public MaritimeGlasses( Serial serial ) : base( serial )
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
