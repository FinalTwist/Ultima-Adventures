using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x26BE, 0x26C8 )]
    public class GiftPike : BaseGiftSpear
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.MagicProtection; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.StunningStrike; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.ForceOfNature; } }

		public override int AosStrengthReq{ get{ return 50; } }
		public override int AosMinDamage{ get{ return 14; } }
		public override int AosMaxDamage{ get{ return 16; } }
		public override int AosSpeed{ get{ return 37; } }
		public override float MlSpeed{ get{ return 3.00f; } }

		public override int OldStrengthReq{ get{ return 50; } }
		public override int OldMinDamage{ get{ return 14; } }
		public override int OldMaxDamage{ get{ return 16; } }
		public override int OldSpeed{ get{ return 37; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 110; } }

		[Constructable]
		public GiftPike() : base( 0x26BE )
		{
			Weight = 8.0;
		}

		public GiftPike( Serial serial ) : base( serial )
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