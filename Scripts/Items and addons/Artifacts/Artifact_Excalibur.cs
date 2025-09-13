using System;
using Server;


namespace Server.Items
{
	public class Excalibur : VikingSword
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		[Constructable]
		public Excalibur()
		{
			Hue = 0x835;
			Name = "Excalibur";
			Attributes.BonusStr = 10;
			SkillBonuses.SetValues( 0, SkillName.Chivalry, 20 );
			AccuracyLevel = WeaponAccuracyLevel.Supremely;
			DamageLevel = WeaponDamageLevel.Vanq;
			Attributes.AttackChance = 10;
            Slayer = SlayerName.Silver;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
            list.Add( 1049644, "King Arthur's Lost Sword");
        }


		public Excalibur( Serial serial ) : base( serial )
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