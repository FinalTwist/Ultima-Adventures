using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2D28, 0x2D34 )]
	public class OrnateAxe : BaseAxe
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.Disarm; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.ShadowInfectiousStrike; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.TalonStrike; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.LightningStriker; } }

		public override int AosStrengthReq{ get{ return 80; } }
		public override int AosMinDamage{ get{ return 16; } }
		public override int AosMaxDamage{ get{ return 17; } }
		public override int AosSpeed{ get{ return 29; } }
		public override float MlSpeed{ get{ return 3.75f; } }

		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 6; } }
		public override int OldMaxDamage{ get{ return 38; } }
		public override int OldSpeed{ get{ return 30; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }

		public override int DefMissSound{ get{ return 0x239; } }

		[Constructable]
		public OrnateAxe() : base( 0x2D28 )
		{
			Weight = 6.0;
			Name = "barbarian axe";
			Layer = Layer.OneHanded;
		}

		public OrnateAxe( Serial serial ) : base( serial )
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