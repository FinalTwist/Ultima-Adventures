/**********************************************************
	Name: Champion Spawn Monster
	Scripted By: Formosa
	Version: v1.0
	Update Date: March 10, 2013

	Notes: 
	Anyone can modify/redistribute this 
	Do Not Remove/Change This Header!!	
**********************************************************/

using Server;
using System;
using Server.Items;
using Server.Gumps;

namespace Server.Mobiles
{
	[CorpseName( "a Cu Sidhe champion corpse" )]
	public class ChampionCuSidhe : BaseMount
	{
		[Constructable]
		public ChampionCuSidhe() : this( "a Cu Sidhe" )
		{
		}

		[Constructable]
		public ChampionCuSidhe( string name ) : base( name, 277, 0x3E91, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{			
			double chance = Utility.RandomDouble() * 23301;

			if ( chance <= 1 )
				Hue = 0x489;
			else if ( chance < 50 )
				Hue = Utility.RandomList( 0x657, 0x515, 0x4B1, 0x481, 0x480, 0x455 );
			else if ( chance < 500 )
				Hue = Utility.RandomList( 0x97A, 0x978, 0x901, 0x8AC, 0x5A7, 0x527 ); 

			SetStr( 1201, 1225 );
			SetDex( 151, 170 );
			SetInt( 251, 282 );

			SetDamage( 21, 28 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Cold, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 30, 45 );
			SetResistance( ResistanceType.Cold, 70, 85 );
			SetResistance( ResistanceType.Poison, 30, 50 );
			SetResistance( ResistanceType.Energy, 70, 85 );

			SetSkill( SkillName.Wrestling, 90.1, 96.8 );
			SetSkill( SkillName.Tactics, 90.3, 99.3 );
			SetSkill( SkillName.MagicResist, 75.3, 90.0 );
			SetSkill( SkillName.Anatomy, 65.5, 69.4 );
			SetSkill( SkillName.Healing, 72.2, 98.9 );

			Fame = 20000;
			Karma = 20000;

			VirtualArmor = 16;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 120.0;					

			if ( Utility.RandomDouble() < 0.2 )
				PackItem( new TreasureMap( 5, Map, Location, X, Y ) );

			//if ( Utility.RandomDouble() < 0.1 )				
			//	PackItem( new ParrotItem() );

			PackGold( 500, 800 );

			PackItem( new Bandage( 10 ) );

			//PackArcaneScroll(1, 2);
		}	

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Gems );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			
			c.DropItem( new Apple( 6 ) );
			
			if ( Utility.RandomDouble() < 0.05 )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: c.DropItem( new Necklace() ); break;
					case 1: c.DropItem( new GoldRing() ); break;
				}
			}
			
			if ( Utility.RandomDouble() < 0.04 )
				c.DropItem( new GoldBracelet() );
						
			if ( Utility.RandomDouble() < 0.03 )
				c.DropItem( new GoldBeadNecklace() );
					
			if ( Utility.RandomDouble() < 0.02 )
				c.DropItem( new GoldNecklace() );
				
			if ( Utility.RandomDouble() < 0.01 )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: c.DropItem( new Beads() ); break;
					case 1: c.DropItem( new GoldEarrings() ); break;
				}
			}
		}

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
		}
		
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

		public ChampionCuSidhe( Serial serial ) : base( serial )
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
