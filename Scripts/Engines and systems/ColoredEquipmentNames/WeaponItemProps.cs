using System;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace ItemNameHue
{
    public class WeaponItemProps
    {
		#region Weapon
        public static string GetWeaponItemValue(Item item)
        {
				bool useartifactrarity = false;
		
				BaseWeapon bw = item as BaseWeapon;
				int values = CheckWeapon(bw); 
				int rarityvalues = CheckArtifactWeapon(bw);
				
				if (useartifactrarity && rarityvalues >= 1)
				{
				if (rarityvalues >= 4)
                    return "<BASEFONT COLOR=#FFFF00>";
                else if (rarityvalues >= 3)
                    return "<BASEFONT COLOR=#00FFFF>";
                else if (rarityvalues >= 2)
                    return "<BASEFONT COLOR=#00FF00>";
                else if (rarityvalues >= 1)
                    return "<BASEFONT COLOR=#FFFFFF>";
					
					return "<BASEFONT COLOR=#D6D6D6>";
				}
				else
				{
				if (values >= 300)
					return "<BASEFONT COLOR=#FF944D>";
				else if (values >= 200)
					return "<BASEFONT COLOR=#B48FFF>";
				else if (values >= 100)
					return "<BASEFONT COLOR=#8DBAE8>";
				else if (values >= 50)
					return "<BASEFONT COLOR=#A1E68A>";

					return "<BASEFONT COLOR=#D6D6D6>";
				}
        }
		
		public static int CheckArtifactWeapon(BaseWeapon item)
		{
				int rarityvalue = 0;
		
				if (item.ArtifactRarity <= 30)
                    rarityvalue = 4;

                if (item.ArtifactRarity <= 20)
                    rarityvalue = 3;

                if (item.ArtifactRarity <= 10)
                    rarityvalue = 2;

                if (item.ArtifactRarity <= 1)
                    rarityvalue = 1;
				
				if (item.ArtifactRarity < 1)
                    rarityvalue = 0;
					
				return rarityvalue;		
		}

        public static int CheckWeapon(BaseWeapon item)
        {
            int value = 0;

            foreach (int i in Enum.GetValues(typeof(AosAttribute)))
            {
                if (item != null && item.Attributes[(AosAttribute)i] > 0)
                    value += 2;
            }

            foreach (int i in Enum.GetValues(typeof(AosWeaponAttribute)))
            {
                if (item.WeaponAttributes[(AosWeaponAttribute)i] > 0)
                    value += 2;
            }

            //Start skill bonus

            if (item.SkillBonuses.Skill_1_Value > 0)
            {
                value += (int)item.SkillBonuses.Skill_1_Value * 4;
                value += 2;
            }

            if (item.SkillBonuses.Skill_2_Value > 0)
            {
                value += (int)item.SkillBonuses.Skill_2_Value * 4;
                value += 2;
            }

            if (item.SkillBonuses.Skill_3_Value > 0)
            {
                value += (int)item.SkillBonuses.Skill_3_Value * 4;
                value += 2;
            }

            if (item.SkillBonuses.Skill_4_Value > 0)
            {
                value += (int)item.SkillBonuses.Skill_4_Value * 4;
                value += 2;
            }

            if (item.SkillBonuses.Skill_5_Value > 0)
            {
                value += (int)item.SkillBonuses.Skill_5_Value * 4;
                value += 2;
            }

            //Start Slayers

            if (item.Slayer != SlayerName.None)
                value += 20;

            if (item.Slayer2 != SlayerName.None)
                value += 20;

            //Start weapon attributes

            if (item.WeaponAttributes.DurabilityBonus > 0)
                value += item.WeaponAttributes.DurabilityBonus / 4;

            if (item.WeaponAttributes.HitColdArea > 0)
                value += item.WeaponAttributes.HitColdArea / 2;

            if (item.WeaponAttributes.HitDispel > 0)
                value += item.WeaponAttributes.HitDispel / 2;

            if (item.WeaponAttributes.HitEnergyArea > 0)
                value += item.WeaponAttributes.HitEnergyArea / 2;

            if (item.WeaponAttributes.HitFireArea > 0)
                value += item.WeaponAttributes.HitFireArea / 2;

            if (item.WeaponAttributes.HitFireball > 0)
                value += item.WeaponAttributes.HitFireball / 2;

            if (item.WeaponAttributes.HitHarm > 0)
                value += item.WeaponAttributes.HitHarm / 2;

            if (item.WeaponAttributes.HitLeechHits > 0)
                value += item.WeaponAttributes.HitLeechHits / 2;

            if (item.WeaponAttributes.HitLeechMana > 0)
                value += item.WeaponAttributes.HitLeechMana / 2;

            if (item.WeaponAttributes.HitLeechStam > 0)
                value += item.WeaponAttributes.HitLeechStam / 2;

            if (item.WeaponAttributes.HitLightning > 0)
                value += item.WeaponAttributes.HitLightning / 2;

            if (item.WeaponAttributes.HitLowerAttack > 0)
                value += item.WeaponAttributes.HitLowerAttack / 2;

            if (item.WeaponAttributes.HitLowerDefend > 0)
                value += item.WeaponAttributes.HitLowerDefend / 2;

            if (item.WeaponAttributes.HitMagicArrow > 0)
                value += item.WeaponAttributes.HitMagicArrow / 2;

            if (item.WeaponAttributes.HitPhysicalArea > 0)
                value += item.WeaponAttributes.HitPhysicalArea / 2;

            if (item.WeaponAttributes.HitPoisonArea > 0)
                value += item.WeaponAttributes.HitPoisonArea / 2;

            if (item.WeaponAttributes.LowerStatReq > 0)
                value += item.WeaponAttributes.LowerStatReq / 2;

            if (item.WeaponAttributes.MageWeapon > 0)
                value += item.WeaponAttributes.MageWeapon;

            if (item.WeaponAttributes.ResistColdBonus > 0)
                value += item.WeaponAttributes.ResistColdBonus / 2;

            if (item.WeaponAttributes.ResistEnergyBonus > 0)
                value += item.WeaponAttributes.ResistEnergyBonus / 2;

            if (item.WeaponAttributes.ResistFireBonus > 0)
                value += item.WeaponAttributes.ResistFireBonus / 2;

            if (item.WeaponAttributes.ResistPhysicalBonus > 0)
                value += item.WeaponAttributes.ResistPhysicalBonus / 2;

            if (item.WeaponAttributes.ResistPoisonBonus > 0)
                value += item.WeaponAttributes.ResistPoisonBonus / 2;

            if (item.WeaponAttributes.SelfRepair > 0)
                value += item.WeaponAttributes.SelfRepair * 2;

            if (item.WeaponAttributes.UseBestSkill > 0)
                value += 10;

            //Start standard attributes

            if (item.Attributes.AttackChance > 0)
                value += item.Attributes.AttackChance * 2;

            if (item.Attributes.BonusDex > 0)
                value += item.Attributes.BonusDex * 4;

            if (item.Attributes.BonusHits > 0)
                value += item.Attributes.BonusHits * 2;

            if (item.Attributes.BonusInt > 0)
                value += item.Attributes.BonusInt * 4;

            if (item.Attributes.BonusMana > 0)
                value += item.Attributes.BonusMana * 2;

            if (item.Attributes.BonusStam > 0)
                value += item.Attributes.BonusStam * 2;

            if (item.Attributes.BonusStr > 0)
                value += item.Attributes.BonusStr * 4;

            if (item.Attributes.CastRecovery > 0)
                value += item.Attributes.CastRecovery * 10;

            if (item.Attributes.CastSpeed > 0)
                value += item.Attributes.CastSpeed * 10;

            if (item.Attributes.DefendChance > 0)
                value += item.Attributes.DefendChance * 2;

            if (item.Attributes.EnhancePotions > 0)
                value += item.Attributes.EnhancePotions;

            if (item.Attributes.LowerManaCost > 0)
                value += item.Attributes.LowerManaCost * 2;

            if (item.Attributes.LowerRegCost > 0)
                value += item.Attributes.LowerRegCost;

            if (item.Attributes.Luck > 0)
                value += item.Attributes.Luck / 2;

            if (item.Attributes.NightSight > 0)
                value += 10;

            if (item.Attributes.ReflectPhysical > 0)
                value += item.Attributes.ReflectPhysical * 2;

            if (item.Attributes.RegenHits > 0)
                value += item.Attributes.RegenHits * 10;

            if (item.Attributes.RegenMana > 0)
                value += item.Attributes.RegenMana * 10;

            if (item.Attributes.RegenStam > 0)
                value += item.Attributes.RegenStam * 10;

            if (item.Attributes.SpellChanneling > 0)
                value += 10;

            if (item.Attributes.SpellDamage > 0)
                value += item.Attributes.SpellDamage * 2;

            if (item.Attributes.WeaponDamage > 0)
                value += item.Attributes.WeaponDamage * 2;

            if (item.Attributes.WeaponSpeed > 0)
                value += item.Attributes.WeaponSpeed * 2;

            //Start Element Damage

            if (item.AosElementDamages.Chaos > 0)
                value += item.AosElementDamages.Chaos / 2;

            if (item.AosElementDamages.Cold > 0)
                value += item.AosElementDamages.Cold / 2;

            if (item.AosElementDamages.Direct > 0)
                value += item.AosElementDamages.Direct / 2;

            if (item.AosElementDamages.Energy > 0)
                value += item.AosElementDamages.Energy / 2;

            if (item.AosElementDamages.Fire > 0)
                value += item.AosElementDamages.Fire / 2;

            if (item.AosElementDamages.Poison > 0)
                value += item.AosElementDamages.Poison / 2;
				
			return value;
        }
		#endregion
		
		public static string RarityNameMod(Item item, string orig)
        {
            return (string)(GetWeaponItemValue(item) + orig + "<BASEFONT COLOR=#FFFFFF>");
        }

    }
}
