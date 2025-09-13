using System;
using Server;


namespace Server.Items
{
	public class ArmsOfInsight : PlateArms
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061096; } } // Arms of Insight


		public override int BaseEnergyResistance{ get{ return 15; } }


		[Constructable]
		public ArmsOfInsight()
		{
			Name = "Arms of Insight";
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


		public ArmsOfInsight( Serial serial ) : base( serial )
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