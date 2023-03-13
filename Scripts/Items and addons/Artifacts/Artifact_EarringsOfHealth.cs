using System;
using Server;


namespace Server.Items
{
	public class EarringsOfHealth : GoldEarrings
	{
		public override int LabelNumber{ get{ return 1061103; } } // Earrings of Health


		[Constructable]
		public EarringsOfHealth()
		{
			Name = "Earrings of Health";
			Hue = 0x21;
			Attributes.BonusHits = 30;
			Attributes.RegenHits = 15;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public EarringsOfHealth( Serial serial ) : base( serial )
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