using System;
using Server;
using Server.Items;
using System.Collections;
using Server.Gumps;
using Server.Network;
using Server.Engines.Plants;

namespace Server.Mobiles
{
	[CorpseName( "a pile of weeds" )]
	public class WeedElemental : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 35.0; } }

		public override int BreathPhysicalDamage{ get{ return 100; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0; } }
		public override int BreathEffectSound{ get{ return 0x56D; } }
		public override int BreathEffectItemID{ get{ return 0; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 35 ); }

		[Constructable]
		public WeedElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.6, 1.2 )
		{
			Name = "a weed elemental";
			Body = 780;
			Hue = 0x7D1;
			BaseSoundID = 442;

			SetStr( 126, 155 );
			SetDex( 66, 85 );
			SetInt( 71, 92 );

			SetHits( 76, 93 );

			SetDamage( 9, 16 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 15, 25 );

			SetSkill( SkillName.MagicResist, 50.1, 95.0 );
			SetSkill( SkillName.Tactics, 60.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 34;

			PackReg( 30 );

			switch ( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0: Item weed1 = new PlantHerbalism_Leaf(); weed1.Amount = Utility.RandomMinMax( 1, 3 ); PackItem( weed1 ); break;
				case 1: Item weed2 = new PlantHerbalism_Flower(); weed2.Amount = Utility.RandomMinMax( 1, 3 ); PackItem( weed2 ); break;
				case 2: Item weed3 = new PlantHerbalism_Mushroom(); weed3.Amount = Utility.RandomMinMax( 1, 3 ); PackItem( weed3 ); break;
				case 3: Item weed4 = new PlantHerbalism_Lilly(); weed4.Amount = Utility.RandomMinMax( 1, 3 ); PackItem( weed4 ); break;
				case 4: Item weed5 = new PlantHerbalism_Cactus(); weed5.Amount = Utility.RandomMinMax( 1, 3 ); PackItem( weed5 ); break;
				case 5: Item weed6 = new PlantHerbalism_Grass(); weed6.Amount = Utility.RandomMinMax( 1, 3 ); PackItem( weed6 ); break;
			}

			PackItem( new FertileDirt( Utility.RandomMinMax( 1, 4 ) ) );

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
			if (Utility.RandomDouble() > 0.90)
				PackItem( new Server.Items.Crops.SmokeweedLeaves( 1 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 1 );
			AddLoot( LootPack.Rich, 1 );
		}

		public override bool BleedImmune{ get{ return true; } }

		public override void OnGaveMeleeAttack( Mobile m )
		{
			base.OnGaveMeleeAttack( m );

			if ( 1 == Utility.RandomMinMax( 1, 20 ) )
			{
				Container cont = m.Backpack;
				Item iWrapped = Server.Items.HiddenTrap.GetMyItem( m );

				if ( iWrapped != null )
				{
					if ( Server.Items.HiddenTrap.IAmShielding( m, 100 ) || Server.Items.HiddenTrap.IAmAWeaponSlayer( m, this ) )
					{
					}
					else if ( Server.Items.HiddenTrap.CheckInsuranceOnTrap( iWrapped, m ) == true )
					{
						m.LocalOverheadMessage(MessageType.Emote, 1150, true, "One of your protected items was almost wrapped in weeds!");
					}
					else
					{
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "One of your items is wrapped in weeds!");
						m.PlaySound( 0x1BB );
						Container box = new WeededItem();
						box.DropItem(iWrapped);
						box.ItemID = iWrapped.ItemID;
						m.AddToBackpack ( box );
					}
				}
			}
		}

		public WeedElemental( Serial serial ) : base( serial )
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