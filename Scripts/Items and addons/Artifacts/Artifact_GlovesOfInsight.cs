using System;
using Server;


namespace Server.Items
{
	public class GlovesOfInsight : PlateGloves
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061096; } } // Gloves of Insight


		public override int BaseEnergyResistance{ get{ return 13; } }


		[Constructable]
		public GlovesOfInsight()
		{
			Name = "Gloves of Insight";
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


		public GlovesOfInsight( Serial serial ) : base( serial )
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