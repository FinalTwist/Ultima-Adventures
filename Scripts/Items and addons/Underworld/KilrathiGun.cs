using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class KilrathiGun : BaseKilrathi
	{
		public override int EffectID{ get{ return 0x28EF; } }
		public override Type AmmoType{ get{ return typeof( Krystal ); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MovingShot; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.StunningStrike; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.ZapManaStrike; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.ZapDexStrike; } }

		public override int AosStrengthReq{ get{ return 35; } }
		public override int AosMinDamage{ get{ return 18; } }
		public override int AosMaxDamage{ get{ return Core.ML ? 22 : 20; } }
		public override int AosSpeed{ get{ return 24; } }
		public override float MlSpeed{ get{ return 4.50f; } }

		public override int OldStrengthReq{ get{ return 30; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 43; } }
		public override int OldSpeed{ get{ return 18; } }

		public override int DefMaxRange{ get{ return 8; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 80; } }

		[Constructable]
		public KilrathiGun() : base( 0x3F8F )
		{
			Hue = 0x961;
			Name = "Kilrathi bowcaster";
			Weight = 7.0;
			Layer = Layer.OneHanded;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Requires Krystals to Fire");
        }

		public KilrathiGun( Serial serial ) : base( serial )
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