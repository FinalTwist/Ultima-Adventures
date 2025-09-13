using System;
using Server;

namespace Server.Items
{
	public class HolySword : Longsword
	{
		public override int LabelNumber{ get{ return 1062921; } } // The Holy Sword

		[Constructable]
		public HolySword()
		{
			Hue = 0x482;
			Slayer = SlayerName.Silver;
			Attributes.WeaponDamage = 40;
			WeaponAttributes.SelfRepair = 5;
			WeaponAttributes.LowerStatReq = 100;
			WeaponAttributes.UseBestSkill = 1;
		}

		public HolySword( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}