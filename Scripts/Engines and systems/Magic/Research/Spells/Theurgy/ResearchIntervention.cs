using System;
using System.Collections;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Research
{
	public class ResearchIntervention : ResearchSpell
	{
		public override int spellIndex { get { return 47; } }
		public int CirclePower = 6;
		public static int spellID = 47;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.5 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
				242,
				9012
			);

		public ResearchIntervention( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		private static Hashtable m_Table = new Hashtable();

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				Mobile targ = Caster;

				ResistanceMod[] mods = (ResistanceMod[])m_Table[targ];

				if ( mods == null )
				{
					Caster.PlaySound( 0x5C9 );
					Point3D wings = new Point3D( Caster.X+1, Caster.Y+1, Caster.Z+18 );
					Effects.SendLocationEffect( wings, Caster.Map, 0x3FE5, 30, 10, 0, 0 );

					int modify = (int)( DamagingSkill( Caster ) / 5 );

					mods = new ResistanceMod[5]
						{
							new ResistanceMod( ResistanceType.Physical, +modify ),
							new ResistanceMod( ResistanceType.Fire, +modify ),
							new ResistanceMod( ResistanceType.Cold, +modify ),
							new ResistanceMod( ResistanceType.Poison, +modify ),
							new ResistanceMod( ResistanceType.Energy, +modify )
						};

					m_Table[targ] = mods;

					for ( int i = 0; i < mods.Length; ++i )
						targ.AddResistanceMod( mods[i] );

					int value = (int)( DamagingSkill( Caster ) / 2 );
					Caster.MagicDamageAbsorb = value;

					int TotalTime = (int)( DamagingSkill( Caster ) / 12 );
					new InternalTimer( Caster, TimeSpan.FromMinutes( TotalTime ) ).Start();
					Server.Misc.Research.ConsumeScroll( Caster, true, spellID, false );
				}
				else
				{
					DoFizzle();
				}
			}

			FinishSequence();
		}

		public static void EndArmor( Mobile m )
		{
			if ( m_Table.Contains( m ) )
			{
				ResistanceMod[] mods = (ResistanceMod[]) m_Table[ m ];

				if ( mods != null )
				{
					for ( int i = 0; i < mods.Length; ++i )
						m.RemoveResistanceMod( mods[ i ] );
				}

				m.MagicDamageAbsorb = 0;
				m_Table.Remove( m );
				m.PlaySound( 0x1F8 );
			}
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
					EndArmor( m_m );
					Stop();
				}
			}
		}
	}
}