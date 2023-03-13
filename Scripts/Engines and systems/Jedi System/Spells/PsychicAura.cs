using System;
using System.Collections;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Jedi
{
    public class PsychicAura : JediSpell
	{
		public override int spellIndex { get { return 285; } }
		public int CirclePower = 1;
		public static int spellID = 285;
		public override int RequiredTithing{ get{ return Int32.Parse(  Server.Spells.Jedi.JediSpell.SpellInfo( spellIndex, 10 )); } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.5 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Spells.Jedi.JediSpell.SpellInfo( spellIndex, 2 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse(  Server.Spells.Jedi.JediSpell.SpellInfo( spellIndex, 3 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Spells.Jedi.JediSpell.SpellInfo( spellID, 1 ),
				Server.Misc.Research.CapsCast( Server.Spells.Jedi.JediSpell.SpellInfo( spellID, 4 ) ),
				236,
				0
			);

        public PsychicAura(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

		public override bool CheckCast(Mobile caster)
		{
			return true;
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
					targ.PlaySound( 0x1E9 );
					targ.FixedParticles( 0x376A, 9, 32, 5008, 0xB41, 0, EffectLayer.Waist );

					int phys = (int)( (targ.Skills[SkillName.Inscribe].Value / 15) + (GetJediDamage(Caster) / 50) );
					int engy = (int)( (targ.Skills[SkillName.Inscribe].Value / 25) + (GetJediDamage(Caster) / 75) );

					mods = new ResistanceMod[5]
						{
							new ResistanceMod( ResistanceType.Physical, phys ),
							new ResistanceMod( ResistanceType.Fire, -5 ),
							new ResistanceMod( ResistanceType.Cold, -5 ),
							new ResistanceMod( ResistanceType.Poison, -5 ),
							new ResistanceMod( ResistanceType.Energy, engy )
						};

					m_Table[targ] = mods;

					for ( int i = 0; i < mods.Length; ++i )
						targ.AddResistanceMod( mods[i] );
				}
				else
				{
					targ.PlaySound( 0x1ED );
					targ.FixedParticles( 0x376A, 9, 32, 5008, 0xB41, 0, EffectLayer.Waist );

					m_Table.Remove( targ );

					for ( int i = 0; i < mods.Length; ++i )
						targ.RemoveResistanceMod( mods[i] );
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

				m_Table.Remove( m );
			}
		}
	}
}