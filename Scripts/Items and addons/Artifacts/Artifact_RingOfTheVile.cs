using System;
using Server;


namespace Server.Items
{
	public class RingOfTheVile : GoldRing
	{
		public override int LabelNumber{ get{ return 1061102; } } // Ring of the Vile


		[Constructable]
		public RingOfTheVile()
		{
			Name = "Ring Of The Vile";
			ItemID = 0x4CF3;
			Hue = 0x4F7;
			Attributes.BonusDex = 20;
			Attributes.RegenStam = 8;
			Attributes.AttackChance = 20;
			Resistances.Poison = 20;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public RingOfTheVile( Serial serial ) : base( serial )
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


			if ( Hue == 0x4F4 )
				Hue = 0x4F7;
		}
	}
}