using System;
using Server;

namespace Server.Items
{
	public class WizardsGlasses : ElvenGlasses
	{
		public override int LabelNumber{ get{ return 1073374; } } //Wizard's Crystal Reading Glasses

		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public WizardsGlasses()
		{
			Attributes.BonusMana = 10;
			Attributes.RegenMana = 3;
			Attributes.SpellDamage = 15;
		}
		public WizardsGlasses( Serial serial ) : base( serial )
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
