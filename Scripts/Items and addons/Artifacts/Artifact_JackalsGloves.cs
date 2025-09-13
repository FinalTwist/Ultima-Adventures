using System;
using Server;


namespace Server.Items
{
	public class JackalsGloves : PlateGloves
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061594; } } // Jackal's Gloves


		public override int BaseFireResistance{ get{ return 13; } }
		public override int BaseColdResistance{ get{ return 9; } }


		[Constructable]
		public JackalsGloves()
		{
			Name = "Jackal's Gloves";
			Hue = 0x6D1;
			Attributes.BonusDex = 20;
			Attributes.RegenHits = 5;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public JackalsGloves( Serial serial ) : base( serial )
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