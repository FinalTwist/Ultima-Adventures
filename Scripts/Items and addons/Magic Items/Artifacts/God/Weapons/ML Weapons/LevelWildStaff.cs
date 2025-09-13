using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2D25, 0x2D31 )]
	public class LevelWildStaff : BaseLevelStaff
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.Block; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ForceOfNature; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.MagicProtection; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.FreezeStrike; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.PsychicAttack; } }

		public override int AosStrengthReq{ get{ return 15; } }
		public override int AosMinDamage{ get{ return 10; } }
		public override int AosMaxDamage{ get{ return 12; } }
		public override int AosSpeed{ get{ return 48; } }
		public override float MlSpeed{ get{ return 2.25f; } }

		public override int OldStrengthReq{ get{ return 15; } }
		public override int OldMinDamage{ get{ return 10; } }
		public override int OldMaxDamage{ get{ return 12; } }
		public override int OldSpeed{ get{ return 48; } }

		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 60; } }

		[Constructable]
		public LevelWildStaff() : base( 0x2D25 )
		{
			Name = "druid staff";
			Weight = 6.0;
			Resource = CraftResource.RegularWood;
		}

		public LevelWildStaff( Serial serial ) : base( serial )
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