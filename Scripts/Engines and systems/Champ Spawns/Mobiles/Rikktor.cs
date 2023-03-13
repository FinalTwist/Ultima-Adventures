using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Spells;
using Server.Engines.CannedEvil;

namespace Server.Mobiles
{
	public class Rikktor : BaseChampion
	{
		public override ChampionSkullType SkullType{ get{ return ChampionSkullType.Power; } }

		public override Type[] UniqueList{ get{ return new Type[] { typeof( CrownOfTalKeesh ) }; } }
		public override Type[] SharedList{ get{ return  new Type[] { 	typeof( TheMostKnowledgePerson ),
										typeof( BraveKnightOfTheBritannia ),
										typeof( LieutenantOfTheBritannianRoyalGuard ) }; } }
		public override Type[] DecorativeList{ get{ return new Type[] { typeof( LavaTile ),
										typeof( MonsterStatuette ),
										typeof( MonsterStatuette ) }; } }

		public override MonsterStatuetteType[] StatueTypes{ get{ return new MonsterStatuetteType[] { 	MonsterStatuetteType.OphidianArchMage,
														MonsterStatuetteType.OphidianWarrior }; } }

		[Constructable]
		public Rikktor() : base( AIType.AI_Melee )
		{
			Body = 766;
			Name = "Rikktor";

			SetStr( 701, 900 );
			SetDex( 201, 350 );
			SetInt( 51, 100 );

			SetHits( 5500 );
			SetStam( 250, 650 );

			SetDamage( 35, 75 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Fire, 50 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 80, 90 );
			SetResistance( ResistanceType.Energy, 80, 90 );

			SetSkill( SkillName.Anatomy, 100.0 );
			SetSkill( SkillName.MagicResist, 140.2, 160.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 120.0 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 130;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 4 );
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override ScaleType ScaleType{ get{ return ScaleType.All; } }
		public override int Scales{ get{ return 20; } }

		public override void OnGotMeleeAttack( Mobile defender )
		{
			base.OnGotMeleeAttack( defender );

			if (defender != null && Utility.RandomDouble() < 0.25 )
			{
				if ( this.GetDistanceToSqrt( defender ) < 2 && Utility.RandomDouble() < 0.25)
					Earthquake();

					if (defender is PlayerMobile )
						defender.SendMessage( 0, "The creature blasts liquid fire towards you!" );
					Effects.SendLocationEffect( defender.Location, defender.Map, 0x3709, 30, 10 );
					defender.PlaySound( 0x208 );

					Timer.DelayCall( TimeSpan.FromSeconds( 1 ), new TimerStateCallback ( Blast ), new object[]{ defender.Location, defender.Map, defender.X, defender.Y } );
				
			}
		}

		public override void AlterDamageScalarFrom( Mobile caster, ref double scalar )
		{
			base.AlterDamageScalarFrom( caster, ref scalar );

			if (caster != null && Utility.RandomDouble() < 0.20 )
			{
				if ( this.GetDistanceToSqrt( caster ) < 2  && Utility.RandomDouble() < 0.25)
					Earthquake();
				
				
					if (caster is PlayerMobile)
						caster.SendMessage( 0, "The creature blasts liquid fire towards you!" );

					Effects.SendLocationEffect( caster.Location, caster.Map, 0x3709, 30, 10 );
					caster.PlaySound( 0x208 );

					Timer.DelayCall( TimeSpan.FromSeconds( 1 ), new TimerStateCallback ( Blast ), new object[]{ caster.Location, caster.Map, caster.X, caster.Y } );
				
			}
		}

		public void Blast( object state )
		{

			if (this.Deleted || this == null)
				return;
				
			object[] states = (object[])state;

			Point3D m = (Point3D)states[0];
			Map map = (Map)states[1];
			int Xpos = (int)states[2];
			int Ypos = (int)states[3];

			if ( map != null )
			{
				for ( int x = -4; x <= 4; ++x )
				{
					for ( int y = -4; y <= 4; ++y )
					{
						double dist = Math.Sqrt(x*x+y*y);

						if ( dist <= 4 )
							new FiresTimer( map, Xpos + x, Ypos + y ).Start();
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
				
				g.MoveToWorld( new Point3D( m_X, m_Y, z ), m_Map );

				if ( 0.5 >= Utility.RandomDouble() )
				{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
							Effects.PlaySound( g, g.Map, 0x208 );
				}
			}
		}

		public void Earthquake()
		{
			Map map = this.Map;

			if ( map == null )
				return;

			ArrayList targets = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 15 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					targets.Add( m );
				else if ( m is PlayerMobile )
					targets.Add( m );
			}

			PlaySound( 0x63E );

			for ( int i = 0; i < targets.Count; ++i )
			{
				Mobile m = (Mobile)targets[i];

				double damage = m.Hits * 0.5;

				if ( damage < 10.0 )
					damage = 10.0;
				//else if ( damage > 75.0 )
				//	damage = 75.0;

				if (m is PlayerMobile)
				{
					m.SendMessage( 0, "The creature emmits a powerful roar!" );

					IMount mount = m.Mount;

					if ( mount != null )
					{
						m.SendLocalizedMessage( 1062315 ); // You fall off your mount!

						m.PlaySound( 0x140 );
						m.FixedParticles( 0x3728, 10, 15, 9955, EffectLayer.Waist );

						Server.Mobiles.EtherealMount.EthyDismount( m, true );
						mount.Rider = null;

						BaseMount.SetMountPrevention( m, BlockMountType.Dazed, TimeSpan.FromSeconds(10) );
					}
				}

				DoHarmful( m );

				AOS.Damage( m, this, (int)damage, 100, 0, 0, 0, 0 );

				if ( m.Alive && m.Body.IsHuman && !m.Mounted )
					m.Animate( 20, 7, 1, true, false, 0 ); // take hit	
			}
		}

		public override int GetAngerSound()
		{
			return Utility.Random( 0x2CE, 2 );
		}

		public override int GetIdleSound()
		{
			return 0x2D2;
		}

		public override int GetAttackSound()
		{
			return Utility.Random( 0x2C7, 5 );
		}

		public override int GetHurtSound()
		{
			return 0x2D1;
		}

		public override int GetDeathSound()
		{
			return 0x2CC;
		}

		public Rikktor( Serial serial ) : base( serial )
		{
		}

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