using System;
using Server;
using System.Collections;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Jedi
{
	public class Celerity : JediSpell
	{
		public override int spellIndex { get { return 284; } }
		public int CirclePower = 3;
		public static int spellID = 284;
		public override int RequiredTithing{ get{ return Int32.Parse(  Server.Spells.Jedi.JediSpell.SpellInfo( spellIndex, 10 )); } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.5 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Spells.Jedi.JediSpell.SpellInfo( spellIndex, 2 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse(  Server.Spells.Jedi.JediSpell.SpellInfo( spellIndex, 3 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Spells.Jedi.JediSpell.SpellInfo( spellID, 1 ),
				Server.Misc.Research.CapsCast( Server.Spells.Jedi.JediSpell.SpellInfo( spellID, 4 ) ),
				-1,
				0
			);

		public Celerity( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public static Hashtable TableJediRunning = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableJediRunning[m] != null );
		}

		public static bool UnderEffect( Mobile m )
		{
			return TableJediRunning.Contains( m );
		}

		public static void RemoveEffect( Mobile m )
		{
			m.Send(SpeedControl.Disable);
			TableJediRunning.Remove( m );
			m.EndAction( typeof( Celerity ) );
		}

		public override void OnCast()
		{
			Item shoes = Caster.FindItemOnLayer( Layer.Shoes );

            if ( Caster.Mounted )
            {
                Caster.SendMessage( "You cannot use this power while on a mount!" );
            }
			else if ( shoes is BootsofHermes )//|( shoes is BootsofHermes )|( shoes is BootsofHermes )
			{
                Caster.SendMessage( "You cannot use this power while wearing those magical boots!" );
			}
			else if (Caster is PlayerMobile && ((PlayerMobile)Caster).SoulBound && ((PlayerMobile)Caster).sbmasterspeed)
			{
                Caster.SendMessage( "You are already moving with speed!" );
			}
			else if ( CheckFizzle() )
			{
				if ( !Caster.CanBeginAction( typeof( Celerity ) ) )
				{
					Celerity.RemoveEffect( Caster );
				}

				int TotalTime = (int)( GetJediDamage( Caster ) * 4 );
					if ( TotalTime < 600 ){ TotalTime = 600; }
				TableJediRunning[Caster] = SpeedControl.MountSpeed;
				Caster.Send(SpeedControl.MountSpeed);
				new InternalTimer( Caster, TimeSpan.FromSeconds( TotalTime ) ).Start();
				Caster.BeginAction( typeof( Celerity ) );
				Point3D air = new Point3D( ( Caster.X+1 ), ( Caster.Y+1 ), ( Caster.Z+5 ) );
				Effects.SendLocationParticles(EffectItem.Create(air, Caster.Map, EffectItem.DefaultDuration), 0x5590, 9, 32, 0, 0, 5022, 0);
				Caster.PlaySound( 0x64F );
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
					Celerity.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}
}