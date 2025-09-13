using System;
using Server;

namespace Server.Items
{
	public class GreaterConflagrationPotion : BaseConflagrationPotion
	{
		public override int MinDamage{ get{ return 20; } }
		public override int MaxDamage{ get{ return 30; } }

		public override int LabelNumber{ get{ return 1072098; } } // a Greater Conflagration potion

		[Constructable]
		public GreaterConflagrationPotion() : base( PotionEffect.ConflagrationGreater )
		{
			Name = "greater conflagration potion";
			ItemID = 0x2406;
			Hue = Server.Items.PotionKeg.GetPotionColor( this );
		}

		public GreaterConflagrationPotion( Serial serial ) : base( serial )
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
