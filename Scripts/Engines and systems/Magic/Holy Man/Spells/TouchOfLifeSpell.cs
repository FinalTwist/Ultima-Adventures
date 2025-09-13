using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Items;

namespace Server.Spells.HolyMan
{
	public class TouchOfLifeSpell : HolyManSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Touch of Life", "Tactus Vitae",
				266,
				9040
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }
		public override int RequiredTithing{ get{ return 40; } }
		public override double RequiredSkill{ get{ return 20.0; } }
		public override int RequiredMana{ get{ return 10; } }

		public TouchOfLifeSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( m is PlayerMobile && m.FindItemOnLayer( Layer.Ring ) != null && m.FindItemOnLayer( Layer.Ring ) is OneRing)
			{
				Caster.SendMessage( "The ONE RING convinces you not to do that, and you listen to it... " );
				return;
			}
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckBSequence( m, false ) )
			{
				SpellHelper.Turn( Caster, m );

				int toHeal = 1 + (int)( (Caster.Skills[SkillName.Healing].Value / 10) + (Caster.Skills[SkillName.SpiritSpeak].Value / 10) );

				toHeal = Server.Misc.MyServerSettings.PlayerLevelMod( toHeal, Caster );

				SpellHelper.Heal( toHeal, m, Caster );
				m.Stam = m.Stam + toHeal;

				m.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
				m.PlaySound( 0x202 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private TouchOfLifeSpell m_Owner;

			public InternalTarget( TouchOfLifeSpell owner ) : base( 12, false, TargetFlags.Beneficial )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
