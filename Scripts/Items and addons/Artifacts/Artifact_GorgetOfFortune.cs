using System;
using Server;


namespace Server.Items
{
	public class GorgetOfFortune : StuddedGorget
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061098; } } // Gorget of Fortune


		[Constructable]
		public GorgetOfFortune()
		{
			Name = "Gorget of Fortune";
			Hue = 0x501;
			Attributes.Luck = 350;
			Attributes.DefendChance = 10;
			Attributes.LowerRegCost = 15;
			ArmorAttributes.MageArmor = 1;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public GorgetOfFortune( Serial serial ) : base( serial )
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