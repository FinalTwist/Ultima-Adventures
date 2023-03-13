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
using Server.Network;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a kappa champion corpse" )]
	public class ChampionKappa : BaseCreature
	{
		[Constructable]
		public ChampionKappa() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			ChampionHue();

			Name = "a kappa";
			Body = 240;

			SetStr( 186, 230 );
			SetDex( 51, 75 );
			SetInt( 41, 55 );

			SetMana ( 30 );

			SetHits( 151, 180 );

			SetDamage( 6, 12 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 50 );
			SetResistance( ResistanceType.Fire, 35, 50 );
			SetResistance( ResistanceType.Cold, 25, 50 );
			SetResistance( ResistanceType.Poison, 35, 50 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.MagicResist, 60.1, 70.0 );
			SetSkill( SkillName.Tactics, 79.1, 89.0 );
			SetSkill( SkillName.Wrestling, 60.1, 70.0 );

			Fame = 1700;
			Karma = -1700;

			VirtualArmor = 16;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 120.0;

			PackItem( new RawFishSteak( 3 ) );
			for( int i = 0; i < 2; i++ )
			{
				switch ( Utility.Random( 6 ) )
				{
					case 0: PackItem( new Gears() ); break;
					case 1: PackItem( new Hinge() ); break;
					case 2: PackItem( new Axle() ); break;
				}
			}
			if ( Core.ML && Utility.RandomDouble() < .33 )
				PackItem( Engines.Plants.Seed.RandomPeculiarSeed(4) );
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
			return 0x50B;
		}

		public override int GetIdleSound()
		{
			return 0x50A;
		}

		public override int GetAttackSound()
		{
			return 0x509;
		}

		public override int GetHurtSound()
		{
			return 0x50C;
		}

		public override int GetDeathSound()
		{
			return 0x508;
		}

		public override void OnGaveMeleeAttack(Mobile defender)
 		{
			base.OnGaveMeleeAttack (defender);

			if ( Utility.RandomBool() )
			{
				if ( !IsBeingDrained( defender ) && Mana > 14)
				{
					defender.SendLocalizedMessage( 1070848 ); // You feel your life force being stolen away.
					BeginLifeDrain( defender, this );
					Mana-=15;
				}
			}
		}

		private static Hashtable m_Table = new Hashtable();

		public static bool IsBeingDrained( Mobile m )
		{
			return m_Table.Contains( m );
		}

		public static void BeginLifeDrain( Mobile m, Mobile from )
		{
			Timer t = (Timer)m_Table[m];

			if ( t != null )
				t.Stop();

			t = new InternalTimer( from, m );
			m_Table[m] = t;

			t.Start();
		}

		public static void DrainLife( Mobile m, Mobile from )
		{
			if ( m.Alive )
			{
				int damageGiven = AOS.Damage( m, from, 5, 0, 0, 0, 0, 100 );
				from.Hits += damageGiven;
			}
			else
			{
				EndLifeDrain( m );
			}
		}

		public static void EndLifeDrain( Mobile m )
		{
			Timer t = (Timer)m_Table[m];

			if ( t != null )
				t.Stop();

			m_Table.Remove( m );

			m.SendLocalizedMessage( 1070849 ); // The drain on your life force is gone.
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( from != null && from.Map != null )
			{
				int amt=0;
				Mobile target = this; 
				int rand = Utility.Random( 1, 100 );
				if ( willKill )
				{
					amt = ((( rand % 5 ) >> 2 ) + 3);
				} 
				if ( ( Hits < 100 ) && ( rand < 21 ) ) 
				{
					target = ( rand % 2 ) < 1 ? this : from;
					amt++;
				}
				if ( amt > 0 )
				{
					SpillAcid( target, amt );
					from.SendLocalizedMessage( 1070820 ); 
					if ( Mana > 14)
						Mana -= 15;
					amt ^= amt;
				}
			}
			base.OnDamage( amount, from, willKill );
		}

		public override Item NewHarmfulItem()
		{
			return new AcidSlime( TimeSpan.FromSeconds(10), 5, 10 );
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

		public ChampionKappa( Serial serial ) : base( serial )
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

		private class InternalTimer : Timer
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

			protected override void OnTick()
			{
				DrainLife( m_Mobile, m_From );

				if ( ++m_Count == 5 )
					EndLifeDrain( m_Mobile );
			}
		}
	}
}
