using System;
using Server;

namespace Server.Items
{
	public class DarkNinjaBoots : Boots
	{
		
		public override int ArtifactRarity{ get{ return 157; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public DarkNinjaBoots()
		{
                        ItemID = 0x170B;
                        Name = "Dark Ninja Boots";
			Hue = 0x345;
                        Attributes.BonusStr = 50;
			Attributes.WeaponDamage = 100;
                        Attributes.BonusInt = 50;
                        Attributes.BonusHits = 25;
			Attributes.ReflectPhysical = 65;
			Attributes.BonusDex = 50;
			Attributes.BonusStam = 25;
			Attributes.AttackChance = 55;
			Attributes.NightSight = 1;
			Attributes.SpellDamage = 15;
			Attributes.WeaponSpeed = 85;
     		}

		public DarkNinjaBoots( Serial serial ) : base( serial )
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



