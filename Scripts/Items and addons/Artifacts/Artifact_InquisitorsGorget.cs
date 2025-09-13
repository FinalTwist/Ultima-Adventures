using System;
using Server;


namespace Server.Items
{
	public class InquisitorsGorget : PlateGorget
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1060206; } } // The Inquisitor's Gorget


		public override int BaseColdResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 12; } }


		[Constructable]
		public InquisitorsGorget()
		{
			Name = "Inquisitor's Gorget";
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


		public InquisitorsGorget( Serial serial ) : base( serial )
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