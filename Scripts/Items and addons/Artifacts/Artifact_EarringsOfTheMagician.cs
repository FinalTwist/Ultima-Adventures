using System;
using Server;


namespace Server.Items
{
	public class EarringsOfTheMagician : GoldEarrings
	{
		public override int LabelNumber{ get{ return 1061105; } } // Earrings of the Magician


		[Constructable]
		public EarringsOfTheMagician()
		{
			Name = "Earrings of the Magician";
			Hue = 0x554;
			Attributes.CastRecovery = 2;
			Attributes.CastSpeed = 1;
			Attributes.LowerManaCost = 25;
			Attributes.LowerRegCost = 25;
			Resistances.Energy = 15;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public EarringsOfTheMagician( Serial serial ) : base( serial )
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


			if ( Hue == 0x12B )
				Hue = 0x554;
		}
	}
}