using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2D2B, 0x2D1F )]
    public class LevelMagicalShortbow : BaseLevelRanged
	{
		public override int EffectID{ get{ return 0xF42; } }
		public override Type AmmoType{ get{ return typeof( Arrow ); } }
		public override Item Ammo{ get{ return new Arrow(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.LightningArrow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.PsychicAttack; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.MovingShot; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.DoubleShot; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.InfectiousStrike; } }

		public override int AosStrengthReq{ get{ return 45; } }
		public override int AosMinDamage{ get{ return 9; } }
		public override int AosMaxDamage{ get{ return 13; } }
		public override int AosSpeed{ get{ return 38; } }
		public override float MlSpeed{ get{ return 3.00f; } }

		public override int OldStrengthReq{ get{ return 45; } }
		public override int OldMinDamage{ get{ return 9; } }
		public override int OldMaxDamage{ get{ return 13; } }
		public override int OldSpeed{ get{ return 38; } }

		public override int DefMaxRange{ get{ return 10; } }

		public override int InitMinHits{ get{ return 41; } }
		public override int InitMaxHits{ get{ return 90; } }

		[Constructable]
		public LevelMagicalShortbow() : base( 0x2D2B )
		{
			Name = "woodland shortbow";
			Weight = 6.0;
			Resource = CraftResource.RegularWood;
		}

		public LevelMagicalShortbow( Serial serial ) : base( serial )
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