using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x27AE, 0x27F9 )]
    public class LevelNunchaku : BaseLevelBashing
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.RidingSwipe; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.FireStrike; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.Bladeweave; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.ConsecratedStrike; } }

		public override int AosStrengthReq{ get{ return 15; } }
		public override int AosMinDamage{ get{ return 11; } }
		public override int AosMaxDamage{ get{ return 13; } }
		public override int AosSpeed{ get{ return 47; } }
		public override float MlSpeed{ get{ return 2.50f; } }

		public override int OldStrengthReq{ get{ return 15; } }
		public override int OldMinDamage{ get{ return 11; } }
		public override int OldMaxDamage{ get{ return 13; } }
		public override int OldSpeed{ get{ return 47; } }

		public override int DefHitSound{ get{ return 0x535; } }
		public override int DefMissSound{ get{ return 0x239; } }

		public override int InitMinHits{ get{ return 40; } }
		public override int InitMaxHits{ get{ return 55; } }

		[Constructable]
		public LevelNunchaku() : base( 0x27AE )
		{
			Weight = 5.0;
			Resource = CraftResource.RegularWood;
		}

		public LevelNunchaku( Serial serial ) : base( serial )
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