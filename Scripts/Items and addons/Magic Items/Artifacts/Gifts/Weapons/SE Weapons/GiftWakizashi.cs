using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x27A4, 0x27EF )]
	public class GiftWakizashi : BaseGiftSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.FrenziedWhirlwind; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.EarthStrike; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.FireStrike; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.ZapStamStrike; } }

		public override int AosStrengthReq{ get{ return 20; } }
		public override int AosMinDamage{ get{ return 11; } }
		public override int AosMaxDamage{ get{ return 13; } }
		public override int AosSpeed{ get{ return 44; } }
		public override float MlSpeed{ get{ return 2.50f; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 11; } }
		public override int OldMaxDamage{ get{ return 13; } }
		public override int OldSpeed{ get{ return 44; } }

		public override int DefHitSound{ get{ return 0x23B; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 45; } }
		public override int InitMaxHits{ get{ return 50; } }

		[Constructable]
		public GiftWakizashi() : base( 0x27A4 )
		{
			Weight = 5.0;
			Layer = Layer.OneHanded;
		}

		public GiftWakizashi( Serial serial ) : base( serial )
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