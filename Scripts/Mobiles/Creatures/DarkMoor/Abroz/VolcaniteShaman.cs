using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an Volcanite corpse" )]
	public class VolcaniteShaman : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.DoubleStrike;
		}

		[Constructable]
		public VolcaniteShaman() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Volcanite Shaman";
			Body = 250;
			BaseSoundID = 0x45A;

			SetStr( 486, 530 );
			SetDex( 101, 115 );
			SetInt( 601, 670 );

			SetHits( 150, 175 );

			SetDamage( 8, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 92.6, 107.5 );
			SetSkill( SkillName.Magery, 105.1, 115.0 );
			SetSkill( SkillName.Meditation, 100.1, 110.0 );
			SetSkill( SkillName.MagicResist, 112.6, 122.5 );
			SetSkill( SkillName.Tactics, 55.1, 105.0 );
			SetSkill( SkillName.Wrestling, 47.6, 57.5 );

			Fame = 9000;
			Karma = 9000;


			PackItem( new GreenGourd() );
			PackItem( new ExecutionersAxe() );

			switch ( Utility.Random( 3 ) )
			{
				case 0: PackItem( new LongPants() ); break;
				case 1: PackItem( new ShortPants() ); break;
			}

			switch ( Utility.Random( 6 ) )
			{
				case 0: PackItem( new Shoes() ); break;
				case 1: PackItem( new Sandals() ); break;
				case 2: PackItem( new Boots() ); break;
				case 3: PackItem( new ThighBoots() ); break;
			}

			if ( Utility.RandomDouble() < .25 ) PackItem( Engines.Plants.Seed.RandomBonsaiSeed() );
		}

		public override FoodType FavoriteFood{ get{ return FoodType.Fish; } }

		public override int Meat{ get{ return 1; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems, 4);
		}

		public override bool OnBeforeDeath()
		{
			if ( Server.Misc.IntelligentAction.HealThySelf( this ) ){ return false; }
			return base.OnBeforeDeath();
		}

		public override bool CanRummageCorpses{ get{ return true; } }

		// TODO: Body Transformation

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

		public VolcaniteShaman( Serial serial ) : base( serial )
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
