using System;
using Server;


namespace Server.Items
{
	public class Windsong : MagicalShortbow
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1075031; } } // Windsong


		[Constructable]
		public Windsong() : base()
		{
			Hue = 0xAC;
			Name = "Windsong";
			Attributes.WeaponDamage = 35;
			WeaponAttributes.SelfRepair = 3;
			SkillBonuses.SetValues( 0, SkillName.Archery, 10 );
			AccuracyLevel = WeaponAccuracyLevel.Supremely;
			Attributes.AttackChance = 5;
			Velocity = 25;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public Windsong( Serial serial ) : base( serial )
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
