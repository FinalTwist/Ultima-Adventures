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
	public class DeadlyPoisonPotion : BasePoisonPotion
	{
		public override Poison Poison{ get{ return Poison.Deadly; } }

		public override double MinPoisoningSkill{ get{ return 60.0; } }
		public override double MaxPoisoningSkill{ get{ return 100.0; } }

		[Constructable]
		public DeadlyPoisonPotion() : base( PotionEffect.PoisonDeadly )
		{
			Name = "deadly poison potion";
			ItemID = 0x2669;
		}

		public DeadlyPoisonPotion( Serial serial ) : base( serial )
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