using System;
using Server;

namespace Server.Items
{
	public class GreaterToxicPotion : BaseToxicPotion
	{
		public override Poison Poison{ get{ return Poison.Greater; } }

		public override int MinDamage { get { return 4; } }
		public override int MaxDamage { get { return 7; } }

		[Constructable]
		public GreaterToxicPotion() : base( PotionEffect.PoisonGreater )
		{
                        Name = "Greater Toxic Potion";
			Hue = 64;
		}

		public GreaterToxicPotion( Serial serial ) : base( serial )
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