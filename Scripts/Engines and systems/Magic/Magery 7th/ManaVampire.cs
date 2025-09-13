using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Seventh
{
	public class ManaVampireSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Mana Vampire", "Ort Sanct",
				221,
				9032,
				Reagent.BlackPearl,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Seventh; } }

		public ManaVampireSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

				int toDrain = 0;

					if (Caster is PlayerMobile && ((PlayerMobile)Caster).Sorcerer() )
						toDrain = (int)((GetDamageSkill( Caster )*1.5) - GetResistSkill( m ));
					else
						toDrain = (int)(GetDamageSkill( Caster ) - GetResistSkill( m ));

					if ( toDrain < 0 )
						toDrain = 0;
					else if ( toDrain > m.Mana )
						toDrain = m.Mana;


				if ( toDrain > (Caster.ManaMax - Caster.Mana) )
					toDrain = Caster.ManaMax - Caster.Mana;

				m.Mana -= toDrain;
				Caster.Mana += Server.Misc.MyServerSettings.PlayerLevelMod( toDrain, Caster );

				m.FixedParticles( 0x374A, 1, 15, 5054, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 23 ), 7, EffectLayer.Head );
				m.PlaySound( 0x1F9 );

				Caster.FixedParticles( 0x0000, 10, 5, 2054, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 23 ), 7, EffectLayer.Head );

				HarmfulSpell( m );
			}

			FinishSequence();
		}

		public override double GetResistPercent( Mobile target )
		{
			return 98.0;
		}

		private class InternalTarget : Target
		{
			private ManaVampireSpell m_Owner;

			public InternalTarget( ManaVampireSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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
