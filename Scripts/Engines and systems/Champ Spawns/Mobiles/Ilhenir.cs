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
	[CorpseName( "a corpse of Ilhenir" )]
	public class Ilhenir : BaseChampion
	{
		public override ChampionSkullType SkullType { get { return ChampionSkullType.Pain; } }

		public override Type[] UniqueList { get { return new Type[] { }; } }
		public override Type[] SharedList
		{
			get
			{
				return new Type[] { 	typeof( ANecromancerShroud ),
										typeof( LieutenantOfTheBritannianRoyalGuard ),
										typeof( OblivionsNeedle ),
										typeof( TheRobeOfBritanniaAri ) };
			}
		}
		public override Type[] DecorativeList { get { return new Type[] { typeof( MonsterStatuette ) }; } }

		public override MonsterStatuetteType[] StatueTypes
		{
			get
			{
				return new MonsterStatuetteType[] { 	MonsterStatuetteType.PlagueBeast,
														MonsterStatuetteType.RedDeath };
			}
		}

		[Constructable]
		public Ilhenir()
			: base( AIType.AI_Mage )
		{
			Name = "Ilhenir";
			Title = "the Stained";
			Body = 0x103;

			BaseSoundID = 589;

			SetStr( 1105, 1350 );
			SetDex( 82, 160 );
			SetInt( 505, 750 );

			SetHits( 9000 );

			SetDamage( 21, 28 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Poison, 20 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 55, 65 );
			SetResistance( ResistanceType.Poison, 70, 90 );
			SetResistance( ResistanceType.Energy, 65, 75 );

			SetSkill( SkillName.EvalInt, 100 );
			SetSkill( SkillName.Magery, 100 );
			SetSkill( SkillName.Meditation, 0 );
			SetSkill( SkillName.Poisoning, 5.4 );
			SetSkill( SkillName.Anatomy, 117.5 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 119.9 );
			SetSkill( SkillName.Wrestling, 119.9 );

			Fame = 50000;
			Karma = -50000;

			VirtualArmor = 44;

			if ( Core.ML ) {
				PackResources( 8 );
				PackTalismans( 5 );
			}
		}

		public virtual void PackResources( int amount )
		{
			for( int i = 0; i < amount; i++ )
				switch( Utility.Random( 6 ) )
				{
					case 0: PackItem( new Blight() ); break;
					case 1: PackItem( new Scourge() ); break;
					case 2: PackItem( new Taint() ); break;
					case 3: PackItem( new Putrefication() ); break;
					case 4: PackItem( new Corruption() ); break;
					case 5: PackItem( new Muculent() ); break;
				}
		}

		public virtual void PackItems( Item item, int amount )
		{
			for( int i = 0; i < amount; i++ )
				PackItem( item );
		}

		public virtual void PackTalismans( int amount )
		{
			int count = Utility.Random( amount );

			for( int i = 0; i < count; i++ )
				PackItem( new MagicTalisman() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 8 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( Core.ML ) {
				c.DropItem( new GrizzledBones() );

				// TODO: Parrots
				/*if ( Utility.RandomDouble() < 0.6 )
					c.DropItem( new ParrotItem() ); */

				if( Utility.RandomDouble() < 0.05 )
					c.DropItem( new GrizzledMareStatuette() );

				if( Utility.RandomDouble() < 0.025 )
					c.DropItem( new CrimsonCincture() );

				// TODO: Armor sets
				/*if ( Utility.RandomDouble() < 0.05 )
				{
					switch ( Utility.Random(5) )
					{
						case 0: c.DropItem( new GrizzleGauntlets() ); break;
						case 1: c.DropItem( new GrizzleGreaves() ); break;
						case 2: c.DropItem( new GrizzleHelm() ); break;
						case 3: c.DropItem( new GrizzleTunic() ); break;
						case 4: c.DropItem( new GrizzleVambraces() ); break;
					}
				}*/
			}
		}

		public override bool Unprovokable { get { return true; } }
		public override bool Uncalmable { get { return true; } }
		public override Poison PoisonImmune { get { return Poison.Lethal; } }
		//public override bool GivesMLMinorArtifact { get { return true; } } // TODO: Needs verification
		public override int TreasureMapLevel { get { return 5; } }

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if( Utility.RandomDouble() < 0.25 )
				CacophonicAttack( defender );
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if( Utility.RandomDouble() < 0.1 )
				DropOoze();

			base.OnDamage( amount, from, willKill );
		}

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

		public Ilhenir( Serial serial )
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

		private static Hashtable m_Table;

		public virtual void CacophonicAttack( Mobile to )
		{
			if( m_Table == null )
				m_Table = new Hashtable();

			if( to.Alive && to.Player && m_Table[ to ] == null )
			{
				to.Send( SpeedControl.WalkSpeed );
				to.SendLocalizedMessage( 1072069 ); // A cacophonic sound lambastes you, suppressing your ability to move.
				to.PlaySound( 0x584 );

				m_Table[ to ] = Timer.DelayCall( TimeSpan.FromSeconds( 30 ), new TimerStateCallback( EndCacophonic_Callback ), to );
			}
		}

		private void EndCacophonic_Callback( object state )
		{
			if( state is Mobile )
				CacophonicEnd( (Mobile)state );
		}

		public virtual void CacophonicEnd( Mobile from )
		{
			if( m_Table == null )
				m_Table = new Hashtable();

			m_Table[ from ] = null;

			from.Send( SpeedControl.Disable );
		}

		public static bool UnderCacophonicAttack( Mobile from )
		{
			if( m_Table == null )
				m_Table = new Hashtable();

			return m_Table[ from ] != null;
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

	public class StainedOoze : Item
	{
		private bool m_Corrosive;
		private Timer m_Timer;
		private int m_Ticks;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Corrosive
		{
			get { return m_Corrosive; }
			set { m_Corrosive = value; }
		}

		[Constructable]
		public StainedOoze()
			: this( false )
		{
		}

		[Constructable]
		public StainedOoze( bool corrosive )
			: base( 0x122A )
		{
			Movable = false;
			Hue = 0x95;

			m_Corrosive = corrosive;
			m_Timer = Timer.DelayCall( TimeSpan.Zero, TimeSpan.FromSeconds( 1 ), OnTick );
			m_Ticks = 0;
		}

		public override void OnAfterDelete()
		{
			if ( m_Timer != null )
			{
				m_Timer.Stop();
				m_Timer = null;
			}
		}

		private void OnTick()
		{
			List<Mobile> toDamage = new List<Mobile>();

			foreach ( Mobile m in GetMobilesInRange( 0 ) )
			{
				if ( m is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)m;

					if ( !bc.Controlled && !bc.Summoned )
						continue;
				}
				else if ( !m.Player )
				{
					continue;
				}

				if ( m.Alive && !m.IsDeadBondedPet && m.CanBeDamaged() )
					toDamage.Add( m );
			}

			for ( int i = 0; i < toDamage.Count; ++i )
				Damage( toDamage[i] );

			++m_Ticks;

			if ( m_Ticks >= 35 )
				Delete();
			else if ( m_Ticks == 30 )
				ItemID = 0x122B;
		}

		public void Damage( Mobile m )
		{
			if ( m_Corrosive )
			{
				List<Item> items = m.Items;
				bool damaged = false;

				for ( int i = 0; i < items.Count; ++i )
				{
					IDurability wearable = items[i] as IDurability;

					if ( wearable != null && wearable.HitPoints >= 10 && Utility.RandomDouble() < 0.25 )
					{
						wearable.HitPoints -= ( wearable.HitPoints == 10 ) ? Utility.Random( 1, 5 ) : 10;
						damaged = true;
					}
				}

				if ( damaged )
				{
					m.LocalOverheadMessage( MessageType.Regular, 0x21, 1072070 ); // The infernal ooze scorches you, setting you and your equipment ablaze!
					return;
				}
			}

			AOS.Damage( m, 40, 0, 0, 0, 100, 0 );
		}

		public StainedOoze( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Corrosive );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Corrosive = reader.ReadBool();

			m_Timer = Timer.DelayCall( TimeSpan.Zero, TimeSpan.FromSeconds( 1 ), OnTick );
			m_Ticks = ( ItemID == 0x122A ) ? 0 : 30;
		}
	}
}
