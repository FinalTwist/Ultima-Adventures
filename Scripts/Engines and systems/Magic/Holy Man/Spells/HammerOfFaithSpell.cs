using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Spells;

namespace Server.Spells.HolyMan
{
	public class HammerOfFaithSpell : HolyManSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Hammer of Faith", "Malleo Fidei",
				266,
				9040
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }
		public override int RequiredTithing{ get{ return 100; } }
		public override double RequiredSkill{ get{ return 50.0; } }
		public override int RequiredMana{ get{ return 25; } }

		public HammerOfFaithSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				Item weap = new HammerOfFaith( Caster );

				Caster.AddToBackpack( weap );
				Caster.SendMessage( "You create a magical hammer and place it in your backpack." );

				Caster.PlaySound( 0x212 );
				Caster.PlaySound( 0x206 );

				Effects.SendLocationParticles( EffectItem.Create( Caster.Location, Caster.Map, EffectItem.DefaultDuration ), 0x376A, 1, 29, 0x47D, 2, 9962, 0 );
				Effects.SendLocationParticles( EffectItem.Create( new Point3D( Caster.X, Caster.Y, Caster.Z - 7 ), Caster.Map, EffectItem.DefaultDuration ), 0x37C4, 1, 29, 0x47D, 2, 9502, 0 );
			}
		}

		private class HammerOfFaith : WarHammer
		{
			private Mobile m_Owner;
			private DateTime m_Expire;
			private Timer m_Timer;

			[Constructable]
			public HammerOfFaith( Mobile owner ) : base()
			{
				m_Owner = owner;
				Weight = 10.0;
				Layer = Layer.TwoHanded;
				Hue = 0x9C2;
				BlessedFor = owner;
				Slayer = SlayerName.Silver;
				Slayer2 = SlayerName.Exorcism;
				WeaponAttributes.LowerStatReq = 100;
				SkillBonuses.SetValues( 0, SkillName.Macing, 10 );
				AccuracyLevel = WeaponAccuracyLevel.Supremely;
				DamageLevel = WeaponDamageLevel.Vanq;
				Attributes.AttackChance = 30;
				Name = "Hammer of Faith";

				double time = ( owner.Skills[SkillName.Healing].Value / 5.0 );
				m_Expire = DateTime.UtcNow + TimeSpan.FromMinutes( (int)time );
				m_Timer = new InternalTimer( this, m_Expire );

				m_Timer.Start();
			}

			public override void OnDelete()
			{
				if ( m_Timer != null )
					m_Timer.Stop();

				base.OnDelete();
			}

			public override bool CanEquip( Mobile m )
			{
				if ( m != m_Owner )
					return false;

				return true;
			}

			public void Remove()
			{
				m_Owner.SendMessage( "Your hammer slowly dissipates." );
				Delete();
			}

			public HammerOfFaith( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 0 ); // version
				writer.Write( m_Owner );
				writer.Write( m_Expire );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
				m_Owner = reader.ReadMobile();
				m_Expire = reader.ReadDeltaTime();

				m_Timer = new InternalTimer( this, m_Expire );
				m_Timer.Start();
			}
		}

		private class InternalTimer : Timer
		{
			private HammerOfFaith m_Hammer;
			private DateTime m_Expire;

			public InternalTimer( HammerOfFaith hammer, DateTime expire ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_Hammer = hammer;
				m_Expire = expire;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					m_Hammer.Remove();
					Stop();
				}
			}
		}
	}
}
