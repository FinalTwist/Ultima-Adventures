using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2D22, 0x2D2E )]
	public class PearlKnife : BaseKnife
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.Feint; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }

		public override int AosStrengthReq{ get{ return 20; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 15; } }
		public override int AosSpeed{ get{ return 42; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 13; } }
		public override int OldMaxDamage{ get{ return 15; } }
		public override int OldSpeed{ get{ return 42; } }

		public override int DefMissSound{ get{ return 0x239; } }

		public override int InitMinHits{ get{ return 30; } } // TODO
		public override int InitMaxHits{ get{ return 60; } } // TODO

		[Constructable]
		public PearlKnife() : base( 0x2D22 )
		{
			Weight = 8.0;
            Hue = 1153;
            Name = "Pearl Knife";

            Attributes.WeaponSpeed = 22;
            Attributes.ReflectPhysical = 15;
            WeaponAttributes.HitLeechStam = 15;
            WeaponAttributes.ResistFireBonus = 50;
            WeaponAttributes.ResistPoisonBonus = 20;
		}

        public PearlKnife(Serial serial)
            : base(serial)
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