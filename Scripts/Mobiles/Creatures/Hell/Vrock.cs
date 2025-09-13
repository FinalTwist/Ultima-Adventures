using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a vrock corpse" )]
	public class Vrock : BaseCreature
	{
		private Timer m_Timer;

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public Vrock() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
		if (Utility.RandomDouble() > 0.85)
			{
				Name = "a greater vrock";
				Body = 675;
				BaseSoundID = 372;

				SetStr( 401, 530 );
				SetDex( 133, 252 );
				SetInt( 101, 140 );

				SetHits( 241, 258 );

				SetDamage( 11, 26 );

				SetDamageType( ResistanceType.Physical, 80 );
				SetDamageType( ResistanceType.Fire, 20 );

				SetResistance( ResistanceType.Physical, 45, 60 );
				SetResistance( ResistanceType.Fire, 50, 70 );
				SetResistance( ResistanceType.Cold, 40, 50 );
				SetResistance( ResistanceType.Poison, 20, 30 );
				SetResistance( ResistanceType.Energy, 30, 40 );

				SetSkill( SkillName.MagicResist, 65.1, 80.0 );
				SetSkill( SkillName.Tactics, 65.1, 90.0 );
				SetSkill( SkillName.Wrestling, 65.1, 80.0 );

				Fame = 7000;
				Karma = -6000;

				VirtualArmor = 46;

				m_Timer = new TeleportTimer( this );
				m_Timer.Start();
			}
		else
			{
				Name = "a vrock";
				Body = 675;
				BaseSoundID = 372;

				SetStr( 401, 430 );
				SetDex( 133, 152 );
				SetInt( 101, 140 );

				SetHits( 241, 258 );

				SetDamage( 11, 17 );

				SetDamageType( ResistanceType.Physical, 80 );
				SetDamageType( ResistanceType.Fire, 20 );

				SetResistance( ResistanceType.Physical, 45, 50 );
				SetResistance( ResistanceType.Fire, 50, 60 );
				SetResistance( ResistanceType.Cold, 40, 50 );
				SetResistance( ResistanceType.Poison, 20, 30 );
				SetResistance( ResistanceType.Energy, 30, 40 );

				SetSkill( SkillName.MagicResist, 65.1, 80.0 );
				SetSkill( SkillName.Tactics, 65.1, 90.0 );
				SetSkill( SkillName.Wrestling, 65.1, 80.0 );

				Fame = 6000;
				Karma = -6000;

				VirtualArmor = 46;

				m_Timer = new TeleportTimer( this );
				m_Timer.Start();
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
		}

		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }

		public Vrock( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
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