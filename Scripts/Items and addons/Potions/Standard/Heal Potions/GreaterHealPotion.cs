using System;
using Server;

namespace Server.Items
{
	public class GreaterHealPotion : BaseHealPotion
	{
		public override int MinHeal { get{ return 20; } }
		public override int MaxHeal { get{ return 25; } }
		public override double Delay { get{ return 10.0; } }

		[Constructable]
		public GreaterHealPotion() : base( PotionEffect.HealGreater )
		{
			ItemID = 0x25FE;
		}

		public GreaterHealPotion( Serial serial ) : base( serial )
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