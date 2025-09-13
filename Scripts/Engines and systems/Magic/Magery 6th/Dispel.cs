using System;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Sixth
{
	public class DispelSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Dispel", "An Ort",
				218,
				9002,
				Reagent.Garlic,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

		public DispelSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public class InternalTarget : Target
		{
			private DispelSpell m_Owner;

			public InternalTarget( DispelSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile && o is BaseCreature )
				{
					Mobile m = (Mobile)o;
					BaseCreature bc = m as BaseCreature;

					if ( !from.CanSee( m ) )
					{
						from.SendLocalizedMessage( 500237 ); // Target can not be seen.
					}
					else if ( bc.ControlSlots == 666 ) // FOR SPECIAL SUMMONED WIZARD CREATURES
					{
						SpellHelper.Turn( from, m );

						if ( from.Skills.Magery.Value > Utility.RandomMinMax( 1, 100 ) )
						{
							Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, Server.Items.CharacterDatabase.GetMySpellHue( from, 0 ), 0, 5042, 0 );
							Effects.PlaySound( m, m.Map, 0x201 );

							m.Delete();
						}
						else
						{
							m.FixedEffect( 0x3779, 10, 20, Server.Items.CharacterDatabase.GetMySpellHue( from, 0 ), 0 );
							from.SendLocalizedMessage( 1010084 ); // The creature resisted the attempt to dispel it!
						}
					}
					else if ( bc == null || !bc.IsDispellable )
					{
						from.SendLocalizedMessage( 1005049 ); // That cannot be dispelled.
					}
					else if ( m_Owner.CheckHSequence( m ) )
					{
						SpellHelper.Turn( from, m );

						double dispelChance = (50.0 + ((100 * (from.Skills.Magery.Value - bc.DispelDifficulty)) / (bc.DispelFocus*2))) / 100;

						if ( dispelChance > Utility.RandomDouble() )
						{
							Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, Server.Items.CharacterDatabase.GetMySpellHue( from, 0 ), 0, 5042, 0 );
							Effects.PlaySound( m, m.Map, 0x201 );

							m.Delete();
						}
						else
						{
							m.FixedEffect( 0x3779, 10, 20, Server.Items.CharacterDatabase.GetMySpellHue( from, 0 ), 0 );
							from.SendLocalizedMessage( 1010084 ); // The creature resisted the attempt to dispel it!
						}
					}
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}