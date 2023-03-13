using System;
using Server;
using Server.Network;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Misc;

namespace Server.Spells.Undead
{
	public abstract class UndeadSpell : Spell
	{
		public abstract double RequiredSkill{ get; }
		public abstract int RequiredMana{ get; }

		public override SkillName CastSkill{ get{ return SkillName.Necromancy; } }
		public override SkillName DamageSkill{ get{ return SkillName.SpiritSpeak; } }

		public override bool ClearHandsOnCast{ get{ return false; } }

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if ( Caster.Skills[CastSkill].Value < RequiredSkill )
			{
				Caster.SendMessage("You lack the skills to use this necromancer liquid!");
				return false;
			}

			if ( Caster.Karma > -2459 ){ Titles.AwardKarma( Caster, -50, true ); }

			return true;
		}

		public UndeadSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info )
		{
		}

		public override void GetCastSkills( out double min, out double max )
		{
			min = RequiredSkill;
			max = RequiredSkill + 20.0;
		}

		public override int GetMana()
		{
			return RequiredMana;
		}
	}
}