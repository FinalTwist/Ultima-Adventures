using System;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Syth
{
	public class DeathGrip : SythSpell
	{
		public override int spellIndex { get { return 271; } }
		public int CirclePower = 1;
		public static int spellID = 271;
		public override int RequiredTithing{ get{ return Int32.Parse(  Server.Spells.Syth.SythSpell.SpellInfo( spellIndex, 10 )); } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.5 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( spellIndex, 2 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse(  Server.Spells.Syth.SythSpell.SpellInfo( spellIndex, 3 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Spells.Syth.SythSpell.SpellInfo( spellID, 1 ),
				Server.Misc.Research.CapsCast( Server.Spells.Syth.SythSpell.SpellInfo( spellID, 4 ) ),
				203,
				0
			);

		public DeathGrip( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.SendMessage( "Who do you want to put to grip?" );
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) && CheckFizzle() )
			{
				TimeSpan duration = TimeSpan.FromSeconds( GetSythDamage(Caster) / 5 );

				Point3D grip = new Point3D( m.X+1, m.Y+1, m.Z+20 );
				Effects.SendLocationParticles(EffectItem.Create(grip, m.Map, EffectItem.DefaultDuration), 0x4CE3, 9, 32, 0x8BF-1, 0, 5022, 0);
				m.PlaySound( 0x65A );

				int min = 5;
				int max = ( (int)( GetSythDamage(Caster) / 25 ) + 5 );
				AOS.Damage( m, Caster, Utility.RandomMinMax( min, max ), true, 100, 0, 0, 0, 0 );

				Caster.DoHarmful( m );

				new GripTimer( m, duration, min, max, Caster ).Start();

				HarmfulSpell( m );
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private DeathGrip m_Owner;

			public InternalTarget( DeathGrip owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

		public class GripTimer : Timer
		{
			private Mobile m_m;
			private Mobile m_Caster;
			private DateTime m_Expire;
			private int m_Time;
			private int m_Min;
			private int m_Max;

			public GripTimer( Mobile sleeper, TimeSpan duration, int min, int max, Mobile caster ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = sleeper;
				m_Caster = caster;
				m_Expire = DateTime.UtcNow + duration;
				m_Time = 0;
				m_Min = min;
				m_Max = max;
			}

			protected override void OnTick()
			{
				m_Time++;
				if ( m_Time > 60 && m_m != null && m_Caster != null )
				{
					m_Time = 0;
					AOS.Damage( m_m, m_Caster, Utility.RandomMinMax( m_Min, m_Max ), true, 100, 0, 0, 0, 0 );
					m_Caster.DoHarmful( m_m );
					m_Caster.RevealingAction();
					Point3D grip = new Point3D( m_m.X+1, m_m.Y+1, m_m.Z+10 );
					Effects.SendLocationParticles(EffectItem.Create(grip, m_m.Map, EffectItem.DefaultDuration), 0x4CE3, 9, 32, 0x8BF-1, 0, 5022, 0);
					m_m.PlaySound( 0x65A );
				}

				if ( DateTime.UtcNow >= m_Expire )
				{
					Stop();
				}
			}
		}
	}
}