using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Syth
{
	public class PsychicBlast : SythSpell
	{
		public override int spellIndex { get { return 277; } }
		public int CirclePower = 6;
		public static int spellID = 277;
		public override int RequiredTithing{ get{ return Int32.Parse(  Server.Spells.Syth.SythSpell.SpellInfo( spellIndex, 10 )); } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.5 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( spellIndex, 2 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse(  Server.Spells.Syth.SythSpell.SpellInfo( spellIndex, 3 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Spells.Syth.SythSpell.SpellInfo( spellID, 1 ),
				Server.Misc.Research.CapsCast( Server.Spells.Syth.SythSpell.SpellInfo( spellID, 4 ) ),
				-1,
				0
			);

		public PsychicBlast( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		private void AosDelay_Callback( object state )
		{
			object[] states = (object[])state;
			Mobile caster = (Mobile)states[0];
			Mobile target = (Mobile)states[1];
			Mobile defender = (Mobile)states[2];
			int min = (int)states[3];
			int max = (int)states[4];

			if ( caster.HarmfulCheck( defender ) )
			{
				AOS.Damage( target, caster, Utility.RandomMinMax( min, max ), 0, 0, 0, 0, 100 );

				Point3D boom = new Point3D( target.X+1, target.Y+2, target.Z+5);
				Effects.SendLocationEffect( boom, target.Map, 0x3822, 60, 10, 0xAF1, 0 );
				target.PlaySound( 0x658 );
			}
		}

		public override bool DelayedDamage{ get{ return !Core.AOS; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( Caster.CanBeHarmful( m ) && CheckSequence() && CheckFizzle() )
			{
				Mobile from = Caster, target = m;

				int min = 26;
				int max = (int)( GetSythDamage( Caster ) / 3 );

				if ( max > 125 )
					max = 125;

				Timer.DelayCall( TimeSpan.FromSeconds( 0.1 ),
					new TimerStateCallback( AosDelay_Callback ),
					new object[]{ Caster, target, m, min, max } );
			}

			FinishSequence();
		}

		public override double GetSlayerDamageScalar( Mobile target )
		{
			return 1.0; //This spell isn't affected by slayer spellbooks
		}

		private class InternalTarget : Target
		{
			private PsychicBlast m_Owner;

			public InternalTarget( PsychicBlast owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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