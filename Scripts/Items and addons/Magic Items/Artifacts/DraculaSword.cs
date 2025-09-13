using System;
using Server;

namespace Server.Items
{
	public class DraculaSword : RoyalSword
	{
		public override int InitMinHits{ get{ return 40; } }
		public override int InitMaxHits{ get{ return 80; } }

		[Constructable]
		public DraculaSword()
		{
			Hue = 0x497;
			Name = "Dracula's Sword";
			Attributes.BonusStr = 5;
			SkillBonuses.SetValues( 0, SkillName.Swords, 10 );
			AccuracyLevel = WeaponAccuracyLevel.Supremely;
			DamageLevel = WeaponDamageLevel.Vanq;
			Attributes.AttackChance = 10;
			WeaponAttributes.HitLeechHits = 50;
		}

		public DraculaSword( Serial serial ) : base( serial )
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