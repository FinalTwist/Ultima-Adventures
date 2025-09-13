using System;
using Server;


namespace Server.Items
{
	public class RobeOfTheEclipse : MagicRobe
	{
		[Constructable]
		public RobeOfTheEclipse()
		{
			ItemID = 0x1F04;
			Name = "Robe of the Eclipse";
			Hue = 0x486;
			//Attributes.Luck = 200;
			Resistances.Physical = 10;
			Attributes.CastRecovery = 2;
			Attributes.CastSpeed = 1;
			Attributes.LowerManaCost = 25;
			SkillBonuses.SetValues(0, SkillName.Chivalry, 20);
			Attributes.DefendChance = 10;
			//SkillBonuses.SetValues(0, SkillName.Necromancy, 20); //grim reapersrobe already does this
			//SkillBonuses.SetValues(1, SkillName.SpiritSpeak, 10);
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public RobeOfTheEclipse( Serial serial ) : base( serial )
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