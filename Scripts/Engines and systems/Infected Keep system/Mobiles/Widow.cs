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
	[CorpseName( "a widow's corpse" )]
	public class Widow : AuraCreature
	{
		private static Item m_gate;
		public static Item gate { get { return m_gate; } set { m_gate = value; } }
		//public override ChampionSkullType SkullType { get { return ChampionSkullType.Pain; } }

		[Constructable]
		public Widow()
			: base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			gate = null;

			Name = "The Widow";
			Title = "";
			Body = 84;
			CanInfect = true;
			Hue = 1193;

			BaseSoundID = 589;

			SetStr( 1105, 1350 );
			SetDex( 182, 460 );
			SetInt( 1505, 1800 );

			SetHits( 17500 );

			SetDamage( 40, 85 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Poison, 20 );

			SetResistance( ResistanceType.Physical, 75, 85 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 55, 65 );
			SetResistance( ResistanceType.Poison, 70, 90 );
			SetResistance( ResistanceType.Energy, 65, 75 );

			SetSkill( SkillName.EvalInt, 100 );
			SetSkill( SkillName.Magery, 100 );
			SetSkill( SkillName.Meditation, 50 );
			SetSkill( SkillName.Poisoning, 90.4 );
			SetSkill( SkillName.Anatomy, 117.5 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 119.9 );
			SetSkill( SkillName.Wrestling, 119.9 );
			SetSkill( SkillName.Necromancy, 119.9 );
			SetSkill( SkillName.SpiritSpeak, 119.9 );
			SetSkill( SkillName.DetectHidden, 90, 120);

			Fame = 50000;
			Karma = -50000;

			VirtualArmor = 44;
			
			MinAuraDelay = 5;
			MaxAuraDelay = 15;
			MinAuraDamage = 30;
			MaxAuraDamage = 45;
			AuraRange = 2;
			AuraPoison = Poison.Deadly;
			AuraMessage = "The Widow lures you";

		}
		
		public override void OnAfterSpawn()
		{
			
			WidowGate gate = new WidowGate();
			gate.MoveToWorld(new Point3D(1091,1346,0),Map.Ilshenar);
			m_gate = gate;

			WidowsLament.widow = (Mobile)this;

			base.OnAfterSpawn();
			
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 8 );

			base.GenerateLoot();
		}

		public override void OnDeath( Container c )
		{
			WidowsLament.DoCountingWidow = true;

			if (AetherGlobe.invasionstage > 0)
				AetherGlobe.invasionstage = 0;

					switch (Utility.Random( 15 ) )
					{
						case 0:
							c.DropItem(new WidowMorphArms());
							break;
						case 1:
							c.DropItem(new WidowMorphChest());
							break;
						case 2:
							c.DropItem(new WidowMorphGloves());
							break;
						case 3:
							c.DropItem(new WidowMorphGorget());
							break;
						case 4:
							c.DropItem(new WidowMorphHelm());
							break;
						case 5:
							c.DropItem(new WidowMorphLegs());
							break;
					}	

					switch (Utility.Random( 30 ) )
					{
						case 0:
							c.DropItem(new WidowCloak());
							break;

					}	
			if (m_gate != null)
				m_gate.Delete();

				c.DropItem(new InfectionPotion());

			base.OnDeath( c );

			
		}

		public override void OnDelete()
		{
			if (m_gate != null)
				m_gate.Delete();

			base.OnDelete();
		}

		public override void OnThink()
		{
			base.OnThink();

			if (m_gate == null )
			{
				WidowGate gate = new WidowGate();
				gate.MoveToWorld(new Point3D(1091,1346,0),Map.Ilshenar);
				m_gate = gate;
			}

			if (this.Combatant != null )
			{
				if (this.Combatant is BaseCreature)
				{
					BaseCreature mob = (BaseCreature)this.Combatant;
					if (mob.Controlled && mob.ControlMaster is PlayerMobile && !mob.Poisoned)
						mob.ApplyPoison( mob, Poison.Deadly );
				}
			}
	
		}

		public void SendEBolt( Mobile to )
		{
			this.MovingParticles( to, 0x3818, 7, 0, false, true, 0xBE3, 0xFCB, 0x211 );
			to.PlaySound( 0x229 );
			this.DoHarmful( to );
			AOS.Damage( to, this, Utility.RandomMinMax(75, 200), 0, 0, 0, 0, 100 );
		}

		public void SendEBoltOnPet( Mobile to )
		{
			this.MovingParticles( to, 0x3818, 7, 0, false, true, 0xBE3, 0xFCB, 0x211 );
			to.PlaySound( 0x229 );
			this.DoHarmful( to );
			AOS.Damage( to, this, Utility.RandomMinMax( 300, 1500) , 0, 0, 0, 0, 100 );
		}

		public override bool OnBeforeDeath()
		{
			
			if ( !NoKillAwards )
			{

				Map map = this.Map;

				if ( map != null )
				{
					for ( int x = -6; x <= 6; ++x )
					{
						for ( int y = -6; y <= 6; ++y )
						{
							double dist = Math.Sqrt(x*x+y*y);

							if ( dist <= 6 )
								new GoodiesTimer( map, X + x, Y + y ).Start();
						}
					}
				}
			}

			return base.OnBeforeDeath();
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
				bool canFit = m_Map.CanFit( m_X, m_Y, z, 6, false, false );

				for ( int i = -3; !canFit && i <= 3; ++i )
				{
					canFit = m_Map.CanFit( m_X, m_Y, z + i, 6, false, false );

					if ( canFit )
						z += i;
				}

				if ( !canFit )
					return;

				Gold g = new Gold( 350, 750 );
				
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
		
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 5; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override double HitPoisonChance{ get{ return 0.75; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override double AutoDispelChance{ get{ return 0.75; } }
		public override bool CanMoveOverObstacles { get { return true; } }
		public override bool CanDestroyObstacles { get { return true; } }
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }

		public override int BreathPhysicalDamage{ get{ return 50; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 50; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x9C2; } }
		public override int BreathEffectSound{ get{ return 0x665; } }
		public override int BreathEffectItemID{ get{ return 0x3735; } }
		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 14 ); }

		public void DoSpecialAbility( Mobile target )
		{
			if ( target == null || target.Deleted ) //sanity
				return;

			this.Paralyze( TimeSpan.FromSeconds( 1 ) );
			this.Animate( 17, 5, 1, true, false, 0 );

			List<Mobile> mobiles = new List<Mobile>();
			Point3D point;

			foreach ( Mobile m in this.Map.GetMobilesInRange( this.Location, 14 ) )
			{
				if ( m != this && Ability.CanTarget( this, m, true, false, false ) )
					mobiles.Add( m );
			}

			for ( int i = 0; i < mobiles.Count; i++ )
			{
				Mobile m = mobiles[i];

				if ( Utility.Random( 5 ) == 0 )
				{
					Effects.SendBoltEffect( m );
					AOS.Damage( m, this, Utility.RandomMinMax( 50, 200 ), 0, 0, 0, 0, 100 );
					m.SendMessage("You get hit by a lightning bolt");
				}
				else
				{
					point = Ability.RandomCloseLocation( this, 1 );

					if ( this.Location == point )
					{
						AOS.Damage( this, this, Utility.RandomMinMax( 50, 200 ), 0, 0, 0, 0, 100 );
						Effects.SendBoltEffect( this );
					}
					else
						Effects.SendBoltEffect( new Entity( Serial.Zero, point, this.Map ) );
				}
			}

			//if ( 0.25 >= Utility.RandomDouble() ) // 25% chance
				// ADD ability here
		}

		public override void OnDamagedBySpell( Mobile from )
		{

			if (from.Hidden && from is PlayerMobile)
				from.RevealingAction();

			if( from != null && from.Alive && 0.4 > Utility.RandomDouble() )
			{
				if (from is BaseCreature)
					SendEBoltOnPet( from );
				else
					SendEBolt( from );
			}

			DoSpecialAbility( from );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{

            if ( Utility.RandomMinMax( 1, 4 ) == 1 )
            {
                int goo = 0;

                string Goo = "Poison Splash";
                int Color = 0x3F;

                foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == Goo ){ goo++; } }

                if ( goo == 0 )
                {
                    MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, Goo, Color, 0 );
                }
            }
			DoSpecialAbility( attacker );

			if( attacker != null && attacker.Alive && attacker.Weapon is BaseRanged && 0.4 > Utility.RandomDouble() )
			{
				if (attacker is BaseCreature)
					SendEBoltOnPet( attacker );
				else
					SendEBolt( attacker );
			}

			base.OnGotMeleeAttack( attacker );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if( Utility.RandomDouble() < 0.1 )
				DropOoze();

			base.OnDamage( amount, from, willKill );
		}

		public Widow( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)1 ); // version
			writer.Write( (Item) m_gate);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			if (version >= 1)
				m_gate = reader.ReadItem();
		}

		private DateTime m_NextDrop = DateTime.UtcNow;

		public virtual void DropOoze()
		{
			int amount = Utility.RandomMinMax( 1, 3 );
			bool corrosive = Utility.RandomBool();

			for( int i = 0; i < amount; i++ )
			{
				Item ooze = new StainedOoze( corrosive );
				Point3D p = new Point3D( Location );

				for( int j = 0; j < 5; j++ )
				{
					p = GetSpawnPosition( 2 );
					bool found = false;

					foreach( Item item in Map.GetItemsInRange( p, 0 ) )
						if( item is StainedOoze )
						{
							found = true;
							break;
						}

					if( !found )
						break;
				}

				ooze.MoveToWorld( p, Map );
			}

			if( Combatant != null )
			{
				if( corrosive )
					Combatant.SendLocalizedMessage( 1072071 ); // A corrosive gas seeps out of your enemy's skin!
				else
					Combatant.SendLocalizedMessage( 1072072 ); // A poisonous gas seeps out of your enemy's skin!
			}
		}

		private int RandomPoint( int mid )
		{
			return ( mid + Utility.RandomMinMax( -2, 2 ) );
		}

		public virtual Point3D GetSpawnPosition( int range )
		{
			return GetSpawnPosition( Location, Map, range );
		}

		public virtual Point3D GetSpawnPosition( Point3D from, Map map, int range )
		{
			if( map == null )
				return from;

			Point3D loc = new Point3D( ( RandomPoint( X ) ), ( RandomPoint( Y ) ), Z );

			loc.Z = Map.GetAverageZ( loc.X, loc.Y );

			return loc;
		}
	}


}
