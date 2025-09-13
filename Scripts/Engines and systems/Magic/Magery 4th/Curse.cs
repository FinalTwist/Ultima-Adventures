using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Multis;

namespace Server.Spells.Fourth
{
	public class CurseSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Curse", "Des Sanct",
				227,
				9031,
				Reagent.Nightshade,
				Reagent.Garlic,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Fourth; } }

		public CurseSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		private static Hashtable m_UnderEffect = new Hashtable();

		public static void RemoveEffect( object state )
		{
			Mobile m = (Mobile)state;

			m_UnderEffect.Remove( m );

			m.UpdateResistances();
		}

		public static bool UnderEffect( Mobile m )
		{
			return m_UnderEffect.Contains( m );
		}

		public static void ApplyCurse(Mobile caster, Mobile target, int spellHue, int soundID)
		{
            SpellHelper.AddStatCurse(caster, target, StatType.Str); SpellHelper.DisableSkillCheck = true;
            SpellHelper.AddStatCurse(caster, target, StatType.Dex);
            SpellHelper.AddStatCurse(caster, target, StatType.Int); SpellHelper.DisableSkillCheck = false;

            // Cancel the previous timer if it exists
            Timer t = (Timer)m_UnderEffect[target];
            if (t != null && t.Running)
            {
                t.Stop();
            }

            TimeSpan duration = SpellHelper.GetDuration(caster, target);

            m_UnderEffect[target] = Timer.DelayCall(duration, new TimerStateCallback(RemoveEffect), target);
            target.UpdateResistances();

            if (target.Spell != null)
                target.Spell.OnCasterHurt();

            target.Paralyzed = false;

            target.FixedParticles(0x374A, 10, 15, 5028, spellHue, 0, EffectLayer.Waist);
            target.PlaySound(soundID);

            int percentage = (int)(SpellHelper.GetOffsetScalar(caster, target, true) * 100);
            TimeSpan length = SpellHelper.GetDuration(caster, target);

            string args = String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", percentage, percentage, percentage, 10, 10, 10, 10);

            BuffInfo.AddBuff(target, new BuffInfo(BuffIcon.Curse, 1075835, 1075836, length, target, args.ToString()));
        }

		public void Target( Mobile m )
		{
			if (SkirtOfPower.TryBlockCurse(m)) return;

			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m );

				ApplyCurse(Caster, m, Server.Items.CharacterDatabase.GetMySpellHue(Caster, 0), 0x1E1);

                HarmfulSpell( m );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private CurseSpell m_Owner;

			public InternalTarget( CurseSpell owner ) : base( Core.ML? 10 : 12, false, TargetFlags.Harmful )
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