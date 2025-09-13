using System;
using Server;


namespace Server.Items
{
	public class ArmsOfTheFallenKing : LeatherArms
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061094; } } // Arms of the Fallen King


		public override int BaseColdResistance{ get{ return 15; } }
		public override int BaseEnergyResistance{ get{ return 15; } }


		[Constructable]
		public ArmsOfTheFallenKing()
		{
			Name = "Arms of the Fallen";
			Hue = 0x76D;
			Attributes.BonusStr = 5;
			Attributes.RegenHits = 15;
			Attributes.RegenStam = 5;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public ArmsOfTheFallenKing( Serial serial ) : base( serial )
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
				if ( Hue == 0x551 )
					Hue = 0x76D;


				ColdBonus = 0;
				EnergyBonus = 0;
			}
		}
	}
}