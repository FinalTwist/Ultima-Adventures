using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Spells.Fourth
{
	public class LightningSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Lightning", "Por Ort Grav",
				239,
				9021,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Fourth; } }

		public LightningSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return false; } }

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

				double damage;

				int nBenefit = 0;
				if ( Caster is PlayerMobile ) // WIZARD
				{
					nBenefit = CalculateMobileBenefit(Caster, 25, 4);
				}

				damage = GetNewAosDamage( 15, 1, 6, m ) + nBenefit;

				if ( Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ) > 0 )
				{
					Point3D blast = new Point3D( ( m.X ), ( m.Y ), m.Z+10 );
					Effects.SendLocationEffect( blast, m.Map, 0x2A4E, 30, 10, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 );
					m.PlaySound( 0x029 );
				}
				else
				{
					m.BoltEffect( 0 );
				}

				SpellHelper.Damage( this, m, damage, 0, 0, 0, 0, 100 );
				if (Scroll is SoulShard) {
					((SoulShard)Scroll).SuccessfulCast = true;
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private LightningSpell m_Owner;

			public InternalTarget( LightningSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
