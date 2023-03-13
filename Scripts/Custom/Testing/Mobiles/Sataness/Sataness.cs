// Created by Tom Sibilsky aka Neptune

using System;
using Server.Items;

namespace Server.Mobiles

              {
              [CorpseName( " corpse of Sataness" )]
              public class Sataness : BaseCreature
              {
				private Timer m_Timer;
                                 [Constructable]
                                    public Sataness() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
                            {
                                               Name = "Sataness";
						Title = "Woman's Scorn";
					             Body = 174;
						        Female = true; 
						           Hue = 2255;
						AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
				
                                 //Body = 149; // Uncomment these lines and input values
                                 //BaseSoundID = 0x4B0; // To use your own custom body and sound.
                                               SetStr( 1000 );
                                               SetDex( 800 );
                                               SetInt( 1200 );
                                               SetHits( 4500 );
                                               SetDamage( 25, 50 );
                                               SetDamageType( ResistanceType.Cold, 25 );
                                               SetDamageType( ResistanceType.Fire, 25 );
                                               SetDamageType( ResistanceType.Energy, 25 );
                                               SetDamageType( ResistanceType.Poison, 25 );

                                               SetResistance( ResistanceType.Physical, 85 );
                                               SetResistance( ResistanceType.Cold, 85 );
                                               SetResistance( ResistanceType.Fire, 95 );
                                               SetResistance( ResistanceType.Energy, 70 );
                                               SetResistance( ResistanceType.Poison, 85 );

			SetSkill( SkillName.EvalInt, 80.0, 90.0 );
			SetSkill( SkillName.Magery, 100.0, 110.0 );
			SetSkill( SkillName.Meditation, 80.0, 90.0 );
			SetSkill( SkillName.Poisoning, 100.0, 105.0 );
			SetSkill( SkillName.MagicResist, 80.0, 95.0 );
			SetSkill( SkillName.Tactics, 100.0, 120.0 );
			SetSkill( SkillName.Wrestling, 110.0, 120.0 );
			SetSkill( SkillName.Swords, 110.0, 120.0 );
			SetSkill( SkillName.Anatomy, 80.0, 115.0 );
			SetSkill( SkillName.Parry, 90.0, 115.0 );


			m_Timer = new TeleportTimer( this );
			m_Timer.Start();



            Fame = 15000;
            Karma = -15000;
            VirtualArmor = 85;

			PackGold( 5000, 15000 );






                            }
        public override void GenerateLoot()
		{
			switch ( Utility.Random( 150 ))
			{
				case 0: PackItem( new SSShirt() ); break;
				case 1: PackItem( new SSSkirt() ); break;
				case 2: PackItem( new SSSash() ); break;
				case 3: PackItem( new SSStaff() ); break;
				
				
		 }		
			
AddLoot( LootPack.FilthyRich, 8 );

				
		 
		}
		
		        public override bool HasBreath{ get{ return true ; } }
			 public override int BreathFireDamage{ get{ return 30; } }
			public override int BreathColdDamage{ get{ return 30; } }
			
//                public override bool IsScaryToPets{ get{ return true; } }
				public override bool AutoDispel{ get{ return true; } }
                public override bool BardImmune{ get{ return true; } }
                public override bool Unprovokable{ get{ return true; } }
                public override Poison HitPoison{ get{ return Poison.Lethal ; } }
                public override bool AlwaysMurderer{ get{ return true; } }
//				public override bool IsScaredOfScaryThings{ get{ return false; } }






		public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			if ( from is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)from;

				if ( bc.Controlled || bc.BardTarget == this )
					damage = 0; // Immune to pets and provoked creatures
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

			public TeleportTimer( Mobile owner ) : base( TimeSpan.FromSeconds( 8.0 ), TimeSpan.FromSeconds( 1.1 ) )
			{
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

				if ( 0.5 < Utility.RandomDouble() )
					return;

				Mobile toTeleport = null;

				foreach ( Mobile m in m_Owner.GetMobilesInRange( 16 ) )
				{
					if ( m != m_Owner && m.Player && m_Owner.CanBeHarmful( m ) && m_Owner.CanSee( m ) )
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


public Sataness( Serial serial ) : base( serial )
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
					AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
                      }
    }
}
