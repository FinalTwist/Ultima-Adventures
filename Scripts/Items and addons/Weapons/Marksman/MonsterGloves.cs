using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x13C6, 0x13CE )]
	public class MonsterGloves : BaseRanged
	{
		public string ThrowType;

		public override int EffectID
		{
			get
			{
				if ( ThrowType == "Stones" ){ return 0x10B6; }
				else if ( ThrowType == "Axes" ){ return 0x10B3; }
				else if ( ThrowType == "Daggers" ){ return 0x529F; }
				else if ( ThrowType == "Darts" ){ return 0x52B0; }
				else if ( ThrowType == "Spear" ){ return 0x528A; }
				else if ( ThrowType == "Boulder" ){ return 0x1368; }
				else if ( ThrowType == "Bones" ){ return 0xF7E; }
				else if ( ThrowType == "Bandages" ){ return 0xE21; }
				else { return 0x10B2; }
			}
		}

		public override int DefHitSound
		{
			get
			{
				if ( ThrowType == "Stones" ){ return 0x5D2; }
				else if ( ThrowType == "Axes" ){ return 0x5D2; }
				else if ( ThrowType == "Daggers" ){ return 0x5D2; }
				else if ( ThrowType == "Darts" ){ return 0x5D2; }
				else if ( ThrowType == "Spear" ){ return 0x5D2; }
				else if ( ThrowType == "Boulder" ){ return 0x5D2; }
				else if ( ThrowType == "Bones" ){ return 0x5D2; }
				else if ( ThrowType == "Bandages" ){ return 0x5D2; }
				return 0x5D2;
			}
		}

		public override int DefMissSound
		{
			get
			{
				if ( ThrowType == "Stones" ){ return 0x5D3; }
				else if ( ThrowType == "Axes" ){ return 0x5D3; }
				else if ( ThrowType == "Daggers" ){ return 0x5D3; }
				else if ( ThrowType == "Darts" ){ return 0x5D3; }
				else if ( ThrowType == "Spear" ){ return 0x5D3; }
				else if ( ThrowType == "Boulder" ){ return 0x5D3; }
				else if ( ThrowType == "Bandages" ){ return 0x5D3; }
				return 0x5D3;
			}
		}

		public override Type AmmoType{ get{ return typeof( ThrowingWeapon ); } }
		public override Item Ammo{ get{ return new ThrowingWeapon(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.DoubleShot; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.MovingShot; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.ZapIntStrike; } }

		public override int AosStrengthReq{ get{ return 30; } }
		public override int AosMinDamage{ get{ return Core.ML ? 15 : 16; } }
		public override int AosMaxDamage{ get{ return Core.ML ? 19 : 18; } }
		public override int AosSpeed{ get{ return 25; } }
		public override float MlSpeed{ get{ return 4.25f; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 9; } }
		public override int OldMaxDamage{ get{ return 41; } }
		public override int OldSpeed{ get{ return 20; } }

		public override int DefMaxRange{ get{ return 10; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override SkillName DefSkill{ get{ return SkillName.Archery; } }
		public override WeaponType DefType{ get{ return WeaponType.Ranged; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Wrestle; } }

		[Constructable]
		public MonsterGloves() : base( 0x13C6 )
		{
			if ( ThrowType == "" || ThrowType == null ){ ThrowType = "Stones"; }
			Name = "monster gloves";
			Weight = 2.0;
			Layer = Layer.OneHanded;
			Attributes.SpellChanneling = 1;
			Resource = CraftResource.RegularLeather;
			LootType = LootType.Blessed;
			Movable = false;
		}

		public MonsterGloves( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( ThrowType );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            ThrowType = reader.ReadString();
		}
	}
}