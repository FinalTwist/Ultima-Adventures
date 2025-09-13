using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an ambroz corpse" )]
	public class AbrozWarrior : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.DoubleStrike;
		}

		[Constructable]
		public AbrozWarrior() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ambroz warrior";
			Body = 328;
			BaseSoundID = 0x45A;

			SetStr( 486, 530 );
			SetDex( 151, 165 );
			SetInt( 17, 31 );

			SetHits( 486, 530 );
			SetMana( 17, 31 );

			SetDamage( 8, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 85.1, 95.0 );
			SetSkill( SkillName.MagicResist, 82.6, 90.5 );
			SetSkill( SkillName.Tactics, 95.1, 105.0 );
			SetSkill( SkillName.Wrestling, 97.6, 107.5 );

			Fame = 4200;	
			Karma = -4200;

			PackItem( new GreenGourd() );
			PackItem( new ExecutionersAxe() );

			if( Utility.RandomBool() )
				PackItem( new LongPants() );
			else
				PackItem( new ShortPants() );

			switch ( Utility.Random( 4 ) )
			{
				case 0: PackItem( new Shoes() ); break;
				case 1: PackItem( new Sandals() ); break;
				case 2: PackItem( new Boots() ); break;
				case 3: PackItem( new ThighBoots() ); break;
			}

			if ( Utility.RandomDouble() < .25 )
				PackItem( Engines.Plants.Seed.RandomBonsaiSeed() );

		}

		public override FoodType FavoriteFood{ get{ return FoodType.Fish; } }

		public override int Meat{ get{ return 1; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 2 );
			AddLoot( LootPack.Gems,2);
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 3; } }

		// TODO: Throwing Dagger

		public override void OnGaveMeleeAttack( Mobile defender ) 
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.1 > Utility.RandomDouble() )
			{
				/* Maniacal laugh
				 * Cliloc: 1070840
				 * Effect: Type: "3" From: "0x57D4F5B" To: "0x0" ItemId: "0x37B9" ItemIdName: "glow" FromLocation: "(884 715, 10)" ToLocation: "(884 715, 10)" Speed: "10" Duration: "5" FixedDirection: "True" Explode: "False"
				 * Paralyzes for 4 seconds, or until hit
				 */

				defender.FixedEffect( 0x37B9, 10, 5 );
				defender.SendLocalizedMessage( 1070840 ); // You are frozen as the creature laughs maniacally.

				defender.Paralyze( TimeSpan.FromSeconds( 4.0 ) );
 			}
		}
		
		public AbrozWarrior( Serial serial ) : base( serial )
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
