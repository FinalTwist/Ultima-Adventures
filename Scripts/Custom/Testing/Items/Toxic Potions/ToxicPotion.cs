using System;
using Server;

namespace Server.Items
{
	public class ToxicPotion : BaseToxicPotion
	{
		public override Poison Poison{ get{ return Poison.Regular; } }

		public override int MinDamage { get { return 3; } }
		public override int MaxDamage { get { return 6; } }

		[Constructable]
		public ToxicPotion() : base( PotionEffect.Poison )
		{
                        Name = "Toxic Potion";
			Hue = 64;
		}

		public ToxicPotion( Serial serial ) : base( serial )
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