using System;
using Server;


namespace Server.Items
{
	public class BloodwoodSpirit : MagicTalisman
	{
		public override int LabelNumber{ get{ return 1075034; } } // Bloodwood Spirit


		[Constructable]
		public BloodwoodSpirit()
		{
			Name = "Bloodwood Spirit";
			ItemID = 0x2C95;
			Hue = 0x27;
			SkillBonuses.SetValues( 0, SkillName.SpiritSpeak, 15.0 );
			SkillBonuses.SetValues( 1, SkillName.Necromancy, 30.0 );
			Attributes.BonusStr = 15;
			Attributes.BonusHits = 25;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public BloodwoodSpirit( Serial serial ) :  base( serial )
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
