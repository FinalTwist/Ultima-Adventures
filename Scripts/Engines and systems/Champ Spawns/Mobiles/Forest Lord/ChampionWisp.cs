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
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a wisp champion corpse" )]
	public class ChampionWisp : BaseCreature
	{
		public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Wisp; } }

		public override TimeSpan ReacquireDelay { get { return TimeSpan.FromSeconds( 1.0 ); } }

		[Constructable]
		public ChampionWisp() : base( AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			ChampionHue();

			Name = "a wisp";
			Body = 58;
			BaseSoundID = 466;

			SetStr( 196, 225 );
			SetDex( 196, 225 );
			SetInt( 196, 225 );

			SetHits( 118, 135 );

			SetDamage( 17, 18 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 20, 40 );
			SetResistance( ResistanceType.Cold, 10, 30 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 50, 70 );

			SetSkill( SkillName.EvalInt, 80.0 );
			SetSkill( SkillName.Magery, 80.0 );
			SetSkill( SkillName.MagicResist, 80.0 );
			SetSkill( SkillName.Tactics, 80.0 );
			SetSkill( SkillName.Wrestling, 80.0 );

			Fame = 4000;
			Karma = 1;

			VirtualArmor = 40;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 120.0;

			if ( Core.ML && Utility.RandomDouble() < .33 )
				PackItem( Engines.Plants.Seed.RandomPeculiarSeed(3) );

			AddItem( new LightSource() );
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

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
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

		public ChampionWisp( Serial serial ) : base( serial )
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