using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2D32, 0x2D26 )]
    public class LevelRuneBlade : BaseLevelSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.Disarm; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Bladeweave; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.TalonStrike; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.ForceOfNature; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.ToxicStrike; } }

		public override int AosStrengthReq{ get{ return 30; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 17; } }
		public override int AosSpeed{ get{ return 35; } }
		public override float MlSpeed{ get{ return 3.00f; } }

		public override int OldStrengthReq{ get{ return 30; } }
		public override int OldMinDamage{ get{ return 15; } }
		public override int OldMaxDamage{ get{ return 17; } }
		public override int OldSpeed{ get{ return 35; } }

		public override int DefHitSound{ get{ return 0x23B; } }
		public override int DefMissSound{ get{ return 0x239; } }

		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 60; } }

		[Constructable]
		public LevelRuneBlade() : base( 0x2D32 )
		{
			Name = "war blades";
			Weight = 5.0;
			Layer = Layer.TwoHanded;
		}

		public LevelRuneBlade( Serial serial ) : base( serial )
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