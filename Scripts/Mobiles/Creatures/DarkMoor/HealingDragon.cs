using System;
using Server;
using Server.Items;
using Server.Gumps;

namespace Server.Mobiles
{
	public class HealingDragon : BaseHealer
	{
		public override bool CanTeach{ get{ return false; } }
		public override bool InitialInnocent{ get{ return true; } }

		public override bool CheckTeach( SkillName skill, Mobile from )
		{
			return false;
		}

		[Constructable]
		public HealingDragon()
		{
			Name = "Healing Fairy";
			
			Hue = 1159;  //Never found a hue I was very happy with... 2050 if you want an annoying white glow.

			BodyValue=128;
            BaseSoundID = 1127; 
			PassiveSpeed = .35;
			ActiveSpeed = .175;
			ControlSlots = 0;
			
			
			SetStr( 506, 561 );
			SetDex( 301, 400 );
			SetInt( 201, 250 );

			SetHits( 13, 18 );

			SetDamage( 9, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.Meditation, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 10.1, 20.0 );
			SetSkill( SkillName.Wrestling, 10.1, 12.5 );

			Fame = 7000;
			Karma = 7000;
		}

		public override bool ClickTitle{ get{ return false; } } // Do not display title in OnSingleClick

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) )
			{
				CheckForNeeds ( m );
			}
		}
		
		public void CheckForNeeds ( Mobile m )
		{
			if ( !( m is PlayerMobile ) )
				return;
			if ( !InRange( m, 4 ) )
				return;
			if ( m.Frozen )
				return;
			if ( !InLOS( m ) )
				return;
			if ( SummonMaster != null && SummonMaster != m ) 
				return;
				
			if ( !m.Alive )
			{
				if ( m.Map == null || !m.Map.CanFit( m.Location, 16, false, false ) )
				{
					m.SendLocalizedMessage( 502391 ); // Thou can not be resurrected there!
					return;
				}
				if ( CheckResurrect( m ) )
				{
					OfferResurrection( m );
				}
				return;
			}
			else
			{
				if ( m.Hits < m.HitsMax )
				{
					OfferHeal( (PlayerMobile) m );
				}
			}
			return;
		}
		
		public override bool CheckResurrect( Mobile m )
		{
			if ( m.Criminal )
			{
				Say( 501222 ); // Thou art a criminal.  I shall not resurrect thee.
				return false;
			}
			else if ( m.Kills >= 5 )
			{
				Say( 501223 ); // Thou'rt not a decent and good person. I shall not resurrect thee.
				return false;
			}
			else if ( m.Karma < 0 )
			{
				Say( 501224 ); // Thou hast strayed from the path of virtue, but thou still deservest a second chance.
			}

			return true;
		}
		
		public override void OfferResurrection( Mobile m )
		{
			Direction = GetDirectionTo( m );

			m.PlaySound(0x1F2);
			m.FixedEffect( 0x376A, 10, 16 );

			m.CloseGump( typeof( ResurrectGump ) );
			m.SendGump( new ResurrectGump( m, ResurrectMessage.Healer ) );
			Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 1, 10, 0x26B6 );
			Effects.PlaySound( this.Location, this.Map, this.BaseSoundID );
			Delete();
		}

		public override void OfferHeal( PlayerMobile m )
		{
			Direction = GetDirectionTo( m );

			if ( m.CheckYoungHealTime() )
			{
				Say( 501229 ); // You look like you need some healing my child.

				m.PlaySound( 0x1F2 );
				m.FixedEffect( 0x376A, 9, 32 );

				m.Hits = m.HitsMax;
				Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 1, 10, 0x26B6 );
				Effects.PlaySound( this.Location, this.Map, this.BaseSoundID );
				Delete();
			}
			else
			{
				Say( 501228 ); // I can do no more for you at this time.
			}
		}
		
		public HealingDragon( Serial serial ) : base( serial )
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
	
	public class SummonedHealingDragon : HealingDragon
	{
		public override bool Commandable{ get{ return false; } }
		public override bool InitialInnocent{ get{ return true; } }
		//public override bool IsInvulnerable{ get{ return true; } }
		
		private DateTime m_HealTime;

		[Constructable]
		public SummonedHealingDragon() : base()
		{
			Hue = 1159;
			Blessed = true;
			m_HealTime = DateTime.UtcNow + TimeSpan.FromSeconds( 3 );
		}

		public SummonedHealingDragon( Serial serial ) : base( serial )
		{
		}
		
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( InRange( m, 4 ) &&  ( m_HealTime <= DateTime.UtcNow ) )
			{
				CheckForNeeds ( m );
				m_HealTime = DateTime.UtcNow + TimeSpan.FromSeconds( 3 );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}