using System;
using Server;


namespace Server.Items
{
	public class ShroudOfDeciet : BoneChest
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int BasePhysicalResistance{ get{ return 11; } }
		public override int BaseFireResistance{ get{ return 6; } }
		public override int BaseColdResistance{ get{ return 18; } }
		public override int BasePoisonResistance{ get{ return 15; } }
		public override int BaseEnergyResistance{ get{ return 13; } }


		public override bool CanFortify{ get{ return true; } }


		[Constructable]
		public ShroudOfDeciet()
		{
			Name = "Shroud of Deceit";
			Hue = 0x38F;


			Attributes.RegenHits = 3;


			ArmorAttributes.MageArmor = 1;


			SkillBonuses.Skill_1_Name = SkillName.MagicResist;
			SkillBonuses.Skill_1_Value = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public ShroudOfDeciet( Serial serial ) : base( serial )
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
