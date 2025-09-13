using System;
using Server;
using Server.Spells;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using System.Collections.Generic;
using System.Collections;

namespace Server.Spells.DeathKnight
{
	public abstract class DeathKnightSpell : Spell
	{
		public abstract int RequiredTithing { get; }
		public abstract double RequiredSkill { get; }
		public abstract int RequiredMana{ get; }
		public override bool ClearHandsOnCast { get { return false; } }
		public override SkillName CastSkill { get { return SkillName.Chivalry; } }
		public override SkillName DamageSkill { get { return SkillName.Chivalry; } }
		public override int CastRecoveryBase { get { return 7; } }

		public DeathKnightSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if ( Caster.Karma > 0 )
			{
				Caster.SendMessage( "You have too much Karma to cast this spell." );
				return false;
			}
			else if ( Caster.Skills[CastSkill].Value < RequiredSkill )
			{
				Caster.SendMessage( "You must have at least " + RequiredSkill + " Chivalry to cast this spell." );
				return false;
			}
			else if ( GetSoulsInLantern( Caster ) < RequiredTithing )
			{
				Caster.SendMessage( "You must have at least " + RequiredTithing + " Souls to cast this spell." );
				return false;
			}
			else if ( Caster.Mana < GetMana() )
			{
				Caster.SendMessage( "You must have at least " + GetMana() + " Mana to cast this spell." );
				return false;
			}

			return true;
		}

		public override bool CheckFizzle()
		{
			int requiredTithing = this.RequiredTithing;

			if ( AosAttributes.GetValue( Caster, AosAttribute.LowerRegCost ) > Utility.Random( 100 ) )
				requiredTithing = 0;

			int mana = GetMana();

			if ( Caster.Karma > 0 )
			{
				Caster.SendMessage( "You have too much Karma to cast this spell." );
				return false;
			}
			else if ( Caster.Skills[CastSkill].Value < RequiredSkill )
			{
				Caster.SendMessage( "You must have at least " + RequiredSkill + " Chivalry to cast this spell." );
				return false;
			}
			else if ( GetSoulsInLantern( Caster ) < requiredTithing )
			{
				Caster.SendMessage( "You must have at least " + requiredTithing + " Souls to cast this spell." );
				return false;
			}
			else if ( Caster.Mana < mana )
			{
				Caster.SendMessage( "You must have at least " + mana + " Mana to cast this spell." );
				return false;
			}

			if ( requiredTithing > 0 ){ DrainSoulsInLantern( Caster, requiredTithing ); }

			//Caster.Mana -= mana;

			return true;
		}

		public override void DoFizzle()
		{
			Caster.PrivateOverheadMessage(MessageType.Regular, 0x3B2, false, "You fail to invoke the power.", Caster.NetState);
			Caster.FixedParticles( 0x3735, 1, 30, 9503, EffectLayer.Waist );
			Caster.PlaySound( 0x19D );
			Caster.NextSpellTime = Core.TickCount;
		}

		public override int ComputeKarmaAward()
		{
			int circle = (int)(RequiredSkill / 10);
				if ( circle < 1 ){ circle = 1; }
			return -( 40 + ( 10 * circle ) );
		}

		public override int GetMana()
		{
			return ScaleMana( RequiredMana );
		}

		public override void DoHurtFizzle()
		{
			Caster.PlaySound( 0x19D );
		}

		public override void OnDisturb( DisturbType type, bool message )
		{
			base.OnDisturb( type, message );

			if ( message )
				Caster.PlaySound( 0x19D );
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

			int v = (int) Math.Sqrt( ( from.Karma * -1 ) + 20000 + ( from.Skills.Chivalry.Fixed * 10 ) );

			return v / div;
		}

		public static void DrainSoulsInLantern( Mobile from, int tithing )
		{
			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			{
				if ( item is SoulLantern )
				{
					SoulLantern lantern = (SoulLantern)item;
					if ( lantern.owner == from )
					{
						lantern.TrappedSouls = lantern.TrappedSouls - tithing;
						if ( lantern.TrappedSouls < 1 ){ lantern.TrappedSouls = 0; }
						lantern.InvalidateProperties();
					}
				}
			}
		}

		public static int GetSoulsInLantern( Mobile from )
		{
			int souls = 0;

			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			{
				if ( item is SoulLantern )
				{
					SoulLantern lantern = (SoulLantern)item;
					if ( lantern.owner == from )
					{
						souls = lantern.TrappedSouls;
					}
				}
			}

			return souls;
		}

		public static double GetKarmaPower( Mobile from )
		{
			int karma = ( from.Karma * -1 );
				if ( karma < 1 ){ karma = 0; }
				if ( karma > MyServerSettings.KarmaMax() ){ karma = MyServerSettings.KarmaMax(); }

			double hate = karma / 125;

			return hate;
		}
	}
}