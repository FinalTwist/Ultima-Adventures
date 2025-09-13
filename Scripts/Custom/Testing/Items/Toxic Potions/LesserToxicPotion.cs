using System;
using Server;

namespace Server.Items
{
	public class LesserToxicPotion : BaseToxicPotion
	{
		public override Poison Poison{ get{ return Poison.Lesser; } }

		public override int MinDamage { get { return 2; } }
		public override int MaxDamage { get { return 5; } }

		[Constructable]
		public LesserToxicPotion() : base( PotionEffect.PoisonLesser )
		{
                        Name = "Lesser Toxic Potion";
			Hue = 64;
		}

		public LesserToxicPotion( Serial serial ) : base( serial )
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