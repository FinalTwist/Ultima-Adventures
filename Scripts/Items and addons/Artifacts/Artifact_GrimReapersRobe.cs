using System;
using Server;
using Server.Misc;


namespace Server.Items
{
	public class GrimReapersRobe : MagicRobe
	{
		[Constructable]
		public GrimReapersRobe()
		{
			ItemID = 0x1F03;
			Name = "Grim Reaper's Robe";
			Hue = 1;
			Attributes.ReflectPhysical = 20;
			SkillBonuses.SetValues( 0, SkillName.Necromancy, 30 );
			SkillBonuses.SetValues( 1, SkillName.SpiritSpeak, 30 );
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public GrimReapersRobe( Serial serial ) : base( serial )
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