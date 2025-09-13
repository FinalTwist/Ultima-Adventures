using System;
using Server;

namespace Server.Items
{
	public class SilverSerpentVenom : BasePoisonPotion
	{
		public override Poison Poison{ get{ return Poison.Deadly; } }

		public override double MinPoisoningSkill{ get{ return 60.0; } }
		public override double MaxPoisoningSkill{ get{ return 100.0; } }

		[Constructable]
		public SilverSerpentVenom() : base( PotionEffect.PoisonDeadly )
		{
			Name = "silver serpent venom";
			Hue = 0x961;
			ItemID = 0x1FDD;
		}

		public SilverSerpentVenom( Serial serial ) : base( serial )
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