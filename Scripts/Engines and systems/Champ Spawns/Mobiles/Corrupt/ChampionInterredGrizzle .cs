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
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName("an interred grizzle champion corpse")]
	public class ChampionInterredGrizzle : BaseCreature
	{
		[Constructable]
		public ChampionInterredGrizzle () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			ChampionHue();

			Name = "an interred grizzle";
			Body = 259;

			SetStr( 451, 500 );
			SetDex( 201, 250 );
			SetInt( 801, 850 );

			SetHits( 1500 );
			SetStam( 150 );

			SetDamage( 16, 19 );

			SetDamageType( ResistanceType.Physical, 30 );
			SetDamageType( ResistanceType.Fire, 70 );

			SetResistance( ResistanceType.Physical, 35, 55 );
			SetResistance( ResistanceType.Fire, 20, 65 );
			SetResistance( ResistanceType.Cold, 55, 80 );
			SetResistance( ResistanceType.Poison, 20, 35 );
			SetResistance( ResistanceType.Energy, 60, 80 );

			SetSkill(SkillName.Meditation, 77.7, 84.0 );
			SetSkill(SkillName.EvalInt, 72.2, 79.6 );
			SetSkill(SkillName.Magery, 83.7, 89.6);
			SetSkill(SkillName.Poisoning, 0 );
			SetSkill(SkillName.Anatomy, 0 );
			SetSkill( SkillName.MagicResist, 80.2, 87.3 );
			SetSkill( SkillName.Tactics, 104.5, 105.1 );
			SetSkill( SkillName.Wrestling, 105.1, 109.4 );

			Fame = 11000;
			Karma = -11000;

			VirtualArmor = 16;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 120.0;
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

		// TODO: Acid Blood
		/*
		 * Message: 1070820
		 * Spits pool of acid (blood, hue 0x3F), hits lost 6-10 per second/step
		 * Damage is resistable (physical)
		 * Acid last 10 seconds
		 */
		 
		public override int GetAngerSound()
		{
			return 0x581;
		}

		public override int GetIdleSound()
		{
			return 0x582;
		}

		public override int GetAttackSound()
		{
			return 0x580;
		}

		public override int GetHurtSound()
		{
			return 0x583;
		}

		public override int GetDeathSound()
		{
			return 0x584;
		}

		/*
		public override bool OnBeforeDeath()
		{
			SpillAcid( 1, 4, 10, 6, 10 );

			return base.OnBeforeDeath();
		}
		*/

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

		public ChampionInterredGrizzle ( Serial serial ) : base( serial )
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
