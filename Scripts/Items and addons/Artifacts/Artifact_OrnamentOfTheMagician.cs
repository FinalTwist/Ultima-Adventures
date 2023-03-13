using System;
using Server;


namespace Server.Items
{
	public class OrnamentOfTheMagician : GoldBracelet
	{
		public override int LabelNumber{ get{ return 1061105; } } // Ornament of the Magician


		[Constructable]
		public OrnamentOfTheMagician()
		{
			Hue = 0x554;
			ItemID = 0x4CF0;
			Attributes.CastRecovery = 3;
			Attributes.CastSpeed = 2;
			Attributes.LowerManaCost = 10;
			Attributes.LowerRegCost = 20;
			Resistances.Energy = 15;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public OrnamentOfTheMagician( Serial serial ) : base( serial )
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