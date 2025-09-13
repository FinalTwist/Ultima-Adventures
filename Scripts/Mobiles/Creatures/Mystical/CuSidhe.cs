using Server;
using System;
using Server.Items;
using Server.Gumps;

namespace Server.Mobiles
{
	[CorpseName( "a Cu Sidhe corpse" )]
	public class CuSidhe : BaseMount
	{
		[Constructable]
		public CuSidhe() : this( "a Cu Sidhe" )
		{
		}

		[Constructable]
		public CuSidhe( string name ) : base( name, 277, 0x3E91, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{			
			double chance = Utility.RandomDouble() * 23301;

			if ( chance <= 1 )
				Hue = 0x489;
			else if ( chance < 50 )
				Hue = Utility.RandomList( 0x657, 0x515, 0x4B1, 0x481, 0x482, 0x455 );
			else if ( chance < 500 )
				Hue = Utility.RandomList( 0x97A, 0x978, 0x901, 0x8AC, 0x5A7, 0x527 ); 

			SetStr( 1200, 1225 );
			SetDex( 150, 170 );
			SetInt( 250, 285 );
			
			SetHits( 1010, 1275 );

			SetDamage( 21, 28 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Cold, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 50, 65 );
			SetResistance( ResistanceType.Fire, 25, 45 );
			SetResistance( ResistanceType.Cold, 70, 85 );
			SetResistance( ResistanceType.Poison, 30, 50 );
			SetResistance( ResistanceType.Energy, 70, 85 );

			SetSkill( SkillName.Wrestling, 90.1, 96.8 );
			SetSkill( SkillName.Tactics, 90.3, 99.3 );
			SetSkill( SkillName.MagicResist, 75.3, 90.0 );
			SetSkill( SkillName.Anatomy, 65.5, 69.4 );
			SetSkill( SkillName.Healing, 72.2, 98.9 );

			Fame = 5000;  //Guessing here
			Karma = 5000;  //Guessing here

			Tamable = true;
			ControlSlots = 4;
			MinTameSkill = 101.1;					

			if ( Utility.RandomDouble() < 0.2 )
				PackItem( new TreasureMap( 5, Map, Location, X, Y ) );

			//if ( Utility.RandomDouble() < 0.1 )				
				//PackItem( new ParrotItem() );

			PackGold( 500, 800 );

			// TODO 0-2 spellweaving scroll
		}	

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosFilthyRich, 5 );
		}
/*
		public override void OnDoubleClick( Mobile from )
		{ 
			if ( from.Race != Race.Elf && from == ControlMaster && from.AccessLevel == AccessLevel.Player )
			{
				Item pads = from.FindItemOnLayer( Layer.Shoes );
				
				if ( pads is PadsOfTheCuSidhe )
					from.SendLocalizedMessage( 1071981 ); // Your boots allow you to mount the Cu Sidhe.
				else
				{
					from.SendLocalizedMessage( 1072203 ); // Only Elves may use this.
					return;
				}
			}
			
			base.OnDoubleClick( from );
		} */
		
		public override bool CanHeal{ get{ return true; } }
		public override bool CanHealOwner{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies; } }
		public override bool CanAngerOnTame{ get { return true; } }
		public override bool StatLossAfterTame{ get{ return true; } }
		public override int Hides{ get{ return 10; } }	
		public override int Meat{ get{ return 3; } }	

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		public CuSidhe( Serial serial ) : base( serial )
		{
		}

		public override int GetIdleSound() { return 0x577; }
		public override int GetAttackSound() { return 0x576; }
		public override int GetAngerSound() { return 0x578; }
		public override int GetHurtSound() { return 0x576; }
		public override int GetDeathSound()	{ return 0x579; }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
