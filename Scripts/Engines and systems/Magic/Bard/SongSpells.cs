using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using System.Threading;

namespace Server.Misc
{
    class BardFunctions
    {
		public static void UseBardInstrument( Item instrument, bool succeed, Mobile singer )
		{
			if ( instrument == null )
				return;

			if ( instrument is BaseInstrument ){}  else { return; }

			if ( singer == null )
				return;

			BaseInstrument harp = (BaseInstrument)instrument;

			if ( succeed == true )
			{
				singer.PlaySound(harp.SuccessSound);
			}
			else
			{
				singer.PlaySound(harp.FailureSound);
			}

			if ( harp.UsesRemaining > 1 )
			{
				harp.UsesRemaining--;
			}
			else
			{
				if ( singer != null )
					singer.SendLocalizedMessage( 502079 ); // The instrument played its last tune.
				instrument.Delete();
			}
		}
	}
}

namespace Server.Spells.Song
{
	public abstract class Song : Spell 
	{
		public abstract double RequiredSkill{ get; } 
		public abstract int RequiredMana{ get; } 

		public override SkillName CastSkill{ get{ return SkillName.Musicianship; } } 
		public override SkillName DamageSkill{ get{ return SkillName.Musicianship; } } 

		public override bool ClearHandsOnCast{ get{ return false; } } 

		public Song( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info ) 
		{ 
		}

		public static int MusicSkill( Mobile m )
		{
			return (int)(m.Skills[SkillName.Musicianship].Value + m.Skills[SkillName.Provocation].Value + m.Skills[SkillName.Discordance].Value + m.Skills[SkillName.Peacemaking].Value );
		}

		public override void DoFizzle()
		{
			Caster.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1115710 ); // The song fizzles.
			// 1115737 You feel invigorated by the bard's spellsong
			// 1115758 The bard's song fills you with resilience
			// 1115759 The bard's song fills you with invincible
			// 1115774 You halt your spellsong
			// 1115938 Your spellsong has finished
			// 1149722 Your spellsong has ended
		}

		public override void GetCastSkills( out double min, out double max ) 
		{ 
			min = RequiredSkill; 
			max = RequiredSkill + 30; 
		} 

		public override int GetMana() 
		{ 
			return RequiredMana; 
		}
	} 
}