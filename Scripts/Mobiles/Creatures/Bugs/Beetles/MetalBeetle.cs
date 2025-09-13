using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a beetle corpse" )]
	public class MetalBeetle : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.CrushingBlow;
		}

		[Constructable]
		public MetalBeetle() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a metallic beetle";
			Body = 82;
			BaseSoundID = 268;

			SetStr( 401, 460 );
			SetDex( 121, 170 );
			SetInt( 376, 450 );

			SetHits( 301, 360 );

			SetDamage( 15, 22 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Poison, 10 );
			SetDamageType( ResistanceType.Energy, 70 );

			SetResistance( ResistanceType.Physical, 40, 65 );
			SetResistance( ResistanceType.Fire, 35, 50 );
			SetResistance( ResistanceType.Cold, 35, 50 );
			SetResistance( ResistanceType.Poison, 75, 95 );
			SetResistance( ResistanceType.Energy, 40, 60 );

			SetSkill( SkillName.Poisoning, 120.1, 140.0 );
			SetSkill( SkillName.MagicResist, 95.1, 110.0 );
			SetSkill( SkillName.Tactics, 78.1, 93.0 );
			SetSkill( SkillName.Wrestling, 70.1, 77.5 );

			Fame = 8000;
			Karma = -8000;

			if ( Utility.RandomDouble() < .25 )
				PackItem( Engines.Plants.Seed.RandomBonsaiSeed() );
				
			switch ( Utility.Random( 10 ))
			{
				case 0: PackItem( new LeftArm() ); break;
				case 1: PackItem( new RightArm() ); break;
				case 2: PackItem( new Torso() ); break;
				case 3: PackItem( new Bone() ); break;
				case 4: PackItem( new RibCage() ); break;
				case 5: PackItem( new RibCage() ); break;
				case 6: PackItem( new BonePile() ); break;
				case 7: PackItem( new BonePile() ); break;
				case 8: PackItem( new BonePile() ); break;
				case 9: PackItem( new BonePile() ); break;
			}

			Item Venom = new VenomSack();
				Venom.Name = "venom sack";
				AddItem( Venom );
		}

		public override void OnAfterSpawn()
		{
			switch ( Utility.RandomMinMax( 0, 9 ) )
			{
				case 0: Hue = MaterialInfo.GetMaterialColor( "copper", "monster", 0 ); break; // Copper
				case 1: Hue = MaterialInfo.GetMaterialColor( "verite", "monster", 0 ); break; // Verite
				case 2: Hue = MaterialInfo.GetMaterialColor( "valorite", "monster", 0 ); break; // Valorite
				case 3: Hue = MaterialInfo.GetMaterialColor( "agapite", "monster", 0 ); break; // Agapite
				case 4: Hue = MaterialInfo.GetMaterialColor( "bronze", "monster", 0 ); break; // Bronze
				case 5: Hue = MaterialInfo.GetMaterialColor( "dull copper", "monster", 0 ); break; // Dull Copper
				case 6: Hue = MaterialInfo.GetMaterialColor( "gold", "monster", 0 ); break; // Gold
				case 7: Hue = MaterialInfo.GetMaterialColor( "shadow iron", "monster", 0 ); break; // Shadow Iron
				case 8:
					if ( Worlds.IsExploringSeaAreas( this ) == true ){ Hue = MaterialInfo.GetMaterialColor( "nepturite", "monster", 0 ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Serpent Island" ){ Hue = MaterialInfo.GetMaterialColor( "obsidian", "monster", 0 ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" && this.Map == Map.TerMur ){ Hue = MaterialInfo.GetMaterialColor( "xormite", "monster", 0 ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" ){ Hue = MaterialInfo.GetMaterialColor( "mithril", "monster", 0 ); }
					break; // Special
			}

			base.OnAfterSpawn();
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( this.Hue == 0 )
			{
				c.DropItem( new IronOre ( Utility.RandomMinMax( 10, 40 ) ) );
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "copper", "monster", 0 ) )
			{
				c.DropItem( new CopperOre ( Utility.RandomMinMax( 10, 40 ) ) );
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "verite", "monster", 0 ) )
			{
				c.DropItem( new VeriteOre ( Utility.RandomMinMax( 10, 40 ) ) );
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "valorite", "monster", 0 ) )
			{
				c.DropItem( new ValoriteOre ( Utility.RandomMinMax( 10, 40 ) ) );
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "agapite", "monster", 0 ) )
			{
				c.DropItem( new AgapiteOre ( Utility.RandomMinMax( 10, 40 ) ) );
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "bronze", "monster", 0 ) )
			{
				c.DropItem( new BronzeOre ( Utility.RandomMinMax( 10, 40 ) ) );
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "dull copper", "monster", 0 ) )
			{
				c.DropItem( new DullCopperOre ( Utility.RandomMinMax( 10, 40 ) ) );
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "gold", "monster", 0 ) )
			{
				c.DropItem( new GoldOre ( Utility.RandomMinMax( 10, 40 ) ) );
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "shadow iron", "monster", 0 ) )
			{
				c.DropItem( new ShadowIronOre ( Utility.RandomMinMax( 10, 40 ) ) );
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "mithril", "monster", 0 ) )
			{
				c.DropItem( new MithrilOre ( Utility.RandomMinMax( 10, 40 ) ) );
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "xormite", "monster", 0 ) )
			{
				c.DropItem( new XormiteOre ( Utility.RandomMinMax( 10, 40 ) ) );
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "obsidian", "monster", 0 ) )
			{
				c.DropItem( new ObsidianOre ( Utility.RandomMinMax( 10, 40 ) ) );
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "nepturite", "monster", 0 ) )
			{
				c.DropItem( new NepturiteOre ( Utility.RandomMinMax( 10, 40 ) ) );
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 2 );
		}

		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override Poison HitPoison{ get{ return Poison.Regular; } }

		public MetalBeetle( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}