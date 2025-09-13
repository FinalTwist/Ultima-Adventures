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
	[CorpseName("a fetid essence champion corpse")]
	public class ChampionFetidEssence : BaseCreature
	{
		[Constructable]
		public ChampionFetidEssence() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			ChampionHue();

			Name = "a fetid essence";
			Body = 273;

			SetStr( 101, 150 );
			SetDex( 210, 250 );
			SetInt( 451, 550 );

			SetHits( 551, 650 );
			
			SetDamage( 21, 25 );

			SetDamageType( ResistanceType.Physical, 30 );
			SetDamageType( ResistanceType.Poison, 70 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 70, 90 );
			SetResistance( ResistanceType.Energy, 75, 80 );

			SetSkill(SkillName.Meditation, 91.4, 99.4 );
			SetSkill(SkillName.EvalInt, 88.5, 92.3 );
			SetSkill(SkillName.Magery, 97.9, 101.7 );
			SetSkill(SkillName.Poisoning, 100 );
			SetSkill(SkillName.Anatomy, 0, 4.5 );
			SetSkill( SkillName.MagicResist, 103.5, 108.8 );
			SetSkill( SkillName.Tactics, 81.0, 84.6 );
			SetSkill( SkillName.Wrestling, 81.3, 83.9 );

			Fame = 3700;  // Guessed
			Karma = -3700;  // Guessed

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
		 
		public override int GetAngerSound()
		{
			return 0x56d;
		}

		public override int GetIdleSound()
		{
			return 0x56b;
		}

		public override int GetAttackSound()
		{
			return 0x56c;
		}

		public override int GetHurtSound()
		{
			return 0x56c;
		}

		public override int GetDeathSound()
		{
			return 0x56e;
		}

		#region Area Damage
		//public override void AreaDamageEffect( Mobile m )
		//{
		//	m.FixedParticles( 0x374A, 10, 15, 5038, 1181, 2, EffectLayer.Head );
		//	m.PlaySound( 0x213 );
		//}
		
		//public override bool CanAreaDamage{ get{ return true; } }
		//public override TimeSpan AreaDamageDelay{ get{ return TimeSpan.FromSeconds( 20 ); } }		
		//public override double AreaDamageScalar{ get{ return 0.5; } }		
		//public override int AreaFireDamage{ get{ return 0; } }
		//public override int AreaColdDamage{ get{ return 100; } }
		#endregion
		
		public override Poison HitPoison{ get{ return Poison.Deadly; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

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

		public ChampionFetidEssence ( Serial serial ) : base( serial )
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

		/*private class InternalTimer : Timer
		{
			private Mobile m_From;
			private Mobile m_Mobile;
			private int m_Count;

			public InternalTimer( Mobile from, Mobile m ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
			{
				m_From = from;
				m_Mobile = m;
				Priority = TimerPriority.TwoFiftyMS;
			}

		}*/
	}
}
