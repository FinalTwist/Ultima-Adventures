using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Undead
{
	public class UndeadBloodPactSpell : UndeadSpell
	{
		private static SpellInfo m_Info = new SpellInfo( "", "", 239, 9021 );
		public override double RequiredSkill{ get{ return 25.0; } }
		public override int RequiredMana{ get{ return 0; } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }

		
		public UndeadBloodPactSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}
		
		public void Target( Mobile m )
		{
			if ( Caster ==  m )
			{
				Caster.SendMessage( "You cannot heal yourself" ); // Cannot heal self.
			}
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( m is BaseCreature && ((BaseCreature)m).IsAnimatedDead )
			{
				Caster.SendLocalizedMessage( 1061654 ); // You cannot heal that which is not alive.
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
				
				int toHeal = (int)(Caster.Skills[SkillName.SpiritSpeak].Value * 0.4);
				toHeal += Utility.Random( 8, 15 );
				
				int toBleed = (int)(Caster.Skills[SkillName.SpiritSpeak].Value * 0.2);
				toBleed += Utility.Random( 5, 10 );
				
				m.Heal( toHeal );
				
				m.PlaySound( 0x19C );
				m.FixedParticles( 0x377A, 244, 25, 9950, 31, 0, EffectLayer.Waist );
				Caster.FixedParticles( 0x377A, 244, 25, 9950, 31, 0, EffectLayer.Waist );
				Caster.Damage( toBleed );
			}
			
			FinishSequence();
		}
		
		public class InternalTarget : Target
		{
			private UndeadBloodPactSpell m_Owner;
			
			public InternalTarget( UndeadBloodPactSpell owner ) : base( 12, false, TargetFlags.Beneficial )
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
