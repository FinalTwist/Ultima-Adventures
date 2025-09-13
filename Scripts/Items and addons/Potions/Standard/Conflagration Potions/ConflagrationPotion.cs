using System;
using Server;

namespace Server.Items
{
	public class ConflagrationPotion : BaseConflagrationPotion
	{
		public override int MinDamage{ get{ return 10; } }
		public override int MaxDamage{ get{ return 20; } }

		public override int LabelNumber{ get{ return 1072095; } } // a Conflagration potion

		[Constructable]
		public ConflagrationPotion() : base( PotionEffect.Conflagration )
		{
			Name = "conflagration potion";
			Hue = Server.Items.PotionKeg.GetPotionColor( this );
		}

		public ConflagrationPotion( Serial serial ) : base( serial )
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
