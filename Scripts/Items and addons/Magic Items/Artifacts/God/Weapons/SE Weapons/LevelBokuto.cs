using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x27A8, 0x27F3 )]
    public class LevelBokuto : BaseLevelSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.Feint; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.NerveStrike; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.Bladeweave; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.DefenseMastery; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.FreezeStrike; } }

		public override int AosStrengthReq{ get{ return 20; } }
		public override int AosMinDamage{ get{ return 9; } }
		public override int AosMaxDamage{ get{ return 11; } }
		public override int AosSpeed{ get{ return 53; } }
		public override float MlSpeed{ get{ return 2.00f; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 9; } }
		public override int OldMaxDamage{ get{ return 11; } }
		public override int OldSpeed{ get{ return 53; } }

		public override int DefHitSound{ get{ return 0x536; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 25; } }
		public override int InitMaxHits{ get{ return 50; } }

		[Constructable]
		public LevelBokuto() : base( 0x27A8 )
		{
			Weight = 7.0;
			Resource = CraftResource.RegularWood;
		}

		public LevelBokuto( Serial serial ) : base( serial )
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