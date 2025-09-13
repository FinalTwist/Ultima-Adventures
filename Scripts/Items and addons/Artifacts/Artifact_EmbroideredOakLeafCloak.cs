using System;
using Server;


namespace Server.Items
{
	public class EmbroideredOakLeafCloak : BaseOuterTorso
	{
		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }


		public override bool CanFortify{ get{ return true; } }


		[Constructable]
		public EmbroideredOakLeafCloak() : base( 0x2684 )
		{
			Name = "Embroidered Oak Leaf Robe";
			Hue = 0x483;
			StrRequirement = 0;
			

			Attributes.SpellDamage = 25;
			Attributes.RegenMana = 10;
			SkillBonuses.Skill_1_Name = SkillName.Stealth;
			SkillBonuses.Skill_1_Value = 60;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public EmbroideredOakLeafCloak( Serial serial ) : base( serial )
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
