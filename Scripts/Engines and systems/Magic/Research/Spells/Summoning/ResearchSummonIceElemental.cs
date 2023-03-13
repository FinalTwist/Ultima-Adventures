using System;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Research
{
	public class ResearchSummonIceElemental : ResearchSpell
	{
		public override int spellIndex { get { return 21; } }
		public int CirclePower = 7;
		public static int spellID = 21;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.25 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
				269,
				9050
			);

		public ResearchSummonIceElemental( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if ( (Caster.Followers + 3) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				double time = DamagingSkill( Caster )*6;
					if ( time > 1500 ){ time = 1500.0; }
					if ( time < 480 ){ time = 480.0; }

				TimeSpan duration = TimeSpan.FromSeconds( time );

				BaseCreature m_Creature = new IceElemental();
				m_Creature.ControlSlots = 3;
				SpellHelper.Summon( m_Creature, Caster, 0x216, duration, false, false );
				m_Creature.FixedParticles(0x3728, 8, 20, 5042, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.Head );
				Server.Misc.Research.ConsumeScroll( Caster, true, spellIndex, false );
			}

			FinishSequence();
		}
	}
}