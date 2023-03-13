using System;
using Server.Items;
using Server.Engines.Plants;

namespace Server.Mobiles
{
	[CorpseName( "a kuthulu corpse" )]
	public class Kuthulu : BaseCreature
	{
		private Timer m_Timer;

		[Constructable]
		public Kuthulu() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a kuthulu";
			Body = 352;
			BaseSoundID = 357;

			SetStr( 801, 950 );
			SetDex( 126, 175 );
			SetInt( 201, 250 );

			SetHits( 650 );

			SetDamage( 22, 26 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.Meditation, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 90.1, 105.0 );
			SetSkill( SkillName.Tactics, 75.1, 85.0 );
			SetSkill( SkillName.Wrestling, 80.1, 100.0 );

			Fame = 9500;
			Karma = -9500;

			VirtualArmor = 44;

			PackNecroReg( 24, 45 );

			m_Timer = new TeleportTimer( this );
			m_Timer.Start();
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int Meat{ get{ return 3; } }

		public Kuthulu( Serial serial ) : base( serial )
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

				foreach ( Mobile m in m_Owner.GetMobilesInRange( 16 ) )
				{
					if ( m != m_Owner && m.Player && m_Owner.CanBeHarmful( m ) && m_Owner.CanSee( m ) && m.AccessLevel == AccessLevel.Player )
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

					Effects.SendLocationParticles( EffectItem.Create( from, m.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
					Effects.SendLocationParticles( EffectItem.Create(   to, m.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );

					m.PlaySound( 0x1FE );

					m_Owner.Combatant = toTeleport;
				}
			}
		}
	}
}