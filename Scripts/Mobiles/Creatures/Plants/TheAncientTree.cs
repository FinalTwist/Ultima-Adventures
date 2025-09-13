using System;
using Server;
using Server.Items;
using Server.Engines.Plants;

namespace Server.Mobiles
{
	[CorpseName( "a fallen tree" )]
	public class TheAncientTree : BaseCreature
	{
		[Constructable]
		public TheAncientTree() : base( AIType.AI_Mage, FightMode.Closest, 15, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "trees" );
			Title = "the ancient tree";
			Body = 313;
			BaseSoundID = 442;

			SetStr( 416, 505 );
			SetDex( 146, 165 );
			SetInt( 566, 655 );

			SetHits( 250, 303 );
			SetMana( 2000 );

			SetDamage( 13, 16 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Energy, 20 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Fire, 20 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Meditation, 90.1, 100.0 );
			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 150.5, 200.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 22000;
			Karma = -22000;

			VirtualArmor = 50;

			PackItem( new Log( 100 ) );
			PackItem( new MandrakeRoot( 100 ) );

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

			OilWood loot1 = new OilWood();
			loot1.Amount = Utility.RandomMinMax( 1, 2 );
			c.DropItem(loot1);

			ReaperOil loot2 = new ReaperOil();
			loot2.Amount = Utility.RandomMinMax( 2, 5 );
			c.DropItem(loot2);

			MysticalTreeSap loot3 = new MysticalTreeSap();
			loot3.Amount = Utility.RandomMinMax( 2, 5 );
			c.DropItem(loot3);
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich );
			AddLoot( LootPack.UltraRich );
		}

        public override int GetAngerSound()
        {
            return 0x61E;
        }

        public override int GetDeathSound()
        {
            return 0x61F;
        }

        public override int GetHurtSound()
        {
            return 0x620;
        }

        public override int GetIdleSound()
        {
            return 0x621;
        }

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 6; } }
		public override bool DisallowAllMoves{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }

		public TheAncientTree( Serial serial ) : base( serial )
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