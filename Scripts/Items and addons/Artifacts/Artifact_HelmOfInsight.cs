using System;
using Server;


namespace Server.Items
{
	public class HelmOfInsight : PlateHelm
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061096; } } // Helm of Insight


		public override int BaseEnergyResistance{ get{ return 17; } }


		[Constructable]
		public HelmOfInsight()
		{
			Name = "Helm of Insight";
			Hue = 0x554;
			Attributes.BonusInt = 20; //=
			Attributes.BonusMana = 10;
			Attributes.RegenMana = 2;
			Attributes.LowerManaCost = 5;
			Attributes.CastRecovery = 1;
			ArmorAttributes.MageArmor = 1;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public HelmOfInsight( Serial serial ) : base( serial )
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
				EnergyBonus = 0;
		}
	}
}