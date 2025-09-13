using System;
using Server;


namespace Server.Items
{
	public class RingOfHealth : GoldRing
	{
		public override int LabelNumber{ get{ return 1061103; } } // Ring of Health


		[Constructable]
		public RingOfHealth()
		{
			Name = "Ring of Health";
			Hue = 0x21;
			ItemID = 0x4CF8;
			Attributes.BonusHits = 30;
			Attributes.RegenHits = 15;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public RingOfHealth( Serial serial ) : base( serial )
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