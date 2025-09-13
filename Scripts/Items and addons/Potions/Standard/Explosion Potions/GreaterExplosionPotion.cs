using System;
using Server;

namespace Server.Items
{
	public class GreaterExplosionPotion : BaseExplosionPotion
	{
		public override int MinDamage { get { return 70; } }
		public override int MaxDamage { get { return 80; } }

		[Constructable]
		public GreaterExplosionPotion() : base( PotionEffect.ExplosionGreater )
		{
			ItemID = 0x2408;
		}

		public GreaterExplosionPotion( Serial serial ) : base( serial )
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