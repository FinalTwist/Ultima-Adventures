using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Engines.CannedEvil;
using Server.Multis;
using Server.Misc;

namespace Server.Mobiles
{
	public abstract class BaseChampion : BaseCreature
	{
		public override bool CanMoveOverObstacles { get { return true; } }
		public override bool CanDestroyObstacles { get { return true; } }

		private bool m_guardwhacked;
		[CommandProperty( AccessLevel.GameMaster )]
        public bool guardwhacked
        {
            get{ return m_guardwhacked; }
            set{ m_guardwhacked = value; }
        }

		public BaseChampion( AIType aiType ) : this( aiType, FightMode.Weakest )
		{
		}

		public BaseChampion( AIType aiType, FightMode mode ) : base( aiType, mode, 18, 1, 0.1, 0.2 )
		{
			AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
			m_guardwhacked = false;
		}

		public BaseChampion( Serial serial ) : base( serial )
		{
		}

		public abstract ChampionSkullType SkullType{ get; }

		public abstract Type[] UniqueList{ get; }
		public abstract Type[] SharedList{ get; }
		public abstract Type[] DecorativeList{ get; }
		public abstract MonsterStatuetteType[] StatueTypes{ get; }

		public virtual bool NoGoodies{ get{ return false; } }

		public override void OnThink()
		{
			base.OnThink();

			if (Combatant != null)
			{
				BaseHouse house = BaseHouse.FindHouseAt( Combatant.Location, Combatant.Map, 16 );
				if (house != null)
				{
					Combatant.Hits = 1;
					Combatant.MoveToWorld(Location, Map);
				}
			}

			if ( this.Hits <= (this.HitsMax /2) ) 
				AIFullSpeedActive = AIFullSpeedPassive = false; // Force full speed
			if ( this.Hits > (this.HitsMax /2) && !AIFullSpeedActive ) 
				AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed

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
			AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
		}

		public Item GetArtifact()
		{
			double random = Utility.RandomDouble();
			if ( 0.05 >= random )
				return CreateArtifact( UniqueList );
			else if ( 0.15 >= random )
				return CreateArtifact( SharedList );
			else if ( 0.30 >= random )
				return CreateArtifact( DecorativeList );
			return null;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
		}

		public Item CreateArtifact( Type[] list )
		{
			if( list.Length == 0 )
				return null;

			int random = Utility.Random( list.Length );
			
			Type type = list[random];

			Item artifact = Loot.Construct( type );

			if( artifact is MonsterStatuette && StatueTypes.Length > 0 )
			{
				((MonsterStatuette)artifact).Type = StatueTypes[Utility.Random( StatueTypes.Length )];
				((MonsterStatuette)artifact).LootType = LootType.Regular;
			}

			return artifact;
		}

		private PowerScroll CreateRandomPowerScroll( bool fake)
		{

			int level = 5;
			double random = Utility.RandomDouble();

			if ( fake || this is AbyssalInfernal || this is PrimevalLich ) 
				random -= 0.25;

			if (m_guardwhacked)
				random -= 0.09;
				
			if ( random >= 0.96 )
				level = 25; // 4%
			else if ( random >= 0.92)
				level = 20; // 8%
			else if ( random >= 0.84)
				level = 15;	//16%
			else if ( random >= 0.50)
				level = 10;	 //50%

			if (random < 0)
				PowerScroll.CreateRandomNoCraft( 1, 1 );	

			return PowerScroll.CreateRandomNoCraft( level, level );	
		}

		public override bool IsEnemy( Mobile m)
		{
			if (m is PlayerMobile && m.AccessLevel == AccessLevel.Player)
				return true;
			if (m is BaseCreature && ( ((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned) )
				return true;
			
			return false;
		}

		public void GivePowerScrolls()
		{
			//if ( Map != Map.Felucca )
			//	return;

			List<Mobile> toGive = new List<Mobile>();
			List<DamageStore> rights = BaseCreature.GetLootingRights( this.DamageEntries, this.HitsMax );

			for ( int i = rights.Count - 1; i >= 0; --i )
			{
				DamageStore ds = rights[i];

				if ( ds.m_HasRight )
					toGive.Add( ds.m_Mobile );
			}

			if ( toGive.Count == 0 )
				return;

			for( int i = 0; i < toGive.Count; i++ )
			{
				Mobile m = toGive[i];

				if( !(m is PlayerMobile) )
					continue;

				bool gainedPath = false;

				int pointsToGain = 800;

				if( VirtueHelper.Award( m, VirtueName.Valor, pointsToGain, ref gainedPath ) )
				{
					if( gainedPath )
						m.SendLocalizedMessage( 1054032 ); // You have gained a path in Valor!
					else
						m.SendLocalizedMessage( 1054030 ); // You have gained in Valor!

					//No delay on Valor gains
				}
			}

			// Randomize
			for ( int i = 0; i < toGive.Count; ++i )
			{
				int rand = Utility.Random( toGive.Count );
				Mobile hold = toGive[i];
				toGive[i] = toGive[rand];
				toGive[rand] = hold;
			}

			Region region = Region.Find( this.Location, this.Map );
			int number = Utility.RandomMinMax(0, 1);

			bool fake = false;

			if ( region.IsPartOf( typeof( ChampionSpawnRegion ) ) || region is ChampionSpawnRegion  ) 
				number = Utility.RandomMinMax( 1, 4);
			else 
				fake = true;

			for ( int i = 0; i < number; ++i )
			{
				Mobile m = toGive[i % toGive.Count];

                int amount = 1;
                if (m is PlayerMobile && GetPlayerInfo.LuckyPlayer(m.Luck, m))
                    amount += Utility.RandomMinMax(1, 2);

                for (int j = 0; j < amount; j++)
                {
                    PowerScroll ps = CreateRandomPowerScroll(fake);
                    if (ps != null)
                        GivePowerScrollTo(m, ps);
                }
			}
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if (attacker is BlueGuard || attacker is BaseCursed )
				m_guardwhacked = true;

		}

		public static void GivePowerScrollTo( Mobile m, PowerScroll ps )
		{
			if( ps == null || m == null )	//sanity
				return;

			m.SendLocalizedMessage( 1049524 ); // You have received a scroll of power!

			if( !Core.SE || m.Alive )
				m.AddToBackpack( ps );
			else
			{
				if( m.Corpse != null && !m.Corpse.Deleted )
					m.Corpse.DropItem( ps );
				else
					m.AddToBackpack( ps );
			}

			if( m is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)m;

				for( int j = 0; j < pm.JusticeProtectors.Count; ++j )
				{
					Mobile prot = pm.JusticeProtectors[j];

					if( prot.Map != m.Map || prot.Kills >= 5 || prot.Criminal || !JusticeVirtue.CheckMapRegion( m, prot ) )
						continue;

					int chance = 0;

					switch( VirtueHelper.GetLevel( prot, VirtueName.Justice ) )
					{
						case VirtueLevel.Seeker: chance = 60; break;
						case VirtueLevel.Follower: chance = 80; break;
						case VirtueLevel.Knight: chance = 100; break;
					}

					if( chance > Utility.Random( 100 ) )
					{
						PowerScroll powerScroll = new PowerScroll( ps.Skill, ps.Value );

						prot.SendLocalizedMessage( 1049368 ); // You have been rewarded for your dedication to Justice!

						if( !Core.SE || prot.Alive )
							prot.AddToBackpack( powerScroll );
						else
						{
							if( prot.Corpse != null && !prot.Corpse.Deleted )
								prot.Corpse.DropItem( powerScroll );
							else
								prot.AddToBackpack( powerScroll );
						}
					}
				}
			}
		}

		public override bool OnBeforeDeath()
		{
			if ( !NoKillAwards )
			{
				GivePowerScrolls();

				if( NoGoodies )
					return base.OnBeforeDeath();

				Map map = this.Map;

				if ( map != null )
				{
					int range = 2;
					Region region = Region.Find( this.Location, this.Map );
					if ( region.IsPartOf( typeof( ChampionSpawnRegion ) ) || region is ChampionSpawnRegion  ) 
						range = 4;

					for ( int x = -range; x <= range; ++x )
					{
						for ( int y = -range; y <= range; ++y )
						{
							double dist = Math.Sqrt(x*x+y*y);

							if ( dist <= range )
								new GoodiesTimer( map, X + x, Y + y ).Start();
						}
					}
				}
			}

			return base.OnBeforeDeath();
		}

		public override void OnDeath( Container c )
		{
			//if ( Map == Map.Felucca )
			{
				//TODO: Confirm SE change or AoS one too?
				List<DamageStore> rights = BaseCreature.GetLootingRights( this.DamageEntries, this.HitsMax );
				List<Mobile> toGive = new List<Mobile>();

				for ( int i = rights.Count - 1; i >= 0; --i )
				{
					DamageStore ds = rights[i];

					if ( ds.m_HasRight )
						toGive.Add( ds.m_Mobile );
				}

				if ( toGive.Count > 0 )
					toGive[Utility.Random( toGive.Count )].AddToBackpack( new ChampionSkull( SkullType ) );
				else
					c.DropItem( new ChampionSkull( SkullType ) );
			}

			base.OnDeath( c );
		}

		private class GoodiesTimer : Timer
		{
			private Map m_Map;
			private int m_X, m_Y;

			public GoodiesTimer( Map map, int x, int y ) : base( TimeSpan.FromSeconds( Utility.RandomDouble() * 10.0 ) )
			{
				m_Map = map;
				m_X = x;
				m_Y = y;
			}

			protected override void OnTick()
			{
				int z = m_Map.GetAverageZ( m_X, m_Y );
				bool canFit = m_Map.CanFit( m_X, m_Y, z, 5, false, false );

				for ( int i = -3; !canFit && i <= 5; ++i )
				{
					canFit = m_Map.CanFit( m_X, m_Y, z + i, 5, false, false );

					if ( canFit )
						z += i;
				}

				if ( !canFit )
					return;

				Gold g = new Gold( 150, 250 );
				
				g.MoveToWorld( new Point3D( m_X, m_Y, z ), m_Map );

				if ( 0.5 >= Utility.RandomDouble() )
				{
					switch ( Utility.Random( 3 ) )
					{
						case 0: // Fire column
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
							Effects.PlaySound( g, g.Map, 0x208 );

							break;
						}
						case 1: // Explosion
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x36BD, 20, 10, 5044 );
							Effects.PlaySound( g, g.Map, 0x307 );

							break;
						}
						case 2: // Ball of fire
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x36FE, 10, 10, 5052 );

							break;
						}
					}
				}
			}
		}
	}
}
