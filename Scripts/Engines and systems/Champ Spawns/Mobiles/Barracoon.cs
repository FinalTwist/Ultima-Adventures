using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using Server.Engines.CannedEvil;
using Server.SkillHandlers;

namespace Server.Mobiles
{
	public class Barracoon : BaseChampion
	{
		public override ChampionSkullType SkullType{ get{ return ChampionSkullType.Greed; } }

		public override Type[] UniqueList{ get{ return new Type[] { typeof( FangOfRactus ) }; } }
		public override Type[] SharedList{ get{ return new Type[] { 	typeof( EmbroideredOakLeafCloak ),
										typeof( DjinnisRing ),
										typeof( DetectiveBoots ),
										typeof( GuantletsOfAnger ) }; } }
		public override Type[] DecorativeList{ get{ return new Type[] { typeof( SwampTile ), typeof( MonsterStatuette ) }; } }

		public override MonsterStatuetteType[] StatueTypes{ get{ return new MonsterStatuetteType[] { MonsterStatuetteType.Slime }; } }

		[Constructable]
		public Barracoon() : base( AIType.AI_Melee )
		{
			Name = "Barracoon";
			Title = "the piper";
			Body = 0x190;
			Hue = 0x83EC;

			SetStr( 305, 425 );
			SetDex( 150, 250 );
			SetInt( 505, 750 );

			SetHits( 5500 );
			SetStam( 250, 450 );

			SetDamage( 45, 75 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 60, 70 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill(SkillName.Swords, 10, 120.0);
			SetSkill(SkillName.Tactics, 105, 120.0);
			SetSkill(SkillName.MagicResist, 105, 120.0);
			SetSkill(SkillName.Tactics, 105, 120.0);
			SetSkill(SkillName.Parry, 105, 120.0);
			SetSkill(SkillName.Anatomy, 85.0, 120.0);
			SetSkill(SkillName.Healing, 85.0, 120.0);
			SetSkill(SkillName.Magery, 85.0, 120.0);
			SetSkill(SkillName.EvalInt, 85.0, 120.0);
			SetSkill(SkillName.Discordance, 95.0, 120.0);

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 70;

			if (Utility.Random(1, 2) == 2) // 50% chance to have an OmniAI skill set
			OmniAI.SetRandomSkillSet(this, 70.0, 110.0);

			AddItem( new FancyShirt( Utility.RandomGreenHue() ) );
			AddItem( new LongPants( Utility.RandomYellowHue() ) );
			AddItem( new JesterHat( Utility.RandomPinkHue() ) );
			AddItem( new Cloak( Utility.RandomPinkHue() ) );
			AddItem( new Sandals() );

			HairItemID = 0x203B; // Short Hair
			HairHue = 0x94;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 3 );
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override double AutoDispelChance{ get{ return 1.0; } }
		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool Uncalmable{ get{ return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

		public override bool ShowFameTitle{ get{ return false; } }
		public override bool ClickTitle{ get{ return false; } }

		// + OmniAI support +
		protected override BaseAI ForcedAI
		{
			get
			{
			return new OmniAI(this);
			}
		}
		// - OmniAI support -

		public void Polymorph(Mobile m)
		{
			Polymorph (m, false, null);
		}

		public void Polymorph( Mobile m, bool switchs, Mobile to )
		{
			if ( !m.CanBeginAction( typeof( PolymorphSpell ) ) || !m.CanBeginAction( typeof( IncognitoSpell ) ) || m.IsBodyMod )
				return;

			IMount mount = m.Mount;

			if ( mount != null )
				mount.Rider = null;

			if ( m.Mounted )
				return;

			if ( m.BeginAction( typeof( PolymorphSpell ) ) )
			{
				if ( !(m is Barracoon) )
				{
					Item disarm = m.FindItemOnLayer( Layer.OneHanded );

					if ( disarm != null && disarm.Movable )
						m.AddToBackpack( disarm );

					disarm = m.FindItemOnLayer( Layer.TwoHanded );

					if ( disarm != null && disarm.Movable )
						m.AddToBackpack( disarm );
				}

				if (switchs && to != null)
				{
					m.BodyMod = to.Body;
					to.BodyMod = m.Body;
					new ExpirePolymorphTimer( m, to ).Start();
				}
				else
				{	
					m.BodyMod = 42;
					m.HueMod = 0;

					new ExpirePolymorphTimer( m, null ).Start();
				}
			}

            if (m is PlayerMobile || ( m is BaseCreature && ((BaseCreature)m).Controlled ) )
            {
                TimeSpan duration = TimeSpan.FromMinutes( 3.0 );
                ResistanceMod[] mods = 
                {
                    new ResistanceMod( ResistanceType.Physical, ( 0 - (int)((double)m.GetResistance(ResistanceType.Physical) * 0.5 ) ) ),
                    new ResistanceMod( ResistanceType.Cold, ( 0 - (int)((double)m.GetResistance(ResistanceType.Cold) * 0.5 ) ) ),
                    new ResistanceMod( ResistanceType.Poison, ( 0 - (int)((double)m.GetResistance(ResistanceType.Poison) * 0.5 ) ) ),
                    new ResistanceMod( ResistanceType.Energy, ( 0 - (int)((double)m.GetResistance(ResistanceType.Energy) * 0.5 ) ) ),
                    new ResistanceMod( ResistanceType.Fire, ( 0 - (int)((double)m.GetResistance(ResistanceType.Fire) * 0.5 ) ) )
                };
                TimedResistanceMod.AddMod(m, "Discordance", mods, duration);
				
                m.AddStatMod(new StatMod(StatType.Str, "DiscordanceStr", ( 0 - (int)((double)m.RawStr * 0.5 ) ), duration));
                m.AddStatMod(new StatMod(StatType.Int, "DiscordanceInt", ( 0 - (int)((double)m.RawInt * 0.5 ) ), duration));
                m.AddStatMod(new StatMod(StatType.Dex, "DiscordanceDex",  ( 0 - (int)((double)m.RawDex * 0.5 ) ), duration));
                new DiscordEffectTimer(m, duration).Start();
            }

		}

        public class DiscordEffectTimer : Timer
        {
            public Mobile Mob;
            public int Count;
            public int MaxCount;

            public DiscordEffectTimer(Mobile mob, TimeSpan duration)
                : base(TimeSpan.FromSeconds(1.25), TimeSpan.FromSeconds(1.25))
            {
                this.Mob = mob;
                this.Count = 0;
                this.MaxCount = (int)((double)duration.TotalSeconds / 1.25);
            }

            protected override void OnTick()
            {
                if (this.Count >= this.MaxCount)
                    this.Stop();
                else
                {
                    this.Mob.FixedEffect(0x376A, 1, 32);
                    this.Count++;
                }
            }
        }

		private class ExpirePolymorphTimer : Timer
		{
			private Mobile m_Owner;
			private Mobile m_Other;

			public ExpirePolymorphTimer( Mobile owner, Mobile other ) : base( TimeSpan.FromMinutes( 3.0 ) )
			{
				m_Owner = owner;
				m_Other = other;

				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				if ( !m_Owner.CanBeginAction( typeof( PolymorphSpell ) ) )
				{
					m_Owner.BodyMod = 0;
					m_Owner.HueMod = -1;
					m_Owner.EndAction( typeof( PolymorphSpell ) );
				}
				if (m_Other != null && !m_Other.CanBeginAction( typeof( PolymorphSpell ) ))
				{
					m_Other.BodyMod = 0;
					m_Other.HueMod = -1;
					m_Other.EndAction( typeof( PolymorphSpell ) );
				}
			}
		}

		public void SpawnRatmen( Mobile target )
		{
			Map map = this.Map;

			if ( map == null )
				return;

			Hidden = true;
			Frozen = true;

			int rats = 0;

			foreach ( Mobile m in this.GetMobilesInRange( 10 ) )
			{
				if ( m is Ratman || m is RatmanArcher || m is RatmanMage )
					++rats;
			}

			if ( rats < 16 )
			{
				PlaySound( 0x3D );

				int newRats = Utility.RandomMinMax( 4, 7 );

				for ( int i = 0; i < newRats; ++i )
				{
					BaseCreature rat;

					switch ( Utility.Random( 5 ) )
					{
						default:
						case 0: case 1:	rat = new ChampionRatman(); break;
						case 2: case 3:	rat = new ChampionRatmanArcher(); break;
						case 4:			rat = new ChampionRatmanMage(); break;
					}

					rat.Team = this.Team;

					bool validLocation = false;
					Point3D loc = this.Location;

					for ( int j = 0; !validLocation && j < 10; ++j )
					{
						int x = X + Utility.Random( 3 ) - 1;
						int y = Y + Utility.Random( 3 ) - 1;
						int z = map.GetAverageZ( x, y );

						if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
							loc = new Point3D( x, y, Z );
						else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
							loc = new Point3D( x, y, z );
					}

					rat.MoveToWorld( loc, map );
					rat.Combatant = target;
				}
			}
			//players target one mob and don't care about the other rats, they drag out the mob's health bar and keep attacking the same mob.  lets fuck with that a bit FINAL
			ChampionSpawn spawn = null;

			foreach ( Item body in World.Items.Values ) 
			if ( body is ChampionSpawn )
			{
				if ( ((ChampionSpawn)body).Champion == this)
				{
					spawn = (ChampionSpawn)body;
				}
			}

				BaseCreature huz = Activator.CreateInstance( typeof(Barracoon) ) as BaseCreature;
				huz.HitsMaxSeed = this.HitsMax;
				huz.Hits = this.Hits;
				Polymorph( huz );
				huz.MoveToWorld(this.Location, this.Map);
				huz.Combatant = this.Combatant;
				((BaseCreature)huz).OnAfterSpawn();

			if (spawn != null)
			{
				((ChampionSpawn)spawn).Champion = huz;	
			}

				this.Delete(); // this means the players will have lost the target/healthbar for this champ and will need to find the champ in the middle of the ratmen


		}

		public void DoSpecialAbility( Mobile target )
		{
			if ( target == null || target.Deleted ) //sanity
				return;
			if ( 0.33 >= Utility.RandomDouble() ) // 50% chance to polymorph attacker into a ratman
				Polymorph( target );

			if ( 0.35 >= Utility.RandomDouble() ) // 20% chance to more ratmen
				SpawnRatmen( target );

			if (0.25 >= Utility.RandomDouble() ) //FINAL ... NOW hes a real piper
			{
				Mobile one = null;
				Mobile two = null;

				foreach ( Mobile mob in this.GetMobilesInRange( 13 ) )
				{
					if ( InLOS( mob ) && mob is BaseCreature && ((BaseCreature)mob).Controlled && ((BaseCreature)mob).ControlMaster != null )
					{
						if (one == null)
							one = mob;

						else if (two == null)
							two = mob;
					}
				}	

				if (one != null)
				{
					PlaySound( 0x504 );
					if (two != null)
					{
						((BaseCreature)one).Provoke( this, two, false );
						//((BaseCreature)one).ControlTarget = two;
						//((BaseCreature)one).ControlOrder = OrderType.Attack;
					}
					else if (Utility.RandomBool() )
					{	
						((BaseCreature)one).Provoke( this, ((BaseCreature)one).ControlMaster, false );
						//((BaseCreature)one).ControlTarget = ((BaseCreature)one).ControlMaster;
						//((BaseCreature)one).ControlOrder = OrderType.Attack;
					}
					else
					{
						Polymorph( this, true, one );
					}
				}
			}

			if ( Hits < (HitsMax/3) && !IsBodyMod ) // Baracoon is low on life, polymorph into a ratman
				Polymorph( this );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			DoSpecialAbility( attacker );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			DoSpecialAbility( defender );
		}

		public Barracoon( Serial serial ) : base( serial )
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