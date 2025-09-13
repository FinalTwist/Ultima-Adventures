using System;
using Server;

namespace Server.Items
{
	public class ZombieKillerPotion : BaseZombieKillerPotion
	{
		public override int MinDamage { get { return 300; } }
		public override int MaxDamage { get { return 2000; } }

		[Constructable]
		public ZombieKillerPotion() : base( PotionEffect.Explosion )
		{
			Name = "The Answer to Infection";
		}

		public ZombieKillerPotion( Serial serial ) : base( serial )
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