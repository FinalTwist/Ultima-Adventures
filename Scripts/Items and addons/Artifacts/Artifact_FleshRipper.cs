using System;
using Server.Network;
using Server.Items;


namespace Server.Items
{
	public class FleshRipper : AssassinSpike
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1075045; } } // Flesh Ripper


		[Constructable]
		public FleshRipper()
		{
			Name = "Flesh Ripper";
			Hue = 0x341;


			SkillBonuses.SetValues( 0, SkillName.Anatomy, 10.0 );


			Attributes.BonusStr = 5;
			Attributes.AttackChance = 15;
			Attributes.WeaponSpeed = 40;


			WeaponAttributes.UseBestSkill = 1;
			Slayer = SlayerName.WizardSlayer;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public FleshRipper( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );


			writer.WriteEncodedInt( 0 ); // version
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );


			int version = reader.ReadEncodedInt();
		}
	}
}