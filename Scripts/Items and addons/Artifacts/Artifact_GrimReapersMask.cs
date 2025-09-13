using System;
using Server;
using Server.Misc;


namespace Server.Items
{
	public class GrimReapersMask : MagicHat
	{
		[Constructable]
		public GrimReapersMask()
		{
			Hue = 0x47E;
			ItemID = 0x1451;
			Name = "Grim Reaper's Mask";
			Resistances.Physical = 15;
			Resistances.Fire = 10;
			Resistances.Cold = 10;
			Resistances.Poison = 10;
			Resistances.Energy = 10;
			SkillBonuses.SetValues( 0, SkillName.Necromancy, 10 );
			SkillBonuses.SetValues( 1, SkillName.SpiritSpeak, 10 );
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public GrimReapersMask( Serial serial ) : base( serial )
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