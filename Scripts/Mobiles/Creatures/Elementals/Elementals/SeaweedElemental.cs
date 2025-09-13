using System;
using Server;
using Server.Items;
using System.Collections;
using Server.Gumps;
using Server.Network;
using Server.Engines.Plants;

namespace Server.Mobiles
{
	[CorpseName( "a plant corpse" )]
	public class SeaweedElemental : BaseCreature
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

		public override double DispelDifficulty{ get{ return 120.5; } }
		public override double DispelFocus{ get{ return 35.0; } }

		[Constructable]
		public SeaweedElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.6, 1.2 )
		{
			Name = "a seaweed elemental";
			Body = 780;
			Hue = 0xB87;
			BaseSoundID = 442;

			CanSwim = true;

			SetStr( 301, 400 );
			SetDex( 46, 65 );
			SetInt( 36, 50 );

			SetHits( 281, 340 );
			SetMana( 0 );

			SetDamage( 10, 23 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Poison, 40 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 20, 25 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 20, 25 );

			SetSkill( SkillName.MagicResist, 90.1, 95.0 );
			SetSkill( SkillName.Tactics, 70.1, 85.0 );
			SetSkill( SkillName.Wrestling, 65.1, 80.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 28;

			PackReg( 3 );

			if ( Utility.Random( 100 ) > 60 )
			{
				int seed_to_give = Utility.Random( 100 );

				if ( seed_to_give > 70 )
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
			AddLoot( LootPack.Average, 2 );
		}

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
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

		public SeaweedElemental( Serial serial ) : base( serial )
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