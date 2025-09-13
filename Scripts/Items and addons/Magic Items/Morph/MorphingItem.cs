using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using System.Text;

namespace Server.Misc
{
    class MorphingItem
    {
		public static void MorphMyItem( Item i, string etch, string prefix, string names, string imbue )
		{
			if ( i is BaseWeapon )
			{
				BaseWeapon x = (BaseWeapon)i;

				string name = i.Name;

				if ( names != "IGNORED" )
				{
					i.Name = names;
				}
				else if ( prefix != "IGNORED" )
				{
					if ( name == null ){ name = AddSpacesToSentence( (i.GetType()).Name ); }
					i.Name = prefix + " " + name;
				}

				if ( etch != "IGNORED" ){ x.EngravedText = etch; }

				char[] delimiterChars = { ',' };
				string[] morph = imbue.Split(delimiterChars);

				int force = 0;
				int cycle = 0;

				double skill1 = 0.0;
				double skill2 = 0.0;
				double skill3 = 0.0;
				double skill4 = 0.0;
				double skill5 = 0.0;

				foreach (string s in morph)
				{
					cycle++;
					force = Int32.Parse( s );

					if ( force > 0 && cycle == 1 ){ x.WeaponAttributes.LowerStatReq = force; }
					if ( force > 0 && cycle == 2 ){ x.WeaponAttributes.SelfRepair = force; }
					if ( force > 0 && cycle == 3 ){ x.WeaponAttributes.HitLeechHits = force; }
					if ( force > 0 && cycle == 4 ){ x.WeaponAttributes.HitLeechStam = force; }
					if ( force > 0 && cycle == 5 ){ x.WeaponAttributes.HitLeechMana = force; }
					if ( force > 0 && cycle == 6 ){ x.WeaponAttributes.HitLowerAttack = force; }
					if ( force > 0 && cycle == 7 ){ x.WeaponAttributes.HitLowerDefend = force; }
					if ( force > 0 && cycle == 8 ){ x.WeaponAttributes.HitMagicArrow = force; }
					if ( force > 0 && cycle == 9 ){ x.WeaponAttributes.HitHarm = force; }
					if ( force > 0 && cycle == 10 ){ x.WeaponAttributes.HitFireball = force; }
					if ( force > 0 && cycle == 11 ){ x.WeaponAttributes.HitLightning = force; }
					if ( force > 0 && cycle == 12 ){ x.WeaponAttributes.HitDispel = force; }
					if ( force > 0 && cycle == 13 ){ x.WeaponAttributes.HitColdArea = force; }
					if ( force > 0 && cycle == 14 ){ x.WeaponAttributes.HitFireArea = force; }
					if ( force > 0 && cycle == 15 ){ x.WeaponAttributes.HitPoisonArea = force; }
					if ( force > 0 && cycle == 16 ){ x.WeaponAttributes.HitEnergyArea = force; }
					if ( force > 0 && cycle == 17 ){ x.WeaponAttributes.HitPhysicalArea = force; }
					if ( force > 0 && cycle == 18 ){ x.WeaponAttributes.ResistPhysicalBonus = force; }
					if ( force > 0 && cycle == 19 ){ x.WeaponAttributes.ResistFireBonus = force; }
					if ( force > 0 && cycle == 20 ){ x.WeaponAttributes.ResistColdBonus = force; }
					if ( force > 0 && cycle == 21 ){ x.WeaponAttributes.ResistPoisonBonus = force; }
					if ( force > 0 && cycle == 22 ){ x.WeaponAttributes.ResistEnergyBonus = force; }
					if ( force > 0 && cycle == 23 ){ x.WeaponAttributes.UseBestSkill = 1; }
					if ( force > 0 && cycle == 24 ){ x.WeaponAttributes.DurabilityBonus = force; }
					if ( force > 0 && cycle == 25 ){ i.Hue = force; }
					if ( force > 0 && cycle == 26 )
					{
						if ( force == 1 ){ x.AccuracyLevel = WeaponAccuracyLevel.Regular; }
						else if ( force == 2 ){ x.AccuracyLevel = WeaponAccuracyLevel.Accurate; }
						else if ( force == 3 ){ x.AccuracyLevel = WeaponAccuracyLevel.Surpassingly; }
						else if ( force == 4 ){ x.AccuracyLevel = WeaponAccuracyLevel.Eminently; }
						else if ( force == 5 ){ x.AccuracyLevel = WeaponAccuracyLevel.Exceedingly; }
						else if ( force == 6 ){ x.AccuracyLevel = WeaponAccuracyLevel.Supremely; }
					}
					if ( force > 0 && cycle == 27 )
					{
						if ( force == 1 ){ x.DamageLevel = WeaponDamageLevel.Regular; }
						else if ( force == 2 ){ x.DamageLevel = WeaponDamageLevel.Ruin; }
						else if ( force == 3 ){ x.DamageLevel = WeaponDamageLevel.Might; }
						else if ( force == 4 ){ x.DamageLevel = WeaponDamageLevel.Force; }
						else if ( force == 5 ){ x.DamageLevel = WeaponDamageLevel.Power; }
						else if ( force == 6 ){ x.DamageLevel = WeaponDamageLevel.Vanq; }
					}
					if ( force > 0 && cycle == 28 ){ x.Quality = WeaponQuality.Exceptional; }
					if ( force > 0 && cycle == 29 )
					{
						if ( force == 1 ){ x.DurabilityLevel = WeaponDurabilityLevel.Regular; }
						else if ( force == 2 ){ x.DurabilityLevel = WeaponDurabilityLevel.Durable; }
						else if ( force == 3 ){ x.DurabilityLevel = WeaponDurabilityLevel.Substantial; }
						else if ( force == 4 ){ x.DurabilityLevel = WeaponDurabilityLevel.Massive; }
						else if ( force == 5 ){ x.DurabilityLevel = WeaponDurabilityLevel.Fortified; }
						else if ( force == 6 ){ x.DurabilityLevel = WeaponDurabilityLevel.Indestructible; }
					}
					if ( force > 0 && cycle == 30 ){ x.AosElementDamages.Physical = force; } // 30-34 SHOULD TOTAL 100
					if ( force > 0 && cycle == 31 ){ x.AosElementDamages.Fire = force; }
					if ( force > 0 && cycle == 32 ){ x.AosElementDamages.Cold = force; }
					if ( force > 0 && cycle == 33 ){ x.AosElementDamages.Poison = force; }
					if ( force > 0 && cycle == 34 ){ x.AosElementDamages.Energy = force; }
					if ( force > 0 && cycle == 35 ){ x.HitPoints = x.HitPoints + force; x.MaxHitPoints = x.MaxHitPoints + force; }
					if ( force > 0 && cycle == 36 ){ x.MinDamage = x.MinDamage + force; x.MaxDamage = x.MaxDamage + force; }
					if ( force > 0 && cycle == 37 ){ x.Attributes.RegenHits = force; }
					if ( force > 0 && cycle == 38 ){ x.Attributes.RegenStam = force; }
					if ( force > 0 && cycle == 39 ){ x.Attributes.RegenMana = force; }
					if ( force > 0 && cycle == 40 ){ x.Attributes.DefendChance = force; }
					if ( force > 0 && cycle == 41 ){ x.Attributes.AttackChance = force; }
					if ( force > 0 && cycle == 42 ){ x.Attributes.BonusStr = force; }
					if ( force > 0 && cycle == 43 ){ x.Attributes.BonusDex = force; }
					if ( force > 0 && cycle == 44 ){ x.Attributes.BonusInt = force; }
					if ( force > 0 && cycle == 45 ){ x.Attributes.BonusHits = force; }
					if ( force > 0 && cycle == 46 ){ x.Attributes.BonusStam = force; }
					if ( force > 0 && cycle == 47 ){ x.Attributes.BonusMana = force; }
					if ( force > 0 && cycle == 48 ){ x.Attributes.WeaponDamage = force; }
					if ( force > 0 && cycle == 49 ){ x.Attributes.WeaponSpeed = force; }
					if ( force > 0 && cycle == 50 ){ x.Attributes.SpellDamage = force; }
					if ( force > 0 && cycle == 51 ){ x.Attributes.CastRecovery = force; }
					if ( force > 0 && cycle == 52 ){ x.Attributes.CastSpeed = force; }
					if ( force > 0 && cycle == 53 ){ x.Attributes.LowerManaCost = force; }
					if ( force > 0 && cycle == 54 ){ x.Attributes.LowerRegCost = force; }
					if ( force > 0 && cycle == 55 ){ x.Attributes.ReflectPhysical = force; }
					if ( force > 0 && cycle == 56 ){ x.Attributes.EnhancePotions = force; }
					if ( force > 0 && cycle == 57 ){ x.Attributes.Luck = force; }
					if ( force > 0 && cycle == 58 ){ x.Attributes.SpellChanneling = 1; }
					if ( force > 0 && cycle == 59 ){ x.Attributes.NightSight = 1; }
					if ( force > 0 && cycle == 60 ){ x.Slayer = GetMorphSlayer( force ); }
					if ( force > 0 && cycle == 61 ){ x.Slayer2 = GetMorphSlayer( force ); }
					if ( force > 0 && cycle == 62 ){ skill1 = (double)force; }
					if ( force > 0 && cycle == 63 )
					{	// GIVE THE WEAPON A BONUS FOR THE WEAPON SKILL IF SET TO 99
						if ( force == 99 ){ x.SkillBonuses.SetValues(0, x.Skill, skill1); }
						else { x.SkillBonuses.SetValues(0, GetMorphSkill( force ), skill1); }
					}
					if ( force > 0 && cycle == 64 ){ skill2 = (double)force; }
					if ( force > 0 && cycle == 65 ){ x.SkillBonuses.SetValues(1, GetMorphSkill( force ), skill2); }
					if ( force > 0 && cycle == 66 ){ skill3 = (double)force; }
					if ( force > 0 && cycle == 67 ){ x.SkillBonuses.SetValues(2, GetMorphSkill( force ), skill3); }
					if ( force > 0 && cycle == 68 ){ skill4 = (double)force; }
					if ( force > 0 && cycle == 69 ){ x.SkillBonuses.SetValues(3, GetMorphSkill( force ), skill4); }
					if ( force > 0 && cycle == 70 ){ skill5 = (double)force; }
					if ( force > 0 && cycle == 71 ){ x.SkillBonuses.SetValues(4, GetMorphSkill( force ), skill5); }
				}
			}

			else if ( i is BaseArmor )
			{
				BaseArmor x = (BaseArmor)i;

				string name = i.Name;

				if ( names != "IGNORED" )
				{
					i.Name = names;
				}
				else if ( prefix != "IGNORED" )
				{
					if ( name == null ){ name = AddSpacesToSentence( (i.GetType()).Name ); }
					i.Name = prefix + " " + name;
				}

				char[] delimiterChars = { ',' };
				string[] morph = imbue.Split(delimiterChars);

				int force = 0;
				int cycle = 0;

				double skill1 = 0.0;
				double skill2 = 0.0;
				double skill3 = 0.0;
				double skill4 = 0.0;
				double skill5 = 0.0;

				foreach (string s in morph)
				{
					cycle++;
					force = Int32.Parse( s );

					if ( force > 0 && cycle == 1 ){ x.ArmorAttributes.LowerStatReq = force; }
					if ( force > 0 && cycle == 2 ){ x.ArmorAttributes.SelfRepair = force; }
					if ( force > 0 && cycle == 3 ){ x.ArmorAttributes.MageArmor = 1; }
					if ( force > 0 && cycle == 4 ){ x.ArmorAttributes.DurabilityBonus = force; }
					if ( force > 0 && cycle == 5 ){ x.PhysicalBonus = force; }
					if ( force > 0 && cycle == 6 ){ x.ColdBonus = force; }
					if ( force > 0 && cycle == 7 ){ x.EnergyBonus = force; }
					if ( force > 0 && cycle == 8 ){ x.FireBonus = force; }
					if ( force > 0 && cycle == 9 ){ x.PoisonBonus = force; }
					if ( force > 0 && cycle == 10 ){ x.HitPoints = x.HitPoints + force; x.MaxHitPoints = x.MaxHitPoints + force; }
					if ( force > 0 && cycle == 11 )
					{
						if ( force == 1 ){ x.Durability = ArmorDurabilityLevel.Regular; }
						else if ( force == 2 ){ x.Durability = ArmorDurabilityLevel.Durable; }
						else if ( force == 3 ){ x.Durability = ArmorDurabilityLevel.Substantial; }
						else if ( force == 4 ){ x.Durability = ArmorDurabilityLevel.Massive; }
						else if ( force == 5 ){ x.Durability = ArmorDurabilityLevel.Fortified; }
						else if ( force == 6 ){ x.Durability = ArmorDurabilityLevel.Indestructible; }
					}
					if ( force > 0 && cycle == 12 ){ x.Quality = ArmorQuality.Exceptional; }
					if ( force > 0 && cycle == 13 )
					{
						if ( force == 1 ){ x.ProtectionLevel = ArmorProtectionLevel.Regular; }
						else if ( force == 2 ){ x.ProtectionLevel = ArmorProtectionLevel.Defense; }
						else if ( force == 3 ){ x.ProtectionLevel = ArmorProtectionLevel.Guarding; }
						else if ( force == 4 ){ x.ProtectionLevel = ArmorProtectionLevel.Hardening; }
						else if ( force == 5 ){ x.ProtectionLevel = ArmorProtectionLevel.Fortification; }
						else if ( force == 6 ){ x.ProtectionLevel = ArmorProtectionLevel.Invulnerability; }
					}
					if ( force > 0 && cycle == 14 ){ x.Attributes.RegenHits = force; }
					if ( force > 0 && cycle == 15 ){ x.Attributes.RegenStam = force; }
					if ( force > 0 && cycle == 16 ){ x.Attributes.RegenMana = force; }
					if ( force > 0 && cycle == 17 ){ x.Attributes.DefendChance = force; }
					if ( force > 0 && cycle == 18 ){ x.Attributes.AttackChance = force; }
					if ( force > 0 && cycle == 19 ){ x.Attributes.BonusStr = force; }
					if ( force > 0 && cycle == 20 ){ x.Attributes.BonusDex = force; }
					if ( force > 0 && cycle == 21 ){ x.Attributes.BonusInt = force; }
					if ( force > 0 && cycle == 22 ){ x.Attributes.BonusHits = force; }
					if ( force > 0 && cycle == 23 ){ x.Attributes.BonusStam = force; }
					if ( force > 0 && cycle == 24 ){ x.Attributes.BonusMana = force; }
					if ( force > 0 && cycle == 25 ){ x.Attributes.WeaponDamage = force; }
					if ( force > 0 && cycle == 26 ){ x.Attributes.WeaponSpeed = force; }
					if ( force > 0 && cycle == 27 ){ x.Attributes.SpellDamage = force; }
					if ( force > 0 && cycle == 28 ){ x.Attributes.CastRecovery = force; }
					if ( force > 0 && cycle == 29 ){ x.Attributes.CastSpeed = force; }
					if ( force > 0 && cycle == 30 ){ x.Attributes.LowerManaCost = force; }
					if ( force > 0 && cycle == 31 ){ x.Attributes.LowerRegCost = force; }
					if ( force > 0 && cycle == 32 ){ x.Attributes.ReflectPhysical = force; }
					if ( force > 0 && cycle == 33 ){ x.Attributes.EnhancePotions = force; }
					if ( force > 0 && cycle == 34 ){ x.Attributes.Luck = force; }
					if ( force > 0 && cycle == 35 ){ x.Attributes.SpellChanneling = 1; }
					if ( force > 0 && cycle == 36 ){ x.Attributes.NightSight = 1; }
					if ( force > 0 && cycle == 37 ){ skill1 = (double)force; }
					if ( force > 0 && cycle == 38 ){ x.SkillBonuses.SetValues(0, GetMorphSkill( force ), skill1); }
					if ( force > 0 && cycle == 39 ){ skill2 = (double)force; }
					if ( force > 0 && cycle == 40 ){ x.SkillBonuses.SetValues(1, GetMorphSkill( force ), skill2); }
					if ( force > 0 && cycle == 41 ){ skill3 = (double)force; }
					if ( force > 0 && cycle == 42 ){ x.SkillBonuses.SetValues(2, GetMorphSkill( force ), skill3); }
					if ( force > 0 && cycle == 43 ){ skill4 = (double)force; }
					if ( force > 0 && cycle == 44 ){ x.SkillBonuses.SetValues(3, GetMorphSkill( force ), skill4); }
					if ( force > 0 && cycle == 45 ){ skill5 = (double)force; }
					if ( force > 0 && cycle == 46 ){ x.SkillBonuses.SetValues(4, GetMorphSkill( force ), skill5); }
					if ( force > 0 && cycle == 47 ){ i.Hue = force; }
				}
			}

			else if ( i is BaseJewel )
			{
				BaseJewel x = (BaseJewel)i;

				string name = i.Name;

				if ( names != "IGNORED" )
				{
					i.Name = names;
				}
				else if ( prefix != "IGNORED" )
				{
					if ( name == null ){ name = AddSpacesToSentence( (i.GetType()).Name ); }
					i.Name = prefix + " " + name;
				}

				char[] delimiterChars = { ',' };
				string[] morph = imbue.Split(delimiterChars);

				int force = 0;
				int cycle = 0;

				double skill1 = 0.0;
				double skill2 = 0.0;
				double skill3 = 0.0;
				double skill4 = 0.0;
				double skill5 = 0.0;

				foreach (string s in morph)
				{
					cycle++;
					force = Int32.Parse( s );

					if ( force > 0 && cycle == 1 ){ x.Resistances.Physical = force; }
					if ( force > 0 && cycle == 2 ){ x.Resistances.Fire = force; }
					if ( force > 0 && cycle == 3 ){ x.Resistances.Cold = force; }
					if ( force > 0 && cycle == 4 ){ x.Resistances.Poison = force; }
					if ( force > 0 && cycle == 5 ){ x.Resistances.Energy = force; }
					if ( force > 0 && cycle == 6 ){ x.Attributes.DefendChance = force; }
					if ( force > 0 && cycle == 7 ){ x.Attributes.AttackChance = force; }
					if ( force > 0 && cycle == 8 ){ x.Attributes.BonusStr = force; }
					if ( force > 0 && cycle == 9 ){ x.Attributes.BonusDex = force; }
					if ( force > 0 && cycle == 10 ){ x.Attributes.BonusInt = force; }
					if ( force > 0 && cycle == 11 ){ x.Attributes.BonusHits = force; }
					if ( force > 0 && cycle == 12 ){ x.Attributes.BonusStam = force; }
					if ( force > 0 && cycle == 13 ){ x.Attributes.BonusMana = force; }
					if ( force > 0 && cycle == 14 ){ x.Attributes.CastRecovery = force; }
					if ( force > 0 && cycle == 15 ){ x.Attributes.CastSpeed = force; }
					if ( force > 0 && cycle == 16 ){ x.Attributes.EnhancePotions = force; }
					if ( force > 0 && cycle == 17 ){ x.Attributes.LowerManaCost = force; }
					if ( force > 0 && cycle == 18 ){ x.Attributes.LowerRegCost = force; }
					if ( force > 0 && cycle == 19 ){ x.Attributes.Luck = force; }
					if ( force > 0 && cycle == 20 ){ x.Attributes.NightSight = 1; }
					if ( force > 0 && cycle == 21 ){ x.Attributes.ReflectPhysical = force; }
					if ( force > 0 && cycle == 22 ){ x.Attributes.RegenHits = force; }
					if ( force > 0 && cycle == 23 ){ x.Attributes.RegenStam = force; }
					if ( force > 0 && cycle == 24 ){ x.Attributes.RegenMana = force; }
					if ( force > 0 && cycle == 25 ){ x.Attributes.SpellDamage = force; }
					if ( force > 0 && cycle == 26 ){ x.Attributes.WeaponDamage = force; }
					if ( force > 0 && cycle == 27 ){ x.Attributes.WeaponSpeed = force; }
					if ( force > 0 && cycle == 28 ){ skill1 = (double)force; }
					if ( force > 0 && cycle == 29 ){ x.SkillBonuses.SetValues(0, GetMorphSkill( force ), skill1); }
					if ( force > 0 && cycle == 30 ){ skill2 = (double)force; }
					if ( force > 0 && cycle == 31 ){ x.SkillBonuses.SetValues(1, GetMorphSkill( force ), skill2); }
					if ( force > 0 && cycle == 32 ){ skill3 = (double)force; }
					if ( force > 0 && cycle == 33 ){ x.SkillBonuses.SetValues(2, GetMorphSkill( force ), skill3); }
					if ( force > 0 && cycle == 34 ){ skill4 = (double)force; }
					if ( force > 0 && cycle == 35 ){ x.SkillBonuses.SetValues(3, GetMorphSkill( force ), skill4); }
					if ( force > 0 && cycle == 36 ){ skill5 = (double)force; }
					if ( force > 0 && cycle == 37 ){ x.SkillBonuses.SetValues(4, GetMorphSkill( force ), skill5); }
					if ( force > 0 && cycle == 38 ){ i.Hue = force; }
				}
			}

			return;
		}

		public static SlayerName GetMorphSlayer( int force )
		{
			SlayerName slayer = SlayerName.None;
			if ( force == 1 ){ slayer = SlayerName.Silver; }
			else if ( force == 2 ){ slayer = SlayerName.OrcSlaying; }
			else if ( force == 3 ){ slayer = SlayerName.TrollSlaughter; }
			else if ( force == 4 ){ slayer = SlayerName.OgreTrashing; }
			else if ( force == 5 ){ slayer = SlayerName.Repond; }
			else if ( force == 6 ){ slayer = SlayerName.DragonSlaying; }
			else if ( force == 7 ){ slayer = SlayerName.Terathan; }
			else if ( force == 8 ){ slayer = SlayerName.SnakesBane; }
			else if ( force == 9 ){ slayer = SlayerName.LizardmanSlaughter; }
			else if ( force == 10 ){ slayer = SlayerName.ReptilianDeath; }
			else if ( force == 11 ){ slayer = SlayerName.DaemonDismissal; }
			else if ( force == 12 ){ slayer = SlayerName.GargoylesFoe; }
			else if ( force == 13 ){ slayer = SlayerName.BalronDamnation; }
			else if ( force == 14 ){ slayer = SlayerName.Exorcism; }
			else if ( force == 15 ){ slayer = SlayerName.Ophidian; }
			else if ( force == 16 ){ slayer = SlayerName.SpidersDeath; }
			else if ( force == 17 ){ slayer = SlayerName.ScorpionsBane; }
			else if ( force == 18 ){ slayer = SlayerName.ArachnidDoom; }
			else if ( force == 19 ){ slayer = SlayerName.FlameDousing; }
			else if ( force == 20 ){ slayer = SlayerName.WaterDissipation; }
			else if ( force == 21 ){ slayer = SlayerName.Vacuum; }
			else if ( force == 22 ){ slayer = SlayerName.ElementalHealth; }
			else if ( force == 23 ){ slayer = SlayerName.EarthShatter; }
			else if ( force == 24 ){ slayer = SlayerName.BloodDrinking; }
			else if ( force == 25 ){ slayer = SlayerName.SummerWind; }
			else if ( force == 26 ){ slayer = SlayerName.ElementalBan; }
			else if ( force == 27 ){ slayer = SlayerName.WizardSlayer; }
			else if ( force == 28 ){ slayer = SlayerName.AvianHunter; }
			else if ( force == 29 ){ slayer = SlayerName.SlimyScourge; }
			else if ( force == 30 ){ slayer = SlayerName.AnimalHunter; }
			else if ( force == 31 ){ slayer = SlayerName.GiantKiller; }
			else if ( force == 32 ){ slayer = SlayerName.GolemDestruction; }
			else if ( force == 33 ){ slayer = SlayerName.WeedRuin; }
			else if ( force == 34 ){ slayer = SlayerName.NeptunesBane; }
			else if ( force == 35 ){ slayer = SlayerName.Fey; }

			return slayer;
		}

		public static SkillName GetMorphSkill( int force )
		{
			SkillName skill = SkillName.Alchemy;

			if ( force == 1 ){ skill = SkillName.Alchemy; }
			else if ( force == 2 ){ skill = SkillName.Anatomy; }
			else if ( force == 3 ){ skill = SkillName.AnimalLore; }
			else if ( force == 4 ){ skill = SkillName.AnimalTaming; }
			else if ( force == 5 ){ skill = SkillName.Archery; }
			else if ( force == 6 ){ skill = SkillName.ArmsLore; }
			else if ( force == 7 ){ skill = SkillName.Begging; }
			else if ( force == 8 ){ skill = SkillName.Blacksmith; }
			else if ( force == 9 ){ skill = SkillName.Bushido; }
			else if ( force == 10 ){ skill = SkillName.Camping; }
			else if ( force == 11 ){ skill = SkillName.Carpentry; }
			else if ( force == 12 ){ skill = SkillName.Cartography; }
			else if ( force == 13 ){ skill = SkillName.Chivalry; }
			else if ( force == 14 ){ skill = SkillName.Cooking; }
			else if ( force == 15 ){ skill = SkillName.DetectHidden; }
			else if ( force == 16 ){ skill = SkillName.Discordance; }
			else if ( force == 17 ){ skill = SkillName.EvalInt; }
			else if ( force == 18 ){ skill = SkillName.Fencing; }
			else if ( force == 19 ){ skill = SkillName.Fishing; }
			else if ( force == 20 ){ skill = SkillName.Fletching; }
			else if ( force == 21 ){ skill = SkillName.Focus; }
			else if ( force == 22 ){ skill = SkillName.Forensics; }
			else if ( force == 23 ){ skill = SkillName.Healing; }
			else if ( force == 24 ){ skill = SkillName.Herding; }
			else if ( force == 25 ){ skill = SkillName.Hiding; }
			else if ( force == 26 ){ skill = SkillName.Inscribe; }
			else if ( force == 27 ){ skill = SkillName.ItemID; }
			else if ( force == 28 ){ skill = SkillName.Lockpicking; }
			else if ( force == 29 ){ skill = SkillName.Lumberjacking; }
			else if ( force == 30 ){ skill = SkillName.Macing; }
			else if ( force == 31 ){ skill = SkillName.Magery; }
			else if ( force == 32 ){ skill = SkillName.MagicResist; }
			else if ( force == 33 ){ skill = SkillName.Meditation; }
			else if ( force == 34 ){ skill = SkillName.Mining; }
			else if ( force == 35 ){ skill = SkillName.Musicianship; }
			else if ( force == 36 ){ skill = SkillName.Necromancy; }
			else if ( force == 37 ){ skill = SkillName.Ninjitsu; }
			else if ( force == 38 ){ skill = SkillName.Parry; }
			else if ( force == 39 ){ skill = SkillName.Peacemaking; }
			else if ( force == 40 ){ skill = SkillName.Poisoning; }
			else if ( force == 41 ){ skill = SkillName.Provocation; }
			else if ( force == 42 ){ skill = SkillName.RemoveTrap; }
			else if ( force == 43 ){ skill = SkillName.Snooping; }
			else if ( force == 44 ){ skill = SkillName.SpiritSpeak; }
			else if ( force == 45 ){ skill = SkillName.Stealing; }
			else if ( force == 46 ){ skill = SkillName.Stealth; }
			else if ( force == 47 ){ skill = SkillName.Swords; }
			else if ( force == 48 ){ skill = SkillName.Tactics; }
			else if ( force == 49 ){ skill = SkillName.Tailoring; }
			else if ( force == 50 ){ skill = SkillName.TasteID; }
			else if ( force == 51 ){ skill = SkillName.Tinkering; }
			else if ( force == 52 ){ skill = SkillName.Tracking; }
			else if ( force == 53 ){ skill = SkillName.Veterinary; }
			else if ( force == 54 ){ skill = SkillName.Wrestling; }

			return skill;
		}

		public static string AddSpacesToSentence(string text)
		{
			StringBuilder newText = new StringBuilder(text.Length * 2);
			newText.Append(text[0]);
			for (int i = 1; i < text.Length; i++)
			{
				if (char.IsUpper(text[i]) && text[i - 1] != ' ')
					newText.Append(' ');
				newText.Append(text[i]);
			}
			return newText.ToString();
		}
	}
}