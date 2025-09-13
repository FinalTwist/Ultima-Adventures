using System;
using Server;
using Server.Items;
using Server.Engines.Plants;

namespace Server.Mobiles
{
	[CorpseName( "a reaper corpse" )]
	public class DarkReaper : BaseCreature
	{
		[Constructable]
		public DarkReaper() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a dark reaper";
			Body = Utility.RandomList( 285, 301 );
			BaseSoundID = 442;
			Hue = 0x497;

			SetStr( 66, 215 );
			SetDex( 66, 75 );
			SetInt( 101, 250 );

			SetHits( 40, 129 );
			SetStam( 0 );

			SetDamage( 9, 11 );

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
			SetSkill( SkillName.Necromancy, 90.1, 100.0 );
			SetSkill( SkillName.SpiritSpeak, 90.1, 100.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 40;

			PackItem( new EbonyLog( Utility.RandomMinMax( 10, 20 ) ) );
			PackItem( new MandrakeRoot( 5 ) );

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

		public DarkReaper( Serial serial ) : base( serial )
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