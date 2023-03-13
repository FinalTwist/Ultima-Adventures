using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Research
{
	public class ResearchHealingTouch : ResearchSpell
	{
		public override int spellIndex { get { return 15; } }
		public int CirclePower = 4;
		public static int spellID = 15;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.25 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
				204,
				9061
			);

		public ResearchHealingTouch( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
            else if ( !Caster.InRange( m, 2 ) )
            {
                Caster.SendLocalizedMessage( 501042 ); // Target is not close enough.
            }
			else if ( m is BaseCreature && ((BaseCreature)m).IsAnimatedDead )
			{
				Caster.SendLocalizedMessage( 1061654 ); // You cannot heal that which is not alive.
			}
			else if ( m.IsDeadBondedPet )
			{
				Caster.SendLocalizedMessage( 1060177 ); // You cannot heal a creature that is already dead!
			}
			else if ( m is Golem )
			{
				Caster.LocalOverheadMessage( MessageType.Regular, 0x3B2, 500951 ); // You cannot heal that.
			}
			else if ( m.Poisoned || Server.Items.MortalStrike.IsWounded( m ) )
			{
				Caster.LocalOverheadMessage( MessageType.Regular, 0x22, (Caster == m) ? 1005000 : 1010398 );
			}
			else if ( CheckBSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				int toHeal = (int)(DamagingSkill( Caster )/2);
					if ( toHeal > 125 ){ toHeal = 125; }
					if ( toHeal < 12 ){ toHeal = 12; }

				SpellHelper.Heal( toHeal, m, Caster );

				m.FixedParticles( 0x376A, 9, 32, 5030, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.Waist );
				m.PlaySound( 0x202 );
				Server.Misc.Research.ConsumeScroll( Caster, true, spellIndex, false );
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private ResearchHealingTouch m_Owner;

			public InternalTarget( ResearchHealingTouch owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Beneficial )
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