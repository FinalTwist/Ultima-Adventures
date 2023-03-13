using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0xEC4, 0xEC5 )]
	public class LevelSkinningKnife : BaseLevelKnife
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ShadowStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.Disrobe; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.ZapStrStrike; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.ForceOfNature; } }

		public override int AosStrengthReq{ get{ return 5; } }
		public override int AosMinDamage{ get{ return 9; } }
		public override int AosMaxDamage{ get{ return 11; } }
		public override int AosSpeed{ get{ return 49; } }
		public override float MlSpeed{ get{ return 2.25f; } }

		public override int OldStrengthReq{ get{ return 5; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 10; } }
		public override int OldSpeed{ get{ return 40; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 40; } }

		[Constructable]
		public LevelSkinningKnife() : base( 0xEC4 )
		{
			Weight = 1.0;
		}

		public LevelSkinningKnife( Serial serial ) : base( serial )
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