using System;
using Server;


namespace Server.Items
{
	public class InquisitorsResolution : PlateGloves
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1060206; } } // The Inquisitor's Resolution


		public override int BaseColdResistance{ get{ return 22; } }
		public override int BaseEnergyResistance{ get{ return 17; } }


		[Constructable]
		public InquisitorsResolution()
		{
			Name = "Inquisitor's Resolution";
			Hue = 0x4F2;
			Attributes.CastRecovery = 2;
			Attributes.LowerManaCost = 10;
			Attributes.LowerRegCost = 10;
			ArmorAttributes.MageArmor = 1;
			Attributes.BonusMana = 30;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public InquisitorsResolution( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );


			writer.Write( (int) 1 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();


			if ( version < 1 )
			{
				ColdBonus = 0;
				EnergyBonus = 0;
			}
		}
	}
}