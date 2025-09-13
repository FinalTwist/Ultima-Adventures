using System;
using Server;


namespace Server.Items
{
	public class SinbadsSword : Cutlass
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		[Constructable]
		public SinbadsSword()
		{
			Hue = 0x491;
			Name = "Sword of Sinbad";
			Attributes.BonusDex = 10;
			SkillBonuses.SetValues( 0, SkillName.Cartography, 30 );
			SkillBonuses.SetValues( 1, SkillName.Fishing, 30 );
			SkillBonuses.SetValues( 2, SkillName.Lockpicking, 30 );
			Quality = WeaponQuality.Exceptional;
			AccuracyLevel = WeaponAccuracyLevel.Supremely;
			DamageLevel = WeaponDamageLevel.Vanq;
			Attributes.AttackChance = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public SinbadsSword( Serial serial ) : base( serial )
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