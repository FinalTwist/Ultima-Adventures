using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Mystic
{
	public class QuiveringPalm : MysticSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Quivering Palm", "Summ Cah Beh Ra",
				269,
				0
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.5 ); } }

		public override double RequiredSkill{ get{ return 20.0; } }
		public override int RequiredMana{ get{ return 20; } }
		public override int RequiredTithing{ get{ return 20; } }
		public override bool BlocksMovement{ get{ return false; } }
		public override int MysticSpellCircle{ get{ return 1; } }

		public QuiveringPalm( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			BaseWeapon weapon = Caster.Weapon as BaseWeapon;

			if ( weapon == null || weapon is Fists )
			{
				Caster.SendMessage( "You must be wearing some type of pugilist gloves!" );
			}
			else if ( !( weapon is GlovesOfThePugilist || weapon is GiftPugilistGloves || weapon is LevelPugilistGloves || weapon is PugilistGloves || weapon is PugilistGlove ) )
			{
				Caster.SendMessage( "You must be wearing some type of pugilist gloves!" );
			}
			else if ( CheckSequence() )
			{
				IEntity from = new Entity( Serial.Zero, new Point3D( Caster.X, Caster.Y, Caster.Z ), Caster.Map );
				IEntity to = new Entity( Serial.Zero, new Point3D( Caster.X, Caster.Y, Caster.Z + 50 ), Caster.Map );

				Caster.PlaySound( 0x212 );
				Effects.SendLocationParticles( EffectItem.Create( Caster.Location, Caster.Map, EffectItem.DefaultDuration ), 0x376A, 1, 29, 0x47D, 2, 9962, 0 );

				double seconds = Caster.Skills[SkillName.Wrestling].Value;

				TimeSpan duration = TimeSpan.FromSeconds( seconds );

				Timer t = (Timer)m_Table[weapon];

				if ( t != null )
					t.Stop();

				weapon.Consecrated = true;

				m_Table[weapon] = t = new ExpireTimer( weapon, duration );

				t.Start();
			}

			FinishSequence();
		}

		private static Hashtable m_Table = new Hashtable();

		private class ExpireTimer : Timer
		{
			private BaseWeapon m_Weapon;

			public ExpireTimer( BaseWeapon weapon, TimeSpan delay ) : base( delay )
			{
				m_Weapon = weapon;
				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				m_Weapon.Consecrated = false;
				Effects.PlaySound( m_Weapon.GetWorldLocation(), m_Weapon.Map, 0x1F8 );
				m_Table.Remove( this );
			}
		}
	}
}