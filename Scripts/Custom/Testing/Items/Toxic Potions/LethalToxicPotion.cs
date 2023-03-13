using System;
using Server;

namespace Server.Items
{
	public class LethalToxicPotion : BaseToxicPotion
	{
		public override Poison Poison{ get{ return Poison.Lethal; } }

		public override int MinDamage { get { return 6; } }
		public override int MaxDamage { get { return 9; } }

		[Constructable]
		public LethalToxicPotion() : base( PotionEffect.PoisonDeadly )
		{
                        Name = "Lethal Toxic Potion";
			Hue = 64;
		}

		public LethalToxicPotion( Serial serial ) : base( serial )
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