using System;
using Server;

namespace Server.Items
{
	public class GreaterStrengthPotion : BaseStrengthPotion
	{
		public override int StrOffset{ get{ return 20; } }
		public override TimeSpan Duration{ get{ return TimeSpan.FromMinutes( 2.0 ); } }

		[Constructable]
		public GreaterStrengthPotion() : base( PotionEffect.StrengthGreater )
		{
			ItemID = 0x25F7;
		}

		public GreaterStrengthPotion( Serial serial ) : base( serial )
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