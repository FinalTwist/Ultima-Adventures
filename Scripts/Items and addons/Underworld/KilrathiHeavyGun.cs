using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class KilrathiHeavyGun : BaseKilrathi
	{
		public override int EffectID{ get{ return 0x28EF; } }
		public override Type AmmoType{ get{ return typeof( Krystal ); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.DoubleShot; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.MovingShot; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.ZapIntStrike; } }

		public override int AosStrengthReq{ get{ return 80; } }
		public override int AosMinDamage{ get{ return Core.ML ? 20 : 19; } }
		public override int AosMaxDamage{ get{ return Core.ML ? 24 : 20; } }
		public override int AosSpeed{ get{ return 22; } }
		public override float MlSpeed{ get{ return 5.00f; } }

		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 11; } }
		public override int OldMaxDamage{ get{ return 56; } }
		public override int OldSpeed{ get{ return 10; } }

		public override int DefMaxRange{ get{ return 8; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 100; } }

		[Constructable]
		public KilrathiHeavyGun() : base( 0x3F65 )
		{
			Hue = 0x961;
			Name = "Kilrathi heavy bowcaster";
			Weight = 9.0;
			Layer = Layer.TwoHanded;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Requires Krystals to Fire");
        }

		public KilrathiHeavyGun( Serial serial ) : base( serial )
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