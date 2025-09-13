using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x27A6, 0x27F1 )]
    public class LevelTetsubo : BaseLevelBashing
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.FrenziedWhirlwind; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.ZapManaStrike; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.RidingAttack; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.ElementalStrike; } }

		public override int AosStrengthReq{ get{ return 35; } }
		public override int AosMinDamage{ get{ return 12; } }
		public override int AosMaxDamage{ get{ return 14; } }
		public override int AosSpeed{ get{ return 45; } }
		public override float MlSpeed{ get{ return 2.50f; } }

		public override int OldStrengthReq{ get{ return 35; } }
		public override int OldMinDamage{ get{ return 12; } }
		public override int OldMaxDamage{ get{ return 14; } }
		public override int OldSpeed{ get{ return 45; } }

		public override int DefHitSound{ get{ return 0x233; } }
		public override int DefMissSound{ get{ return 0x238; } }

		public override int InitMinHits{ get{ return 60; } }
		public override int InitMaxHits{ get{ return 65; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Bash2H; } }

		[Constructable]
		public LevelTetsubo() : base( 0x27A6 )
		{
			Weight = 8.0;
			Layer = Layer.TwoHanded;
		}

		public LevelTetsubo( Serial serial ) : base( serial )
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