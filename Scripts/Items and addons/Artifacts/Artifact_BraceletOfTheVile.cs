using System;
using Server;


namespace Server.Items
{
	public class BraceletOfTheVile : GoldBracelet
	{
		public override int LabelNumber{ get{ return 1061102; } } // Bracelet of the Vile


		[Constructable]
		public BraceletOfTheVile()
		{
			Name = "Bracelet of the Vile";
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


		public BraceletOfTheVile( Serial serial ) : base( serial )
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