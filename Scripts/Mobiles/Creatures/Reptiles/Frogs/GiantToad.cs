using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a giant toad corpse" )]
	[TypeAlias( "Server.Mobiles.Gianttoad" )]
	public class GiantToad : BaseCreature
	{
		private Timer m_Timer;

		[Constructable]
		public GiantToad() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			Name = "a greater giant toad";
			Body = 80;
			Hue = Utility.RandomList( 0, 0xB79, 0xB19, 0xB0D, 0xACE, 0xACF, 0xAB0 );

			BaseSoundID = 0x26B;

			SetStr( 76, 200 );
			SetDex( 6, 25 );
			SetInt( 11, 20 );

			SetHits( 46, 160 );
			SetMana( 0 );

			SetDamage( 5, 25 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 45 );
			SetResistance( ResistanceType.Fire, 5, 20 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 40.1, 60.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 750;
			Karma = -750;

			VirtualArmor = 24;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 82.1;

			m_Timer = new TeleportTimer( this );
			m_Timer.Start();
		}
		else
		{
			Name = "a giant toad";
			Body = 80;
			Hue = Utility.RandomList( 0, 0xB79, 0xB19, 0xB0D, 0xACE, 0xACF, 0xAB0 );

			BaseSoundID = 0x26B;

			SetStr( 76, 100 );
			SetDex( 6, 25 );
			SetInt( 11, 20 );

			SetHits( 46, 60 );
			SetMana( 0 );

			SetDamage( 5, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 40.1, 60.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 750;
			Karma = -750;

			VirtualArmor = 24;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 77.1;

			m_Timer = new TeleportTimer( this );
			m_Timer.Start();
		}
	}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override int Hides{ get{ return 12; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat; } }

		public GiantToad(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 1);
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
					if (!((BaseCreature)m_Owner).Controlled && m != m_Owner && m.Player && m_Owner.CanBeHarmful( m ) && m_Owner.CanSee( m ) && m.AccessLevel == AccessLevel.Player && ( (BaseCreature)m_Owner ).IsEnemy( m ) )
					{
						toTeleport = m;
						break;
					}
					else if (((BaseCreature)m_Owner).Controlled && ((BaseCreature)m_Owner).ControlMaster != null)
					{
						Mobile Francis = ((BaseCreature)m_Owner).ControlMaster;
						if (m is BaseCreature && Francis.Map == m_Owner.Map && !((BaseCreature)m).Controlled && !((BaseCreature)m).Summoned && ((BaseCreature)m).ControlMaster != Francis && Francis.CanBeHarmful(m) && m_Owner.CanSee(m) && ((BaseCreature)m).IsEnemy(Francis) )
						{
							toTeleport = m;
							break;
						}
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
