using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Spells.Third
{
	public class PoisonSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Poison", "In Nox",
				203,
				9051,
				Reagent.Nightshade
			);

		public override SpellCircle Circle { get { return SpellCircle.Third; } }

		public PoisonSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m );

				if ( m.Spell != null )
					m.Spell.OnCasterHurt();

				m.Paralyzed = false;

				if ( CheckResisted( m ) )
				{
					m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
				}
				else
				{
					int level;

					if ( Caster.InRange( m, 2 ) )
					{
						int total = (int)(Caster.Skills[SkillName.Magery].Value + Caster.Skills[SkillName.Poisoning].Value);
						bool empoweredFromPhylactery = false;
						if (Caster is PlayerMobile) {
							PlayerMobile player = (PlayerMobile) Caster;
							Phylactery phylactery = player.FindPhylactery();
							if (phylactery != null && (Scroll is SoulShard)) {
								empoweredFromPhylactery = true;
							} 
						}
						if ( total >= 250 || empoweredFromPhylactery ) {
							level = 4;
						} else if ( total >= 200 ) {
							level = 3;
						} else if ( total > 150 ) {
							level = 2;
						} else if ( total > 100 ) {
							level = 1;
						} else {
							level = 0;
						}
					}
					else
					{
						level = 0;
					}

					m.ApplyPoison( Caster, Poison.GetPoison( level ) );
					if (Scroll is SoulShard) {
						((SoulShard)Scroll).SuccessfulCast = true;
					}
				}

				m.FixedParticles( 0x374A, 10, 15, 5021, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.Waist );
				m.PlaySound( 0x205 );

				HarmfulSpell( m );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private PoisonSpell m_Owner;

			public InternalTarget( PoisonSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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