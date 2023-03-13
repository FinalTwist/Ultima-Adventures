using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a frog corpse" )]
	public class PoisonFrog : BaseCreature
	{
		private Timer m_Timer;

		[Constructable]
		public PoisonFrog() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a marsh frog";
			Body = 270;
			Hue = Utility.RandomList( 0x5AC,0x5A3,0x59A,0x591,0x588,0x57F );
			BaseSoundID = 0x266;

			SetStr( 92, 140 );
			SetDex( 12, 50 );
			SetInt( 22, 40 );

			SetHits( 56, 84 );
			SetMana( 0 );

			SetDamage( 8, 12 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Poison, 60 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 70, 90 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.Poisoning, 70.1, 100.0 );
			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 65.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 32;

			Item Venom = new VenomSack();
				Venom.Name = "deadly venom sack";
				AddItem( Venom );

			m_Timer = new TeleportTimer( this );
			m_Timer.Start();
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 4; } }

		public PoisonFrog(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_Timer = new TeleportTimer( this );
					m_Timer.Start();

					break;
				}
			}
		}

		private class TeleportTimer : Timer
		{
			private Mobile m_Owner;

			private static int[] m_Offsets = new int[]
			{
				-1, -1,
				-1,  0,
				-1,  1,
				0, -1,
				0,  1,
				1, -1,
				1,  0,
				1,  1
			};

			public TeleportTimer( Mobile owner ) : base( TimeSpan.FromSeconds( 5.0 ), TimeSpan.FromSeconds( 5.0 ) )
			{
				Priority = TimerPriority.TwoFiftyMS;

				m_Owner = owner;
			}

			protected override void OnTick()
			{
				if ( m_Owner.Deleted )
				{
					Stop();
					return;
				}

				Map map = m_Owner.Map;

				if ( map == null )
					return;

				if ( 0.25 < Utility.RandomDouble() )
					return;

				Mobile toTeleport = null;

				foreach ( Mobile m in m_Owner.GetMobilesInRange( 10 ) )
				{
					if ( m != m_Owner && m.Player && m_Owner.CanBeHarmful( m ) && m_Owner.CanSee( m ) && m.AccessLevel == AccessLevel.Player && ( (BaseCreature)m_Owner ).IsEnemy( m ) )
					{
						toTeleport = m;
						break;
					}
				}

				if ( toTeleport != null )
				{
					int offset = Utility.Random( 8 ) * 2;

					Point3D to = m_Owner.Location;

					for ( int i = 0; i < m_Offsets.Length; i += 2 )
					{
						int x = m_Owner.X + m_Offsets[(offset + i) % m_Offsets.Length];
						int y = m_Owner.Y + m_Offsets[(offset + i + 1) % m_Offsets.Length];

						if ( map.CanSpawnMobile( x, y, m_Owner.Z ) )
						{
							to = new Point3D( x, y, m_Owner.Z );
							break;
						}
						else
						{
							int z = map.GetAverageZ( x, y );

							if ( map.CanSpawnMobile( x, y, z ) )
							{
								to = new Point3D( x, y, z );
								break;
							}
						}
					}

					Mobile m = toTeleport;

					Point3D from = m.Location;

					m.Location = to;

					Server.Spells.SpellHelper.Turn( m_Owner, toTeleport );
					Server.Spells.SpellHelper.Turn( toTeleport, m_Owner );

					m.ProcessDelta();

					m.PlaySound( 0x58D );

					m_Owner.Combatant = toTeleport;

					BaseCreature.TeleportPets( m, m.Location, m.Map, false );
				}
			}
		}
	}
}