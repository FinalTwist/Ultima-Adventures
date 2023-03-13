using System;
using Server;


namespace Server.Items
{
	public class BraceletOfHealth : GoldBracelet
	{
		public override int LabelNumber{ get{ return 1061103; } } // Bracelet of Health


		[Constructable]
		public BraceletOfHealth()
		{
			Hue = 0x21;
			Attributes.BonusHits = 30;
			Attributes.RegenHits = 15;
			ItemID = 0x4CEC;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public BraceletOfHealth( Serial serial ) : base( serial )
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
		}
	}
}