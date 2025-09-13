using System;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Spells.Necromancy
{
	public class CorpseSkinSpell : NecromancerSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Corpse Skin", "In Agle Corp Ylem",
				203,
				9051,
				Reagent.BatWing,
				Reagent.GraveDust
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.5 ); } }

		public override double RequiredSkill{ get{ return 20.0; } }
		public override int RequiredMana{ get{ return 11; } }

		public CorpseSkinSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				/* Transmogrifies the flesh of the target creature or player to resemble rotted corpse flesh,
				 * making them more vulnerable to Fire and Poison damage,
				 * but increasing their resistance to Physical and Cold damage.
				 * 
				 * The effect lasts for ((Spirit Speak skill level - target's Resist Magic skill level) / 25 ) + 40 seconds.
				 * 
				 * NOTE: Algorithm above is fixed point, should be:
				 * ((ss-mr)/2.5) + 40
				 * 
				 * NOTE: Resistance is not checked if targeting yourself
				 */

				ExpireTimer timer = (ExpireTimer)m_Table[m];

				if ( timer != null )
					timer.DoExpire();
				else
					m.SendLocalizedMessage( 1061689 ); // Your skin turns dry and corpselike.

				 if ( m.Spell != null )
					m.Spell.OnCasterHurt();
				
				m.FixedParticles( 0x373A, 1, 15, 9913, 67, 7, EffectLayer.Head );
				m.PlaySound( 0x1BB );

				double ss = GetDamageSkill( Caster );
				double mr = ( Caster == m ? 0.0 : GetResistSkill( m ) );
				m.CheckSkill( SkillName.MagicResist, 0.0, 120.0 );	//Skill check for gain

				TimeSpan duration = TimeSpan.FromSeconds( ((ss - mr) / 2.5) + 40.0 );

				ResistanceMod[] mods = new ResistanceMod[4]
					{
						new ResistanceMod( ResistanceType.Fire, -15 ),
						new ResistanceMod( ResistanceType.Poison, -15 ),
						new ResistanceMod( ResistanceType.Cold, +Server.Misc.MyServerSettings.PlayerLevelMod( 10, Caster ) ),
						new ResistanceMod( ResistanceType.Physical, +Server.Misc.MyServerSettings.PlayerLevelMod( 10, Caster ) )
					};

				timer = new ExpireTimer( m, mods, duration );
				timer.Start();

				BuffInfo.AddBuff( m, new BuffInfo( BuffIcon.CorpseSkin, 1075663, duration, m ) );

				m_Table[m] = timer;

				for ( int i = 0; i < mods.Length; ++i )
					m.AddResistanceMod( mods[i] );

				HarmfulSpell( m );
			}

			FinishSequence();
		}

		private static Hashtable m_Table = new Hashtable();

		public static bool RemoveCurse( Mobile m )
		{
			ExpireTimer t = (ExpireTimer)m_Table[m];

			if ( t == null )
				return false;

			m.SendLocalizedMessage( 1061688 ); // Your skin returns to normal.
			t.DoExpire();
			return true;
		}

		private class ExpireTimer : Timer
		{
			private Mobile m_Mobile;
			private ResistanceMod[] m_Mods;

			public ExpireTimer( Mobile m, ResistanceMod[] mods, TimeSpan delay ) : base( delay )
			{
				m_Mobile = m;
				m_Mods = mods;
			}

			public void DoExpire()
			{
				for ( int i = 0; i < m_Mods.Length; ++i )
					m_Mobile.RemoveResistanceMod( m_Mods[i] );

				Stop();
				BuffInfo.RemoveBuff( m_Mobile, BuffIcon.CorpseSkin );
				m_Table.Remove( m_Mobile );
			}

			protected override void OnTick()
			{
				m_Mobile.SendLocalizedMessage( 1061688 ); // Your skin returns to normal.
				DoExpire();
			}
		}

		private class InternalTarget : Target
		{
			private CorpseSkinSpell m_Owner;

			public InternalTarget( CorpseSkinSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile) o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}