using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x27A9, 0x27F4 )]
    public class GiftDaisho : BaseGiftSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.Feint; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.ZapStrStrike; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.NerveStrike; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.MeleeProtection2; } }

		public override int AosStrengthReq{ get{ return 40; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 15; } }
		public override int AosSpeed{ get{ return 40; } }
		public override float MlSpeed{ get{ return 2.75f; } }

		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 13; } }
		public override int OldMaxDamage{ get{ return 15; } }
		public override int OldSpeed{ get{ return 40; } }

		public override int DefHitSound{ get{ return 0x23B; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 45; } }
		public override int InitMaxHits{ get{ return 65; } }

		[Constructable]
		public GiftDaisho() : base( 0x27A9 )
		{
			Weight = 8.0;
			Layer = Layer.TwoHanded;
		}

		public GiftDaisho( Serial serial ) : base( serial )
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