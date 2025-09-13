using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a hiryu corpse" )]
	public class Hiryu : BaseMount
	{
		public override double WeaponAbilityChance { get { return 0.07; } } /* 1 in 15 chance of using per landed hit */

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.Dismount;
		}

		public override bool StatLossAfterTame { get { return true; } }

		private static int GetHue()
		{
			int rand = Utility.Random( 1075 );

			/*
		  1000	1075	No Hue Color	93.02%	0x0
			 * 
			10	1075	Ice Green    	0.93%	0x847F
			10	1075	Light Blue    	0.93%	0x848D
			10	1075	Strong Cyan		0.93%	0x8495
			10	1075	Agapite			0.93%	0x8899
			10	1075	Gold			0.93%	0x8032
			 * 
			8	1075	Blue and Yellow	0.74%	0x8487
			 * 
			5	1075	Ice Blue       	0.47%	0x8482
			 * 
			3	1075	Cyan			0.28%	0x8123
			3	1075	Light Green		0.28%	0x8295
			 * 
			2	1075	Strong Yellow	0.19%	0x8037
			2	1075	Green			0.19%	0x8030	//this one is an approximation
			 * 
			1	1075	Strong Purple	0.09%	0x8490
			1	1075	Strong Green	0.09%	0x855C
			 * */

			if( rand <= 0 )
				return 0x855C;
			else if( rand <= 1 )
				return 0x8490;
			else if( rand <= 3 )
				return 0x8030;
			else if( rand <= 5 )
				return 0x8037;
			else if( rand <= 8 )
				return 0x8295;
			else if( rand <= 11 )
				return 0x8123;
			else if( rand <= 16 )
				return 0x8482;
			else if( rand <= 24 )
				return 0x8487;
			else if( rand <= 34 )
				return 0x8032;
			else if( rand <= 44 )
				return 0x8899;
			else if( rand <= 54 )
				return 0x8495;
			else if( rand <= 64 )
				return 0x848D;
			else if( rand <= 74 )
				return 0x847F;
			

			return 0;
		}

		[Constructable]
		public Hiryu()
			: base( "a hiryu", 243, 0x3E94, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Hue = GetHue();

			SetStr( 1201, 1410 );
			SetDex( 171, 270 );
			SetInt( 301, 325 );

			SetHits( 901, 1100 );
			SetMana( 60 );

			SetDamage( 20, 30 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 70 );
			SetResistance( ResistanceType.Fire, 70, 90 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 75.1, 80.0 );
			SetSkill( SkillName.MagicResist, 85.1, 100.0 );
			SetSkill( SkillName.Tactics, 100.1, 110.0 );
			SetSkill( SkillName.Wrestling, 100.1, 120.0 );

			Fame = 18000;
			Karma = -18000;

			Tamable = true;
			ControlSlots = 4;
			MinTameSkill = 98.7;

			if( Utility.RandomDouble() < .33 )
				PackItem( Engines.Plants.Seed.RandomBonsaiSeed() );

			if ( Core.ML && Utility.RandomDouble() < .33 )
				PackItem( Engines.Plants.Seed.RandomPeculiarSeed(3) );
		}


		public override int GetAngerSound()
		{
			return 0x4FE;
		}

		public override int GetIdleSound()
		{
			return 0x4FD;
		}

		public override int GetAttackSound()
		{
			return 0x4FC;
		}

		public override int GetHurtSound()
		{
			return 0x4FF;
		}

		public override int GetDeathSound()
		{
			return 0x4FB;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
			AddLoot( LootPack.Gems, 4 );
		}

		public override int TreasureMapLevel { get { return 5; } }
		public override int Meat { get { return 16; } }
		public override int Hides { get { return 60; } }
		public override FoodType FavoriteFood { get { return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if( 0.1 > Utility.RandomDouble() )
			{
				/* Grasping Claw
				 * Start cliloc: 1070836
				 * Effect: Physical resistance -15% for 5 seconds
				 * End cliloc: 1070838
				 * Effect: Type: "3" - From: "0x57D4F5B" (player) - To: "0x0" - ItemId: "0x37B9" - ItemIdName: "glow" - FromLocation: "(1149 808, 32)" - ToLocation: "(1149 808, 32)" - Speed: "10" - Duration: "5" - FixedDirection: "True" - Explode: "False"
				 */

				ExpireTimer timer = (ExpireTimer)m_Table[defender];

				if( timer != null )
				{
					timer.DoExpire();
					defender.SendLocalizedMessage( 1070837 ); // The creature lands another blow in your weakened state.
				}
				else
					defender.SendLocalizedMessage( 1070836 ); // The blow from the creature's claws has made you more susceptible to physical attacks.

				int effect = -(defender.PhysicalResistance * 15 / 100);

				ResistanceMod mod = new ResistanceMod( ResistanceType.Physical, effect );

				defender.FixedEffect( 0x37B9, 10, 5 );
				defender.AddResistanceMod( mod );

				timer = new ExpireTimer( defender, mod, TimeSpan.FromSeconds( 5.0 ) );
				timer.Start();
				m_Table[defender] = timer;
			}
		}

		private static Hashtable m_Table = new Hashtable();

		private class ExpireTimer : Timer
		{
			private Mobile m_Mobile;
			private ResistanceMod m_Mod;

			public ExpireTimer( Mobile m, ResistanceMod mod, TimeSpan delay )
				: base( delay )
			{
				m_Mobile = m;
				m_Mod = mod;
				Priority = TimerPriority.TwoFiftyMS;
			}

			public void DoExpire()
			{
				m_Mobile.RemoveResistanceMod( m_Mod );
				Stop();
				m_Table.Remove( m_Mobile );
			}

			protected override void OnTick()
			{
				m_Mobile.SendLocalizedMessage( 1070838 ); // Your resistance to physical attacks has returned.
				DoExpire();
			}
		}

		public Hiryu( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)2 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if( version == 0 )
				Timer.DelayCall( TimeSpan.Zero, delegate { Hue = GetHue(); } );

			if( version <= 1 )
				Timer.DelayCall( TimeSpan.Zero, delegate { if( InternalItem != null ) { InternalItem.Hue = this.Hue; } } );

			if( version < 2 )
			{
				for ( int i = 0; i < Skills.Length; ++i )
				{
					Skills[i].Cap = Math.Max( 100.0, Skills[i].Cap * 0.9 );

					if ( Skills[i].Base > Skills[i].Cap )
					{
						Skills[i].Base = Skills[i].Cap;
					}
				}
			}
		}
	}
}
