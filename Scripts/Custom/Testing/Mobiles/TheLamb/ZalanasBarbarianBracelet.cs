//=================================================
//This script was created by Zalana
//This script was created on 1/1/2013 8:18:10 PM
//=================================================

using System;
using Server;

namespace Server.Items
{
	public class ZalanasBarbarianBracelet : SilverEarrings
	{
		public override int ArtifactRarity{ get{ return 600; } }

		[Constructable]
		public ZalanasBarbarianBracelet()
		{
			Name = "Zalanas Barbarian Bracelet";
			Hue = 1157;
			Weight = 0;
			SkillBonuses.SetValues( 0, SkillName.Parry, 5.0 );
			Attributes.AttackChance = 25;
			Attributes.DefendChance = 25;
			Attributes.BonusStr = 20;
			Attributes.WeaponDamage = 25;
			Attributes.WeaponSpeed = 25;
			Attributes.RegenHits = 2;
		}

		public ZalanasBarbarianBracelet( Serial serial ) : base( serial )
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
