using System;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Jester
{
	public class JumpAround : JesterSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Jump Around", "Now you see me...",
				-1,
				0
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.5 ); } }
		public override int RequiredTithing{ get{ return 20; } }
		public override int RequiredMana{ get{ return 20; } }

		public JumpAround( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( Server.Misc.WeightOverloading.IsOverloaded( Caster ) )
			{
				Caster.SendLocalizedMessage( 502359, "", 0x22 ); // Thou art too encumbered to move.
				return false;
			}

			return SpellHelper.CheckTravel( Caster, TravelCheckType.TeleportFrom );
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			IPoint3D orig = p;
			Map map = Caster.Map;

			SpellHelper.GetSurfaceTop( ref p );

			if ( Server.Misc.WeightOverloading.IsOverloaded( Caster ) )
			{
				Caster.SendLocalizedMessage( 502359, "", 0x22 ); // Thou art too encumbered to move.
			}
			else if ( !SpellHelper.CheckTravel( Caster, TravelCheckType.TeleportFrom ) )
			{
			}
			else if ( !SpellHelper.CheckTravel( Caster, map, new Point3D( p ), TravelCheckType.TeleportTo ) )
			{
			}
			else if ( map == null || !map.CanSpawnMobile( p.X, p.Y, p.Z ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( SpellHelper.CheckMulti( new Point3D( p ), map ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( CheckSequence() )
			{
				if (Caster is PlayerMobile){ Point3D peto = new Point3D( p ); BaseCreature.TeleportPets( Caster, peto, map, false ); }
				SpellHelper.Turn( Caster, orig );

				Mobile m = Caster;

				Point3D from = m.Location;
				Point3D to = new Point3D( p );

				Effects.SendLocationParticles( EffectItem.Create( Caster.Location, Caster.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 0, 0, 5042, 0 );

				m.Location = to;
				m.ProcessDelta();

				Effects.SendLocationParticles( EffectItem.Create( Caster.Location, Caster.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 0, 0, 5042, 0 );

				Caster.PlaySound( Caster.Female ? 779 : 1050 );
				Caster.Say( "*ah ha!*" );
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private JumpAround m_Owner;

			public InternalTarget( JumpAround owner ) : base( Core.ML ? 11 : 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;

				if ( p != null )
					m_Owner.Target( p );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}