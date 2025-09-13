using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Research
{
	public class ResearchSwarm : ResearchSpell
	{
		public override int spellIndex { get { return 33; } }
		public int CirclePower = 5;
		public static int spellID = 33;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 6.00 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
				266,
				9040
			);

		public ResearchSwarm( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if( (Caster.Followers + 2) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			Caster.SendMessage( "Choose where you will unleash the swarm." );
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
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				double time = DamagingSkill( Caster )/1.2;
					if ( time > 200 ){ time = 200.0; }
					if ( time < 60 ){ time = 60.0; }

				TimeSpan duration = TimeSpan.FromSeconds( time );

				BaseCreature.Summon( new Swarm(), false, Caster, new Point3D( p ), 0x5CB, duration );
				Server.Misc.Research.ConsumeScroll( Caster, true, spellIndex, false );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private ResearchSwarm m_Owner;

			public InternalTarget( ResearchSwarm owner ) : base( Core.ML ? 10 : 12, true, TargetFlags.None )
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