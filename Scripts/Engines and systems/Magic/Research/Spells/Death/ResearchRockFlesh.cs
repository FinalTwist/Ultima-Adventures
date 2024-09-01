using System;
using Server;
using System.Collections;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Research
{
	public class ResearchRockFlesh : ResearchSpell
	{
		public override int spellIndex { get { return 10; } }
		public int CirclePower = 5;
		public static int spellID = 10;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.5 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
				236,
				9011
			);

		public ResearchRockFlesh( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		private static Hashtable m_TableStoneFlesh = new Hashtable();
        private static Hashtable m_Timers = new Hashtable();

		public static bool UnderEffect( Mobile m )
		{
			return m_Timers.Contains( m );
		}

		public static void RemoveEffect( Mobile m )
		{
			if (StopTimer( m ))
			{
				if (m.Map == null)
					return;
				
				m.HueMod = -1;
				m.BodyMod = 0;
				m.SendMessage( "Your flesh turns back to normal." );

				ResistanceMod[] mods = (ResistanceMod[])m_TableStoneFlesh[m];
				m_TableStoneFlesh.Remove( m );
				if (mods.Length > 0)
				{
					for ( int i = 0; i < mods.Length; ++i )
						m.RemoveResistanceMod( mods[i] );
				}

				Point3D hands = new Point3D( ( m.X+1 ), ( m.Y+1 ), ( m.Z+8 ) );
				Effects.SendLocationParticles(EffectItem.Create(hands, m.Map, EffectItem.DefaultDuration), 0x3837, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( m, 0xB7F ), 0, 5022, 0);
				m.PlaySound( 0x65A );

				m.EndAction( typeof( ResearchRockFlesh ) );
			}
		}

        private static bool StopTimer(Mobile m)
        {
            Timer t = (Timer)m_Timers[m];

            if (t != null)
            {
                t.Stop();
                m_Timers.Remove(m);
            }

            return (t != null);
        }

		public override void OnCast()
		{
			if ( !Caster.CanBeginAction( typeof( ResearchRockFlesh ) ) )
			{
				ResearchRockFlesh.RemoveEffect( Caster );
			}

			ResistanceMod[] mods = new ResistanceMod[1]
				{
					new ResistanceMod( ResistanceType.Physical, 90 )
				};

			m_TableStoneFlesh[Caster] = mods;

			for ( int i = 0; i < mods.Length; ++i )
				Caster.AddResistanceMod( mods[i] );

			double TotalTime = DamagingSkill( Caster )*4;
			Timer timer = new InternalTimer( Caster, TimeSpan.FromSeconds( TotalTime ) );

			Caster.BodyMod = 14;
			Caster.HueMod = 0xB80;

			Mobiles.IMount mt = Caster.Mount;
			if ( mt != null )
				mt.Rider = null;

			Caster.SendMessage( "Your flesh turns to stone." );

			Server.Misc.Research.ConsumeScroll( Caster, true, spellID, false );

			KarmaMod( Caster, ((int)RequiredSkill+RequiredMana) );

			Point3D hands = new Point3D( ( Caster.X+1 ), ( Caster.Y+1 ), ( Caster.Z+8 ) );
			Effects.SendLocationParticles(EffectItem.Create(hands, Caster.Map, EffectItem.DefaultDuration), 0x3837, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0xB7F ), 0, 5022, 0);
			Caster.PlaySound( 0x65A );

			m_Timers[Caster] = timer;
			timer.Start();

            FinishSequence();
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;

			public InternalTimer( Mobile Caster, TimeSpan duration ) : base( duration )
			{
				Priority = TimerPriority.OneSecond;
				m_m = Caster;
			}

			protected override void OnTick()
			{
				RemoveEffect( m_m );
			}
		}
	}
}