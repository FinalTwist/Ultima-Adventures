using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2D33, 0x2D27 )]
    public class GiftRadiantScimitar : BaseGiftSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.WhirlwindAttack; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Bladeweave; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.DefenseMastery; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.ConsecratedStrike; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.FireStrike; } }

		public override int AosStrengthReq{ get{ return 20; } }
		public override int AosMinDamage{ get{ return 12; } }
		public override int AosMaxDamage{ get{ return 14; } }
		public override int AosSpeed{ get{ return 43; } }
		public override float MlSpeed{ get{ return 2.50f; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 12; } }
		public override int OldMaxDamage{ get{ return 14; } }
		public override int OldSpeed{ get{ return 43; } }

		public override int DefHitSound{ get{ return 0x23B; } }
		public override int DefMissSound{ get{ return 0x239; } }

		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 60; } }

		[Constructable]
		public GiftRadiantScimitar() : base( 0x2D33 )
		{
			Name = "falchion";
			Weight = 9.0;
		}

		public GiftRadiantScimitar( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}