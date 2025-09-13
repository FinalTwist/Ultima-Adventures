using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.HolyMan
{
	public class NourishSpell : HolyManSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Nourish", "Famem Prohibere",
				266,
				9040
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }
		public override int RequiredTithing{ get{ return 20; } }
		public override double RequiredSkill{ get{ return 10.0; } }
		public override int RequiredMana{ get{ return 5; } }

		public NourishSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( !( m is PlayerMobile ) )
			{
				Caster.SendMessage( "They don't seem to be hungry or thirsty." );
			}
			else if ( CheckBSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				int nourish = (int)(Caster.Skills[SkillName.Healing].Value / 5 );

				m.Hunger = m.Hunger + nourish;
				m.Thirst = m.Thirst + nourish;

				if ( m.Hunger > 20 ){ m.Hunger = 20; }
				if ( m.Thirst > 20 ){ m.Thirst = 20; }

				m.SendMessage( "You feel much more nourished." );

				m.FixedParticles( 0x376A, 9, 32, 5005, EffectLayer.Waist );
				m.PlaySound( 0x1F2 );
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private NourishSpell m_Owner;

			public InternalTarget( NourishSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Beneficial )
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