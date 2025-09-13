using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Spells.Fourth
{
	public class GreaterHealSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Greater Heal", "In Vas Mani",
				204,
				9061,
				Reagent.Garlic,
				Reagent.Ginseng,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Fourth; } }

		public GreaterHealSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
			else if ( m is PlayerMobile && m.FindItemOnLayer( Layer.Ring ) != null && m.FindItemOnLayer( Layer.Ring ) is OneRing)
			{
				Caster.SendMessage( "The ONE RING convinces you not to do that, and you listen to it... " );
				return;
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
				// Algorithm: (40% of magery OR 60% for soulshard) + (1-10)
				int toHeal = CalculateMobileBenefit(Caster, 2.5, 1.65);
				toHeal += Utility.Random( 1, 10 );
				toHeal = Server.Misc.MyServerSettings.PlayerLevelMod( toHeal, Caster );

				if (Caster is PlayerMobile && ((PlayerMobile)Caster).Sorcerer())
					toHeal = (int)((double)toHeal * 1.25);

				//m.Heal( toHeal, Caster );
				SpellHelper.Heal( toHeal, m, Caster );
				if (Scroll is SoulShard) {
					((SoulShard)Scroll).SuccessfulCast = true;
				}
				m.FixedParticles( 0x376A, 9, 32, 5030, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.Waist );
				m.PlaySound( 0x202 );
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private GreaterHealSpell m_Owner;

			public InternalTarget( GreaterHealSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Beneficial )
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