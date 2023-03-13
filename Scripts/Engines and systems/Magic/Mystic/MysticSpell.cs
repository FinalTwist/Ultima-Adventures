using System;
using Server;
using Server.Spells;
using Server.Network;
using Server.Items;

namespace Server.Spells.Mystic
{
	public abstract class MysticSpell : Spell
	{
		public abstract int RequiredTithing { get; }
		public abstract double RequiredSkill { get; }
		public abstract int RequiredMana{ get; }
		public override bool ClearHandsOnCast { get { return false; } }
		public override SkillName CastSkill { get { return SkillName.Wrestling; } }
		public override SkillName DamageSkill { get { return SkillName.Wrestling; } }
		public override int CastRecoveryBase { get { return 2; } }
		public abstract int MysticSpellCircle{ get; }

		public MysticSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			int mana = ScaleMana( RequiredMana );

			if ( !base.CheckCast( caster ) )
				return false;

			if ( Caster.TithingPoints < RequiredTithing )
			{
				Caster.SendLocalizedMessage( 1060173, RequiredTithing.ToString() ); // You must have at least ~1_TITHE_REQUIREMENT~ Tithing Points to use this ability,
				return false;
			}
			else if ( !MonkNotIllegal( Caster ) && !( this is CreateRobe ) )
			{
				Caster.SendMessage( "Your equipment or skills are not commensurate to that of a true monk." );
				return false;
			}
			else if ( this is WindRunner && Server.Misc.MyServerSettings.NoMountsInCertainRegions() && Server.Mobiles.AnimalTrainer.IsNoMountRegion( Region.Find( Caster.Location, Caster.Map ) ) )
			{
				Caster.SendMessage( "This ability doesn't seem to work in this place." );
				return false;
			}
			else if ( Caster.Mana < mana )
			{
				Caster.SendLocalizedMessage( 1060174, mana.ToString() ); // You must have at least ~1_MANA_REQUIREMENT~ Mana to use this ability.
				return false;
			}

			return true;
		}

		public static bool MonkNotIllegal( Mobile from )
		{
			if ( from.FindItemOnLayer( Layer.OneHanded ) != null )
			{
				Item oneHand = from.FindItemOnLayer( Layer.OneHanded );

				if ( oneHand is GlovesOfThePugilist || oneHand is GiftPugilistGloves || oneHand is LevelPugilistGloves || oneHand is PugilistGloves || oneHand is PugilistGlove ){}
				else if ( oneHand is BaseWeapon )
					return false;
			}

			if ( from.FindItemOnLayer( Layer.TwoHanded ) != null )
			{
				Item twoHand = from.FindItemOnLayer( Layer.TwoHanded );

				if ( twoHand is BaseWeapon )
					return false;

				if ( twoHand is BaseArmor )
				{
					if ( ((BaseArmor)twoHand).Attributes.SpellChanneling == 0 )
						return false;
				}
			}

			if ( Server.Misc.RegenRates.GetArmorOffset( from ) > 0 )
			{
				return false;
			}

			if ( from.FindItemOnLayer( Layer.OuterTorso ) != null )
			{
				Item robe = from.FindItemOnLayer( Layer.OuterTorso );
				if ( !(robe is MysticMonkRobe) )
					return false;
			}
			else { return false; }

			if ( from.Skills[SkillName.Focus].Base < 100 || from.Skills[SkillName.Meditation].Base < 100 )
			{
				return false;
			}

			return true;
		}

		public override bool CheckFizzle()
		{
			int requiredTithing = this.RequiredTithing;

			if ( AosAttributes.GetValue( Caster, AosAttribute.LowerRegCost ) > Utility.Random( 100 ) )
				requiredTithing = 0;

			int mana = ScaleMana( RequiredMana );

			if ( Caster.TithingPoints < requiredTithing )
			{
				Caster.SendLocalizedMessage( 1060173, RequiredTithing.ToString() ); // You must have at least ~1_TITHE_REQUIREMENT~ Tithing Points to use this ability,
				return false;
			}
			else if ( !MonkNotIllegal( Caster ) && !( this is CreateRobe ) )
			{
				Caster.SendMessage( "Your equipment or skills are not commensurate to that of a true monk." );
				return false;
			}
			else if ( this is WindRunner && Server.Misc.MyServerSettings.NoMountsInCertainRegions() && Server.Mobiles.AnimalTrainer.IsNoMountRegion( Region.Find( Caster.Location, Caster.Map ) ) )
			{
				Caster.SendMessage( "This ability doesn't seem to work in this place." );
				return false;
			}
			else if ( Caster.Mana < mana )
			{
				Caster.SendLocalizedMessage( 1060174, mana.ToString() ); // You must have at least ~1_MANA_REQUIREMENT~ Mana to use this ability.
				return false;
			}

			Caster.TithingPoints -= requiredTithing;

			if ( !base.CheckFizzle() )
				return false;

			Caster.Mana -= mana;

			return true;
		}

		public override void DoFizzle()
		{
			Caster.PlaySound( 0x1D6 );
			Caster.NextSpellTime = Core.TickCount;
		}

		public override void DoHurtFizzle()
		{
			Caster.PlaySound( 0x1D6 );
		}

		public override double GetResistSkill( Mobile m )
		{
			int maxSkill = (1 + (int)MysticSpellCircle) * 10;
			maxSkill += (1 + ((int)MysticSpellCircle / 6)) * 25;

			if( m.Skills[SkillName.MagicResist].Value < maxSkill )
				m.CheckSkill( SkillName.MagicResist, 0.0, 120.0 );

			return m.Skills[SkillName.MagicResist].Value;
		}

		public virtual bool CheckResisted( Mobile target )
		{
			double n = GetResistPercent( target );

			n /= 100.0;

			if( n <= 0.0 )
				return false;

			if( n >= 1.0 )
				return true;

			int maxSkill = (1 + (int)MysticSpellCircle) * 10;
			maxSkill += (1 + ((int)MysticSpellCircle / 6)) * 25;

			if( target.Skills[SkillName.MagicResist].Value < maxSkill )
				target.CheckSkill( SkillName.MagicResist, 0.0, 120.0 );

			return (n >= Utility.RandomDouble());
		}

		public virtual double GetResistPercentForCircle( Mobile target )
		{
			double firstPercent = target.Skills[SkillName.MagicResist].Value / 5.0;
			double secondPercent = target.Skills[SkillName.MagicResist].Value - (((Caster.Skills[CastSkill].Value - 20.0) / 5.0) + (1 + (int)MysticSpellCircle) * 5.0);

			return (firstPercent > secondPercent ? firstPercent : secondPercent) / 2.0; // Seems should be about half of what stratics says.
		}

		public virtual double GetResistPercent( Mobile target )
		{
			return GetResistPercentForCircle( target );
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

			SendCastEffect();
		}

		public virtual void SendCastEffect()
		{
			Caster.FixedEffect( 0x37C4, 10, (int)( GetCastDelay().TotalSeconds * 28 ), 4, 3 );
		}

		public override void GetCastSkills( out double min, out double max )
		{
			min = RequiredSkill;
			max = RequiredSkill + 50.0;
		}

		public override int GetMana()
		{
			return 0;
		}
	}
}
