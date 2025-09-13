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
	public class LethalPoisonPotion : BasePoisonPotion
	{
		public override Poison Poison{ get{ return Poison.Lethal; } }

		public override double MinPoisoningSkill{ get{ return 80.0; } }
		public override double MaxPoisoningSkill{ get{ return 120.0; } }

		[Constructable]
		public LethalPoisonPotion() : base( PotionEffect.PoisonLethal )
		{
			Name = "lethal poison potion";
			ItemID = 0x266A;
		}

		public LethalPoisonPotion( Serial serial ) : base( serial )
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