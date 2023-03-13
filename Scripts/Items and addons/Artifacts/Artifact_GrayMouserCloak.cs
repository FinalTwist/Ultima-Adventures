using System;
using Server;


namespace Server.Items
{
	public class GrayMouserCloak : GoldRing
	{
		[Constructable]
		public GrayMouserCloak()
		{
			ItemID = 0x1515;
			Name = "Cloak of the Rogue";
			Hue = 0x967;
			Layer = Layer.Cloak;
			Weight = 5.0;
			Attributes.BonusDex = 10;
			SkillBonuses.SetValues( 0, SkillName.Stealing, 25 );
			SkillBonuses.SetValues( 1, SkillName.Snooping, 25 );
			SkillBonuses.SetValues( 2, SkillName.RemoveTrap, 80 );
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
            list.Add( 1049644, "Gray Mouser's Cloak");
        }


		public GrayMouserCloak( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}