using System;
using Server;
using Server.Spells;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using System.Collections.Generic;
using System.Collections;
using Server.Gumps;

namespace Server.Spells.Research
{
	public abstract class ResearchSpell : Spell
	{
		public virtual int spellIndex { get { return 1; } }
		public abstract double RequiredSkill { get; }
		public abstract int RequiredMana{ get; }
		public override bool ClearHandsOnCast { get { return true; } }
		public override SkillName CastSkill { get { return SkillName.Inscribe; } }
		public override SkillName DamageSkill { get { return SkillName.Inscribe; } }
		public override int CastRecoveryBase { get { return 1; } }

		public static double CastingSkill( Mobile from )
		{
			double skill = from.Skills[SkillName.Necromancy].Value;
				if ( from.Skills[SkillName.Magery].Value > skill ){ skill = from.Skills[SkillName.Magery].Value; }

			return skill;
		}

		public static double DamagingSkill( Mobile from )
		{
			return ( from.Skills[SkillName.Necromancy].Value + from.Skills[SkillName.Magery].Value + from.Skills[SkillName.SpiritSpeak].Value + from.Skills[SkillName.EvalInt].Value ) / 2;
		}

		public static void KarmaMod( Mobile from, int mod )
		{
			int karma = -(int)(70 + mod);
			if ( karma != 0 ){ Misc.Titles.AwardKarma( from, karma, true ); }
		}

		public static bool SpellPrepared( Mobile from, int spellIndex )
		{
			if ( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) != null )
			{
				ResearchBag bag = (ResearchBag)( Server.Misc.ResearchBarSettings.ResearchMaterials( from ) );
				if ( Server.Misc.Research.GetPrepared( bag, spellIndex ) > 0 )
				{
					return true;
				}
			}
			return false;
		}

		public ResearchSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if ( CastingSkill( Caster ) < RequiredSkill )
			{
				Caster.SendMessage( "You must have at least " + RequiredSkill + " in magery or necromancy to cast this spell." );
				return false;
			}
			else if ( Caster.Mana < GetMana() )
			{
				Caster.SendMessage( "You must have at least " + GetMana() + " mana to cast this spell." );
				return false;
			}
			else if ( !SpellPrepared( Caster, spellIndex ) )
			{
				Caster.SendMessage( "You do not have that spell prepared." );
				return false;
			}

			return true;
		}

		public override bool CheckFizzle()
		{
			if ( CastingSkill( Caster ) < RequiredSkill )
			{
				Caster.SendMessage( "You must have at least " + RequiredSkill + " in magery or necromancy to cast this spell." );
				return false;
			}
			else if ( Caster.Mana < GetMana() )
			{
				Caster.SendMessage( "You must have at least " + GetMana() + " mana to cast this spell." );
				return false;
			}
			else if ( !SpellPrepared( Caster, spellIndex ) )
			{
				Caster.SendMessage( "You do not have that spell prepared." );
				return false;
			}

			return true;
		}

		public override int GetMana()
		{
			return ScaleMana( RequiredMana );
		}

		public override void DoHurtFizzle()
		{
			Caster.PlaySound( 0x1D6 );
		}

		public override void OnDisturb( DisturbType type, bool message )
		{
			base.OnDisturb( type, message );

			if ( message )
				Caster.PlaySound( 0x1D6 );
		}

		public override void OnBeginCast()
		{
			base.OnBeginCast();

			Caster.FixedEffect( 0x37C4, 10, 42, 4, 3 );
		}

		public override void GetCastSkills( out double min, out double max )
		{
			min = RequiredSkill;
			max = RequiredSkill + 40.0;
		}

		public int ComputePowerValue( int div )
		{
			return ComputePowerValue( Caster, div );
		}

		public static int ComputePowerValue( Mobile from, int div )
		{
			if ( from == null )
				return 0;

			int v = (int) Math.Sqrt( ( from.Karma * -1 ) + 20000 + ( ( from.Skills.Necromancy.Fixed + from.Skills.Magery.Fixed ) * 10 ) );

			return v / div;
		}
	}
}