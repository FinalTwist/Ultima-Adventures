using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;

namespace Server.Misc
{
    class MorphingTemplates
    {
		public static string TemplateMurk( string s ) //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "5";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "10";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "20";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "0";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + "1072";		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "50";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "50";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "20";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "4";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "5";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "5,99";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "5";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "5";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "5";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "0";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "0";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "0";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "20";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "5";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "10";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "5,38";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + "1072";		// INT ONLY
			}
			else if ( s == "misc" )
			{
				/* ENTRY 1 FOR Resistances.Physical */ 					powers = 				"5";
				/* ENTRY 2 FOR Resistances.Fire */ 						powers = powers + "," + "0";
				/* ENTRY 3 FOR Resistances.Cold */ 						powers = powers + "," + "5";
				/* ENTRY 4 FOR Resistances.Poison */ 					powers = powers + "," + "0";
				/* ENTRY 5 FOR Resistances.Energy */ 					powers = powers + "," + "0";
				/* ENTRY 6 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 7 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 8 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 9 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 10 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 11 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 12 FOR Attributes.BonusStam */ 				powers = powers + "," + "5";
				/* ENTRY 13 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 14 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "5";
				/* ENTRY 17 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 18 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 21 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 28,29 FOR SkillBonuses 1 */ 					powers = powers + "," + "5,19";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 30,31 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 32,33 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 34,35 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 36,37 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 38 FOR Hue */ 									powers = powers + "," + "1072";		// INT ONLY
			}

			return powers;
		}

		public static string TemplateCaddellite( string s ) /////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "2";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "5";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "10";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "30";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "10";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "0";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "caddellite", "", 0 ) ).ToString();		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "50";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "50";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "27";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "10,99";	// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "10,32";	// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "2";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "8";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "3";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "8";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "3";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "3";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "5,32";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "caddellite", "", 0 ) ).ToString();		// INT ONLY

			}

			return powers;
		}

		public static string TemplateStarRuby( string s ) ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"10";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "90";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ).ToString();		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "3";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "0";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "5";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "3";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "5";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "4";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "10";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"10";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "90";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "5";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "0";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "0";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "5";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "0";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "3";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "2";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "3";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "3";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "10";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ) ).ToString();		// INT ONLY
			}

			return powers;
		}

		public static string TemplateSpinel( string s ) /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "80";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ).ToString();		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "3";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "0";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "5";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "5";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "5";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "1";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "80";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "0";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "0";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "0";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "0";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "0";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "3";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "5";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "5";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "5";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ) ).ToString();		// INT ONLY
			}

			return powers;
		}

		public static string TemplateSilver( string s ) /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "90";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ).ToString();		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "0";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "5";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "5";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "5";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "10";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "20";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "1";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "90";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "5";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "2";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "4";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "3";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "2";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "3";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "2";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "2";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "5";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "20";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ) ).ToString();		// INT ONLY
			}

			return powers;
		}

		public static string TemplateRuby( string s ) ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "20";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "80";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ).ToString();		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "40";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "60";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "3";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "4";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "10";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "80";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "3";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "0";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "0";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "8";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "0";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "3";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "2";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "10";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ) ).ToString();		// INT ONLY
			}

			return powers;
		}

		public static string TemplateQuartz( string s ) /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"20";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "80";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ).ToString();		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "2";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "0";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "4";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "5";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "7";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"20";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "80";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "5";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "3";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "2";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "2";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "2";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "2";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "2";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "2";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "5";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ) ).ToString();		// INT ONLY
			}

			return powers;
		}

		public static string TemplateOnyx( string s ) ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"40";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "5";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "80";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ).ToString();		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "25";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "25";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "25";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "25";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "25";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "3";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "5";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "2";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "2";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "5";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "5";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "1";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "1";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "5";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "5";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "30";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "5,31";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "5,36";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"40";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "1";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "80";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "0";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "0";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "0";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "0";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "0";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "2";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "5";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "2";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "1";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "5";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "5";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "3";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "3";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "30";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "5,31";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "5,36";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ) ).ToString();		// INT ONLY
			}

			return powers;
		}

		public static string TemplateMarble( string s ) /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "100";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ).ToString();		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "0";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "10";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "5";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "10";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "23";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "32";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "10,99";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "100";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "8";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "1";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "1";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "1";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "1";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "20";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "3";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "5";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "5";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ) ).ToString();		// INT ONLY
			}

			return powers;
		}

		public static string TemplateGarnet( string s ) /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "80";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ).ToString();		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "0";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "5";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "4";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "5";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "1";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "1";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "5";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "5";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "5";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "1";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "80";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "5";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "3";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "3";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "3";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "3";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "3";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "2";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "5";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "1";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "1";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "5";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "5";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "5";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ) ).ToString();		// INT ONLY
			}

			return powers;
		}

		public static string TemplateIce( string s ) ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "25";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "5";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "30";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ).ToString();		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "50";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "50";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "8";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "2";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "10";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "19";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "30";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "5";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "8";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "0";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "0";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "0";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "5";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "2";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "10";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ) ).ToString();		// INT ONLY
			}

			return powers;
		}

		public static string TemplateJade( string s ) ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"40";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "80";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ).ToString();		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "0";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "2";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "5";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "5";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "40";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "10,99";	// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "10,9";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"40";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "80";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "8";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "0";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "0";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "0";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "0";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "5";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "2";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "3";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "30";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "5,9";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ) ).ToString();		// INT ONLY
			}

			return powers;
		}

		public static string TemplateTopaz( string s ) //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"10";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "2";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "80";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ).ToString();		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "0";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "8";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "8";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "5";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"10";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "2";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "80";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "0";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "3";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "3";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "3";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "3";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "4";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "4";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "3";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ) ).ToString();		// INT ONLY
			}

			return powers;
		}

		public static string TemplateAmethyst( string s ) ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "80";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "3";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "3";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "3";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "3";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "3";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "100";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ).ToString();		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "0";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "5";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "10";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "10";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "10,38";	// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "10,48";	// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "100";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "5";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "5";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "5";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "5";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "5";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "5";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "10";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "10,38";	// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "10,48";	// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ) ).ToString();		// INT ONLY
			}

			return powers;
		}

		public static string TemplateEmerald( string s ) ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "80";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ).ToString();		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "0";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "2";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "2";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "2";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "2";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "2";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "2";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "10";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "80";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "8";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "4";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "4";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "4";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "4";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "2";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "2";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "2";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "2";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "2";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "2";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "8";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ) ).ToString();		// INT ONLY
			}

			return powers;
		}

		public static string TemplateSapphire( string s ) ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "80";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ).ToString();		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "0";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "5";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "8";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "5";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "1";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "4,17";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "4,31";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "4,32";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "4,33";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}

			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "1";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "80";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "10";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "5";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "5";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "5";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "5";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "5";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "4";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "5";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "1";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "3,17";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "3,31";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "3,32";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "3,33";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + ( Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ) ).ToString();		// INT ONLY
			}

			return powers;
		}

		public static string TemplateBlackKnight( string s ) ////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "90";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + "1175";		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "0";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "2";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "2";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "2";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "2";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "5";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "20";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "90";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "4";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "1";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "3";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "2";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "0";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "2";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "2";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "2";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "5";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "20";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + "1175";		// INT ONLY
			}

			return powers;
		}

		public static string TemplateGrundulVarg( string s ) ////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "10";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "10";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "90";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + "1167";		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "3";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "3";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "60";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "10";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "10";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "10";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "10";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "4";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "4";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "5";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "50";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "5";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "15,99";	// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "50";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "4";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "1";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "3";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "2";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "1";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "2";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "3";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "2";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "25";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + "1167";		// INT ONLY
			}

			return powers;
		}

		public static string TemplateKull( string s ) //////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "50";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "50";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "5";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "90";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + "1435";		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "3";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "3";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "3";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "60";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "10";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "10";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "10";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "10";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "5";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "10";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "4";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "100";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "6";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "15,99";	// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "50";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "4";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "1";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "1";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "8";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "1";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "2";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "3";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "3";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "1";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "50";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + "1435";		// INT ONLY
			}

			return powers;
		}

		public static string TemplateLostKnight( string s ) ////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "2";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "5";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "40";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + "1150";		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "3";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "10";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "10";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "35";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "10";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "35";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "4";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "20,99";	// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "2";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "40";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "6";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "8";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "8";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "5";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "5";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "2";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "2";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + "1150";		// INT ONLY
			}

			return powers;
		}

		public static string TemplateVordo( string s ) //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "50";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "0";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + "1194";		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "3";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "3";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "50";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "50";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "10";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "5";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "5";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "5";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "5,36";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "5,22";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "5,44";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"20";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "1";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "5";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "0";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "0";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "0";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "10";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "10";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "5";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "5";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "5,36";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "5,22";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "5,44";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + "1194";		// INT ONLY
			}
			else if ( s == "misc" )
			{
				/* ENTRY 1 FOR Resistances.Physical */ 					powers = 				"0";
				/* ENTRY 2 FOR Resistances.Fire */ 						powers = powers + "," + "0";
				/* ENTRY 3 FOR Resistances.Cold */ 						powers = powers + "," + "0";
				/* ENTRY 4 FOR Resistances.Poison */ 					powers = powers + "," + "5";
				/* ENTRY 5 FOR Resistances.Energy */ 					powers = powers + "," + "0";
				/* ENTRY 6 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 7 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 8 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 9 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 10 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 11 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 12 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 13 FOR Attributes.BonusMana */ 				powers = powers + "," + "10";
				/* ENTRY 14 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "5";
				/* ENTRY 18 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "5";
				/* ENTRY 19 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 21 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 28,29 FOR SkillBonuses 1 */ 					powers = powers + "," + "5,36";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 30,31 FOR SkillBonuses 2 */ 					powers = powers + "," + "5,22";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 32,33 FOR SkillBonuses 3 */ 					powers = powers + "," + "5,44";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 34,35 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 36,37 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 38 FOR Hue */ 									powers = powers + "," + "1194";		// INT ONLY
			}

			return powers;
		}
		public static string TemplateRanger( string s ) //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "0";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + "1281";		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "100";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "10";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "300";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "28";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "30";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "10,99";	// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "0";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "0";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "0";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "0";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "0";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "5";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "200";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "5,48";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + "1281";		// INT ONLY
			}

			return powers;
		}

		public static string TemplateDracolich( string s ) //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "5";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "50";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "20";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "25";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "5";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "0";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + "1150";		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "70";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "30";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "10,99";	// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "0";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "0";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "0";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "0";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "5";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "6";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "2";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + "1150";		// INT ONLY
			}
			else if ( s == "misc" )
			{
				/* ENTRY 1 FOR Resistances.Physical */ 					powers = 				"0";
				/* ENTRY 2 FOR Resistances.Fire */ 						powers = powers + "," + "0";
				/* ENTRY 3 FOR Resistances.Cold */ 						powers = powers + "," + "0";
				/* ENTRY 4 FOR Resistances.Poison */ 					powers = powers + "," + "5";
				/* ENTRY 5 FOR Resistances.Energy */ 					powers = powers + "," + "0";
				/* ENTRY 6 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 7 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 8 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 9 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 10 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 11 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 12 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 13 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 14 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 18 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 21 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.RegenHits */ 				powers = powers + "," + "2";
				/* ENTRY 23 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.SpellDamage */ 				powers = powers + "," + "5";
				/* ENTRY 26 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 28,29 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 30,31 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 32,33 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 34,35 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 36,37 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 38 FOR Hue */ 									powers = powers + "," + "1150";		// INT ONLY
			}

			return powers;
		}

		public static string TemplatePearlJewelry( string s ) ///////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "misc" )
			{
				/* ENTRY 1 FOR Resistances.Physical */ 					powers = 				"0";
				/* ENTRY 2 FOR Resistances.Fire */ 						powers = powers + "," + "0";
				/* ENTRY 3 FOR Resistances.Cold */ 						powers = powers + "," + "0";
				/* ENTRY 4 FOR Resistances.Poison */ 					powers = powers + "," + "0";
				/* ENTRY 5 FOR Resistances.Energy */ 					powers = powers + "," + "0";
				/* ENTRY 6 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 7 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 8 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 9 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 10 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 11 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 12 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 13 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 14 FOR Attributes.CastRecovery */ 				powers = powers + "," + "1";
				/* ENTRY 15 FOR Attributes.CastSpeed */ 				powers = powers + "," + "01";
				/* ENTRY 16 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "10";
				/* ENTRY 18 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "10";
				/* ENTRY 19 FOR Attributes.Luck */ 						powers = powers + "," + "50";
				/* ENTRY 20 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 21 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.SpellDamage */ 				powers = powers + "," + "5";
				/* ENTRY 26 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 28,29 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 30,31 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 32,33 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 34,35 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 36,37 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 38 FOR Hue */ 									powers = powers + "," + "1150";		// INT ONLY
			}

			return powers;
		}

		public static string TemplateIceDemon( string s ) //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "0";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "0";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + "1152";		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "0";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "2";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "2";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "50";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "50";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "3";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "5,99";		// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "0";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "0";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "10";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "0";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "0";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "0";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "2";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "2";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "15";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + "1152";		// INT ONLY
			}
			else if ( s == "misc" )
			{
				/* ENTRY 1 FOR Resistances.Physical */ 					powers = 				"0";
				/* ENTRY 2 FOR Resistances.Fire */ 						powers = powers + "," + "0";
				/* ENTRY 3 FOR Resistances.Cold */ 						powers = powers + "," + "10";
				/* ENTRY 4 FOR Resistances.Poison */ 					powers = powers + "," + "0";
				/* ENTRY 5 FOR Resistances.Energy */ 					powers = powers + "," + "0";
				/* ENTRY 6 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 7 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 8 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 9 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 10 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 11 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 12 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 13 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 14 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 18 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.NightSight */ 				powers = powers + "," + "1";		// VALUE OF 1
				/* ENTRY 21 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "10";
				/* ENTRY 22 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 28,29 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 30,31 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 32,33 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 34,35 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 36,37 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 38 FOR Hue */ 									powers = powers + "," + "1152";		// INT ONLY
			}

			return powers;
		}

		public static string TemplateSpaceAce( string s ) /////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			string powers = "";

			int lightArmor = Utility.RandomList( 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 );

			if ( s == "weapons" )
			{
				/* ENTRY 1 FOR WeaponAttributes.LowerStatReq */ 		powers = 				"0";
				/* ENTRY 2 FOR WeaponAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR WeaponAttributes.HitLeechHits */ 		powers = powers + "," + "0";
				/* ENTRY 4 FOR WeaponAttributes.HitLeechStam */ 		powers = powers + "," + "0";
				/* ENTRY 5 FOR WeaponAttributes.HitLeechMana */ 		powers = powers + "," + "0";
				/* ENTRY 6 FOR WeaponAttributes.HitLowerAttack */ 		powers = powers + "," + "0";
				/* ENTRY 7 FOR WeaponAttributes.HitLowerDefend */ 		powers = powers + "," + "0";
				/* ENTRY 8 FOR WeaponAttributes.HitMagicArrow */ 		powers = powers + "," + "0";
				/* ENTRY 9 FOR WeaponAttributes.HitHarm */ 				powers = powers + "," + "0";
				/* ENTRY 10 FOR WeaponAttributes.HitFireball */ 		powers = powers + "," + "0";
				/* ENTRY 11 FOR WeaponAttributes.HitLightning */ 		powers = powers + "," + "0";
				/* ENTRY 12 FOR WeaponAttributes.HitDispel */ 			powers = powers + "," + "0";
				/* ENTRY 13 FOR WeaponAttributes.HitColdArea */ 		powers = powers + "," + "0";
				/* ENTRY 14 FOR WeaponAttributes.HitFireArea */ 		powers = powers + "," + "0";
				/* ENTRY 15 FOR WeaponAttributes.HitPoisonArea */ 		powers = powers + "," + "0";
				/* ENTRY 16 FOR WeaponAttributes.HitEnergyArea */ 		powers = powers + "," + "0";
				/* ENTRY 17 FOR WeaponAttributes.HitPhysicalArea */ 	powers = powers + "," + "0";
				/* ENTRY 18 FOR WeaponAttributes.ResistPhysicalBonus */ powers = powers + "," + "0";
				/* ENTRY 19 FOR WeaponAttributes.ResistFireBonus */ 	powers = powers + "," + "0";
				/* ENTRY 20 FOR WeaponAttributes.ResistColdBonus */ 	powers = powers + "," + "0";
				/* ENTRY 21 FOR WeaponAttributes.ResistPoisonBonus */ 	powers = powers + "," + "0";
				/* ENTRY 22 FOR WeaponAttributes.ResistEnergyBonus */ 	powers = powers + "," + "" + Utility.RandomMinMax( 0, 10 ).ToString() + "";
				/* ENTRY 23 FOR WeaponAttributes.UseBestSkill */ 		powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 24 FOR WeaponAttributes.DurabilityBonus */ 	powers = powers + "," + "" + Utility.RandomMinMax( 10, 80 ).ToString() + "";
				/* ENTRY 25 FOR Hue */ 									powers = powers + "," + "0";		// INT ONLY
				/* ENTRY 26 FOR AccuracyLevel */ 						powers = powers + "," + "" + Utility.RandomMinMax( 1, 6 ).ToString() + "";		// VALUE OF 1-6
				/* ENTRY 27 FOR DamageLevel */ 							powers = powers + "," + "" + Utility.RandomMinMax( 1, 6 ).ToString() + "";		// VALUE OF 1-6
				/* ENTRY 28 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 29 FOR DurabilityLevel */ 						powers = powers + "," + "" + Utility.RandomMinMax( 1, 6 ).ToString() + "";		// VALUE OF 1-6
				/* ENTRY 30 FOR AosElementDamages.Physical */ 			powers = powers + "," + "100";		// 30-34 SHOULD TOTAL 100
				/* ENTRY 31 FOR AosElementDamages.Fire */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR AosElementDamages.Cold */ 				powers = powers + "," + "0";
				/* ENTRY 33 FOR AosElementDamages.Poison */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR AosElementDamages.Energy */ 			powers = powers + "," + "0";
				/* ENTRY 35 FOR Extra Hit Points */ 					powers = powers + "," + "0";
				/* ENTRY 36 FOR Extra Damage */ 						powers = powers + "," + "0";
				/* ENTRY 37 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 38 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 39 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 40 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 41 FOR Attributes.AttackChance */ 				powers = powers + "," + "" + ( Utility.RandomMinMax( 0, 5 ) * 10 ).ToString() + "";
				/* ENTRY 42 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 43 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 44 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 45 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 46 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 47 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 48 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "" + ( Utility.RandomMinMax( 0, 5 ) * 10 ).ToString() + "";
				/* ENTRY 49 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 50 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 51 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 52 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 53 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 54 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 55 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 56 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 57 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 58 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 59 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 60 FOR Slayer */ 								powers = powers + "," + "0";
				/* ENTRY 61 FOR Slayer2 */ 								powers = powers + "," + "0";
				/* ENTRY 62,63 FOR SkillBonuses 1 */ 					powers = powers + "," + "" + Utility.RandomMinMax( 0, 10 ).ToString() + ",99";	// SKILL LEVEL FIRST...SKILL NAME SECOND...99 FOR WEAPON SKILL
				/* ENTRY 64,65 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 66,67 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 68,69 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 70,71 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
			}
			else if ( s == "armors" )
			{
				/* ENTRY 1 FOR ArmorAttributes.LowerStatReq */ 			powers = 				"0";
				/* ENTRY 2 FOR ArmorAttributes.SelfRepair */ 			powers = powers + "," + "0";
				/* ENTRY 3 FOR ArmorAttributes.MageArmor */ 			powers = powers + "," + "" + lightArmor.ToString() + "";
				/* ENTRY 4 FOR ArmorAttributes.DurabilityBonus */ 		powers = powers + "," + "" + Utility.RandomMinMax( 10, 80 ).ToString() + "";
				/* ENTRY 5 FOR PhysicalBonus */ 						powers = powers + "," + "" + Utility.RandomMinMax( 0, 6 ).ToString() + "";
				/* ENTRY 6 FOR ColdBonus */ 							powers = powers + "," + "" + Utility.RandomMinMax( 0, 6 ).ToString() + "";
				/* ENTRY 7 FOR EnergyBonus */ 							powers = powers + "," + "" + Utility.RandomMinMax( 0, 6 ).ToString() + "";
				/* ENTRY 8 FOR FireBonus */ 							powers = powers + "," + "" + Utility.RandomMinMax( 0, 6 ).ToString() + "";
				/* ENTRY 9 FOR PoisonBonus */ 							powers = powers + "," + "" + Utility.RandomMinMax( 0, 6 ).ToString() + "";
				/* ENTRY 10 FOR Extra HitPoints */ 						powers = powers + "," + "0";
				/* ENTRY 11 FOR Durability */ 							powers = powers + "," + "" + Utility.RandomMinMax( 1, 6 ).ToString() + "";		// VALUE OF 1-6
				/* ENTRY 12 FOR Quality */ 								powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 13 FOR ProtectionLevel */ 						powers = powers + "," + "" + Utility.RandomMinMax( 1, 6 ).ToString() + "";		// VALUE OF 1-6
				/* ENTRY 14 FOR Attributes.RegenHits */ 				powers = powers + "," + "0";
				/* ENTRY 15 FOR Attributes.RegenStam */ 				powers = powers + "," + "0";
				/* ENTRY 16 FOR Attributes.RegenMana */ 				powers = powers + "," + "0";
				/* ENTRY 17 FOR Attributes.DefendChance */ 				powers = powers + "," + "0";
				/* ENTRY 18 FOR Attributes.AttackChance */ 				powers = powers + "," + "0";
				/* ENTRY 19 FOR Attributes.BonusStr */ 					powers = powers + "," + "0";
				/* ENTRY 20 FOR Attributes.BonusDex */ 					powers = powers + "," + "0";
				/* ENTRY 21 FOR Attributes.BonusInt */ 					powers = powers + "," + "0";
				/* ENTRY 22 FOR Attributes.BonusHits */ 				powers = powers + "," + "0";
				/* ENTRY 23 FOR Attributes.BonusStam */ 				powers = powers + "," + "0";
				/* ENTRY 24 FOR Attributes.BonusMana */ 				powers = powers + "," + "0";
				/* ENTRY 25 FOR Attributes.WeaponDamage */ 				powers = powers + "," + "0";
				/* ENTRY 26 FOR Attributes.WeaponSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 27 FOR Attributes.SpellDamage */ 				powers = powers + "," + "0";
				/* ENTRY 28 FOR Attributes.CastRecovery */ 				powers = powers + "," + "0";
				/* ENTRY 29 FOR Attributes.CastSpeed */ 				powers = powers + "," + "0";
				/* ENTRY 30 FOR Attributes.LowerManaCost */ 			powers = powers + "," + "0";
				/* ENTRY 31 FOR Attributes.LowerRegCost */ 				powers = powers + "," + "0";
				/* ENTRY 32 FOR Attributes.ReflectPhysical */ 			powers = powers + "," + "0";
				/* ENTRY 33 FOR Attributes.EnhancePotions */ 			powers = powers + "," + "0";
				/* ENTRY 34 FOR Attributes.Luck */ 						powers = powers + "," + "0";
				/* ENTRY 35 FOR Attributes.SpellChanneling */ 			powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 36 FOR Attributes.NightSight */ 				powers = powers + "," + "0";		// VALUE OF 1
				/* ENTRY 37,38 FOR SkillBonuses 1 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 39,40 FOR SkillBonuses 2 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 41,42 FOR SkillBonuses 3 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 43,44 FOR SkillBonuses 4 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 45,46 FOR SkillBonuses 5 */ 					powers = powers + "," + "0,0";		// SKILL LEVEL FIRST...SKILL NAME SECOND
				/* ENTRY 47 FOR Hue */ 									powers = powers + "," + "0";		// INT ONLY

			}

			return powers;
		}
	}
}