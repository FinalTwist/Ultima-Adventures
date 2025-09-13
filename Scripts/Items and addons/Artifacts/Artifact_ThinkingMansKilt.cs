using System;
using Server.Network;
using Server.Items;
using Server.Targeting;


namespace Server.Items
{
	public class ThinkingMansKilt : Kilt
	{

		[Constructable]
		public ThinkingMansKilt()
		{
          Name = "Thinking Man's Kilt";
		  Attributes.BonusInt = 15;
		  Attributes.BonusMana = 30;
		  Hue = 2117;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public ThinkingMansKilt( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();
		}
	}
}
