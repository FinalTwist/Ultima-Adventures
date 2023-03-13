using System;
using Server;
using System.Collections;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Syth
{
	public class SythSpeed : SythSpell
	{
		public override int spellIndex { get { return 274; } }
		public int CirclePower = 3;
		public static int spellID = 274;
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

		public SythSpeed( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public static Hashtable TableSythRunning = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableSythRunning[m] != null );
		}

		public static bool UnderEffect( Mobile m )
		{
			return TableSythRunning.Contains( m );
		}

		public static void RemoveEffect( Mobile m )
		{
			m.Send(SpeedControl.Disable);
			TableSythRunning.Remove( m );
			m.EndAction( typeof( SythSpeed ) );
		}

		public override void OnCast()
		{
			Item shoes = Caster.FindItemOnLayer( Layer.Shoes );

            if ( Caster.Mounted )
            {
                Caster.SendMessage( "You cannot use this power while on a mount!" );
            }
			else if ( shoes is BootsofHermes )
			{
                Caster.SendMessage( "You cannot use this power while wearing those magical boots!" );
			}
			else if (Caster is PlayerMobile && ((PlayerMobile)Caster).SoulBound && ((PlayerMobile)Caster).sbmasterspeed)
			{
                Caster.SendMessage( "You are already moving with speed!" );
			}
			else if ( CheckFizzle() )
			{
				if ( !Caster.CanBeginAction( typeof( SythSpeed ) ) )
				{
					SythSpeed.RemoveEffect( Caster );
				}

				int TotalTime = (int)( GetSythDamage( Caster ) * 4 );
					if ( TotalTime < 600 ){ TotalTime = 600; }
				TableSythRunning[Caster] = SpeedControl.MountSpeed;
				Caster.Send(SpeedControl.MountSpeed);
				new InternalTimer( Caster, TimeSpan.FromSeconds( TotalTime ) ).Start();
				Caster.BeginAction( typeof( SythSpeed ) );
				Point3D air = new Point3D( ( Caster.X+1 ), ( Caster.Y+1 ), ( Caster.Z+5 ) );
				Effects.SendLocationParticles(EffectItem.Create(air, Caster.Map, EffectItem.DefaultDuration), 0x37CC, 9, 32, 0xB00, 0, 5022, 0);
				Caster.PlaySound( 0x654 );
			}

            FinishSequence();
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile Caster, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = Caster;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					SythSpeed.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}
}