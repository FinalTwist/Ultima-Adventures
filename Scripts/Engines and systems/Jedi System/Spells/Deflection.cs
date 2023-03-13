using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells.Jedi
{
	public class Deflection : JediSpell
	{
		public override int spellIndex { get { return 286; } }
		public int CirclePower = 6;
		public static int spellID = 286;
		public override int RequiredTithing{ get{ return Int32.Parse(  Server.Spells.Jedi.JediSpell.SpellInfo( spellIndex, 10 )); } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Spells.Jedi.JediSpell.SpellInfo( spellIndex, 2 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse(  Server.Spells.Jedi.JediSpell.SpellInfo( spellIndex, 3 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Spells.Jedi.JediSpell.SpellInfo( spellID, 1 ),
				Server.Misc.Research.CapsCast( Server.Spells.Jedi.JediSpell.SpellInfo( spellID, 4 ) ),
				203,
				0
			);

		public Deflection( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if ( Caster.MagicDamageAbsorb > 0 )
			{
				Caster.SendMessage( "Your essence is already protected!" );
				return false;
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendMessage( "Your essence cannot deflect more at this time!" );
				return false;
			}

			return true;
		}

		private static Hashtable m_Table = new Hashtable();

		public override void OnCast()
		{
			if ( Caster.MagicDamageAbsorb > 0 )
			{
				Caster.SendMessage( "Your essence is already protected!" );
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendMessage( "Your essence cannot deflect more at this time!" );
			}
			else if ( CheckSequence() )
			{
				if ( Caster.BeginAction( typeof( DefensiveSpell ) ) && CheckFizzle() )
				{
					int min = 15;
					int max = (int)( GetJediDamage( Caster ) / 4 );
					Caster.MagicDamageAbsorb = Utility.RandomMinMax( min, max );
					Point3D air = new Point3D( ( Caster.X+1 ), ( Caster.Y+1 ), ( Caster.Z+5 ) );
					Effects.SendLocationParticles(EffectItem.Create(air, Caster.Map, EffectItem.DefaultDuration), 0x376A, 9, 32, 0xB41, 0, 5022, 0);
					Effects.PlaySound(Caster.Location, Caster.Map, 0x0F9);
				}
				else
				{
					Caster.SendMessage( "Your essence cannot deflect more at this time!" );
				}

				FinishSequence();
			}
		}
	}
}