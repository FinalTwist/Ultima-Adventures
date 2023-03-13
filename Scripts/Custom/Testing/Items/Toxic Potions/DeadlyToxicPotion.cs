using System;
using Server;

namespace Server.Items
{
	public class DeadlyToxicPotion : BaseToxicPotion
	{
		public override Poison Poison{ get{ return Poison.Deadly; } }

		public override int MinDamage { get { return 5; } }
		public override int MaxDamage { get { return 8; } }

		[Constructable]
		public DeadlyToxicPotion() : base( PotionEffect.PoisonDeadly )
		{
                        Name = "Deadly Toxic Potion";
			Hue = 64;
		}

		public DeadlyToxicPotion( Serial serial ) : base( serial )
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