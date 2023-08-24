using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;
using Server.Mobiles;

namespace Server.Misc
{
	public class RegenRates
	{
		[CallPriority( 10 )]
		public static void Configure()
		{
			Mobile.DefaultHitsRate = TimeSpan.FromSeconds( 11.0 );
			Mobile.DefaultStamRate = TimeSpan.FromSeconds(  7.0 );
			Mobile.DefaultManaRate = TimeSpan.FromSeconds(  7.0 );

			Mobile.ManaRegenRateHandler = new RegenRateHandler( Mobile_ManaRegenRate );
			Mobile.StamRegenRateHandler = new RegenRateHandler( Mobile_StamRegenRate );
			Mobile.HitsRegenRateHandler = new RegenRateHandler( Mobile_HitsRegenRate );
		}

		private static void CheckBonusSkill( Mobile m, int cur, int max, SkillName skill )
		{
			if ( !m.Alive )
				return;

			double n = (double)cur / max;
			double v = Math.Sqrt( m.Skills[skill].Value * 0.005 );

			n *= (1.0 - v);
			n += v;

			m.CheckSkill( skill, n );
		}

		private static bool CheckTransform( Mobile m, Type type )
		{
			return TransformationSpellHelper.UnderTransformation( m, type );
		}

		private static bool CheckAnimal( Mobile m, Type type )
		{
			return AnimalForm.UnderTransformation( m, type );
		}

		private static TimeSpan Mobile_HitsRegenRate( Mobile from )
		{
			int points = AosAttributes.GetValue( from, AosAttribute.RegenHits );
			if ((AdventuresFunctions.IsPuritain((object)from)))
				points = 0;

			if (from is PlayerMobile && points >= MyServerSettings.RegenHitsCap() && from.BodyMod != 263)
				points = MyServerSettings.RegenHitsCap();
			
			if ( from is BaseCreature && !((BaseCreature)from).IsAnimatedDead )
				points += 4;

			if ( (from is BaseCreature && ((BaseCreature)from).IsParagon && !((BaseCreature)from).Controlled ) || from is Leviathan )
				points += 40;

			if ( from is BaseUndead )
				points += 10;
			
			if ( from is BaseChampion )
				points += 20;

			if( Core.ML && from.Race == Race.Human )	//Is this affected by the cap?
				points += 2;

			if ( points < 0 )
				points = 0;

			if( Core.ML && from is PlayerMobile )	//does racial bonus go before/after?
				points = Math.Min( points, 18 );

			if ( CheckTransform( from, typeof( HorrificBeastSpell ) ) )
				points += 15;

			if ( CheckAnimal( from, typeof( Dog ) ) || CheckAnimal( from, typeof( Cat ) ) )
				points += from.Skills[SkillName.Ninjitsu].Fixed / 15;

			if ( CheckAnimal( from, typeof( Kirin ) ) )
				points += 10;
			
			if (from is PlayerMobile)
			{	
				double pts = (double)points;

				if (from.Hunger <= 3)
					pts /= 2;
				else if (from.Hunger <= 10)
					pts /= 1.50;	

				
				if (from.Thirst <= 3)
					pts /= 2;
				else if (from.Thirst <= 10)
					pts /= 1.5;
				
				if (from.Thirst > 20) // thirst bonus!
					pts *= 1 + (0.25 * ( (from.Thirst- 20) / 20 ) ); // max is 40, so at 40 you get 25% bonus!
				if (from.BAC > 0) // drunk bonus!
					pts *= 1 + (0.50 * (from.BAC / 60));

				points = (int)(pts);
			}

			if (from is FrankenFighter && ((BaseCreature)from).ControlMaster is PlayerMobile )
			{
				Mobile master = ((BaseCreature)from).ControlMaster;
				if (((PlayerMobile)master).Alchemist()) //regen of mob is player's regen if greater
				{
					int pt = AosAttributes.GetValue( master, AosAttribute.RegenHits );
					if (pt > points)
						points = pt;
				}
			}

			if (OneRing.wearer == from && from.FindItemOnLayer( Layer.Ring ) != null && from.FindItemOnLayer( Layer.Ring ) is OneRing)
				points = 0;

			return TimeSpan.FromSeconds( 1.0 / (0.1 * (1 + points)) );
		}

		private static TimeSpan Mobile_StamRegenRate( Mobile from )
		{
			if ( from.Skills == null )
				return Mobile.DefaultStamRate;

			CheckBonusSkill( from, from.Stam, from.StamMax, SkillName.Focus );

			int points =(int)(from.Skills[SkillName.Focus].Value * 0.1);

			if( (from is BaseCreature && ((BaseCreature)from).IsParagon && !((BaseCreature)from).Controlled) || from is Leviathan )
				points += 40;

			if ( from is BaseUndead )
				points += 10;
			
			if ( from is BaseChampion )
				points += 20;

			int cappedPoints = AosAttributes.GetValue( from, AosAttribute.RegenStam );

						if ((AdventuresFunctions.IsPuritain((object)from)))
				cappedPoints = 0;

			if (from is PlayerMobile && cappedPoints >= MyServerSettings.RegenStamCap() && from.BodyMod != 263)
				cappedPoints = MyServerSettings.RegenStamCap();

			if ( CheckTransform( from, typeof( VampiricEmbraceSpell ) ) )
				cappedPoints += 15;

			if ( CheckAnimal( from, typeof( Kirin ) ) )
				cappedPoints += 20;

			if( Core.ML && from is PlayerMobile )
				cappedPoints = Math.Min( cappedPoints, 24 );

			if (AdventuresFunctions.IsPuritain((object)from) && from is PlayerMobile)
				cappedPoints = (int)( (double)cappedPoints * (1.3 * ((PlayerMobile)from).Agility()) );

			points += cappedPoints;

			if ( points < -1 )
				points = -1;
			
			if (from is PlayerMobile)
			{	
				double pts = (double)points;

				if (from.Hunger <= 3)
					pts /= 2;
				else if (from.Hunger <= 10)
					pts /= 1.5;	

				if (from.Thirst <= 3)
					pts /= 2;
				else if (from.Thirst <= 10)
					pts /= 1.5;
				
				if (from.Thirst > 20) // thirst bonus!
					pts *= 1 + (0.25 * ( (from.Thirst- 20) / 20 ) ); // max is 40, so at 40 you get 25% bonus!
				if (from.BAC > 0) // drunk bonus!
					pts *= 1 + (0.50 * (from.BAC / 60));

				points = (int)(pts);
			}

			if (OneRing.wearer == from && from.FindItemOnLayer( Layer.Ring ) != null && from.FindItemOnLayer( Layer.Ring ) is OneRing)
				points = 0;

			return TimeSpan.FromSeconds( 1.0 / (0.1 * (2 + points)) );
		}

		private static TimeSpan Mobile_ManaRegenRate( Mobile from )
		{
			if ( from.Skills == null )
				return Mobile.DefaultManaRate;

			if ( !from.Meditating )
				CheckBonusSkill( from, from.Mana, from.ManaMax, SkillName.Meditation );

			double rate;
			double armorPenalty = GetArmorOffset( from );

			if ( Core.AOS )
			{
				double medPoints = from.Int + (from.Skills[SkillName.Meditation].Value * 3);

				medPoints *= ( from.Skills[SkillName.Meditation].Value < 100.0 ) ? 0.025 : 0.0275;

				CheckBonusSkill( from, from.Mana, from.ManaMax, SkillName.Focus );

				double focusPoints = (from.Skills[SkillName.Focus].Value * 0.05);

				if ( armorPenalty > 0 )
					medPoints = 0; // In AOS, wearing any meditation-blocking armor completely removes meditation bonus

				double totalPoints = 0;

				if (from is PlayerMobile && ((PlayerMobile)from).Sorcerer())
					totalPoints = focusPoints + (medPoints*1.5) + (from.Meditating ? (medPoints > 17.0 ? 17.0 : medPoints) : 0.0);
				else
					totalPoints = focusPoints + medPoints + (from.Meditating ? (medPoints > 13.0 ? 13.0 : medPoints) : 0.0);

				if( (from is BaseCreature && ((BaseCreature)from).IsParagon && !((BaseCreature)from).Controlled) || from is Leviathan )
					totalPoints += 40;

				if ( from is BaseUndead )
					totalPoints += 10;

				if (from is PlayerMobile && ((PlayerMobile)from).Sorcerer())
					totalPoints += (2 * ( ( (from.Skills[SkillName.Meditation].Value/125) + (from.Int / 500) / 2 )));
				
				if ( from is BaseChampion )
					totalPoints += 20;

				int cappedPoints = AosAttributes.GetValue( from, AosAttribute.RegenMana );

				if ((AdventuresFunctions.IsPuritain((object)from)))
					cappedPoints = 0;

				if (from is PlayerMobile && cappedPoints >= MyServerSettings.RegenManaCap() && from.BodyMod != 263)
					cappedPoints = MyServerSettings.RegenManaCap();

				if ( CheckTransform( from, typeof( VampiricEmbraceSpell ) ) )
					cappedPoints += 5;
				else if ( CheckTransform( from, typeof( LichFormSpell ) ) )
					cappedPoints += 18;

				if ( CheckAnimal( from, typeof( Kirin ) ) )
				cappedPoints += 10;

				if( Core.ML && from is PlayerMobile )
					cappedPoints = Math.Min( cappedPoints, 18 );

				if (AdventuresFunctions.IsPuritain((object)from) && from is PlayerMobile)
					cappedPoints =(int)((double)cappedPoints * (1.3 * ((PlayerMobile)from).Lucidity()));

				totalPoints += cappedPoints;

				if ( totalPoints < -1 )
					totalPoints = -1;

				if ( Core.ML )
					totalPoints = Math.Floor( totalPoints );

				if (from is PlayerMobile)
				{	
					double pts = (double)totalPoints;

					if (from.Hunger <= 3)
						pts /= 2;
					else if (from.Hunger <= 10)
						pts /= 1.5;	

					if (from.Thirst <= 3)
						pts /= 2;
					else if (from.Thirst <= 10)
						pts /= 1.5;

					if (from.Thirst > 20) // thirst bonus!
						pts *= 1 + (0.25 * ( (from.Thirst- 20) / 20 ) ); // max is 40, so at 40 you get 25% bonus!
					if (from.BAC > 0) // drunk bonus!
						pts *= 1 + (0.50 * (from.BAC / 60));

					totalPoints = (int)(pts);
				}

				rate = 1.0 / (0.1 * (2 + totalPoints));
			}
			else
			{
				double medPoints = (from.Int + from.Skills[SkillName.Meditation].Value) * 0.5;

				if ( medPoints <= 0 )
					rate = 7.0;
				else if ( medPoints <= 100 )
					rate = 7.0 - (239*medPoints/2400) + (19*medPoints*medPoints/48000);
				else if ( medPoints < 120 )
					rate = 1.0;
				else
					rate = 0.75;

				rate += armorPenalty;

				if ( from.Meditating )
					rate *= 0.5;

				if ( rate < 0.5 )
					rate = 0.5;
				else if ( rate > 7.0 )
					rate = 7.0;
			}

			if (OneRing.wearer == from && from.FindItemOnLayer( Layer.Ring ) != null && from.FindItemOnLayer( Layer.Ring ) is OneRing)
				rate = 5;

			return TimeSpan.FromSeconds( rate );
		}

		public static double GetArmorOffset( Mobile from )
		{
			double rating = 0.0;

			if ( !Core.AOS )
				rating += GetArmorMeditationValue( from.ShieldArmor as BaseArmor );

			rating += GetArmorMeditationValue( from.NeckArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.HandArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.HeadArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.ArmsArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.LegsArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.ChestArmor as BaseArmor );

			if( from.FindItemOnLayer( Layer.Shoes ) is BaseArmor )
				rating += GetArmorMeditationValue( (BaseArmor)(from.FindItemOnLayer( Layer.Shoes )));
			if( from.FindItemOnLayer( Layer.Cloak ) is BaseArmor )
				rating += GetArmorMeditationValue( (BaseArmor)(from.FindItemOnLayer( Layer.Cloak )));
			if( from.FindItemOnLayer( Layer.OuterTorso ) is BaseArmor )
				rating += GetArmorMeditationValue( (BaseArmor)(from.FindItemOnLayer( Layer.OuterTorso )));

			return rating / 4;
		}

		private static double GetArmorMeditationValue( BaseArmor ar )
		{
			if ( ar == null || ar.ArmorAttributes.MageArmor != 0 || ar.Attributes.SpellChanneling != 0 )
				return 0.0;

			switch ( ar.MeditationAllowance )
			{
				default:
				case ArmorMeditationAllowance.None: return ar.BaseArmorRatingScaled;
				case ArmorMeditationAllowance.Half: return ar.BaseArmorRatingScaled / 2.0;
				case ArmorMeditationAllowance.All:  return 0.0;
			}
		}
	}
}
