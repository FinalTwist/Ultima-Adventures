using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Research
{
	public class ResearchRestoration : ResearchSpell
	{
		public override int spellIndex { get { return 63; } }
		public int CirclePower = 8;
		public static int spellID = 63;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.25 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
				204,
				9061
			);

		public ResearchRestoration( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
			else if ( m is BaseCreature && ((BaseCreature)m).IsAnimatedDead )
			{
				Caster.SendMessage( "This spell will have no affect on that." );
			}
			else if ( m.IsDeadBondedPet )
			{
				Caster.SendMessage( "This spell will have no affect on that." );
			}
			else if ( m is Golem )
			{
				Caster.SendMessage( "This spell will have no affect on that." );
			}
			else if ( CheckBSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				int toHeal = (int)(DamagingSkill( Caster )/2);
					if ( toHeal > 125 ){ toHeal = 125; }
					if ( toHeal < 12 ){ toHeal = 12; }

				SpellHelper.Heal( toHeal, m, Caster );
				m.Mana = toHeal;
				m.Stam = toHeal;

				m.FixedParticles( 0x3039, 9, 32, 5030, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.Waist );
				m.PlaySound( 0x655 );
				Server.Misc.Research.ConsumeScroll( Caster, true, spellIndex, false );
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private ResearchRestoration m_Owner;

			public InternalTarget( ResearchRestoration owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Beneficial )
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
