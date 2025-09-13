using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "an elemental corpse" )]
	public class GemElemental : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		public override double DispelDifficulty{ get{ return 120.5; } }
		public override double DispelFocus{ get{ return 35.0; } }

		public override int BreathPhysicalDamage{ get{ return 50; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 50; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return Hue-1; } }
		public override int BreathEffectSound{ get{ return 0x658; } }
		public override int BreathEffectItemID{ get{ return 0; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 33 ); }

		[Constructable]
		public GemElemental () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an elemental mineral";
			Body = 322;
			BaseSoundID = 268;
			Hue = 0x4AC;

			Hunger = Utility.RandomMinMax( 0, 23 );
			HueMe();

			SetStr( 256, 385 );
			SetDex( 196, 215 );
			SetInt( 221, 242 );

			SetHits( 194, 211 );

			SetDamage( 18, 29 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.EvalInt, 40.5, 90.0 );
			SetSkill( SkillName.Magery, 40.5, 90.0 );
			SetSkill( SkillName.MagicResist, 60.1, 110.0 );
			SetSkill( SkillName.Tactics, 100.1, 130.0 );
			SetSkill( SkillName.Wrestling, 90.1, 120.0 );

			Fame = 7000;
			Karma = -7000;

			VirtualArmor = 60;

			AddItem( new LightSource() );
		}
	
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			switch ( Hunger )
			{
				case 0:		c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "onyx stones" ) ); break;
				case 1:		c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "quartz stones" ) ); break;
				case 2:		c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "ruby stones" ) ); break;
				case 3:		c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "sapphire stones" ) ); break;
				case 4:		c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "spinel stones" ) ); break;
				case 5:		c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "topaz stones" ) ); break;
				case 6:		c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "amethyst stones" ) ); break;
				case 7:		c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "emerald stones" ) ); break;
				case 8:		c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "garnet stones" ) ); break;
				case 9:		c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "silver stones" ) ); break;
				case 10:	c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "star ruby stones" ) ); break;
				case 11:	c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "jade stones" ) ); break;
				case 12:	c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "copper stones" ) ); break;
				case 13:	c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "verite stones" ) ); break;
				case 14:	c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "valorite stones" ) ); break;
				case 15:	c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "agapite stones" ) ); break;
				case 16:	c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "bronze stones" ) ); break;
				case 17:	c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "dull copper stones" ) ); break;
				case 18:	c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "gold stones" ) ); break;
				case 19:	c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "shadow iron stones" ) ); break;
				case 20:	c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "mithril stones" ) ); break;
				case 21:	c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "xormite stones" ) ); break;
				case 22:	c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "obsidian stones" ) ); break;
				case 23:	c.DropItem( new RareMetals( Utility.RandomMinMax( 5, 10 ), "nepturite stones" ) ); break;
			}
		}

		public void HueMe()
		{
			switch ( Hunger )
			{
				case 0:		Hue = MaterialInfo.GetMaterialColor( "onyx", "monster", 0 ); break;
				case 1:		Hue = MaterialInfo.GetMaterialColor( "quartz", "monster", 0 ); break;
				case 2:		Hue = MaterialInfo.GetMaterialColor( "ruby", "monster", 0 ); break;
				case 3:		Hue = MaterialInfo.GetMaterialColor( "sapphire", "monster", 0 ); break;
				case 4:		Hue = MaterialInfo.GetMaterialColor( "spinel", "monster", 0 ); break;
				case 5:		Hue = MaterialInfo.GetMaterialColor( "topaz", "monster", 0 ); break;
				case 6:		Hue = MaterialInfo.GetMaterialColor( "amethyst", "monster", 0 ); break;
				case 7:		Hue = MaterialInfo.GetMaterialColor( "emerald", "monster", 0 ); break;
				case 8:		Hue = MaterialInfo.GetMaterialColor( "garnet", "monster", 0 ); break;
				case 9:		Hue = MaterialInfo.GetMaterialColor( "silver", "monster", 0 ); break;
				case 10:	Hue = MaterialInfo.GetMaterialColor( "star ruby", "monster", 0 ); break;
				case 11:	Hue = MaterialInfo.GetMaterialColor( "jade", "monster", 0 ); break;
				case 12:	Hue = MaterialInfo.GetMaterialColor( "copper", "monster", 0 ); break;
				case 13:	Hue = MaterialInfo.GetMaterialColor( "verite", "monster", 0 ); break;
				case 14:	Hue = MaterialInfo.GetMaterialColor( "valorite", "monster", 0 ); break;
				case 15:	Hue = MaterialInfo.GetMaterialColor( "agapite", "monster", 0 ); break;
				case 16:	Hue = MaterialInfo.GetMaterialColor( "bronze", "monster", 0 ); break;
				case 17:	Hue = MaterialInfo.GetMaterialColor( "dull copper", "monster", 0 ); break;
				case 18:	Hue = MaterialInfo.GetMaterialColor( "gold", "monster", 0 ); break;
				case 19:	Hue = MaterialInfo.GetMaterialColor( "shadow iron", "monster", 0 ); break;
				case 20:	Hue = MaterialInfo.GetMaterialColor( "mithril", "monster", 0 ); break;
				case 21:	Hue = MaterialInfo.GetMaterialColor( "xormite", "monster", 0 ); break;
				case 22:	Hue = MaterialInfo.GetMaterialColor( "obsidian", "monster", 0 ); break;
				case 23:	Hue = MaterialInfo.GetMaterialColor( "nepturite", "monster", 0 ); break;
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, 2 );
		}

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ reflect = true; } // 25% spells are reflected back to the caster
			else { reflect = false; }
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 2; } }

		public GemElemental( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
