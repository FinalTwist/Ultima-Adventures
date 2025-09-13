using System;
using Server;

namespace Server.Items
{
	public class GreaterRejuvenatePotion : BaseRejuvenatePotion
	{
		public override int MinRejuv { get{ return 20; } }
		public override int MaxRejuv { get{ return 25; } }
		public override double Delay { get{ return 10.0; } }

		[Constructable]
		public GreaterRejuvenatePotion( ) : base( PotionEffect.RejuvenateGreater )
		{
			Name = "greater rejuvenate potion";
			ItemID = 0x2406;
			Hue = Server.Items.PotionKeg.GetPotionColor( this );
		}

		public GreaterRejuvenatePotion( Serial serial ) : base( serial )
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