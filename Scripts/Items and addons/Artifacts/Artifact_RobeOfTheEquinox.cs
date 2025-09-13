using System;
using Server;


namespace Server.Items
{
	public class RobeOfTheEquinox : MagicRobe
	{
		[Constructable]
		public RobeOfTheEquinox()
		{
			ItemID = 0x1F04;
			Name = "Robe of the Equinox";
			Hue = 0xD6;
			//Attributes.Luck = 200;
			Resistances.Physical = 10;
			Attributes.CastRecovery = 2;
			Attributes.CastSpeed = 1;
			Attributes.LowerManaCost = 35;
			SkillBonuses.SetValues(0, SkillName.Magery, 20);
			SkillBonuses.SetValues(1, SkillName.EvalInt, 20);
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public RobeOfTheEquinox( Serial serial ) : base( serial )
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