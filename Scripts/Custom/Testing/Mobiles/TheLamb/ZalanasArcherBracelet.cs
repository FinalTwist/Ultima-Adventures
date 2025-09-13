//=================================================
//This script was created by Zalana
//This script was created on 1/1/2013 8:23:37 PM
//=================================================

using System;
using Server;

namespace Server.Items
{
	public class ZalanasArcherBracelet : SilverBracelet
	{
		public override int ArtifactRarity{ get{ return 600; } }

		[Constructable]
		public ZalanasArcherBracelet()
		{
			Name = "Zalanas Archer Bracelet";
			Hue = 1157;
			Weight = 0;
			SkillBonuses.SetValues( 0, SkillName.Archery, 5.0 );
			Attributes.AttackChance = 25;
			Attributes.BonusStam = 20;
			Attributes.Luck = 100;
			Attributes.WeaponDamage = 25;
			Attributes.WeaponSpeed = 25;
			Attributes.RegenStam = 2;
		}

		public ZalanasArcherBracelet( Serial serial ) : base( serial )
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
