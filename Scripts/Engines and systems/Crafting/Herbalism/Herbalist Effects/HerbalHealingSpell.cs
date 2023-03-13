using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells.Necromancy;

namespace Server.Spells.Herbalist
{
	public class HerbalHealingSpell : HerbalistSpell
	{
		private static SpellInfo m_Info = new SpellInfo( "", "", 239, 9021 );
		public override int HerbalistSpellCircle{ get{ return 4; } }
		public override double CastDelay{ get{ return 0.5; } }
		public override double RequiredSkill{ get{ return 45.0; } }
		public override int RequiredMana{ get{ return 0; } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }

		public HerbalHealingSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
				Caster.SendLocalizedMessage( 1061654 ); // You cannot heal that which is not alive.
			}
			else if ( m.IsDeadBondedPet )
			{
				Caster.SendLocalizedMessage( 1060177 ); // You cannot heal a creature that is already dead!
			}
			else if ( CheckBSequence( m, false ) )
			{
				SpellHelper.Turn( Caster, m );

				m.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
				m.PlaySound( 0x202 );

				StatMod mod;

				mod = m.GetStatMod( "[Magic] Str Offset" );
				if ( mod != null && mod.Offset < 0 )
					m.RemoveStatMod( "[Magic] Str Offset" );

				mod = m.GetStatMod( "[Magic] Dex Offset" );
				if ( mod != null && mod.Offset < 0 )
					m.RemoveStatMod( "[Magic] Dex Offset" );

				mod = m.GetStatMod( "[Magic] Int Offset" );
				if ( mod != null && mod.Offset < 0 )
					m.RemoveStatMod( "[Magic] Int Offset" );

				m.Paralyzed = false;
				m.CurePoison( Caster );

				int toHeal = (int)(Caster.Skills[SkillName.AnimalTaming].Value) + (int)(Caster.Skills[SkillName.AnimalLore].Value);
				SpellHelper.Heal( toHeal, m, Caster );

				EvilOmenSpell.TryEndEffect( m );
				StrangleSpell.RemoveCurse( m );
				CorpseSkinSpell.RemoveCurse( m );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private HerbalHealingSpell m_Owner;

			public InternalTarget( HerbalHealingSpell owner ) : base( 12, false, TargetFlags.Beneficial )
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