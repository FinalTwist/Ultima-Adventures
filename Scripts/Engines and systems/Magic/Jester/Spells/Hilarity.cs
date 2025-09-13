using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Network;
using System.Collections.Generic;
using System.Collections;
using Server.Regions;
using Server.Multis;
using Server.Misc;
using Server.Mobiles;
using Server.Spells.Chivalry;

namespace Server.Spells.Jester
{
	public class Hilarity : JesterSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Hilarity", "Have you heard this one?",
				-1,
				0
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
		public override int RequiredTithing{ get{ return 40; } }
		public override int RequiredMana{ get{ return 50; } }

		public Hilarity( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if (m == null || m.Map == null || m.Deleted  )
				return;

			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( Core.AOS && (m.Frozen || m.Paralyzed || (m.Spell != null && m.Spell.IsCasting && !(m.Spell is PaladinSpell))) )
			{
				Caster.SendLocalizedMessage( 1061923 ); // The target is already frozen.
			}
			else if ( CheckHSequence( m ) )
			{
				double duration = Timed( m, Caster );

				m.Paralyze( TimeSpan.FromSeconds( duration ) );
				DoReaction( m );
				HarmfulSpell( m );

				int TotalRange = Server.Spells.Jester.JesterSpell.Buff( Caster, "range" );

				List<Mobile> targets = new List<Mobile>();

				foreach ( Mobile v in Caster.GetMobilesInRange( TotalRange ) )
				{
					BaseCreature bc = null;
					if ( v is BaseCreature ){ bc = (BaseCreature)v; }
					if (Caster.InLOS( v ) && v.Alive && Caster.CanBeHarmful( v ) && !v.Blessed && Caster != v && v != m )
					{
						if (bc != null)
						{
							if ( bc.ControlMaster != null && bc.ControlMaster != Caster && bc.SummonMaster != Caster )
								targets.Add( v );
							else if (bc.ControlMaster == null)
								targets.Add( v );
						}
						else if (v is PlayerMobile)
							targets.Add( v );
					}
				}
				for ( int i = 0; i < targets.Count; ++i )
				{
					Mobile v = targets[i];
					duration = Timed( v, Caster );
					v.Paralyze( TimeSpan.FromSeconds( duration ) );
					DoReaction( v );
					HarmfulSpell( v );
				}
			}

			Server.Mobiles.ChucklesJester.DoJokes( Caster );
			FinishSequence();
		}

		public static void DoReaction( Mobile m )
		{
			switch ( Utility.Random( 3 ))
			{
				case 0: Effects.PlaySound( m.Location, m.Map, m.Female ? 780 : 1051 ); m.Say( "*claps*" ); break;
				case 1: Effects.PlaySound( m.Location, m.Map, m.Female ? 794 : 1066 ); m.Say( "*giggles*" ); break;
				case 2: Effects.PlaySound( m.Location, m.Map, m.Female ? 801 : 1073 ); m.Say( "*laughs*" ); break;
			}
		}

		public static double Timed( Mobile m, Mobile caster )
		{
			double duration;

			int secs = (int)( caster.Skills[SkillName.EvalInt].Value + ( ( caster.Skills[SkillName.EvalInt].Value + caster.Skills[SkillName.Begging].Value ) / 8 ) );

			int level = 0;
				if ( m is BaseCreature ){ level = Server.Misc.IntelligentAction.GetCreatureLevel( m ); }
				else if ( m is PlayerMobile ){ level = Server.Misc.GetPlayerInfo.GetPlayerLevel( m ); }

			secs = secs - level;

			if ( secs < 5 )
				secs = 5;

			duration = secs;

			return duration;
		}

		public class InternalTarget : Target
		{
			private Hilarity m_Owner;

			public InternalTarget( Hilarity owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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
	}
}