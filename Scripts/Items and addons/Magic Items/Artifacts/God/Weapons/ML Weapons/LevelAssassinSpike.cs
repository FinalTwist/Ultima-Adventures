using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2D21, 0x2D2D )]
	public class LevelAssassinSpike : BaseLevelKnife
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ShadowStrike; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.LightningStriker; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.MeleeProtection; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.FireStrike; } }

		public override int AosStrengthReq{ get{ return 15; } }
		public override int AosMinDamage{ get{ return 10; } }
		public override int AosMaxDamage{ get{ return 12; } }
		public override int AosSpeed{ get{ return 50; } }
		public override float MlSpeed{ get{ return 2.00f; } }

		public override int OldStrengthReq{ get{ return 15; } }
		public override int OldMinDamage{ get{ return 10; } }
		public override int OldMaxDamage{ get{ return 12; } }
		public override int OldSpeed{ get{ return 50; } }

		public override int DefMissSound{ get{ return 0x239; } }
		public override SkillName DefSkill { get { return SkillName.Fencing; } }

		public override int InitMinHits{ get{ return 30; } } // TODO
		public override int InitMaxHits{ get{ return 60; } } // TODO

		[Constructable]
		public LevelAssassinSpike() : base( 0x2D21 )
		{
			Name = "assassin dagger";
			Weight = 4.0;
		}

		public LevelAssassinSpike( Serial serial ) : base( serial )
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