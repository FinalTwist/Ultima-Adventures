using System;
using Server;


namespace Server.Items
{
	public class ConansHelm : PlateHelm
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		[Constructable]
		public ConansHelm()
		{
			ItemID = 0x2645;
			Hue = 0x835;
			Name = "Helm of the Cimmerian";
			Attributes.BonusStr = 5;
			SkillBonuses.SetValues( 0, SkillName.Swords, 5 );
			Attributes.DefendChance = 30;
			PhysicalBonus = 20;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
            list.Add( 1049644, "Conan's Lost Helm");
        } 


		public ConansHelm( Serial serial ) : base( serial )
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