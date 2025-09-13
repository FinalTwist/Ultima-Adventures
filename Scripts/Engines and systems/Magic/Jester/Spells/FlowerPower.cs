using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;
using Server.Misc;

namespace Server.Spells.Jester
{
	public class FlowerPower : JesterSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Flower Power", "Want to smell the flower?",
				-1,
				0
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
		public override int RequiredTithing{ get{ return 50; } }
		public override int RequiredMana{ get{ return 20; } }

		public FlowerPower( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			switch ( Utility.Random( 3 ))
			{
				case 0: Caster.PlaySound( Caster.Female ? 794 : 1066 ); break;
				case 1: Caster.PlaySound( Caster.Female ? 801 : 1073 ); break;
			}

			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return true; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) )
			{
				int damage = 1 + (int)( (Caster.Skills[SkillName.Begging].Value / 5) + (Caster.Skills[SkillName.EvalInt].Value / 3) );
				Caster.MovingParticles( m, 0x3818, 7, 0, false, false, 0xB44, 0, 0 );
				Caster.PlaySound( 0x025 );
				Effects.SendLocationEffect( m.Location, m.Map, 0x23B2, 20, 0xB50, 0 );

				if ( Caster.Skills[SkillName.Begging].Value >= Utility.RandomMinMax( 50, 300 ) && m != null )
				{
					int goo = 0;

					foreach ( Item splash in m.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter ){ goo++; } }

					if ( goo == 0 )
					{
						Point3D p = m.Location;
						MonsterSplatter.AddSplatter( p.X, p.Y, p.Z, m.Map, p, Caster, "poisonous slime", 1167, 0 );
					}
				}

				AOS.Damage( m, Caster, damage, 50, 0, 0, 50, 0 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private FlowerPower m_Owner;

			public InternalTarget( FlowerPower owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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