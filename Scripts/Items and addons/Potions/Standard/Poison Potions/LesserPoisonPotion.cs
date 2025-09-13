using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;
using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
	public class LesserPoisonPotion : BasePoisonPotion
	{
		public override Poison Poison{ get{ return Poison.Lesser; } }

		public override double MinPoisoningSkill{ get{ return 0.0; } }
		public override double MaxPoisoningSkill{ get{ return 40.0; } }

		[Constructable]
		public LesserPoisonPotion() : base( PotionEffect.PoisonLesser )
		{
			Name = "lesser poison potion";
			ItemID = 0x2600;
		}

		public LesserPoisonPotion( Serial serial ) : base( serial )
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