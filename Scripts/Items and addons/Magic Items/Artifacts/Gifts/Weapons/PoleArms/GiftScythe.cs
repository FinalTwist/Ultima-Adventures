using System;
using Server.Network;
using Server.Items;
using Server.Engines.Harvest;

namespace Server.Items
{
	[FlipableAttribute( 0x26BA, 0x26C4 )]
    public class GiftScythe : BaseGiftPoleArm
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.BleedAttack; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.NerveStrike; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.ElementalStrike; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.LightningStriker; } }

		public override int AosStrengthReq{ get{ return 45; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 18; } }
		public override int AosSpeed{ get{ return 32; } }
		public override float MlSpeed{ get{ return 3.50f; } }

		public override int OldStrengthReq{ get{ return 45; } }
		public override int OldMinDamage{ get{ return 15; } }
		public override int OldMaxDamage{ get{ return 18; } }
		public override int OldSpeed{ get{ return 32; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 100; } }

		public override HarvestSystem HarvestSystem{ get{ return null; } }

		[Constructable]
		public GiftScythe() : base( 0x26BA )
		{
			Weight = 5.0;
		}

		public GiftScythe( Serial serial ) : base( serial )
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

			if ( Weight == 15.0 )
				Weight = 5.0;
		}
	}
}