using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0xE87, 0xE88 )]
	public class Pitchfork : BaseSpear
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
		public Pitchfork() : base( 0xE87 )
		{
			Name = "trident";
			Weight = 11.0;
			Hue = 0xB3A;
		}

		public override void OnLocationChange( Point3D oldLocation )
		{
			Server.Items.Pitchfork.IronColor( this );
		}

		public static void IronColor( BaseWeapon weapon )
		{
			if ( weapon.Hue == 0 )
			{
				if ( weapon.Resource == CraftResource.Iron ){ weapon.Hue = 0xB3A; }
				else if ( weapon.Resource == CraftResource.DullCopper ){ weapon.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "dull copper", "", 0 ); }
				else if ( weapon.Resource == CraftResource.ShadowIron ){ weapon.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "shadow iron", "", 0 ); }
				else if ( weapon.Resource == CraftResource.Copper ){ weapon.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "copper", "", 0 ); }
				else if ( weapon.Resource == CraftResource.Bronze ){ weapon.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "bronze", "", 0 ); }
				else if ( weapon.Resource == CraftResource.Gold ){ weapon.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "gold", "", 0 ); }
				else if ( weapon.Resource == CraftResource.Agapite ){ weapon.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "agapite", "", 0 ); }
				else if ( weapon.Resource == CraftResource.Verite ){ weapon.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "verite", "", 0 ); }
				else if ( weapon.Resource == CraftResource.Valorite ){ weapon.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "valorite", "", 0 ); }
				else if ( weapon.Resource == CraftResource.Steel ){ weapon.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "steel", "", 0 ); }
				else if ( weapon.Resource == CraftResource.Brass ){ weapon.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "brass", "", 0 ); }
				else if ( weapon.Resource == CraftResource.Mithril ){ weapon.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "mithril", "", 0 ); }
				else if ( weapon.Resource == CraftResource.Xormite ){ weapon.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "xormite", "", 0 ); }
				else if ( weapon.Resource == CraftResource.Obsidian ){ weapon.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "obsidian", "", 0 ); }
				else if ( weapon.Resource == CraftResource.Nepturite ){ weapon.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "nepturite", "", 0 ); }
				else if ( weapon.Resource == CraftResource.Dwarven ){ weapon.Hue = Server.Misc.MaterialInfo.GetMaterialColor( "dwarven", "", 0 ); }
			}
		}

		public Pitchfork( Serial serial ) : base( serial )
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