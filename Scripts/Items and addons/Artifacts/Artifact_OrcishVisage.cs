using System;
using Server;


namespace Server.Items
{
	public class OrcishVisage : OrcHelm
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1070691; } }


		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 5; } }


		[Constructable]
		public OrcishVisage()
		{
			Hue = 0x592;
			ArmorAttributes.SelfRepair = 3;
			Attributes.BonusStr = 10;
			Attributes.BonusStam = 5;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public OrcishVisage( Serial serial ) : base( serial )
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