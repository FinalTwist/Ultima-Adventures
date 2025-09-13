using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x26BC, 0x26C6 )]
    public class LevelScepter : BaseLevelBashing
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.RidingAttack; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.SpinAttack; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.FreezeStrike; } }

		public override int AosStrengthReq{ get{ return 40; } }
		public override int AosMinDamage{ get{ return 14; } }
		public override int AosMaxDamage{ get{ return 17; } }
		public override int AosSpeed{ get{ return 30; } }
		public override float MlSpeed{ get{ return 3.50f; } }

		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 14; } }
		public override int OldMaxDamage{ get{ return 17; } }
		public override int OldSpeed{ get{ return 30; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 110; } }

		[Constructable]
		public LevelScepter() : base( 0x26BC )
		{
			Weight = 8.0;
		}

		public LevelScepter( Serial serial ) : base( serial )
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