using System;
using Server;

namespace Server.Items
{
	public class LesserExplosionPotion : BaseExplosionPotion
	{
		public override int MinDamage { get { return 30; } }
		public override int MaxDamage { get { return 40; } }

		[Constructable]
		public LesserExplosionPotion() : base( PotionEffect.ExplosionLesser )
		{
			ItemID = 0x2407;
		}

		public LesserExplosionPotion( Serial serial ) : base( serial )
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