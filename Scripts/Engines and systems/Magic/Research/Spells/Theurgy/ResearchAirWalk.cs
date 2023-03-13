using System;
using Server;
using System.Collections;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Research
{
	public class ResearchAirWalk : ResearchSpell
	{
		public override int spellIndex { get { return 55; } }
		public int CirclePower = 7;
		public static int spellID = 55;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
				203,
				9031
			);

		public ResearchAirWalk( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public static void RemoveEffect( Mobile m )
		{
			m.EndAction( typeof( ResearchAirWalk ) );
		}

		public static bool UnderEffect( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ResearchAirWalk ) ) )
				return true;

			return false;
		}

		public override void OnCast()
		{
			if ( !Caster.CanBeginAction( typeof( ResearchAirWalk ) ) )
			{
				ResearchAirWalk.RemoveEffect( Caster );
			}

			int TotalTime = (int)( ( DamagingSkill( Caster ) * 20 ) / 60 );
			new InternalTimer( Caster, TimeSpan.FromSeconds( TotalTime ) ).Start();
			Caster.BeginAction( typeof( ResearchAirWalk ) );
			Point3D air = new Point3D( ( Caster.X+1 ), ( Caster.Y+1 ), ( Caster.Z+5 ) );
			Effects.SendLocationParticles(EffectItem.Create(air, Caster.Map, EffectItem.DefaultDuration), 0x2007, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, 5022, 0);
			Caster.PlaySound( 0x014 );
			Server.Misc.Research.ConsumeScroll( Caster, true, spellID, false );

            FinishSequence();
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile Caster, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = Caster;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ResearchAirWalk.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}
}