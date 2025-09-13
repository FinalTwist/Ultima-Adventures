using System;
using Server.Network;
using Server.Items;


namespace Server.Items
{
	public class TalonBite : OrnateAxe
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1075029; } } // Talon Bite


		[Constructable]
		public TalonBite()
		{
			ItemID = 0x2D34;
			Hue = 0x47E;
			Name = "Talon Bite";


			SkillBonuses.SetValues( 0, SkillName.Tactics, 10.0 );


			Attributes.BonusDex = 8;
			Attributes.WeaponSpeed = 20;
			Attributes.WeaponDamage = 35;


			WeaponAttributes.HitHarm = 33;
			WeaponAttributes.UseBestSkill = 1;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public TalonBite( Serial serial ) : base( serial )
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