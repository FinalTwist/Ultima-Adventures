using System;
using Server;

namespace Server.Items
{
	public class GoldenSerpentVenom : BasePoisonPotion
	{
		public override Poison Poison{ get{ return Poison.Lethal; } }

		public override double MinPoisoningSkill{ get{ return 80.0; } }
		public override double MaxPoisoningSkill{ get{ return 120.0; } }

		[Constructable]
		public GoldenSerpentVenom() : base( PotionEffect.PoisonLethal )
		{
			Name = "golden serpent venom";
			Hue = 0x491;
			ItemID = 0x1FDD;
		}

		public GoldenSerpentVenom( Serial serial ) : base( serial )
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
			ItemID = 0x1FDD;
		}
	}
}