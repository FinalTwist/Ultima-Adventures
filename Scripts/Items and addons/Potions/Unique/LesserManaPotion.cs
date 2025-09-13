using System;
using Server;

namespace Server.Items
{
	public class LesserManaPotion : BaseManaRefreshPotion
	{
		public override int MinMana { get{ return 6; } }
		public override int MaxMana { get{ return 8; } }
		public override double Delay { get{ return 3.0; } }

		[Constructable]
		public LesserManaPotion( ) : base( PotionEffect.ManaLesser )
		{
			Name = "lesser mana potion";
			ItemID = 0x23BD;
			Hue = Server.Items.PotionKeg.GetPotionColor( this );
		}

		public LesserManaPotion( Serial serial ) : base( serial )
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
