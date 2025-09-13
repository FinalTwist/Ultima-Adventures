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
	[CorpseName( "a revenant lion champion corpse" )]
	public class ChampionRevenantLion : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public ChampionRevenantLion() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			ChampionHue();

			Name = "a Revenant Lion";
			Body = 251;

			SetStr( 276, 325 );
			SetDex( 156, 175 );
			SetInt( 76, 105 );

			SetHits( 251, 280 );

			SetDamage( 18, 24 );

			SetDamageType( ResistanceType.Physical, 30 );
			SetDamageType( ResistanceType.Cold, 30 );
			SetDamageType( ResistanceType.Poison, 10 );
			SetDamageType( ResistanceType.Energy, 30 );

			SetResistance( ResistanceType.Physical, 40, 60 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 55, 65 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 80.1, 90.0 );
			SetSkill( SkillName.Magery, 80.1, 90.0 );
			SetSkill( SkillName.Poisoning, 120.1, 130.0 );
			SetSkill( SkillName.MagicResist, 70.1, 90.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 80.1, 88.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 16;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 120.0;

			PackNecroReg( 6, 8 );
		}

		public override int GetAngerSound()
		{
			return 0x518;
		}

		public override int GetIdleSound()
		{
			return 0x517;
		}

		public override int GetAttackSound()
		{
			return 0x516;
		}

		public override int GetHurtSound()
		{
			return 0x519;
		}

		public override int GetDeathSound()
		{
			return 0x515;
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
		public override Poison PoisonImmune{ get{ return Poison.Greater; } }
		public override Poison HitPoison{ get{ return Poison.Greater; } }

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

		public ChampionRevenantLion( Serial serial ) : base( serial )
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
