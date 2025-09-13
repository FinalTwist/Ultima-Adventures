using System;
using Server;

namespace Server.Items
{
	public class GreaterFrostbitePotion : BaseFrostbitePotion
	{
		public override int MinDamage{ get{ return 20; } }
		public override int MaxDamage{ get{ return 30; } }

		[Constructable]
		public GreaterFrostbitePotion() : base( PotionEffect.FrostbiteGreater )
		{
			Name = "greater frostbite potion";
			ItemID = 0x2406;
			Hue = Server.Items.PotionKeg.GetPotionColor( this );
		}

		public GreaterFrostbitePotion( Serial serial ) : base( serial )
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
