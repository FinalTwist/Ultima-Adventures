using System;
using Server;


namespace Server.Items
{
	public class GiantBlackjack : Club
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		[Constructable]
		public GiantBlackjack()
		{
			Hue = 0x497;
			Name = "Giant Blackjack";
			Attributes.BonusStr = 10;
			SkillBonuses.SetValues( 0, SkillName.Macing, 20 );
			AccuracyLevel = WeaponAccuracyLevel.Supremely;
			DamageLevel = WeaponDamageLevel.Vanq;
			Attributes.AttackChance = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public GiantBlackjack( Serial serial ) : base( serial )
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