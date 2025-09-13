using System;
using Server;

namespace Server.Items
{
	public class CaptainJackSparrowsCutlass : Cutlass
	{
		public override int LabelNumber{ get{ return 1063474; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }
		public override float MlSpeed{ get{ return 2.50f; } }

		[Constructable]
		public CaptainJackSparrowsCutlass()
		{
			Hue = 0x5C;
			Attributes.BonusDex = 5;
			Attributes.AttackChance = 20;
			Attributes.WeaponSpeed = 20;
			Attributes.WeaponDamage = 50;
			WeaponAttributes.UseBestSkill = 1;
		}

		public CaptainJackSparrowsCutlass( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if( Attributes.AttackChance == 50 )
				Attributes.AttackChance = 10;
		}
	}
}