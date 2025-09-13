using System;
using Server;
using Server.Items;
using System.Collections;
using Server.Gumps;
using Server.Network;
using Server.Engines.Plants;

namespace Server.Mobiles
{
	[CorpseName( "a patch of kelp" )]
	public class KelpElemental : BaseCreature
	{
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
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 34 ); }

		public override double DispelDifficulty{ get{ return 140.5; } }
		public override double DispelFocus{ get{ return 35.0; } }

		[Constructable]
		public KelpElemental() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a kelp elemental";
			Body = 331;
			BaseSoundID = 442;

			CanSwim = true;

			SetStr( 526, 615 );
			SetDex( 66, 85 );
			SetInt( 226, 350 );

			SetHits( 316, 369 );

			SetDamage( 17, 27 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 85.1, 100.0 );
			SetSkill( SkillName.Magery, 85.1, 100.0 );
			SetSkill( SkillName.Meditation, 10.4, 50.0 );
			SetSkill( SkillName.MagicResist, 80.1, 95.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 100.0 );

			Fame = 12500;
			Karma = -12500;

			VirtualArmor = 60;

			PackReg( 40 );

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
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Rich );
		}

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool BleedImmune{ get{ return true; } }

		public override void OnGaveMeleeAttack( Mobile m )
		{
			base.OnGaveMeleeAttack( m );

			if ( 1 == Utility.RandomMinMax( 1, 1 ) )
			{
				Container cont = m.Backpack;
				Item iWrapped = Server.Items.HiddenTrap.GetMyItem( m );

				if ( iWrapped != null )
				{
					if ( Server.Items.HiddenTrap.IAmShielding( m, 125 ) || Server.Items.HiddenTrap.IAmAWeaponSlayer( m, this ) )
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

		public KelpElemental( Serial serial ) : base( serial )
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