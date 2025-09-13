using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x27Ab, 0x27F6 )]
    public class LevelTekagi : BaseLevelKnife
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DualWield; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.TalonStrike; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.FreezeStrike; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.DevastatingBlow; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.ConsecratedStrike; } }

		public override int AosStrengthReq{ get{ return 10; } }
		public override int AosMinDamage{ get{ return 10; } }
		public override int AosMaxDamage{ get{ return 12; } }
		public override int AosSpeed{ get{ return 53; } }
		public override float MlSpeed{ get{ return 2.00f; } }

		public override int OldStrengthReq{ get{ return 10; } }
		public override int OldMinDamage{ get{ return 10; } }
		public override int OldMaxDamage{ get{ return 12; } }
		public override int OldSpeed{ get{ return 53; } }

		public override int DefHitSound{ get{ return 0x238; } }
		public override int DefMissSound{ get{ return 0x232; } }

		public override int InitMinHits{ get{ return 35; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override SkillName DefSkill{ get{ return SkillName.Fencing; } }
		public override WeaponType DefType{ get{ return WeaponType.Piercing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce1H; } }

		[Constructable]
		public LevelTekagi() : base( 0x27AB )
		{
			Weight = 5.0;
			Layer = Layer.TwoHanded;
		}

		public LevelTekagi( Serial serial ) : base( serial )
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