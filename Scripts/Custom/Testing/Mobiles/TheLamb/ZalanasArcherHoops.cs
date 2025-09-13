//=================================================
//This script was created by Zalana
//This script was created on 1/1/2013 8:24:05 PM
//=================================================

using System;
using Server;

namespace Server.Items
{
	public class ZalanasArcherHoops : SilverEarrings
	{
		public override int ArtifactRarity{ get{ return 600; } }

		[Constructable]
		public ZalanasArcherHoops()
		{
			Name = "Zalanas Archer Hoops";
			Hue = 1157;
			Weight = 0;
			SkillBonuses.SetValues( 0, SkillName.Archery, 5.0 );
			Attributes.AttackChance = 25;
			Attributes.BonusDex = 20;
			Attributes.Luck = 100;
			Attributes.WeaponDamage = 25;
			Attributes.WeaponSpeed = 25;
			Attributes.RegenStam = 2;
		}

		public ZalanasArcherHoops( Serial serial ) : base( serial )
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
