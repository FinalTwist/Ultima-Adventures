/**********************************************************
	Name: Champion Spawn Monster
	Scripted By: Formosa
	Version: v1.0
	Update Date: March 10, 2013

	Notes: 
	Anyone can modify/redistribute this 
	Do Not Remove/Change This Header!!	
**********************************************************/

using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a deathwatchbeetle hatchling champion corpse" )]
	[TypeAlias( "Server.Mobiles.DeathWatchBeetleHatchling" )]
	public class ChampionDeathwatchBeetleHatchling : BaseCreature
	{
		[Constructable]
		public ChampionDeathwatchBeetleHatchling() : base( AIType.AI_Melee, Core.ML ? FightMode.Closest : FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			ChampionHue();

			Name = "a deathwatch beetle hatchling";
			Body = 242;

			SetStr( 26, 50 );
			SetDex( 41, 52 );
			SetInt( 21, 30 );

			SetHits( 51, 60 );
			SetMana( 20 );

			SetDamage( 2, 8 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 15, 30 );
			SetResistance( ResistanceType.Cold, 15, 30 );
			SetResistance( ResistanceType.Poison, 20, 40 );
			SetResistance( ResistanceType.Energy, 20, 35 );

			SetSkill( SkillName.Wrestling, 30.1, 40.0 );
			SetSkill( SkillName.Tactics, 47.1, 57.0 );
			SetSkill( SkillName.MagicResist, 30.1, 38.0 );
			SetSkill( SkillName.Anatomy, 20.1, 24.0 );

			Fame = 700;
			Karma = -700;

			VirtualArmor = 16;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 120.0;

			if( Utility.RandomBool() )
			{
				Item i = Loot.RandomReagent();
				i.Amount = 3;
				PackItem( i );
			}
		}

		public override int GetAngerSound()
		{
			return 0x4F3;
		}

		public override int GetIdleSound()
		{
			return 0x4F2;
		}

		public override int GetAttackSound()
		{
			return 0x4F1;
		}

		public override int GetHurtSound()
		{
			return 0x4F4;
		}

		public override int GetDeathSound()
		{
			return 0x4F0;
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

		public virtual void ChampionHue()
		{
			switch ( Utility.Random ( 10 ) )
			{
				case 0: Hue = ( 11 ); break;
				case 1: Hue = ( 25 ); break;
				case 2: Hue = ( 40 ); break;
				case 3: Hue = ( 44 ); break;
				case 4: Hue = ( 33 ); break;
				case 5: Hue = ( 49 ); break;
				case 6: Hue = ( 47 ); break;
				case 7: Hue = ( 56 ); break;
				case 8: Hue = ( 76 ); break;
				case 9: Hue = ( 95 ); break;
			}
		}

		public ChampionDeathwatchBeetleHatchling( Serial serial ) : base( serial )
		{
		}

		public override int Hides { get { return 8; } }

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