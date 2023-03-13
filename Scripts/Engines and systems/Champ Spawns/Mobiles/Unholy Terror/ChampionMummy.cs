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
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a mummy champion corpse" )]
	public class ChampionMummy : BaseCreature
	{
		[Constructable]
		public ChampionMummy() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
			ChampionHue();

			Name = "a mummy";
			Body = 154;
			BaseSoundID = 471;

			SetStr( 346, 370 );
			SetDex( 71, 90 );
			SetInt( 26, 40 );

			SetHits( 208, 222 );

			SetDamage( 13, 23 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Cold, 60 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.MagicResist, 15.1, 40.0 );
			SetSkill( SkillName.Tactics, 35.1, 50.0 );
			SetSkill( SkillName.Wrestling, 35.1, 50.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 50;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 120.0;

			if ( Core.ML && Utility.RandomDouble() < .33 )
				PackItem( Engines.Plants.Seed.RandomPeculiarSeed(2) );

			PackItem( new Garlic( 5 ) );
			PackItem( new Bandage( 10 ) );
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

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lesser; } }

		public virtual void ChampionHue()
		{
			switch ( Utility.Random ( 10 ) )
			{
				case 0: Hue = ( 1372 ); break;
				case 1: Hue = ( 2000 ); break;
				case 2: Hue = ( 1260 ); break;
				case 3: Hue = ( 947 ); break;
				case 4: Hue = ( 1207 ); break;
				case 5: Hue = ( 1195 ); break;
				case 6: Hue = ( 1177 ); break;
				case 7: Hue = ( 1172 ); break;
				case 8: Hue = ( 1170 ); break;
				case 9: Hue = ( 1289 ); break;
			}
		}

		public ChampionMummy( Serial serial ) : base( serial )
		{
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
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