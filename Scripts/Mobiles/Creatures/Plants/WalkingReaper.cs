using System;
using Server;
using Server.Items;
using Server.Engines.Plants;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a reaper corpse" )]
	public class WalkingReaper : BaseCreature
	{
		[Constructable]
		public WalkingReaper() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a reaper";
			Body = Utility.RandomList( 285, 301 );
			BaseSoundID = 442;

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Poison, 20 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 100.1, 125.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 50.1, 60.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 40;

			PackItem( new MandrakeRoot( 5 ) );

			int modifySta = 0;
			int modifyHit = 0;
			int modifyDmg = 0;

			switch ( Utility.Random( 11 ) )
			{
				case 0: PackItem( new Log( Utility.RandomMinMax( 10, 20 ) ) ); 			Hue = 0x000;	break;
				case 1: PackItem( new AshLog( Utility.RandomMinMax( 10, 20 ) ) ); 		Hue = MaterialInfo.GetMaterialColor( "ash", "", 0 );		modifySta = 5;	modifyHit = 10;		modifyDmg = 1;	break;
				case 2: PackItem( new CherryLog( Utility.RandomMinMax( 10, 20 ) ) ); 	Hue = MaterialInfo.GetMaterialColor( "cherry", "", 0 );		modifySta = 10;	modifyHit = 20;		modifyDmg = 2;	break;
				case 3: PackItem( new EbonyLog( Utility.RandomMinMax( 10, 20 ) ) ); 	Hue = MaterialInfo.GetMaterialColor( "ebony", "", 0 );		modifySta = 15;	modifyHit = 30;		modifyDmg = 3;	break;
				case 4: PackItem( new GoldenOakLog( Utility.RandomMinMax( 10, 20 ) ) ); Hue = MaterialInfo.GetMaterialColor( "golden oak", "", 0 );	modifySta = 20;	modifyHit = 40;		modifyDmg = 4;	break;
				case 5: PackItem( new HickoryLog( Utility.RandomMinMax( 10, 20 ) ) ); 	Hue = MaterialInfo.GetMaterialColor( "hickory", "", 0 );	modifySta = 25;	modifyHit = 50;		modifyDmg = 5;	break;
				case 6: PackItem( new MahoganyLog( Utility.RandomMinMax( 10, 20 ) ) ); 	Hue = MaterialInfo.GetMaterialColor( "mahogany", "", 0 );	modifySta = 30;	modifyHit = 60;		modifyDmg = 6;	break;
				case 7: PackItem( new OakLog( Utility.RandomMinMax( 10, 20 ) ) ); 		Hue = MaterialInfo.GetMaterialColor( "oak", "", 0 );		modifySta = 35;	modifyHit = 70;		modifyDmg = 7;	break;
				case 8: PackItem( new PineLog( Utility.RandomMinMax( 10, 20 ) ) ); 		Hue = MaterialInfo.GetMaterialColor( "pine", "", 0 );		modifySta = 40;	modifyHit = 80;		modifyDmg = 8;	break;
				case 9: PackItem( new RosewoodLog( Utility.RandomMinMax( 10, 20 ) ) ); 	Hue = MaterialInfo.GetMaterialColor( "rosewood", "", 0 );	modifySta = 45;	modifyHit = 90;		modifyDmg = 9;	break;
				case 10: PackItem( new WalnutLog( Utility.RandomMinMax( 10, 20 ) ) ); 	Hue = MaterialInfo.GetMaterialColor( "walnut", "", 0 );		modifySta = 50;	modifyHit = 100;	modifyDmg = 10;	break;
			}

			SetStr( (66+modifySta), (215+modifySta) );
			SetDex( (66+modifySta), (75+modifySta) );
			SetInt( (101+modifySta), (250+modifySta) );

			SetHits( (40+modifyHit), (129+modifyHit) );
			SetStam( 0 );

			SetDamage( (9+modifyDmg), (11+modifyDmg) );

			if ( Utility.Random( 100 ) > 60 )
			{
				int seed_to_give = Utility.Random( 100 );

				if ( seed_to_give > 90 )
				{
					Seed reward;

					PlantType type;
					switch ( Utility.Random( 17 ) )
					{
						case 0: type = PlantType.CampionFlowers; break;
						case 1: type = PlantType.Poppies; break;
						case 2: type = PlantType.Snowdrops; break;
						case 3: type = PlantType.Bulrushes; break;
						case 4: type = PlantType.Lilies; break;
						case 5: type = PlantType.PampasGrass; break;
						case 6: type = PlantType.Rushes; break;
						case 7: type = PlantType.ElephantEarPlant; break;
						case 8: type = PlantType.Fern; break;
						case 9: type = PlantType.PonytailPalm; break;
						case 10: type = PlantType.SmallPalm; break;
						case 11: type = PlantType.CenturyPlant; break;
						case 12: type = PlantType.WaterPlant; break;
						case 13: type = PlantType.SnakePlant; break;
						case 14: type = PlantType.PricklyPearCactus; break;
						case 15: type = PlantType.BarrelCactus; break;
						default: type = PlantType.TribarrelCactus; break;
					}
						PlantHue hue;
						switch ( Utility.Random( 4 ) )
						{
							case 0: hue = PlantHue.Pink; break;
							case 1: hue = PlantHue.Magenta; break;
							case 2: hue = PlantHue.FireRed; break;
							default: hue = PlantHue.Aqua; break;
						}

						PackItem( new Seed( type, hue, false ) );
				}
				else if ( seed_to_give > 70 )
				{
					PackItem( Engines.Plants.Seed.RandomPeculiarSeed( Utility.RandomMinMax( 1, 4 ) ) );
				}
				else if ( seed_to_give > 40 )
				{
					PackItem( Engines.Plants.Seed.RandomBonsaiSeed() );
				}
				else
				{
					PackItem( new Engines.Plants.Seed() );
				}
			}
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			if ( 1 == Utility.RandomMinMax( 1, 50 ) )
			{
				OilWood loot = new OilWood();
				loot.Amount = 1;
				c.DropItem(loot);
			}
			if ( 1 == Utility.RandomMinMax( 1, 3 ) )
			{
				ReaperOil loot = new ReaperOil();
				loot.Amount = 1;
				c.DropItem(loot);
			}
			if ( 1 == Utility.RandomMinMax( 1, 3 ) )
			{
				MysticalTreeSap loot = new MysticalTreeSap();
				loot.Amount = 1;
				c.DropItem(loot);
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override Poison PoisonImmune{ get{ return Poison.Greater; } }
		public override int TreasureMapLevel{ get{ return 2; } }
		public override bool BleedImmune{ get{ return true; } }

		public WalkingReaper( Serial serial ) : base( serial )
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