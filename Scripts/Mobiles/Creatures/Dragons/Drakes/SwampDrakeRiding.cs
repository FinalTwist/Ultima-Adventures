using System;
using Server;
using Server.Items;
using Server.Engines.Plants;

namespace Server.Mobiles
{
	[CorpseName( "a swamp drake corpse" )]
	public class SwampDrakeRiding : BaseMount
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 100; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x3F; } }
		public override int BreathEffectSound{ get{ return 0x658; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override bool CanChew { get{return true;}}
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 18 ); }

		[Constructable]
		public SwampDrakeRiding() : this( "a swamp drake" )
		{
		}

		[Constructable]
		public SwampDrakeRiding( string name ) : base( name, 55, 0x3EBD, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			BaseSoundID = 362;

			SetStr( 401, 430 );
			SetDex( 133, 152 );
			SetInt( 101, 140 );

			SetHits( 241, 258 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Poison, 20 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 65.1, 80.0 );
			SetSkill( SkillName.Tactics, 65.1, 90.0 );
			SetSkill( SkillName.Wrestling, 65.1, 80.0 );

			Fame = 5500;
			Karma = -5500;

			VirtualArmor = 46;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 84.3;

			PackReg( 3 );

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

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override int TreasureMapLevel{ get{ return 2; } }
		public override int Meat{ get{ return 10; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override int Scales{ get{ return 2; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Green ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }

		public SwampDrakeRiding( Serial serial ) : base( serial )
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