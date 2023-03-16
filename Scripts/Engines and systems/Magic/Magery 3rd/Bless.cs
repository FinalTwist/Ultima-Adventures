using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells.Fourth;

namespace Server.Spells.Third
{
	public class BlessSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Bless", "Rel Sanct",
				203,
				9061,
				Reagent.Garlic,
				Reagent.MandrakeRoot
			);

		public override SpellCircle Circle { get { return SpellCircle.Third; } }

		public BlessSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
			else if (m is BaseCreature && ((BaseCreature)m).IsBlessed)
			{
				Caster.SendMessage("That is already blessed.");
			}
			else if ( CheckBSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );
				
				if (m is PlayerMobile && CurseSpell.UnderEffect( m ))
				{
					if (Utility.RandomBool())
					{
						CurseSpell.RemoveEffect(m);
						m.SendMessage("You are no longer cursed!");
						m.UpdateResistances();
					}
					else
						m.SendMessage("You try to remove your curse, but fail!");
				}
					
				else 
				{

					SpellHelper.AddStatBonus( Caster, m, StatType.Str ); SpellHelper.DisableSkillCheck = true;
					SpellHelper.AddStatBonus( Caster, m, StatType.Dex );
					SpellHelper.AddStatBonus( Caster, m, StatType.Int ); SpellHelper.DisableSkillCheck = false;

					m.FixedParticles( 0x373A, 10, 15, 5018, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.Waist );
					m.PlaySound( 0x1EA );

					if (m is BaseCreature)
						((BaseCreature)m).IsBlessed = true;

					int percentage = (int)(SpellHelper.GetOffsetScalar(Caster, m, false) * 100);
					TimeSpan length = SpellHelper.GetDuration(Caster, m);

					if (Caster is PlayerMobile && ((PlayerMobile)Caster).Sorcerer())
					{
						percentage = (int)((double)percentage * (1+ ((Caster.Skills[SkillName.Magery].Value + Caster.Skills[SkillName.EvalInt].Value)/180)));
						length += TimeSpan.FromSeconds(10);
					}

					string args = String.Format("{0}\t{1}\t{2}", percentage, percentage, percentage);

					BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Bless, 1075847, 1075848, length, m, args.ToString()));
				}
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private BlessSpell m_Owner;

			public InternalTarget( BlessSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Beneficial )
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
