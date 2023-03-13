using System;
using Server;


namespace Server.Items
{
	public class HolyKnightsGloves : RoyalGloves
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061097; } } // Holy Knight's Gloves


		public override int BasePhysicalResistance{ get{ return 5; } }


		[Constructable]
		public HolyKnightsGloves()
		{
			Name = "Holy Knight's Gloves";
			Hue = 0x47E;
			Attributes.BonusHits = 50;
			Attributes.ReflectPhysical = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public HolyKnightsGloves( Serial serial ) : base( serial )
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
				PhysicalBonus = 0;
		}
	}
}