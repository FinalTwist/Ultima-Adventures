using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0xE87, 0xE88 )]
    public class LevelPitchfork : BaseLevelSpear
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.BleedAttack; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.ShadowStrike; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.AchillesStrike; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.StunningStrike; } }

		public override int AosStrengthReq{ get{ return 55; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 14; } }
		public override int AosSpeed{ get{ return 43; } }
		public override float MlSpeed{ get{ return 2.50f; } }

		public override int OldStrengthReq{ get{ return 15; } }
		public override int OldMinDamage{ get{ return 4; } }
		public override int OldMaxDamage{ get{ return 16; } }
		public override int OldSpeed{ get{ return 45; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

		[Constructable]
		public LevelPitchfork() : base( 0xE87 )
		{
			Name = "trident";
			Weight = 11.0;
			Hue = 0xB3A;
		}

		public override void OnLocationChange( Point3D oldLocation )
		{
			Server.Items.Pitchfork.IronColor( this );
		}

		public LevelPitchfork( Serial serial ) : base( serial )
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

			if ( Weight == 10.0 )
				Weight = 11.0;
		}
	}
}