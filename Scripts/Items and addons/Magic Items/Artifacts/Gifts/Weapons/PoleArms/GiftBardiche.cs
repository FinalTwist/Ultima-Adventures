using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0xF4D, 0xF4E )]
    public class GiftBardiche : BaseGiftPoleArm
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.PsychicAttack; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.DevastatingBlow; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.DoubleStrike; } }

		public override int AosStrengthReq{ get{ return 45; } }
		public override int AosMinDamage{ get{ return 17; } }
		public override int AosMaxDamage{ get{ return 18; } }
		public override int AosSpeed{ get{ return 28; } }
		public override float MlSpeed{ get{ return 3.75f; } }

		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 5; } }
		public override int OldMaxDamage{ get{ return 43; } }
		public override int OldSpeed{ get{ return 26; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 100; } }

		[Constructable]
		public GiftBardiche() : base( 0xF4D )
		{
			Weight = 7.0;
		}

		public GiftBardiche( Serial serial ) : base( serial )
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