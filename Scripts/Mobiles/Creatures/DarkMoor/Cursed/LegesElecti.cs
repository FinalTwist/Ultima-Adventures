using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Spells;
using Server.Network;
using System.Collections.Generic;
using Server.Engines.CannedEvil;

namespace Server.Mobiles
{
	[CorpseName( "an evil corpse" )]
	public class LegesElecti : BaseCursed
	{

		private int m_modded;
		[CommandProperty( AccessLevel.GameMaster )]
        public int modded
        {
            get{ return m_modded; }
            set{ m_modded = value; }
        }

		private Neira m_mother;
        public Neira mother
        {
            get{ return m_mother; }
            set{ m_mother = value; }
        }

		[Constructable]
		public LegesElecti( Neira momma)
			: base( AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{
			Name = "The Leges Electi";
			Title = "";
			Body = 93;
			Hue = 1979;

			m_modded = 0;
			mother = momma;

			//BaseSoundID = 589;

			SetStr( 450, 550 );
			SetDex( 82, 160 );
			SetInt( 350, 450 );

			SetHits( 800 );

			SetDamage( 21, 32 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Cold, 40 );

			int final = 20 + m_modded;

			SetResistance( ResistanceType.Physical, final, (5+ final) );
			SetResistance( ResistanceType.Fire, final, (5+ final) );
			SetResistance( ResistanceType.Cold, final, (5+ final)  );
			SetResistance( ResistanceType.Poison, final, (5+ final)  );
			SetResistance( ResistanceType.Energy, final, (5+ final)  );

			SetSkill( SkillName.EvalInt, 100.5 );
			SetSkill( SkillName.Magery, 100.5 );
			SetSkill( SkillName.Meditation, 90 );
			SetSkill( SkillName.Anatomy, 100.5 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 119.9 );
			SetSkill( SkillName.Wrestling, 100.9 );
			SetSkill( SkillName.Necromancy, 100.9 );
			SetSkill( SkillName.SpiritSpeak, 119.9 );

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 64;
			
			MinAuraDelay = 3;
			MaxAuraDelay = 9;
			MinAuraDamage = 10;
			MaxAuraDamage = 20;
			AuraRange = 2;
			//AuraPoison = Poison.Greater;
			AuraMessage = "The void beckons...";
			AuraType = ResistanceType.Cold;

		}
		

		public override void OnAfterSpawn()
		{

			base.OnAfterSpawn();
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 8 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			mother.PassDamage(this, 0, true);
			Timer.DelayCall( TimeSpan.FromSeconds( 1 ), new TimerStateCallback ( Blast ), new object[]{ this.Location, this.Map } );
		}

		public void Tele( object state )
		{

			if (this.Deleted || this == null)
				return;
				
			object[] states = (object[])state;

			Neira target = (Neira)states[0];

			Map map = target.Map;

			if ( map == null || map == Map.Internal || target == null )
				return;
			
			this.MoveToWorld( target.Location, target.Map);
		}

		public void Blast( object state )
		{
				
			object[] states = (object[])state;

			Point3D m = (Point3D)states[0];
			Map map = (Map)states[1];

			if ( map != null )
			{
				for ( int x = -3; x <= 3; ++x )
				{
					for ( int y = -3; y <= 3; ++y )
					{
						double dist = Math.Sqrt(x*x+y*y);

						if ( dist <= 3 )
							new FiresTimer( map, X + x, Y + y ).Start();
					}
				}
			}
		}

		private class FiresTimer : Timer
		{
			private Map m_Map;
			private int m_X, m_Y;

			public FiresTimer( Map map, int x, int y ) : base( TimeSpan.FromSeconds( 0.15 ) )
			{
				m_Map = map;
				m_X = x;
				m_Y = y;
			}

			protected override void OnTick()
			{
				int z = m_Map.GetAverageZ( m_X, m_Y );
				bool canFit = m_Map.CanFit( m_X, m_Y, z, 6, false, false );

				for ( int i = -3; !canFit && i <= 3; ++i )
				{
					canFit = m_Map.CanFit( m_X, m_Y, z + i, 6, false, false );

					if ( canFit )
						z += i;
				}

				if ( !canFit )
					return;

				BurningFire g = new BurningFire();
				g.Hue = 1;
				
				g.MoveToWorld( new Point3D( m_X, m_Y, z ), m_Map );

				if ( 0.5 >= Utility.RandomDouble() )
				{
							//Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
							Effects.PlaySound( g, g.Map, 0x208 );
				}
			}
		}

		public override void OnThink()
		{
			m_modded += (int)(60*( (800 - (double)Hits )/800));		

			int final = 30 + m_modded;

			//if (ResistanceType.Physical != final)
				SetResistance( ResistanceType.Physical, final );
			//if (ResistanceType.Fire != final)
				SetResistance( ResistanceType.Fire, final );
			//if (ResistanceType.Cold != final)
				SetResistance( ResistanceType.Cold, final );
			//if (ResistanceType.Poison != final)
				SetResistance( ResistanceType.Poison, final  );
			//if (ResistanceType.Energy != final)
				SetResistance( ResistanceType.Energy, final  );

			if (m_mother == null || !m_mother.Alive)
			{
				this.Kill();
				return;
			}

			if (this.GetDistanceToSqrt( m_mother ) > 15)
			{
				Say("Kal Ort Por");
				Timer.DelayCall( TimeSpan.FromSeconds( 2 ), new TimerStateCallback ( Tele ), new object[]{ m_mother } );
			}
				

			base.OnThink();
		}

		public override bool OnBeforeDeath()
		{

			return base.OnBeforeDeath();
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if (this == m_mother.chosenelecti)
				m_mother.PassDamage(this, amount, false);

			base.OnDamage( amount, from, willKill);
		}

		public LegesElecti( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

		}
	}
}
