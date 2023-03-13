using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Spells;

namespace Server.Spells.HolyMan
{
	public class TrialByFireSpell : HolyManSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Trial by Fire", "Igne Iudicii",
				266,
				9040
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }
		public override int RequiredTithing{ get{ return 500; } }
		public override double RequiredSkill{ get{ return 30.0; } }
		public override int RequiredMana{ get{ return 15; } }

		public TrialByFireSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( Core.AOS )
				return true;

			if ( Caster.MagicDamageAbsorb > 0 )
			{
				Caster.SendMessage( "You are already under the effects of this prayer." );
				return false;
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendMessage( "The spirits do not seem to answer you at this time." );
				return false;
			}

			return true;
		}

		private static Hashtable m_Table = new Hashtable();

		public override void OnCast()
		{
			if ( Caster.MagicDamageAbsorb > 0 )
			{
				Caster.SendMessage( "You are already under the effects of this prayer." );
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendMessage( "The spirits do not seem to answer you at this time." );
			}
			else if ( CheckSequence() )
			{
				if ( Caster.BeginAction( typeof( DefensiveSpell ) ) )
				{
					int value = (int)( ( Caster.Skills[SkillName.Healing].Value + Caster.Skills[SkillName.SpiritSpeak].Value ) / 4 );
					Caster.MagicDamageAbsorb = value;
					Caster.SendMessage( "Your body is covered by holy flames." );
					Caster.FixedParticles( 0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot );
					Caster.PlaySound( 0x208 );
				}
				else
				{
					Caster.SendMessage( "The spirits do not seem to answer you at this time." );
				}

				FinishSequence();
			}
		}
	}
}
