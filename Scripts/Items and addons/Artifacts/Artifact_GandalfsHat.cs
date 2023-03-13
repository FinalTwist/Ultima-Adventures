using System;
using Server;


namespace Server.Items
{
	public class GandalfsHat : WizardsHat
	{
		[Constructable]
		public GandalfsHat()
		{
			Hue = 0xB89;
			Name = "Merlin's Mystical Hat";
			Attributes.LowerManaCost = 25;
			Attributes.LowerRegCost = 25;
			SkillBonuses.SetValues( 0, SkillName.EvalInt, 10 );
			SkillBonuses.SetValues( 1, SkillName.Magery, 10 );
			SkillBonuses.SetValues( 2, SkillName.MagicResist, 10 );
			SkillBonuses.SetValues( 3, SkillName.Meditation, 10 );
			Attributes.RegenMana = 10;
			Attributes.BonusInt = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public GandalfsHat( Serial serial ) : base( serial )
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