using System;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace ItemNameHue
{
    public class ArmorItemProps
    {
		#region Armor
        public static string GetArmorItemValue(Item item)
        {
				bool useartifactrarity = false;
		
				BaseArmor ba = item as BaseArmor;
				int values = CheckArmor(ba);
				int rarityvalues = CheckArtifactArmor(ba);
				
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
		
		public static int CheckArtifactArmor(BaseArmor item)
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

        public static int CheckArmor(BaseArmor item)
        {
            int value = 0;

            foreach (int i in Enum.GetValues(typeof(AosAttribute)))
            {
                if (item != null && item.Attributes[(AosAttribute)i] > 0)
                    value += 2;
            }
			
			foreach (int i in Enum.GetValues(typeof(AosArmorAttribute)))
            {
                if (item.ArmorAttributes[(AosArmorAttribute)i] > 0)
                    value += 2;
            }

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

            //Start armor attributes

            if (item.ArmorAttributes.DurabilityBonus > 0)
                value += item.ArmorAttributes.DurabilityBonus / 4;

            if (item.ArmorAttributes.LowerStatReq > 0)
                value += item.ArmorAttributes.LowerStatReq / 4;

            if (item.ArmorAttributes.MageArmor > 0)
                value += 10;

            if (item.ArmorAttributes.SelfRepair > 0)
                value += item.ArmorAttributes.SelfRepair * 2;

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

            //Start Resist Bonus

            if (item.ColdBonus > 0)
            {
                value += item.ColdBonus * 2;
                value += 2;
            }

            if (item.EnergyBonus > 0)
            {
                value += item.EnergyBonus * 2;
                value += 2;
            }

            if (item.FireBonus > 0)
            {
                value += item.FireBonus * 2;
                value += 2;
            }

            if (item.PhysicalBonus > 0)
            {
                value += item.PhysicalBonus * 2;
                value += 2;
            }

            if (item.PoisonBonus > 0)
            {
                value += item.PoisonBonus * 2;
                value += 2;
            }
				
			return value;
        }
		#endregion
		
		public static string RarityNameMod(Item item, string orig)
        {
            return (string)(GetArmorItemValue(item) + orig + "<BASEFONT COLOR=#FFFFFF>");
        }

    }
}
