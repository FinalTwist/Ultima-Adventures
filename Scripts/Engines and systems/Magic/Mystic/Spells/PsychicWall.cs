using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells.Mystic
{
	public class PsychicWall : MysticSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Psychic Wall", "Cah Summ Om Lum",
				269,
				0
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }
		public override int RequiredTithing{ get{ return 500; } }
		public override double RequiredSkill{ get{ return 60.0; } }
		public override int RequiredMana{ get{ return 45; } }
		public override int MysticSpellCircle{ get{ return 4; } }

		public PsychicWall( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( Core.AOS )
				return true;

			if ( Caster.MagicDamageAbsorb > 0 )
			{
				Caster.SendMessage( "Your mind is already protected!" );
				return false;
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendMessage( "Your mind cannot be shielded at this time!" );
				return false;
			}

			return true;
		}

		private static Hashtable m_Table = new Hashtable();

		public override void OnCast()
		{
			if ( Caster.MagicDamageAbsorb > 0 )
			{
				Caster.SendMessage( "Your mind is already protected!" );
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendMessage( "Your mind cannot be shielded at this time!" );
			}
			else if ( CheckSequence() )
			{
				if ( Caster.BeginAction( typeof( DefensiveSpell ) ) )
				{
					int value = (int)( Caster.Skills[SkillName.Wrestling].Value / 2 );
					Caster.MagicDamageAbsorb = value;
					Caster.FixedParticles( 0x3039, 10, 15, 5038, 0, 2, EffectLayer.Head );
					Caster.PlaySound( 0x5BC );
				}
				else
				{
					Caster.SendMessage( "Your mind cannot be shielded at this time!" );
				}

				FinishSequence();
			}
		}
	}
}