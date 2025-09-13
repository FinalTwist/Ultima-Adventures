using System;
using Server;


namespace Server.Items
{
	public class JackalsHelm : PlateHelm
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061594; } } // Jackal's Helm


		public override int BaseFireResistance{ get{ return 9; } }
		public override int BaseColdResistance{ get{ return 9; } }


		[Constructable]
		public JackalsHelm()
		{
			Name = "Jackal's Helm";
			Hue = 0x6D1;
			Attributes.BonusDex = 20;
			Attributes.RegenHits = 5;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public JackalsHelm( Serial serial ) : base( serial )
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
				if ( Hue == 0x54B )
					Hue = 0x6D1;


				FireBonus = 0;
				ColdBonus = 0;
			}
		}
	}
}