using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2D1E, 0x2D2A )]
	public class LevelElvenCompositeLongbow : BaseLevelRanged
	{
		public override int EffectID{ get{ return 0xF42; } }
		public override Type AmmoType{ get{ return typeof( Arrow ); } }
		public override Item Ammo{ get{ return new Arrow(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ForceArrow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.SerpentArrow; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.ZapIntStrike; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.DoubleShot; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.ZapStamStrike; } }

		public override int AosStrengthReq{ get{ return 45; } }
		public override int AosMinDamage{ get{ return 12; } }
		public override int AosMaxDamage{ get{ return 16; } }
		public override int AosSpeed{ get{ return 27; } }
		public override float MlSpeed{ get{ return 4.00f; } }

		public override int OldStrengthReq{ get{ return 45; } }
		public override int OldMinDamage{ get{ return 12; } }
		public override int OldMaxDamage{ get{ return 16; } }
		public override int OldSpeed{ get{ return 27; } }

		public override int DefMaxRange{ get{ return 10; } }

		public override int InitMinHits{ get{ return 41; } }
		public override int InitMaxHits{ get{ return 90; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public LevelElvenCompositeLongbow() : base( 0x2D1E )
		{
			Weight = 5.0;
			Name = "woodland longbow";
			Resource = CraftResource.RegularWood;
		}

		public LevelElvenCompositeLongbow( Serial serial ) : base( serial )
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