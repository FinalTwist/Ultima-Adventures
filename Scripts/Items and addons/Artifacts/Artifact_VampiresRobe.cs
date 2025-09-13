using System;
using Server;


namespace Server.Items
{
	public class VampiresRobe : Robe
	{
      [Constructable]
		public VampiresRobe()
		{
			Name = "Vampire's Robe";
			Hue = 0x497;
			ItemID = 0x2687;
			Attributes.BonusHits = 50;
			Attributes.BonusStr = 10;
			Attributes.RegenHits = 5;
			SkillBonuses.SetValues( 0, SkillName.SpiritSpeak, 20 );
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public VampiresRobe( Serial serial ) : base( serial )
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