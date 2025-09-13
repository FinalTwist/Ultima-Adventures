using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.DeathKnight
{
	public class DevilPactSpell : DeathKnightSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Devil Pact", "Deumus Foedus",
				269,
				9050,
				false
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1 ); } }
		public override int RequiredTithing{ get{ return 98; } }
		public override double RequiredSkill{ get{ return 90.0; } }
		public override int RequiredMana{ get{ return 60; } }

		public DevilPactSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if ( ( Caster.Followers + 4 ) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			Map map = Caster.Map;

			SpellHelper.GetSurfaceTop( ref p );

			if ( map == null || !map.CanSpawnMobile( p.X, p.Y, p.Z ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() && CheckFizzle() )
			{
				TimeSpan duration;

				int nBenefit = 0;
				if ( Caster is PlayerMobile ) // WIZARD
				{
					nBenefit = (int)(Caster.Skills[SkillName.Chivalry].Value / 2);
				}

				if ( Core.AOS )
					duration = TimeSpan.FromSeconds( 90.0 + nBenefit );
				else
					duration = TimeSpan.FromSeconds( Utility.Random( 80, 40 ) + nBenefit );

				BaseCreature.Summon( new DevilPact(), false, Caster, new Point3D( p ), 0x212, duration );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private DevilPactSpell m_Owner;

			public InternalTarget( DevilPactSpell owner ) : base( Core.ML ? 10 : 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is IPoint3D )
					m_Owner.Target( (IPoint3D)o );
			}

			protected override void OnTargetOutOfLOS( Mobile from, object o )
			{
				from.SendLocalizedMessage( 501943 ); // Target cannot be seen. Try again.
				from.Target = new InternalTarget( m_Owner );
				from.Target.BeginTimeout( from, TimeoutTime - DateTime.UtcNow );
				m_Owner = null;
			}

			protected override void OnTargetFinish( Mobile from )
			{
				if ( m_Owner != null )
					m_Owner.FinishSequence();
			}
		}
	}
}